using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SimpleDoorScript : MonoBehaviour
{
    private bool isReadyToEnter = false;
    
    public GameObject finishText;
    public AudioSource completedSound; 
    
    [SerializeField] private FadeScreen fadeScreen; 

    void Start()
    {
        finishText.SetActive(false);
        isReadyToEnter = false;
        //Debug.Log("Emission is disabled and set to black");
    }

    void Update()
    {
        //Debug.Log("isReadyToEnter in update thing: " + isReadyToEnter);
        if (!isReadyToEnter && GameProgressManager.Instance.AreBothMinigamesComplete())
        {
            isReadyToEnter = true;
            finishText.SetActive(true);
            StartCoroutine(PlaySound());
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(isReadyToEnter + " " + GameProgressManager.Instance.AreBothMinigamesComplete() + " " + other.tag);
        if (isReadyToEnter && GameProgressManager.Instance.AreBothMinigamesComplete() && (other.CompareTag("Player") || other.CompareTag("Left Hand") || other.CompareTag("Right Hand")))
        {
            //Debug.Log("Player entered the door.");
            // Start the fade effect and load the next scene
            StartCoroutine(StartGameWithFade());}
    }

        private IEnumerator StartGameWithFade()
    {
        fadeScreen.EnableWithFade();
        yield return new WaitForSeconds(0.5f); // Wait for the fade effect to complete
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HallwayScene");
        
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(1.5f);
        completedSound.Play();
    }
}
