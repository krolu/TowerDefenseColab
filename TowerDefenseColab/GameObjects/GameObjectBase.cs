using System.Drawing;

namespace TowerDefenseColab.GameObjects
{
    public abstract class GameObjectBase : GameLoopMethods
    {
        protected PointF Location { get; set; }

        protected Image Sprite { get; set; }

        public void SetLocation(Point location)
        {
            Location = location;
        }

        public void SetLocationCenter(Point location)
        {
            Location = new PointF(location.X - Sprite.Width/2, location.Y - Sprite.Height/2);
        }

        public override void Render(BufferedGraphics g)
        {
            g.Graphics.DrawImage(Sprite, Location);
        }
    }
}