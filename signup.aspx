<%@ Page Title="" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        FB.init({ appId: '160996503945227', oauth: true, status: true, cookie: true, xfbml: true });
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
        function pageLoad() {
                        
            $('#ctl00_ContentPlaceHolder1_txtFullName').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtLastName').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtUsernameSignUp').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtPwd').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtConfirmPwd').tipsy({ gravity: 's' });            
            $('#ctl00_ContentPlaceHolder1_ddlProvinceLive').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_ddlCountry').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_ddlCity').tipsy({ gravity: 's' });            
            $('#ctl00_ContentPlaceHolder1_dlhowyouHeared').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_ddlAge').tipsy({ gravity: 's' });
            


            $('#ctl00_ContentPlaceHolder1_btnSignUp').click(function () {
                var isValidated = true;
                var WrongPassword = false;
                var checkboxchecked = false;
                if (!($('input[id=ctl00_ContentPlaceHolder1_cbSignUp]').is(':checked'))) {
                      // alert($('input[id=ctl00_ContentPlaceHolder1_cbSignup]').is(':checked'));                    
                    isValidated = false;    
                    checkboxchecked=true;                
                }
                var filter0 = /^[a-zA-Z ]+$/;
                if ($("#ctl00_ContentPlaceHolder1_txtFullName").val() == '' || !filter0.test($("#ctl00_ContentPlaceHolder1_txtFullName").val())) {
                    $("#ctl00_ContentPlaceHolder1_txtFullName").removeClass("TextBox").addClass("TextBoxError");                    
                    isValidated = false;
                    
                }

                if ($("#ctl00_ContentPlaceHolder1_txtLastName").val() == '' || !filter0.test($("#ctl00_ContentPlaceHolder1_txtLastName").val())) {
                    $("#ctl00_ContentPlaceHolder1_txtLastName").removeClass("TextBox").addClass("TextBoxError");                   
                    isValidated = false;
                }

                // validate Email

                var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
                if ($("#ctl00_ContentPlaceHolder1_txtUsernameSignUp").val() == '' || !filter.test($("#ctl00_ContentPlaceHolder1_txtUsernameSignUp").val())) {
                    $("#ctl00_ContentPlaceHolder1_txtUsernameSignUp").removeClass("TextBox").addClass("TextBoxError");                    
                    isValidated = false;
                }

                //validate Password
                //validate Password
                var filter2 = /([a-zA-Z0-9]{6,15})$/;
                if ($("#ctl00_ContentPlaceHolder1_txtPwd").val() == '' || !filter2.test($("#ctl00_ContentPlaceHolder1_txtPwd").val())) {
                    $("#ctl00_ContentPlaceHolder1_txtPwd").removeClass("TextBox").addClass("TextBoxError");                    
                    isValidated = false;
                    WrongPassword=true;                    
                }

                if ($("#ctl00_ContentPlaceHolder1_txtConfirmPwd").val() == '' || ($("#ctl00_ContentPlaceHolder1_txtConfirmPwd").val() != $("#ctl00_ContentPlaceHolder1_txtPwd").val())) {
                    $("#ctl00_ContentPlaceHolder1_txtConfirmPwd").removeClass("TextBox").addClass("TextBoxError");                    
                    isValidated = false;
                }

                
                if (document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').selectedIndex == 0) {
                    $("#ctl00_ContentPlaceHolder1_ddlCountry").removeClass("TextBox").addClass("TextBoxError");                    
                    isValidated = false;
                }

                

                if (document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').selectedIndex == 0) {
                    $("#ctl00_ContentPlaceHolder1_ddlProvinceLive").removeClass("TextBox").addClass("TextBoxError");                    
                    isValidated = false;
                }

                if (document.getElementById('ctl00_ContentPlaceHolder1_ddlCity').selectedIndex == 0) {
                    $("#ctl00_ContentPlaceHolder1_ddlCity").removeClass("TextBox").addClass("TextBoxError");                    
                    isValidated = false;
                }
                if (isValidated) {
                }
                else {     
                    if(checkboxchecked)
                    {
                        MessegeArea('oops, you are not agree with our terms and agreement.Please accept terms and agreement', 'Error');                
                    }
                    else if(WrongPassword)
                    {
                        MessegeArea("Password requires 6-15 characters", "Error");                    
                    }
                    else
                    {
                        MessegeArea("Please enter required fields.", "Error");         
                    }
                    return false;
                }
            });


           


        }
    </script>

    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                        Sign up
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 980px; background-color: White; min-height: 450px;">
                    <center>
                        <div style="width: 930px !important;" class="PageHeading">
                            Registration:
                        </div>
                    </center>
                    <div style="width: 965px; margin-left: 15px; float: left; overflow: hidden; padding-bottom: 40px;">
                        <div style="margin-top: 30px;">
                            <asp:UpdatePanel ID="udpnl" runat="server">
                                <ContentTemplate>
                                    <div style="float: left; width: 315px;">
                                        <div>
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                First Name
                                            </div>
                                            <div style="margin-bottom: 5px;">
                                                <asp:TextBox CssClass="TextBox" title="Enter first name" onfocus="this.className='TextBox'"
                                                    ID="txtFullName" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Last Name
                                            </div>
                                            <div style="margin-bottom: 5px;">
                                                <asp:TextBox CssClass="TextBox" title="Enter last name" onfocus="this.className='TextBox'"
                                                    ID="txtLastName" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Email Address
                                            </div>
                                            <div style="margin-bottom: 5px;">
                                                <asp:TextBox CssClass="TextBox" title="Enter Email address" onfocus="this.className='TextBox'"
                                                    ID="txtUsernameSignUp" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Password
                                            </div>
                                            <div style="margin-bottom: 5px;">
                                                <asp:TextBox CssClass="TextBox" title="Enter password" onfocus="this.className='TextBox'"
                                                    TextMode="Password" ID="txtPwd" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Confirm Password
                                            </div>
                                            <div style="margin-bottom: 5px;">
                                                <asp:TextBox CssClass="TextBox" title="Enter Confirm password" onfocus="this.className='TextBox'"
                                                    TextMode="Password" ID="txtConfirmPwd" runat="server"></asp:TextBox>
                                            </div>
                                            <div style="margin-top: 10px; width: 120%; clear: both;">
                                                <div style="float: left;">
                                                    <asp:CheckBox ID="cbSignUp" Checked="false" runat="server" Text="" />
                                                </div>
                                                <div style="float: left;">
                                                    <a target="_blank" style="float: left;" href="terms-customer.aspx" class="OrangeLink">
                                                        I agree to Privacy Policy | Terms & Conditions </a>
                                                </div>
                                            </div>
                                            <div style="margin-top: 20px; clear: both; padding-top: 10px;">
                                                <asp:Button ID="btnSignUp" runat="server" Text="Signup" OnClick="btnSignUp_Click"
                                                    CssClass="button big primary" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 315px; float: left;">
                                        <div style="clear: both; margin-bottom: 5px;">
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Country
                                            </div>
                                            <div style="clear: both;">
                                                <asp:DropDownList ID="ddlCountry" ToolTip="Select Your Country." Height="30px" runat="server"
                                                    CssClass="TextBox" onfocus="this.className='TextBox'" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select Your Country" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Canada" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="United States" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="clear: both; margin-bottom: 5px; padding-top: 5px;">
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Province/State
                                            </div>
                                            <div style="clear: both;">
                                                <asp:DropDownList ID="ddlProvinceLive" ToolTip="Select Your Province/State." Height="30px"
                                                    runat="server" CssClass="TextBox" onfocus="this.className='TextBox'" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlProvinceLive_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="clear: both; margin-bottom: 5px; padding-top: 5px;">
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Home City
                                            </div>
                                            <div style="clear: both;">
                                                <asp:DropDownList ID="ddlCity" Height="30px" ToolTip="Select Your City." runat="server"
                                                    CssClass="TextBox" onfocus="this.className='TextBox'">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="clear: both; margin-bottom: 5px; padding-top: 5px;">
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Age (Optional)
                                            </div>
                                            <div style="clear: both;">
                                                <asp:DropDownList ID="ddlAge" Height="30px" ToolTip="Select Your Age." runat="server"
                                                    CssClass="TextBox" onfocus="this.className='TextBox'">
                                                    <asp:ListItem Selected="True" Value="Select">Select</asp:ListItem>
                                                    <asp:ListItem Value="under 20">under 20</asp:ListItem>
                                                    <asp:ListItem Value="21-25">21-25</asp:ListItem>
                                                    <asp:ListItem Value="26-30">26-30</asp:ListItem>
                                                    <asp:ListItem Value="31-35">31-35</asp:ListItem>
                                                    <asp:ListItem Value="36-40">36-40</asp:ListItem>
                                                    <asp:ListItem Value="41-45">41-45</asp:ListItem>
                                                    <asp:ListItem Value="46-50">46-50</asp:ListItem>
                                                    <asp:ListItem Value="51-55">51-55</asp:ListItem>
                                                    <asp:ListItem Value="56-60">56-60</asp:ListItem>
                                                    <asp:ListItem Value="61-65">61-65</asp:ListItem>
                                                    <asp:ListItem Value="66-70">66-70</asp:ListItem>
                                                    <asp:ListItem Value="71+">71+</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="clear: both; margin-bottom: 5px; padding-top: 5px;">
                                            <div class="ItemHiding" style="color: #5F5F5F !important;">
                                                Gender
                                            </div>
                                            <div style="clear: both; padding-top: 5px;">
                                                <div style="float: left;">
                                                    <asp:RadioButton ID="rbMale" runat="server" Checked="true" GroupName="gender" />
                                                </div>
                                                <div style="float: left; padding-left: 5px;">
                                                    <asp:Label ID="Label3" class="ItemHiding" Style="color: #000 !important;" runat="server"
                                                        Text="Male" />
                                                </div>
                                                <div style="float: left; padding-left: 10px;">
                                                    <asp:RadioButton ID="rbFemale" runat="server" GroupName="gender" />
                                                </div>
                                                <div style="float: left; padding-left: 5px;">
                                                    <asp:Label ID="Label6" class="ItemHiding" Style="color: #000 !important;" runat="server"
                                                        Text="Female" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="width: 300px; float: right;">
                                <div style="clear: both;">
                                    <div style="width: 250px; height: 200px;">
                                        <div class="ItemHiding" style="color: #5F5F5F !important;">
                                            If you have Facebook Account</div>
                                        <div style="margin-left: -5px;">
                                            <a href="javascript:login();">
                                                <img src="Images/BtnConnectWithFacebook.png" /></a></div>
                                    </div>
                                </div>
                                <div style="clear: both;">
                                    <div style="margin-top: 20px;">
                                        <div style="height: 180px; width: 268px; border: solid 1px #E6E6E5; background-image: url(Images/verisignBG.png);
                                            background-repeat: repeat-x; background-position: bottom; background-color: #eeeeee;">
                                            <div style="height: 24px; padding-top: 15px; font-family: Berlin Sans FB Demi; color: #a71d4c;
                                                font-size: 12px; text-align: center; font-weight: bold;">
                                                VERISIGN SSL VERIFIED - SECURED
                                            </div>
                                            <div style="text-align: center;">
                                                <div id="myDiv">

                                                    <script type="text/javascript" src="https://seal.verisign.com/getseal?host_name=www.tazzling.com&amp;size=M&amp;use_flash=YES&amp;use_transparent=YES&amp;lang=en"></script>

                                                </div>
                                                <div style="clear: both; text-align: center; padding-left: 50px; padding-top: 10px;">
                                                    <a href="https://www.verisign.com/ssl-certificate/" target="_blank" style="color: #6f6f6f;
                                                        text-decoration: none; font-size: 8px; font-family: Berlin Sans FB; text-align: center;
                                                        line-height: 11px;">
                                                        <div style="clear: both; text-align: center;">
                                                            <div style="float: left;">
                                                                <img src="Images/verisignImgBottom.png" /></div>
                                                            <div style="float: left; width: 150px;">
                                                                ALL DATA ON TASTYGO IS PROTECTED BY TOP-NOTCH TRUSTED SSL CERTIFICATE</div>
                                                        </div>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
