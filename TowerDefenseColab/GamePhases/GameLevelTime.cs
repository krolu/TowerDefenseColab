using System;
using System.Diagnostics;

namespace TowerDefenseColab.GamePhases
{
    public class GameLevelTime
    {
        private readonly Stopwatch _time = new Stopwatch();

        public TimeSpan GetCurrent()
        {
            return _time.Elapsed;
        }

        public void Start()
        {
            _time.Start();
        }

        public void Stop()
        {
            _time.Stop();
        }
    }
}