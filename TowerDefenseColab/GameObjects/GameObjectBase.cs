using System;
using System.Drawing;

namespace TowerDefenseColab.GameObjects
{
    public abstract class GameObjectBase : GameLoopMethods
    {
        protected PointF LocationTopLeft { get; set; }

        protected Image Sprite { get; set; }

        public void SetLocation(Point location)
        {
            LocationTopLeft = location;
        }

        public void SetLocationCenter(Point location)
        {
            LocationTopLeft = new PointF(location.X - Sprite.Width/2, location.Y - Sprite.Height/2);
        }

        public override void Render(BufferedGraphics g)
        {
            g.Graphics.DrawImage(Sprite, LocationTopLeft);
        }

        public float GetDistance(GameObjectBase other)
        {
            float pow = (LocationTopLeft.X - other.LocationTopLeft.X)*(LocationTopLeft.X - other.LocationTopLeft.X) +
                        (LocationTopLeft.Y - other.LocationTopLeft.Y)*(LocationTopLeft.Y - other.LocationTopLeft.Y);
            return (float) Math.Sqrt(pow);
        }
    }
}