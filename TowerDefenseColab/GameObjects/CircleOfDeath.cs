using System.Drawing;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab.GameObjects
{
    public class CircleOfDeath : EnemyBase
    {
        public override void Init()
        {
            Sprite = Image.FromFile("Assets\\circleOfDeath.png");
            Speed = 250f;
        }
    }
}