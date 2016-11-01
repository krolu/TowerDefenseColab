using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Numerics;

namespace TowerDefenseColab.GameObjects
{
    public class EnemyBase : GameObjectBase
    {
        /// <summary>
        /// Pixels per second.
        /// </summary>
        protected float Speed { private get; set; }

        public bool IsAlive { get; set; } = true;

        public List<PointF> Waypoints { get; set; }

        public override void Update(TimeSpan timeDelta)
        {
            //var deltaX = Speed * (float)timeDelta.TotalSeconds;
            var deltaX = Speed * (float)0.0058156;
            var deltaY = Speed * (float)0.0058156;
            //var deltaY = Speed * (float)timeDelta.TotalSeconds;

            foreach (var item in Waypoints)
            {
                var newPoint = new PointF(0, 0);
                //if ((Location.X <= item.X && Location.Y => item.Y) || (Location.X => item.X && Location.Y <= item.Y))

                if (Location.X <= item.X && Location.Y >= item.Y)
                {
                    newPoint.X = deltaX;
                    
                    if(Location.Y == item.Y)
                    {
                        Location = new PointF(Location.X + deltaX, Location.Y);
                        break;
                    }
                }

                if(Location.X >= item.X && Location.Y <= item.Y)
                {
                    continue;
                }
            }
            //Location = new System.Drawing.PointF(Location.X + deltaX, Location.Y + deltaY);

            if (Location.X > 800)
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