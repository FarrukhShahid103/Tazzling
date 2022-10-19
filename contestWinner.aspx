<%@ Page Title="Tastygo | Contest Winner" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="contestWinner.aspx.cs" Inherits="contestWinner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                        Hall of Fame
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 980px; background-color: White; min-height: 450px;">
                    <div style="clear: both;">
                        <asp:DataList runat="server" ID="dlContestWinner" DataKeyField="contestWinner_ID"
                            AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" RepeatColumns="1"
                            RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" GridLines="None"
                            HorizontalAlign="Center" ShowHeader="false" RowStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <div>
                                    <div style="width: 960px; overflow: hidden; border-bottom:solid 1px #e6e6e5;">
                                        <div style="float: left; width: 580px;">
                                            <div style="height: auto; padding-top: 10px; 
                                                padding-right: 10px; line-height: 20px; clear: both; font-size:15px; font-weight:bold; color:#ff7800;" align="left">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("contestWinner_Title") %>'></asp:Label>
                                            </div>
                                            <div style="height: auto; padding-bottom: 5px; padding-left: 0px;
                                                line-height: 20px; clear: both;font-size:13px; font-weight:bold;" align="left">
                                                <asp:Label ID="HyperLink1" runat="server" Text='<%# Eval("contestWinner_Name") %>'></asp:Label>
                                            </div>
                                            <div style="height: auto; line-height: 20px; clear: both; font-size:13px;" align="left">
                                                <div style="float: left;">
                                                    <asp:Label ID="Label2" runat="server"
                                                        Text="Start Date:"></asp:Label>
                                                </div>
                                                <div style="float: left; padding-left: 5px;">
                                                    <asp:Label ID="Label3" runat="server"
                                                        Text='<%# Eval("contestWinner_startTime").ToString().Trim()==""?"": Convert.ToDateTime(Eval("contestWinner_startTime")).ToString("MMM dd, yyyy") %>'></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: right; width: 100px; height: auto;">
                                            <div style="height: auto; padding-top: 10px; padding-bottom: 10px; clear: both;"
                                                align="center">
                                                <asp:Image ID="imgDeal" runat="server" ImageUrl='<%# "~/Images/ContestWinner/" + Eval("contestWinner_Image") %>'
                                                    Width="110px" Height="80px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div style="clear: both; padding-top: 10px;">

                        <div style="float: right; padding-right: 15px; font-size:13px; color:Black; text-decoration:none;">
                            <table cellpadding="0" border="0" style="text-decoration: none;">
                                <tr>
                                    <td align="right">
                                        <asp:LinkButton style="text-decoration:none; font-size:13px; color:Black;" ID="lbtnFirst" runat="server" CausesValidation="false" OnClick="lbtnFirst_Click">First</asp:LinkButton>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:LinkButton ID="lbtnPrevious" style="text-decoration:none; font-size:13px; color:Black;" runat="server" CausesValidation="false" OnClick="lbtnPrevious_Click">Prev</asp:LinkButton>&nbsp;&nbsp;
                                    </td>
                                    <td align="center" valign="middle">
                                        <asp:DataList ID="dlPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="dlPaging_ItemCommand"
                                            OnItemDataBound="dlPaging_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnPaging"  style="text-decoration:none; font-size:13px; color:Black;" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                                                    CommandName="Paging" Text='<%# Eval("PageText") %>'></asp:LinkButton>&nbsp;
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                    <td align="left">
                                        &nbsp;&nbsp;<asp:LinkButton ID="lbtnNext" style="text-decoration:none; font-size:13px; color:Black;" runat="server" CausesValidation="false"
                                            OnClick="lbtnNext_Click">Next</asp:LinkButton>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:LinkButton ID="lbtnLast" runat="server" style="text-decoration:none; font-size:13px; color:Black;" CausesValidation="false" OnClick="lbtnLast_Click">Last</asp:LinkButton>
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
