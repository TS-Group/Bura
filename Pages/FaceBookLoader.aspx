<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Gambling.master" AutoEventWireup="true" CodeFile="FaceBookLoader.aspx.cs" Inherits="Pages_FaceBookLoader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function OnFbInit() {
            if ('<%: !IsPostBack %>' === 'True') {
                FB.getLoginStatus(function(response) {
                    if (response.status === 'connected') {
                        FB.api(
                            "/me",
                            function(response) {
                                if (response && !response.error) {
                                    console.log('FB Response');
                                    setValue('UserId', response.id);
                                    setValue('FirstName', response.first_name);
                                    setValue('LastName', response.last_name);
                                    setValue('Email', response.email);
                                    setValue('BDay', response.birthday);
                                    setValue('Gender', response.gender);
                                    postData();
                                } else {
                                    console.log(response.error);
                                }
                            }
                        );

                    } else {
                        FB.login();
                    }
                });
            }
        }

        function setValue(field, value) {
            $('#ctl00_GamblingContentPlaceHolder_' + field).val(value);
            //console.log(value);
        }

        function postData() {
            //$('#ctl00_GamblingContentPlaceHolder_ButtonRedirect').click();
            __doPostBack('ctl00$GamblingContentPlaceHolder$ButtonRedirect', '');
        }


    </script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GamblingContentPlaceHolder" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanelRedirect" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:HiddenField ID="UserId" runat="server"/>    
            <asp:HiddenField ID="FirstName" runat="server"/>
            <asp:HiddenField ID="LastName"  runat="server"/>
            <asp:HiddenField ID="Email" runat="server"/>
            <asp:HiddenField ID="BDay" runat="server"/>
            <asp:HiddenField ID="Gender" runat="server"/>
    
            <asp:LinkButton ID="ButtonRedirect" OnClick="ButtonRedirect_OnClick" runat="server" style="display: none"/>
            
            <asp:Panel runat="server" ID="PanelRedirect" Visible="False">
                <asp:HyperLink runat="server" NavigateUrl="Bura/BuraLobby.aspx">Redirecting to lobby ... </asp:HyperLink>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

