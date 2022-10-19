<%@ Page Language="C#" AutoEventWireup="true" CodeFile="invoice3.aspx.cs" Inherits="invoice3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="../CSS/fonts.css" type="text/css" charset="utf-8" />
</head>
<body style="font-family: Arial;">
    <form id="form1" runat="server">
    <div id="pagezone">
        <div style="clear: both; padding-top: 40px;">
            <div style="width: 450px; float: left;">
                <div style="clear: both; font-size: 22px; font-weight: bold;">
                    <asp:Label ID="lblInvoiceNumber" runat="server" Text="INVOICE #1259901"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 15px; font-weight: bold;">
                    TO:
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblBusinessName" runat="server"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblBusinessAddress" runat="server"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblBusinessPhone" runat="server"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 25px; font-weight: bold;">
                    CHEQUE SENT TO:
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblBusinessPaymentTitle" runat="server"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblBusinessPaymentAddress" runat="server"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblBusinessPaymentPhone" runat="server"></asp:Label>
                </div>
            </div>
            <div style="width: 450px; float: right; text-align: right;">
                <div style="clear: both; font-size: 14px; font-weight: bold;">
                    <asp:Label ID="lblInvoiceDate" runat="server" Text="INVOICE DATE: yyyy/mm/dd"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 15px; font-weight: bold;">
                    <img src="Images/invoiceLogo.png" />
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 40px; font-weight: bold;">
                    From: Tastygo Online Inc.
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblTastyGOAddress" runat="server" Text="20-206 East 6th Ave Vancouver BC V5T-1J7"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblTastyGoPhone" runat="server" Text="Toll Free: 1-855-295-1771 | Fax: 1-888-717-7073"></asp:Label>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 5px;">
                    <asp:Label ID="lblTastygoSite" runat="server" Text="www.tazzling.com"></asp:Label>
                </div>
            </div>
            <div style="width: 100%; border-bottom: 1px solid black; height: 10px; clear: both;">
            </div>
            <div style="width: 100%; clear: both; font-size: 12px;">
                <div style="width: 650px; float: left; text-align: center;">
                    Deal Title
                </div>
                <div style="width: 250px; float: left; text-align: center;">
                    Campaign Duration
                </div>
            </div>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>
            <asp:Literal ID="ltDealsDetails" runat="server" Text=""></asp:Literal>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>           
            <div style="width: 100%; clear: both; font-size: 16px; padding-top: 20px; padding-left: 15px;
                font-weight: bold;">
                Service
            </div>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; height: 25px; padding-top: 5px;">
                <div style="width: 435px; float: left; text-align: left; padding-left: 15px;">
                    Description
                </div>
                <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                    Unit Price
                </div>
                <div style="width: 150px; float: left; text-align: left;">
                    Line Total
                </div>
            </div>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>
            <asp:Literal ID="ltServiceFeeDetail" runat="server" Text=""></asp:Literal>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>
            <div style="width: 100%; clear: both; font-size: 16px; padding-top: 20px; padding-left: 15px;
                font-weight: bold;">
                Payment Schedule
            </div>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; height: 25px; padding-top: 5px;">
                <div style="width: 435px; float: left; text-align: center; padding-left: 15px;">
                    Date
                </div>
                <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                    % Payment
                </div>
                <div style="width: 150px; float: left; text-align: left;">
                    Line Total
                </div>
            </div>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;
                min-height: 20px;">
                <div style="width: 440px; float: left; text-align: center; padding-left: 10px;">
                    <asp:Label ID="lblFirstPaymentDateTime" runat="server"></asp:Label>
                </div>
                <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                    <asp:Label ID="lblFirstPaymentPer" runat="server"></asp:Label>% First Payment
                </div>
                <div style="width: 150px; float: left; text-align: left; font-weight: normal;">
                    $<asp:Label ID="lblFirstPaymentAmount" runat="server"></asp:Label></div>
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;">
                <div style="width: 440px; float: left; text-align: center; padding-left: 10px;">
                    <asp:Label ID="lblSecondPaymentDateTime" runat="server"></asp:Label>
                </div>
                <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                    <asp:Label ID="lblSecondPaymentPer" runat="server"></asp:Label>% Second Payment
                </div>
                <div style="width: 150px; float: left; text-align: left; font-weight: normal;">
                    $<asp:Label ID="lblSecondPaymentAmount" runat="server"></asp:Label></div>
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;
                min-height: 20px;">
                <div style="width: 440px; float: left; text-align: center; padding-left: 10px;">
                    <asp:Label ID="lblThirdPaymentDateTime" runat="server"></asp:Label>
                </div>
                <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                    <asp:Label ID="lblThirdPaymentPercent" runat="server"></asp:Label>% Final Payment
                </div>
                <div style="width: 150px; float: left; text-align: left; font-weight: normal;">
                    $<asp:Label ID="lblThirdPaymentAmount" runat="server"></asp:Label></div>
            </div>
            <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
            </div>
            <div style="width: 100%; clear: both; font-size: 32px; padding-top: 40px; text-align: center;">
                Thank you for your Business!
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; text-align: center; padding-top: 20px;">
                Interest running another deal? Contact Your Account Manager Today!
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; text-align: center; padding-top: 5px;
                font-weight: bold;">
                <asp:Label ID="lblAccountRepDetail" runat="server" Text=""></asp:Label>
            </div>
            <div style="width: 100%; clear: both; font-size: 12px; text-align: center; padding-top: 30px;">
                For Billing Inquiries, contact us 1-855-295-1771
            </div>        
        </div>
    </div>
    </form>
</body>
</html>
