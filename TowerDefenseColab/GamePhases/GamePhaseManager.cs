using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TowerDefenseColab.GamePhases
{
    public class GamePhaseManager : GameLoopMethods
    {
        private readonly Dictionary<GamePhaseEnum, GamePhase> _gamePhases =
            new Dictionary<GamePhaseEnum, GamePhase>();

        private GamePhase _activeGamePhase;

        public void Add(GamePhaseEnum phaseType, GamePhase gamePhase)
        {
            _gamePhases.Add(phaseType, gamePhase);
        }

        public override void Init()
        {
            _activeGamePhase.Init();
        }

        /// <summary>
        /// TODO: the game phase manager (or a dedicated class) should make decisions regarding pahse changes (based on events/calls from outsude). It should not be public.
        /// </summary>
        public void ChangeActiveGamePhase(GamePhaseEnum gamePhase)
        {
            if (_activeGamePhase != null)
            {
                _activeGamePhase.IsVisible = false;
            }

            _activeGamePhase = _gamePhases[gamePhase];
            _activeGamePhase.Init();
            _activeGamePhase.IsVisible = true;
        }

        public override void Update(TimeSpan timeDelta)
        {
            _activeGamePhase.Update(timeDelta);
        }

        public override void Render(BufferedGraphics gr)
        {
            _activeGamePhase.Render(gr);
        }

        /// <summary>
        /// Called by the level itself when it finishes.
        /// </summary>
        public void LevelEnded(GameLevel gameLevel)
        {
            // Fugly way of selecting the next level.
            // Assumes levels are at the end of the _gamePhases dictionary.
            int nextIndex = _gamePhases.Values.ToList().IndexOf(gameLevel) + 1;
            GamePhaseEnum nextLevel = nextIndex >= _gamePhases.Count
                ? GamePhaseEnum.StartScreen
                : _gamePhases.Keys.ToList()[nextIndex];

            ChangeActiveGamePhase(nextLevel);
        }
    }
}