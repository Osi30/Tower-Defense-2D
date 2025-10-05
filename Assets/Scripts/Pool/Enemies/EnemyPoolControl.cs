
using UnityEngine;

namespace Assets.Scripts.Pool.Enemies
{
    public class EnemyPoolControl : MonoBehaviour
    {
        [SerializeField]
        private EnemyPool[] enemyPools;

        public EnemyPool GetEnemyPool(string enemyType)
        {
            return enemyType switch
            {
                "Slime" => enemyPools[0],
                "Goblin" => enemyPools[1],
                _ => enemyPools[2],
            };
        }
    }
}
