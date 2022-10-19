<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resetPassword.aspx.cs" Inherits="resetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Your Password</title>

    <script type="text/javascript" src="JS/jquery-1.4.min.js"></script>

    <script type="text/javascript" src="JS/jquery.easing.1.3.js"></script>

    <script src="JS/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="CSS/tastygo.css" />

    <script src="JS/tipsy.js"></script>

    <link href="CSS/tipsy.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <link href="CSS/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="JS/jquery.hoverwords.js"></script>

    <script type="text/javascript" src="JS/jquery.lettering.js"></script>

</head>
<body style="background-image: url('') !important;">

    <script type="text/javascript">
        function TastyLogin() {
            var isValidated = true;

            //validate Password
            var filter2 = /([a-zA-Z0-9]{6,15})$/;
            if ($("#txtPwd").val() == '' || !filter2.test($("#txtPwd").val())) {
                $("#txtPwd").removeClass("TextBox").addClass("TextBoxError");
                isValidated = false;
            }
            if (isValidated) {
                return true;
            }
            else {
                return false;
            }

        }
    </script>

    <script type="text/javascript">
        $(window).load(function() {

          
        });

        $(document).ready(function() {
        $('.Tipsy').tipsy({ gravity: 's' });
        $("#messages").click(function() {
            $("#messages").slideUp('slow');
        });
        $('.xyz').hoverwords({ delay: 60 });
        });


        function MessegeArea(msg, msgtype) {
            // $("#messages").slideUp('slow').parent().removeClass("ErrorBorder SuccessBorder");


            $("#messages").slideUp('slow', function() {
                // Animation complete.
            });

            if (msgtype == "success") {
                $("#messages").removeClass("errorMessage").addClass("successMessage").html(msg).slideDown("slow").parent().removeClass("ErrorBorder").addClass("SuccessBorder");
            }
            else {
                $("#messages").removeClass("successMessage").addClass("errorMessage").html(msg).slideDown("slow").parent().removeClass("SuccessBorder").addClass("ErrorBorder");
            }

            setTimeout(function() {
                $("#messages").slideUp('slow', function() {
                    $("#messages").parent().removeClass("ErrorBorder SuccessBorder");
                });
            }, 60000);

        };
      
    </script>

    <form id="form1" runat="server">
    <center>
        <div>
            <asp:Panel ID="PagePnl" runat="server" DefaultButton="btnSignin">
                <div style="width: 275px; margin-top: 200px; border: 2px solid #055FA0; padding: 10px;
                    overflow: hidden;" id="SigninDiv">
                    <asp:HiddenField ID="hfUserID" runat="server" />
                    <div style="text-align: left;" class="ItemHiding">
                        New Password
                    </div>
                    <div style="margin-bottom: 5px;">
                        <asp:TextBox onfocus="this.className='TextBox Tipsy'" title="Enter your new password"
                            CssClass="TextBox Tipsy" TextMode="Password" ID="txtPwd" runat="server"></asp:TextBox>
                    </div>
                    <div style="clear: both; margin-top: 15px; float: left; text-align: left;">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <div style="margin-top: 10px; float: left; clear: both;">
                        <asp:Button ID="btnSignin" OnClientClick="return TastyLogin();" OnClick="btnSignin_Click"
                            runat="server" Text="Reset" CssClass="button big primary" />
                    </div>
                </div>
            </asp:Panel>
            <div style="position: fixed; z-index: 9999999999999; bottom: 0px; width: 100%;">
                <center>
                    <div style="width: 920px" id="messages">
                    </div>
                </center>
            </div>
        </div>
    </center>
    </form>
</body>
</html>
