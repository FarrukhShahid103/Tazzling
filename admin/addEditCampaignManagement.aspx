<%@ Page Title="Campaign Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="addEditCampaignManagement.aspx.cs" Inherits="addEditCampaignManagement"
    ValidateRequest="false" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />
    <script src="JS/CalendarControl.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


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
                                        <asp:Label ID="lblDealInfoHeading" Text="Add New Campaign" runat="server"></asp:Label>
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
                                        <asp:HiddenField ID="hfDealId" Value="0" runat="server" />
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Title"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="txtForm" Width="300px" MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitle"
                                                    ErrorMessage="Title required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator5">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Category"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:DropDownList ID="ddlCategory" runat="server">
                                                    <%-- <asp:ListItem Text="Digital Print" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Screenprint" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Bedroom" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Dining" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Furniture" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Lighting" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Living" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Outdoor" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Workspace" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Apple Accessories" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Makes You Smile" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Stationery" Value="12"></asp:ListItem>
                                                    <asp:ListItem Text="Tech & Gadgets" Value="13"></asp:ListItem>
                                                    <asp:ListItem Text="Bed & Bath" Value="14"></asp:ListItem>
                                                    <asp:ListItem Text="Home Accessories" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="Pillows & Throws" Value="16"></asp:ListItem>
                                                    <asp:ListItem Text="Rugs & Textiles" Value="17"></asp:ListItem>
                                                    <asp:ListItem Text="Tools" Value="18"></asp:ListItem>
                                                    <asp:ListItem Text="Workspace" Value="19"></asp:ListItem>
                                                    <asp:ListItem Text="Baby" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="Boys" Value="21"></asp:ListItem>
                                                    <asp:ListItem Text="Girls" Value="22"></asp:ListItem>
                                                    <asp:ListItem Text="Home" Value="23"></asp:ListItem>
                                                    <asp:ListItem Text="Parents" Value="24"></asp:ListItem>
                                                    <asp:ListItem Text="Toys & Games" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="Cook & Prep" Value="26"></asp:ListItem>
                                                    <asp:ListItem Text="Foodie" Value="27"></asp:ListItem>
                                                    <asp:ListItem Text="Tabletop" Value="28"></asp:ListItem>
                                                    <asp:ListItem Text="Denim" Value="29"></asp:ListItem>
                                                    <asp:ListItem Text="Shirts" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="Sneakers" Value="31"></asp:ListItem>
                                                    <asp:ListItem Text="Tees" Value="32"></asp:ListItem>
                                                    <asp:ListItem Text="Ties" Value="33"></asp:ListItem>
                                                    <asp:ListItem Text="Wallets" Value="34"></asp:ListItem>
                                                    <asp:ListItem Text="Watches" Value="35"></asp:ListItem>
                                                    <asp:ListItem Text="Cats" Value="36"></asp:ListItem>
                                                    <asp:ListItem Text="Dogs" Value="37"></asp:ListItem>
                                                    <asp:ListItem Text="Accents" Value="38"></asp:ListItem>
                                                    <asp:ListItem Text="Fashion" Value="39"></asp:ListItem>
                                                    <asp:ListItem Text="Fashion Accessories" Value="40"></asp:ListItem>
                                                    <asp:ListItem Text="Furniture" Value="41"></asp:ListItem>
                                                    <asp:ListItem Text="Jewelry" Value="42"></asp:ListItem>
                                                    <asp:ListItem Text="Tabletop" Value="43"></asp:ListItem>
                                                    <asp:ListItem Text="Taxidermy" Value="44"></asp:ListItem>
                                                    <asp:ListItem Text="Bags" Value="45"></asp:ListItem>
                                                    <asp:ListItem Text="Dresses" Value="46"></asp:ListItem>
                                                    <asp:ListItem Text="Hats" Value="47"></asp:ListItem>
                                                    <asp:ListItem Text="Pants" Value="48"></asp:ListItem>
                                                    <asp:ListItem Text="Shirts" Value="49"></asp:ListItem>
                                                    <asp:ListItem Text="Shoes" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="Swimwear" Value="51"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" Text="Campaign URL"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtURL" runat="server" CssClass="txtForm" Width="300px" MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtURL"
                                                    Display="None" ErrorMessage="<span id='cMessage'>Campaign URL required!</span>"
                                                    ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="RequiredFieldValidator12"
                                                    runat="server">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtURL" ErrorMessage="<span id='cMessage'>Please enter valid URL e.g.(http://tazzling.com)</span>"
                                                    ValidationGroup="FeaturedFood" Display="None" ValidationExpression="(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></cc1:RegularExpressionValidator><cc2:ValidatorCalloutExtender
                                                        ID="ValidatorCalloutExtender17" runat="server" TargetControlID="RegularExpressionValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label8" runat="server" Text="Ship To"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:RadioButton runat="server" TextAlign="Right" ID="rbUSA" GroupName="Ship" Text="USA"
                                                    Checked="true" />
                                                <asp:RadioButton ID="rbCanada" runat="server" TextAlign="Right" GroupName="Ship"
                                                    Text="Canada" />
                                                <asp:RadioButton ID="rbBoth" runat="server" TextAlign="Right" GroupName="Ship" Text="USA & Canada" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label9" runat="server" Text="Shipp From Address"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShipFromAddress" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShipFromAddress"
                                                    ErrorMessage="Address required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator1">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label13" runat="server" Text="Shipp From City"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShippFromCity" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtShippFromCity"
                                                    ErrorMessage="City required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator7">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label15" runat="server" Text="Shipp From ZipCode"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShippFromZipCode" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtShippFromZipCode"
                                                    ErrorMessage="Zipcode required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator8">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label16" runat="server" Text="Provice/State (code)"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShippingFromState" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="50"></asp:TextBox>
                                                e.g. NY,BC etc
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtShippingFromState"
                                                    ErrorMessage="Provice/State Code required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender10" TargetControlID="RequiredFieldValidator9">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label17" runat="server" Text="Ship From Country (code)"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtShippingFromCountry" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="50"></asp:TextBox>
                                                e.g. US,CA etc
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtShippingFromCountry"
                                                    ErrorMessage="Country Code required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender11" TargetControlID="RequiredFieldValidator10">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label10" runat="server" Text="Estimated arrival time"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtArrivalTime" runat="server" CssClass="txtForm" Width="300px"
                                                    MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtArrivalTime"
                                                    ErrorMessage="Address required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator2">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label11" runat="server" Text="Short Description"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <asp:TextBox ID="txtShortDescription" runat="server" Width="626px" TextMode="MultiLine"
                                                    Height="100px" MaxLength="1000">
                                                </asp:TextBox>
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
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label4" runat="server" Text="Owner Quote"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
                                                <asp:TextBox ID="txtCampaignQuote" runat="server" Width="626px" TextMode="MultiLine"
                                                    Height="150px">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top; padding-top: 35px;">
                                                <asp:Label ID="Label22" runat="server" Text="Start Time"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 31px;">
                                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtForm" Width="92px" MaxLength="12"
                                                    onclick="OpenCalendar('ctl00_ContentPlaceHolder1_txtStartDate');"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStartDate"
                                                    ErrorMessage="Start Time required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator3">
                                                </cc2:ValidatorCalloutExtender>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label6" runat="server" Text="End Time"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 4px;">
                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtForm" Width="92px" MaxLength="12"
                                                    onclick="OpenCalendar('ctl00_ContentPlaceHolder1_txtEndDate');"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEndDate"
                                                    ErrorMessage="End Time required" ValidationGroup="FeaturedFood" Display="None"
                                                    SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4">
                                                </cc2:ValidatorCalloutExtender>
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
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td align="right" style="vertical-align: middle;">
                                                <asp:Label ID="Label7" runat="server" Text="Slot"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="padding-top: 8px;">
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="lblImage" runat="server" Text="Campaign Image"></asp:Label>
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
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <img id="imgUpload1" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                                    visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 5px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label18" runat="server" Text="Is Featured" Visible="false"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:DropDownList ID="ddlFeatured" runat="server" Width="72px" Visible="false">
                                                    <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
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
