using UnityEngine;

public class MainMenuButtonSounds : MonoBehaviour
{
    public static MainMenuButtonSounds Instance;
    public bool isMute = false;

    private AudioSource audioSrc;
    [SerializeField] AudioClip[] sounds;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound, float volume)
    {
        if (!isMute)
        {
            switch (sound)
            {
                case "continuebutton":
                    audioSrc.PlayOneShot(sounds[0], volume);
                    break;
                case "newgamebutton":
                    audioSrc.PlayOneShot(sounds[1], volume);
                    break;
                case "settingbutton":
                    audioSrc.PlayOneShot(sounds[2], volume);
                    break;
                case "quitbutton":
                    audioSrc.PlayOneShot(sounds[3], volume);
                    break;
                default:
                    Debug.LogError("there is no sound with the given name :" + sound);
                    break;
            }
        }
    }
}
