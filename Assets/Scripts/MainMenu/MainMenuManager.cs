using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startPanel;   // Start Panel
    public GameObject authPanel;    // Auth Panel
    public GameObject mainMenuPanel; // Main Menu Panel
    public GameObject optionsPanel; //Option Panel
    private void Start()
    {
        // Đảm bảo không bị NullReference
        if (startPanel == null || authPanel == null || mainMenuPanel == null)
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

    public void ShowMainMenuPanel()
    {
        SetActivePanel(mainMenuPanel);
    }

    /// <summary>
    /// Hàm tiện ích để bật 1 panel và tắt 2 panel còn lại
    /// </summary>
    private void SetActivePanel(GameObject panelToShow)
    {
        startPanel.SetActive(panelToShow == startPanel);
        authPanel.SetActive(panelToShow == authPanel);
        mainMenuPanel.SetActive(panelToShow == mainMenuPanel);
    }

    // --- Main menu actions ---
    public void PlayGame()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        // Kiểm tra để tránh vượt quá số scene trong Build Settings
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.LogError("⚠️ Không có scene tiếp theo trong Build Settings.");
        }
    }

    public void OpenOptions()
    {
       optionsPanel.SetActive(true);
       mainMenuPanel.SetActive(false);
    }
    public void BackToMenu()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
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
