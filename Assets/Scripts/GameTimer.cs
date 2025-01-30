using UnityEngine;
using TMPro; // If using TextMeshPro
using UnityEngine.SceneManagement; // For scene management

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 120; // 2 mins
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        // Start the timer automatically when the game begins
        if(SettingManager.Instance.EventMode == false) {
            timerIsRunning = false;
        }else{
            timerIsRunning = true;
        }

    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                EndGame(); // Call the function to end the game
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the text with a formatted time string
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        // Load the Game Over scene
        SceneManager.LoadScene("GameOverScene");
    }
}

