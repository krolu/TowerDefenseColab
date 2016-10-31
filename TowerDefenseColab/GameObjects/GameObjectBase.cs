using System.Collections.Generic;
using System.Drawing;
using TowerDefenseColab.GamePhases;
using System.Linq;

namespace TowerDefenseColab.GameObjects
{
    public abstract class GameObjectBase : GameLoopMethods
    {
        protected PointF Location { get; set; }

        protected Image Sprite { get; set; }

        public override void Init()
        {
            
        }

        public void Spawn(PointF location)
        {
            //przekazuje liste waypointow
            Location = location;
        }
        
        public override void Render(BufferedGraphics g)
        {
            //przekazuje mu pierwsza pozycje do spawnowania
            g.Graphics.DrawImage(Sprite, Location);
        }
    }
}