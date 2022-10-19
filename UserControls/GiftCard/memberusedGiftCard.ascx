<%@ Control Language="C#" AutoEventWireup="true" CodeFile="memberusedGiftCard.ascx.cs"
    Inherits="Takeout_UserControls_GiftCard_memberusedGiftCard" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>

<script type="text/javascript">
    $(document).ready(function() {
     $('#ctl00_ContentPlaceHolder1_usedGiftCards1_txtGiftCardCode').tipsy({ gravity: 's' });
     
      });
       
         function EmptyFieldvalidate(oSrc, args) {        
            if (args.Value != "") {                  
                args.IsValid = true;
                return;        
            }
            else
            {
                 $("#"+oSrc.controltovalidate).addClass("DiscussionError");                                                                                          
                 args.IsValid = false;
                 return; 
            }
      }
</script>

<div class="PagesBG" style="min-height: 400px;">
    <asp:UpdatePanel ID="upComment" runat="server">
        <ContentTemplate>
            <div style="padding-top: 20px; padding-bottom: 5px; word-spacing: 3px;">
                <div style="text-align: left; padding-left: 10px;">
                    <asp:Label ID="label1" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Deposit Gift Card"
                        Font-Size="29px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
                </div>
                <br />
                <div style="width: 90%">
                    <div style="float: left; padding-top: 10px;">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="text-align: left; padding-right: 5px;">
                                    <asp:Label ID="lblGiftCardNumnber" runat="server" ForeColor="#636363" Font-Bold="true"
                                        Font-Size="16px" Text="Gift Card Code:"></asp:Label>
                                </td>
                                <td width="240px">
                                    <asp:TextBox ID="txtGiftCardCode" CssClass="TextBoxDeal" onfocus="this.className='TextBoxDeal'" Title="Please enter valid Card No"
                                        runat="server" Width="221px"></asp:TextBox>
                                    <cc1:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="EmptyFieldvalidate"
                                        ControlToValidate="txtGiftCardCode" Display="none" ValidateEmptyText="true" ValidationGroup="GiftCard"
                                        ErrorMessage="" SetFocusOnError="false"></cc1:CustomValidator>
                                </td>
                                <td width="350px">
                                    <div style="padding-left: 4px;">
                                        <div style="float: left; padding-right: 5px">
                                            <asp:Image ID="imgGridMessage" runat="server" Visible="true" ImageUrl="~/Images/error.png" />
                                        </div>
                                        <div>
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="padding-top: 10px; font-size: 16px;">
                                    <asp:Button runat="server" ID="btnVerifyCode" CssClass="btn_yellow_more_bigger" ValidationGroup="GiftCard"
                                        Text="Enter Gift Card" OnClick="btnVerifyCode_Click" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: right; padding-bottom: 10px;">
                        <div style="width: 130px; padding-left: 10px; float: right; padding-bottom: 10px;
                            padding-top: 10px;" class="gcardtips">
                            <asp:Image ID="imgtips" runat="server" ImageUrl="~/Images/memberTip.png" />
                            <br />
                            <asp:Label ID="lbltip" runat="server" Text="You can deposit your own gift card"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div width="100%" style="clear: both;">
                <table cellpadding="0" cellspacing="0" class="GridHeader" width="100%">
                    <tr>
                        <td style="text-align: left;" width="18%">
                        </td>
                        <td style="text-align: center;" width="22%">
                            Card Value
                        </td>
                        <td width="25%">
                            Card Remain Value
                        </td>
                        <td width="15%" style="padding-left: 20px;">
                            Card From
                        </td>
                        <td width="20%" style="padding-left: 20px;">
                            Created Date
                        </td>
                    </tr>
                </table>
                <asp:GridView runat="server" ID="gvGiftCards" AllowPaging="true" AllowSorting="false"
                    AutoGenerateColumns="false" CellPadding="5" CellSpacing="0" GridLines="None"
                    OnPageIndexChanging="gvGiftCards_PageIndexChanging" HorizontalAlign="Center"
                    Width="100%" OnRowDataBound="gvGiftCards_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: left" width="25%">
                                            <%#GetGiftCardImage(Eval("gainedAmount").ToString())%>'
                                        </td>
                                        <td style="text-align: left" width="18%">
                                            <asp:Label runat="server" ID="lblcardvalue" Text='<%# "$" + Eval("gainedAmount") + "&nbsp;&nbsp;" + Eval("currencyCode") %>'
                                                ForeColor="White" Font-Bold="true" />
                                        </td>
                                        <td width="25%">
                                            <asp:Label runat="server" ID="lblcardremain" Text='<%# "$" + Eval("remainAmount") + "&nbsp;&nbsp;" + Eval("currencyCode") %>'
                                                ForeColor="White" Font-Bold="true" />
                                        </td>
                                        <td width="15%">
                                            <asp:Label runat="server" CssClass="padding_R10" ID="lblcardfrom" Text='<%# GetCardExplain(Eval("createdBy").ToString(), Eval("fromId").ToString()) %>'
                                                ForeColor="White" Font-Bold="true" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" CssClass="padding_R20" ID="lblExpirationDate" Text='<%#GetExpirationDateString(Eval("creationDate"))%>' />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="emptydatarow">
                            <asp:Label ID="lblEmptyDate" runat="server" Text="There is no data to display."></asp:Label>
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
                                                        CommandArgument="Prev" runat="server" CssClass="GridLink" Text="Previous"></asp:LinkButton>
                                                    <asp:Repeater ID="rptrPage" runat="server">
                                                        <ItemTemplate>
                                                            &nbsp;<asp:LinkButton ID="lnkPage" CssClass="GridLink" OnClick="lnkPage_Click" Font-Underline="false"
                                                                CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>' CommandArgument='<%# Eval("pageNo") %>'
                                                                runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>&nbsp;
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:LinkButton ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>' CommandName="Page"
                                                        CommandArgument="Next" CssClass="GridLink" Text="Next"></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
