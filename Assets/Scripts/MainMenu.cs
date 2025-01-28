using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject titleMenu;
    [SerializeField] public GameObject optionsMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene("Office");
    }
    public void GoOptions()
    { 
        optionsMenu.SetActive(true);
        titleMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
