using Unity.Netcode;
using UnityEngine;

public class PlayerHealthController : NetworkBehaviour
{
    private int health;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        TakeDamage(100);
    }
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        health = 100;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"damage taked {NetworkObjectId}");
        health -= damage;
        if (health <= 0 && IsOwner)
        {
            DeadServerRpc();
        }
    }

    [ServerRpc]
    private void DeadServerRpc()
    {
        GameManager.Instance.RespawnPlayer(GetComponent<NetworkObject>().OwnerClientId);
        var netObject = GetComponent<NetworkObject>();
        netObject.Despawn(true);
    }
}