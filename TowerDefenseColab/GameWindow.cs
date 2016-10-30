using System;
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

        public GameWindow(GamePhaseManager phaseManager, GameLevelFactory gameLevelFactory)
        {
            _phaseManager = phaseManager;
            _gameLevelFactory = gameLevelFactory;
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
            // Create the pahses.
            // TODO: should it be even done here or by the PhageManager class itself?
            _phaseManager.Add(GamePhaseEnum.Level001,
                _gameLevelFactory.CreateLevel(1, new[] {EnemyTypeEnum.CircleOfDeath}, new Point(0, 270)));
            _phaseManager.Add(GamePhaseEnum.Level002,
                _gameLevelFactory.CreateLevel(2, new[] {EnemyTypeEnum.CircleOfDeath, EnemyTypeEnum.CircleOfDeath},
                    new Point(0, 270)));
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
    }
}