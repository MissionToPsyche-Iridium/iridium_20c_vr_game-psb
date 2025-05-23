using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro; // Add this namespace for TMP_Dropdown
public class setTurnType : MonoBehaviour
{
    [SerializeField] private ActionBasedSnapTurnProvider snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurn;
    [SerializeField] private GameObject dropDownObject;
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
