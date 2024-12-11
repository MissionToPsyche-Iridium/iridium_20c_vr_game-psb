using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public void NextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void closingSettingMenu()
    {
        pauseMenu.transform.position = settingsMenu.transform.position;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

}
