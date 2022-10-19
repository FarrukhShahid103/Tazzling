<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="member_affiliate.aspx.cs"
    Inherits="member_affiliate" Title="TastyGo | Member | Affiliate Partner" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <div>
        <div class="DetailPage2ndDiv">
            <div style="width: 980px; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                    <div style="clear: both; padding-top: 10px; margin-bottom: 10px;">
                        <div class="DetailPageTopDiv">
                            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                                <div id="PageTitle" class="PageTopText" style="float: left;">
                                    My Affiliate Area
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="background-color: White; min-height: 450px;">
                    <div style="padding-top: 40px;">
                        <div class="MemberArea_PageHeading" style="padding-left: 15px;">
                            My Affiliate Commission</div>
                        <div style="font-size: 15px; padding-left: 15px; padding-top: 5px;">
                            <div style="float: left; margin-right: 10px;">
                                <asp:Label ID="lblAffComBal" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; margin-top: -5px;">
                                <a href="member_withdraw.aspx" class="button pill icon doller">Withdraw Affiliate Commission</a>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                        <div style="width: auto; padding-bottom: 10px;" align="center">
                            <div style="padding-top: 10px; padding-bottom: 10px;">
                                <div style="color: #ed9702; font-size: 13px; font-weight: bold; padding-left: 20px;
                                    display: none;">
                                    <asp:Label ID="lblAffComTotal" Text="Total Commission Earned" runat="server"></asp:Label>
                                </div>
                                <div style="padding: 5px; text-align: center;">
                                    <h4>
                                        <asp:Label ID="lblHeaderMessage" Visible="false" runat="server" /></h4>
                                </div>
                                <div style="height: auto; overflow: hidden;">
                                    <div style="text-align: left; overflow: hidden; height: auto; width: 100%; background: #E6E6E5">
                                        <div style="padding-left: 15px;">
                                            <div style="float: left; padding-top: 33px;">
                                                <div class="MemberArea_PageHeading">
                                                    Veiw Transcation by
                                                </div>
                                            </div>
                                            <div style="float: left; margin-left: 10px;">
                                                <div class="ItemHiding">
                                                    Year
                                                </div>
                                                <div style="margin-bottom: 5px; clear: both;">
                                                    <asp:DropDownList Width="185px" runat="server" CssClass="TextBox" ID="ddlYear" />
                                                    <cc1:RequiredFieldValidator ID="rfvYear" InitialValue="Select Year" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="ddlYear" ErrorMessage="Please select a year!"
                                                        ValidationGroup="select" Display="None">                            
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="vceYear" runat="server" TargetControlID="rfvYear">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; margin-left: 10px;">
                                                <div class="ItemHiding">
                                                    Month
                                                </div>
                                                <div style="margin-bottom: 5px; clear: both;">
                                                    <asp:DropDownList Width="185px" runat="server" CssClass="TextBox" ID="ddlMonth">
                                                        <asp:ListItem Text="Select Month" Value="Select Month" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Select Month"
                                                        SetFocusOnError="true" runat="server" ControlToValidate="ddlMonth" ErrorMessage="Please select a month!"
                                                        ValidationGroup="select" Display="None">                            
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; margin-left: 10px; padding-top: 25px;">
                                                <asp:Button runat="server" CssClass="button primary big pill" ID="btnSelect" Text="Show Record"
                                                    OnClick="btnSelect_Click" CausesValidation="true" ValidationGroup="select" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-top: 10px;">
                                </div>
                                <div>
                                    <div style="width: 100%; background-color: #E6E6E5; height: 30px; overflow: hidden;">
                                        <div style="clear: both; margin-left: 15px; padding-top: 7px;">
                                            <div style="float: left;" class="MemberArea_GridHeading">
                                                Date</div>
                                            <div style="float: left; padding-left: 270px;" class="MemberArea_GridHeading">
                                                Earned From</div>
                                            <div style="float: left; padding-left: 60px;" class="MemberArea_GridHeading">
                                                Earned Amount</div>
                                            <div style="float: left; padding-left: 135px;" class="MemberArea_GridHeading">
                                                Gross Revenue</div>
                                            <div style="float: left; padding-left: 55px;" class="MemberArea_GridHeading">
                                                Notes</div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; padding-bottom: 10px; padding-left: 15px;">
                                        <asp:GridView runat="server" ID="gridview1" AllowPaging="false" AllowSorting="false"
                                            AutoGenerateColumns="false" CellSpacing="0" GridLines="None" Font-Size="Small"
                                            HorizontalAlign="Center" Width="100%" ShowHeader="false" PageSize="15" RowStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <div style="margin-bottom: 10px;">
                                                                    <td width="15%" style="text-align: left;">
                                                                        <asp:Label Style="text-align: left;" Width="150px" runat="server" ID="lblDate" Text='<% # Convert.ToString(Eval("createdDate"))!=""?Convert.ToDateTime(Eval("createdDate")).ToString("MM-dd-yyyy H.mm tt"):"" %>' />
                                                                    </td>
                                                                    <td width="15%" style="text-align: left;">
                                                                        <asp:Label style="text-align:left;" Width="150px" runat="server" ID="lblTitle" ToolTip='<% # Eval("Title")%>'
                                                                        Text='<% # Eval("Title").ToString().Length > 20?Eval("Title").ToString().Substring(0,19)+"...":Eval("Title")  %>' />
                                                                        
                                                                    </td>
                                                                    <td width="15%" style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblOrderType" Font-Bold="true" Text='<%# Eval("Name") %>' />
                                                                    </td>
                                                                    <td width="25%" style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblOrderFrom" Text='<%#"$"+ Eval("gainedAmount")+ " CAD (" + Eval("affCommPer") + "%)" %>' />
                                                                    </td>
                                                                    <td width="15%" style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblShippingMethod" Text='<%# "$"+ (float.Parse(Eval("totalAmt").ToString())*float.Parse(Eval("Qty").ToString())).ToString() + " CAD" %>' />
                                                                    </td>
                                                                    <td width="25%" style="text-align: left;">
                                                                        <asp:Label runat="server" ID="lblStatus" Font-Bold="true" Text='<%# Eval("gainedtype")%>' />
                                                                    </td>
                                                                </div>
                                                            </tr>
                                                        </table>
                                                        <div class="cutLineOrange">
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="emptydatarow">
                                                    There are no affliate partner credits.
                                                </div>
                                            </EmptyDataTemplate>
                                            <PagerTemplate>
                                                <div class="pagerstyle">
                                                    <div>
                                                        <div>
                                                            <asp:LinkButton ID="lbPrev" runat="server" CssClass="pageraction" CausesValidation="False"
                                                                CommandArgument="Prev" CommandName="Page" />
                                                            <asp:Label runat="server" ID="lblNumeric" />
                                                            <asp:LinkButton ID="lbNext" runat="server" CssClass="pageraction" CausesValidation="False"
                                                                CommandArgument="Next" CommandName="Page" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </PagerTemplate>
                                            <PagerSettings PageButtonCount="10" Position="TopAndBottom" />
                                            <PagerStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div>
                                    <div style="color: Black; height: 30px; overflow: hidden; background-color: #E6E6E5;">
                                        <div style="padding-top: 7px;">
                                            <div style="float: left; width: 612px; font-size: 15px; text-align: right;">
                                                <asp:Label ID="lblTotalB" runat="server" Text="This Period Earing:"></asp:Label>
                                            </div>
                                            <div style="width: 100px; float: right; padding-top: 0px; height: 100%;">
                                                <asp:Label runat="server" CssClass="MemberArea_GridHeading" ID="lblPeriodSales"></asp:Label></div>
                                        </div>
                                    </div>
                                </div>
                                <div style="height: 5px;">
                                </div>
                                <div align="center">
                                    <div style="background-color:#E6E6E5; height:50px;margin-top:5px;">
                                        <div style="width: 46px; height: 42px; float: left; margin-top: 3px; padding-left:5px;">
                                            <asp:Image Width="46px" Height="46px" ID="imgqmark" runat="server" ImageUrl="~/Images/question_mark.png" />
                                        </div>
                                        <div style="padding-top: 8px;">
                                            If you have any questions regarding the affiliate area, please contact us at <a style="color: #f99d1c;
                                                font-weight: bold; text-decoration: none;">1855-295-1771</a> or email at <a style="color: #f99d1c;
                                                    font-weight: bold;">support@tazzling.com</a></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       
    </div>   
</asp:Content>
