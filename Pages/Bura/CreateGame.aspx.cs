using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TS.Gambling.Bura;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using TS.Gambling.Web;

public partial class Gambling_Bura_CreateGame : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UserHostAddress != "127.0.0.1")
        {
            Server.Transfer("~/Pages/Bura/BuraLobby.aspx");
            return;
        }
    }
    protected void ButtonCreateGame_Click(object sender, EventArgs e)
    {
        int playerId = int.Parse(TextBoxPlayerId.Text);

        // Load game creator player object from database
        GamblingModel.Entities entities = new GamblingModel.Entities();
        GamblingModel.Player dbPlayer = entities.Players.Where(x => x.PlayerId == playerId).FirstOrDefault();

        // Create and fill player object
        BuraPlayer player = new BuraPlayer();
        player.ClientId = playerId;
        player.PlayerName = dbPlayer.PlayerName;
        player.Balance = dbPlayer.Balance;
        player.Avatar = dbPlayer.PlayerAvatar;


        GameContext.SetCurrentPlayer(player);
        int gameId = int.Parse(TextBoxGameId.Text);
        int playingTill = int.Parse(TextBoxPlayTill.Text);
        double amount = double.Parse(TextBoxAmount.Text);
        bool stickAllowed = CheckBoxStickAllowed.Checked;
        bool longGameStyle = CheckBoxLongStyle.Checked;
        bool passHiddenCards = CheckBoxPassHiddenCards.Checked;

        BuraGameController.CurrentInstanse.CreateGame(gameId, player, playingTill, amount, longGameStyle, stickAllowed, passHiddenCards);
    }
    protected void ButtonJoin_Click(object sender, EventArgs e)
    {
        int playerId = int.Parse(TextBoxPlayerId.Text);
        // Load game creator player object from database
        GamblingModel.Entities entities = new GamblingModel.Entities();
        GamblingModel.Player dbPlayer = entities.Players.Where(x => x.PlayerId == playerId).FirstOrDefault();

        // Create and fill player object
        BuraPlayer player = new BuraPlayer();
        player.ClientId = playerId;
        player.PlayerName = dbPlayer.PlayerName;
        player.Balance = dbPlayer.Balance;
        player.Avatar = dbPlayer.PlayerAvatar;

        GameContext.SetCurrentPlayer(player);
        int gameId = int.Parse(TextBoxGameId.Text);

        BuraGameController.CurrentInstanse.JoinGame(int.Parse(TextBoxGameId.Text), player);
    }

}