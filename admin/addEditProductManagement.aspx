<%@ Page Title="Product Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="addEditProductManagement.aspx.cs" Inherits="addEditProductManagement"
    ValidateRequest="false" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />
    <script src="JS/CalendarControl.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function checkChange() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_cbEnableSize').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator7'), false);
            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator7'), true);
            }
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
                                        <asp:Label ID="lblDealInfoHeading" Text="Add New Product" runat="server"></asp:Label>
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
                                        <asp:HiddenField ID="hfResturnatID" Value="0" runat="server" />
                                        <asp:HiddenField ID="hfDealId" Value="" runat="server" />
                                        <asp:HiddenField ID="hfDealStartTIme" Value="0" runat="server" />
                                        <asp:HiddenField ID="hfDealEndTime" Value="0" runat="server" />
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Title"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="txtForm" Width="300px" MaxLength="200"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitle"
                                                    ErrorMessage="Title required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator5">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label11" runat="server" Text="Sub Title"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtSubTitle" runat="server" CssClass="txtForm" Width="300px" MaxLength="200"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSubTitle"
                                                    ErrorMessage="Title required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator6">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" Text="Short Description (More Details)"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShortDescription" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="1000" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtShortDescription"
                                                    Display="None" ErrorMessage="<span id='cMessage'>Campaign URL required!</span>"
                                                    ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="RequiredFieldValidator12"
                                                    runat="server">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Return Policy"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtReturnPolicy" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReturnPolicy"
                                                    Display="None" ErrorMessage="<span id='cMessage'>Return Policy required!</span>"
                                                    ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator1"
                                                    runat="server">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label6" runat="server" Text="Shipping Info"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShippingInfo" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShippingInfo"
                                                    Display="None" ErrorMessage="<span id='cMessage'>Return Policy required!</span>"
                                                    ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator2"
                                                    runat="server">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label5" runat="server" Text="Description"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <CKEditor:CKEditorControl ID="txtDescription" Height="460px" Width="626px" runat="server"></CKEditor:CKEditorControl>
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
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^[-+]?[0-9]\d{0,2}(\.\d{1,2})?%?$"></cc1:RegularExpressionValidator>
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
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^[-+]?[0-9]\d{0,2}(\.\d{1,2})?%?$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RegularExpressionValidator6">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Product Maximum Orders Allowed?"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMaxOrders" runat="server" CssClass="txtForm" Width="150px" MaxLength="6"></asp:TextBox>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtMaxOrders" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RegularExpressionValidator1">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" Text="Maximum Orders Qty"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMaxQty" runat="server" CssClass="txtForm" Width="150px" MaxLength="6"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMaxQty"
                                                    ErrorMessage="Maximum Orders required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator7">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtMaxQty" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RegularExpressionValidator2">
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
                                                <asp:Label ID="Label44" runat="server" Text="Product Cost"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtOurComission" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOurComission"
                                                    ErrorMessage="Product Cost required" Display="None" ValidationGroup="FeaturedFood"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender51" TargetControlID="RequiredFieldValidator3">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RXOurComission" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtOurComission" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^[-+]?[0-9]\d{0,2}(\.\d{1,2})?%?$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender52" runat="server" TargetControlID="RXOurComission">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" Text="Height In Inch"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtHeight" runat="server" CssClass="txtForm" Width="150px" MaxLength="6"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtHeight"
                                                    ErrorMessage="Height required." ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender19" TargetControlID="RequiredFieldValidator11">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator4" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtHeight" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^[-+]?[0-9]\d{0,2}(\.\d{1,2})?%?$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" TargetControlID="RegularExpressionValidator4">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" Text="Width In Inch"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtWidth" runat="server" CssClass="txtForm" Width="150px" MaxLength="6"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtWidth"
                                                    ErrorMessage="Width required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender21" TargetControlID="RequiredFieldValidator13">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator7" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtWidth" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^[-+]?[0-9]\d{0,2}(\.\d{1,2})?%?$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" TargetControlID="RegularExpressionValidator7">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label15" runat="server" Text="Weight Ig lb"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtWeight" runat="server" CssClass="txtForm" Width="150px" MaxLength="6"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtWeight"
                                                    ErrorMessage="Weight required." ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender23" TargetControlID="RequiredFieldValidator14">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator8" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtWeight" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="^[-+]?[0-9]\d{0,2}(\.\d{1,2})?%?$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server" TargetControlID="RegularExpressionValidator8">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label16" runat="server" Text="Dimension"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDimension" runat="server" CssClass="txtForm" Width="150px" MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtDimension"
                                                    ErrorMessage="Dimension required." ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender25" TargetControlID="RequiredFieldValidator14">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border: solid 1px #000000;">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table id="Table2" border="0" cellpadding="3" cellspacing="2" width="100%" class="fontStyle">
                                                            <tr>
                                                                <td align="left" class="colLeft" colspan="2" style="padding-top: 5px; padding-left: 42px;">
                                                                    <asp:Label ID="Label25" Font-Bold="true" Font-Size="14px" runat="server" Text="Product Properties" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div style="float: left; padding-left: 90px;">
                                                                        <asp:Label ID="Label26" runat="server" Text="Property Label" /></div>
                                                                    <div style="float: left; padding-left: 10px;">
                                                                        <asp:TextBox ID="txtProductLabel" runat="server" CssClass="txtForm" MaxLength="100"
                                                                            Width="150px"></asp:TextBox>
                                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                                                            ControlToValidate="txtProductLabel" ErrorMessage="Product label required!" ValidationGroup="ProductProperties"
                                                                            Display="None">                                                                          
                                                                        </cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="RequiredFieldValidator9">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 140px;">
                                                                        <asp:Label ID="Label27" runat="server" Text="Product Description" />
                                                                    </div>
                                                                    <div style="float: left; padding-left: 10px;">
                                                                        <asp:TextBox ID="txtProductLabelDescription" runat="server" CssClass="txtForm" MaxLength="200"
                                                                            Width="150px"></asp:TextBox>
                                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true"
                                                                            runat="server" ControlToValidate="txtProductLabelDescription" ErrorMessage="Description required!"
                                                                            ValidationGroup="ProductProperties" Display="None">                                                                          
                                                                        </cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="RequiredFieldValidator10">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="colRight" style="width: 135px;">
                                                                </td>
                                                                <td align="left" class="colLeft">
                                                                    <asp:ImageButton ID="btnProductProperties" runat="server" OnClick="btnProductProperties_Click"
                                                                        ImageUrl="~/admin/images/btnSave.jpg" ValidationGroup="ProductProperties" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="colRight" style="width: 135px;">
                                                                </td>
                                                                <td align="left" class="colLeft">
                                                                    <asp:HiddenField ID="hfProductPropertiesId" runat="server" Value="" />
                                                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="productPropertiesID" Width="100%"
                                                                        AutoGenerateColumns="False" AllowPaging="false" GridLines="None" AllowSorting="false"
                                                                        OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDeleting="GridView2_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div style="display: none">
                                                                                        <asp:Label ID="lblProductPropertiesID" runat="server" Text='<%# Eval("productPropertiesID") %>'
                                                                                            Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="0%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHeadComment" ForeColor="White" runat="server" Text="Label"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <asp:Label ID="lblCommentText" Text='<% #Eval("propertiesLabel") %>' runat="server"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblQuantity" ForeColor="White" runat="server" Text="Description"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <div style="padding-left: 15px;">
                                                                                        <asp:Label ID="lblQuantityText" Text='<% #Eval("propertiesDescription") %>' runat="server"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" CausesValidation="false"
                                                                                        ImageUrl="~/admin/Images/edit.gif" ToolTip="Edit" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/delete.gif"
                                                                                        OnClientClick='return confirm("Are you sure you want to delete this property?");'
                                                                                        ToolTip="Delete" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle CssClass="gridHeader" />
                                                                        <RowStyle CssClass="gridText" Height="27px" />
                                                                        <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label23" runat="server" Text="Enable Size" />
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:CheckBox ID="cbEnableSize" runat="server" Text="" onclick="javascript:checkChange();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border: solid 1px #000000;">
                                                <asp:UpdatePanel ID="upAddAddress" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table id="Table1" border="0" cellpadding="3" cellspacing="2" width="100%" class="fontStyle">
                                                            <tr>
                                                                <td align="left" class="colLeft" colspan="2" style="padding-top: 5px; padding-left: 85px;">
                                                                    <asp:Label ID="Label22" Font-Bold="true" Font-Size="14px" runat="server" Text="Product Size" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div style="float: left; padding-left: 100px;">
                                                                        <asp:Label ID="lblResAddress" runat="server" Text="Product Size" />
                                                                    </div>
                                                                    <div style="float: left; padding-left: 10px;">
                                                                        <asp:TextBox ID="txtProductSize" runat="server" CssClass="txtForm" MaxLength="100"
                                                                            Width="150px"></asp:TextBox>
                                                                        <cc1:RequiredFieldValidator ID="rfvResAddress" SetFocusOnError="true" runat="server"
                                                                            ControlToValidate="txtProductSize" ErrorMessage="Product size required!" ValidationGroup="ProductSize"
                                                                            Display="None">                                                                          
                                                                        </cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="vceResAddress" runat="server" TargetControlID="rfvResAddress">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 200px;">
                                                                        <asp:Label ID="Label24" runat="server" Text="Quantity" />
                                                                    </div>
                                                                    <div style="float: left; padding-left: 10px;">
                                                                        <asp:TextBox ID="txtProductSizeQuantity" runat="server" CssClass="txtForm" MaxLength="3"
                                                                            Width="150px"></asp:TextBox>
                                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server"
                                                                            ControlToValidate="txtProductSizeQuantity" ErrorMessage="Quantity required!"
                                                                            ValidationGroup="ProductSize" Display="None">                                                                          
                                                                        </cc1:RequiredFieldValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RequiredFieldValidator8">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                        <cc1:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true"
                                                                            runat="server" ControlToValidate="txtProductSizeQuantity" ErrorMessage="Only Numeric value required"
                                                                            ValidationGroup="ProductSize" Display="None" ValidationExpression="^\d+$"></cc1:RegularExpressionValidator>
                                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RegularExpressionValidator3">
                                                                        </cc2:ValidatorCalloutExtender>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="colRight" style="width: 135px;">
                                                                </td>
                                                                <td align="left" class="colLeft">
                                                                    <asp:ImageButton ID="btnAddProductSize" runat="server" OnClick="btnAddProductSize_Click"
                                                                        ImageUrl="~/admin/images/btnSave.jpg" ValidationGroup="ProductSize" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="colRight" style="width: 135px;">
                                                                </td>
                                                                <td align="left" class="colLeft">
                                                                    <asp:HiddenField ID="hfProductSize" runat="server" Value="" />
                                                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="sizeID" Width="100%" AutoGenerateColumns="False"
                                                                        AllowPaging="false" GridLines="None" AllowSorting="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                                        OnRowDeleting="GridView1_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div style="display: none">
                                                                                        <asp:Label ID="lblSizeID" runat="server" Text='<%# Eval("sizeID") %>' Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="0%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHeadComment" ForeColor="White" runat="server" Text="Size"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <asp:Label ID="lblCommentText" Text='<% #Eval("sizeText") %>' runat="server"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblQuantity" ForeColor="White" runat="server" Text="Quantity"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <div style="padding-left: 15px;">
                                                                                        <asp:Label ID="lblQuantityText" Text='<% #Eval("quantity") %>' runat="server"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" CausesValidation="false"
                                                                                        ImageUrl="~/admin/Images/edit.gif" ToolTip="Edit" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/delete.gif"
                                                                                        OnClientClick='return confirm("Are you sure you want to delete it?");' ToolTip="Delete" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle CssClass="gridHeader" />
                                                                        <RowStyle CssClass="gridText" Height="27px" />
                                                                        <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="lblImage" runat="server" Text="Image 1 (720x616)"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="fpDealImage1" runat="server" CssClass="txtForm" Width="313px" />
                                                        <cc1:RequiredFieldValidator ID="rfvDealImage1" runat="server" ControlToValidate="fpDealImage1"
                                                            ErrorMessage="Image required" Display="None" ValidationGroup="FeaturedFood" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4" TargetControlID="rfvDealImage1">
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
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <img id="imgUpload1" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                                    visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label8" runat="server" Text="Image 2 (720x616)"></asp:Label>
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
                                                <asp:Label ID="Label9" runat="server" Text="Image 3 (720x616)"></asp:Label>
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
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label17" runat="server" Text="Tracking"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:DropDownList ID="ddlTracking" runat="server" Width="72px">
                                                    <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label10" runat="server" Text="Voucher Product?"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:DropDownList ID="ddlVoucherProduct" runat="server" Width="72px">
                                                    <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
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
    </asp:UpdatePanel>
</asp:Content>
