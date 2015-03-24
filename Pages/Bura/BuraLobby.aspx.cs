using System;
using System.Linq;
using System.Web.UI;
using TS.Gambling.FaceBook;
using TS.Gambling.Web;
using TS.Gambling.Bura;
using TS.Gambling.DataProviders;
using TS.Gambling.Core;
using System.Globalization;

public partial class Pages_BuraLobby : Page
{
    protected readonly BuraGameListProvider.BuraGameFilter filter = new BuraGameListProvider.BuraGameFilter();

    protected void Page_Load(object sender, EventArgs e)
    {
        string fbUserId = "123";


        if (string.IsNullOrEmpty(fbUserId))
        {
            FacebookUser fbUser = new FacebookUser
            {
                UserId = "1",
                FirstName = "Total",
                LastName = "Soft",
                Gender = "mail",
                EMail = "info@crystalbet.com",
                BirthDate = "01/01/2004"

            };
            DataBaseManager.ResultResponse result = DataBaseManager.LoadPlayer(fbUser);
            if (result.errorCode != 0)
            {
                Response.Redirect("~/Pages/Bura/ErrorPage.aspx");
                return;
            }

            // Load game creator player object from database
            GamblingModel.Entities entities = new GamblingModel.Entities();
            GamblingModel.Player dbPlayer = entities.Players.FirstOrDefault(x => x.PlayerId == result.playerId);

            // Create and fill player object
            BuraPlayer player = new BuraPlayer();
            if (dbPlayer != null)
            {                
                player.PlayerName = dbPlayer.PlayerName;
                player.Balance = dbPlayer.Balance;
                player.Avatar = dbPlayer.PlayerAvatar;
            }
            else
            {                
                player.PlayerName = "Undefined";
                player.Balance = 0;
                player.Avatar = "1";
            }

            //LabelUserName.Text = player.PlayerName;
            //LabelBalance.Text = player.Balance.ToString("0.00");
            //ImagePlayerAvatar.ImageUrl = "~/Skins/NewDesign2/Images/avatars/" + player.Avatar + ".png";

            GameContext.SetCurrentPlayer(player);

            Session[SessionKey.SESSION_ID] = Request.QueryString["SessionId"];
        }

        Player bPlayer = GameContext.GetCurrentPlayer();
        if (bPlayer != null)
        {
            // Load game creator player object from database
            GamblingModel.Entities entities = new GamblingModel.Entities();
            GamblingModel.Player dbPlayer = entities.Players.FirstOrDefault(x => x.PlayerId == bPlayer.PlayerId);
            if (dbPlayer != null)
            {
                bPlayer.PlayerName = dbPlayer.PlayerName;
                bPlayer.Balance = dbPlayer.Balance;
                bPlayer.Avatar = dbPlayer.PlayerAvatar;
            }
            else
            {                
                bPlayer.PlayerName = "Undefined";
                bPlayer.Balance = 0;
                bPlayer.Avatar = "1";
            }
            LabelUserName.Text = bPlayer.PlayerName;
            LabelBalance.Text = bPlayer.Balance.ToString("0.00");
            ImagePlayerAvatar.ImageUrl = "~/Skins/NewDesign2/Images/avatars/" + bPlayer.Avatar + ".png";

            GameContext.SetCurrentPlayer(bPlayer);
        }

        if (!IsPostBack)
            ShowFilter();
    }

    private void ShowFilter()
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", "<script type='text/javascript'>InitLobbyScripts();</script>", false);
        filter.AllFreeTables = CheckBoxFreeTables.Checked;
        try
        {
            filter.playerId = GameContext.GetCurrentPlayer().PlayerId;
        }
        catch (Exception ex)
        {
            filter.playerId = 0;
        }

        filter.CardType = "";
        if (CheckBoxCards3.Checked)
            filter.CardType += "3";
        if (CheckBoxCards5.Checked)
            filter.CardType += "5";

        filter.GameRound = "";
        if (CheckBoxRound3.Checked)
            filter.GameRound += "3";
        if (CheckBoxRound7.Checked)
            filter.GameRound += "7";
        if (CheckBoxRound11.Checked)
            filter.GameRound += "11";

        filter.StickAllowed = "";
        if (CheckBoxStickAllowed.Checked)
            filter.StickAllowed += "1";
        if (CheckBoxStickNotAllowed.Checked)
            filter.StickAllowed += "2";
        try
        {
            filter.FromAmount = double.Parse(fFromAmount.Text);
            filter.ToAmount = double.Parse(fToAmount.Text);
        }
        catch (Exception ex)
        {
            filter.FromAmount = 0;
            filter.ToAmount = 1000;
        }
    }

    protected void ButtonCreateTable_Click(object sender, EventArgs e)
    {
        int playingTill = 3;
        if (NewRadioBoxRound7.Checked)
            playingTill = 7;
        if (NewRadioBoxRound11.Checked)
            playingTill = 11;

        bool stickAllowed = !NewRadioBoxMalutka2.Checked;
        bool longGameStyle = NewRadioBoxGameType5.Checked;

        bool passHiddenCards = !NewRadioBoxGameType5.Checked;

        int gameId = IdGenerator.NextValue;

        string decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        String gameAmount = TextBoxAmount.Text;
        gameAmount = gameAmount.Replace(",", decimalSeparator).Replace(".", decimalSeparator);
        double amount = 0;
        try
        {
            amount = Double.Parse(gameAmount);
        }
        catch (Exception ex)
        {
            amount = 0.4;
        }

        Player currentPlayer = GameContext.GetCurrentPlayer();

        if (amount < 0.4)
        {
            LabelMessageHeader.Text = "შეტყობინება";
            LabelMessage.Text = "მაგიდა უნდა იყოს მინიმუმ 40 თეთრიანი!";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", "<script type='text/javascript'>InitLobbyScripts();modal.show('MessageBox');</script>", false);
            ShowFilter();
            return;
        }

        if (currentPlayer.Balance < System.Convert.ToDecimal(amount))
        {
            LabelMessageHeader.Text = "შეტყობინება";
            LabelMessage.Text = "თანხა არ არის საკმარისი მაგიდის შესაქმნელად!";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", "<script type='text/javascript'>InitLobbyScripts();modal.show('MessageBox');</script>", false);
            ShowFilter();
        }
        else
        {
            try
            {
                BuraGameController.CurrentInstanse.CreateGame(gameId, currentPlayer, playingTill, amount, longGameStyle, stickAllowed, passHiddenCards);
                Response.Redirect("~/Pages/Bura/Board.aspx");
            }
            catch (GamblingException ex)
            {
                LabelMessageHeader.Text = "შეტყობინება";
                LabelMessage.Text = ex.ErrorMessage;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", "<script type='text/javascript'>InitLobbyScripts();modal.show('MessageBox');</script>", false);
                ShowFilter();
            }                
        }
    }

    protected void JoinToGame_Click(object sender, EventArgs e)
    {
        string eventArgument = Request["__EVENTARGUMENT"];
        if (string.IsNullOrEmpty(eventArgument))
            return;

        int GameId = int.Parse(eventArgument);
        TS.Gambling.Bura.BuraGame game = BuraGameController.CurrentInstanse.GetGame(GameId);

        // do nothing if game is empty
        if (game == null)
            return;

        TS.Gambling.Core.Player currentPlayer = GameContext.GetCurrentPlayer();

        // do nothing if player not found
        if (game == null)
            return;

        if (game.Status != GameStatus.WaitingForOponent || game.IsRematch)
        {
            LabelMessageHeader.Text = "შეტყობინება";
            LabelMessage.Text = "მაგიდა უკვე დაკავებულია!";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", "<script type='text/javascript'>InitLobbyScripts();modal.show('MessageBox');</script>", false);
            ShowFilter();
            return;
        }

        if (currentPlayer.Balance < System.Convert.ToDecimal(game.Amount))
        {
            LabelMessageHeader.Text = "შეტყობინება";
            LabelMessage.Text = "თანხა არ არის საკმარისი მაგიდაზე შესასვლელად!";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", "<script type='text/javascript'>InitLobbyScripts();modal.show('MessageBox');</script>", false);
            ShowFilter();
            return;
        }
        else
        {
            try
            {
                BuraGameController.CurrentInstanse.JoinGame(GameId, currentPlayer);
            }
            catch (Exception ex)
            {
                LabelMessageHeader.Text = "შეტყობინება";
                LabelMessage.Text = "მაგიდა უკვე დაკავებულია!";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", "<script type='text/javascript'>InitLobbyScripts();modal.show('MessageBox');</script>", false);
                ShowFilter();
                return;
            }
            Response.Redirect("~/Pages/Bura/Board.aspx");
        }
    }
    protected void ButtonShow_Click(object sender, EventArgs e)
    {
        ShowFilter();
    }

    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        filter.AllFreeTables = CheckBoxFreeTables.Checked;
        CheckBoxCards3.Checked = false;
        CheckBoxCards5.Checked = false;
        CheckBoxRound3.Checked = false;
        CheckBoxRound7.Checked = false;
        CheckBoxRound11.Checked = false;
        CheckBoxStickAllowed.Checked = false;
        CheckBoxStickNotAllowed.Checked = false;
        ShowFilter();
    }

    protected void ButtonSelectAvatar_Click(object sender, EventArgs e)
    {
        TS.Gambling.Core.Player currentPlayer = GameContext.GetCurrentPlayer();
        // Load game creator player object from database
        GamblingModel.Entities entities = new GamblingModel.Entities();
        GamblingModel.Player dbPlayer = entities.Players.Where(x => x.PlayerId == currentPlayer.PlayerId).FirstOrDefault();
        if (dbPlayer != null)
        {
            dbPlayer.PlayerAvatar = hfImageId.Value;
            entities.SaveChanges();
            ImagePlayerAvatar.ImageUrl = "~/Skins/NewDesign2/Images/avatars/" + hfImageId.Value + ".png";
            ShowFilter();
        }
    }

    protected void HyperLinkAll_Click(object sender, EventArgs e)
    {
        a_all.Attributes["class"] = "ui-tabs-selected";
        a_10.Attributes["class"] = "";
        a_100.Attributes["class"] = "";
        a_1000.Attributes["class"] = "";
        fFromAmount.Text = "0";
        fToAmount.Text = "1000";
        ButtonReset_Click(sender, e);
    }
    protected void HyperLink10_Click(object sender, EventArgs e)
    {
        a_all.Attributes["class"] = "";
        a_10.Attributes["class"] = "ui-tabs-selected";
        a_100.Attributes["class"] = "";
        a_1000.Attributes["class"] = "";
        fFromAmount.Text = "0";
        fToAmount.Text = "10";
        ShowFilter();
    }
    protected void HyperLink100_Click(object sender, EventArgs e)
    {
        a_all.Attributes["class"] = "";
        a_10.Attributes["class"] = "";
        a_100.Attributes["class"] = "ui-tabs-selected";
        a_1000.Attributes["class"] = "";
        fFromAmount.Text = "10";
        fToAmount.Text = "50";
        ShowFilter();
    }
    protected void HyperLink1000_Click(object sender, EventArgs e)
    {
        a_all.Attributes["class"] = "";
        a_10.Attributes["class"] = "";
        a_100.Attributes["class"] = "";
        a_1000.Attributes["class"] = "ui-tabs-selected";
        fFromAmount.Text = "50";
        fToAmount.Text = "100";
        ShowFilter();
    }
    protected void TimerUpdateLobby_Tick(object sender, EventArgs e)
    {
        ShowFilter();
    }
}