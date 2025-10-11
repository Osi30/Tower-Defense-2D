using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EndUI : OpenPanel
    {
        [SerializeField]
        private TextMeshProUGUI _resultText;
        [SerializeField]
        private TextMeshProUGUI _pointText;
        [SerializeField]
        private TextMeshProUGUI _thunderSkillText;
        [SerializeField]
        private TextMeshProUGUI _boomSkillText;
        [SerializeField]
        private Image[] _stars;
        [SerializeField]
        private Sprite _starOn;

        public void LoadHome()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneController.LoadRoadMap();
            Time.timeScale = 1f;
        }

        public void Restart()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneController.Restart();
            Time.timeScale = 1f;
        }

        public void InitPanel(string result, int point, int stars, int thunderSkill, int boomSkill)
        {
            if (result.Equals("VICTORY")) AudioManager.Instance.PlaySFX("Victory");
            else AudioManager.Instance.PlaySFX("Gameover");

            _resultText.text = result;
            _pointText.text = point.ToString();
            _thunderSkillText.text = "x" + thunderSkill.ToString();
            _boomSkillText.text = "x" + boomSkill.ToString();

            for (int i = 0; i < stars; i++)
            {
                _stars[i].sprite = _starOn;
            }
            Time.timeScale = 0f;
        }
    }
}
