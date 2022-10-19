<%@ Page Language="C#" MasterPageFile="~/Contract/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="EditContract.aspx.cs" Inherits="EditContract"
    Title="TastyGo | Contract | Contract Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="JS/jquery-1.4.min.js"></script>
    <script type="text/javascript" language="javascript">

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

        function checkMaxLength(sender, length) {
            if (sender.value.length > length) {
                sender.value = sender.value.substr(0, length);
            }
            return;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upBusinessMgmtForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
            <asp:Panel ID="pnlForm" runat="server">
                <div style="width: 100%;" align="center">
                    <div id="element-box1">
                        <div class="t">
                            <div class="t">
                                <div class="t">
                                </div>
                            </div>
                        </div>
                        <div class="m">
                            <div class="m">
                                <div>
                                    <div id="popHeader">
                                        <div style="float: left">
                                            <asp:HiddenField ID="hfBusinessId" runat="server" />
                                            <asp:Label ID="lblpopHead" Text="Contract Managment" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <table id="tblRestuarant" border="0" cellpadding="3" cellspacing="2" width="720px"
                                        class="fontStyle">
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Image ID="ImgAddError" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:Label ID="lblAddressError" ForeColor="Red" runat="server" Text="" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label2" runat="server" Text="Item Name" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtItemName" runat="server" CssClass="txtForm" MaxLength="50"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtItemName" ErrorMessage="Item Name required!" ValidationGroup="user"
                                                    Display="None">                                                                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator2">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label3" runat="server" Text="Price" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtPrice" runat="server" CssClass="txtForm" MaxLength="50"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrice"
                                                    ErrorMessage="Please Enter Only Numbers" Style="z-index: 101; left: 715px; position: absolute;
                                                    top: 220px" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtPrice" ErrorMessage="Price required!" ValidationGroup="user"
                                                    Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                Length(Inches)
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtLength" runat="server" CssClass="txtForm" MaxLength="50"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLength"
                                                    ErrorMessage="Please Enter Only Numbers" Style="z-index: 101; left: 715px; position: absolute;
                                                    top: 245px" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtLength" ErrorMessage="Length required!" ValidationGroup="user"
                                                    Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator5">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="lblResBusinessName" runat="server" Text="Weight(Pounds)" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtWeight" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWeight"
                                                    ErrorMessage="Please Enter Only Numbers" Style="z-index: 101; left: 715px; position: absolute;
                                                    top: 270px" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtWeight" ErrorMessage="Weight required!" ValidationGroup="user"
                                                    Display="None">                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight" style="vertical-align: top;">
                                                <asp:Label ID="Label14" runat="server" Text="Width(Inches)" />
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtWidth" runat="server" MaxLength="100" CssClass="txtForm"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtWidth"
                                                    ErrorMessage="Please Enter Only Numbers" Style="z-index: 101; left: 715px; position: absolute;
                                                    top: 295px" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtWidth" ErrorMessage="Business Address required!"
                                                    ValidationGroup="user" Display="None">                                                                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="RequiredFieldValidator13">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                Height(Inches)
                                            </td>
                                            <td align="left" class="colLeft">
                                                <asp:TextBox ID="txtHeight" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtHeight"
                                                    ErrorMessage="Please Enter Only Numbers" Style="z-index: 101; left: 715px; position: absolute;
                                                    top: 320px" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtHeight" ErrorMessage="Height required!" ValidationGroup="user"
                                                    Display="None">                            
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                                </cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator9">
                                                </cc2:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="colRight">
                                                <asp:Label ID="Label1" runat="server" Text="Business Contract Image" />
                                            </td>
                                            <td align="left" class="colLeft fontStyle">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="fpBusinessImg" runat="server" CssClass="txtForm" Width="313px" />
                                                        <cc1:RequiredFieldValidator ID="rfvDealImage1" runat="server" ControlToValidate="fpBusinessImg"
                                                            ErrorMessage="Image required" Display="None" ValidationGroup="user" SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender runat="server" ID="vceForBusinessImage" TargetControlID="rfvDealImage1">
                                                        </cc2:ValidatorCalloutExtender>
                                                        <cc1:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ImageValidation"
                                                            ControlToValidate="fpBusinessImg" Display="None" ErrorMessage="Invalid file format."
                                                            ValidationGroup="user" SetFocusOnError="True"></cc1:CustomValidator>
                                                        <cc2:ValidatorCalloutExtender ID="vcefpImage" TargetControlID="CustomValidator1"
                                                            runat="server">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </ContentTemplate>
                                                    <Triggers>
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
                                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/admin/images/btnSave.jpg"
                                                    ToolTip="Edit Contract Info" ValidationGroup="user" OnClick="btnSave_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                                    OnClientClick="javascript:window.close();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
