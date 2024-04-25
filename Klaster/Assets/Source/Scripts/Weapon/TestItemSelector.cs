using Unity.Netcode;
using UnityEngine;

public class TestItemSelector : NetworkBehaviour
{
    [SerializeField] private Item selectableItem;

    private WeaponSelector weaponSelector;

    private void Awake()
    {
        weaponSelector = FindObjectOfType<WeaponSelector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && IsOwner)
        {
            ShowWeaponServerRpc();
        }
    }

    [ServerRpc]
    private void ShowWeaponServerRpc()
    {
        WeaponShowedClientRpc();
    }

    [ClientRpc]
    private void WeaponShowedClientRpc()
    {
        weaponSelector.SelectNewItem(selectableItem);
    }
}