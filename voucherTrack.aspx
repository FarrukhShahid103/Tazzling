<%@ Page Language="C#" AutoEventWireup="true" CodeFile="voucherTrack.aspx.cs" Inherits="voucherTrack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="Images/favicon.ico" />
    <title>Untitled Page</title>
    <link rel="stylesheet" href="CSS/fonts.css" type="text/css" charset="utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="pagezone">
        <div class="divTop">
            <div style="float: left; padding-left: 10px; padding-top: 10px; width: 310px;">
                <img id="tgLogo" src="Images/logoForNewsLetter.png" />
            </div>
            <div style="float: right; padding-top: 18px; padding-right: 20px; width: 260px;">
                <div style="clear: both; font-family: @Arial Unicode MS; font-size: 16px; font-weight: bold;
                    text-align: right">
                    TastyGo Voucher Number</div>
                <div style="clear: both; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;
                    text-align: right; padding-top: 3px;">
                    <asp:Label ID="lblTastyGoNumber" runat="server" Text="#tsfpc4ahmma9"></asp:Label>
                </div>
            </div>
        </div>
        <div class="divMiddle">
            <div style="clear: both; text-align: center;">
                <div style="clear: both; padding-top: 30px; color: #00aeff; font-size: 18px; font-weight: bold;">
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
                    <div style="float: left; width: 90%;">
                        <div style="clear: both; font-size: 14px; font-weight: bold;">
                            Shipping Status</div>
                        <div style="clear: both; font-size: 12px; padding-top: 5px; padding-right: 10px;">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        </div>
                        <div id="TrackInfo" runat="server" visible="false">
                            <div style="clear: both; padding-top: 15px; font-size: 14px; font-weight: bold;">
                                Tracking Number</div>
                            <div style="clear: both; font-size: 12px; padding-top: 5px; padding-right: 10px;">
                                <asp:HyperLink ID="hlTrackingNumber" runat="server" Target="_blank" NavigateUrl="http://www.canadapost.ca"></asp:HyperLink>
                            </div>
                            <div style="clear: both; padding-top: 15px; font-size: 14px; font-weight: bold;">
                                ETA:</div>
                            <div style="clear: both; font-size: 12px; padding-top: 5px; padding-right: 10px;">
                                <asp:Label ID="lblEAT" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div style="clear: both; padding-top: 15px; font-size: 14px; font-weight: bold;">
                            Description</div>
                        <div style="clear: both; font-size: 12px; padding-top: 5px; padding-right: 10px;">
                            <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                        </div>
                         <div style="clear: both; padding-top: 15px; font-size: 14px; font-weight: bold;">
                            Fine Print</div>
                        <div style="clear: both; font-size: 12px; padding-top: 5px; padding-right: 10px;">
                            <asp:Label ID="lblFinePrint" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 20px; padding-right: 10px;
                            line-height: 25px;">
                            * It may take 3-4 days for tracking status to update on Canada Post.</br>* ETA,
                            estimated time to arrive.</br>* For further tracking inquiries, please email us
                            <a href='mailto:support@tazzling.com'>support@tazzling.com</a>
                        </div>
                    </div>
                </div>
                <div style="clear: both; padding: 20px;">
                    <div style="border-top: solid 1px #d7d7d5;">
                        &nbsp;</div>
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
