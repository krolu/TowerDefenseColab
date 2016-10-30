using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab
{
    public partial class GameWindow : Form
    {
        private bool _isAlive = true;
        private GamePhaseManager _phaseManager;

        public GameWindow()
        {
            InitializeComponent();
            Show();
        }

        public BufferedGraphics InitBackBuffer()
        {
            BufferedGraphicsContext myContext = BufferedGraphicsManager.Current;
            return myContext.Allocate(CreateGraphics(), DisplayRectangle);
        }

        public void InitGame()
        {
            _phaseManager = new GamePhaseManager();
            _phaseManager.Add(GamePhaseEnum.MainGame, new GameLevel(1, _phaseManager));
            _phaseManager.ChangeActiveGamePhase(GamePhaseEnum.MainGame);
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