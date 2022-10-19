<%@ Control Language="C#" AutoEventWireup="true" CodeFile="withdraw.ascx.cs" Inherits="Takeout_UserControls_Templates_withdraw" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript">
    function calculateComissionMoney(oSrc, Args) {
        //alert("IN");
        var ComissionTotal = document.getElementById('cMoney').innerHTML;
        var ValueEntered = document.getElementById('ctl00_ContentPlaceHolder1_withDraw1_txtAmount').value;

        var money = 0;
        var message = document.getElementById('cMessage');

        if (ValueEntered != "" && !isNaN(ValueEntered)) {
            money = parseFloat(ValueEntered);
        }
        else {
            MessegeArea('Please enter a valid value.', 'Error');                            
            Args.IsValid = false;
            return;
        }
        if (money < 50) {
            MessegeArea('Value should be greater than $50 CAD.', 'Error');                                        
            Args.IsValid = false;
            return;
        }

        if (money > ComissionTotal) {
            MessegeArea("Value cannot be greater than $" + ComissionTotal + " CAD.", 'Error');                                        
            Args.IsValid = false;
            return;
        }

        Args.IsValid = true;
        return;
    }
</script>

<div>
    <div>
        <div class="withdrawexplain">
            <div style="padding-left: 10px; padding-top: 30px;">
                <div>
                    <asp:Label ID="Label1" Text="If your funds has exceeded $50, you can click the following button to send a request to issue an withdraw with check request. Once we have received it, we will send you the check to the address registered in your profile. Each withdrawal charges $2.5"
                        runat="server"></asp:Label></div>
                <div class="height15">
                </div>
                <asp:Panel ID="panelWithdraw" runat="server">
                    <span>
                        <asp:Label ID="lblFromBalance" Style="font-size: 15px;" Text="Your withdraw able money is $"
                            runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; </span><span>
                                <asp:TextBox runat="server" ID="txtAmount" CssClass="TextBox" Width="50" BorderColor="#ADC2D5" />
                                <cc1:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
                                    ClientValidationFunction="calculateComissionMoney" ControlToValidate="txtAmount"
                                    Display="None" ErrorMessage="<span style='z-index:5;' id='cMessage'>Please enter valid value to withdraw.</span>"
                                    ValidationGroup="Withdraw" SetFocusOnError="True"></cc1:CustomValidator>
                            </span><span style="padding-left: 10px;">
                                <asp:Button runat="server" ValidationGroup="Withdraw" CausesValidation="true" ID="btnSendRequest"
                                    Text="Send Request" CssClass="button primary pill big" OnClick="btnSendRequest_Click" />
                                <asp:Label runat="server" ID="lblSendResult" ForeColor="Red" EnableViewState="false" />
                            </span>
                </asp:Panel>
                <asp:Label ID="lblWithdrawError" runat="Server" />
            </div>
        </div>
        <div class="MemberArea_PageHeading" style="padding-left: 15px; padding-bottom: 10px;
            padding-top: 15px;">
            My Withdraw Requests
        </div>
        <div>
            <div style="width: 100%;">
                <table style="width: 100%; background-color: #E6E6E5; height: 30px; overflow: hidden;"
                    width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="30%" style="padding-left: 15px;">
                            <asp:Label ID="lblPonintsDate" CssClass="MemberArea_GridHeading" runat="server" Text="Request Date"></asp:Label>
                        </td>
                        <td width="30%" style="padding-left: 30px;">
                            <asp:Label ID="lblPoints" CssClass="MemberArea_GridHeading" runat="server" Text="Request Status"></asp:Label>
                        </td>
                        <td width="40%" style="padding-left: 100px;">
                            <asp:Label ID="lblGetsFrom" runat="server" CssClass="MemberArea_GridHeading" Text="Request Amount"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div align="center" style="width: 100%">
                    <asp:GridView runat="server" ID="gridview1" AllowPaging="true" AllowSorting="false"
                        AutoGenerateColumns="false" CellPadding="5" CellSpacing="0" GridLines="None"
                        OnPageIndexChanging="gridview1_PageIndexChanging" HorizontalAlign="Center" Width="100%"
                        CssClass="gridview" OnRowDataBound="gridview1_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td width="30%" style="padding-left: 15px;">
                                                <asp:Label runat="server" ID="lblDate" Text='<%# Eval("creationDate").ToString().Trim()==""?"":Convert.ToDateTime(Eval("creationDate").ToString()).ToString("MM-dd-yyyy H.mm tt")%>' />
                                            </td>
                                            <td width="30%">
                                                <asp:Label runat="server" ID="lblAction" Text='<%# Eval("requestAction") %>' />
                                            </td>
                                            <td width="30%" style="padding-left: 55px; text-align: left;">
                                                <asp:Label runat="server" ID="lblAmount" Text='<%# "CAD $" + Eval("requestAmount") %>'
                                                    Font-Size="12px" Font-Bold="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="emptydatarow">
                                <asp:Label ID="lblEmptyDate" runat="server" Text="There are no matching data in the system."></asp:Label>
                            </div>
                        </EmptyDataTemplate>
                        <PagerTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" class="search_result">
                                <tr runat="server" id="topPager">
                                    <td colspan="0">
                                        <div class="pagerstyle">
                                            <div>
                                                <div>
                                                    <div class="pagerContent">
                                                        <asp:LinkButton ID="lnkTopPrev" Enabled='<%# displayPrevious %>' CommandName="Page"
                                                            CommandArgument="Prev" runat="server" CssClass="pagerN" Text="Previous"></asp:LinkButton>
                                                        <asp:Repeater ID="rptrPage" runat="server">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:LinkButton ID="lnkPage" ForeColor='<%# GetColor(Eval("pageNo").ToString()) %>'
                                                                    OnClick="lnkPage_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                                    CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>&nbsp;
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:LinkButton ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>' CommandName="Page"
                                                            CommandArgument="Next" CssClass="pagerN" Text="Next"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</div>
