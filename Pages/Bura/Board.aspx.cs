using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TS.Gambling.Bura;
using TS.Gambling.Core;
using TS.Gambling.Web;
using TS.Web.Core;

public partial class Pages_Bura_Board : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillBoardData();
            DrawBoard();
        }
    }
    private void FillBoardData()
    {
        // fill static content
        BuraGame game = (BuraGame)GameContext.GetCurrentGame();
        BuraPlayer player = (BuraPlayer)GameContext.GetCurrentPlayer();
        if (game != null && player != null)
        {
            ImageBottomPlayerAvatar.ImageUrl = "~/Skins/NewDesign2/Images/avatars/" + player.Avatar + ".png";
            LabelBottomPlayerName.Text = player.PlayerName;
            BuraPlayer oponent = game.getOponentPlayer(player.PlayerId);
            if (oponent != null)
            {
                ImageTopPlayerAvatar.ImageUrl = "~/Skins/NewDesign2/Images/avatars/" + oponent.Avatar + ".png";
                LabelTopPlayerName.Text = oponent.PlayerName;
            }
            else
            {
                ImageTopPlayerAvatar.ImageUrl = "~/Skins/NewDesign2/Images/Common/EmptyAvatar.png";
                LabelTopPlayerName.Text = string.Empty;
            }
        }
        UpdatePanelStaticContent.Update();
    }
    private void DrawBoard()
    {
        if (GameContext.GetCurrentPlayer() == null)
            return;

        if (GameContext.GetCurrentPlayer().Events.Count > 0)
        {
            if (!GameContext.GetCurrentPlayer().Events.First().Value.EventPlayed)
            {
                GameContext.GetCurrentPlayer().Events.First().Value.EventPlayed = true;
            }
        }

        Player player = GameContext.GetCurrentPlayer();
        HtmlResponse response = ((BuraBoard)GameContext.GetCurrentGame().Board).GetBoardHtml(player.PlayerId);
        BoardPlaceHolder.Controls.Add(new LiteralControl(response.Html.ToString()));
        BoardUpdatePanel.Update();

        //String script = string.Format("<script>{0}</script>", "try {" + response.Script.ToString() + "} catch(ex) {alert(ex);}");
        String script = string.Format("<script>{0}</script>", response.Script.ToString());
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initBura", script, false);
    }

    protected void BoardEvent_Click(object sender, EventArgs e)
    {
        try
        {
            string eventArgument = Request["__EVENTARGUMENT"];
            if (string.IsNullOrEmpty(eventArgument))
                return;
            Player player = GameContext.GetCurrentPlayer();
            CardGame game = GameContext.GetCurrentGame();

            if (player == null || game == null)
                return;

            if (eventArgument.StartsWith("Continue:"))
            {
                int eventId = int.Parse(eventArgument.Substring("Continue:".Length));
                GameContext.GetCurrentGame().EndEvent(player.PlayerId, eventId);
                ((BuraGame)GameContext.GetCurrentGame()).ContinueGame(player.PlayerId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("TakeCard:"))
            {
                string selectedCards = eventArgument.Substring("TakeCard:".Length);
                bool result = ((BuraGame)GameContext.GetCurrentGame()).PlaceCards(player.PlayerId, selectedCards, true);
                DrawBoard();
                if (!result)
                {
                    String script = string.Format("<script>{0}</script>", "playSound('soundError');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initErrorSound", script, false);
                }
            }
            if (eventArgument.StartsWith("PassCard:"))
            {
                string selectedCards = eventArgument.Substring("PassCard:".Length);
                bool result = ((BuraGame)GameContext.GetCurrentGame()).PlaceCards(player.PlayerId, selectedCards, false);
                DrawBoard();
                if (!result)
                {
                    String script = string.Format("<script>{0}</script>", "playSound('soundError');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "initErrorSound", script, false);
                }
            }
            if (eventArgument.StartsWith("EndEvent:"))
            {
                int eventId = int.Parse(eventArgument.Substring("EndEvent:".Length));
                GameContext.GetCurrentGame().EndEvent(player.PlayerId, eventId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("PlayerTurn:"))
            {
                int eventId = int.Parse(eventArgument.Substring("PlayerTurn:".Length));
                GameContext.GetCurrentGame().EndEvent(player.PlayerId, eventId);
                ((BuraGame)GameContext.GetCurrentGame()).PreparePlayerTurn(player.PlayerId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("DoublingOffer"))
            {
                GameContext.GetCurrentGame().DoublingOffer(player.PlayerId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("DoublingAccept:"))
            {
                int eventId = int.Parse(eventArgument.Substring("DoublingAccept:".Length));
                GameContext.GetCurrentGame().AcceptDoubling(player.PlayerId, eventId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("DoublingReDouble:"))
            {
                int eventId = int.Parse(eventArgument.Substring("DoublingReDouble:".Length));
                GameContext.GetCurrentGame().RedoubleOffer(player.PlayerId, eventId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("DoublingReject:"))
            {
                int eventId = int.Parse(eventArgument.Substring("DoublingReject:".Length));
                GameContext.GetCurrentGame().RejectDoubling(player.PlayerId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("ShowCards:"))
            {
                int eventId = int.Parse(eventArgument.Substring("ShowCards:".Length));
                GameContext.GetCurrentGame().EndEvent(player.PlayerId, eventId);
                ((BuraGame)GameContext.GetCurrentGame()).ShowPlayerCards(player.PlayerId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("AcceptOponent:"))
            {
                int eventId = int.Parse(eventArgument.Substring("AcceptOponent:".Length));
                GameContext.GetCurrentGame().EndEvent(player.PlayerId, eventId);
                ((BuraGame)GameContext.GetCurrentGame()).AcceptOponent();
                DrawBoard();
            }
            if (eventArgument.StartsWith("RejectOponent:"))
            {
                int eventId = int.Parse(eventArgument.Substring("RejectOponent:".Length));
                GameContext.GetCurrentGame().EndEvent(player.PlayerId, eventId);
                ((BuraGame)GameContext.GetCurrentGame()).RejectOponent(player.PlayerId);
                FillBoardData();
                DrawBoard();
            }
            if (eventArgument.StartsWith("RematchOffer:"))
            {
                int eventId = int.Parse(eventArgument.Substring("RematchOffer:".Length));
                ((BuraGame)GameContext.GetCurrentGame()).RematchOffer(player.PlayerId, eventId);
                DrawBoard();
                FillBoardData();
            }
            if (eventArgument.StartsWith("StartGame:"))
            {
                int eventId = int.Parse(eventArgument.Substring("StartGame:".Length));
                ((BuraGame)GameContext.GetCurrentGame()).EndEvent(player.PlayerId, eventId);
                DrawBoard();
                FillBoardData();
            }
            if (eventArgument.StartsWith("TakeCards:"))
            {
                int eventId = int.Parse(eventArgument.Substring("TakeCards:".Length));
                ((BuraGame)GameContext.GetCurrentGame()).TakeCards(player.PlayerId, eventId);
                DrawBoard();
            }
            if (eventArgument.StartsWith("LeaveGame"))
            {
                if (GameContext.GetCurrentPlayer() != null)
                {
                    ((BuraGame)GameContext.GetCurrentGame()).LeaveGame(GameContext.GetCurrentPlayer());
                }
                GameContext.SetCurrentGame(null);
                GameContext.SetCurrentGame(null);
                RedirectToPage("~/Pages/Bura/BuraLobby.aspx");
            }
            if (eventArgument.StartsWith("ContinueGame"))
            {
                ((BuraGame)GameContext.GetCurrentGame()).StartGame();
                DrawBoard();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.Log(ex, Request);
        }
    }

    protected void TimerUpdateLiveData_Tick(object sender, EventArgs e)
    {
        try
        {
            bool updateBoard = false;
            if (GameContext.GetCurrentPlayer() == null)
                return;
            if (GameContext.GetCurrentGame() == null)
                return;

            GameContext.GetCurrentGame().Ping(GameContext.GetCurrentPlayer());

            if (GameContext.MustUpdateGame())
            {
                updateBoard = true;
                GameContext.SetCurrentGameVersion(GameContext.GetCurrentGameVersion() + 1);
            }
            if (GameContext.GetCurrentPlayer().Events.Count == 0 || !GameContext.GetCurrentPlayer().Events.First().Value.EventPlayed)
            {
                updateBoard = true;
            }
            // check if oponent has joined game
            if (GameContext.GetCurrentPlayer().Events.Count > 0 && !GameContext.GetCurrentPlayer().Events.First().Value.EventPlayed)
            {
                if (GameContext.GetCurrentPlayer().Events.First().Value.Type == EventType.StartGameQuestion)
                {
                    FillBoardData();
                }
            }
            if (updateBoard)
            {
                DrawBoard();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.Log(ex, Request);
        }

    }

    protected void RedirectToPage(string virtualPath)
    {
        String TransferPage = string.Format(
            "<script>window.open('{0}','_self');</script>",
            VirtualPathUtility.ToAbsolute(virtualPath));
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tempRedirect", TransferPage, false);
    }

}