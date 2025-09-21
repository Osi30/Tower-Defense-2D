using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestDeadlock : MonoBehaviour
{
    [SerializeField]
    private bool isTrigger = false;
    [SerializeField]
    private AssetReference _asset;
    [SerializeField]
    private GameObject _assetPrefab;

    private async void Update()
    {
        if (isTrigger)
        {
            _assetPrefab = await InitAsset();
            isTrigger = false;
        }
    }

    private async Task<GameObject> InitAsset()
    {
        var asset = _asset.InstantiateAsync(transform);
        await asset.Task;
        return asset.Result;
    }
}
