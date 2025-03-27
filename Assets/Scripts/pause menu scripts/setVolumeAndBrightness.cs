using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setVolumeAndBrightness : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMasterVolume(int value) {
        SettingManager.Instance.MasterVolume = value;
    }

    public void setMusicVolume(int value) {
        SettingManager.Instance.MusicVolume = value;
    }

    public void setSoundEffectVolume(int value) {
        SettingManager.Instance.SoundEffectVolume = value;
    }

    public void setBrightness(int value) {
        SettingManager.Instance.Brightness = value;
    }
}
