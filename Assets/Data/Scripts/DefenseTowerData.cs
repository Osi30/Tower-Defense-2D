using UnityEngine;

[CreateAssetMenu(fileName = "Default Towers", menuName = "Tower/Default")]
public class DefenseTowerData : ScriptableObject
{
    [SerializeField]
    private GameObject[] _towerPrefabs;

    public GameObject GetTowerById(int id)
    {
        return _towerPrefabs[id];
    }
}
