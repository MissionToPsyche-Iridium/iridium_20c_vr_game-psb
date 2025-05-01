using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionPopup : MonoBehaviour
{   
    [SerializeField] private TextMeshPro popuptext;
    [SerializeField] private TextMeshPro Finishtext;
    [SerializeField] private TextMeshProUGUI GameTimer;
    bool didcollide=false;
    public bool complete=false;
    public GameObject musicToReplace;
    public AudioSource musicToAdd; 
    int finalcollide=0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCanvasCheck());
    
    }

    IEnumerator StartCanvasCheck(){
        if(didcollide==true)
        {
            popuptext.enabled=true;
            yield return new WaitForSecondsRealtime(3);
        }

        popuptext.enabled=false;
        Finishtext.enabled=false;
        GameTimer.enabled=true;

    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("Player Rigidbody velocity= "+GetComponent<Rigidbody>().velocity);
    }

    void OnCollisionEnter(Collision collision)
    {  
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Obstacle")
        {
            popuptext.enabled=true;
            didcollide=true;
            Debug.Log("Collision Has Occured");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
        else if(collision.gameObject.tag=="Finish")
        {   
            finalcollide++;
            complete=true;

            if(finalcollide==1)
            {
            collision.gameObject.GetComponent<MeshCollider>().enabled=false;
            Finishtext.enabled=true;
            GameTimer.enabled=false;
            didcollide=false;
            StartCoroutine(NextScene());

            Debug.Log("Game complete!");
                        
            }
            
            
        }
            
    }

    IEnumerator NextScene()
    {
        Debug.Log("End credit delayed coroutine started.");
        Instantiate(musicToAdd, musicToReplace.transform.position, musicToReplace.transform.rotation);
        musicToAdd.Play();
        Destroy(musicToReplace);
        StartCoroutine(hideText()); 
        yield return new WaitForSecondsRealtime(20);
        Debug.Log("Yield 30 seconds complete.");
        finalcollide=0;
        SceneManager.LoadScene("Credits");
    }
    IEnumerator hideText()
    {
        yield return new WaitForSecondsRealtime(10);
        Finishtext.enabled=false;
    }
}
