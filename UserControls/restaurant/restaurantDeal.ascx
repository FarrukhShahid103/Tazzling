<%@ Control Language="C#" AutoEventWireup="true" CodeFile="restaurantDeal.ascx.cs"
    Inherits="Takeout_UserControls_restaurant_restaurantDeal" %>
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

    function ValidateFields(txtItemName, txtItemDesc, txtItemSubName) {

        var itemName = document.getElementById(txtItemName);
        var itemDesc = document.getElementById(txtItemDesc);
        var itemSubName = document.getElementById(txtItemSubName);


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

    function showDelConfirmBoxDeal(intID) {
        document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabMenuDeal_restaurantDeal1_hidFoodTypeId').value = intID;
        document.getElementById('confirmationBoxBackGroundDeal').style.display = 'block';
        document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabMenuDeal_restaurantDeal1_pnlDeleteDeal').style.display = 'block';
        return false;
    }

    function ImageValidationDeal(oSrc, args) {

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
</script>

<asp:UpdatePanel ID="upMain" runat="server">
    <ContentTemplate>
        <div id="confirmationBoxBackGroundDeal">
            <div id="confirmBoxDeal">
            </div>
        </div>
        <asp:Panel ID="pnlDeleteDeal" runat="server" CssClass="confirmationStyle">
            <div class="headingStripConfirm">
                <div class="floatLeft">
                    <asp:Label ID="lblConfirmationBox" runat="server" Text="Tasty Go"></asp:Label></div>
                <div class="floatRight" style="padding-right: 4px;">
                    <asp:LinkButton ID="lbClose" ForeColor="White" Style="text-decoration: none; outline: none"
                        runat="server" Text="[ X ]" OnClientClick="document.getElementById('confirmationBoxBackGroundDeal').style.display='none'; document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabMenuDeal_restaurantDeal1_pnlDeleteDeal').style.display='none'; return false;"></asp:LinkButton>
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
                            <asp:Button ID="btnChange" runat="server" ValidationGroup="changeDeal" Text="Save"
                                CssClass="btn_orange_smaller" ImageUrl="~/admin/Images/btnSave.jpg" OnClick="btnChange_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 12px;">
                            <cc1:CustomValidator ID="cvfpChange" runat="server" ClientValidationFunction="ImageValidationDeal"
                                ControlToValidate="fpChange" Display="Static" ValidateEmptyText="true" ValidationGroup="changeDeal"
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
                        <asp:Label ID="lblpopHead" Text="Custom Type of Food" runat="server"></asp:Label>
                    </h3>
                </div>
            </div>
            <asp:Panel runat="server" ID="panelAddNewType" HorizontalAlign="Right" Style="padding: 5px;">
                <asp:LinkButton runat="server" ID="lbDownloadMenu" Text="Download Menu" OnClick="lbDownloadMenu_Click"></asp:LinkButton>
                &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lbDownloadImages" Text="Download Images"
                    OnClick="lbDownloadImages_Click"></asp:LinkButton>
                &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lbImportDealItems" Text="Import Deal Items"
                    OnClick="lbImportDealItems_Click"></asp:LinkButton>
                &nbsp;&nbsp;<a href="#CustomTypeofFood"><asp:Label ID="lbladdnewDeal" runat="server"
                    Text="Add New Deal"></asp:Label>
                </a>
            </asp:Panel>
            <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                <div style="float: left; padding-right: 5px">
                    <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/images/error.png" />
                </div>
                <div class="floatLeft">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                </div>
            </div>
            <div class="floatLeft">
                <asp:DataGrid ID="gridViewMenus" runat="server" Width="100%" AllowSorting="False"
                    AllowPaging="False" AutoGenerateColumns="False" OnItemCommand="gridViewMenus_ItemCommand"
                    OnCancelCommand="gridViewMenus_CancelCommand" OnItemDataBound="gridViewMenus_ItemDataBound"
                    OnItemCreated="gridViewMenus_ItemCreated" ShowFooter="True" ShowHeader="false"
                    GridLines="None" CssClass="gridView_Menu" >
                    <Columns>
                        <asp:BoundColumn Visible="False" DataField="dealID" HeaderText="ID"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <div class="menu_cell4DealItems">
                                    <div class="menucelltable">
                                        <table cellpadding="0" cellspacing="0" width="1000px">
                                            <tr>
                                                <td style="padding-top: 12px;" align="left">
                                                    <asp:Label Visible="false" runat="server" ID="lblFoodTypeId" Text='<%# Eval("dealID")%>'></asp:Label>
                                                    <asp:Label Visible="true" runat="server" ForeColor="#2A90C1" Font-Size="18px" ID="lblFoodType"><%# Eval("dealName") %></asp:Label>
                                                    <asp:TextBox Visible="false" Width="200px" runat="server" CssClass="txtForm" ID="txtdealName"
                                                        Text='<%# Eval("dealName") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="FoodTypeDescriptionRequired" ValidationGroup="Menus"
                                                        ControlToValidate="txtdealName" ErrorMessage="Deal name required!" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                                <td style="padding-top: 12px;" align="left" colspan="2">
                                                    <asp:Label Visible="true" runat="server" ForeColor="#2A90C1" Font-Size="18px" ID="lblPriceLabel"
                                                        Text="Price:"></asp:Label>
                                                    <asp:Label Visible="true" runat="server" ForeColor="#2A90C1" Font-Size="18px" ID="lblDealPrice"><%# Eval("dealPrice")%></asp:Label>
                                                    <asp:TextBox Visible="false" Width="100px" runat="server" CssClass="txtForm" ID="txtDealPrice"
                                                        Text='<%# Eval("dealPrice") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ValidationGroup="Menus"
                                                        ControlToValidate="txtDealPrice" ErrorMessage="Price required!" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator runat="server" ID="reDealPrice" ValidationGroup="Menus" ControlToValidate="txtDealPrice"
                                                        Type="Currency" ErrorMessage="Price must be in numeric." MinimumValue="0" MaximumValue="999999999"
                                                        Display="Dynamic"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-top: 12px;" align="left" colspan="2">
                                                    <asp:Label Visible="true" runat="server" ForeColor="#2A90C1" Font-Size="18px" ID="Label3"
                                                        Text="Qty:"></asp:Label>
                                                    <asp:Label Visible="true" runat="server" ForeColor="#2A90C1" Font-Size="18px" ID="lblDealQty"><%# Eval("dealOrderItemsQty")%></asp:Label>
                                                    <asp:TextBox Visible="false" Width="100px" runat="server" CssClass="txtForm" ID="txtDealQty"
                                                        Text='<%# Eval("dealOrderItemsQty") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ValidationGroup="Menus"
                                                        ControlToValidate="txtDealQty" ErrorMessage="Qty. required!" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator runat="server" ID="RangeValidator3" ValidationGroup="Menus" ControlToValidate="txtDealQty"
                                                        Type="Integer" ErrorMessage="Qty. must be in numeric." MinimumValue="0" MaximumValue="999999999"
                                                        Display="Dynamic"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" id="divOrders" runat="server" style="display: inline">
                                                    <table cellpadding="0" cellspacing="0" width="900px">
                                                        <td class="foodimage" align="left" valign="top">
                                                            &nbsp;&nbsp;<img src='<%# getImagePath(DataBinder.Eval(Container.DataItem,"dealImage")) %>'
                                                                class="menuImageBorder" alt='' />
                                                            <div>
                                                                <a href="javascript:void(0)" id="ImageButton2" style="outline: none; text-decoration: none"
                                                                    onclick='showDelConfirmBoxDeal("<%# Eval("dealImage") %>");'>
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
                                                                                <HeaderStyle CssClass="gridSubHeading" />
                                                                                <EditItemStyle VerticalAlign="Top" />
                                                                                <FooterStyle VerticalAlign="Top" />
                                                                                <Columns>
                                                                                    <asp:BoundColumn Visible="False" DataField="dealItemId" HeaderText="ID"></asp:BoundColumn>
                                                                                    <asp:TemplateColumn HeaderText="Item Name">
                                                                                        <FooterTemplate>
                                                                                            <asp:TextBox ID="txtAddItemName" MaxLength="500" Width="150px" CssClass="txtForm"
                                                                                                runat="server"></asp:TextBox>
                                                                                        </FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealItemName").ToString()) %>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtUpdateItemName" runat="server" Width="150px" CssClass="txtForm"
                                                                                                MaxLength="500" Text='<%# DataBinder.Eval (Container.DataItem, "dealItemName") %>'>
                                                                                            </asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Description">
                                                                                        <FooterTemplate>
                                                                                            <asp:TextBox ID="txtAddDescription" Width="150px" MaxLength="50" TextMode="MultiLine"
                                                                                                Height="40px" CssClass="txtForm" runat="server"></asp:TextBox>
                                                                                        </FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDescription" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealItemDescription").ToString()) %>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtUpdateItemDescription" runat="server" Width="150px" TextMode="MultiLine"
                                                                                                Height="40px" CssClass="txtForm" MaxLength="50" Text='<%# DataBinder.Eval (Container.DataItem, "dealItemDescription") %>'>
                                                                                            </asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Item Subname">
                                                                                        <FooterTemplate>
                                                                                            <asp:TextBox ID="txtAddItemSubName" Width="120px" MaxLength="50" CssClass="txtForm"
                                                                                                runat="server"></asp:TextBox>
                                                                                        </FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblItemSubName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "dealItemSubname").ToString()) %>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:TextBox ID="txtUpdateItemSubName" runat="server" Width="120px" CssClass="txtForm"
                                                                                                MaxLength="50" Text='<%# DataBinder.Eval (Container.DataItem, "dealItemSubname") %>'
                                                                                                TextMode="SingleLine">
                                                                                            </asp:TextBox>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="Action">
                                                                                        <FooterTemplate>
                                                                                            <asp:Button ID="btnAddMenuItem" CssClass="btn_orange_smaller" Text="Add New" CommandName="AddItem"
                                                                                                CommandArgument='<%# DataBinder.Eval (Container.DataItem, "dealID") %>' runat="server">
                                                                                            </asp:Button>
                                                                                        </FooterTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="Linkbutton4" runat="server" Text="Edit" CausesValidation="false"
                                                                                                CommandName="Edit">Edit</asp:LinkButton>
                                                                                            <asp:LinkButton ID="btnDeleteItems" runat="server" CausesValidation="False" CommandName="DeleteItems">Delete</asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:LinkButton ID="lbUpdateItem" runat="server" Text="Update" CommandName="Update">Update</asp:LinkButton>
                                                                                            <asp:LinkButton ID="Linkbutton6" runat="server" Text="Cancel" CausesValidation="false"
                                                                                                CommandName="Cancel"></asp:LinkButton>
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbldealItemId" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "dealItemId") %>'>
                                                                                            </asp:Label>
                                                                                            <asp:Label ID="lblDealID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "dealID") %>'>
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
                        <asp:BoundColumn Visible="False" DataField="dealID" HeaderText="ID"></asp:BoundColumn>
                        <asp:TemplateColumn Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblFoodTypeID1" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "dealID") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
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
                        <div class="m" style="">
                            <h4 class="blue">
                                <asp:Label ID="lblCusTypeOfFood" runat="server" Text="Custom Type of Food"></asp:Label>
                            </h4>
                            <a name="CustomTypeofFood"></a>
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblTypeOfFood" runat="server" Text="Deal Name"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTypeofFood" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvTypeOfFood" runat="server" ControlToValidate="txtTypeofFood"
                                                ErrorMessage="Deal name required" ValidationGroup="deal" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceTypeOfFood" TargetControlID="rfvTypeOfFood">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblImage" runat="server" Text="Image"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="imgUpload" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="fpImage" runat="server" CssClass="txtForm" Width="150px" />
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fpImage"
                                                        ErrorMessage="Image required" Display="None" ValidationGroup="deal" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidationDeal"
                                                        ControlToValidate="fpImage" Display="None" ErrorMessage="Invalid file format."
                                                        ValidationGroup="deal" SetFocusOnError="True"></cc1:CustomValidator>
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
                                            <asp:Label ID="Label1" runat="server" Text="Order Items Qty."></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtForm" Width="70px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity"
                                                ErrorMessage="Order Qty. required" ValidationGroup="deal" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RangeValidator runat="server" ID="RangeValidator2" ValidationGroup="deal" Type="Integer"
                                                ErrorMessage="Qty. must be in numeric." Display="None" MinimumValue="0" MaximumValue="999999999"
                                                ControlToValidate="txtQuantity"></cc1:RangeValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator2">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label2" runat="server" Text="Deal Price."></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDealPrice" runat="server" CssClass="txtForm" Width="70px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDealPrice"
                                                ErrorMessage="Price required" ValidationGroup="deal" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RangeValidator runat="server" ID="RangeValidator1" ValidationGroup="deal" Type="Currency"
                                                ErrorMessage="Price must be in numeric." Display="None" MinimumValue="0" MaximumValue="999999999"
                                                ControlToValidate="txtDealPrice"></cc1:RangeValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblItem1" runat="server" Text="Item1:"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtItemName" runat="server" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName"
                                                ErrorMessage="Item name required" Display="None" ValidationGroup="deal" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceItemName" TargetControlID="rfvItemName">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblDescription1" runat="server" Text="Description1:"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtItemDescription" onkeyup="checkMaxLength(this, 1000)" runat="server"
                                                TextMode="MultiLine" Height="40px" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvItemDescription" runat="server" ControlToValidate="txtItemDescription"
                                                ErrorMessage="Item name required" Display="None" ValidationGroup="deal" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceItemDescription" TargetControlID="rfvItemDescription">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <%--<asp:ImageButton ID="btnSave" ValidationGroup="deal" ImageUrl="~/admin/Images/btnSave.jpg"
                                                runat="server" OnClick="btnSave_Click" />--%>
                                            <asp:Button runat="server" ID="btnSave" CssClass="btn_orange_smaller" ValidationGroup="deal"
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
