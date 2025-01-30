using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class GameOverUI : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu"); // Replace with your actual main menu scene name
    }

    public void QuitGame()
    {
        // Quit the application
        //Application.Quit();
        Process.GetCurrentProcess().Kill();

    }

}
