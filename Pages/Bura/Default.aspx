<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Bura/Bura.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Bura_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BuraHead" Runat="Server">
    <link href="../../Skins/NewDesign2/Styles/BuraStyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BuraContentPlaceHolder" Runat="Server">
    <asp:Button ID="Button1" runat="server" Text="Shuffle" 
        onclick="Button1_Click" />
    <br />
    <asp:Button ID="Button2" runat="server" Text="Show Message" 
        onclick="Button2_Click" />
    <br />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
</asp:Content>