﻿using System;
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

        public override void Init()
        {
            _activeGamePhase.Init();
        }

        /// <summary>
        /// TODO: the game phase manager (or a dedicated class) should make decisions regarding pahse changes (based on events/calls from outsude). It should not be public.
        /// </summary>
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

        /// <summary>
        /// Called by the level itself when it finishes.
        /// </summary>
        public void LevelEnded(GameLevel gameLevel)
        {
            // TODO: Select the proper "next" pahse.
            ChangeActiveGamePhase(GamePhaseEnum.Level002);
        }
    }
}