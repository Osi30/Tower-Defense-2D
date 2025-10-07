using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMapManagerWorld : MonoBehaviour
{
    [Tooltip("Kéo các LevelNodeWorld theo thứ tự 1..N")]
    public LevelNodeWorld[] nodes;

    void Start()
    {
        RefreshAll();
    }

    void RefreshAll()
    {
        int maxU = SaveSystem.GetMaxUnlocked();
        for (int i = 0; i < nodes.Length; i++)
        {
            int idx = i + 1;
            bool unlocked = idx <= maxU;
            int stars = SaveSystem.GetStars(idx);
            nodes[i].Setup(idx, unlocked, stars);
        }
    }

    void OnClickLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("current_level", levelIndex);
        PlayerPrefs.Save();
        // chuyển sang scene chơi thật
        SceneManager.LoadScene("GameScene");
    }

    // Gọi từ GameScene khi thắng
    public static void CompleteLevel(int levelIndex, int stars)
    {
        SaveSystem.SetStars(levelIndex, stars);
        SaveSystem.SetMaxUnlocked(levelIndex + 1);
    }
}
