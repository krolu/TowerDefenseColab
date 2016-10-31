using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseColab.GameObjects
{
    public class TowerBase : GameObjectBase
    {
        private TowerStateEnum towerStateEnum = TowerStateEnum.Setup;
        private InputManager _inputManager;

        public TowerBase(InputManager inputManager)
        {
            _inputManager = inputManager;
        }
        
        public override void Update(TimeSpan timeDelta)
        {
            switch (towerStateEnum)
            {
                case TowerStateEnum.Setup:
                    Location = _inputManager.GetMousePosition();
                    break;
                case TowerStateEnum.Active:
                    break;
                default:
                    break;
            }
        }
    }
}
