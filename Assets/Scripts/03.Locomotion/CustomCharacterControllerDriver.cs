using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomCharacterControllerDriver : CharacterControllerDriver
{

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();       
    }
}
