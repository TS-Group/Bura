using System;
using TS.Gambling.Bura;

public partial class Pages_Bura_Controller_GameController : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UserHostAddress != "127.0.0.1")
        {
            Server.Transfer("~/Pages/Bura/BuraLobby.aspx");
            return;
        }
    }


    protected void ButtonStopGames_Click(object sender, EventArgs e)
    {
        BuraGameController.CurrentInstanse.StopGames();
    }
}