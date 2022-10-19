<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlPoinGiftCart.ascx.cs"
    Inherits="Takeout_UserControls_ctrlPoinGiftCart" %>
<div class="height10">
</div>
<div id="BasketZone">
    <div class="ZoneOrderTop">Your order</div>
    <div class="ZoneCenter">
        <div class="BodyContent">
            <div id="basketBody">
                <div class="ItemLineContainer">
                    <asp:Label runat="server" ID="lblBlank" Visible="false">Your cart</asp:Label>
                    <asp:Repeater runat="server" ID="rptGiftCard" OnItemCommand="rptGiftCard_ItemCommand">
                        <ItemTemplate>
                            <div class='SingleItem'>
                                <div class='ItemName'>
                                    <div class='ItemNameLeft'>
                                        <%# Eval("dcGiftName")%></div>
                                    <div style='float: right;'>
                                        <asp:ImageButton ID="imgBasketReduce" runat="server" CommandName="Delete" ImageUrl="~/Images/Basket/CartCross.png"
                                            ToolTip="Reduce" /></div>
                                </div>
                                <div class='ItemDetailsLine' style="padding-top:5px;">
                                    <div class='Quantity'>
                                        <%# Eval("dcGiftQty")%>&nbsp;x &nbsp;<%# Eval("dcGiftUnitPrice")%></div>
                                    <div class='Price'>
                                        <%# Eval("dcGiftSubTotalPrice")%>&nbsp;<asp:HiddenField ID="dcGiftID" runat="server"
                                            Value='<%# Eval("dcGiftID")%>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class='SubTotal'>
                    <div class='SubTotalLeft'>
                        Total:</div>
                    <div class='SubTotalRight'>
                        <asp:Label runat="server" ID="lblTotal" Font-Size="14px"></asp:Label></div>
                </div>
            </div>
            <div class="ChooseTime">
                <div class="Link" style="text-align:center;">     
                <asp:LinkButton ID="btnContinue" runat="server" Text="Continue" 
                        OnClientClick="return validateMsgAndAmount();" onclick="btnContinue_Click"></asp:LinkButton>
                           <%--<a href="#ctl00_ContentPlaceHolder1_btnContinue" onclick="document.getElementById('ctl00_ContentPlaceHolder1_btnContinue').focus();">Proceed to continue</a>--%>        
                </div>
            </div>
        </div>
    </div>
</div>