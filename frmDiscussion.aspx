<%@ Page Title="Tastygo | Deal | Discussion" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="frmDiscussion.aspx.cs" Inherits="frmDiscussion" %>

<%@ Register Src="UserControls/Discussion/ctrlDiscussion.ascx" TagName="ctrlDiscussion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        <uc1:ctrlDiscussion ID="ctrlDiscussion1" runat="server" />
    </p>
</asp:Content>
