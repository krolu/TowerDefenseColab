using System;
using System.Collections.Generic;
using System.Drawing;
using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevel : GameLoopMethods
    {
        private readonly Image _background;
        private readonly List<GameObjectBase> _monsters = new List<GameObjectBase>();
        private readonly GamePhaseManager _phaseManager;

        public GameLevel(int levelNumber, GamePhaseManager phaseManager)
        {
            _background = Image.FromFile("Assets\\bglvl" + levelNumber + ".png");
            _phaseManager = phaseManager;
        }

        public override void Init()
        {
            var a = new CircleOfDeath(_phaseManager);
            a.Init();
            _monsters.Add(a);
        }

        public override void Render(BufferedGraphics g)
        {
            // clearing screen
            g.Graphics.DrawImage(_background, 0, 0);
            foreach (var monster in _monsters)
            {
                monster.Render(g);
            }
        }

        public override void Update(TimeSpan timeDelta)
        {
            foreach (var monster in _monsters)
            {
                monster.Update(timeDelta);
            }
        }
    }
}