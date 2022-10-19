<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="New.aspx.cs"
    Inherits="New" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <style>
        .NewText
        {
            float: left;
            font-size: 12px;
            font-weight: bold;
            width: 190px;
            height: auto;
        }
    </style>
    <div style="position: relative;">
        <div style="background-image: url('Images/Cloud.png'); position: absolute; z-index: 1000px;
            left: -40px; margin-top: 150px; height: 33px; width: 88px;">
        </div>
        <div style="background-image: url('Images/Cloud.png'); position: absolute; z-index: 1000px;
            right: -25px; margin-top: 110px; height: 33px; width: 88px;">
        </div>
    </div>
    <div id="BottomCornes" style="position: relative;">
        <div style="background-image: url('Images/New_Left_Corner.png'); position: absolute;
            z-index: 1000px; left: -50px; margin-top: 585px; height: 37px; width: 50px;">
        </div>
        <div style="background-image: url('Images/New_Right_Corner.png'); position: absolute;
            z-index: 1000px; right: -35px; margin-top: 585px; height: 37px; width: 50px;">
        </div>
    </div>
    <div id="TopImage" style="background-image: url('Images/New_Page_BG.png'); height: 618px;
        width: 975px;">
        <div id="InnerText" style="width: 100%; padding-left: 90px; padding-top: 75px; text-align: left;">
            <div id="TextGetIT" class="NewText" style="margin-left: 0px;">
                Check your email, Facebook or Twitter feeds for daily deals on cool local 
                businesses.</div>
            <div id="Text2" class="NewText" style="margin-left: 110px;">
                Groupons are more fun when used with friends. Pass along deals by email or 
                broadcast them to your social networks.</div>
            <div id="Text3" class="NewText" style="margin-left: 145px;">
                Print the voucher or bring it up on your mobile device, then present it at the 
                business to get your deal.</div>
        </div>
        <div style="clear: both; padding-top: 445px;">
            <div style="color: White; font-size: 16px; text-decoration: underline; font-weight: bold;
                float: left;">
                See Todays Deal
            </div>
            <div style="font-size: 16px; font-weight: bold; float: right;">
                <font color="gray">Earn $10 in TastyGo Bucks by inviting friends.</font> <font color="white">
                    Refer a friend now.</font>
            </div>
        </div>
    </div>
    <div id="BottomZone" style="overflow: hidden;">
        <div id="BottomLeft" style="background-color: White; float: left; height: 1000px;
            width: 672px; background-color: White;">
            <div style="margin-left: 5px; margin-right: 5px; overflow: hidden; margin-bottom:15px;">
                <div style="margin-top: 10px; margin-bottom: 40px;">
                    <div style="float: left; font-size: 18px; color: Black;">
                        Recent Deals
                    </div>
                    <div style="float: left; margin-left: 85px;">
                        <img src="Images/Btn_ReferFriend.png" alt="Refer your Friend" /></div>
                </div>
                <div style="clear: both; margin-bottom:15px;">
                    <div style="border: 2px solid #FDA605; margin-bottom: 20px; height: 192px; width: 394px;
                        float: left;">
                        <div style="margin-left: 5px; margin-right: 5px;  margin-top: 5px; font-size: 14px;">
                            $ 15 for $ 30 Worth of Mexican Fare and Drinks at Zapata’s Mexican Restaurant</div>
                        <div style="clear: both; margin-left: 5px;">
                            <div style="float: left; width: 123px;">
                                <div style="background-color: #E6DFE0; height: 50px; width: 123px; text-align: center;">
                                    <div style="font-size: 24px; font-weight: bold; text-align: center; padding-top: 5px;">
                                        90</div>
                                    <div style="margin-top: 5px;">
                                        Tasty Deals Bought</div>
                                </div>
                                <div style="margin-top: 5px; background-color: #FDA605; font-size: 13px; color: White;
                                    font-weight: bold; text-align: center; height: 29px; width: 123px;">
                                    <div style="padding-top: 5px;">
                                        Price : C$15</div>
                                </div>
                                <div style="background-color: #FFD27F; font-size: 13px; color: White; font-weight: bold;
                                    text-align: center; height: 29px; width: 123px;">
                                    <div style="padding-top: 5px;">
                                        Value: C$15</div>
                                </div>
                                <div style="background-color: #FDA605; font-size: 13px; color: White; font-weight: bold;
                                    text-align: center; height: 29px; width: 123px;">
                                    <div style="padding-top: 5px;">
                                        Savings: C$15</div>
                                </div>
                            </div>
                            <div style="float: left; margin-left: 5px; width: 256px;">
                                <img src="Images/New_Image1.jpg" style="border: mediam none; height: 141px; width: 256px;"
                                    alt="" /></div>
                        </div>
                    </div>
                    <div style="float: left; font-size: 12px; font-weight: bold; text-align: left; margin-left: 10px;
                        width: 200px;">
                        TastyGo negotiates huge discout susually 50-90% off-with popular businesses. We 
                        send the deals to thousands of subscribers in our free daily email, and we send 
                        the businesses a ton of new customers. That&#39;s the Tastygo magic.
                    </div>
                    <div style="float: left; margin-left: 10px; margin-top: 15px;">
                        <img src="Images/Btn_SeeRecentDeals.png" /></div>
                </div>
            </div>
            <div style="border-top: 2px solid #E9E9E9; margin-left: 20px; margin-right: 20px; padding-bottom:10px; padding-top:20px;">
            </div>
            <div style="margin-top: 20px; margin-left: 10px; margin-bottom: 10px; clear: both;
                overflow: hidden;">
                <div style="float: left">
                    <img src="Images/New_Go.png" />
                </div>
                <div style="float: left; margin-left: 20px;">
                    <div style="font-size: 20px; padding-bottom:10px; font-weight: bold; text-align: left; margin-bottom: 5px;">
                        Personalise Your Dreams
                    </div>
                    <div style="font-size: 12px; font-weight: bold; text-align: left; width: 450px; margin-bottom:15px;">
                        Tell us a little about yourself—starting with your zip code, gender and age—and 
                        we&#39;ll make sure you see the deals most relevant to you. And don&#39;t worry, you&#39;ll 
                        still be able to discover cool new stuff with your city&#39;s featured deal of the 
                        day.
                        <br />
                        <font style="color: #00AEFF; font-weight: bold;">Read More...</font>
                    </div>
                </div>
            </div>
            <div style="border-top: 2px solid #E9E9E9; margin-left: 20px; margin-right: 20px; padding-bottom:10px; padding-top:20px;">
            </div>
            <div style="margin-top: 20px; margin-left: 10px; margin-bottom: 10px; clear: both;
                overflow: hidden;">
                <div style="float: left">
                    <img src="Images/New_Ifone.png" />
                </div>
                <div style="float: left; margin-left: 35px;">
                    <div style="font-size: 20px; font-weight: bold; padding-bottom:10px; text-align: left; margin-bottom: 5px;">
                        Buy &amp; Redeem Tastygo on Your Mobile Device
                    </div>
                    <div style="font-size: 12px; font-weight: bold; text-align: left; width: 450px; margin-bottom:15px;">
                        Get any deal with the touch of a button, keep track of your Groupons by 
                        location, date and expiration, and redeem them any time you want without killing 
                        trees. Just download our iPhone or Android mobile apps.
                        <br />
                        <font style="color: #00AEFF; font-weight: bold;">Read More...</font>
                    </div>
                </div>
            </div>
            <div style="border-top: 2px solid #E9E9E9; margin-left: 20px; margin-right: 20px; padding-bottom:10px; padding-top:20px;">
            </div>
            <div style="margin-top: 20px; margin-left: 10px; margin-bottom: 10px; clear: both;
                overflow: hidden;">
                <div style="float: left">
                    <img src="Images/New_Gift.png" />
                </div>
                <div style="float: left; margin-left: 35px;">
                    <div style="font-size: 20px; font-weight: bold; text-align: left; margin-bottom: 10px;">
                        Tastygo Make Great Gifts
                    </div>
                    <div style="font-size: 12px; font-weight: bold; padding-bottom:10px; text-align: left; width: 450px;">
                        Tell us a little about yourself—starting with your zip code, gender and age—and 
                        we&#39;ll make sure you see the deals most relevant to you. And don&#39;t worry, you&#39;ll 
                        still be able to discover cool new stuff with your city&#39;s featured deal of the 
                        day.
                        <br />
                        <font style="color: #00AEFF; font-weight: bold;">Read More...</font>
                    </div>
                </div>
            </div>
            <div style="border-top: 2px solid #E9E9E9; margin-left: 20px; margin-right: 20px; padding-bottom:10px; padding-top:20px;">
            </div>
            <div style="margin-top: 20px; margin-left: 10px; margin-bottom: 10px; clear: both;
                overflow: hidden;">
                <div style="float: left">
                    <img src="Images/New_Customers.png" />
                </div>
                <div style="float: left; margin-left: 35px;">
                    <div style="font-size: 20px; font-weight: bold; text-align: left; margin-bottom: 10px;">
                        Get Your Business on TastyGo
                    </div>
                    <div style="font-size: 12px; font-weight: bold; text-align: left; width: 450px; margin-bottom:15px;">
                        Tell us a little about yourself—starting with your zip code, gender and age—and 
                        we&#39;ll make sure you see the deals most relevant to you. And don&#39;t worry, you&#39;ll 
                        still be able to discover cool new stuff with your city&#39;s featured deal of the 
                        day.
                        <br />
                        <font style="color: #00AEFF; font-weight: bold;">Read More...</font>
                    </div>
                </div>
            </div>
        </div>
        <div id="BottomRight" style="float: left; width: 303px; height: 1000px; background-color: white;">
            <div style="height: 195px; width: 302px;">
                <img src="Images/New_WallStreet.jpg" alt="" /></div>
            <div style="height: 221px; width: 302px; background-image: url('Images/New_Promise.png');
                background-repeat: no-repeat;">
                <div style="padding-left: 110px; padding-top: 20px;">
                    <font style="font-size: 14px; font-weight: bold; color: white">The</font></div>
                <div style="padding-left: 110px;">
                    <font style="font-size: 23px; font-weight: bold; color: white">tastygo</font></div>
                <div style="padding-left: 110px;">
                    <font style="font-size: 16px; font-weight: bold; color: #4F4F4F">Promise</font></div>
                <div style="float: left; padding-left:15px; padding-top:20px; font-size: 12px; color: #4F4F4F; font-weight: bold;">
                    Nothing is more important to us than treating our customers well.
                    <br>
                    <br>
                    If you ever feel like TastyGo let you down, give us a call and we&#39;ll return your 
                    purchase — simple as that.
                </div>
            </div>
            
            
            <div style="background-color:#F3F3F3; width:302px; height:256px;">
            <div style="padding-left:15px">
            <div style="font-weight:bold; font-size:17px; color:Black; padding-top:10px; padding-bottom:10px;">
                Happy Tastygo Customers</div>
             <img src="Images/New_HappyCustomer.jpg" alt="">
            </div>
            </div>
            
            
            <div style="background-color:#E3E3E3; width:302px; height:630px;">
             <div style="padding-left:15px;">
            <div style="font-weight:bold; font-size:17px; color:Black; padding-top:10px; padding-bottom:10px;">
                How Tastygo Works</div>
            <div style="overflow:hidden; padding-bottom:15px;">
            
            <div style="float:left; background-image:url('Images/New_KnowIt.png'); background-repeat:no-repeat; height:80px; width:74px;"></div>
           
            <div style="padding-left:80px; padding-right:5px;">
            <div style="float:left; background-image:url('Images/New_KnowIt_Text.png'); background-repeat:no-repeat; height:23px; width:96px;"></div>
            <div style="float:left; font-size:12px; color:Black;">Subscribe our mailing list, 
                and receive cool daily deals around your neighbourhood.</div>
            </div>
            </div>
            
            
            
            
            
            <div style="overflow:hidden; padding-bottom:15px;">
            
            <div style="float:left; background-image:url('Images/New_ShareIt.png'); background-repeat:no-repeat; height:80px; width:74px;"></div>
           
            <div style="padding-left:80px; padding-right:5px;">
            <div style="float:left; background-image:url('Images/New_ShareIt_Text.png'); background-repeat:no-repeat; height:23px; width:96px;"></div>
            <div style="float:left; font-size:12px; color:Black;">Share the deal with friends by 
                using email, twitter, or facebook and tell them to buy with you.</div>
            </div>
            </div>
            
            
            
            
             <div style="overflow:hidden; padding-bottom:15px;">
            
            <div style="float:left; background-image:url('Images/New_TastIt.png'); background-repeat:no-repeat; height:80px; width:74px;"></div>
           
            <div style="padding-left:80px; padding-right:5px;">
            <div style="float:left; background-image:url('Images/New_TastIt_Text.png'); background-repeat:no-repeat; height:23px; width:96px;"></div>
            <div style="float:left; font-size:12px; color:Black;">Print out your deal code, and 
                go grab your deal! Remember to bring your friends!</div>
            </div>
            </div>
            
            
            </div>
            
            </div>
        </div>
    </div>
    <div style="margin-top: 0px; height: 8px; width: 976px; background-image: url(Images/New_PanelBottomArea.png);
        background-position: bottom; background-repeat: repeat-x;">
    </div>
</asp:Content>
