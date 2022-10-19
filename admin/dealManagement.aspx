<%@ Page Title="Deal Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="dealManagement.aspx.cs" Inherits="admin_dealManagement"
    ValidateRequest="false" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
    
        
      

        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSrchDealTitle').value = '';
            return false;
        }
                                               
        function preCheckSelected() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (document.getElementById(list[i]).checked) {
                    return (confirm("Are you sure you want to delete the selected record(s)?"));
                }
            }
            alert("Please select the record(s) to delete!");
            return false;
        }

        function checkAll() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            if (document.getElementById("ctl00_ContentPlaceHolder1_gvViewDeals_ctl01_HeaderLevelCheckBox").checked) {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = true;
                }
            }
            else {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = false;
                }
            }
        }
                                                                       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="udpnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div id="divSrchFields" runat="server">
                    <div id="search">
                        <div class="heading">
                            <asp:Label ID="lblSearchBusinessName" runat="server" Text="Business Name" Width="92px"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSrchBusinessName" runat="server" Width="192px">
                            </asp:DropDownList>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblLastNameSearch" runat="server" Width="63px" Text="Deal Title"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSrchDealTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblUsernameSearch" runat="server" Text="Deals Archive"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSearchStatus" runat="server">
                                <asp:ListItem Value="started" Text="Started Deals"></asp:ListItem>
                                <asp:ListItem Value="upcoming" Text="Upcoming Deals"></asp:ListItem>
                                <asp:ListItem Value="expired" Text="Expired Deals"></asp:ListItem>
                                <asp:ListItem Value="all" Text="All" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="2" />
                        </div>
                    </div>
                </div>
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div class="searchButtons" style="display: none;">
                    <div class="floatLeft">
                        <asp:ImageButton ID="btnDeleteSelected" runat="server" ImageUrl="~/admin/images/btnDeleteSelected.jpg"
                            OnClientClick="return preCheckSelected();" OnClick="btnDeleteSelected_Click" />
                    </div>
                    <div class="floatLeft">
                        &nbsp;<asp:ImageButton ID="btnAddNew" runat="server" ImageUrl="~/admin/images/btnAddNew.jpg"
                            OnClick="btnAddNew_Click" />
                    </div>
                </div>
                <div style="clear: both;" align="center">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="upItems" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="dealId" Width="100%"
                                AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False" OnRowDataBound="gvViewDeals_RowDataBound"
                                ShowHeader="True" ShowFooter="true" GridLines="None" OnRowDeleting="gvViewDeals_RowDeleting"
                                OnSelectedIndexChanged="gvViewDeals_SelectedIndexChanged" OnRowCommand="gvViewDeals_Login">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("dealID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0">
                                                <td style="vertical-align: top" align="left">
                                                    &nbsp;&nbsp;<img src='<%# getImagePath(DataBinder.Eval(Container.DataItem,"restaurantId"),DataBinder.Eval(Container.DataItem,"images")) %>'
                                                        class="menuImageBorder" alt='' width="41px" />
                                                </td>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--  <asp:TemplateField HeaderText="Business Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 25 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,25) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Deal Title">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                            <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 32 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,31) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealCity" CssClass="fontStyle" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "cityName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Selling Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellingPrice" CssClass="fontStyle" runat="server" Text='<%# "$ "+  Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "sellingPrice").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValuePrice" CssClass="fontStyle" runat="server" Text='<%# "$ "+  Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "valuePrice").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" CssClass="fontStyle" runat="server" Text='<%# Eval("createdDate")!=DBNull.Value? Convert.ToDateTime(Eval("createdDate")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealStartTime").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealEndTime").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--   <asp:TemplateField HeaderText="Deal Page">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealSlot" CssClass="fontStyle" runat="server" Text='<%# "Slot " + DataBinder.Eval (Container.DataItem, "dealSlot") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Successful Orders">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuccessfulOrders" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "SuccessfulOrder").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancelled Orders">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCancelledOrder" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "CancelledOrder").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Un-Used Vouchers">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnUsedDeals" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "UsedCount").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActive" CssClass="fontStyle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dealStatus") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="updownloadExcel" runat="server">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="imgbtnExportToExcel" runat="server" Visible='<%# Convert.ToInt32(Convert.ToInt32(Eval("SuccessfulOrder"))+Convert.ToInt32(Eval("SuccessfulOrder"))+Convert.ToInt32(Eval("SuccessfulOrder")))>0  ? true : false %>'
                                                        CommandArgument='<% #Eval("dealId")+","+Eval("voucherExpiryDate")+","+Eval("SuccessfulOrder") %>'
                                                        CommandName="DownloadExcel" CausesValidation="false" ImageUrl="~/admin/Images/download_excel.png"
                                                        ToolTip="Download successfull orders." />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discussion" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("dealStatus1") %>' Visible="false"></asp:Label>
                                            <asp:HyperLink ID="hlDiscuession" runat="server" ImageUrl="~/admin/Images/detail.gif"
                                                Target="_blank" NavigateUrl='<%# "dealdiscuession.aspx?did="+ Eval("dealid") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reminder" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnSendReminderEmail" runat="server" Visible='<%#(Eval("UsedCount")==DBNull.Value ? false : Convert.ToInt32(Eval("UsedCount"))>0 ?  true : false) %> '
                                                CommandArgument='<% #Eval("dealId") %>' CommandName="SendReminder" CausesValidation="false"
                                                ImageUrl="~/admin/Images/email_go.png" ToolTip="Send Reminder email." OnClientClick="return confirm('Are you sure you want to send reminder email?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnPaymentProcess" runat="server" Visible='<%# getDetailStatus(Eval("PendingOrder"))%>'
                                                CommandArgument='<% #Eval("dealId") %>' CommandName="Payment" CausesValidation="false"
                                                ImageUrl="~/admin/Images/MakePayment.gif" Width="26" ToolTip="Process Payment"
                                                OnClientClick="return confirm('Are you sure you want to process payments?');" />
                                            <asp:ImageButton ID="hlEditDeals" runat="server" ImageUrl="~/admin/Images/edit.gif"
                                                PostBackUrl='<%# "addEditdealManagement.aspx?Mode=edit&resID="+Eval("restaurantId")+"&did="+ Eval("dealid") %>' />
                                            <%--    <asp:ImageButton ID="btnDeleteItems" runat="server" CommandName="Delete" CausesValidation="False"
                                                ImageUrl="~/admin/Images/delete.gif" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                ToolTip="Delete Deal" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "dealId") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
