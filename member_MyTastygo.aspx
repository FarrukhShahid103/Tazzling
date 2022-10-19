<%@ Page Title="TastyGo | Member | My TastyGos" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="member_MyTastygo.aspx.cs" Inherits="member_MyTastygo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>
<%@ Register Src="~/UserControls/Templates/Total-Referral.ascx" TagPrefix="RedSgnal"
    TagName="TotalComission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/coda-slider-2.0.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="CSS/gh-buttons.css" type="text/css" media="screen" />
    <script type="text/javascript" src="JS/jquery.coda-slider-2.0.js"></script>
    <link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <link href="CSS/confirm.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="JS/jquery.simplemodal.js" type="text/javascript"></script>
    <script src="JS/confirm.js" type="text/javascript"></script>
    <style type="text/css">
        textarea#txtUserNotes
        {
            width: 350px;
            height: 130px;
            border: 3px solid #cccccc;
            padding: 5px;
            font-family: Tahoma, sans-serif;
        }
    </style>
    <script type="text/javascript">

        function ClosePopup() {

            var Recommedtion = $("#RecommedetionHidden").val();

            var UserID = '<%= UserID %>';
            var BID = $("#BIDHidden").val();

            var DetailID = $("#detailID").val();
            var emptycomments = "";
            var emptyrecomdation = "";
            var Notes = $("#txtUserNotes").val();
            $.ajax({
                type: "POST",
                url: "getStateLocalTime.aspx?FeedbackData=" + emptyrecomdation + "&UserComments=" + emptycomments + "&BID=" + BID + "&UserID=" + UserID + "&DetailID=" + DetailID,
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg == "True") {
                        MessegeArea("Thanks for using this deal and you do not give your feedback", "success");
                        $(".simplemodal-close").click();
                    }
                    else {
                        MessegeArea("Opss erroe", "");
                    }
                }
            });
            $(".simplemodal-close").click();

        }
        function TastyCommentCall(ServerSideValue) {
            var myValue = ServerSideValue.split('_');
            $("#BIDHidden").val(myValue[1]);
            $("#detailID").val(myValue[0]);
            setTimeout(function () {
                $('#confirm').modal({
                    closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
                    position: ["20%", ],
                    overlayId: 'confirm-overlay',
                    containerId: 'confirm-container',
                    onShow: function (dialog) {
                        var modal = this;
                        $('.yes', dialog.data[0]).click(function () {
                            if ($.isFunction(callback)) {
                                callback.apply();
                            }
                            modal.close();
                        });
                    }
                });
                //    $('#ctl00_ContentPlaceHolder1_rdhappy').click();
            }, 2000);
        }


        function SubmitData() {

            var Recommedtion = $("#RecommedetionHidden").val();

            var UserID = '<%= UserID %>';
            var DetailID = $("#detailID").val();
            var BID = $("#BIDHidden").val();



            var Notes = $("#txtUserNotes").val();
            $.ajax({
                type: "POST",
                url: "getStateLocalTime.aspx?FeedbackData=" + Recommedtion + "&UserComments=" + Notes + "&BID=" + BID + "&UserID=" + UserID + "&DetailID=" + DetailID,
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg == "True") {
                        MessegeArea("You are great, Thanks for your feedback", "success");
                        $(".simplemodal-close").click();
                    }
                    else {
                        MessegeArea("Opss, Some error occurred while saving your feedback, Please contact at support@tazzling.com", "");
                    }
                }
            });

        }
    </script>
    <script type="text/javascript">
        function SelectVoucherType() {
            if ($("#ddlVoucherType").val() == "Available") {
                $('#Itemtab1').click();
            }
            else if ($("#ddlVoucherType").val() == "Used") {
                $('#Itemtab2').click();
            }
            else if ($("#ddlVoucherType").val() == "Cancelled") {
                $('#Itemtab3').click();
            }

            else if ($("#ddlVoucherType").val() == "Expired") {
                $('#Itemtab4').click();
            }

            else if ($("#ddlVoucherType").val() == "All") {
                $('#Itemtab5').click();
            }
        }
        function ExpiredVouchers() {
            $('#Itemtab4').click();
            $("#ddlVoucherType").val("Expired");
        }
    </script>
    <script>
        $(window).load(function () {

            $('#Itemtab5').click(function () {
                $("#messages").slideUp("slow");
            });

            $('#Itemtab4').click(function () {
                $("#messages").slideUp("slow");
            });


            $('#Itemtab3').click(function () {
                $("#messages").slideUp("slow");
            });

            $('#Itemtab2').click(function () {
                $("#messages").slideUp("slow");
            });

            $('#Itemtab1').click(function () {
                $("#messages").slideUp("slow");
            });


        });
    </script>
    <asp:Literal ID="ltScript" runat="server"></asp:Literal>
    <div>
        <div class="DetailPage2ndDiv">
            <div style="width: 100%; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                </div>
                <div style="background-color: White; min-height: 450px;">
                    <div style="padding-top: 20px; border-bottom: 1px solid #E6E6E5; overflow: hidden;
                        height: 55px;">
                        <div style="padding-left: 15px;">
                            <div style="float: left; font-size: 24px; font-weight: bold; color: #5e636c; padding-top:10px;">
                                My <span style="color: #ff42e7;">Vouchers</span>
                            </div>
                            <div style="float: left; color: #29B1E6; font-size: 18px; font-weight: bold; margin-right: 10px;
                                padding-left: 60px; padding-top: 13px;">
                                Show :
                            </div>
                            <div style="float: left; padding-top:9px;">
                                <select class="detailDropDown" style="width: 150px;" name="jumpMenu" onchange="javascript:SelectVoucherType();"
                                    id="ddlVoucherType">
                                    <option value="Available">Available</option>
                                    <option value="Used">Used</option>
                                    <option value="Cancelled">Cancelled</option>
                                    <option value="Expired">Expired</option>
                                    <%--<option value="All">All</option>--%>
                                </select>
                            </div>
                            <div style="padding-left: 15px; padding-top: 15px; float: left; color:#5e636c;">
                                Note:You can view your gift vouchers in 'My Gift'
                            </div>
                        </div>
                        <div style="float: right; margin-top: -10px; padding-right: 15px;">
                            <div style="float: left;">
                                <RedSgnal:TotalFunds ID="TotalFunds1" runat="server" />
                            </div>
                            <div style="float: left; display: none;">
                                <RedSgnal:TotalComission ID="TotalComission1" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom: 0px !important;" class="coda-slider-wrapper">
                        <div class="coda-slider preload" id="coda-slider-1">
                            <div class="panelSlider">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-wrapper">
                                            <h2 id="tab1" style="display: none;" class="title">
                                                Available</h2>
                                            <div class="yellowandbold" style="padding-left: 15px; word-spacing: 3px;">
                                                <asp:Label ID="label3" runat="server" Font-Names="Helvetica" Text="Available <span style='color: #ff42e7;'>Vouchers</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <asp:Panel ID="AvailableVouchers" Style="padding-top: 10px;overflow: hidden;" runat="server">
                                                <div style="width: 980px;">                                                    
                                                    <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                        <tr>
                                                            <td class="cellFirst1" style="padding-left: 15px; width: 100px">
                                                                <b>
                                                                    <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 415px; text-align: left;">
                                                                <b>
                                                                    <asp:Label ID="Label2" runat="server" Text="Note"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1">
                                                                <b>
                                                                    <asp:Label ID="Label1" runat="server" Text="Expiry Date"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="padding: 15px;">
                                                        <asp:GridView runat="server" ID="gridview1" DataKeyNames="dOrderID" AllowPaging="true"
                                                            AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" PageSize="5" GridLines="None"
                                                            OnPageIndexChanging="gridview1_PageIndexChanging" Width="100%" ShowHeader="false"
                                                            OnRowDataBound="gridview1_RowDataBound" OnRowCommand="gridview1_Login" OnRowEditing="gridview1_Edit">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 100%; border-bottom:1px solid #ff42e7;">
                                                                            <div style="float: left;">
                                                                                <div style="height: 93px; width: 130px;  text-align: center;
                                                                                    vertical-align: middle;">
                                                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                    <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                        alt="" />
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left; padding-left: 25px; width: 360px;">
                                                                                <div style="padding-left: 5px;" title='<%# Convert.ToString(Eval("title")).ToString().Trim()%>'
                                                                                    class="Tipsy MemberArea_GridHeading">
                                                                                    <%# Eval("title").ToString().Trim().Length > 30 ? Convert.ToString(Eval("title")).ToString().Trim().Substring(0, 27) + "..." : Convert.ToString(Eval("title")).ToString().Trim()%>
                                                                                </div>
                                                                                <div>
                                                                                    <asp:GridView ID="gvSubItem" runat="server" DataKeyNames="detailID" AllowPaging="false"
                                                                                        AutoGenerateColumns="false" OnRowCommand="gvSubItem_Login" ShowHeader="false"
                                                                                        GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div style="padding-top: 5px;">
                                                                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td valign="top">
                                                                                                                    <div class="MemberArea_GridHeading" style="clear: both; padding-right: 5px;">
                                                                                                                        <asp:Label ID="lblDetailID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                                                                                                        <%# getDealCode(Eval("dealOrderCode"),Eval("status"),Eval("displayIt"))%></div>
                                                                                                                    <div style="clear: both; padding-top: 25px;">
                                                                                                                        <asp:UpdatePanel ID="upDownloadFile" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="button-group">
                                                                                                                                    <asp:LinkButton CssClass="button icon print" ID="LinkButton13" runat="server" CommandName="download"
                                                                                                                                        Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' CommandArgument='<%# (Eval("detailID") + "," + Eval("dOrderID")+","+Eval("dealOrderCode"))%>'
                                                                                                                                        Text='Print in PDF' />
                                                                                                                                    <asp:HyperLink CssClass="button icon print" ID="LinkButton2" runat="server" Target="_blank"
                                                                                                                                        Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' NavigateUrl='<%# "~/tastyvoucher.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                        Text="Print Text" />
                                                                                                                                    <asp:HyperLink CssClass="button icon pin" ID="hlTracker" runat="server" Target="_blank"
                                                                                                                                        Visible='<%# (Eval("tracking")==DBNull.Value ? false : Convert.ToBoolean(Eval("tracking"))?  true : false)%>'
                                                                                                                                        NavigateUrl='<%# "~/voucherTrack.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                        Text="Track" />
                                                                                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("markUsed")%>' Visible="false"></asp:Label>
                                                                                                                                    <asp:LinkButton CssClass="button green icon approve" ID="lbMarkAsSold" runat="server"
                                                                                                                                        CommandName="Login" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                                                                                        CommandArgument='<%#Eval("detailID")%>' Text='<%# getDealStatus(Eval("markUsed"))%>' />
                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:PostBackTrigger ControlID="LinkButton13" />
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <RowStyle CssClass="RowStyle rowClass4Height" />
                                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle rowClass4Height" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left;">
                                                                                <div style="clear: both;">
                                                                                    <div style="clear: both;">
                                                                                        <asp:TextBox ID="txtvoucherNote" CssClass="TextBox" Font-Size="14px" runat="server"
                                                                                            TextMode="MultiLine" Text='<%#Eval("customerNote")%>' Height="54px" Width="215px"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="clear: both; padding-top: 5px;">
                                                                                        <asp:LinkButton ID="btnSaveNote" CssClass="button icon edit  green" CommandName="Edit"
                                                                                            Text="Save Note" runat="server"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="MemberArea_GridHeading" style="float: left; padding-left: 90px;">
                                                                                <%# Eval("voucherExpiryDate") != DBNull.Value ? Convert.ToDateTime(Eval("voucherExpiryDate")).ToString("MM-dd-yyyy") : "not available"%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div class="emptydatarow">
                                                                    <asp:Label ID="lblNoData" runat="server" Text="There is no deal codes to be shown."></asp:Label>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <PagerTemplate>
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr runat="server" id="topPager">
                                                                        <td>
                                                                            <div style="padding-top: 35px;">
                                                                                <div>
                                                                                    <div>
                                                                                        <div>
                                                                                            <div class="button-group">
                                                                                                <asp:LinkButton ID="lnkTopPrev" CssClass="button icon arrowleft green" Enabled='<%# displayPrevious %>'
                                                                                                    CommandName="Page" CommandArgument="Prev" runat="server" Text="Previous"></asp:LinkButton>
                                                                                                <asp:Repeater ID="rptrPage" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton Style="min-width: 0px !important;" ID="lnkPage" CssClass='<%# Convert.ToInt32((gridview1.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                                            OnClick="lnkPage_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                                                                            CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                                <asp:LinkButton Style="width: 35px;" ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>'
                                                                                                    CommandName="Page" CommandArgument="Next" CssClass="button icon arrowright green"
                                                                                                    Text="Next"></asp:LinkButton>
                                                                                            </div>
                                                                                            <div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="backtotop">
                                                                    <a href="#">
                                                                        <asp:Label ID="lblbackToTop" runat="server" Text="Back To Top"></asp:Label><img src="images/totop.gif" alt="" /></a>
                                                                </div>
                                                            </PagerTemplate>
                                                            <PagerSettings PageButtonCount="10" Position="Bottom" />
                                                            <PagerStyle HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlAvailableVoucherEmpty" runat="server">
                                                <div style="text-align: center;">
                                                    <span class="MemberArea_PageHeading">Looking to Taste new things?</span>
                                                    <p>
                                                        You have to buy Tasty Deals before you can use them. Start browsing and save</p>
                                                    <p class="buttons">
                                                        <a class="AllDealsButton" href="Default.aspx">Browse All Today’s Deals</a></p>
                                                    <span class="MemberArea_PageHeading">Looking for a specific deal?</span>
                                                    <div style="text-align: center;">
                                                        <p>
                                                            Make sure you’re logged into the right account: <strong>
                                                                <%=UserEmail %></strong>.</p>
                                                        <p style="clear: both;">
                                                            <asp:LinkButton CssClass="OrangeLink" ID="lnkBtnLogOut" OnClientClick="fbLogout();"
                                                                runat="server" Text="Sign in to another account" OnClick="lnkBtnLogOut_Click"></asp:LinkButton>
                                                        </p>
                                                        <p style="clear: both; display: none;">
                                                            You can view all of your past orders by selecting <strong>Show All</strong> in the
                                                            drop-down.</p>
                                                        <p style="clear: both;">
                                                            Still can’t find what you’re looking for? <a class="OrangeLink" href="contact-us.aspx">
                                                                Contact us</a> and we’ll help you out.</p>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div id="pnlAvailableVoucherEmpty2" runat="server" style="margin-top: 20px;">
                                                <div style="margin: 10px; padding: 10px; background-color: #fffdbb; overflow: hidden;
                                                    text-align: left;">
                                                    <h4>
                                                        Did a deal expire before you were able to redeem it?</h4>
                                                    <p>
                                                        Even after the promotional value expires, the voucher may still worth what you paid
                                                        for it! Please contact the vendor directly, and you may still apply the amount for
                                                        what you paid for. Some restrictions may apply.
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panelSlider">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-wrapper">
                                            <h2 id="Tab2" style="display: none;" class="title">
                                                Used
                                            </h2>
                                            <div class="yellowandbold" style="padding-left: 15px; word-spacing: 3px;">
                                                <asp:Label ID="label14" runat="server" Font-Names="Helvetica" Text="Used <span style='color: #ff42e7;'>Vouchers</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <div id="UsedVouchers" style="padding-top: 10px; width: 980px;">
                                                <asp:Panel ID="pnlgvUsed" runat="server">                                                   
                                                    <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                        <tr>
                                                            <td class="cellFirst1" style="padding-left: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label6" runat="server" Text="Name"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 410px;">
                                                                <b>
                                                                    <asp:Label ID="Label7" runat="server" Text="Note"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 40px;">
                                                                <b>
                                                                    <asp:Label ID="Label8" runat="server" Text="Expiry Date"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="padding: 15px;">
                                                        <asp:GridView runat="server" ID="gvUsed" DataKeyNames="dOrderID" AllowPaging="true"
                                                            AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" PageSize="5" GridLines="None"
                                                            OnPageIndexChanging="gvUsed_PageIndexChanging" Width="100%" ShowHeader="false"
                                                            OnRowDataBound="gvUsed_RowDataBound" OnRowCommand="gvUsed_Login" OnRowEditing="gvUsed_Edit">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 100%; border-bottom:1px solid #ff42e7;">
                                                                            <div style="float: left;">
                                                                                <div style="height: 93px; width: 130px;  text-align: center;
                                                                                    vertical-align: middle;">
                                                                                    <asp:Label ID="lblUsedID" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                    <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                        alt="" />
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left; padding-left: 25px; width: 310px;">
                                                                                <div style="padding-left: 5px;" title='<%# Convert.ToString(Eval("title")).ToString().Trim()%>'
                                                                                    class="Tipsy MemberArea_GridHeading">
                                                                                    <%# Eval("title").ToString().Trim().Length > 30 ? Convert.ToString(Eval("title")).ToString().Trim().Substring(0, 27) + "..." : Convert.ToString(Eval("title")).ToString().Trim()%>
                                                                                </div>
                                                                                <div>
                                                                                    <asp:GridView ID="gvSubUsed" runat="server" DataKeyNames="detailID" AllowPaging="false"
                                                                                        AutoGenerateColumns="false" OnRowCommand="gvSubUsed_Login" ShowHeader="false"
                                                                                        GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div style="padding-top: 5px;">
                                                                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td valign="top">
                                                                                                                    <div class="MemberArea_GridHeading" style="clear: both; padding-right: 5px;">
                                                                                                                        <asp:Label ID="lblDetailID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                                                                                                        <%# getDealCode(Eval("dealOrderCode"),Eval("status"),Eval("displayIt"))%></div>
                                                                                                                    <div style="clear: both; padding-top: 25px;">
                                                                                                                        <asp:UpdatePanel ID="upDownloadFile" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="button-group">
                                                                                                                                    <asp:LinkButton CssClass="button icon print" ID="LinkButton133" runat="server" CommandName="download"
                                                                                                                                        Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' CommandArgument='<%# (Eval("detailID") + "," + Eval("dOrderID")+","+Eval("dealOrderCode"))%>'
                                                                                                                                        Text='Print in PDF' />
                                                                                                                                    <asp:HyperLink CssClass="button icon print" ID="LinkButton22" runat="server" Target="_blank"
                                                                                                                                        Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' NavigateUrl='<%# "~/tastyvoucher.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                        Text="Print Text" />
                                                                                                                                    <asp:HyperLink CssClass="button icon pin" ID="hlUsedTracker" runat="server" Target="_blank"
                                                                                                                                        Visible='<%# (Eval("tracking")==DBNull.Value ? false : Convert.ToBoolean(Eval("tracking"))?  true : false)%>'
                                                                                                                                        NavigateUrl='<%# "~/voucherTrack.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                        Text="Track" />
                                                                                                                                    <asp:Label ID="lblStatus1" runat="server" Text='<%#Eval("markUsed")%>' Visible="false"></asp:Label>
                                                                                                                                    <asp:LinkButton CssClass="button   danger icon loop" ID="lbMarkAsSold1" runat="server"
                                                                                                                                        CommandName="Login" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                                                                                        CommandArgument='<%#Eval("detailID")%>' Text='<%# getDealStatus(Eval("markUsed"))%>' />
                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:PostBackTrigger ControlID="LinkButton133" />
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <RowStyle CssClass="RowStyle rowClass4Height" />
                                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle rowClass4Height" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left; padding-left: 25px;">
                                                                                <div style="clear: both;">
                                                                                    <div style="clear: both;">
                                                                                        <asp:TextBox ID="txtvoucherNoteUsed" CssClass="TextBox" Font-Size="14px" runat="server"
                                                                                            TextMode="MultiLine" Text='<%#Eval("customerNote")%>' Height="54px" Width="225px"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="clear: both; padding-top: 5px;">
                                                                                        <asp:LinkButton ID="btnSaveNote" CssClass="button icon edit  green" CommandName="Edit"
                                                                                            Text="Save Note" runat="server"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="MemberArea_GridHeading" style="float: left; padding-left: 120px;">
                                                                                <%# Eval("voucherExpiryDate") != DBNull.Value ? Convert.ToDateTime(Eval("voucherExpiryDate")).ToString("MM-dd-yyyy") : "not available"%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div class="emptydatarow">
                                                                    <asp:Label ID="lblNoData" runat="server" Text="There is no deal codes to be shown."></asp:Label>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <PagerTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%" class="search_result">
                                                                    <tr runat="server" id="topPager">
                                                                        <td>
                                                                            <div>
                                                                                <div>
                                                                                    <div style="padding-top: 35px;">
                                                                                        <div class="button-group">
                                                                                            <asp:LinkButton ID="lnkTopUsedPrev" CssClass="button icon arrowleft   green" Enabled='<%# displayPrevious %>'
                                                                                                CommandName="Page" CommandArgument="Prev" runat="server" Text="Previous"></asp:LinkButton>
                                                                                            <asp:Repeater ID="rptrPage" runat="server">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkPageUsed" OnClick="lnkPageUsed_Click" Font-Underline="false"
                                                                                                        Style="min-width: 0px !important;" CssClass='<%# Convert.ToInt32((gvUsed.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                                        CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>' CommandArgument='<%# Eval("pageNo") %>'
                                                                                                        runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                            <asp:LinkButton ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>' CommandName="Page"
                                                                                                CommandArgument="Next" CssClass="button icon arrowright   green" Text="Next"></asp:LinkButton>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="backtotop">
                                                                    <a href="#">
                                                                        <asp:Label ID="lblbackToTop" runat="server" Text="Back To Top"></asp:Label><img src="images/totop.gif" /></a>
                                                                </div>
                                                            </PagerTemplate>
                                                            <PagerSettings PageButtonCount="10" Position="Bottom" />
                                                            <PagerStyle HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="gvUsedEmpty" runat="server">
                                                    <div style="text-align: center;">
                                                        <span class="MemberArea_PageHeading">Looking to Taste new things?</span>
                                                        <p>
                                                            You have to buy Tasty Deals before you can use them. Start browsing and save</p>
                                                        <p class="buttons">
                                                            <a class="AllDealsButton" href="Default.aspx">Browse All Today’s Deals</a></p>
                                                        <span class="MemberArea_PageHeading">Looking for a specific deal?</span>
                                                        <div style="text-align: center;">
                                                            <p>
                                                                Make sure you’re logged into the right account: <strong>
                                                                    <%=UserEmail %></strong>.</p>
                                                            <p style="clear: both;">
                                                                <asp:LinkButton CssClass="OrangeLink" ID="LinkButton3" OnClientClick="fbLogout();"
                                                                    runat="server" Text="Sign in to another account" OnClick="lnkBtnLogOut_Click"></asp:LinkButton>
                                                            </p>
                                                            <p style="clear: both; display: none;">
                                                                You can view all of your past orders by selecting <strong>Show All</strong> in the
                                                                drop-down.</p>
                                                            <p style="clear: both;">
                                                                Still can’t find what you’re looking for? <a class="OrangeLink" href="contact-us.aspx">
                                                                    Contact us</a> and we’ll help you out.</p>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panelSlider">
                                <asp:UpdatePanel ID="uppnltab3" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-wrapper">
                                            <h2 id="tab3" style="display: none;" class="title">
                                                Cancelled</h2>
                                            <div class="yellowandbold" style="padding-left: 15px; word-spacing: 3px;">
                                                <asp:Label ID="label15" runat="server" Font-Names="Helvetica" Text="Cancelled <span style='color: #ff42e7;'>Vouchers</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <div runat="server" id="pnlgvcancelled" style="padding-top: 10px; width: 980px;">                                               
                                                <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                    <tr>
                                                        <td class="cellFirst1" style="padding-left: 15px;">
                                                            <b>
                                                                <asp:Label ID="Label5" runat="server" Text="Name"></asp:Label></b>
                                                        </td>
                                                        <td class="cellSecond1" style="padding-left: 330px;">
                                                            <b>
                                                                <asp:Label ID="Label9" runat="server" Text="Note"></asp:Label></b>
                                                        </td>
                                                        <td class="cellSecond1" style="padding-left: 40px;">
                                                            <b>
                                                                <asp:Label ID="Label10" runat="server" Text="Expiry Date"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="padding: 15px;">
                                                <asp:GridView runat="server" ID="gvcancelled" DataKeyNames="dOrderID" AllowPaging="true"
                                                    AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" PageSize="5" GridLines="None"
                                                    OnPageIndexChanging="gvcancelled_PageIndexChanging" Width="100%" ShowHeader="false"
                                                    OnRowDataBound="gvcancelled_RowDataBound" OnRowCommand="gvcancelled_Login">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div style="padding-bottom: 10px; padding-top: 10px; width: 100%; overflow: hidden; border-bottom:1px solid #ff42e7;">
                                                                    <div style="float: left;">
                                                                        <div style="float: left;">
                                                                            <div style="height: 93px; width: 130px;  text-align: center;
                                                                                vertical-align: middle;">
                                                                                <asp:Label ID="lblUsedID" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                    alt="" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 25px; width: 260px;">
                                                                        <div style="padding-left: 5px;" title='<%# Convert.ToString(Eval("title")).ToString().Trim()%>'
                                                                            class="Tipsy MemberArea_GridHeading">
                                                                            <%# Eval("title").ToString().Trim().Length > 30 ? Convert.ToString(Eval("title")).ToString().Trim().Substring(0, 27) + "..." : Convert.ToString(Eval("title")).ToString().Trim()%>
                                                                        </div>
                                                                        <div>
                                                                            <asp:GridView ID="gvSubcancelled" runat="server" DataKeyNames="detailID" AllowPaging="false"
                                                                                AutoGenerateColumns="false" OnRowCommand="gvSubcancelled_Login" ShowHeader="false"
                                                                                GridLines="None">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <div style="padding-top: 5px;">
                                                                                                <div class="MemberArea_GridHeading">
                                                                                                    <%# getDealCode(Eval("dealOrderCode"),Eval("status"),Eval("displayIt"))%>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <RowStyle CssClass="RowStyle rowClass4Height" />
                                                                                <AlternatingRowStyle CssClass="AlternatingRowStyle rowClass4Height" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 5px;" class="MemberArea_GridHeading">
                                                                        <%# Eval("status")%>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 295px;" class="MemberArea_GridHeading">
                                                                        <%# GetDateString(Eval("voucherExpiryDate"))%>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div class="emptydatarow">
                                                            <asp:Label ID="lblNoData" runat="server" Text="There is no deal codes to be shown."></asp:Label>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <PagerTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="100%" class="search_result">
                                                            <tr runat="server" id="topPager">
                                                                <td colspan="4">
                                                                    <div style="padding-top: 35px;">
                                                                        <div>
                                                                            <div>
                                                                                <div>
                                                                                    <div class="button-group">
                                                                                        <asp:LinkButton ID="lnkTopPrev" CssClass="button icon arrowleft   green" Enabled='<%# displayPrevious %>'
                                                                                            CommandName="Page" CommandArgument="Prev" runat="server" Text="Previous"></asp:LinkButton>
                                                                                        <asp:Repeater ID="rptrPage" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton Style="min-width: 0px !important;" ID="lnkCancellPage" CssClass='<%# Convert.ToInt32((gvcancelled.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                                    OnClick="lnkCancellPage_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                                                                    CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                        <asp:LinkButton Style="width: 35px;" ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>'
                                                                                            CommandName="Page" CommandArgument="Next" CssClass="button icon arrowright   green"
                                                                                            Text="Next"></asp:LinkButton>
                                                                                    </div>
                                                                                    <div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="backtotop">
                                                            <a href="#">
                                                                <asp:Label ID="lblbackToTop" runat="server" Text="Back To Top"></asp:Label><img src="images/totop.gif" /></a>
                                                        </div>
                                                    </PagerTemplate>
                                                    <PagerSettings PageButtonCount="10" Position="Bottom" />
                                                    <PagerStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlgvCancelledEmpty" runat="server">
                                                <div style="text-align: center;">
                                                    <span class="MemberArea_PageHeading">Looking to Taste new things?</span>
                                                    <p>
                                                        You have to buy Tasty Deals before you can use them. Start browsing and save</p>
                                                    <p class="buttons">
                                                        <a class="AllDealsButton" href="Default.aspx">Browse All Today’s Deals</a></p>
                                                    <span class="MemberArea_PageHeading">Looking for a specific deal?</span>
                                                    <div style="text-align: center;">
                                                        <p>
                                                            Make sure you’re logged into the right account: <strong>
                                                                <%=UserEmail %></strong>.</p>
                                                        <p style="clear: both;">
                                                            <asp:LinkButton CssClass="OrangeLink" ID="LinkButton4" OnClientClick="fbLogout();"
                                                                runat="server" Text="Sign in to another account" OnClick="lnkBtnLogOut_Click"></asp:LinkButton>
                                                        </p>
                                                        <p style="clear: both; display: none;">
                                                            You can view all of your past orders by selecting <strong>Show All</strong> in the
                                                            drop-down.</p>
                                                        <p style="clear: both;">
                                                            Still can’t find what you’re looking for? <a class="OrangeLink" href="contact-us.aspx">
                                                                Contact us</a> and we’ll help you out.</p>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div style="height: 20px;">
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panelSlider">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-wrapper">
                                            <h2 id="tab4" style="display: none;" class="title">
                                                Expired</h2>
                                            <div class="yellowandbold" style="padding-left: 15px; word-spacing: 3px;">
                                                <asp:Label ID="label11" runat="server" Font-Names="Helvetica" Text="Expired <span style='color: #ff42e7;'>Vouchers</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <div id="pnlgvExpired" runat="server" style="padding-top: 10px; width: 980px;">
                                                <div style="width: 980px;">                                                    
                                                    <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                        <tr>
                                                            <td class="cellFirst1" style="padding-left: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label12" runat="server" Text="Name"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 430px;">
                                                                <b>
                                                                    <asp:Label ID="Label13" runat="server" Text="Note"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 40px;">
                                                                <b>
                                                                    <asp:Label ID="Label16" runat="server" Text="Expiry Date"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="padding: 15px;">
                                                        <asp:GridView runat="server" ID="gvExpired" DataKeyNames="dOrderID" AllowPaging="true"
                                                            AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" PageSize="5" GridLines="None"
                                                            OnPageIndexChanging="gvExpired_PageIndexChanging" Width="100%" ShowHeader="false"
                                                            OnRowDataBound="gvExpired_RowDataBound" OnRowCommand="gvExpired_Login" OnRowEditing="gvExpired_Edit">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 100%; border-bottom:1px solid #ff42e7;">
                                                                            <div style="float: left;">
                                                                                <div style="height: 93px; width: 130px;  text-align: center;
                                                                                    vertical-align: middle;">
                                                                                    <asp:Label ID="lblExpiredID" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                    <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                        alt="" />
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left; padding-left: 25px; width: 350px;">
                                                                                <div style="padding-left: 5px;" title='<%# Convert.ToString(Eval("title")).ToString().Trim()%>'
                                                                                    class="Tipsy MemberArea_GridHeading">
                                                                                    <%# Eval("title").ToString().Trim().Length > 30 ? Convert.ToString(Eval("title")).ToString().Trim().Substring(0, 27) + "..." : Convert.ToString(Eval("title")).ToString().Trim()%>
                                                                                </div>
                                                                                <div>
                                                                                    <asp:GridView ID="gvSubExpired" runat="server" DataKeyNames="detailID" AllowPaging="false"
                                                                                        AutoGenerateColumns="false" OnRowCommand="gvSubExpired_Login" ShowHeader="false"
                                                                                        GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div style="padding-top: 5px;">
                                                                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <div class="MemberArea_GridHeading" style="clear: both; padding-right: 5px;">
                                                                                                                        <asp:Label ID="lblDetailExpiredID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                                                                                                        <%# getDealCode(Eval("dealOrderCode"),Eval("status"),Eval("displayIt"))%></div>
                                                                                                                    <div style="clear: both; padding-top: 25px;">
                                                                                                                        <div style="float: left; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                                                                                                            font-size: 13px;">
                                                                                                                            <asp:UpdatePanel ID="upDownloadFile" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <div class="button-group">
                                                                                                                                        <asp:LinkButton CssClass="button icon print" ID="LinkButton199" runat="server" CommandName="download"
                                                                                                                                            Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' CommandArgument='<%# (Eval("detailID") + "," + Eval("dOrderID")+","+Eval("dealOrderCode"))%>'
                                                                                                                                            Text='Print in PDF' />
                                                                                                                                        <asp:HyperLink CssClass="button icon print" ID="LinkButton212" runat="server" Target="_blank"
                                                                                                                                            Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' NavigateUrl='<%# "~/tastyvoucher.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                            Text="Print Text" />
                                                                                                                                        <asp:HyperLink CssClass="button icon pin" ID="hlExpiredTracker" runat="server" Target="_blank"
                                                                                                                                            Visible='<%# (Eval("tracking")==DBNull.Value ? false : Convert.ToBoolean(Eval("tracking"))?  true : false)%>'
                                                                                                                                            NavigateUrl='<%# "~/voucherTrack.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                            Text="Track" />
                                                                                                                                        <asp:Label ID="lblExpiredStatus" runat="server" Text='<%#Eval("markUsed")%>' Visible="false"></asp:Label>
                                                                                                                                        <asp:LinkButton CssClass="button  " ID="lbMarkAsSoldExpired" runat="server" CommandName="Login"
                                                                                                                                            Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' CommandArgument='<%#Eval("detailID")%>'
                                                                                                                                            Text='<%# getDealStatus(Eval("markUsed"))%>' />
                                                                                                                                    </div>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:PostBackTrigger ControlID="LinkButton199" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <RowStyle CssClass="RowStyle rowClass4Height" />
                                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle rowClass4Height" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left;">
                                                                                <div style="clear: both;">
                                                                                    <div style="clear: both;">
                                                                                        <asp:TextBox ID="txtvoucherNoteExpired" CssClass="TextBox" Font-Size="14px" runat="server"
                                                                                            TextMode="MultiLine" Text='<%#Eval("customerNote")%>' Height="54px" Width="225px"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="clear: both; padding-top: 5px;">
                                                                                        <asp:LinkButton ID="btnSaveNoteExpired" CssClass="button icon edit  green" Text="Save Note"
                                                                                            ToolTip="Save Note" CommandName="Edit" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="MemberArea_GridHeading" style="float: left; padding-left: 105px;">
                                                                                <%# Eval("voucherExpiryDate") != DBNull.Value ? Convert.ToDateTime(Eval("voucherExpiryDate")).ToString("MM-dd-yyyy") : "not available"%>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <div class="emptydatarow">
                                                                    <asp:Label ID="lblNoData" runat="server" Text="There is no deal codes to be shown."></asp:Label>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <PagerTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%" class="search_result">
                                                                    <tr runat="server" id="topPager">
                                                                        <td>
                                                                            <div style="padding-top: 35px;">
                                                                                <div>
                                                                                    <div>
                                                                                        <div>
                                                                                            <div class="button-group">
                                                                                                <asp:LinkButton ID="lnkTopExpiredPrev" CssClass="button icon arrowleft   green" Enabled='<%# displayPrevious %>'
                                                                                                    CommandName="Page" CommandArgument="Prev" runat="server" Text="Previous"></asp:LinkButton>
                                                                                                <asp:Repeater ID="rptrPage" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkPageExpired" CssClass='<%# Convert.ToInt32((gvExpired.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                                            Style="min-width: 0px !important;" OnClick="lnkPageExpired_Click" Font-Underline="false"
                                                                                                            CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>' CommandArgument='<%# Eval("pageNo") %>'
                                                                                                            runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                                <asp:LinkButton Style="width: 50px;" ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>'
                                                                                                    CommandName="Page" CommandArgument="Next" CssClass="button icon arrowright   green"
                                                                                                    Text="Next"></asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="backtotop">
                                                                    <a href="#">
                                                                        <asp:Label ID="lblbackToTop" runat="server" Text="Back To Top"></asp:Label><img src="images/totop.gif" /></a>
                                                                </div>
                                                            </PagerTemplate>
                                                            <PagerSettings PageButtonCount="10" Position="Bottom" />
                                                            <PagerStyle HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="gvExpiredEmpty" runat="server">
                                                <div style="text-align: center;">
                                                    <span class="MemberArea_PageHeading">Looking to Taste new things?</span>
                                                    <p>
                                                        You have to buy Tasty Deals before you can use them. Start browsing and save</p>
                                                    <p class="buttons">
                                                        <a class="AllDealsButton" href="Default.aspx">Browse All Today’s Deals</a></p>
                                                    <h4>
                                                        Looking for a specific deal?</h4>
                                                    <div style="text-align: center;">
                                                        <p>
                                                            Make sure you’re logged into the right account: <strong>
                                                                <%=UserEmail %></strong>.</p>
                                                        <p style="clear: both;">
                                                            <asp:LinkButton CssClass="OrangeLink" ID="LinkButton5" OnClientClick="fbLogout();"
                                                                runat="server" Text="Sign in to another account" OnClick="lnkBtnLogOut_Click"></asp:LinkButton>
                                                        </p>
                                                        <p style="clear: both; display: none;">
                                                            You can view all of your past orders by selecting <strong>Show All</strong> in the
                                                            drop-down.</p>
                                                        <p style="clear: both;">
                                                            Still can’t find what you’re looking for? <a class="OrangeLink" href="contact-us.aspx">
                                                                Contact us</a> and we’ll help you out.</p>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="panelSlider">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="panel-wrapper">
                                            <h2 id="tab5" style="display: none;" class="title">
                                                All</h2>
                                            <div class="yellowandbold" style="word-spacing: 3px;">
                                                <asp:Label ID="label17" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="All <span style='color: #ff42e7;'>Vouchers</span>"
                                                    Font-Size="29px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <!-- .coda-slider -->
                    </div>
                    <asp:Literal ID="TabScript" runat="server"></asp:Literal>
                    <asp:Literal ID="ltJavascript" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <style type="text/css">
        #TextBox1
        {
            width: 340px;
            height: 120px;
            border: 3px solid #cccccc;
            padding: 5px;
            font-family: Tahoma, sans-serif;
            margin-left: 20px;
        }
        .btncancel
        {
            text-decoration: none;
            font-size: 15px;
        }
        .btncancel:hover
        {
            text-decoration: underline;
            font-size: 16px;
            color: #FE9DDB;
        }
    </style>
    <script type="text/javascript">
        function ValidateUserComments() {
            var IsValid = true;


            if ($("#RecommedetionHidden").val() != '') {
                if ($("#txtUserNotes").val() != '') {
                    SubmitData();
                }
                else {
                    MessegeArea('Please enter comments before submit your feedback.', 'error');
                    IsValid = false;
                }
            }
            else {
                MessegeArea('Please select one option  from Yes/No for post your comment', 'erro');
                IsValid = false;

            }
        } 
      
    
    
    
    
    
    </script>
    <div id="Div1" style="position: fixed; display: none; width: 400 !important; z-index: 1002;
        left: 25% !important; top: 20%; overflow: hidden; height: 500px; background: white;
        padding: 10px;">
        <div style="outline: 0px none; border: 5px solid red; overflow: hidden; width: 400px !important">
            <div id="confirm" style="display: block; width: 400px !important">
                <div style="color: #1fa7f3; text-align: center; margin-top: 20px; margin-bottom: 10px;
                    font-family: Helvetica; font-size: 18px;">
                    Your feedback is important for us!
                </div>
                <div style="color: Black; text-align: center; font-size: 12px; font-family: Sans-Serif;">
                    would you want us to feature this business again?
                </div>
                <center>
                    <div style="width: 120px; height: 30px; margin-top: 10px;">
                        <div style="float: left;">
                            <input type="radio" id="rdhappy" group="group1" name="rdrecomdation" runat="server"
                                value="Yes" onclick="document.getElementById('RecommedetionHidden').value=this.value" />Yes</div>
                        <div style="float: right;">
                            <input id="rdsad" runat="server" group="group1" name="rdrecomdation" type="radio"
                                value="No" onclick="document.getElementById('RecommedetionHidden').value=this.value" />No
                        </div>
                    </div>
                </center>
                <div style="text-align: center; margin-bottom: 10px;">
                    <asp:Label ID="Label18" runat="server" Text="Label">Your feedback for this deal</asp:Label>
                </div>
                <div style="margin: 13px;">
                    <textarea name="styled-textarea" class="TextBox" id="txtUserNotes"></textarea>
                </div>
                <div style="float: left; margin-bottom: 10px; margin-left: 15px;">
                    <div style="float: left; margin-right: 25px;">
                        <a href="javascript:void(0);" class="big primary button" onclick="return ValidateUserComments();"
                            id="btnSubmitFeedback">Submit</a>
                    </div>
                    <div style="float: right; margin-top: 5px; margin-right: 25px;">
                        <a class="btncancel" href="javascript:void(0);" id="BtnClosePopup" onclick="javascript:ClosePopup()">
                            i do not wish to leave feedbacks</a>
                    </div>
                </div>
                <input type="hidden" id="BIDHidden" />
                <input type="hidden" id="detailID" />
                <input type="hidden" id="RecommedetionHidden" />
                <input type="hidden" id="USerIDHidden" />
            </div>
        </div>
    </div>
</asp:Content>
