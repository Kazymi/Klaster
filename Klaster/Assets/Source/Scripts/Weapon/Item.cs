using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool ReadyToDisable { get; protected set; }
    public bool IsShow { get; protected set; }

    public void Awake()
    {
        ReadyToDisable = false;
        ItemInitialize();
    }

    protected virtual void ItemInitialize()
    {
    }

    public void DisableItem()
    {
        StartCoroutine(Disable());
    }

    public void ShowItem()
    {
        StartCoroutine(Show());
    }

    protected virtual IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.1f);
        ReadyToDisable = true;
    }

    protected virtual IEnumerator Show()
    {
        yield return new WaitForSeconds(0.1f);
        IsShow = true;
    }
}