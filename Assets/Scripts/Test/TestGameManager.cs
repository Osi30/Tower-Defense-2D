using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.LevelManagement.Dtos;
using UnityEngine;

namespace Assets.Scripts.Test
{
    public class TestGameManager : MonoBehaviour
    {
        private void Start()
        {
            UserData us = GameManager.Instance.UserData;
            Debug.Log("User: " + us);
        }
    }
}
