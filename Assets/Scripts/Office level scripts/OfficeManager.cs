using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro; // Add this namespace for TMP_Dropdown

public class OfficeManager : MonoBehaviour
{
    [SerializeField] private GameTimer timer;
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private GameObject textTimer;
    [SerializeField] private ActionBasedSnapTurnProvider snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurn;
    [SerializeField] private GameObject dropDownObject;
    private TMP_Dropdown dropDown; // Change Dropdown to TMP_Dropdown
    [SerializeField] private GameObject safeCollider;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sFXVolume;
    [SerializeField] private Slider brightness;
    [SerializeField] private Image black;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject notePart1;
    [SerializeField] private GameObject notePart2;

    void Awake()
    {
        if (SettingManager.Instance.EventMode == false)
        {
            timer.enabled = false;
            timerCanvas.SetActive(false);
            textTimer.SetActive(false);
        }
        else
        {
            Destroy(safeCollider);
            notePart1.SetActive(false);
            notePart2.SetActive(false);
        }
    }

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