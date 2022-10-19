<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="checkout.aspx.cs"
    Inherits="checkout" Title="Untitled Page" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/colorbox.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery.colorbox.js"></script>

    <script src="JS/jquery.maskedinput-1.2.2.js" type="text/javascript"></script>

    <link href="CSS/jquery.jgrowl.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery.jgrowl.js" type="text/javascript"></script>

    <link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">          
            function onPostalCodeFoucs()
            {           
                    if($("#ctl00_ContentPlaceHolder1_hfPostalCode").val()=="0")
                    {
	                    $("#ctl00_ContentPlaceHolder1_txtPostalCode").mask("***-***");
	                    $("#ctl00_ContentPlaceHolder1_hfPostalCode").val('1');	            
	                }	   
            }
    
            function checkChange() {             
            if(document.getElementById('ctl00_ContentPlaceHolder1_cbShippingSame').checked)
            {                       
                document.getElementById('divShippingInfo').style.display = "none";      
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator15'), false);                
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator16'), false);                
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator17'), false);                
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator19'), false);                
            } 
            else 
            {                               
                document.getElementById('divShippingInfo').style.display = "";      
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator15'), true);                
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator16'), true);                
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator17'), true);                
                ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator19'), true);                
            }
        }
        
  function arrangePostalCode()
    {
         if(document.getElementById("ctl00_ContentPlaceHolder1_txtPostalCode").value.length==6)
         {
             var str=document.getElementById("ctl00_ContentPlaceHolder1_txtPostalCode").value;
             document.getElementById("ctl00_ContentPlaceHolder1_txtPostalCode").value=str.substring(0,3)+"-"+str.substring(3,6);
         }
    }
    
    function checkPostCode()
    {
        alert("Working");
            $(document).ready(function(){   
            alert($("#ctl00_ContentPlaceHolder1_hfPostalCode").val());
            if($("#ctl00_ContentPlaceHolder1_hfPostalCode").val()=="0")
            {
	            $("#ctl00_ContentPlaceHolder1_txtPostalCode").mask("***-***");
	            $("#ctl00_ContentPlaceHolder1_hfPostalCode").val('1');
	            alert($("#ctl00_ContentPlaceHolder1_hfPostalCode").val());
	        }
        });
    }

function ShowAddressPopUp()
    {      
    
    var name = 'dealAddressCount';
    c_start=-1;
    if (document.cookie.length > 0)
    {        
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
            
        c_start=document.cookie.indexOf(name + "=");
        if (c_start != -1) {
        
            var cookAddressCount = cookieValue.split('=');
            //alert(cookAddressCount [1]);
        
            if ((cookAddressCount[1]) > 1)
                {
                    var heightList = "392px";
                    
                    if (cookAddressCount[1] < 3)
                    heightList = "325px";
                    if (cookAddressCount[1] < 4)
                    heightList = "425px";
                    else if (cookAddressCount[1] <8)
                    heightList = "500px";
                    else 
                    heightList= (parseInt(cookAddressCount[1]) *60) + "px";
                    
                
                    jQuery(document).ready(function(){
		              $(document).ready(function() {
                        $.colorbox({
                            scrolling: false,
                            initialWidth: 1,
                            initialHeight: 1,
                            inline: true,
                            width: "626px",
                            height:heightList,
                            href: "#signinform",
                            opacity: 0
                            });
                        });
	                });
	        }
	    }
	}
	$(document).ready(function(){
        $("#ctl00_ContentPlaceHolder1_imgBtnOk").click(function(){
            setTimeout("$.colorbox.close();",313);
            })
        });
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
        
        
        function removeSpaceINCC()
        {            
            var newccval= $("#ctl00_ContentPlaceHolder1_txtCCNumber").val().replace(/ /g,'');
            $("#ctl00_ContentPlaceHolder1_txtCCNumber").val(newccval);         
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

    <script>
    function pageLoad()
    {
        $(".subDealPopup").fancybox({
                openEffect: 'elastic',
                openEasing: 'easeOutBack',
                closeEffect: 'elastic',
                closeEasing: 'easeInBack'
        });
    }
        
	$(function() {
		$("#accordion").accordion({ collapsible: true,active: -1,autoHeight: false });
	});
    </script>

    <asp:Panel ID="pnlinner" runat="server" DefaultButton="btnCompleteOrder">
        <asp:HiddenField ID="hfShowDiv" runat="server" Value="false" />
        <asp:HiddenField ID="hfTastyCredit" runat="server" Value="0" />
        <asp:HiddenField ID="hfComissionMoney" runat="server" Value="0" />
        <div class="PagesBG">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div style="float: left; font-size: 15px;">
                        Checkout
                        <asp:HiddenField ID="hfShippingEnabled" runat="server" Value=""></asp:HiddenField>
                        <asp:HiddenField ID="hfShippingAndTax" runat="server" Value="0"></asp:HiddenField>
                        <asp:HiddenField ID="hfOrderQty" runat="server" Value="0"></asp:HiddenField>
                        <asp:HiddenField ID="hfPQty" runat="server" Value="0"></asp:HiddenField>
                        <asp:HiddenField ID="hfGQty" runat="server" Value="0"></asp:HiddenField>
                    </div>
                    <div style="float: left; padding-left: 20px;">
                        <asp:HyperLink ID="hlContinueShopping" runat="server"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div style="float: left; width: 700px; padding-top: 9px;">
                <div style="clear: both; padding-top: 2px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="clear: both; background-color: White; overflow: hidden;">
                                <div style="clear: both; width: 100%; font-size: 13px; overflow: hidden;">
                                    <asp:GridView runat="server" ID="gridview1" DataKeyNames="dealID" AllowPaging="false"
                                        AutoGenerateColumns="false" CellPadding="2" OnRowDataBound="gridview1_RowDataBound"
                                        CellSpacing="0" GridLines="None" Width="100%" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div style="clear: both; padding-top: 10px;">
                                                        <div style="float: left; padding-left: 2px;">
                                                            <div style="clear: both;">
                                                                <img id="imgCart" width="110px" src='<%# Eval("image") %>' />
                                                            </div>
                                                            <div style="clear: both; padding-top: 5px; width: 110px; text-align: center; font-size: 12px;">
                                                                <asp:Label ID="lblBusinesName" runat="server" Text='<% #Eval("restaurantBusinessName") %>'>
                                                                </asp:Label>
                                                            </div>
                                                            <div style="clear: both; padding-top: 5px; width: 110px; text-align: center; font-size: 12px;">
                                                                <a id='<%# "deal_" + Eval("dealID") %>' href='<%# "#deal_ref_" + Eval("dealID") %>'
                                                                    class="subDealPopup" style='display: <%# Eval("chieldDeals").ToString().Trim()=="1"?"none":""%>'>
                                                                    <div style="float: left;">
                                                                        <img src="Images/btnAddCheckout.png" /></div>
                                                                    <div style="float: left; padding-top: 7px;">
                                                                        Add Options</div>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; padding-left: 10px;">
                                                            <div style="clear: both; width: 530px; padding-left: 10px;">
                                                                <asp:Label ID="lblTopGridDealTitle" Font-Bold="true" runat="server" Text='<% #Eval("dealPageTitle") %>'>
                                                                </asp:Label>
                                                                <asp:Label ID="lblId" runat="server" Visible="false" Text='<% #Eval("dealID") %>'>
                                                                </asp:Label>
                                                                <asp:HiddenField ID="hfchieldDeals" runat="server" Value='<% #Eval("chieldDeals") %>' />
                                                            </div>
                                                            <div style="clear: both; padding-top: 10px;">
                                                                <asp:GridView ID="gvSubItem" OnRowDataBound="gvSubItem_RowDataBound" OnRowCommand="gvSubItem_Login"
                                                                    runat="server" DataKeyNames="dealID" AllowPaging="false" CellPadding="3" AutoGenerateColumns="false"
                                                                    HeaderStyle-CssClass="GridHeaderForCheckout" RowStyle-CssClass="GridRowForCheckout"
                                                                    ShowHeader="true" GridLines="None">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div style="clear: both; padding-right: 10px;">
                                                                                    <div style="float: left; padding-left: 10px;">
                                                                                        <asp:CheckBox ID="cbIsGift" runat="server" Checked='<%#Convert.ToInt32(Eval("isGift"))==1 ?  true : false %>'
                                                                                            OnCheckedChanged="cbIsGift_CheckChanged" AutoPostBack="true" />
                                                                                    </div>
                                                                                    <div style="float: left;">
                                                                                        <img id="imgGiftItGrid" src="Images/giftCheckOut.png" />
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <div style="width: 250px;">
                                                                                    <asp:Label ID="lblGridDealTitle" runat="server" Text='<% #Eval("title") %>'>
                                                                                    </asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                            <ItemTemplate>
                                                                                <div style="float: left; padding-left: 5px; width: 70px;">
                                                                                    <asp:DropDownList ID="ddlQuntityGrid" AutoPostBack="true" OnSelectedIndexChanged="ddlQuntityGrid_SelectedIndexChanged"
                                                                                        runat="server" CssClass="ddQty">
                                                                                    </asp:DropDownList>
                                                                                    <asp:HiddenField ID="hfDealID" runat="server" Value='<% #Eval("dealID") %>' />
                                                                                    <asp:HiddenField ID="hfparentdealId" runat="server" Value='<% #Eval("parentdealId") %>' />
                                                                                    <asp:HiddenField ID="hfminOrdersPerUser" runat="server" Value='<% #Eval("minOrdersPerUser") %>' />
                                                                                    <asp:HiddenField ID="hfmaxOrdersPerUser" runat="server" Value='<% #Eval("maxOrdersPerUser") %>' />
                                                                                    <asp:HiddenField ID="hfmaxGiftsPerOrder" runat="server" Value='<% #Eval("maxGiftsPerOrder") %>' />
                                                                                    <asp:HiddenField ID="hfisGift" runat="server" Value='<% #Eval("isGift") %>' />
                                                                                    <asp:HiddenField ID="hfQty" runat="server" Value='<% #Eval("Qty") %>' />
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Price">
                                                                            <ItemTemplate>
                                                                                <div style="float: left; width: 60px;">
                                                                                    <div style="float: left; font-size: 11px;">
                                                                                        <sup>C$</sup></div>
                                                                                    <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                                                        <asp:Label ID="lblPriceGrid" runat="server" Text='<% #Eval("sellingPrice") %>'></asp:Label></div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="You Pay">
                                                                            <ItemTemplate>
                                                                                <div style="float: left; width: 70px;">
                                                                                    <div style="float: left; font-size: 11px;">
                                                                                        <sup>C$</sup></div>
                                                                                    <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                                                        <asp:Label ID="lblTotalPriceGrid" runat="server" Text="99"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div style="float: left; width: 30px;">
                                                                                    <asp:ImageButton ID="Delete" runat="server" CommandName="Remove" CommandArgument='<%#Eval("dealID")+"_"+Eval("parentdealId")%>'
                                                                                        ImageUrl="~/Images/delete.png" OnClientClick='return confirm("Are you sure you want to delete this deal?");' />
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div id="divShipping" runat="server" style="clear: both; width: 100%; font-size: 13px;
                                    overflow: hidden; padding-top: 15px;" visible="false">
                                    <div style="float: left; padding: 10px 0px 0px 15px; width: 310px; font-weight: bold;">
                                        <asp:Label ID="Label12" runat="server" Text="Shipping & Tax"></asp:Label>
                                    </div>
                                    <div style="float: left; padding: 10px 0px 0px 265px;">
                                        <div style="float: left; font-size: 11px;">
                                            <sup>C$</sup></div>
                                        <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                            <asp:Label ID="lblShippingAndTax" runat="server" Text="0"></asp:Label></div>
                                    </div>
                                </div>
                                <div style="clear: both; width: 100%; font-size: 13px; overflow: hidden; padding-top: 15px;">
                                    <div style="clear: both; padding-left: 10px; padding-right: 10px;">
                                        <div class="DetailPageTopDiv">
                                            <div style="float: left; padding: 8px 0px 0px 15px; width: 310px; font-size: 13px;
                                                font-weight: bold;">
                                                <asp:Label ID="lblTotalText" runat="server" Text="YOUR TOTAL"></asp:Label>
                                            </div>
                                            <div style="float: left; padding: 8px 0px 0px 240px;">
                                                <div style="float: left; font-size: 11px;">
                                                    <sup>C$</sup></div>
                                                <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                    <asp:Label ID="lblGrandTotal" runat="server" Text="99"></asp:Label>
                                                    <asp:HiddenField ID="hfGrandTotal" runat="server"></asp:HiddenField>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-top: 10px; padding-left: 10px;">
                                    <asp:Label ID="Label1" Font-Bold="true" Font-Size="17px" runat="server" Text="Your Payment Information"></asp:Label>
                                </div>
                                <asp:HiddenField ID="hfPayFull" Value="0" runat="server" />
                                <div id="divRefBal" runat="server" visible="false" style="font-size: 13px; font-weight: bold;">
                                    <div style="padding-left: 12px; padding-bottom: 12px;">
                                        <div style="float: left; padding-right: 40px; padding-top: 5px;">
                                            Pay with my Tasty Credits&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblRefBal" runat="server"></asp:Label>
                                        </div>
                                        <div style="float: left; padding-top: 5px;">
                                            <span style='color: #F99D1C;'>(max. C$<asp:Label ID="lblRefBalanace" runat="server"></asp:Label>
                                                )</span>
                                        </div>
                                        <div style="float: left; padding: 0px 5px 0px 10px;">
                                            <asp:TextBox ID="txtTastyCredit" runat="server" Width="63" MaxLength="6" onKeyPress="return numbersonly(this, event,true);"
                                                CssClass="TextBox"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvTastygoCredit" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtTastyCredit" ErrorMessage="value required!" ValidationGroup="Apply"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="vcdPassword" runat="server" TargetControlID="rfvTastygoCredit">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div style="float: left;">
                                            <asp:Button ID="btnApply" ValidationGroup="Apply" CausesValidation="true" runat="server"
                                                Text="Apply" OnClick="btnApply_Click" CssClass="NewButtonStyle" />
                                        </div>
                                    </div>
                                </div>
                                <div id="divDeliveryGridCCI" runat="server" style="clear: both;">
                                    <div style="padding-left: 10px; padding-top: 10px; padding-bottom: 10px; font-weight: bold;
                                        font-size: 17px;">
                                        <div style="float: left;">
                                            Existing Card
                                        </div>
                                        <div style="float: left;">
                                            <asp:Image ID="imgGridMessage" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                                            <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="text-align: center; padding-bottom: 10px; clear: both; float: left;">
                                        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="10"
                                            CellSpacing="0" OnRowDataBound="gvCustomer_RowDataBound" GridLines="None" Width="600px"
                                            ShowHeader="false" OnRowCommand="gvCustomers_RowCommand">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Edit Card">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="MyRadioButton" runat="server" OnCheckedChanged="CheckChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Your Credit Cards">
                                                    <ItemTemplate>
                                                        <div style="font-size: 15px; text-align: left; padding-left: 20px;">
                                                            <%# GetCardExplain(Eval("ccInfoNumber"))%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        <div style="float: left; font-size: 15px; text-align: left; padding-left: 40px;">
                                                            <asp:HiddenField ID="hfccInfoID" Value='<%#Eval("ccInfoID")%>' runat="server" />
                                                            <asp:LinkButton ID="btnEdit" Text="Edit Card" CommandName="EditCustomer" CausesValidation="false"
                                                                CommandArgument='<%#Eval("ccInfoID")%>' runat="server"></asp:LinkButton>
                                                        </div>
                                                        <div style="float: left; font-size: 15px; text-align: left; padding-left: 10px;">
                                                            <asp:LinkButton ID="btnDelete" Text="Delete" OnClientClick='return confirm("Are you sure you want to delete this info?");'
                                                                CommandName="DeleteCustomer" CausesValidation="false" CommandArgument='<%#Eval("ccInfoID")%>'
                                                                runat="server"></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div style="padding-left: 12px; padding-top: 15px; padding-bottom: 12px; font-weight: bold;
                                        font-size: 15px; clear: both;">
                                        <asp:Button ID="btnAddNewCCI" Text="Add New Card" CausesValidation="false" runat="server"
                                            OnClick="btnAddNewCCI_Click" CssClass="NewButtonStyle"></asp:Button>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfMode" runat="server" Value="new" />
                                <asp:HiddenField ID="hfRefAmt" runat="server" Value="0" />
                                <div id="divDelivery1" runat="server" style="clear: both;">
                                    <div style='float: left; padding: 10px 15px 0px 15px; line-height: normal; display: <%=strhideDive %>;'>
                                        <div>
                                            <asp:HyperLink ID="lblNote" runat="server" Font-Size="13px" Text="Thinking about buying this deal?  Visit our FAQ Page for details of our <b>30 Days Money Back Guarantee & Satisfaction Guarantee</b>"
                                                Target="_blank" NavigateUrl="~/faq.aspx" Font-Underline="false" ForeColor="Black"></asp:HyperLink>
                                        </div>
                                        <div style="padding-top: 15px;">
                                            <img id="imgMasterCard" src="Images/checkoutVisaMasterCards.jpg" title="Pay With Visa or Master Card." />
                                        </div>
                                    </div>
                                    <div style="clear: both; width: 100%; padding-top: 10px;">
                                        <div style="padding-left: 10px; padding-right: 10px;">
                                            <div class="onPxStrip">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divSignUpNew" runat="server" style="clear: both; padding-bottom: 10px;">
                                    <div style="padding-top: 10px; padding-bottom: 10px; padding-left: 10px;">
                                        <asp:Label ID="Label6" Font-Bold="true" Font-Size="17px" runat="server" Text="Create Your TastyGo Account: <span style='font-size:12px;'>(If you don’t have a Tastygo Account Yet)</span>"></asp:Label>
                                    </div>
                                    <div style="padding-top: 5px; padding-left: 20px;">
                                        <div style="width: 100%">
                                            <div style="float: left; width: 40%; text-align: left">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label7" Font-Bold="true" Font-Size="13px" runat="server" Text="* Your Name"></asp:Label></div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:TextBox ID="txtSignUpFullName" TabIndex="1" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtSignUpFullName" ErrorMessage="Full Name required!" ValidationGroup="CheckOut"
                                                        Display="None"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label9" Font-Bold="true" Font-Size="13px" runat="server" Text="* Password"></asp:Label></div>
                                                <div style="padding-bottom: 5px;">
                                                    <asp:TextBox ID="txtSignUpPassword" TabIndex="3" runat="server" TextMode="Password"
                                                        CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtSignUpPassword" ErrorMessage="Password required!"
                                                        ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator11">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="PasswordLengthValid" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtSignUpPassword" ErrorMessage="Password must be 6-15 characters without space."
                                                        ValidationGroup="CheckOut" Display="None" ValidationExpression="([a-zA-Z0-9]{6,15})$"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="PasswordLengthValid">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 40%; text-align: left; padding-left: 35px;">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label10" Font-Bold="true" Font-Size="13px" runat="server" Text="* Your Email Address"></asp:Label></div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:TextBox ID="txtSignUpEmail" TabIndex="2" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtSignUpEmail" ErrorMessage="Email required!"
                                                        ValidationGroup="CheckOut" Display="None">                            
                                                                       
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="RequiredFieldValidator13">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtSignUpEmail" ErrorMessage="Invalid email address!"
                                                        ValidationGroup="CheckOut" Display="None" ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*"></cc1:RegularExpressionValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="RegularExpressionValidator2">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc2:TextBoxWatermarkExtender ID="txtWaterMark" runat="server" TargetControlID="txtSignUpEmail"
                                                        WatermarkText="abc@abc.com" WatermarkCssClass="TextBox_Watermark" />
                                                </div>
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label11" Font-Bold="true" Font-Size="13px" runat="server" Text="* Confirm Password"></asp:Label></div>
                                                <div style="padding-bottom: 5px;">
                                                    <asp:TextBox ID="txtSignUpConfirmPassword" TabIndex="4" TextMode="Password" runat="server"
                                                        CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtSignUpConfirmPassword" ErrorMessage="Confirm Password required!"
                                                        ValidationGroup="CheckOut" Display="None">                            
                                            
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator12">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:CompareValidator ID="cvConfirmPassword" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtSignUpConfirmPassword" ControlToCompare="txtSignUpPassword"
                                                        ErrorMessage="Password and confirm password must be same!" ValidationGroup="CheckOut"
                                                        Display="None"></cc1:CompareValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="cvConfirmPassword">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="clear: both; width: 100%; padding-top: 10px;">
                                        <div style="padding-left: 10px; padding-right: 10px;">
                                            <div class="onPxStrip">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divShippingAddress" runat="server" style="clear: both; overflow: hidden;
                                    padding-bottom: 10px;">
                                    <div style="padding-top: 15px; padding-bottom: 10px; padding-left: 10px;">
                                        <div style="float: left">
                                            <asp:Label ID="Label13" Font-Bold="true" Font-Size="17px" runat="server" Text="Shipping Information:"></asp:Label>
                                        </div>
                                        <div style="float: left">
                                            <div style="float: left">
                                                <asp:CheckBox ID="cbShippingSame" runat="server" onclick="javascript:checkChange();" /></div>
                                            <div style="float: left">
                                                <asp:Label ID="lblSameAsBilling" runat="server" Font-Size="13px" Text="Same as Billing"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 15px 15px 0px 15px;">
                                        <div style="float: left">
                                            <span style="font-size: 13px; color: Black;">Your order will be send to you directly.
                                                If you have question about your shipping status, please contact us at <a href="mailto:support@tazzling.com">
                                                    support@tazzling.com</a></span></div>
                                    </div>
                                    <div style="clear: both; padding-left: 20px; padding-top: 20px;">
                                        <div style="float: left; width: 40%; text-align: left">
                                            <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                <asp:Label ID="Label14" Font-Bold="true" Font-Size="13px" runat="server" Text="* Recipient's First Name"></asp:Label></div>
                                            <div style="padding-bottom: 15px;">
                                                <asp:TextBox ID="txtShippingFirstName" TabIndex="5" runat="server" CssClass="TextBox"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator14" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtShippingFirstName" ErrorMessage="First Name required!"
                                                    ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" TargetControlID="RequiredFieldValidator14">
                                                </cc2:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 40%; text-align: left; padding-left: 35px;">
                                            <div style="float: left;">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label21" Font-Bold="true" Font-Size="13px" runat="server" Text="* Recipient's Last Name"></asp:Label></div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:TextBox ID="txtShipppingName" TabIndex="6" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator20" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtShipppingName" ErrorMessage="Last Name required!"
                                                        ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server" TargetControlID="RequiredFieldValidator20">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divShippingInfo" style='clear: both; padding-left: 20px; display: <%=strhideShippingDiv%>'>
                                        <div style="width: 100%">
                                            <div style="float: left; width: 40%; text-align: left">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label16" Font-Bold="true" Font-Size="13px" runat="server" Text="* Shipping Address (No PO-Box)"></asp:Label>
                                                </div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:TextBox ID="txtShippingAddress" TabIndex="7" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator16" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtShippingAddress" ErrorMessage="Address required!"
                                                        ValidationGroup="CheckOut" Display="None">                                                                        
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" TargetControlID="RequiredFieldValidator16">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label17" Font-Bold="true" Font-Size="13px" runat="server" Text="* Province / State"></asp:Label>
                                                </div>
                                                <div style="padding-bottom: 5px;">
                                                    <asp:TextBox ID="txtShippingProvince" TabIndex="9" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator17" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtShippingProvince" ErrorMessage="Province / State required!"
                                                        ValidationGroup="CheckOut" Display="None">                                                                        
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server" TargetControlID="RequiredFieldValidator17">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 40%; text-align: left; padding-left: 35px;">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label15" Font-Bold="true" Font-Size="13px" runat="server" Text="* City"></asp:Label>
                                                </div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:TextBox ID="txtShippingCity" TabIndex="8" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator15" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtShippingCity" ErrorMessage="City required!"
                                                        ValidationGroup="CheckOut" Display="None">                                                                        
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" TargetControlID="RequiredFieldValidator15">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label19" Font-Bold="true" Font-Size="13px" runat="server" Text="* Postal Code / Zip Code"></asp:Label>
                                                </div>
                                                <div style="padding-bottom: 5px;">
                                                    <asp:TextBox ID="txtShippingZipCode" TabIndex="10" runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator19" SetFocusOnError="true"
                                                        runat="server" ControlToValidate="txtShippingZipCode" ErrorMessage="Postal Code / Zip Code required!"
                                                        ValidationGroup="CheckOut" Display="None">                                                                        
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" TargetControlID="RequiredFieldValidator19">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="clear: both; padding-left: 20px;">
                                        <div style="float: left; width: 40%; text-align: left">
                                            <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                <asp:Label ID="Label18" Font-Bold="true" Font-Size="13px" runat="server" Text="* Phone Number (For Carrier Only)"></asp:Label></div>
                                            <div style="padding-bottom: 15px;">
                                                <asp:TextBox ID="txtShippingPhone" TabIndex="11" runat="server" CssClass="TextBox"></asp:TextBox>
                                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator18" SetFocusOnError="true"
                                                    runat="server" ControlToValidate="txtShippingPhone" ErrorMessage="Phone required!"
                                                    ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" TargetControlID="RequiredFieldValidator18">
                                                </cc2:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 40%; text-align: left; padding-left: 35px;">
                                            <div style="float: left;">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="Label4" Font-Bold="true" Font-Size="13px" runat="server" Text="* Country"></asp:Label></div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:DropDownList ID="ddlShippingCountry" TabIndex="12" Height="30px" runat="server"
                                                        CssClass="TextBox" onfocus="this.className='TextBox'">
                                                        <asp:ListItem Text="Canada" Value="Canada" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="United States" Value="United States"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="clear: both; padding-top: 10px; padding-left: 20px;">
                                        <div style="padding-bottom: 5px; color: #5F5F5F;">
                                            <asp:Label ID="Label20" Font-Bold="true" Font-Size="13px" runat="server" Text="Notes for Tastygo Shipping Team (eg. size, color, buzzer number)"></asp:Label></div>
                                        <div style="padding-bottom: 5px;">
                                            <asp:TextBox ID="txtShippingNote" TabIndex="13" TextMode="MultiLine" Height="75px"
                                                Width="530px" runat="server" CssClass="TextBox"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div id="divDelivery2" style="font-size: 13px; font-weight: bold; display: none;">
                                    <div style="padding-top: 20px; padding-bottom: 10px; padding-left: 20px;">
                                        <asp:Label ID="Label2" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="20px" runat="server"
                                            Text="Delivery Information"></asp:Label>
                                    </div>
                                    <div style="padding-top: 20px; padding-left: 140px;">
                                        <div style="width: 100%">
                                            <div style="float: left; width: 40%; text-align: left">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="lblDFirstName" Font-Bold="true" Font-Size="16px" runat="server" Text="* First Name"></asp:Label></div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:HiddenField ID="hfCCInfoIdEdit" runat="server" />
                                                    <asp:TextBox ID="txtFirstname" TabIndex="12" runat="server" CssClass="TextBox"></asp:TextBox>
                                                </div>
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="lblDEmail" Font-Bold="true" Font-Size="16px" runat="server" Text="* Email Address"></asp:Label></div>
                                                <div style="padding-bottom: 5px;">
                                                    <asp:TextBox ID="txtEmail" TabIndex="13" runat="server" CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="float: left; width: 40%; text-align: left; padding-left: 5px;">
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="lblDLastName" Font-Bold="true" Font-Size="16px" runat="server" Text="* Last Name"></asp:Label></div>
                                                <div style="padding-bottom: 15px;">
                                                    <asp:TextBox ID="txtLastName" TabIndex="14" runat="server" CssClass="TextBox"></asp:TextBox>
                                                </div>
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="lblDCEmailAddress" Font-Bold="true" Font-Size="16px" runat="server"
                                                        Text="* Confirm Email Address"></asp:Label></div>
                                                <div style="padding-bottom: 5px;">
                                                    <asp:TextBox ID="txtCEmailAddress" TabIndex="15" runat="server" CssClass="TextBox"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divDelivery3" runat="server" style="font-size: 13px; font-weight: bold;">
                                    <div style="padding-left: 15px; line-height: normal; padding-top: 10px; padding-right: 15px;">
                                        <asp:Label ID="Label3" Width="95%" Font-Bold="false" ForeColor="Black" runat="server"
                                            Text="The tastygo Voucher will be emailed to the address below. It will also be available on the receipt page. If you to not receive your Voucher, please contact support@tazzling.com."></asp:Label>
                                    </div>
                                    <div style="padding-top: 10px; padding-left: 10px;">
                                        <div style="width: 100%">
                                            <div style="float: left; width: 40%; text-align: left">
                                                <div style="padding-bottom: 20px;">
                                                    <asp:Label ID="Label8" Font-Bold="true" Font-Size="17px" runat="server" Text="Billing Information"></asp:Label>
                                                </div>
                                                <div style="padding-left: 5px;">
                                                    <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                        <asp:Label ID="lblBUserName" Font-Bold="true" runat="server" Text="* Billing First and Last Name"></asp:Label></div>
                                                    <div style="padding-bottom: 15px;">
                                                        <asp:TextBox ID="txtBUserName" TabIndex="16" runat="server" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                                            ControlToValidate="txtBUserName" ErrorMessage="Name required!" ValidationGroup="CheckOut"
                                                            Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator2">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                        <asp:Label ID="lblBillingAddress" Font-Bold="true" runat="server" Text="* Billing Address"></asp:Label></div>
                                                    <div style="padding-bottom: 15px;">
                                                        <asp:TextBox ID="txtBillingAddress" TabIndex="17" runat="server" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                                            ControlToValidate="txtBillingAddress" ErrorMessage="Address required!" ValidationGroup="CheckOut"
                                                            Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                        <asp:Label ID="lblBCity" Font-Bold="true" runat="server" Text="* City"></asp:Label></div>
                                                    <div style="padding-bottom: 15px;">
                                                        <asp:TextBox ID="txtBCity" runat="server" TabIndex="18" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                                                            ControlToValidate="txtBCity" ErrorMessage="City required!" ValidationGroup="CheckOut"
                                                            Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator4">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                        <asp:Label ID="lblBProvince" Font-Bold="true" runat="server" Text="* Province / State"></asp:Label></div>
                                                    <div style="padding-bottom: 15px;">
                                                        <asp:TextBox ID="txtProvince" runat="server" TabIndex="19" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                                            ControlToValidate="txtProvince" ErrorMessage="Province required!" ValidationGroup="CheckOut"
                                                            Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RequiredFieldValidator5">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                        <asp:Label ID="lblPostalCode" Font-Bold="true" runat="server" Text="* Postal Code / Zip Code"></asp:Label></div>
                                                    <div style="padding-bottom: 15px;">
                                                        <asp:HiddenField ID="hfPostalCode" runat="server" Value="0" />
                                                        <asp:TextBox ID="txtPostalCode" runat="server" TabIndex="20" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" runat="server"
                                                            ControlToValidate="txtPostalCode" ErrorMessage="Postal code required!" ValidationGroup="CheckOut"
                                                            Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator6">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="padding-bottom: 5px; padding-top: 4px; clear: both;">
                                                        <asp:Button ID="btnSave" ValidationGroup="CheckOut" CausesValidation="true" runat="server"
                                                            OnClick="btnSave_Click" Text="Update" CssClass="NewButtonStyle" />
                                                        &nbsp;
                                                        <asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" Text="Cancel"
                                                            CssClass="NewButtonStyle" /></div>
                                                </div>
                                            </div>
                                            <div style='float: left; width: 40%; text-align: left; padding-left: 35px; display: <%=strhideDive%>;'>
                                                <div id="divlblCCN" runat="server" style="padding-bottom: 6px; padding-top: 37px;
                                                    color: #5F5F5F;">
                                                    <asp:HiddenField ID="hfCCN" runat="server" />
                                                    <asp:Label ID="lblCCNumber" Font-Bold="true" runat="server" Text="* Credit Card Number"></asp:Label></div>
                                                <div id="divtxtCCN" runat="server" style="padding-bottom: 15px;">
                                                    <asp:TextBox ID="txtCCNumber" TabIndex="21" EnableViewState="False" onblur="removeSpaceINCC();"
                                                        runat="server" CssClass="TextBox"></asp:TextBox>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server"
                                                        ControlToValidate="txtCCNumber" ErrorMessage="Credit Card number required!" ValidationGroup="CheckOut"
                                                        Display="None"></cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RequiredFieldValidator7">
                                                    </cc2:ValidatorCalloutExtender>
                                                    <cc1:CustomValidator ID="cvCreditCard" runat="server" ControlToValidate="txtCCNumber"
                                                        ValidateEmptyText="true" ClientValidationFunction="luhn" SetFocusOnError="true"
                                                        ValidationGroup="CheckOut" ErrorMessage="Invalid credit card number." Display="None">
                                                    </cc1:CustomValidator>
                                                    <cc2:ValidatorCalloutExtender ID="vcePhone" runat="server" TargetControlID="cvCreditCard">
                                                    </cc2:ValidatorCalloutExtender>
                                                </div>
                                                <div style="padding-bottom: 5px; color: #5F5F5F;">
                                                    <asp:Label ID="lblExpiration" Font-Bold="true" runat="server" Text="* Expiration"></asp:Label></div>
                                                <div style="padding-bottom: 15px;">
                                                    <div style="float: left;">
                                                        <asp:DropDownList ID="ddlMonth" TabIndex="22" runat="server" CssClass="ddlDeal">
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
                                                    <div style="float: left; padding-left: 10px; font-size: 22px; font-weight: bold;
                                                        padding-top: 5px; padding-right: 10px; color: #5F5F5F;">
                                                        /</div>
                                                    <div style="float: left">
                                                        <asp:DropDownList ID="ddlYear" TabIndex="23" runat="server" CssClass="ddlDeal">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div style="padding-bottom: 5px; padding-top: 15px; clear: both; color: #5F5F5F;">
                                                    <div>
                                                        <asp:Label ID="lblCVNumber" Font-Bold="true" runat="server" Text="* Security Code (CVV)"></asp:Label></div>
                                                </div>
                                                <div style="padding-bottom: 5px;">
                                                    <div style="float: left;">
                                                        <asp:TextBox ID="txtCVNumber" runat="server" CssClass="TextBox" TabIndex="24" Width="150px"
                                                            MaxLength="4" ToolTip="3-4 Digits"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server"
                                                            ControlToValidate="txtCVNumber" ErrorMessage="Credit verification number required!"
                                                            ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RequiredFieldValidator8">
                                                        </cc2:ValidatorCalloutExtender>
                                                        <asp:HiddenField ID="hfCVNumber" runat="server" />
                                                    </div>
                                                    <div style="float: left; padding-left: 10px;">
                                                        <img src="Images/cvv3.png" title="3 or 4 Digit" />
                                                    </div>
                                                </div>
                                                <%--    <div style="padding-bottom: 5px; padding-top: 15px; clear: both; font-size: 13px;
                                            font-family: Arial; color: #636363; font-weight: normal;">
                                            <asp:Literal ID="ltUserIP" runat="server" Text=""></asp:Literal>
                                        </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="height: 130px; font-size: 17px; font-weight: bold; clear: both;">
                                    <div style="padding-left: 20px;">
                                        <div style="float: left;">
                                            <div style="float: left;">
                                                <asp:CheckBox ID="cbAgree" Checked="true" Font-Bold="false" TabIndex="25" ForeColor="#636363"
                                                    Font-Size="13px" runat="server" Text="" />
                                            </div>
                                            <div style="float: left; padding-left: 5px;">
                                                <asp:HyperLink ID="hlTermAndCondition" runat="server" ForeColor="#5F5F5F" Font-Size="13px"
                                                    Text="* I Agree to the Terms & Conditions" Target="_blank" NavigateUrl="~/terms-customer.aspx"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding-top: 40px; text-align: center;">
                                        <div style="float: left; font-size: 13px; font-weight: normal; padding-top: 7px;
                                            padding-left: 190px;">
                                            <a href='<%= ConfigurationManager.AppSettings["YourSite"].ToString()+"/default.aspx" %>'>
                                                Continue Shopping</a>
                                        </div>
                                        <div style="float: left; padding-left: 10px; padding-top: 7px;">
                                            OR
                                        </div>
                                        <div style="float: left; padding-left: 10px;">
                                            <asp:Button ID="btnCompleteOrder" CausesValidation="true" TabIndex="26" ValidationGroup="CheckOut"
                                                runat="server" CssClass="NewButtonStyle" Text="Complete My Order" Font-Bold="true"
                                                UseSubmitBehavior="false" OnClick="btnCompleteOrder_Click" /></div>
                                    </div>
                                    <div style="padding-top: 7px; text-align: center; clear: both;">
                                        <asp:Label ID="lblErrorMessage" runat="server" Visible="false" Font-Names="Arial"
                                            Font-Size="16px" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div style="float: left;">
                <div style="clear: both; padding-left: 10px; width: 270px;">
                    <div style="clear: both; padding-top: 10px;">
                        <div style="height: 180px; width: 268px; border: solid 1px #E6E6E5; background-image: url(Images/verisignBG.png);
                            background-repeat: repeat-x; background-position: bottom; background-color: #eeeeee;">
                            <div style="height: 24px; padding-top: 15px; font-family: Berlin Sans FB Demi; color: #a71d4c;
                                font-size: 12px; text-align: center; font-weight: bold;">
                                VERISIGN SSL VERIFIED - SECURED
                            </div>
                            <div style="text-align: center;">
                                <div id="myDiv">

                                    <script type="text/javascript" src="https://seal.verisign.com/getseal?host_name=www.tazzling.com&amp;size=M&amp;use_flash=YES&amp;use_transparent=YES&amp;lang=en"></script>

                                </div>
                                <div style="clear: both; text-align: center; padding-left: 50px; padding-top: 10px;">
                                    <a href="https://www.verisign.com/ssl-certificate/" target="_blank" style="color: #6f6f6f;
                                        text-decoration: none; font-size: 8px; font-family: Berlin Sans FB; text-align: center;
                                        line-height: 11px;">
                                        <div style="clear: both; text-align: center;">
                                            <div style="float: left;">
                                                <img src="Images/verisignImgBottom.png" /></div>
                                            <div style="float: left; width: 150px;">
                                                ALL DATA ON TASTYGO IS PROTECTED BY TOP-NOTCH TRUSTED SSL CERTIFICATE</div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%">
                        <div style="clear: both; width: 100%; padding: 11px 0px 0px 0px;">
                            <div class="DetailTheDetailDiv" style="font-size: 13px; font-weight: bold;">
                                <div style="float: left; padding: 10px 0px 0px 15px;">
                                    Payment FAQ
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; overflow: hidden;">
                        <div style="clear: both; overflow: hidden;">
                            <div id="accordion">
                                <h3>
                                    <a href="#" style="font-size: 13px;">What happens after I purchase my voucher?</a></h3>
                                <div>
                                    <p class="howtoReferNormalTxt">
                                        After the deal is over, we'll send you an email notification with your receipt and
                                        instructions to print your voucher. All the information required to redeem the deal
                                        is on the voucher. If not enough people buy into the deal to activate it, your credit
                                        card is not charged and no money transfer hands.
                                    </p>
                                </div>
                                <h3>
                                    <a href="#" style="font-size: 13px;">What if I buy a voucher as a gift?</a></h3>
                                <div>
                                    <p class="howtoReferNormalTxt">
                                        Simply purchase the deal as gift, and print the Tastygo Voucher as gift, you'll
                                        be able to find the voucher on "My Gift" section.
                                    </p>
                                </div>
                                <h3>
                                    <a href="#" style="font-size: 13px;">Can I change or cancel my purchase?</a></h3>
                                <div>
                                    <p class="howtoReferNormalTxt">
                                        Our 30 Days money back guarantee allow you to make changes, cancel your purchase
                                        within the 30 day period. Simply send us an email via <a href="mailto:support@tazzling.com">
                                            support@tazzling.com</a> and we'll process your request.
                                    </p>
                                </div>
                                <h3>
                                    <a href="#" style="font-size: 13px;">Is Tastygo Safe?</a></h3>
                                <div>
                                    <p class="howtoReferNormalTxt">
                                        We use top notch Verisign SSL certificate to ensure the highest standard of data
                                        fortification. Information pass through Tastygo's checkout page is transmitted securely
                                        to the payment gateway.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%">
                        <div style="clear: both; width: 100%; padding: 11px 0px 0px 0px;">
                            <div class="DetailTheDetailDiv" style="font-size: 13px; font-weight: bold;">
                                <div style="padding: 10px 0px 0px 15px; clear: both;">
                                    Tastygo’s Promise
                                </div>
                            </div>
                            <div style="clear: both; background-color: White; overflow: hidden;">
                                <div style="clear: both;">
                                    <div style="float: left; font-size: 12px; font-weight: normal; width: 160px; padding-left: 10px;">
                                        Customer satisfactions our top priority. If you are not satisfy with any purchases,
                                        contact us at <a href="mailto:support@tazzling.com">support@tazzling.com</a> or 1855-295-1771
                                        and we‘ll make things right.
                                    </div>
                                    <div style="float: right; padding-right: 10px; padding-top: 10px;">
                                        <img src="Images/tastyGoPromiss.jpg" />
                                    </div>
                                </div>
                                <div style="clear: both; padding:10px 10px 10px 50px;">
                                    <div style="width: 160px; text-align: center">
                                        Click <a href='<%= ConfigurationManager.AppSettings["YourSite"].ToString()+"/faq.aspx" %>'>
                                            Here</a> to see our 30 Days Money Back Guarantee
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div>
        <asp:UpdatePanel ID="upGiftInfo" runat="server">
            <ContentTemplate>
                <div style="float: left;">
                    <div style="float: left; padding-left: 5px;">
                        <div style="display: none">
                            <div id="signinform">
                                <div style="padding-top: 30px; padding-left: 19px;">
                                    <div id="divSignUp" runat="server">
                                        <div style="clear: both;">
                                            <div style="float: left; padding-top: 5px; line-height: 20px;">
                                                <asp:Label ID="lblLabelTop" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="20px"
                                                    runat="server" Text="Please select Locations"></asp:Label><asp:HiddenField ID="hfAddressID"
                                                        runat="server" />
                                            </div>
                                        </div>
                                        <div style="clear: both; padding-top: 10px;">
                                            <div style="float: left; padding-top: 5px; line-height: 20px;">
                                                <asp:Label ID="Label5" ForeColor="#F99D1C" Font-Size="16px" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div style="clear: both; padding-top: 11px;">
                                            <asp:GridView ID="grdViewAddress" runat="server" DataKeyNames="dealId" Width="541px"
                                                AllowSorting="False" AllowPaging="False" AutoGenerateColumns="False" ShowHeader="False"
                                                ShowFooter="true" GridLines="None">
                                                <RowStyle CssClass="gridText" Font-Size="14px" Height="26" HorizontalAlign="Left" />
                                                <AlternatingRowStyle CssClass="AltgridText" Font-Size="14px" Height="26" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hfDealAddressID" Value='<% # Eval("raID") %>' runat="server" />
                                                            <asp:RadioButton ID="rdbGVRow" runat="server" onclick='<%# "RadioCheck(this,\""+ Eval("Address") + "\");" %>'
                                                                value='<% # Eval("raID") %>' Text='<%# Eval("Address") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div style="clear: both;">
                                            <asp:Button ID="imgBtnOk" runat="server" CssClass="NewButtonStyle" Text="Ok" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgBtnOk" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Literal ID="ltrlNotify" runat="server"></asp:Literal>
        <div style="display: none;">
            <asp:Literal ID="ltSubDeals" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>
