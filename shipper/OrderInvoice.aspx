<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true" CodeFile="OrderInvoice.aspx.cs" Inherits="OrderInvoice" Title="Order Invoice" %>
<%@ Register Src="UserControls/OrderInvoice.ascx" TagName="OrderInvoice"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <uc1:OrderInvoice ID="OrderDetails1" runat="server" />
</asp:Content>

