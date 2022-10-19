<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="userManagement.aspx.cs" Inherits="admin_userManagement"
    Title="TastyGo | Admin | User Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
    
     function ImageValidation(oSrc, args) {

            if (args.Value != "") {
                var strFUImage = args.Value;
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
            }
            else {
                args.IsValid = false;
                return;
            }
        }
    
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchFirstName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchLastName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchUserName').value = '';
            return false;
        }

        function countrychanged(obj, objLive, objCh) {
            //alert(obj);
            var ctryID = obj;

            //First it will null all the options of the ProvinceLive drop down list
            for (loop = document.getElementById(objLive).options.length - 1; loop > -1; loop--) {
                document.getElementById(objLive).options[loop] = null;
            }

            var count = 0;

            //The it will get the required value from the Hidden drop down list and set it into the Live drop down list
            document.getElementById(objLive).options[count] = new Option("Select One");
            count++;
            for (loop = document.getElementById(objCh).options.length - 1; loop > -1; loop--) {
                var ProConID = document.getElementById(objCh).options[loop].value;
                //First Part contain the Province ID and Second Part contain the Country ID
                var ProConArray = ProConID.split(",");
                //Validate that Country ID in the country drop down list macthes with the country provinces into the hidden drop down list

                if (ProConArray[1] == ctryID) {

                    var ProvinceName = document.getElementById(objCh).options[loop].Text.Trim();
                    document.getElementById(objLive).options[count] = new Option(ProvinceName);
                    document.getElementById(objLive).options[count].value = ProConArray[0];
                    count++;
                }
            }
        }
        function setProvinceText() {
            var objDll = document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').selectedIndex;
            if (objDll != 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_hfProvince').value = document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').value;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hfProvince').value = '0';
            }
        }
        function setCountrytext() {
            var objDllCon = document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').selectedIndex;
            if (objDllCon != 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_hfCountry').value = document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').value;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hfCountry').value = '0';
                document.getElementById('ctl00_ContentPlaceHolder1_hfProvince').value = '0';
            }
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

        function validateProvince(oSrc, args) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').selectedIndex < 0) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
            return;

        }
        function validateCountry(oSrc, args) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlCountry').selectedIndex == 0) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
            return;
        }

        function validatePhone(oSrc, args) {
            var phone1 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').value;
            var phone2 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').value;
            var phone3 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').value;
            if (phone1 != "") { 
                if(phone1.length != 3)               
                {
                    args.IsValid = false;
                    return;
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();                
                }
                if (!IsNumeric(phone1)) {
                    args.IsValid = false;
                    return;
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhone1').focus();
                }
            }
            else if (phone2 != "") {
                if(phone2.length != 3)
                {
                    args.IsValid = false;
                    return;
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
                }
                if (!IsNumeric(phone2)) {
                    args.IsValid = false;
                    return;
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhone2').focus();
                }
            }
            else if (phone3 != "") {
                if(phone3.length != 4)
                {
                    args.IsValid = false;
                    return;
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
                }
                if (!IsNumeric(phone3)) {
                    args.IsValid = false;
                    return;
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhone3').focus();
                }
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

        function checkForUsers(value) {            
            if (value == 2 || value == "Select One") {                              
                document.getElementById('tblRestuarant').style.display = "none";
//                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvRefID'), false);
               // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResBusinessName'), false);                             
//                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvHowYouKnowUs'), false);
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResName'), false);                                                         
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResAddress'), false);
               // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCity'), false);
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvFax'), false);
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCousineType'), false);
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResDetail'), false);                
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvZipCode'), false);                
            }
            else if (value == 3 || value == 4 || value == 5) {           
                if (value == 3) {
                    document.getElementById('tblRestuarant').style.display = "block";                  
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResName'), true);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResBusinessName'), false);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResAddress'), true);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCity'), true);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvFax'), true);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCousineType'), true);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResDetail'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvPhone'), true);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvZipCode'), true);
                }
                else {
                    document.getElementById('tblRestuarant').style.display = "none";                   
                   // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResName'), false);
                    //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResBusinessName'), false);
                   // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResAddress'), false);
                   // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCity'), false);
                   // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvFax'), false);
                   // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCousineType'), false);
                   // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResDetail'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvPhone'), true);
                   // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvZipCode'), false);
                }               
//                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvRefID'), true);
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvCountry'), true);              
               // ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvHowYouKnowUs'), true);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblFirstNameSearch" runat="server" Text="First Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchFirstName" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="lblLastNameSearch" runat="server" Text="Last Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchLastName" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="lblUsernameSearch" runat="server" Text="Username"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchUserName" runat="server" CssClass="txtSearch"></asp:TextBox>
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
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="userID" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                                OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting" OnRowEditing="pageGrid_RowEditing"
                                OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" OnRowCommand="pageGrid_Login"
                                AllowSorting="False">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("userID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("userID") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUserHeadFName" ForeColor="White" runat="server" Text="First Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserFirstNameText" Text='<% # Eval("firstName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" SortExpression="lastName"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUserHeadLName" ForeColor="White" runat="server" Text="Last Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserLastNameText" Text='<% # Eval("lastName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUserHeadUName" ForeColor="White" runat="server" Text="Username"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserUserNameText" Text='<% # Eval("userName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUserType" ForeColor="White" runat="server" Text="User Type"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserTypeNameText" Text='<% # Eval("userType") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblIPAddress" ForeColor="White" runat="server" Text="IP"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserIPText" Text='<% # Eval("ipAddress") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Affiliate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAffiliate" runat="server" Text='<%# Eval("isAffiliate") %>' Visible="false"></asp:Label>
                                            <asp:ImageButton ID="ibAffiliate" OnClientClick='return confirm("Are you sure to change affiliate user status?");'
                                                CommandArgument='<% # Eval("userID") %>' runat="server" ToolTip='<%#(Convert.ToBoolean(Eval("isAffiliate")) ? "Affiliate User." : "User is not affiliate. You can affilate this user by simply click on this icon.") %> '
                                                CommandName="Affiliate" ImageUrl='<%#(Convert.ToBoolean(Eval("isAffiliate")) ? "~/admin/Images/affiliate_active.png" : "~/admin/Images/affiliate_disable.png") %> ' />
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
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" Visible='<%# (Convert.ToInt32(Eval("userTypeID"))==3? false:true) %>'
                                                CommandName="Select" ImageUrl="~/admin/Images/edit.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("isActive") %>' Visible="false"></asp:Label>
                                            <asp:ImageButton CommandName="Edit" ID="btnEdit" OnClientClick="return confirm('Are you sure you want to change the status of the user?')"
                                                runat="server" ImageUrl='<%#(Eval("isActive")==DBNull.Value ? "" : Convert.ToBoolean(Eval("isActive")) ? "~/admin/images/active.png" : "~/admin/images/deactive.png") %> '
                                                ToolTip='<%#(Eval("isActive")==DBNull.Value ? "" : Convert.ToBoolean(Eval("isActive")) ? "Active" : "In Active") %> ' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/delete.gif"
                                                OnClientClick='return confirm("Are you sure you want to delete this user?");'
                                                ToolTip="Delete User" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No records founds" runat="server"></asp:Label>
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
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfCountry" runat="server" Value="0" />
            <asp:HiddenField ID="hfProvince" runat="server" Value="0" />
            <asp:Panel ID="pnlForm" runat="server" Visible="false">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="User Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Image ID="imgAddError" Visible="false" runat="server" ImageUrl="~/admin/images/error.png" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:Label ID="lblDError" Visible="False" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblUserType" runat="server" Text="User Type" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="txtForm" onchange="checkForUsers(this.value);">
                                        <asp:ListItem Value="1" Selected="True">Select One</asp:ListItem>
                                        <asp:ListItem Value="2" Text="Admin"></asp:ListItem>
                                        <%-- <asp:ListItem Value="3" Text="Retaurant Owner"></asp:ListItem>--%>
                                        <asp:ListItem Value="4" Text="Member"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Sales"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Customer Service"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Promoter"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Shipper"></asp:ListItem>
                                    </asp:DropDownList>
                                    <cc1:RequiredFieldValidator ID="rfvUserType" InitialValue="1" SetFocusOnError="true"
                                        runat="server" ControlToValidate="ddlUserType" ErrorMessage="User type required!"
                                        ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceUserType" runat="server" TargetControlID="rfvUserType">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvFirstName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtFirstName" ErrorMessage="First name required!" ValidationGroup="user"
                                        Display="None">                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcdFirstName" runat="server" TargetControlID="rfvFirstName">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvLastName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtLastName" ErrorMessage="Last name required!" ValidationGroup="user"
                                        Display="None">
                           
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceLastName" runat="server" TargetControlID="rfvLastName">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblUserName" runat="server" Text="Username" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvUserName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtUsername" ErrorMessage="Username required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceUserName" runat="server" TargetControlID="rfvUserName">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvEmail" SetFocusOnError="true" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Email required!" ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceEmail" runat="server" TargetControlID="rfvEmail">
                                    </cc2:ValidatorCalloutExtender>
                                    <cc1:RegularExpressionValidator ID="reEmail" ValidationGroup="user" ControlToValidate="txtEmail"
                                        ErrorMessage="Please enter valid format." SetFocusOnError="true" Display="None"
                                        runat="server" ValidationExpression="^([\w\-\.]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$"></cc1:RegularExpressionValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="reEmail"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblPassword" runat="server" Text="Password" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvPassword" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtPassword" ErrorMessage="Password required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcePassword" runat="server" TargetControlID="rfvPassword">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblPhone" runat="server" Text="Phone" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:TextBox ID="txtPhone1" runat="server" CssClass="txtForm" Width="60px"></asp:TextBox>-<asp:TextBox
                                        ID="txtPhone2" runat="server" CssClass="txtForm" Width="60px"></asp:TextBox>-<asp:TextBox
                                            ID="txtPhone3" runat="server" CssClass="txtForm" Width="60px"></asp:TextBox>
                                    <cc1:CustomValidator ID="cvPhone" runat="server" ControlToValidate="txtPhone3" ValidateEmptyText="true"
                                        ClientValidationFunction="validatePhone" SetFocusOnError="true" ValidationGroup="user"
                                        ErrorMessage="Phone number required in correct format." Display="None">
                                    </cc1:CustomValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcePhone" runat="server" TargetControlID="cvPhone">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label14" runat="server" Text="Country"></asp:Label>
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlCountry" ToolTip="Select Country." runat="server" CssClass="txtForm"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                        <asp:ListItem Text="Canada" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="United States" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblProvince" runat="server" Text="Province/State" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlProvinceLive" runat="server" CssClass="txtForm" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlProvinceLive_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <cc1:CustomValidator SetFocusOnError="true" ID="cvProvince" ValidationGroup="user"
                                        ValidateEmptyText="true" ClientValidationFunction="validateProvince" ControlToValidate="ddlProvinceLive"
                                        runat="server" Display="None" ErrorMessage="Province/State required!"></cc1:CustomValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceProvince" TargetControlID="cvProvince" runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="City" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlSelectCity" runat="server" CssClass="txtForm">
                                    </asp:DropDownList>
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" InitialValue="Select One"
                                        runat="server" ControlToValidate="ddlSelectCity" ErrorMessage="Select City" ValidationGroup="user"
                                        Display="None">                                                                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator5"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label2" runat="server" Text="Profile Picture" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:UpdatePanel ID="upImage" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:FileUpload CssClass="txtForm" ID="fuUserProfilePic" runat="server" />
                                            <cc1:CustomValidator ID="cvfpChange" runat="server" ClientValidationFunction="ImageValidation"
                                                ControlToValidate="fuUserProfilePic" Display="None" ValidateEmptyText="false"
                                                ValidationGroup="user" ErrorMessage="Please upload correct image file." SetFocusOnError="True"></cc1:CustomValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="cvfpChange">
                                            </cc2:ValidatorCalloutExtender>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSave" />
                                            <asp:PostBackTrigger ControlID="btnUpdate" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:Image ID="imgProfilePic" Visible="false" Style="border: 2px solid #CCCCCC;"
                                        runat="server" Height="80px" Width="80px" />
                                </td>
                            </tr>
                        </table>
                        <table id="tblRestuarant" border="0" cellpadding="3" cellspacing="2" width="920px"
                            class="fontStyle" style='display: <%=strRestHide%>'>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblResName" runat="server" Text="Restaurant Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtResName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <%--   <cc1:RequiredFieldValidator ID="rfvResName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtResName" ErrorMessage="Name required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceResName" runat="server" TargetControlID="rfvResName">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblResBusinessName" runat="server" Text="Restaurant Business Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtResBusinessName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <%-- <cc1:RequiredFieldValidator ID="rfvResBusinessName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtResBusinessName" ErrorMessage="Business name required!"
                                        ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvResBusinessName">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblResAddress" runat="server" Text="Restaurant Address" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtResAddress" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <%--  <cc1:RequiredFieldValidator ID="rfvResAddress" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtResAddress" ErrorMessage="Address required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceResAddress" runat="server" TargetControlID="rfvResAddress">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblCity" runat="server" Text="City" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <%-- <cc1:RequiredFieldValidator ID="rfvCity" SetFocusOnError="true" runat="server" ControlToValidate="txtCity"
                                        ErrorMessage="City required!" ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceCity" runat="server" TargetControlID="rfvCity">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblFax" runat="server" Text="Fax" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:TextBox ID="txtFax" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <%--  <cc1:RequiredFieldValidator ID="rfvFax" SetFocusOnError="true" runat="server" ControlToValidate="txtFax"
                                        ErrorMessage="Fax required!" ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceFax" runat="server" TargetControlID="rfvFax">
                                    </cc2:ValidatorCalloutExtender>
                                    <cc1:RegularExpressionValidator ID="rxFAX" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtFax" ErrorMessage="Only digits are required." ValidationGroup="CreateUserWizard1"
                                        Display="None" ValidationExpression="^\d*$"></cc1:RegularExpressionValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="rxFAX">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblZipCode" runat="server" Text="Postal/Zip Code" />
                                </td>
                                <td align="left" class="colLeft fontStyle">
                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <%--  <cc1:RequiredFieldValidator ID="rfvZipCode" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtZipCode" ErrorMessage="Postal/Zip Code required!" ValidationGroup="user"
                                        Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceZipCode" runat="server" TargetControlID="rfvZipCode">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label5" runat="server" Text="Cuisine Type" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:DropDownList ID="ddlCousineType" runat="server" CssClass="txtForm">
                                    </asp:DropDownList>
                                    <%-- <cc1:RequiredFieldValidator ID="rfvCousineType" InitialValue="Select One" SetFocusOnError="true"
                                        runat="server" ControlToValidate="ddlCousineType" ErrorMessage="Please select value"
                                        ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceCousineType" runat="server" TargetControlID="rfvCousineType">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblResDetail" runat="server" Text="About Restaurant" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtResDetail" runat="server" CssClass="txtForm" Height="90px" TextMode="MultiLine"></asp:TextBox>
                                    <%-- <cc1:RequiredFieldValidator ID="rfvResDetail" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtResDetail" ErrorMessage="Please enter short description."
                                        ValidationGroup="user" Display="None">
                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceResDetail" runat="server" TargetControlID="rfvResDetail">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="width: 783px; border: 1px solid #B7B7B7;" class="fontSpaceHeightRegular">
                                        <div style="height: 34px; border-bottom: solid 1px #B7B7B7; background-color: #F5F5F5;
                                            padding-left: 19px; padding-top: 11px; width: auto;">
                                            <asp:Label ID="label13" runat="server" Font-Names="Arial, Arial, sans-serif"
                                                Text="Comment" Font-Bold="True" Font-Size="19px" ForeColor="#97C717"></asp:Label><asp:HiddenField
                                                    ID="HiddenField1" runat="server" />
                                        </div>
                                        <div style="height: 230px; border-bottom: solid 1px #B7B7B7; background-color: #F5F5F5;
                                            width: auto">
                                            <div style="width: 781px; padding-top: 15px;">
                                                <div style="width: 100px; float: left; padding-left: 41px;">
                                                    <asp:Label ID="label15" runat="server" Font-Names="Arial,sans-serif" Text="Comment"
                                                        Font-Size="15px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>
                                                </div>
                                                <div style="width: 640px; float: right;">
                                                    <asp:TextBox ID="txtComment" Width="575px" Height="103px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="rfvComments" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtComment" ErrorMessage="Comments required!" ValidationGroup="vgComments"
                                                        Display="None">                            
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="vcdComments" runat="server" TargetControlID="rfvComments">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                            <div style="width: 781px;">
                                                <div style="width: 716px; float: left; text-align: right; padding-top: 26px;">
                                                    <div style="float: right; padding-left: 15px;">
                                                        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                                            ValidationGroup="vgComments" CausesValidation="false" OnClick="btnCancel_Click" />
                                                    </div>
                                                    <div style="float: right;">
                                                        <asp:ImageButton ID="btnPost" runat="server" ImageUrl="~/admin/Images/btnSave.jpg"
                                                            ValidationGroup="vgComments" CausesValidation="true" OnClick="btnPost_Click" />
                                                    </div>
                                                    <div style="float: right; padding-right: 20px;">
                                                        <asp:Label ID="lblCommentMessage" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div style="width: 63px; float: right;">
                                                </div>
                                            </div>
                                        </div>
                                        <asp:DataList ID="rptrDiscussion" RepeatColumns="1" RepeatDirection="Vertical" DataKeyField="commentId"
                                            runat="server" CellPadding="0" OnItemDataBound="DataListItemDataBound" 
                                             CellSpacing="0" Width="781px" GridLines="None"
                                            ShowHeader="false">
                                            <ItemTemplate>
                                                <div style="border-bottom: solid 1px #B7B7B7; background-color: #FFFFFF; width: auto;
                                                    padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                                                    <div style="width: 141px; float: left; text-align: center">
                                                        <asp:Label ID="lblcommentId" runat="server" Visible="false" Text='<%# Eval("commentId")%>'></asp:Label>
                                                        <asp:Image ID="imgDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px" BorderStyle="Solid"
                                                            ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                        <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("userId")%>' />
                                                    </div>
                                                    <div style="width: 640px; float: right; text-align: left;">
                                                        <div style="width: 640px; height: 26px;">
                                                            <div style="float: left; width: 550px; padding-right: 10px;">
                                                                <asp:Label ID="label5" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("Name") %>'
                                                                    Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                        ID="label6" runat="server" Font-Names="Arial, Arial, sans-serif" Text='<%# Eval("cmtDatetime")%>'
                                                                        Font-Bold="True" Font-Size="13px" ForeColor="#97C717"></asp:Label>
                                                            </div>                                                         
                                                        </div>
                                                        <div style="width: 628px; padding-right: 12px;">
                                                            <asp:Label ID="label7" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("comment")%>'
                                                                Font-Size="13px" ToolTip='<%# Eval("comment")%>' ForeColor="#7C7B7B"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="user" ImageUrl="~/admin/images/btnSave.jpg"
                                        OnClick="btnSave_Click" />
                                    <asp:ImageButton ID="btnUpdate" ValidationGroup="user" runat="server" ImageUrl="~/admin/images/btnUpdate.jpg"
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
