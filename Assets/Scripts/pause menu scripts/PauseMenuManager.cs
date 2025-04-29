using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float spawnDistance = 3f;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHandPause;
    [SerializeField] private GameObject rightHandPause;
    [SerializeField] private InputActionProperty showButton;
    [SerializeField] private AudioSource source;

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            if(SettingManager.Instance.IsRayHandActive == false) {
                leftHand.SetActive(!leftHand.activeSelf);
                rightHand.SetActive(!rightHand.activeSelf);
                leftHandPause.SetActive(!leftHandPause.activeSelf);
                rightHandPause.SetActive(!rightHandPause.activeSelf);
                leftHandPause.transform.position = leftHand.transform.position;
                rightHandPause.transform.position = rightHand.transform.position;
                source.Play();
            }//make a bool for setting the hands
            if (menu.activeSelf)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
            Vector3 whereToSpawnMenu = cameraTransform.position + cameraTransform.forward *  spawnDistance;
            menu.transform.position = whereToSpawnMenu;
            menu.transform.LookAt(cameraTransform);
            menu.transform.Rotate(0f,180f,0f);
        }
    }
}
