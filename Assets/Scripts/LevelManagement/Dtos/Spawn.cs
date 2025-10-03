

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class Spawn
    {
        public int Id { get; set; }
        public int EnemyNumber { get; set; }
        public string EnemyType { get; set; }
        public int Prioroty { get; set; }
        public Spawnpoint Spawnpoint { get; set; }
    }
}
