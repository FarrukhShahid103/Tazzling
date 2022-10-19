<%@ Page Title="TastyGo | Gift Card" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="giftcard.aspx.cs" Inherits="giftcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <div align="center">
        <div class="main" style="padding-bottom: 10px;">
            <div class="pagebodyGiftCard">
                <div class="txtBuyGiftCardmain">
                    <div class="txtBuyGiftCard">
                        <p class="txtBuyGiftHead">
                            Give the gift<br />
                            of choice</p>
                        <div class="txtother">
                            Not sure what to buy for the next birthday, anniversary or Christmas present? Tastygo
                            gift cards can be used toward any Tastygo deal, and with new deals announced every
                            day it’s easily guaranteed to impress!
                            <p>
                                There’s no need to wait for sale season to purchase your next gift, simply click
                                on the Buy Gift Card button, select the amount of your choice, and either print
                                it out or send it electronically. Your loved ones will thank you for it!</p>
                        </div>
                    </div>
                </div>
                <%-- <div class="btnbuygiftcard"><img src="images/GiftCard/btnbuyGiftCard.jpg" /></div>--%>
                <div class="btnbuygiftcard">
                    <a href="checkoutgiftcard.aspx">
                        <img src="images/GiftCard/btnbuyGiftCard.png" /></a></div>
            </div>
        </div>
    </div>
</asp:Content>
