<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" Inherits="Default8"
    CodeFile="Default8.aspx.cs" Title="Untitled Page" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="http://tjs.sjs.sinajs.cn/open/api/js/wb.js?appkey=" type="text/javascript"
        charset="utf-8"></script>

    <script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>
    <script type="text/javascript">stLight.options({ publisher: '8f218e64-86c1-4526-8a44-fa119a1268af' });</script>
    <script type="text/javascript" src="JS/jquery.countdown.js"></script>
    <link href="CSS/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />
    <link href="CSS/MoreDeals.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery.colorbox.js"></script>

    <style type="text/css">
        .MoreDealImage
        {
            overflow: hidden;
            height: 173px;
            width: 345px;
        }
        .MoreDealImage strong
        {
            top: 0;
            display: block;
            position: absolute;
            text-align: left;
            margin: 0;
            padding: 20% 10px 10px 10px;
            width: 265px;
            height: 78px;
            left: 0;
            color: #fff;
            font-size: 15px;
            opacity: 0;
            -moz-opacity: 0;
            filter: alpha(opacity=0);
            background-image: url(                          "Images/minibg.png" );
            background-repeat: repeat;
        }
    </style>

    <script type="text/javascript">
        function ShowAddressPopUp() {

            var heightList = "320px";
            var totalCount = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_hfPopUpRowsCount").value);
            if (totalCount == "1")
                heightList = "292px";
            else if (totalCount == "2")
                heightList = "320px";
            else if (totalCount == "3")
                heightList = "370px";
            else if (totalCount == "4")
                heightList = "430px";
            else if (totalCount == "5")
                heightList = "470px";
            else if (totalCount == "6")
                heightList = "580px";
            else if (totalCount == "7")
                heightList = "630px";
            else if (totalCount == "8")
                heightList = "700px";
            else if (totalCount == "9")
                heightList = "770px";
            else if (totalCount == "10")
                heightList = "820px";
            else if (totalCount == "11")
                heightList = "890px";

            jQuery(document).ready(function () {
                $(document).ready(function () {
                    $.colorbox({
                        scrolling: false,
                        initialWidth: 1,
                        initialHeight: 1,
                        inline: true,
                        width: "677px",
                        height: heightList,
                        href: "#divPriceList",
                        opacity: 0
                    });
                });
            });

            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_imgBtnOk").click(function () {
                    setTimeout("$.colorbox.close();", 313);
                })
            });
        }

    </script>

    <script type='text/javascript'>
        var Site;
        function CountDownTimer(Year, Month, Day, Hour, Minute, DivID, SiteURL) {
            Site = SiteURL;
            var austDay = new Date();
            var newYear = new Date();
            austDay = new Date(Year, Month, Day, Hour, Minute, 0);
            $("#" + DivID).countdown({ until: austDay, compact: true, serverSync: serverTime });
        }

        function serverTime() {
            var time = null;
            $.ajax({ url: Site,
                async: false, dataType: 'text',
                success: function (text) {
                    time = new Date(text);
                }, error: function (http, message, exc) {
                    time = new Date();
                }
            });
            return time;
        }
                    
                   
                    
    </script>

    <script type="text/javascript">

        /*** 
        Simple jQuery Slideshow Script
        Released by Jon Raasch (jonraasch.com) under FreeBSD license: free to use or modify, not responsible for anything, etc.  Please link out to me if you like it :)
        ***/

        function slideSwitch() {
            var $active = $('#slideshow IMG.active');

            if ($active.length == 0) $active = $('#slideshow IMG:last');

            // use this to pull the images in the order they appear in the markup
            var $next = $active.next().length ? $active.next()
        : $('#slideshow IMG:first');

            // uncomment the 3 lines below to pull the images in random order

            // var $sibs  = $active.siblings();
            // var rndNum = Math.floor(Math.random() * $sibs.length );
            // var $next  = $( $sibs[ rndNum ] );


            $active.addClass('last-active');

            $next.css({ opacity: 0.0 })
        .addClass('active')
        .animate({ opacity: 1.0 }, 1000, function () {
            $active.removeClass('active last-active');
        });
        }

        $(function () {
            setInterval("slideSwitch()", 5000);
        });


    </script>

    <style type="text/css">
        /*** set the width and height to match your images **/#slideshow
        {
        }
        #slideshow IMG
        {
            position: absolute;
            top: 0;
            left: 0px;
            z-index: 8;
            opacity: 0.0;
        }
        #slideshow IMG.active
        {
            z-index: 10;
            opacity: 1.0;
        }
        #slideshow IMG.last-active
        {
            z-index: 9;
        }
        .SubDeal_SliderElement
        {
            border: 5px solid #CCCCCC;
            cursor: pointer;
            position: absolute;
            text-align: center;
            top: 0;
            height: 185px;
            width: 285px;
            color: Black;
        }
        .SubDeal_Bottom
        {
            background-color: #ffffff;
            height: 40px;
        }
        .SubDeal_Text
        {
            font-family: Helvetica;
            float: left;
            font-size: 12px;
            line-height: 40px;
            padding-left: 5px;
            text-align: left;
        }
        .SubDeal_PriceTag
        {
            float: right;
            height: 40px;
            width: 75px;
            background-image: url(   'Images/SubDeal_PriceTag2.png' );
        }
        .SubDeal_PriceTagText
        {
            font-family: Helvetica;
            line-height: 40px;
            text-align: center;
            color: White;
            font-size: 14px;
            font-weight: bold;
        }
    </style>
    <asp:HiddenField ID="hfPopUpRowsCount" runat="server" Value="0" />
    <div>
        <asp:UpdatePanel ID="upGridPriceList" runat="server">
            <ContentTemplate>
                <div style="float: left;">
                    <div style="float: left; padding-left: 5px;">
                        <div style="display: none">
                            <div id="divPriceList">
                                <div style="padding-top: 30px; padding-left: 19px;">
                                    <div id="divPriceList12" runat="server">
                                        <div style="clear: both;">
                                            <div style="float: left; padding-top: 5px; line-height: 20px;">
                                                <asp:Label EnableViewState="false" ID="Label10" Font-Names="Helvetica" Font-Bold="true"
                                                    Font-Size="19px" runat="server" Text="Choose your deal:"></asp:Label>
                                            </div>
                                        </div>
                                        <div style="clear: both; padding-top: 11px;">
                                            <asp:GridView EnableViewState="false" ID="grdViewPrices" runat="server" DataKeyNames="dealId"
                                                Width="592px" AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False"
                                                ShowHeader="False" ShowFooter="true" GridLines="None">
                                                <RowStyle Height="26" HorizontalAlign="Left" />
                                                <AlternatingRowStyle Height="26" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <table cellpadding="0" cellspacing="0" style="border: 1px solid #fc37b8;" width="100%"
                                                                class="DetailRightTopDiv2">
                                                                <tr>
                                                                    <td width="65%" style="padding-left: 8px;">
                                                                        <a href='<%# getSubDealURL(Eval("dealId"),Eval("Orders"),Eval("dealDelMaxLmt")) %>'
                                                                            style="text-decoration: none; color: Black; font-size: 13px;">
                                                                            <%# Eval("title") %>
                                                                        </a>
                                                                        <br />
                                                                        <div style="font-size: 13px; font-weight: bold;">
                                                                            Value: <font style="font-weight: normal;">
                                                                                <%# "C$ " + Eval("valuePrice")%></font> - Discount: <font style="font-weight: normal;">
                                                                                    <%#Convert.ToInt32(Convert.ToDouble(Convert.ToDouble(100 / Convert.ToDouble(Eval("valuePrice"))) * (Convert.ToDouble((Eval("valuePrice"))) - Convert.ToDouble(Eval("sellingPrice"))))).ToString() + "% off "%></font>
                                                                            - You Save: <font style="font-weight: normal;">
                                                                                <%# "C$ " + (Convert.ToInt32(Eval("valuePrice")) - Convert.ToInt32(Eval("sellingPrice"))).ToString()%></font>
                                                                        </div>
                                                                    </td>
                                                                    <td style="padding-right: 5px; padding-left: 5px; width: 15%;">
                                                                        <div style="font-size: 13px; float: left;">
                                                                            <%# Eval("Orders") + " bought" %></div>
                                                                    </td>
                                                                    <td style="padding-right: 5px; padding-left: 40px;">
                                                                        <div style="width: 65px; text-align: center; color: White;">
                                                                            <div style="padding-top: 5px;">
                                                                                <font style="font-weight: bold; font-size: 19px;"><a href='<%# getSubDealURL(Eval("dealId"),Eval("Orders"),Eval("dealDelMaxLmt")) %>'
                                                                                    style="text-decoration: none; color: White;">
                                                                                    <%# getSubDealPrice(Eval("dealDelMaxLmt"), Eval("Orders"), Eval("sellingPrice"))%></a></font></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: both; padding-top: 10px">
        <asp:Panel ID="pbnBanner" runat="server" Style="height: auto; width: 100%; margin-bottom: 10px;">
            <asp:Literal ID="ltBanner" runat="server"></asp:Literal>
        </asp:Panel>
        <div class="DetailPageTopDiv" style="background-color: White;">
            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                <div style="float: left;">
                    <span style="color: #ff42e7; font-weight: bold;">Today's Hot Deal</span>
                </div>
            </div>
        </div>
        <div class="DetailPage2ndDiv">
            <div style="clear: both;">
                <div style="float: left; width: 464px;">
                    <div>
                        <div style="clear: both; width: 100%; min-height: 110px; background-color: White;
                            vertical-align: middle; border: 1px solid #ACAFB0">
                            <div style="clear: both; padding-top: 20px; font-size: 18px; font-weight: bold; padding-left: 15px;
                                padding-right: 10px; line-height: 22px;">
                                <asp:Label ID="lblDealTitle" runat="server" Text="Doggie A Taste of Punjab - Surrey"></asp:Label>
                            </div>
                            <div style="clear: both; padding-top: 5px; font-size: 14px; font-weight: normal;
                                padding-left: 15px; padding-bottom: 15px; padding-right: 10px;">
                                <asp:Label ID="lblDealTopTitle" runat="server" Text="Bow Wow Haus"></asp:Label>
                            </div>
                        </div>
                        <div style="clear: both; width: 100%; position: relative; height: 333px; border: 1px solid #ACAFB0;
                            border-top: none;">
                            <asp:Literal ID="ltSlideShow" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%; padding: 10px 0px 0px 0px;">
                        <div style="border: 1px solid #ACAFB0; border-bottom: 0px none;" class="DetailTheDetailDiv">
                            <div style="clear: both; padding: 10px 0px 0px 22px; color: #ff42e7;">
                                The Details
                            </div>
                            <div style="clear: both; height: 1px; border-bottom: solid 1px #ACAFB0; background-color: White;
                                margin-left: 15px; margin-right: 15px; margin-top: 10px;">
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%; border: 1px solid #ACAFB0; border-top: 0 none;
                        background-color: White;">
                        <div style="clear: both; padding: 30px 20px 30px 20px; font-size: 12px;">
                            <div style="clear: both; font-weight: bold;">
                                Fine Print:</div>
                            <div style="clear: both;">
                                <asp:Label ID="lblFinePrint" runat="server" Text="Highlights display here"></asp:Label>
                            </div>
                        </div>
                        <div style="clear: both; padding: 0px 20px 30px 20px; font-size: 12px;">
                            <div style="clear: both; font-weight: bold;">
                                Highlights:</div>
                            <div style="clear: both;">
                                <asp:Label ID="lblHighlights" runat="server" Text="Highlights display here"></asp:Label>
                            </div>
                            <div style="clear: both; width: 100%; height: 1px; padding-top: 20px; border-bottom: solid 1px #e6e6e5;">
                            </div>
                        </div>
                        <div style="clear: both; padding: 0px 20px 30px 20px;">
                            <div style="clear: both;">
                                <asp:Literal ID="lblDealDetail" runat="server" Text="Deal Detail"></asp:Literal></div>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 248px; padding-left: 10px;">
                    <div style="clear: both; background-color: White; overflow: hidden; border: 1px solid #ACAFB0">
                        <div class="DetailRightTopDiv2">
                            <div style="clear: both; width: 100%;">
                                <div style="float: left; width: 125px; font-weight: normal; font-size: 13px; color: Black;
                                    padding-top: 15px; padding-left: 20px;">
                                    <div style="clear: both;">
                                        <asp:Label ID="lblValuePrice" runat="server" Text="<b>Value:</b> $75"></asp:Label>
                                    </div>
                                    <div style="clear: both;">
                                        <asp:Label ID="lblDealDiscount" runat="server" Text="<b>Discount:</b> 61%"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: right; font-size: 22px; color: White; font-weight: bold; padding-right: 30px;
                                    padding-top: 25px;">
                                    <asp:Label ID="lblDealPrice" runat="server" Text="$29"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="height: 18px;">
                        </div>
                        <div class="DetailRight2ndDiv">
                            <div style="clear: both; padding-left: 11px; padding-top: 15px;">
                                <a href='<%=strCheckOutLink %>' style="text-decoration: none;">
                                    <div id="MyBtn" class="BtnBuy">
                                        <div style="padding-top: 30px;">
                                            <%=strDealExpiredText%>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div style="clear: both; padding-left: 10px; padding-top: 18px; padding-bottom: 18px;">
                                <div style="float: left">
                                    <span class="st_sharethis_custom" st_url='<%=strShareLink %>'>
                                        <div style="width: 108px; height: 29px; background-color: #55ace9">
                                            <div style="text-align: center; font-size: 13px; color: White; padding-top: 7px;">
                                                <b>+</b> Share Deal
                                            </div>
                                        </div>
                                    </span>
                                </div>
                                <div style="float: left; padding-left: 10px;">
                                    <a href='<%=strCheckOutLink %>' style="text-decoration: none;">
                                        <div style="width: 108px; height: 29px; background-color: #55ace9">
                                            <div style="float: left; padding-top: 6px; padding-left: 8px;">
                                                <img src="Images/GiftIcon.png" />
                                            </div>
                                            <div style="float: left; font-size: 13px; color: White; padding-top: 7px; padding-left: 8px;">
                                                Gift This
                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="height: 10px;">
                    </div>
                    <div class="DetailRight2ndDiv" style="border: 1px solid #ACAFB0">
                        <div style="clear: both; padding-left: 10px; padding-top: 20px;">
                            <div style="float: left; padding-top: 6px;">
                                <img id="imgClock" src="Images/detailpageClock.png" />
                            </div>
                            <div style="float: left; font-size: 13px; padding-left: 10px; width: 75px; padding-top: 5px;">
                                Time Left:
                            </div>
                            <div class="TastyCountDown" style="float: left; width: 100px; background-color: #f2f2f3;">
                                <div id="defaultCountdown" align="center">
                                </div>
                            </div>
                        </div>
                        <div style="clear: both; padding-left: 10px; padding-top: 10px;">
                            <div style="float: left; padding-top: 6px;">
                                <img id="img1" src="Images/detailpageTag.png" />
                            </div>
                            <div style="float: left; font-size: 13px; padding-left: 10px; width: 73px; padding-top: 5px;">
                                Sold:
                            </div>
                            <div class="TastyCountDown" style="float: left; width: 100px; background-color: #f2f2f3;
                                text-align: center;">
                                <asp:Label ID="lblDealsSold" runat="server" Text="100"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="height: 10px;">
                    </div>
                    <div class="DetailRight2ndDiv" style="border: 1px solid #ACAFB0">
                        <div style="clear: both; padding-left: 10px; padding-top: 20px;">
                            <div style="font-size: 13px; text-align: center; text-decoration: none;">
                                <a href="../howtorefer.aspx" style="text-decoration: none;">Refer a Friend and Earn
                                    $10 </a>
                            </div>
                        </div>
                        <div style="clear: both; padding-left: 10px; padding-top: 10px;">
                            <div style="float: left; padding-right: 5px; padding-top: 2px;
                                width: 50px;">
                                <wb:share-button relateuid="2787221352">
                                </wb:share-button>
                            </div>
                            <div style="float: left; width: 85px;">
                                <a href="https://twitter.com/share" class="twitter-share-button" data-url='<%=strFBString%>'>
                                    Tweet</a>

                                <script>                                    !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>

                            </div>
                            <div style="float: left; padding-right: 5px; padding-left: 5px; width: 70px;">
                                <fb:like href='<%=strFBString%>' data-send="false" data-layout="button_count" data-width="450"
                                    data-show-faces="false" data-font="arial"></fb:like>
                            </div>
                        </div>
                    </div>
                    <div style="height: 9px;">
                    </div>
                    <div style="border: 1px solid #ACAFB0; width: 100%;">
                        <div class="DetailTheDetailDiv" style="background-color: White; color: #ff42e7;">
                            <center>
                                <div style="clear: both; padding-top: 10px; width: 90%; text-align: left;">
                                    Company Info
                                </div>
                            </center>
                            <div style="clear: both; height: 1px; border-bottom: solid 1px #ACAFB0; background-color: White;
                                margin-left: 15px; margin-right: 15px; margin-top: 10px;">
                            </div>
                        </div>
                        <div class="BusinessDetailArea">
                            <div style="clear: both; padding-top: 20px; text-align:center">
                                <div style="clear: both;">
                                    <asp:Image ID="imgBusinessLogo" Visible="false" runat="server" />
                                </div>
                                <div style="clear: both; font-size: 15px; font-weight: bold; color: #5f5f5f; padding-top:5px;">
                                    <asp:Label ID="lblBusinessName" runat="server" Text="Bow Wow Haus"></asp:Label>
                                </div>
                                <div style="clear: both; font-size: 13px; padding-top: 5px;">
                                    <asp:Label ID="lblBusinessPhone" runat="server" Text="Bow Wow Haus"></asp:Label>
                                </div>
                                <div style="font-size: 13px; padding-top: 5px;">
                                    <asp:HyperLink ID="HPBusiness" runat="server"></asp:HyperLink>
                                </div>
                                <div style="font-size: 13px; padding-top: 5px;">
                                    <asp:Literal ID="ltYelpStart" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div style="clear: both; padding-top: 15px; text-align: center;">
                                <asp:Image ID="imgGoogleMap" Width="225px" runat="server" ImageUrl="~/Images/dealfood/48/48.png" />
                            </div>
                            <div style="clear: both; padding-left: 20px; padding-top: 10px; font-size: 13px;">
                                <asp:DataList runat="server" ID="dlGooglePaths" DataKeyField="rgaID" AllowSorting="false"
                                    AutoGenerateColumns="false" RepeatColumns="1" RepeatDirection="Horizontal" CellPadding="0"
                                    CellSpacing="0" GridLines="None" ShowHeader="false">
                                    <ItemTemplate>
                                        <div style="padding-top: 15px;">
                                            <div style="float: left;">
                                                <img src='<%# "Images/gmapIcons/"+Convert.ToString(Container.ItemIndex+1)+".png" %>' />
                                            </div>
                                            <div style="float: left; padding-left: 5px; font-size: 13px; padding-right: 5px;
                                                width: 175px;">
                                                <div style="clear: both;">
                                                    <div>
                                                        <%#Eval("restaurantGoogleAddress")%>
                                                    </div>
                                                    <div>
                                                        <a title="Get Direction" target="_blank" style="color: #096bc7;" href='<%# "http://maps.google.com/maps?f=d&daddr="+Eval("restaurantGoogleAddress") %>'>
                                                            Get Direction</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="float: left; padding-left: 10px; width: 247px;">
                    <div style="position: relative;">
                        <div style="position: absolute; right: 247px; top: 50px; z-index: 100;">
                            <img src="Images/PointingArrow.png" />
                        </div>
                        <div class="DetailRight2ndDiv shadow">
                            <div style="padding-top: 10px; padding-left: 10px; color: #32ccfe; font-weight: bold;
                                font-size: 15px; padding-bottom: 10px;">
                                Today's Deal Discussion</div>
                            <div style="padding: 10px;">
                                <div style="float: left;">
                                    <div>
                                        <asp:Image ID="imgCommentUserImage" runat="server" ImageUrl="~/Images/disImg.gif"
                                            Height="70px" Width="70px" class="Tipsy" />
                                    </div>
                                    <div style="clear: both; width: 70px; padding-top: 5px; text-align: center; color: #5a5a5a;
                                        display: none;">
                                        <div>
                                            <asp:Label ID="lblDealsSold2" runat="server" Text="100"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblTotalPosts" runat="server" Text="Posts : 0"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: right;">
                                    <div style="font-size: 14px; text-align: left; width: 150px; font-weight: bold; color: #5a5a5a;">
                                        <asp:Label ID="lblCommentUserName" runat="server" Text="Sohail Ahmad"></asp:Label>
                                    </div>
                                    <div>
                                        <div style="width: 150px;">
                                            <asp:Label ID="lblDealDiscuessionComment" class="Tipsy" runat="server" Text="Hi, just want to know what if we're not on Facebook and Twitter..."></asp:Label>
                                        </div>
                                        <a href='<%=strDealDiscuessionLink %>'>
                                            <div class="button Tipsy" title="Click here to participate in <b>discussion</b> of Today's Tasty deal."
                                                style="margin-top: 5px; width: 120px; text-align: center;">
                                                Join Today's Discussion</div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="height: 10px;">
                        </div>
                        <div class="DetailTheDetailDiv" style="color: #32ccfe;">
                            <div style="clear: both; padding: 10px 0px 0px 20px;">
                                More Deals
                            </div>
                        </div>
                        <div style="clear: both;">
                            <asp:GridView EnableViewState="false" runat="server" ID="gridDeals" AllowPaging="false"
                                AllowSorting="false" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0"
                                GridLines="None" HorizontalAlign="Center" ShowHeader="false" RowStyle-HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="margin-bottom: 5px;">
                                            <a href='<%# Convert.ToString(Eval("urlTitle")).Trim()==""?ConfigurationManager.AppSettings["YourSite"]+ "/" + strCurrentCityName +"_"+ Eval("dealId"):ConfigurationManager.AppSettings["YourSite"]+ "/" + strCurrentCityName +"/"+ Eval("urlTitle") %>'                                                                                               
                                                <div style="height: auto; width: 100%;">
                                                    <div style="position: relative">
                                                        <div style="clear: both;" align="center">
                                                            <img id="imgDeal" src='<%# "Images/dealfood/" + DataBinder.Eval (Container.DataItem,"restaurantId").ToString().Trim() + "/" + DataBinder.Eval (Container.DataItem,"image1").ToString().Trim() %>'
                                                                width="247" height="177" />
                                                        </div>
                                                    </div>
                                                    <div style="border: 1px solid #ACAFB0; height: auto; overflow: hidden; background-color: White;
                                                        padding-top: 5px; padding-left: 5px; padding-right: 5px; padding-bottom: 5px;"
                                                        align="left">
                                                        <div>
                                                            <div style="float: left;">
                                                                <div style="font-weight: bold; font-size: 12px; color: Black;">
                                                                    <span class="Tipsy" title='<%# Convert.ToString(Eval("shortTitle")).ToString().Trim() != "" ? Convert.ToString(Eval("shortTitle")).ToString().Trim() : (Convert.ToString(Eval("dealPageTitle")).ToString().Trim() != "" ? Convert.ToString(Eval("dealPageTitle")).ToString().Trim() : Eval("title"))%>'>
                                                                        <%# Convert.ToString(Eval("shortTitle")).ToString().Trim() != "" ? Convert.ToString(Eval("shortTitle")).ToString().Trim().Length > 20 ? Convert.ToString(Eval("shortTitle")).ToString().Trim().Substring(0, 17) + "..." : Eval("shortTitle").ToString().Trim() : (Convert.ToString(Eval("dealPageTitle")).ToString().Trim() != "" ? Convert.ToString(Eval("dealPageTitle")).ToString().Trim().Length > 20 ? Convert.ToString(Eval("dealPageTitle")).ToString().Trim().Substring(0, 17) + "..." : Eval("dealPageTitle").ToString().Trim() : Eval("title"))%>
                                                                    </span>
                                                                </div>
                                                                <div style="font-size: 12px; color: #6c6c6c; font-weight: bold;">
                                                                    <%#  Convert.ToString(Eval("restaurantBusinessName")).ToString().Trim().Length > 20 ? Convert.ToString(Eval("restaurantBusinessName")).ToString().Trim().Substring(0,17) + "..." : Eval("restaurantBusinessName").ToString().Trim() %>
                                                                </div>
                                                            </div>
                                                            <div style="float: right; height: 35px; width: 95px; position: relative;">
                                                                <div style="clear: both; right: 0px; bottom: 0px; position: absolute;">
                                                                    <img src="Images/PriceTag_New.png" height="34px" width="91" />
                                                                </div>
                                                                <div style="clear: both; right: 30px; bottom: 8px; position: absolute; color: White;
                                                                    font-size: 14px; font-weight: bold; text-decoration: none;" class="shadowText">
                                                                    <%-- <%# Convert.ToString(Convert.ToInt32((100 / float.Parse(Eval("valuePrice").ToString())) * (float.Parse(Eval("valuePrice").ToString()) - float.Parse(Eval("sellingPrice").ToString()))).ToString()+"% OFF")%>--%>
                                                                    <%# ("$" + Eval("sellingPrice").ToString())%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--  <div style="clear: both; width: 100%;">
                                                                 <div style="font-size: 12px; color: #6c6c6c; float: left;">
                                                                    <%# Eval("Orders")+" Buys" %>
                                                                </div>
                                                                 <div id='<%# Container.DataItemIndex %>' style="float: right; font-size: 12px; color: #6c6c6c;">

                                                                  <script type="text/javascript">
                                                                 <%# "CountDownTimer("+Convert.ToDateTime(Eval("dealEndTime")).Year +","+ (Convert.ToDateTime(Eval("dealEndTime")).Month - 1) +"," + Convert.ToDateTime(Eval("dealEndTime")).Day + "," + Convert.ToDateTime(Eval("dealEndTime")).Hour + "," + Convert.ToDateTime(Eval("dealEndTime")).Minute +"," + Container.DataItemIndex + ",'" + System.Configuration.ConfigurationManager.AppSettings["YourSite"].ToString() + "/getStateLocalTime.aspx?sid=" + Eval("provinceId").ToString().Trim() +"');" %>
                                                                    </script>

                                                                </div>
                                                            </div>--%>
                                                    </div>
                                                </div>
                                             </a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Literal ID="ltCountDown" runat="server"></asp:Literal>
</asp:Content>
