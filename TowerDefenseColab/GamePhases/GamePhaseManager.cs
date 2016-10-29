using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseColab.GamePhases
{
    public class GamePhaseManager : IGameLoopMethods
    {
        private Dictionary<GamePhaseEnum, IGameLoopMethods> _gamePhases = new Dictionary<GamePhaseEnum, IGameLoopMethods>();

        private IGameLoopMethods _activeGamePhase;
        
        public void Add(GamePhaseEnum phaseType, IGameLoopMethods gamePhase)
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
