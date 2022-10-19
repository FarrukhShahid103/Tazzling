<%@ Page Language="C#" MasterPageFile="~/sales/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="addeditrestaurant.aspx.cs" Inherits="addeditrestaurant"
    Title="TastyGo | Sales | Add/Edit Leads" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchResturantName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchUserName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_ddlFaxPhone').selectedIndex = 0;
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

        function validatePhone2(oSrc, args) {
            // alert("enter");
            var phone1 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO1').value;
            var phone2 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO2').value;
            var phone3 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO3').value;
            //alert(phone1 + "-" + phone2+"-"+phone3);
            if (phone1 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO1').focus();
            }
            else if (phone2 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO2').focus();
            }
            else if (phone3 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO3').focus();
            }
            if (phone1.length != 3) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO1').focus();
            }
            else if (phone2.length != 3) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO2').focus();
            }
            else if (phone3.length != 4) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO3').focus();
            }
            if (!IsNumeric(phone1)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO1').focus();
            }
            else if (!IsNumeric(phone2)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO2').focus();
            }
            else if (!IsNumeric(phone3)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneRO3').focus();
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
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvPrinterID'), false);
            }

        }
            
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Leads Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table id="tblRestuarant" border="0" cellpadding="3" cellspacing="2" width="920px"
                            class="fontStyle">
                            <tr>
                                <td class="colRight" colspan="2">
                                    <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                                        <div style="float: left; padding-right: 5px">
                                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/sales/images/error.png" />
                                        </div>
                                        <div class="floatLeft">
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblResName" runat="server" Text="Restaurant Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtResName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvResName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtResName" ErrorMessage="Name required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceResName" runat="server" TargetControlID="rfvResName">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblResBusinessName" runat="server" Text="Restaurant Owner Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtResOwnerName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtResOwnerName" ErrorMessage="Business name required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblResAddress" runat="server" Text="Restaurant Address" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtResAddress" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvResAddress" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtResAddress" ErrorMessage="Address required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceResAddress" runat="server" TargetControlID="rfvResAddress">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label10" runat="server" Text="Assign To" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtAssignTo" runat="server" CssClass="txtForm"></asp:TextBox>
                                   <%--<cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtAssignTo" ErrorMessage="Value required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator4">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label6" runat="server" Text="Province" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="txtForm"
                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                                    <cc1:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlState"
                                        SetFocusOnError="true" ValidationGroup="user" InitialValue="Please Select" ErrorMessage="Value required."
                                        Display="None"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator3">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr id="trCity" runat="server" visible="false">
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label8" runat="server" Text="City" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddCity" runat="server" CssClass="txtForm" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblProvince" runat="server" Text="Status" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtForm" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="Lead Generated" Selected="True">Lead Generated</asp:ListItem>
                                        <asp:ListItem Value="Telemarket Followup Required">Telemarket Followup Required</asp:ListItem>
                                        <asp:ListItem Value="Appointment Made">Appointment Made</asp:ListItem>
                                        <asp:ListItem Value="Follow Up Required">Follow Up Required</asp:ListItem>
                                        <asp:ListItem Value="Second Followup Required">Second Followup Required</asp:ListItem>
                                        <asp:ListItem Value="Owner Rejected(Reason)">Owner Rejected(Reason)</asp:ListItem>
                                        <asp:ListItem Value="Successfully Signuped">Successfully Signuped</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trLoginID" visible="false" runat="server">
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="Restaurant Login ID" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtLoginID" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidator2" SetFocusOnError="true"
                                        runat="server" ControlToValidate="txtLoginID" ErrorMessage="LoginID required!"
                                        ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr id="trResturantInfo" visible="false" runat="server">
                                <td colspan="2">
                                    <table border="0px" cellpadding="0px" cellspacing="5px" width="100%">
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label3" runat="server" Text="Restaurant Signup Detail" Font-Bold="true" />
                                            </td>
                                            <td align="left" class="colLeft">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label5" runat="server" Text="Restaurant Name" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblRestaurantName" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label7" runat="server" Text="Restaurant Business Name" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblRestaurantBusinessName" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label4" runat="server" Text="Email" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblEmail" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label9" runat="server" Text="Restaurant Address" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblRestaurantAddress" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label11" runat="server" Text="City" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblCity" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label13" runat="server" Text="ZipCode" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblZipCode" runat="server" Text="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="padding-right: 10px;">
                                                <asp:Label ID="Label15" runat="server" Text="Phone" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblPhoneSignUp" runat="server" Text="" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblPhone" runat="server" Text="Restaurant Phone" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:TextBox ID="txtPhone1" runat="server" CssClass="txtForm" Width="60px"></asp:TextBox>-<asp:TextBox
                                        ID="txtPhone2" runat="server" CssClass="txtForm" Width="60px"></asp:TextBox>-<asp:TextBox
                                            ID="txtPhone3" runat="server" CssClass="txtForm" Width="60px"></asp:TextBox>
                                    <cc1:CustomValidator ID="cvPhone" runat="server" ControlToValidate="txtPhone3" ValidateEmptyText="true"
                                        ClientValidationFunction="validatePhone" SetFocusOnError="true" ValidationGroup="user"
                                        ErrorMessage="Phone number required in correct format e.g. xxx-xxx-xxxx" Display="None">
                                    </cc1:CustomValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcePhone" runat="server" TargetControlID="cvPhone">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label2" runat="server" Text="Priority" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:CheckBox ID="cbPriority" runat="server" Text="" />
                                </td>
                            </tr>
                           <%-- <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label2" runat="server" Text="Restaurant Owner Phone" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:TextBox ID="txtPhoneRO1" runat="server" MaxLength="3" CssClass="txtForm" Width="60px"></asp:TextBox>-<asp:TextBox
                                        ID="txtPhoneRO2" runat="server" MaxLength="3" CssClass="txtForm" Width="60px"></asp:TextBox>-<asp:TextBox
                                            ID="txtPhoneRO3" runat="server" MaxLength="4" CssClass="txtForm" Width="60px"></asp:TextBox>
                                    <cc1:CustomValidator ID="cvPhone2" runat="server" ControlToValidate="txtPhoneRO3"
                                        ValidateEmptyText="true" ClientValidationFunction="validatePhone2" SetFocusOnError="true"
                                        ValidationGroup="user" ErrorMessage="Phone number required in correct format e.g. xxx-xxx-xxxx"
                                        Display="None">
                                    </cc1:CustomValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="cvPhone2">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:GridView ID="pageGrid" runat="server" DataKeyNames="restaurantLeadCommentID"
                                        Width="80%" AutoGenerateColumns="False" AllowPaging="false" GridLines="None"
                                        AllowSorting="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div style="display: none">
                                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("restaurantLeadCommentID") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblID1" runat="server" Text='<% # Eval("createdBy") %>' Visible="true"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHeadComment" ForeColor="White" runat="server" Text="Comment"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblCommentText" Text='<% #Eval("restaurantLeadComment") %>' runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lbluserNameHead" ForeColor="White" runat="server" Text="Creaded By"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lbluserNameText" Text='<% # Eval("userName") %>' runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Created Date"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblcreationDateText" Text='<% # GetDateString(Eval("creationDate")) %>'
                                                            runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%-- <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                    </div>--%>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <RowStyle CssClass="gridText" Height="27px" />
                                        <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight" valign="top">
                                    <asp:Label ID="lblResDetail" runat="server" Text="Comments" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="txtForm" Height="90px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnUpdate" ValidationGroup="user" runat="server" ImageUrl="~/sales/images/btnUpdate.jpg"
                                        OnClick="btnUpdate_Click" />&nbsp;
                                    <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/sales/Images/btnConfirmCancel.gif"
                                        OnClientClick="document.location.href='restaurantLeadsManagement.aspx'" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
