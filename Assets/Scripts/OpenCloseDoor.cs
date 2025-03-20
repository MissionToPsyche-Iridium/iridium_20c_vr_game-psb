using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour {
  private Animator doorAnim;
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

}