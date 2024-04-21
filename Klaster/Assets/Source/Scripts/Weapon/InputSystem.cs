using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public bool IsReload()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    public bool IsShot()
    {
        return Input.GetMouseButton(0);
    }

    public bool IsScope()
    {
        return Input.GetMouseButton(1);
    }

    public bool IsFreeze()
    {
        return Input.GetKey(KeyCode.LeftAlt);
    }

    public bool IsRun()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool IsReview()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}