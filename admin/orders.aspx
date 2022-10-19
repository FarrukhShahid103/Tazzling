<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="orders.aspx.cs" Inherits="orders" Title="Order Invoice Managment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="CSS/gh-buttons.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div style="clear: both;">
                        <div class="heading" style="padding-right: 10px; float: left;">
                            <asp:Label ID="Label1" runat="server" Text="Deal Code"></asp:Label>
                        </div>
                        <div class="BusinessSearch" style="float: left;">
                            <asp:TextBox ID="txtSrchDealCode" runat="server" Width="92px" CssClass="TextBox"></asp:TextBox>
                        </div>
                        <div class="heading" style="padding-right: 10px; float: left; padding-left: 10px;">
                            <asp:Label ID="Label3" runat="server" Text="User Email"></asp:Label>
                        </div>
                        <div class="BusinessSearch" style="float: left;">
                            <asp:TextBox ID="txtUserEmail" runat="server" Width="92px" CssClass="TextBox"></asp:TextBox>
                        </div>
                        <div class="heading" style="padding-right: 10px; float: left; padding-left: 10px;">
                            <asp:Label ID="Label4" runat="server" Text="User Name"></asp:Label>
                        </div>
                        <div class="BusinessSearch" style="float: left;">
                            <asp:TextBox ID="txtUserName" runat="server" Width="92px" CssClass="TextBox"></asp:TextBox>
                        </div>
                        <div class="heading" style="margin-left: 10px; margin-right: 10px; float: left;">
                            <asp:Label ID="lblShowMe" runat="server" Text="Show me"></asp:Label>
                        </div>
                        <div class="BusinessSearch" style="float: left;">
                            <asp:DropDownList ID="ddlShowMe" CssClass="TextBox" Width="180px" runat="server">
                                <asp:ListItem Value="all" Text="All" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="available" Text="Available Deals"></asp:ListItem>
                                <asp:ListItem Value="used" Text="Used Deals"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="heading" style="display: none;">
                            <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label>
                        </div>
                        <div class="BusinessSearch" style="display: none;">
                            <asp:DropDownList ID="ddlDealAddress" CssClass="TextBox" Width="180px" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="button-group" style="overflow: hidden; height: auto; margin-left: 20px;
                            float: left;">
                            <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />
                        </div>
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
                    <asp:HiddenField ID="hfOrderId" runat="server" />
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="detailID" Width="100%" CellPadding="0"
                                CellSpacing="0" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="Horizontal" HeaderStyle-BorderColor="#737373" AllowSorting="false">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="30%" HeaderText="Title" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--  <asp:Label ID="lblOrderDate" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 60 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,57) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                ToolTip='<%# DataBinder.Eval (Container.DataItem, "title") %>'></asp:Label>--%>
                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "title") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Customer" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Deal Code" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%# getDealCode(Eval("dealOrderCode"))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>                                 
                                    <asp:TemplateField ItemStyle-Width="20%" HeaderText="Note" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="hlShippingNote" runat="server" Width="200px" Text='<%# Convert.ToString(Eval("comment"))==""?"_": getComment(Eval("comment"))%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>                                 
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptydatarow" align="left">
                                        <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                    </div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" Height="30px" />
                                <AlternatingRowStyle CssClass="AltgridText" Height="30px" />
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
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="clear: both; text-align: left">
                        <asp:Label ID="lblDealsHeading" runat="server" ForeColor="Black" Text="Deals Detail"
                            Font-Bold="true" Font-Size="14px"></asp:Label>
                    </div>
                    <div style="clear: both; padding-top: 5px; text-align: left;">
                        <asp:Label ID="lblDealsDeatail" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
