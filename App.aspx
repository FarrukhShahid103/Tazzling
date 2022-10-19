<%@ Page Title="Tastygo | App" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="App.aspx.cs" Inherits="App" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://connect.facebook.net/en_US/all.js"></script>

    <link href="CSS/Login.css" rel="stylesheet" type="text/css" />
    <link href="CSS/citypopup.css" rel="stylesheet" type="text/css" />
   
    <link href="CSS/AlertBox.css" rel="stylesheet" type="text/css" />
    <div class="PagesBG">
        <div style="padding:30px;">
            <div style="float: left">
            <img id="imgApp1" src="Images/app1.png" />
            </div>
            <div style="float: right">
            <img id="img1" src="Images/app2.png" />
            </div>
        </div>
        <div style="padding: 25px;clear:both; text-align:center;" align="center">
            <asp:Label ID="label8" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Coming Soon"
                Font-Size="40px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label></div>
    </div>
</asp:Content>
