<%@ Page Language="C#" MasterPageFile="~/shipper/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="ResendOrdersManagement.aspx.cs" Inherits="ResendOrdersManagement" Title="TastyGo | Shipper | Resend Orders Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">

        function clearFields() {            
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchCustomerName').value = '';
            return false;
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
            if (document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked) {
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

        function ChangeHeaderAsNeeded() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (!document.getElementById(list[i]).checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = false;
                    return;
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = true;
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

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfProvinceID" runat="server" Value="0" />
            <asp:HiddenField ID="hfTaxID" runat="server" Value="" />
            <asp:Panel ID="pnlForm" runat="server">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Resend Orders Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr id="trErrorMsg" runat="server" visible="false">
                                <td>
                                </td>
                                <td align="right" class="colRight">
                                    <div class="clear" style="padding-top: 15px; padding-bottom: 20px; width: 100%" align="center">
                                        <div style="float: left; padding-right: 5px">
                                            <asp:Image ID="Image1" runat="server" Visible="true" ImageUrl="~/admin/images/error.png" />
                                        </div>
                                        <div class="floatLeft">
                                            <asp:Label ID="lblErrorMsg" runat="server" Text="City with this name already exists"
                                                ForeColor="Red" CssClass="fontStyle"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label2" runat="server" Text="Customer Name" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:HiddenField ID="hfresendOrderID" runat="server" Value="0" />
                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvCustomerName" runat="server" ControlToValidate="txtCustomerName"
                                        Display="None" ErrorMessage="<span id='cMessage'>Customer Name required!.</span>"
                                        ValidationGroup="order" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="rfvCustomerName"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblVoucherNumber" runat="server" Text="Voucher Number" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtVoucherNumber" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvVoucherNumber" runat="server" ControlToValidate="txtVoucherNumber"
                                        Display="None" ErrorMessage="<span id='cMessage'>Voucher Number required!.</span>"
                                        ValidationGroup="order" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rfvVoucherNumber"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblTelephone" runat="server" Text="Telephone" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtTelephone" runat="server" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvTelephone" runat="server" ControlToValidate="txtTelephone"
                                        Display="None" ErrorMessage="<span id='cMessage'>Telephone required!.</span>"
                                        ValidationGroup="order" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceCity" TargetControlID="rfvTelephone" runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblTrackingNumber" runat="server" Text="Tracking Number" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtTrackingNumber" runat="server" CssClass="txtForm"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="50px" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                        Display="None" ErrorMessage="<span id='cMessage'>Address required!.</span>" ValidationGroup="order"
                                        SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvAddress"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="lblNote" runat="server" Text="Note" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="50px" CssClass="txtForm"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvNote" runat="server" ControlToValidate="txtNote"
                                        Display="None" ErrorMessage="<span id='cMessage'>Note required!.</span>" ValidationGroup="order"
                                        SetFocusOnError="True"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="rfvNote"
                                        runat="server">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="Image" />
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="fuImage" runat="server" CssClass="txtForm" Width="313px" />                                          
                                            <cc1:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ImageValidation"
                                                ControlToValidate="fuImage" Display="None" ErrorMessage="Invalid file format."
                                                ValidationGroup="order" SetFocusOnError="True"></cc1:CustomValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" TargetControlID="CustomValidator2"
                                                runat="server">
                                            </cc2:ValidatorCalloutExtender>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSave" />
                                            <asp:PostBackTrigger ControlID="btnUpdate" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <img id="imglogo" runat="server" src="" class="menuImageBorder" alt="" visible="false" />
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
                                                    <div style="float: right; padding-right: 20px;">
                                                        <asp:Label ID="lblCommentMessage" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div style="width: 63px; float: right;">
                                                </div>
                                            </div>
                                        </div>
                                        <asp:DataList ID="rptrDiscussion" RepeatColumns="1" RepeatDirection="Vertical" DataKeyField="resendOrdercommentId"
                                            runat="server" CellPadding="0" OnItemDataBound="DataListItemDataBound" CellSpacing="0"
                                            Width="781px" GridLines="None" ShowHeader="false">
                                            <ItemTemplate>
                                                <div style="border-bottom: solid 1px #B7B7B7; background-color: #FFFFFF; width: auto;
                                                    padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                                                    <div style="width: 141px; float: left; text-align: center">
                                                        <asp:Label ID="lblresendOrdercommentId" runat="server" Visible="false" Text='<%# Eval("resendOrdercommentId")%>'></asp:Label>
                                                        <asp:Image ID="imgDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px" BorderStyle="Solid"
                                                            ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                        <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("resendOrderID")%>' />
                                                    </div>
                                                    <div style="width: 640px; float: right; text-align: left;">
                                                        <div style="width: 640px; height: 26px;">
                                                            <div style="float: left; width: 550px; padding-right: 10px;">
                                                                <asp:Label ID="label5" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("Name") %>'
                                                                    Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                        ID="label6" runat="server" Font-Names="Arial, Arial, sans-serif" Text='<%# Eval("cmtDatetime")%>'
                                                                        Font-Bold="True" Font-Size="13px" ForeColor="#97C717"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div style="width: 628px; padding-right: 12px;">
                                                            <asp:Label ID="label7" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("comment")%>'
                                                                Font-Size="13px" ToolTip='<%# Eval("comment")%>' ForeColor="#7C7B7B"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="colRight">
                                </td>
                                <td align="left" class="colLeft">
                                    <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="order" ImageUrl="~/admin/Images/btnSave.jpg"
                                        Visible="True" OnClick="btnSave_Click" />
                                    <asp:ImageButton ID="btnUpdate" runat="server" ValidationGroup="order" ImageUrl="~/admin/images/btnUpdate.jpg"
                                        OnClick="btnUpdate_Click" Visible="false" />&nbsp;
                                    <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                        OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblProvinceNameSearch" runat="server" Text="Customer Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchCustomerName" runat="server" CssClass="txtSearch">
                        </asp:TextBox>
                    </div>
                    <div>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                            OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                TabIndex="2" />
                    </div>
                </div>
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div class="searchButtons">
                    <div class="floatLeft">
                        <asp:ImageButton ID="btnShowAll" runat="server" ImageUrl="~/admin/images/btnShowAll.gif"
                            OnClick="btnShowAll_Click" />&nbsp;
                    </div>
                    <div class="floatLeft">
                        <asp:ImageButton ID="btnDeleteSelected" runat="server" ImageUrl="~/admin/images/btnDeleteSelected.jpg"
                            OnClientClick="return preCheckSelected();" OnClick="btnDeleteSelected_Click" />
                    </div>
                    <div class="floatLeft">
                        &nbsp;<asp:ImageButton ID="btnAddNew" runat="server" ImageUrl="~/admin/images/btnAddNew.jpg"
                            OnClick="btnAddNew_Click" />
                    </div>
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="resendOrderID" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting"
                                AllowSorting="True" OnSorting="pageGrid_Sorting" OnSelectedIndexChanged="pageGrid_SelectedIndexChanged"
                                OnRowEditing="pageGrid_RowEditing">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("resendOrderID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("resendOrderID") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblresendOrder_Image" ForeColor="White" runat="server" Text="Image"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <a href='<%# "../Images/resendOrder/"+ Eval("resendOrder_Image") %>' target="_blank">
                                                    <img id="imgOrderImage" width="50px" src='<%# "../Images/resendOrder/"+ Eval("resendOrder_Image") %>' />
                                                </a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblresendOrder_CustomerNameHeading" ForeColor="White" runat="server"
                                                Text="Customer Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblresendOrder_CustomerName" Text='<% # Eval("resendOrder_CustomerName") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblresendOrder_VoucherNumberHeading" ForeColor="White" runat="server"
                                                Text="Voucher Number"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblresendOrder_VoucherNumber" Text='<% # Eval("resendOrder_VoucherNumber") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblresendOrder_TelephoneHeading" ForeColor="White" runat="server"
                                                Text="Telephone"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblresendOrder_Telephone" Text='<% # Eval("resendOrder_Telephone") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblresendOrder_AddressHeading" ForeColor="White" runat="server" Text="Address"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblresendOrder_Address" Text='<% # Eval("resendOrder_Address") %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblresendOrder_NoteHeading" ForeColor="White" runat="server" Text="Note"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblresendOrder_Note" Text='<% # Eval("resendOrder_Note") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ImageUrl="~/admin/Images/edit.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                ImageUrl="~/admin/Images/delete.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="12%" HeaderText="Track #" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <div>
                                                <asp:TextBox ID="txtTrackNumber" runat="server" Width="100px" Style="border: 1px solid #666666;"
                                                    MaxLength="20" Text='<%# Eval("resendOrder_trackingNumber") %>'></asp:TextBox>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/admin/Images/successfulorders.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No data to display" runat="server"></asp:Label>
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
                                                            <asp:Label ID="lblRecordsPerPage" runat="server" Text="Records per page"></asp:Label>
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
                                                                            Style="padding-left: 12px;" Width="20px" runat="server" Text="1"></asp:TextBox>
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
