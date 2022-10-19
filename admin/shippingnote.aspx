<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="shippingnote.aspx.cs" Inherits="shippingnote" Title="TastyGo | Admin | Shipping Note Management" %>

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
                                <asp:Label ID="lblpopHead" Text="Shipping Note Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">                       
                             <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label3" runat="server" Text="Note" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="100px" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtNote"
                                        Display="None" ErrorMessage="<span id='cMessage'>Note required!.</span>"
                                        ValidationGroup="shipping" SetFocusOnError="True"></cc1:RequiredFieldValidator>
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
