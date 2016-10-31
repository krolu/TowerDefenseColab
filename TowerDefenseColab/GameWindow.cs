using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TowerDefenseColab.GameObjects;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab
{
    public partial class GameWindow : Form
    {
        private readonly GamePhaseManager _phaseManager;
        private readonly StartScreen _startScreen;
        private bool _isAlive = true;
        private readonly GameLevelFactory _gameLevelFactory;
        private readonly InputManager _keyboardManager;

        public GameWindow(GamePhaseManager phaseManager, StartScreen startScreen, GameLevelFactory gameLevelFactory,
            InputManager keyboardManager)
        {
            _phaseManager = phaseManager;
            _startScreen = startScreen;
            _gameLevelFactory = gameLevelFactory;
            _keyboardManager = keyboardManager;
            InitializeComponent();
            Show();
        }

        public BufferedGraphics InitBackBuffer()
        {
            BufferedGraphicsContext myContext = BufferedGraphicsManager.Current;
            return myContext.Allocate(CreateGraphics(), DisplayRectangle);
        }

        private void InitGame()
        {
            var waypoints = new List<PointF>() {new PointF(0, 270)};
            // Create the pahses.
            // TODO: should it be even done here or by the PhageManager class itself?
            _phaseManager.Add(GamePhaseEnum.StartScreen, _startScreen);
            _phaseManager.Add(GamePhaseEnum.Level001,
                _gameLevelFactory.CreateLevel(new GameLevelSettings
                {
                    EnemyTypesToSpawn = new[] {EnemyTypeEnum.CircleOfDeath},
                    SpawnPoint = new Point(0, 270),
                    SpawnFrequency = TimeSpan.FromSeconds(1),
                    LevelNumber = 1
                }));
            _phaseManager.Add(GamePhaseEnum.Level002,
                _gameLevelFactory.CreateLevel(new GameLevelSettings
                {
                    EnemyTypesToSpawn = new[] {EnemyTypeEnum.CircleOfDeath, EnemyTypeEnum.CircleOfDeath},
                    SpawnPoint = new Point(0, 270),
                    SpawnFrequency = TimeSpan.FromSeconds(1),
                    LevelNumber = 1
                }));

            _phaseManager.ChangeActiveGamePhase(GamePhaseEnum.StartScreen);
        }

        public void GameLoop()
        {
            var stopWatch = Stopwatch.StartNew();
            TimeSpan last = stopWatch.Elapsed;
            using (BufferedGraphics backBuffer = InitBackBuffer())
            {
                InitGame();
                while (_isAlive)
                {
                    // update
                    var currentTimeSpan = stopWatch.Elapsed;
                    _phaseManager.Update(currentTimeSpan - last);

                    // render
                    _phaseManager.Render(backBuffer);

                    backBuffer.Render();
                    backBuffer.Render(CreateGraphics());

                    Application.DoEvents();

                    last = currentTimeSpan;
                }
            }
        }

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isAlive = false;
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            _keyboardManager.KeyPressed(e.KeyCode);
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            _keyboardManager.KeyReleased(e.KeyCode);
        }

        private void GameWindow_MouseClick(object sender, MouseEventArgs e)
        {
            _keyboardManager.MouseClicked(e);
        }
    }
}