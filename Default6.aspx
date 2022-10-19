<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="Default6.aspx.cs"
    Inherits="Default6" Title="Untitled Page" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <%--<div>
        <CKEditor:CKEditorControl ID="txtDescription" runat="server" Width="626px" Style="visibility: visible;"
            CausesValidation="false" Height="460px"></CKEditor:CKEditorControl>
    </div>
    <div>
        <asp:TextBox ID="txtFinePrint" TextMode="MultiLine" runat="server" Width="626px"
            Height="100px">
        </asp:TextBox>
        <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFinePrint"
            ErrorMessage="Fine Print required" ValidationGroup="FeaturedFood" Display="None"
            SetFocusOnError="true"></cc1:RequiredFieldValidator>
        <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator6">
        </cc2:ValidatorCalloutExtender>
    </div>
    <div>
        <asp:ImageButton ID="btnImgSave" runat="server" ValidationGroup="FeaturedFood" Visible="true"
            ImageUrl="~/admin/images/btnSave.jpg" ToolTip="Save Deal Info" />
    </div>--%>
</asp:Content>
