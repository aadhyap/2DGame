using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clip-----")]
    public AudioClip titleMusic;
    public AudioClip background;
    public AudioClip kiss;
    public AudioClip[] complimentClips;
    public AudioClip fireball;
    public AudioClip[] enemyVoiceClips;
    public AudioClip knightVoice;

    private int lastComplimentIndex = -1;
    private int lastEnemyVoiceIndex = -1;
    [SerializeField] private float sfxVolumeMultiplier = 1.5f;

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

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
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
        if (clip == null) return;
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
{
    if (clip == null) return;
    SFXSource.PlayOneShot(clip, sfxVolumeMultiplier);
}

    public void PlayRandomCompliment()
    {
        AudioClip clip = GetRandomClipNoRepeat(complimentClips, ref lastComplimentIndex);
        PlaySFX(clip);
    }

    public void PlayRandomEnemyVoice()
    {
        AudioClip clip = GetRandomClipNoRepeat(enemyVoiceClips, ref lastEnemyVoiceIndex);
        PlaySFX(clip);
    }

    private AudioClip GetRandomClipNoRepeat(AudioClip[] clips, ref int lastIndex)
    {
        if (clips == null || clips.Length == 0)
            return null;

        if (clips.Length == 1)
        {
            lastIndex = 0;
            return clips[0];
        }

        int newIndex;
        do
        {
            newIndex = Random.Range(0, clips.Length);
        }
        while (newIndex == lastIndex);

        lastIndex = newIndex;
        return clips[newIndex];
    }
}