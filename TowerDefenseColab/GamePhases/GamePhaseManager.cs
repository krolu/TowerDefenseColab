using System;
using System.Collections.Generic;
using System.Drawing;

namespace TowerDefenseColab.GamePhases
{
    public class GamePhaseManager : GameLoopMethods
    {
        private readonly Dictionary<GamePhaseEnum, GameLoopMethods> _gamePhases =
            new Dictionary<GamePhaseEnum, GameLoopMethods>();

        private GameLoopMethods _activeGamePhase;

        public void Add(GamePhaseEnum phaseType, GameLoopMethods gamePhase)
        {
            _gamePhases.Add(phaseType, gamePhase);
        }

        public void Clear()
        {
            _gamePhases.Clear();
        }

        public override void Init()
        {
            _activeGamePhase.Init();
        }

        public void ChangeActiveGamePhase(GamePhaseEnum gamePhase)
        {
            _activeGamePhase = _gamePhases[gamePhase];

            _activeGamePhase.Init();
        }

        public override void Update(TimeSpan timeDelta)
        {
            _activeGamePhase.Update(timeDelta);
        }

        public override void Render(BufferedGraphics gr)
        {
            _activeGamePhase.Render(gr);
        }
    }
}