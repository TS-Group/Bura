using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Diagnostics;

namespace TS.Web.Core
{
    /// <summary>
    /// Summary description for ErrorLog
    /// </summary>
    public class ErrorLog
    {
        public ErrorLog()
        {
        }

        public static void Log(Exception ex, HttpRequest request)
        {
            if (!EventLog.SourceExists("CrystalWEBException"))
            {
                EventLog.CreateEventSource("CrystalWEBException", "CrystalWEBException");
            }
            EventLog.WriteEntry("CrystalWEBException",
              "MESSAGE: " + ex.Message +
              "\nSOURCE: " + ex.Source +
              "\nPATH: " + request.Path +
              "\nUSER_ID: " + "" +
              "\nQUERYSTRING: " + request.QueryString.ToString() +
              "\nTARGETSITE: " + ex.TargetSite +
              "\nSTACKTRACE: " + ex.StackTrace,
              EventLogEntryType.Error);
        }

        public static void Info(string logMessage, HttpRequest request)
        {
            if (!EventLog.SourceExists("CrystalWEBException"))
            {
                EventLog.CreateEventSource("CrystalWEBException", "CrystalWEBException");
            }
            EventLog.WriteEntry("CrystalWEBException",
              "MESSAGE: " + logMessage +
              "\nSOURCE: " + "" +
              "\nPATH: " + request.Path +
              "\nUSER_ID: " + "" +
              "\nQUERYSTRING: " + request.QueryString.ToString() +
              "\nTARGETSITE: " + "" +
              "\nSTACKTRACE: " + "",
              EventLogEntryType.Information);
        }

        public static void error(string logMessage, HttpRequest request)
        {
            if (!EventLog.SourceExists("CrystalWEBException"))
            {
                EventLog.CreateEventSource("CrystalWEBException", "CrystalWEBException");
            }
            EventLog.WriteEntry("CrystalWEBException",
              "MESSAGE: " + logMessage +
              "\nPATH: " + request.Path +
              "\nQUERYSTRING: " + request.QueryString.ToString(),
              EventLogEntryType.Error);
        }

    }

}