using System.Linq;
using GamblingModel;

namespace TS.Gambling.Core
{
    /// <summary>
    /// Summary description for GamblingController
    /// </summary>
    public class GamblingController
    {

        private static GamblingController _current;

        public static GamblingController Current
        {
            get
            {
                if (_current == null)
                    _current = new GamblingController();
                return _current;
            }
        }

        private GamblingController()
        {
        }

        public GamblingModel.Player GetPlayer(int playerId)
        {
            using (Entities entities = new Entities())
            {
                return entities.Players.FirstOrDefault(player => player.PlayerId == playerId);
            }
        }

    }
}