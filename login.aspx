<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CSS/LoginSignup.css" rel="stylesheet" type="text/css" />
    <script src="JS/LoginSignup.js" type="text/javascript"></script>
    <link rel="shortcut icon" href="favicon.ico" />
    <title>Tazzling.com</title>
    <script type="text/javascript">
        function EnterKey(e) {
            if (e.keyCode == 13) {
                DoLogin();
            }
        }

        $(document).ready(function () {
            SetContentHeight();
        });

        $(window).resize(function () {
            SetContentHeight();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toptile">
        <div style="padding-top: 5px; float: left;">
            <a href='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/Default.aspx" %>' style="text-decoration: none;">
                <img src="Images/logosmall.png" alt="" />
            </a>
        </div>
        <div style="margin-left: 15px; z-index: 1111111; float: left; background-color: #cec6bf;
            width: 2px; height: 51px;">
        </div>
    </div>
    <div id="page-wrap">
        <div style="background-color: Black; width: 300px; text-align: left; height: 320px;
            float: left; margin-left: 115px; margin-top: 200px; font-family: Helvetica;">
            <div style="color: #8c9090; font-size: 40px; margin-left: 20px; margin-top: 20px;">
                Tazzling is</div>
            <div style="color: White; font-size: 34px; margin-left: 20px;">
                Everyday</div>
            <div style="color: #bf31ad; font-size: 38px; margin-left: 20px;">
                Design.</div>
            <div style="color: #464848; font-size: 16px; margin-left: 20px; margin-top: 15px;">
                Free Membership.</div>
            <div id="buttonClick" class="showHide" onclick="javascript:AreaSwitching('signUpArea');"
                style="margin: 35px; width: 93px; margin-left: 20px; display: block;">
                <a style="cursor: pointer; text-decoration: none;">
                    <img src="Images/signup-button.png" alt="" />
                </a>
            </div>
            <div class="showHide" onclick="javascript:AreaSwitching('loginArea');" id="IfMamber"
                style="margin: 35px; margin-left: 20px; display: none; color: White; font-family: Helvetica;
                font-size: 12px;">
                Already tazzling member. <a style="cursor: pointer; color: Red; text-decoration: Underline;">
                    Login</a>
            </div>
        </div>
        <div style="float: left; font-family: Helvetica;" class="loginArea">
            <asp:Panel ID="pngLogin" runat="server" DefaultButton="">
                <div id="loginArea" style="margin-left: 20px; margin-top: 20px; clear: both; overflow: hidden;
                    display: block;">
                    <div style="color: White; text-align: left; font-size: 33px;">
                        Member Sign in</div>
                    <div style="font-size: 13px; margin-top: 20px; text-align: left; color: #a0a1a1;">
                        EMAIL</div>
                    <div style="float: left; margin-top: 5px; clear: both; overflow: hidden;">
                        <asp:TextBox onfocus="this.className='loginTextBox'" ID="txtEmail" runat="server"
                            CssClass="loginTextBox" onkeypress="return EnterKey(event)"></asp:TextBox>
                    </div>
                    <div style="font-size: 13px; margin-top: 45px; text-align: left; color: #a0a1a1;">
                        PASSWORD</div>
                    <div style="float: left; margin-top: 5px; clear: both; overflow: hidden;">
                        <asp:TextBox onfocus="this.className='loginTextBox'" TextMode="Password" CssClass="loginTextBox"
                            ID="txtPwd" runat="server" onkeypress="return EnterKey(event)"></asp:TextBox>
                    </div>
                    <div style="margin-top: 20px; float: left; clear: both; overflow: hidden;">
                        <div style="float: left;">
                            <a href="javascript:void(0)" onclick="javascript:DoLogin();">
                                <img src="Images/login-button.png" />
                            </a>
                        </div>
                        <div style="color: White; font-size: 14px; float: left; margin: 5px 5px;">
                            or</div>
                        <div style="float: left; margin-left: 5px;">
                            <img src="Images/facebook-btn.png" />
                        </div>
                    </div>
                    <div onclick="javascript:AreaSwitching('ForgotPassword');" style="color: #888986;
                        font-size: 20px; text-align: left; font-family: Helvetica; text-decoration: underline;
                        cursor: pointer; margin-top: 95px;">
                        Forgot your password?
                    </div>
                </div>
            </asp:Panel>
            <div id="signUpArea" style="margin-left: 20px; margin-top: 20px; clear: both; overflow: hidden;
                display: none;">
                <div style="color: White; text-align: left; font-size: 33px;">
                    Join Today!</div>
                <div style="font-size: 13px; margin-top: 20px; text-align: left; color: #a0a1a1;">
                    EMAIL</div>
                <div style="float: left; margin-top: 5px; clear: both; overflow: hidden;">
                    <asp:TextBox onfocus="this.className='loginTextBox'" ID="TextBox1" runat="server"
                        CssClass="loginTextBox"></asp:TextBox>
                </div>
                <div style="margin-top: 20px; float: left; clear: both; overflow: hidden;">
                    <div style="width: 93px;">
                        <img src="Images/signup-button.png" />
                    </div>
                    <div style="color: White; font-size: 14px; margin: 5px 5px;">
                        or</div>
                    <div style="margin-left: 5px;">
                        <img src="Images/facebook-btn.png" />
                    </div>
                </div>
            </div>
            <div id="ForgotPassword" style="margin-left: 20px; margin-top: 20px; clear: both;
                overflow: hidden; display: none;">
                <div style="color: White; text-align: left; font-size: 33px;">
                    Forgot Password?</div>
                <div style="font-size: 13px; margin-top: 20px; text-align: left; color: #a0a1a1;">
                    EMAIL</div>
                <div style="float: left; margin-top: 5px; clear: both; overflow: hidden;">
                    <asp:TextBox onfocus="this.className='loginTextBox'" ID="txtEmailAddress" runat="server"
                        CssClass="loginTextBox"></asp:TextBox>
                </div>
                <div style="margin-top: 20px; float: left; clear: both; overflow: hidden;">
                    <div style="width: 93px; float: left;">
                        <a href="javascript:void(0)" id="forgot" onclick="javascript:RecoverPassword();">
                            <img src="Images/signup-button.png" />
                        </a>
                    </div>
                    <div style="float: left; padding-top: 8px;">
                        <div style="float: left; padding-left: 5px; color: White; font-size: 14px;">
                            or</div>
                        <div onclick="javascript:ToggleSignup();" style="float: left; padding-left: 5px;
                            color: Red; font-size: 14px; cursor: pointer;">
                            Back to Login
                        </div>
                    </div>
                </div>
            </div>
            <div id="LoadingArea" style="margin-left: 20px; margin-top: 20px; clear: both; overflow: hidden; display:block;">
                <div style="float: left; clear: both;">
                    <div style="color: White; text-align: left; font-size: 33px; text-align: center;">
                        Please Wait...
                    </div>
                    <center>
                        <div style="clear: both; padding-top: 100px;">
                            <img src="images/LoginLoader.gif" />
                        </div>
                    </center>
                </div>
            </div>
            <div id="MessageArea" style="margin-left: 20px; margin-top: 20px; clear: both; overflow: hidden; display: block;">
                <div style="float: right; padding-right: 20px;">
                    <a href="javascript:void(0);" onclick="javascript:CloseMessageArea();">
                        <img id="CloseImage" src="images/CloseMessageArea.png" />
                    </a>
                </div>
                <div style="padding-top: 80px; clear: both;">
                    <div style="float: left; padding-left: 100px;">
                        <img id="ErrorImage" src="images/ErrorIcon.png" />
                    </div>
                    <div id="ErrorMessageLabel" style="color: #F77700; float: left; font-weight: bold;
                        padding-top: 10px; text-align: center; width: 100%;">
                        Invalid username or password.
                    </div>
                </div>
            </div>
        </div>
        <div id="footerWrapper">
            <div id="footer">
                <ul class="footerLinks floatLeft">
                    <li><a target="_blank" href="http://tazzling.com/about-fab/">About</a></li>
                    <li><a target="_blank" href="http://tazzling/index.html">Careers</a></li>
                    <li><a target="_blank" href="http://blog.tazzling.com/">Blog</a></li>
                    <li><a target="_blank" href="http://tazzling.com/help/">Help</a></li>
                    <li><a target="_blank" href="http://tazzling.com/contact-us/">Contact Us</a></li>
                    <li><a target="_blank" href="http://tazzling.com/return-policy/">Return Policy</a></li>
                    <li><a target="_blank" href="http://tazzling.com/shipping-policy/">Shipping</a></li>
                    <li><a target="_blank" href="/terms/">Terms</a></li>
                    <li><a target="_blank" href="/privacy/">Privacy</a></li>
                </ul>
                <span style="margin-right: 0px" class="copyText floatRight">Tazzling.com &copy; 2012</span>
                <ul style="" class="socialLinks floatRight">
                    <li><a style="cursor: pointer; text-decoration: none;">
                        <img width="19px" height="19px" src="Images/Twitter.png" alt="" />
                    </a></li>
                    <li><a style="cursor: pointer; text-decoration: none;">
                        <img width="19px" height="19px" src="Images/FaceBookSignIn.png" alt="" />
                    </a></li>
                </ul>
                <!-- SAL Admin Check# -->
                <!-- SAL Admin Check# -->
            </div>
        </div>
    </div>
    </form>
</body>
</html>
