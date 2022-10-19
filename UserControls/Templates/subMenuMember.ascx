<%@ Control Language="C#" AutoEventWireup="true" CodeFile="subMenuMember.ascx.cs"
    Inherits="Takeout_UserControls_Templates_submenu" %>

<script type="text/javascript">

    function LavaMenu() {

        var style = 'easeOutElastic';
        var default_left = Math.round($('#lava li.selected').offset().left - $('#lava').offset().left);
        var default_width = $('#lava li.selected').width();
        $('#box').css({ left: default_left });
        $('#box .head').css({ width: default_width });
        $('#lava li').hover(function () {
            left = Math.round($(this).offset().left - $('#lava').offset().left);
            width = $(this).width();
            $('#box').stop(false, true).animate({ left: left }, { duration: 300 });
            $('#box .head').stop(false, true).animate({ width: width }, { duration: 300 });
        }).click(function () {

            //reset the selected item
            $('#lava li').removeClass('selected');

            //select the current item
            $(this).addClass('selected');

        });

        //If the mouse leave the menu, reset the floating bar to the selected item
        $('#lava').mouseleave(function () {

            //Retrieve the selected item position and width
            default_left = Math.round($('#lava li.selected').offset().left - $('#lava').offset().left);
            default_width = $('#lava li.selected').width();

            //Set the floating bar position, width and transition
            $('#box').stop(false, true).animate({ left: default_left }, { duration: 300 });
            $('#box .head').stop(false, true).animate({ width: default_width }, { duration: 300 });

        });

    }

  


  function pageLoad(sender, args)
   {

      // LavaMenu();

       $(document).ready(function () {
        //   LavaMenu();
       });

    }

    $(function () {
        $(window).load(function () {
          //  LavaMenu();
        })
    });
	</script>

<style type="text/css">
    .MemberArea_MenuItem
    {
        border: none;
        color: White;
        font-size: 15px;
        font-weight: bold;
        text-decoration: none;
        font-family: Helvetica;
        cursor: pointer;
    }
    #lava
    {
        /* you must set it to relative, so that you can use absolute position for children elements */
        line-height: 33px;
        position: relative;
        text-align: center;
    }
    #lava ul
    {
        /* remove the list style and spaces*/
        margin: 0;
        padding: 0;
        list-style: none;
        display: inline; /* position absolute so that z-index can be defined */
        position: absolute; /* center the menu, depend on the width of you menu*/
        left: 0px;
        top: 0; /* should be higher than #box */
        z-index: 5;
    }
    #lava ul li
    {
        /* give some spaces between the list items */
        margin: 0 26px; /* display the list item in single row */
        float: left;
    }
    .selected
    {
        /*background-image: url( 'Images/ActiveTabBG.png' );*/
        background-color:#DD0016;
        background-repeat: repeat;
        padding: 1px 15px;
    }
    #lava #box
    {
        background: url(Images/HoverTail.png) no-repeat right center;
        position: absolute;
        left: 0;
        top: 5px;
        z-index: 50;
        height: 23px;
        padding-right: 9px;
        margin-left: -7px;
        display: none;
    }
    #lava #box .head
    {
        background: url(Images/HoverHead.png) no-repeat 0 0;
        height: 23px;
        padding-left: 7px;
    }
</style>
<div>
    <asp:Panel runat="server" ID="pnlMember" CssClass="MemberAreaMenu" Visible="false">
        <div style="height: 35px; margin-top: 10px; background-color: #363636;">
            <div id="lava">
                <ul>
                    <li id="member_MyTastygo" runat="server"><a class="MemberArea_MenuItem" href="MyOrder.aspx">
                        Vouchers</a></li>
                    <%--<li id="member_MyGiftTastygo" runat="server"><a class="MemberArea_MenuItem" href="member_MyGiftTastygo.aspx">
                        My Gifts</a></li>--%>
                    <li id="member_profile" runat="server"><a class="MemberArea_MenuItem" href="MyAccountSetting.aspx">
                        Account</a></li>
                    <%--<li id="member_preference" runat="server"><a class="MemberArea_MenuItem" href="member_preference.aspx">
                        Preferences</a></li>--%>
                    <%--<li id="member_SubscribeCities" runat="server"><a class="MemberArea_MenuItem" href="member_SubscribeCities.aspx">
                        Subscription</a></li>--%>
                    <li id="member_referral" runat="server"><a class="MemberArea_MenuItem" href="referral.aspx">
                        Referral</a></li>
                   <%-- <li id="member_points" runat="server"><a class="MemberArea_MenuItem" href="member_points.aspx">
                        Points</a></li>--%>
                    <li id="member_affiliate" runat="server" visible="false"><a class="MemberArea_MenuItem"
                        href="MemberAffiliate.aspx">Affiliate Area</a></li>
                </ul>
                <!-- If you want to make it even simpler, you can append these html using jquery -->
                <div id="box">
                    <div class="head">
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
