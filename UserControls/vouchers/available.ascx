<%@ Control Language="C#" AutoEventWireup="true" CodeFile="available.ascx.cs" Inherits="UserControls_vouchers_available" %>
<%@ Reference VirtualPath="~/UserControls/vouchers/used.ascx" %>
    <div style="padding-top: 10px; width: 950px;">
            <div class="height15 clear" align="left" style="text-align: left; font-family: Arial;
                font-size: 16px; padding-bottom: 10px;">
                <asp:Image ID="imgGridMessage" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Black" CssClass="fontStyle"></asp:Label>
            </div>    
              <div class="height15 clear" align="left" style="text-align: left; font-family: Arial;
                font-size: 16px; padding-bottom: 10px;">
                <asp:Label ID="Label6" runat="server" ForeColor="Black" CssClass="fontStyle" Text="Note:You can view your gift vouchers in ''My Gift''"></asp:Label>
            </div>     
            <table cellpadding="0" cellspacing="0" class="memberAreaTable" style="width: 950px;">
                <tr>
                    <td class="cellFirst1" style="padding-left: 300px;">
                        <b>
                            <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label></b>
                    </td>
                    <td class="cellSecond1" style="padding-left: 20px;">
                        <b>
                            <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label></b>
                    </td>
                    <td class="cellSecond1" style="padding-left: 40px;">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Expiry Date"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gridview1" DataKeyNames="dOrderID" AllowPaging="true"
                AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" PageSize="5" GridLines="None"
                OnPageIndexChanging="gridview1_PageIndexChanging" Width="950px" ShowHeader="false"
                OnRowDataBound="gridview1_RowDataBound" OnRowCommand="gridview1_Login">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div style="padding-bottom: 5px; padding-top: 5px;">
                                <div style="float: left; padding-right: 10px; padding-left: 5px;">
                                    <div style="height: 121px; width: 168px; border: solid 1px #ACD245; text-align: center;
                                        vertical-align: middle;">
                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                        <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="121px" width="168px"
                                            alt="" />
                                    </div>
                                </div>
                                <div style="float: left;">
                                    <div style="font-family: Arial; font-size: 19px; color: White; width: 450px;">
                                        <%#Eval("title")%>
                                    </div>
                                    <div>
                                        <asp:GridView ID="gvSubItem" runat="server" DataKeyNames="detailID" AllowPaging="false"
                                            AutoGenerateColumns="false" OnRowCommand="gvSubItem_Login" ShowHeader="false"
                                            GridLines="None">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div style="padding-top: 5px;">
                                                            <div style="float: left; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                                                font-size: 19px;">
                                                                <asp:Label ID="lblDetailID" runat="server" Text='<%#Eval("detailID")%>' Visible="false"></asp:Label>
                                                                <%# getDealCode(Eval("dealOrderCode"),Eval("status"))%></div>
                                                            <div style="float: left; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                                                font-size: 13px;">
                                                                <asp:UpdatePanel ID="upDownloadFile" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton CssClass="GridLink" ID="LinkButton1" runat="server" CommandName="download"
                                                                            Visible='<%# getDetailStatus(Eval("status"))%>' CommandArgument='<%# (Eval("detailID") + "," + Eval("dOrderID"))%>'
                                                                            Text='Print in PDF' />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="LinkButton1" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <%--<asp:HyperLink ID="hlPrint" runat="server" Visible='<%# getDetailStatus(Eval("status"))%>'
                                                                    NavigateUrl='<%# getDealPrintPath(Eval("dealOrderCode"),Eval("status"))%>' Text="Print in PDF"
                                                                    Target="_blank"></asp:HyperLink>--%></div>
                                                            <div style="float: left; padding-left: 5px; padding-right: 5px;">
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("markUsed")%>' Visible="false"></asp:Label>
                                                                <asp:LinkButton CssClass="GridLink" ID="lbMarkAsSold" runat="server" CommandName="Login"
                                                                    Visible='<%# getDetailStatus(Eval("status"))%>' CommandArgument='<%#Eval("detailID")%>'
                                                                    Text='<%# getDealStatus(Eval("markUsed"))%>' />
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle rowClass4Height" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle rowClass4Height" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div style="float: left; padding-left: 5px; padding-right: 5px; font-family: Arial;
                                    font-size: 19px; width: 100px;">
                                    <%# Eval("status")%>
                                </div>
                                <div style="float: left; padding-left: 5px; padding-right: 5px; font-size: 16px;">
                                    <asp:LinkButton ID="lblCancel" CssClass="GridLink" Font-Size="16px" runat="server"
                                        CommandName="Login" OnClientClick="return confirm('Are you sure you want to cancel this amazing deal?');"
                                        CommandArgument='<%#Eval("dOrderID")%>' Text='<%# getOrderStatus(Eval("status"))%>' />
                                </div>
                                <div style="float: right; padding-right: 15px;">
                                    <%# GetDateString(Eval("voucherExpiryDate"))%>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="emptydatarow">
                        <asp:Label ID="lblNoData" runat="server" Text="There is no deal codes to be shown."></asp:Label>
                    </div>
                </EmptyDataTemplate>
                <PagerTemplate>
                    <table cellpadding="0" cellspacing="0" width="100%" class="search_result">
                        <tr runat="server" id="topPager">
                            <td colspan="4">
                                <div class="pagerstyle">
                                    <div>
                                        <div>
                                            <div class="pagerContent" style="background-color: #04AFFF;">
                                                <asp:LinkButton ID="lnkTopPrev" Enabled='<%# displayPrevious %>' CommandName="Page"
                                                    CommandArgument="Prev" runat="server" CssClass="GridLink" Text="Previous"></asp:LinkButton>
                                                <asp:Repeater ID="rptrPage" runat="server">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:LinkButton ID="lnkPage" CssClass="GridNumeric" OnClick="lnkPage_Click"
                                                            Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                            CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>&nbsp;
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
                <PagerSettings PageButtonCount="10" Position="Bottom" />
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>          
        </div>
