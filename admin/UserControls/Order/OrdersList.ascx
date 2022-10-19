<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrdersList.ascx.cs" Inherits="admin_UserControls_Order_OrdersList" %>
<asp:Panel ID="pnlGrid" runat="server" Visible="true">
    <div id="dfsd">
        <div class="height05">
        </div>
        <div id="search">
            <div class="heading">
                <%= "<b>" + "Veiw transcation by" + "</b>&nbsp;&nbsp;" + "Year" + ":"%></div>
            <div align="left">
                <asp:DropDownList runat="server" ID="ddlYear" />
            </div>
            <div class="heading">
                <%= "Month" + ":"%></div>
            <div align="left">
                <asp:DropDownList runat="server" ID="ddlMonth" />
            </div>
            <div>
                <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                    OnClick="btnSelect_Click" TabIndex="1" /></div>
        </div>
        &nbsp;<div class="clear">
        </div>
    </div>
    <div class="floatLeft" style="padding-top: 2px;padding-bottom:7px; padding-left: 4px;">
        <div style="float: left; padding-right: 5px">
            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/Checked.png" />
        </div>
        <div class="floatLeft">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" Visible="false" Text="Record has been updated successfully." CssClass="fontStyle"></asp:Label>
        </div>
    </div>
    <div id="gv">
        <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
        </asp:TextBox>
        <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
            <ContentTemplate>
                <asp:GridView runat="server" ID="gridview1" AllowPaging="true" AllowSorting="false"
                    AutoGenerateColumns="false" GridLines="None"
                    HorizontalAlign="Center" Width="100%" PageSize="15" RowStyle-HorizontalAlign="Center"
                    OnPageIndexChanging="gridview1_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Order Date &amp; Time</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDate" Text='<%# GetOrderDate(Convert.ToDateTime(Eval("creationDate")), Eval("OrderId").ToString(), Eval("ProviderId").ToString()) %>' />
                            </ItemTemplate>
                            <HeaderStyle CssClass="datecell" />
                            <ItemStyle CssClass="datecell" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Order Type</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblOrderType" ForeColor="#C15821" Font-Bold="true"
                                    Text='<%# Eval("OrderType") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Ordered From</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblOrderFrom" Text='<%#Eval("userName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Pick Up / Delivery</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblShippingMethod" Text='<%# Eval("deliveryMethod").ToString() %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Status</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus" ForeColor="#C15821" Font-Bold="true" Text='<%# Eval("orderStatus")%>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Gross</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAmount" Text='<%# ShowAmount(Eval("CurrencyCode"), Eval("TotalAmount")) %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%# (ConfigurationSettings.AppSettings["CommissionFee"].ToString()).ToString()%>%
                                <%#"Fee"%></HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFee" Text='<%# ShowCommissionAndNetAmount(Eval("OrderStatus"), Eval("CurrencyCode"), Eval("TotalAmount")) %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Net Amount</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNetAmount" Text='<%# GetNetAmount(Eval("OrderStatus"),Eval("OrderType"),Eval("subTotalAmount"), Eval("totalAmount")) %>' /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div id="emptyRowStyle" align="left">
                            There are no matching data in the system.
                        </div>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="gridHeader" />
                    <RowStyle CssClass="gridText" Height="27px" />
                    <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                    <PagerTemplate>
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
                                            <asp:Label ID="lblRecordsPerPage" runat="server" Text="Records per page"></asp:Label>
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
                                                        <asp:TextBox ID="txtPage" CssClass="fontStyle" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"
                                                            Style="padding-left: 12px;" Width="20px" Text="1" runat="server"></asp:TextBox>
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
                <asp:PlaceHolder runat="server" ID="phBottomTotal">
                    <div class="ordersfooter">
                        <div class="totalAmount" align="left" style="padding-left:05px;">
                            This Period Balance:
                            <asp:Label runat="server" ID="lblPeriodSales"></asp:Label></div>
                        <br />                       
                    </div>
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>
