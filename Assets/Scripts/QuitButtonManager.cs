using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class QuitButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public InputActionProperty showButton;


    public void NextScene()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void closingSettingMenu()
    {
        pauseMenu.transform.position = settingsMenu.transform.position;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        showButton.action.Enable();
    }

}
