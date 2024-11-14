using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Popup_Controller : MonoBehaviour
{
    [SerializeField] private GameObject popupUI;         // Assign the popup UI GameObject in the Inspector
    [SerializeField] private AudioSource myAudioSource;
    private XRSocketInteractor socketInteractor;
    private bool isPopupVisible = false;

    private void Awake()
    {
        // Get the XRSocketInteractor component
        socketInteractor = GetComponent<XRSocketInteractor>();

        // Add listeners for select events (attach and detach)
        socketInteractor.selectEntered.AddListener(OnObjectAttached);
        socketInteractor.selectExited.AddListener(OnObjectDetached);
    }

    private void OnObjectAttached(SelectEnterEventArgs args)
    {
        // Display the popup when an object is attached
        IXRSelectInteractable states = socketInteractor.GetOldestInteractableSelected();
        GameObject stateTemp = states.transform.gameObject;
        DisplayPopup(stateTemp);
    }

    //rework this function to clear instead when button is pressed
    private void OnObjectDetached(SelectExitEventArgs args)
    {
        //reference for controls
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), InputHelpers.Button.PrimaryButton, out bool isPressed);
        // Hide the popup when the object is detached
        ClearPopup();
    }

    private void DisplayPopup(GameObject state)
    {
        if (popupUI != null && !isPopupVisible)
        {
            if(socketInteractor.CompareTag("Florida") == state.CompareTag("Florida"))
            {
                popupUI.SetActive(true); // Enable popup UI
                myAudioSource?.Play();   // Play audio if assigned
                isPopupVisible = true;
            } else if (socketInteractor.CompareTag("Arizona") == state.CompareTag("Arizona"))
            {
                popupUI.SetActive(true); // Enable popup UI
                myAudioSource?.Play();   // Play audio if assigned
                isPopupVisible = true;
            } else if (socketInteractor.CompareTag("Massachuesettes") == state.CompareTag("Massachuesettes"))
            {
                popupUI.SetActive(true); // Enable popup UI
                myAudioSource?.Play();   // Play audio if assigned
                isPopupVisible = true;
            }
        }
    }


    private void ClearPopup()
    {
        if (popupUI != null && isPopupVisible)
        {
            popupUI.SetActive(false); // Disable popup UI
            isPopupVisible = false;
        }
    }

    private void OnDestroy()
    {
        // Clean up listeners to avoid memory leaks
        socketInteractor.selectEntered.RemoveListener(OnObjectAttached);
        socketInteractor.selectExited.RemoveListener(OnObjectDetached);
    }
}