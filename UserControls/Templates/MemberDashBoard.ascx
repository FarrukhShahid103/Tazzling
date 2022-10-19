<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberDashBoard.ascx.cs"
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
   
    #lava
    {
        /* you must set it to relative, so that you can use absolute position for children elements */
        line-height: 34px;
        position: relative;
        text-align: center;
    }
    #lava ul
    {
        /* remove the list style and spaces*/
          display: inline;
    left: 0;
    list-style: none outside none;
    margin: 0;
    padding: 0 0 0;
    position: absolute;
    top: 0;
    z-index: 100;
    }
    #lava ul li
    {
        /* give some spaces between the list items */
        margin: 0 26px; /* display the list item in single row */
        float: left;
    }
    .selected
    {
        background-image: url( 'Images/ActiveTabBG.png' );
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
    
    
    .MemberArea_MenuItem {
    border: medium none;
    color: White;
    cursor: pointer;
    font-family: Helvetica;
    font-size: 15px;
    font-weight: bold;
    text-decoration: none;
    
}
a:hover
{
	text-decoration:none;
	}
</style>
<div>
    <asp:Panel runat="server" ID="pnlMember" CssClass="MemberArea_MenuItem" Visible="false">
        <div style="height: 35px; margin-top: 10px; background-color: #005F9F;">
            <div id="lava">
                <ul>
                    <li id="member_DashBOard" runat="server"><a class="MemberArea_MenuItem" href="userdashboard.aspx">
                       DashBoard</a></li>
                    <li id="member_DailyDeals" runat="server"><a class="MemberArea_MenuItem" href="frmBusDealOrderInfo.aspx?mode=past">
                        Past Campaign</a></li>
                    <li id="member_NowDeals" runat="server"><a class="MemberArea_MenuItem" href="frmBusDealOrderInfo.aspx?mode=now">
                       Current Campaign</a></li>
                    <li id="member_Redemptions" runat="server"><a class="MemberArea_MenuItem" href="redemptions.aspx">
                        Redemptions</a></li>
                  
                 
                   
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
