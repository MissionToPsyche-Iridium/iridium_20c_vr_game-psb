using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionPopup : MonoBehaviour
{   
    private Text popuptext;
    // Start is called before the first frame update
    void Start()
    {
        popuptext.enabled=false;
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
            
        }
            
    }
}
