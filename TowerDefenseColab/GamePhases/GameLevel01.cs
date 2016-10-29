using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevel01 : GamePhaseBase
    {
        private Image _background;

        public GameLevel01()
        {
            _background = Image.FromFile("Assets\\bglvl1.png");
        }

        public override void Render(BufferedGraphics g)
        {
            // clearing screen
            g.Graphics.DrawImage(_background, 0, 0);

        }

        public override void Update(TimeSpan timeDelta)
        {
        }
    }
}
