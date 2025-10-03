

using Assets.Scripts.LevelManagement.Dtos;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.LevelManagement.UI
{
    public class UILevel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _heart;

        [SerializeField]
        private TextMeshProUGUI _point;

        [SerializeField]
        private TextMeshProUGUI _coin;

        [SerializeField]
        private TextMeshProUGUI _waveLevel;

        public void UpdateHeart(int heart) => _heart.text = (int.Parse(_heart.text) + heart).ToString();
        public void UpdatePoint(int point) => _point.text = (int.Parse(_point.text) + point).ToString();
        public void UpdateCoin(int coin) => _coin.text = (int.Parse(_coin.text) + coin).ToString();
        public void UpdateWaveLevel(int waveLevel) => _waveLevel.text = waveLevel.ToString();

        public void Initialize(LevelData levelData)
        {
            _heart.text = levelData.Heart.ToString();
            _coin.text = levelData.Coin.ToString();
            _point.text = "0";
            _waveLevel.text = "0";
        }

    }
}
