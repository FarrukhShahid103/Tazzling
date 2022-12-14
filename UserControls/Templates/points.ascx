<%@ Control Language="C#" AutoEventWireup="true" CodeFile="points.ascx.cs" Inherits="Takeout_UserControls_Templates_points" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript">
    function calculateComissionMoney(oSrc, Args) {
        //alert("IN");
        var ComissionTotal = document.getElementById('cMoney').innerHTML;
        var ValueEntered = document.getElementById('ctl00_ContentPlaceHolder1_withdraw_restaurant1_txtAmount').value;

        var money = 0;
        var message = document.getElementById('cMessage');

        if (ValueEntered != "" && !isNaN(ValueEntered)) {
            money = parseFloat(ValueEntered);
        }
        else {
            message.innerHTML = "Please enter a valid value.";
            Args.IsValid = false;
            return;
        }
        if (money > ComissionTotal) {
            message.innerHTML = "Value cannot be greater than $" + ComissionTotal + " CAD.";
            Args.IsValid = false;
            return;
        }

        Args.IsValid = true;
        return;
    }
</script>

<div style="padding-top: 10px;">
    <h3 class="marginTB10 hr">
        <asp:Label ID="lblWithdrawFunds" CssClass="gcardtext"  Text="Points Detail" runat="server"></asp:Label></h3>
</div>
<div>
    <h4 class="marginTB10 hr ">
        <asp:Label ID="lblPointsDetail"  runat="server"></asp:Label></h4>
</div>
<%--<div class="withdrawexplain">
    <div class="title">
        <asp:Label ID="lblWithdrawWithCheck" Text="Withdraw With Check" runat="server"></asp:Label>
    </div>
    <div class="body">
        <div>
            <asp:Label ID="Label1" Text="If your funds has exceeded $0, you can click the following button to send a request to issue an withdraw with check request. Once we have received it, we will send you the check to the address registered in your profile."
                runat="server"></asp:Label></div>
        <div class="height15">
        </div>
        <asp:Panel ID="panelWithdraw" runat="server">
            <span>
                <asp:Label ID="lblFromBalance" Text="Your withdraw able money is $" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            </span><span class="padding_L30">
                <asp:TextBox runat="server" ID="txtAmount" Width="50" BorderColor="#ADC2D5" BorderStyle="solid"
                    BorderWidth="1px" />
                <cc1:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
                    ClientValidationFunction="calculateComissionMoney" ControlToValidate="txtAmount"
                    Display="None" ErrorMessage="<span id='cMessage'>Please enter valid value to withdraw.</span>"
                    ValidationGroup="Withdraw" SetFocusOnError="True"></cc1:CustomValidator>
                <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                    runat="server">
                </cc2:ValidatorCalloutExtender>
            </span><span class="padding_L30">
                <asp:Button runat="server" ValidationGroup="Withdraw" CausesValidation="true" ID="btnSendRequest"
                    Text="Send Request" CssClass="btn_orange_bigger1" OnClick="btnSendRequest_Click" />
                <asp:Label runat="server" ID="lblSendResult" ForeColor="Red" EnableViewState="false" />
            </span>
        </asp:Panel>
        <asp:Label ID="lblWithdrawError" runat="Server" />
    </div>
</div>--%>
<%--<h3 class="marginTB10">
    <asp:Label ID="lblWithdrawRequests" Text="Withdraw Requests" runat="server"></asp:Label></h3>--%>
<div class="float_left" style="padding-top: 10px;  padding-bottom: 10px;">
   
        <asp:Button ID="Button1"  OnClientClick="document.location.href='member_requested_points.aspx'"  runat="server" Text="Your Gift Requests" CssClass=" btnyourgcard" />
</div>
<div class="height15">
</div>
<div class="gridborder">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="GridHeader">
        <tr>
                <td width="15%" style="padding-left:10px;" >
                <asp:Label ID="lblPonintsDate" runat="server" Text="Points Date"></asp:Label>
            </td>
            
                <td width="20%" style="padding-right:120px; padding-left:55px;">
                    <asp:Label ID="lblPoints" runat="server" Text="Points"></asp:Label>
                </td>
                <td width="20%" style="padding-right:100px; padding-left:10px;">
                    <asp:Label ID="lblGetsFrom"  runat="server" Text="Gets From"></asp:Label>
                </td>
                <td width="20%" style="padding-left:15px;">
                    <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                </td>
        </tr>
    </table>
    <asp:GridView runat="server" ID="gridview1" AllowPaging="true" AllowSorting="false"
        AutoGenerateColumns="false" CellPadding="5" CellSpacing="0" GridLines="None"
        OnPageIndexChanging="gridview1_PageIndexChanging" HorizontalAlign="Center" CssClass="gridview"
        Width="100%" OnRowDataBound="gridview1_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <table width="100%" border="0" cellpadding="0" cellpadding="0">
                        <tr>
                        </tr>
                        <td width="20%">
                            <asp:Label runat="server" ID="lblDate" Text='<%#GetExpirationDateString(Eval("creationDate"))%>' />
                        </td>
                        <td width="20%">
                            <asp:Label runat="server" ID="lblAmount" Text='<%# Eval("points")%> ' ForeColor="#C75821"
                                Font-Bold="true" />
                        </td>
                        <td width="20%">
                            <asp:Label runat="server" ID="lblTypeBool" Text='<%#Eval("pointsGetsFrom") %>' />
                        </td>
                        <td width="20%">
                            <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("description") %>' />
                        </td>
                    </table>
                    <div class="cutLineOrange">
                    </div>
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
                    <td colspan="4">
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
            <div class="backtotop">
                <a href="#">
                    <asp:Label ID="lblbackToTop" runat="server" Text="Back To Top"></asp:Label><img src="images/totop.gif" /></a>
            </div>
        </PagerTemplate>
    </asp:GridView>
</div>
