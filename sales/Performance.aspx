<%@ Page Language="C#" MasterPageFile="~/sales/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="Performance.aspx.cs" Inherits="sales_Performance" Title="Sales Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../admin/JS/CalendarControl.js" type="text/javascript"></script>

    <link href="../admin/CSS/CalendarControl.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
 function OpenCalendar(ctrl)
 {
var Cal = window.document.getElementById(ctrl);
showCalendarControl(Cal);
 }
    </script>

    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search3">
                    <div style="width: 100%;">
                        <div class="heading">
                            <asp:Label ID="Label3" runat="server" Text="Select Provice" Width="92px"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSearchProvince" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchProvince_SelectedIndexChanged"
                                runat="server" Width="192px">
                            </asp:DropDownList>
                        </div>
                        <div class="heading">
                            <asp:Label ID="Label4" runat="server" Text="Select City"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSearchCity" runat="server" Width="192px">
                            </asp:DropDownList>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblUsernameSearch" runat="server" Text="Deal Name"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtDealName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                    </div>
                    <div style="width: 100%; padding-top: 15px;">
                        <div class="heading">
                            <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtBusinessName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="Label5" runat="server" Text="Start Date"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtStartDate" onclick="OpenCalendar('ctl00_ContentPlaceHolder1_txtStartDate');"
                                runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="Label6" runat="server" Text="End Date"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtEndDate" onclick="OpenCalendar('ctl00_ContentPlaceHolder1_txtEndDate');"
                                runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading" style="clear: both; margin-top: 10px;">
                            <asp:Label ID="Label2" runat="server" Text="Sales Person"></asp:Label>
                        </div>
                        <div style="margin-top: 10px; margin-left: 14px;">
                            <asp:TextBox ID="txtSalesPersonAccountName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div style="margin-top: 10px; margin-left: 80px;">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/sales/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="2" />&nbsp;
                                <asp:ImageButton ID="btnClear" OnClick="btnClear_Click" runat="server" ImageUrl="~/sales/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="3" />
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
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:GridView ID="pageGrid" runat="server" DataKeyNames="restaurantId" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" AllowSorting="true"  OnSorting="pageGrid_Sorting" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None">
                        <Columns>
                            
                            
                            <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:LinkButton style="margin-left:10px; text-align:left" ID="lblDealNameHead" ForeColor="White" runat="server" Text="Deal Title"
                                        CommandName="Sort" CommandArgument="title"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label style="padding-left:10px; text-align:left" ID="lblDealName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 21 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,19) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                            ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField  ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:LinkButton  style="text-align:left" ID="lblrestaurantBusinessName" ForeColor="White" runat="server" Text="Business Name"
                                        CommandName="Sort" CommandArgument="restaurantBusinessName"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label style="text-align:left; float:left;" ID="lblBusniessName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 19 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,16) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                            ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                         
                         
                         
                          <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:LinkButton style="text-align:left" ID="lblSalesPersonAccountNameHead" ForeColor="White" runat="server" Text="Sale's Person Acc.Name"
                                        CommandName="Sort" CommandArgument="salePersonAccountName"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label style="text-align:left" ID="lblstatus" runat="server" Text='<%# Eval ("salePersonAccountName") %>'
                                            ToolTip='<%# Eval ("salePersonAccountName") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            
                            
                            <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:LinkButton style="text-align:left" ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Deal End Date"
                                        CommandName="Sort" CommandArgument="dealEndTime"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label style="text-align:left" ID="lblcreationDateText" Text='<% # GetDateString(Eval("dealEndTime")) %>'
                                            runat="server"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:LinkButton style="text-align:left" ID="lblDealSoldHead" ForeColor="White" runat="server" Text="Deals Sold"
                                        CommandName="Sort" CommandArgument="SuccessfulOrder"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label style="text-align:left" ID="lblsucceededorders" runat="server" Text='<%# Eval ("SuccessfulOrder") %>'
                                            ToolTip='<%# Eval ("SuccessfulOrder") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                              <asp:TemplateField Visible="false" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:LinkButton style="text-align:left" ID="lblsellingPriceHead" ForeColor="White" runat="server" Text="Price"
                                        CommandName="Sort" CommandArgument="sellingPrice"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label style="text-align:left" ID="lblsellingPrice" runat="server" Text='<%# Eval ("sellingPrice") %>'
                                            ToolTip='<%# Eval ("sellingPrice") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                             <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:Label style="text-align:left" ID="lblRevenueHead" ForeColor="White" runat="server" Text="Revenue Generated"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label style="text-align:left" ID="lblRevenue" runat="server" Text='<%# "$" + (Convert.ToInt64(Eval("sellingprice")) * Convert.ToInt64(Eval("SuccessfulOrder"))) %>'
                                            ToolTip='<%# "$" + (Convert.ToInt64(Eval("sellingprice")) * Convert.ToInt64(Eval("SuccessfulOrder"))) %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            
                            
                               <asp:TemplateField Visible="false" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:Label style="text-align:left" ID="lblRevenueHead" ForeColor="White" runat="server" Text="Commission History"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                    <asp:HyperLink ID="LinkComissionHistory" Target="_blank" ToolTip="View Commission History" runat="server" ImageUrl="Images/history.png" NavigateUrl='<% # "~/sales/CommissionHistories.aspx?DID=" + Eval("dealid")  %>'></asp:HyperLink>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
                                                                <asp:TextBox ID="txtPage" CssClass="fontStyle" AutoPostBack="true" Style="padding-left: 12px;" Width="20px" Text="1" runat="server"></asp:TextBox>
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
