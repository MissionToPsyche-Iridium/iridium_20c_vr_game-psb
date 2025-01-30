using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public GameTimer timer;
    public GameObject timerCanvas;
    public GameObject textTimer;
    // Start is called before the first frame update
    void Start()
    {
        if(SettingManager.Instance.EventMode == false) {
            timer.enabled = false;
            timerCanvas.SetActive(false);
            textTimer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
