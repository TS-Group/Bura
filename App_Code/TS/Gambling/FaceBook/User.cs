using TS.Gambling.Core;

namespace TS.Gambling.FaceBook
{
    public class User : Player
    {
        public User(string userId, string fullName)
        {
            ClientId = userId;
            PlayerName = fullName;
        }
    }
}