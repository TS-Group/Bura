<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Gambling.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="Pages_Bura_ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GamblingContentPlaceHolder" Runat="Server">
    <link href="../../Skins/Default/Styles/BuraLobbyStyleSheet.css" rel="stylesheet"
        type="text/css" />
    <asp:Label ID="ErrorMessage" runat="server" CssClass="error" Text="საიტზე არასანქცირებული შემოსვლა!"></asp:Label>
</asp:Content>

