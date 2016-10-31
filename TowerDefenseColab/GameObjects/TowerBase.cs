using System;
using System.Drawing;

namespace TowerDefenseColab.GameObjects
{
    public class TowerBase : GameObjectBase
    {
        public TowerStateEnum TowerStateEnum = TowerStateEnum.Setup;
        private readonly InputManager _inputManager;

        public TowerBase(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public override void Init()
        {
            Sprite = Image.FromFile("Assets/squareOfPew.png");
        }


        public override void Update(TimeSpan timeDelta)
        {
            switch (TowerStateEnum)
            {
                case TowerStateEnum.Setup:
                    Location = _inputManager.GetMousePosition();
                    break;
                case TowerStateEnum.Active:
                    break;
                default:
                    throw new IndexOutOfRangeException(nameof(TowerStateEnum));
            }
        }
    }
}