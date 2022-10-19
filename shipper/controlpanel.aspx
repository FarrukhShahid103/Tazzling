<%@ Page Title="TastyGo | Shipper | Control Panel" Language="C#" MasterPageFile="~/shipper/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="controlpanel.aspx.cs" Inherits="controlpanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="popup">
        <div id="popHeader">
            <asp:Label ID="lblDashBoardHead" runat="server" Text="Control Panel"></asp:Label>
        </div>
    </div>
    <div class="floatLeft" style="padding-bottom: 30px;">   
        <asp:Panel ID="pnlShipper" CssClass="dashBoard" runat="server">         
            <div>
                <div style="padding-top: 15px;">
                    <asp:ImageButton ID="ImageButton43" runat="server" PostBackUrl="~/shipper/trackingdealManagement.aspx"
                        ImageUrl="~/admin/Images/tracking.png" />
                </div>
                <div style="padding-top: 17px;">
                    <asp:Label ID="Label44" Text="Tracking Managment" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div style="padding-top: 12px; padding-left: 10px; padding-bottom: 13px;">
                    <asp:ImageButton ID="ImageButton50" runat="server" PostBackUrl="~/shipper/supplierOrder.aspx"
                        ImageUrl="~/admin/Images/supplier.png" />
                </div>
                <div>
                    <asp:Label ID="Label51" Text="Supplier Orders Management" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>      
    </div>
</asp:Content>
