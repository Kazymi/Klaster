using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject decal;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsOwner)
        {
            SpawnDecalServerRpc();
            var playerHealthController = other.GetComponent<PlayerHealthController>();
            if (playerHealthController)
            {
                HitServerRpc(playerHealthController.GetComponent<NetworkObject>().NetworkObjectId, damage);
                Debug.Log("Send hit server rpc");
            }
        }

        gameObject.SetActive(false);
    }

    [ServerRpc]
    private void HitServerRpc(ulong NetWorkId, int damage)
    {
        Debug.Log("take hit server rpc");
        Debug.Log("send hit client rpc");
        HitClientRpc(NetWorkId, damage);
    }

    [ClientRpc]
    private void HitClientRpc(ulong NetWorkId, int damage)
    {
        Debug.Log("take hit client rpc");
        NetworkManager.SpawnManager.SpawnedObjects.TryGetValue(NetWorkId, out var networkObject);
        Debug.Log($"found network {networkObject}");
        networkObject.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);
    }

    [ServerRpc]
    public void SpawnDecalServerRpc()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 2f))
        {
            var decal = Instantiate(this.decal);
            decal.transform.position = hit.point;
            decal.GetComponent<NetworkObject>().Spawn();
        }
    }
}