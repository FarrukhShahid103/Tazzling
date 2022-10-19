<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealVerificationDetail.aspx.cs" Inherits="dealVerificationDetail" Title="TastyGo | Admin | Deal Verification Milestone" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

function openPreview()
myWindow=window.open('" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Preview.aspx?sidedeal=" + NewDealID + "','','width=760,height=700,toolbar=no,status=no, menubar=no, scrollbars=yes,resizable=no');
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
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlDetail" runat="server">
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
                            <asp:HiddenField ID="hfBusinessId" runat="server" />
                            <asp:HiddenField ID="hfDealID" runat="server" />
                            <asp:Label ID="lblDealInfoHeading" Text="Deal Verification" runat="server"></asp:Label>
                        </div>
                    </div>
                    <table cellpadding="2" cellspacing="2" width="100%" class="fontStyle" border="0">
                        <tbody>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Deal Name:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblDealName" runat="server" Text="Deal Name:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Deal Modify By:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblDealModifyBy" runat="server" Text="Deal Start Time:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label8" runat="server" Text="Modify At:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblDealModifyTime" runat="server" Text="Deal End Time:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label10" runat="server" Text="Business Name:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblBusinessName" runat="server" Text="Business Name:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label12" runat="server" Text="Owner First Name:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblOwnerFirstName" runat="server" Text="Owner Name:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label14" runat="server" Text="Owner Last Name:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblOwnerLastName" runat="server" Text="Owner Last Name:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label7" runat="server" Text="Business Email Address:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblBusinessEmailAddress" runat="server" Text="Business Email Address:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label9" runat="server" Text="Business Alternate Email Address:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblAlternateEmailAddress" runat="server" Text="Business Alternate Email Address:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label5" runat="server" Text="Business Phone Number:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblPhoneNumber" runat="server" Text="Business Phone Number"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server" Text="Business Owner's Cell Phone Number:"></asp:Label>
                                </td>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblCellNumber" runat="server" Text="Business Owner's Cell Phone Number:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label18" runat="server" Text="Pre Deal Verification:" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:DropDownList ID="ddlPreDealVerification" runat="server">
                                        <asp:ListItem Value="0% Data Entered" Text="0% Data Entered" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="20% Fine Print Verified" Text="20% Fine Print Verified"></asp:ListItem>
                                        <asp:ListItem Value="40% Date Verified" Text="40% Date Verified"></asp:ListItem>
                                        <asp:ListItem Value="60% Design Verified" Text="60% Design Verified"></asp:ListItem>
                                        <asp:ListItem Value="80% Pregen List sent, email confirmed" Text="80% Pregen List sent, email confirmed"></asp:ListItem>
                                        <asp:ListItem Value="100% Called Business, All Staff ready for deal" Text="100% Called Business, All Staff ready for deal"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label19" runat="server" Text="Post Deal Verification:" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:DropDownList ID="ddlPostDealVerification" runat="server">
                                        <asp:ListItem Value="0% Deal Ended" Text="0% Deal Ended" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="50% Email Notification, sent customer's List" Text="50% Email Notification, sent customer's List"></asp:ListItem>
                                        <asp:ListItem Value="75% Phone Notification" Text="75% Phone Notification"></asp:ListItem>
                                        <asp:ListItem Value="100% Confirmed Received Customer List" Text="100% Confirmed Received Customer List"></asp:ListItem>
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
                                        <asp:ImageButton ID="btnImgCancel" runat="server" CausesValidation="false" ImageUrl="~/admin/images/btnConfirmCancel.gif"  OnClientClick="document.location.href='dealVerificationMilestone.aspx'" /></div>
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
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; padding-right: 5px">
                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                        </div>
                        <div class="floatLeft">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="b">
                <div class="b">
                    <div class="b">
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
