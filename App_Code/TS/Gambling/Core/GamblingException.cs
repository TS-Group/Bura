using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TS.Gambling.Core
{

    /// <summary>
    /// Summary description for GamblingException
    /// </summary>
    public class GamblingException : Exception
    {
        public GamblingException(string errorMessage)
        {
            _info = new ErrorInfo(ErrorCode.GlobalError, errorMessage);
        }

        public GamblingException(ErrorInfo errorInfo)
        {
            _info = errorInfo;
        }

        private ErrorInfo _info;

        public ErrorInfo Info
        {
            get { return _info; }
            set { _info = value; }
        }

        public string ErrorMessage
        {
            get { return _info.Message; }
        }

    }

    public class ErrorInfo
    {

        public static ErrorInfo GLOBAL_ERROR = new ErrorInfo(ErrorCode.GlobalError, "ბოლო ოპერაციის დროს მოხდა შეცდომა");
        public static ErrorInfo NOT_ENOUGH_MONEY = new ErrorInfo(ErrorCode.NotEnoughMoney, "თქვენს ანგარიშსზე არ არის საკმარისი თანხა");
        public static ErrorInfo GAMES_ARE_STOPPED = new ErrorInfo(ErrorCode.NotEnoughMoney, "თამაშების შექმნა შეჩერებულია");


        public ErrorInfo(ErrorCode code, string message)
        {
            _code = code;
            _message = message;
        }

        private ErrorCode _code;
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public ErrorCode Code
        {
            get { return _code; }
            set { _code = value; }
        }

    }

    public enum ErrorCode
    {
        GlobalError = 0,
        NotEnoughMoney = 1
    }



}