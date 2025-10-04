using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.LevelManagement.Dtos;
using UnityEngine;

namespace Assets.Scripts.LevelManagement
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private float _delayEachEnemy = 0.1f;
        [SerializeField]
        private EnemyPool _enemyPool;

        private int _currentSpawn = 0;
        private float _delayAtFirstTime = 0f;
        private float _delayEachSpawn = 0f;
        private List<Spawn> _spawns = new();
        //private Coroutine _spawnCoroutine;

        public async Task Initialize(Spawnpoint spawnpoint)
        {
            _delayAtFirstTime = spawnpoint.delayAtFirstTime;
            _delayEachSpawn = spawnpoint.delayEachSpawn;
            _spawns = spawnpoint.spawns;

            // Start to Spawn
            //_spawnCoroutine = StartCoroutine(Spawn(_delayAtFirstTime));
            await Spawn(_delayAtFirstTime);
        }

        private async Task Spawn(float delayTime = 0f)
        {
            // Delay Time Before Spawn
            //yield return new WaitForSeconds(delayTime);
            await Task.Delay((int)(delayTime * 1000));

            // Spawn
            Spawn spawn = _spawns[_currentSpawn];
            int spawnNumber = 0;
            string enemyType = spawn.enemyType;

            while (spawnNumber != spawn.enemyNumber)
            {
                // Call the enemy pool base on enemy type
                InitializeEnemyAsync();
                //yield return new WaitForSeconds(_delayEachEnemy);
                await Task.Delay((int)(_delayEachEnemy * 1000));
            }

            // Continue to next Spawn
            _currentSpawn++;
            if (_currentSpawn == _spawns.Count - 1)
            {
                // End Spawn
                return;
            }

            //_spawnCoroutine = StartCoroutine(Spawn(_delayEachSpawn));
            await Spawn(_delayEachSpawn);
        }

        private async void InitializeEnemyAsync()
        {
            EnemyControl enemy = await _enemyPool.GetOneEnemy();
            enemy.InitializeEnemy();
        }

        //private void OnDisable()
        //{
        //    StopCoroutine(_spawnCoroutine);
        //}
    }
}
