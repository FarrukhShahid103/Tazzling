<%@ Page Title="Profile" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="member_profile.aspx.cs" Inherits="member_profile" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/FrameStart.ascx" TagPrefix="RedSgnal"
    TagName="FrameStart" %>
<%@ Register Src="~/UserControls/Templates/FrameEnd.ascx" TagPrefix="RedSgnal" TagName="FrameEnd" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>
<%@ Register Src="~/UserControls/Templates/Total-Referral.ascx" TagPrefix="RedSgnal"
    TagName="TotalComission" %>
<%--<%@ Register Src="~/UserControls/Templates/Total-Points.ascx" TagPrefix="RedSgnal"
    TagName="TotalPoints" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/coda-slider-2.0.css" type="text/css" media="screen" />
    <script type="text/javascript" src="JS/jquery.coda-slider-2.0.js"></script>
    <link href="CSS/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.qtip.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.jgrowl.js" type="text/javascript"></script>
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <style type="text/css">
        .ChangePasswordActive
        {
            background-color: #F0F0F0;
            padding: 10px;
            padding-top: 0px !important;
        }
        .ui-widget
        {
            font-size: 11px !important;
        }
        .ui-state-error-text
        {
            margin-left: 10px;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function ImageValidation(oSrc, args) {

            if (args.Value != "") {
                var strFUImage = args.Value;
                var strFUImageArray = strFUImage.split(".");
                var strFUImageExt = strFUImageArray[1];
                if (strFUImageExt == "jpg" || strFUImageExt == "JPG" || strFUImageExt == "gif" || strFUImageExt == "GIF" || strFUImageExt == "JPEG" ||
                strFUImageExt == "jpeg" || strFUImageExt == "png" || strFUImageExt == "PNG" || strFUImageExt == "ico" || strFUImageExt == "ICO"
                || strFUImageExt == "tif" || strFUImageExt == "TIF" || strFUImageExt == "tiff" || strFUImageExt == "TIFF" || strFUImageExt == "bmp"
                || strFUImageExt == "BMP") {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;
                    return;
                }
            }
            else {
                args.IsValid = false;
                return;
            }
        }

        function ValidateImage() {
                alert("in");
            var fup = document.getElementById('ctl00_ContentPlaceHolder1_fuUserProfilePic');

            var fileName = fup.value;
                alert(fileName);
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
                if (ext == "jpg" || ext == "JPG" || ext == "gif" || ext == "GIF" || ext == "JPEG" ||
                ext == "jpeg" || ext == "png" || ext == "PNG" || ext == "ico" || ext == "ICO"
                || ext == "tif" || ext == "TIF" || ext == "tiff" || ext == "TIFF" || ext == "bmp"
                || ext == "BMP") {

                    return true;
                }
                else {
                    MessegeArea("Please Select valid Image Type, Eg: JPG, PNG, GIF, ICO, TIFF, BMP", "Error");
                    fup.focus();
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript">

        function ClearCarditCardInfo() {

            $("#ctl00_ContentPlaceHolder1_txtFirstNameCC").val("");
            $("#ctl00_ContentPlaceHolder1_txtBillingAddress").val("");
            $("#ctl00_ContentPlaceHolder1_txCardNumner").val("");
            $("#ctl00_ContentPlaceHolder1_txtCCVNumber").val("");
            $("#ctl00_ContentPlaceHolder1_txtCity").val("");
            $("#ctl00_ContentPlaceHolder1_txtPostalCode").val("");

            $("#ctl00_ContentPlaceHolder1_ddlMonth").prop("selectedIndex", 0);
            $("#ctl00_ContentPlaceHolder1_ddlYear").prop("selectedIndex", 0);
            $("#ctl00_ContentPlaceHolder1_ddlState").prop("selectedIndex", 0);

        }

        function AddCraditCard() {
            if ($("#BtnAddCraditCard").html() == "Cancel") {
                HideAddCraditCard();
            }
            else {
                ClearCarditCardInfo();
                ShowAddCraditCard();
            }


        }

        function ShowAddCraditCard() {

            $("#DivCraditCards").slideDown("slow");
            $("#BtnAddCraditCard").addClass("danger").html("Cancel");
            setTimeout(function () {
                $('#Itemtab3').click();
            }, 1000);


        }

        function HideAddCraditCard() {

            ClearCarditCardInfo();
            $("#DivCraditCards").slideUp("slow");
            $("#BtnAddCraditCard").removeClass("danger").html("Add Cradit Card");
            IsCarditCardDivVisible = false;
            setTimeout(function () {
                $('#Itemtab3').click();
            }, 1000);
        }

        var IsSlideUp = true;
        function TogglePasswordDive() {
            setTimeout(function () {
                $('#Itemtab1').click();

            }, 1000);

            if (!IsSlideUp) {
                $("#UserPasswordDiv").slideUp({
                // duration: 2000,
                // easing: 'easeOutElastic'
            });
            $("#maindivchangepassword").removeClass("ChangePasswordActive");

            $("#btnChangeUserPassword").removeClass("button danger icon remove").addClass("button icon key").html("Change Password");

            $("#ctl00_ContentPlaceHolder1_OldPassword").removeClass("ChangePasswordTextBox").addClass("TextBox");
            $("#ctl00_ContentPlaceHolder1_NewPassword").removeClass("ChangePasswordTextBox").addClass("TextBox");


            IsSlideUp = true;
        }
        else {

            $("#UserPasswordDiv").slideDown({
            //  duration: 2000,
            //  easing: 'easeOutElastic'
        });

        $("#btnChangeUserPassword").removeClass("button icon key").addClass("button danger icon remove").html("Cancel");
        $("#maindivchangepassword").addClass("ChangePasswordActive");
        $("#ctl00_ContentPlaceHolder1_OldPassword").removeClass("TextBox").addClass("ChangePasswordTextBox");
        $("#ctl00_ContentPlaceHolder1_NewPassword").removeClass("TextBox").addClass("ChangePasswordTextBox");
        IsSlideUp = false;
    }
}


function OpenChangePasswordDiv() {
    $("#UserPasswordDiv").slideDown({
    //  duration: 2000,
    //  easing: 'easeOutElastic'
});


$("#btnChangeUserPassword").removeClass("button icon key").addClass("button danger icon remove").html("Cancel");
$("#maindivchangepassword").addClass("ChangePasswordActive");
$("#ctl00_ContentPlaceHolder1_OldPassword").removeClass("TextBox").addClass("ChangePasswordTextBox");
$("#ctl00_ContentPlaceHolder1_NewPassword").removeClass("TextBox").addClass("ChangePasswordTextBox");
IsSlideUp = false;
}

    </script>
    <script>
        FB.init({
            appId: '160996503945227',
            oauth: true,
            status: true,
            cookie: true,
            xfbml: true
        });

        function login() {
            $("#FBConnectButton").hide("slow");
            $("#FBPleaseWait").show("slow");
            //alert("Going to call fb login");
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
                                window.location.href = "member_profile.aspx?TabID=3";
                                //window.location.reload();
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
            // $("#Itemtab4").hide();
        }
        function ChangePassword() {

            // ctl00_ContentPlaceHolder1_OldPassword
            // ctl00_ContentPlaceHolder1_NewPassword

            var IsChangeValid = true;
            var filter2 = /([a-zA-Z0-9]{6,15})$/;

            var NewPass = $("#ctl00_ContentPlaceHolder1_NewPassword").val();
            var ConfirmPassword = $("#ctl00_ContentPlaceHolder1_ConfirmNewPassword").val();
            if (typeof $("#ctl00_ContentPlaceHolder1_txtUserName").attr("disabled") != "undefined" && $("#ctl00_ContentPlaceHolder1_txtUserName").attr("disabled") == "disabled") {

                if ($("#ctl00_ContentPlaceHolder1_OldPassword").val() == "") {
                    $("#ctl00_ContentPlaceHolder1_OldPassword").addClass("TextBoxError").removeClass("TextBox");
                    IsChangeValid = false;
                }


                if (NewPass == "" || !filter2.test(NewPass)) {
                    $("#ctl00_ContentPlaceHolder1_NewPassword").addClass("TextBoxError").removeClass("TextBox");
                    MessegeArea("Password requires 6-15 characters", "Error");
                    IsChangeValid = false;
                    return false;
                }

                if (NewPass != ConfirmPassword) {
                    $("#ctl00_ContentPlaceHolder1_NewPassword").addClass("TextBoxError").removeClass("TextBox");
                    $("#ctl00_ContentPlaceHolder1_ConfirmNewPassword").addClass("TextBoxError").removeClass("TextBox");
                    MessegeArea("Password does not match", "Error");
                    IsChangeValid = false;
                    return false;
                }

                if ($("#ctl00_ContentPlaceHolder1_OldPassword").val() == "") {

                    $("#ctl00_ContentPlaceHolder1_OldPassword").addClass("TextBoxError").removeClass("TextBox");
                    MessegeArea("Please enter old Password.", "Error");
                    IsChangeValid = false;
                    return false;
                }


                if (ConfirmPassword == "") {
                    $("#ctl00_ContentPlaceHolder1_ConfirmNewPassword").addClass("TextBoxError").removeClass("TextBox");
                    IsChangeValid = false;
                }

                if (ConfirmPassword == "") {

                    $("#ctl00_ContentPlaceHolder1_NewPassword").addClass("TextBoxError").removeClass("TextBox");
                    IsChangeValid = false;
                }
                return IsChangeValid;

            }

            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
            if ($("#ctl00_ContentPlaceHolder1_txtUserName").val() == '' || !filter.test($("#ctl00_ContentPlaceHolder1_txtUserName").val())) {
                $("#ctl00_ContentPlaceHolder1_txtUserName").removeClass("TextBox").addClass("TextBoxError");
                MessegeArea("Please enter a valid email address.", "Error");
                IsChangeValid = false;
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_OldPassword").val() != "") {

                if (NewPass == "" || !filter2.test(NewPass)) {
                    $("#ctl00_ContentPlaceHolder1_NewPassword").addClass("TextBoxError").removeClass("TextBox");
                    MessegeArea("Password requires 6-15 characters", "Error");
                    IsChangeValid = false;
                    return false;
                }

                if (ConfirmPassword == "") {
                    $("#ctl00_ContentPlaceHolder1_ConfirmNewPassword").addClass("TextBoxError").removeClass("TextBox");
                    $("#ctl00_ContentPlaceHolder1_NewPassword").addClass("TextBoxError").removeClass("TextBox");
                    IsChangeValid = false;
                }

                if (NewPass != ConfirmPassword) {
                    $("#ctl00_ContentPlaceHolder1_NewPassword").addClass("TextBoxError").removeClass("TextBox");
                    $("#ctl00_ContentPlaceHolder1_ConfirmNewPassword").addClass("TextBoxError").removeClass("TextBox");
                    MessegeArea("Password and confirm password does not match", "Error");
                    IsChangeValid = false;
                    return false;
                }



            }

            if (NewPass != "" && $("#ctl00_ContentPlaceHolder1_OldPassword").val() == "") {
                $("#ctl00_ContentPlaceHolder1_OldPassword").addClass("TextBoxError").removeClass("TextBox");
                MessegeArea("Please enter old password", "Error");
                IsChangeValid = false;
                return false;
            }

            return IsChangeValid;

        }
        //UserPasswordDiv
        //btnChangeUserPassword







        var ActiveTab = -1;

        //BottomArrow0


        function Tab0() {
            if (ActiveTab != 0) {
                $('#Itemtab1').click();

                $("#TabDiv1").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv2").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv3").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv0").removeClass("NormalTab_Profile").addClass("CurrentActiveTab_Profile");

                $("#BottomArrow1").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow2").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow3").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow0").addClass("CurrentActiveTab_ProfileBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Account Info");
                $("#PageTitle").show('fast');
                ActiveTab = 0;
            }

        }






        function Tab1() {
            if (ActiveTab != 1) {
                $('#Itemtab2').click();
                $("#TabDiv2").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv3").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv1").removeClass("NormalTab_Profile").addClass("CurrentActiveTab_Profile");
                $("#TabDiv0").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");

                $("#BottomArrow0").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow2").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow3").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow1").addClass("CurrentActiveTab_ProfileBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Profile");
                $("#PageTitle").show('fast');
                ActiveTab = 1;
            }

        }

        function Tab2() {
            if (ActiveTab != 2) {
                $('#Itemtab3').click();
                $("#TabDiv1").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv3").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv2").removeClass("NormalTab_Profile").addClass("CurrentActiveTab_Profile");
                $("#TabDiv0").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");

                $("#BottomArrow0").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow3").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow1").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow2").addClass("CurrentActiveTab_ProfileBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Cardit Cards");
                $("#PageTitle").show('fast');
                ActiveTab = 2;
            }

        }

        function Tab3() {
            if (ActiveTab != 3) {
                $('#Itemtab4').click();
                $("#TabDiv1").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv2").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv3").removeClass("NormalTab_Profile").addClass("CurrentActiveTab_Profile");
                $("#TabDiv0").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");

                $("#BottomArrow0").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow1").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow2").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow3").addClass("CurrentActiveTab_ProfileBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Social Connections");
                $("#PageTitle").show('fast');
                ActiveTab = 3;
            }

        }


        function Tab4() {
            if (ActiveTab != 4) {
                $('#Itemtab5').click();
                $("#TabDiv1").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv2").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv3").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");
                $("#TabDiv4").removeClass("NormalTab_Profile").addClass("CurrentActiveTab_Profile");
                $("#TabDiv0").removeClass("CurrentActiveTab_Profile").addClass("NormalTab_Profile");

                $("#BottomArrow0").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow1").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow2").removeClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow3").addClass("CurrentActiveTab_ProfileBottom");
                $("#BottomArrow4").addClass("CurrentActiveTab_ProfileBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Account Information");
                $("#PageTitle").show('fast');
                ActiveTab = 4;
            }

        }

        function UpdateFBShare(Check) {
            //MainFBYes
            //SubFBYes

            //MainFBNo
            //SubFBNo
            if (Check) {
                var Text = $("#SubFBYes").html();
                if (Text == "Please Wait...") {
                    return;
                }
                $("#SubFBYes").addClass('LoadingTag').removeClass('like').html("Please Wait...");
            }
            else {
                var Text = $("#SubFBNo").html();
                if (Text == "Please Wait...") {
                    return;
                }
                $("#SubFBNo").addClass('LoadingTag').removeClass('like').html("Please Wait...");

            }

            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?UpdateFBShare=" + Check,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {

                        if (msg == "True") {

                            if (Check) {
                                $("#SubFBYes").addClass('like').removeClass('LoadingTag').html(Text);
                                $("#MainFBYes").hide("slow");
                                $("#MainFBNo").show("slow");

                                $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/tick.png'  /></span><span>Facebook sharing Enabled successfully.</b></span>"),

                                                    {
                                                        sticky: true,
                                                        glue: 'before',
                                                        speed: 2500,
                                                        easing: 'easeOutBounce'
                                                    };

                                setTimeout(function () {
                                    $("#Itemtab4").click();
                                }, 500);
                            }
                            else {
                                $("#SubFBNo").addClass('like').removeClass('LoadingTag').html(Text);
                                $("#MainFBNo").hide("slow");
                                $("#MainFBYes").show("slow");
                                $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/tick.png'  /></span><span>Facebook sharing Disabled successfully.</span>"),

                                                    {
                                                        sticky: true,
                                                        glue: 'before',
                                                        speed: 2500,
                                                        easing: 'easeOutBounce'
                                                    };

                                setTimeout(function () {
                                    $("#Itemtab4").click();
                                }, 500);
                            }


                        }
                        else {

                            MessegeArea("Some Error occurred while saving data.<br>Please contact Admin", 'error');


                            if (Check) {
                                $("#SubFBYes").addClass('like').removeClass('LoadingTag').html(Text);
                                $("#MainFBYes").show();
                                $("#MainFBNo").hide();
                            }
                            else {
                                $("#SubFBNo").addClass('like').removeClass('LoadingTag').html(Text);
                                $("#MainFBNo").show();
                                $("#MainFBYes").hide();
                            }

                        }
                    }

                });

            }, 1000);
        }
        function CraditCardValidation() {

            var IsValid = true;

            if ($("#ctl00_ContentPlaceHolder1_txCardNumner").val() == "") {
                $("#ctl00_ContentPlaceHolder1_txCardNumner").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }
            else {
                var num = $("#ctl00_ContentPlaceHolder1_txCardNumner").val();
                num = (num + '').replace(/\D+/g, '').split('').reverse();
                if (!num.length) {
                    IsValid = false;
                    $("#ctl00_ContentPlaceHolder1_txCardNumner").removeClass("TextBox").addClass("TextBoxError");

                }
                var total = 0, i;
                for (i = 0; i < num.length; i++) {
                    num[i] = parseInt(num[i])
                    total += i % 2 ? 2 * num[i] - (num[i] > 4 ? 9 : 0) : num[i];
                }
                if ((total % 10) == 0 || (total % 10) == 5) {
                    IsValid = true;
                }
                else {
                    IsValid = false;
                    $("#ctl00_ContentPlaceHolder1_txCardNumner").removeClass("TextBox").addClass("TextBoxError");

                }
            }

            if ($("#ctl00_ContentPlaceHolder1_txtFirstNameCC").val() == "") {
                $("#ctl00_ContentPlaceHolder1_txtFirstNameCC").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtBillingAddress").val() == "") {
                $("#ctl00_ContentPlaceHolder1_txtBillingAddress").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }
            if ($("#ctl00_ContentPlaceHolder1_txtCCVNumber").val() == "") {
                $("#ctl00_ContentPlaceHolder1_txtCCVNumber").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtCity").val() == "") {
                $("#ctl00_ContentPlaceHolder1_txtCity").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtPostalCode").val() == "") {
                $("#ctl00_ContentPlaceHolder1_txtPostalCode").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }
            return IsValid;
        }

        function luhn(oSrc, args) {
            if (args.Value != "") {
                var num = args.Value;
                num = (num + '').replace(/\D+/g, '').split('').reverse();
                if (!num.length) {
                    args.IsValid = false;
                    $("#ctl00_ContentPlaceHolder1_txCardNumner").removeClass("TextBox").addClass("TextBoxError");
                    return;
                }
                var total = 0, i;
                for (i = 0; i < num.length; i++) {
                    num[i] = parseInt(num[i])
                    total += i % 2 ? 2 * num[i] - (num[i] > 4 ? 9 : 0) : num[i];
                }
                if ((total % 10) == 0 || (total % 10) == 5) {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;
                    $("#ctl00_ContentPlaceHolder1_txCardNumner").removeClass("TextBox").addClass("TextBoxError");
                    return;
                }
            }
        }

        $(window).ready(function () {
            $('#coda-slider-1').codaSlider({
                dynamicArrows: false
            });
            $("#Itemtab1").hide();
            $("#Itemtab2").hide();
            $("#Itemtab3").hide();
        });

        $(document).ready(function () {
            $("#divEditCustomer").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 20,
                height: 'auto',
                width: 'auto',
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divEditCustomerDlgContainer");
                }
            });
        });


        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divEditCustomer").dialog('close');
        }

        function checkValidation() {
            if ((document.getElementById('ctl00_ContentPlaceHolder1_NewPassword').value != '') || (document.getElementById('ctl00_ContentPlaceHolder1_OldPassword').value != '')) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvPassword'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RegularExpressionValidator1'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator1'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RegularExpressionValidator1'), true);
            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvPassword'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RegularExpressionValidator1'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator1'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RegularExpressionValidator1'), false);
            }
        }

        function openDialog(title, linkID) {

            var pos = $("#" + linkID).position();
            var top = 200;
            var left = 400;


            $("#divEditCustomer").dialog("option", "title", title);
            $("#divEditCustomer").dialog("option", "position", [left, top]);

            $("#divEditCustomer").dialog('open');

        }



        function openDialogAndBlock(title, linkID) {
            // alert("Title: "+ title+" linkID: "+linkID);
            openDialog(title, linkID);

            //block it to clean out the data
            $("#divEditCustomer").block({
                message: '<img src="~/images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }


        function unblockDialog() {
            $("#divEditCustomer").unblock();
        }

        function onTest() {
            $("#divEditCustomer").block({
                message: '<h1>Processing</h1>',
                css: { border: '3px solid #a00' },
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }




        function isPostCodeLocal(oSrc, args) {

            // checks cdn codes only          

            entry = args.Value;
            //alert(entry);


            strlen = entry.length;
            if (strlen != 7) {
                args.IsValid = false;
                return;
            }
            entry = entry.toUpperCase(); //in case of lowercase
            //Check for legal characters,index starts at zero
            s1 = 'ABCEGHJKLMNPRSTVXY'; s2 = s1 + 'WZ'; d3 = '0123456789';


            if (s1.indexOf(entry.charAt(0)) < 0) {
                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(1)) < 0) {
                args.IsValid = false;
                return;
            }
            if (s2.indexOf(entry.charAt(2)) < 0) {
                args.IsValid = false;
                return;
            }
            if (entry.charAt(3) != '-') {

                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(4)) < 0) {
                args.IsValid = false;
                return;
            }
            if (s2.indexOf(entry.charAt(5)) < 0) {
                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(6)) < 0) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
            return;
        } 

    </script>
    <script type="text/javascript" language="javascript">
        function validatePhone(oSrc, args) {
            var phone1 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').value;
            var phone2 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').value;
            var phone3 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').value;
            if (phone1 == "" && phone2 == "" && phone3 == "") {
                args.IsValid = true;
                return;
            }
            if (phone1 == "") {
                args.IsValid = false;

                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();
            }
            else if (phone2 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
            }
            else if (phone3 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
            }
            if (phone1.length != 3) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();
            }
            else if (phone2.length != 3) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
            }
            else if (phone3.length != 4) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
            }
            if (!IsNumeric(phone1)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();
            }
            else if (!IsNumeric(phone2)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
            }
            else if (!IsNumeric(phone3)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
            }

            args.IsValid = true;
            return;
        }

        function IsNumeric(strString) {
            var strValidChars = "0123456789.";
            var strChar;
            var blnResult = true;

            if (strString.length == 0) return false;

            //  test strString consists of valid characters listed above
            for (i = 0; i < strString.length && blnResult == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    blnResult = false;
                }
            }
            return blnResult;
        }

        function countrychanged(obj, objLive, objCh) {
            //alert(obj);
            var ctryID = obj;

            //First it will null all the options of the ProvinceLive drop down list
            for (loop = document.getElementById(objLive).options.length - 1; loop > -1; loop--) {
                document.getElementById(objLive).options[loop] = null;
            }

            var count = 0;

            //The it will get the required value from the Hidden drop down list and set it into the Live drop down list
            document.getElementById(objLive).options[count] = new Option("Select One");
            count++;
            for (loop = document.getElementById(objCh).options.length - 1; loop > -1; loop--) {
                var ProConID = document.getElementById(objCh).options[loop].value;
                //First Part contain the Province ID and Second Part contain the Country ID
                var ProConArray = ProConID.split(",");
                //Validate that Country ID in the country drop down list macthes with the country provinces into the hidden drop down list

                if (ProConArray[1] == ctryID) {

                    var ProvinceName = document.getElementById(objCh).options[loop].text;
                    document.getElementById(objLive).options[count] = new Option(ProvinceName);
                    document.getElementById(objLive).options[count].value = ProConArray[0];
                    count++;
                }
            }
        }
        function setProvinceText() {
            var objDll = document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').selectedIndex;
            if (objDll != 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_hfProvince').value = document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').value;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hfProvince').value = '0';
            }
        }
        function setCountrytext() {
            var objDllCon = document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').selectedIndex;
            if (objDllCon != 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_hfCountry').value = document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').value;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hfCountry').value = '0';
                document.getElementById('ctl00_ContentPlaceHolder1_hfProvince').value = '0';
            }
        }



        function ChangeHeaderAsNeeded() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (!document.getElementById(list[i]).checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = false;
                    return;
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = true;
        }



        function validateProvince(oSrc, args) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').selectedIndex == 0) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
            return;

        }
        function validateCountry(oSrc, args) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').selectedIndex == 0) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
            return;

        }

        function checkForUsers(value) {
            if (value == 2 || value == 5) {
                document.getElementById('tblHide').style.display = "none";

                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvRefID'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvCountry'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvProvince'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvHowYouKnowUs'), false);
            }
            else if (value == 3 || value == 4) {
                document.getElementById('tblHide').style.display = "block";

                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvRefID'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvCountry'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvProvince'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvHowYouKnowUs'), true);
            }
        }

        function ImageValidation(oSrc, args) {

            if (args.Value != "") {
                var strFUImage = args.Value;
                var strFUImageArray = strFUImage.split(".");
                var strFUImageExt = strFUImageArray[1];
                if (strFUImageExt == "jpg" || strFUImageExt == "JPG" || strFUImageExt == "gif" || strFUImageExt == "GIF" || strFUImageExt == "JPEG" ||
                strFUImageExt == "jpeg" || strFUImageExt == "png" || strFUImageExt == "PNG" || strFUImageExt == "ico" || strFUImageExt == "ICO"
                || strFUImageExt == "tif" || strFUImageExt == "TIF" || strFUImageExt == "tiff" || strFUImageExt == "TIFF" || strFUImageExt == "bmp"
                || strFUImageExt == "BMP") {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;


                    MessegeArea("Please Select valid Image Type, Eg: JPG, PNG, GIF, ICO, TIFF, BMP", "Error");

                    return;
                }
            }
            else {
                args.IsValid = false;
                MessegeArea("Please Select valid Image Type, Eg: JPG, PNG, GIF, ICO, TIFF, BMP", "Error");
                return;
            }
        }


        
        
    </script>
    <script>
        $(function () {
            $("#accordion").accordion({ collapsible: true, active: -1, autoHeight: false });
        });

         
    </script>
    <script type="text/javascript">
        $(function () {
            $(".ExpandPanel").click(function () {

                setInterval(function () {
                    $("#Itemtab3").click();
                }, 1000);
            });

        });

   
       
    </script>
    <div class="DetailPage2ndDiv">
        <div style="width: 980px; float: left;">
            <div>
                <div style="overflow: hidden;">
                    <usrCtrl:subMenu ID="subMenu1" runat="server" />
                </div>
                <div style="clear: both; padding-top: 10px; margin-bottom: 10px;">
                    <div class="DetailPageTopDiv">
                        <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                            <div id="PageTitle" class="PageTopText" style="float: left;">
                                My Account Info
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv" style="padding-top: 0px;">
                <div style="float: left; width: 100%; background-color: White; min-height: 450px;
                    border: 1px solid #ACAFB0;">
                    <div>
                        <div style="width: 100%; background-image: url('Images/FundBG.jpg'); background-repeat: repeat-x;
                            overflow: hidden;">
                            <div id="TabDiv0" onclick="javascript:Tab0();" class="CurrentActiveTab_Profile">
                                <div style="color: White; font-size: 15px; font-weight: bold; line-height: 50px;
                                    text-align: center;">
                                    Account
                                </div>
                            </div>
                            <div id="TabDiv1" onclick="javascript:Tab1();" class="NormalTab_Profile">
                                <div style="color: White; font-size: 15px; font-weight: bold; line-height: 50px;
                                    text-align: center;">
                                    Profile
                                </div>
                            </div>
                            <div id="TabDiv2" onclick="javascript:Tab2();" class="NormalTab_Profile">
                                <div style="color: White; font-size: 15px; font-weight: bold; line-height: 50px;
                                    text-align: center;">
                                    Credit Card
                                </div>
                            </div>
                            <div id="TabDiv3" onclick="javascript:Tab3();" class="NormalTab_Profile">
                                <div style="color: White; font-size: 15px; font-weight: bold; line-height: 50px;
                                    text-align: center;">
                                    Connections
                                </div>
                            </div>
                        </div>
                        <div style="width: 100%; overflow: hidden;">
                            <div class="CurrentActiveTab_ProfileBottom" id="BottomArrow0" style="width: 25%;
                                height: 15px; float: left;">
                            </div>
                            <div id="BottomArrow1" style="width: 25%; height: 15px; float: left;">
                            </div>
                            <div id="BottomArrow2" style="width: 25%; height: 15px; float: left;">
                            </div>
                            <div id="BottomArrow3" style="width: 25%; height: 15px; float: left;">
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom: 0px !important;" class="coda-slider-wrapper">
                        <div class="coda-slider-wrapper">
                            <div class="coda-slider preload" id="coda-slider-1">
                                <div class="panelSlider">
                                    <div class="panel-wrapper">
                                        <h2 style="display: none;" class="title">
                                            Account</h2>
                                        <div class="MemberArea_PageHeading" style="padding-left: 15px;">
                                            Account</div>
                                        <div style="font-size: 15px; padding-left: 15px; padding-top: 5px;">
                                            My Account Information</div>
                                        <asp:UpdatePanel ID="upOneMore" runat="server">
                                            <ContentTemplate>
                                                <div>
                                                    <asp:HiddenField ID="hfCountry" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hfProvince" runat="server" Value="0" />
                                                    <div>
                                                        <div style="float: left; width: auto; margin-right: 25px;">
                                                            <div style="padding-left: 15px; padding-top: 25px;">
                                                                <div class="clear" align="left" style="text-align: left; width: 100%; font-family: Arial;
                                                                    font-size: 16px; padding-bottom: 10px;">
                                                                    <asp:Image ID="imgGridMessage" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                                                                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="#a3cb22" Font-Bold="true"
                                                                        CssClass="fontStyle"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Email
                                                                    </div>
                                                                    <div style="margin-bottom: 5px;">
                                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="TextBox" AutoPostBack="true"
                                                                            OnTextChanged="txtUserName_Changed"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div id="UserPasswordDiv">
                                                                    <div>
                                                                        <div class="ItemHiding">
                                                                            Old Password
                                                                        </div>
                                                                        <div style="margin-bottom: 5px;">
                                                                            <asp:TextBox onfocus="this.className='TextBox'" runat="server" ID="OldPassword" CssClass="TextBox"
                                                                                TextMode="Password" />
                                                                        </div>
                                                                    </div>
                                                                    <div>
                                                                        <div class="ItemHiding">
                                                                            New Password
                                                                        </div>
                                                                        <div style="margin-bottom: 5px;">
                                                                            <asp:TextBox ID="NewPassword" onfocus="this.className='TextBox'" runat="server" CssClass="TextBox"
                                                                                TextMode="Password"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div>
                                                                        <div class="ItemHiding">
                                                                            Confirm New Password
                                                                        </div>
                                                                        <div style="margin-bottom: 5px;">
                                                                            <asp:TextBox ID="ConfirmNewPassword" onfocus="this.className='TextBox'" runat="server"
                                                                                CssClass="TextBox" TextMode="Password"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div style="padding-top: 5px;">
                                                                        <asp:Button OnClientClick="return ChangePassword();" CausesValidation="true" ID="BtnChangeUserPassword"
                                                                            OnClick="BtnChangeUserPassword_Click" CssClass="button big primary" runat="server"
                                                                            Text="Update" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="panelSlider">
                                        <div class="panel-wrapper">
                                            <h2 style="display: none;" class="title">
                                                Profile</h2>
                                            <div class="MemberArea_PageHeading" style="padding-left: 15px;">
                                                Profile</div>
                                            <div style="font-size: 15px; padding-left: 15px; padding-top: 5px;">
                                                My Profile Information</div>
                                            <div style="clear: both; padding-left: 15px; padding-top: 25px;">
                                                <asp:UpdatePanel ID="upMyProfile" runat="server">
                                                    <ContentTemplate>
                                                        <div>
                                                            <div style="float: left;">
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        First Name
                                                                    </div>
                                                                    <div style="margin-bottom: 5px;">
                                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                                                            ControlToValidate="txtFirstName" ErrorMessage="First Name required!" ValidationGroup="ChangePassword"
                                                                            Display="None"></cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator5">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Last Name
                                                                    </div>
                                                                    <div style="margin-bottom: 5px;">
                                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server"
                                                                            ControlToValidate="txtLastName" ErrorMessage="Last Name required!" ValidationGroup="ChangePassword"
                                                                            Display="None"></cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator7">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Phone No
                                                                    </div>
                                                                    <div style="margin-bottom: 5px;">
                                                                        <asp:TextBox ID="txtPhone1" runat="server" Width="50px" MaxLength="3" CssClass="TextBox"></asp:TextBox>-<asp:TextBox
                                                                            ID="txtPhone2" runat="server" Width="50px" MaxLength="3" CssClass="TextBox"></asp:TextBox>-<asp:TextBox
                                                                                ID="txtPhone3" runat="server" Width="50px" MaxLength="4" CssClass="TextBox"></asp:TextBox>
                                                                        <cc1:CustomValidator ID="cvPhone" runat="server" ControlToValidate="txtPhone3" ValidateEmptyText="true"
                                                                            ClientValidationFunction="validatePhone" SetFocusOnError="true" ValidationGroup="ChangePassword"
                                                                            ErrorMessage="Phone number required in correct format." Display="None">
                                                                        </cc1:CustomValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="vcePhone" runat="server" TargetControlID="cvPhone">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                </div>
                                                                <%--  <div  style="visibility:hidden;">
                                                            <div style="margin-bottom: 5px;">
                                                                <div style="float: left;">
                                                                    <div>
                                                                        <asp:RadioButton title="Select if your are male" Checked="true" runat="server" ID="rbMale"
                                                                            GroupName="Gender" />
                                                                    </div>
                                                                    <div class="ItemHiding">
                                                                        Male
                                                                    </div>
                                                                </div>
                                                                <div style="float: left; margin-left: 35px;">
                                                                    <div>
                                                                        <asp:RadioButton title="Select if you are female" runat="server" ID="rbFemale" GroupName="Gender" />
                                                                    </div>
                                                                    <div class="ItemHiding">
                                                                        Female
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                                <%--         <div style="visibility:hidden;">
                                                            <div class="ItemHiding" >
                                                                Age
                                                            </div>
                                                            <div style="padding-bottom: 5px;">
                                                                <asp:DropDownList ID="dlAge" runat="server" Height="30px" CssClass="TextBox">
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
                                                        </div>--%>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        How you heard about us
                                                                    </div>
                                                                    <div style="padding-bottom: 5px;">
                                                                        <asp:DropDownList ID="dlhowyouHeared" Height="30px" runat="server" CssClass="TextBox">
                                                                            <asp:ListItem Selected="True" Value="friend or family">Friend or Family</asp:ListItem>
                                                                            <asp:ListItem Value="facebook">Facebook</asp:ListItem>
                                                                            <asp:ListItem Value="twitter">Twitter</asp:ListItem>
                                                                            <asp:ListItem Value="google">Google</asp:ListItem>
                                                                            <asp:ListItem Value="press">Press</asp:ListItem>
                                                                            <asp:ListItem Value="blog">Blog</asp:ListItem>
                                                                            <asp:ListItem Value="participating merchant">Participating Merchant</asp:ListItem>
                                                                            <asp:ListItem Value="other">Other</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <%--<div style="visibility:hidden;">
                                                            <div>
                                                                <div class="ItemHiding">
                                                                    Postal/Zip Code
                                                                </div>
                                                                <div style="padding-bottom: 5px;">
                                                                    <asp:TextBox ID="txtZipCode" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Country
                                                                    </div>
                                                                    <div style="padding-bottom: 5px;">
                                                                        <asp:DropDownList ID="ddlCountry" Height="30px" CssClass="TextBox" runat="server"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Canada" Value="2" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="United States" Value="1"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Province/State
                                                                    </div>
                                                                    <div style="padding-bottom: 5px;">
                                                                        <asp:DropDownList ID="ddlProvinceLive" Height="30px" CssClass="TextBox" runat="server"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlProvinceLive_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <cc1:RequiredFieldValidator ID="cvProvince" SetFocusOnError="true" InitialValue="Select One"
                                                                            runat="server" ControlToValidate="ddlProvinceLive" ErrorMessage="Province required!"
                                                                            ValidationGroup="ChangePassword" Display="None">                                                                                                       
                                                                        </cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="vceProvince" TargetControlID="cvProvince" runat="server">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Home City
                                                                    </div>
                                                                    <div style="padding-bottom: 5px;">
                                                                        <asp:DropDownList ID="ddlCity" Height="30px" CssClass="TextBox" runat="server">
                                                                        </asp:DropDownList>
                                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" InitialValue="Select One"
                                                                            runat="server" ControlToValidate="ddlCity" ErrorMessage="Select City" ValidationGroup="ChangePassword"
                                                                            Display="None">                                                                                                     
                                                                        </cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator6"
                                                                            runat="server">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                </div>
                                                                <div style="clear: both;">
                                                                    <div style="padding-top: 20px;">
                                                                        <asp:Button runat="server" ID="btnChange"
                                                                            CssClass="button big primary" Text="Save" ValidationGroup="ChangePassword" OnClick="btnChange_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div style="float: right; margin-right: 45px;">
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Profile Picture
                                                                    </div>
                                                                    <div style="padding-bottom: 5px;">
                                                                        <asp:Image ID="imgProfilePic" Style="border: 2px solid #CCCCCC;" runat="server" Height="80px"
                                                                            Width="80px" />
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Choose Profile Picture
                                                                    </div>
                                                                    <div style="padding-bottom: 5px;">
                                                                        <asp:UpdatePanel ID="upImage" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:FileUpload CssClass="txtboxwidth" ID="fuUserProfilePic" runat="server" />
                                                                                <cc1:CustomValidator ID="cvfpChange" runat="server" ClientValidationFunction="ImageValidation"
                                                                                    ControlToValidate="fuUserProfilePic" Display="None" ValidateEmptyText="false"
                                                                                    ValidationGroup="CreateUserWizard1" ErrorMessage="" SetFocusOnError="True"></cc1:CustomValidator>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="btnChange" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panelSlider">
                                        <div class="panel-wrapper">
                                            <h2 style="display: none;" class="title">
                                                Credit Cards</h2>
                                            <div style="overflow: hidden; width: 500px; float: left;">
                                                <div style="padding-left: 15px;">
                                                    <div class="MemberArea_PageHeading">
                                                        Credit Card
                                                    </div>
                                                    <div style="padding-top: 5px; font-size: 15px;">
                                                        My Credit Cards</div>
                                                    <div style="clear: both; padding-top: 20px;">
                                                        <div id="BtnAddCraditCard" onclick="javascript:AddCraditCard();" class="button primary big">
                                                            Add Credit Card</div>
                                                    </div>
                                                    <div id="DivCraditCards" style="display: none;">
                                                        <asp:UpdatePanel ID="upnlEditCustomer" runat="server">
                                                            <ContentTemplate>
                                                                <div style="padding-top: 25px;">
                                                                    <div class="ItemHiding">
                                                                        Billing Name
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both;">
                                                                        <asp:TextBox ID="txtFirstNameCC" onfocus="this.className='TextBox'" CssClass="TextBox"
                                                                            Columns="40" MaxLength="50" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Billing Address
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both;">
                                                                        <asp:TextBox ID="txtBillingAddress" onfocus="this.className='TextBox'" CssClass="TextBox"
                                                                            Columns="40" MaxLength="50" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Card Number
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both;">
                                                                        <asp:TextBox ID="txCardNumner" onfocus="this.className='TextBox'" CssClass="TextBox"
                                                                            Columns="40" MaxLength="50" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Security Code
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both;">
                                                                        <asp:TextBox ID="txtCCVNumber" onfocus="this.className='TextBox'" CssClass="TextBox"
                                                                            Columns="20" MaxLength="20" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Expiration Date
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both; overflow: hidden;">
                                                                        <div style="float: left;">
                                                                            <asp:DropDownList ID="ddlMonth" CssClass="TextBox" runat="server" Height="30" Width="125">
                                                                                <asp:ListItem Value="01" Selected="True">01</asp:ListItem>
                                                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div style="float: left; padding-left: 10px;">
                                                                            <asp:DropDownList ID="ddlYear" CssClass="TextBox" runat="server" Height="30" Width="145">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div style="float: left; padding-left: 5px; color: Red;">
                                                                            <asp:Label ID="lblDateError" runat="server" Text="*" Visible="false"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        City
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both;">
                                                                        <asp:TextBox ID="txtCity" onfocus="this.className='TextBox'" CssClass="TextBox" Columns="40"
                                                                            MaxLength="50" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        State
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both;">
                                                                        <asp:DropDownList CssClass="TextBox" ID="ddlState" runat="server" Height="30">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="ItemHiding">
                                                                        Postal Code
                                                                    </div>
                                                                    <div style="padding-bottom: 5px; clear: both;">
                                                                        <asp:TextBox ID="txtPostalCode" onfocus="this.className='TextBox'" CssClass="TextBox"
                                                                            Columns="20" MaxLength="20" runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div style="margin-top: 20px; clear: both">
                                                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" OnClientClick="return CraditCardValidation();"
                                                                        CssClass="button big primary" ValidationGroup="CC" Text="Save" runat="server"
                                                                        CausesValidation="true" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
                                                    <ContentTemplate>
                                                        <asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div id="AddCarditCard" style="clear: both;">
                                                                <asp:PlaceHolder ID="phrEditCustomer" runat="server">
                                                                    <div class="contactus_table" style="font-size: 16px; color: #636363; font-weight: bold;
                                                                        margin-right: 25px; font-family: Arial; margin-bottom: 30px;">
                                                                    </div>
                                                                </asp:PlaceHolder>
                                                            </div>
                                                            <div style="clear: both; margin-right: 15px; margin-left: 15px;">
                                                                <div style="clear: both;" id="CraditCardGrid">
                                                                    <div style="padding-bottom: 10px;" class="MemberArea_PageHeading">
                                                                        Billing Info</div>
                                                                    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
                                                                        <ContentTemplate>
                                                                            <div style="clear: both;">
                                                                                <table cellspacing="0" cellpadding="0" style="width: 100%;" class="DetailPageTopDiv">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <b><span style="padding-left: 5px;">Credit Card</span></b>
                                                                                            </td>
                                                                                            <td>
                                                                                                <b><span style="padding-left: 255px;">Action</span></b>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                            <div style="float: left; border-top: none; width: 100%">
                                                                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="10"
                                                                                    CellSpacing="0" OnRowDataBound="gvCustomer_RowDataBound" GridLines="None" ShowHeader="false"
                                                                                    OnRowCommand="gvCustomers_RowCommand">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Your Credit Cards">
                                                                                            <ItemTemplate>
                                                                                                <div style="font-size: 15px; width: 300px; text-align: left;">
                                                                                                    <%# GetCardExplain(Eval("ccInfoNumber"))%>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Actions">
                                                                                            <ItemTemplate>
                                                                                                <div class="button-group">
                                                                                                    <asp:LinkButton ID="btnEdit" CssClass="pill button icon approve" Text="Edit" CommandName="EditCustomer"
                                                                                                        CausesValidation="false" CommandArgument='<%#Eval("ccInfoID")%>' runat="server"></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="btnDelete" CssClass="pill button icon danger trash" Text="Delete"
                                                                                                        OnClientClick='return confirm("Are you sure you want to delete this info?");'
                                                                                                        CommandName="DeleteCustomer" CausesValidation="false" CommandArgument='<%#Eval("ccInfoID")%>'
                                                                                                        runat="server"></asp:LinkButton>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                            <asp:LinkButton ID="btnRefreshGrid" CausesValidation="false" OnClick="btnRefreshGrid_Click"
                                                                                Style="display: none" runat="server"></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div style="width: 480px; float: right;">
                                                <%--Accourdian--%>
                                                <div style="clear: both; width: 465px;">
                                                    <div style="clear: both; width: 465px;">
                                                        <div class="DetailTheDetailDiv">
                                                            <div class="MemberArea_PageHeading" style="float: left;">
                                                                Credit Card FAQ
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="clear: both; overflow: hidden;">
                                                    <div style="clear: both; overflow: hidden; width: 465px; padding-top: 10px;">
                                                        <div id="accordion">
                                                            <h3>
                                                                <a class="ExpandPanel" style="font-size: 13px;">Are you saving my credit card information?</a></h3>
                                                            <div>
                                                                <p class="howtoReferNormalTxt">
                                                                    No, we do not save credit card information. Bylaw, we are only allowed to save last
                                                                    4 digits on your credit card number. The remainings are encrypted info we do not
                                                                    have access of and they goes straight to the payment gateway.
                                                                </p>
                                                            </div>
                                                            <h3>
                                                                <a class="ExpandPanel" href="#" style="font-size: 13px;">What kind of encryption you
                                                                    using?</a></h3>
                                                            <div>
                                                                <p class="howtoReferNormalTxt">
                                                                    We use 256 bits Verisign <a href="http://www.symantec.com" class="FAQInnerBodyLink" target="_blank">SSL</a>
                                                                    certificate to ensure the highest standard of data fortification. Information pass
                                                                    through all pages on Tastygo with “https” are encrypted with industry standards
                                                                    and govern bylaw.
                                                                </p>
                                                            </div>
                                                            <h3>
                                                                <a class="ExpandPanel" href="#" style="font-size: 13px;">How do I edit/remove my card
                                                                    info?</a></h3>
                                                            <div>
                                                                <p class="howtoReferNormalTxt">
                                                                    Simply click the edit/remove button under the action tab, and you’ll be able to
                                                                    do so. If you are not sure, you can always contact us at <a class="FAQInnerBodyLink" href="mailto:support@tazzling.com">
                                                                        support@tazzling.com</a> or call <span style="color:#E17009 !important;font-weight:bold;">1-855-295-1771</span>.
                                                                </p>
                                                            </div>
                                                          
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--End Accourdian--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panelSlider">
                                        <div class="panel-wrapper">
                                            <h2 style="display: none;" class="title">
                                                Connections xx</h2>
                                            <asp:UpdatePanel ID="uppanelfb" runat="server">
                                                <ContentTemplate>
                                                    <div class="MemberArea_PageHeading" style="padding-left: 15px;">
                                                        Facebook</div>
                                                    <div style="padding-left: 15px; font-size: 15px; padding-top: 5px;">
                                                        My facebook account settings</div>
                                                    <div style="padding-left: 15px; padding-top: 25px;">
                                                        <asp:Panel Style="clear: both;" ID="pnlFacebookConnect" runat="server">
                                                            <div id="FBConnectButton">
                                                                <div style="float: left; text-align: center;">
                                                                    <asp:Label ID="Label27" Style="color: #636363; text-align: left; font-size: 15px;"
                                                                        runat="server" Text="Tastygo purchases are better when shared with friends. Connecting to Facebook makes it easy to do just that."></asp:Label>
                                                                </div>
                                                                <div style="clear: both; padding-top: 10px;">
                                                                    <a href="javascript:login();">
                                                                        <img src="Images/BtnConnectWithFacebook.png" alt="Facebook Connect" />
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <div style="display: none;" id="FBPleaseWait">
                                                                <div class="button-group">
                                                                    <div id="DoneTag1" style="cursor: default !important;" class="DealTagDone primary icon LoadingTag">
                                                                        Please wait, we are processing on your request....
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel Style="clear: both;" ID="pnlFacebookStuff" Visible="false" runat="server">
                                                            <div id="MainFBYes" style='<%=ViewState["pnlYesShare"]%>' class="button-group">
                                                                <div id="SubFBYes" style="cursor: default !important;" class="CityNameButton primary">
                                                                    Do you want to share your Tastygo purchases on Facebook ?</div>
                                                                <a href="javascript:UpdateFBShare(true);" style="height: 15px; font-size: 11px; font-family: Sans-Serif;
                                                                    border-color: #3072B3 #3072B3 #2A65A0; border-style: solid; border-width: 1px;"
                                                                    class="button green icon approve">Yes</a>
                                                            </div>
                                                            <div id="MainFBNo" style='<%=ViewState["PnlNoShare"]%> margin-left: 0px !important'
                                                                class="button-group">
                                                                <div id="SubFBNo" style="cursor: default !important;" class="CityNameButton primary">
                                                                    Do you want to share your Tastygo purchases on Facebook ?</div>
                                                                <a href="javascript:UpdateFBShare(false);" style="height: 15px; font-size: 11px;
                                                                    font-family: Sans-Serif; border-color: #3072B3 #3072B3 #2A65A0; border-style: solid;
                                                                    border-width: 1px;" class="button danger icon remove">No</a>
                                                            </div>
                                                            <div id="oldstyle" style="display: none;">
                                                                <div style="float: left; line-height: 23px; margin-right: 10px; text-align: center;">
                                                                    <asp:Label ID="Label5" Style="color: #636363; font-family: Arial; font-size: 16px;
                                                                        font-weight: bold;" runat="server" Text="Share my Tastygo purchases on Facebook"></asp:Label></div>
                                                                <div style="float: left;">
                                                                    <asp:CheckBox ID="cbFBShare" runat="server" Checked="true" /></div>
                                                                <div style="float: left; margin-top: 5px; clear: both;">
                                                                    <asp:Button ID="btnSaveFBShare" CssClass="loginsubmitbutton_new" runat="server" Text="Save" />
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <!-- .coda-slider -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id='Notify' class='bottom-right' style='position: fixed; bottom: 0px; right: 0px;
            font-size: 11px !important;'>
        </div>
        <asp:Literal ID="ltFacebook" runat="server"></asp:Literal>
    </div>
</asp:Content>
