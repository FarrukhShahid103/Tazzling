<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="uploaddocuments.aspx.cs" Inherits="uploaddocuments" Title="TastyGo | Admin | Upload Documents Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script src="JS/CalendarControl.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
       
        function ImageValidation(oSrc, args) {

            if (args.Value != "") {
                var strFUImage = args.Value;
                var strFUImageArray = strFUImage.split(".");
                var strFUImageExt = strFUImageArray[1];
                if (strFUImageExt == "EXE" || strFUImageExt == "exe" || strFUImageExt == "JS" || strFUImageExt == "js" || strFUImageExt == "rar" ||
                strFUImageExt == "RAR" || strFUImageExt == "ZIP" || strFUImageExt == "zip" || strFUImageExt == "EML" || strFUImageExt == ".eml"
                 || strFUImageExt == "VB" || strFUImageExt == "vb" || strFUImageExt == "VBS" || strFUImageExt == "vbs" 
                 || strFUImageExt == "HTA" || strFUImageExt == "hta" || strFUImageExt == "INI" || strFUImageExt == "ini" 
                 || strFUImageExt == "CS" || strFUImageExt == "cs" || strFUImageExt == "aspx" || strFUImageExt == "ASPX") {
                    args.IsValid = false;
                    return;
                }
                else {
                    args.IsValid = true;
                    return;
                }
            }
            else {
                args.IsValid = false;
                return;
            }
        }


        function clearFields() {            
            document.getElementById('ctl00_ContentPlaceHolder1_txtContestWinnerTitleSearch').value = '';
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
                <div id="searchBig">
                    <div style="clear: both;">
                        <div class="heading" style="float: left;">
                            <asp:Label ID="lblCityNameSearch" runat="server" Text="Doucment Name"></asp:Label>
                        </div>
                        <div style="float: left; padding-left: 10px;">
                            <asp:TextBox ID="txtContestWinnerTitleSearch" runat="server" CssClass="txtSearch">
                            </asp:TextBox>
                        </div>
                        <div style="float: left; padding-left: 10px;">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="2" />
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 3px;">
                        <div class="heading" style="float: left;">
                            <asp:Label ID="lblpopHead" Text="Select File" runat="server"></asp:Label></div>
                        <div style="float: left; padding-left: 10px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:FileUpload ID="fuContestImage1" runat="server" CssClass="txtForm" Width="200px" />
                                    <cc1:RequiredFieldValidator ID="rfvDealImage1" runat="server" ControlToValidate="fuContestImage1"
                                        ErrorMessage="File required" Display="None" ValidationGroup="city" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="rfvDealImage1">
                                    </cc2:ValidatorCalloutExtender>
                                    <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidation"
                                        ControlToValidate="fuContestImage1" Display="None" ErrorMessage="This file not allowed to uplaod."
                                        ValidationGroup="city" SetFocusOnError="True"></cc1:CustomValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div style="float: left; padding-left: 10px;">
                            <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="city" ImageUrl="~/admin/Images/btnSave.jpg"
                                Visible="True" OnClick="btnSave_Click" />
                        </div>
                          <div class="heading" style="float: left; padding-left:20px;">
                           Note: Upload file size upto 4MB Max.</div>
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
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="udID" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                                OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting" AllowSorting="True"
                                OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("udID") %>' ID="RowLevelCheckBox" onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("udID") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblcontestWinnerTitleHead" ForeColor="White" runat="server" Text="File Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblFileName" Text='<% # Eval("ud_Name") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblFileURLHead" ForeColor="White" runat="server" Text="File URL"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <a href='<%# ConfigurationManager.AppSettings["YourSite"]+"/Images/uploaddata/"+Eval("ud_Name") %>'
                                                    target="_blank">
                                                    <%# ConfigurationManager.AppSettings["YourSite"]+"/Images/uploaddata/"+Eval("ud_Name") %></a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblDealStartATimeTitle" ForeColor="White" runat="server" Text="Type"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblDealStartTimeText" Text='<%# Eval("ud_Type")%>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                ImageUrl="~/admin/Images/delete.gif" />
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
</asp:Content>
