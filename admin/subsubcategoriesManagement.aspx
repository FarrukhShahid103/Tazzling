<%@ Page Title="TastyGo | Admin | Sub Sub Category Managment" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="subsubcategoriesManagement.aspx.cs" Inherits="subsubcategoriesManagement" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchcategoryNumber').value = '';
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

        function ImageValidation(oSrc, args) {
            var strFUImage = document.getElementById("ctl00_ContentPlaceHolder1_fuGiftcategoryImage").value;
            var strFUImageArray = strFUImage.split(".");
            var strFUImageExt = strFUImageArray[1];
            if (strFUImageExt == "jpg" || strFUImageExt == "JPG" || strFUImageExt == "gif" || strFUImageExt == "GIF" || strFUImageExt == "JPEG" ||
            strFUImageExt == "jpeg" || strFUImageExt == "png" || strFUImageExt == "PNG" || strFUImageExt == "ico" || strFUImageExt == "ICO"
            || strFUImageExt == "tif" || strFUImageExt == "TIF" || strFUImageExt == "tiff" || strFUImageExt == "TIFF" || strFUImageExt == "bmp"
            || strFUImageExt == "BMP") {
                args.IsValid = true;
                return;
            }
            else {
                args.IsValid = false;
                return;
            }

            //alert(strFUImageExt);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblCousineNameSearch" runat="server" Text="Category Amount"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchcategoryNumber" runat="server" CssClass="txtSearch"></asp:TextBox>
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
                        <asp:ImageButton ID="ImageButton2" runat="server" 
                             ImageUrl="~/admin/images/btnBack.gif" onclick="ImageButton2_Click" />&nbsp;
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
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server"> </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="categoryId" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting"
                                OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" AllowSorting="True" OnRowEditing="pageGrid_RowEditing"
                                OnSorting="pageGrid_Sorting" OnRowUpdating="pageGrid_Updating">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("categoryId") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("categoryId") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCategoryNammeHead" ForeColor="White" runat="server" Text="Category Name"
                                                CommandName="Sort" CommandArgument="categoryName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblcategoryNameText" Text='<% # Eval("categoryName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblParentCategoryNammeHead" ForeColor="White" runat="server" Text="Parent Category"></asp:Label>
                                                
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblParentcategoryNameText" Text='<% # Eval("categoryParentname") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblcategoryDescriptionHead" ForeColor="White" runat="server"
                                                Text="Category Description" CommandName="Sort" CommandArgument="categoryDescription"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblcategoryDescriptionText" Text='<% # Eval("categoryDescription") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblcategoryStatusHead" ForeColor="White" runat="server" Text="Status"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>' Visible="false"></asp:Label>
                                                <asp:ImageButton CommandName="Edit" ID="btnEdit" OnClientClick="return confirm('Are you sure you want to change the status of this category.?')"
                                                    runat="server" ImageUrl='<%#(Eval("status")==DBNull.Value ? "" : Convert.ToBoolean(Eval("status")) ? "~/admin/images/active.png" : "~/admin/images/deactive.png") %> '
                                                    ToolTip='<%#(Eval("status")==DBNull.Value ? "" : Convert.ToBoolean(Eval("status")) ? "Active" : "In Active") %> ' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="Edit" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl="~/admin/Images/edit.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="Delete" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/delete.gif"
                                                OnClientClick='return confirm("Are you sure you want to delete this item?");'
                                                ToolTip="Delete Giftcategory" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfCountry" runat="server" Value="0" />
            <asp:HiddenField ID="hfProvince" runat="server" Value="0" />
            <asp:Panel ID="pnlForm" runat="server" Visible="false">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Categories Manager" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblUserType" runat="server" Text="Category Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvcategoryName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtCategoryName" ErrorMessage="Category name required!" ValidationGroup="category"
                                        Display="None">                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcdCousineName" runat="server" TargetControlID="rfvcategoryName">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="Parent Category" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlCategories" CssClass="txtForm" runat="server" AutoPostBack="true" 
                                        onselectedindexchanged="ddlCategories_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trSubCategory" runat="server" visible="false">
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label4" runat="server" Text="Sub Category" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddSubCategory" CssClass="txtForm" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label2" runat="server" Text="Active" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:CheckBox ID="cbStatus" runat="server" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight" valign="top">
                                    <asp:Label ID="Label3" runat="server" Text="Description" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" Height="100px" runat="server"
                                        CssClass="txtForm"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="category" ImageUrl="~/admin/images/btnSave.jpg"
                                        OnClick="btnSave_Click" />
                                    <asp:ImageButton ID="btnUpdate" runat="server" ValidationGroup="category" ImageUrl="~/admin/images/btnUpdate.jpg"
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
