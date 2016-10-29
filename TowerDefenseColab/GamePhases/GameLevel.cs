using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevel : IGameLoopMethods
    {
        private Image _background;
        private List<GameObjectBase> Monsters = new List<GameObjectBase>();
        private GamePhaseManager _phaseManager;

        public GameLevel(int levelNumber, GamePhaseManager phaseManager)
        {
            _background = Image.FromFile("Assets\\bglvl" + levelNumber + ".png");
            _phaseManager = phaseManager;
        }

        public override void Init()
        {
            var a = new CircleOfDeath(_phaseManager);
            a.Init();
            Monsters.Add(a);
        }

        public override void Render(BufferedGraphics g)
        {
            // clearing screen
            g.Graphics.DrawImage(_background, 0, 0);
            foreach (var monster in Monsters)
            {
                monster.Render(g);
            }
        }

        public override void Update(TimeSpan timeDelta)
        {
            foreach (var monster in Monsters)
            {
                monster.Update(timeDelta);
            }
        }
    }
}
