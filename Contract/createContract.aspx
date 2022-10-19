<%@ Page Title="Create Contract" Language="C#" MasterPageFile="~/Contract/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="createContract.aspx.cs" Inherits="Contract_createContract" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script type="text/javascript">
         $(document).ready(function () {
             var HerfID = 'ctl00_ContentPlaceHolder1_pageGrid_ctl02_HPImage';
             $('a#' + HerfID).fancybox({ 'overlayShow': false, 'transitionIn': 'elastic', 'transitionOut': 'elastic' });
         });
       </script>  --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JS/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="JS/jquery-ui-1.7.2.min.js" type="text/javascript"></script>
    <script src="JS/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.easing.1.3.js" type="text/javascript"></script>
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
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
            <asp:HiddenField ID="hfcontractID" runat="server" Value="0" />
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
                                        <asp:Label ID="lblpopHead" Text="Create Contract" runat="server"></asp:Label>
                                        <asp:Label ID="lbleditContract" Visible="false" Text="Edit Contract" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <table id="tblRestuarant" border="0" cellpadding="3" cellspacing="2" width="720px"
                                    class="fontStyle">
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Image ID="ImgAddError" runat="server" Visible="false" ImageUrl="~/Contract/images/error.png" />
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
                                            <asp:TextBox ID="txtItem" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtItem" ErrorMessage="Item Name required!" ValidationGroup="user"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator2">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label3" runat="server" Text="Price" />
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtPrice" runat="server" ValidationGroup="check" CssClass="txtForm"
                                                MaxLength="50"></asp:TextBox>
                                            <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrice"
                                                SetFocusOnError="true" Display="None" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?"
                                                ErrorMessage="Enter Correct Formate" ValidationGroup="user"></cc1:RegularExpressionValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RegularExpressionValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtPrice" ErrorMessage="Price required!" ValidationGroup="user"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label8" runat="server" Text="Weight(Pounds)" />
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtWeight" runat="server" CssClass="txtForm" MaxLength="50"></asp:TextBox>
                                            <cc1:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtWeight"
                                                SetFocusOnError="true" Display="None" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?"
                                                ErrorMessage="Enter Correct Formate" ValidationGroup="user"></cc1:RegularExpressionValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RegularExpressionValidator2">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtWeight" ErrorMessage="Weight required!" ValidationGroup="user"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator5">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="lblResBusinessName" runat="server" Text="Width(Inches)" />
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtWidth" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                            <cc1:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWidth"
                                                SetFocusOnError="true" Display="None" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?"
                                                ErrorMessage="Enter Correct Formate" ValidationGroup="user"></cc1:RegularExpressionValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RegularExpressionValidator3">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtWidth" ErrorMessage="Width required!" ValidationGroup="user"
                                                Display="None">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight" style="vertical-align: top;">
                                            <asp:Label ID="Label14" runat="server" Text="Height(Inches)" />
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtHeight" runat="server" MaxLength="100" CssClass="txtForm"></asp:TextBox>
                                            <cc1:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtHeight"
                                                SetFocusOnError="true" Display="None" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?"
                                                ErrorMessage="Enter Correct Formate" ValidationGroup="user"></cc1:RegularExpressionValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RegularExpressionValidator4">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                                runat="server" ControlToValidate="txtHeight" ErrorMessage="Height required!"
                                                ValidationGroup="user" Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="RequiredFieldValidator13">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label9" runat="server" Text="Length(Inches)" />
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:TextBox ID="txtLength" runat="server" CssClass="txtForm" MaxLength="100"></asp:TextBox>
                                            <cc1:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtLength"
                                                SetFocusOnError="true" Display="None" ValidationExpression="[+-]?((\d+(\.\d*)?)|\.\d+)([eE][+-]?[0-9]+)?"
                                                ErrorMessage="Enter Correct Formate" ValidationGroup="user"></cc1:RegularExpressionValidator>
                                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6" TargetControlID="RegularExpressionValidator5">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtLength" ErrorMessage="Length required!" ValidationGroup="user"
                                                Display="None">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator9">
                                            </cc2:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                            <asp:Label ID="Label1" runat="server" Text="Business Contract Image" />
                                        </td>
                                        <td align="left" class="colLeft fontStyle">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                                    <asp:PostBackTrigger ControlID="btnUpdate" />
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
                                            <asp:ImageButton ID="btnSave" ValidationGroup="user" runat="server" ImageUrl="~/Contract/images/btnSave.jpg"
                                                OnClick="btnSave_Click" ToolTip="Add New Business Info" />&nbsp;
                                            <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/Contract/Images/btnConfirmCancel.gif"
                                                OnClientClick="javascript:window.close();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="colRight">
                                        </td>
                                        <td align="left" class="colLeft">
                                            <asp:ImageButton ID="btnUpdate" Visible="false" runat="server" ImageUrl="~/Contract/images/btnUpdate.jpg"
                                                ToolTip="Edit Contract Info" ValidationGroup="user" OnClick="btnUpdate_Click" />
                                            &nbsp;
                                            <asp:ImageButton ID="btncanclupdate" Visible="false" runat="server" ImageUrl="~/Contract/Images/btnConfirmCancel.gif"
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px;">
                <asp:TextBox ID="hiddenIds" Style="display: none" runat="server"> </asp:TextBox>
                <asp:GridView ID="pageGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" DataKeyNames="contractId"  OnPageIndexChanging="pageGrid_PageIndexChanging"

                    AllowPaging="True" OnRowDataBound="pageGrid_RowDataBound" GridLines="None" AllowSorting="true">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Contract ID"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="contractId" runat="server" Text='<% # Eval("contractId") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Item Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="itemName" runat="server" Text='<%  # Eval("itemName") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="View Image"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:HyperLink ID="HPImage" runat="server" NavigateUrl='<% # "~/Images/createContract/" + Eval("Image") %>'
                                        Target="_blank">
                                        <asp:Image ID="imgBannerImage" runat="server" Height="26px" Width="40px" ImageUrl='<% # "~/Images/createContract/" + Eval("Image") %>' /></asp:HyperLink>
                                    <asp:Literal ID="LTRLScript" runat="server"></asp:Literal>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Price"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="Price" runat="server" Text='<% # Eval("Price") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Weight</br> (pounds)"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="weight" runat="server" Text='<% # Eval("weight") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Width </br>(Inches)"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="width" runat="server" Text='<% #Eval("width") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Length </br>(Inches)"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="length" runat="server" Text='<% # Eval("length") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblRestaurantNameHeadFName" ForeColor="White" runat="server" Text="Height </br>(Inches)"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="height" runat="server" Text='<% # Eval("height") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit Contract" Visible="true" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%-- <asp:HyperLink ID="imgBtnEditContract" runat="server" ToolTip="Edit Contract" CausesValidation="false"
                            ImageUrl="~/admin/Images/edit.gif" Target="_blank" NavigateUrl='<%# "~/contract/EditContract.aspx?contractId=" + Eval("contractId")+ "&resid="+Eval("restaurantId") %>' />--%>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%#Eval("contractId") %>'
                                    CommandName="Select" ImageUrl="~/Contract/Images/edit.gif" />
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
                                                                CommandArgument="Prev" runat="server" ImageUrl="~/Contract/images/imgPrev.jpg" />
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
                                                                runat="server" ImageUrl="~/Contract/images/imgNext.jpg" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
