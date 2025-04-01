using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sFXVolume;
    [SerializeField] private Slider brightness;


    // Start is called before the first frame update
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
    }
}
