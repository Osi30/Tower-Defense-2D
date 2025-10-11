using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Assets.Scripts.UI;

public class OptionManager : OpenPanel
{
    [Header("Audio")]
    private AudioMixer audioMixer;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    [Header("Graphics")]
    public Toggle fullscreenToggle;

    //Preference key
    const string KEY_MASTER = "MasterVolume";
    const string KEY_MUSIC = "MusicVolume";
    const string KEY_SFX = "SFXVolume";
    const string KEY_MUSIC_MUTED = "MusicMuted";
    const string KEY_SFX_MUTED = "SFXMuted";

    private void Awake()
    {
        audioMixer = AudioManager.Instance.audioMixer;
    }

    void Start()
    {
        if (!audioMixer || !fullscreenToggle)
        {
            Debug.LogError("Missing UI or AudioMixer in OptionManager.");
            return;
        }

        float master = PlayerPrefs.GetFloat(KEY_MASTER, 1f);
        float music = PlayerPrefs.GetFloat(KEY_MUSIC, 1f);
        float sfx = PlayerPrefs.GetFloat(KEY_SFX, 1f);

        bool musicMuted = PlayerPrefs.GetInt(KEY_MUSIC_MUTED, 0) == 1;
        bool sfxMuted = PlayerPrefs.GetInt(KEY_SFX_MUTED, 0) == 1;

        if (musicToggle) musicToggle.SetIsOnWithoutNotify(!musicMuted);
        if (sfxToggle) sfxToggle.SetIsOnWithoutNotify(!sfxMuted);

        if (musicMuted) MuteMusicNow();

        if (sfxMuted) MuteSfxNow();

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            UnmuteMusic();
            PlayerPrefs.SetInt(KEY_MUSIC_MUTED, 0);
        }
        else
        {
            MuteMusicNow();
            PlayerPrefs.SetInt(KEY_MUSIC_MUTED, 1);
        }
    }

    public void ToggleSFX(bool isOn)
    {
        if (isOn)
        {
            UnmuteSFX();
            PlayerPrefs.SetInt(KEY_SFX_MUTED, 0);
        }
        else
        {
            MuteSfxNow();
            PlayerPrefs.SetInt(KEY_SFX_MUTED, 1);
        }
    }

    public void ToggleFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
    }

    //helpers
    void SetMixer01(string param, float linear01)
    {
        if (!audioMixer) return;
        float v = Mathf.Clamp(linear01, 0.0001f, 1f);
        float dB = Mathf.Log10(v) * 20f;
        audioMixer.SetFloat(param, dB);
    }

    void MuteMusicNow()
    {
        SetMixer01("Music", 0.0001f);
        if (AudioManager.Instance) AudioManager.Instance.SetMusicVolume(0.0001f);
    }
    void MuteSfxNow()
    {
        SetMixer01("SFX", 0.0001f);
        if (AudioManager.Instance) AudioManager.Instance.SetSFXVolume(0.0001f);
    }

    void UnmuteMusic()
    {
        SetMixer01("Music", 1f);
        if (AudioManager.Instance) AudioManager.Instance.SetMusicVolume(1f);
    }

    void UnmuteSFX()
    {
        SetMixer01("SFX", 1f);
        if (AudioManager.Instance) AudioManager.Instance.SetSFXVolume(1f);
    }
}
