using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.Pool.Enemies;
using UnityEngine;

namespace Assets.Scripts.LevelManagement
{
    public class SpawnManager : MonoBehaviour
    {
        public delegate void OnEnemyEvent(string enemyType);
        public OnEnemyEvent OnDefeatedEvent;
        public OnEnemyEvent OnArriveEvent;

        [SerializeField]
        private float _delayEachEnemy = 0.1f;
        [SerializeField]
        private MapPatrolWayPoints _wayPoints;
        [SerializeField]
        private EnemyPoolControl _enemyPoolControl;

        private int _currentSpawn = 0;
        private float _delayAtFirstTime = 0f;
        private float _delayEachSpawn = 0f;
        private List<Spawn> _spawns = new();

        public void Initialize(Spawnpoint spawnpoint)
        {
            _delayAtFirstTime = spawnpoint.delayAtFirstTime;
            _delayEachSpawn = spawnpoint.delayEachSpawn;
            _spawns = spawnpoint.spawns;

            // Start to Spawn
            Spawn(_delayAtFirstTime);
        }

        private async void Spawn(float delayTime = 0f)
        {
            // Delay Time Before Spawn
            await Task.Delay((int)(delayTime * 1000));

            // Spawn
            Spawn spawn = _spawns[_currentSpawn];
            int spawnNumber = 0;

            while (spawnNumber < spawn.enemyNumber)
            {
                // Call the enemy pool base on enemy type
                await InitializeEnemyAsync(spawn.enemyType);
                await Task.Delay((int)(_delayEachEnemy * 1000));
                spawnNumber++;
            }

            // Continue to next Spawn
            _currentSpawn++;
            if (_currentSpawn == _spawns.Count)
            {
                // End Spawn
                _currentSpawn = 0;
                return;
            }

            Spawn(_delayEachSpawn);
        }

        /// <summary>
        /// Get enemy from pool, set up way points and events
        /// </summary>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        private async Task InitializeEnemyAsync(string enemyType)
        {
            EnemyControl enemy = await _enemyPoolControl.GetEnemyPool(enemyType).GetOneEnemy();
            enemy.SetWayPoints(_wayPoints);
            if (enemy.OnEnemyDefeated == null) enemy.OnEnemyDefeated += OnEnemyDefeated;
            if (enemy.OnEnemyArrive == null) enemy.OnEnemyArrive += OnEnemyArrive;
            enemy.InitializeEnemy();
        }

        private void OnEnemyDefeated(string enemyType)
        {
            OnDefeatedEvent.Invoke(enemyType);
        }

        private void OnEnemyArrive(string enemyType)
        {
            OnArriveEvent.Invoke(enemyType);
        }
    }
}
