
/*
Name of the module: Virtual movement
Date on which the module is created: 18/4/18
Author of the module:Manoj Reddy
Modification History: By Bhargav Mallala 19/4/18
                      By Balabolu Tushara Langulya 19/4/18
Synopsis of the module : This module is executed when the user orients his bhead at appropriate angles to move
Functions: 1.Start :void Start()
           2.void Update()
Global Variables: No global variables
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLookWalk : MonoBehaviour
{

    public Transform vrCamera;
    // this is the minimum angle reqired for the user to move
    public float toggleAngle = 30.0f;
    // speed with which user moves
    public float speed = 3.0f;
    public bool moveForward;

    private CharacterController cc;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if these conditions are not satisfied then there wont be any movement of user
        if (vrCamera.eulerAngles.x >=toggleAngle && vrCamera.eulerAngles.x < 90.0f)
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        if (moveForward )
        {
            // forward is used to store the direction in which the user moves which will depend on the direction he is facing
            Vector3 forward = vrCamera.TransformDirection(Vector3.forward);
            // move in the forward direction at the rate of speed
            cc.SimpleMove(forward * speed);
        }
    }
}


