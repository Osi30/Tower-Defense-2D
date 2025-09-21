
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyPool : BasePool<EnemyControl>
{
    /// <summary>
    /// Get Enemies From Pool
    /// </summary>
    /// <param name="number"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<List<EnemyControl>> GetEnemies(int number)
    {
        Debug.Log("GetEnemies");
        return await base.GetPoolElements(number);
    }

    public async Task<EnemyControl> GetOneEnemy()
    {
        List<EnemyControl> enemies = await GetEnemies(1);
        return enemies.First();
    }
}

