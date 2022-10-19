<%@ Page Title="Tastygo | FAQ" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="faq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
	$(function() {
		$("#accordion").accordion({ collapsible: true,autoHeight: false });
	});
    </script>

    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                        FAQ
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 980px; background-color: White; min-height: 450px;">
                    <div style="clear: both; padding: 20px;">
                        <div id="accordion">
                            <h3>
                                <a href="#">1) What is Tastygo?</a></h3>
                            <div>
                                <table border="0" cellpadding="4" align="center">
                                    <tr>
                                        <td style="border-bottom: solid 1px #E6E6E5;">
                                            <div align="justify">
                                                <span class="howtoReferNormalTxt">Tastygo is a group buying site featuring daily deals
                                                    of 50-90% off at local businesses. We promise our vendors a minimum number of purchases
                                                    in exchange for these immense discounts. That is how we are able to offer you such
                                                    great deals!</span></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="howtoReferBlackHeader">I've never heard of Tastygo. Is my information safe?</span><br />
                                            <span class="howtoReferNormalTxt">Absolutely! We do not send out spam or sell your information
                                                to any third parties. We partner with <a href="http://www.optimalpayments.com" style="text-decoration: none;
                                                    color: #E25102;" target="_blank">optimalpayments.com</a> to provide you with
                                                the most secure payment gateway. You can validate our Verisign SSL <a href="https://trustsealinfo.verisign.com/splash?form_file=fdf/splash.fdf&dn=www.tazzling.com&lang=en"
                                                    style="text-decoration: none; color: #E25102;" target="_blank">certificate</a>
                                                before submitting your primary information. </span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">2) How do I get my deal?</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                Simply click on the Buy button on the daily deal page and enter your payment information.
                                                Once the minimum number of buys is reached, the deal is on! You will receive an
                                                email confirming your purchase along with your voucher after the deal expires. You
                                                may print it at home or go green and show the voucher on your smart phone.</div>
                                        </td>
                                    </tr>
                                    <td>
                                        <span class="howtoReferBlackHeader">What happens if deal is not on?</span><br />
                                        <span class="howtoReferNormalTxt">If the minimum number of buys is not reached the deal
                                            will be cancelled. You will not be charged but you will also not be able to reap
                                            the benefits of the deal. This is why it is important to spread the word to friends,
                                            family and coworkers to ensure that no one misses out on the opportunity at hand.</span>
                                    </td>
                                </table>
                            </div>
                            <h3>
                                <a href="#">3) I bought the deal, whats next?</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                Congratulations! You’ve purchased your first deal! The hardest part is over. Now
                                                simply follow these next steps:
                                                <br>
                                                1. Wait to receive email notification once the deal has ended<br>
                                                2. Sign in at Tazzling.com<br>
                                                3. Click on the Member Area tab<br>
                                                4. Print your voucher and bring it in store or go green and display it on your smart
                                                phone<br>
                                                5. Enjoy!<br>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">4) What are the benefits of referring my friends onto Tastygo?</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                Thanks for sharing! Not only you'll spread the good words around, but you'll also
                                                earn $10 TastyGo Bucks on your friend's first order! (min $20), which you can apply
                                                towards any purchase in the future.
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">5) What happen if the business closes down after I purchase the voucher?</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                In rare cases if this ever happens please contact us at support@tazzling.com and
                                                we'll do everything we can to make you happy.</div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">6) 100% Money Back Policy? I bought multiple vouchers and I want refunds!</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                No problem! If you are not satisfied with the service you are receiving, you may
                                                request refund for any unused vouchers within 30 days of your purchase, and we’ll
                                                process your refund immediately.
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">7) I like deals! and I would like to buy it as gift and send it to my friends!</a></h3>
                            <div>
                                <table border="0" align="Left">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                1. Login Tastygo
                                                <br>
                                                2. Click member area on the top right hand corner.
                                                <br>
                                                3. Send Gift to your friend, or print it for them
                                                <br>
                                                4. That's it!
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">8) Sounds too good to be true! What's the catch?</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            Read the fine prints carefully before you buy. Some vouchers have time restrictions,
                                            others may have specific rules. If you have any questions, go to our discussion
                                            area, call the business, or contact Tastygo, and we'll have your questions answered.
                                            Tastygo wants you to buy the deal with confidence!
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">9) The experience was horrible, I would never taste any deals again!</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                Contact us immediately for any unpleasant experiences. Was the deal not the same
                                                as what you expected? Were you unable to redeem the voucher? Let us know, and we'll
                                                make things right!</div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">10) Tastygo Reward System</a></h3>
                            <div>
                                <table border="0" align="Left">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                Deal Price x 0.3 = exp you earn (e.g. $19 x 0.3 = 6 exp)<br>
                                                <br>
                                                1 exp – You left a comment (1 per deal)<br>
                                                2 exp – Daily Login</div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                <a href="#">11) Need to Refund an Item?</a></h3>
                            <div>
                                <table border="0" align="center">
                                    <tr>
                                        <td class="howtoReferNormalTxt">
                                            <div align="justify">
                                                Within 30 days of delivery of your shipment, you may return any of the items to
                                                Tazzling.com, for any reason, for a full refund (we'll also compensate the return
                                                shipping cost, if the return is due to an error from our side):
                                                <br>
                                                <br>
                                                If you have never received the product that was promised to be send to you stated
                                                in the fine print, we will try our best to contact the vendor on your behalf, and
                                                follow up the shipping status. If the business fails to provide a proof of delivery,
                                                Tastygo will offer full refund on your purchase.
                                                <br>
                                                <br>
                                                After we've received and processed the returned item, we will start processing your
                                                refund and notify you about it via e-mail. You can expect a refund in the same form
                                                of payment originally used for purchase within 3 to 5 business days after we receive
                                                your return.
                                                <br>
                                                <br>
                                                If you paid for all or part of an item you want to return with our Tastygo Credits,
                                                that part of your refund will be credited to your account, and will be available
                                                for use the next time you place an order at Tazzling.com. Your credits will be always
                                                available and will never expire.
                                                <br>
                                                <br>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
