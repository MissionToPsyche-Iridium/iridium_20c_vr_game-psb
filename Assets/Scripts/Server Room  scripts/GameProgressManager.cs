using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    public bool isPuzzleComplete = false;
    public bool isJigsawComplete = false;

    private void Awake()
    {
        // Ensure there's only one instance of GameProgressManager
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Force both mini-games to start as incomplete (false)
        isPuzzleComplete = false;
        isJigsawComplete = false;
    }

    public bool AreBothMinigamesComplete()
    {
        //Debug.Log("isPuzzleComplete: " + isPuzzleComplete);
        //Debug.Log("isJigsawComplete: " + isJigsawComplete);
        return isPuzzleComplete && isJigsawComplete;
    }
}