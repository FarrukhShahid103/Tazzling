<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="CSS/Index.css" rel="stylesheet" type="text/css" />
    <script src="JS/LoginSignup.js" type="text/javascript"></script>    
    <link rel="shortcut icon" href="favicon.ico" />
    <script type="text/javascript" src="js/jquery-1.4.min.js"></script>
    <script src="JS/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
    <script src="JS/jquery.colorbox.js"></script>
    <script src="JS/tipsy.js"></script>
    <script src="JS/latlon.js" type="text/javascript"></script>
    <script src="JS/geo.js" type="text/javascript"></script>
    <script language="JavaScript" src="http://j.maxmind.com/app/geoip.js"></script>
    <title>Tazzling.com</title>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnContinue").click(function () {

                EmailValidation();                
            });

            $('.AlreadySubscribed').click(function () {
                window.location.href = "Default.aspx?sub=true";
            });

        });
    </script>
    <script type="text/javascript">

        var CityNameList = new Array();
        var CityDistanceList = new Array();

        function TraceIP() {

            $.ajax({
                type: "POST",
                url: "getStateLocalTime.aspx?GetAllCities=True",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {
                    var split = msg.split('|');
                    var HTMLData = "";
                    for (i = 0; i < split.length; i++) {

                        var OneCityData = split[i].split(';');
                        if (typeof OneCityData[1] != "undefined") {
                            CityNameList[i] = OneCityData[0];
                            CityDistanceList[i] = CalcDistanceBetween(geoip_latitude(), geoip_longitude(), OneCityData[1], OneCityData[2]);

                        }
                    }
                    var ClosestCityInfo = Minimum_Value(CityDistanceList).split('|');
                    //alert("Distance : " + ClosestCityInfo[0] + "\nArray Index : " + ClosestCityInfo[1] + "\nCity Name : " + CityNameList[ClosestCityInfo[1]]);
                    //alert(CityNameList[ClosestCityInfo[1]]);
                    $("#DynamicText").html("Get 50% - 90% Exclusive Deals in " + CityNameList[ClosestCityInfo[1]]);
                    $("#ddlCity option:contains(" + CityNameList[ClosestCityInfo[1]] + ")").attr('selected', 'selected');
                    TwentyFive_Percent();
                    $("#IsAutoProcess").val("true");
                    function Minimum_Value(array) {
                        mn = array[0];
                        var Index;
                        for (i = 0; i < array.length; i++) {
                            if (array[i] < mn) {
                                mn = array[i];
                                Index = i;
                            }
                        }
                        return mn + "|" + Index;
                    };



                }
            });


        }






        var lat1 = geoip_latitude();
        var lon1 = geoip_longitude();

        var lat2 = "50.4547";
        var lon2 = "-104.6067";

        // alert(CalcDistanceBetween(lat1, lon1, lat2, lon2));
        function CalcDistanceBetween(lat1, lon1, lat2, lon2) {
            //alert("lat1 : " + lat1 + "\nlon1 : " + lon1 + "\n\nlat2 : " + lat2 + "\nlon2 : " + lon2);
            //Radius of the earth in:  1.609344 miles,  6371 km  | var R = (6371 / 1.609344);
            var R = 3958.7558657440545; // Radius of earth in Miles 
            var dLat = toRad(lat2 - lat1);
            var dLon = toRad(lon2 - lon1);
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
            Math.cos(toRad(lat1)) * Math.cos(toRad(lat2)) *
            Math.sin(dLon / 2) * Math.sin(dLon / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = R * c;

            // alert(d);
            return d;
        }

        function toRad(Value) {
            /** Converts numeric degrees to radians */
            return Value * Math.PI / 180;
        }
    </script>
    <script type="text/javascript">


        var Is100 = false;

        jQuery.fn.extend({
            animateCount: function (from, to, time) {
                var steps = 1,
        self = this,
        counter,
        DivText;

                if (from - to > 0) {
                    steps = -1;
                };

                from -= steps;

                function step() {
                    DivText = from += steps;
                    self.text(DivText + "%");

                    if ((steps < 0 && to >= from) || (steps > 0 && from >= to)) {
                        clearInterval(counter);
                    };
                };

                counter = setInterval(step, time || 10);
            }
        });


        function Zero_Percent() {
            $('#PercentageCounter').animateCount(50, 0);
            $('#ProgressBar').animate({
                'width': '0'

            }, 1500, function () {
                $("#ProgressBarArea").hide();
                $("#StepNo").html("");
            });



            $('#MyTestDiv').animate({
                'margin-left': '0'

            }, 1500, function () {
            });
            $('.tipsy').animate({
                'left': '485'

            }, 1500, function () {

            });
        }

        function TwentyFive_Percent() {
            var Check = $("#IsAutoProcess").val();
            if (Check == "true") {
                return;
            }
            //            $('#ProgressBar').animate({
            //                'width': '92px'

            //            }, 1500, function () {
            //            });


            $("#ProgressBarArea").show();
            $("#StepNo").html("Step 1");
        }

        function FiftyPercent_Percent() {
            $('#ProgressBar').animate({
                'width': '138px'

            }, 1500, function () {
            });
        }



        function Fifty_To_TwentyFive() {
            $('#ProgressBar').animate({
                'width': '69px'

            }, 1500, function () {
            });

        }


        function Hundred_To_Fifty() {
            $('#PercentageCounter').animateCount(100, 50);
            $('#ProgressBar').animate({
                'width': '138px'

            }, 1500, function () {
            });


            $("#ProgressBarArea").show();
            $('#MyTestDiv').animate({
                'margin-left': '+=135'

            }, 1500, function () {
            });
            $('.tipsy').animate({
                'left': '+=140'

            }, 1500, function () {

            });

            $("#StepNo").html("Step 2");
            $('#StepNo').css('margin-left', '35px');

        }

        function Hundred_Percent() {
            $('#PercentageCounter').animateCount(50, 100);
            $('#ProgressBar').animate({
                'width': '278px'

            }, 1500, function () {
            });


            $("#ProgressBarArea").show();
            $('#MyTestDiv').animate({
                'margin-left': '+=277'

            }, 1500, function () {
            });
            $('.tipsy').animate({
                'left': '+=282'

            }, 1500, function () {

            });

            $("#StepNo").html("Step 2");
            //            $('#StepNo').css('margin-left', '35px');

            Is100 = true;
        }

        function SimpleErrorDialog(Title, Message) {
            $("#SimpleErrorMessage").html(Message);
            $("#SimpleErrorDialog").dialog({
                title: Title,
                body: 'This is body',
                show: 'slide',
                width: 500,
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }

            });
        }


        //277px
        $(window).load(function () {
            //            if($.browser.msie && $.browser.version=="6.0")
            //            {
            //                SimpleErrorDialog("Browser Update Notification","<div style='color:gray;'>Our system detect you are using an old browser, and may affect your browsing experience. Please update your browser to the newest version.</div>");
            //            }
            $(".button").hover(function () {
                $(".button img")
                // first jump  
            .animate({ top: "-10px" }, 200).animate({ top: "-3px" }, 200)
                // second jump
            .animate({ top: "-7px" }, 100).animate({ top: "-3px" }, 100)
                // the last jump
            .animate({ top: "-6px" }, 100).animate({ top: "-3px" }, 100);
            });
            $(".buttonRight").hover(function () {
                $(".buttonRight img")
                // first jump  
            .animate({ top: "-10px" }, 200).animate({ top: "-3px" }, 200)
                // second jump
            .animate({ top: "-7px" }, 100).animate({ top: "-3px" }, 100)
                // the last jump
            .animate({ top: "-6px" }, 100).animate({ top: "-3px" }, 100);

            });

            $("#ddlCity").change(function (e) {
                var IsUsed = $("#IsUsed").val();
                var Check = $("#IsAutoProcess").val();
                $("#DynamicText").html("Get 50% - 90% Exclusive Deals in " + $("#ddlCity option:selected").text());

                //                var selectedIndex = $('#ddlCity').get(0).selectedIndex;
                //                if (selectedIndex > 0) {
                //                    if (IsUsed == "true") {
                //                        return;
                //                    }

                //                    $("#IsUsed").val("true");
                //                    var Check = $("#IsAutoProcess").val();
                //                    TwentyFive_Percent();

                //                }
                //                else {
                //                    $("#IsAutoProcess").val("false");
                //                    $("#IsUsed").val("false");
                //                    Zero_Percent();
                //                }



            });
            $('#txtEmail').click(function () {
                if ($(this).val() == 'Enter your email') {
                    $(this).val('');
                    $(this).css('color', 'black');
                    $(this).css('border', '2px solid #D1D6DC');
                }
                else {
                    $(this).css('border', '2px solid #D1D6DC');
                }
            });

            $("#txtEmail").focusout(function () {
                if ($(this).val() == '') {
                    $(this).val('Enter your email');
                    $(this).css('color', 'lightgray');
                }
                var IsFunUsed = $("#IsFunUsed").val();
                var IsBackUsed = $("#IsBackUsed").val();
                var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
                if ($("#txtEmail").val() == '' || !filter.test($("#txtEmail").val())) {

                    if (IsFunUsed == "true") {

                        if (IsBackUsed == "false") {

                            Hundred_To_Fifty();
                            $("#IsFunUsed").val("false");
                            $("#IsBackUsed").val("true");

                        }
                    }
                }
                else {

                    if (IsFunUsed == "false") {

                        Hundred_Percent();
                        $("#StepNo").html("Loading Deals...");
                        $("#IsFunUsed").val("true");
                        $("#IsBackUsed").val("false");
                    }
                }


            });
            // jQuery('#MyTestDiv').tipsy({ trigger: 'manual', gravity: 's' });
            // jQuery("#MyTestDiv").tipsy('show');

        });


        $("#BtnGoBack").live('click', function () {
            $('#EmailDiv').toggle('slide');
            setTimeout(function () {
                $('#CityDiv').toggle('slide');
            }, 500);
            $("#navigationArea").css({ "visibility": "hidden" });
            //Hundred_To_Fifty();
            Fifty_To_TwentyFive();
            $("#IsFunUsed").val("false")
            $("#StepNo").html("Step 1");
        });





        function EmailValidation() {


            var isValidated = true;
            // validate Email

            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
            if ($("#txtEmail").val() == '' || !filter.test($("#txtEmail").val())) {
                $("#txtEmail").removeClass("DropDownBG").addClass("TextBoxError");
                $("#txtEmail").css('border', '2px solid red');

                return false;
            }

            else {
                $("#txtEmail").css('border', '');
                $("#StepNo").html("Loading Deals...");
                $('#ProgressBar').animate({
                    'width': '258px'

                }, 1500, function () {
                });                
                $.ajax({
                    type: 'POST',
                    url: 'getStateLocalTime.aspx?subEmail=' + $("#txtEmail").val() + "&subCity="+$("#ddlCity option:selected").val(),
                    success: function (msg) {
                       window.location.href = "Default.aspx";
                    }
                });
                return true;
            }
        }

       
    </script>
    <script>

        function ResetField(ID) {
            $("#" + ID).removeClass("TextBoxError").addClass("TextBox");
        }
        $(document).ready(function () {
            $('.Tipsy').tipsy({ gravity: 's' });
            $('#ddlCity').tipsy({ gravity: 's' });
            $('#txtEmail').tipsy({ gravity: 's' });
            $("#button1").click(function () {

                $('#txtEmail').css('color', 'lightgray');
                FiftyPercent_Percent();
                $('#CityDiv').toggle('slide');

                //                   $("#CityDiv").slideUp("slow");
                //                    $("#EmailDiv").slideDown("slow");

                setTimeout(function () {
                    $('#EmailDiv').toggle('slide');
                }, 500);

                $("#BoxTopText").html("Enter Email");

                $("#StepNo").html("Step 2");
                //                $('#StepNo').css('margin-left', '35px');

                $("#navigationArea").css({ "visibility": "visible" });


                if ($("#txtEmail").val() == '') {
                    return;
                }
                var IsFunUsed = $("#IsFunUsed").val();
                var IsBackUsed = $("#IsBackUsed").val();
                var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
                if (!filter.test($("#txtEmail").val())) {

                    // $("#txtEmail").removeClass("TextBox").addClass("TextBoxError");

                    setTimeout(function () {
                        // $("#txtEmail").effect("bounce", { times: 3, distance: 50 }, 100);
                    }, 1000);

                    if (IsFunUsed == "true") {

                        if (IsBackUsed == "false") {


                            Hundred_To_Fifty();
                            $("#IsFunUsed").val("false");
                            $("#IsBackUsed").val("true");

                        }

                    }

                }
                else {

                    if (IsFunUsed == "false") {

                        Hundred_Percent();
                        $("#StepNo").html("Loading Deals...");
                        $("#IsFunUsed").val("true");
                        $("#IsBackUsed").val("false");
                    }
                }




            });

            $("#cancel").click(function () {
                $("#button1").show();
                $("#button2").hide();
                $("#formdiv").hide();
                $("#contentdiv").fadeIn();
            });


        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="toptile">
        <div style="padding-top: 5px; float: left;">
            <a href='<%= ConfigurationManager.AppSettings["YourSite"].ToString() +"/Default.aspx" %>'
                style="text-decoration: none;">
                <img src="Images/TezzlingIcon.png" />
            </a>
        </div>
        <div style="margin-left: 15px; z-index: 1111111; float: left; background-color: #cec6bf;
            width: 2px; height: 51px;">
        </div>
    </div>
    <div id="page-wrap">
        <div style="background-color: Black; width: 300px; text-align: left; height: 320px;
            float: left; margin-left: 115px; margin-top: 200px; font-family: Helvetica;">
            <div style="color: #8c9090; font-size: 37px; margin-left: 20px; margin-top: 85px;">
                Design Deals</div>
            <div style="color: White; font-size: 32px; margin-left: 22px; margin-top: 7px; font-weight: bold">
                Which Makes</div>
            <div style="color: #dd0016; font-size: 41px; margin-left: 20px; margin-top: 7px;
                font-weight: bold">
                Live Dazzle.</div>
            <div class="showHide" onclick="javascript:AreaSwitching('loginArea');" id="IfMamber"
                style="margin: 35px; margin-left: 20px; display: none; color: White; font-family: Helvetica;
                font-size: 12px;">
                Already tazzling member. <a style="cursor: pointer; color: Red; text-decoration: Underline;">
                    Login</a>
            </div>
        </div>
        <div style="float: left; font-family: Helvetica;" class="loginArea">
            <div style="height: 165px;">
                <div id="CityDiv" style="overflow: hidden;">
                    <div style="background-image: url(Images/step.png); height: 57px; width: 60px;
                        margin: 25px 0px 0px 20px">
                        <div style="font-size: smaller; font-weight: bold; color: White; padding: 5px 0 0 14px">
                            Step</div>
                        <div style="font-size: xx-large; font-weight: bold; color: White; padding-left: 35px">
                            1</div>
                    </div>
                    <div style="color: White; font-size: large; padding: 20px 0 0 23px">
                        Please confirm your country</div>
                    <div>
                        <%--      <select class="DropDownBG" style="width: 255px; height: 43px; margin: 15px 0 0 23px;background-image: url('Images/DropDownBGIndex.png');">
                        <option></option>
                    </select>--%>
                        <asp:DropDownList ID="ddlCity" Style="width: 255px; height: 43px; margin: 15px 0 0 23px;
                            background-image: url('Images/DropDownBGIndex.png');" runat="server" ToolTip="Please select your city."
                            CssClass="DropDownBG">
                        </asp:DropDownList>
                    </div>
                    <div id="button1" style="background-image: url(Images/btnContinue.png); height: 40px;
                        width: 123px; float: left; margin: 12px 0 0 19px; cursor: pointer">
                        <div style="color: White; margin: 10px 0 0 22px; font-size: 14px; font-weight: bold;
                            float: left">
                            Continue</div>
                        <div style="margin: 13px 0 0 8px; float: left">
                            <img alt="" src="Images/arrowContinue.png" />
                        </div>
                    </div>
                </div>
                <div id="EmailDiv" style="overflow: hidden; display: none; padding-top: 5px;">
                    <div style="background-image: url(Images/step.png); height: 57px; width: 60px;
                        margin: 25px 0px 0px 20px">
                        <div style="font-size: smaller; font-weight: bold; color: White; padding: 5px 0 0 14px">
                            Step</div>
                        <div style="font-size: xx-large; font-weight: bold; color: White; padding-left: 35px">
                            2</div>
                    </div>
                    <div style="color: White; font-size: large; padding: 20px 0 0 23px">
                        Please enter your email</div>
                    <div>
                        <%--<input id="txtEmail" class="DropDownBG"  type="text" style="width: 231px;  height: 17px; margin: 15px 0 0 23px;background-image: url('Images/DropDownBGIndex.png');"/>--%>
                        <asp:TextBox Text="Enter your email" Style="width: 231px; height: 17px; margin: 15px 0 0 23px;
                            background-image: url('Images/DropDownBGIndex.png');" ID="txtEmail" runat="server"
                            ToolTip="Please enter your email." CssClass="DropDownBG">
                        </asp:TextBox>
                    </div>
                    <div id="btnContinue" style="background-image: url(Images/btnContinue.png);
                        height: 40px; width: 123px; float: left; margin: 10px 0 0 19px; cursor: pointer">
                        <div style="color: White; margin: 10px 0 0 22px; font-size: 14px; font-weight: bold;
                            float: left;">
                            Continue</div>
                        <div style="margin: 13px 0 0 8px; float: left">
                            <img alt="" src="Images/arrowContinue.png" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="padding-top: 5px; clear: both;">
                <div style="background-image: url(Images/stepSlider.png); height: 30px; width: 268px;
                    margin: 71px 0 0 19px">
                    <div id="ProgressBar" style="background-color: #DD0017; margin: 3px 0 0 3px; float: left;
                        text-align: center; width: 92px; border-radius: 14px 14px 14px 14px; background: -moz-linear-gradient( center top, #fe1a00 5%, #ce0100 100% );
                        background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #fe1a00), color-stop(1, #ce0100) );
                        background: -linear-gradient( center top, #fe1a00 5%, #ce0100 100% );">
                        <div style="background-image: url(Images/stepCapsuleLayer.png); height: 25px;
                            width: auto; border-radius: 14px 14px 14px 14px;">
                            <div align="center" id="StepNo" style="color: White; margin: 4px 0 0 0px; font-size: 14px;
                                font-weight: bold; width: 100%; float: left;">
                                Step 1</div>
                        </div>
                    </div>
                </div>
                <div class="AlreadySubscribed" style="color: White; float: left; font-size: 12px;
                    margin: 15px 0 0 27px; cursor: pointer; font-style: italic; font-family: Arial">
                    I am already subscribed, take me to the deals
                </div>
            </div>
        </div>
        <div id="footerWrapper">
            <div id="footer">
                <ul class="footerLinks floatLeft">
                    <li><a target="_blank" href="#">About</a></li>
                    <li><a target="_blank" href="#">Careers</a></li>
                    <li><a target="_blank" href="#">Blog</a></li>
                    <li><a target="_blank" href="#">Help</a></li>
                    <li><a target="_blank" href="#">Contact Us</a></li>
                    <li><a target="_blank" href="#">Return Policy</a></li>
                    <li><a target="_blank" href="#">Shipping</a></li>
                    <li><a target="_blank" href="#">Terms</a></li>
                    <li><a target="_blank" href="#">Privacy</a></li>
                </ul>
                <span style="margin-right: 0px" class="copyText floatRight">Tazzling.com &copy; 2012</span>
                <ul style="" class="socialLinks floatRight">
                    <li><a style="cursor: pointer; text-decoration: none;">
                        <img width="19px" height="19px" src="Images/Twitter.png" alt="" />
                    </a></li>
                    <li><a style="cursor: pointer; text-decoration: none;">
                        <img width="19px" height="19px" src="Images/FaceBookSignIn.png" alt="" />
                    </a></li>
                </ul>
                <!-- SAL Admin Check# -->
                <!-- SAL Admin Check# -->
            </div>
        </div>
    </div>
    <asp:Literal ID="ltRunScript" runat="server"></asp:Literal>
    <input type="hidden" id="IsUsed" value="false" />
    </form>
</body>
</html>
