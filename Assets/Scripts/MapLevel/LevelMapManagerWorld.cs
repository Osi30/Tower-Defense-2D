using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.Security;
using Assets.Scripts.UI;
using UnityEngine;

public class LevelMapManagerWorld : MonoBehaviour
{
    [SerializeField]
    private LevelNodeWorld[] nodes;
    [SerializeField]
    private ContinueUI _continuePanel;

    private async void Awake()
    {
        // Play Music
        AudioManager.Instance.PlayMusic(Random.Range(0,2) == 0 ? "bgm_road_01" : "bgm_road_02");

        // Setup Level Nodes
        RefreshLevelNodes();

        // Show Game Progress (if any)
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
            nodes[i].Setup(unlocked, currentLevel - 1 == i ? 0 : resultLevels[i].star);
        }
    }
}
