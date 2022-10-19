<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="slotManagmentcategory.aspx.cs" Inherits="slotManagmentcategory" Title="Slot Managment For Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="JS/jquery-1.4.0.min.js"></script>
    <script type="text/javascript" src="JS/jquery-ui-1.7.2.min.js"></script>
    <link rel='stylesheet' href='CSS/styles.css' type='text/css' media='all' />
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/CalendarControl.js"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {

            $("#test-list").sortable({
                handle: '.handle',
                update: function () {
                    var order = escape($('#test-list').sortable('serialize'));
                    // alert(order);
                    $.ajax({
                        type: "POST",
                        url: "../getStateLocalTime.aspx?slotProductCategory=" + order,
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        cache: false,
                        success: function (msg) {
                            if (msg == "true") {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    });

                }
            });
        }


        function callCalander() {
            var Cal = window.document.getElementById("ctl00_ContentPlaceHolder1_txtdlStartDate");
            showCalendarControl(Cal);
        }
    </script>
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div id="divSrchFields" runat="server">
                    <div id="search">
                        <div style="clear: both;">
                            <div style="float: left;">
                                <div style="float: left;">
                                    <asp:Label ID="Label1" runat="server" Text="Select Category"></asp:Label>
                                </div>
                                <div style="float: left; padding-left: 10px;">
                                    <asp:DropDownList ID="ddlCategory" runat="server">                                      
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; padding-left: 15px;">
                                    <asp:Label ID="lbldlStartTime" runat="server" Text="Select Time"></asp:Label>
                                </div>
                                <div style="padding-left: 10px; float: left;">
                                    <asp:TextBox ID="txtdlStartDate" runat="server" onclick="javascript:callCalander();"
                                        CssClass="txtForm" Width="92px" MaxLength="12"></asp:TextBox>
                                    <asp:DropDownList ID="ddlDLStartHH" runat="server">
                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDLStartMM" runat="server">
                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                        <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                        <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                        <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                        <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                        <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                        <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                        <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                        <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                        <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                        <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                        <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                        <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                        <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                        <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                        <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                        <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                        <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                        <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                        <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                        <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                        <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                        <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                        <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                        <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                        <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDLStartPortion" runat="server">
                                        <asp:ListItem Selected="True" Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; padding-left: 10px;">
                                <div style="float: left;">
                                    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                        OnClick="btnSearch_Click" TabIndex="1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div style="clear: both">
                    <table width="100%" cellpadding="0px" cellspacing="0px">
                        <tr class="gridHeader">
                            <td style="width: 90px; padding-left: 100px;">
                                Campaign Title
                            </td>
                            <td style="width: 35px; padding-left: 45px;">
                                Selling Price
                            </td>
                            <td style="width: 50px;">
                                Value Price
                            </td>
                            <td style="width: 30px; padding-left: 10px;">
                                isActive
                            </td>
                            <td style="width: 50px; padding-left: 10px;">
                                Created Date
                            </td>
                            <td style="width: 40px; padding-left: 40px;">
                               &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="clear: both;" align="center">
                    <asp:Literal ID="ltDealDetail" runat="server" Text=""></asp:Literal>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
