using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clip-----")]
    public AudioClip titleMusic;     // 👈 ADD THIS
    public AudioClip background;
    public AudioClip kiss;
    public AudioClip compliment;
    public AudioClip fireball;
    public AudioClip enemy;
    public AudioClip jump;
    public AudioClip hit;

    private void Awake()
    {
        // singleton (prevents duplicates)
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "StartScene")
        {
            PlayMusic(titleMusic);
        }
        else
        {
            PlayMusic(background);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}