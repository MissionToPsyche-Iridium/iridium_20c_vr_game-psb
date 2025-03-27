using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance;

    private bool continuousTurn = true;
    private bool eventMode = true;
    private AudioMixer audioMixer;
    private float brightness = 10;
    private bool isRayHandActive = false;

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

    public AudioMixer AudioMixer
    {
        get {return audioMixer; }
    }

    public float Brightness
    {
        get { return brightness; }
        set { brightness = value; }
    }

    public bool IsRayHandActive
    {
        get { return isRayHandActive; }
        set { isRayHandActive = value; }
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