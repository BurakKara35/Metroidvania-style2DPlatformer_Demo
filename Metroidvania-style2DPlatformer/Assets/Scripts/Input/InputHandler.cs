using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    InputController inputController;

    void Awake()
    {
#if UNITY_STANDALONE_WIN
        inputController = new WindowsInputController();
#endif

#if UNITY_IOS || UNITY_ANDROID
        inputController = new MobileInputController();
#endif

        //inputController = new WindowsInputController();
    }

    public float GetHorizontalInput()
    {
        return inputController.WalkInput();
    }

    public bool GetJumpInput()
    {
        return inputController.JumpInput();
    }
}
