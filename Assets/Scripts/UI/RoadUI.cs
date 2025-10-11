using UnityEngine;

public class RoadUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsPanel;

    public void SetStatusSettingsPanel(bool isActive)
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        _settingsPanel.SetActive(isActive);
    }
}
