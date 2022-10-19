<%@ Page Title="TastyGo | Admin | Deal Order Detail" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="dealOrderDetailInfo.aspx.cs" Inherits="admin_dealOrderDetailInfo" %>

<%@ Register Src="UserControls/ctrlDealOrderDetailInfo.ascx" TagName="ctrlDealOrderDetailInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ctrlDealOrderDetailInfo ID="ctrlDealOrderDetailInfo1" runat="server" />
</asp:Content>