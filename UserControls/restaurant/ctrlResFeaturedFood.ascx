<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlResFeaturedFood.ascx.cs" Inherits="Takeout_UserControls_restaurant_ctrlResFeaturedFood" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript">

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
        document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabFeaturedFood_ctrlResFeaturedFood1_hidFoodTypeId').value = intID;
        document.getElementById('confirmationBoxBackGround').style.display = 'block';
        document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabFeaturedFood_ctrlResFeaturedFood1_pnlDelete').style.display = 'block';
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
                        runat="server" Text="[ X ]" OnClientClick="document.getElementById('confirmationBoxBackGround').style.display='none'; document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_TabFeaturedFood_ctrlResFeaturedFood1_pnlDelete').style.display='none'; return false;"></asp:LinkButton>
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
                                    <asp:FileUpload ID="fpChangeFoodImage" runat="server" />
                                    <asp:TextBox ID="hidFoodTypeId" runat="server" Style="display: none"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnChange" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="padding-left: 12px;">
                            <asp:Button ID="btnChange" runat="server" Text="Save" CssClass="btn_orange_smaller"
                                ImageUrl="~/admin/Images/btnSave.jpg" CausesValidation="true" ValidationGroup="changeFood" OnClick="btnChange_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 12px;">
                            <cc1:CustomValidator ID="cvfpChange" runat="server" ClientValidationFunction="ImageValidation"
                                ControlToValidate="fpChangeFoodImage" Display="Static" ValidateEmptyText="true" ValidationGroup="changeFood"
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
                        <asp:Label ID="lblpopHead" Text="Featured Food Management" CssClass="clientHeading" runat="server"></asp:Label>
                    </h3>
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
                <asp:UpdatePanel ID="upItems" runat="server">
                    <ContentTemplate>
                        <asp:DataGrid ID="gridViewMenus" runat="server" Width="100%" AllowSorting="False"
                            AllowPaging="False" AutoGenerateColumns="False" CellPadding="4" CellSpacing="2"
                            OnItemCommand="gridViewMenus_ItemCommand" OnCancelCommand="gridViewMenus_CancelCommand"
                            OnItemDataBound="gridViewMenus_ItemDataBound" OnEditCommand="gridViewMenus_EditCommand" ShowHeader="True" ShowFooter="true"
                            GridLines="None" OnUpdateCommand="gridViewMenus_UpdateCommand">
                            <HeaderStyle ForeColor="#2290C1" Width="20%" HorizontalAlign="Center" />
                            <EditItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                            <FooterStyle VerticalAlign="Top" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="featuredFoodId" HeaderText="ID"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Image">
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <td style="vertical-align: top" align="left">
                                                &nbsp;&nbsp;<img src='<%# getImagePath(DataBinder.Eval(Container.DataItem,"foodImage")) %>'
                                                    class="menuImageBorder" alt='' />
                                                <div>
                                                    <a href="javascript:void(0)" id="ImageButton2" style="outline: none; text-decoration: none"
                                                        onclick='showDelConfirmBox("<%# Eval("foodImage") %>");'>
                                                        <asp:Label ID="lblChangeImage" runat="server" Font-Size="12px" Text="Change Image"></asp:Label></a>
                                                </div>
                                            </td>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Food Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "foodName").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUpdateFoodName" runat="server" Width="141px" CssClass="txtForm"
                                            MaxLength="200" Text='<%# DataBinder.Eval (Container.DataItem, "foodName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "foodDescription").ToString()) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUpdateDescription" runat="server" Width="212px" TextMode="MultiLine"
                                            Height="40px" CssClass="txtForm" MaxLength="1000" Text='<%# DataBinder.Eval (Container.DataItem, "foodDescription") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSubName" CssClass="fontStyle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "foodPrice").ToString()) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUpdateFoodPrice" runat="server" Width="100px" CssClass="txtForm"
                                            MaxLength="10" Text='<%# DataBinder.Eval (Container.DataItem, "foodPrice") %>'
                                            TextMode="SingleLine">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Linkbutton4" runat="server" Text="Edit" CausesValidation="false"
                                            CommandName="Edit">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="btnDeleteItems" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete?');"
                                            CommandName="DeleteItems">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lbUpdateItem" runat="server" Text="Update" CommandName="Update">Update</asp:LinkButton>
                                        <asp:LinkButton ID="Linkbutton6" runat="server" Text="Cancel" CausesValidation="false"
                                            CommandName="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFeaturedFoodID" runat="server" Text='<%# DataBinder.Eval (Container.DataItem, "featuredFoodId") %>'>
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
                                <asp:Label ID="lblCusTypeOfFood" runat="server" ForeColor="#2290C1" Font-Names="Tahoma" Font-Size="13px" Text="Add New Featured Food"></asp:Label>
                            </h4>
                            <a name="CustomTypeofFood"></a>
                            <table cellpadding="2" cellspacing="2" width="100%" class="fontStyle" border="0">
                                <tbody>
                                    <tr id="trSelectRes" runat="server" visible="false">
                                        <td align="right">
                                            <asp:Label ID="Label1" runat="server" Text="Select Restaurant"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <asp:DropDownList ID="ddlSelectRes" runat="server">
                                            </asp:DropDownList>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSelectRes"
                                                ErrorMessage="Type of food required" ValidationGroup="food" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="rfvTypeOfFood">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblTypeOfFood" runat="server" Text="Food Name"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFoodName" runat="server" CssClass="txtForm" Width="150px" MaxLength="200"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvTypeOfFood" runat="server" ControlToValidate="txtFoodName"
                                                ErrorMessage="Food Name required" ValidationGroup="FeaturedFood" Display="None" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceTypeOfFood" TargetControlID="rfvTypeOfFood">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFoodDescription" onkeyup="checkMaxLength(this, 1000)" runat="server"
                                                TextMode="MultiLine" Height="30px" CssClass="txtForm" Width="241px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvItemDescription" runat="server" ControlToValidate="txtFoodDescription"
                                                ErrorMessage="Description required" Display="None" ValidationGroup="FeaturedFood" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceItemDescription" TargetControlID="rfvItemDescription">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFoodPrice" runat="server" MaxLength="10" CssClass="txtForm" Width="150px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvItemPrice" runat="server" ControlToValidate="txtFoodPrice"
                                                ErrorMessage="Price required" Display="None" ValidationGroup="FeaturedFood" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="vceItemPrice" TargetControlID="rfvItemPrice">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RangeValidator runat="server" ID="rngItemPrice" ValidationGroup="FeaturedFood" Type="Currency"
                                                ErrorMessage="Price must be in numeric." Display="None" MinimumValue="0" MaximumValue="999999999"
                                                ControlToValidate="txtFoodPrice"></cc1:RangeValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="rngItemPrice">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblImage" runat="server" Text="Image"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="fpFoodImage" runat="server" CssClass="txtForm" Width="241px" />
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fpFoodImage"
                                                        ErrorMessage="Image required" Display="None" ValidationGroup="FeaturedFood" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidation"
                                                        ControlToValidate="fpFoodImage" Display="None" ErrorMessage="Invalid file format."
                                                        ValidationGroup="FeaturedFood" SetFocusOnError="True"></cc1:CustomValidator>
                                                    <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                                        runat="server">
                                                    </cc2:ValidatorCalloutExtender>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSave" />
                                                    <asp:PostBackTrigger ControlID="btnImgSave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btnSave" CssClass="btn_orange_smaller" CausesValidation="true"
                                                ValidationGroup="FeaturedFood" Text="Save" OnClick="btnSave_Click" />
                                            <asp:ImageButton ID="btnImgSave" runat="server" ValidationGroup="FeaturedFood" Visible="false"
                                                ImageUrl="~/admin/images/btnSave.jpg" OnClick="btnImgSave_Click" />                                            
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