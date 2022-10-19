<%@ Page Title="My Account" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="MyAccountSetting.aspx.cs" Inherits="MyAccountSetting" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/coda-slider-2.0.css" type="text/css" media="screen" />
    <script type="text/javascript" src="JS/jquery.coda-slider-2.0.js"></script>
    <link href="CSS/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.qtip.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.jgrowl.js" type="text/javascript"></script>
    <script src="JS/jquery.iphone-switch.js" type="text/javascript"></script>
    <script src="JS/jquery.watermarkinput.js" type="text/javascript"></script>
    <script src="JS/jquery.simple-watermark.js" type="text/javascript"></script>
    <script src="js/jquery.simplemodal.js" type="text/javascript"></script>
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <style type="text/css">
        .leftCaptionControl
        {
            float: left;
            width: 20%;
            padding-top: 10px;
        }
        
        .captionValue
        {
            float: left;
            vertical-align: middle;
            padding-top: 10px;
            width: 720px;
        }
        
        .divErrorBox
        {
            float: left;
            font-size: 10px;
            color: Red;
            display: none;
            padding: 10px 0px 0px 25px;
            vertical-align: top;
        }
        
        .imgButtonPadding
        {
            padding-left: 195px;
        }
        .leftCaption
        {
            float: left;
            width: 20%;
            text-align: right;
            padding-bottom: 10px;
            padding-top: 5px;
        }
        
        .rightCaption
        {
            float: left;
            padding: 0px 0px 10px 15px;
        }
        
        .DisplayDiv
        {
            display: none;
            min-height: 40px;
            padding: 20px 0 20px 50px;
        }
        .editControl
        {
            text-align: left;
            width: 3%;
            text-align: right;
            vertical-align: middle;
            float: right;
            margin-top: 10px;
        }
        .outerDiv
        {
            /*border: 1px solid #D1D6DC;*/
            overflow: hidden;
            margin-bottom: 15px;
            background-color:White;
        }
        
        .innerDivContent
        {
            overflow: hidden;
            min-height: 40px;
        }
        
        .bottomBorder
        {
            border-bottom: 1px solid #D1D6DC;
        }
        
        .padding15
        {
            padding: 15px;
        }
        
        .MyAccountInner
        {
            font-size: 18px;
            color: #363636;
        }
        
        .MyAccountInner .span
        {
            color: #DD0016;
        }
        
        
        .MyAccountHead
        {
            font-weight: bold;
            font-size: 24px;
            color: #363636;
            padding-top:30px;
            padding-left:15px;
            padding-bottom:20px;
        }
        
        .MyAccountHead span
        {
            color: #DD0016;
        }
        
        .clear
        {
            clear: both;
        }
        .NormalBeforeEdit
        {
            background-image: url('Images/edit.png');
            background-repeat: no-repeat;
            padding: 10px;
        }
        
        .NormalBeforeEdit:hover
        {
            background-image: url('Images/edit-hov.png');
        }
        
        .NormalAfterEdit
        {
            background-image: url('Images/view.png');
            background-repeat: no-repeat;
            height: 17px;
            width: 25px;
            padding-right: 30px;
        }
        .NormalAfterEdit:hover
        {
            background-image: url('Images/view-hov.png');
        }
        
        
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
        
        .DropDownBG
        {
            background-image: url('images/textBoxBG.png');
            border-radius: 5px;
            border: 1px solid #D1D6DC;
            padding: 4px;
        }
        
        .textBoxBG
        {
            background-image: url('images/textBoxBG.png');
            border-radius: 5px;
            height: 28px;
            width: 220px;
            border: 1px solid #D1D6DC;
        }
        
        .textBoxError
        {
            background-image: url('images/textBoxBG.png');
            border-radius: 5px;
            height: 28px;
            width: 220px;
            border: 2px solid #F6CCDA;
        }
        
        #divPopup
        {
            height: 430px;
            width: 350px;
            padding-left: 20px;
        }
        
        .divPopupCaption
        {
            float: left;
            width: 100px;
            text-align: right;
            padding: 5px 10px 0px 0px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var IsDisplay = 1;
        function ClickImageChange(divDisplay, objClass, labelControl) {
            var CurrentClass = $(objClass).attr("class");
            if (CurrentClass == "NormalAfterEdit") {
                $(objClass).attr("class", "NormalBeforeEdit");
            }
            else {
                $(objClass).attr("class", "NormalAfterEdit");
            }
            $("#" + divDisplay).toggle();

            //var ScrollVal = $("body").scrollTop($("#" + divDisplay).css + "px";

            var labelMargin = $("#ctl00_ContentPlaceHolder1_" + labelControl).css("margin-left");
            if (labelMargin == "580px") {
                $("#ctl00_ContentPlaceHolder1_" + labelControl).animate({ marginLeft: "0px" }, 500).css("text-align", "left");
            }
            else {
                $("#ctl00_ContentPlaceHolder1_" + labelControl).animate({ marginLeft: "580px" }, 500).css("text-align", "right");
                $('html,body').animate({ scrollTop: $("#" + divDisplay).parent().parent().offset().top }, 'slow');
            }


        }

        $(document).ready(function () {
            setInterval(function () {
                //    var test = __doPostBack('upTop', '');
            }, 2000);
            $("#OnOff").iphoneSwitch("on", function () {
                UpdateFBShare('True');
            },
            function () {
                UpdateFBShare('False');
            },
            {
                switch_on_container_path: 'Images/iphone_switch_container_off.png'
            });

            $("input[id$='_txtBillingName']").simpleWaterMark("Billing Name");
            $("input[id$='_txtBillingAddress']").simpleWaterMark("Billing Address");
            $("input[id$='_txtCardNumber']").simpleWaterMark("Card Number");
            $("input[id$='_txtSecurityCode']").simpleWaterMark("Security Code");
            $("input[id$='_txtCity']").simpleWaterMark("City");
            $("input[id$='_txtPostalCode']").simpleWaterMark("Postal Code");

            var date = new Date();
            var currentYear = date.getFullYear();
            for (var i = currentYear; i <= currentYear + 10; i++) {
                $('#ctl00_ContentPlaceHolder1_ddlYear').append(new Option(i, i));
            }
        });



        function runPopup(values) {
            //var hfDivPopupValue = $('#ctl00_ContentPlaceHolder1_hfDivPopupValue').val();

            $('#divPopup').modal({
                closeHTML: "<a href='#' title='Close' class='modal-close commentclose' ></a>",
                position: ["20%", ],
                overlayId: 'model-overlay',
                containerId: 'tastygoModal',
                onShow: function (dialog) {
                    var modal = this;
                    $('.yes', dialog.data[0]).click(function () {
                        if ($.isFunction(callback)) {
                            callback.apply();
                        }
                        modal.close();
                    });
                }
            });
            if (values != "" && values.length > 0) {
                var ControlValues = values.split('|');
                //alert(values);
                if (ControlValues.length > 0 && ControlValues != undefined) {
                    $("input[id$='_txtBillingName']").val(ControlValues[0]);
                    $("input[id$='_txtBillingAddress']").val(ControlValues[1]);
                    $("input[id$='_txtCardNumber']").val(ControlValues[2]);
                    $("input[id$='_txtSecurityCode']").val("");
                    $("#ctl00_ContentPlaceHolder1_ddlMonth option:contains(" + ControlValues[3] + ")").attr('selected', 'selected');
                    $("#ctl00_ContentPlaceHolder1_ddlYear option:contains(" + ControlValues[4] + ")").attr('selected', 'selected');
                    $("input[id$='_txtCity']").val(ControlValues[5]);
                    $("#ctl00_ContentPlaceHolder1_ddlState option:contains(" + ControlValues[6] + ")").attr('selected', 'selected');
                    $("input[id$='_txtPostalCode']").val(ControlValues[7]);
                    $("#ctl00_ContentPlaceHolder1_hfCCInfoID").val(ControlValues[8]);
                    $("#ctl00_ContentPlaceHolder1_hfDivPopupValue").val("");
                    //alert(ControlValues[8]);
                }
            }
        }


        function UpdateCreditCardRecord() {
            var txtBillingName = $("input[id$='_txtBillingName']");
            var txtBillingAddress = $("input[id$='_txtBillingAddress']");
            var txtCardNumber = $("input[id$='_txtCardNumber']");
            if ($("input[id$='_txtCardNumber']").val() == "") {
                $("input[id$='_txtCardNumber']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            else {
                var num = $("input[id$='_txtCardNumber']").val();
                $("input[id$='_txtCardNumber']").removeClass("textBoxError").addClass("textBoxBG");
                num = (num + '').replace(/\D+/g, '').split('').reverse();
                if (!num.length) {
                    IsValid = false;
                    $("input[id$='_txtCardNumber']").removeClass("textBoxBG").addClass("textBoxError");
                }
                var total = 0, i;
                for (i = 0; i < num.length; i++) {
                    num[i] = parseInt(num[i])
                    total += i % 2 ? 2 * num[i] - (num[i] > 4 ? 9 : 0) : num[i];
                }
                if ((total % 10) == 0 || (total % 10) == 5) {
                    IsValid = true;
                    txtCardNumber = $("input[id$='_txtCardNumber']");
                }
                else {
                    IsValid = false;
                    $("input[id$='_txtCardNumber']").removeClass("textBoxBG").addClass("textBoxError");
                }
            }

            var txtSecurityCode = $("input[id$='_txtSecurityCode']");
            var ddlMonth = $("select[id$='_ddlMonth']");
            var ddlYear = $("select[id$='_ddlYear']");
            var txtCity = $("input[id$='_txtCity']");
            var ddlState = $("select[id$='_ddlState']");
            var txtPostalCode = $("input[id$='_txtPostalCode']");
            var hfCCInfoID = $("#ctl00_ContentPlaceHolder1_hfCCInfoID");

            //alert(txtPostalCode.val());
            if (txtBillingAddress.val() != "" && txtBillingName.val() != "" && txtCardNumber.val() != "" && txtSecurityCode.val() != ""
            && ddlMonth.length > 0 & ddlYear.length > 0 && txtCity.val() != "" && ddlState.length > 0 && txtPostalCode.val() != "") {
                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?BillingName=" + txtBillingName.val() + "&BillingAddress=" + txtBillingAddress.val() +
                        "&CardNumber=" + txtCardNumber.val() + "&SecurityCode=" + txtSecurityCode.val() + "&Month=" + ddlMonth.val() +
                        "&Year=" + ddlYear.val() + "&City=" + txtCity.val() + "&State=" + ddlState.val() + "&PostalCode=" + txtPostalCode.val() +
                        "&CCInfoID=" + hfCCInfoID.val(),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (msg == "2") {
                            MessegeArea('Record is updated', 'success');
                            $(".modal-close").click();
                            //window.location.reload();
                        }
                        else if (msg == "1") {
                            MessegeArea('Record is inserted', 'success');
                            $(".modal-close").click();
                        }
                        else if (msg == "3") {
                            MessegeArea('Invalid record insert', 'error');
                        }
                    }
                });
            }
            else {
                CraditCardValidation();
            }


        }


        function CraditCardValidation() {

            var IsValid = true;

            if ($("input[id$='_txtCardNumber']").val() == "") {
                $("input[id$='_txtCardNumber']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            else {
                var num = $("input[id$='_txtCardNumber']").val();
                $("input[id$='_txtCardNumber']").removeClass("textBoxError").addClass("textBoxBG");
                num = (num + '').replace(/\D+/g, '').split('').reverse();
                if (!num.length) {
                    IsValid = false;
                    $("input[id$='_txtCardNumber']").removeClass("textBoxBG").addClass("textBoxError");
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
                    $("input[id$='_txtCardNumber']").removeClass("textBoxBG").addClass("textBoxError");
                }
            }

            if ($("input[id$='_txtBillingName']").val() == "") {
                $("input[id$='_txtBillingName']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            else {
                $("input[id$='_txtBillingName']").removeClass("textBoxError").addClass("textBoxBG");
            }

            if ($("input[id$='_txtBillingAddress']").val() == "") {
                $("input[id$='_txtBillingAddress']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            else {
                $("input[id$='_txtBillingAddress']").removeClass("textBoxError").addClass("textBoxBG");
            }

            if ($("input[id$='_txtSecurityCode']").val() == "") {
                $("input[id$='_txtSecurityCode']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            else {
                $("input[id$='_txtSecurityCode']").removeClass("textBoxError").addClass("textBoxBG");
            }

            if ($("input[id$='_txtCity']").val() == "") {
                $("input[id$='_txtCity']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            else {
                $("input[id$='_txtCity']").removeClass("textBoxError").addClass("textBoxBG");
            }

            if ($("input[id$='_txtPostalCode']").val() == "") {
                $("input[id$='_txtPostalCode']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            else {
                $("input[id$='_txtPostalCode']").removeClass("textBoxError").addClass("textBoxBG");
            }
            return IsValid;
        }

        function checkFirstLastName() {
            var isValid = true;
            isValid = accountControlValidate('txtFirstName', 'divImbYourNameSave');
            if (isValid == true) {
                isValid = accountControlValidate('txtLastName', 'divImbYourNameSave');
            }
            return isValid;
        }

        function accountControlValidate(control, imgObj) {
            var isValid = true;
            if ($("#ctl00_ContentPlaceHolder1_" + control).val() == "") {
                $("#" + imgObj).next().show("slow");
                setTimeout(function () {
                    $("#" + imgObj).next().hide("slow");
                }, 5000);
                isValid = false;
            }
            return isValid;
        }

        function compareValidate() {
        
            var isValid = true;
            isValid = accountControlValidate('txtCurrentPassword', 'divImgPasswordSave');
            if (isValid == true) {
                isValid = accountControlValidate('txtNewPassword', 'divImgPasswordSave');
            }
            if (isValid == true) {
                isValid = accountControlValidate('txtConfirmPassword', 'divImgPasswordSave');
            }
            var currentPass = $("input[id$='_txtCurrentPassword']");
            var newPass = $("input[id$='_txtNewPassword']");
            var confirmPass = $("input[id$='_txtConfirmPassword']");
            if (isValid == true) {
                if (currentPass.length <= 0 || currentPass.val().length < 6 || currentPass.val().length > 15 || 
                newPass.length <= 0 || newPass.val().length < 6 || newPass.val().length > 15 ||
                confirmPass.length <= 0 || confirmPass.val().length < 6 || confirmPass.val().length > 15) {
                    MessegeArea('Password requires 6-15 characters', 'error');
                    isValid = false;
                }
            }
            if (isValid == true) {
                var txtNewPassword = $("input[id$='_txtNewPassword']");
                var txtConfirmPassword = $("input[id$='_txtConfirmPassword']");
                if (txtNewPassword.val() != "" && txtConfirmPassword.val() != "") {
                    if (txtNewPassword.val() != txtConfirmPassword.val()) {
                        $("#divCompareMessage").show("slow");
                        setTimeout(function () {
                            $("#divCompareMessage").hide("slow");
                        }, 5000);
                        isValid = false;
                    }
                }
            }
            //alert(isValid);
            return isValid;
        }

        function emailValidate(control) {
            var isValid = true;
            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
            if ($("#ctl00_ContentPlaceHolder1_" + control).val() != "") {
                if (!filter.test($("#ctl00_ContentPlaceHolder1_" + control).val())) {
                    $("#divEmailValidate").show();
                    setTimeout(function () {
                        $("#divEmailValidate").hide("slow");
                    }, 5000);
                    isValid = false;
                }
            }
            return isValid;
        }

        function UpdateFBShare(Check) {

            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?UpdateFBShare=" + Check,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {

                        if (Check == "True") {
                            MessegeArea('Facebook sharing Enabled.', 'success');
                            //alert("Facebook sharing Enabled successfully.");
                        }
                        else {
                            MessegeArea('Facebook sharing Disabled.', 'error');
                            //alert("Facebook sharing Disabled successfully.");
                        }
                    }
                });
            }, 1000);
        }
        




        function DropDownFills() {
            var ddlCountry = $("select[id$='_ddlCountry']");
            var ddlProvinceLive = $("select[id$='_ddlProvinceLive']");            
            if (ddlCountry.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "MyAccountSetting.aspx/FillDropDown",
                    data: "{'countryID':" + ddlCountry.val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d.length);
                        if (data.d != null) {
                            if (data.d.length > 0) {
                                for (var i = 0; i < data.d.length; i++) {
                                    var opt = document.createElement("option");
                                    opt.text = result.d[i].provinceName;
                                    opt.value = result.d[i].provinceId;
                                    //$(opt).attr("IsChildLess", result.d[i].IsChildLess == null ? "" : result.d[i].IsChildLess);
                                    ddlProvince[0].options.add(opt);
                                }
                            }
                        }
                    }
                });
            }
        }

        function DisplayUpdated(control, ConValue) {
            $("#" + control).text(ConValue);
        }


    </script>

    <div class="DetailPage2ndDiv">
        <div style="width: 980px; float: left;">
            <div>
                <div style="overflow: hidden;">
                    <usrCtrl:subMenu ID="subMenu1" runat="server" />
                </div>
                <%--<div style="clear: both; padding-top: 10px; margin-bottom: 10px;">
                    <div class="DetailPageTopDiv">
                        <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                            <div id="PageTitle" class="PageTopText" style="float: left;">
                                My Account Info
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
            <div class="DetailPage2ndDiv" style="padding-top: 0px;">
                <div class="clear MyAccountHead">
                    My <span>Account</span></div>
                <asp:UpdatePanel ID="upAccountPersonal" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="outerDiv">
                            <div class="clear padding15 MyAccountInner">
                                Account <span class="span">Information </span><span style="font-style: italic;
                                    font-size: 13px; color: #363636;">- Customize your account information</span></div>
                            <div class="padding15">
                                <div class="innerDivContent bottomBorder">
                                    <div class="leftCaptionControl">
                                        Email</div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblEmail" runat="server" Text=''></asp:Label></div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divEmail',this,'lblEmail');">
                                        </a>
                                    </div>
                                    <div id="divEmail" class="DisplayDiv clear">
                                        <div class="leftCaption">
                                            Enter new email</div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtNewEmail" runat="server" CssClass="textBoxBG"></asp:TextBox></div>
                                        <div class="clear imgButtonPadding" style="overflow: hidden;">
                                            <div id="divImbEmail" style="float: left;">
                                                <asp:ImageButton ID="imbEmailSave" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="javascript:return accountControlValidate('txtNewEmail', 'divImbEmail'); return emailValidate('txtNewEmail');"
                                                    OnClick="imbEmailSave_Click" />
                                            </div>
                                            <div id="divEmailMessage" class="divErrorBox">
                                                Email can not be blank</div>
                                            <div id="divEmailValidate" class="divErrorBox">
                                                Provide a valid email.</div>
                                            <div id="divEmailUpdate" runat="server" class="divErrorBox">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="padding15">
                                <div class="innerDivContent">
                                    <div class="leftCaptionControl">
                                        Password</div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblPassword" runat="server" Text="********"></asp:Label></div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divPassword', this, 'lblPassword');">
                                        </a>
                                    </div>
                                    <div id="divPassword" class="DisplayDiv clear">
                                        <div style="padding-bottom: 20px;">
                                            <span style="font-style: italic; font-size: 13px; color: #363636; clear: both;">Passwords
                                                are case sensitive. Please check your Caps Lock key</span></div>
                                        <div class="leftCaption">
                                            Enter current Password</div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="textBoxBG" TextMode="Password"></asp:TextBox></div>
                                        <div class="leftCaption clear">
                                            Enter new Password</div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="textBoxBG" TextMode="Password"></asp:TextBox></div>
                                        <div class="leftCaption clear">
                                            Confirm new Password
                                        </div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textBoxBG" TextMode="Password"></asp:TextBox>
                                        </div>
                                        <div class="clear imgButtonPadding">
                                            <div id="divImgPasswordSave" style="float: left; padding-bottom: 15px;">
                                                <asp:ImageButton ID="imbPasswordSave" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="return compareValidate();" OnClick="imbPasswordSave_Click" />
                                            </div>
                                            <div id="divPasswordMessage" class="divErrorBox">
                                                Required fields cannot be left blank.</div>
                                            <div id="divCompareMessage" class="divErrorBox">
                                                Passwords do not match.</div>
                                            <div id="divPasswordUpdate" runat="server" class="divErrorBox">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="padding15">
                        <div class="innerDivContent">
                            <div style="float: left; width: 20%;">
                                Email Address
                            </div>
                            <div style="float: left; width: 50%;">
                                <asp:Label ID="lblEmail" runat="server" Text=''></asp:Label>
                            </div>
                            <div class="editControl">
                                <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divEmail', this);">
                                </a>
                            </div>
                            <div id="divEmail" class="DisplayDiv clear">
                                <div class="leftCaption">
                                    Enter new email address
                                </div>
                                <div class="rightCaption">
                                    <asp:TextBox ID="txtNewEmail" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                </div>
                                <div class="clear imgButtonPadding">
                                    <div id="divImbEmailSave" style="float:left;">
                                        <asp:ImageButton ID="imbEmailSave" runat="server" ImageUrl="~/Images/saveButton.png"  OnClientClick="javascript:accountControlValidate('txtNewEmail', 'divImbEmailSave'); emailValidate('txtNewEmail'); return false;" />
                                    </div>
                                    <div id="divEmailMessage" class="divErrorBox">Please provide an email address.</div>
                                    <div id="divEmailValidate" class="divErrorBox">Provide a valid email.</div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                        </div>
                        <div class="outerDiv">
                            <div class="clear padding15 MyAccountInner">
                                <span>Personal </span><span class="span">Information </span><span style="font-style: italic;
                                    font-size: 13px; color: #363636;">- Customize your personal information</span></div>
                            <div class="padding15">
                                <div class="innerDivContent bottomBorder">
                                    <div class="leftCaptionControl">
                                        Your Name</div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblYourName" runat="server" Text=''></asp:Label></div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divYourName',this, 'lblYourName');">
                                        </a>
                                    </div>
                                    <div id="divYourName" class="DisplayDiv clear">
                                        <div class="leftCaption">
                                            Enter first name</div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="textBoxBG"></asp:TextBox></div>
                                        <div class="leftCaption clear">
                                            Enter last name</div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="textBoxBG"></asp:TextBox></div>
                                        <div class="clear imgButtonPadding">
                                            <div id="divImbYourNameSave" style="float: left; padding-bottom: 15px;">
                                                <asp:ImageButton ID="imbYourNameSave" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="return checkFirstLastName();" OnClick="imbYourNameSave_Click" />
                                            </div>
                                            <div id="divYourNameMessage" class="divErrorBox">
                                                Fields can not be blank.</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="padding15">
                                <div class="innerDivContent bottomBorder">
                                    <div class="leftCaptionControl">
                                        Phone Number</div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblPhoneNumber" runat="server" Text=''></asp:Label></div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divPhoneNumber', this, 'lblPhoneNumber');">
                                        </a>
                                    </div>
                                    <div id="divPhoneNumber" class="DisplayDiv clear">
                                        <div class="leftCaption">
                                            Enter new phone number</div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="textBoxBG"></asp:TextBox></div>
                                        <div class="clear imgButtonPadding">
                                            <div id="divImbPhoneNumberSave" style="float: left; padding-bottom: 15px;">
                                                <asp:ImageButton ID="imbPhoneNumberSave" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="return accountControlValidate('txtPhoneNumber', 'divImbPhoneNumberSave');"
                                                    OnClick="imbPhoneNumberSave_Click" />
                                            </div>
                                            <div id="divPhoneNumberMessage" class="divErrorBox">
                                                Phone number can not be blank.</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="padding15">
                                <div class="innerDivContent bottomBorder">
                                    <div class="leftCaptionControl">
                                        How you hear about us?
                                    </div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblHowYouHear" runat="server" Text=''></asp:Label>
                                    </div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divHowYouHear', this, 'lblHowYouHear');">
                                        </a>
                                    </div>
                                    <div id="divHowYouHear" class="DisplayDiv clear">
                                        <div class="leftCaption">
                                            Enter to edit hear about us
                                        </div>
                                        <div class="rightCaption">
                                            <asp:TextBox ID="txtHowYouHear" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                        </div>
                                        <div class="clear imgButtonPadding">
                                            <div id="divImbHowYouHearSave" style="float: left; padding-bottom: 15px;">
                                                <asp:ImageButton ID="imbHowYouHearSave" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="return accountControlValidate('txtHowYouHear', 'divImbHowYouHearSave');"
                                                    OnClick="imbHowYouHearSave_Click" />
                                            </div>
                                            <div id="divHowYouHearMessage" class="divErrorBox">
                                                Field can not be blank.</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="padding15">
                                <div class="innerDivContent bottomBorder">
                                    <div class="leftCaptionControl">
                                        Country</div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblCountry" runat="server" Text=''></asp:Label></div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divCountry', this, 'lblCountry');">
                                        </a>
                                    </div>
                                    <div id="divCountry" class="DisplayDiv clear">
                                        <div class="leftCaption">
                                            Enter to change country</div>
                                        <div class="rightCaption">
                                            <asp:DropDownList ID="ddlCountry" Height="30px" CssClass="TextBox" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Text="Canada" Value="2" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="United States" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txtCountry" runat="server" CssClass="textBoxBG"></asp:TextBox>--%>
                                        </div>
                                        <div class="clear imgButtonPadding">
                                            <div id="divImbCountrySave" style="float: left; padding-bottom: 15px;">
                                                <asp:ImageButton ID="imbCountrySave" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="return accountControlValidate('ddlCountry', 'divImbCountrySave');"
                                                    OnClick="imbCountrySave_Click" />
                                            </div>
                                            <div id="divCountryMessage" class="divErrorBox">
                                                Country can not be blank.</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="padding15">
                                <div class="innerDivContent bottomBorder">
                                    <div class="leftCaptionControl">
                                        Province/State</div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblProvinceState" runat="server" Text=''></asp:Label></div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divProvinceState', this, 'lblProvinceState');">
                                        </a>
                                    </div>
                                    <div id="divProvinceState" class="DisplayDiv clear">
                                        <div class="leftCaption">
                                            Enter new Province/State</div>
                                        <div class="rightCaption">
                                            <asp:DropDownList ID="ddlProvinceLive" Height="30px" CssClass="TextBox" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlProvinceLive_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <cc1:RequiredFieldValidator ID="cvProvince" SetFocusOnError="true" InitialValue="Select One"
                                                runat="server" ControlToValidate="ddlProvinceLive" ErrorMessage="Province required!"
                                                Display="None">                                                                                                       
                                            </cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="vceProvince" TargetControlID="cvProvince" runat="server">
                                            </cc2:ValidatorCalloutExtender>
                                            <%--<asp:TextBox ID="txtProvinceState" runat="server" CssClass="textBoxBG"></asp:TextBox>--%>
                                        </div>
                                        <div class="clear imgButtonPadding">
                                            <div id="divImbProvinceStateSave" style="float: left; padding-bottom: 15px;">
                                                <asp:ImageButton ID="imbProvinceStateSave" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="return accountControlValidate('ddlProvinceLive', 'divImbProvinceStateSave');"
                                                    OnClick="imbProvinceStateSave_Click" />
                                            </div>
                                            <div id="divProvinceStateMessage" class="divErrorBox">
                                                Province/State can not be blank.</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="padding15">
                                <div class="innerDivContent">
                                    <div class="leftCaptionControl">
                                        Home City</div>
                                    <div class="captionValue">
                                        <asp:Label ID="lblHomeCity" runat="server" Text=''></asp:Label></div>
                                    <div class="editControl">
                                        <a class="NormalBeforeEdit" title="Edit" href="javascript:void(0);" onclick="javascript:ClickImageChange('divHomeCity', this, 'lblHomeCity');">
                                        </a>
                                    </div>
                                    <div id="divHomeCity" class="DisplayDiv clear">
                                        <div class="leftCaption">
                                            Enter new home city</div>
                                        <div class="rightCaption">
                                            <asp:DropDownList ID="ddlCity" Height="30px" CssClass="TextBox" runat="server">
                                            </asp:DropDownList>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" InitialValue="Select One"
                                                runat="server" ControlToValidate="ddlCity" ErrorMessage="Select City" Display="None">
                                            </cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator6"
                                                runat="server">
                                            </cc2:ValidatorCalloutExtender>
                                            <%--<asp:TextBox ID="txtHomeCity" runat="server" CssClass="textBoxBG"></asp:TextBox>--%>
                                        </div>
                                        <div class="clear imgButtonPadding">
                                            <div id="divImbHomeCity" style="float: left; padding-bottom: 15px;">
                                                <asp:ImageButton ID="imbHomeCity" runat="server" ImageUrl="~/Images/saveButton.png"
                                                    OnClientClick="return accountControlValidate('ddlCity', 'divImbHomeCity');" OnClick="imbHomeCity_Click" />
                                            </div>
                                            <div id="divHomeCityMessage" class="divErrorBox">
                                                Home city can not be blank.</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upCreditCard" runat="server">
                    <ContentTemplate>
                        <div class="outerDiv">
                            <div class="clear padding15 MyAccountInner">
                                <span>Credit </span><span class="span">Card </span><span style="font-style: italic;
                                    font-size: 13px; color: #363636;">- My credit cards</span></div>
                            <div class="padding15">
                                <div class="innerDivContent">
                                    <div class="clear" style="border-top: 1px solid #D1D6DC; padding-bottom: 15px; padding-top: 15px;">
                                        <asp:ImageButton ID="imgAddCreditCard" runat="server" ImageUrl="~/Images/AddCreditCards_red.png"
                                            OnClientClick="javascript:runPopup(''); return false;" />
                                    </div>
                                    <div style="float: left; padding: 8px 0px 0px 15px; width: 48%; background-color: #F4F4F4;
                                        min-height: 30px; border: 1px solid #DEE2E6;">
                                        Creadit Card</div>
                                    <div style="float: left; padding: 8px 0px 0px 15px; width: 48%; background-color: #F4F4F4;
                                        min-height: 30px; border: 1px solid #DEE2E6;">
                                        Action</div>
                                    <div style="float: left; border-top: none; width: 100%">
                                        <asp:UpdatePanel ID="upGridView" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="10"
                                                    CellSpacing="0" GridLines="None" ShowHeader="false" OnRowCommand="gvCustomers_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Your Credit Cards">
                                                            <ItemTemplate>
                                                                <div style="font-size: 15px; width: 450px; padding: 10px; text-align: left;">
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="outerDiv">
                    <div class="clear padding15">
                        <span class="MyAccountInner">Connections </span><span style="font-style: italic;
                            font-size: 13px; color: #363636;">- My facebook account settings</span></div>
                    <div class="padding15">
                        <div class="innerDivContent" style="border-top: 1px solid #D1D6DC; padding-bottom: 15px;
                            padding-top: 15px;">
                            <div style="float: left; width: 50%;">
                                Do you want to share your Tastygo purchases on Facebook?</div>
                            <div style="float: left; text-align: left;" id="OnOff">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divPopup" style="display: none;">
                    <asp:UpdatePanel ID="upPopup" runat="server">
                        <ContentTemplate>
                            <div style="overflow: hidden;">
                                <div class="clear padding15 MyAccountInner">
                                    <span>Credit </span><span class="span">Card </span>
                                </div>
                                <div style="overflow: hidden;">
                                    <div class="clear divPopupCaption">
                                        Billing Name</div>
                                    <div style="padding-bottom: 8px; float: left;">
                                        <asp:TextBox ID="txtBillingName" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                    </div>
                                    <div class="clear divPopupCaption">
                                        Billing Address</div>
                                    <div style="padding-bottom: 8px;">
                                        <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                    </div>
                                    <div class="clear divPopupCaption">
                                        Card Number</div>
                                    <div style="padding-bottom: 8px;">
                                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                    </div>
                                    <div class="clear divPopupCaption">
                                        Security Code</div>
                                    <div style="padding-bottom: 8px;">
                                        <asp:TextBox ID="txtSecurityCode" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                    </div>
                                    <div class="clear divPopupCaption">
                                        Expiration Date</div>
                                    <div style="padding-bottom: 5px; overflow: hidden;">
                                        <div style="padding-bottom: 8px;">
                                            <div style="float: left;">
                                                <asp:DropDownList ID="ddlMonth" CssClass="DropDownBG" runat="server" Height="28"
                                                    Width="90">
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
                                                <asp:DropDownList ID="ddlYear" CssClass="DropDownBG" runat="server" Height="28" Width="120">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left; padding-left: 5px; color: Red;">
                                                <asp:Label ID="lblDateError" runat="server" Text="*" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clear divPopupCaption">
                                        City</div>
                                    <div style="padding-bottom: 8px;">
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                    </div>
                                    <div class="clear divPopupCaption">
                                        State</div>
                                    <div style="padding-bottom: 8px;">
                                        <asp:DropDownList CssClass="DropDownBG" ID="ddlState" runat="server" Height="28"
                                            Width="220">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clear divPopupCaption">
                                        Postal Code</div>
                                    <div style="padding-bottom: 8px;">
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textBoxBG"></asp:TextBox>
                                    </div>
                                    <div class="clear" style="padding-left: 110px;">
                                        <asp:ImageButton ID="imbSaveCreditCardInfo" runat="server" ImageUrl="~/Images/save-button.png"
                                            OnClientClick="UpdateCreditCardRecord(); return false;" OnClick="imbSaveCreditCardInfo_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfDivPopupValue" runat="server" />
        <asp:HiddenField ID="hfCCInfoID" runat="server" />
        <asp:HiddenField ID="hfProvince" runat="server" Value="0" />
        <div id='Notify' class='bottom-right' style='position: fixed; bottom: 0px; right: 0px;
            font-size: 11px !important;'>
        </div>
        <asp:Literal ID="ltFacebook" runat="server"></asp:Literal>
    </div>
    </asp:Content>
