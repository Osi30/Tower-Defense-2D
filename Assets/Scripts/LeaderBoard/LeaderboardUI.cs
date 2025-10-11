using System.Threading.Tasks;
using Assets.Scripts;
using Assets.Scripts.Security;
using Assets.Scripts.UI;
using UnityEngine;

public class LeaderboardUI : OpenPanel
{
    public Transform content;               
    public LeaderboardRowUI rowPrefab; 

    void OnEnable() { Refresh(); }

    public async void Refresh()
    {
        var list = await APICaller.Instance.GetCustomerPoints();
        for (int i = 0; i < list.Length; i++)
        {
            var row = Instantiate(rowPrefab, content);
            row.Setup(i + 1, list[i], GameManager.Instance.UserData.username);
        }
    }
}
