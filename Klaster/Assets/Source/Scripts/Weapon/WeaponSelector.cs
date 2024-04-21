using System.Collections;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private Transform weaponSpawnPosition;

    private Item currentItem;
    private bool isSelectedItem;

    public void SelectNewItem(Item item)
    {
        if (isSelectedItem)
        {
            return;
        }

        StartCoroutine(SelectNewCurrentItem(item));
    }

    private IEnumerator SelectNewCurrentItem(Item newItem)
    {
        isSelectedItem = true;
        if (currentItem != null)
        {
            currentItem.DisableItem();
            while (currentItem.ReadyToDisable == false)
            {
                yield return null;
            }

            currentItem.ReturnToPool();
        }

        currentItem = newItem;
        var transformCurrentItem = currentItem.transform;
        transformCurrentItem.parent = weaponSpawnPosition;
        transformCurrentItem.localPosition = Vector3.zero;
        transformCurrentItem.localRotation = Quaternion.identity;

        currentItem.ShowItem();
        while (currentItem.IsShow == false)
        {
            yield return null;
        }

        isSelectedItem = false;
    }
}