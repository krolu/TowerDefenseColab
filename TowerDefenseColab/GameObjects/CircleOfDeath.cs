using System;
using System.Collections.Generic;
using System.Drawing;
using TowerDefenseColab.GameMechanisms;

namespace TowerDefenseColab.GameObjects
{
    public class CircleOfDeath : EnemyBase
    {
        public override void Init()
        {
            Sprite = Image.FromFile("Assets\\sprite1.png");
            Speed = 200f;
            Health = 3;

            AnimSprite = new AnimatedSprite(Sprite, new Size(32, 32));
            AnimSprite.Animations = new List<Animation>()
            {
                new Animation(0, 3, 20f)
            };
            AnimSprite.CurrentAnimation = AnimSprite.Animations[0];
        }

        public override void Update(TimeSpan timeDelta)
        {
            base.Update(timeDelta);
            AnimSprite.Update(timeDelta, LocationCenter);
        }

        public override void Render(BufferedGraphics g)
        {
            //base.Render(g);

            AnimSprite.Render(g.Graphics);

            g.Graphics.DrawString($"{Health}", new Font("monospace", 10),
                new SolidBrush(Color.Blue), LocationCenter.X, LocationCenter.Y - 10);
        }
    }
}