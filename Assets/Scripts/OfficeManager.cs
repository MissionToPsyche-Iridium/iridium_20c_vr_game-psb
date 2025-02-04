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
    [SerializeField] public GameObject dropDownObject;
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
            snapTurn.enabled = true;
            continuousTurn.enabled = false;

            if (dropDownObject == null)
            {
                Debug.LogError("dropDownObject is not assigned!");
                return;
            }

            dropDown = dropDownObject.GetComponent<TMP_Dropdown>(); // Change to TMP_Dropdown
            if (dropDown == null)
            {
                Debug.LogError("TMP_Dropdown component is missing on dropDownObject!");
                return;
            }

            int index = dropDown.options.FindIndex(option => option.text == "Snap Turn");
            if (index == -1)
            {
                Debug.LogError("Option 'Snap Turn' not found in TMP_Dropdown!");
                return;
            }

            dropDown.value = index;
            dropDown.RefreshShownValue();
        }
    }

}