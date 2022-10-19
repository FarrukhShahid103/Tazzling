<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="member_points.aspx.cs"
    Inherits="member_points" Title="TastyGo | Member | Points" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <script language="javascript" type="text/javascript">
      function clientcheck(strString) {
          var points = document.getElementById('ctl00_ContentPlaceHolder1_hfPoints').value;            
          if(points>=1000)
          {
            return (confirm('Convert 1000 points into $10 credits?'));
          }
          else
          {
             MessegeArea('You need atleast 1000 points to convert.' , 'error');
             return false;
          }
          return false;
      }
    </script>
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
                                    Tastygo Points
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="background-color: White; min-height: 450px;">
                    <div style="padding-top: 20px;">
                        <div class="MemberArea_PageHeading" style="padding-left: 15px;">
                           <asp:Label ID="lblAffComBal" runat="server"></asp:Label></div>
                        <div style="font-size: 15px; padding-left: 15px; padding-top: 5px;">                           
                            <div style="float: left; margin-top: 20px;">
                            <asp:HiddenField ID="hfPoints" runat="server" Value="0" />
                                <asp:Button ID="btnWithdrawPoints" runat="server" OnClick="btnWithdrawPoints_Click" OnClientClick="return clientcheck();"
                                    class="button pill" Text="Redeem Points" />
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
                                <div style="padding-top: 10px;">
                                </div>
                                <div>
                                    <div style="width: 100%; background-color: #E6E6E5; height: 30px; overflow: hidden;">
                                        <div style="clear: both; margin-left: 15px; padding-top: 7px;">
                                            <div style="float: left; padding-left: 20px;" class="MemberArea_GridHeading">
                                                Date</div>
                                            <div style="float: left; padding-left: 300px;" class="MemberArea_GridHeading">
                                                Points From</div>
                                            <div style="float: left; padding-left: 300px;" class="MemberArea_GridHeading">
                                                Point</div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; padding-bottom: 10px; padding-left: 15px;">
                                        <asp:GridView runat="server" ID="gridview1" AllowPaging="true" DataKeyNames="karmaPointsID"
                                            AllowSorting="false" OnPageIndexChanging="gridview1_PageIndexChanging" OnRowDataBound="gridview1_RowDataBound"
                                            AutoGenerateColumns="false" CellSpacing="0" GridLines="None" Font-Size="Small"
                                            HorizontalAlign="Center" Width="100%" ShowHeader="false" PageSize="20" RowStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <div style="margin-bottom: 10px;">
                                                                    <td style="text-align: left; padding-left: 20px;">
                                                                        <asp:Label Style="text-align: left;" Width="150px" runat="server" ID="lblDate" Text='<% # Convert.ToString(Eval("createdDate"))!=""?Convert.ToDateTime(Eval("createdDate")).ToString("MM-dd-yyyy H.mm tt"):"" %>' />
                                                                    </td>
                                                                    <td style="text-align: left; padding-left: 130px;">
                                                                        <asp:Label Style="text-align: left;" Width="150px" runat="server" ID="lblTitle" ToolTip='<% # Eval("karmaPointsType")%>'
                                                                            Text='<% # Eval("karmaPointsType")%>' />
                                                                    </td>
                                                                    <td style="text-align: left; padding-left: 130px;">
                                                                        <asp:Label runat="server" ID="lblOrderType" Width="150px" Font-Bold="true" Text='<%# Eval("karmaPoints") %>' />
                                                                    </td>
                                                                </div>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="emptydatarow">
                                                    There are no points.
                                                </div>
                                            </EmptyDataTemplate>
                                            <PagerTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr runat="server" id="topPager">
                                                        <td>
                                                            <div style="padding-top: 35px;">
                                                                <div>
                                                                    <div>
                                                                        <div>
                                                                            <div class="button-group">
                                                                                <asp:LinkButton ID="lnkTopPrev" CssClass="button icon arrowleft   green" Enabled='<%# displayPrevious %>'
                                                                                    CommandName="Page" CommandArgument="Prev" runat="server" Text="Previous"></asp:LinkButton>
                                                                                <asp:Repeater ID="rptrPage" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton Style="min-width: 0px !important;" ID="lnkPage" CssClass='<%# Convert.ToInt32((gridview1.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                                            OnClick="lnkPage_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                                                            CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:LinkButton Style="width: 35px;" ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>'
                                                                                    CommandName="Page" CommandArgument="Next" CssClass="button icon arrowright   green"
                                                                                    Text="Next"></asp:LinkButton>
                                                                            </div>
                                                                            <div>
                                                                            </div>
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
                                            <PagerSettings PageButtonCount="10" Position="Bottom" />
                                            <PagerStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div style="height: 5px;">
                                </div>
                                <div align="center">
                                    <div style="background-color: #E6E6E5; height: 50px; margin-top: 5px;">
                                        <div style="width: 46px; height: 42px; float: left; margin-top: 3px; padding-left: 5px;">
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
