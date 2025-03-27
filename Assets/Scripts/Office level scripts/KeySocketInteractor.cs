using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class KeySocketInteractor : XRSocketInteractor
{
    [SerializeField] private GameObject interactorObject; //the snap zone object

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        GameObject objectToCheck = interactable.transform.gameObject;

        // Check if the object has the desired tag
        if (objectToCheck.CompareTag("Key") == interactorObject.CompareTag("Key"))
        {
            return base.CanSelect(interactable);
        } 

        return false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        //go to the conference scene
        SceneManager.LoadScene("ConferenceRoom");
    }
}
