using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBrightness : MonoBehaviour
{

    [SerializeField] private GameObject[] lightObjects;

    public void SetLightIntensity(float value)
    {
        foreach(GameObject obj in lightObjects)
        {
            Light lightPart = obj.GetComponent<Light>();
            if(lightPart != null) {
                SettingManager.Instance.Brightness = value;
                lightPart.intensity = value;
            }
        }
    }
}