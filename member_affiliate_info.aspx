<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="member_affiliate_info.aspx.cs"
    Inherits="member_affiliate_info" Title="TastyGo | Member | Affiliate Partner" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <usrCtrl:subMenu ID="subMenu1" runat="server" />
    <div style="padding-top: 40px; padding-bottom: 10px;">
        <div class="height10">
        </div>
        <div style="float: left; width: 725px;">
            <div class="profilebackground" style="padding-left: 5px; padding-top: 3px;">
                <b>
                    <asp:Label ID="lblprofile" runat="server" Text="Become a Affiliate Partner"></asp:Label></b>
            </div>
            <div class="bordermeberprofile" style="font-size: 13px; font-family: Arial;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <div class="height15">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 19px;">
                            This area is for affiliate partner only, to become an affiliate partner, please
                            click on apply today
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="height40">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 19px;">
                            <div>
                                <a href="affiliate.aspx">
                                    <asp:Image ID="img" runat="server" Height="41" Width="126" ImageUrl="~/Images/ApplyToday.png" /></a></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="height40">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
