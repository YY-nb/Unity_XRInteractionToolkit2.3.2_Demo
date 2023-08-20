using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputTest : MonoBehaviour
{
    public InputActionReference testActionReference;    
    private InputAction testAction;

    void Start()
    {
        testAction = testActionReference.action;

        testAction.performed += ActivateBehavior; 
    }


    private void ActivateBehavior(InputAction.CallbackContext context)
    {
        print("Ö´ÐÐ·½·¨");
    }    
}
