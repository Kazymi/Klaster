using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class ShotHelper : NetworkBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform spawnPosition;

    public void Shot()
    {
        if (GetComponentInParent<InputSystem>().IsOwner)
        {
            ShotServerRpc(spawnPosition.position, Quaternion.LookRotation(camera.forward));
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        gameObject.SetActive(false);
    }

    [ServerRpc]
    private void ShotServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams serverRpcParams = default)
    {
        var bullet = Instantiate(prefabBullet);
        bullet.transform.SetPositionAndRotation(position,
            rotation);
        bullet.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
    }
}