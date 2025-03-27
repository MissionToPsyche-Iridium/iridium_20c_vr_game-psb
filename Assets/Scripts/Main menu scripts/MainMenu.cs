using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private GameObject optionsMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene("Office");
    }

    public void GoOptions()
    { 
        optionsMenu.SetActive(true);
        titleMenu.SetActive(false);
    }

    public void GoTitle()
    { 
        optionsMenu.SetActive(false);
        titleMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void setTypeFromIndex(int index)
    {
        if (index == 0)
        {
            SettingManager.Instance.ContinuousTurn = true;
        }
        else if (index == 1)
        {
            SettingManager.Instance.ContinuousTurn = false;
        }
    }

    public void setTypeToggle(bool value)
    {
        if (value)
        {
            SettingManager.Instance.EventMode = true;
        }
        else 
        {
            SettingManager.Instance.EventMode = false;
        }
    }
}
