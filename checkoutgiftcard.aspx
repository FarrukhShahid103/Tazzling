<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="checkoutgiftcard.aspx.cs"
    Inherits="checkoutgiftcard" Title="Buy Gift Card" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                

    <link href="CSS/colorbox.css" rel="stylesheet" type="text/css" />


    <link href="CSS/tipsy.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Login.css" rel="stylesheet" type="text/css" />
    <link href="CSS/AlertBox.css" rel="stylesheet" type="text/css" />
    <div id="fb-root"></div>
    <script src="https://connect.facebook.net/en_US/all.js"></script>

   <script>
			FB.init({
			appId: '160996503945227',
			oauth : true,
			status: true,
			cookie: true,
			xfbml: true
			});
			FB.Event.subscribe('auth.login', function(response) {
			
			var ACC_Tokken = response.authResponse.accessToken;
			
			
			
			var c_login = 'tastygoLogin';
            c_login_start=-1;        
            
            if (document.cookie.length>0)
            {
                c_login_start=document.cookie.indexOf(c_login + "=");
            }
           if(c_login_start==-1)
            {
                            $.ajax({
                            type: "POST",
                            url: "getStateLocalTime.aspx?FBLogin=" +  ACC_Tokken,
                            contentType: "application/json; charset=utf-8",
                            async: true,
                            cache: false,
                            success: function(msg) 
                             {
                                
                                if(msg=="true")
                                {
                               window.location.href = "Default.aspx";
                                }
                                
                             
                             }
                             
                             });        
                             
                     }        
			
				//window.location.reload();
			});
					
   
			function login()
			{			  
			FB.login(function(response) {
            if (response.authResponse) {          
            
            var ACC_Tokken = response.authResponse.accessToken;
			
			
            
            $.ajax({
                            type: "POST",
                            url: "getStateLocalTime.aspx?FBLogin=" +  ACC_Tokken,
                            contentType: "application/json; charset=utf-8",
                            async: true,
                            cache: false,
                            success: function(msg) 
                             {
                                
                                if(msg=="true")
                                {
                                 window.location.href = "Default.aspx";
                                }
                                
                             
                             }
                             
                             });        
            
            
            
            
           // window.location.reload();        
    } else {
    // user is not logged in
    }
        }, {scope:'read_stream,publish_stream,offline_access,email'});
}
    </script>

    <script>
        $(document).ready(function() {
         document.getElementById("ctl00_ContentPlaceHolder1_ddl5Quntity").selectedIndex=0;
         document.getElementById("ctl00_ContentPlaceHolder1_ddl10Quntity").selectedIndex=0;
          document.getElementById("ctl00_ContentPlaceHolder1_ddl20Quntity").selectedIndex=0;
           document.getElementById("ctl00_ContentPlaceHolder1_ddl50Quntity").selectedIndex=0;           
	});

    </script>

    <script language="javascript" type="text/javascript">    
     function onPostalCodeFoucs()
     {           
            if($("#ctl00_ContentPlaceHolder1_hfPostalCode").val()=="0")
            {
	            $("#ctl00_ContentPlaceHolder1_txtPostalCode").mask("***-***");
	            $("#ctl00_ContentPlaceHolder1_hfPostalCode").val('1');	            
	        }	   
     }
           
    

        function uncheckOthers(id) {
            var elm = document.getElementsByTagName('input');
            for (var i = 0; i < elm.length; i++) {
                if (elm.item(i).id.substring(id.id.lastIndexOf('_')) == id.id.substring(id.id.lastIndexOf('_'))) {
                    if (elm.item(i).type == "radio" && elm.item(i) != id)
                        elm.item(i).checked = false;
                }
            }
        }

        function checknum(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if ((keyEntry == '48')) {
                var str = document.getElementById(event.srcElement.name).value;
                if ((str.indexOf('0') == -1 || str.indexOf('0') == 0) && str.length <= 0)
                    return false;
                else if (str.indexOf('0') == 0 && str.length > 0)
                    return false;
            }
            else {
                if ((keyEntry > '47') && (keyEntry < '58') || (keyEntry == '46'))
                    return true;
                else if (keyEntry == '8')
                    return true;
                else
                    return false;
            }
        }
        


        function count() {

            var Qty5 = document.getElementById("ctl00_ContentPlaceHolder1_ddl5Quntity").value;
            var Qty10 = document.getElementById("ctl00_ContentPlaceHolder1_ddl10Quntity").value;
            var Qty20 = document.getElementById("ctl00_ContentPlaceHolder1_ddl20Quntity").value;
            var Qty50 = document.getElementById("ctl00_ContentPlaceHolder1_ddl50Quntity").value;
                      
            var varhfPayFull = document.getElementById("ctl00_ContentPlaceHolder1_hfPayFull").value;            
          /*  if (Qty == "" || Qty == "0") {
                // document.getElementById("ctl00_ContentPlaceHolder1_txtQuntity").value=1;
                Qty = 1;
            }*/            
            var Total5 = document.getElementById("ctl00_ContentPlaceHolder1_lbl5TotalPrice");
            var Total10 = document.getElementById("ctl00_ContentPlaceHolder1_lbl10TotalPrice");
            var Total20 = document.getElementById("ctl00_ContentPlaceHolder1_lbl20TotalPrice");
            var Total50 = document.getElementById("ctl00_ContentPlaceHolder1_lbl50TotalPrice");            
            var GTotal = document.getElementById("ctl00_ContentPlaceHolder1_lblGrandTotal"); 
            var GhfTotal = document.getElementById("ctl00_ContentPlaceHolder1_hfGrandTotal");             
           // alert(GhfTotal.value);
            Total5.innerHTML = (Qty5 * 5) ;
            Total10.innerHTML = (Qty10 * 10) ;
            Total20.innerHTML = (Qty20 * 20) ;
            Total50.innerHTML = (Qty50 * 50) ;
                        
            GTotal.innerHTML = (Qty5 * 5) + (Qty10 * 10)+(Qty20 * 20)+(Qty50 * 50);
            GhfTotal.value = (Qty5 * 5) + (Qty10 * 10)+(Qty20 * 20)+(Qty50 * 50);
          
            //var OrderedAmt = (Qty5 * 5) + (Qty10 * 10)+(Qty20 * 20)+(Qty50 * 50);
               if(varhfPayFull!="0")
               {
               window.location.reload(true);
               }
          
        }

        function isPostCodeLocal(oSrc, args) {

            // checks cdn codes only          

            entry = args.Value;
            //alert(entry);


            strlen = entry.length;
            if (strlen != 7) {
                args.IsValid = false;
                return;
            }
            entry = entry.toUpperCase(); //in case of lowercase
            //Check for legal characters,index starts at zero
            s1 = 'ABCEGHJKLMNPRSTVXY'; s2 = s1 + 'WZ'; d3 = '0123456789';


            if (s1.indexOf(entry.charAt(0)) < 0) {
                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(1)) < 0) {
                args.IsValid = false;
                return;
            }
            if (s2.indexOf(entry.charAt(2)) < 0) {
                args.IsValid = false;
                return;
            }
            if (entry.charAt(3) != '-') {

                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(4)) < 0) {
                args.IsValid = false;
                return;
            }
            if (s2.indexOf(entry.charAt(5)) < 0) {
                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(6)) < 0) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
            return;
        }

        function luhn(oSrc, args) {

            if (args.Value != "") {
                var num = args.Value;
                num = (num + '').replace(/\D+/g, '').split('').reverse();
                if (!num.length) {
                    args.IsValid = false;
                    return;
                }
                var total = 0, i;
                for (i = 0; i < num.length; i++) {
                    num[i] = parseInt(num[i])
                    total += i % 2 ? 2 * num[i] - (num[i] > 4 ? 9 : 0) : num[i];
                }
                if ((total % 10) == 0 || (total % 10) == 5) {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;
                    return;
                }
            }
        }

        function checkAgreement(oSrc, args) {
           // alert("validate start");
            var elem = document.getElementById('<%= cbAgree.ClientID %>');
            //alert(elem.checked);
            if (elem.checked) {
                args.IsValid = true;
                return;
            }
            else {
                alert("Please accept terms and conditions.");
                args.IsValid = false;
                return;
            }

        }

        function numbersonly(myfield, e, dec) {
            var key;
            var keychar;

            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            
            keychar = String.fromCharCode(key);

            // control keys
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27))
                return true;
            // numbers
            else if ((("0123456789").indexOf(keychar) > -1)) {
            if ((parseFloat(myfield.value + keychar) > (document.getElementById("ctl00_ContentPlaceHolder1_hfRefAmt").value))
            || (parseFloat(myfield.value + keychar) > (document.getElementById("ctl00_ContentPlaceHolder1_hfGrandTotal").value))) {
                //If entered value is greater than Tasty Credit                
                    document.getElementById("ctl00_ContentPlaceHolder1_txtTastyCredit").value = "0";
                    return false;
                }
                else if(parseFloat(myfield.value + keychar) == (document.getElementById("ctl00_ContentPlaceHolder1_hfGrandTotal").value))
                {
                   // alert("Payment is equal");
                    return true;
                }
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {
                myfield.form.elements[dec].focus();
                return false;
            }
            else
                return false;
        }
        
    </script>

    <%-- <script type="text/javascript">
     function RadioCheck(rb, address) {
        document.getElementById("<%=hfAddressID.ClientID%>").value = rb.value;
        document.getElementById("<%=lblBusinessAddress.ClientID%>").innerHTML = address;
        var gv = document.getElementById("<%=grdViewAddress.ClientID%>");
        var rbs = gv.getElementsByTagName("input");
        var row = rb.parentNode.parentNode;
        for (var i = 0; i < rbs.length; i++) {
            if (rbs[i].type == "radio") {
                if (rbs[i].checked && rbs[i] != rb) {
                    rbs[i].checked = false;
                    break;
                }
            }
        }
     }
    </script>--%>

    <script>
        $(document).ready(function() {
            $("#button1").click(function() {
                $("#button1").hide();
                $("#button2").show();
                $("#contentdiv").hide();
                $("#formdiv").slideDown();
            });
            /*$("#button2").click(function(){
            alert('form posted');
            });*/
            $("#cancel").click(function() {
                $("#button1").show();
                $("#button2").hide();
                $("#formdiv").hide();
                $("#contentdiv").slideDown();
            });
        });
    </script>

    <asp:Panel ID="pnlinner" runat="server" DefaultButton="btnCompleteOrder">
    <asp:HiddenField ID="hfTastyCredit" runat="server" Value="0" />
    <asp:HiddenField ID="hfComissionMoney" runat="server" Value="0" />
        <div class="PagesBG">
            <div>               
                <div style="float: right; padding: 10px;">
                    <div style="height: 130px; width: 160px; border: solid 1px #F9A736; background-color: #FEFBDE;">
                        <div style="height: 24px; padding-top: 4px; border-bottom: solid 1px #F9A736; padding-left: 10px;
                            font-family: Arial; font-size: 16px;">
                            Buy with confidence
                        </div>
                        <div style="text-align: center;">
                            <%-- <img src="Images/verisign.jpg" />--%>
                            <div id="myDiv">

                                <script type="text/javascript" src="https://seal.verisign.com/getseal?host_name=www.tazzling.com&amp;size=M&amp;use_flash=YES&amp;use_transparent=YES&amp;lang=en"></script>

                            </div>
                            <div>
                                <a href="https://www.verisign.com/ssl-certificate/" target="_blank" style="color: #000000;
                                    text-decoration: none; font: bold 7px verdana,sans-serif; letter-spacing: .5px;
                                    text-align: center; margin: 0px; padding: 0px;">ABOUT SSL CERTIFICATES</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both; padding-top: 10px;">
                <div style="height: 27px; border: solid 1px #B7B7B7; background-color: #DBDBDB; color: #444444;
                    font-family: Arial; font-size: 16px; font-weight: bold; padding-top: 8px;">
                    <div style="float: left; padding-left: 20px;">
                        Item Description</div>
                    <div style="float: left; padding-left: 380px;">
                        Quantity</div>
                    <div style="float: left; padding-left: 130px;">
                        Price</div>
                    <div style="float: left; padding-left: 120px;">
                        You Pay</div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div style="height: 75px; border: solid 1px #B7B7B7; border-top: none; background-color: #F5F5F5;
                            color: #444444; font-family: Arial; font-size: 13px;">
                            <div style="float: left; padding-left: 20px; width: 500px; padding-top: 25px;">
                                <div>
                                    <asp:Label ID="lblDealTitle" Width="450px" runat="server" Text="$5 Gift Card"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; padding-left: 10px; width: 100px; padding-top: 20px;">
                                <asp:DropDownList ID="ddl5Quntity" runat="server" onchange="count();" CssClass="ddQty">
                                    <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 10px; font-size: 32px; font-weight: bold;
                                width: 50px; padding-top: 5px; color: #C1C0C0; padding-right: 10px; padding-top: 30px;">
                                X</div>
                            <div style="float: left; padding-left: 10px; width: 120px; padding-top: 30px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl5Price" runat="server" Font-Bold="true" Font-Size="32px" Text="5"></asp:Label></div>
                            </div>
                            <div style="float: right; padding-left: 40px; padding-top: 30px; width: 95px; background-color: #ffd456;
                                height: 45px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl5TotalPrice" runat="server" Font-Bold="true" Font-Size="32px" Text="0"></asp:Label></div>
                            </div>
                        </div>
                        <div style="height: 75px; border: solid 1px #B7B7B7; border-top: none; background-color: #F5F5F5;
                            color: #444444; font-family: Arial; font-size: 13px;">
                            <div style="float: left; padding-left: 20px; width: 500px; padding-top: 25px;">
                                <div>
                                    <asp:Label ID="Label6" Width="450px" runat="server" Text="$10 Gift Card"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; padding-left: 10px; width: 100px; padding-top: 20px;">
                                <asp:DropDownList ID="ddl10Quntity" runat="server" onchange="count();" CssClass="ddQty">
                                    <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 10px; font-size: 32px; font-weight: bold;
                                width: 50px; padding-top: 5px; color: #C1C0C0; padding-right: 10px; padding-top: 30px;">
                                X</div>
                            <div style="float: left; padding-left: 10px; width: 120px; padding-top: 30px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl10Price" runat="server" Font-Bold="true" Font-Size="32px" Text="10"></asp:Label></div>
                            </div>
                            <div style="float: right; padding-left: 40px; padding-top: 30px; width: 95px; background-color: #ffd456;
                                height: 45px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl10TotalPrice" runat="server" Font-Bold="true" Font-Size="32px"
                                        Text="0"></asp:Label></div>
                            </div>
                        </div>
                        <div style="height: 75px; border: solid 1px #B7B7B7; border-top: none; background-color: #F5F5F5;
                            color: #444444; font-family: Arial; font-size: 13px;">
                            <div style="float: left; padding-left: 20px; width: 500px; padding-top: 25px;">
                                <div>
                                    <asp:Label ID="Label10" Width="450px" runat="server" Text="$20 Gift Card"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; padding-left: 10px; width: 100px; padding-top: 20px;">
                                <asp:DropDownList ID="ddl20Quntity" runat="server" onchange="count();" CssClass="ddQty">
                                    <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 10px; font-size: 32px; font-weight: bold;
                                width: 50px; padding-top: 5px; color: #C1C0C0; padding-right: 10px; padding-top: 30px;">
                                X</div>
                            <div style="float: left; padding-left: 10px; width: 120px; padding-top: 30px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl20Price" runat="server" Font-Bold="true" Font-Size="32px" Text="20"></asp:Label></div>
                            </div>
                            <div style="float: right; padding-left: 40px; padding-top: 30px; width: 95px; background-color: #ffd456;
                                height: 45px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl20TotalPrice" runat="server" Font-Bold="true" Font-Size="32px"
                                        Text="0"></asp:Label></div>
                            </div>
                        </div>
                        <div style="height: 75px; border: solid 1px #B7B7B7; border-top: none; background-color: #F5F5F5;
                            color: #444444; font-family: Arial; font-size: 13px;">
                            <div style="float: left; padding-left: 20px; width: 500px; padding-top: 25px;">
                                <div>
                                    <asp:Label ID="Label13" Width="450px" runat="server" Text="$50 Gift Card"></asp:Label>
                                </div>
                            </div>
                            <div style="float: left; padding-left: 10px; width: 100px; padding-top: 20px;">
                                <asp:DropDownList ID="ddl50Quntity" runat="server" onchange="count();" CssClass="ddQty">
                                    <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 10px; font-size: 32px; font-weight: bold;
                                width: 50px; padding-top: 5px; color: #C1C0C0; padding-right: 10px; padding-top: 30px;">
                                X</div>
                            <div style="float: left; padding-left: 10px; width: 120px; padding-top: 30px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl50Price" runat="server" Font-Bold="true" Font-Size="32px" Text="50"></asp:Label></div>
                            </div>
                            <div style="float: right; padding-left: 40px; padding-top: 30px; width: 95px; background-color: #ffd456;
                                height: 45px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lbl50TotalPrice" runat="server" Font-Bold="true" Font-Size="32px"
                                        Text="0"></asp:Label></div>
                            </div>
                        </div>
                        <div style="height: 75px; border: solid 1px #B7B7B7; border-top: none; background-color: #F5F5F5;
                            color: #444444;">
                            <div style="float: right; padding-left: 40px; padding-top: 30px; width: 95px; background-color: #f99d1c;
                                height: 45px;">
                                <div style="float: left;">
                                    <sup>C$</sup></div>
                                <div style="float: left; padding-left: 5px; padding-top:2px;  padding-bottom:2px;">
                                    <asp:Label ID="lblGrandTotal" runat="server" Font-Bold="true" Font-Size="32px" Text="0"></asp:Label>
                                    <asp:HiddenField ID="hfGrandTotal" runat="server"></asp:HiddenField>
                                </div>
                            </div>
                            <div style="float: right; padding-left: 10px; width: 140px; padding-top: 30px;">
                                <asp:Label ID="lblTotalText" runat="server" Font-Bold="true" Font-Size="24px" Text="TOTAL"></asp:Label></div>
                        </div>
                        <div style="padding: 15px;">
                            <asp:Label ID="Label1" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="24px"
                                runat="server" Text="Your Payment Information"></asp:Label>
                        </div>
                        <asp:HiddenField ID="hfPayFull" Value="0" runat="server" />
                        <div id="divRefBal" runat="server" visible="false" style="border-top: solid 1px #B7B7B7;
                            border-left: solid 1px #B7B7B7; border-right: solid 1px #B7B7B7; background-color: #F5F5F5;
                            color: #444444; font-family: Arial; font-size: 16px; font-weight: bold; height: 120px;">
                            <div style="padding-left: 12px; padding-top: 12px; padding-bottom: 12px; font-weight: bold;
                                font-family: Arial; font-size: 19px; width: 700px;">
                                <div style="float: left; padding-right: 30px;">
                                    Pay with my Tasty Credits&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblRefBal" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                </div>
                                <div style="float: left;">
                                </div>
                            </div>
                            <div class="height15">
                            </div>
                            <div style="padding-left: 470px; padding-top: 15px; padding-bottom: 12px; text-align: right;
                                padding-right: 26px; clear: both;">
                                <div style="float: left; padding-right: 5px; padding-top: 10px;">
                                    <span style='color: #F99D1C; font-family: Arial; font-size: 16px; font-weight: bold'>
                                        Pay it from Tasty Credits &nbsp;(max. C$<asp:Label ID="lblRefBalanace" runat="server"></asp:Label>
                                        )</span>
                                </div>
                                <div style="float: left; padding-right: 5px; padding-top: 3px;">
                                    <asp:TextBox ID="txtTastyCredit" runat="server" Width="63" MaxLength="3" onKeyPress="return numbersonly(this, event);"
                                        CssClass="TextBoxDeal"></asp:TextBox>
                                    <cc1:RequiredFieldValidator ID="rfvTastygoCredit" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtTastyCredit" ErrorMessage="value required!" ValidationGroup="Apply"
                                        Display="None"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcdPassword" runat="server" TargetControlID="rfvTastygoCredit">
                                    </cc2:ValidatorCalloutExtender>
                                </div>
                                <div style="float: left;">
                                    <asp:ImageButton ID="btnApply" ValidationGroup="Apply" CausesValidation="true" OnClick="btnApply_Click"
                                        runat="server" ImageUrl="~/Images/btnApply.png" />
                                </div>
                            </div>
                        </div>
                        <div id="divDeliveryGridCCI" runat="server" style="border: solid 1px #B7B7B7; background-color: #F5F5F5;
                            color: #444444; font-family: Arial; font-size: 16px; font-weight: bold;">
                            <div style="padding-left: 12px; padding-top: 12px; padding-bottom: 12px; font-weight: bold;
                                font-family: Arial; font-size: 19px; width: 700px;">
                                <div style="float: left; padding-right: 30px;">
                                    Pay With Existing Card
                                </div>
                                <div style="float: left;">
                                    <asp:Image ID="imgGridMessage" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                                </div>
                            </div>
                            <div style="text-align: center; padding-bottom: 12px; clear: both; float: left;">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="10"
                                    CellSpacing="0" OnRowDataBound="gvCustomer_RowDataBound" GridLines="None" Width="726px"
                                    ShowHeader="false" OnRowCommand="gvCustomers_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="19%" HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="MyRadioButton" runat="server" OnCheckedChanged="CheckChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                            <ItemStyle Width="19%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Your Credit Cards">
                                            <ItemTemplate>
                                                <div style="font-family: Arial; font-size: 21px; width: 311px; text-align: left;
                                                    padding-left: 20px;">
                                                    <%# GetCardExplain(Eval("ccInfoNumber"))%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <div style="font-family: Arial; font-size: 21px; text-align: left; padding-left: 40px;">
                                                    <asp:HiddenField ID="hfccInfoID" Value='<%#Eval("ccInfoID")%>' runat="server" />
                                                    <asp:LinkButton ID="btnEdit" Text="Edit Card Info" CommandName="EditCustomer" CausesValidation="false"
                                                        CommandArgument='<%#Eval("ccInfoID")%>' runat="server"></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="padding-left: 12px; padding-top: 15px; padding-bottom: 12px; font-weight: bold;
                                font-family: Arial; font-size: 19px; width: 600px;">
                                <asp:LinkButton ID="btnAddNewCCI" Text="Add New Card" CausesValidation="false" runat="server"
                                    OnClick="btnAddNewCCI_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfMode" runat="server" Value="new" />
                        <asp:HiddenField ID="hfRefAmt" runat="server" Value="0" />
                        <div id="divDelivery1" runat="server" style="height: 220px; border: solid 1px #B7B7B7;
                            border-bottom: none; background-color: #F5F5F5; color: #444444; font-family: Arial;
                            font-size: 16px; font-weight: bold;">
                            <div style='float: left; padding-top: 90px; padding-left: 30px; display: <%=strhideDive %>;'>
                                <asp:CheckBox ID="cbMasterCard" runat="server" Checked="true" Visible="false" Text="" />
                            </div>
                            <div style='float: left; padding-top: 30px; padding-left: 20px; width: 400px; line-height: normal;
                                display: <%=strhideDive %>;'>
                                <div>
                                    <asp:HyperLink ID="lblNote" runat="server" Font-Size="13px" Font-Names="Arial" Text="Your card will only be charged if the minimum buy is reached. For more info please visit our FAQ"
                                        Target="_blank" NavigateUrl="~/faq.aspx" Font-Underline="false" ForeColor="#636363"></asp:HyperLink>
                                </div>
                                <div style="padding-top: 15px;">
                                    <img id="imgMasterCard" src="Images/checkoutVisaMasterCards.jpg" title="Pay With Visa or Master Card." />
                                </div>
                            </div>
                            <div id="divLogin" runat="server" style="float: left; padding-top: 15px; padding-left: 50px;">
                                <div style="height: 190px; width: 216px; border: solid 1px #F9A736; background-color: #FEFBDE;">
                                    <div id="contentdiv" style="padding-top: 10px; height: 123px; padding-left: 20px;
                                        line-height: 24px;">
                                        <asp:Label ID="lblCompanyDetail" Font-Bold="false" ForeColor="#F99D1C" Font-Size="16px"
                                            runat="server" Text="If you have tastygo account please click here to login."></asp:Label>
                                    </div>
                                     <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">
                                    <div id="formdiv" style="padding-top: 10px; height: 123px; padding-left: 20px; line-height: 24px;
                                        display: none;">
                                        <div style="padding: 2px;">
                                            Email</div>
                                        <div>
                                            <asp:TextBox ID="txtLoginEmailAddress" runat="server"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtLoginEmailAddress" ErrorMessage="Email required!" ValidationGroup="btnLogin"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator9">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                                runat="server" ControlToValidate="txtLoginEmailAddress" ErrorMessage="Invalid email address!"
                                                ValidationGroup="btnLogin" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RegularExpressionValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding: 2px;">
                                            Password</div>
                                        <div>
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true"
                                                runat="server" ControlToValidate="txtPassword" ErrorMessage="Password required!"
                                                ValidationGroup="btnLogin" Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RequiredFieldValidator10">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding: 0px;">
                                            <a id="cancel" href="#">Cancel</a></div>
                                    </div>
                                    <div id="button1">
                                        <img id="Img2" src="Images/CheckoutbtnSignIn.jpg" style="cursor: pointer;" /></div>
                                    <div id="button2" style="display: none">
                                        <asp:ImageButton ID="btnLogin" runat="server" ValidationGroup="btnLogin" ImageUrl="~/Images/CheckoutbtnSignIn.jpg"
                                            OnClick="btnLogin_Click" /></div>
                                            </asp:Panel>
                                </div>
                            </div>
                            <div id="divFacebook" runat="server" style="float: left; padding-top: 15px; padding-left: 50px;">
                                <img id="imgfbConnect" src="Images/checkoutFBConnect.jpg" style="cursor: pointer;
                                    height: 193px; widows: 220px;" onclick='login();' />
                            </div>
                        </div>
                        <div id="divDelivery2" runat="server" style="height: 250px; border: solid 1px #B7B7B7;
                            background-color: #F5F5F5; color: #444444; font-family: Arial; font-size: 16px;
                            font-weight: bold;">
                            <div style="padding-top: 20px; padding-bottom: 10px; padding-left: 30px;">
                                <asp:Label ID="Label2" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="19px" runat="server"
                                    Text="Delivery Information"></asp:Label>
                            </div>
                            <div style="padding-left: 30px; line-height: normal;">
                                <asp:Label ID="Label3" Width="95%" Font-Bold="false" ForeColor="Black" Font-Size="16px"
                                    runat="server" Text="The tastygo Voucher will be emailed to the address below. It will also be available on the receipt page. If you to not receive your Voucher, please contact support@tazzling.com."></asp:Label>
                            </div>
                            <div style="padding-top: 20px; padding-left: 140px;">
                                <div style="width: 100%">
                                    <div style="float: left; width: 40%; text-align: left">
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblDFirstName" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* First Name"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                            <asp:HiddenField ID="hfCCInfoIdEdit" runat="server" />
                                            <asp:TextBox ID="txtFirstname" TabIndex="1" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvUserName" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtFirstname" ErrorMessage="First Name required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="vcdUserName" runat="server" TargetControlID="rfvUserName">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblDEmail" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                                                Text="* Email Address"></asp:Label></div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:TextBox ID="txtEmail" TabIndex="3" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc2:TextBoxWatermarkExtender ID="txtWaterMarkEmail" runat="server" TargetControlID="txtEmail"
                                                WatermarkText="Your email to receive the deal" WatermarkCssClass="watermark_EmailChecout" />
                                            <cc1:RequiredFieldValidator ID="rfvEmailAddress" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtEmail" ErrorMessage="Email required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvEmailAddress">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:RegularExpressionValidator ID="revEmailAddress" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtEmail" ErrorMessage="Invalid email address!" ValidationGroup="CheckOut"
                                                Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                                            <cc2:ValidatorCalloutExtender ID="vceREEmail" runat="server" TargetControlID="revEmailAddress">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 40%; text-align: left; padding-left: 5px;">
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblDLastName" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Last Name"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                            <asp:TextBox ID="txtLastName" TabIndex="2" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvLastName" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtLastName" ErrorMessage="Last Name required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvLastName">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblDCEmailAddress" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Confirm Email Address"></asp:Label></div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:TextBox ID="txtCEmailAddress" TabIndex="4" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc2:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCEmailAddress"
                                                WatermarkText="Your email to receive the deal" WatermarkCssClass="watermark_EmailChecout" />
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtCEmailAddress" ErrorMessage="Email required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:CompareValidator ID="CompareValidator1" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtCEmailAddress" ControlToCompare="txtEmail" ErrorMessage="Email and confirm email must be same!"
                                                ValidationGroup="CheckOut" Display="None"></cc1:CompareValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="CompareValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divDelivery3" runat="server" style="height: 472px; border: solid 1px #B7B7B7;
                            border-top: none; border-bottom: none; background-color: #F5F5F5; color: #444444;
                            font-family: Arial; font-size: 16px; font-weight: bold;">
                            <div style="padding-top: 20px; padding-left: 140px;">
                                <div style="width: 100%">
                                    <div style="float: left; width: 40%; text-align: left">
                                        <div style="padding-bottom: 20px;">
                                            <asp:Label ID="Label8" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="19px" runat="server"
                                                Text="Billing Information"></asp:Label>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblBUserName" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Billing First and Last Name"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                            <asp:TextBox ID="txtBUserName" TabIndex="5" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtBUserName" ErrorMessage="Name required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator2">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblBillingAddress" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Billing Address"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                            <asp:TextBox ID="txtBillingAddress" TabIndex="6" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtBillingAddress" ErrorMessage="Address required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblBCity" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                                                Text="* City"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                            <asp:TextBox ID="txtBCity" runat="server" TabIndex="7" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtBCity" ErrorMessage="City required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator4">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblBProvince" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Province / State"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                          <asp:DropDownList ID="ddlProvince" runat="server" TabIndex="8" CssClass="TextBoxDeal">
                                            </asp:DropDownList>
                                        <%--    <asp:TextBox ID="txtProvince" runat="server" TabIndex="8" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtProvince" ErrorMessage="Province required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RequiredFieldValidator5">
                                            </cc2:ValidatorCalloutExtender>--%>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblPostalCode" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Postal Code"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                            <asp:HiddenField ID="hfPostalCode" runat="server" Value="0" />                                          
                                            <asp:TextBox ID="txtPostalCode" runat="server" onfocus="onPostalCodeFoucs();" TabIndex="9" CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtPostalCode" ErrorMessage="Postal code required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator6">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:CustomValidator ID="revPostalCode" SetFocusOnError="true" runat="server" ControlToValidate="txtPostalCode"
                                                ClientValidationFunction="isPostCodeLocal" ErrorMessage="Enter Valid Postal Code e.g(xxx-xxx)."
                                                ValidationGroup="CheckOut" Display="None"></cc1:CustomValidator>
                                            <cc2:ValidatorCalloutExtender ID="vcePostalCode" runat="server" TargetControlID="revPostalCode">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding-bottom: 5px; padding-top: 4px; clear: both;">
                                            <asp:ImageButton ID="btnSave" ValidationGroup="CheckOut" CausesValidation="true"
                                                runat="server" ImageUrl="~/admin/images/btnUpdate.jpg" OnClick="btnSave_Click" />
                                            &nbsp;
                                            <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                                OnClick="CancelButton_Click" /></div>
                                    </div>
                                    <div style='float: left; width: 40%; text-align: left; padding-left: 5px; display: <%=strhideDive%>;'>
                                        <div style="padding-bottom: 20px;">
                                            <asp:Label ID="Label4" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="19px" runat="server"
                                                Text="Payment Information"></asp:Label><asp:HiddenField ID="hfCCN" runat="server" />
                                        </div>
                                        <div id="divlblCCN" runat="server" style="padding-bottom: 5px;">
                                            <asp:Label ID="lblCCNumber" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Credit Card Number"></asp:Label></div>
                                        <div id="divtxtCCN" runat="server" style="padding-bottom: 15px;">
                                            <asp:TextBox ID="txtCCNumber" TabIndex="10" EnableViewState="False" runat="server"
                                                CssClass="TextBoxDeal"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtCCNumber" ErrorMessage="Credit Card number required!" ValidationGroup="CheckOut"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RequiredFieldValidator7">
                                            </cc2:ValidatorCalloutExtender>
                                            <cc1:CustomValidator ID="cvCreditCard" runat="server" ControlToValidate="txtCCNumber"
                                                ValidateEmptyText="true" ClientValidationFunction="luhn" SetFocusOnError="true"
                                                ValidationGroup="CheckOut" ErrorMessage="Invalid credit card number." Display="None">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</cc1:CustomValidator>
                                            <cc2:ValidatorCalloutExtender ID="vcePhone" runat="server" TargetControlID="cvCreditCard">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:Label ID="lblExpiration" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Expiration"></asp:Label></div>
                                        <div style="padding-bottom: 15px;">
                                            <div style="float: left;">
                                                <asp:DropDownList ID="ddlMonth" TabIndex="11" runat="server" CssClass="ddlDeal">
                                                    <asp:ListItem Value="01" Selected="True">01</asp:ListItem>
                                                    <asp:ListItem Value="02">02</asp:ListItem>
                                                    <asp:ListItem Value="03">03</asp:ListItem>
                                                    <asp:ListItem Value="04">04</asp:ListItem>
                                                    <asp:ListItem Value="05">05</asp:ListItem>
                                                    <asp:ListItem Value="06">06</asp:ListItem>
                                                    <asp:ListItem Value="07">07</asp:ListItem>
                                                    <asp:ListItem Value="08">08</asp:ListItem>
                                                    <asp:ListItem Value="09">09</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: left; padding-left: 10px; font-size: 32px; font-weight: bold;
                                                padding-top: 5px; color: #C1C0C0; padding-right: 10px;">
                                                /</div>
                                            <div style="float: left">
                                                <asp:DropDownList ID="ddlYear" TabIndex="12" runat="server" CssClass="ddlDeal">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="padding-bottom: 5px; padding-top: 15px; clear: both;">
                                            <asp:Label ID="lblCVNumber" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                                                runat="server" Text="* Card verification number"></asp:Label></div>
                                        <div style="padding-bottom: 5px;">
                                            <div style="float: left;">
                                                <asp:TextBox ID="txtCVNumber" runat="server" CssClass="TextBoxDeal" TabIndex="13"
                                                    Width="150px"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server"
                                                    ControlToValidate="txtCVNumber" ErrorMessage="Credit verification number required!"
                                                    ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RequiredFieldValidator8">
                                                </cc2:ValidatorCalloutExtender>
                                                <asp:HiddenField ID="hfCVNumber" runat="server" />
                                            </div>
                                            <div style="float: left; padding-left: 10px;">
                                                <img src="Images/creditCardBack.jpg" />
                                            </div>
                                        </div>
                                        <div style="padding-bottom: 5px; padding-top: 15px; clear: both; font-size: 13px;
                                            font-family: Arial; color: #636363; font-weight: normal;">
                                            <asp:Literal ID="ltUserIP" runat="server" Text=""></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="height: 130px; border: solid 1px #B7B7B7; background-color: #F5F5F5;
                            color: #444444; font-family: Arial; font-size: 16px; font-weight: bold;">
                            <div style="padding-top: 20px; padding-left: 140px;">
                                <div style="float: left; padding-right: 240px;">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="cbAgree" Font-Bold="false" TabIndex="14" ForeColor="#636363" Font-Size="13px"
                                            runat="server" Text="" />
                                    </div>
                                    <div style="float: left; padding-left: 5px;">
                                        <asp:HyperLink ID="hlTermAndCondition" runat="server" ForeColor="#636363" Font-Size="13px"
                                            Text="* I Agree to the Terms & Conditions" Target="_blank" NavigateUrl="~/terms-customer.aspx"></asp:HyperLink>
                                    </div>
                                </div>
                                <div style="float: left;">
                                </div>
                            </div>
                            <div style="padding-top: 40px; text-align: center;">
                                <asp:ImageButton ID="btnCompleteOrder" CausesValidation="true" TabIndex="15" ValidationGroup="CheckOut"
                                    runat="server" ImageUrl="~/Images/btnCompleteOrder.jpg" OnClick="btnCompleteOrder_Click" />
                            </div>
                            <div style="padding-top: 7px; text-align:center;">
                                <asp:Label ID="lblErrorMessage" runat="server" Visible="false" Font-Names="Arial"
                                    Font-Size="16px" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
