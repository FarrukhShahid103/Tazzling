<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tazzlingVoucher.aspx.cs" Inherits="tazzlingVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="Images/favicon.ico" />
    <title>Voucher</title>
    <link rel="stylesheet" href="CSS/fonts.css" type="text/css" charset="utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="pagezone">
        <div class="divTop">
            <div style="float: left; padding-left: 10px; padding-top: 10px; width: 310px;">
                <img id="tazzlingLogo" src="Images/logo_new.png" alt="" />
            </div>
            <div style="float: right; padding-top: 18px; padding-right: 20px; width: 260px;">
                <div style="clear: both; font-family: @Arial Unicode MS; font-size: 16px; font-weight: bold;
                    text-align: right">
                    Tazzling Voucher Number</div>
                <div style="clear: both; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;
                    text-align: right; padding-top: 3px;">
                    <asp:Label ID="lblTastyGoNumber" runat="server" Text="#tsfpc4ahmma9"></asp:Label>
                </div>
            </div>
        </div>
        <div class="divMiddle">
            <div style="clear: both; text-align: center;">
                <div style="clear: both; padding-top: 30px; color: #DD0016; font-size: 18px; font-weight: bold;">
                    <asp:Label ID="lblDealTitle" runat="server" Text="$5 for Eyebrow Shaping with Thread ($10 Value)"></asp:Label>
                </div>
                <div style="clear: both; padding-top: 30px; color: #babab8; font-size: 18px; font-weight: bold;">
                    <asp:Label ID="lblResturantName" runat="server" Text="Hycroft Chiropractic & Massage"></asp:Label>
                </div>
                <div style="clear: both; padding-top: 20px; font-size: 14px;">
                    <asp:Label ID="lblVoucherDeatail" runat="server" Text="604-733-7744<br>3195 Granville St<br>Vancouver, BC V6H 1S5"></asp:Label>
                </div>
                <div id="imgBarcode" runat="server" visible="false" style="clear: both; padding-top: 20px;">
                    <div style="clear: both; width: 100%; text-align: center;">
                        <img id="imgB" src="Images/barcode2.png" />
                    </div>
                    <div style="clear: both; width: 100%; text-align: center; font-size: 14px;">
                        <asp:Label ID="lblSecurityCode2" runat="server" Text="SecurityCode"></asp:Label>
                    </div>
                </div>
                <div id="lblBarcode" runat="server" visible="true" style="clear: both; padding-top: 50px;">
                    <div style="clear: both; width: 100%; font-family: BarcodeFont; font-size: 60px;
                        text-align: center;">
                        <asp:Label ID="lblSecurityCode" runat="server" Text="SecurityCode"></asp:Label>
                    </div>
                </div>
                <div style="clear: both; padding: 20px; padding-top: 30px;">
                    <div style="border-top: solid 1px #d7d7d5;">
                        &nbsp;</div>
                </div>            
                <div style="clear: both; padding-left: 20px; padding-right: 20px; text-align: left;">
                    <div style="float: left; width: 45%;">
                        <div style="clear: both; font-size: 18px; font-weight: bold;">
                            Rules</div>
                        <div style="clear: both; font-size: 14px; padding-top: 10px; padding-right: 10px;">
                            <asp:Label ID="lblRules" runat="server" Text="These are restrictions that apply to every Tastygo voucher. (Unless the Fine Print specifies an exception)"></asp:Label>
                        </div>
                        <div style="clear: both; padding-top: 15px; font-size: 18px; font-weight: bold;">
                            Redemption and Refund</div>
                        <div style="clear: both; font-size: 14px; padding-top: 10px; padding-right: 10px;
                            padding-bottom: 20px;">
                            <asp:Label ID="lblRedemptionAndRefund" runat="server" Text="Redeemable only at ''business name'' for the goods or service listed above unless specifies an exception in fine print.  If the merchant refuses to honor this voucher, please contact Tastygo to arrange for refund. Visit us <a href='http://www.tazzling.com/faq.aspx' target='_blank' style='color:Black; text-decoration:none;'>http://www.tazzling.com/faq.aspx</a> for our refund policy."></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 45%; padding-left: 5%;">
                        <div style="clear: both;">
                            <div style="clear: both; font-size: 14px; padding-top: 10px;">
                                <asp:Label ID="lblRestText" runat="server" Text="• Not redeemable for cash unless required by law •  Taxes and gratuity not included unless specifies in the fine print •  Voucher cannot be combined with other additional discounts or offers •  Not reloadable. Unauthorized reproduction, modification, or resell are prohibited •  “Business name” is the issuer of this voucher. Purchase, use , or acceptance of this voucher constitutes acceptance of these terms."></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="divBottom">
            <div style="padding-top: 30px; padding-left: 5px; padding-right: 5px;">
                We're here to help! | 1-(855)-295-1771 | Mon-Fri 9:00am – 5:30pm PST | <a href="mailto:support@tazzling.com"
                    style="color: White; text-decoration: none;">support@tazzling.com</a> |
            </div>
        </div>
    </div>
    </form>
</body>
</html>
