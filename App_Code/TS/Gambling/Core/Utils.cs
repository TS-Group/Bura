using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace TS.Gambling.Core
{

    /// <summary>
    /// Summary description for Utils
    /// </summary>
    public class Utils
    {

        private static string GEO_TEXT = "ზხცვბნმასდფგჰჯკლქწერტყუიოპძჩNშჟჭღთ";
        private static string LATIN_TEXT = "zxcvbnmasdfghjklqwertyuiopZCNSJWRT";


        public Utils()
        {
        }

        public static string TranslateToLatin(string geoText)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in geoText.ToCharArray()) {
                int charIndex = GEO_TEXT.IndexOf(ch);
                if (charIndex > -1)
                    builder.Append(LATIN_TEXT[charIndex]);
                else
                    builder.Append(ch);
            }
            return builder.ToString();
        }

    }

}