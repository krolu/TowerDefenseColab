using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevel : GameLoopMethods
    {
        private readonly TimeSpan _spawnFrequency = TimeSpan.FromSeconds(1);
        private readonly Image _background;
        private readonly List<EnemyBase> _monsters = new List<EnemyBase>();
        private readonly Queue<EnemyTypeEnum> _enemyTypesToSpawn;
        private readonly EnemyFactory _enemyFactory;
        private readonly PointF _location;
        private TimeSpan _lastSpawn = TimeSpan.Zero;
        private readonly Stopwatch _timeSinceStart = new Stopwatch();
        private readonly GamePhaseManager _gamePhaseManager;
        private readonly List<PointF> _waypoints;

        public GameLevel(int levelNumber, IEnumerable<EnemyTypeEnum> enemyTypes, EnemyFactory enemyFactory,
            PointF location, List<PointF> waypoints, GamePhaseManager gamePhaseManager)
        {
            _background = Image.FromFile($@"Assets\bglvl{levelNumber}Path.png");
            _enemyTypesToSpawn = new Queue<EnemyTypeEnum>(enemyTypes);
            _enemyFactory = enemyFactory;
            _location = location;
            _gamePhaseManager = gamePhaseManager;
            _waypoints = waypoints;
        }

        public override void Init()
        {
            _timeSinceStart.Start();
        }

        public override void Render(BufferedGraphics g)
        {
            // clearing screen
            g.Graphics.DrawImage(_background, 0, 0);
            foreach (EnemyBase monster in _monsters)
            {
                monster.Render(g);
            }
        }

        public override void Update(TimeSpan timeDelta)
        {
            // Create a new enemy if appropriate.
            SpawnSomething();

            if (_monsters.Count == 0 && _enemyTypesToSpawn.Count == 0)
            {
                _gamePhaseManager.LevelEnded(this);
            }

            foreach (EnemyBase monster in _monsters.ToList())
            {
                // "Despawn" if dead...
                if (!monster.IsAlive)
                {
                    _monsters.Remove(monster);
                }
                monster.Update(timeDelta);
            }
        }

        private void SpawnSomething()
        {
            var nao = _timeSinceStart.Elapsed;
            bool shouldSpawnEnemy = _lastSpawn + _spawnFrequency <= nao;
            if (_enemyTypesToSpawn.Count > 0 && shouldSpawnEnemy)
            {
                EnemyTypeEnum enemyType = _enemyTypesToSpawn.Dequeue();
                EnemyBase enemy = _enemyFactory.GetEnemy(enemyType);
                enemy.Init();
                enemy.Spawn(_location);
                enemy.Waypoints = _waypoints;
                _monsters.Add(enemy);
                _lastSpawn = nao;
            }
        }
    }
}