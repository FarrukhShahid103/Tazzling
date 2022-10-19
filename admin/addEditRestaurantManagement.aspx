<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="addEditRestaurantManagement.aspx.cs"
    Inherits="addEditRestaurantManagement" Title="TastyGo | Admin | Business Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="JS/jquery-1.4.min.js"></script>

    <script type="text/javascript" language="javascript">
        
       function checkChange() {            
            if(document.getElementById('ctl00_ContentPlaceHolder1_cbOnlineDeal').checked)
            {            
                document.getElementById('divGoogleAddress').style.display = "none"; 
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator5'), false);
                
            } 
            else 
            {               
                document.getElementById('divGoogleAddress').style.display = ""; 
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator5'), true);
            }
        }
        
    
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchBusinessName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchCity').value = '';            
            return false;
        }

        function checkAll() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            if (document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked) {
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

        function validatePhone(oSrc, args) {

            var phone1 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').value;
            var phone2 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').value;
            var phone3 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').value;
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

        function changePrinterValidator() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_rbIsPhoneFax_1').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvPrinterID'), true);                       
            }
            else
            {             
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvPrinterID'), false);
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
                    return;
                }
            }
            else {
                args.IsValid = false;
                return;
            }
        }

        function checkMaxLength(sender, length) {
            if (sender.value.length > length) {
                sender.value = sender.value.substr(0, length);
            }
            return;
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upBusinessMgmtForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfCountry" runat="server" Value="0" />
            <asp:HiddenField ID="hfProvince" runat="server" Value="0" />
            <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
            <asp:Panel ID="pnlForm" runat="server">
                <div style="width: 100%;" align="center">
                    <div id="element-box1">
                        <div class="t">
                            <div class="t">
                                <div class="t">
                                </div>
                            </div>
                        </div>
                        <div class="m">
                            <div class="m">
                                <div>
                                    <div id="popHeader">
                                        <div style="float: left">
                                            <asp:HiddenField ID="hfBusinessId" runat="server" />
                                            <asp:Label ID="lblpopHead" Text="Business Managment" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <table id="tblRestuarant" border="0" cellpadding="3" cellspacing="2" width="720px"
                                        class="fontStyle">
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Image ID="ImgAddError" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblAddressError" ForeColor="Red" runat="server" Text="" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label2" runat="server" Text="First Name" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtFname" runat="server" CssClass="txtForm" MaxLength="50"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtFname" ErrorMessage="First Name required!" ValidationGroup="user"
                                                    Display="None">                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator2">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label3" runat="server" Text="Last Name" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtLname" runat="server" CssClass="txtForm" MaxLength="50"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtLname" ErrorMessage="Last Name required!" ValidationGroup="user"
                                                    Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="lblResBusinessName" runat="server" Text="Business Name" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtBusinessName" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtBusinessName" ErrorMessage="Business Name required!" ValidationGroup="user"
                                                    Display="None">                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="vertical-align: top;">
                                                <asp:Label ID="Label14" runat="server" Text="Business Address" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtBusinessAddress" runat="server" TextMode="MultiLine" Height="100px"
                                                    CssClass="txtForm"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtBusinessPaymentAddress" ErrorMessage="Business Address required!"
                                                    ValidationGroup="user" Display="None">                                                                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="RequiredFieldValidator13">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label9" runat="server" Text="Business Payment Title" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtBPaymentTitle" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtBPaymentTitle" ErrorMessage="Business Payment Title required!"
                                                    ValidationGroup="user" Display="None">                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator9">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="vertical-align: top;">
                                                <asp:Label ID="Label12" runat="server" Text="Business Payment Address" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtBusinessPaymentAddress" TextMode="MultiLine" Height="100px" runat="server"
                                                    CssClass="txtForm"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtBusinessPaymentAddress" ErrorMessage="Business Payment Address required!"
                                                    ValidationGroup="user" Display="None">                                                                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RequiredFieldValidator11">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label10" runat="server" Text="Commission" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtCommission" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtCommission" ErrorMessage="Commission required!"
                                                    ValidationGroup="user" Display="None">                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RequiredFieldValidator10">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RXOurComission" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtCommission" ErrorMessage="Only Numeric value required"
                                                    ValidationGroup="user" Display="None" ValidationExpression="(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9]{1,})?$)"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender52" runat="server" TargetControlID="RXOurComission">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label4" runat="server" Text="Business Email" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <div>
                                                    <div style="float: left; padding-right: 5px;">
                                                        <asp:HiddenField ID="hfEmail" runat="server" />
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtForm" AutoPostBack="true"
                                                            OnTextChanged="txtEmail_Changed" MaxLength="100"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                                                            ControlToValidate="txtEmail" ErrorMessage="Business Email required!" ValidationGroup="user"
                                                            Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator4">
                                                        </cc2:ValidatorCalloutExtender>
                                                        <cc1:RegularExpressionValidator ID="reEmail" ValidationGroup="user" ControlToValidate="txtEmail"
                                                            ErrorMessage="Please enter valid email format." SetFocusOnError="true" Display="None"
                                                            runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="reEmail"
                                                            runat="server">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="float: left;">
                                                        <asp:CheckBox ID="cbUseThisEmail" runat="server" Visible="false" Text="Email already registered as member, check to convert into business account." />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label6" runat="server" Text="Password" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtPwd" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtPwd" ErrorMessage="Password required!" ValidationGroup="user"
                                                    Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RequiredFieldValidator6">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="PasswordLengthValid" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtPwd" ErrorMessage="Password must be 6-15 characters without space."
                                                    ValidationGroup="user" Display="None" ValidationExpression="([a-zA-Z0-9]{6,15})$"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="PasswordLengthValid">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label7" runat="server" Text="Confirm Password" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtPwdConfirm" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtPwdConfirm" ErrorMessage="Confirm Password required!" ValidationGroup="user"
                                                    Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator7">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:CompareValidator ID="cvConfirmPassword" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtPwdConfirm" ControlToCompare="txtPwd" ErrorMessage="Password and confirm password must be same!"
                                                    ValidationGroup="user" Display="None"></cc1:CompareValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="cvConfirmPassword">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label11" runat="server" Text="Alternative Email" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtAlternativeEmail" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <cc1:RegularExpressionValidator ID="reAlternativeEmail" ValidationGroup="user" ControlToValidate="txtAlternativeEmail"
                                                    ErrorMessage="Please enter valid email format." SetFocusOnError="true" Display="None"
                                                    runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" TargetControlID="reAlternativeEmail"
                                                    runat="server">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label13" runat="server" Text="Business URL" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtLink" runat="server" CssClass="txtForm"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLink"
                                                    Display="None" ErrorMessage="<span id='cMessage'>Business URL required!.</span>"
                                                    ValidationGroup="user" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="RequiredFieldValidator12"
                                                    runat="server">
                                                </cc2:ValidatorCalloutExtender>
                                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtLink" ErrorMessage="<span id='cMessage'>Please enter valid URL e.g.(http://tazzling.com)</span>"
                                                    ValidationGroup="user" Display="None" ValidationExpression="(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></cc1:RegularExpressionValidator><cc2:ValidatorCalloutExtender
                                                        ID="ValidatorCalloutExtender17" runat="server" TargetControlID="RegularExpressionValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border: solid 1px #000000;">
                                                <asp:UpdatePanel ID="upAddAddress" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table id="Table1" border="0" cellpadding="3" cellspacing="2" width="100%" class="fontStyle">
                                                            <tr>
                                                                <td align="left" class="colLeft" colspan="2" style="padding-top: 5px;">
                                                                    <asp:Label ID="Label8" Font-Bold="true" Font-Size="14px" runat="server" Text="Deal Addresses" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="colRight">
                                                                    <asp:Label ID="lblResAddress" runat="server" Text="Deal Address" />
                                                                </td>
                                                                <td align="left" class="colLeft">
                                                                    <asp:TextBox ID="txtDealAddress" runat="server" CssClass="txtForm" MaxLength="500"></asp:TextBox>
                                                                    <cc1:RequiredFieldValidator ID="rfvResAddress" SetFocusOnError="true" runat="server"
                                                                        ControlToValidate="txtDealAddress" ErrorMessage="Deal Address required!" ValidationGroup="DealAddress"
                                                                        Display="None">                                                                          
                                                                    </cc1:RequiredFieldValidator>
                                                                    <cc2:ValidatorCalloutExtender ID="vceResAddress" runat="server" TargetControlID="rfvResAddress">
                                                                    </cc2:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: right; padding-right: 20px;">
                                                                    <asp:HiddenField ID="hfDealAddress" runat="server" Value="" />
                                                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="raID" Width="100%" AutoGenerateColumns="False"
                                                                        AllowPaging="false" GridLines="None" AllowSorting="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                                        OnRowDeleting="GridView1_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div style="display: none">
                                                                                        <asp:Label ID="lblraID" runat="server" Text='<%# Eval("raID") %>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblGridrestaurantId" runat="server" Text='<%# Eval("restaurantId") %>'
                                                                                            Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="0%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHeadComment" ForeColor="White" runat="server" Text="Address"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <asp:Label ID="lblCommentText" Text='<% #Eval("restaurantAddress") %>' runat="server"></asp:Label>
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
                                                                                        OnClientClick='return confirm("Are you sure you want to delete this address.");'
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
                                                            <tr>
                                                                <td align="right" class="colRight" colspan="2" style="padding-top: 5px; padding-right: 120px;">
                                                                    <asp:ImageButton ID="btnAddDealAddress" runat="server" OnClick="btnAddDealAddress_Click"
                                                                        ImageUrl="~/admin/images/btnSave.jpg" ValidationGroup="DealAddress" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label20" runat="server" Text="Online Deal" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:CheckBox ID="cbOnlineDeal" runat="server" onclick="javascript:checkChange();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style='border: solid 1px #000000; display: <%=strGoogleAddress%>'
                                                id="divGoogleAddress">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table id="Table2" border="0" cellpadding="3" cellspacing="2" width="100%" class="fontStyle">
                                                            <tr>
                                                                <td align="left" class="colLeft" colspan="2" style="padding-top: 5px;">
                                                                    <asp:Label ID="Label15" Font-Bold="true" Font-Size="14px" runat="server" Text="Google Addresses" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" class="colRight">
                                                                    <asp:Label ID="Label16" runat="server" Text="Google Address" />
                                                                </td>
                                                                <td align="left" class="colLeft">
                                                                    <asp:TextBox ID="txtGoogleAddress" runat="server" CssClass="txtForm" MaxLength="200"></asp:TextBox>
                                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                                                        ControlToValidate="txtGoogleAddress" ErrorMessage="Google Address required!"
                                                                        ValidationGroup="GoogleAddress" Display="None">                                                                          
                                                                    </cc1:RequiredFieldValidator>
                                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator5">
                                                                    </cc2:ValidatorCalloutExtender>
                                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true"
                                                                        runat="server" ControlToValidate="txtGoogleAddress" ErrorMessage="# not allowed."
                                                                        ValidationGroup="GoogleAddress" Display="None" ValidationExpression="^[^#]*$"></cc1:RegularExpressionValidator>
                                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" TargetControlID="RegularExpressionValidator2">
                                                                    </cc2:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: right; padding-right: 20px;">
                                                                    <asp:HiddenField ID="hfGoogleAddress" runat="server" Value="" />
                                                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="rgaID" Width="100%" AutoGenerateColumns="False"
                                                                        AllowPaging="false" GridLines="None" AllowSorting="false" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                                                                        OnRowDeleting="GridView2_RowDeleting">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div style="display: none">
                                                                                        <asp:Label ID="lblrgaID" runat="server" Text='<%# Eval("rgaID") %>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblGridrestaurantId" runat="server" Text='<%# Eval("restaurantId") %>'
                                                                                            Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="0%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblHeadComment" ForeColor="White" runat="server" Text="Address"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <asp:Label ID="lblCommentText" Text='<% #Eval("restaurantGoogleAddress") %>' runat="server"></asp:Label>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" CausesValidation="false"
                                                                                        ImageUrl="~/admin/Images/edit.gif" ToolTip="Edit" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div>
                                                                                        <a id="hrefGooglMap" target="_blank" href='<%#"GoogleMap.aspx?gAdd="+Eval("restaurantGoogleAddress") %>'>
                                                                                            Show in Map </a>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/delete.gif"
                                                                                        OnClientClick='return confirm("Are you sure you want to delete this address.");'
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
                                                            <tr>
                                                                <td align="right" class="colRight" colspan="2" style="padding-top: 5px; padding-right: 120px;">
                                                                    <asp:ImageButton ID="btnAddGoogleAddress" runat="server" OnClick="btnAddGoogleAddress_Click"
                                                                        ImageUrl="~/admin/images/btnSave.jpg" ValidationGroup="GoogleAddress" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="lblPhone" runat="server" Text="Phone" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:TextBox ID="txtPhone1" runat="server" CssClass="txtForm"></asp:TextBox>
                                                <%--<cc1:CustomValidator ID="cvPhone" runat="server" ControlToValidate="txtPhone3" ValidateEmptyText="true"
                                                    ClientValidationFunction="validatePhone" SetFocusOnError="true" ValidationGroup="user"
                                                    ErrorMessage="Phone number required in correct format." Display="None">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </cc1:CustomValidator>
                                                <cc2:ValidatorCalloutExtender ID="vcePhone" runat="server" TargetControlID="cvPhone">
                                                </cc2:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="lblFax" runat="server" Text="Fax" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:TextBox ID="txtFax" runat="server" CssClass="txtForm"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label17" runat="server" Text="Cell Number" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:TextBox ID="txtCellNumber" runat="server" CssClass="txtForm"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtCellNumber" ErrorMessage="Please enter cell number." ValidationGroup="user"
                                                    Display="None">                                                                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator8">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label18" runat="server" Text="Pre Deal Verification" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:DropDownList ID="ddlPreDealVerification" runat="server">
                                                    <asp:ListItem Value="0% Not contacted yet" Text="0% Not contacted yet" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="50% Emailed Pregen List & Preview Link" Text="50% Emailed Pregen List & Preview Link"></asp:ListItem>
                                                    <asp:ListItem Value="75% Called, no response and Left messages" Text="75% Called, no response and Left messages"></asp:ListItem>
                                                    <asp:ListItem Value="100% Called & Confirmed. Business ready for the deal" Text="100% Called & Confirmed. Business ready for the deal"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label19" runat="server" Text="Post Deal Verification" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:DropDownList ID="ddlPostDealVerification" runat="server">
                                                    <asp:ListItem Value="0% Not contacted yet" Text="0% Not contacted yet" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="50% Emailed Buyer's list, waiting for response" Text="50% Emailed Buyer's list, waiting for response"></asp:ListItem>
                                                    <asp:ListItem Value="100% Called & Confirmed." Text="100% Called & Confirmed."></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label1" runat="server" Text="Business Image" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="fpBusinessImg" runat="server" CssClass="txtForm" Width="313px" />
                                                        <%-- <cc1:RequiredFieldValidator ID="rfvDealImage1" runat="server" ControlToValidate="fpBusinessImg"
                                                            ErrorMessage="Image required" Display="None" ValidationGroup="user" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="rfvDealImage1">
                                                        </cc2:ValidatorCalloutExtender>--%>
                                                        <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidation"
                                                            ControlToValidate="fpBusinessImg" Display="None" ErrorMessage="Invalid file format."
                                                            ValidationGroup="user" SetFocusOnError="True"></cc1:CustomValidator>
                                                        <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                                            runat="server">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <img id="imgUpload1" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                                    visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="vertical-align: top;">
                                                <asp:Label ID="Label21" runat="server" Text="Business Logo" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div style="clear: both;">
                                                            <asp:FileUpload ID="fuLogo" runat="server" CssClass="txtForm" Width="313px" /></div>
                                                        <div style="clear: both; padding-top: 10px;">
                                                            <b>Note: </b>Logo width must be less than 150 pixals</div>
                                                        <%-- <cc1:RequiredFieldValidator ID="rfvDealImage1" runat="server" ControlToValidate="fpBusinessImg"
                                                            ErrorMessage="Image required" Display="None" ValidationGroup="user" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="rfvDealImage1">
                                                        </cc2:ValidatorCalloutExtender>--%>
                                                        <cc1:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ImageValidation"
                                                            ControlToValidate="fuLogo" Display="None" ErrorMessage="Invalid file format."
                                                            ValidationGroup="user" SetFocusOnError="True"></cc1:CustomValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" TargetControlID="CustomValidator2"
                                                            runat="server">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <img id="imglogo" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                                    visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="lblResDetail" runat="server" Text="About Business" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtResDetail" runat="server" CssClass="txtForm" Height="90px" TextMode="MultiLine"
                                                    onkeyup="checkMaxLength(this, 1000)" MaxLength="1000"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvResDetail" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtResDetail" ErrorMessage="Please enter short description."
                                                    ValidationGroup="user" Display="None">                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="vceResDetail" runat="server" TargetControlID="rfvResDetail">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label5" runat="server" Text="Status" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" Checked="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:ImageButton ID="btnSave" ValidationGroup="user" runat="server" ImageUrl="~/admin/images/btnSave.jpg"
                                                    OnClick="btnSave_Click" ToolTip="Add New Business Info" />&nbsp;
                                                <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                                    OnClientClick="javascript:window.close();" />
                                            </td>
                                        </tr>
                                    </table>
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
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
