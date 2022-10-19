<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlGiftCart.ascx.cs"
    Inherits="Takeout_UserControls_ctrlGiftCart" %>
<div class="height10">
</div>
<div id="GiftCardBasketZone">
    <div align="left" class="ZoneTop">
        <img src="images/head-your-order.png" />
    </div>
    <div class="ZoneCenter">
        <asp:Repeater runat="server" ID="rptGiftCard" OnItemCommand="rptGiftCard_ItemCommand">
            <ItemTemplate>
                <div style="margin-bottom: 5px;padding-left:15px;padding-right:15px;">
                    <div style="width: 170px;overflow: hidden;color:#84837E;text-align:center;font-family:Arial;font-size:13px;font-weight:regular;">
                        <%# Eval("dcGiftName")%></div>
                    <div style="width: 170px;color:#6D6D6D;font-family:Arial;font-size:13px;font-weight:bold;">
                        <div style="width:85px;float: left;padding-left:15px;text-align:right;">
                            <%# Eval("dcGiftQty")%>x ..............</div>
                        <div style="width:70px;float: right;text-align:left;">
                            &nbsp;$<%# Eval("dcGiftUnitPrice")%>&nbsp;<asp:HiddenField ID="hfdcGiftUnitPrice" runat="server"
                                Value='<%# Eval("dcGiftUnitPrice")%>' />
                            <asp:ImageButton ID="imgBasketReduce" runat="server" CommandName="Delete" ImageUrl="~/Images/Basket/basket_reduce.gif"
                                ToolTip="Reduce" /></a>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="ZoneAmount">
        <b>
            <asp:Label runat="server" ID="Label2" Font-Size="14px">Total: </asp:Label>
            <asp:Label runat="server" ID="lblTotal" Font-Size="14px"></asp:Label></b>
    </div>
    <div align="center" class="ZoneBottom">
        <asp:LinkButton ID="btnContinue" runat="server" ForeColor="White" Font-Bold="true"
            Font-Underline="false" Font-Size="14px" Text="Continue >>" OnClientClick="return validateMsgAndAmount();"
            OnClick="btnContinue_Click"></asp:LinkButton>
    </div>
</div>
