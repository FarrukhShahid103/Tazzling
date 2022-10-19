<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="OrderDetails" Title="Order Details" %>
<%@ Register Src="UserControls/Order/OrderDetails.ascx" TagName="OrderDetails"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <uc1:OrderDetails ID="OrderDetails1" runat="server" />
</asp:Content>

