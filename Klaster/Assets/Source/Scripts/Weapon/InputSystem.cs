using Unity.Netcode;
using UnityEngine;

public class InputSystem : NetworkBehaviour
{
    public bool isOwner => IsOwner;
    public bool IsReload()
    {
        if (IsOwner == false) return false;
        return Input.GetKeyDown(KeyCode.R);
    }

    public bool IsShot()
    {
        if (IsOwner == false) return false;
        return Input.GetMouseButton(0);
    }

    public bool IsScope()
    {
        if (IsOwner == false) return false;
        return Input.GetMouseButton(1);
    }

    public bool IsFreeze()
    {
        if (IsOwner == false) return false;
        return Input.GetKey(KeyCode.LeftAlt);
    }

    public bool IsRun()
    {
        if (IsOwner == false) return false;
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool IsReview()
    {
        if (IsOwner == false) return false;
        return Input.GetKeyDown(KeyCode.F);
    }
}