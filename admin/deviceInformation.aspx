<%@ Page Language="C#" Title="TastyGo | Admin | Manage Device Version" AutoEventWireup="true"
    MasterPageFile="~/admin/adminTastyGo.master" CodeFile="deviceInformation.aspx.cs"
    Inherits="deviceInformation" %>

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
            background: url(          "images/menu_bg.jpg" ) repeat-x scroll 0 0 transparent;
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
            background: url(          "images/menu_bg.jpg" ) repeat-x scroll 0 0 transparent;
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
  
          var filterdate = /^([0-9]*|\d*\.\d{1}?\d*)$/;
  
           var Version = $('#ctl00_ContentPlaceHolder1_txtVersion').val();
           if (Version == "" || !filterdate.test($("#ctl00_ContentPlaceHolder1_txtVersion").val())) {                  
                $('#ctl00_ContentPlaceHolder1_txtVersion').removeClass("MessageBox").addClass("MessageBoxError");
                return false;
               }              
  
         var Value1 = $('#ctl00_ContentPlaceHolder1_txtTitle').val();
           if (Value1 == "") {                  
                $('#ctl00_ContentPlaceHolder1_txtTitle').removeClass("MessageBox").addClass("MessageBoxError");
                return false;
               }              
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

    <div style="background-image: url('Images/Admin_DeviceInfoBG.png'); background-repeat: no-repeat;
        width: 550px; height: 400px;">
        <div style="color: White; font-size: 27px; font-weight: bold; height: 30px; width: 100%;">
            Device Information</div>
        <div style="clear: both; padding-top: 25px;">
            <div style="float: left; padding-left: 10px; padding-right: 10px; width: 100px;">
                Select Device
            </div>
            <div style="float: left; padding-left: 10px; padding-right: 10px;">
                <asp:DropDownList ID="ddlDeviceType" runat="server" AutoPostBack="true" ToolTip="Please select your Device."
                    OnSelectedIndexChanged="ddlDeviceType_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="iPhone" Value="iPhone"></asp:ListItem>
                    <asp:ListItem Text="Android" Value="Android"></asp:ListItem>
                    <asp:ListItem Text="Black Barry" Value="Black Barry"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div style="padding-top: 10px; padding-bottom: 5px; clear: both;">
            <div style="float: left; padding-right: 10px; padding-left: 10px; width: 100px;">
                Version</div>
            <div style="float: left;">
                <asp:TextBox CssClass="MessageBox" onfocus="this.className='MessageBox'" ID="txtVersion"
                    runat="server" Width="300px"></asp:TextBox></div>
        </div>
        <div style="padding-top: 10px; padding-bottom: 5px; clear: both;">
            <div style="float: left; padding-right: 10px; padding-left: 10px; width: 100px;">
                Title</div>
            <div style="float: left;">
                <asp:TextBox CssClass="MessageBox" onfocus="this.className='MessageBox'" ID="txtTitle"
                    runat="server" Width="300px"></asp:TextBox></div>
        </div>
        <div style="padding-top: 10px; padding-bottom: 5px; clear: both;">
            <div style="float: left; padding-right: 10px; padding-left: 10px; width: 100px;">
                Message</div>
            <div style="float: left;">
                <asp:TextBox CssClass="MessageBox" onfocus="this.className='MessageBox'" ID="txtMessage"
                    TextMode="MultiLine" runat="server" Width="300px" Height="157px"></asp:TextBox></div>
        </div>
        <div style="clear: both; padding-top: 10px;">
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
