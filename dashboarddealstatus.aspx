<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="dashboarddealstatus.aspx.cs"
    Inherits="dashboarddealstatus" Title="Deal Status" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/MemberDashBoard.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/jquery-jqplot.css" rel="stylesheet" type="text/css" />
    <link href="CSS/PieChart.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery-jqplot.js" type="text/javascript"></script>

    <script src="JS/jqplot.pieRenderer.min.js" type="text/javascript"></script>

    <script src="JS/jqplot.canvasTextRenderer.min.js" type="text/javascript"></script>

    <script src="JS/highcharts.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>

    <script src="JS/markerclusterer.min.js" type="text/javascript"></script>

    <script src="JS/pieChart.js" type="text/javascript"></script>

    <script src="JS/pieChart.pieRenderer.js" type="text/javascript"></script>

    <script type="text/javascript">
       var total=0;
       var markers=[];
       var RunCounter = 0;
       var markercount=[];
       var latlist=[];
       var lonlist=[];
       var contentString = "";
       var contentdata=[];
       var myorders=[];
       
    
   var IPMapper = {
    	map: null,
	    mapTypeId: google.maps.MapTypeId.ROADMAP,
	    latlngbound: null,
	    infowindow: null,
	    baseUrl: "http://freegeoip.net/json/",
	    initializeMap: function(mapId){
		    IPMapper.latlngbound = new google.maps.LatLngBounds();
		    var latlng = new google.maps.LatLng(0, 0);
		   var center = new google.maps.LatLng(55.424035, -95.675941);
		  //  var center= latlng;	
		    //set Map options
		    var mapOptions = {
    			zoom: 3,
	    		center: center,
		    	mapTypeId: IPMapper.mapTypeId
		    	}
		    //init Map
	    	IPMapper.map = new google.maps.Map(document.getElementById(mapId), mapOptions);
		    //init info window
		    IPMapper.infowindow = new google.maps.InfoWindow();
		    //info window close event
		    google.maps.event.addListener(IPMapper.infowindow, 'closeclick', function() {
			IPMapper.map.fitBounds(IPMapper.latlngbound);
			IPMapper.map.panToBounds(IPMapper.latlngbound);
		}); 
	}, 
	        addIPArray: function(ipArray){
	        ipArray = IPMapper.uniqueArray(ipArray); //get unique array elements
		    //add Map Marker for each IP
		    for (var i = 0; i < ipArray.length; i++){
		    var string=IPMapper.addIPMarker;
		    IPMapper.addIPMarker(ipArray[i]);
		    markercount(ipArray);
		}
	},
	addIPMarker: function(ip){
	    var substr = ip.split('|');
        var IP = substr[0];
        var TotalOrders= substr[1];
        var IPCounter = substr[2];
        ipRegex = /^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$/;
		    if($.trim(IP) != '' && ipRegex.test(IP)){ //validate IP Address format
			var url = encodeURI(IPMapper.baseUrl + IP + "?callback=?");
			 //geocoding url
			$.getJSON(url, function(data) { //get Geocoded JSONP data
				if($.trim(data.latitude) != '' && data.latitude != '0' && !isNaN(data.latitude)){ //Geocoding successfull
					var latitude = data.latitude;
					var longitude = data.longitude;
					myorders.push(TotalOrders);
					latlist.push(latitude);
					lonlist.push(longitude);
				    RunCounter++;
					        if(RunCounter == IPCounter)
					        {
					            var infowindow = new google.maps.InfoWindow();
					            var newLayList = new Array();
					            var newLongList = new Array();
					            newLayList = removeDuplicateElement(latlist);
					            newLongList = removeDuplicateElement(lonlist);
					            latlist = new Array();
					            latlist = newLayList;
					             lonlist = new Array();
					            lonlist = newLongList;
					           
					           for(var i=0; i<lonlist.length; i++)
					                {
				
					                     var latlng = new google.maps.LatLng(latlist[i], lonlist[i]);
					                     var marker = new google.maps.Marker({ //create Map Marker
					                     map: IPMapper.map,
						                 draggable: false,
						                 position:latlng,
						                 animation: google.maps.Animation.DROP,
						                 raiseOnDrag: true,
                                         //labelContent: contentdata[i],
                                         labelAnchor: new google.maps.Point(30, 0),
                                         labelClass: "labels", // the CSS class for the label
                                         labelStyle: {opacity: 0.75},
						                 //animation: google.maps.Animation.BOUNCE,
						                 icon: "http://demo.tazzling.com/images/gmap_pin.png"
					                     });
					                     markers.push(marker);    				                       
			                
					                 }
    					                var markerClustererOptions = {   
                                        description: ' tracks: click to show',  
                                        maxZoom: 10,  
                                        gridSize: 50  
                                         };
                                     
					                    var markerCluster = new  MarkerClusterer(IPMapper.map , markers , markerClustererOptions, latlng);
					                
					         }
    					     //place Marker on Map
				        }
				         else
				          {
					        IPMapper.logError('IP Address geocoding failed!');
					        $.error('IP Address geocoding failed!');
				           }
			            });
		        }
		         else 
		         {
			        IPMapper.logError('Invalid IP Address!');
			        $.error('Invalid IP Address!');
		        }
	    },
	
      
	    uniqueArray: function(inputArray){ //return unique elements from Array
		    var a = [];
		    for(var i=0; i<inputArray.length; i++) {
			    for(var j=i+1; j<inputArray.length; j++) {
				    if (inputArray[i] === inputArray[j]) j = ++i;
			    }
    			
			    a.push(inputArray[i]);
		    }
		    return a;
	    },
	    logError: function(error){
		    if (typeof console == 'object') { console.error(error); }
	    }
    }	

        function removeDuplicateElement(arrayName)
          {
          var newArray=new Array();
          label:for(var i=0; i<arrayName.length;i++ )
          {  
          for(var j=0; j<newArray.length;j++ )
          {
          if(newArray[j]==arrayName[i]) 
          continue label;
          }
          newArray[newArray.length] = arrayName[i];
          }
          return newArray;
          
  }
    </script>

    <div style="width: auto; height: 36px; background-color: #005f9f; clear: both; margin-top: 20px;
        margin-bottom: 10px;">
        <div style="color: White; font-weight: bold; clear: both; text-decoration: none;">
            <usrCtrl:subMenu ID="subMenu1" runat="server" />
        </div>
    </div>
    <div style="clear: both; overflow: hidden; margin-top: 10px;">
        <div style="height: 45px; background-color: #5f5f5f; font-family: Helvetica;
            color: White">
            <div style="float: left;">
                <div style="width: 200px; font-size: 24px; padding-left: 10px; padding-top: 10px;">
                    Deal Detail
                </div>
            </div>
        </div>
    </div>
    <div style="background-color: White; height: auto; margin-top: 10px; clear: both;
        overflow: hidden; margin-bottom: 20px;">
        <div style="background-color: #acc661; height: 20px; color: White; font-family: Helvetica;
            font-size: 18px; margin-top: 10px; margin-left: 20px; padding: 5px; width: 96%;">
            <asp:Label ID="lblDealName" runat="server" Text=""></asp:Label>
        </div>
        <div style="float: left; margin-left: 18px; margin-top: 20px; width: 60%; height: 145px;
            background-color: #eeeeee">
            <div style="float: left; margin-left: 10px; margin-top: 10px; margin-bottom: 10px;
                background-color: #f5f9fc; width: 123px; height: 113px;">
                <div style="padding: 29px 20px;">
                    <div style="color: Green; font-family: Helvetica; font-size: 18px; font-weight: bold;
                        text-align: center;">
                        <%= TotalSold %></div>
                    <div style="color: Black; font-family: Helvetica; font-size: 18px; font-weight: bold;
                        text-align: center; margin-top: 12px;">
                        Sold</div>
                </div>
            </div>
            <div style="float: left; margin-left: 10px; margin-top: 10px; margin-bottom: 10px;
                background-color: #f5f9fc; width: 123px; height: 113px;">
                <div style="padding: 29px 20px;">
                    <div style="color: Green; font-family: Helvetica; font-size: 18px; font-weight: bold;
                        text-align: center;">
                        $<%= TotalEarn%></div>
                    <div style="color: Black; font-family: Helvetica; font-size: 18px; font-weight: bold;
                        text-align: center; margin-top: 12px;">
                        Revenue</div>
                </div>
            </div>
            <div style="float: left; width: 300px; margin-top: 10px; margin-left: 10px; font-family: Helvetica;
                font-size: 15px; color: Black;">
                <div>
                    <div style="float: left; width: 250px; margin-top: 10px;">
                        Unique Purchase
                    </div>
                    <div style="float: left; margin-top: 10px;">
                        <%= TotalSold %>
                    </div>
                </div>
                <div>
                    <div style="margin-top: 30px;">
                        <div style="float: left; width: 250px; margin-top: 10px;">
                            Redeemed Voucher
                        </div>
                        <div style="float: left; margin-top: 10px;">
                            <%= Redeemed %>
                        </div>
                    </div>
                    <div style="background-color: Black; background-repeat: repeat-x; height: 1px; margin-top: 30px;">
                    </div>
                </div>
                <div style="margin-top: 30px;">
                    <div style="margin-top: 10px;">
                        <div style="float: left; width: 250px; margin-top: 10px;">
                            Unredeemed Voucher
                        </div>
                        <div style="float: left; margin-top: 10px;">
                            <%= Unredeemed %>
                        </div>
                    </div>
                    <div style="background-color: Black; background-repeat: repeat-x; height: 1px; margin-top: 30px;">
                    </div>
                </div>
                <div style="margin-top: 10px;">
                    <div style="background-color: Black; background-repeat: repeat-x; height: 1px; margin-top: 30px;">
                    </div>
                </div>
            </div>
        </div>
        <div style="float: left; height: 145px; width: 345px; margin-top: 20px; margin-left: 10px;">
            <div style="float: left; color: Black; font-family: Helvetica; font-size: 18px;
                font-weight: bold;">
                Percent Redeemed
            </div>
            <div style="float: left; margin-left: 20px; width: 100px; color: #87b6d9; font-size: 18px;
                font-weight: bold;">
                <%= PerRedeemed %>%</div>
            <div style="height: 20px; margin-top: 30px;">
                <div id="progressbar">
                    <asp:Literal ID="ltlprogree" runat="server"></asp:Literal>
                </div>
            </div>
            <div style="margin-top: 28px;">
                <asp:LinkButton runat="server" ID="btnRedeemed" Text="Redeem Voucher" CssClass=" button big primary"
                    OnClick="btnRedeemed_Click"></asp:LinkButton>
            </div>
            <div style="margin-top: 16px;">
                <div style="text-align: left; color: Black; font-family: Helvetica; font-size: 16px;
                    font-weight: bold;">
                    Download Puchase List</div>
                <div style="margin-top: 10px;">
                    <div style="width: 50px; margin-top:15px;">
                        <asp:DropDownList ID="drpdownlistfordownload" runat="server">
                            <asp:ListItem Value="pdf" Selected="True">PDF</asp:ListItem>
                            <asp:ListItem Value="xls">XLS</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="float: right; text-align: left; width: 255px; margin-top: -27px; color: Black;
                        font-family: Helvetica; font-size: 16px; font-weight: bold;">
                        <asp:UpdatePanel ID="updownloadExcel" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton runat="server" ID="Download" Text="Download" CssClass=" button big primary"
                                    OnClick="Download_Click"></asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Download" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <div style="float: left; margin-left: 22px; margin-top: 20px; font-family: Helvetica;
            font-size: 18px; font-weight: bold">
            When deals were purchased
        </div>
        <div id="container" style="width: 940px; height: 290px; margin-left: 20px; margin-top: 225px;">
            <asp:Literal ID="ltlgraph" runat="server"></asp:Literal>
        </div>
        <div id="mapareaheading" runat="server" style="color: Black; margin-left: 30px; padding-top: 70px;
            margin-bottom: 10px; font-family: Helvetica; font-size: 18px; font-weight: bold;
            clear: both; overflow: hidden;">
            Purchaser Location
        </div>
        <div id="MapArea" runat="server" style="background-color: #E5E3DF; clear: both; height: 340px;
            margin-left: 60px; margin-top: 25px; overflow: hidden; position: relative; width: 770px;">
            <asp:Literal ID="ltIPScript" runat="server"></asp:Literal>
        </div>
        <div style="float: left; width: 440px;">
            <div style="color: Black; margin-left: 30px; padding-top: 70px; margin-bottom: 10px;
                font-family: Helvetica; font-size: 18px; font-weight: bold; clear: both;
                overflow: hidden;">
                How deals were purchased
            </div>
            <div style="margin-left: 0px; float: left; margin-top: 20px;">
                <div id="chart3">
                </div>
                <asp:Literal ID="ltlchart" runat="server"></asp:Literal>
            </div>
        </div>
        <div style="float: right; width: 540px;">
            <div style="color: Black; margin-left: 50px; padding-top: 70px; margin-bottom: 10px;
                font-family: Helvetica; font-size: 18px; font-weight: bold; clear: both;
                overflow: hidden;">
                Orders By Gender
            </div>
            <div style="margin-left: 42px; float: left; margin-top: 0px;">
                <div id="chart4" style="margin-top: 20px; margin-left: 0px; width: 400px; height: 300px;">
                </div>
                <asp:Literal ID="ltlgender" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
