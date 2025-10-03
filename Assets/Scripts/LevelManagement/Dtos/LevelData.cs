using System.Collections.Generic;

namespace Assets.Scripts.LevelManagement.Dtos
{
    [System.Serializable]
    public class LevelData
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int Coin { get; set; }
        public int Heart { get; set; }
        public List<WaveData> WaveDatas { get; set; }
    }
}
