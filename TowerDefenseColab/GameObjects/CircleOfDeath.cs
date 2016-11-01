using System.Drawing;

namespace TowerDefenseColab.GameObjects
{
    public class CircleOfDeath : EnemyBase
    {
        public override void Init()
        {
            Sprite = Image.FromFile("Assets\\circleOfDeath.png");
            Speed = 100f;
            Health = 3;
        }

        public override void Render(BufferedGraphics g)
        {
            base.Render(g);

            g.Graphics.DrawString($"{Health}", new Font("monospace", 10),
                new SolidBrush(Color.Blue), LocationTopLeft.X, LocationTopLeft.Y - 10);
        }
    }
}