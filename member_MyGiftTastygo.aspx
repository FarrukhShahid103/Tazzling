<%@ Page Title="TastyGo | Member | My Gift TastyGos" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="member_MyGiftTastygo.aspx.cs" Inherits="member_MyGiftTastygo" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/coda-slider-2.0.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="CSS/gh-buttons.css" type="text/css" media="screen" />
    <script type="text/javascript" src="JS/jquery.coda-slider-2.0.js"></script>
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
    <script type="text/javascript">

        $(document).ready(function () {
            $("#divEditCustomer").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 20,
                height: 'auto',
                width: 'auto',
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divEditCustomerDlgContainer");
                }
            });
        });


        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divEditCustomer").dialog('close');
        }

        function ShowPDF() {
            //Could cause an infinite loop because of "on close handling"
            // alert("start");
            //alert(document.getElementById('ctl00_ContentPlaceHolder1_hdDealCode').value);
            window.open('Images/ClientData/' + document.getElementById('ctl00_ContentPlaceHolder1_hdDealCode').value, target = 'new');
            //alert("End");
            //$("#divEditCustomer").dialog('close');
        }

        function openDialog(title, detailID, dealCode) {
            //var pos = $("#" + linkID).position();
            var top = 250;
            var left = 250;

            $('#ctl00_ContentPlaceHolder1_hdId').val(detailID.toString());
            $('#ctl00_ContentPlaceHolder1_hdDealCode').val(dealCode.toString());
            $("#ctl00_ContentPlaceHolder1_txtEmil").val("");
            $("#ctl00_ContentPlaceHolder1_txtMesage").val("");
            $("#ctl00_ContentPlaceHolder1_txtFirstName").val("");
            $("#ctl00_ContentPlaceHolder1_txtLastName").val("");
            $("#ctl00_ContentPlaceHolder1_txtEFirstName").val("");
            $("#ctl00_ContentPlaceHolder1_txtELastName").val("");
            if (title == 'Send Email') {
                //document.getElementById('divPopup').style.height = '272px';
                document.getElementById('divEmail').style.display = 'block';
                document.getElementById('divUserInfo').style.display = 'none';

            }
            else {
                document.getElementById('divPopup').style.height = '132px';
                document.getElementById('divEmail').style.display = 'none';
                document.getElementById('divUserInfo').style.display = 'block';

            }
            $("#divEditCustomer").dialog("option", "title", title);
            //$("#divEditCustomer").dialog("option", "position", [left, top]);
            $("#divEditCustomer").dialog('open');
        }

        function openDialogAndBlock(detailID, dealCode) {
            openDialog("Send Email", detailID, dealCode);
        }

        function openDialogForPrint(detailID, dealCode) {
            openDialog("Your Friend's Name", detailID, dealCode);

        }

        function unblockDialog() {
            $("#divEditCustomer").unblock();
        }

        function onTest() {
            $("#divEditCustomer").block({
                message: '<h1>Processing</h1>',
                css: { border: '3px solid #a00' },
                overlayCSS: { backgroundColor: '#88d3fe', opacity: 1 }
            });
        }
    </script>
    <div>
        <div>
            <div>
                <div id="divEditCustomerDlgContainer">
                    <div id="divEditCustomer" style="display: none">
                        <asp:UpdatePanel ID="upnlEditCustomer" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phrEditCustomer" runat="server">
                                    <asp:Panel ID="pnlId" runat="server">
                                        <div style="text-align: center; padding-bottom: 5px;">
                                            <div id="divPopup" style="width: 300px; overflow-y: auto; overflow-x: hidden; padding-top: 15px;">
                                                <div id="divEmail">
                                                    <div style="padding-left: 10px; padding-bottom: 10px;">
                                                        <div style="float: left;">
                                                            <div style="clear: both; text-align: left;">
                                                                <asp:Label ID="Label7" runat="server" Font-Size="13px" Text="First Name"></asp:Label>
                                                            </div>
                                                            <div style="clear: both">
                                                                <asp:TextBox ID="txtEFirstName" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                                                    ControlToValidate="txtEFirstName" ErrorMessage="*" ValidationGroup="btnSend"
                                                                    Display="Dynamic"></cc1:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; padding-left: 10px; padding-top: 10px;">
                                                        <div style="float: left;">
                                                            <div style="clear: both; text-align: left;">
                                                                <asp:Label ID="Label8" runat="server" Font-Size="13px" Text="Last Name"></asp:Label>
                                                            </div>
                                                            <div style="clear: both; text-align: left;">
                                                                <asp:TextBox ID="txtELastName" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                                                                    ControlToValidate="txtELastName" ErrorMessage="*" ValidationGroup="btnSend" Display="Dynamic"></cc1:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; padding-left: 10px; padding-bottom: 10px; padding-top: 10px;">
                                                        <div style="float: left;">
                                                            <div style="clear: both; text-align: left;">
                                                                <asp:Label ID="lblEmail" runat="server" Font-Size="13px" Text="Email"></asp:Label>
                                                                <asp:HiddenField ID="hdId" runat="server" />
                                                                <asp:HiddenField ID="hdDealCode" runat="server" />
                                                            </div>
                                                            <div style="clear: both; text-align: left;">
                                                                <asp:TextBox ID="txtEmil" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                                                    ControlToValidate="txtEmil" ErrorMessage="*" ValidationGroup="btnSend" Display="Dynamic"></cc1:RequiredFieldValidator>
                                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                                                    runat="server" ControlToValidate="txtEmil" ErrorMessage="*" ValidationGroup="btnSend"
                                                                    Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; padding-left: 10px; padding-top: 10px;">
                                                        <div style="float: left;">
                                                            <div style="clear: both; text-align: left;">
                                                                <asp:Label ID="lblEmailMessage" runat="server" Font-Size="13px" Text="Message"></asp:Label>
                                                            </div>
                                                            <div style="clear: both; text-align: left;">
                                                                <asp:TextBox ID="txtMesage" runat="server" TextMode="MultiLine" Height="60px" CssClass="TextBoxDeal"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; padding-left: 92px; padding-top: 10px;">
                                                        <div style="float: left; padding-right: 10px;">
                                                            <asp:Button ID="btnSendEmail" runat="server" OnClick="btnSendEmail_Click" ValidationGroup="btnSend"
                                                                CssClass="button big primary" Text="Send Email" />
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:Button ID="btnCancel" OnClientClick="closeDialog();return false;" runat="server"
                                                                CssClass="button big primary" Text="Cancel"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divUserInfo">
                                                    <div style="padding-left: 10px; padding-bottom: 10px;">
                                                        <div style="float: left; width: 110px; padding-right: 5px; padding-top: 4px; text-align: right;">
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="16px" Text="First Name"></asp:Label>
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                                            <%--<cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                                ControlToValidate="txtFirstName" ErrorMessage="*" ValidationGroup="btnPrint"
                                                                Display="Dynamic"></cc1:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; padding-left: 10px; padding-top: 10px;">
                                                        <div style="float: left; width: 110px; padding-right: 5px; padding-top: 4px; text-align: right;">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="16px" Text="Last Name"></asp:Label></div>
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                                            <%--<cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                                                ControlToValidate="txtLastName" ErrorMessage="*" ValidationGroup="btnPrint" Display="Dynamic"></cc1:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; padding-left: 222px; padding-top: 10px;">
                                                        <div style="float: left; padding-right: 10px;">
                                                            <asp:ImageButton ID="btnPrintVoucher" runat="server" OnClick="btnPrintVoucher_Click"
                                                                ImageUrl="~/Images/btnPopupPrint.png" />
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:ImageButton ID="btnClose" OnClientClick="closeDialog();return false;" ImageUrl="~/Images/btnPopupClose.png"
                                                                runat="server"></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:PlaceHolder>
                            </ContentTemplate>
                            <%-- <Triggers>
                            <asp:PostBackTrigger ControlID="btnPrintVoucher" />
                        </Triggers>--%>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdateProgress ID="upprog" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <img src="../images/working.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <div class="DetailPage2ndDiv">
            <div style="width: 980px; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                </div>
                <div style="background-color: White; min-height: 450px;">
                    <div style="padding-top: 20px; border-bottom: 1px solid #E6E6E5; overflow: hidden;
                        height: 55px;">
                        <div style="padding-left: 15px;">
                            <div style="float: left; font-size: 24px; font-weight: bold; color: #5e636c; padding-top: 10px;">
                                My <span style="color: #ff42e7;">Gifts</span>
                            </div>
                            <div style="float: left; color: #29B1E6; font-size: 18px; font-weight: bold; margin-right: 10px;
                                padding-left: 60px; padding-top: 13px;">
                                Show :
                            </div>
                            <div style="float: left; padding-top: 9px;">
                                <select class="detailDropDown" style="width: 150px;" name="jumpMenu" onchange="javascript:SelectVoucherType();"
                                    id="ddlVoucherType">
                                    <option value="Available">Available</option>
                                    <option value="Used">Used</option>
                                    <option value="Cancelled">Cancelled</option>
                                    <option value="Expired">Expired</option>
                                    <%--<option value="All">All</option>--%>
                                </select>
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
                                                <asp:Label ID="label18" runat="server" Font-Names="Helvetica" Text="Available <span style='color: #ff42e7;'>Gifts</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <asp:Panel ID="AvailableVouchers" Style="overflow: hidden; padding-top: 10px;" runat="server">
                                                <div style="width: 980px;">
                                                    <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                        <tr>
                                                            <td class="cellFirst1" style="padding-left: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label19" runat="server" Text="Name"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 480px;">
                                                                <b>
                                                                    <asp:Label ID="Label20" runat="server" Text="Note"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 40px;">
                                                                <b>
                                                                    <asp:Label ID="Label21" runat="server" Text="Expiry Date"></asp:Label></b>
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
                                                                        <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 100%;
                                                                            border-bottom: 1px solid #ff42e7;">
                                                                            <div style="float: left;">
                                                                                <div style="height: 93px; width: 130px; text-align: center; vertical-align: middle;">
                                                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                    <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                        alt="" />
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left; padding-left: 25px; width: 380px;">
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
                                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <div class="MemberArea_GridHeading" style="clear: both; padding-right: 5px;">
                                                                                                                        <asp:Label ID="lblDetailID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                                                                                                        <%# getDealCode(Eval("dealOrderCode"), Eval("status"), Eval("displayIt"))%></div>
                                                                                                                    <div style="clear: both; padding-top: 25px;">
                                                                                                                        <asp:UpdatePanel ID="upDownloadFile" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="button-group">
                                                                                                                                    <%-- <asp:LinkButton ID="btnRequester" CssClass="button  " runat="server" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                                                                                        OnClientClick='<%# "openDialogAndBlock(" + Eval("detailID") + "," + Eval("dOrderID") + ");return false;"%>'
                                                                                                                                        CausesValidation="false" Text="Email It" />--%>
                                                                                                                                    <asp:LinkButton ID="LinkButton1" CssClass="button icon print" runat="server" CommandName="download"
                                                                                                                                        Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' CommandArgument='<%# (Eval("detailID") + "," + Eval("dOrderID")+","+Eval("dealOrderCode"))%>'
                                                                                                                                        Text="Print for Friend" />
                                                                                                                                    <asp:HyperLink CssClass="button icon print" ID="LinkButton2" runat="server" Target="_blank"
                                                                                                                                        Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' NavigateUrl='<%# "~/tastyvoucher.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                        Text="Print Text" />
                                                                                                                                    <asp:HyperLink CssClass="button icon pin" ID="hlTracker" runat="server" Target="_blank"
                                                                                                                                        Visible='<%# (Eval("tracking")==DBNull.Value ? false : Convert.ToBoolean(Eval("tracking"))?  true : false)%>'
                                                                                                                                        NavigateUrl='<%# "~/voucherTrack.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                        Text="Track" />
                                                                                                                                    <asp:LinkButton CssClass="button icon approve green" ID="lbMarkAsSold" runat="server"
                                                                                                                                        CommandName="Login" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                                                                                        CommandArgument='<%#Eval("detailID")%>' Text='<%# getDealStatus(Eval("markUsed"))%>' />
                                                                                                                                </div>
                                                                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("markUsed")%>' Visible="false"></asp:Label>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:PostBackTrigger ControlID="LinkButton1" />
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
                                                                                            TextMode="MultiLine" Text='<%#Eval("customerNote")%>' Height="54px" Width="225px"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="clear: both; padding-top: 5px;">
                                                                                        <asp:LinkButton ID="btnSaveNote" CssClass="button icon edit  green" CommandName="Edit"
                                                                                            Text="Save Note" runat="server"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="MemberArea_GridHeading" style="float: left; padding-left: 65px;">
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
                                                                                                        <asp:LinkButton Style="min-width: 0px !important;" ID="lnkPage" CssClass='<%# Convert.ToInt32((gridview1.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                                            OnClick="lnkPage_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
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
                                            </asp:Panel>
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
                                                <asp:Label ID="label23" runat="server" Font-Names="Helvetica" Text="Used <span style='color: #ff42e7;'>Gifts</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <div id="UsedVouchers" style="padding-top: 10px; width: 980px;">
                                                <asp:Panel ID="pnlgvUsed" runat="server">
                                                    <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                        <tr>
                                                            <td class="cellFirst1" style="padding-left: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label24" runat="server" Text="Name"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 450px;">
                                                                <b>
                                                                    <asp:Label ID="Label25" runat="server" Text="Note"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 40px;">
                                                                <b>
                                                                    <asp:Label ID="Label26" runat="server" Text="Expiry Date"></asp:Label></b>
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
                                                                        <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 100%;
                                                                            border-bottom: 1px solid #ff42e7;">
                                                                            <div style="float: left;">
                                                                                <div style="height: 93px; width: 130px; text-align: center; vertical-align: middle;">
                                                                                    <asp:Label ID="lblUsedID" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                    <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                        alt="" />
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left; padding-left: 35px; width: 340px;">
                                                                                <div style="padding-left: 5px;" title='<%# Convert.ToString(Eval("title")).ToString().Trim()%>'
                                                                                    class="Tipsy MemberArea_GridHeading">
                                                                                    <%# Eval("title").ToString().Trim().Length > 30 ? Convert.ToString(Eval("title")).ToString().Trim().Substring(0, 27) + "..." : Convert.ToString(Eval("title")).ToString().Trim()%>
                                                                                </div>
                                                                                <div>
                                                                                    <asp:GridView ID="gvSubUsed" runat="server" DataKeyNames="detailID" AllowPaging="false"
                                                                                        AutoGenerateColumns="false" OnRowCommand="gvSubItem_Login" ShowHeader="false"
                                                                                        GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <div style="padding-top: 5px;">
                                                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <div class="MemberArea_GridHeading" style="clear: both; padding-right: 5px;">
                                                                                                                        <asp:Label ID="lblDetailIDUsed" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                                                                                                        <%# getDealCode(Eval("dealOrderCode"), Eval("status"), Eval("displayIt"))%></div>
                                                                                                                    <asp:UpdatePanel ID="upDownloadFileUsed" runat="server" UpdateMode="Conditional">
                                                                                                                        <ContentTemplate>
                                                                                                                            <div style="padding-top: 25px;" class="button-group">
                                                                                                                                <%-- <asp:LinkButton ID="btnRequesterUsed" CssClass="button  " runat="server" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                                                                                    OnClientClick='<%# "openDialogAndBlock(" + Eval("detailID") + "," + Eval("dOrderID") + ");return false;"%>'
                                                                                                                                    CausesValidation="false" Text="Email It" />--%>
                                                                                                                                <asp:LinkButton ID="LinkButton1Used" CssClass="button icon print" runat="server"
                                                                                                                                    CommandName="download" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                                                                                    CommandArgument='<%# (Eval("detailID") + "," + Eval("dOrderID")+","+Eval("dealOrderCode"))%>'
                                                                                                                                    Text="Print for Friend" />
                                                                                                                                <asp:HyperLink CssClass="button icon print" ID="LinkButton232" runat="server" Target="_blank"
                                                                                                                                    Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' NavigateUrl='<%# "~/tastyvoucher.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                    Text="Print Text" />
                                                                                                                                <asp:HyperLink CssClass="button icon pin" ID="hlUsedTracker" runat="server" Target="_blank"
                                                                                                                                    Visible='<%# (Eval("tracking")==DBNull.Value ? false : Convert.ToBoolean(Eval("tracking"))?  true : false)%>'
                                                                                                                                    NavigateUrl='<%# "~/voucherTrack.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'
                                                                                                                                    Text="Track" />
                                                                                                                                <asp:LinkButton CssClass="button danger icon loop" ID="lbMarkAsSoldUsed" runat="server"
                                                                                                                                    CommandName="Login" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                                                                                    CommandArgument='<%#Eval("detailID")%>' Text='<%# getDealStatus(Eval("markUsed"))%>' />
                                                                                                                            </div>
                                                                                                                            <asp:Label ID="lblStatusUsed" runat="server" Text='<%#Eval("markUsed")%>' Visible="false"></asp:Label>
                                                                                                                        </ContentTemplate>
                                                                                                                        <Triggers>
                                                                                                                            <asp:PostBackTrigger ControlID="LinkButton1Used" />
                                                                                                                        </Triggers>
                                                                                                                    </asp:UpdatePanel>
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
                                                                                        <asp:TextBox ID="txtvoucherNoteUsed" CssClass="TextBox" Font-Size="14px" runat="server"
                                                                                            TextMode="MultiLine" Text='<%#Eval("customerNote")%>' Height="54px" Width="255px"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="clear: both; padding-top: 5px;">
                                                                                        <asp:LinkButton ID="btnSaveNoteUsed" CssClass="button icon edit  green" CommandName="Edit"
                                                                                            Text="Save Note" runat="server"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="MemberArea_GridHeading" style="float: left; padding-left: 45px;">
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
                                                                                                        <asp:LinkButton Style="min-width: 0px !important;" ID="lnkPage" CssClass='<%# Convert.ToInt32((gvUsed.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                                            OnClick="lnkPageUsed_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                                                                            CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                                <asp:LinkButton Style="width: 35px;" ID="LinkButton3" runat="server" Enabled='<%# displayNext %>'
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
                                                <asp:Label ID="label27" runat="server" Font-Names="Helvetica" Text="Cancelled <span style='color: #ff42e7;'>Gifts</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <div runat="server" id="pnlgvcancelled" style="padding-top: 10px; width: 980px;">
                                                <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                    <tr>
                                                        <td class="cellFirst1" style="padding-left: 15px; width: 470px;">
                                                            <b>
                                                                <asp:Label ID="Label28" runat="server" Text="Name"></asp:Label></b>
                                                        </td>
                                                        <td class="cellFirst1" style="width: 270px;">
                                                            <b>
                                                                <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label></b>
                                                        </td>
                                                        <td class="cellSecond1">
                                                            <b>
                                                                <asp:Label ID="Label30" runat="server" Text="Expiry Date"></asp:Label></b>
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
                                                                    <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 100%;
                                                                        border-bottom: 1px solid #ff42e7;">
                                                                        <div style="float: left;">
                                                                            <div style="height: 93px; width: 130px; text-align: center; vertical-align: middle;">
                                                                                <asp:Label ID="lblcancelledId" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                    alt="" />
                                                                            </div>
                                                                        </div>
                                                                        <div style="float: left; padding-left: 25px; width: 310px;">
                                                                            <div style="padding-left: 5px; float: left;" title='<%# Convert.ToString(Eval("title")).ToString().Trim()%>'
                                                                                class="Tipsy MemberArea_GridHeading">
                                                                                <%# Eval("title").ToString().Trim().Length > 30 ? Convert.ToString(Eval("title")).ToString().Trim().Substring(0, 27) + "..." : Convert.ToString(Eval("title")).ToString().Trim()%>
                                                                            </div>
                                                                        </div>
                                                                        <div style="float: left;" class="MemberArea_GridHeading">
                                                                            <%# Eval("status")%>
                                                                        </div>
                                                                        <div class="MemberArea_GridHeading" style="float: left; padding-left: 200px;">
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
                                                <asp:Label ID="label31" runat="server" Font-Names="Helvetica" Text="Expired <span style='color: #ff42e7;'>Gifts</span>"
                                                    Font-Size="17px" Font-Bold="true" ForeColor="#5e636c"></asp:Label></div>
                                            <div id="pnlgvExpired" runat="server" style="padding-top: 10px; width: 980px;">
                                                <div style="width: 980px;">
                                                    <table cellpadding="0" cellspacing="0" class="DetailPageTopDiv" style="width: 980px;">
                                                        <tr>
                                                            <td class="cellFirst1" style="padding-left: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label32" runat="server" Text="Name"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 360px;">
                                                                <b>
                                                                    <asp:Label ID="Label33" runat="server" Text="Note"></asp:Label></b>
                                                            </td>
                                                            <td class="cellSecond1" style="padding-left: 40px;">
                                                                <b>
                                                                    <asp:Label ID="Label34" runat="server" Text="Expiry Date"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="padding-left: 15px;">
                                                        <asp:GridView runat="server" ID="gvExpired" DataKeyNames="dOrderID" AllowPaging="true"
                                                            AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" PageSize="5" GridLines="None"
                                                            OnPageIndexChanging="gvExpired_PageIndexChanging" Width="100%" ShowHeader="false"
                                                            OnRowDataBound="gvExpired_RowDataBound" OnRowCommand="gvExpired_Login" OnRowEditing="gvExpired_Edit">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 950px;
                                                                            border-bottom: 1px solid #ff42e7;">
                                                                            <div style="float: left;">
                                                                                <div style="height: 93px; width: 130px; text-align: center; vertical-align: middle;">
                                                                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                                                    <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                                                        alt="" />
                                                                                </div>
                                                                            </div>
                                                                            <div style="float: left; padding-left: 25px; width: 280px;">
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
                                                                                                                    <div style="clear: both; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                                                                                                        font-size: 19px;">
                                                                                                                        <asp:Label ID="lblDetailExpiredID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                                                                                                        <%# getDealCode(Eval("dealOrderCode"),Eval("status"),Eval("displayIt"))%></div>
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
                                                                                            TextMode="MultiLine" Text='<%#Eval("customerNote")%>' Height="54px" Width="255px"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="clear: both; padding-top: 5px;">
                                                                                        <asp:LinkButton ID="btnSaveNoteExpired" CssClass="button icon edit  green" CommandName="Edit"
                                                                                            Text="Save Note" runat="server"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="MemberArea_GridHeading" style="float: left; padding-left: 80px;">
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
                                                                                                        <asp:LinkButton Style="min-width: 0px !important;" ID="lnkCancellPage" CssClass='<%# Convert.ToInt32((gvExpired.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                                            OnClick="lnkPageExpired_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
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
                                            </div>
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
                                                <asp:Label ID="label35" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="All vouchers"
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
</asp:Content>
