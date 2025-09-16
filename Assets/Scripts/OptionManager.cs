using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionManager : MonoBehaviour
{
    [Header("Audio")]   
    public AudioMixer audioMixer; // Dùng để chỉnh volume
    public Slider masterVolumeSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;
    public GameObject mainMenuPanel; // Main Menu Panel

    [Header("Graphics")]
    public Toggle fullscreenToggle;

    void Start()
    {
        // Kiểm tra null trước khi sử dụng các thành phần UI
        if (masterVolumeSlider == null || fullscreenToggle == null)
        {
            Debug.LogError("⚠️ UI components are not assigned in the Inspector.");
            return;
        }

        // Load settings (nếu có PlayerPrefs)
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float vol = PlayerPrefs.GetFloat("MasterVolume");
            masterVolumeSlider.value = vol;
            SetMasterVolume(vol);
        }
        else
        {
            Debug.Log("No saved MasterVolume found. Using default value.");
        }

        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetMasterVolume(float volume)
    {
        if (audioMixer == null)
        {
            Debug.LogError("⚠️ AudioMixer is not assigned in the Inspector.");
            return;
        }

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); // dB
        PlayerPrefs.SetFloat("MasterVolume", volume);
        Debug.Log($"Master volume set to {volume}");
    }

    public void ToggleMusic(bool isOn)
    {
        Debug.Log($"Music toggled: {(isOn ? "On" : "Off")}");
        // Có thể mute/unmute AudioSource của nhạc nền
    }

    public void ToggleSFX(bool isOn)
    {
        Debug.Log($"SFX toggled: {(isOn ? "On" : "Off")}");
        // Có thể mute/unmute AudioSource của hiệu ứng
    }

    public void ToggleFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
        Debug.Log($"Fullscreen toggled: {(isOn ? "Enabled" : "Disabled")}");
    }

   
}