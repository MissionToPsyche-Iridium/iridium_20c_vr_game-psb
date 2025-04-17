using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBrightness : MonoBehaviour
{

    [SerializeField] private GameObject lightObjects;

    public void SetLightIntensity(float value)
    {
        Light lightPart = lightObjects.GetComponent<Light>();
        if(lightPart != null) {
            SettingManager.Instance.Brightness = value;
            lightPart.intensity = value;
        }
    }
}