<%@ Page Language="C#" AutoEventWireup="true" CodeFile="invoicecheck.aspx.cs" Inherits="admin_invoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checque</title>
    <link rel="stylesheet" href="../CSS/fonts.css" type="text/css" charset="utf-8" />
    <style>
        @font-face
        {
            font-family: 'Sher Azam';
            src: url(    '../Fonts/gothic.eot' );
            src: url(    '../Fonts/gothic.eot?#iefix' ) format(   'embedded-opentype' ), url(    '../Fonts/gothic.woff' ) format('woff'), url(    '../Fonts/gothic.ttf' ) format(   'truetype' );
            font-weight: normal;
            font-style: normal;
        }
        @font-face
        {
            font-family: 'Sher Azam3';
            src: url(    '../Fonts/nu-century-gothic.eot' );
            src: url(    '../Fonts/nu-century-gothic.eot?#iefix' ) format(   'embedded-opentype' ), url(    '../Fonts/nu-century-gothic.woff' ) format(   'woff' ), url(    '../Fonts/nu-century-gothic.ttf' ) format(   'truetype' );
            font-weight: bold;
        }
        @font-face
        {
            font-family: 'Sher Azam2';
            src: url(    '../Fonts/CALIBRI.eot' );
            src: url(    '../Fonts/CALIBRI.eot?#iefix' ) format(   'embedded-opentype' ), url(    '../Fonts/CALIBRI.woff' ) format(   'woff' ), url(    '../Fonts/CALIBRI.ttf' ) format(   'truetype' );
            font-weight: normal;
            font-style: normal;
        }
    </style>
</head>
<body style="font-family: Sher Azam;">
    <form id="form1" runat="server">
    <div id="pagezone">
        <div style="clear: both; padding-top: 100px;">
            <div>
                <div style="clear: both; font-size: 22px; font-weight: normal; padding-top: 20px;">
                    <div style="text-align:right; font-size:16px;">
                       <asp:Label ID="lblInvoiceDate" runat="server" Text="2012/07/17"></asp:Label>
                    </div>
                    <div style="padding-top:10px;">
                        <div style="float: left; width: 650px; padding-left: 40px;">
                            <asp:Label ID="lblBusinessPaymentTitle" runat="server" Text="ABC Business Inc Cheque payment Title"></asp:Label></div>
                        <div style="float: right; padding-top: 5px; font-size: 23px;">
                            <asp:Label ID="lblInvoiceTopAmount" runat="server" Text="2512.19"></asp:Label></div>
                    </div>
                </div>
                <div style="clear: both; padding-top: 15px; padding-bottom: 10px; font-size: 14px;
                    text-align: right; padding-right: 60px; font-family: Sher Azam2;">
                    <asp:Label ID="lblAmountInWords" runat="server" Text="TWO THOUSAND FIVE HUNDRED TWELVE"></asp:Label>
                </div>
                <div style="width: 550px; float: left;">
                    <div style="clear: both;">
                        <div style="clear: both; font-size: 11px; padding-top: 100px; padding-left: 18px;">
                            <asp:Label ID="lblIPaymentType" runat="server" Text="Tastygo first payment"></asp:Label>
                        </div>
                    </div>
                    <div style="clear: both; padding-left: 70px; padding-top: 265px;">
                        <div style="clear: both; font-size: 22px; padding-top: 50px;">
                            <asp:Label ID="lblBusinessName" runat="server" Text="Business Name"></asp:Label>
                        </div>
                        <div style="clear: both; font-size: 22px; padding-top: 10px; line-height:normal;">
                            <asp:Label ID="lblBusinessAddress" runat="server" Text="111 Main St"></asp:Label>
                        </div>
                        <div style="clear: both; font-size: 22px; padding-top: 10px;">
                            &nbsp;
                            <asp:Label ID="lblBusinessPhone" Visible="false" runat="server" Text="Vancouver BC V5T-1J7"></asp:Label>
                        </div>
                    </div>
                </div>
                <div style="width: 350px; float: right; text-align: right;">
                    <div style="clear: both; padding-top: 50px; padding-right: 75px;">
                        <img src="Images/checkSignatures.jpg" />
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 610px; text-align: right;">
                        <asp:Label ID="lblInvoiceNumber" runat="server" Text="Invoice Number #1022124345 -1"></asp:Label>
                    </div>
                </div>
            </div>
            <div>
                <div style="clear: both;">
                    <div style="clear: both; font-size: 17px; padding-top: 20px; font-family: Sher Azam3;">
                        Payment Schedule Overview
                    </div>
                    <div style="clear: both; font-size: 15px; padding-top: 25px;">
                        <asp:Label ID="lblFirstPayment" runat="server" Text="First Payment -------------------------2012/07/15 ------------------------$4510.98"></asp:Label>
                    </div>
                    <div style="clear: both; font-size: 15px; padding-top: 20px;">
                        <asp:Label ID="lblSecondPayment" runat="server" Text="Second Payment -------------------2012/08/25 ------------------------$618.22"></asp:Label>
                    </div>
                    <div style="clear: both; font-size: 15px; padding-top: 20px;">
                        <asp:Label ID="lblThirdPayment" runat="server" Text="Third Payment ------------------------2012/10/25------------------------$112.23"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
