using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Add this namespace for TMP_Dropdown
public class setTurnType : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;
    public GameObject dropDownObject;
    private TMP_Dropdown dropDown; // Change Dropdown to TMP_Dropdown
    public void setTypeFromIndex()
    {
        dropDown = dropDownObject.GetComponent<TMP_Dropdown>(); // Change to TMP_Dropdown
        if (dropDown.options[dropDown.value].text == "Snap Turn")
        {
            SettingManager.Instance.ContinuousTurn = false;
            continuousTurn.enabled = false;
            snapTurn.enabled = true;
        }
        else 
        {
            SettingManager.Instance.ContinuousTurn = true;
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }
    }
}
