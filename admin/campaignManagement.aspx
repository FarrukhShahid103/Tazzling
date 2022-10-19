<%@ Page Title="Campaign Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="campaignManagement.aspx.cs" Inherits="campaignManagement"
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
                            <asp:Label ID="lblLastNameSearch" runat="server" Width="63px" Text="Title"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSrchCampaignTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblUsernameSearch" runat="server" Text="Archive"></asp:Label>
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
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="campaignID" Width="100%"
                                AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False" OnRowDataBound="gvViewDeals_RowDataBound"
                                ShowHeader="True" ShowFooter="true" GridLines="None" OnRowDeleting="gvViewDeals_RowDeleting"
                                OnSelectedIndexChanged="gvViewDeals_SelectedIndexChanged">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("campaignID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Campaign Title">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                            <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "campaignTitle").ToString().Length > 52 ? DataBinder.Eval (Container.DataItem, "campaignTitle").ToString().Substring(0,51) + "..." :DataBinder.Eval (Container.DataItem, "campaignTitle").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "campaignTitle").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCampaignStartDate" CssClass="fontStyle" runat="server" Text='<%# Eval("campaignStartTime")!=DBNull.Value? Convert.ToDateTime(Eval("campaignStartTime")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" CssClass="fontStyle" runat="server" Text='<%# Eval("campaignEndTime")!=DBNull.Value? Convert.ToDateTime(Eval("campaignEndTime")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ship Canada">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShipCanada" CssClass="fontStyle" runat="server" Text='<%# Convert.ToBoolean(Eval("shipCanada"))?"Yes":"No"%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ship USA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShipUSA" CssClass="fontStyle" runat="server" Text='<%# Convert.ToBoolean(Eval("shipUSA"))?"Yes":"No"%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" CssClass="fontStyle" runat="server" Text='<%# Eval("creationDate")!=DBNull.Value? Convert.ToDateTime(Eval("creationDate")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <div style="clear: both;">
                                                <div style="float: left; padding-left:20px;">
                                                    <asp:HyperLink ImageUrl="~/admin/Images/addProduct.png" ID="hlAddNewProduct" runat="server"
                                                        ToolTip="Add Product" NavigateUrl='<%# "addEditProductManagement.aspx?Mode=new&resID="+Eval("restaurantId")+"&cid="+ Eval("campaignID") %>' Target="_blank"></asp:HyperLink>
                                                </div>
                                                <div style="float: left; padding-left: 5px; padding-right: 5px;">
                                                    <asp:HyperLink ImageUrl="~/admin/Images/manageProduct.png" ID="hlManageProducts"
                                                        runat="server" ToolTip="Manage Product" NavigateUrl='<%# "productManagement.aspx?cid="+ Eval("campaignID") %>' Target="_blank"></asp:HyperLink>
                                                </div>
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="hlEditDeals" runat="server" ImageUrl="~/admin/Images/edit.gif"
                                                        PostBackUrl='<%# "addEditCampaignManagement.aspx?Mode=edit&resID="+Eval("restaurantId")+"&did="+ Eval("campaignID") %>' />
                                                </div>
                                            </div>
                                            <%--    <asp:ImageButton ID="btnDeleteItems" runat="server" CommandName="Delete" CausesValidation="False"
                                                ImageUrl="~/admin/Images/delete.gif" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                ToolTip="Delete Deal" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcampaignID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "campaignID") %>'>
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
