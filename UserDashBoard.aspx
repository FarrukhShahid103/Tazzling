<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="UserDashBoard.aspx.cs"
    Inherits="UserDashBoard" Title="Bussiness" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/MemberDashBoard.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/page.css" type="text/css" />
    <link href="CSS/jquery-jqplot.css" rel="stylesheet" type="text/css" />
    <link href="CSS/examples.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/shThemejqPlot.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/shCoreDefault.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery.jscrollpane.css" rel="stylesheet" type="text/css" />

    <script src="JS/excanvas.min.js" type="text/javascript"></script>

    <script src="JS/shCore.min.js" type="text/javascript"></script>

    <script src="JS/shBrushJScript.min.js" type="text/javascript"></script>

    <script src="JS/shBrushXml.min.js" type="text/javascript"></script>

    <script src="JS/example.min.js" type="text/javascript"></script>

    <script src="JS/jquery.jscrollpane.min.js" type="text/javascript"></script>

    <script src="JS/jquery.mousewheel.js" type="text/javascript"></script>

    <script src="JS/jquery.percentageloader-01a.js" type="text/javascript"></script>

    <style type="text/css">
        .jqplot-image-button
        {
            display: none !important;
        }
        .mydeals
        {
            color: White;
            text-decoration: none;
            font-size: 15px;
            font-weight: bold;
            font-family: Helvetica;
        }
        .mydeals:hover
        {
            text-decoration: none;
            color: #fe9ddb;
        }
    </style>
    <style type="text/css">
        .hyperlinkstyle
        {
            color: Black;
            font-family: Helvetica;
            font-size: 14px;
            font-weight: bold;
            text-decoration: none;
        }
        .hyperlinkstyle:hover
        {
            color: Yellow;
            font-family: Helvetica;
            font-size: 14px;
            font-weight: bold;
            text-decoration: none;
        }
    </style>

    <script type="text/javascript">
            
             var valuenew = '<%= UsedQuantityInPers %>';
            var valuetotal = "100";
            var PercentVal;
            var totalorder =  '<%= TotalOrders  %>';
            PercentVal = valuenew / valuetotal * 100;
           
         
       
    </script>

    <style type="text/css">
        .content-area
        {
            height: 500px;
            width: 300px;
            
        }
        /*scrollpane custom CSS*/
        .jspVerticalBar
        {
            width: 8px;
            background: transparent;
            right: 10px;
        }
        .jspHorizontalBar
        {
            bottom: 5px;
            width: 100%;
            height: 8px;
            background: transparent;
        }
        .jspTrack
        {
            background: transparent;
        }
        .jspDrag
        {
            background: http://img.takien.com/2011/12/transparent-black.png repeat;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
        }
        .jspHorizontalBar .jspTrack, .jspHorizontalBar .jspDrag
        {
            float: left;
            height: 100%;
        }
        .jspCorner
        {
            display: none;
        }
    </style>

    <script type="text/javascript">
    $(document).ready(function () {
        $('.content-area').jScrollPane({
            horizontalGutter: 5,
            verticalGutter: 5,
            'showArrows': false
        });

        $('.jspDrag').hide();
        $('.jspScrollable').mouseenter(function () {
            $('.jspDrag').stop(true, true).fadeIn('slow');
        });
        $('.jspScrollable').mouseleave(function () {
            $('.jspDrag').stop(true, true).fadeOut('slow');
        });

    });
    </script>

    <script type="text/javascript">
		    $(document).ready(function () {
		        function cektkp_growtextarea(textarea) {
		            textarea.each(function (index) {
		                textarea = $(this);
		                textarea.css({ 'overflow': 'hidden', 'word-wrap': 'break-word' });
		                var pos = textarea.position();
		                var growerid = 'textarea_grower_' + index;
		                textarea.after('<div style="position:absolute;z-index:-1000;visibility:hidden;top:' + pos.top + ';height:' + textarea.outerHeight() + '" id="' + growerid + '"></div>');
		                var growerdiv = $('#' + growerid);
		                growerdiv.css({ 'min-height': '20px', 'font-size': textarea.css('font-size'), 'width': textarea.width(), 'word-wrap': 'break-word' });
		                growerdiv.html($('<div/>').text(textarea.val()).html().replace(/\n/g, "<br />."));
		                if (textarea.val() == '') {
		                    growerdiv.text('.');
		                }

		                textarea.height(growerdiv.height() + 10);

		                textarea.keyup(function () {
		                    growerdiv.html($('<div/>').text($(this).val()).html().replace(/\n/g, "<br />."));
		                    if ($(this).val() == '') {
		                        growerdiv.text('.');
		                    }
		                    $(this).height(growerdiv.height() + 10);
		                });
		            });
		        }
		        cektkp_growtextarea($('textarea.autogrow'));
		    });
    </script>

    <script type="text/javascript">
        $(function () {   
          var valuenew = '<%= UsedQuantityInPers %>';
        
            var PercentVal;
            var totalorder =  '<%= TotalOrders  %>';
            PercentVal = valuenew / valuetotal * 100;
           
                 
     
        
                 
            var $topLoader1 = $("#topLoader1").percentageLoader({ width: 256, height: 256, controllable: false, progress: 0.5, onProgressUpdate: function (val) {
                $topLoader1.setValue(Math.round(val * 100.0));
            }
            });

            $topLoader1.setProgress(PercentVal/100);
            $topLoader1.setValue(totalorder);
        });      
    </script>

    <div style="width: auto; height: 36px; background-color: #005f9f; clear: both; margin-top: 20px;
        margin-bottom: 10px;">
        <div style="color: White; font-weight: bold; clear: both; text-decoration: none;">
            <usrCtrl:subMenu ID="subMenu1" runat="server" />
        </div>
    </div>
    <div class="DetailPageTopDiv" style="margin-bottom: 10px;">
        <div style="clear: both; padding-top: 7px; padding-left: 15px;">
            <div class="PageTopText" style="float: left;">
                DashBoard
            </div>
        </div>
    </div>
    <div style="width: auto; height: auto; background-color: White; clear: both; padding-bottom:40px;">
        <div style="width: auto; height: 450px; margin-top: 20px; padding-top: 30px;">
            <div style="height: 300px; width: 315px; float: left;">
                <div style="width: 326px; height: 300px; float: left">
                    <div style="width: 260px; color: Black; font-size: 15px; font-weight: bold; background-color: #f9f9f9;
                        border-style: solid; margin-left: 20px; padding: 5px; border-width: 3px; border-color: #f5f5f5;">
                        Active Featured Deals
                    </div>
                    <div style="width: 275px; height: 200px; margin-left: 20px;">
                        <div style="float: left; height: 50px; width: 125px; margin-top: 10px;">
                            <div style="color: Black; font-size: 15px; height: 25px;">
                                Sold</div>
                            <div style="height: 25px; font-size: 20px; font-weight: bold">
                                <%= TotalSoled %></div>
                        </div>
                        <div style="float: right; width: 150px; height: 50px; margin-top: 10px; text-align: right">
                            <div style="color: Black; font-size: 15px; height: 25px;">
                                Total Earnned</div>
                            <div style="height: 25px; font-size: 20px; font-weight: bold;">
                                $<%= TotalAmmount %>
                            </div>
                        </div>
                        <div style="clear: both; margin-top: 30px; height: 260px;">
                            <div id="topLoader1">
                            </div>
                        </div>
                        <div style="clear: both; margin-top: 20px; height: 50px; width: 275px;">
                            <div style="float: left; height: 50px; width: 65px; margin-top: -15px; margin-left: 18px;">
                                <div style="color: Black; font-size: 15px; height: 25px;">
                                    Redeem
                                </div>
                                <div style="height: 25px; font-size: 20px; font-weight: bold; text-align: center;">
                                    <%= Redeeme %></div>
                            </div>
                            <div style="float: right; height: 50px; width: 65px; margin-top: -15px; margin-right: 25px;">
                                <div style="color: Black; font-size: 15px; height: 25px;">
                                    To Go
                                </div>
                                <div style="height: 25px; font-size: 20px; text-align: center; font-weight: bold;
                                    text-align: center;">
                                    <%= ToGoOrder %></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="height: 300px; width: 630px; float: left;">
                <div style="width: 326px; height: 30px;">
                    <div style="width: 585px; color: Black; font-size: 15px; font-weight: bold; background-color: #f9f9f9;
                        border-style: solid; margin-left: 20px; padding: 5px; border-width: 3px; border-color: #f5f5f5;">
                        Customer Feedback
                    </div>
                </div>
                <div style="width: 630px; height: 300px;">
                    <div style="float: left">
                        <div id="chart3" style="margin-top: 65px; margin-left: 20px; width: 300px; border-width: 0px !important;
                            height: 250px;">
                        </div>
                    </div>
                    <div style="float: right; margin-right: 15px; width: 285px;">
                        <div style="margin-top: 15px; font-weight: bold; font-size: 16px; clear: both">
                            Customers Comments
                        </div>
                        <div id="content" class="content-area">
                            <asp:Literal ID="ltlcomments" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <div style="width: 300px; display: none; margin-top: 28px; font-size: 14px; font-family: Verdana;
                    margin-left: 30px;">
                    <div style="float: left;">
                        <asp:Label ID="lblCommentBelow" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div style="height: auto; width: 315px;">
            <div style="width: 260px; color: Black; font-size: 15px; font-weight: bold; background-color: #f9f9f9;
                border-style: solid; margin-left: 20px; padding: 5px; border-width: 3px; border-color: #f5f5f5;">
                Your Businesses
            </div>
            <div id="content1">
                <asp:Literal ID="ltlBussinessNames" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

    <script src="JS/jquery-jqplot.js" type="text/javascript"></script>

    <script src="JS/jqplot.pieRenderer.min.js" type="text/javascript"></script>

</asp:Content>
