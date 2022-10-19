<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="affiliatePartners.aspx.cs" Inherits="admin_affiliatePartners" Title="TastyGo | Admin | Affiliate Partner" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
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
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlProvinceLive').selectedIndex == 0) {
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

        function checkForUsers(value) {            
            if (value == 2 || value == "Select One") {                
                document.getElementById('tblHide').style.display = "none";
                document.getElementById('tblRestuarant').style.display = "none";
//                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvRefID'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResBusinessName'), false);                
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvProvince'), false);
//                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvHowYouKnowUs'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResName'), false);                                                         
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResAddress'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCity'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvFax'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCousineType'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResDetail'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvPhone'), false);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvZipCode'), false);                
            }
            else if (value == 3 || value == 4 || value == 5) {           
                if (value == 3) {
                    document.getElementById('tblRestuarant').style.display = "block";
                    document.getElementById('tblHide').style.display = "block";
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResName'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResBusinessName'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResAddress'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCity'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvFax'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCousineType'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResDetail'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvPhone'), true);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvZipCode'), true);
                }
                else {
                    document.getElementById('tblRestuarant').style.display = "none";
                    document.getElementById('tblHide').style.display = "block";
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResName'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResBusinessName'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResAddress'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCity'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvFax'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvCousineType'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvResDetail'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvPhone'), false);
                    ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvZipCode'), false);
                }               
//                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_rfvRefID'), true);
                //ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvCountry'), true);
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_cvProvince'), true);
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
                        <asp:Label ID="lblUsernameSearch" runat="server" Text="Username"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchUserName" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="lblFirstNameSearch" runat="server" Text="Affiliate Request Archive"></asp:Label>
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlSearchStatus" runat="server" Width="126px">
                            <asp:ListItem Value="new" Text="New Requests"></asp:ListItem>
                            <asp:ListItem Value="approved" Text="Approved"></asp:ListItem>
                            <asp:ListItem Value="declined" Text="Declined"></asp:ListItem>
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
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>                
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="userID" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                                OnRowDataBound="pageGrid_RowDataBound" OnRowEditing="pageGrid_RowEditing" OnRowDeleting="pageGrid_RowDeleting"                                
                                AllowSorting="True" OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
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
                                            <asp:LinkButton ID="lblUserHeadFName" ForeColor="White" runat="server" Text="First Name"
                                                CommandName="Sort" CommandArgument="firstName"></asp:LinkButton>
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
                                            <asp:LinkButton ID="lblUserHeadLName" ForeColor="White" runat="server" Text="Last Name"
                                                CommandName="Soort" CommandArgument="lastName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserLastNameText" Text='<% # Eval("lastName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="26%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblUserHeadUName" ForeColor="White" runat="server" Text="Username"
                                                CommandName="Sort" CommandArgument="userName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserUserNameText" Text='<% # Eval("userName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="26%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblUserHeadTypeName" ForeColor="White" runat="server" Text="User Type"
                                                CommandName="Sort" CommandArgument="userType"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserTypeNameText" Text='<% # Eval("userType") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                     <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblUserHeadTypeName1" ForeColor="White" runat="server" Text="Status"
                                                CommandName="Sort" CommandArgument="ReqStatus"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserTypeNameTexw" Text='<% # Eval("ReqStatus") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" HeaderText="Action" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("isActive") %>' Visible="false"></asp:Label>
                                            <asp:ImageButton ID="imgBtnApprove" CommandName="Edit" Width="41px" ToolTip="Approve" Height="41" runat="server" Visible='<%# (Eval("affiliateReq").ToString().Trim()=="new" || (Eval("affiliateReq").ToString().Trim()=="declined")) ? true : false %>' ImageUrl="~/admin/Images/AppYes.png" />
                                            <asp:ImageButton ID="imgBtnReject" CommandName="Delete" Width="41px" ToolTip="Reject" Height="41px" runat="server" Visible='<%# (Eval("affiliateReq").ToString().Trim()!="approved" && (Eval("affiliateReq").ToString().Trim()!="declined")) ? true : false %>' ImageUrl="~/admin/Images/RejectNo.png" />                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No records founds" runat="server"></asp:Label>
                                    </div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" Height="36px" />
                                <AlternatingRowStyle CssClass="AltgridText" Height="36px" />
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