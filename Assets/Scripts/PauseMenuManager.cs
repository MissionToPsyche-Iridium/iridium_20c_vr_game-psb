using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 3;
    public GameObject menu;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftHandPause;
    public GameObject rightHandPause;
    public InputActionProperty showButton;
        [SerializeField] float x = 0.0f;

    [SerializeField] float y = 0.0f;
        [SerializeField] float z = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            leftHand.SetActive(!leftHand.activeSelf);
            rightHand.SetActive(!rightHand.activeSelf);
            leftHandPause.SetActive(!leftHandPause.activeSelf);
            rightHandPause.SetActive(!rightHandPause.activeSelf);
            leftHandPause.transform.position = leftHand.transform.position;
            rightHandPause.transform.position = rightHand.transform.position;
            if (menu.activeSelf)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
            menu.transform.position = head.position + new Vector3(head.forward.x+x ,y,head.forward.z+z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y,head.position.z));
        menu.transform.forward *= -1;
    }
}
