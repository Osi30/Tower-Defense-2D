using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip winClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // giữ lại khi load scene khác
        }
        else
        {
            Destroy(gameObject); // tránh tạo trùng
        }
    }

    void Start()
    {
        if (musicClip != null)
        {
            musicAudioSource.clip = musicClip;
            musicAudioSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip != null)
        {
            vfxAudioSource.PlayOneShot(sfxClip);
        }
    }
}
