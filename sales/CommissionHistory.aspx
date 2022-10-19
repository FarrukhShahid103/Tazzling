<%@ Page Language="C#" MasterPageFile="~/sales/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="CommissionHistory.aspx.cs" Inherits="sales_CommissionHistory" Title="Commission History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../admin/JS/CalendarControl.js" type="text/javascript"></script>
    <link href="../admin/CSS/CalendarControl.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function OpenCalendar(ctrl) {
            var Cal = window.document.getElementById(ctrl);
            showCalendarControl(Cal);
        }
    </script>
    <style type="text/css">
        .visoft__tab_xpie7 .ajax__tab_body
        {
            padding-top: 0 !important;
            padding-left: 0 !important;
        }
    </style>
    <link href="CSS/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery-1.2.6.js" type="text/javascript"></script>
    <script src="JS/jquery-ui.min.js" type="text/javascript"></script>
    <script src="JS/jquery.mtz.monthpicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_tc_tp1_txtDealName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tc_tp1_txtBusinessName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tc_tp1_txtStartDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tc_tp1_txtEndDate').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tc_tp1_txtSalesPersonAccountName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_tc_tp1_ddlSearchCity').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_tc_tp1_ddlSearchProvince').selectedIndex = 0;

            return false;
        }


        function clearFields2() {
            document.getElementById('ctl00_ContentPlaceHolder1_tc_TabPanel1_txtMonthYear').value = '';
            return false;
        }

        function pageLoad() {
            $('#ctl00_ContentPlaceHolder1_tc_TabPanel1_txtMonthYear').monthpicker({
                'monthNames': ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Out', 'Nov', 'Dec']
            })

        }
    </script>
    <link href="../CSS/visoft__tab_xpie7.css" rel="stylesheet" type="text/css" />
    <cc2:TabContainer ID="tc" runat="server" CssClass="visoft__tab_xpie7">
        <cc2:TabPanel ID="tp1" runat="server" HeaderText="All Record">
            <ContentTemplate>
                <div id="Area1">
                    <asp:UpdatePanel ID="upGrid" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                                <div id="search3">
                                    <div style="width: 100%;">
                                        <div class="heading">
                                            <asp:Label ID="Label3" runat="server" Text="Select Provice" Width="92px"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlSearchProvince" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchProvince_SelectedIndexChanged"
                                                runat="server" Width="192px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="heading">
                                            <asp:Label ID="Label4" runat="server" Text="Select City"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlSearchCity" runat="server" Width="192px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="heading">
                                            <asp:Label ID="lblUsernameSearch" runat="server" Text="Deal Name"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDealName" runat="server" CssClass="txtSearch"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="width: 100%; padding-top: 15px;">
                                        <div class="heading">
                                            <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtBusinessName" runat="server" CssClass="txtSearch"></asp:TextBox>
                                        </div>
                                        <div class="heading">
                                            <asp:Label ID="Label5" runat="server" Text="Start Date"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtStartDate" onclick="OpenCalendar('ctl00_ContentPlaceHolder1_tc_tp1_txtStartDate');"
                                                runat="server" CssClass="txtSearch"></asp:TextBox>
                                        </div>
                                        <div class="heading">
                                            <asp:Label ID="Label6" runat="server" Text="End Date"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEndDate" onclick="OpenCalendar('ctl00_ContentPlaceHolder1_tc_tp1_txtEndDate');"
                                                runat="server" CssClass="txtSearch"></asp:TextBox>
                                        </div>
                                        <div id="SalesPersonDiv" runat="server" class="heading" style="clear: both; margin-top: 10px;">
                                            <asp:Label ID="lblSalesPerson" runat="server" Text="Sales Person"></asp:Label>
                                        </div>
                                        <div style="margin-top: 10px; margin-left: 14px;">
                                            <asp:TextBox ID="txtSalesPersonAccountName" runat="server" CssClass="txtSearch"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: 10px; margin-left: 80px;">
                                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/sales/Images/btnSearch.png"
                                                OnClick="btnSearch_Click" TabIndex="2" />&nbsp;
                                            <asp:ImageButton ID="btnClear" OnClick="btnClear_Click" runat="server" ImageUrl="~/sales/Images/btnClear.png"
                                                OnClientClick="return clearFields();" TabIndex="3" />
                                        </div>
                                    </div>
                                </div>
                                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                                    <div style="float: left; padding-right: 5px">
                                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                                    </div>
                                    <div class="floatLeft">
                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                    </div>
                                </div>
                                <div id="gv">
                                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                                    </asp:TextBox>
                                    <asp:GridView ID="pageGrid" runat="server" DataKeyNames="restaurantId" Width="100%"
                                        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true" OnSorting="pageGrid_Sorting"
                                        OnPageIndexChanging="pageGrid_PageIndexChanging" OnRowCommand="pageGrid_OnRowCommand"
                                        GridLines="None">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton Style="margin-left: 10px; text-align: left" ID="lblDealNameHead"
                                                        ForeColor="White" runat="server" Text="Deal Title" CommandName="Sort" CommandArgument="title"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label Style="padding-left: 10px; text-align: left" ID="lblDealName" runat="server"
                                                            Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 21 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,19) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                            ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton Style="text-align: left" ID="lblrestaurantBusinessName" ForeColor="White"
                                                        runat="server" Text="Business Name" CommandName="Sort" CommandArgument="restaurantBusinessName"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label Style="text-align: left; float: left;" ID="lblBusniessName" runat="server"
                                                            Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 19 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,16) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                            ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblUsernameHead" runat="server" Style="text-align: left" ForeColor="White"
                                                        Text="Sales Person’s Name"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label Style="text-align: left" ID="lblfirstname" runat="server" Text='<%# Eval ("firstname") + " " + Eval ("lastname")  %>'
                                                            ToolTip='<%#  Eval ("firstname") + " " + Eval ("lastname") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton Style="text-align: left" ID="lblDealEndDateHead" ForeColor="White"
                                                        runat="server" Text="Deal End Date" CommandName="Sort" CommandArgument="dealEndTime"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label Style="text-align: left" ID="lbldealenddate" Text='<% # GetDateString(Eval("dealEndTime")) %>'
                                                            runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton Style="text-align: left" ID="lblSalesPersonLastNameHead" ForeColor="White"
                                                        runat="server" Text="Revenue Generated" CommandName="Sort" CommandArgument="lastname"></asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label Style="text-align: left" ID="lbllastname" runat="server" Text='<%# RevenueGenerated((Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>'
                                                            ToolTip='<%# RevenueGenerated((Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label Style="text-align: left" ID="lblRevenueHead" ForeColor="White" runat="server"
                                                        Text="Commission Earned"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="float: left;">
                                                        <%--Visible='<%# (Convert.ToBoolean(IsAdminLogin().ToString()) ? false:true)%>'--%>
                                                        <asp:Label Style="text-align: left" ID="lblRevenue" runat="server" Text='<%# EarnedCommission( (Eval("NegotiatedCommission")).ToString()  ,(Eval("SalePersonCommission")).ToString(),(Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>'
                                                            ToolTip=""></asp:Label>
                                                    </div>
                                                    <div style="float: left;">
                                                        <%--<asp:TextBox ID="txtEarnedCommission" Visible='<%# (Convert.ToBoolean(IsAdminLogin().ToString()) ? true:false)%>' Text='<%# Eval("CommissionEarned") %>' runat="server" Width="50px"></asp:TextBox>--%>
                                                    </div>
                                                    <%--  <div style="float:left; padding-left:5px;">
                                                <asp:ImageButton ID="btnSaveCommission" Visible='<%# (Convert.ToBoolean(IsAdminLogin().ToString()) ? true:false)%>' CommandArgument='<%# Eval ("dealid") + "_" + Container.DataItemIndex  %>' runat="server" CommandName="SaveCommission" CausesValidation="false"
                                                    ImageUrl="~/admin/Images/BtnSaveMini.png" ToolTip="Save Sales Commission" />
                                            </div>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div id="emptyRowStyle" align="left">
                                                <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                            </div>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridText" Height="27px" />
                                        <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                                        <PagerTemplate>
                                            <div style="padding-top: 0px;">
                                                <div id="pager">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family: Tahoma;
                                                        font-size: 11px; color: #666666;">
                                                        <tr>
                                                            <td width="30%" align="left" style="padding-left: 2px;">
                                                                <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                                                                <asp:Label ID="lblTotal" Text=" results" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="30%">
                                                                <div class="floatRight">
                                                                    <asp:DropDownList ID="ddlPage" runat="server" CssClass="fontStyle" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="floatRight" style="padding-top: 3px; padding-right: 6px;">
                                                                    <asp:Label ID="lblRecordsPerPage" runat="server" Text="Record per Page"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td width="40%" align="right" style="padding-right: 2px;">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="font-family: Tahoma; font-size: 11px;
                                                                    color: #666666;">
                                                                    <tr>
                                                                        <td style="padding-right: 2px">
                                                                            <div id="divPrevious">
                                                                                <asp:ImageButton ID="btnPrev" Enabled='<%# displayPrevious %>' CommandName="Page"
                                                                                    CommandArgument="Prev" runat="server" ImageUrl="~/admin/images/imgPrev.jpg" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="floatLeft" style="padding-top: 3px; padding-left: 8px;">
                                                                                <asp:Label ID="lblpage1" runat="server" Text="Page"></asp:Label>
                                                                            </div>
                                                                            <div style="padding-left: 10px; padding-right: 10px; float: left">
                                                                                <asp:TextBox ID="txtPage" CssClass="fontStyle" AutoPostBack="true" Style="padding-left: 12px;"
                                                                                    Width="20px" Text="1" runat="server"></asp:TextBox>
                                                                            </div>
                                                                            <div class="floatLeft" style="padding-top: 3px; padding-right: 4px;">
                                                                                <asp:Label ID="lblOf" runat="server" Text="of"></asp:Label>
                                                                                <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                        <td style="padding-left: 4px">
                                                                            <div id="divNext">
                                                                                <asp:ImageButton ID="btnNext" Enabled='<%# displayNext %>' CommandName="Page" CommandArgument="Next"
                                                                                    runat="server" ImageUrl="~/admin/images/imgNext.jpg" />
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                        </PagerTemplate>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Monthly Sale Report" CssClass="voucherTabs">
            <ContentTemplate>
                <div id="Area2">
                    <asp:UpdatePanel ID="udpnl2" runat="server">
                        <ContentTemplate>
                            <div id="SearchDiv" runat="server">
                                <div id="search">
                                    <div class="heading">
                                        <asp:Label ID="lblFirstNameSearch" runat="server" Text="Select Month"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtMonthYear" runat="server" CssClass="txtSearch"></asp:TextBox>
                                    </div>
                                    <div class="heading">
                                        <asp:Label ID="lblSelectSalesman" runat="server" Text="Select Sales Person"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlSalePersonAccountName" runat="server" CssClass="txtForm">
                                        </asp:DropDownList>
                                    </div>
                                    <div>
                                        <asp:ImageButton ID="btnGetReport" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                            OnClick="btnGetReport_Click" TabIndex="1" />
                                        &nbsp;
                                        <asp:ImageButton ID="BtnClear2" runat="server" ImageUrl="~/admin/Images/btnClear.png"
                                            OnClientClick="return clearFields2();" TabIndex="2" />
                                    </div>
                                </div>
                            </div>
                            <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                                <div style="float: left; padding-right: 5px">
                                    <asp:Image ID="imgError" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                                </div>
                                <div class="floatLeft">
                                    <asp:Label ID="lblErrorForSectab" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                </div>
                            </div>
                            <div style="margin-top: 15px; clear: both;">
                                <asp:GridView ID="grdReport" runat="server" DataKeyNames="restaurantId" Width="100%"
                                    AutoGenerateColumns="False" OnRowDataBound="grdReport_RowDataBound" AllowPaging="false"
                                    AllowSorting="false" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label Style="margin-left: 10px; text-align: left" ID="lblDealNameHead" ForeColor="White"
                                                    runat="server" Text="Deal Title"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label Style="padding-left: 10px; text-align: left" ID="lblDealName" runat="server"
                                                        Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 21 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,19) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                        ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label Style="text-align: left" ID="lblrestaurantBusinessName" ForeColor="White"
                                                    runat="server" Text="Business Name"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label Style="text-align: left; float: left;" ID="lblBusniessName" runat="server"
                                                        Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 19 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,16) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                        ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblUsernameHead" runat="server" Style="text-align: left" ForeColor="White"
                                                    Text="Sales Person’s Name"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label Style="text-align: left" ID="lblfirstname" runat="server" Text='<%# Eval ("firstname") + " " + Eval ("lastname")  %>'
                                                        ToolTip='<%#  Eval ("firstname") + " " + Eval ("lastname") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label Style="text-align: left" ID="lblDealEndDateHead" ForeColor="White" runat="server"
                                                    Text="Deal End Date"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label Style="text-align: left" ID="lbldealenddate" Text='<% # GetDateString(Eval("dealEndTime")) %>'
                                                        runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label Style="text-align: left" ID="lblSalesPersonLastNameHead" ForeColor="White"
                                                    runat="server" Text="Revenue Generated"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:HiddenField ID="hfTotalRevenue" runat="server" Value='<%# RevenueGenerated((Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>' />
                                                    <asp:Label Style="text-align: left" ID="lblTotalRevenue" runat="server" Text='<%# RevenueGenerated((Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>'
                                                        ToolTip='<%# "$" + RevenueGenerated((Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label Style="text-align: left" ID="lblRevenueHead" ForeColor="White" runat="server"
                                                    Text="Commission Earned"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="float: left;">
                                                    <asp:HiddenField ID="hfTotal" runat="server" Value='<%# EarnedCommission( (Eval("NegotiatedCommission")).ToString()  ,(Eval("SalePersonCommission")).ToString(),(Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>' />
                                                    <asp:Label Style="text-align: left" ID="lblTotal" runat="server" Text='<%# "$" + EarnedCommission( (Eval("NegotiatedCommission")).ToString()  ,(Eval("SalePersonCommission")).ToString(),(Eval("SuccessfulOrder")).ToString(),(Eval("sellingPrice")).ToString()) %>'
                                                        ToolTip=""></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                      <%--  <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:GridView ID="gvSubItem" runat="server" DataKeyNames="detailID" AllowPaging="false"
                                                    AutoGenerateColumns="false" ShowHeader="false" GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div style="padding-top: 5px;">
                                                                    <div style="background-color: Gray; color: White; font-size: 14px; width: 100%">
                                                                        <div>
                                                                            Header</div>
                                                                        <div id="SubDiv">
                                                                            <div id="item1">
                                                                                Item 1
                                                                            </div>
                                                                            <div id="Item2">
                                                                                Item 2
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="RowStyle rowClass4Height" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle rowClass4Height" />
                                                </asp:GridView>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div id="emptyRowStyle" align="left" style="clear: both;">
                                            <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                        </div>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridText" Height="27px" />
                                    <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                                </asp:GridView>
                                <div id="BottomArea" visible="false" runat="server" style="background: url('images/BottombgGridHeader.jpg') repeat-x scroll 0 0 transparent;
                                    height: 130px; width: 100%">
                                    <div style="margin-top: 5px; font-weight: bold; color: White; float: right; margin-right: 10px;">
                                        <div style="margin-top: 10px; clear: both;">
                                            <div style="text-align: left; margin-top: 5px;">
                                                <asp:Label ID="lblTotalRevenueGenerated" runat="server" ForeColor="White" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div style="margin-top: 5px; clear: both;">
                                            <div style="text-align: left; margin-top: 5px;">
                                                <asp:Label ID="lblTotalCommissionEarned" runat="server" ForeColor="White" Text="$40"></asp:Label>
                                            </div>
                                        </div>
                                        <div style="margin-top: 5px; clear: both;">
                                            <div runat="server" id="BounsTextBoxDiv">
                                                <div style="float: left; color: Black;">
                                                    <asp:TextBox ID="txtBonus" runat="server" Width="60px" Text="50"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtBonus"
                                                        ErrorMessage="Bonus Amount required" Display="None" ValidationGroup="SaveBonus"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender49" TargetControlID="RequiredFieldValidator18">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator13" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtBonus" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="SaveBonus" Display="None" ValidationExpression="(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9]{1,})?$)"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender50" runat="server" TargetControlID="RegularExpressionValidator13">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                                <div style="float: left; margin-top: 2px; margin-left: 5px;">
                                                    <asp:ImageButton ValidationGroup="SaveBonus" ID="BtnSaveBonus" runat="server" OnClick="BtnSaveBonus_Click"
                                                        ImageUrl="../admin/Images/BtnSaveMini.png" />
                                                </div>
                                            </div>
                                            <div id="lblBounsTextBoxDiv" runat="server" style="text-align: left;">
                                                <asp:Label ID="lblBonus" runat="server" ForeColor="White" Text="$50"></asp:Label>
                                            </div>
                                        </div>
                                        <div style="margin-top: 5px; clear: both;">
                                            <div style="text-align: left;">
                                                <div runat="server" id="TextAdjustmentDiv">
                                                    <div style="float: left; color: Black;">
                                                        <asp:TextBox ID="txtAdjustment" runat="server" Width="60px" Text="-30"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdjustment"
                                                            ErrorMessage="Adjustment Amount required" Display="None" ValidationGroup="SaveAdjustment"
                                                            SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1">
                                                        </cc2:ValidatorCalloutExtender>
                                                        <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                                            runat="server" ControlToValidate="txtAdjustment" ErrorMessage="Only Numeric value required (E.g : 10 or -10)"
                                                            ValidationGroup="SaveAdjustment" Display="None" ValidationExpression="(^-{0,1}\d*\.{0,1}\d+$)"></cc1:RegularExpressionValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RegularExpressionValidator1">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="float: left; margin-top: 2px; margin-left: 5px;">
                                                        <asp:ImageButton ValidationGroup="SaveAdjustment" ID="BtnSaveAdjustment" runat="server"
                                                            OnClick="BtnSaveAdjustment_Click" ImageUrl="../admin/Images/BtnSaveMini.png" />
                                                    </div>
                                                </div>
                                                <div id="lblTextAdjustmentDiv" runat="server">
                                                    <asp:Label ID="lblAdjustment" runat="server" ForeColor="White" Text="$50"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-top: 5px; clear: both;">
                                            <div style="text-align: left;">
                                                <asp:Label ID="lblGrandTotal" runat="server" ForeColor="White" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="margin-top: 5px; font-weight: bold; color: White; float: right; width: 250px;">
                                        <div style="margin-top: 5px; text-align: left;">
                                            <div style="width: 200px; float: left; margin-top: 5px;">
                                                Total Revenue Generated</div>
                                            <div style="float: left; margin-top: 5px;">
                                                =
                                            </div>
                                        </div>
                                        <div style="margin-top: 5px; text-align: left;">
                                            <div style="width: 200px; float: left; margin-top: 5px;">
                                                Total Commission Earned</div>
                                            <div style="float: left; margin-top: 5px;">
                                                =
                                            </div>
                                        </div>
                                        <div style="margin-top: 5px; text-align: left;">
                                            <div style="width: 200px; float: left; margin-top: 5px;">
                                                Bonus</div>
                                            <div style="float: left; margin-top: 5px;">
                                                =
                                            </div>
                                        </div>
                                        <div style="margin-top: 5px; text-align: left;">
                                            <div style="float: left; width: 200px; margin-top: 5px;">
                                                Adjustment</div>
                                            <div style="float: left; margin-top: 5px;">
                                                =</div>
                                        </div>
                                        <div style="margin-top: 5px; text-align: left;">
                                            <div style="float: left; width: 200px; margin-top: 5px;">
                                                Total Earned</div>
                                            <div style="float: left; margin-top: 5px;">
                                                =</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hfGrandTotal" runat="server" />
                            <asp:HiddenField ID="hfGrandTotalRevenue" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
