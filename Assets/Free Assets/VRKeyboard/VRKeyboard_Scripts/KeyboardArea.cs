using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardArea : MonoBehaviour
{
    // triggered by the control ray which is exited the keyboard area

    private Coroutine closeKeyboardCoroutine;       // close keyboard coroutine


    public void OnPointerEnter(PointerEventData eventData)
    {
        if(closeKeyboardCoroutine != null)
            StopCoroutine(closeKeyboardCoroutine);
#if(UNITY_EDITOR)
        print("Keyboard Area Enter");
#endif
    }

    public void OnPointerExit(PointerEventData eventData)
    {
#if(UNITY_EDITOR)
        print("Keyboard Area Exit");
#endif
    }

}
