

using System.Collections.Generic;

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class Spawnpoint
    {
        public int Id { get; set; }
        public float DelayAtFirstTime { get; set; }
        public float DelayEachSpawn { get; set; }
        public WaveData Wave { get; set; }
        public List<Spawn> Spawns { get; set; }
    }
}
