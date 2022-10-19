<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealVerificationManagement.aspx.cs" Inherits="dealVerificationManagement"
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
            document.getElementById('ctl00_ContentPlaceHolder1_txtSrchBusinessName').value = '';  
                      
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
                    <div id="search">
                        <div style="clear: both;">
                            <div class="heading" style="float: left;">
                                <asp:Label ID="lblLastNameSearch" runat="server" Width="63px" Text="Deal Title"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px; padding-right: 5px;">
                                <asp:TextBox ID="txtSrchDealTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
                            </div>
                            <div class="heading" style="float: left;">
                                <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 10px; padding-right: 5px;">
                                <asp:TextBox ID="txtSrchBusinessName" runat="server" CssClass="txtSearch"></asp:TextBox>
                            </div>
                            <div class="heading" style="float: left;">
                                <asp:Label ID="lblUsernameSearch" runat="server" Text="Deals Archive"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 10px; padding-right: 5px;">
                                <asp:DropDownList ID="ddlSearchStatus" runat="server">
                                    <asp:ListItem Value="started" Text="Started Deals"></asp:ListItem>
                                    <asp:ListItem Value="upcoming" Text="Upcoming Deals"></asp:ListItem>
                                    <asp:ListItem Value="expired" Text="Expired Deals"></asp:ListItem>
                                    <asp:ListItem Value="all" Text="All" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 10px;">
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                    OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                        ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                        TabIndex="2" />&nbsp;<asp:ImageButton ID="btnShowAll" runat="server" ImageUrl="~/admin/images/btnShowAll.gif"
                                            OnClick="btnShowAll_Click" />
                            </div>
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
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="dealId" Width="100%"
                                AllowSorting="False" AllowPaging="true" OnRowDataBound="gvViewDeals_RowDataBound"
                                OnPageIndexChanging="gvViewDeals_PageIndexChanging" OnRowCommand="gvViewDeals_Login"
                                AutoGenerateColumns="False" ShowHeader="True" ShowFooter="true" GridLines="None">
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="AltgridText" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfdealpayment" runat="server" Value='<% # Eval("dealpayment") %>' />
                                            <asp:CheckBox runat="server" value='<% # Eval("dealID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealStartTime").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealEndTime").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deal Title" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 30 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,29) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 30 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,29) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Email" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealCity" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "email").ToString().Length > 20 ? DataBinder.Eval (Container.DataItem, "email").ToString().Substring(0,17) + "..." :DataBinder.Eval (Container.DataItem, "email").ToString()) %>'
                                                ToolTip='<%# DataBinder.Eval (Container.DataItem, "email") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Phone" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuccessfulOrders" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "phone").ToString().Length > 15 ? DataBinder.Eval (Container.DataItem, "phone").ToString().Substring(0,13) + "..." :DataBinder.Eval (Container.DataItem, "phone").ToString()) %>'
                                                ToolTip='<%# Eval("phone") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pre Deal Verification" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreDealVerification" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "preDealVerification").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Post Deal Verification" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostDealVerification" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "postDealVerification").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pre Voucers" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="updownloadExcel" runat="server">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="imgbtnExportToExcel" runat="server" Visible='<%#(Eval("TotalVoucher")==DBNull.Value ? false : Convert.ToInt32(Eval("TotalVoucher"))>0 ?  true : false) %>'
                                                        CommandArgument='<% #Eval("dealId") %>' CommandName="DownloadVoucers" CausesValidation="false"
                                                        ImageUrl="~/admin/Images/download_excel.png" ToolTip="Download Sample voucehrs." />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Successful Orders" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="updownloadExcelOrders" runat="server">
                                                <ContentTemplate>
                                                    <asp:ImageButton ID="imgbtnExportToExcelSuccessfulOrder" runat="server" Visible='<%#(Eval("SuccessfulOrder")==DBNull.Value ? false : Convert.ToInt32(Eval("SuccessfulOrder"))>0 ?  true : false) %>'
                                                        CommandArgument='<% #Eval("dealId")+","+Eval("voucherExpiryDate")+","+Eval("SuccessfulOrder") %>'
                                                        CommandName="DownloadOrders" CausesValidation="false" ImageUrl="~/admin/Images/download_excel.png"
                                                        ToolTip="Download successful orders." />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="imgbtnExportToExcelSuccessfulOrder" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlViewInvoice" runat="server" Target="_blank" NavigateUrl='<%#"dealInvoiceManagment.aspx?did="+Eval("dealId")%>'
                                                ImageUrl="~/admin/Images/view_icon.gif" ToolTip="View Invoice"> </asp:HyperLink>
                                            &nbsp;
                                            <asp:ImageButton ID="btnEditInGrid" runat="server" CommandName="EditDetail" CommandArgument='<% #Eval("dealId")%>'
                                                CausesValidation="false" ImageUrl="~/admin/Images/edit.gif" ToolTip="Edit Deal" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "dealId") %>'></asp:Label>
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
                                            <asp:Label ID="Label16" runat="server" Text="Deal Preview:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:HyperLink ID="hlPreviewLink" runat="server" Target="_blank"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label4" runat="server" Text="Deal Start Time:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblDealStartTime" runat="server" Text="Deal Start Time:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label8" runat="server" Text="Deal End Time:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblDealEndTime" runat="server" Text="Deal End Time:"></asp:Label>
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
                                            <asp:Label ID="Label14" runat="server" Text="Deal End Time:"></asp:Label>
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
                                        <td align="right" style="vertical-align: top;">
                                            <asp:Label ID="Label77" runat="server" Text="Total Deal Sold:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4" style="line-height: 18px;">
                                            <asp:Label ID="lblDealSoldSummery" runat="server" Text="Total Deal Sold:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label18" runat="server" Text="Pre Deal Verification:" />
                                        </td>
                                        <td align="left" class="colLeft fontStyle">
                                            <asp:DropDownList ID="ddlPreDealVerification" runat="server">
                                                <asp:ListItem Value="0% Not contacted yet" Text="0% Not contacted yet" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="50% Emailed Pregen List & Preview Link" Text="50% Emailed Pregen List & Preview Link"></asp:ListItem>
                                                <asp:ListItem Value="75% Called, no response and Left messages" Text="75% Called, no response and Left messages"></asp:ListItem>
                                                <asp:ListItem Value="100% Called & Confirmed. Business ready for the deal" Text="100% Called & Confirmed. Business ready for the deal"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label19" runat="server" Text="Post Deal Verification:" />
                                        </td>
                                        <td align="left" class="colLeft fontStyle">
                                            <asp:DropDownList ID="ddlPostDealVerification" runat="server">
                                                <asp:ListItem Value="0% Not contacted yet" Text="0% Not contacted yet" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="50% Emailed Buyer's list, waiting for response" Text="50% Emailed Buyer's list, waiting for response"></asp:ListItem>
                                                <asp:ListItem Value="100% Called & Confirmed." Text="100% Called & Confirmed."></asp:ListItem>
                                            </asp:DropDownList>
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
