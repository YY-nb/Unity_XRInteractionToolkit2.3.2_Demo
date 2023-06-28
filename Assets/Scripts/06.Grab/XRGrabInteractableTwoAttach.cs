using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;
    /*
    //方法一，需要在可抓取的物体上添加XRRSingleGrabFreeTransformer脚本
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if(args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }
        base.OnSelectEntered(args);
    } */
    public override Transform GetAttachTransform(IXRInteractor interactor)
    {

        Transform i_attachTransform = null;
        if (interactor.transform.CompareTag("Left Hand"))
        {
            i_attachTransform = leftAttachTransform;
        }
        if (interactor.transform.CompareTag("Right Hand"))
        {
            i_attachTransform = rightAttachTransform;
        }
        return i_attachTransform != null ? i_attachTransform : base.GetAttachTransform(interactor);

    }
}
