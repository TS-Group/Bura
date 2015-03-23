<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GameController.aspx.cs" Inherits="Pages_Bura_Controller_GameController" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="ButtonStopGames" runat="server" Text="Stop Games" 
            onclick="ButtonStopGames_Click" OnClientClick="return confirm('Do you want to Stop Game Creation');" />
        <hr />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" DataSourceID="GameStatesesDataSource" ForeColor="Black" 
            GridLines="Vertical" Width="253px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Status" HeaderText="Status" 
                    SortExpression="Status" />
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count">
                <ItemStyle Width="60px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <asp:ObjectDataSource ID="GameStatesesDataSource" runat="server" 
            SelectMethod="GetGameStatuses" 
            TypeName="TS.Gambling.DataProviders.GameStatusListProvider">
        </asp:ObjectDataSource>
        <br />
        
    </div>
    </form>
</body>
</html>
