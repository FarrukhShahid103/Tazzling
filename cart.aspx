<%@ Page Title="Tazzling.com" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="cart.aspx.cs" Inherits="ShopingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function RemoveFromCart(id) {
        var isDelete = confirm("Do you want to delete item?");
        if (!isDelete) {
            return false;
        }
        $("#div-" + id).slideUp("slow");
        $.ajax({
            type: 'POST',
            url: 'getStateLocalTime.aspx?RemoveFromCart=' + id,
            success: function (msg) {
                setTimeout(function () {
                    $('#ctl00_topView_hlCartText').slideUp('fast', function () {
                        $("#ctl00_topView_hlCartText").html('(' + (msg) + ')');
                        $('#ctl00_topView_hlCartText').slideDown("fast");
                        if (msg == "0") {
                            window.location = "default.aspx";
                        }
                        else {
                            $('#myDiv').fadeOut('slow').load('renderCartHTML.aspx').fadeIn("slow");
                        }
                    });
                    setTimeout(function () { updateShopingCart(msg); }, 1000); 
                }, 1000);
            }
        });
    }
    function RemoveFromCartWithSize(id, size) {
        var isDelete = confirm("Do you want to delete item?");
        if (!isDelete) {
            return false;
        }        
        $("#div-" + size + id).slideUp("slow");
        $.ajax({
            type: 'POST',
            url: 'getStateLocalTime.aspx?RemoveFromCartWithSize=' + id + "&size=" + size,
            success: function (msg) {
                setTimeout(function () {
                    $('#ctl00_topView_hlCartText').slideUp('fast', function () {
                        $("#ctl00_topView_hlCartText").html('(' + (msg) + ')');
                        $('#ctl00_topView_hlCartText').slideDown("fast");
                        if (msg == "0") {
                            window.location = "default.aspx";
                        }
                        else {
                            $('#myDiv').fadeOut('slow').load('renderCartHTML.aspx').fadeIn("slow");
                        }
                    });
                    updateShopingCart(msg);
                }, 1000);
            }
        });
    }

    function updateCart(id, size) {
        var Qty = $("#series" + size + id + " option:selected").text();
        $.ajax({
            type: 'POST',
            url: 'getStateLocalTime.aspx?updateCartWithSize=' + id + "&size=" + size + "&qty=" + Qty,
            success: function (msg) {
                $('#myDiv').fadeOut('slow').load('renderCartHTML.aspx').fadeIn("slow");
                //window.location.reload();               
            }
        });
    }
    $(document).ready(function () {
        //alert($("body").height() - $("#divFooter").height());
        //var scrolVal = 

        $(window).scroll(function () {
            var divFooter = $("#divFooter").offset().top;
            var divScrol = $("body").height() - divFooter;
            //alert(divScrol);
            //alert($(window).scrollTop());

            //alert($("#divHeader").height());
            //$("#divCart").css("top", $(this).scrollTop() + $("#divHeader").height() + 25 + "px");

            //alert($("#divFooter").height());
            //$("#divCart").css("padding-bottom", $(this).scrollTop() - $("#divFooter").height() + "px");
        });
    });
    
    </script>
    <div style="line-height: normal;">
        <div style="clear: both; width: 100%; text-align: left; padding-top: 40px;">
            <img src="Images/CartHeading.png" alt="" />
        </div>
        <div style="clear: both; font-size: 31px; color:Black; font-weight: 700; width: 100%;
            padding-bottom: 10px; padding-top: 30px;">
            Shopping Cart
        </div>
        <div id="myDiv" style="clear: both; padding-top: 20px;">
            <asp:Literal ID="ltShoppingCart" runat="server" Text=""></asp:Literal>
        </div>
    </div>


<%--
    <div style="clear:both; position:fixed; width:317px; height:345px; background-color:white; overflow:hidden; box-shadow: 0px 2px 1px #888888;">
        <div style="background-image:url('images/SubContentDot.png') no-repeat; width:100%; margin-left:5px; margin-top:10px; height:14px; text-align:center;"></div>
        <div style="clear:both; float:left; overflow:hidden; padding:15px; width:90%;">
            <div style="float:left; overflow:hidden;">
                <div style="float:left; font-size:25px; font-weight:bold; padding-top:10px;">Your Order</div>
            </div>
            <div style="float:right;"><img src="Images/YourOrderLogo.png" alt="" /></div>
        </div>
        <div style="clear:both; float:left; padding:0px 20px; overflow:hidden; width:87%;">
            <div style="overflow:hidden; float:left; padding:5px 0px; border-top:1px solid #d1d6dc; width:100%;">
                <div style="float:left;  width:150px; color:#919aa8;">Order Subtotal</div>
                <div style="float:left; color:#5e636c;">Order Value</div>
            </div>
            <div style="clear:both; float:left; border-top:1px solid #d1d6dc; width:100%;  padding:5px 0px;">
                <div style="float:left; overflow:hidden; width:150px;">
                    <div style="float:left; color:#919aa8;">Shipping</div>
                    <div style="float:left; padding-left:5px;">
                        <img src='Images/qustion-icon.png' alt="" />
                    </div>
                </div>
                <div style="float:left; text-align:left; color:#5e636c;">Shiping Value</div>
            </div>
            <div style="clear:both; border-top:1px solid #d1d6dc; color:#103054; font-size:17px; font-weight:700; width:100%; padding:15px 0px;">
                <div style="float:left; width:150px;">Total</div>
                <div style="float:left;">Total Value</div>
            </div>
            <div style="clear:both; background-color:#d1d6dc; border-radius:5px; width:100%; text-align:center; margin-top:30px; padding:5px 0px; color:#DD0016;">Your are saving </div>
            <div style="clear:both; border-bottom:1px solid #d1d6dc; padding-top:15px;"></div>
            <div style="clear:both; padding-top:20px;">
                <asp:ImageButton ID="imbCheckOut" runat="server" ImageUrl="~/Images/CheckOut_cart.png" />
            </div>
        </div>

    </div>
--%>
</asp:Content>

