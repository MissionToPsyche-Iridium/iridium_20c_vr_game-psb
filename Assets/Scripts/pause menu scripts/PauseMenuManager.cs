using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private float spawnDistance = 3;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHandPause;
    [SerializeField] private GameObject rightHandPause;
    [SerializeField] private InputActionProperty showButton;
    [SerializeField] private AudioSource source;
    [SerializeField] float x = 0.0f;
    [SerializeField] float y = 0.0f;
    [SerializeField] float z = 0.0f;

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
            menu.transform.position = head.position + new Vector3(head.forward.x+x ,y,head.forward.z+z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y,head.position.z));
        menu.transform.forward *= -1;
    }
}
