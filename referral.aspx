<%@ Page Title="Referral" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="referral.aspx.cs" Inherits="referral" %>


<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/FrameStart.ascx" TagPrefix="RedSgnal"
    TagName="FrameStart" %>
<%@ Register Src="~/UserControls/Templates/FrameEnd.ascx" TagPrefix="RedSgnal" TagName="FrameEnd" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style type="text/css">
        .textBoxBG
        {
            background-image: url('images/textBoxBG.png');
            border-radius: 5px;
            height: 28px;
            border: 1px solid #D1D6DC;
        }
        .textBoxError
        {
            /*background-image: url('images/textBoxBG.png');*/
            border-radius: 5px;
            height: 28px;
            border: 2px solid #FF0000;
            background-color:#F6CCDA;
        }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        //$('#ctl00_ContentPlaceHolder1_txtFriendName').tipsy({ gravity: 's' });
        $('#ctl00_ContentPlaceHolder1_txtEmail').tipsy({ gravity: 's' });

        $("#ctl00_ContentPlaceHolder1_lnkSendInvitation").click(function (e) {
            var isValidated = true;
            //var value = $("#ctl00_ContentPlaceHolder1_txtFriendName").val();
            var valueEmail = $("#ctl00_ContentPlaceHolder1_txtEmail").val();

            //if (value == '') {
            //   $("#ctl00_ContentPlaceHolder1_txtFriendName").addClass("TextBoxError");
            //   isValidated = false;

            //}

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

    function checkEmailEmpty() {
        var isValid = true;
        if ($("input[id$='_txtEmail']").val() == "") {
            $("input[id$='_txtEmail']").removeClass("textBoxBG").addClass("textBoxError");
            IsValid = false;
        }
        return isValid;
    }

    </script>
    <div>
        <div class="DetailPage2ndDiv">
            <div style="width:980px; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                </div>
                <div style="font-family:Arial;">
                    <div style="overflow:hidden;">
                        <div style="float:left; overflow:hidden; padding-top:100px; width:600px; font-weight:bold;">
                            <div style="float:left; font-size:49px; clear:both; padding-bottom:45px;">Invite your friends!</div>
                            <div style="float:left; font-size:49px; color:#DD0016; padding:20px 0px;">Get $25 tazzling Credits!</div>
                        </div>
                        <div style="float:right; overflow:hidden;">
                            <div style="float:right;"><img src="Images/Referral_Img.png" alt="" /></div>
                            <div style="float:right; clear:both; font-size:12px; text-align:right; padding-right:45px; padding-bottom:20px;">*When your friend purchase orders of $50 or more</div>
                        </div>
                    </div>
                    <div style="clear:both; float:left; overflow:hidden; width:100%;">
                        <div style="float:left; padding:10px; background-color:White; overflow:hidden; width:400px; height:200px;">
                            <div style="float:left; font-size:26px; padding:10px 0px;">Your Special <span style="color:#DD0016;">Invite Link</span></div>
                            <div style="clear:both; float:left; font-size:13px; padding:10px 0px 20px 0px;">Copy paste the URL below into <b>Twitter, Facebook</b> or in an <b>Email</b></div>
                            <div style="clear:both; float:left;"><asp:TextBox ID="txtShareLink" ReadOnly="true" runat="server" Width="215px" CssClass="textBoxBG"></asp:TextBox></div>
                            <div style="float:left; padding:8px 0px 0px 15px;"> Share </div>
                            <div style="float:left; padding:10px 0px 0px 12px;">
                                <a id="linkTweeter1" runat="server" target="_blank">
                                    <img id="img3" src="Images/twitter.png" alt="" />
                                </a>
                            </div>
                            <div style="float:left; padding:10px 0px 0px 5px;">
                                <a id="linkFacebook1" runat="server" target="_blank">
                                    <img id="img2" src="images/facebook-icn.png" alt="" />
                                </a>
                            </div>
                        </div>
                        <div style="float:left; width:10px;">&nbsp;</div>
                        <div style="float:left; background-color:White; overflow:hidden; width:530px; height:200px; padding:10px;">
                            <div style="float:left; font-size:26px; padding:10px 0px 20px;"><span style="color:#DD0016;">Invite</span> More People</div>
                            <div style="clear:both; float:left; overflow:hidden; border-bottom:1px solid #DEDEDE; width:100%; font-size:17px;">
                                <div style="float:left; border-top:1px solid #DEDEDE; border-left:1px solid #DEDEDE; border-right:1px solid #DEDEDE; padding:8px 12px; margin-right:5px; color:white; background-color:#DD0016;">Email</div>
                            </div>
                            <div style="clear:both; font-size:12px; padding:10px 0px;">Add friend's email addresses</div>
                            <div style="clear:both; float:left; overflow:hidden;">
                                <div style="float:left;"><asp:TextBox ID="txtEmail" runat="server" Width="410px" CssClass="textBoxBG"></asp:TextBox></div>
                                <div style="float:left; padding-left:5px;"><asp:ImageButton ID="lnkSendInvitation" runat="server" ImageUrl="images/SendInvitation.png" OnClientClick="return checkEmailEmpty();" OnClick="lnkSendInvitation_Click" /></div>
                            </div>
                        </div>
                    </div>
                    <div style="clear:both; float:left; padding:20px 10px 10px 10px; font-size:26px;">Interested Earning <span style="color:#DD0016;">Cash </span>Instead?</div>
                    <div style="clear:both; font-size:13px; padding:10px;">Click <a href="#">HERE </a>to Join our Affiliate Program</div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>

