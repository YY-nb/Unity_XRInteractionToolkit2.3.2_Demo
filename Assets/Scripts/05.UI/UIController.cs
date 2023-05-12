using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public InputActionReference menu;
    public Canvas canvas;

    private void Start()
    {
        if(menu != null)
        {
            menu.action.Enable();
            menu.action.performed += ToggleMenu;
        }
        
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        canvas.enabled = !canvas.enabled;
    }
}
