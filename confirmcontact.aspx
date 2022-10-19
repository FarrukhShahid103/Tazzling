<%@ Page Title="Activate Account" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="confirmcontact.aspx.cs" Inherits="confirmcontact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script src="https://connect.facebook.net/en_US/all.js"></script>

    <link href="CSS/Login.css" rel="stylesheet" type="text/css" />
    <link href="CSS/citypopup.css" rel="stylesheet" type="text/css" />
    <link href="CSS/AlertBox.css" rel="stylesheet" type="text/css" />
    <div class="PagesBG" style=" min-height:400px;">
        <table border="0" cellpadding="6" cellspacing="0" width="100%">
            <tr>
                <td style="height: 8px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 19px;">
                    <asp:Label ID="label1" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Account Activated"
                        Font-Size="35px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 41px; text-align: left; height: 30px; padding-top: 7px;
                    padding-bottom: 2px; padding-right: 25px;" colspan="2">
                    <asp:Image ID="imgGridMessage" runat="server" ImageUrl="images/Checked.png" />
                    <asp:Label ID="lblMessage" Font-Size="17px" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
