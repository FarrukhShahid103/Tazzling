<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Total-Funds.ascx.cs" Inherits="Takeout_UserControls_Templates_Total_Funds" %>
<div align="center">
    <table cellspacing="0" cellpadding="0px" border="0" style="padding-top:40px;">
        <tr>
            <td>
                <div style="float: left;display:none;">
                    <div class="btnMain">
                       <div class="btnBodyBG">
                            <div class="txtGainMain">
                                <div class="Line">
                                    <div class="txtGain">
                                        Tasty Credits: <strong></strong>
                                    </div>
                                    <div class="dynamic">
                                       </div>
                                    <div class="btnWithdraw" style="display:none;">
                                        <a href="member_withdraw.aspx" style="text-decoration: none;">
                                            <div class="txtWithdraw">
                                                <strong>
                                                    <asp:Label ID="lblwithdraw" runat="server" Text="Withdraw"></asp:Label></strong></div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="txtReward">
                                Credits</div>
                        </div>
                    </div>
                </div>

                <div style="float:left; background-image:url('Images/FundBG.jpg'); background-repeat:repeat-x; padding:20px;">
                <div style="float:left; color:White; font-size:15px; padding-right:4px;">
                Tasty Credits:
                </div>
                <div style="float:left; color:White;font-size:15px;"> <asp:Label runat="server" Font-Bold="true" ID="ltReferralGained"></asp:Label> CAD</div>
                </div>
            </td>          
        </tr>
    </table>
</div>
