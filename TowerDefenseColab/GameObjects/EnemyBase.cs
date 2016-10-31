using System;
using System.Linq;

namespace TowerDefenseColab.GameObjects
{
    public class EnemyBase : GameObjectBase
    {
        /// <summary>
        /// Pixels per second.
        /// </summary>
        protected float Speed { private get; set; }

        public bool IsAlive { get; set; } = true;

        public override void Update(TimeSpan timeDelta)
        {
            var deltaX = Speed*(float) timeDelta.TotalSeconds;
            var deltaY = 0;
            //Location = new System.Drawing.PointF(Location.First().X + deltaX, Location.First().Y + deltaY);

            if (Location.Last().X > 800)
            {
                Die();
            }
        }

        private void Die()
        {
            IsAlive = false;
        }
    }
}