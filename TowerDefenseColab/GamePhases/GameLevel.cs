using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases
{


    public class GameLevel : GamePhase
    {
        private Image _background;
        public readonly List<EnemyBase> CurrentMonsters = new List<EnemyBase>();
        private readonly GameLevelSettings _settings;
        private readonly EnemyFactory _enemyFactory;
        private TimeSpan _lastSpawn = TimeSpan.MinValue;
        private readonly GamePhaseManager _gamePhaseManager;
        private readonly InputManager _inputManager;
        private readonly TowerFactory _towerFactory;
        private readonly GameLevelTime _time = new GameLevelTime();
        private Queue<EnemyTypeEnum> _monstersLeftToSpawn;
        private GameState _gameState = GameState.Paused;
        private readonly List<TowerBase> _towers = new List<TowerBase>();

        public GameLevel(GameLevelSettings settings, EnemyFactory enemyFactory, GamePhaseManager gamePhaseManager,
            InputManager inputManager, TowerFactory towerFactory)
        {
            _settings = settings;
            _enemyFactory = enemyFactory;
            _gamePhaseManager = gamePhaseManager;
            _inputManager = inputManager;
            _towerFactory = towerFactory;
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
                if (_gameState == GameState.Won)
                {
                    _gamePhaseManager.LevelEndedPlayerWon(this);
                    CurrentMonsters.Clear();
                    return;
                } else if (_gameState == GameState.Lost)
                {
                    _gamePhaseManager.LevelEndedPlayerLost(this);
                    CurrentMonsters.Clear();
                    return;
                }

                switch (key)
                {
                    case Keys.Space:
                        TogglePause();
                        break;
                    case Keys.D1:
                        _towers.RemoveAll(t => t.TowerStateEnum == TowerStateEnum.Setup);
                        TowerBase newTower = _towerFactory.GetTower(_time, this, new TowerSettings
                        {
                            Powah = 1,
                            RangePixels = 100,
                            ShootFrequency = TimeSpan.FromSeconds(1)
                        });
                        newTower.Init();
                        _towers.Add(newTower);
                        break;
                }
            }
        }

        private void TogglePause()
        {
            if (_gameState == GameState.Rolling)
            {
                _gameState = GameState.Paused;
                _time.Stop();
            }
            else if (_gameState == GameState.Paused)
            {
                _gameState = GameState.Rolling;
                _time.Start();
            }
        }

        public override void Init()
        {
            _time.Reset();
            _towers.Clear();
            _gameState = GameState.Paused;
            _monstersLeftToSpawn = new Queue<EnemyTypeEnum>(_settings.EnemyTypesToSpawn);
            _background = Image.FromFile($@"Assets\bglvl{_settings.LevelNumber}Path.png");
        }

        public override void Render(BufferedGraphics g)
        {
            // clearing screen
            g.Graphics.DrawImage(_background, 0, 0);

            if (_gameState == GameState.Won)
            {
                g.Graphics.DrawString("Wow. You won.", new Font("monospace", 20), new SolidBrush(Color.Blue), 300, 270);
                return;
            } else if (_gameState == GameState.Lost)
            {
                g.Graphics.DrawString("You noob. You lost. Again.", new Font("monospace", 20), new SolidBrush(Color.Blue), 300, 270);
                return;
            }

            foreach (EnemyBase monster in CurrentMonsters)
            {
                monster.Render(g);
            }
            foreach (TowerBase tower in _towers)
            {
                tower.Render(g);
            }
            // Show pause info.
            if (_gameState == GameState.Paused)
            {
                g.Graphics.DrawString("! PAUSED !", new Font("monospace", 20),
                    new SolidBrush(Color.Blue), 300, 270);

                g.Graphics.DrawString($"space - pause{Environment.NewLine}1 - new tower (click to place)",
                    new Font("monospace", 10), new SolidBrush(Color.Blue), 370, 500);
            }

            g.Graphics.DrawString($"{_time.GetCurrent()}",
                new Font("monospace", 10), new SolidBrush(Color.Blue), 10, 0);
        }

        public override void Update(TimeSpan timeDelta)
        {
            // Update the location of the tower being currently placed.
            TowerBase placing = _towers.SingleOrDefault(t => t.TowerStateEnum == TowerStateEnum.Setup);
            placing?.SetLocationCenter(_inputManager.GetMousePosition());

            if (_gameState == GameState.Paused)
            {
                return;
            }

            // Create a new enemy if appropriate.
            SpawnSomething();

            if (CurrentMonsters.Count == 0 && _monstersLeftToSpawn.Count == 0)
            {
                _gameState = GameState.Won;
                return;
            }

            foreach (TowerBase tower in _towers)
            {
                tower.Update(timeDelta);
            }

            foreach (EnemyBase monster in CurrentMonsters.ToList())
            {
                if (monster.FoundPointG)
                {
                    _gameState = GameState.Lost;
                    break;
                }

                // "Despawn" if dead...
                if (!monster.IsVisible)
                {
                    CurrentMonsters.Remove(monster);
                }
                monster.Update(timeDelta);
            }
        }

        private void SpawnSomething()
        {
            TimeSpan nao = _time.GetCurrent();
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
                CurrentMonsters.Add(enemy);
                _lastSpawn = nao;
            }
        }
    }
}