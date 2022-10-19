<%@ Page Title="Online Deal Management" Language="C#" MasterPageFile="~/shipper/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="onlinedealManagement.aspx.cs" Inherits="onlinedealManagement"
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
                                <asp:ListItem Value="all" Selected="True" Text="All"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/shipper/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/shipper/Images/btnClear.png" OnClientClick="return clearFields();"
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
                <div style="clear: both;" align="center">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="upItems" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="dealId" Width="100%"
                                AllowSorting="False" RowStyle-Height="24px" AllowPaging="False" AutoGenerateColumns="False"
                                OnRowDataBound="gvViewDeals_RowDataBound" OnRowCommand="gvViewDeals_Login" ShowHeader="True"
                                ShowFooter="true" GridLines="None">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                   <%-- <asp:TemplateField HeaderText="Business">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBusinessName" Width="120px" CssClass="fontStyle" runat="server"
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 32 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,30) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Deal Title">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                            <asp:HyperLink Width="380px" ID="lblItemName" runat="server" Target="_blank"
                                                Text='<%# Eval("title") %>'
                                                NavigateUrl='<%# ConfigurationManager.AppSettings["YourSite"].ToString()+"/Vancouver_" + Eval("dealid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" CssClass="fontStyle" runat="server" Text='<%# Eval("dealStartTime")!=DBNull.Value? Convert.ToDateTime(Eval("dealStartTime")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpiryDate" CssClass="fontStyle" runat="server" Text='<%# Eval("dealEndTime")!=DBNull.Value? Convert.ToDateTime(Eval("dealEndTime")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Successful Orders">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlEditDeals" runat="server" Target="_blank" ToolTip="View successful orders"
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "RemainTrackingOrders").ToString()) %>'
                                                NavigateUrl='<%# "orderTracker.aspx?order=success&did="+ Eval("dealid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Refunded Orders">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlcancelorders" runat="server" Target="_blank" ToolTip="View cancel or refunded orders"
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "RefundedOrder").ToString()) %>'
                                                NavigateUrl='<%# "orderTracker.aspx?order=cancel&did="+ Eval("dealid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActive" CssClass="fontStyle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dealStatus") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="30px" HeaderText="Resend Orders">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlResendOrders" runat="server" Target="_blank" ToolTip="Resend orders"
                                                Text='<%#Eval("ResendOrder")%>' ForeColor="Red" NavigateUrl='<%# "ResendOrdersManagement.aspx?did="+ Eval("dealid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="updownloadExcel" runat="server">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="imgbtnExportToExcel" runat="server" CommandArgument='<% #Eval("dealId") %>'
                                                        CommandName="DownloadExcel" CausesValidation="false" ImageUrl="~/admin/Images/download_excel.png"
                                                        ToolTip="Download successfull orders." />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
                                                </Triggers>
                                            </asp:UpdatePanel>
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
