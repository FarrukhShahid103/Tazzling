<%@ Control Language="C#" AutoEventWireup="true" CodeFile="restaurantMenu.ascx.cs"
    Inherits="Takeout_UserControls_Templates_restaurantMenu" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/FrameStart.ascx" TagName="FrameStart"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Templates/FrameEnd.ascx" TagName="FrameEnd"
    TagPrefix="uc2" %>

<script language="javascript" type="text/javascript">

    function Toggle(commId, imageId) {
        //alert(commId + imageId);
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

    function ValidateMenuFields(txtItemName, txtItemDesc, txtItemSubName, txtItemPrice) {

        var itemName = document.getElementById(txtItemName);
        var itemDesc = document.getElementById(txtItemDesc);
        var itemSubName = document.getElementById(txtItemSubName);
        var itemPrice = document.getElementById(txtItemPrice);

        if (itemName.value == "") {
            alert("Please insert item name.");
            itemName.focus();
            return false;
        }
        if (itemDesc.value == "") {
            alert("Please insert item description.");
            itemDesc.focus();
            return false;
        }
        if (itemPrice.value == "") {
            alert("Please insert item price.");
            itemPrice.focus();
            return false;
        }

        if (!IsNumeric(itemPrice.value)) {
            alert("Please insert valid value.");
            itemPrice.value = "";
            itemPrice.focus();
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
        document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_restaurantMenu_restaurantMenu1_hidFoodTypeId').value = intID;
        document.getElementById('confirmationBoxBackGround').style.display = 'block';
        document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_restaurantMenu_restaurantMenu1_pnlDelete').style.display = 'block';
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
</script>

<asp:UpdatePanel ID="upMain" runat="server">
    <ContentTemplate>
        <div id="confirmationBoxBackGround" style="display: none;">
            <div id="confirmBox">
            </div>
        </div>
        <asp:Panel ID="pnlDelete" runat="server" CssClass="confirmationStyle">
            <div class="headingStripConfirm">
                <div class="floatLeft">
                    <asp:Label ID="lblConfirmationBox" runat="server" Text="Tasty Go"></asp:Label></div>
                <div class="floatRight" style="padding-right: 4px;">
                    <asp:LinkButton ID="lbClose" ForeColor="White" Style="text-decoration: none; outline: none"
                        runat="server" Text="[ X ]" OnClientClick="document.getElementById('confirmationBoxBackGround').style.display='none'; document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_restaurantMenu_restaurantMenu1_pnlDelete').style.display='none'; return false;"></asp:LinkButton>
                </div>
            </div>
            <div class="conTextStyle">
                <asp:Label ID="lblMsg" runat="server" Text="Select the image to change."></asp:Label>
            </div>
            <div>
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="padding-left: 12px;">
                            <asp:UpdatePanel ID="upImage" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:FileUpload ID="fpChange" runat="server" />
                                    <asp:TextBox ID="hidFoodTypeId" runat="server" Style="display: none"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnChange" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="padding-left: 12px;">
                            <asp:Button ID="btnChange" runat="server" ValidationGroup="change" Text="Save" CssClass="btn_orange_smaller"
                                ImageUrl="~/admin/Images/btnSave.jpg" OnClick="btnChange_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 12px;">
                            <cc1:CustomValidator ID="cvfpChange" runat="server" ClientValidationFunction="ImageValidation"
                                ControlToValidate="fpChange" Display="Static" ValidateEmptyText="true" ValidationGroup="change"
                                ErrorMessage="Please upload image file." SetFocusOnError="True"></cc1:CustomValidator>
                        </td>
                        <td style="padding-left: 12px;">
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlForm" runat="server" Visible="true">
            <div id="popHeader">
                <div style="text-align: center">
                    <h3>
                        <asp:Label ID="lblpopHead" Text="Custom Type of Food" CssClass="clientHeading" runat="server"></asp:Label>
                    </h3>
                </div>
            </div>
            <asp:Panel runat="server" ID="panelAddNewType" HorizontalAlign="Right" Style="padding: 5px;">
                <asp:LinkButton runat="server" ID="lbDownloadMenu" Text="Download Menu" OnClick="lbDownloadMenu_Click"></asp:LinkButton>
                &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lbDownloadImages" Text="Download Images"
                    OnClick="lbDownloadImages_Click"></asp:LinkButton>
                &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lbImportDealItems" Text="ImportMenu Items"
                    OnClick="lbImportDealItems_Click"></asp:LinkButton>
                &nbsp;&nbsp;<a href="#CustomTypeofFood"><asp:Label ID="lbladdnewDeal" runat="server"
                    Text="Add New Item"></asp:Label>
                </a>
            </asp:Panel>
            <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                <div style="float: left; padding-right: 5px">
                    <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                </div>
                <div class="floatLeft">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                </div>
            </div>
            <div style="clear: both;" align="center">
                <asp:UpdatePanel ID="upItems" runat="server">
                    <ContentTemplate>
                        <asp:DataGrid ID="gridViewMenus" runat="server" Width="100%" AllowSorting="False"
                            AllowPaging="False" AutoGenerateColumns="False" OnItemCommand="gridViewMenus_ItemCommand"
                            OnCancelCommand="gridViewMenus_CancelCommand" OnItemDataBound="gridViewMenus_ItemDataBound"
                            OnItemCreated="gridViewMenus_ItemCreated" ShowFooter="True" ShowHeader="false"
                            GridLines="None">
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="foodTypeId" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <div class="menu_cell4DealItems">
                                            <div class="menucelltable">
                                                <table cellpadding="0" cellspacing="0" width="1000px">
                                                    <tr>
                                                        <td style="padding-top: 12px;" align="left">
                                                            <asp:Label Visible="false" runat="server" ID="lblFoodTypeId" Text='<%# Eval("foodTypeId")%>'></asp:Label>
                                                            <asp:Label Visible="true" runat="server" ForeColor="#2A90C1" Font-Size="18px" ID="lblFoodType"><%# Eval("foodType") %></asp:Label>
                                                            <asp:TextBox Visible="false" Width="250px" runat="server" CssClass="txtForm" ID="txtFoodType"
                                                                Text='<%# Eval("foodType") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="FoodTypeDescriptionRequired" ValidationGroup="Menus"
                                                                ControlToValidate="txtFoodType" ErrorMessage="Menu name required!" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td align="right" class="padding_R7">
                                                            <asp:LinkButton runat="server" ID="lbEdit" Text="Edit" CausesValidation="false" CommandName="ToEdit"></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbUpdate" Text="Update" ValidationGroup="Menus"
                                                                Visible="false" CommandName="ToUpdate"></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbCancel" Text="Cancel" CausesValidation="false"
                                                                Visible="false" CommandName="ToCancel"></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbDelete" Text="Delete" CausesValidation="false"
                                                                CommandName="ToDelete"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" id="divOrders" runat="server" style="display: inline">
                                                            <table cellpadding="0" cellspacing="0" width="900px">
                                                                <td style="vertical-align:top" align="left">
                                                                    &nbsp;&nbsp;<img src='<%# getImagePath(DataBinder.Eval(Container.DataItem,"foodImage")) %>'
                                                                        class="menuImageBorder" alt='' />
                                                                    <div>
                                                                        <a href="javascript:void(0)" id="ImageButton2" style="outline: none; text-decoration: none"
                                                                            onclick='showDelConfirmBox("<%# Eval("foodImage") %>");'>
                                                                            <asp:Label ID="lblChangeImage" runat="server" Text="Change Image"></asp:Label></a>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <table border="0" cellpadding="3" cellspacing="3" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <div>
                                                                                    <asp:DataGrid ID="gridViewItems" runat="server" AllowPaging="False" AllowSorting="False"
                                                                                        AutoGenerateColumns="False" ShowHeader="True" CellPadding="3" CellSpacing="2"
                                                                                        BorderStyle="None" Visible="True" ShowFooter="True" GridLines="None" Width="100%">
                                                                                        <HeaderStyle CssClass="itemname_header" />
                                                                                        <EditItemStyle VerticalAlign="Top" />
                                                                                        <FooterStyle VerticalAlign="Top" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn Visible="False" DataField="menuItemId" HeaderText="ID"></asp:BoundColumn>
                                                                                            <asp:TemplateColumn HeaderText="Item Name">
                                                                                                <FooterTemplate>
                                                                                                    <asp:TextBox ID="txtAddItemName" MaxLength="500" Width="100px" CssClass="txtForm"
                                                                                                        runat="server"></asp:TextBox>
                                                                                                </FooterTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "itemName").ToString()) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtUpdateItemName" runat="server" Width="100px" CssClass="txtForm"
                                                                                                        MaxLength="500" Text='<%# DataBinder.Eval (Container.DataItem, "itemName") %>'>
                                                                                                    </asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="Description">
                                                                                                <FooterTemplate>
                                                                                                    <asp:TextBox ID="txtAddDescription" Width="130px" MaxLength="50" TextMode="MultiLine"
                                                                                                        Height="40px" CssClass="txtForm" runat="server"></asp:TextBox>
                                                                                                </FooterTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblDescription" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "itemDescription").ToString()) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtUpdateItemDescription" runat="server" Width="130px" TextMode="MultiLine"
                                                                                                        Height="40px" CssClass="txtForm" MaxLength="50" Text='<%# DataBinder.Eval (Container.DataItem, "itemDescription") %>'>
                                                                                                    </asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="Item Subname">
                                                                                                <FooterTemplate>
                                                                                                    <asp:TextBox ID="txtAddItemSubName" Width="100px" MaxLength="50" CssClass="txtForm"
                                                                                                        runat="server"></asp:TextBox>
                                                                                                </FooterTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItemSubName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "itemSubname").ToString()) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtUpdateItemSubName" runat="server" Width="100px" CssClass="txtForm"
                                                                                                        MaxLength="50" Text='<%# DataBinder.Eval (Container.DataItem, "itemSubname") %>'
                                                                                                        TextMode="SingleLine">
                                                                                                    </asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn HeaderText="Item Price">
                                                                                                <FooterTemplate>
                                                                                                    <asp:TextBox ID="txtAddItemPrice" Width="80px" MaxLength="8" CssClass="txtForm"
                                                                                                        runat="server"></asp:TextBox>
                                                                                                </FooterTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItemPrice" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "itemPrice").ToString()) %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:TextBox ID="txtUpdateItemPrice" runat="server" Width="80px" CssClass="txtForm"
                                                                                                        MaxLength="8" Text='<%# DataBinder.Eval (Container.DataItem, "itemPrice") %>'
                                                                                                        TextMode="SingleLine">
                                                                                                    </asp:TextBox>
                                                                                                </EditItemTemplate>
                                                                                            </asp:TemplateColumn>                                                                                            
                                                                                            <asp:TemplateColumn HeaderText="Action">
                                                                                                <FooterTemplate>
                                                                                                    <div align="right" style="padding-right: 15px;">
                                                                                                        <asp:Button ID="btnAddMenuItem" Text="Add New" CssClass="btn_orange_smaller" CommandName="AddItem"
                                                                                                            CommandArgument='<%# DataBinder.Eval (Container.DataItem, "foodTypeID") %>' runat="server"
                                                                                                            ImageUrl="~/admin/Images/btnAddNew.jpg"></asp:Button>
                                                                                                    </div>
                                                                                                </FooterTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="Linkbutton4" runat="server" Text="Edit" CausesValidation="false"
                                                                                                        CommandName="Edit">Edit</asp:LinkButton>
                                                                                                    <asp:LinkButton ID="btnDeleteItems" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete?');"
                                                                                                        CommandName="DeleteItems">Delete</asp:LinkButton>
                                                                                                    <asp:LinkButton ID="lnkBtnSubMenuItem" runat="server" Text="SubMenu" PostBackUrl='<%# "~/sub_subitems.aspx?MID="+ Eval("menuItemId") %>'></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <EditItemTemplate>
                                                                                                    <asp:LinkButton ID="lbUpdateItem" runat="server" Text="Update" CommandName="Update">Update</asp:LinkButton>
                                                                                                    <asp:LinkButton ID="Linkbutton6" runat="server" Text="Cancel" CausesValidation="false"
                                                                                                        CommandName="Cancel"></asp:LinkButton>
                                                                                                </EditItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn Visible="false">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblMenuItemID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "menuItemId") %>'>
                                                                                                    </asp:Label>
                                                                                                    <asp:Label ID="lblMenuID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "foodTypeId") %>'>
                                                                                                    </asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                        </Columns>
                                                                                    </asp:DataGrid>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" DataField="foodTypeID" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFoodTypeID1" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "foodTypeID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="height10">
            </div>
            <div style="padding-left: 18px;">
                <div id="element-box1">
                    <div class="t">
                        <div class="t">
                            <div class="t">
                            </div>
                        </div>
                    </div>
                    <div class="m">
                        <div class="m">
                            <h4 class="blue">
                                <asp:Label ID="lblCusTypeOfFood" runat="server" Text="Custom Type of Food"></asp:Label>
                            </h4>
                            <a name="CustomTypeofFood"></a>
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tbody>                                   
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblTypeOfFood" runat="server" Text="Type of Food"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTypeofFood" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvTypeOfFood" runat="server" ControlToValidate="txtTypeofFood"
                                                ErrorMessage="Type of food required" ValidationGroup="food" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceTypeOfFood" TargetControlID="rfvTypeOfFood">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblImage" runat="server" Text="Image"></asp:Label>
                                        </td>
                                        <td colspan="3" align="left">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="fpImage" runat="server" CssClass="txtForm" Width="150px" />
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fpImage"
                                                        ErrorMessage="Image required" Display="None" ValidationGroup="food" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidation"
                                                        ControlToValidate="fpImage" Display="None" ErrorMessage="Invalid file format."
                                                        ValidationGroup="food" SetFocusOnError="True"></cc1:CustomValidator>
                                                    <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                                        runat="server">
                                                    </cc2:ValidatorCalloutExtender>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblItem1" runat="server" Text="Item1:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtItemName" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName"
                                                ErrorMessage="Item name required" Display="None" ValidationGroup="food" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceItemName" TargetControlID="rfvItemName">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblDescription1" runat="server" Text="Description1:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtItemDescription" onkeyup="checkMaxLength(this, 1000)" runat="server"
                                                TextMode="MultiLine" Height="40px" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvItemDescription" runat="server" ControlToValidate="txtItemDescription"
                                                ErrorMessage="Item name required" Display="None" ValidationGroup="food" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceItemDescription" TargetControlID="rfvItemDescription">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Price1" runat="server" Text="Price1:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtItemPrice" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvItemPrice" runat="server" ControlToValidate="txtItemPrice"
                                                ErrorMessage="Item name required" Display="None" ValidationGroup="food" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceItemPrice" TargetControlID="rfvItemPrice">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RangeValidator runat="server" ID="rngItemPrice" ValidationGroup="food" Type="Currency"
                                                ErrorMessage="Price must be in numeric." Display="None" MinimumValue="0" MaximumValue="999999999"
                                                ControlToValidate="txtItemPrice"></cc1:RangeValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="rngItemPrice">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="right" style="padding-right: 32px;">
                                            <%--<asp:ImageButton ID="btnSave" ValidationGroup="food" ImageUrl="~/admin/Images/btnSave.jpg"
                                                runat="server" OnClick="btnSave_Click" />--%>
                                            <asp:Button runat="server" ID="btnSave" CssClass="btn_orange_smaller" ValidationGroup="food"
                                                Text="Save" OnClick="btnSave_Click" /></span>
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
    <Triggers>
        <asp:PostBackTrigger ControlID="lbDownloadMenu" />
        <asp:PostBackTrigger ControlID="lbDownloadImages" />
    </Triggers>
</asp:UpdatePanel>
