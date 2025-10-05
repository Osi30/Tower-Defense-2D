
using System.Collections.Generic;

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class WaveData
    {
        public int id;
        public int waveLevel;
        public int totalEnemy;
        public List<Spawnpoint> spawnpoints;
    }
}
