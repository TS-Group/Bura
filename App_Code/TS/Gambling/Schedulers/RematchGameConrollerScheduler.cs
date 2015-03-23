using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using TS.Gambling.Bura;

namespace TS.Gambling.Schedulers
{

    /// <summary>
    /// Summary description for TsScheduler
    /// </summary>
    public class RematchGameConrollerScheduler
    {

        private static RematchGameConrollerScheduler _instance = null;

        public static void StartScheduler()
        {
            if (_instance == null)
            {
                _instance = new RematchGameConrollerScheduler();
                _instance.Start();
            }
        }

        protected RematchGameConrollerScheduler()
        {
        }

        protected const int UPDATE_TIME_IN_SECONDS = 1;
        protected const int REMATCH_TIMEOUT_IN_SECONDS = 20;

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
            // Init gatbage array list
            List<int> garbagedGames = new List<int>();

            Dictionary<int, BuraGame> games = BuraGameController.CurrentInstanse.BuraGames;
            List<int> gameIds = games.Keys.ToList();

            foreach (int gameId in gameIds)
            {
                if (!games.ContainsKey(gameId))
                    continue;
                try
                {
                    if (games[gameId].Players == null || games[gameId].Players.Count == 0 || !games[gameId].IsRematch)
                        continue;
                    
                    if (games[gameId].StartTime.Ticks + TimeSpan.TicksPerSecond * REMATCH_TIMEOUT_IN_SECONDS < currentTicks)
                    {
                        if (games[gameId].Players.Count == 1)
                        {
                            // rematch game is not accepted by oponent
                            games[gameId].IsRematch = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

    }

}