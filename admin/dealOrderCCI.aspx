<%@ Page Title="View Credit Card Info" Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true" CodeFile="dealOrderCCI.aspx.cs" Inherits="admin_dealOrderCCI" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript">

       function luhn(oSrc, args) {                
            if (args.Value != "") {
                var num = args.Value;
                num = (num + '').replace(/\D+/g, '').split('').reverse();
                if (!num.length) {
                    args.IsValid = false;
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
                    return;
                }
            }
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width: 100%;" align="center">
        <div id="popup">
            <div id="popHeader">
                <div style="float: left">
                    <asp:Label ID="lblpopHead" Text="Credit Card Information" runat="server"></asp:Label>
                </div>
            </div>
                <div id="divEditCustomer">
                    <asp:UpdatePanel ID="upnlEditCustomer" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlAskForPassword" runat="server">
                                <table id="Table1" border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                                    <tr>
                                        <td align="right" class="colRight">
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:Image ID="imgErrPwd" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                                            <asp:Label ID="lblErrPwd" runat="server" Visible="false" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label4" runat="server" Text="Enter Secure Password" />
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtpassword" TextMode="Password" runat="server" CssClass="txtForm"></asp:TextBox>
                                            <cc1:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtpassword"
                                                SetFocusOnError="true" ValidationGroup="login" ErrorMessage="Password required."
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:ImageButton ID="ImageButton1" ValidationGroup="login" runat="server" ImageUrl="~/admin/images/button_login.gif"
                                                OnClick="ImageButton1_Click" />&nbsp;
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                                OnClick="CancelButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlCCinfo" runat="server" Visible="false">
                                <table cellpadding="3" cellspacing="1" width="450px" class="fontStyle">
                                    <tr>
                                        <td align="right" class="colRight">
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:Image ID="imgCCI" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                                            <asp:Label ID="lblCCI" runat="server" Visible="false" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            *Billing Name:
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtFirstName" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="vtxtFirstName" runat="server" CssClass="ui-state-error-text"
                                                Display="Dynamic" ValidationGroup="CC" ErrorMessage="*" ControlToValidate="txtFirstName" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            *Billing Address:
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtBillingAddress" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="vtxtLastName" runat="server" CssClass="ui-state-error-text"
                                                Display="Dynamic" ValidationGroup="CC" ErrorMessage="*" ControlToValidate="txtBillingAddress" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            *Card Number:
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txCardNumner" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvCCnumber" runat="server" CssClass="ui-state-error-text"
                                                Display="Dynamic" ValidationGroup="CC" ErrorMessage="*" ControlToValidate="txCardNumner" />
                                            <asp:CustomValidator ID="cvCreditCard" runat="server" ControlToValidate="txCardNumner"
                                                ValidateEmptyText="true" ClientValidationFunction="luhn" SetFocusOnError="true"
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*">                                       
                                            </asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            *Security Code:
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtCCVNumber" Columns="20" MaxLength="20" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ui-state-error-text"
                                                Display="Dynamic" ValidationGroup="CC" ErrorMessage="*" ControlToValidate="txtCCVNumber" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            Expiration Date:
                                        </td>
                                        <td align="left" class="colLeft">
                                            <div>
                                                <div style="float: left;">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" Height="20">
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
                                                    <asp:DropDownList ID="ddlYear" runat="server" Height="20">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            *City:
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtCity" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="ui-state-error-text"
                                                Display="Dynamic" ValidationGroup="CC" ErrorMessage="*" ControlToValidate="txtCity" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            State:
                                        </td>
                                        <td align="left" class="colLeft">
                                           <%-- <asp:DropDownList ID="ddlState" runat="server" Height="20">
                                            </asp:DropDownList>--%>                                            
                                            <asp:TextBox ID="txtState" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="ui-state-error-text"
                                                Display="Dynamic" ValidationGroup="CC" ErrorMessage="*" ControlToValidate="txtState" />
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            *Postal Code:
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtPostalCode" Columns="20" MaxLength="20" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="ui-state-error-text"
                                                Display="Dynamic" ValidationGroup="CC" ErrorMessage="*" ControlToValidate="txtPostalCode" />
                                            <cc1:CustomValidator ID="revPostalCode" SetFocusOnError="true" runat="server" ControlToValidate="txtPostalCode"
                                                ClientValidationFunction="isPostCodeLocal" ErrorMessage="*" ValidationGroup="CC"
                                                Display="Dynamic"></cc1:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:ImageButton ID="btnUpdate" ValidationGroup="CC" CausesValidation="true" runat="server"
                                                ImageUrl="~/admin/images/btnUpdate.jpg" OnClick="btnUpdate_Click" />&nbsp;
                                            <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                                OnClick="CancelButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            </div>
</asp:Content>

