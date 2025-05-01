using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true; // Flag to determine if the screen should fade on start
    public float fadeDuration = 1.0f; // Duration of the fade effect
    public Color fadeColor = Color.black; // Color of the fade effect
    private Renderer screenRenderer; // Reference to the screen's renderer

    [SerializeField] private GameObject self;
    void Start()
    {
        screenRenderer = GetComponent<Renderer>(); // Get the renderer component of the screen
        if (fadeOnStart) // Check if fade on start is enabled
        {
            FadeIn(); // Start the fade-in effect
        }
    }
    public void FadeIn()
    {
        // Start the fade-in coroutine
        Fade(1.0f, 0.0f);
    }

    public void FadeOut()
    {
        // Start the fade-out coroutine
        Fade(0.0f, 1.0f);
    }
    public void Fade(float alphaIn, float alphaOut)
    {
        // Start the fade coroutine
        StartCoroutine(FadeCoroutine(alphaIn, alphaOut));
        StartCoroutine(DelayDestroy(2.0f)); // Start the delay destroy coroutine
    }


    public IEnumerator FadeCoroutine(float alphaIn, float alphaOut)
    {
    float timer = 0.0f; // Timer to track the fade duration
    while (timer <= fadeDuration)
    {
        Color newColor = fadeColor; // Set the new color to the fade color
        newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration); // Interpolate the alpha value
        screenRenderer.material.SetColor("_Color", newColor); // Set the color of the screen's material
        timer += Time.deltaTime; // Increment the timer
        yield return null; // Wait for the next frame
    }
        Color newColor2 = fadeColor; // Set the new color to the fade color
        newColor2.a = alphaOut; // Set the final alpha value
        screenRenderer.material.SetColor("_Color", newColor2); // Set the color of the screen's material
    }

    public IEnumerator DelayDestroy(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        Destroy(self); // Destroy the screen object
    }
}
