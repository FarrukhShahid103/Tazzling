
loadJavaScriptFile("JS/jquery-1.4.0.min.js");
loadJavaScriptFile("JS/jquery-ui-1.8.18.custom.min.js");
function loadJavaScriptFile(jspath) {
    document.write('<script type="text/javascript" src="' + jspath + '"><\/script>');
}
function SetContentHeight() {
    $("#page-wrap").height($(window).height());
}
function DoLogin() {
    var isValidated = true;
    // validate Email

    var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
    if ($("#txtEmail").val() == '' || !filter.test($("#txtEmail").val())) {
        $("#txtEmail").removeClass("loginTextBox").addClass("loginTextBoxError");
        isValidated = false;
    }

    //validate Password                
    if ($("#txtPwd").val() == '') {
        $("#txtPwd").removeClass("loginTextBox").addClass("loginTextBoxError");
        isValidated = false;
    }
    if (isValidated) {
        Loading("Login");
        $.ajax({
            type: "POST",
            url: "getStateLocalTime.aspx?loginID=" + $("#txtEmail").val() + "&loginPass=" + $("#txtPwd").val(),
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: false,
            success: function (msg) {
                if (msg == "User login successfully.") {
                    window.location.href = "Default.aspx";
                }
                else {
                    setTimeout(function () {
                        Error('Error', msg)
                    }, 2000);
                }
            }

        });
    }
}


function RecoverPassword() {
    var isValidated = true;
    // validate Email

    var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
    if ($("#txtEmailAddress").val() == '' || !filter.test($("#txtEmailAddress").val())) {
        $("#txtEmailAddress").removeClass("loginTextBox").addClass("loginTextBoxError");
        isValidated = false;
    }
    else {
        Loading("Forgot");
        $.ajax({
            type: "POST",
            url: "getStateLocalTime.aspx?forgetpassword=" + $("#txtEmailAddress").val(),
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: false,
            success: function (msg) {
                if (msg == "Please enter a valid Email ID") {
                    setTimeout(function () {
                        Error('Error', msg);
                    }, 2000);

                }
                else if (msg == "Account information sent to your email address.") {
                    setTimeout(function () {
                        Error('Success', msg);
                    }, 2000);
                }
                else if (msg == "Email sending failed. Please try again.") {
                    setTimeout(function () {
                        Error('Error', msg);
                    }, 2000);
                }

                else if (msg == "Account information sent to your email address.") {
                    setTimeout(function () {
                        Error('Success', msg);
                    }, 2000);
                }

                else if (msg == "Email sending failed. Please try again.") {
                    setTimeout(function () {
                        Error('Error', msg);
                    }, 2000);
                }

                else if (msg == "This email address does not exist.") {
                    setTimeout(function () {
                        Error('Error', msg);
                    }, 2000);
                }
                else {
                    setTimeout(function () {
                        Error('Error', msg);
                    }, 2000);
                }
            }
        });

    }

    return false;
}

function CloseMessageArea() {
    var area = $("#CloseImage").attr("area");

    $("#MessageArea").slideToggle("fast");

    if (area == "forgot") {
        $("#ForgotPassword").slideToggle("fast");
    }
    else if (area == "login") {

        $("#loginArea").slideToggle("fast");
    }

    $("#buttonClick").show();
    $("IfMamber").show();
}
function Loading(Area) {
    $("#buttonClick").hide();
    $("IfMamber").hide();
    if (Area == "Forgot") {
        $("#ForgotPassword").slideToggle("fast");
        $("#CloseImage").attr("area", "forgot");
    }
    else if (Area == "Login") {
        $("#loginArea").slideToggle("fast");
        $("#CloseImage").attr("area", "login");
    }
    $("#LoadingArea").slideToggle("fast");
}
function Error(MessageType, Message) {
    if (MessageType == "Success") {
        $("#ErrorImage").attr("src", "images/Success.png");
        $("#ErrorMessageLabel").css({ color: '#23851f' }).html(Message);
    }
    else {
        $("#ErrorImage").attr("src", "images/ErrorIcon.png");
        $("#ErrorMessageLabel").css({ color: '#F77700' }).html(Message);

    }
    $("#LoadingArea").slideToggle("fast");
    $("#MessageArea").slideToggle("fast");
    Bounce("ErrorImage");
    Bounce("ErrorMessageLabel");
}

function Bounce(id) {
    setTimeout(function () {
        $("#" + id).effect("bounce", { times: 3 }, 100);
    }, 500
        );
}



function AreaSwitching(CurrentArea) {
    if (CurrentArea == "signUpArea") {

        // Hide Login Area
        $("#loginArea").slideUp("fast");
        $("#buttonClick").slideUp("fast");

        $("#ForgotPassword").slideUp("fast");

        //Show Signup Area
        $("#signUpArea").slideDown("fast");
        $("#IfMamber").slideDown("fast");
    }
    else if (CurrentArea == "loginArea") {
        //Hide Signup Area
        $("#signUpArea").slideUp("fast");
        $("#IfMamber").slideUp("fast");


        $("#ForgotPassword").slideUp("fast");

        // Show Login Area
        $("#loginArea").slideDown("fast");
        $("#buttonClick").slideDown("fast");
    }
    else if (CurrentArea == "ForgotPassword") {
        //Hide Signup Area
        $("#signUpArea").slideUp("fast");
        // $("#IfMamber").slideUp("fast");

        // Hide Login Area
        $("#loginArea").slideUp("fast");
        // $("#buttonClick").slideUp("fast");

        $("#ForgotPassword").slideDown("fast");
    }

}
function ToggleSignup() {
    $("#loginArea").slideToggle("fast");
    $("#ForgotPassword").slideToggle("fast");
}