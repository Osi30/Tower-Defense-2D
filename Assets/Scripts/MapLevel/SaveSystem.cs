using UnityEngine;

public static class SaveSystem
{
    public static int GetMaxUnlocked() => Mathf.Max(1, PlayerPrefs.GetInt("max_unlocked", 1));
    public static void SetMaxUnlocked(int lv)
    {
        if (lv > GetMaxUnlocked()) { PlayerPrefs.SetInt("max_unlocked", lv); PlayerPrefs.Save(); }
    }

    public static int GetStars(int lv) => PlayerPrefs.GetInt($"lv_{lv}_stars", 0);
    public static void SetStars(int lv, int stars)
    {
        PlayerPrefs.SetInt($"lv_{lv}_stars", Mathf.Clamp(stars, 0, 3));
        PlayerPrefs.Save();
    }
}
