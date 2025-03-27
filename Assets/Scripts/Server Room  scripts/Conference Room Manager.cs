using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro; // Add this namespace for TMP_Dropdown

public class ConferenceRoomManager : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;
    [SerializeField] public GameObject dropDownObject;
    private TMP_Dropdown dropDown; // Change Dropdown to TMP_Dropdown

    void Start()
    {
        if (SettingManager.Instance.ContinuousTurn == false)
        {
            continuousTurn.enabled = false;
            snapTurn.enabled = true;
            dropDown = dropDownObject.GetComponent<TMP_Dropdown>(); // Change to TMP_Dropdown
            int index = dropDown.options.FindIndex(option => option.text == "Snap Turn");
            dropDown.value = index;
            dropDown.RefreshShownValue();
        }
    }
}