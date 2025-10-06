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
        // ================================
        // 1️⃣ GET: /api/GameLevel/{level}
        // ================================
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

        // ========================================
        // 2️⃣ GET: /api/GameLevel/wave/{waveLevel}
        // ========================================
        public async Task<LevelData[]> GetLevelByWaveLevel(int waveLevel)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/GameLevel/wave/" + waveLevel.ToString();

            UnityWebRequest webRequest = UnityWebRequest.Get(fullUrl);
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                try
                {
                    // JsonUtility không parse được mảng -> dùng JsonHelper
                    return JsonHelper.FromJson<LevelData>(jsonResponse);
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

            return null;
        }

        // ================================
        // 3️⃣ PUT: /api/GameProgress
        // ================================
        public async Task<bool> UpdateGameProgress(string jsonBody)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/GameProgress";

            UnityWebRequest webRequest = new UnityWebRequest(fullUrl, "PUT");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
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

        // ================================
        // 4️⃣ PUT: /api/Inventory
        // ================================
        public async Task<bool> UpdateInventory(string jsonBody)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/Inventory";

            UnityWebRequest webRequest = new UnityWebRequest(fullUrl, "PUT");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
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

        // ================================
        // 5️⃣ POST: /api/ResultLevel
        // ================================
        public async Task<bool> CreateResultLevel(string jsonBody)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/ResultLevel";

            UnityWebRequest webRequest = new UnityWebRequest(fullUrl, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
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
