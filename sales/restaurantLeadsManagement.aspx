<%@ Page Language="C#" MasterPageFile="~/sales/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="restaurantLeadsManagement.aspx.cs" Inherits="restaurantLeadsManagement"
    Title="TastyGo | Sales | Restaurant Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchResturantName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchUserName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_ddlFaxPhone').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddSearchProvince').selectedIndex = 0;
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

        function validatePhone(oSrc, args) {

            var phone1 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').value;
            var phone2 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').value;
            var phone3 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').value;
            if (phone1 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();
            }
            else if (phone2 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
            }
            else if (phone3 == "") {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
            }
            if (phone1.length != 3) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();
            }
            else if (phone2.length != 3) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
            }
            else if (phone3.length != 4) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
            }
            if (!IsNumeric(phone1)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();
            }
            else if (!IsNumeric(phone2)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
            }
            else if (!IsNumeric(phone3)) {
                args.IsValid = false;
                return;
                document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
            }

            args.IsValid = true;
            return;
        }

        function IsNumeric(strString) {
            var strValidChars = "0123456789.";
            var strChar;
            var blnResult = true;

            if (strString.length == 0) return false;

            //  test strString consists of valid characters listed above
            for (i = 0; i < strString.length && blnResult == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    blnResult = false;
                }
            }
            return blnResult;
        }

        function changePrinterValidator() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_rbIsPhoneFax_1').checked) {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvPrinterID'), true);
            }
            else {
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvPrinterID'), false);
            }

        }
            
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search2">
                    <div style="width: 100%;">
                        <div class="heading">
                            <asp:Label ID="lblFirstNameSearch" runat="server" Text="Restaurant Name"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSearchResturantName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblLastNameSearch" runat="server" Text="Owner Name"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSearchUserName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblUsernameSearch" runat="server" Text="Status"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList runat="server" ID="ddlFaxPhone">
                                <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Lead Generated">Lead Generated</asp:ListItem>
                                <asp:ListItem Value="Telemarket Followup Required">Telemarket Followup Required</asp:ListItem>
                                <asp:ListItem Value="Appointment Made">Appointment Made</asp:ListItem>
                                <asp:ListItem Value="Follow Up Required">Follow Up Required</asp:ListItem>
                                <asp:ListItem Value="Second Followup Required">Second Followup Required</asp:ListItem>
                                <asp:ListItem Value="Owner Rejected(Reason)">Owner Rejected(Reason)</asp:ListItem>
                                <asp:ListItem Value="Successfully Signuped">Successfully Signuped</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div style="width: 100%; padding-top: 15px;">
                        <div class="heading">
                            <asp:Label ID="Label1" runat="server" Text="Province"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList runat="server" ID="ddSearchProvince" AutoPostBack="true" OnSelectedIndexChanged="ddSearchProvince_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="heading" id="divCityLabel" runat="server" visible="false">
                            <asp:Label ID="Label2" runat="server" Text="City"></asp:Label>
                        </div>
                        <div id="divCityDD" runat="server" visible="false">
                            <asp:DropDownList runat="server" ID="ddSearchCity">
                            </asp:DropDownList>
                        </div>
                        <div>
                        <asp:Button ID="btnPriority" runat="server" 
                                Text="Show Priority Only" TabIndex="1" onclick="btnPriority_Click" />&nbsp;
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/sales/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="2" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/sales/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="3" />
                        </div>
                    </div>
                </div>
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/sales/images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div class="searchButtons">
                    <div class="floatLeft">
                        <asp:ImageButton ID="btnShowAll" runat="server" ImageUrl="~/sales/images/btnShowAll.gif"
                            OnClick="btnShowAll_Click" />&nbsp; <asp:ImageButton ID="btnRefresh" 
                            ToolTip="Click ToolTip Refresh Page" runat="server" 
                            ImageUrl="~/sales/images/office-icon-refresh.png" onclick="btnRefresh_Click"
                            />
                    </div>
                    <div class="floatRight">
                    <asp:HyperLink ID="btnAddNew" runat="server" ImageUrl="~/sales/Images/btnAddNew.jpg" NavigateUrl="~/sales/addeditrestaurant.aspx"
                                                Target="_blank" />
                                                
                       
                    </div>
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="restaurantLeadID" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting"
                                AllowSorting="True" OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("restaurantId") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("restaurantLeadID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("createdBy") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="priority" runat="server" Text='<% # Eval("priority") %>' Visible="false"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server"
                                                Text="Restaurant Name" CommandName="Sort" CommandArgument="restaurantLeadName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserFirstNameText" Text='<% # Eval("restaurantLeadName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblUserHeadTypeName" ForeColor="White" runat="server" Text="Owner Name"
                                                CommandName="Sort" CommandArgument="restaurantLeadOwnerName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserTypeNameisFaxPhone" Text='<% # Eval("restaurantLeadOwnerName") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStatusHead" ForeColor="White" runat="server" Text="Status"
                                                CommandName="Sort" CommandArgument="restaurantLeadStatus"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblStatusTypeText" Text='<% # Eval("restaurantLeadStatus") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeadPhoneNumber" ForeColor="White" runat="server" Text="Contact Number"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblPhoneNumberText" Text='<% #Eval("restaurantLeadPhoneNumber") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" SortExpression="lastName"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblUserHeadLuserName" ForeColor="White" runat="server" Text="Creaded By"
                                                CommandName="Sort" CommandArgument="userName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lbluserNameText" Text='<% # Eval("userName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Assign To"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblAssignToDateText" Text='<% # Eval("restaurantAssignto") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit/View" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlCC" runat="server" ImageUrl="~/sales/Images/edit.gif" NavigateUrl='<% # "~/sales/addeditrestaurant.aspx?rid="+Eval("restaurantLeadID") %>'
                                                Target="_blank" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/sales/Images/delete.gif"
                                                OnClientClick='return confirm("Are you sure you want to delete this restaurant? Its owner will also deleted.");'
                                                ToolTip="Delete User" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
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
                                                                            CommandArgument="Prev" runat="server" ImageUrl="~/sales/images/imgPrev.jpg" />
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
                                                                            runat="server" ImageUrl="~/sales/images/imgNext.jpg" />
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
                <div style="clear:both; float:left; padding-top:10px; padding-bottom:20px;">
                <asp:Label ID="lblNewMessage" runat="server" Font-Names="Arial" Font-Size="19px" Text="High-light leads are first priority"></asp:Label>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
