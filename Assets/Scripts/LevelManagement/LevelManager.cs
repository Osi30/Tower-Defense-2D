using System.Collections.Generic;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.LevelManagement.UI;
using Assets.Scripts.Security;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField]
        private int _level;
        [SerializeField]
        private SpawnManager[] _spawnManagers;
        [SerializeField]
        private LevelData _levelData;
        [SerializeField]
        private NodeControl[] _towerPlaces;

        [Header("UI")]
        [SerializeField]
        private UILevel _uiLevel;
        [SerializeField]
        private EndUI _endUI;


        private int _currentWaveLevel = 0;

        [SerializeField]
        private int _currentEnemy;
        private bool _isLoading = true;

        public bool IsLoading => _isLoading;

        private async void Awake()
        {
            // Init Events
            InitializeEvents();

            // Call Api to Get LevelData By Level
            var levelData = await APICaller.Instance.GetGameLevelByLevel(_level);

            if (levelData == null)
            {
                // End Level
                Debug.Log("Level Data is Null");
                return;
            }

            // Update Game Progress (if any)
            GameProgress gameProgress = GameManager.Instance.UserData.gameProgress;
            if (gameProgress != null && gameProgress.waveId != 0)
            {
                RemovePlayedWave(levelData);
                UpdateGameProgress(gameProgress.towerplaces);
                _uiLevel.Initialize(gameProgress);
            }
            else
            {
                _uiLevel.Initialize(levelData);
            }

            _levelData = levelData;


            _isLoading = false;

            SpawnEnemy();
        }

        private void RemovePlayedWave(LevelData levelData)
        {
            List<WaveData> waves = levelData.waves;
            int currentWaveId = GameManager.Instance.UserData.gameProgress.waveId;

            if (currentWaveId == 0) return;

            for (int i = 0; i < levelData.waves.Count; i++)
            {
                if (waves[i].id == currentWaveId)
                {
                    break;
                }
                else
                {
                    levelData.waves.RemoveAt(i);
                }
            }
        }

        private void UpdateGameProgress(List<TowerPlace> towerPlaces)
        {
            for (int i = 0; i < _towerPlaces.Length; i++)
            {
                if (towerPlaces[i].towerType != -1)
                    _towerPlaces[i].ApplyTower(towerPlaces[i].towerType);
            }
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
            if (_currentWaveLevel == _levelData.waves.Count)
            {
                // End Level
                Debug.Log("End Level");
                WinGameLevel();
                return;
            }

            // Start Spawn System
            WaveData waveData = _levelData.waves[_currentWaveLevel];

            // Update Progress
            if (_currentWaveLevel != 0)
            {
                UpdateGameProgress(waveData.id);
            }

            _currentEnemy = waveData.totalEnemy;
            _uiLevel.UpdateWaveLevel(waveData.waveLevel);

            List<Spawnpoint> spawnpoints = waveData.spawnpoints;
            for (int i = 0; i < spawnpoints.Count; i++)
            {
                _spawnManagers[i].Initialize(spawnpoints[i]);
            }

            // After Spawn
            _currentWaveLevel++;
        }

        private async void UpdateGameProgress(int waveId)
        {
            List<TowerPlace> towerPlaces = new();
            for (int i = 0; i < _towerPlaces.Length; i++)
            {
                towerPlaces.Add(new TowerPlace()
                {
                    node = i + 1,
                    towerType = _towerPlaces[i].GetTowerType
                });
            }

            GameProgress gameProgress = new()
            {
                currentCoin = _uiLevel.GetCoin,
                currentHeart = _uiLevel.GetHeart,
                customerId = GameManager.Instance.UserData.id,
                waveId = waveId,
                currentPoint = 0,
                towerplaces = towerPlaces
            };
            var result = await APICaller.Instance.UpdateGameProgress(gameProgress);

            // Update Successfully
            if (result)
            {
                Debug.Log("Update Success");
            }
            // Update Failed
            else
            {
                Debug.Log("Update Fail");
            }
        }

        public void OnEnemyDefeated(string enemyType)
        {
            _currentEnemy--;

            int addCoin = 0;

            switch (enemyType)
            {
                case "Slime":
                    addCoin = 50;
                    break;
                case "Goblin":
                    addCoin = 75;
                    break;
                default:
                    addCoin = 100;
                    break;
            }

            _uiLevel.UpdateCoin(addCoin);

            if (_currentEnemy <= 0)
            {
                // Start Next Wave
                SpawnEnemy();
            }
        }

        public void OnEnemyArrive(string enemyType)
        {
            _uiLevel.UpdateHeart(-1);
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

            if (_uiLevel.GetHeart == 0)
            {
                // Lose Game Level
                LoseGameLevel();
                return;
            }

            if (_currentEnemy <= 0)
            {
                // Start Next Wave
                SpawnEnemy();
            }
        }

        private void WinGameLevel()
        {
            Debug.Log("Win");

            UserData userData = GameManager.Instance.UserData;

            // Result after game
            ResultLevel result = new()
            {
                customerId = userData.id,
                star = _uiLevel.GetHeart / (_levelData.heart / 3),
                point = 0,
                gameLevelId = _level
            };

            // Reward after game
            Inventory inventory = userData.inventory;
            inventory.customerId = userData.id;
            inventory.upgradePoint += 10;

            int thunderSkill = Random.Range(0, 3);
            int boomSkill = Random.Range(0, 3);

            inventory.thunderSkill += thunderSkill;
            inventory.boomSkill += boomSkill;

            // Update
            UpdateResultLevel(result);
            UpdateInventory(inventory);
            _endUI.OpenChoicePanel();
            _endUI.InitPanel("VICTORY", result.point, result.star, thunderSkill, boomSkill);
        }

        private void LoseGameLevel()
        {
            UserData userData = GameManager.Instance.UserData;

            // Result after game
            ResultLevel result = new()
            {
                customerId = userData.id,
                star = 0,
                point = 0,
                gameLevelId = 0
            };

            // Inventory after game
            Inventory inventory = userData.inventory;
            inventory.customerId = userData.id;
            // need to update the thunderskill and boom skill here

            UpdateResultLevel(result);
            UpdateInventory(inventory);

            _endUI.OpenChoicePanel();
            _endUI.InitPanel("GAMEOVER", 0, 0, 0, 0);
        }

        private async void UpdateResultLevel(ResultLevel result)
        {
            bool isSuccess = await APICaller.Instance.CreateResultLevel(result);

            if (isSuccess)
            {
                GameManager.Instance.UserData.resultLevels.Add(result);
            }
        }

        private async void UpdateInventory(Inventory inventory)
        {
            bool result = await APICaller.Instance.UpdateInventory(inventory);

            if (result)
            {
                GameManager.Instance.UserData.inventory = inventory;
            }
        }
    }
}
