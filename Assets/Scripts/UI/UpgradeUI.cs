

using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UpgradeUI : OpenPanel
    {
        [SerializeField]
        private TextMeshProUGUI _upgradePointText;
        [SerializeField]
        private TextMeshProUGUI _thunderSkill;
        [SerializeField]
        private TextMeshProUGUI _boomSkill;
        [SerializeField]
        private Image _attackSpeed;
        [SerializeField]
        private Image _range;
        [SerializeField]
        private Image _damage;


        private Inventory _initInventory;
        private Inventory _currentInventory;

        public void InitializePanel()
        {
            var inventory = GameManager.Instance.UserData.inventory;

            _initInventory = new Inventory
            {
                upgradePoint = inventory.upgradePoint,
                attackSpeed = inventory.attackSpeed,
                range = inventory.range,
                damage = inventory.damage,
                thunderSkill = inventory.thunderSkill,
                boomSkill = inventory.boomSkill,
            };
            _currentInventory = new Inventory
            {
                upgradePoint = inventory.upgradePoint,
                attackSpeed = inventory.attackSpeed,
                range = inventory.range,
                damage = inventory.damage,
                thunderSkill = inventory.thunderSkill,
                boomSkill = inventory.boomSkill,
            };
            UpdateTechnique(_currentInventory);
        }

        private void UpdateTechnique(Inventory inventory)
        {
            inventory.upgradePoint = inventory.upgradePoint < 0 ? 0 : inventory.upgradePoint;
            _upgradePointText.text = inventory.upgradePoint.ToString();
            _thunderSkill.text = "x" + inventory.thunderSkill.ToString();
            _boomSkill.text = "x" + inventory.boomSkill.ToString();
            _attackSpeed.fillAmount = inventory.attackSpeed / 10f;
            _range.fillAmount = inventory.range / 10f;
            _damage.fillAmount = inventory.damage / 10f;
        }

        public void ResetInventory()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            _currentInventory.upgradePoint = _initInventory.upgradePoint;
            _currentInventory.range = _initInventory.range;
            _currentInventory.damage = _initInventory.damage;
            _currentInventory.attackSpeed = _initInventory.attackSpeed;
            UpdateTechnique(_currentInventory);
        }

        public void AddTechnique(string technique)
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            if (_currentInventory.upgradePoint <= 0)
            {
                return;
            }

            switch (technique)
            {
                case "AttackSpeed":
                    if (_currentInventory.attackSpeed < 10)
                    {
                        _currentInventory.attackSpeed++;
                        _currentInventory.upgradePoint--;
                    }
                    break;
                case "Range":
                    if (_currentInventory.range < 10)
                    {
                        _currentInventory.range++;
                        _currentInventory.upgradePoint--;
                    }
                    break;
                default:
                    if (_currentInventory.damage < 10)
                    {
                        _currentInventory.damage++;
                        _currentInventory.upgradePoint--;
                    }
                    break;
            }

            UpdateTechnique(_currentInventory);
        }

        public async void Save()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");

            var userData = GameManager.Instance.UserData;

            var inventory = userData.inventory;
            inventory.customerId = userData.id;
            inventory.range = _currentInventory.range;
            inventory.damage = _currentInventory.damage;
            inventory.attackSpeed = _currentInventory.attackSpeed;
            inventory.upgradePoint = _currentInventory.range + _currentInventory.damage + _currentInventory.attackSpeed;

            var result = await APICaller.Instance.UpdateInventory(inventory);
            if (result)
            {
                GameManager.Instance.UserData.inventory = _currentInventory;
            }
        }

    }
}
