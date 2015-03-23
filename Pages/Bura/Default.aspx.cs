using System;

public partial class Pages_Bura_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.Transfer("~/Pages/Bura/BuraLobby.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        /*
        Dictionary<string, Card> Cards = CardSet.LONG_BURA_CARDS;
        List<Card> _dealingCards = new List<Card>();
        //_dealingCards.Add(Cards["EmptyCard"]);
        List<string> randomNumbers = new List<string>();
        Random random = new Random();
        foreach (string cardId in Cards.Keys)
        {
            Card card = Cards[cardId];
            if (card.Type == CardType.EmptyCard || card.Type == CardType.Hidden)
                continue;
            int cardIndex = random.Next(0, _dealingCards.Count);
            _dealingCards.Insert(cardIndex , card);
            randomNumbers.Add(cardId + " - " + cardIndex);
        }
        Card lastCard = _dealingCards[_dealingCards.Count - 1];
        _dealingCards.RemoveAt(_dealingCards.Count - 1);
        int lastCardIndex = random.Next(0, _dealingCards.Count);
        _dealingCards.Insert(lastCardIndex, lastCard);
        randomNumbers.Add(lastCard.Name + " - " + lastCardIndex);

        int cardCount = 0;
        StringBuilder builder = new StringBuilder();
        foreach (Card card in _dealingCards)
        {
            cardCount++;
            builder.AppendFormat("<img alt='' src='{0}{1}.png' />", VirtualPathUtility.ToAbsolute("~/Skins/NewDesign2/Images/Cards/"), card.Name);
            if (cardCount > 8)
            {
                cardCount = 0;
                builder.AppendFormat("<br />");
            }
        }
        StringBuilder builder2 = new StringBuilder();
        foreach (string number in randomNumbers)
        {
            builder2.AppendFormat("{0}; ", number);
        }
        PlaceHolder1.Controls.Add(new LiteralControl(builder2.ToString()));
        PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
        PlaceHolder1.Controls.Add(new LiteralControl(builder.ToString()));
        */
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        /*
        string message =
            BuraMessage.GetMessage(string.Format("თქვენი სვლაა"),
                new MessageOption[2] { 
            new MessageOption("სვლა" , "placeCards(true)", "Red"),
            new MessageOption(GameDoubling.Items[1].DoublingText, string.Format("BoardEvent(\"DoublingOffer\")"),"Silver")});
        PlaceHolder1.Controls.Clear();
        PlaceHolder1.Controls.Add(new LiteralControl(message));
         * */
    }
}