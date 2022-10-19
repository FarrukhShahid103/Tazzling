<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealVerificationManagement2.aspx.cs" Inherits="dealVerificationManagement2"
    Title="TastyGo | Admin | Deal Verification" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script src="JS/CalendarControl.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

function openPreview()
myWindow=window.open('" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Preview.aspx?sidedeal=" + NewDealID + "','','width=760,height=700,toolbar=no,status=no, menubar=no, scrollbars=yes,resizable=no');
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

    <script src="JS/jquery-1.4.min.js" type="text/javascript"></script>

    <script src="JS/CalendarControl.js" type="text/javascript"></script>

    <link href="CSS/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
    function pageLoad(){
         $("#<%=txtSearchBusinessName.ClientID%>").autocomplete('Search_CS.ashx');
         $("#<%=txtSaveBusinessName.ClientID%>").autocomplete('Search_CS.ashx');
    }
    
    </script>

    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div id="divSrchFields" runat="server">
                    <div id="search">
                        <div style="clear: both;">
                            <div class="heading" style="float: left;">
                                <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 10px; padding-right: 5px;">
                                <asp:TextBox ID="txtSearchBusinessName" runat="server" CssClass="txtSearch"></asp:TextBox>
                            </div>
                            <div class="heading" style="float: left;">
                                <asp:Label ID="lblLastNameSearch" runat="server" Width="63px" Text="Deal Title"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px; padding-right: 5px;">
                                <asp:TextBox ID="txtSrchDealTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
                            </div>
                            <div style="float: left; padding-left: 5px; padding-right: 5px;">
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                    OnClick="btnSearch_Click" />&nbsp;<asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/admin/Images/btnClear.png"
                                        OnClientClick="return clearFields();" TabIndex="2" />&nbsp;<asp:ImageButton ID="btnShowAll"
                                            runat="server" ImageUrl="~/admin/images/btnShowAll.gif" OnClick="btnShowAll_Click" />
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; height: 10px;">
                    </div>
                    <div id="search">
                        <div style="clear: both;">
                            <div class="heading" style="float: left;">
                                <asp:Label ID="Label11" runat="server" Text="Business Name"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 10px; padding-right: 5px;">
                                <asp:TextBox ID="txtSaveBusinessName" runat="server" CssClass="txtSearch"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                    ControlToValidate="txtSaveBusinessName" ErrorMessage="Business Name required!"
                                    ValidationGroup="btnSave" Display="None"></cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator9">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                            <div class="heading" style="float: left;">
                                <asp:Label ID="Label3" runat="server" Width="63px" Text="Deal Title"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px; padding-right: 5px;">
                                <asp:TextBox ID="txtSaveDealTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                    ControlToValidate="txtSaveDealTitle" ErrorMessage="Business Name required!" ValidationGroup="btnSave"
                                    Display="None"></cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                            <div style="float: left; padding-left: 5px; padding-right: 5px;">
                                <asp:ImageButton ID="btnSave" runat="server" CausesValidation="true" ValidationGroup="btnSave"
                                    ImageUrl="~/admin/Images/btnSave.jpg" OnClick="btnSave_Click" />&nbsp;<asp:ImageButton
                                        ID="ImageButton2" runat="server" ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields2();"
                                        TabIndex="2" />
                            </div>
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
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="updealId" Width="100%"
                                AllowSorting="False" AllowPaging="true" OnRowDataBound="gvViewDeals_RowDataBound"
                                OnPageIndexChanging="gvViewDeals_PageIndexChanging" OnRowCommand="gvViewDeals_Login"
                                AutoGenerateColumns="False" ShowHeader="True" ShowFooter="true" GridLines="None"
                                OnRowDeleting="gvViewDeals_RowDeleting">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("updealId") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deal Title" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" CssClass="fontStyle" ForeColor="Black" runat="server"
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 50 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,49) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResName" CssClass="fontStyle" ForeColor="Black" runat="server"
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 50 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,49) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Phone" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuccessfulOrders" CssClass="fontStyle" ForeColor="Black" runat="server"
                                                Text='<%# (Eval("phone").ToString().Trim()=="CustomerService@NameYourTuneDigital.com"?"": Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "phone").ToString())) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pre Deal Verification" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreDealVerification" CssClass="fontStyle" ForeColor="Black" runat="server"
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "preDealVerification").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Post Deal Verification" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostDealVerification" CssClass="fontStyle" ForeColor="Black" runat="server"
                                                Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "postDealVerification").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditInGrid" runat="server" CommandName="EditDetail" CommandArgument='<% #Eval("updealId")%>'
                                                CausesValidation="false" ImageUrl="~/admin/Images/edit.gif" ToolTip="Edit Deal" />&nbsp;
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                ImageUrl="~/admin/Images/delete.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "updealId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
            <asp:Panel ID="pnlDetail" runat="server" Visible="false">
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
                                    <asp:HiddenField ID="hfDealID" runat="server" />
                                    <asp:Label ID="lblDealInfoHeading" Text="Deal Verification" runat="server"></asp:Label>
                                </div>
                            </div>
                            <table cellpadding="2" cellspacing="2" width="100%" class="fontStyle" border="0">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label2" runat="server" Text="Deal Name:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblDealName" runat="server" Text="Deal Name:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label4" runat="server" Text="Deal Modify By:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblDealModifyBy" runat="server" Text="Deal Start Time:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label8" runat="server" Text="Modify At:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblDealModifyTime" runat="server" Text="Deal End Time:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label10" runat="server" Text="Business Name:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblBusinessName" runat="server" Text="Business Name:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label12" runat="server" Text="Owner First Name:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblOwnerFirstName" runat="server" Text="Owner Name:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label14" runat="server" Text="Owner Last Name:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblOwnerLastName" runat="server" Text="Owner Last Name:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label7" runat="server" Text="Business Email Address:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblBusinessEmailAddress" runat="server" Text="Business Email Address:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label9" runat="server" Text="Business Alternate Email Address:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblAlternateEmailAddress" runat="server" Text="Business Alternate Email Address:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label5" runat="server" Text="Business Phone Number:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblPhoneNumber" runat="server" Text="Business Phone Number"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label6" runat="server" Text="Business Owner's Cell Phone Number:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblCellNumber" runat="server" Text="Business Owner's Cell Phone Number:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label18" runat="server" Text="Pre Deal Verification:" />
                                        </td>
                                        <td align="left" class="colLeft fontStyle">
                                            <asp:DropDownList ID="ddlPreDealVerification" runat="server">
                                                <asp:ListItem Value="0% Data Entered" Text="0% Data Entered" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="20% Fine Print Verified" Text="20% Fine Print Verified"></asp:ListItem>
                                                <asp:ListItem Value="40% Date Verified" Text="40% Date Verified"></asp:ListItem>
                                                <asp:ListItem Value="60% Design Verified" Text="60% Design Verified"></asp:ListItem>
                                                <asp:ListItem Value="80% Pregen List sent, email confirmed" Text="80% Pregen List sent, email confirmed"></asp:ListItem>
                                                <asp:ListItem Value="100% Called Business, All Staff ready for deal" Text="100% Called Business, All Staff ready for deal"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label19" runat="server" Text="Post Deal Verification:" />
                                        </td>
                                        <td align="left" class="colLeft fontStyle">
                                            <asp:DropDownList ID="ddlPostDealVerification" runat="server">
                                                <asp:ListItem Value="0% Deal Ended" Text="0% Deal Ended" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="50% Email Notification, sent customer's List" Text="50% Email Notification, sent customer's List"></asp:ListItem>
                                                <asp:ListItem Value="75% Phone Notification" Text="75% Phone Notification"></asp:ListItem>
                                                <asp:ListItem Value="100% Confirmed Received Customer List" Text="100% Confirmed Received Customer List"></asp:ListItem>
                                            </asp:DropDownList>
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
                                                             <div style="float: right;  padding-right:20px;">
                                                                <asp:Label ID="lblCommentMessage" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div style="width: 63px; float: right;">
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:DataList ID="rptrDiscussion" RepeatColumns="1" RepeatDirection="Vertical" DataKeyField="discussionId"
                                                    runat="server" CellPadding="0" OnItemDataBound="DataListItemDataBound" OnEditCommand="Edit_Command"
                                                    OnDeleteCommand="Delete_Command" CellSpacing="0" Width="781px" GridLines="None"
                                                    ShowHeader="false">
                                                    <ItemTemplate>
                                                        <div style="border-bottom: solid 1px #B7B7B7; background-color: #FFFFFF; width: auto;
                                                            padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                                                            <div style="width: 141px; float: left; text-align: center">
                                                                <asp:Label ID="lbldiscussionId" runat="server" Visible="false" Text='<%# Eval("discussionId")%>'></asp:Label>
                                                                <asp:Image ID="imgDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px" BorderStyle="Solid"
                                                                    ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                                <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("userId")%>' />
                                                            </div>
                                                            <div style="width: 640px; float: right; text-align: left;">
                                                                <div style="width: 640px; height: 26px;">
                                                                    <div style="float: left; width: 550px; padding-right: 10px;">
                                                                        <asp:Label ID="label5" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("Name") %>'
                                                                            Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                                ID="label6" runat="server" Font-Names="Arial, Arial, sans-serif" Text='<%#Eval("cmtDatetime")%>'
                                                                                Font-Bold="True" Font-Size="13px" ForeColor="#97C717"></asp:Label>
                                                                    </div>
                                                                    <%-- <div style="float: left; width: 80px;">
                                                                        <div style="float: left; padding-right: 10px;">
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Edit Comment" CommandArgument='<%# Eval("discussionId") %>'
                                                                                CommandName="Edit" ImageUrl="~/admin/Images/edit.gif" />
                                                                        </div>
                                                                        <div style="float: left;">
                                                                            <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("discussionId") %>'
                                                                                ImageUrl="~/admin/Images/delete.gif" OnClientClick='return confirm("Are you sure you want to delete this comment?");'
                                                                                ToolTip="Delete Comment" />
                                                                        </div>
                                                                    </div>--%>
                                                                </div>
                                                                <div style="width: 628px; padding-right: 12px;">
                                                                    <asp:Label ID="label7" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("comments")%>'
                                                                        Font-Size="13px" ToolTip='<%# Eval("comments")%>' ForeColor="#7C7B7B"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
