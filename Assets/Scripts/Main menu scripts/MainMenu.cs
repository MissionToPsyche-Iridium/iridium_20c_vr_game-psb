using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Image black;
    [SerializeField] private Animator anim;
    public void PlayGame()
    {
        StartCoroutine(PlayFade());
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Office");
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


    IEnumerator PlayFade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a==1);

    }
}
