<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="dealVerificationMilestone.aspx.cs" Inherits="dealVerificationMilestone"
    Title="TastyGo | Admin | Deal Verification Milestone" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

function openPreview()
myWindow=window.open('" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Preview.aspx?sidedeal=" + NewDealID + "','','width=760,height=700,toolbar=no,status=no, menubar=no, scrollbars=yes,resizable=no');
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSrchDealTitle').value = '';            
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchBusinessName').value = '';            
            return false;
        }
        
          function clearFields2() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSaveDealTitle').value = '';            
            document.getElementById('ctl00_ContentPlaceHolder1_txtSaveBusinessName').value = '';            
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
                                <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 10px; padding-right: 5px;">
                                <asp:TextBox ID="txtSaveBusinessName" runat="server" CssClass="txtSearch"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                    ControlToValidate="txtSaveBusinessName" ErrorMessage="Business Name required!" ValidationGroup="btnSave"
                                    Display="None"></cc1:RequiredFieldValidator>
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
                                <asp:ImageButton ID="btnSave" runat="server" CausesValidation="true" ValidationGroup="btnSave" ImageUrl="~/admin/Images/btnSave.jpg"
                                    OnClick="btnSave_Click" />&nbsp;<asp:ImageButton ID="ImageButton2" runat="server"
                                        ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields2();"
                                        TabIndex="2" />
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
                            <asp:GridView ID="gvViewDeals" runat="server" DataKeyNames="updealId" Width="100%"
                                AllowSorting="False" AllowPaging="true" ForeColor="Black" OnPageIndexChanging="gvViewDeals_PageIndexChanging"
                                AutoGenerateColumns="False" OnRowDataBound="gvViewDeals_RowDataBound" ShowHeader="True" ShowFooter="true" GridLines="None">
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
                                            <asp:Label ID="lblItemName" CssClass="fontStyle" ForeColor="Black" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 50 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,49) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResName" CssClass="fontStyle" ForeColor="Black" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Length > 50 ? DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString().Substring(0,49) + "..." :DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantBusinessName").ToString()) %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hfResID" runat="server" Value='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "restaurantID").ToString()) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Email" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealCity" CssClass="fontStyle" ForeColor="Black" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "email").ToString().Length > 25 ? DataBinder.Eval (Container.DataItem, "email").ToString().Substring(0,25) + "..." :DataBinder.Eval (Container.DataItem, "email").ToString()) %>'
                                                ToolTip='<%# DataBinder.Eval (Container.DataItem, "email") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Business Phone" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuccessfulOrders" CssClass="fontStyle" ForeColor="Black" runat="server" Text='<%# (Eval("phone").ToString().Trim()=="CustomerService@NameYourTuneDigital.com"?"": Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "phone").ToString())) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pre Deal Verification" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreDealVerification" CssClass="fontStyle" ForeColor="Black" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "preDealVerification").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Post Deal Verification" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostDealVerification" CssClass="fontStyle" ForeColor="Black" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "postDealVerification").ToString()) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20px">
                                        <ItemTemplate>
                                            <a href='<%# "dealVerificationDetail.aspx?did=" +DataBinder.Eval (Container.DataItem, "updealId")%>'
                                                target="_blank">
                                                <img id="imgEdit" src="../admin/Images/edit.gif" title="Edit Deal" />
                                            </a>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
