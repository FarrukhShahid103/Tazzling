<%@ Page Title="Tastygo | Past Deals" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="pastDeals.aspx.cs" Inherits="pastDeals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                        Past Deals
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 100%; background-color: White; min-height: 450px;">
                    <div class="DetailTheDetailDiv" style="font-size: 17px; font-weight: bold;">
                        <div style="float: left; padding: 10px 0px 0px 15px;">
                            <asp:Label ID="label8" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div style="clear: both;">
                        <asp:DataList runat="server" ID="dtlPastDeals" DataKeyField="dealId" AllowPaging="true"
                            AllowSorting="false" AutoGenerateColumns="false" RepeatColumns="2" RepeatDirection="Horizontal"
                            CellPadding="0" CellSpacing="0" GridLines="None" HorizontalAlign="Left" ShowHeader="false"
                            RowStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div style="border: solid 3px #e6e6e5; width: 343px;  margin-left:100px; margin-bottom:30px; overflow: hidden;">
                                    <%--<a href='<%# strCurrentCityName +"_"+Eval("dealid") %>' style="text-decoration: none;">--%>
                                    <div style="width: 343px; height: 222px;">
                                        <div style="height: auto; background-color: #5F5F5F; padding-top: 10px; padding-left: 10px;
                                            clear: both;">
                                            <div style="clear: both; font-size: 15px; font-weight: bold;">
                                                <asp:Label ID="HyperLink1" ForeColor="White" runat="server" Text='<%# Convert.ToString(Eval("shortTitle")).ToString().Trim()!="" ? (Convert.ToString(Eval("shortTitle")).Trim().Length < 35 ? Eval("shortTitle") : Convert.ToString(Eval("shortTitle")).Substring(0, 32) + "..."):(Convert.ToString(Eval("shortTitle")).ToString().Trim()!="" ? ( Convert.ToString(Eval("dealPageTitle")).ToString().Trim().Length < 35 ? Eval("dealPageTitle") : Convert.ToString(Eval("dealPageTitle")).Substring(0, 32) + "..."):( Convert.ToString(Eval("title")).ToString().Trim().Length < 35 ? Eval("title") : Convert.ToString(Eval("title")).Substring(0, 32) + "...")) %>'
                                                    ToolTip='<%# Convert.ToString(Eval("shortTitle")).ToString().Trim()!="" ? Eval("shortTitle"):(Convert.ToString(Eval("dealPageTitle")).ToString().Trim()!="" ? Eval("dealPageTitle"):Eval("title")) %>'></asp:Label></div>
                                            <div style="clear: both; padding-top: 10px; padding-bottom: 10px;">
                                                <asp:Label ID="Label1" ForeColor="White" runat="server" Text='<%# Convert.ToDateTime(Eval("dealStartTime")).ToString("MMM dd, yyyy") %>'></asp:Label></div>
                                        </div>
                                        <div style="clear: both; padding-top: 10px; padding-bottom:10px; overflow:hidden;">
                                            <div>
                                                <div style="float: left; width: 140px;">
                                                    <div style="clear: both;" align="center">
                                                        <div>
                                                            <div style="padding-top: 0px;">
                                                                <asp:Label ID="lblDealsSaleCount" Style="width: 95px !important; font-size: 15px !important;"
                                                                    CssClass="TastyCountDown" runat="server" Text='<%#"<b>Sold:</b> "+ Eval("Orders")%>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both; padding-top: 13px; font-size: 13px;" align="center">
                                                        <asp:Label ID="Label3" Style="width: 95px !important; font-size: 15px !important;"
                                                            CssClass="TastyCountDown" runat="server" Text='<%# "<b>Price:</b> $"+ Eval("sellingPrice") %>'></asp:Label>
                                                    </div>
                                                    <div style="clear: both; padding-top: 13px; font-size: 13px;" align="center">
                                                        <asp:Label ID="Label7" Style="width: 95px !important; font-size: 15px !important;"
                                                            CssClass="TastyCountDown" runat="server" Text='<%# "<b>Savings:</b> $"+ (float.Parse(Eval("valuePrice").ToString())-float.Parse(Eval("sellingPrice").ToString())).ToString() %>'></asp:Label>
                                                    </div>
                                                </div>
                                                <div style="float: left;">
                                                    <asp:Image ID="imgDeal" runat="server" ImageUrl='<%# "~/Images/dealfood/" + DataBinder.Eval (Container.DataItem,    "restaurantId").ToString().Trim() + "/" + DataBinder.Eval (Container.DataItem,"image1").ToString().Trim() %>'
                                                        Width="195px" Height="140px" /></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--</a>--%>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: right; padding-right: 80px; font-size: 13px; color: Black; text-decoration: none;">
                            <table cellpadding="0" border="0" style="text-decoration: none; color: White;">
                                <tr>
                                    <td align="right">
                                        <asp:LinkButton ID="lbtnFirst" Style="text-decoration: none; font-size: 13px; color: Black;"
                                            runat="server" CausesValidation="false" OnClick="lbtnFirst_Click">First</asp:LinkButton>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:LinkButton ID="lbtnPrevious" Style="text-decoration: none; font-size: 13px;
                                            color: Black;" runat="server" CausesValidation="false" OnClick="lbtnPrevious_Click">Prev</asp:LinkButton>&nbsp;&nbsp;
                                    </td>
                                    <td align="center" valign="middle">
                                        <asp:DataList ID="dlPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="dlPaging_ItemCommand"
                                            OnItemDataBound="dlPaging_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnPaging" Style="text-decoration: none; font-size: 13px;
                                                    color: Black;" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="Paging"
                                                    Text='<%# Eval("PageText") %>'></asp:LinkButton>&nbsp;
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                    <td align="left">
                                        &nbsp;&nbsp;<asp:LinkButton ID="lbtnNext" runat="server" Style="text-decoration: none;
                                            font-size: 13px; color: Black;" CausesValidation="false" OnClick="lbtnNext_Click">Next</asp:LinkButton>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:LinkButton ID="lbtnLast" runat="server" Style="text-decoration: none; font-size: 13px;
                                            color: Black;" CausesValidation="false" OnClick="lbtnLast_Click">Last</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center" style="height: 30px" valign="middle">
                                        <asp:Label ID="lblPageInfo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
