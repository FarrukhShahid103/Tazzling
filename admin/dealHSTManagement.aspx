<%@ Page Title="Deal HST Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="dealHSTManagement.aspx.cs" Inherits="dealHSTManagement"
    ValidateRequest="false" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script src="JS/CalendarControl.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSrchDealTitle').value = '';            
            return false;
        }

        function Toggle(commId, imageId) {
            var div = document.getElementById(commId);
            var GetImg = document.getElementById(imageId);
            if (document.getElementById(commId).style.display == 'none') {
                document.getElementById(commId).style.display = 'block';
                document.getElementById(imageId).src = 'Images/expand.gif';
            }
            else {
                document.getElementById(commId).style.display = 'none';
                document.getElementById(imageId).src = 'Images/collapse.gif';
            }
        }

        function ValidateFoodFields(txtFoodName, txtFoodDesc, txtFoodPrice) {

            var foodName = document.getElementById(txtFoodName);
            var foodDesc = document.getElementById(txtFoodDesc);
            var foodPrice = document.getElementById(txtFoodPrice);

            if (foodName.value == "") {
                alert("Please insert food name.");
                foodName.focus();
                return false;
            }
            if (foodDesc.value == "") {
                alert("Please insert food description.");
                foodDesc.focus();
                return false;
            }
            if (foodPrice.value == "") {
                alert("Please insert food price.");
                foodPrice.focus();
                return false;
            }

            if (!IsNumeric(foodPrice.value)) {
                alert("Please insert food price.");
                foodPrice.value = "";
                foodPrice.focus();
                return false;
            }
            return true;


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

        function showDelConfirmBox(intID) {
            document.getElementById('ctl00_ContentPlaceHolder1_ctrlResFeaturedFood2_hidFoodTypeId').value = intID;
            document.getElementById('confirmationBoxBackGround').style.display = 'block';
            document.getElementById('ctl00_ContentPlaceHolder1_ctrlResFeaturedFood2_pnlDelete').style.display = 'block';
            return false;
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

        function priceValidation(oSrc, args) {

            if (args.Value != "") {
                var strPrice = args.Value;

                if (IsNumeric(strPrice)) {
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

        function showAddBox() {
            document.getElementById('confirmationBoxBackGround').style.display = 'block';
            document.getElementById('ctl00_ContentPlaceHolder1_pnlSubItem').style.display = 'block';
        }

        function OpenCalendar(ctrl) {
            var Cal = window.document.getElementById(ctrl);
            showCalendarControl(Cal);
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
            if (document.getElementById("ctl00_ContentPlaceHolder1_gvViewDeals_ctl01_HeaderLevelCheckBox").checked) {
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
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div id="divSrchFields" runat="server">
                    <div id="search">
                        <div class="heading">
                            <asp:Label ID="Label3" runat="server" Text="Select Month"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList runat="server" Width="200px" ID="ddlMonth">
                                <asp:ListItem Text="January" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="heading">
                            <asp:Label ID="Label1" runat="server" Text="Select Year" Width="92px"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="192px">
                            </asp:DropDownList>
                        </div>
                        <div>
                            <div style="float: left;">
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                    OnClick="btnSearch_Click" TabIndex="1" />
                            </div>
                            <div style="float: left; padding-left: 10px;">
                                <asp:ImageButton ID="imgbtnExportToExcel" runat="server" ImageUrl="~/admin/Images/export_excel.png"
                                    TabIndex="3" OnClick="imgbtnExportToExcel_Click" /></div>
                        </div>
                    </div>
                </div>
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div style="clear: both;" align="center">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="dealID" Width="100%"
                        AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False" ShowHeader="True"
                        ShowFooter="true" GridLines="None">
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridText" HorizontalAlign="Center" Height="25px" />
                        <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" Height="25px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" CssClass="fontStyle" runat="server" Text='<%#Eval("Date")%>'>                                                
                                    </asp:Label>
                                    <asp:HiddenField ID="lblDealId" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "dealId").ToString()) %>' />
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deal Title" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Title").ToString()) %>'
                                        ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "Title").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="35%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HST Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblHSTType" CssClass="fontStyle" runat="server" Text='<%# Eval("Type")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HST Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblHSTAmount" CssClass="fontStyle" runat="server" Text='<%# Eval("Amount") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div id="emptyRowStyle" align="left">
                                <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
