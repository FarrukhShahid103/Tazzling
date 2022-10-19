<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderInvoice.ascx.cs"
    Inherits="OrderInvoice" %>
<div class="height10">
</div>
<div id="search">
    <asp:Label ID="lblHeader" runat="server" Text="Order Info"></asp:Label></div>
<div class="height10">
</div>
<div class="indent30">
    <table cellpadding="3" cellspacing="7" width="100%" border="0" class="fontStyle">   
        <tr>
            <td align="left">
                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Status:"></asp:Label>
            </td>
            <td align="left">
                <div style="width: 400px;">
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Date & Time:"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblDateTime" runat="server" Text="Order Number:"></asp:Label>
            </td>
        </tr>
        <tr id="trCardNumber" runat="server" visible="false">
            <td align="left">
                <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Card Number:"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblCardNumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Billing Name:"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblBillingName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblBillingAddressText" runat="server" Font-Bold="true" Text="Billing Address:"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Province/State:"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblBillingProvince" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Billing City:"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblBillingCity" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</div>
<div class="height10">
</div>
<div id="formHeading2">
    <h3>
        <asp:Label ID="Label1" runat="server" Text="Order Detail"></asp:Label></h3>
</div>
<div class="indent30">
    <table cellpadding="3" cellspacing="7" width="100%" border="0" class="fontStyle">
        <tr>
            <td style='float: left; width: 300px;'>
                <strong>Deal Description</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>Quantity</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>Unit Price</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>Total</strong>
            </td>
        </tr>
        <asp:Literal ID="ltPersonalDeal" runat="server" Text=""></asp:Literal>
        <tr id="trPersonal" runat="server" visible="false">
            <td style='float: left; width: 300px;'>
                <asp:Label ID="lblDealPersoanl" runat="server" Text="Deal For Personal"></asp:Label>
            </td>
            <td style='float: left; width: 150px;'>
                <asp:Label ID="lblPersoanlQty" runat="server" Text=""></asp:Label>
            </td>
            <td style='float: left; width: 150px;'>
                <asp:Label ID="lblPersoanlUnitPrice" runat="server" Text=""></asp:Label>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblPersoanlTotal" runat="server" Text=""></asp:Label></strong>
            </td>
        </tr>   
        <tr>
            <td colspan="4" style="width: 100%; border-top: solid 2px gray;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style='float: left; width: 300px;'>
                &nbsp;
            </td>
            <td style='float: left; width: 150px;'>
                &nbsp;
            </td>
            <td style='float: left; width: 150px;'>
                <strong>Total</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                </strong>
            </td>
        </tr>
        <tr id="trShippingAndTax" runat="server" visible="false">
            <td style='float: left; width: 300px;'>
                &nbsp;
            </td>
            <td style='float: left; width: 150px;'>
                &nbsp;
            </td>
            <td style='float: left; width: 150px;'>
                <strong>Shipping & Tax</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblShippingAndTax" runat="server" Text=""></asp:Label>
                </strong>
            </td>
        </tr>
        <tr id="trGrandTotal" runat="server" visible="false">
            <td style='float: left; width: 300px;'>
                &nbsp;
            </td>
            <td style='float: left; width: 150px;'>
                &nbsp;
            </td>
            <td style='float: left; width: 150px;'>
                <strong>Grand Total</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblGrandTotal2" runat="server" Text=""></asp:Label>
                </strong>
            </td>
        </tr>
        <tr id="trTastyCredit" runat="server" visible="false">
            <td style='float: left; width: 300px;'>
                &nbsp;
            </td>
            <td colspan="2" style='float: left; width: 300px;'>
                <strong>Charge From Tasty Credit</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblTastyCreditUsed" runat="server" Text=""></asp:Label>
                </strong>
            </td>
        </tr>
        <tr id="trChargeCreditCard" runat="server" visible="false">
            <td style='float: left; width: 300px;'>
                &nbsp;
            </td>
            <td colspan="2" style='float: left; width: 300px;'>
                <strong>Charge from Credit Card</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblCreditCreditUsed" runat="server" Text=""></asp:Label>
                </strong>
            </td>
        </tr>
        <tr id="trComission" runat="server" visible="false">
            <td style='float: left; width: 300px;'>
                &nbsp;
            </td>
            <td colspan="2" style='float: left; width: 300px;'>
                <strong>Charge from Commission</strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblComission" runat="server" Text=""></asp:Label>
                </strong>
            </td>
        </tr>
        <tr id="trgiven" runat="server" visible="false">
            <td style='float: left; width: 300px;'>
                &nbsp;
            </td>
            <td colspan="2" style='float: left; width: 300px;'>
                <strong>
                    <asp:Label ID="lblTastyCreaditComissionText" runat="server" Text=""></asp:Label></strong>
            </td>
            <td style='float: left; width: 150px;'>
                <strong>
                    <asp:Label ID="lblTastyCreaditComission" runat="server" Text=""></asp:Label>
                </strong>
            </td>
        </tr>
    </table>
</div>
<div class="buttonrow" align="left" style="padding-top: 12px; padding-left: 19px;">
    <asp:ImageButton ID="btnSelect" runat="server" OnClientClick="javascript:window.close()"
        ImageUrl="~/shipper/Images/btnConfirmCancel.gif" />
</div>
