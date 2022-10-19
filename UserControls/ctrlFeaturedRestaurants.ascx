<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlFeaturedRestaurants.ascx.cs"
    Inherits="Takeout_UserControls_ctrlFeaturedRestaurants" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="rightcolumn" style="float: left; height: 242px; vertical-align: top;">
    <div class="Recomendedcolumn" style="height: 30px;width:238px;">
        <div style="padding-top: 5px;"><asp:HiddenField ID="hfFeaturedRes" runat="server" />
            <asp:Label ID="lblRecomendedRes" runat="server" CssClass="recomendedtext" Text="Recomended Restaurants"></asp:Label></div>
    </div>
    <asp:Repeater ID="rptrFeaturedRes" runat="server">    
        <ItemTemplate>
            <div class="insidelablels recommendedrestaurantlabels">
                <div style="padding-left: 10px; padding-top: 5px;">
                    <a href='../<%# Eval("userName").ToString()%>' class='reddboldSmall'>
                        <%#Eval("restaurantName")%></a>
                </div>
                <div rel='<%# Eval("restaurantName").ToString().Replace("'","\\'") +"_=_"+ (Eval("restaurantAddress") + ", " + Eval("city") + ", " + Eval("provinceName") + ", " + Eval("zipCode")).ToString().Replace("'","\\'") +"_=_"+ Eval("restaurantPicture") +"_=_"+ ((Eval("detail").ToString().Replace("'","\\'").Length > 80) ? Eval("detail").ToString().Replace("'","\\'").Substring(0, 77) + "..." : Eval("detail").ToString().Replace("'","\\'")) %>' class="Restaurentaddress" style="cursor: pointer; text-decoration: none;">
                    <asp:Label ID="lblResAdd" runat="server" Text='<%# (Eval("restaurantAddress") + ", " + Eval("city") + ", " + Eval("provinceName") + ", " + Eval("zipCode")).ToString().Length > 43 ? (Eval("restaurantAddress") + ", " + Eval("city") + ", " + Eval("provinceName") + ", " + Eval("zipCode")).ToString().Substring(0, 41) + "..." : Eval("restaurantAddress") + ", " + Eval("city") + ", " + Eval("provinceName") + ", " + Eval("zipCode") %>'
                        ToolTip='<%#Eval("restaurantAddress") + ", "+ Eval("city") + ", " + Eval("provinceName") + ", " + Eval("zipCode") %>'></asp:Label>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>