<%@ Page Title="TastyGo | Admin | Control Panel" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="controlpanel.aspx.cs" Inherits="controlpanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="popup">
        <div id="popHeader">
            <asp:Label ID="lblDashBoardHead" runat="server" Text="Control Panel"></asp:Label>
        </div>
    </div>
    <div class="floatLeft" style="padding-bottom: 30px;">
        <asp:Panel ID="pnlCustomerService" CssClass="dashBoard" runat="server" Visible="false">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton29" runat="server" PostBackUrl="~/admin/restaurantManagement.aspx"
                        ImageUrl="~/admin/Images/Restaurant_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label30" Text="Business Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton30" PostBackUrl="~/admin/slotManagment.aspx" runat="server"
                        ImageUrl="~/admin/Images/Referal-banner-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label31" Text="Deal Slot Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton23" runat="server" PostBackUrl="~/admin/userManagement.aspx"
                        ImageUrl="~/admin/Images/User_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label24" Text="User Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton31" runat="server" PostBackUrl="~/admin/dealOrderInfo.aspx"
                        ImageUrl="~/admin/Images/order-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label32" Text="Deal Order Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton26" runat="server" PostBackUrl="~/admin/dealVerificationManagement.aspx"
                        ImageUrl="~/admin/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label27" Text="Deal Verification Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton27" runat="server" PostBackUrl="~/admin/dealOrdersMgmtByUsers.aspx"
                        ImageUrl="~/admin/Images/-cuisine_management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label28" Text="View Deal Ordered History" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton28" runat="server" PostBackUrl="~/admin/dealSampleVouchersManagement.aspx"
                        ImageUrl="~/admin/Images/Gift-Card_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label29" Text="Sample Vouchers Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton24" runat="server" PostBackUrl="~/admin/foodCreditManagement.aspx"
                        ImageUrl="~/admin/Images/Deposite_food-credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label25" Text="Deposit Tasty Credit" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCustomerService2" CssClass="dashBoard" runat="server" Visible="false">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton35" runat="server" PostBackUrl="~/admin/dealVerificationManagement2.aspx"
                        ImageUrl="~/admin/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label36" Text="Deal Verification Management New" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton37" runat="server" PostBackUrl="~/admin/createNewsLetter.aspx"
                        ImageUrl="~/admin/Images/-user_gift-request.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label38" Text="Newsletter Managment" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-top: 15px;">
                    <asp:ImageButton ID="ImageButton44" runat="server" PostBackUrl="~/admin/trackingdealManagement.aspx"
                        ImageUrl="~/admin/Images/tracking.png" />
                </div>
                <div style="padding-top: 17px;">
                    <asp:Label ID="Label45" Text="Tracking Managment" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton45" runat="server" PostBackUrl="~/admin/uploaddocuments.aspx"
                        ImageUrl="~/admin/Images/uploadFiles.png" />
                </div>
                <div style="padding-top: 10px;">
                    <asp:Label ID="Label46" Text="Upload Documents Managment" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlPromoter" CssClass="dashBoard" runat="server" Visible="false">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton25" runat="server" PostBackUrl="~/admin/dealSampleVouchersManagement.aspx"
                        ImageUrl="~/admin/Images/Gift-Card_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label26" Text="Sample Vouchers Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-top: 12px; padding-left: 20px; padding-bottom: 13px;">
                    <asp:ImageButton ID="ImageButton21" runat="server" PostBackUrl="~/admin/contestWinnerManagement.aspx"
                        ImageUrl="~/admin/Images/contestWinner.png" />
                </div>
                <div>
                    <asp:Label ID="Label22" Text="Contest Winner Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton22" runat="server" PostBackUrl="~/admin/dealCauseManagement.aspx"
                        ImageUrl="~/Images/MenuHeader/feature_food.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label23" Text="Deal Cause Management" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlForadmin" CssClass="dashBoard" runat="server" Visible="false">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton10" runat="server" PostBackUrl="~/admin/restaurantManagement.aspx"
                        ImageUrl="~/admin/Images/Restaurant_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label11" Text="Business Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton11" PostBackUrl="~/admin/slotManagment.aspx" runat="server"
                        ImageUrl="~/admin/Images/Referal-banner-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label12" Text="Deal Slot Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton18" runat="server" PostBackUrl="~/admin/dealVerificationManagement.aspx"
                        ImageUrl="~/admin/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label19" Text="Deal Verification Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton19" runat="server" PostBackUrl="~/admin/dealOrdersMgmtByUsers.aspx"
                        ImageUrl="~/admin/Images/-cuisine_management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label20" Text="View Deal Ordered History" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton20" runat="server" PostBackUrl="~/admin/dealSampleVouchersManagement.aspx"
                        ImageUrl="~/admin/Images/Gift-Card_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label21" Text="Sample Vouchers Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton34" runat="server" PostBackUrl="~/admin/dealVerificationManagement2.aspx"
                        ImageUrl="~/admin/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label35" Text="Deal Verification Management New" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-top: 15px;">
                    <asp:ImageButton ID="ImageButton43" runat="server" PostBackUrl="~/admin/trackingdealManagement.aspx"
                        ImageUrl="~/admin/Images/tracking.png" />
                </div>
                <div style="padding-top: 17px;">
                    <asp:Label ID="Label44" Text="Tracking Managment" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-top: 12px; padding-left: 10px; padding-bottom: 13px;">
                    <asp:ImageButton ID="ImageButton50" runat="server" PostBackUrl="~/admin/supplierOrder.aspx"
                        ImageUrl="~/admin/Images/supplier.png" />
                </div>
                <div>
                    <asp:Label ID="Label51" Text="Supplier Orders Management" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel1" CssClass="dashBoard" runat="server" Visible="false">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton2" runat="server" PostBackUrl="~/admin/restaurantManagement.aspx"
                        ImageUrl="~/admin/Images/Restaurant_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label3" Text="Business Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="imgRef" PostBackUrl="~/admin/slotManagment.aspx" runat="server"
                        ImageUrl="~/admin/Images/Referal-banner-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="lblRef" Text="Deal Slot Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="imgOrder" runat="server" PostBackUrl="~/admin/dealOrderInfo.aspx"
                        ImageUrl="~/admin/Images/order-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="lblOrder" Text="Deal Order Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="imgUser" PostBackUrl="~/admin/dealInvoice.aspx" runat="server"
                        ImageUrl="~/admin/Images/User_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="lblUser" Text="Payments Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton14" runat="server" PostBackUrl="~/admin/dealVerificationManagement.aspx"
                        ImageUrl="~/admin/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label15" Text="Deal Verification Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton9" runat="server" PostBackUrl="~/admin/createNewsLetter.aspx"
                        ImageUrl="~/admin/Images/-user_gift-request.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label10" Text="Newsletter Managment" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton39" runat="server" PostBackUrl="~/admin/uploaddocuments.aspx"
                        ImageUrl="~/admin/Images/uploadFiles.png" />
                </div>
                <div style="padding-top: 10px;">
                    <asp:Label ID="Label40" Text="Upload Documents Managment" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-top: 15px;">
                    <asp:ImageButton ID="ImageButton42" runat="server" PostBackUrl="~/admin/trackingdealManagement.aspx"
                        ImageUrl="~/admin/Images/tracking.png" />
                </div>
                <div style="padding-top: 17px;">
                    <asp:Label ID="Label43" Text="Tracking Managment" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel3" CssClass="dashBoard" runat="server" Style="padding-top: 40px;"
            Visible="false">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton15" runat="server" PostBackUrl="~/admin/giftcard.aspx"
                        ImageUrl="~/admin/Images/Gift-Card_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label16" Text="Gift Cards Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton16" runat="server" PostBackUrl="~/admin/giftcardManagment.aspx"
                        ImageUrl="~/admin/Images/Send_giftcard.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label17" Text="Send Gift Cards" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton17" runat="server" PostBackUrl="~/admin/foodCreditManagement.aspx"
                        ImageUrl="~/admin/Images/Deposite_food-credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label18" Text="Deposit Tasty Credit" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="imgCuisine" runat="server" PostBackUrl="~/admin/dealOrdersMgmtByUsers.aspx"
                        ImageUrl="~/admin/Images/-cuisine_management.jpg" />
                </div>
                <div>
                    <asp:Label ID="lblCuisine" Text="View Deal Ordered History" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-top: 15px; padding-bottom: 10px;">
                    <asp:ImageButton ID="imgTaxRate" runat="server" PostBackUrl="~/admin/IPhoneNotification.aspx"
                        ImageUrl="~/admin/Images/iPhonecp.png" />
                </div>
                <div>
                    <asp:Label ID="lblTaxRate" Text="iPhone Notification" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton4" runat="server" PostBackUrl="~/admin/cityManagement.aspx"
                        ImageUrl="~/admin/Images/-city management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label5" Text="City Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton40" runat="server" PostBackUrl="~/admin/deviceInformation.aspx"
                        ImageUrl="~/admin/Images/deviceVersion.png" />
                </div>
                <div>
                    <asp:Label ID="Label41" Text="App Version Managment" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton46" runat="server" PostBackUrl="~/admin/paymentDue.aspx"
                        ImageUrl="~/admin/Images/check_Payment.png" />
                </div>
                <div>
                    <asp:Label ID="Label47" Text="Payment Due" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel2" CssClass="dashBoard" runat="server" Style="padding-top: 40px;"
            Visible="false">
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton7" runat="server" PostBackUrl="~/admin/affiliatePartners.aspx"
                        ImageUrl="~/admin/Images/-points_user-history_management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label8" Text="Affiliate Partner Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton6" PostBackUrl="~/admin/affiliateCreditManagement.aspx"
                        runat="server" ImageUrl="~/admin/Images/-Gift_on_points.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label7" Text="Affiliate Credit Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton8" runat="server" PostBackUrl="~/admin/pointsCreditManagement.aspx"
                        ImageUrl="~/admin/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label9" Text="Points Credit Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="imgWithdraw" runat="server" PostBackUrl="~/admin/withdrawRequests.aspx"
                        ImageUrl="~/admin/Images/Withdraw-Request.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label2" Text="Withdraw Requests Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl="~/admin/dealCauseManagement.aspx"
                        ImageUrl="~/Images/MenuHeader/feature_food.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label1" Text="Deal Cause Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton3" runat="server" PostBackUrl="~/admin/userManagement.aspx"
                        ImageUrl="~/admin/Images/User_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label4" Text="User Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton5" PostBackUrl="~/admin/bannersManagement.aspx" runat="server"
                        ImageUrl="~/admin/Images/Referal-banner-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label6" Text="Banners Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton47" PostBackUrl="~/admin/slotManagmentcategory.aspx"
                        runat="server" ImageUrl="~/admin/Images/Referal-banner-management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label48" Text="Products Slot Management" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel4" CssClass="dashBoard" runat="server" Style="padding-top: 40px;"
            Visible="false">
            <div>
                <div style="padding-top: 12px; padding-left: 20px; padding-bottom: 13px;">
                    <asp:ImageButton ID="ImageButton12" runat="server" PostBackUrl="~/admin/contestWinnerManagement.aspx"
                        ImageUrl="~/admin/Images/contestWinner.png" />
                </div>
                <div>
                    <asp:Label ID="Label13" Text="Contest Winner Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton13" runat="server" PostBackUrl="~/admin/dealSampleVouchersManagement.aspx"
                        ImageUrl="~/admin/Images/Gift-Card_Management.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label14" Text="Sample Vouchers Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton32" runat="server" PostBackUrl="~/admin/createNewsLetterforExactTarget.aspx"
                        ImageUrl="~/admin/Images/-user_gift-request.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label33" Text="Newsletter For Exact Target" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton33" runat="server" PostBackUrl="~/admin/dealVerificationManagement2.aspx"
                        ImageUrl="~/admin/Images/-points_credit.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label34" Text="Deal Verification Management New" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-bottom: 5px;">
                    <asp:ImageButton ID="ImageButton36" runat="server" PostBackUrl="~/admin/ipAddressManagement.aspx"
                        ImageUrl="~/admin/Images/ip_network.png" />
                </div>
                <div>
                    <asp:Label ID="Label37" Text="IP Block Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-bottom: 20px;">
                    <asp:ImageButton ID="ImageButton38" runat="server" PostBackUrl="~/admin/dealHSTManagement.aspx"
                        ImageUrl="~/admin/Images/hst.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label39" Text="HST Management" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton41" runat="server" PostBackUrl="~/admin/createNewsLetterforCampaignMonitor.aspx"
                        ImageUrl="~/admin/Images/-user_gift-request.jpg" />
                </div>
                <div>
                    <asp:Label ID="Label42" Text="Newsletter For Campaign Monitor" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>
                    <asp:ImageButton ID="ImageButton48" runat="server" PostBackUrl="~/admin/categoriesManagement.aspx"
                        ImageUrl="~/admin/Images/imgBranchManagement.gif" />
                </div>
                <div>
                    <asp:Label ID="Label49" Text="Categories Management" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel5" CssClass="dashBoard" runat="server" Style="padding-top: 40px;"
            Visible="false">
            <div>
                <div style="padding-top: 12px; padding-left: 10px; padding-bottom: 13px;">
                    <asp:ImageButton ID="ImageButton49" runat="server" PostBackUrl="~/admin/supplierOrder.aspx"
                        ImageUrl="~/admin/Images/supplier.png" />
                </div>
                <div>
                    <asp:Label ID="Label50" Text="Supplier Orders Management" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
