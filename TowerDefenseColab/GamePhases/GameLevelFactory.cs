﻿using System.Collections.Generic;
using System.Drawing;
using TowerDefenseColab.GameObjects;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevelFactory
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly GamePhaseManager _gamePhaseManager;

        public GameLevelFactory(EnemyFactory enemyFactory, GamePhaseManager gamePhaseManager)
        {
            _enemyFactory = enemyFactory;
            _gamePhaseManager = gamePhaseManager;
        }

        public GameLevel CreateLevel(int levelNumber, IEnumerable<EnemyTypeEnum> enemyTypes, Point spawnPoint)
        {
            return new GameLevel(levelNumber, enemyTypes, _enemyFactory, spawnPoint, _gamePhaseManager);
        }
    }
}