<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealCauseManagement.aspx.cs" Inherits="admin_dealCauseManagement" Title="TastyGo | Admin | Deal Cause Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script src="JS/CalendarControl.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    
     function OpenCalendar(ctrl) {
            var Cal = window.document.getElementById(ctrl);
            showCalendarControl(Cal);
        }

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
            document.getElementById('ctl00_ContentPlaceHolder1_txtDealCauseTitleSearch').value = '';
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
                        <asp:Label ID="lblCityNameSearch" runat="server" Text="Cause Title"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtDealCauseTitleSearch" runat="server" CssClass="txtSearch">
                        </asp:TextBox>
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
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="cause_ID" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                                OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting" AllowSorting="True"
                                OnSorting="pageGrid_Sorting" OnSelectedIndexChanged="pageGrid_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("cause_ID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("cause_ID") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCauseTitleHead" ForeColor="White" runat="server" Text="Cause Title"
                                                CommandName="Sort" CommandArgument="cause_title"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblCauseTitleText" Text='<% # Eval("cause_title") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblDealStartATimeTitle" ForeColor="White" runat="server" Text="Start Time"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblDealStartTimeText" Text='<%# Eval("cause_startTime").ToString().Trim()==""?"":Convert.ToDateTime(Eval("cause_startTime")).ToString("MM-dd-yyyy")%>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblDealEndTimeTitle" ForeColor="White" runat="server" Text="End Time"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblDealEndTimeText" Text='<%# Eval("cause_endTime").ToString().Trim()==""?"": Convert.ToDateTime(Eval("cause_endTime")).ToString("MM-dd-yyyy")%>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblDealCauseCity" ForeColor="White" runat="server" Text="City"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblCauseCityText" Text='<%# Eval("cityName").ToString().Trim()%>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblDealLinkTitle" ForeColor="White" runat="server" Text="Link"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblDealLinkText" Text='<%# Eval("cause_link") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCityHeadDescription" ForeColor="White" runat="server" Text="Short Description"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lbltaxDescription" Text='<% # Eval("cause_shortDescription").ToString().Length > 150 ? Eval("cause_shortDescription").ToString().Substring(0,150) + "..." : Eval("cause_shortDescription").ToString() %>'
                                                    runat="server"></asp:Label>
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
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfProvinceID" runat="server" Value="0" />
            <asp:HiddenField ID="hfTaxID" runat="server" Value="" />
            <asp:Panel ID="pnlForm" runat="server" Visible="false">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Deal Cause Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr id="trErrorMsg" runat="server" visible="false">
                                <td>
                                </td>
                                <td align="right" class="colRight">
                                    <div class="clear" style="padding-top: 15px; padding-bottom: 20px; width: 100%" align="center">
                                        <div style="float: left; padding-right: 5px">
                                            <asp:Image ID="Image1" runat="server" Visible="true" ImageUrl="~/admin/images/error.png" />
                                        </div>
                                        <div class="floatLeft">
                                            <asp:Label ID="lblErrorMsg" runat="server" Text="Cause with this name already exists"
                                                ForeColor="Red" CssClass="fontStyle"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <%--  <tr id="trUpdate" runat="server">
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblCityNameTitle" runat="server" Text="City Name" />
                                </td>
                                <td style="text-align: left; padding-left: 5px;">
                                     <asp:Label ID="lblCityNameToUpdate" runat="server"></asp:Label>
                                </td>
                            </tr>--%>                           
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblCityName" runat="server" Text="Cause Title" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtCauseTitle" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCauseTitle"
                                        Display="None" ErrorMessage="<span id='cMessage'>Cause Title required!.</span>"
                                        ValidationGroup="city" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceCity" TargetControlID="rfvCity" runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label4" runat="server" Text="Cause Link" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtLink" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLink"
                                        Display="None" ErrorMessage="<span id='cMessage'>Cause Link required!.</span>"
                                        ValidationGroup="city" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<span id='cMessage'>Please enter valid URL.<br>e.g. http(s)://abc.com</span>"
                                        ControlToValidate="txtLink" Display="None" ValidationGroup="city" SetFocusOnError="True"
                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></cc1:RegularExpressionValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RegularExpressionValidator1"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="Short Description" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtShortDescription" runat="server" TextMode="MultiLine" Height="50px"
                                        CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShortDescription"
                                        Display="None" ErrorMessage="<span id='cMessage'>Short Description required!.</span>"
                                        ValidationGroup="city" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label2" runat="server" Text="Long Description" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtLongDescription" runat="server" TextMode="MultiLine" Height="80px"
                                        CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLongDescription"
                                        Display="None" ErrorMessage="<span id='cMessage'>Long Description required!.</span>"
                                        ValidationGroup="city" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label5" runat="server" Text="Start Time" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtForm" Width="92px" MaxLength="12" onclick="OpenCalendar('ctl00_ContentPlaceHolder1_txtStartDate');"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartDate"
                                        ErrorMessage="Start Date required" ValidationGroup="city" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label6" runat="server" Text="End Time" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtForm" Width="92px" MaxLength="12"
                                        onclick="OpenCalendar('ctl00_ContentPlaceHolder1_txtEndDate');"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEndDate"
                                        ErrorMessage="End Date required" Display="None" ValidationGroup="city" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator5">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                             <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label7" runat="server" Text="City" />
                                </td>
                                <td align="left" class="colLeft">
                                   <asp:DropDownList ID="ddlDealCauseCity" runat="server" CssClass="txtForm">
                                   </asp:DropDownList>                             
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label3" runat="server" Text="Cause Image" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="fuCauseImage1" runat="server" CssClass="txtForm" Width="313px" />
                                            <cc1:RequiredFieldValidator ID="rfvDealImage1" runat="server" ControlToValidate="fuCauseImage1"
                                                ErrorMessage="Image required" Display="None" ValidationGroup="city" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="rfvDealImage1">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidation"
                                                ControlToValidate="fuCauseImage1" Display="None" ErrorMessage="Invalid file format."
                                                ValidationGroup="city" SetFocusOnError="True"></cc1:CustomValidator>
                                            <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                                runat="server">
                                            </cc2:ValidatorCalloutExtender>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnUpdate" />
                                            <asp:PostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <img id="imgUpload1" runat="server" src="" class="menuImageBorder" alt="" width="41"
                                        visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="city" ImageUrl="~/admin/Images/btnSave.jpg"
                                        Visible="True" OnClick="btnSave_Click" />
                                    <asp:ImageButton ID="btnUpdate" runat="server" ValidationGroup="city" ImageUrl="~/admin/images/btnUpdate.jpg"
                                        OnClick="btnUpdate_Click" Visible="false" />&nbsp;
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
