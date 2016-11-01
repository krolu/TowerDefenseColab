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

        protected float Health { get; set; } = 1;

        public bool IsAlive { get; private set; } = true;

        public override void Init()
        {
        }

        public override void Update(TimeSpan timeDelta)
        {
            var deltaX = Speed*(float) timeDelta.TotalSeconds;
            var deltaY = 0;
            LocationTopLeft = new System.Drawing.PointF(LocationTopLeft.X + deltaX, LocationTopLeft.Y + deltaY);

            if (LocationTopLeft.X > 800)
            {
                Die();
            }
        }

        private void Die()
        {
            IsAlive = false;
        }

        // Thie enemy was shot by the tower.
        public void Shot(TowerBase towerBase)
        {
            Health -= towerBase.Settings.Powah;
            if (Health <= 0)
            {
                Die();
            }
        }
    }
}