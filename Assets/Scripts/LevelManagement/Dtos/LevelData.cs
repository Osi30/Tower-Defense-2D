using System.Collections.Generic;

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class LevelData
    {
        public int id;
        public int level;
        public int coin;
        public int heart;
        public List<WaveData> waves;
    }
}
