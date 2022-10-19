<%@ Page Title="Featured Food Management" Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true" CodeFile="featuredFoodManagement.aspx.cs" Inherits="admin_featuredFoodManagement" %>

<%@ Register src="UserControls/ctrlResFeaturedFood.ascx" tagname="ctrlResFeaturedFood" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc2:ctrlResFeaturedFood ID="ctrlResFeaturedFood2" 
        runat="server" />
</asp:Content>

