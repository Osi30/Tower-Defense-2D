using System.Collections.Generic;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.LevelManagement.UI;
using Assets.Scripts.Security;
using UnityEngine;

namespace Assets.Scripts.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private int _level;
        [SerializeField]
        private UILevel _uiLevel;
        [SerializeField]
        private SpawnManager[] _spawnManagers;
        [SerializeField]
        private APICaller _apiCaller;
        [SerializeField]
        private LevelData _levelData;


        private int _currentLevel = 0;
        private int _currentEnemy;
        private bool _isLoading = true;

        public bool IsLoading => _isLoading;

        private async void Awake()
        {
            // Init Events
            InitializeEvents();

            // Call Api to Get LevelData By Level
            var levelData = await _apiCaller.GetGameLevelByLevel(_level);

            if (levelData == null)
            {
                // End Level
            }

            _levelData = levelData;
            _uiLevel.Initialize(levelData);

            _isLoading = false;

            SpawnEnemy();
        }

        private void InitializeEvents()
        {
            // Spawn Managers Events
            foreach (var manager in _spawnManagers)
            {
                manager.OnDefeatedEvent += OnEnemyDefeated;
                manager.OnArriveEvent += OnEnemyArrive;
            }
        }

        private void SpawnEnemy()
        {
            if (_currentLevel == _levelData.waves.Count)
            {
                // End Level

            }

            // Start Spawn System
            WaveData waveData = _levelData.waves[_currentLevel];
            _currentEnemy = waveData.totalEnemy;

            List<Spawnpoint> spawnpoints = waveData.spawnpoints;
            for (int i = 0; i < spawnpoints.Count; i++)
            {
                _spawnManagers[i].Initialize(spawnpoints[i]);
            }

            // After Spawn
            _currentLevel++;
        }

        public void OnEnemyDefeated(string enemyType)
        {
            _currentEnemy--;

            switch (enemyType)
            {
                case "Slime":
                    break;
                case "Goblin":
                    break;
                default:
                    break;
            }

            if (_currentEnemy <= 0)
            {
                // Start Next Wave
                SpawnEnemy();
            }
        }

        public void OnEnemyArrive(string enemyType)
        {
            _uiLevel.UpdateHeart(-1);

            switch (enemyType)
            {
                case "Slime":
                    break;
                case "Goblin":
                    break;
                default:
                    break;
            }
        }


    }
}
