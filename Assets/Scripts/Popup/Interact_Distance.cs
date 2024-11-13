using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDistanceVR
{
    private float playerDistance;

    // Singleton pattern for the InteractDistanceVR
    private static InteractDistanceVR instance = new InteractDistanceVR();
    private InteractDistanceVR() { }
    public static InteractDistanceVR GetInstance()
    {
        return instance;
    }

    // Calculate distance from VR player to the GameObject and check if within the interaction distance
    public bool CheckDistance(float distance, GameObject interactObj)
    {
        // Reference to VR player position (ensure VRPlayerController is a singleton or accessible class)
        Vector3 playerPosition = VRPlayerController.Instance.GetPlayerPosition();

        // Calculate the 3D distance between player and object
        float distanceToObj = Vector3.Distance(playerPosition, interactObj.transform.position);

        // Return true if within interaction distance
        return distanceToObj <= distance;
    }
}
