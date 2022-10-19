<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="category.aspx.cs"
    Inherits="category" Title="Tazzling.com" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="JS/jquery.chained.js"></script>
    <script type="text/javascript" src="JS/mosaic.1.0.1.js"></script>
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

                    }, 1000);
                }
            });
        }

        function changeDivs(id) {
            $("#divBuy-" + id).slideUp("fast");
            $("#sizeDiv-" + id).slideDown("fast");
        }

        function updateHTML(id) {
            $("#myDiv").hide("fast");
            $("#divLoader").show();
            setTimeout(function () {
                $('#myDiv').load('renderProductsHTML.aspx?scid=' + id);
                $("#divLoader").hide();
                $("#myDiv").show("fast");
            }, 500);
        }

    </script>
    <div style="min-height: 370px;">
        <div style="clear: both; padding-top: 25px;">
            <div style="float: left; font-size: 24px; color: #2d3e5a; font-weight: bold; padding-top: 15px;">
                <asp:Label ID="lblParentCategory" runat="server" Text="Home"></asp:Label>
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
        </div>
        <asp:Literal ID="ltSubCategory" runat="server"></asp:Literal>
        <div id="divLoader" style="clear: both; text-align: center; padding-top: 100px; display: none;">
            <center>
                <img src="Images/categoryLoader.gif" />
            </center>
        </div>
        <div id="myDiv" style="clear: both;">
            <asp:Literal ID="ltCampaingsProducts" runat="server" Text=""></asp:Literal>
        </div>
    </div>
</asp:Content>
