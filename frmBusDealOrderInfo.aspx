<%@ Page Title="TastyGo | Business | Deal Order Management" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="frmBusDealOrderInfo.aspx.cs" Inherits="frmBusDealOrderInfo" %>

<%@ Register Src="UserControls/Business/ctrlDealOrderInfo.ascx" TagName="ctrlDealOrderInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <uc1:ctrlDealOrderInfo ID="ctrlDealOrderInfo1" runat="server" />
</asp:Content>
