using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputField))]
public class InputFieldDetection : MonoBehaviour
{
    private InputField myselfInputField;            // myself component - InputField
    private Text inputFieldText;                    // myself component in InputField - Text
    private InputField.LineType inputFieldLineType; // the lineType of InputField component

    private void Awake()
    {
        if (myselfInputField == null)
            myselfInputField = GetComponent<InputField>();
        
        if (inputFieldText == null)
            inputFieldText = myselfInputField.textComponent;
        
        inputFieldLineType = myselfInputField.lineType;

        // Disable interaction with the InputField
        myselfInputField.interactable = false;

        // Connect the InputField to the keyboard on load
        GetInputFieldTarget.SelectInputFieldName = transform.name;
    }

    private void Update()
    {
        UpdateCharacterIndex();
    }

    private void UpdateCharacterIndex()
    {
        Vector2 localMousePos;
        Vector2 screenMousePos = Mouse.current.position.ReadValue();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(inputFieldText.rectTransform, screenMousePos, null, out localMousePos);
        GetInputFieldTarget.Index = GetCharacterIndexFromPosition(localMousePos, inputFieldText, inputFieldLineType);
        
#if(UNITY_EDITOR)
        print($"localMousePos = ({localMousePos.x},{localMousePos.y})");
        print("index = " + GetInputFieldTarget.Index);
#endif
    }

    private int GetCharacterIndexFromPosition(Vector2 pos, Text text, InputField.LineType lineType)
    {
        TextGenerator gen = text.cachedTextGenerator;

        if (gen.lineCount == 0)
        {
            print("cachedTextGenerator = 0");
            return 0;
        }

        int line = GetUnclampedCharacterLineFromPosition(pos, gen, lineType, text);
        if (line < 0)
            return 0;
        if (line >= gen.lineCount)
            return gen.characterCountVisible;

        int startCharIndex = gen.lines[line].startCharIdx;
        int endCharIndex = GetLineEndPosition(gen, line);

        for (int i = startCharIndex; i < endCharIndex; i++)
        {
            if (i >= gen.characterCountVisible)
                break;

            UICharInfo charInfo = gen.characters[i];
            Vector2 charPos = charInfo.cursorPos / text.pixelsPerUnit;

            float distToCharStart = pos.x - charPos.x;
            float distToCharEnd = charPos.x + (charInfo.charWidth / text.pixelsPerUnit) - pos.x;
            if (distToCharStart < distToCharEnd)
                return i;
        }

        return endCharIndex;
    }

    private int GetUnclampedCharacterLineFromPosition(Vector2 pos, TextGenerator generator, InputField.LineType lineType, Text text)
    {
        if (!(lineType == InputField.LineType.MultiLineNewline || lineType == InputField.LineType.MultiLineSubmit))
            return 0;

        float y = pos.y * text.pixelsPerUnit;
        float lastBottomY = 0.0f;

        for (int i = 0; i < generator.lineCount; ++i)
        {
            float topY = generator.lines[i].topY;
            float bottomY = topY - generator.lines[i].height;

            if (y > topY)
            {
                float leading = topY - lastBottomY;
                if (y > topY - 0.5f * leading)
                    return i - 1;
                else
                    return i;
            }

            if (y > bottomY)
                return i;

            lastBottomY = bottomY;
        }

        return generator.lineCount;
    }

    private static int GetLineEndPosition(TextGenerator gen, int line)
    {
        line = Mathf.Max(line, 0);
        if (line + 1 < gen.lines.Count)
            return gen.lines[line + 1].startCharIdx - 1;
        return gen.characterCountVisible;
    }
}