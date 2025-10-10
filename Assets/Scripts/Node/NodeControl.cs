using System.Threading.Tasks;
using Assets.Scripts.LevelManagement.UI;
using Assets.Scripts.Tower;
using Assets.Scripts.UI;
using UnityEngine;

public class NodeControl : OpenPanel
{
    [SerializeField]
    private UILevel _uiLevel;
    [SerializeField]
    private GameObject _placeToChoose;
    [SerializeField]
    private DefenseTowerData _defenseTowerData;
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private int[] _towerCoin = { 500, 650, 750 };

    private int _towerType = -1;
    public int GetTowerType => _towerType;

    public async void ApplyTower(int id)
    {
        // Close Node
        _placeToChoose.SetActive(false);
        CloseChoicePanel();

        // Instantiate Tower
        await InstantiateTower(id);
    }

    public void BuyTower(int id)
    {
        Debug.Log("Buy");
        int coinId = id == 0 ? id : id / 2;
        int coin = _towerCoin[coinId];

        if (_uiLevel.IsEnoughCoin(coin))
        {
            _uiLevel.UpdateCoin(-coin);
            _towerType = id;
            ApplyTower(id);
        }
    }

    private async Task InstantiateTower(int id)
    {
        Debug.Log("Instantiate");

        var tower = _defenseTowerData.GetTowerById(id).InstantiateAsync(_parent);
        await tower.Task;

        // Set postion
        var towerGO = tower.Result;
        towerGO.transform.position = _parent.position;
        towerGO.SetActive(true);

        // Setup Event (Upgrade / Sold)
        var control = towerGO.GetComponent<TowerControl>();
        control.UpgradeCanvas.overrideSorting = true;
        control.UpgradeCanvas.sortingOrder = 2;
        control.OnSoldEvent += OnSoleTower;
        control.OnUpgradeEvent += OnUpgradeTower;
    }

    public void OnSoleTower(int coin)
    {
        // Update Current Coin
        _uiLevel.UpdateCoin(coin);

        // Activate Node to choose
        _placeToChoose.SetActive(true);

        // Set Tower to -1
        _towerType = -1;
    }

    public async Task<bool> OnUpgradeTower(int id, int coin)
    {
        // Validate Coin
        if (!_uiLevel.IsEnoughCoin(coin)) return false;

        // Update Current Coin
        _uiLevel.UpdateCoin(coin);

        // Instantiate Tower
        _towerType = id;
        await InstantiateTower(id);

        return true;
    }
}
