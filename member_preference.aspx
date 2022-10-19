<%@ Page Title="My Preference" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="member_preference.aspx.cs" Inherits="member_preference" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/coda-slider-2.0.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <script type="text/javascript" src="JS/jquery.coda-slider-2.0.js"></script>
    <link href="CSS/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.jgrowl.js" type="text/javascript"></script>
    <style type="text/css">
        #td
        {
            width: 40px;
        }
        .sub_column3
        {
            float: left;
            height: auto;
            overflow: hidden;
            margin-bottom: 25px !important;
            overflow: auto;
            padding: 0 0px 10px;
            width: 504px;
        }
        #leftbar
        {
            width: 200px;
            float: left;
        }
        #settings
        {
            width: 720px;
            float: left;
        }
        .payment_methods td, .payment_methods td span
        {
            color: #333;
            font-size: 13px;
        }
        .settings_primary
        {
            border: 1px solid #b0e7fe;
            background-color: #ebf7fc;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            padding: 16px 16px 16px 20px;
            color: #333;
        }
        h2, h3
        {
            font-size: 16px;
            font-weight: normal;
            margin: 0;
        }
        .city
        {
            font-size: 14px;
        }
        .contact_label
        {
            color: #333;
            font-weight: normal;
            font-size: 12px;
            margin: 6px 0 4px;
            display: block;
        }
        .desc
        {
            color: #333;
            font-size: 12px;
            margin: 2px 0 12px 0;
        }
        .desc:link, .desc:visited, .desc:hover, .desc:active
        {
            font-size: 12px;
        }
        .PagesBG
        {
            overflow: hidden;
        }
        .sub_loader
        {
            font-size: 13px;
            width: 130px;
            margin-right: 10px;
            height: 16px;
            padding: 4px 0;
            text-align: center;
            background: #fffcf2;
            color: #4297D7;
            display: none;
            float: left;
            border: 1px solid #a2def9;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            -moz-box-shadow: 1px 1px 2px #888;
            -webkit-box-shadow: 1px 1px 2px #888;
            box-shadow: 1px 1px 2px #888;
        }
        .CountryHeader
        {
            font-size: 20px;
            background-color: #00AEFF;
            color: white;
            font-weight: bold;
            margin-left: -10px !important;
            text-align: center;
            min-height: 30px;
            margin-right: -10px !important;
            margin-right: -10px !important;
            line-height: 1.7;
        }
        .sub_saved
        {
            color: #999;
        }
        .sub_check
        {
            padding: 6px 0 4px 0;
            height: 16px;
            width: 425px;
            float: left;
        }
        .sub_container
        {
            padding: 3px 0 7px 0;
            height: 16px;
            width: 285px;
            float: left;
            margin-bottom: 4px;
        }
        .validation
        {
            color: red;
            display: none;
            font-size: 10px;
            margin-bottom: 1px;
        }
        .edit_mode
        {
            display: none;
            font-size: 13px;
        }
        #account_menu
        {
            list-style-type: none;
        }
        h1, #account_menu li
        {
            margin-right: 25px;
            text-align: right;
        }
        #account_menu li
        {
            margin-bottom: 8px;
        }
        .payment_delete, .edit_button
        {
            display: block;
            margin-bottom: 8px;
            text-align: center;
            width: 60px;
        }
        #contact_form input, #contact_form select
        {
            width: 250px;
        }
        #contact_form input.sub_submit
        {
            float: right;
            height: 37px;
            width: 119px;
        }
        #contact_form td
        {
            padding-bottom: 10px;
            padding-right: 25px;
        }
        .contact_label span
        {
            font-size: 13px;
            color: #777;
        }
        #contact_form textarea
        {
            width: 533px;
        }
        .phone_number
        {
            font-size: 14px;
            font-weight: normal;
            color: black;
        }
        .contact_us
        {
            border: 1px solid #666;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px;
            width: 242px;
            padding: 2px;
        }
        .contact_reason
        {
            border: 1px solid #666;
            width: 256px;
            padding: 0;
        }
        .readonly
        {
            border: 1px solid #aaa;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px;
            width: 252px;
            padding: 2px;
            background: transparent;
        }
        .section
        {
            border: 1px dotted #ccc;
            border-left: 0;
            border-right: 0;
            margin: 20px 0;
            padding: 10px 0 0;
            width: 550px;
        }
        .billing
        {
            border: 1px solid #4294b7;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border-radius: 4px;
            padding: 2px 4px;
            margin-bottom: 4px;
            font-size: 13px;
        }
        td.label
        {
            padding-right: 20px;
            text-align: right;
        }
        .divider
        {
            border-top: 1px dotted #CCC;
            margin: 16px 0;
        }
        #confirm_row
        {
            display: none;
        }
        #c2c_widget
        {
            margin-top: 15px;
        }
        #c2c_widget p
        {
            color: #333;
            font-size: 12px;
            margin: 4px 0;
            line-height: 1.6;
        }
        .c2c-yournumber, .c2c-extension
        {
            border: 1px solid #666;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px;
            padding: 2px;
        }
        .c2c-whentocall
        {
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px;
            border: 1px solid #666;
            padding: 0;
        }
        .c2c-callme-button-spacer
        {
            margin: 0;
        }
        .c2c-form-row-callme-button
        {
            margin-top: 10px;
        }
        .c2c-extension-label, div.c2c-form-row label, .c2c-form-row-receptionist, .c2c-form-row-receptionist span
        {
            font-size: 12px;
            color: black;
        }
        .c2c-whentocall
        {
            margin-left: 10px;
        }
        div.c2c-form-row label
        {
            float: inherit;
            margin-right: 40px;
        }
        .c2c-form-row-whentocall
        {
            margin-top: 4px;
        }
        .c2c-inprogress-message
        {
            font-size: 12px;
            color: #333;
        }
        .subscription_state_title
        {
            color: #777;
            clear: both;
            padding: 12px 0 5px;
        }
        
        .sub_column a:link, .sub_column a:visited, .sub_column a:hover, .sub_column a:active
        {
            font-size: 14px;
            display: block;
            margin-bottom: 3px;
        }
        .sub_col_title
        {
            padding: 0 20px 5px 0;
            clear: both;
            color: #0A3B5F;
            float: left;
            font-size: 15px !important;
            font-weight: normal !important;
        }
        .sub_column
        {
            width: 425px;
            background: white;
            float: left;
            padding: 0 10px 10px;
            height: 240px;
            overflow: auto;
            border: 1px solid #B0E7FE;
            margin-bottom: 25px !important;
        }
        .sub_spacer
        {
            width: 20px;
            float: left;
        }
        .payment_actions
        {
            width: 175px;
        }
        div.subscribe_section
        {
            background-color: white;
            border: 1px solid #B0E7FE;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            margin: 21px 1px 1px;
            padding: 15px;
        }
        div.subscribe_section.selected_section
        {
            border: 2px solid #F89820;
            margin: 20px 0 0;
        }
        label.section_label
        {
            font-size: 12px;
            font-weight: bold;
            padding-left: 8px;
        }
        p.radio_desc
        {
            font-size: 12px;
            line-height: 1.4;
            margin: 2px 0 0;
            padding-left: 27px;
        }
        div.sub_section
        {
            float: left;
            margin-top: 20px;
        }
        #available_subscriptions
        {
            margin-left: 27px;
        }
        #your_subscriptions
        {
            margin-left: 15px;
        }
        div.section_loader
        {
            font-size: 12px;
            display: none;
            margin: 0 0 10px 5px;
            line-height: 18px;
        }
        div.section_loader div.loader_saving, div.section_loader div.loader_saved
        {
            font-size: inherit;
            display: none;
        }
        div.section_loader div.loader_saving img, div.section_loader div.loader_saved img
        {
            margin-right: 8px;
        }
        div.section_loader div.loader_saving
        {
            color: #333;
        }
        div.section_loader div.loader_saved
        {
            color: #376B30;
        }
        div.section_saving div.section_loader, div.section_saving div.loader_saving, div.section_saved div.section_loader, div.section_saved div.loader_saved
        {
            display: block;
        }
        div.popup_bubble
        {
            background-color: white;
            border: 1px solid #B0E7FE;
            border-radius: 5px;
            padding: 25px;
            -moz-box-shadow: 0 10px 15px #888;
            -webkit-box-shadow: 0 10px 15px #888;
            box-shadow: 0 10px 15px #888;
        }
        div.popup_bubble div.bubble_down_shadow
        {
            position: absolute;
            bottom: -13px;
            left: 90px;
        }
        #opt_out_feedback_container
        {
            height: 240px;
            position: absolute;
            top: 234px;
            width: 500px;
            display: none;
        }
        #opt_out_feedback div.close_button
        {
            float: right;
            margin: -20px -20px 0 0;
            cursor: pointer;
            padding: 5px;
        }
        #opt_out_feedback h2
        {
            color: #006400;
            font-size: 14px;
            font-weight: bold;
        }
        #opt_out_feedback div.form_header p
        {
            font-size: 12px;
            font-style: italic;
            margin-top: 5px;
        }
        #opt_out_feedback div.form_body
        {
            color: black;
            margin-top: 20px;
        }
        #opt_out_feedback div.form_body p
        {
            font-size: 12px;
            margin-top: 2px;
        }
        #opt_out_feedback h3
        {
            font-size: 15px;
            font-weight: bold;
        }
        #opt_out_feedback ul
        {
            margin: 0;
            padding: 0;
        }
        #opt_out_feedback li
        {
            width: 250px;
            float: left;
            line-height: 25px;
            list-style: none;
            font-size: 12px;
        }
        #opt_out_feedback input
        {
            margin: 3px 10px 0 0;
        }
        #opt_out_feedback label
        {
            font-size: inherit;
        }
        #feedback_submit
        {
            clear: both;
            margin-top: 20px;
        }
        #feedback_submit img
        {
            cursor: pointer;
        }
        #feedback_submit img.disabled
        {
            opacity: .5;
            cursor: auto;
        }
        #feedback_submit_error
        {
            color: red;
            font-size: 12px;
            display: none;
        }
        #opt_out_feedback_container div.section_loader
        {
            position: relative;
        }
        #opt_out_feedback_container div.loader_saving
        {
            color: #666;
            font-size: 15px;
            left: 160px;
            position: absolute;
            top: 104px;
        }
        #opt_out_feedback_container div.loader_saved
        {
            color: #333;
            font-size: 17px;
            left: 150px;
            position: absolute;
            top: 105px;
        }
        
        
        .ui-state:hover, .ui-widget-content .ui-state-hover, .ui-widget-header .ui-state-hover, .ui-state-focus, .ui-widget-content .ui-state-focus, .ui-widget-header .ui-state-focus
        {
            background: url("images/ui-bg_glass_100_fdf5ce_1x400.png") repeat-x scroll 50% 50% #C5DBEC;
            border: 1px solid #C5DBEC;
            color: #4297D7;
        }
    </style>
    <div id='Notify' class='bottom-right' style='position: fixed; bottom: 0px; right: 0px;
        font-size: 11px !important;'>
    </div>

<script>
    $(window).load(function () {

        $('#Itemtab3').click(function () {
           $("#messages").slideUp("slow");
       });

       $('#Itemtab2').click(function () {
           $("#messages").slideUp("slow");
       });

       $('#Itemtab1').click(function () {
           $("#messages").slideUp("slow");
       });


    });
 </script>


    <script type="text/javascript">
        var ActiveTab = 1;
        function Tab1() {
            if (ActiveTab != 1) {
                $('#Itemtab1').click();
                $("#TabDiv2").removeClass("CurrentActiveTab").addClass("NormalTab");
                $("#TabDiv3").removeClass("CurrentActiveTab").addClass("NormalTab");
                $("#TabDiv1").removeClass("NormalTab").addClass("CurrentActiveTab");

                $("#BottomArrow2").removeClass("CurrentActiveTabBottom");
                $("#BottomArrow3").removeClass("CurrentActiveTabBottom");
                $("#BottomArrow1").addClass("CurrentActiveTabBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Subscriptions");
                $("#PageTitle").show('fast');
                ActiveTab = 1;
            }

        }

        function Tab2() {
            if (ActiveTab != 2) {
                $('#Itemtab2').click();
                $("#TabDiv1").removeClass("CurrentActiveTab").addClass("NormalTab");
                $("#TabDiv3").removeClass("CurrentActiveTab").addClass("NormalTab");
                $("#TabDiv2").removeClass("NormalTab").addClass("CurrentActiveTab");

                $("#BottomArrow3").removeClass("CurrentActiveTabBottom");
                $("#BottomArrow1").removeClass("CurrentActiveTabBottom");
                $("#BottomArrow2").addClass("CurrentActiveTabBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Favorite Deals");
                $("#PageTitle").show('fast');
                ActiveTab = 2;
            }

        }

        function Tab3() {
            if (ActiveTab != 3) {
                $('#Itemtab3').click();
                $("#TabDiv1").removeClass("CurrentActiveTab").addClass("NormalTab");
                $("#TabDiv2").removeClass("CurrentActiveTab").addClass("NormalTab");
                $("#TabDiv3").removeClass("NormalTab").addClass("CurrentActiveTab");

                $("#BottomArrow1").removeClass("CurrentActiveTabBottom");
                $("#BottomArrow2").removeClass("CurrentActiveTabBottom");
                $("#BottomArrow3").addClass("CurrentActiveTabBottom");

                $("#PageTitle").hide('fast');
                $("#PageTitle").html("My Profile");
                $("#PageTitle").show('fast');
                ActiveTab = 3;
            }

        }

        function WhoValidation() {

            var filter0 = /^[a-zA-Z ]+$/;
            var IsValid = true;
            if ($("#ctl00_ContentPlaceHolder1_txtFirstName").val() == "" || !filter0.test($("#ctl00_ContentPlaceHolder1_txtFirstName").val())) {
                $("#ctl00_ContentPlaceHolder1_txtFirstName").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtLastName").val() == "" || !filter0.test($("#ctl00_ContentPlaceHolder1_txtLastName").val())) {
                $("#ctl00_ContentPlaceHolder1_txtLastName").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtZipCode").val() == "") {
                $("#ctl00_ContentPlaceHolder1_txtZipCode").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }



            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlDealsFor').selectedIndex == 0) {
                $("#ctl00_ContentPlaceHolder1_ddlDealsFor").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }

            if (document.getElementById('ctl00_ContentPlaceHolder1_ddYEar').selectedIndex == 0) {
                $("#ctl00_ContentPlaceHolder1_ddYEar").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }

            if (document.getElementById('ctl00_ContentPlaceHolder1_ddMonth').selectedIndex == 0) {
                $("#ctl00_ContentPlaceHolder1_ddMonth").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddDate').selectedIndex == 0) {
                $("#ctl00_ContentPlaceHolder1_ddDate").removeClass("TextBox").addClass("TextBoxError");
                IsValid = false;
            }



            return IsValid;
        }



        function AddToLoveList(ID) {
            var Text = $("#Tag" + ID).html();
            if (Text == "Please Wait...") {
                return;
            }
            $("#Tag" + ID).addClass('LoadingTag').removeClass('like').html("Please Wait...");
            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?AddFavDealID=" + ID,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {

                        if (msg == "True") {
                            $("#Tag" + ID).addClass('like').removeClass('LoadingTag').html(Text);
                            $("#MainDivDealTag" + ID).hide("slow");
                            $("#MainDivDoneTag" + ID).show("slow");

                            $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/tick.png'  /></span><span><b>" + Text + "</b> have been added to your favorite list.</span>"),
                            
                                                {
                                                    sticky: true,
                                                    glue: 'before',
                                                    speed: 2500,
                                                    easing: 'easeOutBounce'
                                                };

                            setTimeout(function () {
                                $("#Itemtab2").click();
                            }, 500);
                        }
                        else {


                            ErrorDialog("Oops some error occurred", "Some Error occurred while saving data.<br>Please contact at Admin");
                            $("#Tag" + ID).addClass('like').removeClass('LoadingTag').html(Text);
                            $("#MainDivDealTag" + ID).show("slow");
                            $("#MainDivDoneTag" + ID).hide("slow");

                        }
                    }

                });

            }, 1000);    //

        }
        function RemoveFromLoveList(ID) {
            var Text = $("#Tag" + ID).html();
            if (Text == "Please Wait...") {
                return;
            }
            $("#DoneTag" + ID).addClass('LoadingTag').removeClass('like').html("Please Wait...");

            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?DeleteFavDealID=" + ID,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {

                        if (msg == "True") {
                        $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/tick.png'  /></span><span><b>" + Text + "</b> have been removed from your favorite list.</span>"),
                          {
                           sticky: true,
                           glue: 'before',
                           speed: 2500,
                           easing: 'easeOutBounce'
                           };

                            $("#DoneTag" + ID).addClass('like').removeClass('LoadingTag').html(Text);
                            $("#MainDivDoneTag" + ID).hide("slow");
                            $("#MainDivDealTag" + ID).show("slow");
                            setTimeout(function () {
                                $("#Itemtab2").click();
                            }, 500);
                        }
                        else {

                            MessegeArea("Some Error occurred while saving data.<br>Please contact Admin" , 'error');
                            $("#DoneTag" + ID).addClass('like').removeClass('LoadingTag').html(Text);
                            $("#MainDivDoneTag" + ID).show("slow");
                            $("#MainDivDealTag" + ID).hide("slow");

                        }
                    }

                });

            }, 1000);
        }
        // alert(document.getElementById('Tag' + ID).value);

        function enableSub(a) {
            //$("html, body").animate({ scrollTop: $("#sub_check_container_" + a).show().offset().top }, "fast");

            // window.location.href = "getStateLocalTime.aspx?Subscribe=" + a;




            if (!$("#sub_check_container_" + a).find('input[name="geoId"]').attr("checked")) {

                $("#sub_check_container_" + a).show().find('input[name="geoId"]').attr("checked", "checked").change();
                $("#" + a + "_prog").show('slow');
                $("#" + a + "_check").hide();



                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?Subscribe=" + a,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {

                        if (msg == "True") {

                            $("#sub_check_container_" + a).show().find('input[name="geoId"]').attr("checked", "checked").change();
                            $("#" + a + "_prog").hide();
                            $("#" + a + "_done").show();
                            setTimeout(function () {
                                $("#" + a + "_done").hide('slow');
                                $("#" + a + "_check").show('slow');
                            }, 2000);
                        }
                    }

                });


            }
            else {
                $("#sub_check_container_" + a).stop(false, true).show("highlight", "slow");
            }
        }

        function Unsubscribe(a) {
            var OrignalValue = $("#Left" + a).html();
            if (OrignalValue == "Please wait...") {
                return;
            }
            $("#Left" + a).html("Please wait...");
            $("#Left" + a).addClass("icon LoadingTag");
            //$("#" + a + "_check").hide();
            //$("#" + a + "_prog").show('slow');
            $.ajax({
                type: "POST",
                url: "getStateLocalTime.aspx?UnSubscribe=" + a,
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg == "True") {
                        //$("#" + a + "_prog").hide();
                        //$("#" + a + "_done").show();

                        setTimeout(function () {
                        $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/tick.png'  /></span><span>You have been successfully Unsubscribed to <b>" + OrignalValue + "</b></span>"),
                                                    {
                                                        sticky: true,
                                                        glue: 'before',
                                                        speed: 2500,
                                                        easing: 'easeOutBounce'
                                                    };
                            $("#sub_check_container_" + a).hide('fast');
                        }, 2000);
                    }
                    else {
                        MessegeArea(" Some Error occurred while saving data.<br>Please contact Admin" , 'error');
                    }
                }

            });
        }

        function DummySaved(a) {

            $("#" + a + "_prog").hide('slow');


            setTimeout(function () {
                $("#" + a + "_done").show('slow');
            }, 1000);


            setTimeout(function () {
                $("#" + a + "_done").hide('slow');
                $("#" + a + "_check").show('slow');
            }, 2000);



        }

    </script>
    <div>
    

        <div class="DetailPage2ndDiv">
            <div style="width: 980px; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                    
                    <div style="clear: both; padding-top: 10px; margin-bottom: 10px;">
                        <div class="DetailPageTopDiv">
                            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                                <div id="PageTitle" class="PageTopText" style="float: left;">
                                    My Subscriptions
                                </div>
                            </div>
                        </div>
                    </div>                   
                                                                           
                </div>
                <div style="background-color: White; min-height: 450px;">
                    <div style="width: 100%">
                        <div style="width: 100%; background-image: url('Images/FundBG.jpg'); background-repeat: repeat-x;
                            overflow: hidden;">
                            <div id="TabDiv1" onclick="javascript:Tab1();" class="CurrentActiveTab">
                                <div style="color: White; font-size: 15px; font-weight: bold; line-height: 50px;
                                    text-align: center;">
                                    Where
                                </div>
                            </div>
                            <div id="TabDiv2" onclick="javascript:Tab2();" class="NormalTab">
                                <div style="color: White; font-size: 15px; font-weight: bold; line-height: 50px;
                                    text-align: center;">
                                    What
                                </div>
                            </div>
                            <div id="TabDiv3" onclick="javascript:Tab3();" class="NormalTab">
                                <div style="color: White; font-size: 15px; font-weight: bold; line-height: 50px;
                                    text-align: center;">
                                    Who
                                </div>
                            </div>
                        </div>
                        <div style="width: 100%; overflow: hidden;">
                            <div class="CurrentActiveTabBottom" id="BottomArrow1" style="width: 33%; height: 15px;
                                float: left;">
                            </div>
                            <div id="BottomArrow2" style="width: 33%; height: 15px; float: left;">
                            </div>
                            <div id="BottomArrow3" style="width: 33%; height: 15px; float: left;">
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom: 0px !important;" class="coda-slider-wrapper">
                        <div class="coda-slider-wrapper">
                            <div class="coda-slider preload" id="coda-slider-1">
                                <div class="panelSlider">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="panel-wrapper">
                                                <h2 id="tab1" style="display: none;" class="title">
                                                    Where</h2>
                                                <div style="padding-left: 15px;" class="MemberArea_PageHeading">
                                                    Where you want your Tasty deals ?
                                                </div>
                                                <asp:Literal ID="ltCities" runat="server"></asp:Literal>
                                                <div style="margin-top: 15px; float: left; clear: both; margin-left: 15px;">
                                                    <div class="button-group">
                                                        <div style='cursor: default !important;height:14px !important;  font: 11px sans-serif !important;' class='CityNameButton primary'>
                                                            Take a few second to add the city of your choice</div>
                                                        <a href="member_SubscribeCities.aspx" style="border-color: #3072B3 #3072B3 #2A65A0;
                                                            border-style: solid; border-width: 1px;font: 11px sans-serif !important;" class="button green icon settings">Manage
                                                            my subscription</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="panelSlider">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="panel-wrapper">
                                                <h2 id="Tab2" style="display: none;" class="title">
                                                    What</h2>
                                                <div style="padding-left: 15px; padding-bottom: 15px;" class="MemberArea_PageHeading">
                                                    My Favorite Deals
                                                </div>
                                                <div style="height: auto; padding-left: 15px;">
                                                    <asp:Literal ID="ltMyDealtags" runat="server"></asp:Literal>
                                                </div>
                                                <div style="clear: both; margin-top: 10px; height: auto;">
                                                    <asp:Literal ID="ltDealTags" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="panelSlider">
                                    <asp:UpdatePanel ID="uppnltab3" runat="server">
                                        <ContentTemplate>
                                            <div class="panel-wrapper">
                                                <h2 id="tab3" style="display: none;" class="title">
                                                    Who</h2>
                                                <div style="padding-left: 15px; padding-bottom: 15px;" class="MemberArea_PageHeading">
                                                    My Profile
                                                </div>
                                                <div style="padding-left: 15px; clear: both; font-size: 15px;">
                                                    Its all about me
                                                </div>
                                                <div class="contactus_table" style="font-size: 16px; color: #636363; margin-right: 25px;
                                                    font-family: Arial;">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style="padding-left: 95px; min-height: 40px;" colspan="2">
                                                                <div style="float: left; padding-right: 5px">
                                                                    <asp:Image ID="imgGridMessageProfile" runat="server" Visible="false" ImageUrl="images/error.png" />
                                                                </div>
                                                                <div style="margin-left: 5px; float: left; text-align: left;">
                                                                    <asp:Label ID="lblErrorMessageProfile" runat="server" Visible="false"></asp:Label></div>
                                                            </td>
                                                        </tr>
                                                        <div style="clear: both; padding-left: 15px; padding-top: 20px;">
                                                            <div>
                                                                <div class="ItemHiding">
                                                                    First Name
                                                                </div>
                                                                <div style="margin-bottom: 5px;">
                                                                    <asp:TextBox ID="txtFirstName" ToolTip="Enter your first name" onfocus="this.className='TextBox'" runat="server"
                                                                        CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="ItemHiding">
                                                                    Last Name
                                                                </div>
                                                                <div style="margin-bottom: 5px;">
                                                                    <asp:TextBox ID="txtLastName" ToolTip="Enter your last name" onfocus="this.className='TextBox Tipsy'" runat="server" CssClass="Tipsy TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="ItemHiding">
                                                                    I prefer deals for
                                                                </div>
                                                                <div style="margin-bottom: 5px;">
                                                                    <asp:DropDownList ID="ddlDealsFor" runat="server" onfocus="this.className='Tipsy TextBox'"
                                                                        Height="30px" CssClass="TextBox Tipsy">
                                                                        <asp:ListItem Selected="True" Value="Select">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                        <asp:ListItem Value="Both">Both</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="ItemHiding">
                                                                    My birthday
                                                                </div>
                                                                <div style="margin-bottom: 5px;">
                                                                    <div style="float: left;">
                                                                        <asp:DropDownList onfocus="this.className='TextBox'" ID="ddYEar"
                                                                            Width="95px" runat="server" Height="30px" CssClass="TextBox">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div style="float: left; margin-left: 8px;">
                                                                        <asp:DropDownList AutoPostBack="true" onfocus="this.className='TextBox'" ID="ddMonth"
                                                                            Width="95px" runat="server" Height="30px" CssClass="TextBox" OnSelectedIndexChanged="ddMonth_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div style="float: left; margin-left: 8px;">
                                                                        <asp:DropDownList ID="ddDate" onfocus="this.className='TextBox'" Width="70px" runat="server"
                                                                            Height="30px" CssClass="TextBox">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="ItemHiding">
                                                                    Home town postal Code
                                                                </div>
                                                                <div style="margin-bottom: 5px;">
                                                                    <asp:TextBox ID="txtZipCode" onfocus="this.className='TextBox'" runat="server" MaxLength="100"
                                                                        CssClass="TextBox"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div style="margin-top: 25px;">
                                                                    <asp:Button runat="server" ID="btnChange" OnClientClick="return WhoValidation();"
                                                                        CssClass="button big primary" Text="Save" ValidationGroup="ChangePassword" OnClick="btnChange_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </table>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddMonth" EventName="SelectedIndexChanged" />                                            
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <!-- .coda-slider -->
                        </div>
                        <!-- .coda-slider -->
                    </div>
                    <asp:Literal ID="TabScript" runat="server"></asp:Literal>
                    <asp:Literal ID="ltJavascript" runat="server"></asp:Literal>
                    <asp:Literal ID="ltScript" runat="server"></asp:Literal>
                </div>
            </div>
           
        </div>
    </div>
    <asp:Label ID="lblerror" runat="server"></asp:Label>
</asp:Content>
