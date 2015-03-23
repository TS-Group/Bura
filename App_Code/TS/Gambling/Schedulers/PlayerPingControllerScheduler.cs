using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TS.Gambling.Bura;
using System.Diagnostics;

namespace TS.Gambling.Schedulers
{
    
    /// <summary>
    /// Summary description for TsScheduler
    /// </summary>
    public class PlayerPingControllerScheduler
    {

        private static PlayerPingControllerScheduler _instance = null;

        public static void StartScheduler()
        {
            if (_instance == null)
            {
                _instance = new PlayerPingControllerScheduler();
                _instance.Start();
            }
        }

        protected PlayerPingControllerScheduler()
        {
        }

        protected const int UPDATE_TIME_IN_SECONDS = 5;
        protected const int PING_TIMEOUT_IN_SECONDS = 10;

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
                    if (games[gameId].Players == null || games[gameId].Players.Count == 0)
                        continue;
                    foreach (int playerId in games[gameId].Players.Keys)
                    {
                        BuraPlayer player = (BuraPlayer)games[gameId].Players[playerId];

                        if (player.LastPingTime.Ticks + TimeSpan.TicksPerSecond * UPDATE_TIME_IN_SECONDS < currentTicks)
                        {
                            // player is not responding
                            if (games[gameId].Status == Core.GameStatus.WaitingForOponent)
                            {
                                garbagedGames.Add(gameId);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
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