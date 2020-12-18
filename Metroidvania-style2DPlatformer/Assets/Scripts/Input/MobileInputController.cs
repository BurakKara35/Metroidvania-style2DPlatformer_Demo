using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class MobileInputController : InputController
{
    public bool JumpInput()
    {
        if(CrossPlatformInputManager.GetButtonDown("Jump"))
            return true;

        return false;
    }

    public float WalkInput()
    {
        return CrossPlatformInputManager.GetAxis("Horizontal");
    }
}
