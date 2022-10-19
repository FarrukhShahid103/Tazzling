<%@ Page Title="Referral" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="member_referral.aspx.cs" Inherits="member_referral" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/FrameStart.ascx" TagPrefix="RedSgnal"
    TagName="FrameStart" %>
<%@ Register Src="~/UserControls/Templates/FrameEnd.ascx" TagPrefix="RedSgnal" TagName="FrameEnd" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $('#ctl00_ContentPlaceHolder1_txtFriendName').tipsy({ gravity: 's' });
        $('#ctl00_ContentPlaceHolder1_txtEmail').tipsy({ gravity: 's' });

        $("#ctl00_ContentPlaceHolder1_btnEmailSubmit").click(function (e) {
            var isValidated = true;
            var value = $("#ctl00_ContentPlaceHolder1_txtFriendName").val();
            var valueEmail = $("#ctl00_ContentPlaceHolder1_txtEmail").val();

            if (value == '') {
                $("#ctl00_ContentPlaceHolder1_txtFriendName").addClass("TextBoxError");
                isValidated = false;

            }

            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
            if (valueEmail == '' || !filter.test(valueEmail)) {
                $("#ctl00_ContentPlaceHolder1_txtEmail").addClass("TextBoxError");
                isValidated = false;
            }
            if (isValidated) {
                return;
            }
            else {
                return false;
            }


        });

    });    
    </script>
    <div>
        <div class="DetailPage2ndDiv">
            <div style="width:980px; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                    <div style="clear: both; padding-top: 10px; margin-bottom: 10px;">
                        <div class="DetailPageTopDiv">
                            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                                <div class="PageTopText" style="float: left;">
                                    My Referral Link
                                </div>
                            </div>
                        </div>
                    </div>
                </div>                
                <div style="background-color: White; min-height: 450px;">
                    <div style="clear: both; width: 100%;">
                        <div style="clear: both; text-align: center; padding-top: 25px;">
                            <div style="font-size: 17px; font-weight: bold;">
                                <asp:Label ID="lblShareDeal" runat="server" Width="590px" Text="Share this deal and receive <span style='color:#ff7800;'>$10</span> when you refer a new Tasty Customer!"></asp:Label>
                            </div>
                        </div>
                        <center>
                        <div style="clear: both; padding-top: 25px; text-align: center;">
                            <div style="clear: both; padding-left: 220px; overflow:hidden;">
                                <div style="float: left;">
                                    <a id="linkFacebook1" runat="server" target="_blank">
                                        <img id="img2" src="Images/ocFBShare.jpg" />
                                    </a>
                                </div>
                                <div style="float: left; padding-left: 40px;">
                                    <a id="linkTweeter1" runat="server" target="_blank">
                                        <img id="img3" src="Images/ocTWShare.jpg" />
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both; padding-top: 25px;">
                            <div style="font-size: 17px; font-weight: bold; padding-left: 75px;">
                                Ready to share with friends?
                            </div>
                        </div>
                        <div style="clear: both; padding-top: 10px; padding-left: 207px; text-align:left;">
                            <div style="float: left;">
                                <div class="ItemHiding">
                                    Name
                                </div>
                                <div style="margin-bottom: 10px;">
                                    <asp:TextBox ID="txtFriendName" runat="server" onfocus="this.className='TextBox'"
                                        CssClass="TextBox" ToolTip="Your friend name"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; padding-left: 10px;">
                                <div class="ItemHiding">
                                    Email
                                </div>
                                <div style="margin-bottom: 10px;">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox" onfocus="this.className='TextBox'"
                                        ToolTip="Your friend email"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both;">
                            <div style="float: right; padding-right: 210px;">
                                <asp:Button CssClass="button big primary" runat="server" ID="btnEmailSubmit" OnClick="btnEmailSubmit_Click" Text="Submit"
                                    ValidationGroup="CreateUserWizard1" />
                            </div>
                        </div>                       
                        <div style="clear: both; padding-top: 10px;">
                            <div style="font-size: 17px; font-weight: bold; padding-left: 75px;">
                                Share your referral link
                            </div>
                        </div>
                        <div style="clear: both; padding-top: 10px;">
                            <div style="padding-left: 0px;">
                                <asp:TextBox ID="txtShareLink" TextMode="MultiLine" Width="560px" Height="55px" runat="server"
                                    CssClass="TextBox" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div style="clear: both; text-align: center; padding-top: 25px;">
                            <div style="font-size: 17px; font-weight: bold;">
                                <asp:Label ID="Label3" runat="server" Width="535px" Text="Copy the link and send to your friend to receive <span style='color:#ff7800;'>$10</span> credits when they order $20 or more!"></asp:Label>
                            </div>
                        </div>
                        <div style="clear: both; text-align: center; padding-top: 20px;">
                            <div>
                                If you have any question, don't hesitate to contact us at <a href="mailto:support@tazzling.com"
                                    style="color: Black;">support@tazzling.com</a>
                            </div>
                        </div>
                        </center>
                    </div>
                </div>
            </div>
          
        </div>
    </div>
</asp:Content>
