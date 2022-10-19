<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="affiliateform.aspx.cs"
    Inherits="affiliateform" Title="Tastygo | Affliate Partner | Request" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="height40">
    </div>
    <asp:Label ID="label1" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Affiliate Partner Request"
        Font-Size="29px" Font-Bold="true" ForeColor="#97C717"></asp:Label>
    <div class="height15">
    </div>
    <div class="height5">
    </div>
    <div class="height5">
    </div>
    <div id="tblBusinessInfo" runat="server" style="width: 100%; border: 1px solid #373737;
        background: #F5F5F5; height: 972px;">
        <div id="divSignUp" style="width: 100%;">
            <table style="width: 100%; border-bottom: solid 1px #373737; height: 63px;" cellpadding="0"
                cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%; border-right: solid 1px #373737; padding-left: 19px; background-color: #E9E9E9;" class="fontSpaceHeightRegular">
                        <asp:Label ID="Label17" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Fill out the following form and we'll contact you shortly"></asp:Label>
                    </td>
                    <td style="width: 50%; padding-left: 19px; background-color: #DDDDDD;" class="fontSpaceHeightRegular>
                        <asp:Label ID="Label18" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Asterik <font style='color:RED;'>(*)</font> represents required field"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="contactus_affiliateform">
                <div style="padding-left: 19px; padding-bottom: 8px; padding-top: 19px;" class="GreenFontMiddle fontSpaceHeightHeading">
                    Site Information</div>
            </div>
            <div class="height15">
            </div>
            <table border="0" cellpadding="6" cellspacing="0" width="100%">
                <tr>
                    <td width="172px" style="text-align: right;">
                        <asp:Label ID="Label19" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Web site or newsletter name *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtWebSiteName" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator15" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtWebSiteName" ErrorMessage="Web site or newsletter name required!"
                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="RequiredFieldValidator15">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td width="152px" style="text-align: right;">
                        <asp:Label ID="Label20" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Web site URL *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                        <asp:TextBox ID="txtWebSiteURL" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator16" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtWebSiteURL" ErrorMessage="Web site URL required!"
                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="RequiredFieldValidator16">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtWebSiteURL" ErrorMessage="Invalid Web site URL!"
                            ValidationGroup="businessReg" Display="None" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="RegularExpressionValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label16" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Tell us a little bit about your website"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;" colspan="3">
                        <asp:TextBox ID="txtWebsiteInfo" Width="772px" runat="server" MaxLength="100" TextMode="MultiLine"
                            Height="92" CssClass="TextBoxDeal"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="height15">
            </div>
            <div class="contactus_affiliateformBottom">
                <div style="padding-left: 19px; padding-bottom: 8px; padding-top: 19px;" class="GreenFontMiddle fontSpaceHeightHeading">
                    Contact Information</div>
            </div>
            <div class="height15">
            </div>
            <table border="0" cellpadding="6" cellspacing="0" width="100%">
                <tr>
                    <td width="172px" style="text-align: right;">
                        <asp:Label ID="Label4" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Title / Function in Organization *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtTitleInOrg" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtTitleInOrg" ErrorMessage="Title / Function in Organization required!"
                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td width="152px" style="text-align: right;">
                        <asp:Label ID="Label2" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Email Address *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                        <asp:TextBox ID="txtEmail" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Email Address required!" ValidationGroup="businessReg"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator5">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RegularExpressionValidator ID="revEmailAddress" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Invalid email address!" ValidationGroup="businessReg"
                            Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="vceREEmail" runat="server" TargetControlID="revEmailAddress">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label3" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="First Name *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtFirstName" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtFirstName" ErrorMessage="First Name required!" ValidationGroup="businessReg"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td class="fieldLoginUsername" width="152px" style="text-align: right;">
                        <asp:Label ID="Label5" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Last Name *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                        <asp:TextBox ID="txtLastName" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtLastName" ErrorMessage="Last Name required!" ValidationGroup="businessReg"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label12" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Phone Number *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtPhoneNo" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Phone Number required!"
                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RequiredFieldValidator11">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td class="fieldLoginUsername" width="152px" style="text-align: right;">
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                    </td>
                </tr>
            </table>
            <div class="height15">
            </div>
            <div class="contactus_affiliateformBottom">
                <div style="padding-left: 19px; padding-bottom: 8px; padding-top: 19px;" class="GreenFontMiddle fontSpaceHeightHeading">
                    Company Information</div>
            </div>
            <div class="height15">
            </div>
            <table border="0" cellpadding="6" cellspacing="0" width="100%">
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label6" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Organization Name (Your name if none) *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtOrgName" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtOrgName" ErrorMessage="Organization Name (Your name if none) required!"
                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator4">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td class="fieldLoginUsername" width="152px" style="text-align: right;">
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label7" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Address 1 *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtAddress1" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtAddress1" ErrorMessage="Address 1 required!" ValidationGroup="businessReg"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator8">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td class="fieldLoginUsername" width="152px" style="text-align: right;">
                        <asp:Label ID="Label13" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Address 2"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                        <asp:TextBox ID="txtAddress2" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label10" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="City *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtCity" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtCity" ErrorMessage="City required!" ValidationGroup="businessReg"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator12">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td class="fieldLoginUsername" width="152px" style="text-align: right;">
                        <asp:Label ID="Label8" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Province *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                        <asp:TextBox ID="txtProvince" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtProvince" ErrorMessage="Province required!" ValidationGroup="businessReg"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator6">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label9" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Zip Code *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtZip" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtZip" ErrorMessage="Zip Code required!" ValidationGroup="businessReg"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator7">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td class="fieldLoginUsername" width="152px" style="text-align: right;">
                        <asp:Label ID="Label11" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Country *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                        <asp:TextBox ID="txtCountry" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtCountry" ErrorMessage="Country required!"
                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RequiredFieldValidator10">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label14" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Organization Phone *"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 14px;">
                        <asp:TextBox ID="txtOrgPhone" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtOrgPhone" ErrorMessage="Organization Phone required!"
                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RequiredFieldValidator13">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td class="fieldLoginUsername" width="152px" style="text-align: right;">
                        <asp:Label ID="Label15" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Organization Fax"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 11px;">
                        <asp:TextBox ID="txtOrgFax" Width="263px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="padding-bottom: 19px; padding-top: 19px; padding-left: 472px; float: left;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="businessReg"
                                OnClick="btnSubmit_Click" CssClass="btnlogin txtLogin" /></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="float: left; padding-right: 5px;">
                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="images/error.png" />
                            <asp:Label ID="lblErrorMessage" runat="server" Font-Size="16px" Visible="false"></asp:Label></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="height40">
    </div>
</asp:Content>