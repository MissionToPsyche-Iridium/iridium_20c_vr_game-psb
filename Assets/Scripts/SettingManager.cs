using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    static private bool _continuesTurn = true;
    static private bool _snapTurn = false;
    static private bool _eventMode = true;
    static private bool _normalMode = false;

        public static bool continuesTurn
    {
        get { return _continuesTurn; }
        set { _continuesTurn = value; }
    }

        public static bool snapTurn
    {
        get { return _snapTurn; }
        set { _snapTurn = value; }
    }

        public static bool eventMode
    {
        get { return _eventMode; }
        set { _eventMode = value; }
    }
    
        public static bool normalMode
    {
        get { return _normalMode; }
        set { _normalMode = value; }
    }
}
