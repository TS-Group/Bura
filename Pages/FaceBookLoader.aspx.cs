using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TS.Gambling.FaceBook;
using TS.Gambling.Web;

public partial class Pages_FaceBookLoader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButtonRedirect_OnClick(object sender, EventArgs e)
    {
        FacebookUser fbUser = new FacebookUser
        {
            UserId = UserId.Value,
            FirstName = FirstName.Value,
            LastName = LastName.Value,
            Gender = Gender.Value,
            EMail = Email.Value,
            BirthDate = BDay.Value
        };

        DataBaseManager.ResultResponse result = DataBaseManager.LoadPlayer(fbUser);
        if (result.errorCode == 0)
        {
            Session[SessionKey.PLAYER_ID] = result.playerId;
            PanelRedirect.Visible = true;
            //Response.Redirect(VirtualPathUtility.ToAbsolute("~/Pages/Bura/BuraLobby.aspx"));
            string redirectScript = string.Format("window.location='{0}';",
                VirtualPathUtility.ToAbsolute("~/Pages/Bura/BuraLobby.aspx"));
            ScriptManager.RegisterStartupScript(UpdatePanelRedirect, UpdatePanelRedirect.GetType(), "tmpredirect", redirectScript, true);
            UpdatePanelRedirect.Update();
        }

    }
}