<%@ Page Title="Gift Card" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="member_gift_card.aspx.cs" Inherits="member_gift_card" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Src="~/UserControls/GiftCard/memberusedGiftCard.ascx" TagPrefix="RedSgnal"
    TagName="usedGiftCards" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function preCheckSelected() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (document.getElementById(list[i]).checked) {
                    showBox();
                    return false;
                }
            }
           csscody.alert('<br><div id="Success" style="margin-left:50px;"><h1>Please select the Gift Card(s) to send!</h1></Div><br>');

            return false;
        }
        function checkAll() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            if (document.getElementById("ctl00_ContentPlaceHolder1_gvGiftCards_ctl01_HeaderLevelCheckBox").checked) {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = true;
                }
            }
            else {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = false;
                }
            }
        }

        function ChangeHeaderAsNeeded() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (!document.getElementById(list[i]).checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvGiftCards_ctl01_HeaderLevelCheckBox").checked = false;
                    return;
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_gvGiftCards_ctl01_HeaderLevelCheckBox").checked = true;
        }

    </script>
    
    

    <asp:TextBox ID="hiddenIds" runat="server" Style="display: none;"></asp:TextBox>
    <div style="clear:both; background-color:#00AEFF; overflow:hidden;">
    <usrCtrl:subMenu ID="subMenu1" runat="server" />
   </div>
   <div style="width: auto; padding-bottom: 10px; background-color:#8AD3FE;" align="center">
    <div class="profilecontainer" >
        <div style="float:left;width:100%; padding-top:15px; padding-bottom:15px;">
            <RedSgnal:usedGiftCards ID="usedGiftCards1" runat="server" />
        </div>
        <div style="float:left;width:100%; padding-top:15px; padding-bottom:15px; border-top:solid 3px #05AEFF;">
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td width="40%">
                    
                    
<div style="text-align:left; padding-left:10px;">
        <asp:Label ID="lblGiftCard" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Gift Card"
            Font-Size="29px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
            
            </div>
                        
                    </td>
                    <td width="20%">
                        <asp:Panel ID="pnlMsg" runat="server" Visible="false" Style="text-align: center">
                            <asp:Image ImageUrl="~/Images/Checked.png" ID="imgGridMessage" runat="server"
                                align="texttop" />&nbsp;
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td align="right" style="padding-right: 10px;">
                        <asp:Button ID="btnsendGiftCard" runat="server" Text="Send Gift Card" CssClass=" btnsendgiftcard"
                            OnClientClick="return preCheckSelected();" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear gridborder"">
            <table cellpadding="0" cellspacing="0" class="GridHeader" width="100%">
                <tr>
                    <td style="text-align: left" width="18%">
                        <asp:CheckBox Visible="false" runat="server" value='<% # Eval("giftCardId") %>' Enabled='<% # getCardStatus(Eval("createdBy").ToString(), Eval("takenBy").ToString()) %>'
                            ID="RowLevelCheckBox" onclick="ChangeHeaderAsNeeded()" />
                    </td>
                    <td style="text-align: left" width="22%">
                        Gift Card Code
                    </td>
                    <td width="25%">
                        Created Date
                    </td>
                    <td width="15%">
                        Price
                    </td>
                    <td width="20%">
                        Redeemed By
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gvGiftCards" AllowPaging="true" AllowSorting="false"
                AutoGenerateColumns="false" CellPadding="5" CellSpacing="0" GridLines="None"
                OnPageIndexChanging="gvGiftCards_PageIndexChanging" HorizontalAlign="Center"
                Width="95%" CssClass="gridview" OnRowDataBound="gvGiftCards_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="text-align: left" width="15%">
                                        <asp:CheckBox runat="server" value='<% # Eval("giftCardId") %>' Enabled='<% # getCardStatus(Eval("createdBy").ToString(), Eval("takenBy").ToString()) %>'
                                            ID="RowLevelCheckBox" onclick="ChangeHeaderAsNeeded()" />
                                        <asp:Label ID="lblID1" runat="server" Text='<% # Eval("giftCardId") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td style="text-align: left" width="25%">
                                        <asp:Label runat="server" ID="lblBalance" Text='<%# Eval("giftCardCode")%>' />
                                    </td>
                                    <td width="25%">
                                        <asp:Label runat="server" ID="lblExpirationDate" Text='<%#GetExpirationDateString(Eval("creationDate"))%>' />
                                    </td>
                                    <td width="15%">
                                        <asp:Label runat="server" ID="lblFunds" Text='<%# "$" + Eval("giftCardAmount") + "&nbsp;&nbsp;" + Eval("currencyCode") %>'
                                            ForeColor="#636363" Font-Bold="true" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblFrom" Text='<%# GetCardExplain(Eval("createdBy").ToString(), Eval("takenBy").ToString()) %>'
                                            ForeColor="#636363" Font-Bold="true" />
                                    </td>
                                </tr>
                            </table>
                            <div class="cutLineOrange">
                            </div>
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
                                            <div class="pagerContent" style="background-color:#04AFFF;">
                                                <asp:LinkButton ID="lnkTopPrev" Font-Size="12px" Enabled='<%# displayPrevious %>' CommandName="Page"
                                                    CommandArgument="Prev" runat="server" CssClass="GridLink" Text="Previous"></asp:LinkButton>
                                                <asp:Repeater ID="rptrPage" runat="server">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:LinkButton ID="lnkPage" CssClass="GridNumeric" Font-Size="12px" 
                                                            OnClick="lnkPage_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                            CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>&nbsp;
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:LinkButton ID="lnkTopNext" Font-Size="12px" runat="server" Enabled='<%# displayNext %>' CommandName="Page"
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
        <div width="100%" style="padding-top: 15px">
            <div width="100%" class=" gcardtips">
                <div width="70%" style="padding-left: 10px">
                    <table>
                        <tr>
                            <td>
                                <asp:Image ID="imgtips" runat="server" ImageUrl="~/Images/memberTip.png" />
                            </td>
                            <td>
                                Purchasing gift card to your friend and sending to them, you'll become their referer
                                and you can start earning commission.
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div widht="100%" style="padding-top: 15px; padding-bottom:15px;">
            <div>
               <asp:Button runat="server" ID="btnNext" CssClass="btn_giftcard" OnClientClick="document.location.href='checkoutgiftcard.aspx'" />
            </div>
        </div>
        <RedSgnal:TotalFunds runat="server" ID="TotalFunds1" />
    </div>

    <script type="text/javascript" language="javascript">
        function showBox() {
            document.getElementById('confirmationBoxBackGround').style.display = 'block';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlTiming').style.display = 'block';

            document.getElementById('ctl00_ContentPlaceHolder1_txtEmailTo').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtMessage').value = '';
            return false;
        }
    </script>

    <div id="confirmationBoxBackGround" style="display: none;">
        <div id="confirmBox">
        </div>
    </div>
    <asp:Panel ID="pnlTiming" runat="server" CssClass="confirmationStyleTiming" Width="600px">
        <div class="headingStripConfirmTiming" style="width: 100%;">
            <div class="floatLeft">
                <asp:Label ID="lblConfirmationBox" runat="server" Text="Tasty Go-Send gift card(s)"></asp:Label></div>
            <div class="floatRight" style="padding-right: 15px;">
                <asp:LinkButton ID="lbClose" ForeColor="White" Style="text-decoration: none; outline: none"
                    runat="server" Text="[ X ]" OnClientClick="document.getElementById('confirmationBoxBackGround').style.display='none'; document.getElementById('ctl00_ContentPlaceHolder1_pnlTiming').style.display='none'; return false;"></asp:LinkButton>
            </div>
        </div>
        <div align="left" style="padding: 20px;">
            <table>
             <tr>
                    <td align="right">
                       <strong> First Name</strong>
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="TextBoxDeal" />                      
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtFirstName" ErrorMessage="First Name required!" ValidationGroup="Card"
                            Display="None">                                                               
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                               
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator3">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                 <tr>
                    <td align="right">
                       <strong> Last Name</strong>
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtLastName" CssClass="TextBoxDeal" />                      
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtLastName" ErrorMessage="Last Name required!" ValidationGroup="Card"
                            Display="None">                                                               
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                               
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator4">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                       <strong> Email Address</strong>
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="txtEmailTo" CssClass="TextBoxDeal" />
                        <cc1:RegularExpressionValidator ID="revEmailAddress" runat="server" ControlToValidate="txtEmailTo"
                            Display="None" ErrorMessage="Invalid email address!" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="Card"></cc1:RegularExpressionValidator>
                        <cc2:ValidatorCalloutExtender ID="vceREEmail" runat="server" TargetControlID="revEmailAddress">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtEmailTo" ErrorMessage="Email required!" ValidationGroup="Card"
                            Display="None">                                                               
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                               
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator2">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                       <strong> Message</strong>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtMessage" runat="server" Height="100px" TextMode="MultiLine" CssClass="TextBoxDeal" />
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMessage"
                            Display="None" ErrorMessage="Message required!" SetFocusOnError="true" ValidationGroup="Card">                            
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                        </cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSend" runat="server" ValidationGroup="Card" Text="Send" CssClass="btn_orange_smaller"
                            OnClick="btnSend_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    </div>
</asp:Content>
