using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro; // Add this namespace for TMP_Dropdown

public class OfficeManager : MonoBehaviour
{
    public GameTimer timer;
    public GameObject timerCanvas;
    public GameObject textTimer;
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;
    public GameObject dropDownObject;
    private TMP_Dropdown dropDown; // Change Dropdown to TMP_Dropdown

    // Start is called before the first frame update
    void Awake()
    {
        if (SettingManager.Instance.EventMode == false)
        {
            timer.enabled = false;
            timerCanvas.SetActive(false);
            textTimer.SetActive(false);
        }
    }

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