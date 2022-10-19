<%@ Page Title="TastyGo | Admin | Provinces Tax Rates" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="provincesTaxRates.aspx.cs" Inherits="provincesTaxRates" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">

        function checkRateValue(oSrc, Args) {
            //alert("IN");

            var ValueEntered = document.getElementById('ctl00_ContentPlaceHolder1_txtTaxRate').value;

            var money = 0;
            var message = document.getElementById('cMessage');

            if (ValueEntered != "" && !isNaN(ValueEntered)) {
                money = parseFloat(ValueEntered);
            }
            else {
                message.innerHTML = "Please enter a valid value.";
                Args.IsValid = false;
                return;
            }
            if (money > 99.99 ) {
                message.innerHTML = "Value should not be greater than 99.99";
                Args.IsValid = false;
                return;
            }

            Args.IsValid = true;
            return;
        }


        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchProvinceName').value = '';
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblCuisineNameSearch" runat="server" Text="Province Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchProvinceName" runat="server" CssClass="txtSearch"></asp:TextBox>
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
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="provinceId" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting"
                                AllowSorting="True" OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("provinceId") %>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblTaxID" runat="server" Text='<% # Eval("taxID") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblProvinceHeadProvinceName" ForeColor="White" runat="server"
                                                Text="Province Name" CommandName="Sort" CommandArgument="provinceName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblProvinceNameText" Text='<% # Eval("provinceName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lbltaxRateHead" ForeColor="White" runat="server" Text="Province Tax Rate"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lbltaxRateText" Text='<% # GetTaxRates(Eval("taxRates"))+"%"%>'
                                                    runat="server"></asp:Label>
                                                <asp:Label ID="lblRate" Text='<% # GetTaxRates(Eval("taxRates")) %>' runat="server"
                                                    Visible="false"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" ImageUrl="~/admin/Images/edit.gif" />
                                        </ItemTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfProvinceID" runat="server" Value="0" />
            <asp:HiddenField ID="hfTaxID" runat="server" Value="" />
            <asp:Panel ID="pnlForm" runat="server" Visible="false">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Cuisine Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="Province Name" />
                                </td>
                                <td style="text-align: left; padding-left: 5px;">
                                    <asp:Label ID="lblProvinceNameToUpdate" Text="" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblProvince" runat="server" Text="Province" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtProvince" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvProvince" runat="server" ControlToValidate="txtProvince"
                                        Display="None" ErrorMessage="<span id='cMessage'>Please enter province name.</span>"
                                        ValidationGroup="Rate" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceProvince" TargetControlID="rfvProvince" runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblUserType" runat="server" Text="Tax Rate" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtTaxRate" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:CustomValidator ID="CustomValidator1" runat="server" ValidateEmptyText="true"
                                        ClientValidationFunction="checkRateValue" ControlToValidate="txtTaxRate" Display="None"
                                        ErrorMessage="<span id='cMessage'>Please enter valid value.</span>" ValidationGroup="Rate"
                                        SetFocusOnError="True"></cc1:CustomValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                    <%-- <cc1:RequiredFieldValidator ID="rfvCuisineName" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtCuisineName" ErrorMessage="Cuisine name required!" ValidationGroup="Cuisine"
                                        Display="None">                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcdCuisineName" runat="server" TargetControlID="rfvCuisineName">
                                    </cc2:ValidatorCalloutExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnUpdate" runat="server" ValidationGroup="Rate" ImageUrl="~/admin/images/btnUpdate.jpg"
                                        OnClick="btnUpdate_Click" Visible="True" />&nbsp;
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
