using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public Transform content;                 // ScrollView/Viewport/Content
    public LeaderboardRowUI rowPrefab;
    public string myPlayerName = "Player";    // có thể lấy từ profile

    void OnEnable() { Refresh(); }

    public void Refresh()
    {
        foreach (Transform t in content) Destroy(t.gameObject);
        var list = LeaderboardManager.Instance.GetSorted();
        for (int i = 0; i < list.Count; i++)
        {
            var row = Instantiate(rowPrefab, content);
            row.Setup(i + 1, list[i], myPlayerName);
        }
    }
}
