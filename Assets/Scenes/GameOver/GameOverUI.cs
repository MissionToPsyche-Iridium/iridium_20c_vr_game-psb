using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public void GoToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu"); // Replace with your actual main menu scene name
    }
    public void QuitGame()
    {
        //work pleasae
        // Quit the application
        Debug.Log("Quit Game"); // This will log a message in the Editor for testing purposes
        Application.Quit();
    }
}

