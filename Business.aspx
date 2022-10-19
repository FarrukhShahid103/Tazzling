<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="Business.aspx.cs"
    Inherits="Business" Title="$7 for $15 worth of Authentic South Indian Cuisine at Krishna’s Dosa Grill" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="UserControls/Discussion/ctrlDiscussion.ascx" TagName="ctrlDiscussion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <link href="CSS/fonts.css" rel="stylesheet" type="text/css" />
    <link href="CSS/temp.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function RecoverPassword() {
            var isValidated = true;
            // validate Email

            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
            if ($("#ctl00_ContentPlaceHolder1_txtEmailAddress").val() == '' || !filter.test($("#ctl00_ContentPlaceHolder1_txtEmailAddress").val())) {
                $("#ctl00_ContentPlaceHolder1_txtEmailAddress").removeClass("TextBox").addClass("TextBoxError");
                isValidated = false;
            }
            else {

                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?forgetpassword=" + $("#ctl00_ContentPlaceHolder1_txtEmailAddress").val(),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (msg == "Please enter a valid Email ID") {
                            MessegeArea(msg, 'Error');
                        }
                        else if (msg == "Account information sent to your email address.") {
                            MessegeArea(msg, 'success');
                        }

                        else if (msg == "Email sending failed. Please try again.") {
                            MessegeArea(msg, 'Error');
                        }

                        else if (msg == "Account information sent to your email address.") {
                            MessegeArea(msg, 'success');
                        }

                        else if (msg == "Email sending failed. Please try again.") {
                            MessegeArea(msg, 'Error');
                        }

                        else if (msg == "This email address does not exist.") {
                            MessegeArea(msg, 'Error');
                        }
                        else {
                            MessegeArea(msg, 'Error');
                        }
                    }
                });

            }

            return false;
        }
        function pageLoad() {
            $('#ctl00_ContentPlaceHolder1_txtEmail').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtPwd').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtEmailAddress').tipsy({ gravity: 's' });

            $("#ctl00_ContentPlaceHolder1_txtEmailAddress").keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    ForgotPassword();
                    RecoverPassword();
                    //alert('Enter Key Pressed');
                }
            });



            $('#ctl00_ContentPlaceHolder1_btnSignin').click(function () {
                var isValidated = true;
                // validate Email

                var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
                if ($("#ctl00_ContentPlaceHolder1_txtEmail").val() == '' || !filter.test($("#ctl00_ContentPlaceHolder1_txtEmail").val())) {
                    $("#ctl00_ContentPlaceHolder1_txtEmail").removeClass("TextBox").addClass("TextBoxError");
                    isValidated = false;
                }

                //validate Password                
                if ($("#ctl00_ContentPlaceHolder1_txtPwd").val() == '') {
                    $("#ctl00_ContentPlaceHolder1_txtPwd").removeClass("TextBox").addClass("TextBoxError");
                    isValidated = false;
                }
                if (isValidated) {
                }
                else {
                    return false;
                }
            });

            $('#ctl00_ContentPlaceHolder1_PasswordRecover1_btnSubmit').click(function () {
                var isValidated = true;
                // validate Email

                var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
                if ($("#ctl00_ContentPlaceHolder1_PasswordRecover1_txtEmail").val() == '' || !filter.test($("#ctl00_ContentPlaceHolder1_PasswordRecover1_txtEmail").val())) {
                    $("#ctl00_ContentPlaceHolder1_PasswordRecover1_txtEmail").removeClass("TextBox").addClass("TextBoxError");
                    isValidated = false;
                }
                if (isValidated) {
                }
                else {
                    return false;
                }
            });
        }
    </script>
    <script type="text/javascript">
        function ForgotPassword() {
            $("#ctl00_ContentPlaceHolder1_txtEmail").removeClass("TextBoxError").addClass("TextBox");
            $("#ctl00_ContentPlaceHolder1_txtPwd").removeClass("TextBoxError").addClass("TextBox");
            $(".PageHeading").html("Password Recovery:");
            $("#SigninDiv").slideUp("slow");
            $("#ForgotPasswordDiv").slideDown("slow");

        }

        function Signin() {
            $("#ctl00_ContentPlaceHolder1_txtEmail").removeClass("TextBoxError").addClass("TextBox");
            $("#ctl00_ContentPlaceHolder1_txtPwd").removeClass("TextBoxError").addClass("TextBox");
            $(".PageHeading").html("Business Login:");
            $("#SigninDiv").slideDown("slow");
            $("#ForgotPasswordDiv").slideUp("slow");

        }


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.DealImage a').hover(function () {
                $(this).find('strong').stop().fadeTo('normal', 1);
            }, function () {
                $(this).find('strong').stop().fadeTo('normal', 0);
            });
        });
    </script>
    <script type='text/javascript'>
        var Site;
        function CountDownTimer(Year, Month, Day, Hour, Minute, DivID, SiteURL) {
            Site = SiteURL;
            var austDay = new Date();
            var newYear = new Date();
            austDay = new Date(Year, Month, Day, Hour, Minute, 0);
            $("#" + DivID).countdown({ until: austDay, compact: true, serverSync: serverTime });
        }

        function serverTime() {
            var time = null;
            $.ajax({ url: Site,
                async: false, dataType: 'text',
                success: function (text) {
                    time = new Date(text);
                }, error: function (http, message, exc) {
                    time = new Date();
                }
            });
            return time;
        }
                    
                   
                    
    </script>
    <style type="text/css">
        @import "CSS/jquery.countdown.css";
        #defaultCountdown
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript" src="JS/jquery.countdown.js"></script>
    <asp:HiddenField ID="hfCurrentDealId" runat="server" />
    <div style="clear: both; padding-top: 20px; height: 160px;width:980px;">
     
        <div style="overflow: hidden; padding-top: 20px; clear: both;">
            <div style="background-color: #ffffff; width: 100%; height: 100px; ">
                <div style="padding-top: 10px">
                    <asp:Image ID="Image1" runat="server" style="border-width: 0;height: 57px;padding-left: 20px;padding-top: 10px;width: 930px;" ImageUrl="~/Images/temp-images/getpaid.png" /></div>
            </div>
        </div>
    </div>
    <div style="width:980px;">
    <div style="background: none repeat scroll 0 0 #FFFFFF; float: left; min-height: 500px;
        padding: 20px 20px 60px; width: 682px;">
        <div style="background-color: #ffffff; width: 680px; padding-bottom: 10px; float: left;
            clear: both;">
            <div>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/temp-images/video1.png" />
            </div>
        </div>
        <div>
            <h4>
                Businesses that get a 1 day feature on Tastygo receive tons of customers, literally
                overnight!</h4>
        </div>
        <div style="color: #FF00A4">
            <h2>
                Why Tastygo?</h2>
        </div>
        <div>
            <div style="float: left; margin: -7px 0 0 -13px;">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/temp-images/icon-customers.png" />
            </div>
            <div class="fontStyle" style="margin-left: 85px; margin-top: 5px; color: #00AFEF;">
                Customers, Customers & More Customers</div>
            <div style="font-size: small; font-family: Verdana; margin-left: 85px; margin-right: 47px;
                margin-top: 9px;">
                Did we say customers? Our Tastygo subscribers are looking for a reason to check
                out new things to buy, eat, see, and do in their city. We work with local businesses
                like you and provide our audience the chance to experience your business at a discount.
                The customer has a great reason to check your business out, and you'll have the
                chance to receive new customers and earn their loyalty. Can your business benefit
                from new customers ready to spend and refer others? Work with Tastygo and we'll
                send these very same customers your way, literally overnight!</div>
        </div>
        <div>
            <div style="float: left; margin: -7px 0 0 -13px;">
                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/temp-images/icon-exposure.png" />
            </div>
            <div class="fontStyle" style="margin-left: 85px; margin-top: 5px; color: #00AFEF;">
                Huge Exposure & Great Word of Mouth</div>
            <div style="font-size: small; font-family: Verdana; margin-left: 85px; margin-right: 47px;
                margin-top: 9px;">
                Did we say customers? Our Tastygo subscribers are looking for a reason to check
                out new things to buy, eat, see, and do in their city. We work with local businesses
                like you and provide our audience the chance to experience your business at a discount.
                The customer has a great reason to check your business out, and you'll have the
                chance to receive new customers and earn their loyalty. Can your business benefit
                from new customers ready to spend and refer others? Work with Tastygo and we'll
                send these very same customers your way, literally overnight!</div>
        </div>
        <div>
            <div style="float: left; margin: -7px 0 0 -13px;">
                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/temp-images/icon-spotlight.png" />
            </div>
            <div class="fontStyle" style="margin-left: 85px; margin-top: 5px; color: #00AFEF;">
                Spotlight - All Eyes on Your Business</div>
            <div style="font-size: small; font-family: Verdana; margin-left: 85px; margin-right: 47px;
                margin-top: 9px;">
                Did we say customers? Our Tastygo subscribers are looking for a reason to check
                out new things to buy, eat, see, and do in their city. We work with local businesses
                like you and provide our audience the chance to experience your business at a discount.
                The customer has a great reason to check your business out, and you'll have the
                chance to receive new customers and earn their loyalty. Can your business benefit
                from new customers ready to spend and refer others? Work with Tastygo and we'll
                send these very same customers your way, literally overnight!</div>
        </div>
        <div style="color: #FF00A4">
            <h2>
                Customers & More Customers</h2>
        </div>
        <div>
            <div style="float: left; margin: -7px 0 0 -13px;">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/temp-images/icon-dollar.png" />
            </div>
            <div class="fontStyle" style="margin-left: 85px; margin-top: 5px; color: #00AFEF;">
                SocialShopper Makes Dollars & Sense</div>
            <div style="font-size: small; font-family: Verdana; margin-left: 85px; margin-right: 47px;
                margin-top: 9px;">
                Did we say customers? Our Tastygo subscribers are looking for a reason to check
                out new things to buy, eat, see, and do in their city. We work with local businesses
                like you and provide our audience the chance to experience your business at a discount.
                The customer has a great reason to check your business out, and you'll have the
                chance to receive new customers and earn their loyalty. Can your business benefit
                from new customers ready to spend and refer others? Work with Tastygo and we'll
                send these very same customers your way, literally overnight!</div>
        </div>
        <div>
            <div>
                <div style="float: left; margin: -7px 0 0 -13px;">
                    <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/temp-images/icon-risk.png" />
                </div>
                <div class="fontStyle" style="margin-left: 85px; margin-top: 5px; color: #00AFEF;">
                    No Risky Business Here</div>
                <div style="font-size: small; font-family: Verdana; margin-left: 85px; margin-right: 47px;
                    margin-top: 9px;">
                    Did we say customers? Our Tastygo subscribers are looking for a reason to check
                    out new things to buy, eat, see, and do in their city. We work with local businesses
                    like you and provide our audience the chance to experience your business at a discount.
                    The customer has a great reason to check your business out, and you'll have the
                    chance to receive new customers and earn their loyalty. Can your business benefit
                    from new customers ready to spend and refer others? Work with Tastygo and we'll
                    send these very same customers your way, literally overnight!</div>
            </div>
            <div style="color: #FF00A4">
                <h2>
                    Comparison? There is no comparison!</h2>
            </div>
            <div>
                <div>
                    <div style="float: left; margin: -7px 0 0 -13px;">
                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/temp-images/icon-balance.png" />
                    </div>
                    <div class="fontStyle" style="margin-left: 85px; margin-top: 5px; color: #00AFEF;">
                        Leave Traditional Marketing Behind. Join the SocialShopper Revolution</div>
                    <div style="font-size: small; font-family: Verdana; margin-left: 85px; margin-right: 47px;
                        margin-top: 9px;">
                        Did we say customers? Our Tastygo subscribers are looking for a reason to check
                        out new things to buy, eat, see, and do in their city. We work with local businesses
                        like you and provide our audience the chance to experience your business at a discount.
                        The customer has a great reason to check your business out, and you'll have the
                        chance to receive new customers and earn their loyalty. Can your business benefit
                        from new customers ready to spend and refer others? Work with Tastygo and we'll
                        send these very same customers your way, literally overnight!</div>
                </div>
            </div>
            <table width="580" cellspacing="0" cellpadding="0" border="0" class="benefits" style="font-size: 12px;
                margin: 10px 0px 20px 80px; background: url('images/benefitsgrad.png') no-repeat scroll 0pt 15px transparent;">
                <tbody>
                    <tr style="margin: 10px 0px 20px 80px;">
                        <td width="40%" height="55" style="border: medium none;">
                            <strong style="font-size: 13px;">&nbsp;&nbsp;Does your business want to</strong>
                        </td>
                        <td align="center" width="15%" height="55" style="border: medium none;">
                            <strong style="font-size: 13px;">Tastygo</strong>
                        </td>
                        <td align="center" width="15%" height="55" style="border: medium none;">
                            <strong style="font-size: 13px;">Print</strong>
                        </td>
                        <td align="center" width="15%" height="55" style="border: medium none;">
                            <strong style="font-size: 13px;">TV/Radio</strong>
                        </td>
                        <td align="center" width="15%" style="border: medium none;">
                            <strong style="font-size: 13px;">Online/<br>
                                Email/Other</strong>
                        </td>
                    </tr>
                    <tr>
                        <td height="55">
                            Receive new customers guaranteed?
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/tick.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                    </tr>
                    <tr>
                        <td height="55">
                            Advertise to subscribers who are smart, have money to spend, and growing daily?
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/tick.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                    </tr>
                    <tr>
                        <td height="55">
                            Spend money only on actual customers that your ad brings in, risk free?
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/tick.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                    </tr>
                    <tr>
                        <td height="55">
                            Have the spotlight all to yourself and be the only business featured?
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/tick.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                    </tr>
                    <tr>
                        <td height="55">
                            Receive endless amount of referrals via word of mouth and social media?
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/tick.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center" height="55">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                        <td align="center">
                            <img width="24" height="27" src="images/temp-images/cross.png">
                        </td>
                    </tr>
                </tbody>
            </table>
            <div>
                <h4 style="color: #58595B; font-family: Arial,Helvetica,sans-serif; font-size: 21px;
                    font-weight: bold; letter-spacing: -1px; margin: 0; padding: 0 0 20px;">
                    Take Advantage of the Tastygo Revolution!</h4>
            </div>
            <div>
                <h1 style="padding-top: 20px; color: #FF00A4; font-family: Arial,Helvetica,sans-serif;
                    font-size: 20px; font-weight: bold; margin: 0; padding-bottom: 20px;">
                    Get Featured
                </h1>
            </div>
            <div style="float: left; width: 315px; height: 320px">
                <div style="float: left; width: 315px; height: 20px; margin-top: 20px">
                    <div style="float: left;">
                        First name<span style="color: rgb(255, 0, 255);">*</span></div>
                    <div style="float: right">
                        <input type="text" style="width: 200px; height: 17px" class="TextBox" />
                    </div>
                </div>
                <div style="float: left; width: 315px; height: 20px; margin-top: 20px">
                    <div style="float: left;">
                        Last name<span style="color: rgb(255, 0, 255);">*</span></div>
                    <div style="float: right">
                        <input type="text" style="width: 200px; height: 17px" class="TextBox" />
                    </div>
                </div>
                <div style="float: left; width: 315px; height: 20px; margin-top: 20px">
                    <div style="float: left;">
                        Business name<span style="color: rgb(255, 0, 255);">*</span>
                    </div>
                    <div style="float: right">
                        <input type="text" style="width: 200px; height: 17px" class="TextBox" />
                    </div>
                </div>
                <div style="float: left; width: 315px; height: 20px; margin-top: 20px">
                    <div style="float: left;">
                        Website</div>
                    <div style="float: right">
                        <input type="text" style="width: 200px; height: 17px" class="TextBox" />
                    </div>
                </div>
                <div style="float: left; width: 315px; height: 20px; margin-top: 20px">
                    <div style="float: left;">
                        Email<span style="color: rgb(255, 0, 255);">*</span>
                    </div>
                    <div style="float: right">
                        <input type="text" style="width: 200px; height: 17px" class="TextBox" />
                    </div>
                </div>
                <div style="float: left; width: 315px; height: 30px; margin-top: 20px">
                    <div style="float: left;">
                        Phone number<span style="color: rgb(255, 0, 255);">*</span>
                    </div>
                    <div style="float: right">
                        <input type="text" style="width: 200px; height: 17px" class="TextBox" />
                    </div>
                </div>
                <div style="float: left; width: 315px; height: 30px; margin-top: 20px">
                    <div style="float: left;">
                        Phone number<span style="color: rgb(255, 0, 255);">*</span>
                    </div>
                    <div style="float: right;">
                        <select style="width: 200px;" tabindex="6" class="featuredfields" name="cid">
                            <option value="">--- click to choose ---</option>
                            <option value="5">Atlanta</option>
                            <option value="6">Austin</option>
                            <option value="7">Baltimore</option>
                            <option value="8">Boston</option>
                            <option value="9">Brooklyn</option>
                            <option value="4">Calgary</option>
                            <option value="10">Chicago</option>
                            <option value="11">Cleveland</option>
                            <option value="12">Dallas</option>
                            <option value="13">Denver</option>
                            <option value="52">Edmonton</option>
                            <option value="14">Houston</option>
                            <option value="15">Indianapolis</option>
                            <option value="16">Jacksonville</option>
                            <option value="17">Las Vegas</option>
                            <option value="18">Long Island</option>
                            <option value="19">Los Angeles</option>
                            <option value="20">Memphis</option>
                            <option value="21">Miami</option>
                            <option value="22">Minneapolis | St Paul</option>
                            <option value="2">Montreal</option>
                            <option value="23">New Orleans</option>
                            <option value="24">New York</option>
                            <option value="62">Orange County</option>
                            <option value="26">Orlando</option>
                            <option value="27">Philadelphia</option>
                            <option value="28">Phoenix</option>
                            <option value="29">Pittsburgh</option>
                            <option value="30">Portland</option>
                            <option value="31">Raleigh | Durham</option>
                            <option value="32">Sacramento</option>
                            <option value="33">San Antonio</option>
                            <option value="34">San Diego</option>
                            <option value="35">San Francisco</option>
                            <option value="36">San Jose</option>
                            <option value="37">Seattle</option>
                            <option value="38">St Louis</option>
                            <option value="39">Tampa</option>
                            <option value="1">Toronto</option>
                            <option value="3">Vancouver</option>
                            <option value="40">Washington DC</option>
                        </select>
                    </div>
                </div>
            </div>
            <div style="float: right; height: 368px; margin: 20px 22px 0 0; width: 315px;">
                <div>
                    Message (optional)</div>
                <div>
                    <textarea tabindex="7" class="featuredfields" style="width: 280px; height: 158px;"
                        rows="5" cols="45" name="comment">
            </textarea>
                </div>
                <div style="width: 295px; height: 45px;">
                    <div style="float: right; margin-top: 13px">
                        <input type="button" id="Button1" value="Submit" class="button" onclick="javascript:MessegeArea('Under Construction This page is currently under construction<br> please try again later.' , 'error')" /></div>
                </div>
            </div>
            <%--   End OF Left Side Of Body --%>
            <%--      Right Side Of Body--%>
        </div>
    </div>
    <div style="position: relative; float:right; width: 234px;" class="right">
        <div style=" background-color: White;clear: both;float: left;margin-bottom: 20px;padding-bottom: 20px;width: 100%;">
            <center>
                <div style="width: 180px;" class="PageHeading">
                    Business Login:
                </div>
            </center>
            <asp:UpdatePanel ID="udpnl" runat="server">
                <ContentTemplate>
                    <div style="margin-right: 15px;">
                        <div style="margin-top: 30px;">
                            <div style="float: right;">
                                <asp:Panel ID="PagePnl" runat="server" DefaultButton="btnSignin">
                                    <div id="SigninDiv">
                                        <div class="ItemHiding">
                                            Email Address
                                        </div>
                                        <div style="margin-bottom: 5px;">
                                            <asp:TextBox Width="200px" onfocus="this.className='TextBox'" title="Enter your email address"
                                                CssClass="TextBox" ID="txtEmail" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="ItemHiding">
                                            Password
                                        </div>
                                        <div style="margin-bottom: 5px;">
                                            <asp:TextBox Width="200px" onfocus="this.className='TextBox'" title="Enter your password"
                                                CssClass="TextBox" TextMode="Password" ID="txtPwd" runat="server"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: 10px;">
                                            <a href="javascript:ForgotPassword();" class="OrangeLink">Forgot password?</a>
                                        </div>
                                        <div style="margin-top: 20px;">
                                            <asp:Button ID="btnSignin" OnClick="btnSignin_Click" runat="server" Text="Sign in"
                                                CssClass="button big primary" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div id="ForgotPasswordDiv" style="display: none;">
                                    <div class="ItemHiding">
                                        Email Address
                                    </div>
                                    <div style="margin-bottom: 5px;">
                                        <asp:TextBox Width="200px" onfocus="this.className='TextBox'" title="Enter your email address to recover your password."
                                            CssClass="TextBox" ID="txtEmailAddress" runat="server"></asp:TextBox>
                                    </div>
                                    <div style="margin-top: 10px;">
                                        <a href="javascript:Signin();" class="OrangeLink">Sign in?</a>
                                    </div>
                                    <div style="margin-top: 20px;">
                                        <asp:Button ID="BtnFprgotPassword" OnClientClick="return RecoverPassword();" runat="server"
                                            Text="Recover Password" CssClass="button big primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="margin-top: 10px;">
            <div style="background-color: White; clear: both; padding:10px;">
                <div style="font-weight: bold; font-size: 17px;">
                    Business FAQ</div>
                <div style="clear:both;">
                    <p>
                        <strong class="blue">What does it cost to be featured on Tastygo?</strong><br />
                        Tastygo is a risk free solution to businesses and costs you nothing up front or
                        out of pocket, unlike other marketing methods such as (print ads, radio spots, TV
                        commercials, etc.) which ask you to pay upfront for advertising and don't guarantee
                        you any customers.</p>
                    <p>
                        <strong class="blue">After featured, how do businesses get paid?</strong>
                        <br />
                        We simply process the sales of your businesses featured deal through Tazzling.com,
                        collect the payment from customers, email them the vouchers, and then send you a
                        check.</p>
                    <p>
                        <strong class="blue">So how does Tastygo make any money?</strong><br />
                        It's actually very simple, we only win if you win. Meaning when you are working
                        with Tastygo you only pay for customers we actually send your way. We only take
                        a % of the revenue from the vouchers we sell on your behalf, and your business gets
                        all of the rest.</p>
                    <p>
                        <strong class="blue">How soon can my business be featured?</strong>
                        <br />
                        Tastygo has a team who decides when each business will get featured based on a few
                        factors from our experience to ensure that your business is featured at the best
                        date in order to get best exposure and response from our subscribers. Rest assured,
                        we keep the lines of communication open and contact you in advance to ensure your
                        business has a heads up notice before we feature you, so that you are fully prepared
                        for the increase of customers which will be sent to your business.</p>
                </div>
            </div>
        </div>
    </div>
    </div>
    <asp:Literal ID="ltCountDown" runat="server"></asp:Literal>
</asp:Content>
