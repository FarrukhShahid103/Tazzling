<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="Default10.aspx.cs"
    Inherits="Default10" Title="Untitled Page" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <link href="CSS/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="CSS/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery.colorbox.js"></script>
       
    <link href="CSS/emailpopup.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .LayerOnImage
        {
            background-image: url(                                       'Images/DivBg2.png' );
            background-repeat: repeat-x;
            font-family: Tahoma;
            position: absolute;
            height: 18px;
            font-size: 12px;
            font-weight: bold;
            margin-left: 268px;
            text-align: center;
            margin-top: 315px;
            width: 464px;
            color: #F16D00;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript">
    $(document).ready(function() {
    $('#imggoogle').tipsy({gravity: 's', html:true});                
    $("#divCauseLong").click(function(){
	
		// Toggle the bar up 
		$("#divLongCauseDescription").slideToggle();
		});
    
     $('#txtMessage').keypress(function(e){
          if(e.which == 13){
           $('#btnsendEmail').click();
           return false;
           }
      });
      $('#TxtToEmail').keypress(function(e){
          if(e.which == 13){
           $('#btnsendEmail').click();
           return false;
           }
      });           
    
    $('#btnsendEmail').click(function() {
         var isValidated = true;
         // validate Email                  
         var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;       
            if ($("#TxtToEmail").val() == '' || !filter.test($("#TxtToEmail").val())) {
             $("#TxtToEmail").removeClass("input").addClass("EmailInputError");  
             isValidated = false;
                }              
                if (isValidated) {            
                 $(".emailmenudropped").toggle("fast");
                MyAlert('success', "Your message has been sent successfully.");
                var message=$("#txtMessage").val();
                var toEmail=$("#TxtToEmail").val();                
                 $("#txtMessage").val("");
                 $("#TxtToEmail").val("");                                          
                    $.ajax({
                        type: "POST",
                        url: "getStateLocalTime.aspx?TrackFriendEmail=" + toEmail + "&TrachFriendMessage=" + message+"&TrachFriendTitle="+document.title+"&TrachDealID="+$("#ctl00_ContentPlaceHolder1_hfCurrentDealId").val(),
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        cache: false,
                        success: function(msg) {
                           
                             }

                    });
                 
  }
 
 
 
 
 
});
    
    
    
        $(".emaildownMenu").click(function(){
         var SiginInCheck = $("#hlMemberArea2").val();
            if(typeof SiginInCheck  == "undefined")
            {
                MyAlert('alert', "Login your account to keep your referral credits tracked!");
                
                return false;
            }
            else
            {
		        $(".emailmenudropped").toggle("fast");
		    }
	    });

        $("#hlShareReferral").click(function(e) {
        var SiginInCheck = $("#hlMemberArea2").val();
        if(typeof SiginInCheck  == "undefined")
        {
            MyAlert('alert', "Login your account to refer your friends!");          
            return false;
        }         
        });
        
          $("#linkReferral").click(function(e) {
        var SiginInCheck = $("#hlMemberArea2").val();
        if(typeof SiginInCheck  == "undefined")
        {
            MyAlert('alert', "Login your account to refer your friends!");            
            return false;
        }         
        });
        
        $("#Label6").click(function(e) {
        var SiginInCheck = $("#hlMemberArea2").val();
        if(typeof SiginInCheck  == "undefined")
        {
            MyAlert('alert', "Login your account to refer your friends!");            
            return false;
        }         
        });
        
              
        
    });
    
   
    
    </script>
   
    <script type="text/javascript">

        /*** 
        Simple jQuery Slideshow Script
        Released by Jon Raasch (jonraasch.com) under FreeBSD license: free to use or modify, not responsible for anything, etc.  Please link out to me if you like it :)
        ***/

        function slideSwitch() {
            var $active = $('#slideshow IMG.active');

            if ($active.length == 0) $active = $('#slideshow IMG:last');

            // use this to pull the images in the order they appear in the markup
            var $next = $active.next().length ? $active.next()
        : $('#slideshow IMG:first');

            // uncomment the 3 lines below to pull the images in random order

            // var $sibs  = $active.siblings();
            // var rndNum = Math.floor(Math.random() * $sibs.length );
            // var $next  = $( $sibs[ rndNum ] );


            $active.addClass('last-active');

            $next.css({ opacity: 0.0 })
        .addClass('active')
        .animate({ opacity: 1.0 }, 1000, function() {
            $active.removeClass('active last-active');
        });
        }

        $(function() {
            setInterval("slideSwitch()", 5000);
        });


    </script>

    <style type="text/css">
        /*** set the width and height to match your images **/#slideshow
        {
        }
        #slideshow IMG
        {
            position: absolute;
            top: 0;
            left: 268px;
            z-index: 8;
            opacity: 0.0;
        }
        #slideshow IMG.active
        {
            z-index: 10;
            opacity: 1.0;
        }
        #slideshow IMG.last-active
        {
            z-index: 9;
        }
    </style>
    <style type="text/css">
        @import "CSS/jquery.countdown.css";
        #defaultCountdown
        {
            width: 240px;
            height: 45px;
        }
        #defaultCountdown2
        {
            width: 200px;
            height: 45px;
        }
    </style>

    <script type="text/javascript">           
        function ShowAddressPopUp() {
            var heightList = "326px";
            var totalCount = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_hfPopUpRowsCount").value);

            if (totalCount == 1)
                heightList = "292px";
            else if (totalCount == 2)
                heightList = "326px";
            else if (totalCount == 3)
                heightList = "392px";
            else if (totalCount == 4)
                heightList = "441px";
                else if (totalCount == 5)
                heightList = "521px";

            jQuery(document).ready(function() {
                $(document).ready(function() {
                    $.colorbox({
                        scrolling: false,
                        initialWidth: 1,
                        initialHeight: 1,
                        inline: true,
                        width: "677px",
                        height: heightList,
                        href: "#divPriceList",
                        opacity: 0
                    });
                });
            });

            $(document).ready(function() {
                $("#ctl00_ContentPlaceHolder1_imgBtnOk").click(function() {
                    setTimeout("$.colorbox.close();", 313);
                })
            });
        }

    </script>

    <script type="text/javascript" src="JS/jquery.countdown.js"></script>

    <script src="JS/jquery-ui-1.8.7.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">
    $(document).ready(function() {
     $('#ctl00_ContentPlaceHolder1_ctrlDiscussion1_txtComment').tipsy({ gravity: 's' });
         
      $("#ctl00_ContentPlaceHolder1_ctrlDiscussion1_hLinkSignIn").click(function(e) {
            e.preventDefault();
            $("fieldset#signin_menu").toggle();
            $(".signin").toggleClass("menu-open");
            $('html, body').animate({scrollTop:0}, 'slow');
            return false;
        }); 
         });
         
            function EmptyFieldvalidate(oSrc, args) {        
            if (args.Value != "") {                  
                args.IsValid = true;
                return;        
            }
            else
            {
                 $("#"+oSrc.controltovalidate).addClass("DiscussionError");                                                                                          
                 args.IsValid = false;
                 return; 
            }
                       
        }
        
         function validateEmptyField(txtComment) {   
                        
            if ($("#"+txtComment).val() != "") {                                 
                return true;        
            }
            else
            {
                 $("#"+txtComment).addClass("DiscussionError");                                                                                                           
                 return false; 
            }
           }
           
            function hideShowDiv(divID,imgID) {                                
               var Commentclass = document.getElementById(divID).getAttribute("class");              
               if(Commentclass  == "hideComment")      
               {                     
                    $("#"+divID).show('slow');                 
                    var textAreaID=divID.replace("pnlFooter","txtSubComment");                         
                    //$(window).scrollTop($("#"+divID).offset().top-300); 
                    $('html, body').animate({ scrollTop: $("#"+divID).offset().top -300}, 'slow')
                    $("#"+textAreaID).focus();    
                    document.getElementById(divID).setAttribute("class","showComment")                
                    $("#"+imgID).attr("src", "images/hide_comment.png");

              }
              else
              {
                $("#"+divID).hide('slow'); 
                document.getElementById(divID).setAttribute("class","hideComment");                              
                $("#"+imgID).attr("src", "images/comment_reply.png");
              }              
           }
                                                           
        
    </script>

    <asp:Literal ID="ltSlidebox" runat="server"></asp:Literal>
    <asp:Literal ID="ltCountDown" runat="server"></asp:Literal>
    <asp:Literal ID="ltCountDown2" runat="server"></asp:Literal>
    <div style="clear: both; position: relative;">
        <div style="position: absolute; left: 460px; top: -20px; z-index: 100;">
            <img id="Image4" src="Images/speaker.png" />
        </div>
    </div>
    <div style="padding-bottom: 40px;">      
        <asp:HiddenField ID="hfPopUpRowsCount" runat="server" Value="0" />
        <asp:HiddenField ID="hfCurrentDealId" runat="server" />
        <div>
            <asp:UpdatePanel ID="upGridPriceList" runat="server">
                <ContentTemplate>
                    <div style="float: left;">
                        <div style="float: left; padding-left: 5px;">
                            <div style="display: none">
                                <div id="divPriceList">
                                    <div style="padding-top: 30px; padding-left: 19px;">
                                        <div id="divPriceList12" runat="server">
                                            <div style="clear: both;">
                                                <div style="float: left; padding-top: 5px; line-height: 20px;">
                                                    <asp:Label EnableViewState="false" ID="Label10" Font-Bold="true" ForeColor="#97C717"
                                                        Font-Size="19px" runat="server" Text="Choose your deal:"></asp:Label><asp:HiddenField
                                                            ID="hfAddressID" runat="server" />
                                                </div>
                                            </div>
                                            <div style="clear: both; padding-top: 10px;">
                                                <div style="float: left; padding-top: 5px; line-height: 20px;">
                                                    <asp:Label EnableViewState="false" ID="Label11" ForeColor="#F99D1C" Font-Size="16px"
                                                        runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div style="clear: both; padding-top: 11px;">
                                                <asp:GridView EnableViewState="false" ID="grdViewPrices" runat="server" DataKeyNames="dealId"
                                                    Width="592px" AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False"
                                                    ShowHeader="False" ShowFooter="true" GridLines="None">
                                                    <RowStyle CssClass="gridText" Font-Size="15px" Height="26" HorizontalAlign="Left" />
                                                    <AlternatingRowStyle CssClass="AltgridText" Font-Size="15px" Height="26" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 72px;">
                                                                    <tr>
                                                                        <td width="80%" style="padding-left: 8px;">
                                                                            <asp:Label EnableViewState="false" ID="lblDealSubTitle" runat="server" Text='<% # Eval("title") %>'></asp:Label>
                                                                            <br />
                                                                            <div style="color: #97C717; font-size: 13px; font-weight: bold;">
                                                                                Value <font style="font-style: italic; color: Black;">
                                                                                    <%# "C$ " + Eval("valuePrice")%></font> - Discount <font style="font-style: italic;
                                                                                        color: Black;">
                                                                                        <%# Convert.ToDouble(Convert.ToDouble(100 / Convert.ToDouble(Eval("valuePrice"))) * (Convert.ToDouble((Eval("valuePrice"))) - Convert.ToDouble(Eval("sellingPrice")))).ToString("###.00") + "% off "%></font>
                                                                                - You Save <font style="font-style: italic; color: Black;">
                                                                                    <%# "C$ " + (Convert.ToInt32(Eval("valuePrice")) - Convert.ToInt32(Eval("sellingPrice"))).ToString()%></font>
                                                                            </div>
                                                                        </td>
                                                                        <td style="padding-right: 5px; padding-left: 11px;">
                                                                            <div style="background-image: url(Images/buyPrice.png); width: 76px; height: 48px;
                                                                                text-align: center; color: White; font-size: 16px;">
                                                                                <div style="padding-top: 12px;">
                                                                                    <font style="font-weight: bold; font-size: 19px;"><a href='<%# ConfigurationManager.AppSettings["YourSite"]+ "/checkout.aspx?did=" + Eval("dealId") %>'
                                                                                        style="text-decoration: none; color: White;">C$
                                                                                        <%# Eval("sellingPrice")%></a></font></div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="clear: both; padding-left: 8px;">
            <table border="0px" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="background-color: #00AEFF; vertical-align: top;">
                        <div>
                            <div style="clear: both;">
                                <div style="float: left; margin: 0px; background-image: url(Images/todayDealTop_New.jpg);
                                    background-repeat: no-repeat; height: 47px; width: 474px;">
                                    <div style="padding-top: 13px; padding-left: 200px;">
                                        <div style="float: left; padding-right: 5px;">
                                            <a id="linkGift" href='<%=strCheckOutLink %>'>
                                                <img id="img7" width="22px" height="24px" src="Images/giftCheckOut.png" title="Buy as Gift" />
                                            </a>
                                        </div>
                                        <div style="float: left; padding-right: 5px;">
                                            <a id="linkReferral" target="_blank" href='<%=strReferralLink %>'>
                                                <img id="img6" width="22" src="Images/dollarRight.png" title="Referral" />
                                            </a>
                                        </div>
                                        <div style="float: left; padding-right: 5px;">
                                            <a id="linkFacebook1" target="_blank" href='<%= "http://www.facebook.com/sharer.php?u="+strShareLink %>'>
                                                <img id="img1" width="21" src="Images/fbHomeRight.jpg" title="Share on Facebook" />
                                            </a>
                                        </div>
                                        <div style="float: left; padding-right: 6px;">
                                            <a id="linkTweeter1" target="_blank" href='<%= "http://twitter.com/share?url="+strShareLink%>'>
                                                <img id="img2" width="21" src="Images/twHomeRight.jpg" title="Share on Twitter" />
                                            </a>
                                        </div>
                                        <div style="float: left; padding-right: 6px;">
                                            <div class="emaildownMenu" style="cursor: pointer;">
                                                <img id="img3" width="27" src="Images/EmailHomeRight.jpg" title="Email to a Friend" />
                                            </div>
                                            <div style="clear: both; position: relative;">
                                                <div class='emailmenudropped'>
                                                    <div class='textleft'>
                                                        <div style="text-align: left;">
                                                            To: (email address)
                                                        </div>
                                                        <div style="float: left;">
                                                            <input type="text" class="EmailInput" onfocus="this.className='EmailInput'" style="height: 30px;"
                                                                id="TxtToEmail" />
                                                        </div>
                                                        <div style="text-align: left;">
                                                            Message: (Optional)
                                                        </div>
                                                        <div style="float: left;">
                                                            <textarea class="EmailInput" onfocus="this.className='EmailInput'" id="txtMessage"
                                                                style="height: 70px;"></textarea>
                                                        </div>
                                                        <div style="text-align: left; padding-top: 5px; clear: both;">
                                                            <input type="button" class="BtnSend" id="btnsendEmail" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                     
                                        <div style="float: left;">
                                            <a id="linkRssFeed" target="_blank" href='<%=strRSSLink %>'>
                                                <img id="img4" src="Images/feed_icon.png" title="Subscribe to RSS Feed" width="21" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; margin: 0px;">
                                    <a id="hlShareReferral" href='<%=strShareReferal %>'>
                                        <img onmouseover="src='Images/refer-to-friend.gif';" onmouseout="src='Images/imgReferGer10.jpg';"
                                            src="Images/imgReferGer10.jpg" />
                                    </a>
                                </div>
                            </div>
                            <div style="clear: both; width: 724px; background-color: #f0f8fe; padding-top: 10px;
                                padding-left: 8px;">
                                <div style="padding-bottom: 10px; line-height: normal; clear: both;">
                                    <asp:Label EnableViewState="false" ID="lblTopTitle" runat="server" Font-Names="Tahoma"
                                        ForeColor="#9f9f9f" Font-Size="34px" Font-Bold="false" Text="$5 for $10 Worth of Signature Waffles, Sandwiches, Coffee at WE. Coffee"></asp:Label>
                                </div>
                                <div style="padding-bottom: 10px; line-height: normal; clear: both;">
                                    <a href='<%=strCheckOutLink%>' style="font-family: Tahoma; color: #0a3b5f; font-size: 30px;
                                        text-decoration: none;">
                                        <%=strDealTitle%></a>
                                </div>                               
                            </div>
                            <div style="clear: both; padding-top: 5px;">
                            </div>
                            <div style="position: relative;">
                                <div style="position: absolute; left: -45px; top: 173px; z-index: 100;">
                                    <img id="Image3" runat="server" src="Images/clock2Top.png" />
                                </div>
                                <div style="position: absolute; left: 160px; top: 10px; z-index: 100;">
                                    <a id="btnBuyDeal" href='<%=strCheckOutLink %>'>
                                        <img id="Img12" onmouseover="src='Images/btnBuyBigHover.png';" onmouseout="src='Images/btnBuyBig.png';"
                                            src="Images/btnBuyBig.png" />
                                    </a>
                                </div>
                                <div style="float: left; background-image: url(images/bgDealDetail_Left.jpg); background-repeat: no-repeat;
                                    height: 333px; width: 269px;">
                                    <div style="padding-top: 40px; padding-left: 20px; min-height: 25px;">
                                        <a id="imgBuy" href='<%= strCheckOutLink%>' class="TextWithSize40pxAndShadow">
                                            <%= strSellingPrice %></a>
                                    </div>
                                    <div style="padding-top: 70px;">
                                        <div style="float: left; width: 87px;" align="center">
                                            <asp:Label EnableViewState="false" ID="lblValue" runat="server" Text="$10" CssClass="TextWithSize25pxAndShadow"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 87px;" align="center">
                                            <asp:Label EnableViewState="false" ID="lblDiscount" runat="server" CssClass="TextWithSize25pxAndShadow"
                                                Text="50%"></asp:Label>
                                        </div>
                                        <div style="float: left; width: 87px;" align="center">
                                            <asp:Label EnableViewState="false" ID="lblSave" runat="server" CssClass="TextWithSize25pxAndShadow"
                                                Text="C$200"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="padding-top: 80px; padding-left: 47px;">
                                        <div id="defaultCountdown" style="width: 240px;" align="center">
                                        </div>
                                    </div>
                                    <div style="padding-top: 14px; width: 269px">
                                        <div>
                                            <div style="float: left; padding-left: 6px;">
                                                <img id="imgDealOnOff" src='<%=strImageOnOff %>' />
                                            </div>
                                            <div style="float: left; padding-left: 10px; margin-top: -5px;">
                                                <asp:Label EnableViewState="false" ID="lblDealTotal2" runat="server" CssClass="TextWithSize15pxAndShadow"
                                                    Text="Deal is on"></asp:Label>
                                            </div>
                                            <div style="float: right; margin-top: -5px; padding-right: 10px;">
                                                <asp:Label EnableViewState="false" ID="lblDealTotal" runat="server" ForeColor="#feb75b"
                                                    CssClass="TextWithSize15pxAndShadow" Text="25 bought"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div style="margin-left: 18px; padding-top: 0px;">
                                            <div style="margin-left: 30px; padding-top: 0px; padding-bottom: 5px; color: White;
                                                font-size: 12px; font-family: Tahoma;">
                                                <asp:Label EnableViewState="false" runat="server" ID="lblDealPurchaseDetailBottom"></asp:Label>
                                            </div>
                                            <asp:Literal ID="ltProgressBarImage" runat="server"></asp:Literal>
                                            <div class="demo" style="width: 200px; margin-left: 30px;">
                                                <div id="progressbar">
                                                </div>
                                                <div style="padding-top: 3px;">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left;">
                                        <asp:Literal ID="ltSlideShow" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div style="padding-top: 5px;">
                                </div>
                                <div style="clear: both; width: 702px; background-color: #E9E9E9; overflow: auto;
                                    padding-top: 25px; padding-left: 30px; padding-bottom: 10px;">
                                    <div style="width: 45%; float: left;">
                                        <div style="padding-bottom: 15px;">
                                            <asp:Label EnableViewState="false" ID="lblFinePrintHeading" Font-Bold="true" Font-Names="Tahoma"
                                                ForeColor="#0a3b5f" Font-Size="19px" runat="server" Text="The Fine Print"></asp:Label>
                                        </div>
                                        <div style="text-decoration: smooth; font-family: Arial;">
                                            <asp:Label EnableViewState="false" ID="lblFinePrintText" Font-Size="12px" ForeColor="#676767"
                                                runat="server" Text="Fine Printer text will be displayed here"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="width: 45%; float: right; padding-right: 20px;">
                                        <div style="padding-bottom: 20px;">
                                            <asp:Label EnableViewState="false" ID="lblHighlightsHeading" Font-Bold="true" Font-Names="Tahoma"
                                                ForeColor="#0a3b5f" Font-Size="19px" runat="server" Text="Highlights"></asp:Label>
                                        </div>
                                        <div style="font-family: Arial;">
                                            <asp:Label EnableViewState="false" ID="lblHighlightsText" Font-Size="12px" ForeColor="#676767"
                                                runat="server" Text="Highlights text will be displayed here"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both; width: 732px;">
                                    <div style="clear: both;">
                                        <div style="float: left;">
                                            <img id="stripAboutDeal" src="Images/stripAboutTheDeal.jpg" alt="" />
                                        </div>
                                        <div id="DivGoogleMapHeader" runat="server" style="float: left;">
                                            <img id="stripLocation" src="Images/stripLocation.jpg" alt="" />
                                        </div>
                                    </div>
                                    <div style="clear: both;">
                                        <div style="float: left; width: 438px; padding: 10px; background-color: #F0F8FD">
                                            <div style="clear: both; line-height: 30px;">
                                                <asp:Label EnableViewState="false" ID="lblDealDiscription" runat="server" Text="Deal Discription will be displayed here"></asp:Label>
                                            </div>
                                            <div style="clear: both; padding-top: 5px;">
                                                <div style="padding-top: 10px; padding-left: 20px; line-height: 24px;">
                                                    <asp:Label EnableViewState="false" ID="lblCompanyDetail" Font-Bold="false" ForeColor="#504F4F"
                                                        Font-Size="16px" runat="server" Text="Momentum Grooming Company website 1237 Burrard St.Vancouver, British Columbia V6Z 1Z5 (604) 689-4636"></asp:Label>
                                                </div>
                                                <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 20px; line-height: 24px;">
                                                    <asp:HyperLink ID="hlBusinessURL" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; padding-left: 2px;">
                                            &nbsp;
                                        </div>
                                        <div style="float: left;">
                                            <div id="DivGoogleMap" runat="server" style="width: 248px; height: 220px; padding: 10px;
                                                background-color: #f0f8fe;">
                                                <a id="hlGoogleAddress" target="_blank" href='<%=strGoogleLink %>'>
                                                    <img id="imggoogle" src='<%=strimgGoogle %>' title='<%=strimgGoogleToolTip %>' />
                                                </a>
                                            </div>
                                            <div style='clear: both; margin-top: <%=MarginTop%>'>
                                                <img id="Img8" src="Images/stripPhotos.jpg" alt="" />
                                            </div>
                                            <div style="width: 248px; padding: 10px; background-color: #f0f8fe; text-align: center;">
                                                <asp:Literal ID="ltPhotos" runat="server" Text=""></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both">
                                    <img id="Img9" src="Images/stripDiscuession.jpg" alt="" />
                                </div>
                                <div style="clear: both;">
                                    <div style="width: 730px; border: 1px solid #B7B7B7; clear: both;" class="fontSpaceHeightRegular">
                                        <asp:HiddenField ID="hfDealId" runat="server" />
                                        <asp:UpdatePanel ID="upComment" runat="server">
                                            <ContentTemplate>
                                                <div>
                                                    <asp:DataList ID="rptrDiscussion" RepeatColumns="1" RepeatDirection="Vertical" runat="server"
                                                        CellPadding="0" OnItemDataBound="DataListItemDataBound" CellSpacing="0" Width="730px"
                                                        GridLines="None" ShowHeader="false">
                                                        <ItemTemplate>
                                                            <div style="border-bottom: solid 1px #B7B7B7; background-color: #f0f8fe; width: auto;
                                                                padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                                                                <div style="width: 120px; float: left; text-align: center">
                                                                    <asp:Image ID="imgDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px" BorderStyle="Solid"
                                                                        ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                                    <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("userId")%>' />
                                                                    <asp:HiddenField ID="hfDiscuessionID" runat="server" Value='<%# Eval("discussionId")%>' />
                                                                </div>
                                                                <div style="width: 540px; float: left; text-align: left;">
                                                                    <div style="width: 540px; height: 26px;">
                                                                        <div style="float: left;">
                                                                            <asp:Label ID="label5" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("Name") %>'
                                                                                Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label></div>
                                                                        <div style="float: left; padding-left: 10px;">
                                                                            <asp:Label ID="label6" runat="server" Font-Names="Arial,  sans-serif" Text='<%# "Commented " + Eval("days") + " days, " + Eval("hour") + " hours and " + Eval("min") + " minutes ago" %>'
                                                                                Font-Bold="True" Font-Size="13px" ForeColor="#0976c3"></asp:Label></div>
                                                                        <div style="float: right;">
                                                                            <asp:Image ID="imgCommentReply" ToolTip="Click here to reply" Style="cursor: pointer;"
                                                                                runat="server" ImageUrl="~/Images/comment_reply.png" />
                                                                        </div>
                                                                    </div>
                                                                    <div style="width: 540px; padding-right: 12px;">
                                                                        <asp:Label ID="label7" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("comments")%>'
                                                                            Font-Size="13px" ForeColor="#7C7B7B"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div style="clear: both; background-color: #f0f8fe; width: auto;">
                                                                <div style="float: right;">
                                                                    <asp:DataList ID="rptrSubDiscussion" RepeatColumns="1" RepeatDirection="Vertical"
                                                                        runat="server" CellPadding="0" OnItemDataBound="SubCommentDataListItemDataBound"
                                                                        CellSpacing="0" Width="730px" GridLines="None" OnItemCommand="rptrSubDiscussion_ItemCommand"
                                                                        ShowHeader="false">
                                                                        <ItemTemplate>
                                                                            <div style="border-bottom: solid 1px #B7B7B7; background-color: #f0f8fe; width: auto;
                                                                                padding-left: 100px; padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                                                                                <div style="width: 120px; float: left; text-align: center">
                                                                                    <asp:Image ID="imgSubDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px"
                                                                                        BorderStyle="Solid" ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                                                    <asp:HiddenField ID="hfSubCommentUserID" runat="server" Value='<%# Eval("userId")%>' />
                                                                                    <asp:HiddenField ID="hfSubDiscuessionID" runat="server" Value='<%# Eval("discussionId")%>' />
                                                                                </div>
                                                                                <div style="width: 450px; float: left; text-align: left;">
                                                                                    <div style="width: 450px; height: 26px;">
                                                                                        <asp:Label ID="sublabel5" runat="server" Font-Names="Arial,sans-serif"
                                                                                            Text='<%# Eval("Name") %>' Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                                                ID="sublabel6" runat="server" Font-Names="Arial, sans-serif" Text='<%# "Commented " + Eval("days") + " days, " + Eval("hour") + " hours and " + Eval("min") + " minutes ago" %>'
                                                                                                Font-Bold="True" Font-Size="13px" ForeColor="#0976c3"></asp:Label></div>
                                                                                    <div style="width: 450px; padding-right: 12px;">
                                                                                        <asp:Label ID="sublabel7" runat="server" Font-Names="Arial,sans-serif"
                                                                                            Text='<%# Eval("comments")%>' Font-Size="13px" ForeColor="#7C7B7B"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Panel ID="pnlFooter" runat="server" CssClass="hideComment" Style="display: none;">
                                                                                <div style="border-bottom: solid 1px #B7B7B7; background-color: #f0f8fe; width: auto;
                                                                                    padding-top: 10px; padding-bottom: 10px; overflow: auto;">
                                                                                    <div style="clear: both;">
                                                                                        <div style="width: 500px; float: right;">
                                                                                            <asp:TextBox ID="txtSubComment" title="Add Comments" onfocus="this.className=''"
                                                                                                Width="475px" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div style="clear: both; padding-top: 10px;">
                                                                                        <div style="float: right; padding-right: 23px;">
                                                                                            <asp:ImageButton ID="btnSubCommentPost" CommandName="addComment" CommandArgument='<%# Eval ("pdiscussionId") %>'
                                                                                                runat="server" ImageUrl="~/Images/post.gif" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </FooterTemplate>
                                                                    </asp:DataList>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                                <div style="height: 230px; border-bottom: solid 1px #B7B7B7; background-color: #f5f5f5;
                                                    width: auto">
                                                    <div style="padding-left: 100px; clear: both; padding-top: 5px;">
                                                        <div style="float: left; padding-right: 5px;">
                                                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="Images/error.png" />
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Black" Visible="false" CssClass="fontStyle"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="padding-left: 20px; clear: both; padding-top: 10px; padding-bottom: 10px;">
                                                        <asp:ImageButton ID="hLinkSignIn" runat="server" ImageUrl="~/Images/btnSingInComment.jpg">
                                                        </asp:ImageButton></div>
                                                    <div style="width: 730px; clear: both;">
                                                        <div style="width: 100px; float: left; padding-left: 20px;">
                                                            <div style="clear: both;">
                                                                <asp:Label ID="label3" runat="server" Font-Names="Arial,sans-serif" Text="Comment"
                                                                    Font-Size="15px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>
                                                            </div>
                                                            <div style="clear: both; padding-top: 10px;">
                                                                <asp:Image ID="imgLoginUser" runat="server" BorderColor="#F99D1C" BorderWidth="2px"
                                                                    BorderStyle="Solid" ImageUrl="~/Images/disImg.gif" Width="62px" Height="62px" />
                                                            </div>
                                                        </div>
                                                        <div style="width: 600px; float: right;">
                                                            <asp:TextBox ID="txtComment" title="Add Comments" onfocus="this.className=''" Width="575px"
                                                                Height="103px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                            <cc1:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="EmptyFieldvalidate"
                                                                ControlToValidate="txtComment" Display="none" ValidateEmptyText="true" ValidationGroup="vgComments"
                                                                ErrorMessage="" SetFocusOnError="false"></cc1:CustomValidator>
                                                        </div>
                                                    </div>
                                                    <div style="width: 730px; clear: both;">
                                                        <div style="clear: both; padding-top: 20px;">
                                                            <div style="float: right; padding-right: 23px;">
                                                                <asp:ImageButton ID="btnPost" runat="server" ImageUrl="~/Images/post.gif" ValidationGroup="vgComments"
                                                                    CausesValidation="true" OnClick="btnPost_Click" />
                                                            </div>
                                                        </div>
                                                        <div style="width: 63px; float: right;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div style="clear: both; padding-bottom: 20px; background-color: #f0f8fe; padding-top: 20px;
                                    border: 1px solid #B7B7B7; border-top: none; width: 730px;" align="center">
                                    <div class="bottomDivWithClock">
                                        <div style="padding-left: 20px; padding-top: 10px;">
                                            <div style="float: left; padding-top: 25px; padding-right: 20px; padding-bottom: 10px;">
                                                <asp:Label EnableViewState="false" ID="lblDealPrice" runat="server" CssClass="TextWithSize50pxAndShadow"></asp:Label>
                                            </div>
                                            <div style="float: left; padding-right: 50px;">
                                                <a id="hlBuyBottom" href='<%=strCheckOutLink %>'>
                                                    <img id="Image1" onmouseover="src='Images/btnBuyBigHover.png';" onmouseout="src='Images/btnBuyBig.png';"
                                                        src="Images/btnBuyBig.png" /></a>
                                            </div>
                                            <div style="float: left; padding-right: 10px;">
                                                <img id="imgClock" src="Images/dealClock.png" />
                                            </div>
                                            <div style="float: left">
                                                <div style="clear: both;">
                                                    <asp:Label EnableViewState="false" ID="Label12" runat="server" Text="Time Left to Buy"
                                                        CssClass="TextWithSize17pxAndShadow"></asp:Label></div>
                                                <div style="clear: both; padding-top: 20px; padding-left: 10px;">
                                                    <div id="defaultCountdown2" style="width: 200px;" align="center">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td style="padding-left: 5px;">
                    </td>
                    <td style="background-color: #6cc4ff; vertical-align: top;">
                        <div>
                            <asp:Panel ID="pnlDealCause" runat="server" Visible="false">
                                <div style="clear: both; background-color: #f0f8fe; height: 30px; width: 100%;">
                                    <div style="padding-top: 5px; width: 100%; text-align: center;">
                                        <asp:Label EnableViewState="false" ID="lblCauseHeadingTop" Font-Size="22px" ForeColor="#0076cd"
                                            Font-Bold="true" runat="server" Text="Local Causes"></asp:Label>
                                    </div>
                                </div>
                                <div style="clear: both; padding-top: 5px; width: 226px; padding-left: 5px; padding-right: 5px;
                                    background-color: #1397ed; background-image: url(Images/bg-blue.png); background-repeat: repeat-x;
                                    background-position: bottom;" align="center">
                                    <div>
                                        <div style="clear: both; text-align: center;">
                                            <a id="hlCauseImage" href='<%=strCauseLink%>' target="_blank">
                                                <img id='imgCause' width="214px" height="155px" src='<%=strCauseImage %>' />
                                            </a>
                                        </div>
                                        <div style="clear: both; padding: 5px 0px 5px 0px; text-align: left;">
                                            <asp:Label EnableViewState="false" ID="lblCauseTitle" runat="server" Font-Bold="true"
                                                ForeColor="White" Font-Size="16px"></asp:Label>
                                        </div>
                                        <div style="clear: both; padding: 0px 0px 5px 0px; text-align: left;">
                                            <asp:Label EnableViewState="false" ID="lblCauseShortDescription" runat="server" ForeColor="White"
                                                Font-Size="12px"></asp:Label>
                                        </div>
                                        <div style="clear: both; padding: 0px 0px 5px 0px; text-align: left;">
                                            <div id="divCauseLong">
                                                >> More information
                                            </div>
                                        </div>
                                        <div id="divLongCauseDescription" style="display: none;">
                                            <div style="clear: both; padding: 5px 0px 5px 0px; text-align: left;">
                                                <asp:Label EnableViewState="false" ID="lblCauseLongDescription" runat="server" ForeColor="White"
                                                    Font-Size="12px"></asp:Label>
                                            </div>
                                            <div style="clear: both; padding: 0px 0px 5px 0px; text-align: left;">
                                                <a id="hlCauseSiteURL" style="color: White; text-decoration: none; font-size: 14px;"
                                                    href='<%=strCauseLink%>' target="_blank">View Website</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div style='clear: both; <%=strpaddingCause%>'>
                            </div>
                            <div style='clear: both; <%=strNewarByImagedisplay %>'>
                                <img id="imgNearBy" src="Images/nearBy.jpg" />
                            </div>
                            <div style="clear: both;">
                                <asp:GridView EnableViewState="false" runat="server" ID="gridDeals" AllowPaging="false"
                                    AllowSorting="false" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0"
                                    GridLines="None" HorizontalAlign="Center" ShowHeader="false" RowStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div style="padding-top: 10px;">
                                                    <a href='<%# ConfigurationManager.AppSettings["YourSite"]+ "/default10.aspx?sidedeal=" + Eval("dealId")%>'
                                                        style="text-decoration: none;">
                                                        <div style="height: auto; width: 234px; background-color: #F5F5F5;">
                                                            <div style="height: auto; background-color: #b5dffd; padding-top: 5px; padding-left: 5px;
                                                                padding-right: 5px; padding-bottom: 5px;" align="center">
                                                                <div style="padding-bottom: 5px; color: #909090; font-size: 12px; font-weight: bold;">
                                                                    <%# Eval("topTitle") %>
                                                                </div>
                                                                <div style="color: #0976c3; font-weight: bold; font-size: 14px;">
                                                                    <%# Convert.ToString(Eval("shortTitle")).ToString().Trim()!="" ? Eval("shortTitle"):(Convert.ToString(Eval("dealPageTitle")).ToString().Trim()!="" ? Eval("dealPageTitle"):Eval("title")) %>
                                                                </div>
                                                            </div>
                                                            <div style="position: relative">
                                                                <div style="clear: both; right: 10px; bottom: 10px; position: absolute;">
                                                                    <img src="Images/btn_dealviewSmall.png" />
                                                                </div>
                                                               <%-- <div style="clear: both; padding-top: 11px; padding-left: 4px; padding-right: 3px;
                                                                    padding-bottom: 10px;" align="center">
                                                                    <img id="imgDeal" src='<%# "Images/dealfood/" + DataBinder.Eval (Container.DataItem,"restaurantId").ToString().Trim() + "/" + DataBinder.Eval (Container.DataItem,"image1").ToString().Trim() %>'
                                                                        width="214" height="155" />
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                         
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
