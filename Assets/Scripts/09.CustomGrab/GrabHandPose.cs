using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GrabHandPose : MonoBehaviour
{
    public HandData rightHandPose;
    public HandData leftHandPose;
    public bool smoothTransition; //�Ƿ���ƽ���仯����
    public float poseTransitionDuration = 0.2f; //ƽ���仯���Ƶ�ʱ��

    private XRGrabInteractable grabInteractable;
    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;
    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnsetPose);
        //����Ԥ�����Ƶ��ֲ�ģ��
        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        
        XRBaseControllerInteractor interactor = arg.interactorObject as XRBaseControllerInteractor; 
        if(interactor != null)
        {
            //�ҵ�����HandData�����壬������Ը����Լ���Ŀ�㼶��ʵ����������޸�
            HandData handData = interactor.transform.parent.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;
            //ȷ��������ץȡ��������ץȡ�������ö�Ӧ�ֲ������ݣ���ʼ��������Ϊʵ���ֲ�ģ�͵����ݣ��仯�����������ΪԤ���ץȡ�����������
            if(handData.handType == HandData.HandModelType.Right)
            {
                SetHandDataValues(handData, rightHandPose);
            }
            else
            {
                SetHandDataValues(handData, leftHandPose);
            }
            
            if (smoothTransition)
            {
                StartCoroutine(SetHandDataRouting(handData, finalHandPosition, finalHandRotation, finalFingerRotations, startingHandPosition, startingHandRotation, startingFingerRotations));
            }
            else
            {
                SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);
            }
            
        }
    }
    public void UnsetPose(BaseInteractionEventArgs arg)
    {
        XRBaseControllerInteractor interactor = arg.interactorObject as XRBaseControllerInteractor;
        if (interactor != null)
        {
            HandData handData = interactor.transform.parent.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;
            if (smoothTransition)
            {
                StartCoroutine(SetHandDataRouting(handData, startingHandPosition, startingHandRotation, startingFingerRotations, finalHandPosition, finalHandRotation, finalFingerRotations));
            }
            else
            {
                SetHandData(handData, startingHandPosition, startingHandRotation, startingFingerRotations);
            }
            
        }
    }
    /// <summary>
    /// ��ʼ���仯ǰ�ͱ仯��������������
    /// </summary>
    /// <param name="h1">ʵ���ֲ�ģ��</param>
    /// <param name="h2">ץȡ����ƥ�������</param>
    public void SetHandDataValues(HandData h1, HandData h2)
    {
        //startingHandPosition = h1.root.localPosition;
        //finalHandPosition = h2.root.localPosition;              
        startingHandPosition = new Vector3(h1.root.localPosition.x / h1.root.localScale.x, 
            h1.root.localPosition.y / h1.root.localScale.y, h1.root.localPosition.z / h1.root.localScale.z);
        finalHandPosition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x,
            h2.root.localPosition.y / h2.root.localScale.y, h2.root.localPosition.z / h2.root.localScale.z);

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h1.fingerBones.Length];

        for(int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;   
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;
        for(int i=0;i<newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }
    public IEnumerator SetHandDataRouting(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation, Vector3 startingPosition, Quaternion startingRotation, Quaternion[] startingBonesRotation)
    {
        float timer = 0;
        while(timer < poseTransitionDuration)
        {
            float lerpTime = timer / poseTransitionDuration;
            Vector3 p = Vector3.Lerp(startingPosition, newPosition, lerpTime);
            Quaternion r = Quaternion.Lerp(startingRotation, newRotation, lerpTime);
            h.root.localPosition = p;
            h.root.localRotation = r;
            for(int i = 0; i < newBonesRotation.Length; i++)
            {
                h.fingerBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i], lerpTime);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;
        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }
#if UNITY_EDITOR
    [MenuItem("Tools/Mirror Right Hand Pose")]
    public static void MirrorRightPose()
    {
        GrabHandPose handpose = Selection.activeGameObject.GetComponent<GrabHandPose>();
        handpose.MirrorPose(handpose.leftHandPose, handpose.rightHandPose);
    }
    [MenuItem("Tools/Mirror Left Hand Pose")]
    public static void MirrorLeftPose()
    {
        GrabHandPose handpose = Selection.activeGameObject.GetComponent<GrabHandPose>();
        handpose.MirrorPose(handpose.rightHandPose, handpose.leftHandPose);
    }
#endif
    /// <summary>
    /// �������ƣ���Unity�༭����ʹ��
    /// </summary>
    /// <param name="poseToMirror">�����õ�������</param>
    /// <param name="poseUsedToMirror">����֮ǰ������</param>
    public void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror)
    {
        Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
        mirroredPosition.x *= -1;

        Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
        mirroredQuaternion.y *= -1;
        mirroredQuaternion.z *= -1;

        poseToMirror.root.localPosition = mirroredPosition;
        poseToMirror.root.localRotation = mirroredQuaternion;

        for(int i = 0; i < poseUsedToMirror.fingerBones.Length; i++)
        {
            poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
        }
    }
}
