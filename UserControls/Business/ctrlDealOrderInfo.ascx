<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlDealOrderInfo.ascx.cs"
    Inherits="UserControls_Business_ctrlDealOrderInfo" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/MemberDashBoard.ascx" %>
<div>
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <div>
                <div style="clear: both;">
                    <div style="width: auto; height: 36px; background-color: #005f9f; clear: both; margin-top: 20px;
                        margin-bottom: 10px;">
                        <div style="color: White; font-weight: bold; clear: both; text-decoration: none;">
                            <usrCtrl:subMenu ID="subMenu1" runat="server" />
                        </div>
                    </div>
                    <div class="DetailPageTopDiv">
                        <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                            <div style="float: left; font-size: 15px;">
                                Deals Archive
                            </div>
                        </div>
                    </div>
                    <div class="DetailPage2ndDiv">
                        <div style="float: left; width: 100%; background-color: White; min-height: 450px;
                            border: 1px solid #ACAFB0;">
                            <div style="background-color: White; overflow: hidden; padding: 20px;">
                                <div id="innerDiv">
                                    <div id="search" style="overflow: hidden; height: auto; padding-top: 10px; margin-left: 10px;
                                        margin-right: 10px;">
                                        <div style="margin-right: 10px; padding-top: 8px;">
                                            <asp:DropDownList ID="ddlSearchStatus" CssClass="TextBox" runat="server">
                                                <asp:ListItem Value="all" Text="All" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="started" Text="Started Deals"></asp:ListItem>
                                                <asp:ListItem Value="expired" Text="Expired Deals"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="overflow: hidden; padding-top: 7px; height: auto;">
                                            <asp:Button runat="server" ID="btnSearch" CssClass="button big icon search primary"
                                                Text="Search" OnClick="btnSearch_Click" TabIndex="1" />
                                        </div>
                                    </div>
                                    <div style="padding: 10px; padding-left: 4px;">
                                        <div style="float: left; padding-right: 5px">
                                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                                        </div>
                                        <div style="float: left;">
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="gv">
                                        <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                                        </asp:TextBox>
                                        <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%; background-color: #E6E6E5; height: 30px; overflow: hidden;"
                                                    width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr style="height: 50px;">
                                                        <td style="padding-left: 15px; width: 150px;">
                                                            <asp:Label ID="lblPonintsDate" CssClass="MemberArea_GridHeading" runat="server" Text="Deal Title"></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 30px; width: 30px; clear: both; overflow: hidden; text-align: center;">
                                                            <asp:Label ID="lblPoints" CssClass="MemberArea_GridHeading" runat="server" Text="Total Orders"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; clear: both; overflow: hidden; text-align: center;">
                                                            <asp:Label ID="lblGetsFrom" runat="server" CssClass="MemberArea_GridHeading" Text="Successful Orders"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; text-align: center;">
                                                            <asp:Label ID="Label1" runat="server" CssClass="MemberArea_GridHeading" Text="Cancelled Orders"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; clear: both; overflow: hidden; text-align: center;">
                                                            <asp:Label ID="Label2" runat="server" CssClass="MemberArea_GridHeading" Text="Refunded Orders"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; clear: both; overflow: hidden; text-align: center;">
                                                            <asp:Label ID="Label3" runat="server" CssClass="MemberArea_GridHeading" Text="Used Vouchers"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; clear: both; overflow: hidden; text-align: center;">
                                                            <asp:Label ID="Label4" runat="server" CssClass="MemberArea_GridHeading" Text="Un-used Vouchers"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; clear: both; overflow: hidden; text-align: center;">
                                                            <asp:Label ID="Label5" runat="server" CssClass="MemberArea_GridHeading" Text="Download Xls"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; clear: both; overflow: hidden; padding-right: 8px;
                                                            text-align: center;">
                                                            <asp:Label ID="Label6" runat="server" CssClass="MemberArea_GridHeading" Text="Download Pdf"></asp:Label>
                                                        </td>
                                                        <td style="width: 30px; height: 50px; clear: both; overflow: hidden; padding-right: 8px;
                                                            text-align: center;">
                                                            <asp:Label ID="Label7" runat="server" CssClass="MemberArea_GridHeading" Text="Analytic"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:GridView ID="pageGrid" runat="server" DataKeyNames="dealId" Width="100%" AutoGenerateColumns="False"
                                                    CellPadding="0" CellSpacing="0" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                                    GridLines="None" AllowSorting="True" OnSorting="pageGrid_Sorting" OnRowCommand="pageGrid_download">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div>
                                                                    <div style="text-align: left; width: 225px; float: left; padding-left: 5px;">
                                                                        <asp:HyperLink ID="lblItemName" Style="min-width: 200px; width: 200px; text-align: center !important;"
                                                                            ToolTip='<%# Eval("title") %>' CssClass="button Tipsy" runat="server" Text='<%# Eval("title").ToString().Trim().Length > 40 ?  Eval("title").ToString().Trim().Substring(0,37) + "..." :  Eval("title").ToString().Trim() %>'
                                                                            NavigateUrl='<%# "~/default.aspx?sidedeal="+ Eval("dealId")%>'></asp:HyperLink>
                                                                    </div>
                                                                    <div style="float: left; width: 65px;">
                                                                        <asp:Label ID="lblDealOrdred" CssClass="fontStyle" runat="server" Text='<%# Eval("Qty Ordered") %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="float: left; width: 66px;">
                                                                        <asp:HyperLink ID="HyperLink2" Style="width: 66px; color: Red; text-align: center !important;"
                                                                            ToolTip='<%# Eval("title") %>' runat="server" Text='<%# Server.HtmlEncode(Eval("Successful Ordered").ToString()) %>'
                                                                            NavigateUrl='<%# "~/frmBusDealOrderDetailInfo.aspx?did="+ Eval("dealId")%>'></asp:HyperLink>
                                                                    </div>
                                                                    <div style="float: left; width: 80px;">
                                                                        <asp:Label ID="lblCancelledOrders" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(Eval("Cancelled Ordered").ToString()) %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="float: left; width: 90px;">
                                                                        <asp:Label ID="lblRefundedOrders" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(Eval("Refunded Ordered").ToString()) %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="float: left; width: 75px;">
                                                                        <asp:Label ID="lblUsedCount" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(Eval("UsedCount").ToString()) %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="float: left; width: 90px;">
                                                                        <asp:Label ID="lblUnUsedVouchers" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(Eval("UnUsedCount").ToString()) %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="float: left; width: 75px;">
                                                                        <asp:UpdatePanel ID="updownloadExcel" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:ImageButton ID="imgbtnExportToExcel" runat="server" Visible='<%# Convert.ToInt32(Convert.ToInt32(Eval("Successful Ordered"))+Convert.ToInt32(Eval("Successful Ordered"))+Convert.ToInt32(Eval("Successful Ordered")))>0  ? true : false %>'
                                                                                    CommandArgument='<% #Eval("dealId")%>' CommandName="DownloadExcel" CausesValidation="false"
                                                                                    ImageUrl="~/Images/download_excel.png" ToolTip="Download successfull orders in Excel." />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        &nbsp;
                                                                    </div>
                                                                    <div style="float: left; width: 72px;">
                                                                        <asp:UpdatePanel ID="downloadPDF" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:ImageButton ID="imgbtnExportToPDF" runat="server" Visible='<%# Convert.ToInt32(Convert.ToInt32(Eval("Successful Ordered"))+Convert.ToInt32(Eval("Successful Ordered"))+Convert.ToInt32(Eval("Successful Ordered")))>0  ? true : false %>'
                                                                                    CommandArgument='<% #Eval("dealId")%>' CommandName="DownloadPdf" CausesValidation="false"
                                                                                    ImageUrl="~/Images/downloadpdf.png" ToolTip="Download successfull orders in PDF." />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="imgbtnExportToPDF" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        &nbsp;
                                                                    </div>
                                                                    <div style="float: left; width: 97px;">
                                                                        <%-- <asp:HyperLink ID="HyperLink1" Style="width: 75px; text-align: center !important;"
                                                                            ToolTip='<%# Eval("title") %>' CssClass="button Tipsy" runat="server" Text="Analytic" Visible='<%# Convert.ToInt32(Convert.ToInt32(Eval("Successful Ordered"))+Convert.ToInt32(Eval("Successful Ordered"))+Convert.ToInt32(Eval("Successful Ordered")))>0  ? true : false %>'
                                                                            NavigateUrl='<%# "~/dashboarddealstatus.aspx?sidedealId="+ Eval("dealId")%>'></asp:HyperLink>--%>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<% #Eval("dealId")%>'
                                                                            CommandName="checkdealstatus" CausesValidation="false" ImageUrl="~/Images/Analytic.png"
                                                                            ToolTip="Deal Status." />
                                                                        &nbsp;
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div id="emptydatarow" align="left">
                                                            <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <RowStyle Height="41px" HorizontalAlign="Center" />
                                                    <PagerTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td width="30%" align="left" style="padding-left: 2px; display: none;">
                                                                    <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblTotal" Text=" results" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="display: none;" width="30%">
                                                                    <div class="floatRight">
                                                                        <asp:DropDownList ID="ddlPage" runat="server" CssClass="fontStyle" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="floatRight" style="padding-top: 3px; padding-right: 6px;">
                                                                        <asp:Label ID="lblRecordsPerPage" runat="server" Text="Record per Page"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td width="100%" align="center" style="margin-top: 20px;">
                                                                    <table border="0" cellpadding="0" cellspacing="0" style="font-family: Tahoma; font-size: 11px;
                                                                        color: #666666;">
                                                                        <tr>
                                                                            <td style="padding-right: 2px">
                                                                                <div id="divPrevious" style="padding-left: 10px;">
                                                                                    <asp:LinkButton ID="btnPrev" Enabled='<%# displayPrevious %>' CommandName="Page"
                                                                                        CommandArgument="Prev" runat="server" CssClass="button icon arrowleft" Text="Prev" />
                                                                                </div>
                                                                            </td>
                                                                            <td style="padding-top: 5px;">
                                                                                <div class="floatLeft" style="padding-top: 3px; padding-left: 8px;">
                                                                                    <asp:Label ID="lblpage1" runat="server" Text="Page"></asp:Label>
                                                                                </div>
                                                                                <div style="padding-left: 10px; padding-right: 10px; float: left">
                                                                                    <asp:TextBox ID="txtPage" CssClass="TextBox" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"
                                                                                        Style="padding-left: 12px;" Width="20px" Height="20px" Text="1" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <div class="floatLeft" style="padding-top: 3px; padding-right: 4px;">
                                                                                    <asp:Label ID="lblOf" runat="server" Text="of"></asp:Label>
                                                                                    <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                            <td style="padding-left: 4px">
                                                                                <div id="divNext">
                                                                                    <asp:LinkButton ID="btnNext" Enabled='<%# displayNext %>' CssClass="button icon arrowright"
                                                                                        Text="Next" CommandName="Page" CommandArgument="Next" runat="server" />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </PagerTemplate>
                                                    <PagerSettings PageButtonCount="10" Position="Bottom" />
                                                    <PagerStyle HorizontalAlign="Center" Height="41px" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
<div class="height15">
</div>
<div class="height15">
</div>
