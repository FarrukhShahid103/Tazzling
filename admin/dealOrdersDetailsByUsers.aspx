<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealOrdersDetailsByUsers.aspx.cs" Inherits="admin_dealOrdersDetailsByUsers"
    Title="TastyGo | Admin | Deal Orders Details" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
    function clearFields() {
        document.getElementById('ctl00_ContentPlaceHolder1_txtUserAcc').value = '';        
        document.getElementById('ctl00_ContentPlaceHolder1_txtVoucher').value = '';
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
                            <asp:Label ID="Label1" runat="server" Text="Voucher #"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtVoucher" runat="server" CssClass="txtSearch"></asp:TextBox>
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
                                    TabIndex="2" />&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                        OnClientClick="javascript:window.close()" TabIndex="3" />
                        </div>
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
                                    AllowSorting="True" OnSorting="pageGrid_Sorting" OnRowCommand="pageGrid_Login">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div style="display: none">
                                                    <asp:Label ID="lblDealID" runat="server" Text='<% # Eval("dealId") %>' Visible="true"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
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
                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblQUserType" ForeColor="White" runat="server" Text="Deal Code"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblUserType" Text='<%# getDealCode(Eval("dealOrderCode"),Eval("status"))%>'
                                                        runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
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
                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" SortExpression="psgTranNo"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblQUsserType" ForeColor="White" runat="server" Text="Psigate #"
                                                    CommandName="Sort" CommandArgument="UserType"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblUsesdrType" Text='<% # Eval("psgTranNo") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" SortExpression="phoneNo"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblQphoneNo" ForeColor="White" runat="server" Text="Phone #"
                                                    CommandName="Sort" CommandArgument="phoneNo"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblphoneNo" Text='<% # Eval("phoneNo") %>' runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
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
                                        <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
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
                                        <asp:TemplateField HeaderText="Login As" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibLogin" OnClientClick='return confirm("Are you sure to login as this user?");'
                                                    CommandArgument='<% # Eval("userID") %>' Enabled='<%#(Eval("isActive")==DBNull.Value ? false : Convert.ToBoolean(Eval("isActive")) ?  true : false) %> '
                                                    runat="server" ToolTip='<%#(Convert.ToBoolean(Eval("isActive")) ? "Login as this user." : "User is not active so you cannot login as this user.") %> '
                                                    CommandName="Login" ImageUrl='<%#(Convert.ToBoolean(Eval("isActive")) ? "~/admin/Images/lock_go.png" : "~/admin/Images/lock_go-deavtive.png") %> ' />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
