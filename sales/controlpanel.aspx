<%@ Page Title="TastyGo | Sales | Control Panel" Language="C#" MasterPageFile="~/sales/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="controlpanel.aspx.cs" Inherits="controlpanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="popup">
        <div id="popHeader">
            <asp:Label ID="lblDashBoardHead" runat="server" Text="Control Panel"></asp:Label>
        </div>
    </div>
    <div class="floatLeft" style="padding-bottom:30px;">
        <asp:Panel ID="pnlSearch" CssClass="dashBoard" runat="server">
          
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton2" runat="server" PostBackUrl="~/sales/restaurantLeadsManagement.aspx"
                        ImageUrl="~/sales/Images/Restaurant_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label3" Text="Leads Management" runat="server"></asp:Label>
                </div>
            </div>
          
        </asp:Panel>
     <%--   <asp:Panel ID="Panel3" CssClass="dashBoard" runat="server" Style="padding-top: 40px;">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton15" runat="server" PostBackUrl="~/sales/giftcard.aspx"
                        ImageUrl="~/sales/Images/Gift-Card_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label16" Text="Gift Cards Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton16" runat="server" PostBackUrl="~/sales/giftcardManagment.aspx"
                        ImageUrl="~/sales/Images/Send_giftcard.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label17" Text="Send Gift Cards" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton17" runat="server" PostBackUrl="~/sales/foodCreditManagement.aspx"
                        ImageUrl="~/sales/Images/Deposite_food-credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label18" Text="Deposit Food Credit" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="imgRef" PostBackUrl="~/sales/referralbanner.aspx" runat="server"
                        ImageUrl="~/sales/Images/Referal-banner-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="lblRef" Text="Referral Banner Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="imgTaxRate" runat="server" PostBackUrl="~/sales/provincesTaxRates.aspx"
                        ImageUrl="~/sales/Images/tax-rate.jpg" />
                </div>
                <div>
                    <asp:Label ID="lblTaxRate" Text="Tax Rate Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton4" runat="server" PostBackUrl="~/sales/cityManagement.aspx"
                        ImageUrl="~/sales/Images/-city management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label5" Text="City Management" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel2" CssClass="dashBoard" runat="server" Style="padding-top: 40px;">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton7" runat="server" PostBackUrl="~/sales/pointsManagment.aspx"
                        ImageUrl="~/sales/Images/-points_user-history_management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label8" Text="Points History" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton6" PostBackUrl="~/sales/pointgift.aspx" runat="server"
                        ImageUrl="~/sales/Images/-Gift_on_points.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label7" Text="Points Gift Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton8" runat="server" PostBackUrl="~/sales/pointsCreditManagement.aspx"
                        ImageUrl="~/sales/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label9" Text="Points Credit Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton9" runat="server" PostBackUrl="~/sales/pointGiftRequests.aspx"
                        ImageUrl="~/sales/Images/-user_gift-request.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label10" Text="Gift Requests Management" runat="server"></asp:Label>
                </div>
            </div>            
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl="~/sales/featuredFoodManagement.aspx"
                        ImageUrl="~/Images/MenuHeader/feature_food.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label1" Text="Featured Food Management" runat="server"></asp:Label>
                </div>
            </div>     
        </asp:Panel>--%>
    </div>
</asp:Content>
