<%@ Control Language="C#" AutoEventWireup="true" CodeFile="subMenuRestaurant.ascx.cs"
    Inherits="Takeout_UserControls_Templates_subMenuRestaurant" %>
<div style="padding-top: 10px;">
    <asp:Panel runat="server" ID="pnlRestaurant" CssClass="submenu" Visible="false">
        <ul>
            <li runat="server" id="restaurant_Banlance"><a href="../takeout/restaurant_banlance.aspx">
                <asp:Label ID="lblRBanlance" runat="server" Text="Balance"></asp:Label></a></li>
            <li runat="server" id="restaurant_Setup"><a href="../takeout/restaurant_setup.aspx">
                <asp:Label ID="lblRSetup" runat="server" Text="Setup"></asp:Label></a></li>
            <li runat="server" id="restaurant_Orders"><a href="../takeout/restaurant_orders.aspx">
                <asp:Label ID="lblROrders" runat="server" Text="Orders"></asp:Label></a></li>
            <li runat="server" id="restaurant_Statistics"><a href="../takeout/restaurant_statistics.aspx">
                <asp:Label ID="lblRStatistics" runat="server" Text="Statistics"></asp:Label></a></li>
            <li runat="server" id="restaurant_Profile"><a href="../takeout/restaurant_profile.aspx">
                <asp:Label ID="lblRProfile" runat="server" Text="Profile"></asp:Label></a></li>
            <li runat="server" id="restaurant_Comments"><a href="../takeout/restaurant_comments.aspx">
                <asp:Label ID="lblRComments" runat="server" Text="Comments"></asp:Label></a></li>
            <li runat="server" id="restaurant_Package"><a href="../takeout/restaurant_package.aspx">
                <asp:Label ID="lblRPackage" runat="server" Text="Package"></asp:Label></a></li>
            <li runat="server" id="restaurant_Bank_Account"><a href="../takeout/restaurant_paymenthistory.aspx">
                <asp:Label ID="lblRPaymentHistory" runat="server" Text="Payment History"></asp:Label></a></li>
        </ul>
    </asp:Panel>
</div>
