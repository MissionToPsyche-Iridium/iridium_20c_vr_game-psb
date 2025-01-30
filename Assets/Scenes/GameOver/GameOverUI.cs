using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For Button
using UnityEngine.XR.Interaction.Toolkit; // For XR interaction components
using UnityEngine.EventSystems; // For handling UI events

public class GameOverUI : MonoBehaviour
{

    public EventSystem eventSystem; // Reference to the event system

    public void GoToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu"); // Replace with your actual main menu scene name
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit Game"); // This will log a message in the Editor for testing purposes
        Application.Quit();
    }

}
