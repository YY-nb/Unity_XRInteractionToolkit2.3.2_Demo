using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandController : MonoBehaviour
{
    public InputActionProperty pinchActionProperty;
    public InputActionProperty gripActionProperty;
    private InputAction pinchAction;
    private InputAction gripAction;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        pinchAction = pinchActionProperty.action;
        gripAction = gripActionProperty.action;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAction.ReadValue<float>();
        animator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAction.ReadValue<float>();
        animator.SetFloat("Grip", gripValue);
    }
}
