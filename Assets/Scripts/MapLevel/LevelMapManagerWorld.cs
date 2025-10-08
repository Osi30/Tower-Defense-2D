using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.Security;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMapManagerWorld : MonoBehaviour
{
    [SerializeField]
    private LevelNodeWorld[] nodes;
    [SerializeField]
    private ContinueUI _continuePanel;

    private async void Awake()
    {
        RefreshLevelNodes();

        int waveId = GameManager.Instance.UserData.gameProgress.waveId;

        if (waveId != 0)
        {
            // Continue to game progress
            int level = await APICaller.Instance.GetLevelByWaveId(waveId);
            _continuePanel.SetActivePanel(level);
        }
    }

    private void RefreshLevelNodes()
    {
        List<ResultLevel> resultLevels = GameManager.Instance.UserData.resultLevels;
        if (resultLevels == null)
        {
            return;
        }

        int currentLevel = resultLevels.Count + 1;

        for (int i = 0; i < currentLevel; i++)
        {
            bool unlocked = i + 1 <= currentLevel;
            nodes[i].Setup(unlocked, currentLevel == 1 ? 0 : resultLevels[i].star);
        }
    }

    void OnClickLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("current_level", levelIndex);
        PlayerPrefs.Save();
        // chuyển sang scene chơi thật
        SceneManager.LoadScene("GameScene");
    }

    // Gọi từ GameScene khi thắng
    public static void CompleteLevel(int levelIndex, int stars)
    {
        //SaveSystem.SetStars(levelIndex, stars);
        //SaveSystem.SetMaxUnlocked(levelIndex + 1);
    }
}
