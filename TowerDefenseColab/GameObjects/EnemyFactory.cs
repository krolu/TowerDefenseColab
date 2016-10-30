using System;
using TowerDefenseColab.GamePhases;

namespace TowerDefenseColab.GameObjects
{
    public class EnemyFactory
    {
        private readonly GamePhaseManager _gamePhaseManager;

        public EnemyFactory(GamePhaseManager gamePhaseManager)
        {
            _gamePhaseManager = gamePhaseManager;
        }

        public EnemyBase GetEnemy(EnemyTypeEnum type)
        {
            switch (type)
            {
                case EnemyTypeEnum.CircleOfDeath:
                    return new CircleOfDeath();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}