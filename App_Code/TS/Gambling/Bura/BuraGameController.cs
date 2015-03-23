using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TS.Gambling.Core;
using TS.Gambling.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;


namespace TS.Gambling.Bura
{

    /// <summary>
    /// Summary description for BuraGameController
    /// </summary>
    public class BuraGameController
    {

        private static BuraGameController _instanse;

        public static BuraGameController CurrentInstanse
        {
            get 
            {
                if (_instanse == null)
                    _instanse = new BuraGameController();
                return _instanse; 
            }
        }

        private BuraGameController()
        {
            _games = new Dictionary<int, BuraGame>();
        }

        private Dictionary<int, BuraGame> _games;
        private bool _stopGames = false;


        public void StopGames()
        {
            if (_stopGames)
                throw new GamblingException(ErrorInfo.GAMES_ARE_STOPPED);
            _stopGames = true;
            foreach (int gameId in _games.Keys)
            {
                GetGame(gameId).CreateGameStopMessage();
            }
        }

        public void CreateGame(int gameId, Player player, int playTill, double amount, bool longGameStyle, bool stickAllowed, bool passHiddenCards)
        {
            if (_stopGames)
                throw new GamblingException("Game Creation not Allowed");

            if (_games.ContainsKey(gameId))
                throw new GamblingException("GameID is busy");
           
            GamblingModel.Entities entities = new GamblingModel.Entities();
            GamblingModel.Player dbPlayer = entities.Players.Where(x => x.PlayerId == player.PlayerId).FirstOrDefault();
            if (dbPlayer != null)
            {
                player.Balance = dbPlayer.Balance;
            }

            if ((decimal)amount > player.Balance)
            {
                throw new GamblingException(ErrorInfo.NOT_ENOUGH_MONEY);
            }

            BuraPlayer bp = new BuraPlayer();
            bp.ClientId = player.PlayerId;
            bp.PlayerName = player.PlayerName;
            bp.Avatar = player.Avatar;

            BuraGame game = new BuraGame(bp, playTill, amount, longGameStyle, stickAllowed, passHiddenCards);
            game.PlayerTurn = bp.PlayerId;
            game.GameId = gameId;

            GameContext.SetCurrentGame(game);
            GameContext.SetCurrentPlayer(bp);

            _games[gameId] = game;
        }

        public void JoinGame(int gameId, Player player)
        {
            if (! _games.ContainsKey(gameId))
                throw new GamblingException("Game with GameID is not started");
            
            BuraGame buraGame = _games[gameId];

            GameContext.SetCurrentGame(_games[gameId]);

            GamblingModel.Entities entities = new GamblingModel.Entities();
            GamblingModel.Player dbPlayer = entities.Players.Where(x => x.PlayerId == player.PlayerId).FirstOrDefault();
            if (dbPlayer != null)
            {
                player.Balance = dbPlayer.Balance;
            }

            if ((decimal)buraGame.Amount > player.Balance)
            {
                throw new GamblingException(ErrorInfo.NOT_ENOUGH_MONEY);
            }
            
            BuraPlayer bp = new BuraPlayer();
            bp.ClientId = player.PlayerId;
            bp.PlayerName = player.PlayerName;
            bp.Avatar = player.Avatar;

            GameContext.SetCurrentPlayer(bp);

            _games[gameId].JoinGame(bp);
        }

        public void RematchGame(int newGameId, int winnerPlayerId)
        {
            BuraGame rematchGame = GetGame(newGameId);
            BuraPlayer player = (BuraPlayer)GameContext.GetCurrentPlayer();
            if (rematchGame == null)
            {
                // create rematch game
                BuraGame buraGame = (BuraGame)GameContext.GetCurrentGame();
                int gameId = newGameId;
                
                int playTill = buraGame.PlayingTill;
                double amount = buraGame.Amount;
                bool longGameStyle = buraGame.LongGameStyle;
                bool stickAllowed = buraGame.StickAllowed;
                bool passHiddenCards = buraGame.PassHiddenCards;

                CreateGame(gameId, player, playTill, amount, longGameStyle, stickAllowed, passHiddenCards);
                rematchGame = GetGame(gameId);
                rematchGame.IsRematch = true;
                if (player.PlayerId == winnerPlayerId)
                    rematchGame.LastCardTakerPlayer = player;
            }
            else
            {
                // join to rematch game 
                if (player.PlayerId == winnerPlayerId)
                    rematchGame.LastCardTakerPlayer = player;
                JoinGame(newGameId, player);
            }
            GameContext.SetCurrentGame(rematchGame);
        }

        public BuraGame GetGame(int gameId)
        {
            if (_games.ContainsKey(gameId))
                return _games[gameId];
            else
                return null;
        }

        public void RemoveGame(int gameId)
        {
            if (_games.ContainsKey(gameId))
                _games.Remove(gameId);
            /*
            else
                throw new GamblingException("Unable to remove Game (not found).");
             * */
        }

        public Dictionary<int, BuraGame> BuraGames
        {
            get
            {
                return _games;
            }
        }

    }

}