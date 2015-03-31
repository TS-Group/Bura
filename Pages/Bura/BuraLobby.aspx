<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Gambling.master" AutoEventWireup="true"
    CodeFile="BuraLobby.aspx.cs" Inherits="Pages_BuraLobby" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="description" content="CrystalBet" />
    <meta name="keywords" content="CrystalBet" />
    <title>CrystalBet HTML</title>
    <!-- CSS -->
    <link href="../../Skins/NewDesign2/Styles/BuraLobbyStyleSheet.css?1" rel="stylesheet"
        type="text/css" />
    <link href="../../Skins/NewDesign2/Styles/fonts/fonts.css" rel="stylesheet" type="text/css" />
    <!-- JS -->
    <script src="../../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/ts.crystalbet.gambling.bura.js" type="text/javascript"></script>
    <!-- jQuery UI -->
    <script src="../../Scripts/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../../Scripts/modal.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <!-- end JQuery UI -->
    <link href="../../Scripts/ezMark/ezmark.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ezMark/ezmark.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            InitLobbyScripts();
        });
    </script>
    <!--[if lte IE 7]>
	<link rel="stylesheet" type="text/css" href="../../Skins/NewDesign2/Styles/style_ie_old.css" />
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GamblingContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelTimer" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="TimerUpdateLobby" runat="server" Interval="5000" OnTick="TimerUpdateLobby_Tick">
                    </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="wrapper">
                <div class="fixedWidth">
                    <div id="top">
                        <div id="closeWin">
                            <a href="javascript:window.close()">
                                <img src="../../Skins/NewDesign2/Images/Lobby/close.png" width="25" height="25" border="0"
                                    alt="" /></a></div>
                        <div id="bura">
                            <img src="../../Skins/NewDesign2/Images/Lobby/bura.png" width="133" height="40" border="0"
                                alt="Bura" /></div>
                        <a id="logo" href="http://crystalbet.com/">
                            <img src="../../Skins/NewDesign2/Images/Lobby/logo-crystal-bet.png" width="196" height="36"
                                border="0" alt="CrystalBet" /></a>
                    </div>
                    <!-- right -->
                    <div id="right">
                        <div id="profile" class="rounded3px">
                            <div id="profileInner">
                                <div id="avatar" class="rounded3px">
                                    <asp:Image ID="ImagePlayerAvatar" runat="server" Width="81" Height="81" border="0"
                                        alt="" />
                                </div>
                                <div id="profileInfo">
                                    <div class="line">
                                        <span class="infoTitle">სახელი: </span>
                                        <asp:Label ID="LabelUserName" runat="server" Text="?"></asp:Label></div>
                                    <div class="line">
                                        <span class="infoTitle">ბალანსი: </span>
                                        <asp:Label ID="LabelBalance" runat="server" Text="?"></asp:Label></div>
                                    <a class="yellowButton rounded3px" onclick="modal.show('myAvatar')">ავატარის შეცვლა</a>
                                </div>
                            </div>
                        </div>
                        <div class="clear">
                            &nbsp;</div>
                        <div id="rightFilter">
                            <h2>
                                ფილტრი</h2>
                            <div class="inner">
                                <div class="filterRow">
                                    <div>
                                        მინ. ფსონი:</div>
                                    <div>
                                        <asp:TextBox ID="fFromAmount" CssClass="inputText" Style="width: 35px;" Text="0"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <div>
                                        მაქს. ფსონი:
                                    </div>
                                    <div>
                                        <asp:TextBox ID="fToAmount" CssClass="inputText" Style="width: 35px;" Text="1000"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="filterRow">
                                    <div>
                                        <asp:CheckBox ID="CheckBoxFreeTables" runat="server" /></div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxFreeTables">
                                            თავისუფალი მაგიდები</label></div>
                                </div>
                                <div class="filterRow">
                                    <div>
                                        <asp:CheckBox ID="CheckBoxCards3" runat="server" /></div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxCards3">
                                            3 კარტა</label></div>
                                    <div>
                                        <asp:CheckBox ID="CheckBoxCards5" runat="server" /></div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxCards5">
                                            5 კარტა</label></div>
                                </div>
                                <div class="filterRow">
                                    <div>
                                        <asp:CheckBox ID="CheckBoxStickAllowed" runat="server" />
                                    </div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxStickAllowed">
                                            ურიგოდ</label>
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="CheckBoxStickNotAllowed" runat="server" /></div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxStickNotAllowed">
                                            რიგით</label></div>
                                </div>
                                <div class="filterRow">
                                    <div>
                                        <asp:CheckBox ID="CheckBoxRound3" runat="server" /></div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxRound3">
                                            3 რაუნდი</label></div>
                                    <div>
                                        <asp:CheckBox ID="CheckBoxRound7" runat="server" /></div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxRound7">
                                            7 რაუნდი</label></div>
                                    <div>
                                        <asp:CheckBox ID="CheckBoxRound11" runat="server" /></div>
                                    <div>
                                        <label for="ctl00_GamblingContentPlaceHolder_CheckBoxRound11">
                                            11 რაუნდი</label></div>
                                </div>
                                <div style="padding-top: 70px; text-align: center;">
                                    <asp:Button runat="server" Text="ჩვენება" ID="ButtonShow" CssClass="submit" OnClick="ButtonShow_Click">
                                    </asp:Button>
                                    <asp:Button runat="server" Text="გასუფთავება" ID="ButtonReset" CssClass="submit"
                                        OnClick="ButtonReset_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div style="padding: 30px 0px; text-align: center;">
                            <a class="magida" href="javascript:modal.show('newTable')">ახალი მაგიდის შექმნა</a>
                        </div>
                    </div>
                    <!-- /right -->
                    <!-- main -->
                    <div id="main">
                        <div id="tabs" style="overflow: hidden;">
                            <ul class="ui-tabs-nav">
                                <li id="a_all" runat="server" class="ui-tabs-selected">
                                    <asp:LinkButton runat="server" ID="HyperLinkAll" OnClick="HyperLinkAll_Click">ყველა</asp:LinkButton></li>
                                <li id="a_10" runat="server" class="">
                                    <asp:LinkButton runat="server" ID="HyperLink10" OnClick="HyperLink10_Click">10 ლარამდე</asp:LinkButton></li>
                                <li id="a_100" runat="server" class="">
                                    <asp:LinkButton runat="server" ID="HyperLink100" OnClick="HyperLink100_Click">50 ლარამდე</asp:LinkButton></li>
                                <li id="a_1000" runat="server" class="">
                                    <asp:LinkButton runat="server" ID="HyperLink1000" OnClick="HyperLink1000_Click">100 ლარამდე</asp:LinkButton></li>
                            </ul>
                            <div class="clear">
                                &nbsp;</div>
                            <div id="lobbyMain" class="rounded5px">
                                <div id="lobbyMainInner" class="rounded5px">
                                    <div class="scrollerContent">
                                        <div id="tabs-yvela">
                                            <asp:UpdatePanel ID="UpdatePanelLobby" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Repeater runat="server" ID="RepeaterGameList" ItemType="TS.Gambling.DataProviders.BuraGameItem" DataSource='<%# GetGamesList() %>'>
                                                        <HeaderTemplate>
                                                            <table id="lobbytable" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <thead>
                                                                    <tr class="header">
                                                                        <th id="header_1">
                                                                            &nbsp;
                                                                        </th>
                                                                        <th id="header_2" class="sort lobbyInfo">
                                                                            მოთამაშე
                                                                        </th>
                                                                        <th id="header_3" class="sort lobbyInfo">
                                                                            თანხა
                                                                        </th>
                                                                        <th id="header_4" class="sort lobbyInfo">
                                                                            რაუნდი
                                                                        </th>
                                                                        <th id="header_5" class="sort lobbyInfo">
                                                                            კარტები
                                                                        </th>
                                                                        <th id="header_6" class="sort lobbyInfo">
                                                                            მალიუტკა
                                                                        </th>
                                                                        <th id="header_7" class="sort lobbyInfo">
                                                                            სტატუსი
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                            
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                                <tr class="lobbyRow <%# Item.RowStatus %>" onclick="DoJoinToGame(<%# Item.GameId %>)">
                                                                    <td class="magidaInfo">
                                                                        <%# Item.GameId %>
                                                                    </td>
                                                                    <td class="lobbyInfo">
                                                                        <%# Item.Player1Name %>
                                                                    </td>
                                                                    <td class="lobbyInfo">
                                                                        <%# Item.Amount %>
                                                                    </td>
                                                                    <td class="lobbyInfo">
                                                                        <%# Item.PlayingTill %>
                                                                    </td>
                                                                    <td class="lobbyInfo">
                                                                        <%# Item.LongGameStyle ? "5 კარტა" : "3 კარტა"%>
                                                                    </td>
                                                                    <td class="lobbyInfo">
                                                                        <%# Item.StickAllowed ? "ურიგოთ" : "რიგით"%>
                                                                    </td>
                                                                    <td class="lobbyInfo">
                                                                        <%# Item.GameStatusValue %>
                                                                    </td>
                                                                </tr>
                                                            
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>                                                            
                                                        </FooterTemplate>
                                                    </asp:Repeater>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div style="border-bottom: 1px solid #454C56; margin-bottom: 2px;">
                                            </div>
                                        </div>
                                        <div id="tabs-100">
                                        </div>
                                        <div id="tabs-1000">
                                        </div>
                                        <div id="tabs-10000">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /main -->
                    <div class="clear">
                        <asp:LinkButton ID="JoinToGame" runat="server" Style="display: none" OnClick="JoinToGame_Click"></asp:LinkButton>
                        &nbsp;</div>
                </div>
            </div>
            <div class="clear">
                &nbsp;</div>
            <!-- formebi -->
            <div id="canvas" class="opac">
                &nbsp;</div>
            <!-- avatarebi -->
            <div id="myAvatar" class="opac" style="display: none;">
                <div class="win">
                    <div class="winContentAvatar">
                        <h2>
                            აირჩიეთ ავატარი</h2>
                        <div class="avatarContainer">
                            <% for (int i = 0; i < 25; i++)
                               { %>
                            <div id="Avatar_<%=i %>" class="chooseAvatar">
                                <img src="../../Skins/NewDesign2/Images/avatars/sml/<%=i+1 %>.png" width="45" height="45"
                                    border="0" alt="" /></div>
                            <%} %>
                            <div class="clear">
                                &nbsp;</div>
                        </div>
                        <div class="clear">
                            &nbsp;</div>
                        <div style="padding-top: 20px; text-align: center;">
                            <asp:Button runat="server" Text="არჩევა" ID="ButtonSelectAvatar" CssClass="submit"
                                OnClick="ButtonSelectAvatar_Click"></asp:Button>
                            &nbsp; &nbsp;
                            <input type="button" name="rst" class="submit" value="გაუქმება" onclick="modal.close();" />
                        </div>
                        <asp:HiddenField ID="hfImageId" runat="server" Value="0" />
                    </div>
                    <a class="winClose" href="javascript:" onclick="modal.close();">&nbsp;</a>
                </div>
            </div>
            <!-- avatarebi -->
            <!-- axali
    magida -->
            <div id="newTable" class="opac" style="display: none;">
                <div class="win">
                    <div class="winContent">
                        <h2>
                            მაგიდის შექმნა</h2>
                        <div class="avatarContainer">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="pwinFormLeft BPGNinoMtavruliRegular">
                                        ფსონი
                                    </td>
                                    <td class="pwinFormRight">
                                        <asp:TextBox ID="TextBoxAmount" Text="0.4" Style="width: 80px;" CssClass="rounded3px"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pwinFormLeft BPGNinoMtavruliRegular">
                                        თამაშის ტიპი
                                    </td>
                                    <td class="pwinFormRight">
                                        <label>
                                            <asp:RadioButton ID="NewRadioBoxGameType3" GroupName="NewRadioBoxGameType" value="3"
                                                Checked="true" runat="server" />3 კარტა</label>
                                        &nbsp;
                                        <label>
                                            <asp:RadioButton ID="NewRadioBoxGameType5" GroupName="NewRadioBoxGameType" value="5"
                                                runat="server" />5 კარტა</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pwinFormLeft BPGNinoMtavruliRegular">
                                        რაუნდი
                                    </td>
                                    <td class="pwinFormRight">
                                        <label>
                                            <asp:RadioButton ID="NewRadioBoxRound3" value="3" GroupName="NewRadioBoxRound" Checked="true"
                                                runat="server" />3</label>
                                        &nbsp;
                                        <label>
                                            <asp:RadioButton ID="NewRadioBoxRound7" value="7" GroupName="NewRadioBoxRound" runat="server" />7</label>
                                        &nbsp;
                                        <label>
                                            <asp:RadioButton ID="NewRadioBoxRound11" value="11" GroupName="NewRadioBoxRound"
                                                runat="server" />11</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="pwinFormLeft BPGNinoMtavruliRegular">
                                        მალიუტკა
                                    </td>
                                    <td class="pwinFormRight">
                                        <label>
                                            <asp:RadioButton ID="NewRadioBoxMalutka1" value="0" GroupName="NewRadioBoxMalutka"
                                                Checked="true" runat="server" />ურიგოდ</label>
                                        &nbsp;
                                        <label>
                                            <asp:RadioButton ID="NewRadioBoxMalutka2" value="1" GroupName="NewRadioBoxMalutka"
                                                runat="server" />რიგით</label>
                                    </td>
                                </tr>
                            </table>
                            <div style="padding-top: 30px; text-align: center;">
                                <asp:Button runat="server" Text="შექმნა" ID="ButtonCreateTable" CssClass="submit"
                                    OnClick="ButtonCreateTable_Click"></asp:Button>
                                <input type="button" name="rst" class="submit" value="გაუქმება" />
                            </div>
                        </div>
                    </div>
                    <a class="winClose" href="javascript:" onclick="modal.close();">&nbsp;</a>
                </div>
            </div>
            <!-- /axali magida -->
            <!-- Message Box Dialog -->
            <div id="MessageBox" style="display: none;">
                <div class="win">
                    <div class="clear">
                        &nbsp;</div>
                    <div class="winText">
                        <h2 style="text-align: center; border-bottom: 1px solid #5E6D7C; margin: 20px 20px 20px 20px;">
                            <asp:Label ID="LabelMessageHeader" runat="server" Text=""></asp:Label>
                        </h2>
                        <div class="winTextContent">
                            <div class="theText">
                                <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="clear">
                                &nbsp;</div>
                            <div style="padding-top: 30px; text-align: center;">
                                <input type="button" style="font-family: BPGNinoMtavruliBold, Sylfaen, Arial; font-size: 14px;"
                                    onclick="modal.close(); return false;" value="გამოსვლა" class="submit" name="rst" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
                &nbsp;</div>
            <!-- /Message Box Dialog -->
            <!-- /formebi   -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
