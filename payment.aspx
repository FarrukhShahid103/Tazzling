<%@ Page Title="" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="payment.aspx.cs" Inherits="MyPayment" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
.DropDownBG
        {
            background-image: url('images/textBoxBG.png');
            border-radius: 5px;
            border: 1px solid #B2B2B2;
            padding: 4px;
        }

</style>


<script language="javascript" type="text/javascript">

    function uncheckOthers(id) {
        var elm = document.getElementsByTagName('input');
        for (var i = 0; i < elm.length; i++) {
            if (elm.item(i).id.substring(id.id.lastIndexOf('_')) == id.id.substring(id.id.lastIndexOf('_'))) {
                if (elm.item(i).type == "radio" && elm.item(i) != id)
                    elm.item(i).checked = false;
            }
        }
    }


    function removeSpaceINCC() {
        var newccval = $("#ctl00_ContentPlaceHolder1_txtCCNumber").val().replace(/ /g, '');
        $("#ctl00_ContentPlaceHolder1_txtCCNumber").val(newccval);
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

    function checkChange() {
        //alert("working");
        if (document.getElementById('ctl00_ContentPlaceHolder1_cbShippingSame').checked) {
            document.getElementById('divShippingInfo').style.display = "none";
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator10'), false);
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator12'), false);
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator13'), false);
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator14'), false);
        }
        else {
            document.getElementById('divShippingInfo').style.display = "";
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator10'), true);
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator12'), true);
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator13'), true);
            ValidatorEnable(document.getElementById('ctl00_ContentPlaceHolder1_RequiredFieldValidator14'), true);
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
            else if (parseFloat(myfield.value + keychar) == (document.getElementById("ctl00_ContentPlaceHolder1_hfGrandTotal").value)) {
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


    <div style="line-height: normal; font-family:Arial;">
        <asp:HiddenField ID="hfGrandTotal" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hfPayFull" Value="0" runat="server" />
        <asp:HiddenField ID="hfTastyCredit" runat="server" Value="0" />
        <asp:HiddenField ID="hfComissionMoney" runat="server" Value="0" />
        <asp:HiddenField ID="hfMode" runat="server" Value="new" />
        <asp:HiddenField ID="hfRefAmt" runat="server" Value="0" />
        <asp:HiddenField ID="hfCCInfoIdEdit" runat="server" />
        <asp:HiddenField ID="hfOrderQty" runat="server" Value="0"></asp:HiddenField>
        <div style="clear: both; width: 100%; text-align: left;
            padding-top: 40px;">
            <img src="Images/payment_method.png" />
        </div>
        <div style="clear: both; width: 100%; padding-top: 30px;">
        </div>
        <div style="clear: both; width: 100%; overflow:hidden; background-color:White;">
            <div style="clear: both; font-size: 24px; font-weight: 700; padding: 15px 0px 80px 15px;">
                <div style="float: left; padding-top: 3px;">
                    <img src="Images/payment_lock.png" />
                </div>
                <div style="float: left; padding-left: 20px; padding-bottom:5px;">
                    <div style="clear: both;">
                        <asp:Label ID="lblPaymentTop" runat="server" Text="Add <span style='color: #DD0017;'>Secure</span> Credit Card Payment"></asp:Label></div>
                    <div style="clear: both; font-size: 18px; font-weight: 500;">
                        This is s secure 128 SSL encrypted payment</div>
                </div>
            </div>
        </div>
        <div style="clear: both; width: 100%; padding-top: 15px;">
        </div>
        <div style="clear: both; width: 100%; background-color:White; overflow: hidden;
            margin-bottom: 15px;">
            <div style="padding-left: 10px; padding-top: 10px; padding-bottom: 10px; font-weight: bold;
                font-size: 17px;">
                <div style="clear: both;">
                    You need to pay $ <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div id="divRefBal" runat="server"  visible="false" style="clear: both; width: 100%; background-color:White; 
            overflow: hidden; margin-bottom: 15px;">
            <div style="clear: both; padding-left: 10px; padding-top: 10px; padding-bottom: 10px;
                font-weight: bold; font-size: 17px;">
                Pay with Tazzling Credits
                <asp:Label ID="lblRefBal" runat="server"></asp:Label>
            </div>
            <div style="clear: both; padding-left: 10px; margin-bottom: 10px; overflow: hidden;">
                <div style="float: left; padding-top: 5px;">
                    <span style='color: #DD0017; font-size: 14px;'>(max. C$<asp:Label ID="lblRefBalanace"
                        runat="server"></asp:Label>
                        )</span>
                </div>
                <div style="float: left; padding-left: 10px;">
                    <asp:TextBox ID="txtTastyCredit" runat="server" Width="63" MaxLength="6" onKeyPress="return numbersonly(this, event,true);"
                        CssClass="TextBox"></asp:TextBox>
                    <cc1:RequiredFieldValidator ID="rfvTastygoCredit" SetFocusOnError="true" runat="server"
                        ControlToValidate="txtTastyCredit" ErrorMessage="value required!" ValidationGroup="Apply"
                        Display="None"></cc1:RequiredFieldValidator>
                    <cc2:ValidatorCalloutExtender ID="vcdPassword" runat="server" TargetControlID="rfvTastygoCredit">
                    </cc2:ValidatorCalloutExtender>
                </div>
                <div style="float: left; padding-left: 10px;">
                    <asp:ImageButton ID="btnApply" ImageUrl="~/Images/save-button.png" ValidationGroup="Apply"
                        CausesValidation="true" runat="server" OnClick="btnApply_Click" />
                </div>
            </div>
        </div>
        <div id="divDeliveryGridCCI" runat="server" style="clear: both; width: 100%; background-color:White; 
            overflow: hidden; margin-bottom: 15px;">
            <div style="padding-left: 10px; padding-top: 10px; padding-bottom: 10px; font-weight: bold;
                font-size: 17px;">
                <div style="clear: both;">
                    Existing Card
                </div>
                <div style="clear: both; padding-top: 10px;">
                    <asp:Image ID="imgGridMessage" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                </div>
            </div>
            <div style="clear: both; padding: 10px;">
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
            <div style="padding-left: 10px; padding-top: 10px; padding-bottom: 10px;">
                <asp:ImageButton ID="btnAddNewCCInfo" CausesValidation="true" OnClick="btnAddNewCCInfo_Click"
                    runat="server" ImageUrl="~/Images/addnew-btn.png" />
            </div>
        </div>
        <div id="divBilling" runat="server" style="clear: both; margin-bottom: 15px; overflow: hidden;">
            <div style="float: left; width: 481px; background-color:White; 
                height: 540px;">
                <div style="clear: both; padding-top: 30px; padding-left: 40px;">
                    <div style="clear: both; font-size: 22px; font-weight: 700;">
                        Credit Card <span style="color: #DD0017;">Information</span>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 20px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            First Name</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:TextBox ID="txtBFirstName" runat="server" CssClass="paymentTextBox" Width="325px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtBFirstName" ErrorMessage="First Name required!" ValidationGroup="CheckOut"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3">
                        </cc2:ValidatorCalloutExtender>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            Last Name</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:TextBox ID="txtBLastName" runat="server" CssClass="paymentTextBox" Width="325px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtBLastName" ErrorMessage="Last Name required!" ValidationGroup="CheckOut"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                        </cc2:ValidatorCalloutExtender>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            Card Number <span style="font-style: italic;">(the 16 gigits on the front of the card)</span></div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:TextBox ID="txtCCNumber" Width="325px" EnableViewState="False" onblur="removeSpaceINCC();"
                            runat="server" CssClass="paymentTextBox"></asp:TextBox>
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
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <img src="Images/paymentCarts.png" />
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            CVC or CVS</div>
                    </div>
                    <div style="clear: both; font-size: 14px; font-style: italic;">
                        (Last 3 digits on back of card, Amex: 4 digit code on front)
                    </div>
                    <div style="float:left; overflow:hidden; clear: both; font-size: 14px; padding-top: 10px;">
                        <div style="float:left; padding:0px 5px 0px 0px;">
                            <asp:TextBox ID="txtCVNumber" runat="server" CssClass="paymentTextBox" Width="80px"
                                MaxLength="4"></asp:TextBox>
                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server"
                                ControlToValidate="txtCVNumber" ErrorMessage="Credit verification number required!"
                                ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RequiredFieldValidator8">
                            </cc2:ValidatorCalloutExtender>
                            <asp:HiddenField ID="hfCVNumber" runat="server" />
                        </div>
                        <div style="float:left; padding-left:5px; padding-top:2px;"><img src="Images/cvcCard.png" alt="" /></div>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            Expiration Date</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <div style="float: left; padding-top: 3px; font-style: italic;">
                            MM</div>
                        <div style="float: left; padding-left: 5px;">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="DropDownBG">
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
                        <div style="float: left; padding-left: 20px; padding-top: 3px; font-style: italic;">
                            YY</div>
                        <div style="float: left; padding-left: 5px;">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="DropDownBG">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; padding-left: 11px; height: 540px;">
                &nbsp;
            </div>
            <div style="float: left; width: 481px; background-color:White; 
                height: 540px;">
                <div style="clear: both; padding-top: 30px; padding-left: 40px;">
                    <div style="clear: both; font-size: 22px; font-weight: 700;">
                        Billing <span style="color: #DD0017;">Address</span>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 20px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            Address Line 1</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:TextBox ID="txtBAddress1" runat="server" CssClass="paymentTextBox" Width="325px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtBAddress1" ErrorMessage="Address 1 required!" ValidationGroup="CheckOut"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                        </cc2:ValidatorCalloutExtender>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        Address Line 2
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 5px;">
                        <asp:TextBox ID="txtBAddress2" runat="server" CssClass="paymentTextBox" Width="325px"></asp:TextBox>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            Country</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:DropDownList ID="ddlBillingCountry" runat="server" CssClass="DropDownBG"
                            onfocus="this.className='DropDownBG'" Width="330px" Height="32px">
                            <asp:ListItem Text="Canada" Value="Canada" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="United States" Value="United States"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            Province/State</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:TextBox ID="txtBProvince" runat="server" CssClass="paymentTextBox" Width="325px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtBProvince" ErrorMessage="Province required!" ValidationGroup="CheckOut"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator4">
                        </cc2:ValidatorCalloutExtender>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            City</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:TextBox ID="txtBCity" runat="server" CssClass="paymentTextBox" Width="325px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtBCity" ErrorMessage="City required!" ValidationGroup="CheckOut"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator5">
                        </cc2:ValidatorCalloutExtender>
                    </div>
                    <div style="clear: both; font-size: 14px; padding-top: 15px;">
                        <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                            *</div>
                        <div style="float: left; padding-left: 3px;">
                            Postal Code</div>
                    </div>
                    <div style="clear: both; font-size: 14px;">
                        <asp:TextBox ID="txtBPostalCode" runat="server" CssClass="paymentTextBox" Width="325px"></asp:TextBox>
                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" runat="server"
                            ControlToValidate="txtBPostalCode" ErrorMessage="Postal Code required!" ValidationGroup="CheckOut"
                            Display="None"></cc1:RequiredFieldValidator>
                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator6">
                        </cc2:ValidatorCalloutExtender>
                    </div>
                </div>
            </div>
        </div>
        <div id="divSaveCancelArea" runat="server" style="clear: both; width: 100%; background-color:White; 
            overflow: hidden; margin-bottom: 15px;">
            <div style="clear: both; padding: 10px; overflow: hidden;">
                <div style="float: left;">
                    <asp:ImageButton ID="btnSave" CausesValidation="true" ValidationGroup="AddNew" OnClick="btnSave_Click"
                        runat="server" ImageUrl="~/Images/save-button.png" />
                    <asp:ImageButton ID="btnUpdate" CausesValidation="true" ValidationGroup="Update"
                        OnClick="btnUpdate_Click" runat="server" Visible="false" ImageUrl="~/Images/save-button.png" />
                </div>
                <div style="float: left; padding-left: 10px;">
                    <asp:ImageButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" ImageUrl="~/Images/cancel-button.png" />
                </div>
            </div>
        </div>
        <div id="divShippingAddress" runat="server" style="clear: both; width: 100%; background-color:White; 
            overflow: hidden;">
            <div style="clear: both; padding-top: 30px; padding-left: 40px; padding-bottom: 70px;">
                <div style="clear: both; font-size: 22px; font-weight: 700;">
                    Shipping <span style="color: #DD0017;">Information</span>
                </div>
                <div style="clear: both; font-size: 14px; padding-top: 15px;">
                    <div style="float: left">
                        <asp:CheckBox ID="cbShippingSame" runat="server" onclick="javascript:checkChange();" /></div>
                    <div style="float: left">
                        <asp:Label ID="lblSameAsBilling" runat="server" Text="Same as Billing"></asp:Label>
                    </div>
                </div>
                <div style="clear: both; padding-top: 15px;">
                    <div style="float: left;">
                        <div style="clear: both; font-size: 14px;">
                            <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                *</div>
                            <div style="float: left; padding-left: 3px;">
                                First Name</div>
                        </div>
                        <div style="clear: both; font-size: 14px;">
                            <asp:TextBox ID="txtSFirstName" runat="server" CssClass="paymentTextBox"
                                Width="300px"></asp:TextBox>
                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                ControlToValidate="txtSFirstName" ErrorMessage="First Name required!" ValidationGroup="CheckOut"
                                Display="None"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator9">
                            </cc2:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div style="float: left; padding-left: 30px">
                        <div style="clear: both; font-size: 14px;">
                            <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                *</div>
                            <div style="float: left; padding-left: 3px;">
                                Last Name</div>
                        </div>
                        <div style="clear: both; font-size: 14px;">
                            <asp:TextBox ID="txtSLastName" runat="server" CssClass="paymentTextBox"
                                Width="300px"></asp:TextBox>
                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtSLastName" ErrorMessage="Last Name required!"
                                ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator11">
                            </cc2:ValidatorCalloutExtender>
                        </div>
                    </div>
                </div>
                <div id="divShippingInfo" style='clear: both; display: <%=strhideShippingDiv%>'>
                    <div style="clear: both; padding-top: 15px;">
                        <div style="float: left;">
                            <div style="clear: both; font-size: 14px;">
                                <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                    *</div>
                                <div style="float: left; padding-left: 3px;">
                                    Address Line 1</div>
                            </div>
                            <div style="clear: both; font-size: 14px;">
                                <asp:TextBox ID="txtSAddress1" runat="server" CssClass="paymentTextBox"
                                    Width="300px"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true"
                                    runat="server" ControlToValidate="txtSAddress1" ErrorMessage="Address 1 required!"
                                    ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator12">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div style="float: left; padding-left: 30px">
                            <div style="clear: both; font-size: 14px;">
                                Address Line 2
                            </div>
                            <div style="clear: both; font-size: 14px; padding-top: 5px;">
                                <asp:TextBox ID="txtSAddress2" runat="server" CssClass="paymentTextBox"
                                    Width="300px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 15px;">
                        <div style="float: left;">
                            <div style="clear: both; font-size: 14px;">
                                <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                    *</div>
                                <div style="float: left; padding-left: 3px;">
                                    City</div>
                            </div>
                            <div style="clear: both; font-size: 14px;">
                                <asp:TextBox ID="txtSCity" runat="server" CssClass="paymentTextBox"
                                    Width="300px"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true"
                                    runat="server" ControlToValidate="txtSCity" ErrorMessage="City required!" ValidationGroup="CheckOut"
                                    Display="None"></cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RequiredFieldValidator10">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div style="float: left; padding-left: 30px">
                            <div style="clear: both; font-size: 14px;">
                                <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                    *</div>
                                <div style="float: left; padding-left: 3px;">
                                    Province/State</div>
                            </div>
                            <div style="clear: both; font-size: 14px;">
                                <asp:TextBox ID="txtSProvince" runat="server" CssClass="paymentTextBox"
                                    Width="300px"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                    runat="server" ControlToValidate="txtSProvince" ErrorMessage="Province required!"
                                    ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" TargetControlID="RequiredFieldValidator13">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 15px;">
                        <div style="float: left;">
                            <div style="clear: both; font-size: 14px;">
                                <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                    *</div>
                                <div style="float: left; padding-left: 3px;">
                                    Postal Code</div>
                            </div>
                            <div style="clear: both; font-size: 14px;">
                                <asp:TextBox ID="txtSPostalCode" runat="server" CssClass="paymentTextBox"
                                    Width="300px"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator14" SetFocusOnError="true"
                                    runat="server" ControlToValidate="txtSPostalCode" ErrorMessage="City required!"
                                    ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RequiredFieldValidator14">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div style="float: left; padding-left: 30px">
                            <div style="clear: both; font-size: 14px;">
                                <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                    *</div>
                                <div style="float: left; padding-left: 3px;">
                                    Country</div>
                            </div>
                            <div style="clear: both; font-size: 14px;">
                                <asp:DropDownList ID="ddlShippingCountry" runat="server" CssClass="DropDownBG"
                                    onfocus="this.className='DropDownBG'" Width="305px" Height="32px">
                                    <asp:ListItem Text="Canada" Value="Canada" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="United States" Value="United States"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both; padding-top: 15px;">
                    <div style="float: left;">
                        <div style="clear: both; font-size: 14px;">
                            <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                *</div>
                            <div style="float: left; padding-left: 3px;">
                                Telephone Number</div>
                        </div>
                        <div style="clear: both; font-size: 14px;">
                            <asp:TextBox ID="txtSCellNumber" runat="server" CssClass="paymentTextBox"
                                Width="300px"></asp:TextBox>
                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator15" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtSCellNumber" ErrorMessage="Telephone Number required!"
                                ValidationGroup="CheckOut" Display="None"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" TargetControlID="RequiredFieldValidator15">
                            </cc2:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div style="float: left; padding-left: 30px">
                        <div style="clear: both; font-size: 14px;">
                            <div style="float: left; color: #DD0017; font-size: 18px; padding-top: 2px;">
                                *</div>
                            <div style="float: left; padding-left: 3px;">
                                Note</div>
                        </div>
                        <div style="clear: both; font-size: 14px;">
                            <asp:TextBox ID="txtSNote" runat="server" CssClass="paymentTextBox"
                                Width="300px"></asp:TextBox>
                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator16" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtSNote" ErrorMessage="Note required!" ValidationGroup="CheckOut"
                                Display="None"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="RequiredFieldValidator16">
                            </cc2:ValidatorCalloutExtender>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both; width: 100%; padding-top: 15px; text-align: center;">
            <div style="clear: both;">
                <asp:ImageButton ID="btnContinue" CausesValidation="true" ValidationGroup="CheckOut"
                    runat="server" ImageUrl="~/Images/ContinuePayment.png" UseSubmitBehavior="false"
                    OnClick="btnContinue_Click" />
            </div>
            <div style="clear: both; padding-top: 10px;">
                <asp:Label ID="lblErrorMessage" runat="server" Visible="false" Font-Size="16px" ForeColor="Red"></asp:Label>
            </div>
        </div>
        </div>
</asp:Content>

