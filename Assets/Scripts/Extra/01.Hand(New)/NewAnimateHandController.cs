using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewAnimateHandController : MonoBehaviour
{
    public InputActionProperty triggerActionProperty;
    public InputActionProperty gripActionProperty;
    public InputActionProperty thumbActionProperty;
    private InputAction pinchAction;
    private InputAction gripAction;
    private InputAction thumbAction;
    private Animator animator;

    private string triggerName = "Trigger";
    private string gripName = "Grip";
    private string thumbName = "Thumb";

    private enum HandAnimationLayer
    {
        grip,
        trigger,
        thumb,
    }


    void Start()
    {
        pinchAction = triggerActionProperty.action;
        gripAction = gripActionProperty.action;
        thumbAction = thumbActionProperty.action;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        float gripValue = gripAction.ReadValue<float>();
        animator.SetFloat(gripName, gripValue);

        float triggerValue = pinchAction.ReadValue<float>();
       // animator.SetLayerWeight((int)HandAnimationLayer.trigger, triggerValue);
        animator.SetFloat(triggerName, triggerValue);


       
        float thumbValue = thumbAction.ReadValue<float>();
       // animator.SetLayerWeight((int)HandAnimationLayer.thumb, thumbValue);
        animator.SetFloat(thumbName, thumbValue);
    }
}
