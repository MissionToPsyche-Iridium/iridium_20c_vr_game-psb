using UnityEngine;

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance;

    private bool continuousTurn = true;
    private bool eventMode = true;

    public static SettingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SettingManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<SettingManager>();
                    singletonObject.name = typeof(SettingManager).ToString();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    public bool ContinuousTurn
    {
        get { return continuousTurn; }
        set { continuousTurn = value; }
    }


    public bool EventMode
    {
        get { return eventMode; }
        set { eventMode = value; }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}