using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    public enum HandModelType { Left, Right}
    public HandModelType handType;
    public Animator animator;
    public Transform root; //手部模型的根物体    
    public Transform[] fingerBones; //手部模型的每个关节
}
