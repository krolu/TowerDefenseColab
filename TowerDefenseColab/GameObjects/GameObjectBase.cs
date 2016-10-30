using System.Drawing;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab.GameObjects
{
    public abstract class GameObjectBase : GameLoopMethods
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