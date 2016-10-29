using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab.GameObjects
{
    public abstract class GameObjectBase : IGameLoopMethods
    {
        protected PointF Location { get; set; }

        protected Image Sprite { get; set; }

        public override void Init()
        {

        }

        public override void Render(BufferedGraphics g)
        {
            g.Graphics.DrawImage(Sprite, Location);
        }
    }
}
