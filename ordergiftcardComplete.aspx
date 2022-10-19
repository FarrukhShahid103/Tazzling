<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="ordergiftcardComplete.aspx.cs"
    Inherits="ordergiftcardComplete" Title="You have purchase gift card successfully" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-top: 40px; padding-bottom: 40px;">
        <div style="clear: both; padding-top: 10px; padding-bottom: 20px;">
            <div style="height: 45px; border: solid 1px #B7B7B7; background-color: #8ad3fe; border-bottom: none;
                color: White; font-family: Arial; font-size: 45px; font-weight: bold; padding-top: 25px;
                text-align: center;">
                <div>
                    Congratulations</div>
            </div>
            <div style="height: 40px; border-left: solid 1px #B7B7B7; border-right: solid 1px #B7B7B7;
                border-bottom: solid 1px #B7B7B7; background-color: #f99d1c; color: White; font-family: Arial;
                font-size: 24px; text-align: center; font-weight: bold;">
                <div style="padding-top: 12px;">
                    <asp:Label ID="lblMessage" runat="server" Text="Thank you for purchase gift card on Tazzling.com, you will receive an email shortly."></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
