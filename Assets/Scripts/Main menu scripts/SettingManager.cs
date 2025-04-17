using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance;

    private bool continuousTurn = true;
    private bool eventMode = true;
    [SerializeField] private AudioMixer mixer;
    private float brightness = 1.5f;
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

    public AudioMixer Mixer
    {
        get { return mixer; }
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
            SettingManager.Instance.Mixer.SetFloat("MusicVolume", 0f);
            SettingManager.Instance.Mixer.SetFloat("SfxVolume", 0f);
            SettingManager.Instance.Mixer.SetFloat("MasterVolume", 0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}