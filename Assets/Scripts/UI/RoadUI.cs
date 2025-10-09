using UnityEngine;

public class RoadUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsPanel;
    [SerializeField]
    private GameObject _upgradePanel;

    public void SetStatusSettingsPanel(bool isActive)
    {
        _settingsPanel.SetActive(isActive);
    }
}
