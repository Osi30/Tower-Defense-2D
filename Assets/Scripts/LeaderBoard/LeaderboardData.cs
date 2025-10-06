using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int levelIndex;
    public int score;            // 100..1000
    public float timeSeconds;    // thời gian dọn màn
    public int stars;            // 0..3
    public long timestamp;       // để tie-break
}
[Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new();
}