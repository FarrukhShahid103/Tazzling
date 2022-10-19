<%@ Page Language="C#" Title="TastyGo | Admin | Manage Newsletter Subscribers" AutoEventWireup="true"
    MasterPageFile="~/admin/adminTastyGo.master" CodeFile="IPhoneNotification.aspx.cs"
    Inherits="admin_IPhoneNotification" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .MessageBox
        {
            border: 1px solid black;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
        }
        .MessageBoxError
        {
            border: 1px solid Red;
            background-color: #fbcec9;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
        }
        .SendButton
        {
            border: none;
            background: url(    "images/menu_bg.jpg" ) repeat-x scroll 0 0 transparent;
            color: White;
            font-size: 16px;
            height: 30px;
            width: 150px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
        }
        .SendButton:hover
        {
            border: none;
            background: url(    "images/menu_bg.jpg" ) repeat-x scroll 0 0 transparent;
            color: White;
            font-size: 18px;
            font-weight: bold;
            text-shadow: 0 0 10px #fff, 0 0 20px #fff, 0 0 30px #fff, 0 0 40px #fff, 0 0 70px #fff, 0 0 80px #fff, 0 0 100px #fff, 0 0 150px #fff;
            height: 30px;
            width: 150px;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
        }
    </style>

    <script src="../JS/jquery-1.4.min.js" type="text/javascript"></script>

    <script type="text/javascript">
  function EmptyFieldvalidate() {  
  
           var Value = $('#ctl00_ContentPlaceHolder1_txtMessage').val();
           if (Value != "") {                  
               return;    
               }
            else
            {
             
            $('#ctl00_ContentPlaceHolder1_txtMessage').removeClass("MessageBox").addClass("MessageBoxError");
            return false;
            }
      }
    </script>

    <div style="background-image: url('Images/Admin_IPhone_NotificationBG.png'); background-repeat: no-repeat;
        width: 345px; height: 310px;">
        <div style="color: White; font-size: 27px; font-weight: bold; height: 30px; width: 100%;">
            Send Message</div>
        <div style="clear: both; padding-top: 15px;">
            <asp:DropDownList ID="ddlCity" runat="server" ToolTip="Please select your city.">
                <asp:ListItem Text="Vancouver" Value="337" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Toronto" Value="338"></asp:ListItem>
                <asp:ListItem Text="Calagry" Value="1376"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div style="padding-top: 10px; padding-bottom: 5px;">
            <asp:TextBox CssClass="MessageBox" onfocus="this.className='MessageBox'" ID="txtMessage"
                TextMode="MultiLine" runat="server" Width="300px" Height="157px"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnSendMessage" CssClass="SendButton" Text="Send" runat="server"
                OnClientClick="return EmptyFieldvalidate();" OnClick="btnSendMessage_Click" ValidationGroup="SendText" />
        </div>
    </div>
    <div style="clear: both; padding-top: 10px; text-align: center;">
        <asp:Label ID="lblMessage" runat="server" Font-Names="Tahoma" Font-Bold="true" Font-Size="16px"
            Text="" Visible="false">
    
        </asp:Label>
    </div>
</asp:Content>
