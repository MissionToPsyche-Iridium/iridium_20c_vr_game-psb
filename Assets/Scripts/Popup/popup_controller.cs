using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup_controller
{
    private static popup_controller popupC = new popup_controller();
    private GameObject popup_display;
    private Image transparent;
    private bool popup_visible = false;

    private popup_controller() { }
    public static popup_controller getInstance() { return popupC; }

    public void display_popup(Image objImage)
    {
        camC.changeLock();
        popup_display.GetComponent<Image>().sprite = objImage.sprite;
        popup_display.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        popup_visible = true;
    }
    public void clear_popup()
    {
        camC.changeLock();
        popup_display.GetComponent<Image>().sprite = transparent.sprite;

        //check if the correct location has its transparency adjusted
        popup_display.GetComponent<Image>().color = new Color(1, 1, 1, .25f);
        popup_visible = false;
    }

    public void setPopupView(GameObject popup)
    {
        //sets the UI popup component
        //sets the transparent image, to be accessed everytime the UI popup is cleared.
        //need popup_view panel with a Image child UI element, with the "Transparent" image
        popup_display = popup;
        transparent = popup.transform.GetChild(0).gameObject.GetComponent<Image>();
    }
    public bool isPopupVisible() { return popup_visible;}

}
