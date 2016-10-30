using System;
using System.Drawing;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab.GameObjects
{
    public class CircleOfDeath : EnemyBase
    {
        private GamePhaseManager _gameManager;

        public CircleOfDeath(GamePhaseManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override void Init()
        {
            Location = new PointF(0, 270);
            Sprite = Image.FromFile("Assets\\circleOfDeath.png");
            Speed = 0.01f;
        }

        public override void Update(TimeSpan timeDelta)
        {
            base.Update(timeDelta);

            if (Location.X >= 800)
            {
                //_gameManager.ChangeActiveGamePhase(GamePhaseEnum.StartScreen);
            }
        }
    }
}