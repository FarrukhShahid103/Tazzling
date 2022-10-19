<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlDealOrderDetailInfo.ascx.cs"
    Inherits="UserControls_Business_ctrlDealOrderDetailInfo" %>
<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/MemberDashBoard.ascx" %>
<div class="height15">
</div>
<div>
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <div>
                <div style="clear: both; padding-top: 20px">
                    <div style="width: auto; height: 36px; background-color: #005f9f; clear: both; margin-bottom: 10px;">
                        <div style="color: White; font-weight: bold; clear: both;">
                            <usrCtrl:subMenu ID="subMenu1" runat="server" />
                        </div>
                    </div>
                    <div class="DetailPageTopDiv">
                        <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                            <div style="float: left; font-size: 15px;">
                                Deals Order Detail
                            </div>
                        </div>
                    </div>
                    <div class="DetailPage2ndDiv">
                        <div style="float: left; width: 100%; background-color: White; min-height: 450px;
                            border: 1px solid #ACAFB0;">
                            <div style="background-color: White; overflow: hidden; padding: 20px;">
                                <div id="innerDiv">
                                    <div id="search" style="overflow: hidden; height: auto; padding: 15px 10px 0px 10px;">
                                        <div class="heading" style="padding-right: 10px;">
                                            <asp:Label ID="Label1" runat="server" Text="Deal Code"></asp:Label>
                                        </div>
                                        <div class="BusinessSearch">
                                            <asp:TextBox ID="txtSrchDealCode" runat="server" Width="92px" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                        <div class="heading" style="margin-left: 20px; margin-right: 10px;">
                                            <asp:Label ID="lblShowMe" runat="server" Text="Show me"></asp:Label>
                                        </div>
                                        <div class="BusinessSearch">
                                            <asp:DropDownList ID="ddlShowMe" CssClass="TextBox" Width="180px" runat="server">
                                                <asp:ListItem Value="all" Text="All" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="available" Text="Available Deals"></asp:ListItem>
                                                <asp:ListItem Value="used" Text="Used Deals"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="heading" style="margin-left: 20px; margin-right: 10px;">
                                            <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label>
                                        </div>
                                        <div class="BusinessSearch">
                                            <asp:DropDownList ID="ddlDealAddress" CssClass="TextBox" Width="180px" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="button-group" style="overflow: hidden; height: auto; margin-left: 20px;
                                            padding-top: 3px;">
                                            <asp:Button runat="server" ID="btnSearch" CssClass="button big primary Tipsy" ToolTip="Click to cearch"
                                                Text="Search" OnClick="btnSearch_Click" TabIndex="1" />
                                            <asp:Button runat="server" ID="btnDownload" ToolTip="Click to Download Deal Codes in Excel"
                                                CssClass="button icon arrowdown big primary Tipsy" Text="Download" OnClick="btnDownload_Click"
                                                TabIndex="2" />
                                        </div>
                                    </div>
                                    <div style="clear: both; padding: 20px 0px 0px 10px;">
                                        <asp:Label ID="lblNote" Text="<b>Note:</b> Must enter complete voucher code in search"
                                            runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                    </div>
                                    <div style="padding: 10px 0px 5px 10px; clear: both;">
                                        <div style="float: left; padding-right: 5px">
                                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                                        </div>
                                        <div style="float: left;">
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="gv">
                                        <asp:HiddenField ID="hfOrderId" runat="server" />
                                        <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                                        </asp:TextBox>
                                        <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="pageGrid" runat="server" DataKeyNames="detailID" Width="100%" CellPadding="0"
                                                    CellSpacing="0" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                                    GridLines="None" AllowSorting="true" OnSorting="pageGrid_Sorting" OnRowCommand="pageGrid_Login">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="Sr." ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'> </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Customer" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Deal Code" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <a style="min-width: 70px;" class="button" href='<%# "vouchernotes.aspx?did=" +Eval("detailID")%>'>
                                                                    <%# getDealCode(Eval("dealOrderCode"))%>
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="12%" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblIsRedeemed" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Redeemed Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRedeemDate" Visible='<%# Convert.ToBoolean(Eval("isRedeemed"))==false ? false :true %>'
                                                                    runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "redeemedDate").ToString()) %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Security Code" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("voucherSecurityCode") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnEdit" Style="min-width: 80px;" CssClass='<%# Convert.ToBoolean(Eval("isRedeemed"))?"button danger Tipsy":"button Tipsy" %>'
                                                                    ToolTip='<%# Convert.ToBoolean(Eval("isRedeemed"))?"Click to Undo":"Click to Mark as Used" %>'
                                                                    CommandName="Login" CommandArgument='<%#Eval("detailID")%>' Text='<%# Convert.ToBoolean(Eval("isRedeemed"))?"Undo":"Mark as used" %>'
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div id="emptydatarow" align="left">
                                                            <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="memberAreaTable" />
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
                                                                <td width="100%" align="center" style="padding-top: 20px; padding-bottom: 20px;">
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
                                                </asp:GridView>
                                                <asp:Panel ID="pnlHideGrid" runat="server" Visible="false">
                                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="detailID" Width="70%" CellPadding="0"
                                                        CellSpacing="0" AutoGenerateColumns="false" AllowPaging="false" ShowFooter="false"
                                                        GridLines="None" AllowSorting="true" Visible="true">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="8%" HeaderText="Sr." ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'> </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Customer" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Deal Code" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBusniessName" runat="server" Text='<%# Eval("dealOrderCode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Security Code" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("voucherSecurityCode") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Shipping Name" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblShippingName" runat="server" Text='<%# Eval("shippingName") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Shipping Address" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblShippingAddress" runat="server" Text='<%# Eval("ShippingAddress") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Phone Number" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblShippingPhone" runat="server" Text='<%# Eval("Telephone") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="Shipping Note" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="lblShipingNote" runat="server" Text='<%# Eval("shippingNote") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div id="emptydatarow" align="left">
                                                                <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle CssClass="memberAreaTable" />
                                                        <RowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </asp:Panel>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
    </asp:UpdatePanel>
</div>
<div class="height15">
</div>
<div class="height15">
</div>
