<%@ Page Language="C#" MasterPageFile="~/shipper/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="orderTracker.aspx.cs" Inherits="orderTracker" Title="Order Tracking Managment" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
                                <asp:ListItem Value="pending" Text="Pending" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="all" Text="All"></asp:ListItem>
                                <asp:ListItem Value="send" Text="Send"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="button-group" style="overflow: hidden; height: auto; margin-left: 20px;
                            float: left;">
                            <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/shipper/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />
                        </div>
                    </div>
                </div>
                <div style="padding-top: 15px; padding-left: 4px; clear: both;">
                    <div style="float: left; padding: 0px 0px 10px 0px;">
                        <b>Note:</b> Changes will only take effect after you refresh the page
                    </div>
                </div>
                <div style="padding-top: 5px; padding-left: 4px; clear: both;">
                    <div style="float: left; padding: 0px 0px 10px 0px;">
                        <asp:Label ID="lblDealTitle" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div style="padding-top: 5px; padding-left: 4px; clear: both;">
                    <div class="floatLeft" style="padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/shipper/images/error.png" />
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
                                GridLines="Horizontal" HeaderStyle-BorderColor="#737373" AllowSorting="true"
                                OnSorting="pageGrid_Sorting" OnRowEditing="pageGrid_RowEditing">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="15px" HeaderText="Sr." ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'> </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="15px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="25%" HeaderText="Order No." ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                        <asp:HyperLink ID="lblIOrderNumber" runat="server" Target="_blank"
                                                Text='<%# Eval("orderNo")%>'
                                                NavigateUrl='<%# ConfigurationManager.AppSettings["YourSite"].ToString()+"/shipper/OrderInvoice.aspx?OID=" + Eval("orderNo") %>' />                                                                                            
                                            <%--<asp:Label ID="lblOrderDate" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "orderNo") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Customer" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Name").ToString()==""?Eval("Name"):Eval("shippingName") %>'></asp:Label>
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
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Telephone" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblTelephone" runat="server" Text='<%# Eval("Telephone") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" HeaderText="Note" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div>
                                                <asp:HyperLink ID="hlShippingNote" runat="server" Width="110px" Text='<%# Convert.ToString(Eval("shippingNote"))==""?"_": Eval("shippingNote")%>'
                                                    Target="_blank" NavigateUrl='<%# "shippingnote.aspx?sid="+ Eval("shippingInfoId") %>'></asp:HyperLink>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="Address" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div>
                                                <asp:HyperLink ID="hlAddress" runat="server" Width="140px" Text='<%# Eval("ShippingAddress") %>'
                                                    Target="_blank" NavigateUrl='<%# "shippingaddress.aspx?sid="+ Eval("shippingInfoId") %>'></asp:HyperLink>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" HeaderText="Track #" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div>
                                                <asp:TextBox ID="txtTrackNumber" runat="server" Width="100px" Style="border: 1px solid #666666;"
                                                    MaxLength="20" Text='<%# Eval("trackingNumber") %>'></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvTrackingNumber" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtTrackNumber" ValidationGroup='<% # "detailID"+Eval("detailID") %>'
                                                    ErrorMessage="Value required!" Display="None">
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="vceFoodCredit1" runat="server" TargetControlID="rfvTrackingNumber">
                                                </cc2:ValidatorCalloutExtender>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkbtnEdit" CommandName="Edit" runat="server" CausesValidation="true"
                                                ValidationGroup='<% # "detailID"+Eval("detailID") %>' ImageUrl="~/shipper/Images/successfulorders.png" />
                                        </ItemTemplate>
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
                                                                            CommandArgument="Prev" runat="server" ImageUrl="~/shipper/images/imgPrev.jpg" />
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
                                                                            runat="server" ImageUrl="~/shipper/images/imgNext.jpg" />
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
</asp:Content>
