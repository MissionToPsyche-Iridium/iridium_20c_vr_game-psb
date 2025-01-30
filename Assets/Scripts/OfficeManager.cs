using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OfficeManager : MonoBehaviour
{
    public GameTimer timer;
    public GameObject timerCanvas;
    public GameObject textTimer;
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    // Start is called before the first frame update
    void Awake()
    {
        if(SettingManager.Instance.EventMode == false) {
            timer.enabled = false;
            timerCanvas.SetActive(false);
            textTimer.SetActive(false);
        }

        if(SettingManager.Instance.ContinuousTurn == false) {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
