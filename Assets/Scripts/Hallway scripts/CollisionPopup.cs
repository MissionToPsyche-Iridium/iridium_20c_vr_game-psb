using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionPopup : MonoBehaviour
{   
    [SerializeField] private TextMeshPro popuptext;
    [SerializeField] private TextMeshPro Finishtext;
    // Start is called before the first frame update
    void Start()
    {
        popuptext.enabled=false;
        Finishtext.enabled=false;
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

            Debug.Log("Collision Has Occured");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
        else if(collision.gameObject.tag=="Finish")
        {
            Finishtext.enabled=true;
            Debug.Log("Game complete!");
            
        }
            
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(5);
    }
}
