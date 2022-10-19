using UnityEngine;

public class TestItemSelector : MonoBehaviour
{
    [SerializeField] private Item selectableItem;

    private WeaponSelector weaponSelector;

    private void Awake()
    {
        weaponSelector = FindObjectOfType<WeaponSelector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var newItem = Instantiate(selectableItem);
            weaponSelector.SelectNewItem(newItem);
        }
    }
}