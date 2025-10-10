using System.Threading.Tasks;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Tower
{
    public class TowerControl : OpenPanel
    {
        [SerializeField]
        private int _upgradeCoin;
        [SerializeField]
        private int _sellCoin;
        [SerializeField]
        private TextMeshProUGUI _upgradeCointText;
        [SerializeField]
        private TextMeshProUGUI _sellCoinText;

        public delegate void OnSold(int coin = 0);
        public delegate Task OnUpgrade(int id, int coin = 0);

        public OnSold OnSoldEvent;

        public OnUpgrade OnUpgradeEvent;

        private void Awake()
        {
            if (_upgradeCointText != null) _upgradeCointText.text = _upgradeCoin.ToString();
            _sellCoinText.text = _sellCoin.ToString();
        }

        public async void Upgrade(int id)
        {
            CloseChoicePanel();
            await OnUpgradeEvent.Invoke(id, -_upgradeCoin);
            Destroy(gameObject);
        }

        public void Sold()
        {
            CloseChoicePanel();
            OnSoldEvent.Invoke(_sellCoin);
            Destroy(gameObject);
        }
    }
}
