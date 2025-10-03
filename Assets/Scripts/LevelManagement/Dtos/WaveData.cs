
using System.Collections.Generic;

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class WaveData
    {
        public int Id { get; set; }
        public int WaveLevel { get; set; }
        public int TotalEnemy { get; set; }
        public LevelData GameLevel { get; set; }
        public List<Spawnpoint> Spawnpoints { get; set; }
    }
}
