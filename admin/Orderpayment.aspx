<%@ Page Title="Deal Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="Orderpayment.aspx.cs" Inherits="admin_Orderpayment"
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
    <asp:UpdatePanel ID="udpnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfDealId" runat="server" Value="0" />
            <asp:HiddenField ID="hfCityID" runat="server" Value="0" />
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div id="divSrchFields" runat="server">
                    <div id="search">
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
                                AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False" OnRowDataBound="gvViewDeals_RowDataBound"
                                ShowHeader="True" ShowFooter="true" GridLines="None" OnRowEditing="gvViewDeals_RowEditing"
                                OnSelectedIndexChanged="gvViewDeals_SelectedIndexChanged">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("dealID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 25 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,25) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deal Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 22 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,20) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealCity" CssClass="fontStyle" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "cityName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellingPrice" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "paymentStatus").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Selling Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellingPrice" CssClass="fontStyle" runat="server" Text='<%# "$ "+  Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "sellingPrice").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValuePrice" CssClass="fontStyle" runat="server" Text='<%# "$ "+  Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "valuePrice").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>       --%>
                                    <asp:TemplateField HeaderText="Deal Note">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealSlot" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealNote").ToString().Length > 22 ? DataBinder.Eval (Container.DataItem, "dealNote").ToString().Substring(0,20) + "..." :DataBinder.Eval (Container.DataItem, "dealNote").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealNote").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Successful Orders">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuccessfulOrders" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "SuccessfulOrder").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancelled Orders">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCancelledOrder" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "CancelledOrder").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Refunded Orders">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPendingOrders" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "RefundedOrder").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnUsedDeals" CssClass="fontStyle" runat="server" Text='<%# Convert.ToDateTime(Eval("dealEndTime")).ToString("yyyy-MM-dd") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActive" CssClass="fontStyle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dealStatus") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCityIDHidden" runat="server" Visible="false" Text='<%# Eval("cityid") %>'></asp:Label>
                                            <asp:ImageButton ID="btnEditInGrid" runat="server" CommandName="Edit" CausesValidation="false"
                                                ImageUrl="~/admin/Images/edit.gif" ToolTip="Edit Deal" />
                                        </ItemTemplate>
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
                <div id="divAddNewDeal" runat="server" visible="false" style='padding-left: 18px;'>
                    <div id="element-box1">
                        <div class="t">
                            <div class="t">
                                <div class="t">
                                </div>
                            </div>
                        </div>
                        <div class="m">
                            <div class="m">
                                <div id="popHeader">
                                    <div style="float: left">
                                        <asp:HiddenField ID="hfBusinessId" runat="server" />
                                        <asp:Label ID="lblDealInfoHeading" Text="Add New Deal Info" runat="server"></asp:Label>
                                        <asp:Label ID="lblBusinessName" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <table cellpadding="2" cellspacing="2" width="100%" class="fontStyle" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label1" runat="server" Text="Deal Name:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblDealName" runat="server" Text="Deal Name:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Business Payment Title:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblBusinessPaymentTitle" runat="server" Text="Business Payment Title:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label8" runat="server" Text="Business Payment Address:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblBusinessPaymentAddress" runat="server" Text="Business Payment Title:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" Text="Business Email Address:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblBusinessEmailAddress" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Commission:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblCommission" runat="server" Text="Commission:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Deal Ended Date:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblDealEndedDate" runat="server" Text="Deal Ended Date:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trShippingAndTax" runat="server" visible="false">
                                            <td align="right" style="vertical-align: top;">
                                                <asp:Label ID="Label9" runat="server" Text="Shipping & Tax Collect:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="line-height: 18px;">
                                                <asp:Label ID="lblShippingAndTax" runat="server" Text="Total Deal Sold:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top;">
                                                <asp:Label ID="Label5" runat="server" Text="Total Deal Sold:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4" style="line-height: 18px;">
                                                <asp:Label ID="lblDealSoldSummery" runat="server" Text="Total Deal Sold:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label6" runat="server" Text="Business Payment Status"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:DropDownList ID="ddlPaymentStatus" runat="server">
                                                    <asp:ListItem Value="Not Paid" Selected="True">Not Paid</asp:ListItem>
                                                    <asp:ListItem Value="1st Payment">1st Payment</asp:ListItem>
                                                    <asp:ListItem Value="2nd Payment">2nd Payment</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top;">
                                                <asp:Label ID="lblTopTitle" runat="server" Text="Deal Note<br>(Max 500 characters):"></asp:Label>
                                            </td>
                                            <td align="left" colspan="4">
                                                <asp:TextBox ID="txtDealNote" runat="server" CssClass="txtForm" TextMode="MultiLine"
                                                    Width="650px" Height="200px" MaxLength="500"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDealNote"
                                                    ErrorMessage="Note required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender24" TargetControlID="RequiredFieldValidator11">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                                <div style="float: left;">
                                                    <asp:ImageButton ID="btnImgSave" runat="server" ValidationGroup="FeaturedFood" Visible="true"
                                                        ImageUrl="~/admin/images/btnSave.jpg" ToolTip="Save Deal Info" OnClick="btnImgSave_Click" /></div>
                                                <div style="float: left; padding-left: 5px;">
                                                    <asp:ImageButton ID="btnImgCancel" runat="server" CausesValidation="false" ImageUrl="~/admin/images/btnConfirmCancel.gif"
                                                        OnClick="btnImgCancel_Click" /></div>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="b">
                            <div class="b">
                                <div class="b">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
