using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CustomSocketInteractor : XRSocketInteractor
{
    [SerializeField] public GameObject popupImage; // Reference to your UI image
    private bool isPopupActive = false; // Track if the popup is active
    [SerializeField] private InputActionProperty triggerAction; //using the triggerAction to close the popup
    [SerializeField] public GameObject interactorObject; //the snap zone object

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        GameObject objectToCheck = interactable.transform.gameObject;

        // Check if the object has the desired tag
        if (objectToCheck.CompareTag("Florida") == interactorObject.CompareTag("Florida"))
        {
            return base.CanSelect(interactable);
        } 
         else if (objectToCheck.CompareTag("Arizona") == interactorObject.CompareTag("Arizona"))
        {
            return base.CanSelect(interactable);
        } 
         else if (objectToCheck.CompareTag("Massachuesettes") == interactorObject.CompareTag("Massachuesettes"))
        {
            return base.CanSelect(interactable);
        }

        return false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Show the popup when the object is selected
        ShowPopup();
    }

    private void ShowPopup()
    {
        if (!isPopupActive && popupImage != null)
        {
            popupImage.SetActive(true); // Activate the popup image
            isPopupActive = true;
        }
    }

    private void Update()
    {
        // Check for button press to close the popup
        if (isPopupActive && triggerAction.action.ReadValue<float>() > 0) // Adjust button mapping as needed
        {
            ClosePopup();
        }
    }

    private void ClosePopup()
    {
        if (popupImage != null)
        {
            popupImage.SetActive(false); // Deactivate the popup image
            isPopupActive = false;
        }
    }
}
