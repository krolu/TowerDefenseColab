using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseColab.GamePhases
{
    public class GamePhaseManager
    {
        private Dictionary<GamePhaseEnum, GamePhaseBase> _gamePhases = new Dictionary<GamePhaseEnum, GamePhaseBase>();

        private GamePhaseBase _activeGamePhase;
        
        public void Add(GamePhaseEnum phaseType, GamePhaseBase gamePhase)
        {
            _gamePhases.Add(phaseType, gamePhase);
        }

        public void Clear()
        {
            _gamePhases.Clear();
        }

        public void ChangeActiveGamePhase(GamePhaseEnum gamePhase)
        {
            _activeGamePhase = _gamePhases[gamePhase];
        }

        public void Update(TimeSpan timeDelta)
        {
            _activeGamePhase.Update(timeDelta);
        }

        public void Render(BufferedGraphics gr)
        {
            _activeGamePhase.Render(gr);
        }
    }
}
