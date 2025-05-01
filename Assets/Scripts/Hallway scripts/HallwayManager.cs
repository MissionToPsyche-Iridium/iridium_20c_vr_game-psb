using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class HallwayManager : MonoBehaviour
{
    [SerializeField] private ActionBasedSnapTurnProvider snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurn;

    void Start()
    {
        if (SettingManager.Instance.ContinuousTurn == false)
        {
            continuousTurn.enabled = false;
            snapTurn.enabled = true;
        } 
    }

}
