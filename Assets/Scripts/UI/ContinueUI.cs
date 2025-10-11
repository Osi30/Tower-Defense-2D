

using Assets.Scripts.Security;
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
            AudioManager.Instance.PlaySFX("ButtonClick");
            _panel.SetActive(false);
        }

        public void Accept()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneController.LoadScene(_level + 1);
        }

        public void Refuse()
        {
            APICaller.Instance.DeleteGameProgress();
            ClosePanel();
        }
    }
}
