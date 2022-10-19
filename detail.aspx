<%@ Page Title="Tazzling.com" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="MyShopDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="JS/jquery.countdown.js"></script>
    <script type="text/javascript" src="JS/jquery.jqzoom-core.js"></script>
    <link rel="stylesheet" type="text/css" href="CSS/jquery.jqzoom.css" />
    <script type="text/javascript" src="JS/jquery.timers-1.2.js"></script>
    <script type="text/javascript" src="JS/jquery.galleryview-3.0-dev.js"></script>
    <script type="text/javascript" src="JS/jquery.chained.js"></script>
    <script type="text/javascript" src="JS/mosaic.1.0.1.js"></script>

    <script type="text/javascript">
        function AddToMyFavourite(ParamData) {
            var UserData = new Array();
            UserData = ParamData.split('|');
            var id = UserData[0];
            var CampaignID = UserData[1];
            var DivID = "#fav-" + id;
            var DivID2 = "#FavouriteDeal-1";
            var CurrentFav = Number($(DivID).html());
            $(DivID).slideUp("fast");
            $(DivID).html(CurrentFav + 1);
            $(DivID).slideDown("fast");
            $.ajax({
                type: 'POST',
                url: 'getStateLocalTime.aspx?AddToMyFavourite=' + CampaignID,
                success: function (msg) {
                    $(DivID2).addClass('FavouriteDealNonClickable').removeClass('FavouriteDeal').removeAttr("onclick");
                }
            });

        }

        function AddToCartwithSize(id) {
            $("#Buy-1").slideUp("fast");
            $("#Loader-1").slideDown("fast");

            $("#Buy-2").slideUp("fast");
            $("#Loader-2").slideDown("fast");

            var size = $("#mark" + " option:selected").text();
            var Qty = $("#series" + " option:selected").text();
            //alert("Size: "+ small + " Qty: " + Qty);
            $.ajax({
                type: 'POST',
                url: 'getStateLocalTime.aspx?AddToCartWithSize=' + id + "&size=" + size + "&qty=" + Qty,
                success: function (msg) {
                    setTimeout(function () {
                        $("#Buy-1").slideDown("fast");
                        $("#Loader-1").slideUp("fast");
                        $("#Buy-2").slideDown("fast");
                        $("#Loader-2").slideUp("fast");
                        $('#ctl00_topView_hlCartText').slideUp('fast', function () {
                            $("#ctl00_topView_hlCartText").html('(' + (msg) + ')');
                            $('#ctl00_topView_hlCartText').slideDown("fast");
                            $('html, body').animate({ scrollTop: 0 }, 1500);
                        });
                        updateShopingCart(msg);
                    }, 1000);
                }
            });
        }

        function AddToCart(id) {
            $("#Buy-1").slideUp("fast");
            $("#Loader-1").slideDown("fast");

            $("#Buy-2").slideUp("fast");
            $("#Loader-2").slideDown("fast");
            var Qty = $("#series" + " option:selected").text();

            $.ajax({
                type: 'POST',
                url: 'getStateLocalTime.aspx?updateCartWithSize=' + id + "&size=sher&qty=" + Qty,
                success: function (msg) {
                    setTimeout(function () {
                        $("#Buy-1").slideDown("fast");
                        $("#Loader-1").slideUp("fast");
                        $("#Buy-2").slideDown("fast");
                        $("#Loader-2").slideUp("fast");
                        $('#ctl00_topView_hlCartText').slideUp('fast', function () {
                            $("#ctl00_topView_hlCartText").html('(' + (msg) + ')');
                            $('#ctl00_topView_hlCartText').slideDown("fast");
                            $('html, body').animate({ scrollTop: 0 }, 1500);
                        });
                        setTimeout(function () { updateShopingCart(msg); }, 1000); 
                    }, 1000);
                }
            });
        }
    </script>


    <script type="text/javascript">

        $(document).ready(function () {
            $('.jqzoom').jqzoom({
                zoomType: 'standard',
                title: false,
                lens: true,
                preloadImages: false,
                alwaysOn: false
            });

        });

    </script>


    <script type="text/javascript">


        $(function () { $("#series").chained("#mark"); });
        $(function () { $("#series2").chained("#mark2"); });


        function changeQty() {
            var Qty = $("#series" + " option:selected").text();
            $("#series2").val(Qty);
        }


        function changeQty2() {
            var Qty = $("#series2" + " option:selected").text();
            $("#series").val(Qty);
        }

        function changeSize() {
            var Qty = $("#mark" + " option:selected").text();
            $("#mark2").val(Qty);
            changeQty();
        }


        function changeSize2() {
            var Qty = $("#mark2" + " option:selected").text();
            $("#mark").val(Qty);
            changeQty2();
        }

     </script>

    <div>
        <%--<div style="clear: both; padding-top: 40px;">       already
            <div style="float: left;">
                <img src="Images/sold-icon.png" />
            </div>
            <div style="float: left; font-size: 20px; color: #2d3e5a; padding-left: 10px;">
                <asp:Label ID="lblTotalSold" runat="server" Text="Sold: <span style='color:#FF42E7;'>4</span>"></asp:Label>
            </div>
            <div style="float: right;">
                <div style="float: right; padding-left: 10px;">
                    <img src="Images/facebook-icn.png" />
                </div>
                <div style="float: right; padding-left: 10px;">
                    <img src="Images/twitter-icn.png" />
                </div>
                <div style="float: right; font-size: 18px; font-style: italic; color: #2d3e5a;">
                    <span style="color: #FF42E7;">Share this sale and </span>earn cash
                </div>
            </div>
        </div>--%>
        <div style="clear: both; padding-top: 10px; font-family:arial;">
            <div style="float: left;">
                <asp:Literal ID="ltSlideShow" runat="server" Text=""></asp:Literal>
                <div style="clear:both; float:left;">
                    <div style="float: right; padding-top: 17px; padding-left: 10px;">
                        <img src="Images/facebook-icn.png" />
                    </div>
                    <div style="float: right; padding-top: 17px; padding-left: 10px;">
                        <img src="Images/twitter-icn.png" />
                    </div>
                    <div style="float: right; font-size: 18px; padding-top: 17px;">
                        <span style="color: #DD0017;">Share this sale and </span>earn cash
                    </div>
                </div>
                <div style="clear: both; width: 360px; padding-top: 40px; line-height: normal;">
                    <div style="clear: both; font-size: 19px; color: #102343; font-weight: bold;">
                        <div style="float: left; padding-top: 1px;">
                            Shipping Info</div>
                        <div style="float: left; padding-left: 10px;">
                            <img src="Images/shippin-info.png" />
                        </div>
                    </div>
                    <div style="clear: both; font-size: 13px; padding-top: 10px;">
                        <div style="width: 110px; float: left; text-align: left; color: #919aa8;">
                            Estimated Arrival
                        </div>
                        <div style="padding-left: 10px; float: left; text-align: left; color: #5e636c;">
                            <asp:Label ID="lblEstimatedArrivalTime" runat="server" Text=" 11 - 20 Days"></asp:Label>
                        </div>
                    </div>
                    <div style="clear: both; font-size: 13px; padding-top: 10px; padding-bottom: 10px;">
                        <div style="width: 110px; float: left; text-align: left; color: #919aa8; vertical-align: top;">
                            Return Policy
                        </div>
                        <div style="padding-left: 10px; float: left; text-align: left; color: #5e636c; width: 230px;">
                            <asp:Label ID="lblReturnPolicy" runat="server" Text="Exchanges on U.S. orders only. We will exchange this unworn, unwashed & undamaged
                            item for another size. Tazzling credit will be issued if your size is not available."></asp:Label>
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 20px; width: 100%;">
                    </div>
                    <div style="clear: both; font-size: 13px; padding-top: 10px; border-top: 1px solid #d1d6dc;
                        border-bottom: 1px solid #d1d6dc; height: 28px; margin-top: 10px;">
                        <div style="width: 110px; float: left; text-align: left; color: #919aa8; vertical-align: top;">
                            Availablity
                        </div>
                        <div style="padding-left: 10px; float: left; text-align: left; color: #5e636c; width: 230px;">
                            <asp:Label ID="lblShipUSToCanada" runat="server" Text="Ship To US and Canada Only"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; padding-left: 25px; color: #363636; width: 575px;">
                <div style="clear: both; font-size: 24px; font-weight:bold; ">
                    <asp:Label ID="lblTitle" runat="server" Text="I PWN NOOBS Shirt Women's"></asp:Label>
                </div>
                <div style="clear: both; font-size: 18px; padding-top:10px;">
                    <asp:Label ID="lblSubTitle" runat="server" Text="by Mike Plater"></asp:Label>
                </div>
                <div style="clear: both;">
                    <div style="float: left; padding-top:5px;">
                        <div style="float: left; padding-top: 5px;">
                            <img src="Images/detailreturnarrow.png" />
                        </div>
                        <div style="float: left; font-size: 12px; padding-left: 10px;">
                            <asp:HyperLink ID="hlReturnURL" runat="server" ForeColor="#5e636c" NavigateUrl="#"
                                Text="Return to Espada Limited"></asp:HyperLink>
                        </div>
                    </div>
                    <div style="float: right; padding-top: 15px;">
                        <div style="float: left;">
                            <div style="clear: both; font-size: 13px;text-align: right;">
                                Like this product?
                            </div>
                            <div style="clear: both; font-size: 18px; color: #363636; text-align: right;">
                                Add to favorities
                            </div>
                        </div>
                        <div style="float: right; padding-left: 12px;">
                            <asp:Literal ID="ltFavroutite" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <div style="width: 100%; clear: both; padding-top:1px; ">
                    <div style="width: 100%; height: 64px; border-bottom: 1px solid #DEDEDE; border-top: 1px solid #DEDEDE;
                        clear: both; margin-top: 15px; background-color: #f4f4f4;">
                        <div style="float: left; padding-left: 15px; padding-top: 7px;">
                            <div style='float: left; width: 150px;'>
                                <div style='clear: both; font-size: 18px; color: #102343; padding-top: 7px;'>
                                    <div style='float: left; font-size: 22px; font-weight: bold;'>
                                        <asp:Label ID="lblProductPrice" runat="server" Text="$25"></asp:Label>
                                    </div>
                                    <div style='float: left; padding-top: 3px; padding-left: 2px; padding-right: 2px;'>
                                        <img src='Images/fav-hear-hov.png' /></div>
                                    <div style='float: left; color:#434343; padding-left:5px;'>
                                        tazzling</div>
                                </div>
                                <div style='clear: both; color: #686868; font-size: 16px; text-decoration: line-through;
                                    padding-top: 5px;'>
                                    <asp:Label ID="lblProdcutActualPrice" runat="server" Text="$50 retail price"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; padding-left: 10px; width: 105px; padding-top: 18px;">
                            <asp:Panel ID="pnlEnableSize" runat="server" Visible="false">
                                <div style="float: left; font-size: 15px; padding-top: 3px; padding-right: 5px;">
                                    Size</div>
                                <div style="float: left; font-size: 15px;">
                                    <asp:Literal ID="ltSize" runat="server" Text=""></asp:Literal>                                   
                                </div>
                            </asp:Panel>
                        </div>
                        <div style="float: left; padding-left: 15px; width: 150px; padding-top: 18px;">
                            <div style="float: left; font-size: 15px; padding-top: 3px; padding-right: 5px;">
                                Qty</div>
                            <div style="float: left; font-size: 15px;">
                                <asp:Literal ID="ltQty" runat="server" Text=""></asp:Literal>                              
                            </div>
                        </div>
                        <div style="float: left; padding-top: 15px;">
                            <asp:Literal ID="ltAddToCart" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <%--<div style="width: 100%; clear: both; padding-top: 20px;">      already
                    <div style="float: left;">
                        <img src="Images/clock-icon.png" />
                    </div>
                    <div style="float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;">
                        Time Left:
                    </div>
                    <div style="float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;">
                        <div id="defaultCountdown" align="center">
                        </div>
                    </div>
                </div>--%>
                <div style="clear: both; padding-top: 30px; line-height: normal;">
                    <asp:Label ID="lblDescription" runat="server" Text="Sauce makes cozy casualwear look effortlessly polished and fashionable. Launched
                            by stylist-turned-designer Christina Minasian, this NYC-based line channels an
                        aesthetic Minasian dubs “surf-rock meets Tokyo.” Whatever the inspiration, these
                            tees, tanks, dresses and accessories are a giant, stylish leap beyond basic."></asp:Label>
                </div>
                <div style="clear: both; padding-top: 60px; line-height: normal;">
                    <div style="float: left; font-size: 19px; color: #102343; font-weight: bold;">
                        More Details
                    </div>
                    <div style="float: left; padding-left: 10px; padding-top: 7px;">
                        <img src="Images/more-details.png" />
                    </div>
                </div>
                <div style="clear: both; padding-top: 20px; line-height: normal;">
                    <asp:Label ID="lblShortDescription" runat="server" Text="Narrow neck rib. Tailored sleeves. Redesigned cut for a better Women’s fit. With plenty of extra room, this shirt is ideal for most body types. The fabric is made from 100% pre-shrunk cotton and has a weight of 5.4 oz."></asp:Label>
                </div>
                <div style="clear: both; padding-top: 20px; line-height: normal; font-size: 13px;">
                    <asp:Literal ID="ltProductProperties" runat="server" Text=""></asp:Literal>
                </div>
                <div style="clear: both; float:left; overflow:hidden; padding-top: 60px; line-height: normal;">
                    <div style="float:left; width:30px;">&nbsp;</div>
                    <div style="float:left; background-color:White; overflow:hidden; width:548px; height:60px;">
                        <div style="float:left; position:absolute; margin-top:-12px; margin-left:-30px;">
                            <img src="Images/tazzlingLogo.png" alt="" />
                        </div>
                        <div style="float:left; font-size: 16px; width:450px; padding:10px 0px 0px 60px;">
                                We guarantee that Tazzling is authorized to sell this product and that every brand
                                we sell is authentic.
                        </div>
                    </div>
                    <%--<div style="width: 578px; height: 80px;  background-color:White; float:left; overflow:hidden;">     already
                        <div style="width: 550px; padding-left: 10px; font-size: 16px;color: black; overflow:hidden;">
                            <div style="float:left;">
                                <img src="Images/tazzlingLogo.png" alt="" />
                            </div>
                            <div style="float:left; padding-left:25px;">
                                
                            </div>
                        </div>
                    </div>--%>
                </div>
                <%--<div style="width: 100%; clear: both; padding-top: 20px;">      already
                    <div style="width: 100%; height: 64px; border-bottom: 1px solid #FF42E7; border-top: 1px solid #FF42E7;
                        clear: both; margin-top: 15px; background-color: #f4f4f4;">
                        <div style="float: left; padding-left: 15px; padding-top: 7px;">
                            <div style='float: left; width: 150px;'>
                                <div style='clear: both; font-size: 18px; color: #102343; font-weight: bold; padding-top: 7px;'>
                                    <div style='float: left;'>
                                        <asp:Label ID="lblProductPrice2" runat="server" Text="$25"></asp:Label>
                                    </div>
                                    <div style='float: left; padding-top: 3px; padding-left: 2px; padding-right: 2px;'>
                                        <img src='Images/price-star.png' /></div>
                                    <div style='float: left;'>
                                        tazzling</div>
                                </div>
                                <div style='clear: both; color: #adb3be; font-size: 16px; text-decoration: line-through;
                                    padding-top: 5px;'>
                                    <asp:Label ID="lblProdcutActualPrice2" runat="server" Text="$50 retail price"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="float: left; padding-left: 10px; width: 105px; padding-top: 18px;">
                            <asp:Panel ID="pnlEnableSize2" runat="server" Visible="false">
                                <div style="float: left; font-size: 15px; padding-top: 3px; padding-right: 5px;">
                                    Size</div>
                                <div style="float: left; font-size: 15px;">
                                    <asp:Literal ID="ltSize2" runat="server" Text=""></asp:Literal>                                   
                                </div>
                            </asp:Panel>
                        </div>
                        <div style="float: left; padding-left: 15px; width: 150px; padding-top: 18px;">
                            <div style="float: left; font-size: 15px; padding-top: 3px; padding-right: 5px;">
                                Quantity</div>
                            <div style="float: left; font-size: 15px;">
                                <asp:Literal ID="ltQty2" runat="server" Text=""></asp:Literal>                                   
                            </div>
                        </div>
                        <div style="float: left; padding-top: 15px;">
                            <asp:Literal ID="ltAddToCart2" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>--%>


            </div>
        </div>
    </div>
    <asp:Literal ID="ltCountDown" runat="server"></asp:Literal>

</asp:Content>

