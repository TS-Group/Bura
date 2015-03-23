using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TS.Gambling.Bura;
using TS.Gambling.Core;

namespace TS.Gambling.DataProviders
{

    /// <summary>
    /// Summary description for BuraGameProvider
    /// </summary>
    public class BuraGameListProvider
    {
        public BuraGameListProvider()
        {
        }

        public class BuraGameFilter
        {
            public Boolean AllFreeTables;
            public string CardType;
            public string GameRound;
            public string StickAllowed;
            public double FromAmount;
            public double ToAmount;
            public int playerId;
        }


        public static List<BuraGameItem> GetBuraGamesList(BuraGameFilter filter)
        {
            List<BuraGameItem> list = new List<BuraGameItem>();

            foreach (int gameId in BuraGameController.CurrentInstanse.BuraGames.Keys)
            {
                BuraGame game = BuraGameController.CurrentInstanse.BuraGames[gameId];

                // Do not display players own created game
                if (game.Players.ContainsKey(filter.playerId))
                    continue;

                // Check show all table
                if (filter.AllFreeTables && (game.Status != GameStatus.WaitingForOponent))
                    continue;

                // Check stick allowed or not
                if (filter.StickAllowed != null && filter.StickAllowed.Length > 0)
                {
                    string sAllowed = game.StickAllowed ? "1" : "2";
                    if (!filter.StickAllowed.Contains(sAllowed))
                        continue;
                }

                // Check card type
                if (filter.CardType != null && filter.CardType.Length > 0)
                {
                    string cType = game.LongGameStyle ? "5" : "3";
                    if (!filter.CardType.Contains(cType))
                        continue;
                }

                // Check game round
                if (filter.GameRound != null && filter.GameRound.Length > 0)
                {
                    if (!filter.GameRound.Contains(game.PlayingTill.ToString()))
                        continue;
                }
                /*
                if (game.IsRematch)
                    continue;
                */

                // Check game amount range
                if (!(filter.FromAmount <= game.Amount && game.Amount < filter.ToAmount))
                    continue;

                // Load game player(s)
                BuraPlayer player1 = (BuraPlayer)game.Players.First().Value;
                BuraPlayer player2 = (game.Players.Count > 1) ? (BuraPlayer)game.Players.Skip(1).First().Value : null;

                // Create bura game items
                BuraGameItem item = new BuraGameItem(
                    game.GameId,
                    player1.PlayerName,
                    player2 != null ? player2.PlayerName : string.Empty,
                    game.Amount,
                    player2 == null ? "? : ?" : player1.Score + " : " + player2.Score,
                    game.PlayingTill,
                    player1.PlayerId,
                    (player2 == null ? -1 : player2.PlayerId),
                    game.StickAllowed,
                    game.LongGameStyle,
                    game.IsRematch ? GameStatus.GameFinished : game.Status // If game is rematched mark as Finished
                    );

                list.Add(item);
            }


            list.Sort(delegate(BuraGameItem BuraItem1, BuraGameItem BuraItem2) { return BuraItem2.RowStatus.CompareTo(BuraItem1.RowStatus); });
            return list;
        }

        public static List<BuraGameItem> GetBuraGamesList()
        {
            List<BuraGameItem> list = new List<BuraGameItem>();

            foreach (int gameId in BuraGameController.CurrentInstanse.BuraGames.Keys)
            {
                BuraGame game = BuraGameController.CurrentInstanse.BuraGames[gameId];
                if (game.IsRematch)
                    continue;
                BuraPlayer player1 = (BuraPlayer)game.Players.First().Value;
                BuraPlayer player2 = (game.Players.Count > 1) ? (BuraPlayer)game.Players.Skip(1).First().Value : null;
                BuraGameItem item = new BuraGameItem(
                    game.DBGameId,
                    player1.PlayerName,
                    player2 != null ? player2.PlayerName : string.Empty,
                    game.Amount,
                    player2 == null ? "? : ?" : player1.Score + " : " + player2.Score,
                    game.PlayingTill,
                    player1.PlayerId,
                    (player2 == null ? -1 : player2.PlayerId),
                    game.StickAllowed,
                    game.LongGameStyle,
                    game.Status
                    );

                list.Add(item);
            }

            return list;
        }

    }


    public class BuraGameItem
    {

        public BuraGameItem(int gameId, string player1Name, string player2Name, double amount, string score,
                int playingTill, int player1Id, int player2Id, Boolean stickAllowed, Boolean LongGameStyle, GameStatus gameStatusValue)
        {
            _gameId = gameId;
            _player1Name = player1Name;
            _player2Name = player2Name;
            _amount = amount;
            _score = score;
            _playingTill = playingTill;
            _player1Id = player1Id;
            _player2Id = player2Id;
            _StickAllowed = stickAllowed;
            _gameStatusValue = gameStatusValue;
            _LongGameStyle = LongGameStyle;
        }

        private int _gameId;
        private string _player1Name;
        private string _player2Name;
        private double _amount;
        private string _score;
        private int _playingTill;
        private int _player1Id;
        private int _player2Id;
        private Boolean _StickAllowed;
        private GameStatus _gameStatusValue;
        private Boolean _LongGameStyle;

        public int Player2Id
        {
            get { return _player2Id; }
            set { _player2Id = value; }
        }

        public int Player1Id
        {
            get { return _player1Id; }
            set { _player1Id = value; }
        }

        public int PlayingTill
        {
            get { return _playingTill; }
            set { _playingTill = value; }
        }

        public string Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string Player2Name
        {
            get { return _player2Name; }
            set { _player2Name = value; }
        }

        public string Player1Name
        {
            get { return _player1Name; }
            set { _player1Name = value; }
        }

        public int GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }

        public Boolean LongGameStyle
        {
            get { return _LongGameStyle; }
            set { _LongGameStyle = value; }
        }

        public Boolean StickAllowed
        {
            get { return _StickAllowed; }
            set { _StickAllowed = value; }
        }
        public String GameStatusValue
        {
            get { return _gameStatusValue == GameStatus.WaitingForOponent ? "გახსნილი" : "დახურული"; }
        }
        public String RowStatus
        {
            get { return _gameStatusValue == GameStatus.WaitingForOponent ? "opened" : "closed"; }
        }
    }

}
