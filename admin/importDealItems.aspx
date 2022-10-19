<%@ Page Title="TastyGo | Import Deal Items" Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true" CodeFile="importDealItems.aspx.cs" Inherits="importDealItems" %>

<%@ Register src="../UserControls/Menu/ImportDealItems.ascx" tagname="ImportDealItems" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ImportDealItems ID="ImportDealItems1" runat="server" />
</asp:Content>

