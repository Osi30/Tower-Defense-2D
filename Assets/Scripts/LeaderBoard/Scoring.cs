using UnityEngine;

public static class Scoring
{
    // ?i?m 100..1000. parTime = th?i gian chu?n c?a màn (t? ??t trong level).
    public static int ComputeScore(float timeSeconds, float parTime, int starsFromRules)
    {
        int baseScore = 250;                           // có th? ch?nh
        float t = Mathf.Clamp01(1f - (timeSeconds / (parTime * 2f))); // nhanh h?n 2×parTime ? 0..1
        int speedBonus = Mathf.RoundToInt(650 * t);    // 0..650
        int starBonus = starsFromRules == 3 ? 100 : (starsFromRules == 2 ? 50 : (starsFromRules == 1 ? 20 : 0));
        int total = baseScore + speedBonus + starBonus;
        return Mathf.Clamp(total, 100, 1000);
    }

    // Quy ??i ?i?m ra sao (b?n ??i m?c tùy ı)
    public static int StarsFromScore(int score)
    {
        if (score >= 800) return 3;   // yêu c?u
        if (score >= 600) return 2;
        if (score >= 300) return 1;
        return 0;
    }
}
