using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public void setUpSettingMenu()
    {
        settingsMenu.transform.position = pauseMenu.transform.position;
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
