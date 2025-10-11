

using UnityEngine;

namespace Assets.Scripts.UI
{
    public class OpenPanel : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _panel;

        public void OpenChoicePanel()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            _panel.gameObject.SetActive(true);
            _panel.alpha = 1;
        }

        public void CloseChoicePanel()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            _panel.alpha = 0;
            _panel.gameObject.SetActive(false);
        }

        public void SetActivePanel()
        {
            if (_panel.gameObject.activeSelf) CloseChoicePanel();
            else OpenChoicePanel();
        }
    }
}
