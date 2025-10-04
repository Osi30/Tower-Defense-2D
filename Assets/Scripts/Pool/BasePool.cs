
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BasePool<V> : MonoBehaviour
    where V : BasePoolMember
{
    [SerializeField]
    private int _poolSize;

    [SerializeField]
    private AssetReference _asset;

    [SerializeField]
    private Transform _parent;

    protected StaticPool<V> _pool;

    protected async void Start()
    {
        await InitPool(_poolSize);
    }

    private async Task<List<V>> InitPool(int poolSize)
    {
        // Return condition
        if (_asset == null || _parent == null)
        {
            Debug.LogWarning($"Missing prefab: {_asset} or parent: {_parent}");
            return null;
        }

        // Create pool if is not initialized yet
        _pool ??= new StaticPool<V>();

        List<V> poolMembers = new();
        for (int i = 0; i < poolSize; i++)
        {
            // Asset Ref help for instantiate async
            var asset = _asset.InstantiateAsync(_parent);
            await asset.Task;

            // Set default state is false
            asset.Result.SetActive(false);

            var member = asset.Result.GetComponent<V>();
            _pool.Add(member);
            poolMembers.Add(member);
        }

        return poolMembers;
    }

    protected async Task<List<V>> GetPoolElements(int number)
    {
        List<V> elementList = new();

        // Get inactive elements
        int missingNumber = 0;
        for (int i = 0; i < number; i++)
        {
            V element = _pool.GetInactiveElement();

            if (element == null) missingNumber++;

            else elementList.Add(element);
        }

        // Init more if there is no inactive elements
        if (missingNumber > 0)
        {
            elementList.AddRange(await InitPool(missingNumber));
        }

        return elementList;
    }

    // Force inactivate all elements
    protected void Clear()
    {
        foreach (var e in _pool.Members)
        {
            e.MarkAsInactive();
        }
    }

    // Force active all elements
    protected void ShowAll(Vector3 position)
    {
        foreach (var e in _pool.Members)
        {
            e.MarkAsActive();
            e.Initialize(position);
        }
    }
}
