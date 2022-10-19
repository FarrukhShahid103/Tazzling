<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Total-Referral.ascx.cs"
    Inherits="Takeout_UserControls_Templates_Total_Referral" %>
<div align="center">
    <table cellspacing="0" cellpadding="0px" border="0" style="padding-top: 40px;">
        <tr>
            <td>
                <div style="float: left;">
                    <div class="btnMain">
                        <div class="btnBodyBG">
                            <div class="txtGainMain">
                                <div class="Line">
                                    <div class="txtGain">
                                        Tasty Comission: <strong></strong>
                                    </div>
                                    <div class="dynamic">
                                        <asp:Label runat="server" Font-Bold="true" ID="ltReferralGained"></asp:Label></div>
                                    <div class="btnWithdraw" style="display: none;">
                                        <a href="member_withdraw.aspx" style="text-decoration: none;">
                                            <div class="txtWithdraw">
                                                <strong>
                                                    <asp:Label ID="lblwithdraw" runat="server" Text="Withdraw"></asp:Label></strong></div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="txtReward">
                                Comission</div>
                        </div>
                    </div>
                </div>

            </td>
        </tr>
    </table>
</div>
