using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio; // Required for AudioMixer

public class GetKeyCode : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color32 mNormalColor = Color.white;  // Default key color
    public Color32 mDownColor = Color.grey;   // Pressed color
    private Image buttonImage;  // Key Image
    private string buttonString;  // Key text
    private Text showString;  // Text displayed on key

    private InputField inputTarget;
    private bool toLowLetterCase;
    private readonly CultureInfo cult = new CultureInfo("en-US", false);
    public GameObject safeDoor;
    public GameObject key;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftHandPause;
    public GameObject rightHandPause;
    public GameObject safeCollider;
    public AudioSource correctAudioSource; 
    public AudioSource incorrectAudioSource; 

    private void Awake()
    {
        buttonImage = GetComponent<Image>();

        if (TryGetComponent(out Button button)) 
        {
            button.transition = Selectable.Transition.None; // Disable Unity button transition
        }

        buttonString = transform.name;
        showString = transform.Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        if (toLowLetterCase == LetterCaseDetection.Lowercase)
            return;

        toLowLetterCase = LetterCaseDetection.Lowercase;

        if (Regex.IsMatch(buttonString, "^[a-zA-Z0-9]*$") &&
            !(string.Equals(buttonString, "delete") || string.Equals(buttonString, "clear") ||
              string.Equals(buttonString, "backward") || string.Equals(buttonString, "forward") ||
              string.Equals(buttonString, "Letter case") || string.Equals(buttonString, "To0") ||
              string.Equals(buttonString, "ToLast")))
        {
            buttonString = toLowLetterCase ? buttonString.ToLower(cult) : buttonString.ToUpper(cult);
            showString.text = buttonString;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = mDownColor; // Set key color when pressed
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.color = mNormalColor; // Reset key color when released
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string target = GetInputFieldTarget.SelectInputFieldName;
        int index = GetInputFieldTarget.Index;

        if (inputTarget == null)
            inputTarget = GameObject.Find(target)?.GetComponent<InputField>();

        if (inputTarget == null) return;

        if (inputTarget.gameObject.name != target)
            inputTarget = GameObject.Find(target)?.GetComponent<InputField>();

        string targetText = inputTarget.text;

        // Handle regular number/letter input
        if (!(string.Equals(buttonString, "delete") || string.Equals(buttonString, "enter")))
        {
            // Append text to the end instead of inserting at index
            inputTarget.text = targetText + buttonString;
            GetInputFieldTarget.Index = inputTarget.text.Length;
        }
        else
        {
            switch (buttonString.ToLower())
            {
                case "delete":
                    if (inputTarget.text.Length > 0)
                    {
                        // Remove the last character
                        inputTarget.text = inputTarget.text.Substring(0, inputTarget.text.Length - 1);
                        GetInputFieldTarget.Index = inputTarget.text.Length;
                    }
                    break;

                case "enter":
                    if (inputTarget.text.Length > 0)
                    {
                        if (inputTarget.text.Equals("2017"))
                        {
                            key.SetActive(true);
                            StartCoroutine(RotateSafeDoor(safeDoor, new Vector3(0, 0, -90), 1f));
                            Transform parentToDestroy = transform.parent.parent.parent;
                            Destroy(parentToDestroy.gameObject);
                            leftHand.SetActive(!leftHand.activeSelf);
                            rightHand.SetActive(!rightHand.activeSelf);
                            leftHandPause.SetActive(!leftHandPause.activeSelf);
                            rightHandPause.SetActive(!rightHandPause.activeSelf);
                            leftHand.transform.position = leftHandPause.transform.position;
                            rightHand.transform.position = rightHandPause.transform.position;
                            inputTarget.textComponent.color = Color.green;
                            correctAudioSource.Play();
                            Destroy(safeCollider);
                        }
                        else
                        {
                            incorrectAudioSource.Play();
                            inputTarget.textComponent.color = Color.red;
                            StartCoroutine(ResetTextColor());
                            inputTarget.text = "";
                            GetInputFieldTarget.Index = 0;
                        }
                    }
                    break;
            }
        }
    }


    private IEnumerator ResetTextColor()
    {
        yield return new WaitForSeconds(1f);  //Wait for 1 second
        inputTarget.textComponent.color = Color.white; // Reset text color to white
    }
    private IEnumerator RotateSafeDoor(GameObject door, Vector3 rotationAngles, float duration)
{
    Quaternion initialRotation = door.transform.rotation;
    Quaternion targetRotation = initialRotation * Quaternion.Euler(rotationAngles);
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        door.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure the final rotation is set
    door.transform.rotation = targetRotation;
}
}
