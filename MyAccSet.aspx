<%@ Page Title="" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="MyAccSet.aspx.cs" Inherits="MyAccSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upNewUpdating" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblTest" runat="server" Text="abc"></asp:Label>
            <asp:Button ID="btnTest" runat="server" Text="Test" />

            <asp:Button ID="btnTest2" runat="server" Text="Testing" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

