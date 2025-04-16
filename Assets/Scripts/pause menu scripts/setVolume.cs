using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class setVolume : MonoBehaviour
{
    public void setMusicVolume(float value) {
        SettingManager.Instance.Mixer.SetFloat("MusicVolume", Mathf.Clamp(value, -80f, 0f));
    }

    public void setSoundEffectVolume(float value) {
        SettingManager.Instance.Mixer.SetFloat("SfxVolume", Mathf.Clamp(value, -80f, 0f));
    }

    public void setMasterVolume(float value) {
        SettingManager.Instance.Mixer.SetFloat("MasterVolume", Mathf.Clamp(value, -80f, 0f));
    }
}
