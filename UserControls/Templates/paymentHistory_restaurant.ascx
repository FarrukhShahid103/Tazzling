<%@ Control Language="C#" AutoEventWireup="true" CodeFile="paymentHistory_restaurant.ascx.cs"
    Inherits="Takeout_UserControls_Templates_paymentHistory_restaurant" %>
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

<div style="padding-top: 20px;">
    <h3 class="marginTB10 hr">
        <asp:Label ID="lblWithdrawFunds" Text="Payment History" runat="server"></asp:Label></h3>
</div>
<div id="divSrchHeader" style="text-align: center; border: 1px solid gray; background-color: Beige;
    margin: 10px; padding: 6px;">
    <div>
        <%= "<b>" + "Veiw transcation by" + "</b> &nbsp;&nbsp;" + "Year" + ":&nbsp;&nbsp;"%><asp:DropDownList
            runat="server" ID="ddlYear" />
        &nbsp;&nbsp;&nbsp;&nbsp;<%= "Month" + ":&nbsp;&nbsp;"%><asp:DropDownList runat="server"
            ID="ddlMonth" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnSelect" OnClick="btnSelect_Click" />
    </div>
</div>
<div class="height15">
</div>
<asp:GridView runat="server" ID="gridview1" AllowPaging="true" AllowSorting="false"
    AutoGenerateColumns="false" CellPadding="5" CellSpacing="0" GridLines="Horizontal" BorderColor="#cfcfcf"
    OnPageIndexChanging="gridview1_PageIndexChanging" HorizontalAlign="Center" Width="80%"
    CssClass="GridHeader" HeaderStyle-CssClass="tableheader2" OnRowDataBound="gridview1_RowDataBound">
    <Columns>
        <asp:TemplateField ItemStyle-Width="25%">
            <HeaderTemplate>
                <asp:Label runat="server" ID="lblRequestDate" Text="Date"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lblDate" Text='<%#GetExpirationDateString(Eval("Date"))%>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
            <HeaderStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="25%">
            <HeaderTemplate>
                <asp:Label runat="server" ID="lblDescriptionHead" Text="Description"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("Description") %> '
                    ForeColor="#C75821" Font-Bold="true" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="25%">
            <HeaderTemplate>
                <asp:Label runat="server" ID="lblAmountHead" Text="Amount"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lblAmount" Text='<%# "$" + Eval("Amount")%>' />
                <asp:Label runat="server" ID="lblAmount2" Visible="false" Text='<%# Eval("Amount")%>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="25%">
            <HeaderTemplate>
                <asp:Label runat="server" ID="lblBalanceHead" Text="Balance"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lblBalance" Text='<%# "$" + Eval("Balance") %>' />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
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
