using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TS.Gambling.Bura;
using System.Diagnostics;

namespace TS.Gambling.Schedulers
{

    /// <summary>
    /// Summary description for GameControllerScheduler
    /// </summary>
    public class GarbageControllerScheduler
    {

        private static GarbageControllerScheduler _instance = null;

        public static void StartScheduler()
        {
            if (_instance == null)
            {
                _instance = new GarbageControllerScheduler();
                _instance.Start();
            }
        }

        protected GarbageControllerScheduler()
        {
        }

        protected const int UPDATE_TIME_IN_SECONDS = 5;

        public void Start()
        {
            // Create the timer callback delegate.
            System.Threading.TimerCallback cb = new System.Threading.TimerCallback(ProcessTimerEvent);

            // Create the timer. It is autostart, so creating the timer will start it.
            timer = new System.Threading.Timer(cb, String.Empty, 500, UPDATE_TIME_IN_SECONDS * 1000);
        }

        private System.Threading.Timer timer;

        protected virtual void ProcessTimerEvent(object obj)
        {
            long currentTicks = DateTime.Now.Ticks;
            Dictionary<int, BuraGame> games = BuraGameController.CurrentInstanse.BuraGames;
            // Init gatbage array list
            List<int> garbagedGames = new List<int>();
            garbagedGames.Clear();
            // Collect finished & expired games
            List<int> gameIds = games.Keys.ToList();
            foreach (int gameId in gameIds)
            {
                if (!games.ContainsKey(gameId))
                    continue;
                if (games[gameId].Status == Core.GameStatus.GameFinished)
                {
                    garbagedGames.Add(gameId);
                }
            }
            // Remove finished & expired games from game list
            if (garbagedGames != null)
            {
                foreach (int gameId in garbagedGames)
                {
                    if (games.ContainsKey(gameId))
                        games.Remove(gameId);
                }
            }
        }

    }

}
