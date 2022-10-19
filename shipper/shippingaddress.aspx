<%@ Page Language="C#" MasterPageFile="~/shipper/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="shippingaddress.aspx.cs" Inherits="shippingaddress" Title="TastyGo | Shipper | Shipping Address Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfProvinceID" runat="server" Value="0" />
            <asp:HiddenField ID="hfTaxID" runat="server" Value="" />
            <asp:Panel ID="pnlForm" runat="server">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Shipping Address Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblCityName" runat="server" Text="City" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                                        Display="None" ErrorMessage="<span id='cMessage'>City name required!.</span>"
                                        ValidationGroup="shipping" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceCity" TargetControlID="rfvCity" runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label2" runat="server" Text="Postal Code / Zip Code" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode"
                                        Display="None" ErrorMessage="<span id='cMessage'>Postal Code / Zip Code required!.</span>"
                                        ValidationGroup="shipping" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="rfvZipCode"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="Province / State" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtProvince" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvProvince" runat="server" ControlToValidate="txtProvince"
                                        Display="None" ErrorMessage="<span id='cMessage'>Province / State required!.</span>"
                                        ValidationGroup="shipping" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvProvince"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label4" runat="server" Text="Country" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlShippingCountry" runat="server" CssClass="txtForm">
                                        <asp:ListItem Text="Canada" Value="Canada" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="United States" Value="United States"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label3" runat="server" Text="Address" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                        Display="None" ErrorMessage="<span id='cMessage'>Address required!.</span>" ValidationGroup="shipping"
                                        SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rfvAddress"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnUpdate" runat="server" ValidationGroup="shipping" ImageUrl="~/admin/images/btnUpdate.jpg"
                                        OnClick="btnUpdate_Click" />&nbsp;
                                    <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                        OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
