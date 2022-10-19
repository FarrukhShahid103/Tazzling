<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="subMenu.aspx.cs" Inherits="admin_subMenu" Title="TastyGo | Admin | Sub Menu" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchCardNumber').value = '';
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

    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblItemNameSearch" runat="server" Text="Sub-Item Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchItemName" runat="server" CssClass="txtSearch"></asp:TextBox>
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
                    <div class="floatLeft">
                        <asp:ImageButton ID="btnDeleteSelected" runat="server" ImageUrl="~/admin/images/btnDeleteSelected.jpg"
                            OnClientClick="return preCheckSelected();" OnClick="btnDeleteSelected_Click" />
                    </div>
                    <div class="floatLeft">
                        &nbsp;<asp:ImageButton ID="btnAddNew" runat="server" ImageUrl="~/admin/images/btnAddNew.jpg"
                            OnClick="btnAddNew_Click" />
                    </div>
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="menuSubItemId" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting"
                                OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" AllowSorting="True" OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("menuSubItemId") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("menuSubItemId") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblsubItemHeadID" ForeColor="White" runat="server" Text="ID"
                                                CommandName="Sort" CommandArgument="cardID"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblsubItemText" Text='<% # Eval("menuSubItemId") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblsubItemName" ForeColor="White" runat="server" Text="Item Name"
                                                CommandName="Sort" CommandArgument="subItemName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblsubItemNameText" Text='<% # Eval("subItemName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblsubItemDescText" Text="Description" runat="server"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblsubItemDescText" Text='<% # Eval("subItemDescription") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblsubItemSubName" ForeColor="White" runat="server" Text="Sub Name"
                                                CommandName="Sort" CommandArgument="subItemSubname"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblsubItemSubNameText" Text='<% # Eval("subItemSubname") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblsubItemPrice" ForeColor="White" runat="server" Text="Price"
                                                CommandName="Sort" CommandArgument="subItemPrice"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblsubItemSubPriceText" Text='<% # Eval("subItemPrice") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl="~/admin/Images/edit.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/delete.gif"
                                                OnClientClick='return confirm("Are you sure you want to delete this item?");'
                                                ToolTip="Delete User" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No data to display" runat="server"></asp:Label>
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
                                                            <asp:Label ID="lblRecordsPerPage" runat="server" Text="Records per page"></asp:Label>
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
                                                                            Style="padding-left: 12px;" Width="20px" runat="server" Text="1"></asp:TextBox>
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
            <div style="clear: both; float: right; padding-top: 12px;">
                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/admin/Images/btnBack.gif"
                    OnClientClick="history.back(-1);" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="false">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Sub Item Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblSubItemName" runat="server" Text="Item Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtItemName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvItemName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtItemName" ErrorMessage="Item name required!" ValidationGroup="subItem"
                                        Display="None">                            
                                    &nbsp;                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceItemName" runat="server" TargetControlID="rfvItemName">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight" valign="top">
                                    <asp:Label ID="lbl" runat="server" Text="Item Decsription" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtItemDesc" runat="server" TextMode="MultiLine" Height="80px" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvItemDesc" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtItemDesc" ErrorMessage="Description required!" ValidationGroup="subItem"
                                        Display="None">                            
                                                        
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcdItemDesc" runat="server" TargetControlID="rfvItemDesc">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblItemSubName" runat="server" Text="Item Subname" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtItemSubName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvItemSubName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtItemSubName" ErrorMessage="Subname required!" ValidationGroup="subItem"
                                        Display="None">                            
                                                          
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceItemSubName" runat="server" TargetControlID="rfvItemSubName">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblItemPrice" runat="server" Text="Item Price" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtItemPrice" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvItemPrice" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtItemPrice" ErrorMessage="Price required!" ValidationGroup="Card"
                                        Display="None">                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvItemPrice">
                                    </cc2:ValidatorCalloutExtender>
                                    <cc1:RangeValidator ID="rngItemPriceRange" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtItemPrice" Type="Currency" MinimumValue="0" MaximumValue="999999999"
                                        ErrorMessage="Numeric value required!" ValidationGroup="subItem" Display="None">                            
                                    </cc1:RangeValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rngItemPriceRange">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="subItem" ImageUrl="~/admin/images/btnSave.jpg"
                                        OnClick="btnSave_Click" />
                                    <asp:ImageButton ID="btnUpdate" runat="server" ValidationGroup="subItem" ImageUrl="~/admin/images/btnUpdate.jpg"
                                        OnClick="btnUpdate_Click" Visible="False" />&nbsp;
                                    <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                        OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
