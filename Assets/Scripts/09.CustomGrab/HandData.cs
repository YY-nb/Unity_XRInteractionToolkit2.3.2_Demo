using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    public enum HandModelType { Left, Right}
    public HandModelType handType;
    public Animator animator;
    public Transform root; //�ֲ�ģ�͵ĸ�����    
    public Transform[] fingerBones; //�ֲ�ģ�͵�ÿ���ؽ�
}
