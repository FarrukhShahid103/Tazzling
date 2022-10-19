<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true" CodeFile="dealdiscuession.aspx.cs" Inherits="dealdiscuession" Title="Deal Discuession" %>

<%@ Register Src="UserControls/Discussion/ctrlDiscussion.ascx" TagName="ctrlDiscussion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ctrlDiscussion ID="ctrlDiscussion1" runat="server" />
</asp:Content>
