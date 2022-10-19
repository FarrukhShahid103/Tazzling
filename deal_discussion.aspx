<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="deal_discussion.aspx.cs"
    Inherits="detail" Title="Untitled Page" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="http://tjs.sjs.sinajs.cn/open/api/js/wb.js?appkey=" type="text/javascript"
        charset="utf-8"></script>

    <script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>

    <script type="text/javascript">stLight.options({ publisher: '8f218e64-86c1-4526-8a44-fa119a1268af' });</script>

    <script type="text/javascript" src="JS/jquery.countdown.js"></script>

    <link href="CSS/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery.colorbox.js"></script>

    <script type="text/javascript">
        function CancelComment(ID) {
            hideShowDiv(ID);
            return false;
        }
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

        $(function () {
            $("#accordion").accordion({ collapsible: true, active: -1, autoHeight: false });
        });

    </script>

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

    <script type="text/javascript">
        $(document).ready(function () {

            $("#ctl00_ContentPlaceHolder1_hLinkSignIn").click(function (e) {
                e.preventDefault();
                $("fieldset#signin_menu").toggle();
                $(".signin").toggleClass("menu-open");
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                return false;
            });
        });

        function EmptyFieldvalidate(oSrc, args) {
            if (args.Value != "") {
                args.IsValid = true;
                return;
            }
            else {
                $("#" + oSrc.controltovalidate).addClass("TextBoxError");
                args.IsValid = false;
                return;
            }

        }

        function validateEmptyField(txtComment) {

            if ($("#" + txtComment).val() != "") {
                return true;
            }
            else {
                $("#" + txtComment).addClass("TextBoxError");
                return false;
            }
        }

        function hideShowDiv(divID) {

            var Commentclass = $("#" + divID).attr("class");

            if (Commentclass == "hideComment") {


                $("#" + divID).show('slow');
                var textAreaID = divID.replace("pnlFooter", "txtSubComment");
                //$(window).scrollTop($("#"+divID).offset().top-300); 
                $('html, body').animate({ scrollTop: $("#" + divID).offset().top - 300 }, 'slow')
                $("#" + textAreaID).focus();
                document.getElementById(divID).setAttribute("class", "showComment")


            }
            else {

                $("#" + divID).hide('slow');
                document.getElementById(divID).setAttribute("class", "hideComment");

            }

            return false;
        }
                                                           
        
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
                                                                        <div style="width: 60px; text-align: center; color: White;">
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
    <div style="clear: both; padding-top: 20px; float: left;">
        <div class="DetailPageTopDiv">
            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                <div style="float: left;">
                    <a class="DetailHyperLink" id="a1" href="Default.aspx">All Deals</a>
                </div>
                <div style="float: left; padding-left: 3px;">
                    »
                </div>
                <div style="float: left; padding-left: 3px;">
                    <asp:Label ID="lblDealShortTitle" runat="server" Text="Doggie Birthday Pawty Catering Package"></asp:Label>
                </div>
            </div>
        </div>
        <div class="DetailPage2ndDiv">
            <div style="clear: both;">
                <div style="float: left; width: 464px;">
                    <div style="clear: both; width: 100%; background-color: White;">
                        <div style="clear: both; padding-top: 20px; min-height: 33px; font-size: 18px; font-weight: bold;
                            padding-left: 15px; padding-right: 10px; line-height: 22px;">
                            <asp:Label ID="lblDealTitle" runat="server" Text="Doggie A Taste of Punjab - Surrey"></asp:Label>
                        </div>
                        <div style="clear: both; padding-top: 5px; font-size: 14px; font-weight: normal;
                            padding-left: 15px; padding-bottom: 15px;">
                            <asp:Label ID="lblDealTopTitle" runat="server" Text="Bow Wow Haus"></asp:Label>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%; position: relative; height: 350px;">
                        <asp:Literal ID="ltSlideShow" runat="server"></asp:Literal>
                    </div>
                </div>
                <div style="float: left; width: 248px; padding-left: 10px;">
                    <div style="clear: both; background-color: White; overflow: hidden;">
                        <div class="DetailRightTopDiv2">
                            <div style="clear: both; width: 100%;">
                                <div style="float: left; width: 125px; font-weight: normal; font-size: 13px; color: Black;
                                    padding-top: 15px; padding-left: 20px;">
                                    <div style="clear: both;">
                                        <asp:Label ID="lblValuePrice" runat="server" Text="<b>Value:</b> $00"></asp:Label>
                                    </div>
                                    <div style="clear: both;">
                                        <asp:Label ID="lblDealDiscount" runat="server" Text="<b>Discount:</b> 00%"></asp:Label>
                                    </div>
                                </div>
                                <div style="float: right; font-size: 22px; color: White; font-weight: bold; padding-right: 30px;
                                    padding-top: 25px;">
                                    <asp:Label ID="lblDealPrice" runat="server" Text="$00"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="height: 10px;">
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
                            <div style="clear: both; padding-left: 10px; padding-top: 15px; padding-bottom: 15px;">
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
                    <div class="DetailRight2ndDiv">
                        <div style="clear: both; padding-left: 10px; padding-top: 20px;">
                            <div style="float: left; padding-top: 6px;">
                                <img id="img2" src="Images/detailpageClock.png" />
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
                                <img id="img3" src="Images/detailpageTag.png" />
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
                    <div class="DetailRight2ndDiv">
                        <div style="clear: both; padding-left: 10px; padding-top: 20px;">
                            <div style="font-size: 13px; text-align: center; text-decoration: none;">
                                <a href="../howtorefer.aspx" style="text-decoration: none;">Refer a Friend and Earn
                                    $10 </a>
                            </div>
                        </div>
                        <div style="clear: both; padding-left: 10px; padding-top: 10px;">
                            <div style="float: left; padding-right: 5px; padding-top: 2px; width: 50px;">
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
                    <div style="height: 10px;">
                    </div>
                </div>
            </div>
            <div style="clear: both; width: 725px;">
                <div style="clear: both;">
                    <div style="clear: both; width: 100%; padding: 10px 0px 10px 0px;">
                        <div class="DetailTheDetailDiv">
                            <div style="clear: both; padding: 10px 0px 0px 20px;">
                                Discussion
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 10px; width: 100%; background-color: White;">
                        <div style="clear: both;">
                            <div style="width: 100%; clear: both;">
                                <asp:HiddenField ID="hfDealId" runat="server" />
                                <asp:UpdatePanel ID="upComment" runat="server">
                                    <ContentTemplate>
                                        <div style="clear: both; padding: 15px;">
                                            <asp:DataList ID="rptrDiscussion" Style="background-color: #FAFAFA" RepeatColumns="1"
                                                RepeatDirection="Vertical" runat="server" CellPadding="0" OnItemDataBound="DataListItemDataBound"
                                                CellSpacing="0" Width="100%" GridLines="None" ShowHeader="false">
                                                <ItemTemplate>
                                                    <div style="background-color: #fafafa; width: auto; padding-top: 19px; padding-bottom: 19px;
                                                        overflow: auto;">
                                                        <div style="width: 120px; float: left; text-align: center">
                                                            <asp:Image ID="imgDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px" BorderStyle="Solid"
                                                                ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                            <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("userId")%>' />
                                                            <asp:HiddenField ID="hfDiscuessionID" runat="server" Value='<%# Eval("discussionId")%>' />
                                                        </div>
                                                        <div style="width: 80%; float: left; text-align: left;">
                                                            <div style="width: 100%; height: 26px;">
                                                                <div style="float: left; font-size: 13px;">
                                                                    <asp:Label ID="label5" runat="server" Text='<%# Eval("Name") %>' Font-Bold="True"></asp:Label></div>
                                                                <div style="float: left; padding-left: 10px; font-size: 13px;">
                                                                    <asp:Label ID="label6" runat="server" Text='<%# "Commented " + Eval("days") + " days, " + Eval("hour") + " hours and " + Eval("min") + " minutes ago" %>'></asp:Label></div>
                                                                <div style='<%=IsLogedIn.ToString().Trim() %>; float: right;'>
                                                                    <asp:LinkButton ID="BtnReply" CssClass="button icon chat Tipsy DivHide" ToolTip="Click here to reply this thread"
                                                                        runat="server" Text="Reply" />
                                                                </div>
                                                            </div>
                                                            <div style="width: 85%; padding-right: 12px; padding-top: 10px; font-size: 13px;">
                                                                <asp:Label ID="label7" runat="server" Text='<%# Eval("comments")%>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; background-color: #fafafa; width: 100%;">
                                                        <div style="float: right; background-image: #FAFAFA; width: 650px;">
                                                            <asp:DataList ID="rptrSubDiscussion" RepeatColumns="1" RepeatDirection="Vertical"
                                                                runat="server" CellPadding="0" OnItemDataBound="SubCommentDataListItemDataBound"
                                                                CellSpacing="0" Width="100%" GridLines="None" OnItemCommand="rptrSubDiscussion_ItemCommand"
                                                                ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <div style="background-color: #fafafa; width: auto; padding-left: 80px; padding-top: 19px;
                                                                        padding-bottom: 19px; overflow: hidden;">
                                                                        <div style="float: left; text-align: center">
                                                                            <div style="float: left;">
                                                                                <asp:Image ID="imgSubDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px"
                                                                                    BorderStyle="Solid" ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                                                <asp:HiddenField ID="hfSubCommentUserID" runat="server" Value='<%# Eval("userId")%>' />
                                                                                <asp:HiddenField ID="hfSubDiscuessionID" runat="server" Value='<%# Eval("discussionId")%>' />
                                                                            </div>
                                                                            <div style="float: left; padding-left: 20px; width: 450px;">
                                                                                <div style="height: 26px; text-align: left;">
                                                                                    <asp:Label ID="sublabel5" runat="server" Text='<%# Eval("Name") %>' Font-Bold="true"
                                                                                        Font-Size="13px"></asp:Label>&nbsp;&nbsp;<asp:Label ID="sublabel6" runat="server"
                                                                                            Text='<%# "Commented " + Eval("days") + " days, " + Eval("hour") + " hours and " + Eval("min") + " minutes ago" %>'
                                                                                            Font-Size="13px"></asp:Label></div>
                                                                                <div style="padding-right: 12px; text-align: left; clear: both;">
                                                                                    <asp:Label ID="sublabel7" runat="server" Text='<%# Eval("comments")%>' Font-Size="13px"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div style="width: 535px; float: left; text-align: left;">
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Panel ID="pnlFooter" runat="server" CssClass="hideComment" Style="display: none;">
                                                                        <div id="CommentArea" style="background-color: #fafafa; width: auto; padding-top: 10px;
                                                                            padding-bottom: 10px; overflow: hidden;">
                                                                            <div style="clear: both;">
                                                                                <div style="width: 650px; height: 120px;">
                                                                                    <asp:TextBox ID="txtSubComment" Style="float: right; margin-right: 10px;" title="Add Comments"
                                                                                        onfocus="this.className='TextBox'" Width="565px" CssClass="TextBox Tipsy" Height="110px"
                                                                                        TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div id="ButtonMain" style="clear: both;">
                                                                                <div id="ThisID" style="float: right; padding-right: 10px;">
                                                                                    <asp:LinkButton ID="BtnCancel" Style="margin-right: 0px !important;" CssClass="button big primary danger"
                                                                                        runat="server" Text="Cancel"></asp:LinkButton>
                                                                                </div>
                                                                                <div style="float: right; padding-right: 5px;">
                                                                                    <asp:LinkButton ID="btnSubCommentPost" Text="Submit" Style="margin-right: 0px !important;"
                                                                                        CssClass="button big primary green" CommandName="addComment" CommandArgument='<%# Eval ("pdiscussionId") %>'
                                                                                        runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </FooterTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                        <div style="height: 320px; width: auto;">
                                            <div style="padding-left: 20px; clear: both; padding-top: 10px; padding-bottom: 10px;">
                                                <asp:Button ID="hLinkSignIn" runat="server" Text="Signin to post a comment" CssClass="button big primary">
                                                </asp:Button></div>
                                            <div style="width: 100%; clear: both;">
                                                <div style="width: 100px; float: left; padding-left: 20px;">
                                                    <div style="clear: both;">
                                                        <asp:Label ID="label3" runat="server" Text="Comment" Font-Size="15px" Font-Bold="True"></asp:Label>
                                                    </div>
                                                    <div style="clear: both; padding-top: 10px;">
                                                        <asp:Image ID="imgLoginUser" runat="server" BorderColor="#F99D1C" BorderWidth="2px"
                                                            BorderStyle="Solid" ImageUrl="~/Images/disImg.gif" Width="62px" Height="62px" />
                                                    </div>
                                                </div>
                                                <div style="float: left; padding-left: 20px; padding-top: 20px;">
                                                    <asp:TextBox ID="txtComment" title="Add Comments" onfocus="this.className='TextBox'"
                                                        Width="680px" Height="110px" CssClass="TextBox Tipsy" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    <cc1:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="EmptyFieldvalidate"
                                                        ControlToValidate="txtComment" Display="none" ValidateEmptyText="true" ValidationGroup="vgComments"
                                                        ErrorMessage="" SetFocusOnError="false"></cc1:CustomValidator>
                                                </div>
                                            </div>
                                            <div style="width: 100%; clear: both;">
                                                <div style="clear: both; padding-top: 20px;">
                                                    <div style="float: right; padding-right: 25px;">
                                                        <asp:Button ID="btnPost" runat="server" Text="Submit" CssClass="button big primary"
                                                            ValidationGroup="vgComments" CausesValidation="true" OnClick="btnPost_Click" />
                                                    </div>
                                                </div>
                                                <div style="width: 63px; float: right;">
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="float: left;">
        <div style="clear: both; padding-left: 10px; width: 245px;">
            <div class="shadow" style="clear: both; margin-top: 20px; padding-top: 10px; padding-bottom: 10px;
                width: 100%; background-color: White; overflow: hidden; width: 245px;">
                <div style="color: #2ebcf7; margin-left: 8px; font-weight: bold;">
                    Tastygo Support</div>
                <div style="color: #636363; margin: 5px 0px 0px 8px;">
                    • Post your questions on today‘s deal</div>
                <div style="color: #636363; margin: 5px 5px 0px 10px;">
                    • We monitor discussion boards regularly and we‘ll answer your question as soon
                    as possible</div>
                <div style="float: right; margin-right: 15px; text-align: right;">
                    <div style="color: #2ebcf7; font-weight: bold; padding-top: 5px;">
                        Wana Talk to Someone Live?</div>
                    <div style="float: right; font-weight: bold;">
                        Mon-Fri 9:00AM - 5:30PM PST<br />
                        1-855-295-1771
                        <br />
                        support@tazzling.com
                    </div>
                </div>
            </div>
            <div style="clear: both; width: 100%">
                <div style="clear: both; padding: 11px 0px 0px 0px;">
                    <div class="DetailTheDetailDiv" style="font-size: 13px; font-weight: bold;">
                        <div style="float: left; padding: 10px 0px 0px 15px;">
                            Tastygo Top 10 Q & A
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both; overflow: hidden; width: 245px;">
                <div style="clear: both; overflow: hidden;">
                    <div id="accordion">
                        <h3>
                            <a href="#" style="font-size: 13px;">1. Where is my Tastygo Voucher?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Simply login to your account on the top right hand corner, and click my Tastygo.
                                You‘ll be able to find your voucher under the available tab.
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">2. How do I check my gift voucher?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Login to your account, and click My gift tab in your member area.
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">3. How do I purchase the deal as gift?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Simply hit the Buy, and you‘ll see the purchase as gift option.
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">4. Is Tastygo Safe?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Absolutely! All information entered on Tastygo is securied and protected by our
                                top notch SSL Verisign Certificate and we will never pass those info to anyone.
                                To learn more about our Verisign Certificate click HERE.
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">5. How do I get Tasty Dollars?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Share the deal, if your friend is new and the order is > $20, you‘ll receive $10
                                Tasty Dollars!
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">6. I am trying to book appointment with merchant,
                                but no one respond!</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Contact us immediately. Our team will investigate the issue, figure out if the merchant
                                is still in business. If they close down, we‘ll compensate your purchase.
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">7. The experience was horrible, what can I do?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Let us know right away, provide us pictures, descriptions. We‘ll investigate the
                                issue and compensate you for it. Give us a chance, and we‘ll make things right for
                                you!
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">8. I‘ve ordered a product, but it‘s not here yet,
                                what should I do?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Contact us, and we‘ll track down your order for you!
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">9. How do I manage my subscription? </a>
                        </h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                Login to your account, and click the my subscription tab.
                            </p>
                        </div>
                        <h3>
                            <a href="#" style="font-size: 13px;">10. How do I refer my friend?</a></h3>
                        <div>
                            <p class="howtoReferNormalTxt">
                                You can refer your friend through facebook, twitter, or email! Simply login to your
                                account, and hit the referral tab.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Literal ID="ltCountDown" runat="server"></asp:Literal>
</asp:Content>
