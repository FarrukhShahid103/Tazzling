<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="login2.aspx.cs"
    Inherits="login2" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        FB.init({
            appId: '160996503945227',
            oauth: true,
            status: true,
            cookie: true,
            xfbml: true
        });
        FB.Event.subscribe('auth.login', function (response) {

            var ACC_Tokken = response.authResponse.accessToken;



            var c_login = 'tastygoLogin';
            c_login_start = -1;

            if (document.cookie.length > 0) {
                c_login_start = document.cookie.indexOf(c_login + "=");
            }
            if (c_login_start == -1) {
                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?FBLogin=" + ACC_Tokken,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {

                        if (msg == "true") {
                            window.location.href = "Default.aspx";
                        }


                    }

                });

            }

            //window.location.reload();
        });


        function login() {
            FB.login(function (response) {
                if (response.authResponse) {

                    var ACC_Tokken = response.authResponse.accessToken;



                    $.ajax({
                        type: "POST",
                        url: "getStateLocalTime.aspx?FBLogin=" + ACC_Tokken,
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        cache: false,
                        success: function (msg) {

                            if (msg == "true") {
                                window.location.href = "Default.aspx";
                            }


                        }

                    });




                    // window.location.reload();        
                } else {
                    // user is not logged in
                }
            }, { scope: 'read_stream,publish_stream,offline_access,email' });
        }
    </script>

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
                        if (msg == "Account information sent to your email address.") {
                            MessegeArea(msg, 'success');
                        }
                        else if (msg == "Account information sent to your email address.") {
                            MessegeArea(msg, 'success');
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
            $(".PageTopText").html("Forgot Password");
            $(".PageHeading").html("Password Recovery:");
            $("#SigninDiv").slideUp("slow");
            $("#ForgotPasswordDiv").slideDown("slow");

        }

        function Signin() {
            $("#ctl00_ContentPlaceHolder1_txtEmail").removeClass("TextBoxError").addClass("TextBox");
            $("#ctl00_ContentPlaceHolder1_txtPwd").removeClass("TextBoxError").addClass("TextBox");
            $(".PageTopText").html("Sign in");
            $(".PageHeading").html("Login:");
            $("#SigninDiv").slideDown("slow");
            $("#ForgotPasswordDiv").slideUp("slow");

        }


    </script>

    <div>
        <asp:UpdatePanel ID="udpnl" runat="server">
            <ContentTemplate>
                <div style="clear: both; padding-top: 20px">
                    <div class="DetailPageTopDiv">
                        <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                            <div class="PageTopText" style="float: left;">
                                Sign in
                            </div>
                        </div>
                    </div>
                                                      
                    <div class="DetailPage2ndDiv">
                        <div style="float: left; width: 980px; background-color: White; min-height: 450px;">
                            <center>
                                <div style="width: 930px !important;" class="PageHeading">
                                    Login:
                                </div>
                            </center>
                            <div style="width: 660px; margin-left: 15px;">
                                <div style="margin-top: 30px;">
                                    <div style="float: left; width: 330px;">
                                        <asp:Panel ID="PagePnl" runat="server" DefaultButton="btnSignin">
                                            <div id="SigninDiv">
                                                <div class="ItemHiding" style="color:#5F5F5F;">
                                                    Email Address
                                                </div>
                                                <div style="margin-bottom: 5px;">
                                                    <asp:TextBox onfocus="this.className='TextBox'" title="Enter your email address"
                                                        CssClass="TextBox" ID="txtEmail" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="ItemHiding" style="color:#5F5F5F;">
                                                    Password
                                                </div>
                                                <div style="margin-bottom: 5px;">
                                                    <asp:TextBox onfocus="this.className='TextBox'" title="Enter your password" CssClass="TextBox"
                                                        TextMode="Password" ID="txtPwd" runat="server"></asp:TextBox>
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
                                            <div class="ItemHiding" style="color:#5F5F5F;">
                                                Email Address
                                            </div>
                                            <div style="margin-bottom: 5px;">
                                                <asp:TextBox onfocus="this.className='TextBox'" title="Enter your email address to recover your password."
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
                                        <div style="margin-top: 40px;">
                                            <div class="ItemHiding" style="float: left; margin-right: 2px;color:#5F5F5F;">
                                                Dont have account yet?</div>
                                            <div style="float: left; line-height: 25px; padding-left: 5px;">
                                                <a href="signup.aspx" class="OrangeLink">Click here to register</a></div>
                                        </div>
                                    </div>
                                    <div style="float: right; width: 330px;">
                                        <div class="ItemHiding">
                                            If you have Facebook Account</div>
                                        <div style="margin-left: -5px;">
                                            <a href="javascript:login();">
                                                <img src="Images/BtnConnectWithFacebook.png" /></a></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
