
using System.Collections.Generic;

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class UserData
    {
        public int id;
        public string username;
        public int point;
        public Inventory inventory;
        public GameProgress gameProgress;
        public List<ResultLevel> resultLevels;
    }

    [System.Serializable]
    public class Inventory
    {
        public int id;
        public int thunderSkill;
        public int boomSkill;
        public int upgradePoint;
        public int attackSpeed;
        public int damage;
        public int range;
        public int customerId;
    }

    [System.Serializable]
    public class GameProgress
    {
        public int id;
        public int currentCoin;
        public int currentHeart;
        public int currentPoint;
        public int waveId;
        public int customerId;
        public List<TowerPlace> towerplaces;
    }

    [System.Serializable]
    public class ResultLevel
    {
        public int id;
        public int star;
        public int point;
        public int gameLevelId;
        public int customerId;
    }

    [System.Serializable]
    public class TowerPlace
    {
        public int id;
        public int node;
        public int towerType;
        public int gameProgressId;
    }
}
