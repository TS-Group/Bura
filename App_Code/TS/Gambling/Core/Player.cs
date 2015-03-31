using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TS.Gambling.Core
{

    /// <summary>
    /// Summary description for Player
    /// </summary>
    public class Player
    {

        public Player()
        {
            _events = new SortedDictionary<int, GameEvent>();
            _lastPingTime = DateTime.Now;
        }

        private int _playerId;
        private string _playerName;
        private string _clientId;
        private decimal _balance;
        private string _avatar;
        private DateTime _lastPingTime;
        private SortedDictionary<int, GameEvent> _events;

        public DateTime LastPingTime
        {
            get { return _lastPingTime; }
            set { _lastPingTime = value; }
        }

        public SortedDictionary<int, GameEvent> Events
        {
            get { return _events; }
            set { _events = value; }
        }

        public int PlayerId
        {
            get { return _playerId; }
            set { _playerId = value; }
        }

        public string ClientId
        {
            get { return _clientId; }
            set { _clientId = value; }
        }

        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
    }

}