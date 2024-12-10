using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinueButtonManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftHandPause;
    public GameObject rightHandPause;
    public InputActionProperty showButton;

    
    public void disablePauseMenu()
    {
        showButton.action.Disable();
        menu.SetActive(false);
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        leftHandPause.SetActive(false);
        rightHandPause.SetActive(false);
        leftHandPause.transform.position = leftHand.transform.position;
        rightHandPause.transform.position = rightHand.transform.position;
        Time.timeScale = 1.0f;
        showButton.action.Enable();
    }
}
