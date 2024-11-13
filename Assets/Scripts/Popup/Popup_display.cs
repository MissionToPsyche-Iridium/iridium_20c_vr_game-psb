using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_display : MonoBehaviour
{
    // Start is called before the first frame update
    private popup_controller popupC = popup_controller.getInstance();
    private void Awake()
    {
        popupC.setPopupView(gameObject);
    }
    void Start()
    {

        popupC.setPopupView(gameObject);
    }
}
