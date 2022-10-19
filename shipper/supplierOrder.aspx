<%@ Page Language="C#" MasterPageFile="~/shipper/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="supplierOrder.aspx.cs" Inherits="supplierOrder" Title="Supplier Order Managment" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="CSS/gh-buttons.css">
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
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="productID" Width="100%"
                                AllowSorting="False" RowStyle-Height="24px" AllowPaging="False" AutoGenerateColumns="False"
                                OnRowDataBound="gvViewDeals_RowDataBound" OnRowEditing="gvViewDeals_RowEditing"
                                ShowHeader="True" ShowFooter="true" GridLines="None">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" CssClass="fontStyle" runat="server" Text='<%# Eval("campaignStartTime")!=DBNull.Value? Convert.ToDateTime(Eval("campaignStartTime")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpiryDate" CssClass="fontStyle" runat="server" Text='<%# Eval("campaignEndTime")!=DBNull.Value? Convert.ToDateTime(Eval("campaignEndTime")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deal Title">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                            <asp:HyperLink Width="250px" ID="lblItemName" runat="server" Target="_blank" Text='<%# Eval("title")%>'
                                                NavigateUrl='<%# ConfigurationManager.AppSettings["YourSite"].ToString()+"/detail.aspx?pid=" + Eval("productID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblColor" CssClass="fontStyle" runat="server" Text='<%# Eval("sizeText")!=DBNull.Value? Eval("sizeText"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Orders">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalSuccessfulOrders" runat="server" Text='<%# Eval("SuccessfulOrder")  %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remain">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemainOrders" runat="server" Text='<%# Convert.ToString(Convert.ToInt32(Eval("SuccessfulOrder")) - Convert.ToInt32(Eval("SupplierOrder")))  %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" HeaderText="Ordered" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div>
                                                <asp:TextBox ID="txtSendingOrdered" runat="server" Width="100px" Style="border: 1px solid #666666;"
                                                    Text=""></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="rfvSendingOrdered" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtSendingOrdered" ValidationGroup='<% # "Order"+Convert.ToString(Eval("sizeID")!=DBNull.Value? Eval("sizeID"): "")+Eval("productID") %>'
                                                    ErrorMessage="Value required!" Display="None">
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="vceFoodCredit1" runat="server" TargetControlID="rfvSendingOrdered">
                                                </cc2:ValidatorCalloutExtender>
                                                <asp:RangeValidator runat="server" ID="rxFoodCredit" ValidationGroup='<% # "Order"+Convert.ToString(Eval("sizeID")!=DBNull.Value? Eval("sizeID"): "")+Eval("productID") %>'
                                                    ControlToValidate="txtSendingOrdered" Type="Integer" ErrorMessage="Value must be numeric."
                                                    MinimumValue="-999999999" MaximumValue="999999999" Display="None"></asp:RangeValidator>
                                                <cc2:ValidatorCalloutExtender ID="vceFoodCredit" runat="server" TargetControlID="rxFoodCredit">
                                                </cc2:ValidatorCalloutExtender>                                                
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkbtnEdit" CommandName="Edit" runat="server" CausesValidation="true"
                                                ValidationGroup='<% # "Order"+Convert.ToString(Eval("sizeID")!=DBNull.Value? Eval("sizeID"): "")+Eval("productID") %>'
                                                ImageUrl="~/shipper/Images/successfulorders.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductID" Visible="false" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "productID") %>'>
                                            </asp:Label>
                                            <asp:Label ID="lblSizeID" Visible="false" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "sizeID") %>'>
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
