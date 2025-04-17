using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollision : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject self;
    
    private bool playedOnce = false;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }


void Update()
{
    if ((floor.transform.position.y - self.transform.position.y)  == 1 && !playedOnce)
    {
        source.Play();
        playedOnce = true;
    }
    else
    {
        source.Stop();
        playedOnce = false;
    }
}
}