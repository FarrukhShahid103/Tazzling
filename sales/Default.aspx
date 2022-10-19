<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TastyGo | Sales | Login</title>
    <link href="CSS/adminTastyGo.css" rel="Stylesheet" />
    <link rel="shortcut icon" href="Images/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <div align="center" id="topCenter">
        <div id="container">
            <div id="headBG">
                <div id="header">
                    <div id="logo">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/admin/images/logo.jpg" ToolTip="logo" /></div>
                </div>
                <div class="floatLeft">
                    <img src="Images/rightTopHeader.jpg" alt="leftcorner" />
                </div>
            </div>
            <div class="floatRight">
                <asp:UpdateProgress ID="upTasty" runat="server" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="lightbox">
                            <div style="background-color: White; padding: 20px; width: 120px; border: solid 2px #e1e1e1;">
                                <asp:Image ImageUrl="~/admin/Images/_load.gif" ToolTip="Processing..." runat="server"
                                    ID="imgLoad" />
                                <br />
                                <br />
                                <asp:Label ID="lblLoad" runat="server" Text="Processing..."></asp:Label>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div id="divLogin">
                <asp:UpdatePanel ID="updatepanel" runat="server">
                    <ContentTemplate>
                        <div style="width: 300px;">
                            <fieldset id="fldLogin">
                                <legend align="left">
                                    <asp:Label ID="lblLogin" runat="server" Text="Login" CssClass="fontStyle"></asp:Label></legend>
                                <div style="width: 100%" align="center">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left">
                                                <br />
                                                <asp:Panel ID="pnlError" runat="server" Visible="False" Width="100%">
                                                    <div class="floatLeft">
                                                        <div style="float: left; padding-right: 3px">
                                                            <asp:Image ID="imgGridMessage" runat="server" Visible="true" ImageUrl="~/admin/Images/error.png" />
                                                        </div>
                                                        <div class="floatLeft">
                                                            <asp:Label ID="lblMessage" runat="server" ForeColor="red" CssClass="fontStyle"></asp:Label>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="padding-bottom: 3px; padding-top: 8px">
                                                <asp:Label CssClass="fontStyle" ID="lblUserName" Text="Username" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div align="left">
                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="divLogininput fontStyle" Width="180px"
                                                        ValidationGroup="login"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVUserName" runat="server" ErrorMessage="Username required."
                                                        ControlToValidate="txtUserName" SetFocusOnError="True" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RFVUserName">
                                                    </cc1:ValidatorCalloutExtender>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="padding-bottom: 3px; padding-top: 3px">
                                                <asp:Label CssClass="fontStyle" ID="lblPassaword" Text="Password" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div align="center" style="text-align: left">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="divLogininput fontStyle" TextMode="Password"
                                                        Width="180px" ValidationGroup="login"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVPassword" runat="server" ControlToValidate="txtPassword"
                                                        Display="None" ErrorMessage="Password required." SetFocusOnError="True" ValidationGroup="login"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RFVPassword">
                                                    </cc1:ValidatorCalloutExtender>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <table width="50" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="padding-top: 10px;">
                                                            <asp:ImageButton ID="btnLogin" ImageUrl="images/button_login.gif" runat="server"
                                                                OnClick="btnLogin_Click" ValidationGroup="login" Style="outline: none;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divFrgtPwd">
                                    <asp:LinkButton ID="lnkForgotPswd" Text="Fogot Password?" PostBackUrl="~/admin/forgotPassword.aspx"
                                        runat="server"></asp:LinkButton>
                                </div>
                            </fieldset>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="footer">
            <asp:Label ID="lblFooter" Text="" runat="server"></asp:Label></div>
    </div>
    </form>
</body>
</html>
