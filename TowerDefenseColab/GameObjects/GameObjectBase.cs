using System.Collections.Generic;
using System.Drawing;
using TowerDefenseColab.GamePhases;
using System.Linq;

namespace TowerDefenseColab.GameObjects
{
    public abstract class GameObjectBase : GameLoopMethods
    {
        protected List<Point> Location { get; set; }

        protected Image Sprite { get; set; }

        public override void Init()
        {
            
        }

        public void Spawn(List<Point> location)
        {
            //przekazuje liste waypointow
            Location = location;
        }

        public override void Render(BufferedGraphics g)
        {
            //przekazuje mu pierwsza pozycje do spawnowania
            g.Graphics.DrawImage(Sprite, Location.First());
        }
    }
}