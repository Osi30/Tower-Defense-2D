

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
        private TextMeshProUGUI _coin;

        [SerializeField]
        private TextMeshProUGUI _waveLevel;

        private int _point;

        public int GetCoin => int.Parse(_coin.text);
        public int GetHeart => int.Parse(_heart.text);
        public int GetPoint => _point;

        public void Initialize(LevelData levelData)
        {
            _heart.text = levelData.heart.ToString();
            _coin.text = levelData.coin.ToString();

            if (levelData.waves == null || levelData.waves.Count == 0)
                _waveLevel.text = "1";
            else
                _waveLevel.text = levelData.waves[0].waveLevel.ToString();
        }

        public void Initialize(GameProgress gameProgress)
        {
            _heart.text = gameProgress.currentHeart.ToString();
            _coin.text = gameProgress.currentCoin.ToString();
            _point = gameProgress.currentPoint;
        }

        public bool IsEnoughCoin(int coin)
        {
            if (int.TryParse(_coin.text, out int currentCoin))
            {
                return currentCoin >= Mathf.Abs(coin);
            }

            return false;
        }

        #region Update Fields

        public void UpdateHeart(int heart) => _heart.text = (int.Parse(_heart.text) + heart).ToString();
        public void UpdatePoint(int point) => _point += point;
        public void UpdateCoin(int coin)
        {
            int updatedCoin = int.Parse(_coin.text) + coin;
            updatedCoin = updatedCoin < 0 ? 0 : updatedCoin;

            _coin.text = updatedCoin.ToString();
        }
        public void UpdateWaveLevel(int waveLevel) => _waveLevel.text = waveLevel.ToString();

        #endregion
    }
}
