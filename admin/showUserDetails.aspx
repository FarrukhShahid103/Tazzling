<%@ Page Language="C#" MasterPageFile="adminTastyGo.master" AutoEventWireup="true"
    CodeFile="showUserDetails.aspx.cs" Inherits="admin_showUserDetails" Title="Show User Details" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<usrCtrl:subMenu ID="subMenu1" runat="server" />--%>
    <div style="width: auto; padding-bottom: 10px;" align="center">
        <div>
            
          
            <div style="height: auto; overflow: hidden;">
                <div id="search">
                    <div style="float: left; width: 170px; text-align: center; vertical-align: middle;
                        height: auto; padding-top: 5px">
                        <div>
                            <%= "<b>" + "View Transcation by" + "</b> &nbsp;&nbsp;" %>
                        </div>
                    </div>
                    <div style="margin-top: -5px;">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="memebreferral" style="padding-left: 10px; padding-top: 5px; padding-bottom: 5px;">
                                    <%= "Year" + ":&nbsp;"%>
                                </td>
                                <td style="padding-left: 10px; padding-top: 5px; padding-bottom: 5px;">
                                    <asp:DropDownList runat="server" CssClass="TextBoxDeal" Width="200px" ID="ddlYear" />
                                    <%--<asp:RequiredFieldValidator ID="rfvYear" InitialValue="Select Year" SetFocusOnError="true"
                                        runat="server" ControlToValidate="ddlYear" ErrorMessage="Please select a year!"
                                        ValidationGroup="select" Display="None">                            
                                    </asp:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceYear" runat="server" TargetControlID="rfvYear">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                                <td class="memebreferral" style="padding-top: 3px; padding-left: 10px;">
                                    <%= "Month" + ":&nbsp;&nbsp;"%>
                                </td>
                                <td style="padding-top: 5px; padding-left: 10px; padding-bottom: 5px;">
                                    <asp:DropDownList runat="server" CssClass="TextBoxDeal" Width="200px" ID="ddlMonth">
                                        <asp:ListItem Text="Select Month" Value="Select Month" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Select Month"
                                        SetFocusOnError="true" runat="server" ControlToValidate="ddlMonth" ErrorMessage="Please select a month!"
                                        ValidationGroup="select" Display="None">                            
                                    </asp:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                                <td class="memebreferral" style="padding-top: 5px; padding-left: 10px; padding-bottom: 5px;
                                    font-size: 18px; height: auto; overflow: hidden;">
                                    <asp:ImageButton OnClick="BtnSearch_Click" ID="BtnSearch" runat="server" ValidationGroup="select" ImageUrl="Images/btnSearch.png" />
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            
              <div style="color: #ed9702; font-size: 13px; font-weight: bold; padding-left: 20px;display: none;">
                <asp:Label ID="lblAffComTotal" Text="Total Commission Earned" runat="server"></asp:Label>
            </div>
            <div style="padding: 5px; text-align: center;">
                <h4>
                    <asp:Label ID="lblHeaderMessage" Visible="false" runat="server" /></h4>
            </div>
            <div style="color: #ed9702; font-size: 13px; font-weight: bold; padding-left: 20px;
                    float: left; padding-right: 10px; margin-bottom:25px;">
                    <div style="word-spacing: 3px;" align="left">
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial,sans-serif" Text="Credit Earning History"
                            Font-Size="20px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblAffComBal" runat="server" Font-Names="Arial,sans-serif"
                            Text="Affiliate Commission Balance" Font-Size="15px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
                    </div>
                </div>
                
            <div style="padding-top: 10px;">
            </div>
            <div>
                <div class="gridborder" style="width: 100%; padding-bottom: 10px; clear:both;">
                    <%--<table cellpadding="0" cellspacing="0" border="0" width="100%" class="GridHeader">
                        <tr>
                            <td width="20%" style="text-align: center;">
                                Earned Date &amp; Time
                            </td>
                            <td width="25%" style="text-align: center;">
                                Earned From
                            </td>
                            <td width="15%" style="text-align: center;">
                                Earned Amount
                            </td>
                            <td width="24%" style="text-align: left; padding-left: 25px;">
                                Gross Revenue
                            </td>
                            <td width="25%" style="text-align: left;">
                                Notes
                            </td>
                        </tr>
                    </table>--%>
                    <asp:GridView runat="server" ID="gridview1" AutoGenerateColumns="False" CellPadding="5"
                        GridLines="None" Font-Size="Small" HorizontalAlign="Center" Width="100%" PageSize="15"
                        RowStyle-HorizontalAlign="Center" EnableSortingAndPagingCallbacks="True" ShowFooter="True">
                        <Columns>
                            <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td width="20%" style="text-align: center;">
                                                <asp:Label CssClass="textalign" Width="160px" runat="server" ID="lblDate" Text='<% # GetDateString(Eval("createdDate")) %>' />
                                            </td>
                                            <td width="25%" style="text-align: center;">
                                                <asp:Label runat="server" ID="lblOrderType" ForeColor="#C15821" Font-Bold="true"
                                                    Text='<%# Eval("Name") %>' />
                                                <asp:Label runat="server" ID="Label1" ForeColor="#C15821" Font-Bold="true" Text='<%#" ("+ Eval("days")+ " days)" %>'
                                                    ToolTip="Tracker Lifetime" />
                                            </td>
                                            <td width="15%" style="text-align: center;">
                                                <asp:Label runat="server" ID="lblOrderFrom" Text='<%#"$"+ Eval("gainedAmount")+ " CAD (" + Eval("affCommPer") + "%)" %>' />
                                            </td>
                                            <td width="15%" style="text-align: center;">
                                                <asp:Label runat="server" ID="lblShippingMethod" Text='<%# "$"+ Eval("totalAmt").ToString() + " CAD" %>' />
                                            </td>
                                            <td width="25%" style="text-align: center;">
                                                <asp:Label runat="server" ID="lblStatus" ForeColor="#C15821" Font-Bold="true" Text='<%# Eval("gainedtype")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="cutLineOrange">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Earned Date &amp; Time">
                                <HeaderTemplate>
                                    <asp:Label ID="earnedDate" ForeColor="White" runat="server" Text="Earned Date &amp; Time"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label CssClass="textalign" Width="160px" runat="server" ID="createdDate" Text='<% # GetDateString(Eval("createdDate")) %>' />
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Earned From">
                                <HeaderTemplate>
                                    <asp:Label ID="earnedFrom" ForeColor="White" runat="server" Text="Earned From"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="width: 30%">
                                        <asp:Label runat="server" Style="float: left; text-align: left;" Width="160px" ID="lblOrderType"
                                            ForeColor="#C15821" Font-Bold="true" Text='<%# Eval("Name") %>' />
                                        <asp:Label CssClass="textalign" Style="float: left;" runat="server" ID="lblDate"
                                            Text='<%#" ("+ Eval("days")+ " days)" %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Earned From">
                                <HeaderTemplate>
                                    <asp:Label ID="earnedAmount" ForeColor="White" runat="server" Text="Earned Amount"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label CssClass="textalign" Style="text-align: left;" Width="160px" runat="server"
                                            ID="lblDate" Text='<%#"$"+ Eval("gainedAmount")+ " CAD (" + Eval("affCommPer") + "%)" %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gross Revenue">
                                <HeaderTemplate>
                                    <asp:Label ID="grossRevenue" ForeColor="White" runat="server" Text="Gross Revenue"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label CssClass="textalign" Style="text-align: left;" Width="160px" runat="server"
                                            ID="lblDate" Text='<%# "$"+ Eval("totalAmt").ToString() + " CAD" %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes">
                                <HeaderTemplate>
                                    <asp:Label ID="notes" ForeColor="White" runat="server" Text="Notes"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label CssClass="textalign" Style="text-align: left;" Width="160px" runat="server"
                                            ID="lblDate" Text='<%# Eval("gainedtype")%>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div id="emptyRowStyle" align="left">
                                <asp:Label ID="emptyText" Text=" There are no affliate partner credits." runat="server"></asp:Label>
                            </div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridText" Height="27px" />
                        <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                        <%--<PagerTemplate>
                            <div class="pagerstyle">
                                <div>
                                    <div>
                                        <asp:LinkButton ID="lbPrev" runat="server" CssClass="pageraction" CausesValidation="False"
                                            CommandArgument="Prev" CommandName="Page" />
                                        <asp:Label runat="server" ID="lblNumeric" />
                                        <asp:LinkButton ID="lbNext" runat="server" CssClass="pageraction" CausesValidation="False"
                                            CommandArgument="Next" CommandName="Page" />
                                    </div>
                                </div>
                            </div>
                        </PagerTemplate>
                        <PagerSettings PageButtonCount="10" Position="TopAndBottom" />
                        <PagerStyle HorizontalAlign="Center" />--%>
                    </asp:GridView>
                    <div style="padding-left: 10px;">
                        <div class="ordersfooter" style="color: Black;">
                            <div style="text-align: right; float: left; width: 700px;">
                                <asp:Label ID="lblTotalB" Font-Names="Arial,sans-serif" runat="server"
                                    Text="This Period Earning" Font-Size="15px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
                            </div>
                            <div class="totalbalancebg" style="width: 100px; float: right; padding-top: 0px;
                                height: 100%;">
                                <b>
                                    <asp:Label runat="server" ID="lblPeriodSales" Font-Names="Arial,sans-serif"
                                        Font-Size="15px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label></b></div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="height: 5px;">
            </div>
            <div align="center" style="padding: 5px 20px 10px 0px;">
                <%--<div class="transanctionbottomalert">
                    <div style="width: 46px; height: 42px; float: left; padding-top: 5px;">
                        <asp:Image ID="imgqmark" runat="server" ImageUrl="~/Images/loginimages/imgforgotPW.png" />
                    </div>
                    <div style="padding-top: 15px; padding-left: 20px;">
                        If you have any questions regarding the affiliate area, please contact us at <a style="color: #f99d1c;
                            font-weight: bold; text-decoration: none;">1855-295-1771</a> or email at <a style="color: #f99d1c;
                                font-weight: bold;">support@tazzling.com</a></div>
                </div>--%>
            </div>
        </div>
    </div>
    <div>
        <div style="word-spacing: 3px; padding-left: 20px" align="left">
            <asp:Label ID="Label2" runat="server" Font-Names="Arial,sans-serif" Text="Order History"
                Font-Size="20px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
        </div>
        <asp:UpdatePanel ID="upGrid" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                    <%-- <div id="Div1">
                        <div class="heading">
                            <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblFirstNameSearch" runat="server" Text="Deal Name"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSearchDealName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblLastNameSearch" runat="server" Text="Order Status"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSrchOrderStatus" Width="126px" runat="server">
                                <asp:ListItem Text="All" Selected="True" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Successful" Value="Successful"></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                <asp:ListItem Text="Declined" Value="Declined"></asp:ListItem>
                                <asp:ListItem Text="Refunded" Value="Refunded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="2" />
                        </div>
                    </div>--%>
                    <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                        <div style="float: left; padding-right: 5px">
                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                        </div>
                        <div class="floatLeft">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                        </div>
                    </div>
                    <div id="gv">
                        <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                        </asp:TextBox>
                        <asp:GridView ID="pageGrid" runat="server" DataKeyNames="dOrderID" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                            AllowSorting="True" OnRowEditing="pageGrid_RowEditing" OnRowCommand="pageGrid_RowCommand"
                            OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" OnSorting="pageGrid_Sorting">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div style="display: none">
                                            <asp:Label ID="lblDealID" runat="server" Text='<% # Eval("dealId") %>' Visible="true"></asp:Label>
                                            <asp:HiddenField ID="hfOrderTotal" runat="server" Value='<% # Eval("totalAmt") %>'>
                                            </asp:HiddenField>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Psigate">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <a id="hlNewDetail" target="_blank" href='<%# "OrderDetails.aspx?OID="+ Eval("orderNo") %>'
                                            tooltip="View Deal Order Detail">
                                            <asp:Label ID="lblpsigateTransaction" runat="server" Text='<%#Convert.ToString(Eval("psgTranNo")).Trim()==""?"View Detail":Eval("psgTranNo") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblnsername" ForeColor="White" runat="server" Text="Username"
                                            CommandName="Sort" CommandArgument="username"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lblsUsername" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "username").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "username").ToString()) %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="11%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblDealNameHead" ForeColor="White" runat="server" Text="Deal"
                                            CommandName="Sort" CommandArgument="DealName"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lblDealName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "DealName").ToString().Length > 21 ? DataBinder.Eval (Container.DataItem, "DealName").ToString().Substring(0,19) + "..." :DataBinder.Eval (Container.DataItem, "DealName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "DealName").ToString()) %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="12%" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblrestaurantBusinessName" ForeColor="White" runat="server" Text="Business Name"
                                        CommandName="Sort" CommandArgument="restaurantBusinessName"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lblBusniessName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 19 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,16) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                            ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>--%>
                                <asp:TemplateField ItemStyle-Width="7%" Visible="false" ItemStyle-HorizontalAlign="Left"
                                    SortExpression="cityName" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblUserHeadLuserNazxcme" ForeColor="White" runat="server" Text="City"
                                            CommandName="Sort" CommandArgument="cityName"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lbluserNameText" Text='<% # Eval("cityName") %>' runat="server"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" SortExpression="Qty"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblQtyHead" ForeColor="White" runat="server" Text="Qty" CommandName="Sort"
                                            CommandArgument="Qty"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lblQty" Text='<% # Eval("Qty") %>' runat="server"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Created Date"
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
                                        <asp:LinkButton ID="lblStatusHead" ForeColor="White" runat="server" Text="Status"
                                            CommandName="Sort" CommandArgument="status"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "status").ToString().Length > 22 ? DataBinder.Eval (Container.DataItem, "status").ToString().Substring(0,20) + "..." :DataBinder.Eval (Container.DataItem, "status").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "status").ToString()) %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="btnViewDetail" runat="server" ImageUrl="~/admin/Images/detail.gif"
                                            Target="_blank" NavigateUrl='<%# "~/admin/OrderDetails.aspx?ID="+ Eval("dOrderID") %>'
                                            ToolTip="View Deal Order Detail" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manage" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" CommandName="Edit" runat="server" Width="32" ImageUrl="~/admin/Images/ViewAll.png"
                                            ToolTip="Manage Deal Order" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card Info" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="ViewCreditCardInfo"
                                            CommandArgument='<%#Eval("ccInfoID")%>' CausesValidation="false" ToolTip="View Credit Card Info"
                                            ImageUrl="~/admin/Images/credit_card.jpg" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make Payment" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnimgMakePayment" runat="server" CommandName="MakePayment"
                                            Visible='<%# getDetailStatus(Eval("status"),Eval("ccCreditUsed"))%>' Width="28"
                                            CommandArgument='<%#Eval("dOrderID")%>' CausesValidation="false" ToolTip="Process Pesigate Payment for this Order"
                                            OnClientClick="return confirm('Are you sure you want to process payment?');"
                                            ImageUrl="~/admin/Images/MakePayment.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay Back" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnPayBack" runat="server" CommandName="PayBack" Visible='<%# getPayBackStatus(Eval("status"),Eval("psgTranNo"),Eval("ccCreditUsed"),Eval("tastyCreditUsed"),Eval("comissionMoneyUsed"))== true ? (Convert.ToInt32(Eval("RedeemCount"))>0) ? false :true :false %>'
                                            CommandArgument='<%#Eval("dOrderID")%>' Width="28" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to roll back payment?');"
                                            ToolTip="Roll back Payment for this Order" ImageUrl="~/admin/Images/RollBack.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfUserId" runat="server" Value='<%# Eval("userId") %>' />
                                        <asp:HiddenField ID="hfAffComm" runat="server" Value='<% # Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "affComm").ToString()) %>' />
                                        <asp:DropDownList ID="ddlStatus" Font-Size="9" runat="server">
                                            <asp:ListItem Text="Successful" Value="Successful"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                            <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                            <asp:ListItem Text="Declined" Value="Declined"></asp:ListItem>
                                            <asp:ListItem Text="Refunded" Value="Refunded"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="btnLinkUpdate" OnClientClick="return confirm('Are you sure you want to change the status?');"
                                            CommandName="Select" ToolTip="Change Order Status" CausesValidation="false" runat="server"
                                            Width="28" ImageUrl="~/admin/Images/process.gif" />
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
                    </div>
                    <div style="word-spacing: 3px; padding-left: 20px; margin-top: 50px;" align="left">
                        <asp:Label ID="Label3" runat="server" Font-Names="Arial,sans-serif" Text="User Notes"
                            Font-Size="20px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
                    </div>
                    <div id="DivUserNotes" style="float: left; margin-top: 10px;">
                        <asp:TextBox Style="width: 915px" Height="150px" runat="server" ID="txtUserNotes" TextMode="MultiLine"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFVUserNotes" runat="server" ControlToValidate="txtUserNotes"
                          Display="None" ErrorMessage="Please Enter Notes for this user" SetFocusOnError="True" ValidationGroup="UserNotesGroup"></asp:RequiredFieldValidator>
                          <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RFVUserNotes">
                          </cc1:ValidatorCalloutExtender>
                    </div>
                    <div style="padding-top:10px; clear:both; float:left;">
                    <asp:ImageButton ID="BtnSaveUserNotes" runat="server"  ValidationGroup="UserNotesGroup" ToolTip="Save Notes for this User"
                            ImageUrl="~/admin/Images/btnSave.jpg" OnClick="BtnSaveUserNotes_Click"></asp:ImageButton>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
