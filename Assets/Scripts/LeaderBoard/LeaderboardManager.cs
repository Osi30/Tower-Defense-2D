using System.Linq;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    const string KEY = "leaderboard_v1";
    public LeaderboardData data = new();

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); Load(); }
        else Destroy(gameObject);
    }

    public void AddScore(string playerName, int levelIndex, int score, float timeSeconds, int stars)
    {
        data.entries.Add(new LeaderboardEntry
        {
            playerName = string.IsNullOrEmpty(playerName) ? "Player" : playerName,
            levelIndex = levelIndex,
            score = Mathf.Clamp(score, 100, 1000),
            timeSeconds = Mathf.Max(0, timeSeconds),
            stars = Mathf.Clamp(stars, 0, 3),
            timestamp = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        });
        SortAndTrim();
        Save();
    }

    public System.Collections.Generic.List<LeaderboardEntry> GetSorted() =>
        data.entries.OrderByDescending(e => e.score)
                    .ThenBy(e => e.timeSeconds)
                    .ThenBy(e => e.timestamp).ToList();

    void SortAndTrim()
    {
        data.entries = GetSorted();
        if (data.entries.Count > 200) data.entries = data.entries.Take(200).ToList(); // giữ top 200
    }

    void Save()
    {
        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
    }

    void Load()
    {
        var json = PlayerPrefs.GetString(KEY, "");
        if (!string.IsNullOrEmpty(json)) data = JsonUtility.FromJson<LeaderboardData>(json);
    }

    [ContextMenu("Clear Leaderboard")]
    void Clear() { data = new LeaderboardData(); Save(); }
}
