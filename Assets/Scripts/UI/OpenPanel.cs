

using UnityEngine;

namespace Assets.Scripts.UI
{
    public class OpenPanel : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _panel;

        public void OpenChoicePanel()
        {
            _panel.alpha = 1;
        }

        public void CloseChoicePanel()
        {
            _panel.alpha = 0;
        }
    }
}
