

using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ContinueUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _panel;

        private int _level;

        public void SetActivePanel(int level)
        {
            _panel.SetActive(true);
            _level = level;
        }

        public void ClosePanel()
        {
            _panel.SetActive(false);
        }

        public void Accept()
        {
            SceneController.LoadScene(_level + 1);
        }

        public void Refuse()
        {
            GameManager.Instance.UserData.gameProgress.waveId = 0;
            ClosePanel();
        }
    }
}
