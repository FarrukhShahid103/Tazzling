<%@ Page Title="TastyGo | Import Menu Items" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="importMenuItems.aspx.cs" Inherits="importMenuItems" %>

<%@ Register Src="~/UserControls/Templates/FrameStart.ascx" TagPrefix="RedSignal"
    TagName="FrameStart" %>
<%@ Register Src="~/UserControls/Templates/FrameEnd.ascx" TagPrefix="RedSignal"
    TagName="FrameEnd" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function ExcelValidation(oSrc, args) {
            var strFUImage = document.getElementById("ctl00_ContentPlaceHolder1_ImportDealItems1_fuExcelFile").value;
            var strFUImageArray = strFUImage.split(".");
            var strFUImageExt = strFUImageArray[1];
            if (strFUImageExt == "xls" || strFUImageExt == "XLS" || strFUImageExt == "xlsx" || strFUImageExt == "XLSX") {
                args.IsValid = true;
                return;
            }
            else {
                args.IsValid = false;
                return;
            }

            //alert(strFUImageExt);
        }
        function ZipValidation(oSrc, args) {
            var strFUImage = document.getElementById("ctl00_ContentPlaceHolder1_ImportDealItems1_fuImageZip").value;
            var strFUImageArray = strFUImage.split(".");
            var strFUImageExt = strFUImageArray[1];
            if (strFUImageExt == "zip" || strFUImageExt == "ZIP") {
                args.IsValid = true;
                return;
            }
            else {
                args.IsValid = false;
                return;
            }

            //alert(strFUImageExt);
        }                              
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3 style="text-align: center">
        <asp:Label ID="lblDealHeader" runat="server" Text="Import Menu"></asp:Label></h3>
    <div class="height10">
    </div>
    <div class="text_right">
        <a href="../ImportTemplates/MenuTemplate2003.xls" target="_blank">
            <asp:Label ID="lblTemplateFile" runat="server" Text="MenuTemplate2003.xls"></asp:Label></a>
        <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
            <div style="float: left; padding-right: 5px">
                <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
            </div>
            <div class="floatLeft">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
            </div>
        </div>
    </div>
    <div class="height10">
    </div>
    
    <h4 class="blue">
        <asp:Label ID="lblHeader2" runat="server" Text="Import Menu Items"></asp:Label></h4>
    <table cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td class="fieldname nowrap">
                <asp:Label ID="lblSelectExcelFile" runat="server" Text="Select Excel File"></asp:Label>
            </td>
            <td>
                <asp:UpdatePanel ID="imgUpload" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:FileUpload runat="server" ID="fuExcelFile" />
                        <cc1:RequiredFieldValidator ID="rfvExcel" runat="server" ErrorMessage="Please select a file first."
                            ControlToValidate="fuExcelFile" Display="None" ValidationGroup="ImportDeal" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="vceExcel" runat="server" TargetControlID="rfvExcel">
                        </cc2:ValidatorCalloutExtender>
                        <cc1:CustomValidator ID="CustomValidator1" ValidationGroup="ImportDeal" runat="server"
                            ClientValidationFunction="ExcelValidation" ControlToValidate="fuExcelFile" Display="None"
                            ValidateEmptyText="true" ErrorMessage="Invalid file format." SetFocusOnError="True"></cc1:CustomValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="CustomValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnImport" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left">
                <asp:CheckBox runat="server" ID="cbDeleteOldMenu" Width="400" Text="Delete Old Deals"
                    Checked="False" />
            </td>
        </tr>
        <tr>
            <td class="fieldname nowrap">
                <asp:Label ID="lblSelectZipFile" runat="server" Text="Select your image zip file"></asp:Label>
            </td>
            <td class="fieldcontrol">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:FileUpload runat="server" ID="fuImageZip" />
                       <%-- <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select a file first."
                            ControlToValidate="fuImageZip" Display="None" ValidationGroup="ImportDeal" SetFocusOnError="True"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                        </cc2:ValidatorCalloutExtender>--%>
                        <cc1:CustomValidator ID="CustomValidator2" ValidationGroup="ImportDeal" runat="server"
                            ClientValidationFunction="ZipValidation" ControlToValidate="fuImageZip" Display="None"
                            ErrorMessage="Invalid file format." SetFocusOnError="True"></cc1:CustomValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="CustomValidator2">
                        </cc2:ValidatorCalloutExtender>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnImport" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3" class="buttonrow" align="center">
                <br />
                <span>
                 <asp:ImageButton ID="btnImport" runat="server" ValidationGroup="ImportDeal" 
                ImageUrl="Images/btnImport.gif" onclick="btnImport_Click" />
                    <%--<asp:Button runat="server" ID="btnImport" CssClass="btn_orange_smaller" ValidationGroup="ImportDeal"
                        Text="Import" OnClick="btnImport_Click" />--%></span>
                <div style="margin: 0 auto; width: 500px; text-align: left;">
                    <asp:Label runat="server" ID="lblResult" ForeColor="red" />
                </div>
            </td>
        </tr>
    </table>
    <RedSignal:FrameEnd runat="server" ID="FrameEnd3" />
</asp:Content>
