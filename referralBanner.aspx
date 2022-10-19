<%@ Page Title="Tastygo | Referral Banner" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="referralBanner.aspx.cs" Inherits="referralBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    $(document).ready(function() {

        $("#UserSignin").click(function(e) {
            e.preventDefault();
            $("fieldset#signin_menu").toggle('slow');
            $(".signin").toggleClass("menu-open");
            $('html, body').animate({scrollTop:0}, 'slow');
            return false;
        });      
    });
    </script>

    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div style="float: left; font-size: 15px;">
                        Referral Banners
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 100%; background-color: White; min-height: 450px;
                    border: 1px solid #ACAFB0;">
                    <div style="background-color: White; overflow: hidden; padding: 20px;">
                        <div id="divLoginArea" runat="server" style="float: left; padding-left: 26px; padding-right: 19px;
                            padding-top: 12px; padding-bottom: 15px;">
                            <a id="UserSignin" style="font-size: 18px; cursor: pointer; font-weight: bold;" class="topHyperLink">
                                Login to your account and copy the codes for your personal referral link!</a></div>
                        <div id="Div1" class="jobs_table" runat="server">
                            <div id="Div2" class="contactus_tableJobs" runat="server">
                                <div style="padding-left: 26px; padding-top: 11px; padding-bottom: 5px; float: left;
                                    font-size: 25px; font-weight: bold; color: #636363;">
                                    <asp:Label ID="label4" runat="server" Text="How it Works"></asp:Label></div>
                            </div>
                            <div style="color: #636363; font-size: 16px; font-weight: bold;">
                                <div style="float: left; padding-top: 12px; padding-bottom: 12px; padding-left: 26px;
                                    padding-right: 19px;" class="fontSpaceHeightRegular">
                                    Copy the code with your personal referral link attached and paste it in your site,
                                    then get $10 Tastygo Bucks every time someone buys their first Tasty deal within
                                    72 hours after clicking your widget.
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
