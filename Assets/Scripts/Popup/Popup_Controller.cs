using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachPointPopup : MonoBehaviour
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
        DisplayPopup();
    }

    //rework this function to clear instead when button is pressed
    private void OnObjectDetached(SelectExitEventArgs args)
    {
        //reference for controls
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), InputHelpers.Button.PrimaryButton, out bool isPressed);
        // Hide the popup when the object is detached
        ClearPopup();
    }

    private void DisplayPopup()
    {
        if (popupUI != null && !isPopupVisible)
        {
            popupUI.SetActive(true); // Enable popup UI
            myAudioSource?.Play();   // Play audio if assigned
            isPopupVisible = true;
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