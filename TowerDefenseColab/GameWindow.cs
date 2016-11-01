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
        private bool _isAlive = true;
        private readonly GameLevelFactory _gameLevelFactory;
        private readonly InputManager _keyboardManager;

        public GameWindow(GamePhaseManager phaseManager, GameLevelFactory gameLevelFactory, InputManager keyboardManager)
        {
            _phaseManager = phaseManager;
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

            var waypoints = new List<PointF>() { new PointF(260, 270), new PointF(260, 120), new PointF(575, 120), new PointF(575, 270), new PointF(800, 270)  };
            // Create the pahses.
            // TODO: should it be even done here or by the PhageManager class itself?
            _phaseManager.Add(GamePhaseEnum.Level001,
                _gameLevelFactory.CreateLevel(1, new[] {EnemyTypeEnum.CircleOfDeath}, new PointF(0, 270), waypoints));
            _phaseManager.Add(GamePhaseEnum.Level002,
                _gameLevelFactory.CreateLevel(2, new[] { EnemyTypeEnum.CircleOfDeath, EnemyTypeEnum.CircleOfDeath }, new PointF(0, 270), waypoints));
            _phaseManager.ChangeActiveGamePhase(GamePhaseEnum.Level001);
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