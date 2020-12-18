using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mobileController;

    private void Awake()
    {
#if UNITY_IOS || UNITY_ANDROID
        mobileController.SetActive(true);
#endif
    }
}
