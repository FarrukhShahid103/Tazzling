<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="Redemptions.aspx.cs"
    Inherits="Redemptions" Title="Redeem Your Voucher" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/MemberDashBoard.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: auto; height: 36px; background-color: #005f9f; clear: both; margin-bottom: 10px;
        margin-top: 20px;">
        <div style="color: White; font-weight: bold; clear: both;">
            <%--<div style="width: 180px; font-size: 25px; padding: 14px; float: left; margin-left: 10px;">
                    Dash Board
                </div>
                <div style="float: right; margin-right: 20px;">
                    <div style="font-size: 15px; padding: 15px; float: right; margin-right: 20px;">
                        <a class="mydeals" href="frmBusDealOrderInfo.aspx">My Deals</a>
                    </div>
                </div>--%>
            <usrCtrl:subMenu ID="subMenu1" runat="server" />
        </div>
    </div>
    
    
    
    <div class="DetailPageTopDiv" style="margin-bottom:10px;">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                       Redemptions
                    </div>
                </div>
            </div>
    
    <div style="width: auto; height: auto; background-color: White; clear: both;">
        <div style="height: auto; padding-bottom: 20px; padding-top: 40px;">
            <div style="padding: 10px 0px 5px 10px; clear: both;">
                <div style="float: left; padding-right: 5px">
                    <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                </div>
                <div style="float: left;">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                </div>
            </div>
            <div style="clear: both; padding-top: 10px; text-align: left; margin-left: 10px;
                padding-top: 10px;">
                <span style="color: Black; font-family: Helvetica; font-size: 14px;">
                Please enter the Voucher code for Redeem a voucher.
                    </div>
        </div>
        <div style="margin-top: 50px; height: 100px; margin-bottom: 20px; padding-bottom: 20px;">
     
            <div style="width: 43%; margin-left:30px; float: left">
                <div style="float: left; font-family: century gothic; font-size: 15px; font-weight: bold;
                    margin-top: 7px;">
                    <asp:Label ID="Label2" runat="server" Text="Voucher Number"></asp:Label></div>
                <div style="float: left; margin-left: 10px;">
                    <asp:TextBox CssClass="TextBox" ID="txtVoucherNumber" runat="server"></asp:TextBox></div>
            </div>
            <div style="width: 12%; padding-left: 25px; height: 100px; float: left">
                <asp:Button ID="btnSearch" runat="server" Text="Redeem" CssClass="button big" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>
    </div>
</asp:Content>
