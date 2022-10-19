<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="Default7.aspx.cs"
    Inherits="Default7" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
    <asp:Label ID="lblResult" runat="server"></asp:Label>
        <cc2:TabContainer ID="tc" runat="server">
            <cc2:TabPanel ID="tp1" runat="server" HeaderText="Available" CssClass="voucherTabs">
                <ContentTemplate>
                    Available</ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Used" CssClass="voucherTabs">
                <ContentTemplate>
                    Used</ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="Cancelled" CssClass="voucherTabs">
                <ContentTemplate>
                    Cancelled</ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
    </div>
</asp:Content>
