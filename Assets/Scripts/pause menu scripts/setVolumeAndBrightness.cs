using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setVolumeAndBrightness : MonoBehaviour
{
    public void setMusicVolume(float value) {
        SettingManager.Instance.Mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    public void setSoundEffectVolume(float value) {
        SettingManager.Instance.Mixer.SetFloat("SfxVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    public void setMasterVolume(float value) {
        SettingManager.Instance.Mixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    public void setBrightness(float value) {
        SettingManager.Instance.Brightness = value;
    }
}
