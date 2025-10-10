using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
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
    const string KEY_LAST_MUSIC = "LastMusicVolume";
    const string KEY_LAST_SFX = "LastSFXVolume";

    void Start()
    {
        if (!audioMixer || !masterVolumeSlider || !fullscreenToggle)
        {
            Debug.LogError("Missing UI or AudioMixer in OptionManager.");
            return;
        }

        float master = PlayerPrefs.GetFloat(KEY_MASTER, 1f);
        float music = PlayerPrefs.GetFloat(KEY_MUSIC, 1f);
        float sfx = PlayerPrefs.GetFloat(KEY_SFX, 1f);

        masterVolumeSlider.value = master;
        if (musicVolumeSlider) musicVolumeSlider.value = music;
        if (sfxVolumeSlider) sfxVolumeSlider.value = sfx;

        bool musicMuted = PlayerPrefs.GetInt(KEY_MUSIC_MUTED, 0) == 1;
        bool sfxMuted = PlayerPrefs.GetInt(KEY_SFX_MUTED, 0) == 1;

        if (musicToggle) musicToggle.SetIsOnWithoutNotify(!musicMuted);
        if (sfxToggle) sfxToggle.SetIsOnWithoutNotify(!sfxMuted);

        OnMasterVolumeChanged(master);

        if (musicMuted) MuteMusicNow();
        else OnMusicVolumeChanged(music);

        if (sfxMuted) MuteSfxNow();
        else OnSFXVolumeChanged(sfx);

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    //Event change sounds
    public void OnMasterVolumeChanged(float value)
    {
        SetMixer01("Master", value);
        if (AudioManager.Instance) AudioManager.Instance.SetMasterVolume(value);
        PlayerPrefs.SetFloat(KEY_MASTER, value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        SetMixer01("Music", value);
        if (AudioManager.Instance) AudioManager.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat(KEY_MUSIC, value);

        // nếu kéo slider > 0 trong khi đang mute → tự bật toggle
        if (musicToggle && !musicToggle.isOn && value > 0.001f)
            musicToggle.SetIsOnWithoutNotify(true);
    }

    public void OnSFXVolumeChanged(float value)
    {
        SetMixer01("SFX", value);
        if (AudioManager.Instance) AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat(KEY_SFX, value);

        if (sfxToggle && !sfxToggle.isOn && value > 0.001f)
            sfxToggle.SetIsOnWithoutNotify(true);
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            float restore = PlayerPrefs.GetFloat(KEY_LAST_MUSIC,
                               musicVolumeSlider ? musicVolumeSlider.value : 1f);
            if (musicVolumeSlider) musicVolumeSlider.value = restore;
            OnMusicVolumeChanged(restore);
            PlayerPrefs.SetInt(KEY_MUSIC_MUTED, 0);
        }
        else
        {
            float current = musicVolumeSlider ? musicVolumeSlider.value : 1f;
            PlayerPrefs.SetFloat(KEY_LAST_MUSIC, current);
            MuteMusicNow();
            PlayerPrefs.SetInt(KEY_MUSIC_MUTED, 1);
        }
    }

    public void ToggleSFX(bool isOn)
    {
        if (isOn)
        {
            float restore = PlayerPrefs.GetFloat(KEY_LAST_SFX,
                               sfxVolumeSlider ? sfxVolumeSlider.value : 1f);
            if (sfxVolumeSlider) sfxVolumeSlider.value = restore;
            OnSFXVolumeChanged(restore);
            PlayerPrefs.SetInt(KEY_SFX_MUTED, 0);
        }
        else
        {
            float current = sfxVolumeSlider ? sfxVolumeSlider.value : 1f;
            PlayerPrefs.SetFloat(KEY_LAST_SFX, current);
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
}
