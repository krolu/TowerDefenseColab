using System;
using System.Collections.Generic;
using System.Drawing;
using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases.GameLevels
{
    public class GameLevelSettings
    {
        public int LevelNumber { get; set; }
        public TimeSpan SpawnFrequency { get; set; }
        public IEnumerable<EnemyTypeEnum> EnemyTypesToSpawn { get; set; }
        public List<PointF> Waypoints { get; set; }
        public Point SpawnPoint { get; set; }
        public decimal StartingResources { get; set; }
    }
}