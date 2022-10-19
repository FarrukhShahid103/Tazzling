<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TastyLogin.aspx.cs" Inherits="TastyLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <script type="text/javascript" src="JS/jquery-1.4.min.js"></script>
    <script type="text/javascript" src="JS/jquery.easing.1.3.js"></script>
    <script src="JS/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="CSS/tastygo.css" />
     <link rel="stylesheet" href="CSS/gh-buttons.css">
    <script src="JS/tipsy.js"></script>
    <link href="CSS/tipsy.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-image: url('') !important;">
    <script type="text/javascript">
        function RecoverPassword() {
            var isValidated = true;
            // validate Email

            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
            if ($("#txtEmailAddress").val() == '' || !filter.test($("#txtEmailAddress").val())) {
                $("#txtEmailAddress").removeClass("TextBox").addClass("TextBoxError");
                isValidated = false;
            }
            else {

                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?forgetpassword=" + $("#txtEmailAddress").val(),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (msg == "Please enter a valid Email ID") {
                            MessegeArea(msg, 'error');
                        }
                        else if (msg == "Account information sent to your email address.") {
                            MessegeArea( msg,'success');
                        }

                        else if (msg == "Email sending failed. Please try again.") {
                            MessegeArea(msg, 'error');
                        }

                        else if (msg == "Account information sent to your email address.") {
                            MessegeArea(msg, 'error');
                        }

                        else if (msg == "Email sending failed. Please try again.") {
                            MessegeArea(msg, 'error');
                        }

                        else if (msg == "This email address does not exist.") {
                            MessegeArea(msg, 'error');
                        }
                        else {
                            MessegeArea(msg, 'error');
                        }
                    }
                });

            }

            return false;
        }
        function pageLoad() {
            $('#txtEmail').tipsy({ gravity: 's' });
            $('#txtPwd').tipsy({ gravity: 's' });
            $('#txtEmailAddress').tipsy({ gravity: 's' });

            $("#txtEmailAddress").keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    ForgotPassword();
                    RecoverPassword();
                    //alert('Enter Key Pressed');
                }
            });




        }

        function TastyLogin() {
            var isValidated = true;
            // validate Email
            
            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
            if ($("#txtEmail").val() == '' || !filter.test($("#txtEmail").val())) {
                $("#txtEmail").removeClass("TextBox").addClass("TextBoxError");
                isValidated = false;
            }

            //validate Password                
            if ($("#txtPwd").val() == '') {
                $("#txtPwd").removeClass("TextBox").addClass("TextBoxError");
                isValidated = false;
            }
            if (isValidated) {
            }
            else {
                return false;
            }
            
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $('.Tipsy').tipsy({ gravity: 's' });
        }

        function ErrorDialog(Title, Message, ID) {
            $("#ErrorMessage").html(Message);
            $("#ErrorDialog").dialog({
                title: Title,
                body: 'This is body',
                show: 'slide',
                width: 500,
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }

            });
        }
        function MessegeArea(msg, msgtype) {
            if (msgtype == "success") {
                $("#messages").removeClass("errorMessage").addClass("successMessage").html(msg).slideDown("slow");


            }
            else {
                $("#messages").removeClass("successMessage").addClass("errorMessage").html(msg).slideDown("slow");

            }
        };

        function SuccessDialog(Title, Message) {
            $("#SuccessMessage").html(Message);
            $("#SuccessDialog").dialog({
                title: Title,
                show: 'slide',
                width: 500,
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    </script>
    <form id="form1" runat="server">
    <center>
        <div>         
            <asp:Panel ID="PagePnl" runat="server" DefaultButton="btnSignin">
                <div style="width:275px; margin-top:200px; border:2px solid #055FA0; padding:10px; overflow:hidden;" id="SigninDiv">
                    <div style="text-align:left;" class="ItemHiding">
                        Email Address
                    </div>
                    <div style="margin-bottom: 5px;">
                        <asp:TextBox onfocus="this.className='TextBox'" title="Enter your email address"
                            CssClass="TextBox" ID="txtEmail" runat="server"></asp:TextBox>
                    </div>
                    <div style="text-align:left;" class="ItemHiding">
                        Password
                    </div>
                    <div style="margin-bottom: 5px;">
                        <asp:TextBox onfocus="this.className='TextBox'" title="Enter your password" CssClass="TextBox"
                            TextMode="Password" ID="txtPwd" runat="server"></asp:TextBox>
                    </div>
                    <div style="clear:both; margin-top:15px;float:left;text-align:left;">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <div style="margin-top: 20px; float:left;clear:both;">
                        <asp:Button ID="btnSignin" OnClientClick="return TastyLogin();" OnClick="btnSignin_Click" runat="server" Text="Sign in"
                            CssClass="button big primary" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </center>
    </form>
</body>
</html>
