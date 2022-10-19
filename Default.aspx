<%@ Page Language="C#" Title="Tazzling.com" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="HomeTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="http://tjs.sjs.sinajs.cn/open/api/js/wb.js?appkey=" type="text/javascript"
        charset="utf-8"></script>
    <%--<script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>
    <script type="text/javascript">        stLight.options({ publisher: '8f218e64-86c1-4526-8a44-fa119a1268af' });</script>    --%>
    <div style="clear: both; padding-top: 10px">
        <asp:Panel ID="pbnBanner" runat="server" Style="height: auto; width: 100%; margin-bottom: 10px;">
            <asp:Literal ID="ltBanner" runat="server"></asp:Literal>
        </asp:Panel>
        <div>
            <%--   <div style="clear: both; padding-top: 10px; text-align: center; padding-bottom: 10px;">
                <img src="images/topbannerNew.png" />
            </div>--%>
            <asp:Literal ID="ltFuaturedCampaigns" runat="server" Text=""></asp:Literal>
        </div>
    </div>
    <script type="text/javascript" src="JS/jquery.easing.1.3.js"></script>
    <!-- the jScrollPane script -->
    <script type="text/javascript" src="JS/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="JS/jquery.contentcarousel.js"></script>
    <script type="text/javascript" src="JS/mosaic.1.0.1.js"></script>
    <script type="text/javascript" src="js/jquery.hoverdir.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#TazzlingProducts > li').hoverdir();
        });
		</script>
    <script type="text/javascript">
        jQuery(function ($) {
            $('.bar').mosaic({
                animation: 'slide'		//fade or slide
            });
        });

        jQuery(function ($) {
            $('.bar3').mosaic({
                animation: 'none'		//fade or slide
            });
        });

        jQuery(function ($) {
            $('.bar4').mosaic({
                animation: 'slide', 	//fade or slide
                anchor_y: 'top'		//Vertical anchor position
            });
        });

        jQuery(function ($) {
            $('.bar2').mosaic({
                animation: 'slide'		//fade or slide
            });
        });
		    
    </script>
    <script type="text/javascript">
        $('#ca-container').contentcarousel({
            // number of items to scroll at a time
            scroll: 4
        });

        $(document).ready(function () {
            var orignalHtml = $.trim($(".TagText").html());
            var _html = new Array();
            _html = orignalHtml.split(' ');
            var First = _html[0];
            var Second = _html[1];
            $(".TagText").html("<b>" + First + "</b> " + Second);

        });
    </script>
    <noscript>
        <style>
            .TazzlingProducts li a p
            {
                top: 0px;
                left: -100%;
                -webkit-transition: all 0.3s ease;
                -moz-transition: all 0.3s ease-in-out;
                -o-transition: all 0.3s ease-in-out;
                -ms-transition: all 0.3s ease-in-out;
                transition: all 0.3s ease-in-out;
            }
            .TazzlingProducts li a:hover p
            {
                left: 0px;
            }
        </style>
    </noscript>
    <style type="text/css">
        .DealBox
        {
            border: 2px solid #D9D2D0;
            height: 340px;
            list-style: none outside none;
            width: 290px;
        }
        .DealTopTag
        {
            color: white;
            font-size: 16px;
            font-weight: bold;
            margin-left: 17px;
            padding-top: 5px;
            text-align: center;
            text-transform: uppercase;
            width: 50px;
        }
        
        .TazzlingProducts
        {
            list-style: none;
            position: relative;
            margin: 20px auto;
            padding: 0;
        }
        .TazzlingProducts li
        {
            float: left;
            position: relative;
        }
        .TazzlingProducts li a, .TazzlingProducts li a img
        {
            display: block;
            position: relative;
        }
        .TazzlingProducts li a
        {
            overflow: hidden;
        }
        .TazzlingProducts li a p
        {
            position: absolute;
            background-image: url('images/Overlay_bg.png');
            background-repeat: repeat;
            width: 100%;
            height: 100%;
        }
        .TazzlingProducts li a p.da-animate
        {
            -webkit-transition: all 0.3s ease;
            -moz-transition: all 0.3s ease-in-out;
            -o-transition: all 0.3s ease-in-out;
            -ms-transition: all 0.3s ease-in-out;
            transition: all 0.3s ease-in-out;
        }
        /* Initial state classes: */.da-slideFromTop
        {
            left: 0px;
            top: -100%;
        }
        .da-slideFromBottom
        {
            left: 0px;
            top: 100%;
        }
        .da-slideFromLeft
        {
            top: 0px;
            left: -100%;
        }
        .da-slideFromRight
        {
            top: 0px;
            left: 100%;
        }
        /* Final state classes: */.da-slideTop
        {
            top: 0px;
        }
        .da-slideLeft
        {
            left: 0px;
        }
        .TazzlingProducts li a p span
        {
        	/*
            border-bottom: 1px solid rgba(255, 255, 255, 0.5);
            box-shadow: 0 1px 0 rgba(0, 0, 0, 0.1), 0 -10px 0 rgba(255, 255, 255, 0.3); */
             color: white;
            display: block;
            font-size: 18px;
            font-weight: bold;
            margin: 100px 15px 45px;
            padding: 10px 0;
            text-align: center;
            text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.2);
        }
        .DealClock
        {
                padding-left: 55px;
                padding-right: 10px;
                padding-top: 90px;
        }
        .DealRightArrow
        {
            float: right;
            clear:both;
            padding-right: 20px;
            padding-top: 10px;
        }
        .DealImage
        {
            height: 290px;
            width: 290px;
        }
        .DealTopArrow
        {
            background: url('images/Deal_Top_Arrow.png');
            background-repeat: no-repeat;
            width: 85px;
            height: 56px;
        }
        
        
        /* New Product Box */
        
        .picture
        {
            background: none repeat scroll 0 0 white;
            border: 1px solid #D7D3D1;
            border-radius: 4px 4px 4px 4px;
            box-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
            margin-top: 30px;
            width: 480px;
        }
        
        
        .top .profile
        {
            -moz-transition: all 0.5s ease 0s;
            background: url("images/demo-bg.png") repeat scroll 0 0 transparent;
            height: 260px;
            opacity: 0;
            position: absolute;
            text-align: center;
            visibility: hidden;
            width: 520px;
        }
        .arrow
        {
            background: url("images/arrow.png") repeat scroll 0 0 transparent;
            height: 10px;
            margin-left: 20px;
            margin-top: 230px;
            position: absolute;
            width: 19px;
            z-index: 100;
        }
        .new
        {
            background: url("images/new.png") repeat scroll 0 0 transparent;
            height: 53px;
            margin-left: 400px;
            margin-top: -4px;
            position: absolute;
            width: 53px;
            z-index: 100;
        }
        .cover
        {
            background: url("images/product_loading.gif") no-repeat scroll center center white;
            height: 240px;
            width: 480px;
        }
        .bottom
        {
            padding: 10px 10px 25px;
        }
        .cover img
        {
            border-radius: 4px 4px 0px 0px;
        }
        .ProductTitle
        {
            font-size: 14px;
            font-weight: bold;
            float: left;
            color: #363636;
        }
        .ProductSubTitle
        {
            font-size: 14px;
            float: left;
            color: #828282;
            padding-left: 15px;
        }
        .ProductBought
        {
            font-size: 14px;
            float: left;
            color: #828282;
            padding-left: 5px;
        }
        .right
        {
            float: right;
        }
        .TotalBuyCount
        {
            color: #dd0017;
            font-size: 14px;
            font-weight: bold;
            float: left;
        }
        .TagText
        {
            color: White;
            width: 53px;
            text-align: center;
        }
    </style>
    <div style="width: 100%; min-height: 700px; overflow: hidden;">
       <ul id="TazzlingProducts" class="TazzlingProducts" style="clear: both; margin-top: 10px;">
            <asp:Literal ID="ltDeals" runat="server"></asp:Literal>
        </ul>
        <div style="height: 30px; width: 100%; clear: both;">
        </div>
    </div>
</asp:Content>
