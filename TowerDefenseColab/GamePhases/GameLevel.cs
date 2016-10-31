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
        private Image _background;
        private readonly List<EnemyBase> _currentMonsters = new List<EnemyBase>();
        private readonly GameLevelSettings _settings;
        private readonly EnemyFactory _enemyFactory;
        private TimeSpan _lastSpawn = TimeSpan.Zero;
        private readonly Stopwatch _timeSinceStart = new Stopwatch();
        private readonly GamePhaseManager _gamePhaseManager;
        private Queue<EnemyTypeEnum> _monstersLeftToSpawn;

        public GameLevel(GameLevelSettings settings, EnemyFactory enemyFactory, GamePhaseManager gamePhaseManager)
        {
            _settings = settings;
            _enemyFactory = enemyFactory;
            _gamePhaseManager = gamePhaseManager;
        }

        public override void Init()
        {
            _monstersLeftToSpawn = new Queue<EnemyTypeEnum>(_settings.EnemyTypesToSpawn);
            _background = Image.FromFile($@"Assets\bglvl{_settings.LevelNumber}Path.png");
            _timeSinceStart.Start();
        }

        public override void Render(BufferedGraphics g)
        {
            // clearing screen
            g.Graphics.DrawImage(_background, 0, 0);
            foreach (EnemyBase monster in _currentMonsters)
            {
                monster.Render(g);
            }
        }

        public override void Update(TimeSpan timeDelta)
        {
            // Create a new enemy if appropriate.
            SpawnSomething();

            if (_currentMonsters.Count == 0 && _monstersLeftToSpawn.Count == 0)
            {
                _gamePhaseManager.LevelEnded(this);
            }

            foreach (EnemyBase monster in _currentMonsters.ToList())
            {
                // "Despawn" if dead...
                if (!monster.IsAlive)
                {
                    _currentMonsters.Remove(monster);
                }
                monster.Update(timeDelta);
            }
        }

        private void SpawnSomething()
        {
            var nao = _timeSinceStart.Elapsed;
            bool shouldSpawnEnemy = _lastSpawn + _settings.SpawnFrequency <= nao;
            if (_monstersLeftToSpawn.Count > 0 && shouldSpawnEnemy)
            {
                EnemyTypeEnum enemyType = _monstersLeftToSpawn.Dequeue();
                EnemyBase enemy = _enemyFactory.GetEnemy(enemyType);
                enemy.Init();
                enemy.Spawn(_settings.SpawnPoint);
                _currentMonsters.Add(enemy);
                _lastSpawn = nao;
            }
        }
    }
}