<%@ Control Language="C#" AutoEventWireup="true" CodeFile="All-Total.ascx.cs" Inherits="Takeout_UserControls_Templates_All_Total" %>
<div align="center">
    <table cellspacing="0" cellpadding="0px" border="0" style="padding-top: 40px;">
        <tr>
            <td>
                <div style="float: left;">
                    <div class="btnMain">
                        <div class="btnBodyBG">
                            <div class="txtGainMain2">
                                <div class="Line2">
                                    <%-- <div class="dynamic">
                                        <asp:Label runat="server" Font-Bold="true" ID="lblTastyPoints" Text="0"></asp:Label></div>--%>
                                    <div class="dynamic" style="padding-top:8px;">
                                        <asp:Label runat="server" Font-Bold="true" ID="lblTastyCredit" Text="0"></asp:Label></div>
                                    <div class="dynamic">
                                        <asp:Label runat="server" Font-Bold="true" ID="lblTastyComission" Text="0"></asp:Label></div>
                                </div>
                            </div>
                            <div style="padding-left: 6px; padding-top: 8px;">
                               <%-- <div class="dynamic2">
                                    Points:</div>--%>
                                <div class="dynamic2" style="padding-top:9px;">
                                    Credits:</div>
                                <div class="dynamic2">
                                    Comission:</div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
