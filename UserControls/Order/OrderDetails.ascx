<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderDetails.ascx.cs"
    Inherits="Takeout_UserControls_Order_OrderDetails" %>
<div class="height10">
</div>
<asp:Panel ID="pnlOrderDetail" runat="server">
    <div class="hr none blueandbold1">
        <h3>
            <asp:Label ID="lblHeader" runat="server" Text="Order Info"></asp:Label></h3>
    </div>
    <div class="height10">
    </div>
     <div style="float: left;">
                <div style="float: left;">
                    <asp:Image align="text-top" ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="red"></asp:Label>
                </div>
            </div>
    <div class="indent30">
        <table cellpadding="0" cellspacing="4" width="100%" border="0">
            <tr>
                <td width="141px" align="left">
                    <asp:Label ID="lblOrder" runat="server" Font-Bold="true" Text="Order Number:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblOrderNo" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Status:"></asp:Label>
                </td>
                <td align="left">
                    <div style="width: 400px;">
                        <div style="width: 111px; float: left;">
                            <asp:Label ID="lblStatus" runat="server" Text="Order Number:"></asp:Label>
                            <asp:DropDownList ID="ddlStatus" runat="server" Visible="false">
                                <asp:ListItem Text="Confirmed" Value="Confirmed"></asp:ListItem>
                                <asp:ListItem Text="Refunded" Value="Refunded"></asp:ListItem>
                                <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="width: 289px; float: right">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn_orange_smaller" Text="Update"
                                Visible="false" OnClick="btnUpdate_Click" /></div>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Order Type:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblOrderType" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Date & Time:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblDateTime" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Restaurant Name:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblRestaurantName" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Ordered From:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblOrderFrom" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Pick Up / Delivery:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblPickUpDelivery" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lbllblDeliveryAddress" runat="server" Font-Bold="true" Text="Delivery Address:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblDeliveryAddress" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
             <tr>
                <td align="left">
                    <asp:Label ID="lbllblDeliveryPhone" runat="server" Font-Bold="true" Text="Delivery Phone:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblDeliveryPhone" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblBillingAddressText" runat="server" Font-Bold="true" Text="Billing Address:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblBillingAddress" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lbllblBillingPhone" runat="server" Font-Bold="true" Text="Billing Phone:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblBillingPhone" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Pick Up / Delivery Time:"></asp:Label>
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblPickUpDeliveryTime" runat="server" Text="Order Number:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Special Request:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblSpecialRequest" runat="server" Text="Special Request"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="height10">
    </div>
    <div class="hr none blueandbold1">
        <h3>
            <asp:Label ID="Label1" runat="server" Text="Order Items"></asp:Label></h3>
    </div>
    <div class="indent30">
        <div style="text-align: left">
        </div>
        <div class="left" style="text-align: left; padding-top: 8px; padding-bottom: 8px;">
            <asp:Label ID="lblTime" runat="server"></asp:Label>
        </div>
        <div class="ordertable">
            <div class="clear">
            </div>
            <asp:GridView runat="server" ID="GridView_Sub" CellPadding="5" CellSpacing="0" Width="100%"
                GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false"
                ShowHeader="true" CssClass="cartItemHeader">
                <Columns>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblItemId" Text='<%# Eval("orderId")%>' />
                            <asp:Label runat="server" ID="lblOldNumber" Text='<%# Eval("quantity")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                        ItemStyle-Width="30%">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblItemNameHeader" Text="Item Name" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Eval("itemName")%></ItemTemplate>
                        <ItemStyle CssClass="menu" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                        ItemStyle-Width="30%">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblItemNameHeader" Text="Special Request" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Eval("instruction")%></ItemTemplate>
                        <ItemStyle CssClass="menu" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblItemNameHeader" Text="Item Price" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            $<asp:Label runat="server" ID="lblPrice" Text='<%# Convert.ToDouble(Eval("unitPrice")).ToString("###.00")%>' /></ItemTemplate>
                        <ItemStyle CssClass="price" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblItemNameHeader" Text="Number" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblItemNameText" Text='<%# Eval("quantity")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="order" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label runat="server" ID="lblItemNameHeader" Text="Amount" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAmount" Text='<%#"$" +  Convert.ToDouble(Convert.ToDouble(Eval("unitPrice")) * Convert.ToInt32(Eval("quantity"))).ToString("###.00") %>' /></ItemTemplate>
                        <ItemStyle CssClass="number" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="cartItemHeader" />
            </asp:GridView>
            <div class="step3_buttonrow">
                <div class="right" align="right" style="padding-right: 52px;">
                    <asp:Literal runat="server" ID="ltTotal" /></div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="height15 clear">
        </div>
    </div>
</asp:Panel>
<div>
    <div class="step3_buttonrow" style="padding-right: 52px;">
        <div class="right" align="right">
            <input name="ctl00$cpSite$ctl00$btnPrint" id="ctl00_cpSite_ctl00_btnPrint" class="btn_orange_bigger"
                onclick="window.print();" value="Print Receipt" type="button"></div>
    </div>
    <div class="left">
        <asp:Button ID="btnBack" runat="server" OnClientClick="javascript:history.go(-1)"
            CssClass="btn_orange_smaller" Text="Back" /></div>
</div>
<asp:Panel runat="server" ID="panelActionMerchant" CssClass="footerStyle" Visible="false">
    <div class="height15">
    </div>
    <div class="buttonrow">
        <asp:Button runat="server" ID="btnDone" CssClass="btn_orange_smaller" OnClick="btnDone_Click" />
        <asp:Button runat="server" ID="btnCancel" CssClass="btn_orange_bigger" OnClick="btnCancel_Click" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="panelActionMember" CssClass="footerStyle" Visible="false">
    <div class="height15">
    </div>
    <div class="buttonrow">
        <asp:Button runat="server" ID="btnGoToCheckout" CssClass="btn_orange_bigger" OnClick="btnGoToCheckout_Click" />
        <asp:Button runat="server" ID="btnAddFavorate" CssClass="btn_orange_bigger" OnClick="btnAddFavorate_Click" />
        <asp:Button runat="server" ID="btnDelete" CssClass="btn_orange_bigger" OnClick="btnDelete_Click" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="panelRefund" CssClass="footerStyle" Visible="false">
    <div class="height15">
    </div>
    <div class="buttonrow">
        <asp:Button runat="server" ID="btnRefund" CssClass="btn_orange_smaller" OnClick="btnRefund_Click" />
    </div>
</asp:Panel>
