<%@ Page Title="TastyGo | Admin | Deal Order Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="dealOrderInfo.aspx.cs" Inherits="admin_dealOrderInfo" %>

<%@ Register Src="UserControls/ctrlDealOrderInfo.ascx" TagName="ctrlDealOrderInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <link href="CSS/confirm.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery-1.4.0.min.js" type="text/javascript"></script>
    <script src="JS/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    
    

    <script src="JS/jquery.simplemodal.js" type="text/javascript"></script>

    <script src="JS/confirm.js" type="text/javascript"></script>

    <uc1:ctrlDealOrderInfo ID="ctrlDealOrderInfo1" runat="server" />
</asp:Content>
