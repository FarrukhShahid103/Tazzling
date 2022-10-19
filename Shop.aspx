<%@ Page Title="" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="MyShop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function AddToMyFavourite(ParamData) {
            var UserData = new Array();
            UserData = ParamData.split('|');
            var id = UserData[0];
            var CampaignID = UserData[1];
            var DivID = "#fav-" + id;
            var CurrentFav = Number($(DivID).html());
            $(DivID).slideUp("fast");
            $(DivID).html(CurrentFav + 1);
            $(DivID).slideDown("fast");
            $.ajax({
                type: 'POST',
                url: 'getStateLocalTime.aspx?AddToMyFavourite=' + CampaignID,
                success: function (msg) {
                    $(DivID).next(":first").addClass('FavouriteDealNonClickable').removeClass('FavouriteDeal').removeAttr("onclick");
                }
            });

        }

        function AddToCartwithSize(id) {
            $("#Buy-" + id).slideUp("fast");
            $("#Loader-" + id).slideDown("fast");
            var size = $("#mark" + id + " option:selected").text();
            var Qty = $("#series" + id + " option:selected").text();
            //alert("Size: "+ small + " Qty: " + Qty);
            $.ajax({
                type: 'POST',
                url: 'getStateLocalTime.aspx?AddToCartWithSize=' + id + "&size=" + size + "&qty=" + Qty,
                success: function (msg) {
                    setTimeout(function () {
                        $("#Buy-" + id).slideDown("fast");
                        $("#Loader-" + id).slideUp("fast");
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
            $("#Buy-" + id).slideUp("fast");
            $("#Loader-" + id).slideDown("fast");
            $.ajax({
                type: 'POST',
                url: 'getStateLocalTime.aspx?AddToCart=' + id,
                success: function (msg) {
                    setTimeout(function () {
                        $("#Buy-" + id).slideDown("fast");
                        $("#Loader-" + id).slideUp("fast");
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

        function changeDivs(id) {
            $("#divBuy-" + id).slideUp("fast");
            $("#sizeDiv-" + id).slideDown("fast");
        }
    </script>
    <script type="text/javascript" src="JS/jquery.chained.js"></script>
    <script type="text/javascript" src="JS/mosaic.1.0.1.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            $('.bar5').mosaic({
                animation: 'slide', 	//fade or slide
                anchor_y: 'top'		//Vertical anchor position
            });
        });
    </script>

    <div>    
        <%--<div style="clear: both; padding-top: 25px;">
            <div style="float: left;">            
                <img src="Images/shopClock.png" />
            </div>
            <div style="float: left; font-size: 20px; font-style: italic; color: #2d3e5a; padding-top: 15px;
                padding-left: 10px;">
                <asp:Label ID="lblSalesTimeTop" runat="server" Text="Sale Ends <span style='color:#FF42E7;'>in</span> 6 days <span style='color:#FF42E7;'>and</span> 8 hours"></asp:Label>
            </div>
            <div style="float: right;">
                <div style="float: right; padding-top: 17px; padding-left: 10px;">
                    <img src="Images/facebook-icn.png" />
                </div>
                <div style="float: right; padding-top: 17px; padding-left: 10px;">
                    <img src="Images/twitter-icn.png" />
                </div>
                <div style="float: right; font-size: 18px; font-style: italic; color: #2d3e5a; padding-top: 17px;">
                    <span style="color: #FF42E7;">Share this sale and </span>earn cash
                </div>
            </div>
        </div>--%>
        <div style="clear: both; padding-top: 15px; font-family:helvetica;">
            <div style="float:left;">
                <div style="width:480px; height:280px; float:left; overflow:hidden; background-color:White; border-radius:3px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2);">
                    <div style="float:left; height:240px; width:100%; vertical-align:top;" >
                        <asp:Image ID="imgSalesMainImage" runat="server" ImageUrl="~/Images/main-img-product-details-image.jpg" Width="480px" Height="240px" />
                    </div>
                    <div style="float:left; position:absolute; padding:230px 0px 0px 20px;"><img src="Images/upArrowWhite.png" alt="" /></div>
                    <div style="clear:both; float:left; height:50px; width:100%; overflow:hidden;">
                        <div style="float:left; padding:5px 0px 0px 10px;">
                            <img src="Images/shop_Clock.png" alt="" />
                        </div>
                        <div style="float:left; font-size:14px; padding:10px 0px 0px 10px;">
                            <asp:Label ID="lblSalesTimeTop" runat="server" Text="Sale Ends in<span style='color:#DD0017;'> 5 days </span>and<span style='color:#DD0017;'> 8 hours</span>"></asp:Label>
                        </div>
                    </div>
                    
                </div>
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
            </div>
            <div style="float: left; padding-left: 35px; font-family:Helvetica; width: 465px;">
                <div style="clear: both; font-size: 24px; font-weight: bold;">
                    <asp:Label ID="lblTitle" runat="server" Text="Sauce"></asp:Label>
                </div>
                <div style="clear: both; font-size: 18px; padding-top: 15px;">
                    <asp:Label ID="lblShortDescription" runat="server" Text="Delicious Designs For The Ladies"></asp:Label>
                </div>
                <div style="clear: both; font-size: 16px; padding-top: 20px; line-height: normal;">
                    <asp:Label ID="lblDescription" runat="server" Text="Sauce makes cozy casualwear look effortlessly polished and fashionable. Launched
                            by stylist-turned-designer Christina Minasian, this NYC-based line channels an
                        aesthetic Minasian dubs “surf-rock meets Tokyo.” Whatever the inspiration, these
                            tees, tanks, dresses and accessories are a giant, stylish leap beyond basic."></asp:Label>
                </div>
                <div style="clear: both; font-size: 16px; padding-top: 10px; line-height: normal;">
                    <div style="float: left; width: 100px;">
                        <div style="clear: both; font-size: 15px; font-weight: bold;">
                            <asp:Label ID="lblMerchantName" runat="server" Text="Cat Birch"></asp:Label>
                        </div>
                        <div style="clear: both; padding-top: 5px;">
                            <asp:Image ID="imgMechantImage" runat="server" Height="84px" Width="84px" ImageUrl="~/Images/imgMerchantTemp.png" />
                        </div>
                    </div>
                    <div style="float: left; width: 350px; padding-left: 10px; line-height: normal; padding-top: 25px;">
                        <div style="clear: both; font-size: 14px; font-style: italic;">
                            <img src="Images/commas.png" />
                            <asp:Label ID="lblMerchantCote" runat="server" Text="When I emigrated to the United States as a young girl, fashion quickly became my gateway into new surroundings. Born and raised in Romania, I discovered that my passion for designing allowed me to express myself in my new home."></asp:Label></div>
                        <div style="clear: both; text-align: right; font-size: 16px; padding-top:8px;">
                            <asp:Label ID="lblMerchantSign" runat="server" Text="– Eva Franco, Designer"></asp:Label></div>
                    </div>
                </div>
            </div>
        </div>
        <div style="border-bottom:1px solid #DEDEDE; padding-top:30px; clear:both;"></div>
        <div style="clear: both; padding-top: 15px; font-family:Helvetica;">
            <asp:Literal ID="ltCampaingsProducts" runat="server" Text=""></asp:Literal>
        </div>
    </div>
</asp:Content>

