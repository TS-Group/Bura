using System;
using System.Web.UI.WebControls;
using TS.Gambling.Web;

public partial class Pages_Bura_Controller_ViewGameList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UserHostAddress != "127.0.0.1")
        {
            Server.Transfer("~/Pages/Bura/BuraLobby.aspx");
            return;
        }
        if (!IsPostBack)
            GridViewGameList.DataBind();
    }
    protected void GridViewGameList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View Game")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session[SessionKey.VIEW_BURA_GAME_ID] = GridViewGameList.DataKeys[rowIndex].Value;
            Server.Transfer("~/Pages/Bura/Controller/ViewGame.aspx");
        }
    }
    protected void ButtonSetGame_Click(object sender, EventArgs e)
    {
        Session[SessionKey.VIEW_BURA_GAME_ID] = TextBoxGameId.Text;
        Server.Transfer("~/Pages/Bura/Controller/ViewGame.aspx");
    }
}