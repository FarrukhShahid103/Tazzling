<%@ Page Title="Deal Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="addEditdealManagement.aspx.cs" Inherits="addEditdealManagement"
    ValidateRequest="false" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script src="JS/CalendarControl.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    
    
      function checkForExpiryDate() {            
            if(document.getElementById('ctl00_ContentPlaceHolder1_cbNoExpiryDate').checked)
            {                            
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvExpiryDate'), false);
                
            } 
            else 
            {                               
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvExpiryDate'), true);
            }
        }

        function hideShowDiv(divID) {
            var Commentclass = document.getElementById(divID).getAttribute("class");
            if (Commentclass == "hideTimeDetail") {
                document.getElementById(divID).style.display = "block";
                document.getElementById(divID).setAttribute("class", "showTimeDetail")
            }
            else {
                document.getElementById(divID).style.display = "none";
                document.getElementById(divID).setAttribute("class", "hideTimeDetail");
            }
        }

        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSrchDealTitle').value = '';
            return false;
        }

        function Toggle(commId, imageId) {
            var div = document.getElementById(commId);
            var GetImg = document.getElementById(imageId);
            if (document.getElementById(commId).style.display == 'none') {
                document.getElementById(commId).style.display = 'block';
                document.getElementById(imageId).src = 'Images/expand.gif';
            }
            else {
                document.getElementById(commId).style.display = 'none';
                document.getElementById(imageId).src = 'Images/collapse.gif';
            }
        }

        function ValidateFoodFields(txtFoodName, txtFoodDesc, txtFoodPrice) {

            var foodName = document.getElementById(txtFoodName);
            var foodDesc = document.getElementById(txtFoodDesc);
            var foodPrice = document.getElementById(txtFoodPrice);

            if (foodName.value == "") {
                alert("Please insert food name.");
                foodName.focus();
                return false;
            }
            if (foodDesc.value == "") {
                alert("Please insert food description.");
                foodDesc.focus();
                return false;
            }
            if (foodPrice.value == "") {
                alert("Please insert food price.");
                foodPrice.focus();
                return false;
            }

            if (!IsNumeric(foodPrice.value)) {
                alert("Please insert food price.");
                foodPrice.value = "";
                foodPrice.focus();
                return false;
            }
            return true;


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

        function showDelConfirmBox(intID) {
            document.getElementById('ctl00_ContentPlaceHolder1_ctrlResFeaturedFood2_hidFoodTypeId').value = intID;
            document.getElementById('confirmationBoxBackGround').style.display = 'block';
            document.getElementById('ctl00_ContentPlaceHolder1_ctrlResFeaturedFood2_pnlDelete').style.display = 'block';
            return false;
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
                    return;
                }
            }
            else {
                args.IsValid = false;
                return;
            }
        }

        function priceValidation(oSrc, args) {

            if (args.Value != "") {
                var strPrice = args.Value;

                if (IsNumeric(strPrice)) {
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

        function showAddBox() {
            document.getElementById('confirmationBoxBackGround').style.display = 'block';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlSubItem').style.display = 'block';
        }

        function OpenCalendar(ctrl) {
            var Cal = window.document.getElementById(ctrl);
            showCalendarControl(Cal);
        }

        function preCheckSelected() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (document.getElementById(list[i]).checked) {
                    return (confirm("Are you sure you want to delete the selected record(s)?"));
                }
            }
            alert("Please select the record(s) to delete!");
            return false;
        }

        function checkAll() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            if (document.getElementById("ctl00_ContentPlaceHolder1_gvViewDeals_ctl01_HeaderLevelCheckBox").checked) {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = true;
                }
            }
            else {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = false;
                }
            }
        }
        
        
         function checkReview() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbYelpReviews').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvReviewText'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvReviewLink'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_reReviewLink'), true);
            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvReviewText'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvReviewLink'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_reReviewLink'), false);
            }
        }
        

        function checkChange() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax'), false);
            }
        }

        function checkChange1() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax1').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax1'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax1'), false);
            }
        }

        function checkChange2() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax2').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax2'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax2'), false);
            }
        }

        function checkChange3() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax3').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax3'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax3'), false);
            }
        }

        function checkChange4() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax4').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax4'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax4'), false);
            }
        }
        
         function checkChange5() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax5').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax5'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax5'), false);
            }
        }
        
         function checkChange6() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax6').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax6'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax6'), false);
            }
        }
        
         function checkChange7() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax7').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax7'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax7'), false);
            }
        }
        
         function checkChange8() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax8').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax8'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax8'), false);
            }
        }
        
         function checkChange9() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax9').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax9'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax9'), false);
            }
        }
        
         function checkChange10() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingAndTax10').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax10'), true);

            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvShippingAndTax10'), false);
            }
        }
        
        
                                                                       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="JS/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="JS/jquery.mousewheel-3.0.4.pack.js"></script>

    <script type="text/javascript" src="JS/jquery.fancybox-1.3.4.pack.js"></script>

    <link rel="stylesheet" type="text/css" href="CSS/jquery.fancybox-1.3.4.css" media="screen" />
    <asp:UpdatePanel ID="udpnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                </asp:TextBox>
                <asp:HiddenField ID="hfDealOrder0" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder1" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder2" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder3" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder4" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder5" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder6" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder7" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder8" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder9" Value="0" runat="server" />
                <asp:HiddenField ID="hfDealOrder10" Value="0" runat="server" />
                <div id="divAddNewDeal" style='padding-left: 18px;'>
                    <div id="element-box1">
                        <div class="t">
                            <div class="t">
                                <div class="t">
                                </div>
                            </div>
                        </div>
                        <div class="m">
                            <div class="m">
                                <div id="popHeader">
                                    <div style="float: left">
                                        <asp:Label ID="lblDealInfoHeading" Text="Add New Deal Info" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <table cellpadding="2" cellspacing="2" width="100%" class="fontStyle" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td align="left" colspan="4">
                                                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                                                    <div style="float: left; padding-right: 5px">
                                                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                                                    </div>
                                                    <div class="floatLeft">
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblBusinessULRTitle" runat="server" Text="Business URL"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-left: 5px;">
                                                <asp:Label ID="lblBusinessURL" Font-Size="14px" Font-Bold="true" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:HiddenField ID="hfResturnatID" Value="0" runat="server" />
                                                <asp:HiddenField ID="hfDealId" Value="0" runat="server" />
                                                <asp:Label ID="Label6" runat="server" Text="Deal for Design"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:CheckBox ID="cbForDesign" runat="server" Checked="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" Text="URL Title"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtURLTitle" runat="server" CssClass="txtForm" Width="258px" AutoPostBack="true"
                                                    OnTextChanged="txtURLTitle_Changed" MaxLength="200"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtURLTitle"
                                                    ErrorMessage="URL Title required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator8">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator32" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtURLTitle" ErrorMessage="& not allowed."
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^[^&]*$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender93" runat="server" TargetControlID="RegularExpressionValidator32">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTopTitle" runat="server" Text="Top Title"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtTopTitle" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTopTitle"
                                                    ErrorMessage="Title required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender24" TargetControlID="RequiredFieldValidator11">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" Text="Short Title"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShortTitle" runat="server" CssClass="txtForm" Width="258px" MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShortTitle"
                                                    ErrorMessage="Short Title required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Title"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="txtForm" Width="258px" TextMode="MultiLine"
                                                    Height="60px" MaxLength="200"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitle"
                                                    ErrorMessage="Title required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator5">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label3" runat="server" Text="Fine Print"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <asp:TextBox ID="txtFinePrint" TextMode="MultiLine" runat="server" Width="626px"
                                                    Height="150px">
                                                </asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFinePrint"
                                                    ErrorMessage="Fine Print required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator6">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label4" runat="server" Text="Deal Hightlights"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <asp:TextBox ID="txtDealHightlights" runat="server" Width="626px" TextMode="MultiLine"
                                                    Height="150px">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label5" runat="server" Text="Description"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <CKEditor:CKEditorControl ID="elm1" Height="460px" Width="626px" runat="server"></CKEditor:CKEditorControl>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="lblHowToUse" runat="server" Text="How To Use"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <asp:TextBox ID="txtHowToUse" runat="server" Width="626px" Height="120px" TextMode="MultiLine">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; padding-top: 35px;">
                                                <asp:Label ID="Label22" runat="server" Text="Select City"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <asp:DataList CssClass="CityList" runat="server" ID="dlCities" DataKeyField="cityid"
                                                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" RepeatColumns="2"
                                                    RepeatDirection="Horizontal" CellPadding="20" CellSpacing="0" GridLines="None"
                                                    HorizontalAlign="Left" ShowHeader="false" RowStyle-HorizontalAlign="Left" OnItemDataBound="dlCities_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div>
                                                            <div style="float: left; width: 25px;">
                                                                <asp:CheckBox ID="chkbxSelect" runat="server" />
                                                            </div>
                                                            <div id="divCityName" style="float: left;">
                                                                <asp:Label ID="lblCity" Font-Bold="true" Text='<% # Eval("cityname") %>' runat="server" />
                                                                <asp:Label ID="lblCityID" runat="server" Text='<% # Eval("cityId") %>' Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <asp:Panel ID="pnlDealTimeDetail" runat="server">
                                                            <div style="clear: both; padding-top: 5px;">
                                                                <div>
                                                                    <asp:Label ID="lbldlStartTime" runat="server" Text="Start Time"></asp:Label>
                                                                </div>
                                                                <div style="padding-top: 2px;">
                                                                    <asp:TextBox ID="txtdlStartDate" runat="server" CssClass="txtForm" Width="92px" MaxLength="12"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlDLStartHH" runat="server">
                                                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlDLStartMM" runat="server">
                                                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                        <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                                                        <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                                                        <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                                                        <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                                        <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                                        <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                                                        <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                                                        <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                        <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                                                        <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                                                        <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                                                        <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                                                        <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                                        <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                                                        <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                                                        <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                                        <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                        <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                                                        <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                                                        <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                                                        <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                                                        <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                                        <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                                                        <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                                                        <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                                                        <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlDLStartPortion" runat="server">
                                                                        <asp:ListItem Selected="True" Text="AM" Value="AM"></asp:ListItem>
                                                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div style="clear: both; padding-top: 5px;">
                                                                <div>
                                                                    <asp:Label ID="lblDLEndTime" runat="server" Text="End Time"></asp:Label>
                                                                </div>
                                                                <div style="padding-top: 2px;">
                                                                    <asp:TextBox ID="txtDLEndDate" runat="server" CssClass="txtForm" Width="92px" MaxLength="12"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlDLEndHH" runat="server">
                                                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlDLEndMM" runat="server">
                                                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                        <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                                                        <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                                                        <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                                                        <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                                        <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                                        <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                                                        <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                                                        <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                        <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                                                        <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                                                        <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                                                        <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                                                        <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                                        <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                                                        <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                                                        <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                                        <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                        <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                                                        <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                                                        <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                                                        <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                                                        <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                                        <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                                                        <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                                                        <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                                                        <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlDLEndPortion" runat="server">
                                                                        <asp:ListItem Selected="True" Text="AM" Value="AM"></asp:ListItem>
                                                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div style="clear: both; padding-top: 5px;">
                                                                <div>
                                                                    <asp:Label ID="lblDLSelectSlot" runat="server" Text="Deal Slot"></asp:Label>
                                                                </div>
                                                                <div style="padding-top: 2px;">
                                                                    <asp:DropDownList ID="ddlDLSideDeal" runat="server" Width="72px">
                                                                        <asp:ListItem Text="Slot 1" Value="1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 31" Value="31"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 32" Value="32"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 33" Value="33"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 34" Value="34"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 35" Value="35"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 36" Value="36"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 37" Value="37"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 38" Value="38"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 39" Value="39"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 40" Value="40"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 41" Value="41"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 42" Value="42"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 43" Value="43"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 44" Value="44"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 45" Value="45"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 46" Value="46"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 47" Value="47"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 48" Value="48"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 49" Value="49"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 50" Value="50"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 51" Value="51"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 52" Value="52"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 53" Value="53"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 54" Value="54"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 55" Value="55"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 56" Value="56"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 57" Value="57"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 58" Value="58"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 59" Value="59"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 60" Value="60"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 61" Value="61"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 62" Value="62"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 63" Value="63"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 64" Value="64"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 65" Value="65"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 66" Value="66"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 67" Value="67"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 68" Value="68"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 69" Value="69"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 70" Value="70"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 71" Value="71"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 72" Value="72"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 73" Value="73"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 74" Value="74"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 75" Value="75"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 76" Value="76"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 77" Value="77"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 78" Value="78"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 79" Value="79"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 80" Value="80"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 81" Value="81"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 82" Value="82"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 83" Value="83"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 84" Value="84"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 85" Value="85"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 86" Value="86"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 87" Value="87"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 88" Value="88"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 89" Value="89"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 90" Value="90"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 91" Value="91"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 92" Value="92"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 93" Value="93"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 94" Value="94"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 95" Value="95"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 96" Value="96"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 97" Value="97"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 98" Value="98"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 99" Value="99"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 100" Value="100"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 101" Value="101"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 102" Value="102"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 103" Value="103"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 104" Value="104"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 105" Value="105"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 106" Value="106"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 107" Value="107"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 108" Value="108"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 109" Value="109"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 110" Value="110"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 111" Value="111"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 112" Value="112"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 113" Value="113"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 114" Value="114"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 115" Value="115"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 116" Value="116"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 117" Value="117"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 118" Value="118"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 119" Value="119"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 120" Value="120"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 121" Value="121"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 122" Value="122"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 123" Value="123"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 124" Value="124"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 125" Value="125"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 126" Value="126"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 127" Value="127"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 128" Value="128"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 129" Value="129"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 130" Value="130"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 131" Value="131"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 132" Value="132"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 133" Value="133"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 134" Value="134"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 135" Value="135"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 136" Value="136"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 137" Value="137"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 138" Value="138"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 139" Value="139"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 140" Value="140"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 141" Value="141"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 142" Value="142"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 143" Value="143"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 144" Value="144"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 145" Value="145"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 146" Value="146"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 147" Value="147"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 148" Value="148"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 149" Value="149"></asp:ListItem>
                                                                        <asp:ListItem Text="Slot 150" Value="150"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label21" runat="server" Text="Voucher Expiry Date"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtVoucherExpiryDate" runat="server" CssClass="txtForm" Width="92px"
                                                    MaxLength="12" onclick="OpenCalendar('ctl00_ContentPlaceHolder1_txtVoucherExpiryDate');"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvExpiryDate" runat="server" ControlToValidate="txtVoucherExpiryDate"
                                                    ErrorMessage="Voucher Expiry Date required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="vceExpiryDate" TargetControlID="rfvExpiryDate">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="cbNoExpiryDate" runat="server" Text="No Expiry Date" onclick="javascript:checkForExpiryDate();" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTypeOfFood" runat="server" Text="Selling Price ($)"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDealPrice" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvTypeOfFood" runat="server" ControlToValidate="txtDealPrice"
                                                    ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="vceTypeOfFood" TargetControlID="rfvTypeOfFood">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator5" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtDealPrice" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="RegularExpressionValidator5">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblDescription" runat="server" Text="Value Price ($)"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtActualPrice" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvItemDescription" runat="server" ControlToValidate="txtActualPrice"
                                                    ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="vceItemDescription" TargetControlID="rfvItemDescription">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator6" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtActualPrice" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="RegularExpressionValidator6">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label10" runat="server" Text="Minimum no. of Orders"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMinNoOfOrders" runat="server" CssClass="txtForm" Width="150px"
                                                    MaxLength="6"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMinNoOfOrders"
                                                    ErrorMessage="Minimum no. of Orders required" ValidationGroup="FeaturedFood"
                                                    Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator4">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="revMinNoOfOrders" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtMinNoOfOrders" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="vcerevMinNoOfOrders" runat="server" TargetControlID="revMinNoOfOrders">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label11" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMaxNoOfOrders" runat="server" CssClass="txtForm" Width="150px"
                                                    MaxLength="6"></asp:TextBox>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator4" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtMaxNoOfOrders" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RegularExpressionValidator4">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" Text="Maximum Orders Per User"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMaxOrdersPerUser" runat="server" CssClass="txtForm" Width="150px"
                                                    MaxLength="6"></asp:TextBox>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtMaxOrdersPerUser" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RegularExpressionValidator2">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label15" runat="server" Text="Maximum Gifts Per Order"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMaxGiftsPerOrder" runat="server" CssClass="txtForm" Width="150px"
                                                    MaxLength="6"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaxGiftsPerOrder"
                                                    ErrorMessage="Maximum Gifts Per Order required" ValidationGroup="FeaturedFood"
                                                    Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator1">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtMaxGiftsPerOrder" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RegularExpressionValidator3">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label44" runat="server" Text="Our Comission (%)"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtOurComission" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOurComission"
                                                    ErrorMessage="Our Commission(%) required" Display="None" ValidationGroup="FeaturedFood"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender51" TargetControlID="RequiredFieldValidator3">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RXOurComission" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtOurComission" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9]{1,})?$)"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender52" runat="server" TargetControlID="RXOurComission">
                                                </cc2:ValidatorCalloutExtender>
                                                <span>(This does not include 3.9%, for example if contract
                                                    says 30% all inclusive, please put in 26.1)</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label33" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtShippingAndTax" runat="server" CssClass="txtForm" Width="150px"
                                                    MaxLength="3"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvShippingAndTax" runat="server" ControlToValidate="txtShippingAndTax"
                                                    ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                    Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender39" TargetControlID="rfvShippingAndTax">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="reShippingAndTax" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtShippingAndTax" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender40" runat="server" TargetControlID="reShippingAndTax">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label16" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="cbShippingAndTax" runat="server" Text="" onclick="javascript:checkChange();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" Text="Allow Tracking"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="cbTracking" runat="server" Text="" />
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label85" runat="server" Text="Double Points"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="cbDoublePoints" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label39" runat="server" Text="Add Reviews"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="cbYelpReviews" runat="server" Text="" onclick="javascript:checkReview();" />
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label47" runat="server" Text="Review Rate"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlReviewRate" runat="server" CssClass="txtForm" Width="100px">
                                                    <asp:ListItem Selected="True" Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="1.5" Value="1.5"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="2.5" Value="2.5"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="3.5" Value="3.5"></asp:ListItem>
                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="4.5" Value="4.5"></asp:ListItem>
                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label45" runat="server" Text="Review Text"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtReviewText" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvReviewText" runat="server" ControlToValidate="txtReviewText"
                                                    ErrorMessage="Review Text Required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender31" TargetControlID="rfvReviewText">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label46" runat="server" Text="Review Link"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtReviewLink" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvReviewLink" runat="server" ControlToValidate="txtReviewLink"
                                                    ErrorMessage="Review Link Required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender32" TargetControlID="rfvReviewLink">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="reReviewLink" runat="server" ErrorMessage="<span id='cMessage'>Please enter valid URL.<br>e.g. http(s)://abc.com</span>"
                                                    ControlToValidate="txtReviewLink" Display="None" ValidationGroup="FeaturedFood"
                                                    SetFocusOnError="True" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender33" TargetControlID="reReviewLink"
                                                    runat="server">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Deal assign to (SalesMan)
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSalePersonAccountName" runat="server" CssClass="txtForm">
                                                </asp:DropDownList>
                                                <cc1:RequiredFieldValidator runat="server" ID="Rfvsalesaccountname" ControlToValidate="ddlSalePersonAccountName"
                                                    SetFocusOnError="true" ValidationGroup="FeaturedFood" InitialValue="Please Select"
                                                    ErrorMessage="Sales Person Account required." Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="Rfvsalesaccountnamex" runat="server" TargetControlID="Rfvsalesaccountname">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="lblImage" runat="server" Text="Image 1"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="fpDealImage1" runat="server" CssClass="txtForm" Width="313px" />
                                                        <cc1:RequiredFieldValidator ID="rfvDealImage1" runat="server" ControlToValidate="fpDealImage1"
                                                            ErrorMessage="Image required" Display="None" ValidationGroup="FeaturedFood" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="rfvDealImage1">
                                                        </cc2:ValidatorCalloutExtender>
                                                        <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidation"
                                                            ControlToValidate="fpDealImage1" Display="None" ErrorMessage="Invalid file format."
                                                            ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:CustomValidator>
                                                        <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                                            runat="server">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnImgSave" />
                                                        <asp:PostBackTrigger ControlID="BtnPreview" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <img id="imgUpload1" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                                    visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label8" runat="server" Text="Image 2"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="fpDealImage2" runat="server" CssClass="txtForm" Width="313px" />
                                                            <cc1:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ImageValidation"
                                                                ControlToValidate="fpDealImage2" Display="None" ErrorMessage="Invalid file format."
                                                                ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:CustomValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="CustomValidator2"
                                                                runat="server">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnImgSave" />
                                                            <asp:PostBackTrigger ControlID="BtnPreview" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div>
                                                    <div style="float: left;">
                                                        <img id="imgUpload2" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                                            visible="false" /></div>
                                                    <div style="float: left; padding-left: 10px; padding-top: 10px;">
                                                        <asp:LinkButton ID="imgUpload2Remove" OnClick="imgUpload2Remove_Click" CausesValidation="false"
                                                            Font-Size="12px" Font-Names="verdana" Visible="false" runat="server">Remove it</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label9" runat="server" Text="Image 3"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="fpDealImage3" runat="server" CssClass="txtForm" Width="313px" />
                                                            <cc1:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ImageValidation"
                                                                ControlToValidate="fpDealImage3" Display="None" ErrorMessage="Invalid file format."
                                                                ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:CustomValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="CustomValidator3"
                                                                runat="server">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnImgSave" />
                                                            <asp:PostBackTrigger ControlID="BtnPreview" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div>
                                                    <div style="float: left;">
                                                        <img id="imgUpload3" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                                            visible="false" />
                                                    </div>
                                                    <div style="float: left; padding-left: 10px; padding-top: 10px;">
                                                        <asp:LinkButton ID="imgUpload3Remove" OnClick="imgUpload3Remove_Click" CausesValidation="false"
                                                            Font-Size="12px" Font-Names="verdana" Visible="false" runat="server">Remove it</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 5px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:LinkButton ID="lBtn0AddThirdDiv" OnClick="lBtn0AddThirdDiv_Click" CausesValidation="false"
                                                    Font-Size="15px" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left" style="padding-left: 19px;">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 5px;">
                                            </td>
                                        </tr>
                                        <div id="divFirstSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label20" runat="server" Text="Deal Page Title"></asp:Label>
                                                </td>
                                                <td align="left" colspan="4">
                                                    <asp:TextBox ID="txtTitleMain" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTitleMain"
                                                        ErrorMessage="Deal Page Title (if sub deal exits) required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender23" TargetControlID="RequiredFieldValidator10">
                                                    </cc2:ValidatorCalloutExtender>
                                                    (if sub deal exits)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="sds54">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label27" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId1" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label18" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle1" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTitle1"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender35" TargetControlID="RequiredFieldValidator7">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label17" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders1" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders1" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RegularExpressionValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label181" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice1" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" ControlToValidate="txtDealPrice1"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender19" TargetControlID="RequiredFieldValidator71">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator8" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice1" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" TargetControlID="RegularExpressionValidator8">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label19" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice1" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtActualPrice1"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender21" TargetControlID="RequiredFieldValidator9">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator9" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice1" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" TargetControlID="RegularExpressionValidator9">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label34" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax1" runat="server" Text="" onclick="javascript:checkChange1();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label35" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax1" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax1" runat="server" ControlToValidate="txtShippingAndTax1"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4" TargetControlID="rfvShippingAndTax1">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax1" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax1" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender34" runat="server" TargetControlID="reShippingAndTax1">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn1AddThirdDiv" OnClick="lBtn1AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn1DeleteSecondDiv" CausesValidation="false" OnClick="lBtn1DeleteSecondDiv_Click"
                                                        Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divSecondSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div1">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label28" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId2" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label29" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle2" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTitle2"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender351" TargetControlID="RequiredFieldValidator71">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label25" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders2" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator7" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders2" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="RegularExpressionValidator7">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label1811" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice2" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator711" runat="server" ControlToValidate="txtDealPrice2"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender191" TargetControlID="RequiredFieldValidator711">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator81" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice2" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender36" runat="server" TargetControlID="RegularExpressionValidator81">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label30" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice2" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator91" runat="server" ControlToValidate="txtActualPrice2"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender37" TargetControlID="RequiredFieldValidator91">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator92" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice2" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender38" runat="server" TargetControlID="RegularExpressionValidator92">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label37" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax2" runat="server" Text="" onclick="javascript:checkChange2();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label38" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax2" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax2" runat="server" ControlToValidate="txtShippingAndTax2"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender41" TargetControlID="rfvShippingAndTax2">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax2" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax2" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender42" runat="server" TargetControlID="reShippingAndTax2">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 12px;" colspan="5">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn2AddThirdDiv" OnClick="lBtn2AddThirdDiv_Click" Font-Size="15px"
                                                        Font-Names="verdana" CausesValidation="false" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn2DeleteSecondDiv" Font-Size="15px" CausesValidation="false"
                                                        OnClick="lBtn2DeleteSecondDiv_Click" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divThirdSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div>
                                                        <div style="float: left">
                                                            <asp:Label ID="Labelsd27" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId3" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Labelsd18" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle3" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator7ss1" runat="server" ControlToValidate="txtTitle3"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatwerorCalloutExtender351"
                                                        TargetControlID="RequiredFieldValidator7ss1">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label32" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders3" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator12" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders3" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="RegularExpressionValidator12">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label1d811" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice3" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFdsieldValidator711" runat="server" ControlToValidate="txtDealPrice3"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="VawerlidatorCalloutExtender191"
                                                        TargetControlID="RequiredFdsieldValidator711">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpdfressionValidator81" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice3" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValweidatorCalloutExtender20" runat="server" TargetControlID="RegularExpdfressionValidator81">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Labdsfel19" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice3" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFiewerldValidator91" runat="server" ControlToValidate="txtActualPrice3"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="VweralidatorCalloutExtender21" TargetControlID="RequiredFiewerldValidator91">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExdfpressionValidator92" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice3" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtewernder22" runat="server" TargetControlID="RegularExdfpressionValidator92">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label40" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax3" runat="server" Text="" onclick="javascript:checkChange3();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label41" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax3" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax3" runat="server" ControlToValidate="txtShippingAndTax3"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender43" TargetControlID="rfvShippingAndTax3">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax3" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax3" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender44" runat="server" TargetControlID="reShippingAndTax3">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn3AddThirdDiv" OnClick="lBtn3AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn3DeleteSecondDiv" Font-Size="15px" OnClick="lBtn3DeleteSecondDiv_Click"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divForthSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div3">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label23" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId4" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label24" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle4" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTitle4"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender25" TargetControlID="RequiredFieldValidator12">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label36" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders4" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator13" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders4" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" TargetControlID="RegularExpressionValidator13">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label26" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice4" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDealPrice4"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender26" TargetControlID="RequiredFieldValidator14">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator10" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice4" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender27" runat="server" TargetControlID="RegularExpressionValidator10">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label31" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice4" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtActualPrice4"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender28" TargetControlID="RequiredFieldValidator9">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator11" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice4" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" TargetControlID="RegularExpressionValidator11">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label42" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax4" runat="server" Text="" onclick="javascript:checkChange4();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label43" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax4" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax4" runat="server" ControlToValidate="txtShippingAndTax4"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender45" TargetControlID="rfvShippingAndTax4">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax4" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax4" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender46" runat="server" TargetControlID="reShippingAndTax4">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn4AddThirdDiv" OnClick="lBtn4AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn4DeleteSecondDiv" CausesValidation="false" OnClick="lBtn4DeleteSecondDiv_Click"
                                                        Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divFifthSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div4">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label48" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId5" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label49" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle5" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTitle5"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender47" TargetControlID="RequiredFieldValidator13">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label50" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders5" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator14" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders5" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender48" runat="server" TargetControlID="RegularExpressionValidator14">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label51" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice5" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtDealPrice5"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender49" TargetControlID="RequiredFieldValidator17">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator15" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice5" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender50" runat="server" TargetControlID="RegularExpressionValidator15">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label52" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice5" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtActualPrice5"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender53" TargetControlID="RequiredFieldValidator18">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator16" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice5" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender54" runat="server" TargetControlID="RegularExpressionValidator16">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label53" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax5" runat="server" Text="" onclick="javascript:checkChange5();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label54" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax5" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax5" runat="server" ControlToValidate="txtShippingAndTax5"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender55" TargetControlID="rfvShippingAndTax5">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax5" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax5" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender56" runat="server" TargetControlID="reShippingAndTax5">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn5AddThirdDiv" OnClick="lBtn5AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn5DeleteSecondDiv" OnClick="lBtn5DeleteSecondDiv_Click" CausesValidation="false"
                                                        Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divSixthSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div5">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label55" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId6" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label56" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle6" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtTitle6"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender57" TargetControlID="RequiredFieldValidator19">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label57" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders6" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator17" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders6" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender58" runat="server" TargetControlID="RegularExpressionValidator17">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label58" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice6" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtDealPrice6"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender59" TargetControlID="RequiredFieldValidator20">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator18" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice6" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender60" runat="server" TargetControlID="RegularExpressionValidator18">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label59" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice6" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtActualPrice6"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender61" TargetControlID="RequiredFieldValidator21">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator19" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice6" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender62" runat="server" TargetControlID="RegularExpressionValidator19">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label62" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax6" runat="server" Text="" onclick="javascript:checkChange6();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label63" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax6" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax6" runat="server" ControlToValidate="txtShippingAndTax6"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender65" TargetControlID="rfvShippingAndTax6">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax6" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax6" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender66" runat="server" TargetControlID="reShippingAndTax6">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn6AddThirdDiv" OnClick="lBtn6AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn6DeleteSecondDiv" OnClick="lBtn6DeleteSecondDiv_Click" CausesValidation="false"
                                                        Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divSeventhSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div6">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label60" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId7" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label61" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle7" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtTitle7"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender63" TargetControlID="RequiredFieldValidator22">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label64" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders7" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator20" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders7" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender64" runat="server" TargetControlID="RegularExpressionValidator20">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label65" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice7" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtDealPrice7"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender67" TargetControlID="RequiredFieldValidator23">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator21" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice7" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender68" runat="server" TargetControlID="RegularExpressionValidator21">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label66" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice7" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtActualPrice7"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender69" TargetControlID="RequiredFieldValidator24">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator22" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice7" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender70" runat="server" TargetControlID="RegularExpressionValidator22">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label72" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax7" runat="server" Text="" onclick="javascript:checkChange7();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label73" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax7" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax7" runat="server" ControlToValidate="txtShippingAndTax7"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender75" TargetControlID="rfvShippingAndTax7">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax7" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax7" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender76" runat="server" TargetControlID="reShippingAndTax7">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn7AddThirdDiv" OnClick="lBtn7AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn7DeleteSecondDiv" OnClick="lBtn7DeleteSecondDiv_Click" CausesValidation="false"
                                                        Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divEightSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div7">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label67" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId8" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label68" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle8" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtTitle8"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender71" TargetControlID="RequiredFieldValidator25">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label69" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders8" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator23" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders8" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender72" runat="server" TargetControlID="RegularExpressionValidator23">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label70" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice8" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtDealPrice8"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender73" TargetControlID="RequiredFieldValidator26">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator24" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice8" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender74" runat="server" TargetControlID="RegularExpressionValidator24">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label71" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice8" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtActualPrice8"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender77" TargetControlID="RequiredFieldValidator27">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator25" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice8" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender78" runat="server" TargetControlID="RegularExpressionValidator25">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label82" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax8" runat="server" Text="" onclick="javascript:checkChange8();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label83" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax8" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax8" runat="server" ControlToValidate="txtShippingAndTax8"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender85" TargetControlID="rfvShippingAndTax8">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax8" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax8" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender86" runat="server" TargetControlID="reShippingAndTax8">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn8AddThirdDiv" OnClick="lBtn8AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn8DeleteSecondDiv" OnClick="lBtn8DeleteSecondDiv_Click" CausesValidation="false"
                                                        Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divNinthSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div8">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label74" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId9" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label75" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle9" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtTitle9"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender79" TargetControlID="RequiredFieldValidator28">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label76" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders9" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator26" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders9" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender80" runat="server" TargetControlID="RegularExpressionValidator26">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label77" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice9" runat="server" CssClass="txtForm" Width="150px" MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtDealPrice9"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender81" TargetControlID="RequiredFieldValidator29">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator27" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice9" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender82" runat="server" TargetControlID="RegularExpressionValidator27">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label78" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice9" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtActualPrice9"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender83" TargetControlID="RequiredFieldValidator30">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator28" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice9" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender84" runat="server" TargetControlID="RegularExpressionValidator28">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label92" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax9" runat="server" Text="" onclick="javascript:checkChange9();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label93" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax9" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax9" runat="server" ControlToValidate="txtShippingAndTax9"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender95" TargetControlID="rfvShippingAndTax9">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax9" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax9" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender96" runat="server" TargetControlID="reShippingAndTax9">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lBtn9AddThirdDiv" OnClick="lBtn9AddThirdDiv_Click" Font-Size="15px"
                                                        CausesValidation="false" Font-Names="verdana" runat="server">Add new sub deal</asp:LinkButton>
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn9DeleteSecondDiv" OnClick="lBtn9DeleteSecondDiv_Click" CausesValidation="false"
                                                        Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <div id="divTenthSubDeal" runat="server" visible="false">
                                            <tr>
                                                <td colspan="5" style="height: 8px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div id="Div9">
                                                        <div style="float: left">
                                                            <asp:Label ID="Label79" Text="Add New Sub Deal Info" Font-Size="13px" Font-Underline="true"
                                                                Font-Bold="true" runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hfDealId10" Value="0" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label210" runat="server" Text="Sub Deal Title"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTitle10" runat="server" CssClass="txtForm" Width="258px" MaxLength="200"
                                                        TextMode="MultiLine" Height="60px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtTitle10"
                                                        ErrorMessage="Sub Deal Title required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender87" TargetControlID="RequiredFieldValidator31">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label80" runat="server" Text="Maximum no. of Orders"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxNoOfOrders10" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator29" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtMaxNoOfOrders10" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender88" runat="server" TargetControlID="RegularExpressionValidator29">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label81" runat="server" Text="Selling Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDealPrice10" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="9"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator110" runat="server" ControlToValidate="txtDealPrice10"
                                                        ErrorMessage="Selling Price ($)required" ValidationGroup="FeaturedFood" Display="None"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender89" TargetControlID="RequiredFieldValidator110">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator30" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtDealPrice10" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender90" runat="server" TargetControlID="RegularExpressionValidator30">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label84" runat="server" Text="Value Price ($)"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtActualPrice10" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtActualPrice10"
                                                        ErrorMessage="Value Price ($) required" Display="None" ValidationGroup="FeaturedFood"
                                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender91" TargetControlID="RequiredFieldValidator32">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator31" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtActualPrice10" ErrorMessage="Only Numeric value required"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender92" runat="server" TargetControlID="RegularExpressionValidator31">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label102" runat="server" Text="Apply Shipping and Tax"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="cbShippingAndTax10" runat="server" Text="" onclick="javascript:checkChange10();" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label103" runat="server" Text="Shipping and Tax Amount"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingAndTax10" runat="server" CssClass="txtForm" Width="150px"
                                                        MaxLength="3"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvShippingAndTax10" runat="server" ControlToValidate="txtShippingAndTax10"
                                                        ErrorMessage="Shipping and Tax Amount required" ValidationGroup="FeaturedFood"
                                                        Display="None" SetFocusOnError="true" Enabled="false"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender105" TargetControlID="rfvShippingAndTax10">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="reShippingAndTax10" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtShippingAndTax10" ErrorMessage="Only Numeric value required e.g.(1, 2.5)"
                                                        ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^([0-9]*|\d*\.\d{1}?\d*)$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender106" runat="server" TargetControlID="reShippingAndTax10">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="height: 12px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="right">
                                                </td>
                                                <td align="left" style="padding-left: 19px;">
                                                    <asp:LinkButton ID="lBtn10DeleteSecondDiv" OnClick="lBtn10DeleteSecondDiv_Click"
                                                        CausesValidation="false" Font-Size="15px" Font-Names="verdana" runat="server">Delete sub deal</asp:LinkButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </div>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label14" runat="server" Text="Active"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="72px">
                                                    <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="btnImgSave" runat="server" ValidationGroup="FeaturedFood" Visible="true"
                                                        ImageUrl="~/admin/images/btnSave.jpg" ToolTip="Save Deal Info" OnClick="btnImgSave_Click" /></div>
                                                <div style="float: left; padding-left: 5px;">
                                                    <asp:Button ID="BtnPreview" OnClick="BtnPreview_Click" ValidationGroup="FeaturedFood"
                                                        runat="server" Text="Preview" />
                                                </div>
                                                <div style="float: left; padding-left: 5px;">
                                                    <asp:ImageButton ID="btnImgCancel" runat="server" CausesValidation="false" ImageUrl="~/admin/images/btnConfirmCancel.gif"
                                                        OnClick="btnImgCancel_Click" /></div>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="b">
                            <div class="b">
                                <div class="b">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnPreview" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
