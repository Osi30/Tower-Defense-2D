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

    public async void ChooseTower(int id)
    {
        // Close Node
        _placeToChoose.SetActive(false);
        CloseChoicePanel();

        // Instantiate Tower
        await InstantiateTower(id);
    }

    private async Task InstantiateTower(int id)
    {
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
    }

    public async Task OnUpgradeTower(int id, int coin)
    {
        // Update Current Coin
        _uiLevel.UpdateCoin(coin);

        // Instantiate Tower
        await InstantiateTower(id);
    }
}
