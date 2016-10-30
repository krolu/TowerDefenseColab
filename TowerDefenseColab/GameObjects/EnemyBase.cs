using System;

namespace TowerDefenseColab.GameObjects
{
    public class EnemyBase : GameObjectBase
    {
        public float Speed { get; set; }

        public override void Update(TimeSpan timeDelta)
        {
            Location = new System.Drawing.PointF(Location.X + Speed*timeDelta.Milliseconds, Location.Y);
        }
    }
}