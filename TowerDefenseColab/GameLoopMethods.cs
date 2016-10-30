using System;
using System.Drawing;

namespace TowerDefenseColab.GamePhases
{
    public abstract class GameLoopMethods
    {
        public abstract void Init();

        public abstract void Update(TimeSpan timeDelta);

        public abstract void Render(BufferedGraphics g);
    }
}