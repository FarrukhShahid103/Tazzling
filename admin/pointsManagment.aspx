<%@ Page Title="TastyGo | Admin | Points Managment" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="pointsManagment.aspx.cs" Inherits="pointsManagment" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchPoints').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchUsername').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_ddlSearchGivenType').selectedIndex = 0;
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

        function ImageValidation(oSrc, args) {
            var strFUImage = document.getElementById("ctl00_ContentPlaceHolder1_fuGiftcardImage").value;
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
                        <asp:Label ID="lblCousineNameSearch" runat="server" Text="Points"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchPoints" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                     <div class="heading">
                        <asp:Label ID="Label5" runat="server" Text="Username"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchUsername" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                     <div class="heading">
                        <asp:Label ID="Label6" runat="server" Text="Gets From"></asp:Label>
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlSearchGivenType" runat="server">
                        <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Order" Value="Order"></asp:ListItem>
                            <asp:ListItem Text="Comment" Value="Comment"></asp:ListItem>
                            <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                        </asp:DropDownList>
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
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server"> </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="pID" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting"
                                OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" AllowSorting="True" OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("pID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("pID") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCousineHeadpID" ForeColor="White" runat="server" Text="Username"
                                                CommandName="Sort" CommandArgument="userName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblpIDText" Text='<% # Eval("userName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCardHeadPoints" ForeColor="White" runat="server" Text="Points"
                                                CommandName="Sort" CommandArgument="points"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblCardPoints" Text='<% #Eval("points")%>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                     <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCardHeadpointsGetsFrom" ForeColor="White" runat="server" Text="Gets From"
                                                CommandName="Sort" CommandArgument="pointsGetsFrom"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblpointsGetsFromText" Text='<% #Eval("pointsGetsFrom")%>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCardHeadDescription" ForeColor="White" runat="server" Text="Description"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblDescriptionText" Text='<% # Eval("Description") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>          
                                    
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Creation Date"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblcreationDateText" Text='<% # GetDateString(Eval("creationDate")) %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                      
                                                                    
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="Delete" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/delete.gif"
                                                OnClientClick='return confirm("Are you sure you want to delete this item?");'
                                                ToolTip="Delete Gift Card" />
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
</asp:Content>
