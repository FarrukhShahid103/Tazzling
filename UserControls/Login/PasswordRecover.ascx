<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PasswordRecover.ascx.cs"
    Inherits="Takeout_UserControls_Login_PasswordRecover" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<div style="padding-bottom: 50px;">
    <div style="width: 100%; vertical-align: top;">
        <div style="float: left; padding-left: 12px; width: 72px;">
            <asp:Image ID="imgqstmark" runat="server" ImageUrl="~/Images/loginimages/imgforgotPW.png" /></div>
        <div style="float: left; width: 780px; padding-bottom: 20px; padding-top: 10px;">
            <asp:Label ID="lblHeader" runat="server" Font-Names="Arial,Helvetica,sans-serif"
                ForeColor="White" Font-Size="32px" Font-Bold="True" 
                Text="If you forgot your username and/or password..."></asp:Label>
        </div>
    </div>
    <div style="clear: both; float: left; width: 870px; padding-left: 84px; padding-bottom: 15px;">
        <asp:Label ID="UserNameLabel" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
            runat="server" Text="Enter the e-mail address associated with your account and click the appropriate link below."></asp:Label>
    </div>
    <div style="width: 746px;">
        <div>
            <asp:Panel ID="pnlforgetPassword" DefaultButton="btnSubmit">
                <div style="float: left; padding-left: 150px; padding-top: 7px;">
                    <asp:Label ID="Label1" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                        Text="E-mail Address"></asp:Label>
                </div>
                <div style="float: left; padding-left: 10px;">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="Input" onfocus="this.className='Input'" />
                </div>
                <div style="float: left; padding-left: 10px;">
                    <asp:Button runat="server" ID="btnSubmit" CssClass="btnforgetPassword" OnClick="btnSubmit_Click" />
                </div>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlError" runat="server" Visible="False" Width="100%">
            <div style="float: left; padding-left: 50px;">
                <div style="float: left; padding-left: 215px">
                    <asp:Image align="text-top" ID="imgGridMessage" runat="server" Visible="true" ImageUrl="~/Images/error.png" />
                    <asp:Label ID="lblMessage" runat="server" Font-Size="17px" ForeColor="red"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>
