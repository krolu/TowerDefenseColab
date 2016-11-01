using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevelFactory
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly GamePhaseManager _gamePhaseManager;
        private readonly InputManager _inputManager;

        public GameLevelFactory(EnemyFactory enemyFactory, GamePhaseManager gamePhaseManager, InputManager inputManager)
        {
            _enemyFactory = enemyFactory;
            _gamePhaseManager = gamePhaseManager;
            _inputManager = inputManager;
        }

        public GameLevel CreateLevel(GameLevelSettings levelSettings)
        {
            return new GameLevel(levelSettings, _enemyFactory, _gamePhaseManager, _inputManager);
        }
    }
}