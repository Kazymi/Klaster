using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    private Item currentItem;
    private bool isSelectedItem;

    public void SelectNewItem(Item item)
    {
        if (currentItem != null)
        {
            currentItem.gameObject.SetActive(false);
            item.gameObject.SetActive(true);
            currentItem = item;
        }
        else
        {
            item.gameObject.SetActive(true);
            currentItem = item;
        }
    }
}