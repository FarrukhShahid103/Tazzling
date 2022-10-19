<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="restaurantccinfo.aspx.cs" Inherits="restaurantccinfo"
    Title="TastyGo | Admin | Credit Card Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%;" align="center">
        <div id="popup">
            <div id="popHeader">
                <div style="float: left">
                    <asp:Label ID="lblpopHead" Text="Credit Card Information" runat="server"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="pnlAskForPassword" runat="server">
            <table id="Table1" border="0" cellpadding="3" cellspacing="2" width="920px"
                class="fontStyle">                               
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="Label4" runat="server" Text="Enter Secure Password" />
                    </td>
                    <td align="left" class="colLeft">
                        <asp:TextBox ID="txtpassword" TextMode="Password" runat="server" CssClass="txtForm"></asp:TextBox>
                        <cc1:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtpassword"
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
                        <asp:ImageButton ID="ImageButton1" ValidationGroup="login" runat="server" 
                            ImageUrl="~/admin/images/button_login.gif" onclick="ImageButton1_Click"/>
                    </td>
                </tr>                          
            </table>
            </asp:Panel>
            <asp:Panel ID="pnlCCinfo" runat="server">
            <table id="tblRestuarant" border="0" cellpadding="3" cellspacing="2" width="920px"
                class="fontStyle">
                <tr>
                    <td align="right" class="colRight">
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblAddressError" ForeColor="Red" runat="server" Text="" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="Label1" runat="server" Text="UserID" />
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblUserID" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="Label3" runat="server" Text="Package" />
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblPackage" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="Label6" runat="server" Text="Duration" />
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblDuration" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="lblResName" runat="server" Text="Package Value" />
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblPackageValue" runat="server" Text=""></asp:Label>   
                        <asp:HiddenField ID="hfPackageValue" runat="server" />                                
                    </td>
                </tr>
                <tr id="trDiscount" runat="server">
                    <td align="right" class="colRight">
                        <asp:Label ID="lblDiscountText" runat="server" Text="Discount"></asp:Label>
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblDiscountValue" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="lblTaxText" runat="server" Text="Tax"></asp:Label>
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblTax" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="Label8" runat="server" Text="Total Fee"></asp:Label>
                    </td>
                    <td align="left" class="colLeft">
                        <asp:Label ID="lblTotalFee" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="lblResBusinessName" runat="server" Text="Card Holder Name" />
                    </td>
                    <td align="left" class="colLeft">
                        <asp:TextBox ID="txtCardHolderName" runat="server" CssClass="txtForm"></asp:TextBox>
                        <cc1:RequiredFieldValidator runat="server" ID="UsernameRequired" ControlToValidate="txtCardHolderName"
                            SetFocusOnError="true" ValidationGroup="CreditCard" ErrorMessage="Value required."
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="UsernameRequired">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="lblResAddress" runat="server" Text="Card Number" />
                    </td>
                    <td align="left" class="colLeft">
                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="txtForm"></asp:TextBox>
                        <cc1:RequiredFieldValidator runat="server" ID="rfvcardnumber" ControlToValidate="txtCardNumber"
                            SetFocusOnError="true" ValidationGroup="CreditCard" ErrorMessage="Value required."
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvcardnumber">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtCardNumber" ErrorMessage="Only digits are required e.g.4111111111111111"
                            ValidationGroup="CreditCard" Display="None" ValidationExpression="^\d*$"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RegularExpressionValidator3">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="lblCity" runat="server" Text="Expiration Date" />
                    </td>
                    <td align="left" class="colLeft">
                         <asp:DropDownList runat="server" ID="expDateMonth">
                                <asp:ListItem Value="01">01</asp:ListItem>
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
                            &nbsp;
                            <asp:DropDownList runat="server" ID="expDateYear">
                            </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="lblProvince" runat="server" Text="CSC" />
                    </td>
                    <td align="left" class="colLeft">
                       <input type="text" size="3" maxlength="4" id="cvv2Number" runat="server" style="border: solid 1px #99cccc;" >                            
                            <cc1:RequiredFieldValidator runat="server" ID="rfvCCV" ControlToValidate="cvv2Number"
                                SetFocusOnError="true" ValidationGroup="CreditCard" ErrorMessage="Value required."
                                Display="None"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvCCV">
                            </cc2:ValidatorCalloutExtender>
                            <cc1:RegularExpressionValidator ID="RegularExpressionValidator4" SetFocusOnError="true"
                                runat="server" ControlToValidate="cvv2Number" ErrorMessage="Only digits are required e.g.123"
                                ValidationGroup="CreditCard" Display="None" ValidationExpression="^\d*$"></cc1:RegularExpressionValidator>
                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RegularExpressionValidator4">
                            </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="colRight">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone" />
                    </td>
                    <td align="left" class="colLeft fontStyle">
                         <asp:DropDownList runat="server" ID="ddcType">
                                <asp:ListItem Selected="True" Value="Visa">Visa</asp:ListItem>
                                <asp:ListItem Value="Master">Master</asp:ListItem>
                                <asp:ListItem Value="American Express">American Express</asp:ListItem>
                            </asp:DropDownList>
                    </td>
                </tr>               
            </table>
            <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">              
                <tr>
                    <td align="right" class="colRight">
                    </td>
                    <td align="left" class="colLeft">
                        <asp:ImageButton ID="btnUpdate" ValidationGroup="CreditCard" runat="server" ImageUrl="~/admin/images/btnUpdate.jpg"
                            OnClick="btnUpdate_Click" />&nbsp;
                        <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                            OnClick="CancelButton_Click" />
                    </td>
                </tr>
                <tr>
                <td colspan="2" align="center">
                 <div class="floatLeft" style="padding-top: 10px; padding-bottom: 7px; padding-left: 200px;">
        <div style="float: left; padding-right: 5px">
            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/Checked.png" />
        </div>
        <div class="floatLeft">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" Visible="false" Text="Record has been updated successfully."
                CssClass="fontStyle"></asp:Label>
        </div>
    </div>
                </td>
                </tr>
            </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
