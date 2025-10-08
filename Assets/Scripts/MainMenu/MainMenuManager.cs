using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startPanel;  
    public GameObject authPanel;  

    private void Start()
    {
        // Đảm bảo không bị NullReference
        if (startPanel == null || authPanel == null)
        {
            Debug.LogError("⚠️ Một hoặc nhiều panel chưa được gán trong Inspector.");
            return;
        }

        ShowStartPanel();
    }

    // --- Panel switching ---
  
    public void ShowStartPanel()
    {
        SetActivePanel(startPanel);
    }

    public void ShowAuthPanel()
    {
        SetActivePanel(authPanel);
    }

    /// <summary>
    /// Hàm tiện ích để bật 1 panel và tắt 2 panel còn lại
    /// </summary>
    private void SetActivePanel(GameObject panelToShow)
    {
        startPanel.SetActive(panelToShow == startPanel);
        authPanel.SetActive(panelToShow == authPanel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit requested");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Chỉ hoạt động trong Editor
#endif
    }
}
