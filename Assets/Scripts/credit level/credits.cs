using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene management

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 100f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            // Positive Y moves UP
        }
        if(rectTransform.anchoredPosition.y > 3000) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
