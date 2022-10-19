<%@ Page Title="Tastygo | No Deal" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="nodeal.aspx.cs" Inherits="nodeal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div style="float: left; font-size: 15px;">
                        <asp:Label ID="lblTopTitle" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 100%; background-color: White; min-height: 450px;border:1px solid #ACAFB0;">
                    <div style="background-color: White; overflow: hidden; padding: 20px;">
                        <div style="padding-bottom: 10px;">
                            <asp:Label ID="lblHeading" runat="server" Text="No deal for your city" Font-Size="29px"
                                Font-Bold="true"></asp:Label></div>
                        <div style="float: left; color: #056eb7; font-size: 16px; font-weight: bold; word-spacing: 3px;
                            line-height: 23px;">
                            <asp:Label ID="lblText" runat="server" Text="Your city did not exist! Subscribe to our newsletter and get first-hand incredible deals when we launch!"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
