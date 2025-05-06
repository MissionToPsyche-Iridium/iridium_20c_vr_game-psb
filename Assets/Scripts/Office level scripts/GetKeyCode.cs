using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio; // Required for AudioMixer
using PrimeTween;
public class GetKeyCode : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color32 mNormalColor = Color.white;  // Default key color
    public Color32 mDownColor = Color.grey;   // Pressed color
    private Image buttonImage;  // Key Image
    private string buttonString;  // Key text
    public Text showString;  // Text displayed on key
    public InputField inputTarget;
    private bool toLowLetterCase;
    private readonly CultureInfo cult = new CultureInfo("en-US", false);
    [SerializeField] private GameObject safeDoor;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHandPause;
    [SerializeField] private GameObject rightHandPause;
    [SerializeField] private GameObject safeCollider;
    [SerializeField] private AudioSource correctAudioSource; 
    [SerializeField] private AudioSource incorrectAudioSource; 
    [SerializeField] private AudioSource keyPress;
    [SerializeField] private AudioSource doorOpen;
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
            keyPress.Play();
            GetInputFieldTarget.Index = inputTarget.text.Length;
        }
        else
        {
            switch (buttonString.ToLower())
            {
                case "delete":
                    if (inputTarget.text.Length > 0)
                    {
                        keyPress.Play();
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
             StartCoroutine(HandleCorrectCode());
         }
         else
         {
             incorrectAudioSource.Play();
             inputTarget.textComponent.color = Color.red;
             StartCoroutine(Delay());
         }
     }
     break;
             }
         }
     }
 
     private IEnumerator Delay()
     {
     yield return new WaitForSeconds(2f); // Wait for 2 seconds
     inputTarget.textComponent.color = Color.white; // Reset text color to white
     inputTarget.text = "";
     GetInputFieldTarget.Index = 0;
     }
     private IEnumerator RotateSafeDoor()
     {
         // Play the door open sound
         doorOpen.Play();
         yield return Tween.Rotation(safeDoor.transform, endValue: Quaternion.Euler(safeDoor.transform.rotation.eulerAngles.x, safeDoor.transform.rotation.eulerAngles.y, safeDoor.transform.rotation.eulerAngles.z - 90), duration: 2);
     }
 
     private IEnumerator HandleCorrectCode()
 {
     key.SetActive(true);
     correctAudioSource.Play();
     // Wait a small amount of time to ensure audio starts playing
     yield return new WaitForSeconds(0.1f);
     
     StartCoroutine(RotateSafeDoor());
     
     // Allow door opening animation and sound to complete
     yield return new WaitForSeconds(2f);
     
     Transform parentToDestroy = transform.parent.parent.parent;
     leftHand.SetActive(!leftHand.activeSelf);
     rightHand.SetActive(!rightHand.activeSelf);
     leftHandPause.SetActive(!leftHandPause.activeSelf);
     rightHandPause.SetActive(!rightHandPause.activeSelf);
     leftHand.transform.position = leftHandPause.transform.position;
     rightHand.transform.position = rightHandPause.transform.position;
     inputTarget.textComponent.color = Color.green;
     Destroy(safeCollider);
     SettingManager.Instance.IsRayHandActive = false;
     Destroy(parentToDestroy.gameObject);
 }
}
