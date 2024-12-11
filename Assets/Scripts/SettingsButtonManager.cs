using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public InputActionProperty showButton;

    public void setUpSettingMenu()
    {
        settingsMenu.transform.position = pauseMenu.transform.position;
        settingsMenu.transform.rotation = pauseMenu.transform.rotation;
        showButton.action.Disable();
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
