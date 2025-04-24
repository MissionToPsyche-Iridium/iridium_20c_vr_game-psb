using UnityEngine;
using TMPro; // If using TextMeshPro
using UnityEngine.SceneManagement; // For scene management

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 120; // 2 mins
    public bool timerIsRunning = false;

    public CollisionPopup collision;
    [SerializeField] private TextMeshProUGUI timerText;
    private void Start()
    {
        // Start the timer automatically when the game begins
        timerIsRunning = true;
        if(SceneManager.GetActiveScene().name=="HallwayScene")
            timeRemaining=120;

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
                if(SceneManager.GetActiveScene().name=="Office")
                    EndGame(); 
                else 
                {
                    if(SceneManager.GetActiveScene().name=="HallwayScene")
                    // {   if(collision.complete==true)
                    //     {
                    //         Debug.Log("GameEndedHallway");
                    //         EndGame();
                    //     }
                    //     else
                    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    //     }// Call the function to end the game
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the text with a formatted time string
        if(SceneManager.GetActiveScene().name=="HallwayScene")
        timerText.text=  "Get to the end! Don't hit any obstacles!\nTime remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        else
        timerText.text = "Time remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void EndGame()
    {
        // Load the Game Over scene
        SceneManager.LoadScene("credits");
    }
}

