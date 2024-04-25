using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void RespawnPlayer(ulong playerIf)
    {
        PlayerSpawnServerRpc(playerIf);
    }

    [ServerRpc]
    private void PlayerSpawnServerRpc(ulong playerId)
    {
        var newPlayer = Instantiate(playerPrefab);
        var netObject = newPlayer.GetComponent<NetworkObject>();
        netObject.SpawnAsPlayerObject(playerId, true);
    }
}