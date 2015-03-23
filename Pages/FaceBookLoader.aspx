<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Gambling.master" AutoEventWireup="true" CodeFile="FaceBookLoader.aspx.cs" Inherits="Pages_FaceBookLoader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        window.fbAsyncInit = function () {
            FB.init({
                appId: '356739501200640',
                xfbml: true,
                version: 'v2.2'
            });

            FB.api(
                    "/me",
                    function (response) {
                        if (response && !response.error) {
                            setValue('UserId', response.id);
                            setValue('FirstName', response.first_name);
                            setValue('LastName', response.last_name);
                            setValue('Email', response.email);
                            setValue('BDay', response.birthday);
                            setValue('Gender', response.gender);
                        }
                    }
                );
        };

        function setValue(field, value) {
            $('#ctl0$ctl0$' + field).val(value);
        }

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GamblingContentPlaceHolder" runat="Server">
    <asp:HiddenField ID="UserId" runat="server"/>    
    <asp:HiddenField ID="FirstName" runat="server"/>
    <asp:HiddenField ID="LastName"  runat="server"/>
    <asp:HiddenField ID="Email" runat="server"/>
    <asp:HiddenField ID="BDay" runat="server"/>
    <asp:HiddenField ID="Gender" runat="server"/>
    <asp:Button ID="ButtonRedirect" OnClick="ButtonRedirect_OnClick" runat="server"/>
</asp:Content>

