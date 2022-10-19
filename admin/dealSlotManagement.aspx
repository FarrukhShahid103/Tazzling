<%@ Page Title="Deal Slot Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="dealSlotManagement.aspx.cs" Inherits="admin_dealSlotManagement"
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
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div id="divSrchFields" runat="server">
                    <div id="search" style="display: none;">
                        <div class="heading">
                            <asp:Label ID="lblSearchBusinessName" runat="server" Text="Business Name" Width="92px"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSrchBusinessName" runat="server" Width="192px">
                            </asp:DropDownList>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblLastNameSearch" runat="server" Width="63px" Text="Deal Title"></asp:Label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSrchDealTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading">
                            <asp:Label ID="lblUsernameSearch" runat="server" Text="Deals Archive"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSearchStatus" runat="server">
                                <asp:ListItem Value="started" Text="Started Deals" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="upcoming" Text="Upcoming Deals"></asp:ListItem>
                                <asp:ListItem Value="expired" Text="Expired Deals"></asp:ListItem>
                                <asp:ListItem Value="all" Text="All"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="search">
                        <%--  <div class="heading">
                            <asp:Label ID="Label1" runat="server" Text="Select Provice" Width="92px"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSearchProvince" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchProvince_SelectedIndexChanged" runat="server" Width="192px">
                            </asp:DropDownList>                            
                        </div>--%>
                        <div class="heading">
                            <asp:Label ID="Label3" runat="server" Text="Select City"></asp:Label>
                        </div>
                        <div>
                            <asp:DropDownList ID="ddlSearchCity" runat="server" Width="192px">
                                <asp:ListItem Text="Select One" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="National" Value="8129"></asp:ListItem>
                                <asp:ListItem Text="Abbotsford" Value="1710"></asp:ListItem>
                                <asp:ListItem Text="Brampton" Value="1720"></asp:ListItem>
                                <asp:ListItem Text="Calgary" Value="1376"></asp:ListItem>
                                <asp:ListItem Text="Edmonton" Value="1709"></asp:ListItem>
                                <asp:ListItem Text="Halifax" Value="1716"></asp:ListItem>
                                <asp:ListItem Text="Hamilton" Value="1722"></asp:ListItem>
                                <asp:ListItem Text="Mississauga" Value="1726"></asp:ListItem>
                                <asp:ListItem Text="Oakville - Burlington" Value="1727"></asp:ListItem>
                                <asp:ListItem Text="St. Catharines" Value="1729"></asp:ListItem>
                                <asp:ListItem Text="Surrey" Value="1712"></asp:ListItem>
                                <asp:ListItem Text="Vancouver" Value="337"></asp:ListItem>
                                <asp:ListItem Text="Victoria" Value="1713"></asp:ListItem>
                                <asp:ListItem Text="Toronto" Value="338"></asp:ListItem>
                                <asp:ListItem Text="York Region" Value="1733"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="2" />
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
                    <asp:UpdatePanel ID="upItems" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="dealId" Width="100%"
                                AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False" ShowHeader="True"
                                ShowFooter="true" GridLines="None">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Business Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "restaurantID").ToString()) %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deal Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "title").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealCity" CssClass="fontStyle" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "cityName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deal Slot">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealSlot" CssClass="fontStyle" runat="server" Text='<%# "Slot " + DataBinder.Eval (Container.DataItem, "dealSlot") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartTime" CssClass="fontStyle" runat="server" Text='<%# Convert.ToDateTime(Eval("dealStartTimeC")).ToString("MM-dd-yyyy") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndTIme" CssClass="fontStyle" runat="server" Text='<%# Convert.ToDateTime(Eval("dealEndTimeC")).ToString("MM-dd-yyyy") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpiry" CssClass="fontStyle" runat="server" Text='<%# Eval("voucherExpiryDate")!=DBNull.Value? Convert.ToDateTime(Eval("voucherExpiryDate")).ToString("MM-dd-yyyy"): "" %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "dealId") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="text-align: left;">
                    <asp:Button ID="btnUpdateSlot" runat="server" Text="Bump up Slot" OnClick="btnUpdateSlot_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
