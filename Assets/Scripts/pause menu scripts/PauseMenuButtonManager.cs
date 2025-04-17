using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class PauseMenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHandPause;
    [SerializeField] private GameObject rightHandPause;
    [SerializeField] private InputActionProperty showButton;

    public void MainMenuScene() {
        SceneManager.LoadScene("MainMenu");
        SettingManager.Instance.ContinuousTurn = true;
        SettingManager.Instance.EventMode = true; 
        Time.timeScale = 1.0f;
    }

    public void closingSettingMenu() {
        pauseMenu.transform.position = settingsMenu.transform.position;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        showButton.action.Enable();
    }

    public void setUpSettingMenu() {
        settingsMenu.transform.position = pauseMenu.transform.position;
        settingsMenu.transform.rotation = pauseMenu.transform.rotation;
        showButton.action.Disable();
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void disablePauseMenu() {
        showButton.action.Disable();
        pauseMenu.SetActive(false);
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        leftHandPause.SetActive(false);
        rightHandPause.SetActive(false);
        leftHandPause.transform.position = leftHand.transform.position;
        rightHandPause.transform.position = rightHand.transform.position;
        Time.timeScale = 1.0f;
        showButton.action.Enable();
    }
}
