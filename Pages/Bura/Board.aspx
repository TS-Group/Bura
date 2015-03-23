<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Bura/Bura.master" AutoEventWireup="true"
    CodeFile="Board.aspx.cs" Inherits="Pages_Bura_Board" %>

<asp:Content ID="ContentBuraHead" ContentPlaceHolderID="BuraHead" runat="Server">
    <META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
    <META HTTP-EQUIV="EXPIRES" CONTENT="01 Jan 1970 00:00:00 GMT">
    <META HTTP-EQUIV="PRAGMA" CONTENT="NO-CACHE">
    <link href="../../Skins/NewDesign2/Fonts/fonts.css" rel="stylesheet" type="text/css" />
    <link href="../../Skins/NewDesign2/Styles/BuraStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jQueryRotate.js" type="text/javascript"></script>
    <script src="../../Scripts/ts.crystalbet.gambling.bura.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="ContentBuraBody" ContentPlaceHolderID="BuraContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="BuraScriptManager" runat="server">
    </asp:ScriptManager>
    <div id="BuraBoard" class="Board">
        <asp:UpdatePanel ID="UpdatePanelStaticContent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!-- fixed items -->
                <div class="Table">
                </div>
                <div class="BuraLogo">
                </div>
                <div class="PlayerScore Top">
                </div>
                <div class="PlayerNameHolder Top">
                </div>
                <div class="PlayerName Top">
                    <asp:Label ID="LabelTopPlayerName" runat="server" Text=""></asp:Label>
                </div>
                <div class="PlayerScore Bottom">
                    0</div>
                <div class="CardArea Top">
                </div>
                <div class="CardArea Bottom">
                </div>
                <div class="CardArea Center">
                </div>
                <div class="PlayerNameHolder Bottom">
                </div>
                <div class="PlayerName Bottom">
                    <asp:Label ID="LabelBottomPlayerName" runat="server" Text=""></asp:Label></div>
                <div class="PlayerAvatar Top">
                    <div class="Avatar">
                        <asp:Image ID="ImageTopPlayerAvatar" Width="70" Height="70" runat="server" ImageUrl="~/Skins/NewDesign2/Images/Common/EmptyAvatar.png" />
                    </div>
                </div>
                <div class="PlayerAvatar Bottom">
                    <div class="Avatar">
                        <asp:Image ID="ImageBottomPlayerAvatar" Width="70" Height="70" runat="server" ImageUrl="~/Skins/NewDesign2/Images/Common/EmptyAvatar.png" />
                    </div>
                </div>
                <div class='PlayerTimer Empty Top' ></div>
                <div class='PlayerTimer Empty Bottom' ></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="GameInfo">
        </div>
        <div class="MessageBox" id="MessageBox">
        </div>
        <div class="GameInfoDetail">
        </div>
        <div class="DoublingInfo">
        </div>
        <div class="sound soundOn"></div>
        <div class="sound soundOff"></div>
        <div class="iconExit"></div>
        <asp:UpdatePanel ID="BoardUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanelTimer" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Timer ID="TimerUpdateLiveData" runat="server" Interval="1500" Enabled="True"
                            OnTick="TimerUpdateLiveData_Tick">
                        </asp:Timer>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:LinkButton ID="BoardEvent" runat="server" Style="display: none;" OnClick="BoardEvent_Click">BoardEvent</asp:LinkButton>
                <asp:PlaceHolder ID="BoardPlaceHolder" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="">
        <embed src="../../Skins/NewDesign2/Sounds/deal.mp3" autostart="false" width="1" height="1" id="soundDeal" enablejavascript="true" />
        <embed src="../../Skins/NewDesign2/Sounds/error.wav" autostart="false" width="1" height="1" id="soundError" enablejavascript="true" />
        <embed src="../../Skins/NewDesign2/Sounds/PlaceCard.wav" autostart="false" width="1" height="1" id="soundPlaceCard" enablejavascript="true" />
    </div>
</asp:Content>
