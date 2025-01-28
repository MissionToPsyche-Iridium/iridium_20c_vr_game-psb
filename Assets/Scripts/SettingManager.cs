using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    static private bool continuesTurn = true;
    static private bool snapTurn = false;
    static private bool eventMode = true;
    static private bool normalMode = false;

        public static bool continuesTurn
    {
        get { return continuesTurn; }
        set { continuesTurn = value; }
    }

        public static bool snapTurn
    {
        get { return snapTurn; }
        set { snapTurn = value; }
    }

        public static bool eventMode
    {
        get { return eventMode; }
        set { eventMode = value; }
    }
    
        public static bool normalMode
    {
        get { return normalMode; }
        set { normalMode = value; }
    }
}
