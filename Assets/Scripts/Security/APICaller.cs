

using System;
using System.Threading.Tasks;
using Assets.Scripts.LevelManagement.Dtos;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Security
{
    /// <summary>
    /// For Api Calls
    /// </summary>
    public class APICaller : MonoBehaviour
    {
        public async Task<LevelData> GetGameLevelByLevel(int level)
        {
            string fullUrl = BuildConstants.PRODUCTION_URL + "/api/GameLevel/" + level.ToString();

            UnityWebRequest webRequest = UnityWebRequest.Get(fullUrl);
            await webRequest.SendWebRequest();

            // Success
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                try
                {
                    // Deserialization to LevelData
                    LevelData levelData = JsonUtility.FromJson<LevelData>(jsonResponse);
                    return levelData;
                }
                catch (Exception e)
                {
                    Debug.LogError("Error when deserialization: " + e.Message);
                }
            }

            // Fail
            return null;
        }

    }
}
