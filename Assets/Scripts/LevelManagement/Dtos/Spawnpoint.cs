

using System.Collections.Generic;

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class Spawnpoint
    {
        public int id;
        public float delayAtFirstTime;
        public float delayEachSpawn;
        public List<Spawn> spawns;
    }
}
