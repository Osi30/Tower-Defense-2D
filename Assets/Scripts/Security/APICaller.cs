using Assets.Scripts.LevelManagement.Dtos;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Security
{
    /// <summary>
    /// Gọi API cho game TowerDefense
    /// </summary>
    public class APICaller : MonoBehaviour
    {
        public static APICaller Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// GET: /api/GameLevel/{level}
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<LevelData> GetGameLevelByLevel(int level)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/GameLevel/" + level.ToString();

            UnityWebRequest webRequest = UnityWebRequest.Get(fullUrl);
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                try
                {
                    LevelData levelData = JsonUtility.FromJson<LevelData>(jsonResponse);
                    return levelData;
                }
                catch (Exception e)
                {
                    Debug.LogError("Error when deserialization (LevelData): " + e.Message);
                }
            }
            else
            {
                Debug.LogError("Request failed: " + webRequest.error);
            }

            return null;
        }

        /// <summary>
        /// GET: /api/GameLevel/wave/{waveLevel}
        /// </summary>
        /// <param name="waveId"></param>
        /// <returns></returns>
        public async Task<int> GetLevelByWaveId(int waveId)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/GameLevel/wave/" + waveId.ToString();

            UnityWebRequest webRequest = UnityWebRequest.Get(fullUrl);
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                try
                {
                    return int.Parse(jsonResponse);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error when deserialization (LevelData list): " + e.Message);
                }
            }
            else
            {
                Debug.LogError("Request failed: " + webRequest.error);
            }

            return 0;
        }

        /// <summary>
        /// PUT: /api/GameProgress
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public async Task<bool> UpdateGameProgress(GameProgress gameProgress)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/GameProgress";
            string jsonBody = JsonUtility.ToJson(gameProgress);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

            UnityWebRequest webRequest = new(fullUrl, "PUT")
            {
                uploadHandler = new UploadHandlerRaw(bodyRaw),
                downloadHandler = new DownloadHandlerBuffer()
            };
            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Game progress updated successfully");
                return true;
            }
            else
            {
                Debug.LogError("Failed to update game progress: " + webRequest.error);
                return false;
            }
        }

        /// <summary>
        /// PUT: /api/Inventory
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public async Task<bool> UpdateInventory(Inventory inventory)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/Inventory";
            string jsonBody = JsonUtility.ToJson(inventory);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

            UnityWebRequest webRequest = new(fullUrl, "PUT")
            {
                uploadHandler = new UploadHandlerRaw(bodyRaw),
                downloadHandler = new DownloadHandlerBuffer()
            };
            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Inventory updated successfully");
                return true;
            }
            else
            {
                Debug.LogError("Failed to update inventory: " + webRequest.error);
                return false;
            }
        }

        /// <summary>
        /// POST: /api/ResultLevel
        /// </summary>
        /// <param name="resultLevel"></param>
        /// <returns></returns>
        public async Task<bool> CreateResultLevel(ResultLevel resultLevel)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/ResultLevel";
            string jsonBody = JsonUtility.ToJson(resultLevel);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

            UnityWebRequest webRequest = new(fullUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(bodyRaw),
                downloadHandler = new DownloadHandlerBuffer()
            };
            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("ResultLevel created successfully");
                return true;
            }
            else
            {
                Debug.LogError("Failed to create ResultLevel: " + webRequest.error);
                return false;
            }
        }
    }

    // Helper parse JSON array (vì JsonUtility chỉ parse object)
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{\"array\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
}
