using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TS.Gambling.Core;
using TS.Gambling.Bura;

namespace TS.Gambling.DataProviders
{


    /// <summary>
    /// Summary description for GameStatusListProvider
    /// </summary>
    public class GameStatusListProvider
    {
        public GameStatusListProvider()
        {
        }

        public static List<GameStatusItem> GetGameStatuses()
        {

            Dictionary<GameStatus, GameStatusItem> gameStatuses = new Dictionary<GameStatus, GameStatusItem>();

            foreach (int gameId in BuraGameController.CurrentInstanse.BuraGames.Keys)
            {
                BuraGame game = BuraGameController.CurrentInstanse.GetGame(gameId);
                if (! gameStatuses.ContainsKey(game.Status))
                {
                    gameStatuses[game.Status] = new GameStatusItem(game.Status.ToString(), 0);
                }
                gameStatuses[game.Status].Count++;
            }
            List<GameStatusItem> list = new List<GameStatusItem>();
            foreach (GameStatus status in gameStatuses.Keys)
            {
                list.Add(gameStatuses[status]);
            }
            return list;
        }

    }

    public class GameStatusItem
    {
        public GameStatusItem(string status, int count)
        {
            _status = status;
            _count = count;
        }

        private string _status;
        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }

}