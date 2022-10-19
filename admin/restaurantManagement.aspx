<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="restaurantManagement.aspx.cs" Inherits="restaurantManagement"
    Title="TastyGo | Admin | Business Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="JS/jquery-1.4.min.js"></script>

    <script type="text/javascript" language="javascript">
                       
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchBusinessName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchCity').value = '';            
            return false;
        }

        function checkAll() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            if (document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked) {
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

        function ChangeHeaderAsNeeded() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (!document.getElementById(list[i]).checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = false;
                    return;
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = true;
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
       
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblFirstNameSearch" runat="server" Text="Business Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchBusinessName" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="lblLastNameSearch" runat="server" Text="Deal Address"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchCity" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="lblUsernameSearch" runat="server" Text="Business Email"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchZipCode" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                            OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                TabIndex="2" />
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
                <div class="searchButtons">
                    <div class="floatLeft">
                        <asp:ImageButton ID="btnShowAll" runat="server" ImageUrl="~/admin/images/btnShowAll.gif"
                            OnClick="btnShowAll_Click" />&nbsp;
                    </div>
                    <%--  <div class="floatLeft">
                        <asp:ImageButton ID="btnDeleteSelected" runat="server" ImageUrl="~/admin/images/btnDeleteSelected.jpg"
                            OnClientClick="return preCheckSelected();" OnClick="btnDeleteSelected_Click" />
                    </div>--%>
                    <div class="floatLeft">
                        &nbsp;<asp:HyperLink Target="_blank" ID="btnAddNew" runat="server" ImageUrl="~/admin/images/btnAddNew.jpg" NavigateUrl="~/admin/addEditBusinessManagement.aspx?addNew=true" />
                    </div>
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="restaurantId" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowEditing="pageGrid_RowEditing"
                                OnRowDeleting="pageGrid_RowDeleting" OnRowCommand="pageGrid_Login" 
                                AllowSorting="False">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("restaurantId") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblResttaurantID" runat="server" Text='<% # Eval("restaurantId") %>'
                                                    Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Business Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblBusniessName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 19 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,16) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                    ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblPostalZipCode" ForeColor="White" runat="server" Text="Address"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblPostalZip" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantAddress").ToString().Length > 150 ? DataBinder.Eval (Container.DataItem, "restaurantAddress").ToString().Substring(0,149) + "..." :DataBinder.Eval (Container.DataItem, "restaurantAddress").ToString()) %>'
                                                    ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantAddress").ToString()) %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUserHeadTypzxeName" ForeColor="White" runat="server" Text="Phone"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserTypeNameisFaxPhone" Text='<% # Eval("phone") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Created Date"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblcreationDateText" Text='<% # GetDateString(Eval("createdDate")) %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" HeaderText="Deal Actions" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="imgBtnAddNewDeal" runat="server" Width="41" ToolTip="Add New Campaign"
                                                CausesValidation="false" ImageUrl="~/admin/Images/restaddTofavIcon.jpg" Target="_blank" NavigateUrl='<%# "~/admin/addEditCampaignManagement.aspx?Mode=New&resID=" + Eval("restaurantId") %>' />
                                            <asp:HyperLink ID="imgBtnViewAllDeal" runat="server" Width="32" ToolTip="Manage Campaign"
                                                CausesValidation="false" ImageUrl="~/admin/Images/ViewAll.png" Target="_blank" NavigateUrl='<%# "~/admin/campaignManagement.aspx?Mode=All&resID=" + Eval("restaurantId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Login As" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibLogin" OnClientClick='return confirm("Are you sure to login as this user?");'
                                                CommandArgument='<% # Eval("userID") %>' Enabled='<%#(Eval("isActive")==DBNull.Value ? false : Convert.ToBoolean(Eval("isActive")) ?  true : false) %> '
                                                runat="server" ToolTip='<%#(Convert.ToBoolean(Eval("isActive")) ? "Login as this user." : "User is not active so you cannot login as this user.") %> '
                                                CommandName="Login" ImageUrl='<%#(Convert.ToBoolean(Eval("isActive")) ? "~/admin/Images/lock_go.png" : "~/admin/Images/lock_go-deavtive.png") %> ' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("isActive") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("userId") %>' Visible="false"></asp:Label>
                                            <asp:ImageButton CommandName="Edit" ID="btnEdit" OnClientClick="return confirm('Are you sure you want to change the status of the business?')"
                                                runat="server" ImageUrl='<%#(Eval("isActive")==DBNull.Value ? "" : Convert.ToBoolean(Eval("isActive")) ? "~/admin/images/active.png" : "~/admin/images/deactive.png") %> '
                                                ToolTip='<%#(Eval("isActive")==DBNull.Value ? "" : Convert.ToBoolean(Eval("isActive")) ? "Active" : "In Active") %> ' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:HyperLink ID="imgBtnEditResturants" runat="server" ToolTip="Edit Business"
                                                CausesValidation="false" ImageUrl="~/admin/Images/edit.gif" Target="_blank" NavigateUrl='<%# "~/admin/addEditBusinessManagement.aspx?edit=" + Eval("restaurantId") %>' />
                                                
                                            <%--<asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl="~/admin/Images/edit.gif" />--%>
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
</asp:Content>
