using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWhenDroppingStates : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject self;
    
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
     if(collision.gameObject.tag != "Left Hand" || collision.gameObject.tag != "Right Hand" || collision.gameObject.tag != "Player")
        {
            source.Play();
        }
    }
}