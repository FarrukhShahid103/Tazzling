<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoogleMap.aspx.cs" Inherits="Takeout_GoogleMap" %>

<!DOCTYPE html "-//W3C//DTD XHTML 1.0 Strict//EN" 
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <title>Google Map</title>
    <script language="javascript" type="text/javascript" src="JS/Functions.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <span id="lblJSCode">

        <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&key=AIzaSyB_6ZG7US6bwIzTVOOJU15CpBmxekMUVwg"></script>

        <script type="text/javascript">var map,myLatlng;var geocoder;AttachEvent(window, "load", init);function init(){geocoder = new google.maps.Geocoder();getGeoLocation("<%=locationOfVendor %>");}function getGeoLocation(address){var geocoder1 = new google.maps.Geocoder();if (geocoder1) {geocoder.geocode( { 'address': address}, function(results, status) {if (status == google.maps.GeocoderStatus.OK) {if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {LoadMap(results[0].geometry.location);}}});}}function LoadMap(curLoc){myLatlng = new google.maps.LatLng(curLoc.lat(), curLoc.lng());var myOptions = {zoom: 14,center: myLatlng,mapTypeId: google.maps.MapTypeId.ROADMAP};map = new google.maps.Map(document.getElementById("divMap"), myOptions);google.maps.event.addListener(map, 'zoom_changed', function() {setTimeout(moveToDarwin, 1500);});var marker = new google.maps.Marker({position: myLatlng, map: map, title:"Google Map",color:"blue",label:"S"});google.maps.event.addListener(marker, 'click', function() {map.set_zoom(14);});}function moveToDarwin() {map.set_center(myLatlng);}</script>

    </span>
    <div id="divMap" style="width: 500px; height: 350px;">
    </div>
    </form>
</body>
</html>
