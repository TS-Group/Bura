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

namespace TS.Web.Core
{
    /// <summary>
    /// Summary description for ErrorParser
    /// </summary>
    public class ErrorParser
    {
        public ErrorParser()
        {
        }

        private static String GetTagValue(String text, String StartTag, String EndTag)
        {
            int startTextIndex = text.IndexOf(StartTag) + StartTag.Length;
            int endTextIndex = text.IndexOf(EndTag);
            if (text != null && startTextIndex >= StartTag.Length)
            {
                return text.Substring(startTextIndex, endTextIndex - startTextIndex);
            }
            else
            {
                return null;
            }
        }

        private static String GetTagValue(String Text, String Tag)
        {
            String EndTag = Tag.Insert(1, "/");
            return GetTagValue(Text, Tag, EndTag);
        }

        public static string Parse(string errorText)
        {
            return errorText;
        }
    }

}