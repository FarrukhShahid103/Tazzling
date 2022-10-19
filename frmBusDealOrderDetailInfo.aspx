<%@ Page Title="TastyGo | Business | Deal Order Detail" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="frmBusDealOrderDetailInfo.aspx.cs" Inherits="frmBusDealOrderDetailInfo" %>

<%@ Register Src="UserControls/Business/ctrlDealOrderDetailInfo.ascx" TagName="ctrlDealOrderDetailInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <uc1:ctrlDealOrderDetailInfo ID="ctrlDealOrderDetailInfo1" runat="server" />
</asp:Content>
