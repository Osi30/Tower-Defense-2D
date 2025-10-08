
using System.Threading.Tasks;
using Assets.Scripts.LevelManagement.Dtos;
using Assets.Scripts.Security;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Test
{
    public class TestCallApi : MonoBehaviour
    {
        [SerializeField]
        private bool isCall = false;
        [SerializeField]
        private APICaller api;

        private async void Start()
        {
            await Call();
        }

        private async Task Call()
        {
            int level = await api.GetLevelByWaveId(1);
            Debug.Log("Level: " + level);
        }
    }
}
