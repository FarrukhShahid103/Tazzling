<%@ Page Title="Tastygo | Contest Winner" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="affiliate.aspx.cs" Inherits="affiliate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    $(document).ready(function() {

        $("#ctl00_ContentPlaceHolder1_imgBtnAffiliate").click(function(e) {
        var SiginInCheck = $("#ctl00_topView_lblUsername").val();
        if(typeof SiginInCheck  == "undefined")
        {
            e.preventDefault();                        
            $('html, body').animate({scrollTop:0}, 'slow');
            return false;
        }  
        else
        {
        
        }   
        });
    });
    
    </script>

    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                        Make Money with Tastygo
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 980px; background-color: White; min-height: 450px; line-height:20px;">
                    <div style="clear: both; padding: 20px;">
                        <div style="clear: both; font-size: 13px;">
                            Tastygo provides an opportunity for customers who want to make extra cash on the
                            side or those who do affiliate marketing. Whenever you refer a deal to friends,
                            not only are you sharing the great deal but you also have the potential to make
                            money when your friend purchases the deal. There is no limit to how much you can
                            earn, so spread the word!Tastygo provides an opportunity for customers who want
                            to make extra cash on the side or those who do affiliate marketing. Whenever you
                            refer a deal to friends, not only are you sharing the great deal but you also have
                            the potential to make money when your friend purchases the deal. There is no limit
                            to how much you can earn, so spread the word!
                        </div>
                        <div style="clear: both; font-size: 13px; padding: 10px 0px 10px 0px;">
                            This is how it works: if Tastygo has a steak deal for $20 that is worth of $40,
                            you will get paid an estimated $2 to $10 for every customer you refer. That means
                            if you tell 100 people to buy the deal, you'll earn $200 to $1000 just on that deal!
                            So if we have multiple deals in multiple cities, your earnings could turn out to
                            be a HUGE payday! We know there is only a certain amount of people we can reach
                            and that’s why we need your help.
                        </div>
                        <div style="clear: both; font-size: 13px; padding: 10px 0px 10px 0px;">
                            Tastygo's advance affiliate tracker shows all transactions online and you'll be
                            able to access this detailed information with only a button click. Everything is
                            crystal clear, easy, and fair. You will be able to see who you referred (only first
                            name), $x they ordered, and how much you made.
                        </div>
                        <div style="clear: both; font-size: 13px; padding: 10px 0px 10px 0px;">
                            Tastygo provides live personnel to assist all of your referral inquiries. Whether
                            email or live chat, Tastygo fulfills your referral business with the click of a
                            button!
                        </div>
                        <div style="clear: both; font-size: 15px; font-weight:bold; padding: 10px 0px 5px 0px; color: #E25102;">
                           • Earn up to 40% based on sales commissions!
                        </div>
                        <div style="clear: both; font-size: 15px; font-weight:bold; padding: 0px 0px 5px 0px; color: #E25102;">
                           • Get detailed account reporting for traffic, sales, and commission!
                        </div>
                        <div style="clear: both; font-size: 15px; font-weight:bold; padding: 0px 0px 20px 0px; color: #E25102;">
                           • To start your referral business, simply sign up free account today!
                        </div>
                        <div style="clear: both; padding: 10px 0px 5px 0px;" align="center">
                            <a id="HL" href="affiliateBanners.aspx" class="button big primary" style="padding: 15px">Get Started</a>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>
