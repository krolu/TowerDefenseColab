using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab
{
    public partial class GameWindow : Form
    {
        private bool _isAlive = true;
        private float x = 0;
        private GamePhaseManager _phaseManager;

        public GameWindow()
        {
            InitializeComponent();
            Show();
        }

        public BufferedGraphics InitBackBuffer()
        {
            BufferedGraphicsContext myContext = BufferedGraphicsManager.Current;
            return myContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
        }

        public void InitGame()
        {
            _phaseManager = new GamePhaseManager();
            _phaseManager.Add(GamePhaseEnum.MainGame, new GameLevel01());
            _phaseManager.ChangeActiveGamePhase(GamePhaseEnum.MainGame);
        }

        public void GameLoop()
        {
            using (BufferedGraphics backBuffer = InitBackBuffer())
            {
                InitGame();
                while (_isAlive)
                {
                    // update
                    _phaseManager.Update(new TimeSpan());

                    // render
                    _phaseManager.Render(backBuffer);

                    backBuffer.Render();
                    backBuffer.Render(this.CreateGraphics());
                    Application.DoEvents();
                }
            }
        }

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isAlive = false;
        }
    }
}
