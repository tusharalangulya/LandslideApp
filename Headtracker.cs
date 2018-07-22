/*10
Name of the module: Head tracker
Date on which the module is created: 10/4/18
Author of the module:Manoj Reddy
Modification History: By Bhargav Mallala 13/4/18
                      By Balabolu Tushara Langulya 15/4/18
Synopsis of the module : This module is executed when the user orients his head to look around
Name of function: 1)Update : output: rotation based on the input angles
                             input: Axis of rotation,rotationY angle and rotationX angle
                  2)Start: output:system gets ready to start rotation
                           input:No input
Global Variables:No global variables used in this module
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    // we have 3 possible RotationAxes with both x and y axes or only x axis or only y axis
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    // the limits of angle with X axis min and max value respectively
    public float minimumX = -360F;
    public float maximumX = 360F;
    // the limits of angle with Y axis min and max value respectively
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            //Input.GetAxis will give the value of virtual Xaxis .By multiplying with sensitivity X we get its real angle
            // transform.localEulerAngles.y gives us the angle with respect to y axis in degrees
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            // clamps the rotationY between minimumY and maximumY
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            // this gives us new angle  at which we view
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            // this gives us new angle  at which we view
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
}
