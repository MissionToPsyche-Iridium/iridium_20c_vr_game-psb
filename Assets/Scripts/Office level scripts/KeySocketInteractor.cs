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
    [SerializeField] private Animator anim;
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
        StartCoroutine(PlayFade());
        new WaitForSeconds(5f);
        SceneManager.LoadScene("SeverRoomScene");
    }

    IEnumerator PlayFade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a==1);

    }
}
