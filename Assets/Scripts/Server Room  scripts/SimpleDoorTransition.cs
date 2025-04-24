using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleDoorScript : MonoBehaviour
{
    public Renderer doorRenderer;
    public Color glowColor = Color.green;
    private Material doorMat;
    private bool isReadyToEnter = false;

    void Start()
    {
        if (doorRenderer == null)
        {
            Debug.LogError("DoorRenderer not assigned! Please assign the Door's MeshRenderer.");
            return;
        }

        doorMat = doorRenderer.material;

        // Ensure glow is OFF at start
        doorMat.DisableKeyword("_EMISSION");
        doorMat.SetColor("_EmissionColor", Color.black);
        isReadyToEnter = false;
        Debug.Log("Emission is disabled and set to black");
    }

    void Update()
    {
        Debug.Log("isReadyToEnter in update thing: " + isReadyToEnter);
        if (!isReadyToEnter && GameProgressManager.Instance.AreBothMinigamesComplete())
        {
            isReadyToEnter = true;
            EnableDoorGlow();
        }
    }

    void EnableDoorGlow()
    {
        if (doorMat != null)
        {
            doorMat.EnableKeyword("_EMISSION");
            doorMat.SetColor("_EmissionColor", glowColor);
            DynamicGI.SetEmissive(doorRenderer, glowColor); // Optional
            Debug.Log("Glow enabled, emission color set to: " + glowColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReadyToEnter && GameProgressManager.Instance.AreBothMinigamesComplete() && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Left Hand") || other.gameObject.CompareTag("Right Hand")))
        {
            Debug.Log("Player entered the door.");
            SceneManager.LoadScene("HallwayScene");
        }
    }
}
