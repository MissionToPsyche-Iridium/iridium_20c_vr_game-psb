using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class KeySocketInteractor : XRSocketInteractor
{
    [SerializeField] private GameObject interactorObject; //the snap zone object
    [SerializeField] private Image black;

    [SerializeField] private FadeScreen fadeScreen; 

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
        StartCoroutine(StartGameWithFade());
    }

    private IEnumerator StartGameWithFade()
    {
        fadeScreen.EnableWithFade();
        yield return new WaitForSeconds(0.5f); // Wait for the fade effect to complete
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SeverRoomScene");
        
    }
}
