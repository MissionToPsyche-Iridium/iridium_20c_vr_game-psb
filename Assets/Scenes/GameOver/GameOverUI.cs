using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For Button
using UnityEngine.XR.Interaction.Toolkit; // For XR interaction components
using UnityEngine.EventSystems; // For handling UI events

public class GameOverUI : MonoBehaviour
{
    public XRRayInteractor leftInteractor;  // XR Ray Interactor for the left controller
    public XRRayInteractor rightInteractor; // XR Ray Interactor for the right controller

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

    private void Update()
    {
        // Check if the left or right controller is interacting with an object
        if (leftInteractor && leftInteractor.hasSelection)
        {
            HandleButtonPress(leftInteractor);
        }
        else if (rightInteractor && rightInteractor.hasSelection)
        {
            HandleButtonPress(rightInteractor);
        }
    }

    // Handles button press based on the selected UI button
    private void HandleButtonPress(XRRayInteractor interactor)
    {
        // Get the raycast hit information from the interactor
        RaycastHit hit;
        Ray ray = new Ray(interactor.transform.position, interactor.transform.forward);

        if (Physics.Raycast(ray, out hit, 10f)) // Adjust the raycast distance as needed
        {
            // Check if the raycast hits a UI button
            if (hit.collider.CompareTag("UIButton"))
            {
                Button button = hit.collider.GetComponent<Button>();  // Now it will recognize the Button class
                if (button != null)
                {
                    // Trigger the button click via the EventSystem
                    ExecuteEvents.Execute(button.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerClickHandler);
                }
            }
        }
    }
}
