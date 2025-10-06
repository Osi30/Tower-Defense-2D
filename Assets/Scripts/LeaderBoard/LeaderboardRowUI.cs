using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LeaderboardRowUI : MonoBehaviour
{
    public TextMeshProUGUI rankText, nameText, scoreText, timeText, levelText;
    public Image[] starImages;
    public Sprite starOn, starOff;
    public GameObject goldCrown, silverCrown, bronzeCrown;
    public Image background;
    public Color evenColor = new(1, 1, 1, 0.08f), oddColor = new(1, 1, 1, 0.02f), meColor = new(1f, 1f, 0.4f, 0.20f);

    public void Setup(int rank, LeaderboardEntry e, string myName)
    {
        rankText.text = rank.ToString();
        nameText.text = e.playerName;
        scoreText.text = e.score.ToString("N0");
        timeText.text = TimeSpan.FromSeconds(e.timeSeconds).ToString(@"m\:ss");
        levelText.text = $"Lv {e.levelIndex}";

        for (int i = 0; i < starImages.Length; i++)
            starImages[i].sprite = i < e.stars ? starOn : starOff;

        if (goldCrown) goldCrown.SetActive(rank == 1);
        if (silverCrown) silverCrown.SetActive(rank == 2);
        if (bronzeCrown) bronzeCrown.SetActive(rank == 3);

        background.color = (e.playerName == myName) ? meColor : (rank % 2 == 0 ? evenColor : oddColor);
    }
}
