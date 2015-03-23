using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using TS.Gambling.Core;

namespace TS.Gambling.Bura
{

    /// <summary>
    /// Summary description for BuraMessage
    /// </summary>
    public class BuraMessage
    {
        public BuraMessage()
        {
        }

        public static string GetMessage(string messageText, MessageOption[] options)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<div class='MessageBox' style='z-index:10;'>");
            builder.AppendFormat("<div class='Text'>{0}</div>", messageText);
            builder.Append("<div class='MessageOptions'>");
            foreach (MessageOption option in options)
            {
                builder.AppendFormat(
                    @"
                    <div class='Option {2}' onclick='{1}'>
                        <div class='Left'></div>
                        <div class='Middle'>{0}</div>
                        <div class='Right'></div>
                        <div class='clearer'></div>
                    </div>",
                    option.OptionText, option.OptionAction, option.OptionColor);
            }
            builder.Append("<div class='clearer'></div></div>");
            builder.Append("</div>");
            return builder.ToString();
        }

        public static string GetDialogMessage(string messageText, MessageOption[] options)
        {
            /*
                    <div id='popup_bg' style='position: absolute; left: 0pt; top: 0pt; width: 100%;
                        height: 100%; background: none repeat scroll 0% 0% rgb(0, 0, 0); opacity: 0.3;'>
                    </div>
             * */
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(
                @"
                <div id='popup' style='position: absolute; left: 0pt; top: 0pt; width: 100%; height: 100%; z-index: 100; display:block;'>
                    <div style='margin-top: 30px;' class='win'>
    	                    <div class='winContent2'>
                                <div id='avatarContainer'>")
                .AppendFormat("<br/><h2 style='text-align: center; margin-top: 5px;'>{0}</h2>", messageText)
                .AppendFormat("</div>");

            builder.Append("<div style='padding-top:0px; text-align:center;'>");
            foreach (MessageOption option in options)
            {
                builder.AppendFormat(
                    @"<input type='button' value='{0}' class='submit {2}' onclick='{1}'>",
                    option.OptionText, option.OptionAction, option.OptionColor);
            }
            builder.Append("</div>");
            builder.Append(
                @"
                        </div>
                    </div>
                </div>");
            return builder.ToString();

        }

    }

    public class MessageOption
    {
        public MessageOption(string text, string action, string color)
        {
            _optionText = text;
            _optionAction = action;
            _optionColor = color;
        }

        private string _optionText;
        private string _optionAction;
        private string _optionColor;

        public string OptionColor
        {
            get { return _optionColor; }
            set { _optionColor = value; }
        }

        public string OptionAction
        {
            get { return _optionAction; }
            set { _optionAction = value; }
        }

        public string OptionText
        {
            get { return _optionText; }
            set { _optionText = value; }
        }

    }

}