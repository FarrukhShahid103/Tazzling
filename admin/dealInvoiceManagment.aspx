<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealInvoiceManagment.aspx.cs" Inherits="dealInvoiceManagment" Title="Deal Invoice Managment" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divTop" runat="server">
        <asp:Literal ID="ltMain" runat="server" Text=""></asp:Literal>
        <div class="indent30">
            <table cellpadding="3" cellspacing="7" width="100%" border="0" class="fontStyle">
                <tr>
                    <td colspan="5" style="width: 100%; border-top: solid 2px gray;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style='float: left; width: 120px;'>
                        <asp:Label ID="Label3" runat="server" Text="Payout" Font-Bold="true" Font-Size="15px"></asp:Label>
                    </td>
                    <td style='float: left; width: 300px;'>
                    </td>
                    <td style='float: left; width: 150px;'>
                    </td>
                    <td style='float: left; width: 100px;'>
                    </td>
                    <td style='float: left; width: 100px;'>
                    </td>
                </tr>
                <tr>
                    <td style='float: left; width: 120px; text-align: right'>
                        <strong>Description:</strong>
                    </td>
                    <td style='float: left; width: 300px;'>
                        <asp:TextBox ID="txtDescription" runat="server" Text="" Width="270px" Height="70px"
                            TextMode="MultiLine"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtDescription" ErrorMessage="Please enter Description!" ValidationGroup="payOut"
                            Display="None">                            
                                               
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td style='float: left; width: 150px; text-align: right'>
                        <strong>Amount:</strong>
                    </td>
                    <td style='float: left; width: 100px;'>
                        <asp:TextBox ID="txtPayOut" runat="server" Width="75px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtPayOut" ErrorMessage="Please enter Amount!"
                            ValidationGroup="payOut" Display="None">                            
                                               
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RequiredFieldValidator10">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RegularExpressionValidator ID="RXOurComission" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtPayOut" ErrorMessage="Only Numeric value required" ValidationGroup="payOut"
                            Display="None" ValidationExpression="^\d{1,9}(\.\d{1,2})?$"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender52" runat="server" TargetControlID="RXOurComission">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td style='float: left; width: 100px;'>
                        <asp:ImageButton ID="btnPayOut" runat="server" ValidationGroup="payOut" ImageUrl="~/admin/Images/btnPayout.png"
                            OnClick="btnPayOut_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="width: 100%; border-top: solid 2px gray;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style='float: left; width: 120px;'>
                        <asp:Label ID="Label4" runat="server" Text="Adjustment" Font-Bold="true" Font-Size="15px"></asp:Label>
                    </td>
                    <td style='float: left; width: 300px;'>
                    </td>
                    <td style='float: left; width: 150px;'>
                    </td>
                    <td style='float: left; width: 100px;'>
                    </td>
                    <td style='float: left; width: 100px;'>
                    </td>
                </tr>
                <tr>
                    <td style='float: left; width: 120px; text-align: right'>
                        <strong>Description:</strong>
                    </td>
                    <td style='float: left; width: 300px;'>
                        <asp:TextBox ID="txtAdjustmentDescription" runat="server" Text="" Width="270px" Height="70px"
                            TextMode="MultiLine"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtAdjustmentDescription" ErrorMessage="Please enter Description!"
                            ValidationGroup="Adjustment" Display="None">                                                                           
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td style='float: left; width: 150px; text-align: right'>
                        <strong>Amount:</strong>
                    </td>
                    <td style='float: left; width: 100px;'>
                        <asp:TextBox ID="txtAdjustmentAmount" runat="server" Width="75px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtAdjustmentAmount" ErrorMessage="Please enter Amount!" ValidationGroup="Adjustment"
                            Display="None">                                                                           
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                            runat="server" ControlToValidate="txtAdjustmentAmount" ErrorMessage="Only Numeric value required"
                            ValidationGroup="Adjustment" Display="None" ValidationExpression="(^-{0,1}\d*\.{0,1}\d+$)"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RegularExpressionValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                    <td style='float: left; width: 100px;'>
                        <asp:ImageButton ID="btnAdjustment" runat="server" ValidationGroup="Adjustment" ImageUrl="~/admin/Images/btnAdjustment.png"
                            OnClick="btnAdjustment_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="width: 100%;">
                        <asp:Label ID="lblAdjustmentNote" runat="server" Text="<b>Note:</b> When submit adjustment, positive means adding amount which Tastygo owe, negative means adding money towards Tastygo."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="width: 100%; border-top: solid 2px gray;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style='float: left; width: 120px;'>
                        <asp:Label ID="Label1" runat="server" Text="Payment" Font-Bold="true" Font-Size="15px"></asp:Label>
                    </td>
                    <td colspan="3" style='float: left; width: 300px;'>
                    <div style="float:left; padding-right:5px;"><asp:Image ID="imgError" runat="server" Visible="false" /> </div>
                    <div style="float:left;"><asp:Label ID="lblMessage" runat="server" Visible="false" Text=""></asp:Label></div>
                        
                    </td>
                    <td style='float: left; width: 100px;'>
                    </td>
                </tr>
                <tr>
                    <td style='float: left; width: 120px; text-align: right'>
                        <strong>You Pay:</strong>
                    </td>
                    <td colspan="3" style='float: left;'>
                        <div style="float: left; padding-left: 15px;">
                            <asp:RadioButton ID="rdFirstPayment" Text="First Payment" runat="server" GroupName="payment" /></div>
                        <div style="float: left; padding-left: 15px;">
                            <asp:RadioButton ID="rdSecondPayment" Text="Second Payment" runat="server" GroupName="payment" /></div>
                        <div style="float: left; padding-left: 15px; padding-right: 15px;">
                            <asp:RadioButton ID="rdThirdPayment" Text="Third Payment" runat="server" GroupName="payment" /></div>
                    </td>
                    <td style='float: left; width: 170px;'>
                        <div style="float: left; padding-right: 10px;">
                            <asp:ImageButton ID="imgbtnClearPayment" runat="server" ImageUrl="~/admin/Images/btnClear.png" OnClick="imgbtnClearPayment_Click" /></div>
                        <div style="float: left;">
                            <asp:ImageButton ID="btnPaymentToBusiness" runat="server" ImageUrl="~/admin/Images/btnSave.jpg"
                                OnClick="btnPaymentToBusiness_Click" /></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="buttonrow" align="left" style="padding-top: 12px; padding-left: 19px;">
            <div style="float: left; padding-left: 20px;">
                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                    OnClientClick="javascript:window.close()"></asp:ImageButton></div>
            <div style="float: left; padding-left: 10px;">
                <asp:UpdatePanel ID="updownloadExcel" runat="server">
                    <ContentTemplate>
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Download Doc" ImageUrl="~/admin/Images/btnpdfDownload.png"
                            OnClick="btnSave_Click" TabIndex="2" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="imgbtnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
