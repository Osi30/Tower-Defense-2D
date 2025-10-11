

using Assets.Scripts.Security;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PausePanel : OpenPanel
    {
        public void Restart()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            Time.timeScale = 1f;
            APICaller.Instance.DeleteGameProgress();
            SceneController.Restart();
        }

        public void Home()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            Time.timeScale = 1f;
            APICaller.Instance.DeleteGameProgress();
            SceneController.LoadRoadMap();
        }

        public void OpenPausePanel()
        {
            OpenChoicePanel();
            Time.timeScale = 0f;
        }

        public void CloseOpenPausePanel()
        {
            Time.timeScale = 1f;
            CloseChoicePanel();
        }
    }
}
