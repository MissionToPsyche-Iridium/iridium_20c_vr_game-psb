using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCollision : MonoBehaviour
{
    public GameObject safeCanvas;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftHandPause;
    public GameObject rightHandPause;

    private void OnTriggerEnter(Collision collision)
    {
        // Check if the collision is with a specific object, e.g., "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle the collision with the player
            safeCanvas.SetActive(true);
            leftHand.SetActive(!leftHand.activeSelf);
            rightHand.SetActive(!rightHand.activeSelf);
            leftHandPause.SetActive(!leftHandPause.activeSelf);
            rightHandPause.SetActive(!rightHandPause.activeSelf);
            leftHandPause.transform.position = leftHand.transform.position;
            rightHandPause.transform.position = rightHand.transform.position;
        }
    }

    private void OnTriggerExit(Collision collision)
    {
        // Check if the collision is with a specific object, e.g., "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle the collision exit with the player
            safeCanvas.SetActive(false);
            leftHand.SetActive(!leftHand.activeSelf);
            rightHand.SetActive(!rightHand.activeSelf);
            leftHandPause.SetActive(!leftHandPause.activeSelf);
            rightHandPause.SetActive(!rightHandPause.activeSelf);
            leftHandPause.transform.position = leftHand.transform.position;
            rightHandPause.transform.position = rightHand.transform.position;
        }
    }
}
