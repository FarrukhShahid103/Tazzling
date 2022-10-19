<%@ Page Title="Product Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="productManagement.aspx.cs" Inherits="productManagement"
    ValidateRequest="false" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSrchDealTitle').value = '';
            return false;
        }

        function preCheckSelected() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (document.getElementById(list[i]).checked) {
                    return (confirm("Are you sure you want to delete the selected record(s)?"));
                }
            }
            alert("Please select the record(s) to delete!");
            return false;
        }

        function checkAll() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            if (document.getElementById("ctl00_ContentPlaceHolder1_gvViewDeals_ctl01_HeaderLevelCheckBox").checked) {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = true;
                }
            }
            else {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = false;
                }
            }
        }
                                                                       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="JS/jquery-1.4.0.min.js"></script>
    <script type="text/javascript" src="JS/jquery-ui-1.7.2.min.js"></script>
    <link rel='stylesheet' href='CSS/styles.css' type='text/css' media='all' />
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            $("#test-list").sortable({
                handle: '.handle',
                update: function () {
                    var order = escape($('#test-list').sortable('serialize'));
                    // alert(order);
                    $.ajax({
                        type: "POST",
                        url: "../getStateLocalTime.aspx?slotProduct=" + order,
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
   
    </script>
    <asp:UpdatePanel ID="udpnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div id="divSrchFields" runat="server">
                    <div id="search">
                        <div class="heading">
                            <asp:Label ID="lblLastNameSearch" runat="server" Width="63px" Text="Title"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSrchCampaignTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="2" />
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
                           <td style="width:90px; padding-left:100px;">Campaign Title</td>
                           <td style="width:35px; padding-left:45px;">Selling Price</td>
                           <td style="width:50px;">Value Price</td>
                           <td style="width:30px; padding-left:10px;">isActive</td>
                           <td style="width:50px; padding-left:10px;">Created Date</td>
                           <td style="width:40px; padding-left:40px;">Action</td>
                        </tr>
                    </table>
                </div>
                <div style="clear: both;" align="center">
                    <asp:Literal ID="ltProducts" runat="server"></asp:Literal>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
