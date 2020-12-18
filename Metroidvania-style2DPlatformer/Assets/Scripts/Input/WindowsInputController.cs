using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsInputController : InputController
{
    KeyCode JumpKey = KeyCode.Space;

    public bool JumpInput()
    {
        if (Input.GetKeyDown(JumpKey))
            return true;

        return false;
    }

    public float WalkInput()
    {
        return Input.GetAxis("Horizontal");
    }
}
