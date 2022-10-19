function loadMap(address)
{
    //alert('working');
    var map,myLatlng;var geocoder;AttachEvent(window, "load", init);
    function init(){geocoder = new google.maps.Geocoder();
    getGeoLocation(address);}
    function getGeoLocation(address){var geocoder1 = new google.maps.Geocoder();
    if (geocoder1) {geocoder.geocode( { 'address': address}, function(results, status) {if (status == google.maps.GeocoderStatus.OK) {if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {LoadMap(results[0].geometry.location);}}});}}
    function LoadMap(curLoc){myLatlng = new google.maps.LatLng(curLoc.lat(), curLoc.lng());
    var myOptions = {zoom: 14,center: myLatlng,mapTypeId: google.maps.MapTypeId.ROADMAP};
    map = new google.maps.Map(document.getElementById("divMap"), myOptions);google.maps.event.addListener(map, 'zoom_changed', function() {setTimeout(moveToDarwin, 1500);});
    var marker = new google.maps.Marker({position: myLatlng, map: map, title:"Google Map"});google.maps.event.addListener(marker, 'click', function() {map.set_zoom(14);});}
    function moveToDarwin() {map.set_center(myLatlng);}
    //alert('end working');
}