using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        //buttonImage.color = mHoverColor; // Temporary hover effect

        string target = GetInputFieldTarget.SelectInputFieldName;
        int index = GetInputFieldTarget.Index;

        if (inputTarget == null)
            inputTarget = GameObject.Find(target)?.GetComponent<InputField>();

        if (inputTarget == null) return;

        if (inputTarget.gameObject.name != target)
            inputTarget = GameObject.Find(target)?.GetComponent<InputField>();

        string targetText = inputTarget.text;

        if (!(string.Equals(buttonString, "delete") ||  string.Equals(buttonString, "enter")))
        {
            inputTarget.text = targetText.Insert(index, buttonString);
            GetInputFieldTarget.Index++;
        }
        else
        {
            switch (buttonString)
            {
                case "delete":
                    if (GetInputFieldTarget.Index > 0)
                    {
                    inputTarget.text = "";
                    GetInputFieldTarget.Index = 0;
                    }
                break;
                case "enter":
                    if (GetInputFieldTarget.Index > 0)
                    {
                       if (inputTarget.text.Equals("1987"))
                       {
                            Debug.Log("Correct");
                          }
                          else
                          {
                            Debug.Log("Incorrect");
                       }
                       
                    }
                break;
            }
        }

        Invoke(nameof(ResetColor), 0.2f);
    }

    private void ResetColor()
    {
        buttonImage.color = mNormalColor; // Ensure color resets after click
    }
}
