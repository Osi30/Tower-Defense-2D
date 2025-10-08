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

    private int _towerType = -1;
    public int GetTowerType => _towerType;

    public void ChooseTower(int id)
    {
        Debug.Log("Choose");
        _towerType = id;
    }

    public async void ApplyTower(int id)
    {
        // Close Node
        _placeToChoose.SetActive(false);
        CloseChoicePanel();

        // Instantiate Tower
        await InstantiateTower(id);
    }

    public void BuyTower(int coin)
    {
        Debug.Log("Buy");

        while (_towerType == -1)
        {

        }
        if (_uiLevel.IsEnoughCoin(coin))
        {
            _uiLevel.UpdateCoin(-coin);
            ApplyTower(_towerType);
        }
        else
        {
            _towerType = -1;
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
