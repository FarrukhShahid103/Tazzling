<%@ Control Language="C#" AutoEventWireup="true" CodeFile="login.ascx.cs" Inherits="Takeout_UserControls_Login_login" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<link rel="stylesheet" type="text/css" href="../../CSS/takeout_style.css" />

<script type="text/javascript" src="js/jquery-1.4.min.js"></script>

<asp:Panel ID="pnlLogin" DefaultButton="btnSignUp" runat="server">
    <div style="width: 600px; border: 1px solid #373737; background: #F5F5F5; height: 363px;">        
        <div id="divSignUp" style="width: 600px;" runat="server">
            <table border="0" cellpadding="6" cellspacing="0">
                <tr>
                    <td style="height: 8px;">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 19px;">
                        <asp:Label ID="label1" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Sign Up"
                            Font-Size="35px" Font-Bold="true" ForeColor="#97C717" Font-Underline="True"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 41px; text-align: left; height: 30px; padding-top: 7px;
                        padding-bottom: 2px; padding-right: 25px;" colspan="2">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/images/error.png" />
                        <asp:Label ID="lblErrorMessage" runat="server" Font-Size="17px" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="tblSignUp" border="0" cellpadding="6" cellspacing="0" runat="server">
                <tr>
                    <td class="fieldLoginUsername" width="172px" style="text-align: right;">
                        <asp:Label ID="Label4" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Your Name"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 20px;">
                        <asp:TextBox ID="txtFullName" Width="222px" runat="server" MaxLength="100" CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtFullName" ErrorMessage="First Name required!" ValidationGroup="SignUp"
                            Display="None">                            
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginPassword" style="text-align: right;">
                        <asp:Label ID="Label5" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Email"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 20px;">
                        <asp:TextBox ID="txtUsernameSignUp" Width="222px" runat="server" MaxLength="100"
                            CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtUsernameSignUp" ErrorMessage="Email required!" ValidationGroup="SignUp"
                            Display="None">                            
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtUsernameSignUp" ErrorMessage="Invalid email address!"
                            ValidationGroup="SignUp" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RegularExpressionValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginPassword" style="text-align: right;">
                        <asp:Label ID="Label7" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Password"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 20px;">
                        <asp:TextBox ID="txtPwd" Width="222px" runat="server" MaxLength="20" TextMode="Password"
                            CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtPwd" ErrorMessage="Password required!" ValidationGroup="SignUp"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RegularExpressionValidator ID="PasswordLengthValid" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtPwd" ErrorMessage="Password must be 6-15 characters without space."
                            ValidationGroup="SignUp" Display="None" ValidationExpression="([a-zA-Z0-9]{6,15})$"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="PasswordLengthValid">
                        </cc2:ValidatorCalloutExtender>
                        <%-- <cc1:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtPwd" ErrorMessage="Password must be 6-15 characters without space, and must contain least one character and a number."
                            ValidationGroup="SignUp" Display="None" ValidationExpression="\w{5,255}"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RegularExpressionValidator3">
                        </cc2:ValidatorCalloutExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginPassword" style="text-align: right;">
                        <asp:Label ID="Label8" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                            Text="Confirm Password"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; padding-left: 20px;">
                        <asp:TextBox ID="txtConfirmPwd" Width="222px" runat="server" MaxLength="20" TextMode="Password"
                            CssClass="TextBoxDeal"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtConfirmPwd" ErrorMessage="Confirm Password required!" ValidationGroup="SignUp"
                            Display="None">                            
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator4">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:CompareValidator ID="cvConfirmPassword" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtConfirmPwd" ControlToCompare="txtPwd" ErrorMessage="Password and confirm password must be same!"
                            ValidationGroup="SignUp" Display="None"></cc1:CompareValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="cvConfirmPassword">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginButton">
                    </td>
                    <td style="padding-left: 20px;">
                        <div style="float: left">
                            <asp:CheckBox ID="cbSignUp" Checked="true" runat="server" Text="" />
                        </div>
                        <div style="float: left;">
                            <asp:HyperLink ID="hlTermsAndCondition" runat="server" Font-Names="Arial,Helvetica,sans-serif"
                                Font-Size="15px" ForeColor="#97C717" Text="I agree terms and condition" NavigateUrl="~/terms-customer.aspx"
                                Target="_blank"></asp:HyperLink>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLoginButton">
                    </td>
                    <td style="padding-left: 20px;">
                        <div style="width: 100px; float: left;">
                            <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" ValidationGroup="SignUp"
                                OnClick="btnSignUp_Click" CssClass="btnlogin txtLogin" /></div>                       
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Panel>
