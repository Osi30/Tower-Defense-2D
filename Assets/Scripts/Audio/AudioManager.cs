using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("References")]
    public AudioLibrary audioLibrary;
    public AudioMixer audioMixer;
    public AudioSource musicAudioSource; // Output -> Music
    public AudioSource sfxAudioSource;   // Output -> SFX

    [Header("Default Keys")]
    public string defaultBgmKey = "bgm_main";

    [Header("Mixer Exposed Params (must match AudioMixer)")]
    public string masterParam = "MasterVolume";
    public string musicParam  = "MusicVolume";
    public string sfxParam    = "SFXVolume";

    void Awake()
    {
        if (Instance == null)

        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitVolumesFromPrefs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (!string.IsNullOrEmpty(defaultBgmKey))
            PlayMusic(defaultBgmKey, true);
    }

    // ========= Music =========
    public void PlayMusic(string key, bool restartIfSame = false)
    {
        if (audioLibrary == null || musicAudioSource == null) return;
        if (!audioLibrary.TryGet(key, out var entry) || entry.type != SoundType.Music)
        {
            Debug.LogWarning($"[AudioManager] Music key not found or not Music: {key}");
            return;
        }

        if (!restartIfSame && musicAudioSource.clip == entry.clip && musicAudioSource.isPlaying) return;

        musicAudioSource.clip = entry.clip;
        musicAudioSource.loop = entry.loop;
        musicAudioSource.volume = entry.volume;
        musicAudioSource.pitch = entry.pitch;
        musicAudioSource.Play();
    }

    public void StopMusic()
    {
        if (musicAudioSource) musicAudioSource.Stop();
    }

    // ========= SFX =========
    public void PlaySFX(string key)
    {
        if (audioLibrary == null || sfxAudioSource == null) return;
        if (!audioLibrary.TryGet(key, out var entry) || entry.type != SoundType.SFX)
        {
            Debug.LogWarning($"[AudioManager] SFX key not found or not SFX: {key}");
            return;
        }

        sfxAudioSource.pitch = entry.pitch;
        sfxAudioSource.PlayOneShot(entry.clip, entry.volume);
    }

    public void PlaySFX(AudioClip clip, float vol = 1f, float pitch = 1f)
    {
        if (!clip || !sfxAudioSource) return;
        sfxAudioSource.pitch = pitch;
        sfxAudioSource.PlayOneShot(clip, vol);
    }

    // ========= Volumes =========
    public void SetMasterVolume(float linear01)
    {
        SetMixer01(masterParam, linear01);
        PlayerPrefs.SetFloat("MasterVolume", linear01);
    }

    public void SetMusicVolume(float linear01)
    {
        SetMixer01(musicParam, linear01);
        PlayerPrefs.SetFloat("MusicVolume", linear01);
    }

    public void SetSFXVolume(float linear01)
    {
        SetMixer01(sfxParam, linear01);
        PlayerPrefs.SetFloat("SFXVolume", linear01);
    }

    public void MuteMusic(bool mute)
    {
        SetMusicVolume(mute ? 0.0001f : PlayerPrefs.GetFloat("MusicVolume", 1f));
        PlayerPrefs.SetInt("MusicMuted", mute ? 1 : 0);
    }

    public void MuteSFX(bool mute)
    {
        SetSFXVolume(mute ? 0.0001f : PlayerPrefs.GetFloat("SFXVolume", 1f));
        PlayerPrefs.SetInt("SFXMuted", mute ? 1 : 0);
    }

    private void InitVolumesFromPrefs()
    {
        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float music  = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx    = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMasterVolume(master);
        SetMusicVolume(music);
        SetSFXVolume(sfx);

        if (PlayerPrefs.GetInt("MusicMuted", 0) == 1) MuteMusic(true);
        if (PlayerPrefs.GetInt("SFXMuted", 0) == 1) MuteSFX(true);
    }

    private void SetMixer01(string exposedParam, float linear01)
    {
        if (!audioMixer || string.IsNullOrEmpty(exposedParam)) return;
        float v = Mathf.Clamp(linear01, 0.0001f, 1f);
        float dB = Mathf.Log10(v) * 20f;
        audioMixer.SetFloat(exposedParam, dB);
    }
}
