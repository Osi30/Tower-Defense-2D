using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Assets.Scripts.LevelManagement.Dtos;

public class LeaderboardRowUI : MonoBehaviour
{
    public TextMeshProUGUI rankText, nameText, scoreText;
    public GameObject goldCrown, silverCrown, bronzeCrown;
    public Image background;
    public Color evenColor = new(1, 1, 1, 0.08f), oddColor = new(1, 1, 1, 0.02f), meColor = new(1f, 1f, 0.4f, 0.20f);

    public void Setup(int rank, UserData userData, string myName)
    {
        rankText.text = rank.ToString();
        nameText.text = userData.username;
        scoreText.text = userData.point.ToString("N0");

        if (goldCrown) goldCrown.SetActive(rank == 1);
        if (silverCrown) silverCrown.SetActive(rank == 2);
        if (bronzeCrown) bronzeCrown.SetActive(rank == 3);

        background.color = (userData.username == myName) ? meColor : (rank % 2 == 0 ? evenColor : oddColor);
    }
}
