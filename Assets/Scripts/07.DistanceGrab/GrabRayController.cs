using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabRayController : MonoBehaviour
{
    public XRRayInteractor grabRayInteractor;
    public XRDirectInteractor grabDirectInteractor;

    public InputActionReference rotateAnchorReference;
    public InputActionReference translateAnchorReference;
    public InputActionReference teleportActivateReference;
    public InputActionReference moveReference;
    public InputActionReference turnReference;

    private void Start()
    {
        grabRayInteractor.selectEntered.AddListener(OnEnterGrab);
        grabRayInteractor.selectExited.AddListener(OnExitGrab);
    }
    private void OnDestroy()
    {
        grabRayInteractor.selectEntered.RemoveListener(OnEnterGrab);
        grabRayInteractor.selectExited.RemoveListener(OnExitGrab);
    }
    private void OnEnterGrab(SelectEnterEventArgs arg)
    {
        DisableAction(teleportActivateReference);
        DisableAction(moveReference);
        DisableAction(turnReference);
        EnableAction(rotateAnchorReference);
        EnableAction(translateAnchorReference);
    }
    private void OnExitGrab(SelectExitEventArgs arg)
    {
        EnableAction(teleportActivateReference);
        EnableAction(moveReference);
        EnableAction(turnReference);
        DisableAction(rotateAnchorReference);
        DisableAction(translateAnchorReference); 
    }
    void Update()
    {
         //grabRayInteractor.enabled = grabDirectInteractor.interactablesSelected.Count == 0; 
    }

    private void EnableAction(InputActionReference actionReference)
    {
        var action = GetInputAction(actionReference);
        if (action != null && !action.enabled)
            action.Enable();
    }

    private void DisableAction(InputActionReference actionReference)
    {
        var action = GetInputAction(actionReference);
        if (action != null && action.enabled)
            action.Disable();
    }

    private InputAction GetInputAction(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action : null;
    }
}
