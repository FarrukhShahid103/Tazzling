<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="member_SubscribeCities.aspx.cs"
    Inherits="member_SubscribeCities" Title="Subscription Cities" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/FrameStart.ascx" TagPrefix="RedSgnal"
    TagName="FrameStart" %>
<%@ Register Src="~/UserControls/Templates/FrameEnd.ascx" TagPrefix="RedSgnal" TagName="FrameEnd" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="JS/jquery.scrollTo-min.js"></script>

    <script src="JS/jquery.jgrowl.js" type="text/javascript"></script>

    <link href="CSS/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/gh-buttons.css">
    <style lang="en" type="text/css">
        .BGgreen
        {
            background-color: #ae0a20;
        }
        #td
        {
            width: 40px;
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
            width: 105%;
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
            margin-top: 10px;
            padding: 0 20px 5px 0;
            font-size: 12px;
        }
        .sub_column
        {
            width: 430px;
            background: white;
            float: left;
            padding: 0 10px 10px;
            height: 300px;
            overflow: auto;
            border: 1px solid #E6E6E5;
            margin-bottom: 15px !important;
            background-color: #f3f3f3;
            margin-top: 5px;
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
            margin-left: 15px;
            float: left;
        }
        #your_subscriptions
        {
            margin-right: 15px;
            float: right;
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
            background: url( "images/ui-bg_glass_100_fdf5ce_1x400.png" ) repeat-x scroll 50% 50% #C5DBEC;
            border: 1px solid #C5DBEC;
            color: #4297D7;
        }
    </style>

    <script type="text/javascript">
       
        function enableSub(a) {
            if (!$("#TG" + a).is(":visible")) {
                if ($("#Left" + a).html() == "Please Wait...") {
                    return;
                }
                var Text = $("#Left" + a).html();
                $("#Left" + a).html("Please Wait...");
                //$("#" + a + "_prog").show('slow');
                $.ajax({
                    type: "POST",
                    url: "getStateLocalTime.aspx?Subscribe=" + a,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {

                        if (msg == "True") {
                            setTimeout(function () {
                                $("#sub_check_container_" + a).show('slow');
                                $("#Left" + a).html(Text);
                                $("#TG" + a).show();
                                $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/tick.png'  /></span><span>You have been successfully subscribed to <b>" + Text + "</b></span>"),
                                                    {
                                                        sticky: true,
                                                        glue: 'before',
                                                        speed: 2500,
                                                        easing: 'easeOutBounce'
                                                    };
                            }, 2000);
                        }
                    }

                });
            }
            else {
                $('#SubscribedDiv').scrollTo(
                    $("#TG" + a),
                    {
                        speed: 200,
                        axis: 'y'
                    });
                    (function () {

                        $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/Caution.png'  /></span><span>You have already subscribed to <b>" + $("#Left" + a).html() + "</b></span>"),
                                                    {
                                                        sticky: true,
                                                        glue: 'before',
                                                        speed: 2500,
                                                        easing: 'easeOutBounce'
                                                    };

                    var count = 0, $div = $("#TG" + a), interval = setInterval(function () {
                        if ($div.hasClass('BGgreen')) {
                            $div.removeClass('BGgreen'); ++count;
                        }
                        else
                            $div.addClass('BGgreen');

                        if (count === 3) clearInterval(interval);
                    }, 300);
                })();
            }
        }

        function Unsubscribe(a) {
            if ($("#Right" + a).html() == "Please Wait...") {
                return;
            }
            var Text = $("#Right" + a).html();
            $("#Right" + a).html("Please Wait...");
            //$("#TG" + a).hide();
            //$("#" + a + "_prog").show('slow');
            $.ajax({
                type: "POST",
                url: "getStateLocalTime.aspx?UnSubscribe=" + a,
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg == "True") {
                        setTimeout(function () {
                            //$("#" + a + "_done").hide('slow');
                            $(".bottom-right").jGrowl("<span style='padding-right:5px;'><img style='line-height:5px;' src='Images/tick.png'  /></span><span>You have been successfully Unsubscribed to <b>" + Text + "</b></span>"),
                                                    {
                                                        sticky: true,
                                                        glue: 'before',
                                                        speed: 2500,
                                                        easing: 'easeOutBounce'
                                                    };
                            $("#Right" + a).html(Text);
                            $("#sub_check_container_" + a).hide('slow');
                            //$("#TG" + a).hide();
                        }, 2000);
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
            <div style="width: 980px;">
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
                <div style="background-color: White;">
                    <div style="padding-top: 40px;">
                        <div class="MemberArea_PageHeading" style="padding-left: 15px;">
                            My Subscription</div>
                        <div style="font-size: 15px; padding-left: 15px; padding-top: 5px;">
                            My Subscription List</div>
                    </div>
                    <div id="NewSubscription">
                        <asp:Literal ID="ltCities" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>  
    
    <div id='Notify' class='bottom-right' style='position: fixed; bottom: 0px; right: 0px;
        font-size: 11px !important;'>
    </div>
</asp:Content>
