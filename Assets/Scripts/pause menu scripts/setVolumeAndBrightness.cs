using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setVolumeAndBrightness : MonoBehaviour
{
    public void setMusicVolume(float value) {
        SettingManager.Instance.AudioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void setSoundEffectVolume(float value) {
        SettingManager.Instance.AudioMixer.SetFloat("SfxVolume", Mathf.Log10(value) * 20);
    }

    public void setBrightness(float value) {
        SettingManager.Instance.Brightness = value;
    }
}
