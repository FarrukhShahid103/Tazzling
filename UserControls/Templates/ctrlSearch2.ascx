<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlSearch2.ascx.cs" Inherits="Takeout_UserControls_Templates_ctrlSearch2" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<div>
    <div style="height: 111px; width: 1050px; margin-top: 0px; background-image: url(Images/topSearchBackGround.jpg);
        background-repeat: no-repeat; text-align: center;">
        <div style="padding-top: 55px; padding-left: 285px; vertical-align: middle;">
            <div style="float: left; padding-top:3px;">
                <asp:Label Font-Bold="true" Font-Size="21px" Font-Names="Arial" ForeColor="#757474"
                    ID="lblPostalCode" runat="server" Text="Postal Code"></asp:Label>
            </div>
            <asp:Panel ID="pnlSearch" DefaultButton="btnSearchRestaurant" runat="server">
            <div style="float:left; padding-left:10px;">
                <div style="height: 25px; width: 250px; border: solid 1px #d1d0d0; float: left; background-color:White;">
                    <div style="float: left; padding-top: 2px; padding-right: 2px; padding-left: 3px;">
                        <asp:TextBox ID="txtSearchPostalCode" runat="server" CssClass="txtSearch_New2" MaxLength="3"></asp:TextBox>
                        <cc2:TextBoxWatermarkExtender ID="txtWaterMark" runat="server" TargetControlID="txtSearchPostalCode"
                            WatermarkText="First Part of postal code e.g. V5Z ..." WatermarkCssClass="watermark_new2" />
                    </div>
                    <div style="float: left; padding-top: 2px; padding-left:5px;">
                        <asp:ImageButton ID="btnSearchRestaurant" ImageUrl="~/Images/btnSearcTop.jpg"
                            runat="server" onclick="btnSearchRestaurant_Click" />
                    </div>
                </div>
            </div>
            </asp:Panel>
            <div style="float: left; padding-left:5px; padding-top:3px; cursor:pointer;">
               <asp:Label Font-Underline="true" Font-Size="13px" Font-Names="Arial" ForeColor="#aeaeae"
                    ID="Label1" runat="server" Text="Advance Search"></asp:Label> 
            </div>
        </div>
    </div>
</div>
