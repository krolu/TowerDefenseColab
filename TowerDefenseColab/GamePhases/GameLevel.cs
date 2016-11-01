using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevel : GamePhase
    {
        private Image _background;
        private readonly List<EnemyBase> _currentMonsters = new List<EnemyBase>();
        private readonly GameLevelSettings _settings;
        private readonly EnemyFactory _enemyFactory;
        private TimeSpan _lastSpawn = TimeSpan.MinValue;
        private readonly Stopwatch _timeSinceStart = new Stopwatch();
        private readonly GamePhaseManager _gamePhaseManager;
        private readonly InputManager _inputManager;
        private Queue<EnemyTypeEnum> _monstersLeftToSpawn;
        private bool _isPaused = true;
        private readonly List<TowerBase> _towers = new List<TowerBase>();

        public GameLevel(GameLevelSettings settings, EnemyFactory enemyFactory, GamePhaseManager gamePhaseManager,
            InputManager inputManager)
        {
            _settings = settings;
            _enemyFactory = enemyFactory;
            _gamePhaseManager = gamePhaseManager;
            _inputManager = inputManager;
            inputManager.OnKeyReleased += InputManagerOnOnKeyReleased;
            inputManager.OnClick += InputManagerOnOnClick;
        }

        private void InputManagerOnOnClick(MouseEventArgs mouseEventArgs)
        {
            if (IsVisible)
            {
                TowerBase placing = _towers.SingleOrDefault(t => t.TowerStateEnum == TowerStateEnum.Setup);
                if (placing != null) placing.TowerStateEnum = TowerStateEnum.Active;
            }
        }

        private void InputManagerOnOnKeyReleased(Keys key)
        {
            if (IsVisible)
            {
                switch (key)
                {
                    case Keys.Space:
                        TogglePause();
                        break;
                    case Keys.D1:
                        _towers.RemoveAll(t => t.TowerStateEnum == TowerStateEnum.Setup);
                        var newTower = new TowerBase(_inputManager);
                        newTower.Init();
                        _towers.Add(newTower);
                        break;
                }
            }
        }

        private void TogglePause()
        {
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                _timeSinceStart.Stop();
            }
            else
            {
                _timeSinceStart.Start();
            }
        }


        public override void Init()
        {
            _towers.Clear();
            _isPaused = true;
            _monstersLeftToSpawn = new Queue<EnemyTypeEnum>(_settings.EnemyTypesToSpawn);
            _background = Image.FromFile($@"Assets\bglvl{_settings.LevelNumber}Path.png");
        }

        public override void Render(BufferedGraphics g)
        {
            // clearing screen
            g.Graphics.DrawImage(_background, 0, 0);
            foreach (EnemyBase monster in _currentMonsters)
            {
                monster.Render(g);
            }
            foreach (TowerBase tower in _towers)
            {
                tower.Render(g);
            }
            // Show pause info.
            if (_isPaused)
            {
                g.Graphics.DrawString("! PAUSED !", new Font("monospace", 20),
                    new SolidBrush(Color.Blue), 300, 270);

                g.Graphics.DrawString($"space - pause{Environment.NewLine}1 - new tower (click to place)",
                    new Font("monospace", 10), new SolidBrush(Color.Blue), 370, 500);
            }
        }

        public override void Update(TimeSpan timeDelta)
        {
            // Update the location of the tower being currently placed.
            TowerBase placing = _towers.SingleOrDefault(t => t.TowerStateEnum == TowerStateEnum.Setup);
            placing?.SetLocationCenter(_inputManager.GetMousePosition());

            if (_isPaused)
            {
                return;
            }

            // Create a new enemy if appropriate.
            SpawnSomething();

            if (_currentMonsters.Count == 0 && _monstersLeftToSpawn.Count == 0)
            {
                _gamePhaseManager.LevelEnded(this);
            }

            foreach (TowerBase tower in _towers)
            {
                tower.Update(timeDelta);
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
            bool isFirstSpawn = _lastSpawn == TimeSpan.MinValue;
            // Spawn enemy if no enemy was spawned yet or if the time since last spawn is long enough.
            bool shouldSpawnEnemy = (_lastSpawn + _settings.SpawnFrequency <= nao) || isFirstSpawn;
            if (_monstersLeftToSpawn.Count > 0 && shouldSpawnEnemy)
            {
                EnemyTypeEnum enemyType = _monstersLeftToSpawn.Dequeue();
                EnemyBase enemy = _enemyFactory.GetEnemy(enemyType);
                enemy.Init();
                enemy.SetLocation(_settings.SpawnPoint);
                enemy.Waypoints = _settings.Waypoints;
                _currentMonsters.Add(enemy);
                _lastSpawn = nao;
            }
        }
    }
}