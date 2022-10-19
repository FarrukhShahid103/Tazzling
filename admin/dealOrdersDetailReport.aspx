<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealOrdersDetailReport.aspx.cs" Inherits="dealOrdersDetailReport" Title="TastyGo | Admin | Deal Orders Details Report" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
    function clearFields() {
        document.getElementById('ctl00_ContentPlaceHolder1_txtUserAcc').value = '';
        document.getElementById('ctl00_ContentPlaceHolder1_txtSearchCity').value = '';
        return false;
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdatePanel ID="upGrid" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                    <div id="search">
                        <div class="heading">
                            <asp:Label ID="lblFirstNameSearch" runat="server" Text="User Account"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtUserAcc" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblLastNameSearch" runat="server" Text="Payment Status"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlPayment" runat="server" Width="112px">
                                <asp:ListItem Value="all" Text="All" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Successful" Text="Successful"></asp:ListItem>
                                <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                                <asp:ListItem Value="Declined" Text="Declined"></asp:ListItem>
                                <asp:ListItem Value="Cancelled" Text="Cancelled"></asp:ListItem>
                                <asp:ListItem Text="Refunded" Value="Refunded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="2" />
                            &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" OnClientClick='javascript:window.close();' ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                 TabIndex="3" />
                        </div>
                        <div style="padding-bottom: 5px;">
                            <asp:ImageButton ID="imgbtnExportToExcel" runat="server" ImageUrl="~/admin/Images/export_excel.png"
                                TabIndex="3" OnClick="imgbtnExportToExcel_Click" /></div>
                    </div>
                    <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                        <div style="float: left; padding-right: 5px">
                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                        </div>
                        <div class="floatLeft">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                        </div>
                    </div>
                    <div id="gv">
                        <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                                </asp:TextBox>
                                <asp:GridView ID="pageGrid" runat="server" DataKeyNames="dOrderID" Width="100%" AutoGenerateColumns="False"
                                    AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                                    AllowSorting="True" OnSorting="pageGrid_Sorting">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div style="display: none">
                                                    <asp:Label ID="lblDealID" runat="server" Text='<% # Eval("dealId") %>' Visible="true"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblYuserName" ForeColor="White" runat="server" Text="User Account"
                                                    CommandName="Sort" CommandArgument="userName"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblusserName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "userName").ToString().Length > 21 ? DataBinder.Eval (Container.DataItem, "userName").ToString().Substring(0,19) + "..." :DataBinder.Eval (Container.DataItem, "userName").ToString()) %>'
                                                        ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "userName").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblName" ForeColor="White" runat="server" Text="Full Name" CommandName="Sort"
                                                    CommandArgument="Name"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblussserName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "Name").ToString().Length > 21 ? DataBinder.Eval (Container.DataItem, "Name").ToString().Substring(0,19) + "..." :DataBinder.Eval (Container.DataItem, "Name").ToString()) %>'
                                                        ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "Name").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblQHaedingQtu" ForeColor="White" runat="server" Text="Qty."></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblOrderQty" Text='<%# Eval("Qty")%>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCreditCardAmountUsed" ForeColor="White" runat="server" Text="Credit Card"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblCreditCard" Text='<% # "$"+ Eval("ccCreditUsed") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHeadingComission" ForeColor="White" runat="server" Text="Comission"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblComission" Text='<% # "$" + Eval("comissionMoneyUsed") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHeadingTastyCredit" ForeColor="White" runat="server" Text="Credits"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblCredits" Text='<% # "$" + Eval("tastyCreditUsed") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHeadingTotal" ForeColor="White" runat="server" Text="Total"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblToal" Text='<% # "$" + Eval("totalAmt") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHeadingComissionEarned" ForeColor="White" runat="server" Text="Comission Earned"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblComissionEarned" Text='<% # "$" + Eval("comission") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHeadingTastyCreditEarned" ForeColor="White" runat="server" Text="Tasty Credit Earned"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblTastyCreditEarned" Text='<% # "$" + Eval("TastyCredit") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Ordered Date"
                                                    CommandName="Sort" CommandArgument="createdDate"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblcreationDateText" Text='<% # GetDateString(Eval("createdDate")) %>'
                                                        runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblStatusHead" ForeColor="White" runat="server" Text="Payment"
                                                    CommandName="Sort" CommandArgument="status"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "status").ToString().Length > 22 ? DataBinder.Eval (Container.DataItem, "status").ToString().Substring(0,20) + "..." :DataBinder.Eval (Container.DataItem, "status").ToString()) %>'
                                                        ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "status").ToString()) %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div id="emptyRowStyle" align="left">
                                            <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                        </div>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <RowStyle CssClass="gridText" Height="27px" />
                                    <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                                    <PagerTemplate>
                                        <div style="padding-top: 0px;">
                                            <div id="pager">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family: Tahoma;
                                                    font-size: 11px; color: #666666;">
                                                    <tr>
                                                        <td width="30%" align="left" style="padding-left: 2px;">
                                                            <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                                                            <asp:Label ID="lblTotal" Text=" results" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="30%">
                                                            <div class="floatRight">
                                                                <asp:DropDownList ID="ddlPage" runat="server" CssClass="fontStyle" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="floatRight" style="padding-top: 3px; padding-right: 6px;">
                                                                <asp:Label ID="lblRecordsPerPage" runat="server" Text="Record per Page"></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td width="40%" align="right" style="padding-right: 2px;">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="font-family: Tahoma; font-size: 11px;
                                                                color: #666666;">
                                                                <tr>
                                                                    <td style="padding-right: 2px">
                                                                        <div id="divPrevious">
                                                                            <asp:ImageButton ID="btnPrev" Enabled='<%# displayPrevious %>' CommandName="Page"
                                                                                CommandArgument="Prev" runat="server" ImageUrl="~/admin/images/imgPrev.jpg" />
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="floatLeft" style="padding-top: 3px; padding-left: 8px;">
                                                                            <asp:Label ID="lblpage1" runat="server" Text="Page"></asp:Label>
                                                                        </div>
                                                                        <div style="padding-left: 10px; padding-right: 10px; float: left">
                                                                            <asp:TextBox ID="txtPage" CssClass="fontStyle" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"
                                                                                Style="padding-left: 12px;" Width="20px" Text="1" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="floatLeft" style="padding-top: 3px; padding-right: 4px;">
                                                                            <asp:Label ID="lblOf" runat="server" Text="of"></asp:Label>
                                                                            <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td style="padding-left: 4px">
                                                                        <div id="divNext">
                                                                            <asp:ImageButton ID="btnNext" Enabled='<%# displayNext %>' CommandName="Page" CommandArgument="Next"
                                                                                runat="server" ImageUrl="~/admin/images/imgNext.jpg" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                    </PagerTemplate>
                                </asp:GridView>
                                <div class="heading" style="text-align:left;">
                                    <asp:Label ID="lblSummery" runat="server" Text="Summery"></asp:Label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
