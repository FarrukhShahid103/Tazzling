<%@ Page Title="TastyGo | Member | My TastyGos" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="member_receievedDeals.aspx.cs" Inherits="member_receievedDeals" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <div style="clear:both; background-color:#00AEFF; overflow:hidden;">
    <usrCtrl:subMenu ID="subMenu1" runat="server" />
   </div>
    <div style="width: auto; padding-bottom: 10px; background-color:#8AD3FE" align="center">
    <div style="width: auto; padding-bottom: 10px;" align="center">
        <div style="padding-top: 50px; width: 950px; min-height:400px;">
            <div class="height15 clear" align="left" style="text-align: left; font-family: Arial;
                font-size: 16px; padding-bottom: 10px;">
                <asp:Image ID="imgGridMessage" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Black" CssClass="fontStyle"></asp:Label>
            </div>
            <table cellpadding="0" cellspacing="0" class="memberAreaTable" style="width: 950px;">
                <tr>
                    <td class="cellFirst1" style="padding-left: 200px;">
                        <b>
                            <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label></b>
                    </td>
                    <td class="cellSecond1" style="padding-left: 20px;">
                        <b>
                            <asp:Label ID="Label2" runat="server" Text="Code"></asp:Label></b>
                    </td>
                     <td class="cellSecond1" style="padding-left: 10px;">
                        <b>
                            <asp:Label ID="Label3" runat="server" Text="Security Code"></asp:Label></b>
                    </td>
                    <td class="cellSecond1" style="padding-left: 40px;">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Actions"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gridview1" DataKeyNames="dOrderID" AllowPaging="true"
                AutoGenerateColumns="false" CellPadding="0" CellSpacing="0"  PageSize="5" GridLines="None" OnPageIndex Changing="gridview1_PageIndexChanging"
                Width="950px" ShowHeader="false" OnRowDataBound="gridview1_RowDataBound" OnRowCommand="gridview1_Login">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div style="padding-bottom: 5px; padding-top: 5px;">
                                <div style="float: left; padding-right: 10px; padding-left: 5px;">
                                    <div style="height: 121px; width: 168px; border: solid 0px #ACD245; text-align: center;
                                        vertical-align: middle;">
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                        <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="121px" width="168px"
                                            alt="" />
                                    </div>
                                </div>
                                <div style="float: left;">
                                    <div style="font-family: Arial; font-size: 19px; color: White; width: 300px; padding-top:30px;">
                                        <%#Eval("title")%>
                                    </div>
                                </div>
                                <div style="float: left; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                    font-size: 19px; width: 125px;">
                                    <div style="float: left; padding-top:30px; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                        font-size: 19px;">
                                        <asp:Label ID="lblDetailID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                        <%# getDealCode(Eval("dealOrderCode"),Eval("status"))%></div>
                                </div>
                                 <div style="float: left; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                    font-size: 19px; width: 125px;">
                                    <div style="float: left; padding-top:30px; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                        font-size: 19px;">
                                        <%#Eval("voucherSecurityCode")%>
                                    </div>
                                </div>
                                <div style="float: left; padding-left: 25px; padding-right: 5px; font-size: 16px;">
                                <div style=" padding-top:30px;">
                                    <div style="float: left; padding-left: 5px; font-size:16px; padding-right: 5px; font-family: Arial;
                                        font-size: 16px;">
                                        <a class="GridLink" href='<%# getDealPrintPath(Eval("dealOrderCode"),Eval("status"))%>' target="_blank">
                                            Print</a></div>
                                    <div style="float: left; padding-left: 5px; padding-right: 5px;">                                       
                                        <asp:LinkButton ID="lbMarkAsSold" class="GridLink" runat="server" CommandName="Login" Visible='<%# getDetailStatus(Eval("status"))%>'
                                            CommandArgument='<%#Eval("detailID")%>' Text='<%# getDealStatus(Eval("markUsed"))%>' />
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="emptydatarow">
                        <asp:Label ID="lblNoData" runat="server" Text="There is no tastygo yet."></asp:Label>
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
                                                            CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                        &nbsp;
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
                <PagerSettings PageButtonCount="10" Position="Bottom" />
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>
            <%--<div width="100%" style="padding-top: 15px">
                <div width="100%" class=" gcardtips">
                    <div width="70%" style="padding-left: 10px">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="imgtips" runat="server" ImageUrl="~/Images/memberTip.jpg" />
                                </td>
                                <td>
                                    You can share your favourite restaurant to your friend,and when they click your
                                    link and join the site, they become your refferals.You can start earning Commission
                                    when they order!
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>--%>
        </div>
    </div>
    </div>
</asp:Content>
