<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="affiliateBanners.aspx.cs"
    Inherits="affiliateBanners" Title="Tastygo | Affiliate Program" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://connect.facebook.net/en_US/all.js"></script>

    <link href="CSS/Login.css" rel="stylesheet" type="text/css" />
    <link href="CSS/citypopup.css" rel="stylesheet" type="text/css" />    

    <link href="CSS/AlertBox.css" rel="stylesheet" type="text/css" />
    <div class="PagesBG" style="padding-bottom: 10px; overflow:hidden;">
        <div class="yellowandbold" style="padding-top: 15px; padding-bottom: 15px; padding-left:10px; word-spacing: 3px;">
            <asp:Label ID="label8" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Affiliate Banners"
                Font-Size="29px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label></div>
        <div id="Div1" class="jobs_table" runat="server">
            <div id="Div2" class="contactus_tableJobs" runat="server">
                <div style="padding-left: 26px; padding-top: 11px; padding-bottom: 5px; float: left;"
                    class="GreenFontMiddle fontSpaceHeightHeading">
                    <asp:Label ID="label4" runat="server" Text="How it Works?"></asp:Label></div>
            </div>
            <div style="color: #636363; font-size: 16px; font-weight: bold;">
                <div style="float: left; padding-top: 12px; padding-bottom: 12px; padding-left: 26px;
                    padding-right: 19px;" class="fontSpaceHeightRegular">
                    Copy the code with your personal referral link attached and paste it in your site,
                    then get 7% Tastygo Bucks every time someone buys their Tasty deal within 45 days
                    after clicking your widget.
                </div>
            </div>
        </div>
        <div class="height15">
        </div>
        <div id="Div3" style="width: 100%; float: left;">
            <asp:DataList ID="dlRefBanners" RepeatColumns="3" RepeatDirection="Horizontal" runat="server"
                CellPadding="5" CellSpacing="0" Width="100%" GridLines="None" ShowHeader="false">
                <ItemTemplate>
                    <%# GetBannerHtmlCode(Convert.ToDouble(Eval("BannerWidth")), Convert.ToDouble(Eval("BannerHeight")), Eval("ImageFile").ToString(), Eval("HtmlCodeTemplate").ToString())%>
                </ItemTemplate>
            </asp:DataList>
        </div>
       
    </div>
</asp:Content>
