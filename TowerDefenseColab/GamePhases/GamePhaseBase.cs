using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseColab.GamePhases
{
    public abstract class GamePhaseBase
    {
        public abstract void Update(TimeSpan timeDelta);

        public abstract void Render(BufferedGraphics g);
    }
}
