using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro; // Add this namespace for TMP_Dropdown

public class ConferenceRoomManager : MonoBehaviour
{
    [SerializeField] private ActionBasedSnapTurnProvider snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurn;
    [SerializeField] private GameObject dropDownObject;
    private TMP_Dropdown dropDown; // Change Dropdown to TMP_Dropdown
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sFXVolume;
    [SerializeField] private Slider brightness;

    void Start()
    {
        float value;
        bool result;
        result = SettingManager.Instance.Mixer.GetFloat("MasterVolume", out value);
        if(result) {
            masterVolume.value = value;
        }
        result = SettingManager.Instance.Mixer.GetFloat("MusicVolume", out value);
        if(result) {
            musicVolume.value = value;
        }
        result = SettingManager.Instance.Mixer.GetFloat("SfxVolume", out value);
        if(result) {
            sFXVolume.value = value;
        }
        brightness.value = SettingManager.Instance.Brightness;
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