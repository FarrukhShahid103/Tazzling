<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="meetTeam.aspx.cs"
    Inherits="meetTeam" Title="Tastygo | Meet the team" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript" src="js/jquery-1.4.min.js"></script>
<script type="text/javascript" src="JS/jquery.progressbar.js"></script>
    <link href="CSS/tipsy.css" rel="stylesheet" type="text/css" />

    <script src="JS/tipsy.js"></script>

    <link href="CSS/Login.css" rel="stylesheet" type="text/css" />

  

    <script type="text/javascript" src="JS/alertbox.js"></script>

    <link href="CSS/AlertBox.css" rel="stylesheet" type="text/css" />
    
    <style>
        .tooltip
        {
            display: none;
            background: transparent url(Images/black_arrow.png);
            font-size: 11px;
            height: 70px;
            width: 160px;
            padding: 25px;
            color: #fff;
        }
    </style>

    <script src="JS/Teamtooltip.js"></script>

    <script>
    $(document).ready(function(){
    
       $("#demo img[title]").tooltip({
	    effect: 'slide',
	    slideOffset: 10
	    });
	    var images = $(".gallery");
	    images.each(function(i){
		    var orgSrc = $(this).attr('src');
		    var newSrc = $("img[rel='"+$(this).attr('id')+"']").attr('src');
		   // var mydiv = $("div[rel='"+$(this).attr('id')+"']");
		    $(this).hover(
			    function(){
			       // mydiv.fadeIn("fast");
				    $(this).attr('src',newSrc);
			    },
			    function(){
			     //   mydiv.fadeOut("fast");
				    $(this).attr('src',orgSrc);
			    }
		    );
	    });
    });
    </script>

     <div class="PagesBG" style="overflow:hidden;">
        <div>
            <div style="clear: both; float: left;">
                <div id="demo" style="text-align: center;" align="center">
                    <div style="height: auto; width: 630px; border: solid 1px #f99d1c; background-color: #F5F5F5;">
                        <div style="height: 35px; text-align: left; background-color: #E9E9E9; padding-left: 15px;
                            padding-top: 10px;">
                            <asp:Label ID="Label3" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="19px" runat="server"
                                Text="Meet the Team... "></asp:Label>
                        </div>
                        <div style="padding: 10px 15px 10px 15px; line-height: 24px; text-align: justify;">
                            <asp:Label ID="Label4" Font-Bold="false" ForeColor="#056eb7" Font-Size="12px" runat="server"
                                Text="Working at Tastygo is a fun and unique experience involving a highly collaborative team of passionate go-getters. Here at Tastygo we work hard and have fun along the way! We truly care about our business and continually strive to improve the customer buying experience. We take great pride in what we do and value hard work, enthusiasm, originality and teamwork!"></asp:Label>
                        </div>
                    </div>
                    <div style="padding-top: 20px;">
                        <div style="float: left; padding-right: 30px;">
                            <img id="1" src="Images/team/colin1.gif" title="<span style='font-size:20px;'>Colin Cheng</span><br>Chief Executive & Architect Officer<br>Co-Founder"
                                width="128" height="128" class="gallery" /><img rel="1" src="Images/team/colin2.gif"
                                    width="128" height="128" style="display: none" />
                        </div>
                        <div style="float: left; padding-right: 30px;">
                            <img id="2" src="Images/team/shen.gif" title="<span style='font-size:20px;'>Shih Shen</span><br>Chief Financial & Marketing Officer<br>Co-Founder"
                                width="128" height="128" class="gallery" /><img rel="2" src="Images/team/shen3.gif"
                                    width="128" height="128" style="display: none" />
                        </div>
                        <div style="float: left; padding-right: 30px;">
                            <img id="3" src="Images/team/azam1.gif" title="<span style='font-size:20px;'>Sher Azam</span><br>Chief Developer Officer"
                                width="128" height="128" class="gallery" /><img rel="3" src="Images/team/azam2.gif"
                                    width="128" height="128" style="display: none" />
                        </div>
                        <div style="float: left; padding-right: 30px;">
                            <img id="4" src="Images/team/snya4.gif" title="<span style='font-size:20px;'>Sunnya Usman</span><br>Director of Graphics"
                                width="128" height="128" class="gallery" /><img rel="4" src="Images/team/snya3.gif"
                                    width="128" height="128" style="display: none" />
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 20px;">
                        
                        <div style="float: left; padding-right: 30px;">
                            <img id="6" src="Images/team/female1.png" title="<span style='font-size:25px;'>T.H.</span><br>Regional Account Manager"
                                width="100" height="100" class="gallery" /><img rel="6" src="Images/team/female2.png"
                                    width="100" height="100" style="display: none" />
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 30px;">
                    </div>
                    <div style="height: auto; width: 630px; border: solid 1px #f99d1c; background-color: #F5F5F5;
                        clear: both;">
                        <div style="height: 35px; text-align: left; background-color: #E9E9E9; padding-left: 15px;
                            padding-top: 10px;">
                            <asp:Label ID="Label5" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="19px" runat="server"
                                Text="Want to See your Face Here?"></asp:Label>
                        </div>
                        <div style="padding: 10px 15px 10px 15px; line-height: 24px; text-align: justify;">
                            <asp:Label ID="Label6" Font-Bold="false" ForeColor="#056eb7" Font-Size="12px" runat="server"
                                Text="Click <a href='jobs.aspx'>Here</a> to see if we're hiring! "></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: right;">
                <div style="height: auto; width: 350px; border: solid 1px #f99d1c; background-color: #F5F5F5;
                    display: none;">
                    <div style="height: 35px; background-color: #E9E9E9; padding-left: 20px; padding-top: 10px;">
                        <asp:Label ID="lblCompanyHeading" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="19px"
                            runat="server" Text="United We Stand"></asp:Label>
                    </div>
                    <div style="padding: 10px 15px 10px 15px; line-height: 24px; text-align: justify;">
                        <asp:Label ID="lblCompanyDetail" Font-Bold="false" ForeColor="#056eb7" Font-Size="12px"
                            runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div>
                    <div style="height: auto; width: 350px; border: solid 1px #f99d1c; background-color: #F5F5F5;">
                        <div style="height: 35px; background-color: #E9E9E9; padding-left: 20px; padding-top: 10px;">
                            <asp:Label ID="Label1" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="19px" runat="server"
                                Text="TastyGo HQ"></asp:Label>
                        </div>
                        <div style="padding: 10px 15px 10px 15px; line-height: 24px; text-align: justify;">
                            <div>
                                <asp:Label ID="Label2" Font-Bold="false" ForeColor="#056eb7" Font-Size="10px" runat="server"
                                    Text="#20-206 E.6th Ave Vancouve,<br> BC V5T 1J7"></asp:Label>
                            </div>
                            <div style="padding-top: 10px;">
                                <span id="lblJSCode">

                                    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&key=AIzaSyB_6ZG7US6bwIzTVOOJU15CpBmxekMUVwg"></script>

                                    <script type="text/javascript">var map,myLatlng;var geocoder;AttachEvent(window, "load", init);function init(){geocoder = new google.maps.Geocoder();getGeoLocation("#20-206 E.6th Ave Vancouve, BC V5T 1J7, Canada");}function getGeoLocation(address){var geocoder1 = new google.maps.Geocoder();if (geocoder1) {geocoder.geocode( { 'address': address}, function(results, status) {if (status == google.maps.GeocoderStatus.OK) {if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {LoadMap(results[0].geometry.location);}}});}}function LoadMap(curLoc){myLatlng = new google.maps.LatLng(curLoc.lat(), curLoc.lng());var myOptions = {zoom: 14,center: myLatlng,mapTypeId: google.maps.MapTypeId.ROADMAP};map = new google.maps.Map(document.getElementById("divMap"), myOptions);google.maps.event.addListener(map, 'zoom_changed', function() {setTimeout(moveToDarwin, 1500);});var marker = new google.maps.Marker({position: myLatlng, map: map, title:"#20-206 E.6th Ave Vancouve, BC V5T 1J7",color:"blue",label:"S"});google.maps.event.addListener(marker, 'click', function() {map.set_zoom(14);});}function moveToDarwin() {map.set_center(myLatlng);}</script>

                                </span>
                                <div id="divMap" style="width: 320px; height: 455px;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
