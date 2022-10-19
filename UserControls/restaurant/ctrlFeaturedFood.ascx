<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlFeaturedFood.ascx.cs"
    Inherits="Takeout_UserControls_restaurant_ctrlFeaturedFood" %>
<div class="rightcolumnlables" style="float: left; width: 238px;">
    <div class="Recomendedcolumn " style="height: 32px;">
        <div style="padding-top: 5px;">
            <asp:Label ID="lblRecomendedRes" runat="server" CssClass="recomendedtext" Text="Featured Food"></asp:Label></div>
    </div>
    <asp:Repeater ID="rptrFeaturedRes" runat="server">
        <ItemTemplate>
            <div class="insidelablels recommendedrestaurantlabels">
                <div rel='<%# Eval("userId") +"_=_"+  Eval("foodName").ToString().Replace("'","\\'") +"_=_"+ Eval("foodDescription").ToString().Replace("'","\\'") +"_=_"+ Eval("foodPrice") +"_=_"+ Eval("foodImage") %>'
                    style="width: 85%; float: left; cursor: pointer;">
                    <div style="padding-left: 5px; padding-top: 5px;">
                        <asp:HiddenField ID="hfFeaturedFood" runat="server" />
                        <%#Eval("foodName")%></div>
                    <div style="padding-left: 5px; color: Black;">
                        CAD&nbsp; <%# Eval("foodPrice")%></div>
                </div>
                <div style="width: 15%; float: right;">
                    <div style="padding-top: 11px;">
                        <a href="javascript:UpdateItems('addtobasket','<%# Eval("featuredFoodId") %>','<%# Eval("foodName") %>','<%# Eval("foodPrice") %>','Featured Food','4','','');">
                            <img src="images/Basket/cart-icon-latest.gif" alt="" style="cursor: pointer" /></a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
