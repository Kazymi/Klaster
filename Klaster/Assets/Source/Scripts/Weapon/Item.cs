using System.Collections;
using UnityEngine;

public class Item : MonoPooled
{
    public bool ReadyToDisable { get; protected set; }
    public bool IsShow { get; protected set; }

    public override void Initialize()
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

    public void ShowWeapon()
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