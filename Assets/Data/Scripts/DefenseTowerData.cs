using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Tower Datas
/// </summary>

[CreateAssetMenu(fileName = "Default Towers", menuName = "Tower/Default")]
public class DefenseTowerData : ScriptableObject
{
    [SerializeField]
    private AssetReference[] _towerPrefabs;

    public AssetReference GetTowerById(int id)
    {
        return _towerPrefabs[id];
    }
}
