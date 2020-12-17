using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used for fixing the camera to the player, so it follows the player throughout the game
public class CameraMotor : MonoBehaviour
{
    //Variables
    public GameObject target;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(target.transform.position);
    }
}
