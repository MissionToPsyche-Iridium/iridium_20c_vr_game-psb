using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenCloseDoor : MonoBehaviour {
  private Animator doorAnim;
  [SerializeField] private TextMeshPro hovertext;
  private bool doorOpen = false;

  public void ToggleDoorState() {
    doorAnim = gameObject.GetComponent<Animator>();
    if( !doorOpen ) {
      doorAnim.Play( "door_2_open" );
    } else {
      doorAnim.Play( "door_2_close" );
    }
    doorOpen = !doorOpen;
  }

  public void triggerHoverText(){
    if(gameObject.GetComponent<TextMeshPro>().enabled==false)
    {
      gameObject.GetComponent<TextMeshPro>().enabled=true;

    }

  }

}