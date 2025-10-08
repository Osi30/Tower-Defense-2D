using UnityEngine;

public class RoadUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsPanel;

    public void SetStatusSettingsPanel(bool isActive)
    {
        _settingsPanel.SetActive(isActive);
    }
}
