using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.LevelManagement.UI;
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

        //private Coroutine _spawnCoroutine;
        [SerializeField]
        private LevelData _levelData;
        private int _currentLevel = 0;
        private int _currentEnemy;

        private void Awake()
        {
            // Call Api to Get LevelData By Level
            //_levelData = new();
            _uiLevel.Initialize(_levelData);
        }

        private void MockLevel()
        {
            LevelData levelData = new();
            levelData.Level = 1;
            levelData.Coin = 1500;
            levelData.Heart = 15;

            List<WaveData> waveDatas = new List<WaveData>();

            List<Spawnpoint> spawnpoints = new List<Spawnpoint>();  


            waveDatas.Add(new WaveData()
            {
                WaveLevel = 1,
                TotalEnemy = 20
            });
        }

        private void MockWave()
        {

        }

        private async void Start()
        {
            //_spawnCoroutine = StartCoroutine(SpawnEnemy());
            await SpawnEnemy();
        }

        private async Task SpawnEnemy()
        {
            // Start Spawn System
            WaveData waveData = _levelData.WaveDatas[_currentLevel];
            _currentEnemy = waveData.TotalEnemy;

            List<Spawnpoint> spawnpoints = waveData.Spawnpoints;
            for (int i = 0; i < spawnpoints.Count - 1; i++)
            {
                await _spawnManagers[i].Initialize(spawnpoints[i]);
            }

            // After Spawn
            _currentLevel++;

            if (_currentLevel > _levelData.WaveDatas.Count - 1)
            {
                // End Level

            }
        }

        //private void OnDisable()
        //{
        //    StopCoroutine(_spawnCoroutine);
        //}
    }
}
