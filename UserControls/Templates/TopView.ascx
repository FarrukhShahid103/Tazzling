<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopView.ascx.cs" Inherits="Takeout_UserControls_Templates_topview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc3" %>
<div id="fb-root">
</div>
<script src="https://connect.facebook.net/en_US/all.js"></script>
<link rel="stylesheet" href="CSS/Tastymenu.css" type="text/css" media="screen" />
<script>

    function MouseOver(ID) {

        if (ID == "MySubscriptions") {
            $("." + ID).animate({ "text-indent": "30px", "font-weight": "bold" }, "fast");
        }
        else {
            $("." + ID).animate({ "text-indent": "40px", "font-weight": "bold" }, "fast");
        }
    }

    function MouseLeave(ID) {

        $("." + ID).animate({ "text-indent": "5px", "font-weight": "normal" }, "fast");
    }


    FB.init({
        appId: '160996503945227',
        oauth: true,
        status: true,
        cookie: true,
        xfbml: true
    });
    FB.Event.subscribe('auth.login', function (response) {

        var ACC_Tokken = response.authResponse.accessToken;



        var c_login = 'tastygoLogin';
        c_login_start = -1;

        if (document.cookie.length > 0) {
            c_login_start = document.cookie.indexOf(c_login + "=");
        }
        if (c_login_start == -1) {
            $.ajax({
                type: "POST",
                url: "getStateLocalTime.aspx?FBLogin=" + ACC_Tokken,
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg == "true") {
                        window.location.href = "Default.aspx";
                    }


                }

            });

        }

        //window.location.reload();
    });
					
			
</script>
<script type='text/javascript'>

    jQuery(document).ready(function () {

        $("#downMenu").click(function () {
            $("#menudropped").toggle("fast");
        });

    });
</script>
<script type='text/javascript'>
    function fbLogout() {
        FB.init({ appId: '160996503945227',
            oauth: true,
            status: true,
            cookie: true,
            xfbml: true
        });
        FB.logout(function (response) {
        });

    }

    function fbConnect() {
        FB.init({ appId: '160996503945227',
            oauth: true,
            status: true,
            cookie: true,
            xfbml: true
        });
        FB.login(function (response) {
            if (response.authResponse) {
                if (response.perms) {
                    // user is logged in and granted some permissions.
                    // perms is a comma separated list of granted permissions
                    window.location.reload();
                } else {
                    // user is logged in, but did not grant any permissions
                }
            } else {
                // user is not logged in
            }
        }, { scope: 'read_stream,publish_stream,offline_access,email' });
    }

    function fblogin() {

        FB.init({ appId: '160996503945227',
            status: true,
            cookie: true,
            xfbml: true
        });

        FB.getLoginStatus(function (response) {
            if (response.authResponse) {
                // logged in and connected user, someone you know

                window.location.reload();
            } else {

                document.location.href = 'login.aspx';
                // no user session available, someone you dont know
            }
        });

    }           
            
</script>
<script language="javascript" type="text/javascript">

    function validateEmail(oSrc, args) {


        if (args.Value == '') {
            $("#" + oSrc.controltovalidate).removeClass("watermark_topSubscribeTextBox");
            $("#" + oSrc.controltovalidate).removeClass("topSubscribeTextBox");
            $("#" + oSrc.controltovalidate).addClass("ErrorSubscribe");
            args.IsValid = false;
            return;
        }
        else {
            args.IsValid = true;

            return;
        }
    }
        
    
</script>
<style type="text/css">
    .CategoryMenu
    {
         color: Black;
        cursor: pointer;
        float: left;
        font-family: arial;
        font-size: 14px;
        min-height: 27px;
        overflow: hidden;
        padding: 5px;
    }
    .CategoryMenu:hover
    {
        float: left;
        padding: 5px;
        padding-bottom:0px;
        background-color: #f0f0f0;
        border-radius: 5px 5px 5px 5px;
    }
    .CategoryIcon
    {
        float: left;
    }
    .CategoryTitle
    {
        float: left;
        padding-left: 5px;
        padding-top: 2px;
    }
    .CategoryTitle a
    {
        font-size: 14px;
        color: Black;
    }
    .MenuBottomLine
    {
        border-bottom: 1px solid #FF42E7;
        clear: both;
        height: 1px;
        left: 0;
        margin-top: 48px;
        position: absolute;
        width: 100%;
    }
    .MenuMarginLeft
    {
        margin-left: 22px;
    }
    
  
    
</style>

<div id="topZone">
    <div style="clear: both; display:none;">
        <div style="float: left; padding-top: 10px;">
            <a href='<%=strHomePageSite %>'>
                <img src="images/logosmall.png" />
            </a>
        </div>
        <div style="float: left; padding-left: 10px;">
            <div style="float: left;">
                <a href='#' style="text-decoration: none;">
                    <div class="menu_tazzling_Left">
                    </div>
                    <div class="menu_tazzling_middle">
                        <a href="feeds.aspx">
                            <div style="padding-top: 7px;">
                                <img src='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/Images/menu_feed.png" %>'></img></div>
                            <div>
                                Feed
                            </div>
                        </a>
                    </div>
                    <div class="menu_tazzling_right">
                    </div>
                </a>
            </div>
            <div style="float: left;">
                <a href='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/UpcomingDeals.aspx" %>'
                    style="text-decoration: none;">
                    <div class="menu_tazzling_middle">
                        <div style="padding-top: 7px;">
                            <img src='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/Images/menu_calendar.png" %>'></img>
                        </div>
                        <div>
                            Calander
                        </div>
                    </div>
                    <div class="menu_tazzling_right">
                    </div>
                </a>
            </div>
            <div style="float: left;">
                <a href='#' style="text-decoration: none;">
                    <div class="menu_tazzling_middle">
                        <div style="padding-top: 7px;">
                            <img src='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/Images/menu_mobile.png" %>'></img></div>
                        <div>
                            Mobile
                        </div>
                    </div>
                    <div class="menu_tazzling_right">
                    </div>
                </a>
            </div>
            <div style="float: left;">
                <a href='#' style="text-decoration: none;">
                    <div class="menu_tazzling_middle">
                        <div style="padding-top: 7px;">
                            <img src='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/Images/menu_invite.png" %>'></img></div>
                        <div>
                            Invite
                        </div>
                    </div>
                    <div class="menu_tazzling_right">
                    </div>
                </a>
            </div>
            <div style="float: left;">
                <a href='#' style="text-decoration: none;">
                    <div class="menu_tazzling_middle">
                        <div style="padding-top: 7px;">
                            <img src='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/Images/menu_how-it-work.png" %>'></img></div>
                        <div>
                            How it works
                        </div>
                    </div>
                    <div class="menu_tazzling_right">
                    </div>
                </a>
            </div>
        </div>
        <div style="float: right;">
            <div style="float: right;">
                <asp:Panel ID="PnlSignin" runat="server">
                    <div style="float: right;">
                        <a id="hlSignin" href='<%= ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "login.aspx" %>'
                            class="Top_User_Label">Login</a>
                    </div>
                    <div style="float: right; color: White; padding-left: 10px; padding-right: 10px;">
                        |
                    </div>
                    <div style="float: right;">
                        <a id="hlJoin" href='<%= ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "signup.aspx" %>'
                            class="Top_User_Label">Sign up</a>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlUser" runat="server">
                    <div>
                        <div style="float: right; position: relative; min-width: 125px; padding-top: 12px;">
                            <div class="UserName">
                                <ul id="TastyMenu" style="margin: 0 0 0 0;">
                                    <li class="menu_right" style="padding-top: 10px;"><a href="#" class="drop UserName">
                                        <%= UserName %></a>
                                        <div class="dropdown_1column align_right">
                                            <div class="col_1" style="width: 150px;">
                                                <ul class="simple">
                                                    <li><a onmouseout="javascript:MouseLeave('MyTastyGo');" onmouseover="javascript:MouseOver('MyTastyGo');"
                                                        class="TopMenu icon MyTastyGo" href="member_MyTastygo.aspx">My Tastygo</a></li>
                                                    <li><a onmouseout="javascript:MouseLeave('MyGifts');" onmouseover="javascript:MouseOver('MyGifts');"
                                                        class="TopMenu icon MyGifts" href="member_MyGiftTastygo.aspx">My Gifts</a></li>
                                                    <li><a onmouseout="javascript:MouseLeave('MyAccount');" onmouseover="javascript:MouseOver('MyAccount');"
                                                        class="TopMenu icon MyAccount" href="member_profile.aspx">My Account</a></li>
                                                    <li><a onmouseout="javascript:MouseLeave('MyPreference');" onmouseover="javascript:MouseOver('MyPreference');"
                                                        class="TopMenu icon MyPreference" href="member_preference.aspx">My Preference</a></li>
                                                    <li style='display: <%=strBusinessOwner%>;'><a onmouseout="javascript:MouseLeave('MyBusiness');"
                                                        onmouseover="javascript:MouseOver('MyBusiness');" class="TopMenu icon MyBusiness"
                                                        href='<%= ConfigurationManager.AppSettings["YourSite"].ToString() + "/UserDashBoard.aspx" %>'>
                                                        My Business</a></li>
                                                    <li><a id="MyGifts" onmouseout="javascript:MouseLeave('MySubscriptions');" onmouseover="javascript:MouseOver('MySubscriptions');"
                                                        class="TopMenu icon MySubscriptions" href="member_SubscribeCities.aspx">My Subscriptions</a></li>
                                                    <li><a id="Referral" onmouseout="javascript:MouseLeave('Referral');" onmouseover="javascript:MouseOver('Referral');"
                                                        class="TopMenu icon Referral" href="member_referral.aspx">Refferal</a></li>
                                                    <li>
                                                        <asp:LinkButton onmouseout="javascript:MouseLeave('Signout');" onmouseover="javascript:MouseOver('Signout');"
                                                            CssClass="TopMenu icon Signout" ID="lnkBtnLogOut" OnClientClick="fbLogout();"
                                                            runat="server" Text="Sign out" OnClick="lnkBtnLogOut_Click"></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                            <%--<a href="#" style="font-size: 16px !important; font-weight: bold;">Sign out</a>--%>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div style="">
                            </div>
                            <div style="clear: both; position: absolute; padding-top: 5px; right: 0px;">
                            </div>
                        </div>
                        <div style="float: right; padding-right: 20px;">
                            <div>
                                <asp:Image runat="server" ID="imgLoginUser" Style="height: 51px; width: 51px;" ImageUrl="~/Images/disImg.gif" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlForOldBrowserUsers" runat="server" Visible="false">
                    <div style="height: 30px;">
                        <div style="clear: both; padding-top: 0px;">
                            <div class="UserName" style="float: right;">
                                <%= UserName %></div>
                        </div>
                        <div style="clear: both; padding-top: 0px;">
                            <div style="float: right;">
                                <a id="aMemeberArea" href='<%= ConfigurationManager.AppSettings["YourSite"].ToString() + "/member_MyTastygo.aspx" %>'
                                    class="Top_User_Label">Member Area</a>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="float: right; padding-right: 10px; height: 51px; width: 50px; background-color: #918886;">
                <div style="padding: 17px 0px 0px 10px;">
                    <a id="hlCartTextHeading" href='<%= ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "cart.aspx" %>'
                        class="Top_User_Label">
                        <div style="float: left;">
                            <img id="imgCart" src='<%= ConfigurationManager.AppSettings["YourSite"].ToString()+"/Images/menu_cart.png" %>' /></div>
                        <div style="float: left; padding-left: 3px;">
                            <asp:Label ID="hlCartText" runat="server" Text="(0)"></asp:Label></div>
                    </a>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <div style="clear: both;">
        <div style="float: left; padding-top: 5px;">
            <asp:Literal Visible="false" ID="ltCategoryTopMenu" runat="server"></asp:Literal>
            <div style="clear: both;">
                <div class="CategoryMenu">
                    <a href="Default.aspx">
                        <div class="CategoryIcon">
                            <img src="images/Menu_NewDeals.png" title="New Deals" />
                        </div>
                        <div class="CategoryTitle">
                        <asp:Label ID="lblNewDeals" runat="server" Text="New Deals"></asp:Label>
                        </div>
                    </a>
                </div>
                <div class="CategoryMenu MenuMarginLeft">
                    <a href="category.aspx?pcid=2">
                        <div class="CategoryIcon">
                            <img src="images/Menu_Fashion.png" title="Fashion" />
                        </div>
                        <div class="CategoryTitle">
                        <asp:Label ID="lblFashion" runat="server" Text="Fashion"></asp:Label>
                        </div>
                    </a>
                </div>
                <div class="CategoryMenu MenuMarginLeft">
                    <a href="category.aspx?pcid=3">
                        <div class="CategoryIcon">
                            <img src="images/Menu_HomeAndDoctor.png" title="Home & Doctor" />
                        </div>
                        <div class="CategoryTitle">
                        <asp:Label ID="lblHomeDoctor" runat="server" Text="Home & Doctor"></asp:Label>
                            
                        </div>
                    </a>
                </div>
                <div class="CategoryMenu MenuMarginLeft">
                    <a href="category.aspx?pcid=4">
                        <div class="CategoryIcon">
                            <img src="images/Menu_TechGadgets.png" title="Tech Gadgets" />
                        </div>
                        <div class="CategoryTitle">
                        <asp:Label ID="lblTechGadgets" runat="server" Text="Tech Gadgets"></asp:Label>
                            
                        </div>
                    </a>
                </div>
                <div class="CategoryMenu MenuMarginLeft">
                    <a href="category.aspx?pcid=5">
                        <div class="CategoryIcon">
                            <img src="images/Menu_PopularServices.png" title="Popular Services" />
                        </div>
                        <div class="CategoryTitle">
                        <asp:Label ID="lblPopularServices" runat="server" Text="Popular Services"></asp:Label>
                            
                        </div>
                    </a>
                </div>
                <div class="CategoryMenu MenuMarginLeft">
                    <a href="category.aspx?pcid=6">
                        <div class="CategoryIcon">
                            <img src="images/Menu_Health.png" title="Health" />
                        </div>
                        <div class="CategoryTitle">
                        <asp:Label ID="lblHealth" runat="server" Text="Health"></asp:Label>
                            
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
