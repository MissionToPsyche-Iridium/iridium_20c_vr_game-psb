using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCollision : MonoBehaviour
{
    [SerializeField] private GameObject safeCanvas;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHandPause;
    [SerializeField] private GameObject rightHandPause;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with a specific object, e.g., "Player"
        if (other.CompareTag("Player"))
        {
            // Handle the collision with the player
            safeCanvas.SetActive(true);
            leftHand.SetActive(!leftHand.activeSelf);
            rightHand.SetActive(!rightHand.activeSelf);
            leftHandPause.SetActive(!leftHandPause.activeSelf);
            rightHandPause.SetActive(!rightHandPause.activeSelf);
            leftHandPause.transform.position = leftHand.transform.position;
            rightHandPause.transform.position = rightHand.transform.position;
            SettingManager.Instance.IsRayHandActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collision is with a specific object, e.g., "Player"
        if (other.CompareTag("Player"))
        {
            // Handle the collision exit with the player
            safeCanvas.SetActive(false);
            leftHand.SetActive(!leftHand.activeSelf);
            rightHand.SetActive(!rightHand.activeSelf);
            leftHandPause.SetActive(!leftHandPause.activeSelf);
            rightHandPause.SetActive(!rightHandPause.activeSelf);
            leftHandPause.transform.position = leftHand.transform.position;
            rightHandPause.transform.position = rightHand.transform.position;
            SettingManager.Instance.IsRayHandActive = false;
        }
    }
}
