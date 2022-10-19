<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspiration.aspx.cs" Inherits="Inspiration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="js/jquery-1.4.1-and-plugins.min.js"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <link href="css/Feeds.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.simplemodal.js" type="text/javascript"></script>
    <link href="CSS/Inspiration.css" rel="stylesheet" type="text/css" />
    <script src="//connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <script src="http://twitterjs.googlecode.com/svn/trunk/src/twitter.min.js"></script>
    <script src="JS/jquery.scrollTo.js" type="text/javascript"></script>
    <script type="text/javascript">        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));</script>
    <script type="text/javascript">
        !function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (!d.getElementById(id)) {
                js = d.createElement(s);
                js.id = id; js.src = "https://platform.twitter.com/widgets.js";
                fjs.parentNode.insertBefore(js, fjs);
            } 
        }
         (document, "script", "twitter-wjs");
    </script>
    <title>tastygo | Inspiration</title>
</head>
<body>
    <div id="wrapper">
        <div id="tastygoHeaderPhantomDiv" style="height: 54px">
        </div>
        <div id="inspirationHeaderPhantomDiv" style="height: 48px">
        </div>
        <div id="mainContent" class="poRel">
            <div class="fixedContent fullWidthContent" id="outerContentFixed">
                <div id="innerContentFixed" class=" innerContentFWidth">
                    <div id="successMsg" style="display: none;">
                        <span class="tickMark ticIcon tastygoShopSprite"></span><span class="successMsgText">
                            Marked as inappropriate. </span>
                    </div>
                    <div class="innerContent">
                        <div class="leftBlock floatLeft">
                            <div class="imgContainer filler">
                                <div style="line-height: 610px;">
                                    <img src="<%= imagePath %>" style="vertical-align: middle; width: 612px; height: 612px;"
                                        alt="" title="" />
                                </div>
                                <div class="imgButtons">
                                    <a class="iSpmImg" href="javascript: void(0);" title="Mark as inappropriate" alt="Mark as inappropriate">
                                        <span class="iSpmImgIn tastygoShopSprite"></span></a>
                                </div>
                            </div>
                            <div class="rBcomntCount">
                                <a id="bookmark_cmnt"><span class="comnt tastygoShopSprite"></span></a><strong class="color999">
                                    <fb:comments-count href="http://www.demo.tazzling.com/dealid=<%= dealId %>"></fb:comments-count>
                                </strong><span class="comntName" style="display: inline">&nbsp;Comments</span>
                            </div>
                            <br clear="all" />
                            <div id="fbComments">
                                <fb:comments href="http://www.demo.tazzling.com/dealid=<%= dealId %>" num_posts="3"
                                    width="612" migrated="1"></fb:comments>
                            </div>
                        </div>
                        <div class="rightBlock floatLeft">
                            <h2 class="title">
                                <%= title%></h2>
                            <span class="addedVia">Added 3 months ago by <a href="/ShanahZ/">ShanahZ</a> via Mr.
                                Rootbeer </span><span class="moreImgDetails"><span class="blockquote tastygoShopSprite marginRight5">
                                </span>
                                    <%= Discription%>
                                </span>
                            <div class="viewCmnttastygo">
                                <ul>
                                    <li><span class="viewIcon icon tastygoShopSprite" title="Total Views"></span><span><span
                                        class="viewCount" title="Total Views">
                                        <%=Purchase %></span> <span class="viewName" title="Total Views">Sold</span> </span>
                                    </li>
                                    <li><a href="#facebook" id="favInspImg" rel="async" style="cursor:pointer" id="tastygoImg" title="FAVE this photo"><span
                                        class="tastygoIcon icon tastygoShopSprite"  onclick="javascript:favbuttonclick(this)"> 
                                    </span></a><span class="tastygoCount">
                                        <%=Favourite %></span> <span class="tastygoName">FAVES</span> </li>
                                    <li><a class="scrollPage" id="comment" style="cursor:pointer"><span class="comntIcon icon tastygoShopSprite"
                                        title="Comment on this photo"></span></a><span><span class="comntCount">
                                            <fb:comments-count href="http://www.demo.tazzling.com/dealid=<%= dealId %>"></fb:comments-count>
                                        </span><span class="comntName" id="comment_pcount">COMMENTS</span> </span></li>
                                </ul>
                            </div>
                            <div class="priceTagInspImageBlock">
                                <div class="priceTagInspImageText">
                                    <h1 class="fontB color333 font22">
                                        $<%= sellingPrice%><span class="priceTagInspImageBlockTagIcon tastygoShopSprite"></span>tastygo</h1>
                                    <h3>
                                        <span style="text-decoration: line-through;">$<%= valuePrice%>
                                            retail price</span></h3>
                                </div>
                                <a href="/sale/9269/product/136410/" class="tastygoGrad borderR5 font14">Buy Now!</a>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="shareOption">
                                <ul class="shareButtons floatLeft">
                                    <li>Share</li>
                                    <li><a target="_blank" href="https://twitter.com/share" data-dnt="true" data-count="none"
                                        class="twIconNew tastygoShopSprite textHide " data-lang="en" title="Share on Twitter">
                                        Share on Twitter</a></li>
                                    <li><a href="#" class="fbIconNew tastygoShopSprite textHide" onclick='window.open("http:\/\/www.facebook.com\/sharer.php?u=" + window.location.href +"&v=112?fref=fb", "my_window", "height=440,width=620,scrollbars=true");return false;'
                                        title="Share on Facebook">Share on Facebook</a></li>
                                    <li><a href="#" class="tumblrIconNew tastygoShopSprite textHide" onclick='window.open("#", "my_window", "height=440,width=620,scrollbars=true");return false;'
                                        title="Share on Tumblr">Share on Tumblr</a></li>
                                    <li style="max-width: 85px; margin-top: 4px;">
                                        <fb:like href="http://www.demo.tazzling.com/dealId=<%=dealId %>" width="100" show_faces="false"
                                            layout="button_count"></fb:like>
                                    </li>
                                    <li style="width: 60px; margin-top: 4px; margin-right: 5px;"><a onclick="return false"
                                        href="#" class="pin-it-button" count-layout="horizontal">
                                        <img border="0" src="//assets.pinterest.com/images/PinExt.png" title="Pin It" /></a>
                                    </li>
                                    <span onclick="pinterestShare(); window.open(#); return false" style="float: left;
                                        position: relative; right: 65px; width: 43px; height: 20px; top: 4px; background: transparent;
                                        margin-right: -42px; cursor: pointer" title="Pin it on Pinterest"></span>
                                </ul>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="postcomments" style="position: fixed; display: none; width: 1000px !important;
        z-index: 1002; left: 25% !important; top: 20%; overflow: auto; height: 500px;
        background: white; padding: 10px;">
        <div id="tastygoModalContent" style="height:200px !important;">
            <div>
                <div class="commentToolHeader">
                    <div class="imageCt">
                        <img class="popupimg" style="width:180px" src="<%= imagePath %>"></div>
                    <div class="content">
                        <h3>
                            Add your tastygo favorites to your Facebook Timeline
                        </h3>
                        <div id="title">
                            <div>
                                Share your favorite design finds with your friends</div>
                        </div>
                    </div>
                    <span><span style="vertical-align: top; margin-right: 10px; float: left; margin-left: 95px;"
                        class="tastygoShopSprite fbTimeLine fbTimeLineProdTMLine dIB"></span><a id="A1" style="text-decoration: underline;
                            line-height: 21px;" class="color333 fontB font13 modelhover" onclick='window.open("http:\/\/www.facebook.com\/sharer.php?u=" + window.location.href +"&v=112?fref=fb", "my_window", "height=440,width=620,scrollbars=true");return false;'>Add to Timeline</a> </span>
                    <div style="clear: both;">
                    </div>
                </div>
                <div class="footer">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
       function favbuttonclick() {
           var DealID = '<%= dealId %>';
           $.ajax({
               type: "POST",
               url: "Inspiration.aspx?DealID=" + DealID,
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

         //  alert(DealID);
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
    </script>
    <style type="text/css">
    
    .modelhover:hover{
    color:red;
    cursor:pointer;
    }
    </style>
  <script type="text/javascript">
  
   $(document).ready(function() {


$("#comment").click(function () {
    $(document).scrollTo('#fbComments');

});







 });

  </script>
</body>
</html>
