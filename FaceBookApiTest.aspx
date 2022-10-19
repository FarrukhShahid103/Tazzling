<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaceBookApiTest.aspx.cs"
    Inherits="FaceBookApiTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Live Feeds | Tastygo</title>
    <script type="text/javascript" src="js/jquery-1.4.1-and-plugins.min.js"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <link href="css/Feeds.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.simplemodal.js" type="text/javascript"></script>
    <script src="//connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Changes = 0;
        var LiveFeeds = new Array();
        $.cookie('FeedsCount', '1', '08-09-2016');
        GetFeeds();
        function GetFeeds() {
            var Data = "";
            var _html = "";
            var IsAnyChange = false;
            $.ajax({
                type: "POST",
                url: "FaceBookApiTest.aspx?GetFeeds=True",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {
                    $("#hfCurrentData").val(msg);
                    $("#hfCountData").val(msg);
                    var splitLavel1 = msg.split('|');
                    var counter = 0;
                    for (var i = 0; i < splitLavel1.length; i++) {
                        if (splitLavel1[i] != "") {
                            var splitLavel2 = splitLavel1[i].split('*');
                            var DealID = splitLavel2[0];
                            var DealTitle = splitLavel2[1];
                            var DealImage = splitLavel2[2];
                            var TotalFavorites = splitLavel2[3];
                            var TotalPurchases = splitLavel2[4];
                            var ActivityTime = splitLavel2[5];
                            var RestaurantId = splitLavel2[6];
                            var FirstName = splitLavel2[7];
                            var lastName = splitLavel2[8];
                            var sellingPrice = splitLavel2[9];
                            var TotalComments = splitLavel2[10];
                            var UserID = splitLavel2[11];
                            LiveFeeds.push(i);
                            counter++;

                            _html += "<li onmouseleave='javascript:HideLike(" + DealID + ");' onmouseover='javascript:showLike(" + DealID + ");' id='id-" + DealID + "' data-id='id-" + DealID + "' data-type='app' style='width: 240px; margin-top:40px; height: 338px; float:left; margin-right:30px;'>";
                            _html += "<span style='display:none;' id='Like" + counter + "' data-type='size'>" + i + "</span>";
                            _html += "<div class='feedItem nextPage' style='border: 1px dotted rgb(127, 127, 127);'>";
                            _html += "<div class='imageContainer'>";
                            _html += "<div class='imgParent'>";
                            _html += " <img class='lazy img' src='" + DealImage + "' alt='' width='135px' height='175px' /></div>";
                            _html += "<span class='priceTag'><span class='spriteIcon salesTagIcon floatLeft'></span><span ";
                            _html += "class='floatLeft  marginRight7 color333'>$" + sellingPrice + "</span></span></div>";
                            _html += "<div class='LikeDiv' style='margin-top:-35px; position:absolute;background-color:White; clear:both; padding-top:5px; margin-left:9px; Display:none;'>";
                            _html += "<iframe src='//www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.tazzling.com%2Fdid%3D" + DealID + "&amp;send=false&amp;layout=standard&amp;width=450&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font&amp;height=80' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:210px; height:28px;' allowTransparency='false'></iframe>";
                            _html += "</div>";
                            _html += " <div class='tbCt'>";
                            _html += " <div class='action'>";
                            _html += "<span class=' tastygoShopSprite spriteIcon  '></span><span class='user clickable'>" + FirstName + "  " + lastName + "</span><span ";
                            _html += "class='actionDesc'> faved</span><span class='product'> " + DealTitle + "</span></div>";
                            _html += "<div class='infoToolbar'>";
                            _html += "  <span class='timestamp'><span class='tastygoShopSprite spriteIcon timeIcon'></span><span";
                            _html += " class='value'>1m</span></span><span class='tastygoShopSprite spriteIcon separator'></span><span";
                            _html += "  class='favCount'><span title=' " + TotalFavorites + " Favorites' class='tastygoShopSprite spriteIcon icon favCountIcon' did='" + DealID + "' id='fav-" + counter + "' uid='" + UserID + "' onclick='javascript:AddToFav(" + counter + ");'></span><span";
                            _html += "   class='counter'>" + TotalFavorites + "</span></span><span class='commentCount'><span  title='Add/View Comments'  did=" + DealID + " id='fav-" + counter + "' uid='" + UserID + "' titleofdeal='" + DealTitle + "' imagepath='" + DealImage + "' onclick='javascript:TastyCommentCall(this);' ";
                            _html += " class='tastygoShopSprite spriteIcon icon'></span>" + TotalComments + "</span><span title='" + TotalPurchases + " Purchased'";
                            _html += "  class='cartCount'><span class='tastygoShopSprite spriteIcon icon'></span>" + TotalPurchases + "</span><span";
                            _html += "  class='buyIt'>Buy It</span><div class='buyItLoader posAbs'>  </div>";
                            _html += "  <div class='buyItLoaderWithS posAbs'>";
                            _html += "</div></div>";
                            _html += " <div class='sizeSelector'>";
                            _html += " <div class='selectCt'>";
                            _html += " <span style='font-size: 12px; color: #666; margin-right: 7px;'>Size</span>";
                            _html += " <select> </select></div>";
                            _html += "<div class='cartWrap'>";
                            _html += " <span class='tastygoSubmitBtn addToCart tastygoGrad borderR15 noShadow floatLeft'><span";
                            _html += "  class='tastygoShopSprite cart_icon marginRight5'></span>Add to Cart</span><span class='greytastygoButton confrmB round15 canbutFromFeed cancel'>Cancel</span></div>";
                            _html += "</div></div>";
                            _html += "<div class='comments'>";
                            _html += " <div class='bottomToolbar' style='border-bottom: medium none; text-align:right;margin-top:0px;'>";
                            _html += "<span class='button noComment'  did='" + DealID + "' id='fav-" + counter + "' uid='" + UserID + "' titleofdeal=" + DealTitle + " imagepath='" + DealImage + "'   onclick='javascript:TastyCommentCall(this);'>Add comment</span></div>";
                            _html += "  </div>";
                            _html += "</div>";
                            _html += "</li>";
                        }
                        $("#htmlArea").html(_html);
                        Sorting();
                        RecallUpdateCount();
                    }


                }
            });
        }
        function showLike(ID) {
            var OrignalID = "id-" + ID;
            $("#" + OrignalID).children().find(".LikeDiv").show("fast");
        }
        function HideLike(ID) {
            var OrignalID = "id-" + ID;
            $("#" + OrignalID).children().find(".LikeDiv").hide("fast");
        }
        function AddToFav(id) {

            var UserID = $("#fav-" + id).attr("uid");
            var DealID = $("#fav-" + id).attr("did");
            $.ajax({
                type: "POST",
                url: "FaceBookApiTest.aspx?UserID=" + UserID + "&DealID=" + DealID,
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg == "True") {
                    }
                    else {
                    }
                }
            });
        }
        function TastyCommentCall(id) {
            var UserID = $(id).attr("uid");
            var DealID = $(id).attr("did");
            var DealTitle = $(id).attr("titleofdeal");
            var image = $(id).attr("imagepath");
            $(".popupimg").attr('src', image);
            // $(".viewallcomments").attr('href', 'default.aspx?sidedeal=' + DealID);Inspiration
            $(".viewallcomments").attr('href', 'Inspiration.aspx?sidedeal=' + DealID);
            var newUrl = "http://www.demo.tazzling.com/dealid=" + DealID;
            parser = document.getElementById('comments');
            parser.innerHTML = '<div class="fb-comments" data-href="' + newUrl + '" data-num-posts="2"></div>';
            FB.XFBML.parse(parser);
            parser = document.getElementById('title');
            parser.innerHTML = '<div> "' + DealTitle + '" </div>';
            FB.XFBML.parse(parser);
            $.ajax({
                type: "POST",
                url: "FaceBookApiTest.aspx?sidedeal=" + DealID,
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg == "True") {
                    }
                    else {
                    }
                }
            });

            var dealidforcomments = $("#hfcommentscount").val(DealID);
            $('#tastygoModalContent').modal({
                closeHTML: "<a href='#' title='Close' class='modal-close commentclose' ></a>",
                position: ["20%", ],
                overlayId: 'model-overlay',
                containerId: 'tastygoModal',
                onShow: function (dialog) {
                    var modal = this;
                    $('.yes', dialog.data[0]).click(function () {
                        if ($.isFunction(callback)) {
                            callback.apply();
                        }
                        modal.close();
                    });
                }
            });
        }

        function Updatevalues() {
            var counter = 0;
            for (var i = 0; i < 4; i++) {
                counter++;
                var number = 1 + Math.floor(Math.random() * 4);
                $("#Like" + counter).html(number);

            }

        }
        function GetandupdateFeeds() {
            var Data = "";
            var IsAnyChange = false;
            $.ajax({
                type: "POST",
                url: "FaceBookApiTest.aspx?GetFeeds=True",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {
                    var splitLavel1 = msg.split('|');
                    for (var i = 0; i < splitLavel1.length; i++) {
                        if (splitLavel1[i] != "") {
                            var splitLavel2 = splitLavel1[i].split('*');
                            var DealID = splitLavel2[0];
                            var DealTitle = splitLavel2[1];
                            var DealImage = splitLavel2[2];
                            var TotalFavorites = splitLavel2[3];
                            var TotalPurchases = splitLavel2[4];
                            var ActivityTime = splitLavel2[5];
                            var RestaurantId = splitLavel2[6];
                            var FirstName = splitLavel2[7];
                            var lastName = splitLavel2[8];
                            var sellingPrice = splitLavel2[9];
                            var TotalComments = splitLavel2[10];
                            var UserID = splitLavel2[11];
                            Data += DealID + "*" + DealTitle + "*" + DealImage + "*" + TotalFavorites + "*" + TotalPurchases + "*" + ActivityTime + "*" + RestaurantId + "*" + FirstName + "*" + lastName + "*" + sellingPrice + "*" + TotalComments + "*" + UserID + "|";
                            if ($("#hfCurrentData").val() != "") {
                                var _Data = "";
                                var SavedData = $("#hfCurrentData").val();
                                var _splitLavel1 = SavedData.split('|');
                                if (_splitLavel1 != "") {
                                    for (var j = 0; j < _splitLavel1.length; j++) {
                                        var _splitLavel2 = _splitLavel1[j].split('*');
                                        var _DealID = _splitLavel2[0];
                                        var _DealTitle = _splitLavel2[1];
                                        var _DealImage = _splitLavel2[2];
                                        var _TotalFavorites = _splitLavel2[3];
                                        var _TotalPurchases = _splitLavel2[4];
                                        var _ActivityTime = _splitLavel2[5];
                                        var _RestaurantId = splitLavel2[6];
                                        var _FirstName = splitLavel2[7];
                                        var _lastName = splitLavel2[8];
                                        var _sellingPrice = splitLavel2[9];
                                        var _TotalComments = splitLavel2[10];
                                        var _UserID = splitLavel2[11];
                                        if (DealID == _DealID) {
                                            if (TotalFavorites != _TotalFavorites) {
                                                var biggest = Math.max.apply(null, LiveFeeds);
                                                $("#id-" + DealID).children("span").html((biggest + 1));
                                                LiveFeeds.push((biggest + 1));
                                                IsAnyChange = true;
                                                Changes++;
                                            }
                                            if (TotalPurchases != _TotalPurchases) {
                                                var biggest = Math.max.apply(null, LiveFeeds);
                                                $("#id-" + DealID).children("span").html((biggest + 1));
                                                LiveFeeds.push((biggest + 1));
                                                IsAnyChange = true;
                                                Changes++;
                                            }
                                        }
                                    }
                                }

                            }
                        }

                        if (IsAnyChange) {
                            $("#hfCurrentData").val("");
                            $("#hfCurrentData").val(Data);
                        }
                    }

                    if (Changes != 0) {
                        $(document).attr('title', "(" + Changes + ") Tastygo");
                    }

                    if ($("#hfCurrentData").val() == "") {
                        $("#hfCurrentData").val(Data);
                    }

                    Sorting();
                    Changes = 0;
                    $("#ReFetchUpdates").html("No new Update");
                    $(document).attr('title', "Live Feeds | Tastygo");
                }
            });
        }

        function RecallUpdateCount() {
            setInterval(function () {
                GetUpdateCount();
            }, 2000);

        }

        function GetUpdateCount() {
            var Data = "";
            var IsAnyChange = false;
            $.ajax({
                type: "POST",
                url: "FaceBookApiTest.aspx?GetFeeds=True",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {
                    var splitLavel1 = msg.split('|');
                    for (var i = 0; i < splitLavel1.length; i++) {
                        if (splitLavel1[i] != "") {
                            var splitLavel2 = splitLavel1[i].split('*');
                            var DealID = splitLavel2[0];
                            var DealTitle = splitLavel2[1];
                            var DealImage = splitLavel2[2];
                            var TotalFavorites = splitLavel2[3];
                            var TotalPurchases = splitLavel2[4];
                            var ActivityTime = splitLavel2[5];
                            var RestaurantId = splitLavel2[6];
                            var FirstName = splitLavel2[7];
                            var lastName = splitLavel2[8];
                            var sellingPrice = splitLavel2[9];
                            var TotalComments = splitLavel2[10];
                            var UserID = splitLavel2[11];
                            Data += DealID + "*" + DealTitle + "*" + DealImage + "*" + TotalFavorites + "*" + TotalPurchases + "*" + ActivityTime + "*" + RestaurantId + "*" + FirstName + "*" + lastName + "*" + sellingPrice + "*" + TotalComments + "*" + UserID + "|";
                            if ($("#hfCountData").val() != "") {
                                var _Data = "";
                                var SavedData = $("#hfCountData").val();
                                var _splitLavel1 = SavedData.split('|');
                                if (_splitLavel1 != "") {
                                    for (var j = 0; j < _splitLavel1.length; j++) {
                                        var _splitLavel2 = _splitLavel1[j].split('*');
                                        var _DealID = _splitLavel2[0];
                                        var _DealTitle = _splitLavel2[1];
                                        var _DealImage = _splitLavel2[2];
                                        var _TotalFavorites = _splitLavel2[3];
                                        var _TotalPurchases = _splitLavel2[4];
                                        var _ActivityTime = _splitLavel2[5];
                                        var _RestaurantId = splitLavel2[6];
                                        var _FirstName = splitLavel2[7];
                                        var _lastName = splitLavel2[8];
                                        var _sellingPrice = splitLavel2[9];
                                        var _TotalComments = splitLavel2[10];
                                        var _UserID = splitLavel2[11];

                                        if (DealID == _DealID) {
                                            if (TotalFavorites != _TotalFavorites) {
                                                IsAnyChange = true;
                                                Changes++;
                                            }
                                            if (TotalPurchases != _TotalPurchases) {
                                                IsAnyChange = true;
                                                Changes++;
                                            }

                                        }

                                    }
                                }

                            }

                        }
                    }

                    if (Changes != 0) {
                        if (Changes > 99) {
                            $(document).attr('title', "(99+) Tastygo");
                            $("#ReFetchUpdates").html("99+ new updates");
                        }
                        else {
                            $(document).attr('title', "(" + Changes + ") Tastygo");
                            if (Changes > 1) {
                                $("#ReFetchUpdates").html("" + Changes + " new updates");
                            }
                            else {
                                $("#ReFetchUpdates").html("" + Changes + " new Update");
                            }
                        }
                    }

                    if (Data != "") {
                        $("#hfCountData").val(Data);



                    }
                }
            });

        }
        function Sorting() {
            var $applications = $('#htmlArea');
            var $data = $applications.clone();
            var $filteredData = $data.find('li');
            var $sortedData = $filteredData.sorted({
                by: function (v) {
                    if (!isNaN(parseFloat($(v).find('span[data-type=size]').text()))) {
                        return parseFloat($(v).find('span[data-type=size]').text());
                    }
                }
            });

            $applications.quicksand($sortedData, {
                duration: 800,
                easing: 'easeInOutQuad'
            });

        }


        (function ($) {
            $.fn.sorted = function (customOptions) {
                var options = {
                    reversed: true,
                    by: function (a) { return a.text(); }
                };
                $.extend(options, customOptions);
                $data = $(this);
                arr = $data.get();
                arr.sort(function (a, b) {
                    var valA = options.by($(a));
                    var valB = options.by($(b));
                    if (options.reversed) {
                        return (valA < valB) ? 1 : (valA > valB) ? -1 : 0;
                    } else {
                        return (valA < valB) ? -1 : (valA > valB) ? 1 : 0;
                    }
                });
                return $(arr);
            };
        })(jQuery);

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
            fjs.parentNode.insertBefore(js, fjs);
        }
    (document, 'script', 'facebook-jssdk'));

    </script>
    <style type="text/css">
        .learnmore
        {
            color: White;
        }
        .learnmore:hover
        {
            color: Red;
        }
        
        
        
        #div
        {
            display: none !important;
            background-color: Red;
            width: 100%;
            height: 100px;
        }
        #div:hover
        {
            display: block !important;
            background-color: Red;
            width: 100%;
            height: 100px;
        }
    </style>
    <script type="text/javascript">        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));</script>
    <style type="text/css">
        textarea#txtUserNotes
        {
            width: 350px;
            height: 130px;
            border: 3px solid #cccccc;
            padding: 5px;
            font-family: Tahoma, sans-serif;
        }
    </style>
    <style type="text/css">
        .btncancel
        {
            text-decoration: none;
            font-size: 15px;
        }
        .btncancel:hover
        {
            text-decoration: underline;
            font-size: 16px;
            color: #FE9DDB;
        }
        .viewallcomments
        {
            float: right;
            color: Black;
        }
        .viewallcomments:hover
        {
            float: right;
            color: #666666;
        }
    </style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">    
    <asp:HiddenField ID="hfCurrentData" runat="server" />
    <asp:HiddenField ID="hfCountData" runat="server" />
    <asp:HiddenField ID="hfCountChanges" runat="server" />
    <asp:HiddenField ID="hfcommentscount" runat="server" />

    <div id="liveFeedHeaderCtWrap" style="position: fixed; width: 100%; margin-top: -80px;
        z-index: 100;">
        <div id="the-feeds" style="padding-right: 12px;" class="tastygoHeadingWrapper">
            <a href="#">
                <h1 style="font-size: 18px; font-family: Helvetica; font-weight: bold;" class="tastygoHText floatLeft">
                    <em>Live</em> <font style="color: Black">Feed</font></h1>
            </a>
            <ul class="socialTools">
                <li><span class="selector dIB selected" id="showEverything"><span class="text">Fitness
                    &amp; Sports </span><span class="removeFilter tastygoShopSprite">&nbsp;&nbsp;</span></span></li>
                <li><span class="downIcon  dIB"><span class="downArrow"></span></span></li>
                <li><span class="separator dIB"></span></li>
                <li><span class="selector dIB" id="showTrending">Popular</span></li>
                <li><span class="separator dIB"></span></li>
                <li><span class="selector dIB" id="showFriends">Friends</span></li>
                <li><span class="separator dIB"></span></li>
                <li><span class="selector dIB" id="showProfile">Me</span></li>
                <li><span class="separator showFriendsProfileSeparator dIB"></span></li>
                <li><span style="display: none;" class="selector dIB" id="showFriendsProfile">378</span></li>
                <li id="feed_uni_loader" style="margin-left: 15px; margin-top: 6px; display: none;">
                </li>
            </ul>
            <div class="floatRight feedRgtBlk">
                <span style="line-height: 20px;" class="floatLeft"><a href=""><span class="editSS  round5 confrmB">
                    <span class="tastygoShopSprite preferences"></span></span></a></span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div style="clear: both; position: fixed; margin-bottom: 0px; overflow: hidden; margin-top: -28px;
        color: Black; width: 100%; text-align: center; font-size: 16px; z-index: 100;">
        <a id="ReFetchUpdates" style="color: Black !important; width: 150px; height: 50px;
            background-color: #E7E7E7;" onclick="javascript:GetandupdateFeeds();" href="javascript:void(0);">
            No new update</a>
    </div>
    <div id="tastygofbbanner">
        <div class="fBbannerBlock">
            <span class="tastygoShopSprite fbBannerCross" alt="cross" title="Close "></span>
            <span class="tastygoShopSprite fbBannerFbIcon"></span>
            <h4>
                New! <span class="tastygoShopSprite trumpetNewIcon"></span>Shop With Your Facebook
                Friends</h4>
            <span style="float: left; font-size: 20px; margin-left: 10px; margin-top: 11px; font-family: Helvetica;">
                <a href="#" class="learnmore">Learn More</a></span><span class="tryItOutButton" style="float: right;
                    margin-right: 15px;"><a href="#">Try it Out!</a></span>
        </div>
    </div>
    <ul id="htmlArea" style="clear: both; overflow: hidden;" class="image-grid">
    </ul>
    <div id="fb-root">
    </div>
    <div id="postcomments" style="position: fixed; display: none; width: 1000px !important;
        z-index: 1002; left: 25% !important; top: 20%; overflow: auto; height: 500px;
        background: white; padding: 10px;">
        <div id="tastygoModalContent">
            <div>
                <div class="commentToolHeader">
                    <div class="imageCt">
                        <img class="popupimg" src=""></div>
                    <div class="content">
                        <h3>
                            Add a comment on
                        </h3>
                        <div id="title">
                            <div>
                                Ali Raza Test</div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                <div class="footer" style="margin-top: 10px">
                    <span><a class="viewallcomments" href="/inspiration/geometric-ring-unisex">View all
                        comments</a></span>
                    <div id="comments">
                        <div class='fb-comments' href="http://www.demo.tazzling.com" data-num-posts='2'>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
