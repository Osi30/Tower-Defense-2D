

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
        private const string BASE_URL_PLACEHOLDER = "https://towerdefense-2d-api.onrender.com";

        public async Task<LevelData> GetGameLevelByLevelt(int level)
        {
            // Sử dụng chuỗi này để xây dựng URL đầy đủ
            string fullUrl = BASE_URL_PLACEHOLDER + "/api/GameLevel/" + level.ToString();

            // ... (phần còn lại của code)
            UnityWebRequest webRequest = UnityWebRequest.Get(fullUrl);
            await webRequest.SendWebRequest();

            // Success
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;

                try
                {
                    // 1. Deserialization: Chuyển đổi JSON thành đối tượng LevelData
                    LevelData levelData = JsonUtility.FromJson<LevelData>(jsonResponse);

                    // 2. Sử dụng dữ liệu đã map
                    Debug.Log($"✅ Tải thành công Level {levelData.level}");
                    return levelData;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Lỗi khi chuyển đổi JSON: " + e.Message);
                }
            }
            // Fail
            else
            {

            }
            return null;
        }



    }
}
