<%@ Control Language="C#" AutoEventWireup="true" CodeFile="deliveryOptions.ascx.cs"
    Inherits="Takeout_UserControls_restaurant_deliveryOptions" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/FrameStart.ascx" TagPrefix="RedSignal"
    TagName="FrameStart" %>
<%@ Register Src="~/UserControls/Templates/FrameEnd.ascx" TagPrefix="RedSignal"
    TagName="FrameEnd" %>
<RedSignal:FrameStart runat="server" ID="FrameStart3" ElementBoxId="element-box1" />
<div style="overflow: hidden;">

    <script language="javascript" type="text/javascript">

        function validateRestaurantSettings() {
            var objChkDel1 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDelivery');
            var objtxtInsideKM = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_txtInsideKM');
            var objchkFree = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkFree');
            var objtxtInsideMoney = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_txtInsideMoney');
            var objChkDel2 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDeliveryOK1');
            var objtxtPer = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_txtPer');
            var objtxtKMs = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_txtKM');
            
            //If delivery limit is below minimum amount
            var objMinChkDel = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDelMinAmt');
            var objMintxtPer = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_txtDelMinAmtPer');
                        
            var objtxtMinDelOrderAmnt = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_txtMinDelOrderAmnt');

            if (objChkDel1.checked) {

                if (objtxtInsideKM.value == "") {
                    alert("Please enter value");
                    objtxtInsideKM.focus();
                    return false;
                }
                else {
                    if (!IsNumeric(objtxtInsideKM.value)) {
                        alert("Please enter numeric value");
                        objtxtInsideKM.value = "";
                        objtxtInsideKM.focus();
                        return false;
                    }
                }
                if (!objchkFree.checked) {
                    if (objtxtInsideMoney.value == "") {
                        alert("Please enter value");
                        objtxtInsideMoney.focus();
                        return false;
                    }
                    else {
                        if (!IsNumeric(objtxtInsideMoney.value)) {
                            alert("Please enter numeric value");
                            objtxtInsideMoney.value = "";
                            objtxtInsideMoney.focus();
                            return false;
                        }
                    }
                }
            }
            else {
                if (objtxtInsideKM.value != "" || objtxtInsideMoney.value != "" || objtxtPer.value != "" || objtxtKMs.value != "") {
                    alert("Please select the delivery check box.");
                    return false;
                }
            }
            if (!IsNumeric(objtxtMinDelOrderAmnt.value)) {
                alert("Please enter numeric value");
                objtxtMinDelOrderAmnt.value = "";
                objtxtMinDelOrderAmnt.focus();
                return false;
            }
            if (objChkDel2.checked) {
                if (objChkDel1.checked) {
                    if (objtxtPer.value == "") {
                        alert("Please enter value");
                        objtxtPer.focus();
                        return false;
                    }
                    else {
                        if (!IsNumeric(objtxtPer.value)) {
                            alert("Please enter numeric value");
                            objtxtPer.value = "";
                            objtxtPer.focus();
                            return false;
                        }
                    }
                    if (objtxtKMs.value == "") {
                        alert("Please enter value");
                        objtxtKMs.focus();
                        return false;
                    }
                    else {
                        if (!IsNumeric(objtxtKMs.value)) {
                            alert("Please enter numeric value");
                            objtxtKMs.value = "";
                            objtxtKMs.focus();
                            return false;
                        }
                    }
                }
                else {
                    alert("Please select upper options first.");
                    return false;
                }
            }
            else {
                if (objtxtPer.value != "" || objtxtKMs.value != "") {
                    alert('Please select the "Exceed limit distance" check box.');
                    return false;
                }
            }

            //If Delivery Amount is less than the minimum amount
            if (objMinChkDel.checked) {
                if (objChkDel1.checked) {
                    if (objMintxtPer.value == "") {
                        alert("Please enter value");
                        objMintxtPer.focus();
                        return false;
                    }
                    else {
                        if (!IsNumeric(objMintxtPer.value)) {
                            alert("Please enter numeric value");
                            objMintxtPer.value = "";
                            objMintxtPer.focus();
                            return false;
                        }
                    }                   
                }
                else {
                    alert("Please select upper options first.");
                    return false;
                }
            }
            else {
                if (objMintxtPer.value != "" || objMintxtPer.value != "") {
                    alert('Please select the "Below minimum amount" check box.');
                    return false;
                }
            }

            var bool1 = false;
            var bool2 = false;

            var objOpen11 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen1_1');
            var objClose11 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose1_1');
            var objOpen12 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen1_2');
            var objClose12 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose1_2');

            var objMonday = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDay1Monday');
            if (!objMonday.checked) {

                if (objOpen11.selectedIndex == 0 && objClose11.selectedIndex == 0) {
                    bool1 = true;
                }
                else {
                    bool1 = false;
                }
                if (objOpen12.selectedIndex == 0 && objClose12.selectedIndex == 0) {
                    bool2 = true;
                }
                else {
                    bool2 = false;
                }
                if (bool1 && bool2) {
                    alert("Please select atleast one opening and closing hours for Monday.");
                    return false;
                }

                if (!bool1) {

                    if (objOpen11.selectedIndex == 0 || objClose11.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Monday for first shift.");
                        return false;
                    }
                }
                if (!bool2) {

                    if (objOpen12.selectedIndex == 0 || objClose12.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Monday for second shift.");
                        return false;
                    }
                }


                if (parseInt(objOpen11.value) > parseInt(objClose11.value)) {
                    alert("Closing time should be greater than opening time for Monday for first shift.");
                    return false;
                }
            }

            var objOpen21 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen2_1');
            var objClose21 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose2_1');
            var objOpen22 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen2_2');
            var objClose22 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose2_2');

            var objTuesday = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDay2Tuesday');
            if (!objTuesday.checked) {

                if (objOpen21.selectedIndex == 0 && objClose21.selectedIndex == 0) {
                    bool1 = true;
                }
                else {
                    bool1 = false;
                }
                if (objOpen22.selectedIndex == 0 && objClose22.selectedIndex == 0) {
                    bool2 = true;
                }
                else {
                    bool2 = false;
                }

                if (bool1 && bool2) {
                    alert("Please select atleast one opening and closing hours for Tuesday.");
                    return false;
                }

                if (!bool1) {

                    if (objOpen21.selectedIndex == 0 || objClose21.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Tuesday for first shift.");
                        return false;
                    }
                }
                if (!bool2) {

                    if (objOpen22.selectedIndex == 0 || objClose22.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Tuesday for second shift.");
                        return false;
                    }
                }


                if (parseInt(objOpen21.value) > parseInt(objClose21.value)) {
                    alert("Closing time should be greater than opening time for Tuesday for first shift.");
                    return false;
                }
            }

            var objOpen31 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen3_1');
            var objClose31 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose3_1');
            var objOpen32 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen3_2');
            var objClose32 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose3_2');

            var objWednesday = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDay3Wednesday');
            if (!objWednesday.checked) {
                if (objOpen31.selectedIndex == 0 && objClose31.selectedIndex == 0) {
                    bool1 = true;
                }
                else {
                    bool1 = false;
                }
                if (objOpen32.selectedIndex == 0 && objClose32.selectedIndex == 0) {
                    bool2 = true;
                }
                else {
                    bool2 = false;
                }

                if (bool1 && bool2) {
                    alert("Please select atleast one opening and closing hours for Wednesday.");
                    return false;
                }

                if (!bool1) {

                    if (objOpen31.selectedIndex == 0 || objClose31.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Wednesday for first shift.");
                        return false;
                    }
                }
                if (!bool2) {

                    if (objOpen32.selectedIndex == 0 || objClose32.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Wednesday for second shift.");
                        return false;
                    }
                }


                if (parseInt(objOpen31.value) > parseInt(objClose31.value)) {
                    alert("Closing time should be greater than opening time for Wednesday for first shift.");
                    return false;
                }
            }

            var objOpen41 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen4_1');
            var objClose41 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose4_1');
            var objOpen42 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen4_2');
            var objClose42 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose4_2');

            var objThursday = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDay4Thursday');
            if (!objThursday.checked) {
                if (objOpen41.selectedIndex == 0 && objClose41.selectedIndex == 0) {
                    bool1 = true;
                }
                else {
                    bool1 = false;
                }
                if (objOpen42.selectedIndex == 0 && objClose42.selectedIndex == 0) {
                    bool2 = true;
                }
                else {
                    bool2 = false;
                }

                if (bool1 && bool2) {
                    alert("Please select atleast one opening and closing hours for Thursday.");
                    return false;
                }

                if (!bool1) {

                    if (objOpen41.selectedIndex == 0 || objClose41.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Thursday for first shift.");
                        return false;
                    }
                }
                if (!bool2) {

                    if (objOpen42.selectedIndex == 0 || objClose42.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Thursday for second shift.");
                        return false;
                    }
                }


                if (parseInt(objOpen41.value) > parseInt(objClose41.value)) {
                    alert("Closing time should be greater than opening time for Thursday for first shift.");
                    return false;
                }
            }

            var objOpen51 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen5_1');
            var objClose51 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose5_1');
            var objOpen52 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen5_2');
            var objClose52 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose5_2');

            var objFriday = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDay5Friday');
            if (!objFriday.checked) {
                if (objOpen51.selectedIndex == 0 && objClose51.selectedIndex == 0) {
                    bool1 = true;
                }
                else {
                    bool1 = false;
                }
                if (objOpen52.selectedIndex == 0 && objClose52.selectedIndex == 0) {
                    bool2 = true;
                }
                else {
                    bool2 = false;
                }

                if (bool1 && bool2) {
                    alert("Please select atleast one opening and closing hours for Friday.");
                    return false;
                }

                if (!bool1) {

                    if (objOpen51.selectedIndex == 0 || objClose51.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Friday for first shift.");
                        return false;
                    }
                }
                if (!bool2) {

                    if (objOpen52.selectedIndex == 0 || objClose52.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Friday for second shift.");
                        return false;
                    }
                }


                if (parseInt(objOpen51.value) > parseInt(objClose51.value)) {
                    alert("Closing time should be greater than opening time for Friday for first shift.");
                    return false;
                }
            }

            var objOpen61 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen6_1');
            var objClose61 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose6_1');
            var objOpen62 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen6_2');
            var objClose62 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose6_2');

            var objSaturday = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDay6Saturday');
            if (!objSaturday.checked) {
                if (objOpen61.selectedIndex == 0 && objClose61.selectedIndex == 0) {
                    bool1 = true;
                }
                else {
                    bool1 = false;
                }
                if (objOpen62.selectedIndex == 0 && objClose62.selectedIndex == 0) {
                    bool2 = true;
                }
                else {
                    bool2 = false;
                }

                if (bool1 && bool2) {
                    alert("Please select atleast one opening and closing hours for Saturday.");
                    return false;
                }

                if (!bool1) {

                    if (objOpen61.selectedIndex == 0 || objClose61.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Saturday for first shift.");
                        return false;
                    }
                }
                if (!bool2) {

                    if (objOpen62.selectedIndex == 0 || objClose62.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Saturday for second shift.");
                        return false;
                    }
                }


                if (parseInt(objOpen61.value) > parseInt(objClose61.value)) {
                    alert("Closing time should be greater than opening time for Saturday for first shift.");
                    return false;
                }
            }

            var objOpen71 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen7_1');
            var objClose71 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose7_1');
            var objOpen72 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlOpen7_2');
            var objClose72 = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_ddlClose7_2');

            var objSunday = document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_orderOptions_deliveryOptions1_chkDay7Sunday');
            if (!objSunday.checked) {
                if (objOpen71.selectedIndex == 0 && objClose71.selectedIndex == 0) {
                    bool1 = true;
                }
                else {
                    bool1 = false;
                }
                if (objOpen72.selectedIndex == 0 && objClose72.selectedIndex == 0) {
                    bool2 = true;
                }
                else {
                    bool2 = false;
                }

                if (bool1 && bool2) {
                    alert("Please select atleast one opening and closing hours for Sunday.");
                    return false;
                }

                if (!bool1) {

                    if (objOpen71.selectedIndex == 0 || objClose71.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Sunday for first shift.");
                        return false;
                    }
                }
                if (!bool2) {

                    if (objOpen72.selectedIndex == 0 || objClose72.selectedIndex == 0) {
                        alert("Please select both opening and closing hours for Sunday for second shift.");
                        return false;
                    }
                }


                if (parseInt(objOpen71.value) > parseInt(objClose71.value)) {
                    alert("Closing time should be greater than opening time for Sunday for first shift.");
                    return false;
                }
            }

            return true;
        }


        function hideFreeOption(obj) {
            if (obj.checked) {
                document.getElementById('hideFree').style.display = "none";
            }
            else {
                document.getElementById('hideFree').style.display = "block";
            }
        }




        function IsNumeric(strString) {
            var strValidChars = "0123456789.";
            var strChar;
            var blnResult = true;

            if (strString.length == 0) return false;

            //  test strString consists of valid characters listed above
            for (i = 0; i < strString.length && blnResult == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    blnResult = false;
                }
            }
            return blnResult;
        }
    
    
    </script>

    <div style="clear: both;">
        <div style="float: left">
            <h4 class="blue">
                <asp:Label ID="lblDeliveryHeading" runat="server" Text="Delivery Option"></asp:Label></h4>
        </div>
        <div style="float: left; padding-left: 160px;">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="Restaurant settings have been saved sucessfully."></asp:Label>
        </div>
    </div>
    <div style="clear: both;">
        <ul class="deliveryOptions">
            <li class="top">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <span>
                                <asp:Label ID="lblDeliveryOK" runat="server" Text="Delivery OK?"></asp:Label></span>
                            <span>
                                <asp:CheckBox runat="server" ID="chkDelivery" Text="Yes" /></span>&nbsp; <span>
                                    <asp:CheckBox runat="server" ID="chkFree" Text="Free" onclick="hideFreeOption(this);" /></span>&nbsp;
                            &nbsp;<span>
                                <asp:Label ID="lblWithinSecond" runat="server" Text="Within"></asp:Label>
                                <asp:TextBox runat="server" ID="txtInsideKM" Width="50" />
                                &nbsp;<asp:Label ID="lblKM2" runat="server" Text="KM"></asp:Label></span>
                        </td>
                        <td>
                            <span id="hideFree" style='display: <%=strHide%>'>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblcharges"
                                runat="server" Text="charges"></asp:Label>&nbsp;$&nbsp;<asp:TextBox runat="server"
                                    ID="txtInsideMoney" Width="50" /></span>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMinDelOrderAmount" runat="server" Text="Min. delivery order amount"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtMinDelOrderAmnt" Width="50" />
                            
                        </td>
                    </tr>
                </table>
            </li>
            <li>
                <table>
                    <tr>
                        <td>
                            <span>
                                <asp:Label ID="lblDeliveryOK2" runat="server" Text="If exceed limited distance,delivery OK?"></asp:Label></span>
                        </td>
                        <td>
                            <span>
                                <asp:CheckBox runat="server" ID="chkDeliveryOK1" /><asp:Label ID="lblYes" runat="server"
                                    Text="Yes"></asp:Label></span>
                        </td>
                        <td>
                            <asp:Label ID="lblNeedCharge" runat="server" Text="Need Charge $"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPer" Width="50" />&nbsp;<asp:Label ID="lblPer"
                                runat="server" Text="Per"></asp:Label>
                        </td>
                        <td>
                            <span>&nbsp;<asp:TextBox runat="server" ID="txtKM" Width="50" />&nbsp;<asp:Label
                                ID="lblKM3" runat="server" Text="KM"></asp:Label>
                            </span>
                        </td>
                    </tr>
                </table>
            </li>
            <li>
                <table>
                    <tr>
                        <td>
                            <span>
                                <asp:Label ID="Label1" runat="server" Text="If delivery amount is below minimum amount,delivery OK?"></asp:Label></span>
                        </td>
                        <td>
                            <span>
                                <asp:CheckBox runat="server" ID="chkDelMinAmt" /><asp:Label ID="Label2" runat="server"
                                    Text="Yes"></asp:Label></span>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Need Charge $"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDelMinAmtPer" Width="50" />
                        </td>
                        <td>
                            <span>&nbsp;</span>
                        </td>
                    </tr>
                </table>
            </li>
            <li class="bottom"><span><b>
                <asp:Label ID="lblTimeOfOperation" runat="server" Text="Time Of Operation:"></asp:Label></b>&nbsp;<asp:Label
                    ID="lblTimeOfOperationNote" runat="server" Text="[If only has 1 open time with no break, please enter in open time 1]"></asp:Label></span>
                <table cellpadding="08:00" cellspacing="00:00" border="00:00">
                    <thead>
                        <tr>
                            <th>
                            </th>
                            <th>
                                <asp:Label ID="lblOpenTime1" runat="server" Text="Open Time 1"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblCloseTime1" runat="server" Text="Close Time 1"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblOpenTime2" runat="server" Text="Open Time 2"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblCloseTime2" runat="server" Text="Close Time 2"></asp:Label>
                            </th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblMonday" runat="server" Text="Monday"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen1_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose1_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen1_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose1_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>
                                    <asp:Label ID="lblMondayOr" runat="server" Text="Or"></asp:Label></span><asp:CheckBox
                                        runat="server" ID="chkDay1Monday" /><asp:Label ID="lblMondayHour" runat="server"
                                            Text="24 Hours"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTuesday" runat="server" Text="Tuesday"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen2_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose2_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen2_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose2_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>
                                    <asp:Label ID="lblTuesdayOr" runat="server" Text="Or"></asp:Label></span><asp:CheckBox
                                        runat="server" ID="chkDay2Tuesday" /><asp:Label ID="lblTuesdayHours" runat="server"
                                            Text="24 Hours"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblWednesday" runat="server" Text="Wednesday"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen3_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose3_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen3_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose3_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>
                                    <asp:Label ID="lblWednesdayOr" runat="server" Text="Or"></asp:Label></span><asp:CheckBox
                                        runat="server" ID="chkDay3Wednesday" /><asp:Label ID="lblWednesdayHours" runat="server"
                                            Text="24 Hours"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblThursday" runat="server" Text="Thursday"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen4_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose4_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen4_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose4_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>
                                    <asp:Label ID="lblThursdayOr" runat="server" Text="Or"></asp:Label></span><asp:CheckBox
                                        runat="server" ID="chkDay4Thursday" /><asp:Label ID="lblThursdayHour" runat="server"
                                            Text="24 Hours"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFriday" runat="server" Text="Friday"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen5_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose5_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen5_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose5_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>
                                    <asp:Label ID="lblFridayOr" runat="server" Text="Or"></asp:Label></span><asp:CheckBox
                                        runat="server" ID="chkDay5Friday" /><asp:Label ID="lblFridayHours" runat="server"
                                            Text="24 Hours"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSaturday" runat="server" Text="Saturday"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen6_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose6_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen6_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose6_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>
                                    <asp:Label ID="lblSaturdayOr" runat="server" Text="Or"></asp:Label></span><asp:CheckBox
                                        runat="server" ID="chkDay6Saturday" /><asp:Label ID="lblSaturdayHours" runat="server"
                                            Text="24 Hours"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSunday" runat="server" Text="Sunday"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen7_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose7_1">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlOpen7_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlClose7_2">
                                    <asp:ListItem Value="" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0.00">00:00</asp:ListItem>
                                    <asp:ListItem Value="0.25">00:15</asp:ListItem>
                                    <asp:ListItem Value="0.50">00:30</asp:ListItem>
                                    <asp:ListItem Value="0.75">00:45</asp:ListItem>
                                    <asp:ListItem Value="1.00">01:00</asp:ListItem>
                                    <asp:ListItem Value="1.25">01:15</asp:ListItem>
                                    <asp:ListItem Value="1.50">01:30</asp:ListItem>
                                    <asp:ListItem Value="1.75">01:45</asp:ListItem>
                                    <asp:ListItem Value="2.00">02:00</asp:ListItem>
                                    <asp:ListItem Value="2.25">02:15</asp:ListItem>
                                    <asp:ListItem Value="2.50">02:30</asp:ListItem>
                                    <asp:ListItem Value="2.75">02:45</asp:ListItem>
                                    <asp:ListItem Value="3.00">03:00</asp:ListItem>
                                    <asp:ListItem Value="3.25">03:15</asp:ListItem>
                                    <asp:ListItem Value="3.50">03:30</asp:ListItem>
                                    <asp:ListItem Value="3.75">03:45</asp:ListItem>
                                    <asp:ListItem Value="4.00">04:00</asp:ListItem>
                                    <asp:ListItem Value="4.25">04:15</asp:ListItem>
                                    <asp:ListItem Value="4.50">04:30</asp:ListItem>
                                    <asp:ListItem Value="4.75">04:45</asp:ListItem>
                                    <asp:ListItem Value="5.00">05:00</asp:ListItem>
                                    <asp:ListItem Value="5.25">05:15</asp:ListItem>
                                    <asp:ListItem Value="5.50">05:30</asp:ListItem>
                                    <asp:ListItem Value="5.75">05:45</asp:ListItem>
                                    <asp:ListItem Value="6.00">06:00</asp:ListItem>
                                    <asp:ListItem Value="6.25">06:15</asp:ListItem>
                                    <asp:ListItem Value="6.50">06:30</asp:ListItem>
                                    <asp:ListItem Value="6.75">06:45</asp:ListItem>
                                    <asp:ListItem Value="7.00">07:00</asp:ListItem>
                                    <asp:ListItem Value="7.25">07:15</asp:ListItem>
                                    <asp:ListItem Value="7.50">07:30</asp:ListItem>
                                    <asp:ListItem Value="7.75">07:45</asp:ListItem>
                                    <asp:ListItem Value="8.00">08:00</asp:ListItem>
                                    <asp:ListItem Value="8.25">08:15</asp:ListItem>
                                    <asp:ListItem Value="8.50">08:30</asp:ListItem>
                                    <asp:ListItem Value="8.75">08:45</asp:ListItem>
                                    <asp:ListItem Value="9.00">09:00</asp:ListItem>
                                    <asp:ListItem Value="9.25">09:15</asp:ListItem>
                                    <asp:ListItem Value="9.50">09:30</asp:ListItem>
                                    <asp:ListItem Value="9.75">09:45</asp:ListItem>
                                    <asp:ListItem Value="10.00">10:00</asp:ListItem>
                                    <asp:ListItem Value="10.25">10:15</asp:ListItem>
                                    <asp:ListItem Value="10.50">10:30</asp:ListItem>
                                    <asp:ListItem Value="10.75">10:45</asp:ListItem>
                                    <asp:ListItem Value="11.00">11:00</asp:ListItem>
                                    <asp:ListItem Value="11.25">11:15</asp:ListItem>
                                    <asp:ListItem Value="11.50">11:30</asp:ListItem>
                                    <asp:ListItem Value="11.75">11:45</asp:ListItem>
                                    <asp:ListItem Value="12.00">12:00</asp:ListItem>
                                    <asp:ListItem Value="12.25">12:15</asp:ListItem>
                                    <asp:ListItem Value="12.50">12:30</asp:ListItem>
                                    <asp:ListItem Value="12.75">12:45</asp:ListItem>
                                    <asp:ListItem Value="13.00">13:00</asp:ListItem>
                                    <asp:ListItem Value="13.25">13:15</asp:ListItem>
                                    <asp:ListItem Value="13.50">13:30</asp:ListItem>
                                    <asp:ListItem Value="13.75">13:45</asp:ListItem>
                                    <asp:ListItem Value="14.00">14:00</asp:ListItem>
                                    <asp:ListItem Value="14.25">14:15</asp:ListItem>
                                    <asp:ListItem Value="14.50">14:30</asp:ListItem>
                                    <asp:ListItem Value="14.75">14:45</asp:ListItem>
                                    <asp:ListItem Value="15.00">15:00</asp:ListItem>
                                    <asp:ListItem Value="15.25">15:15</asp:ListItem>
                                    <asp:ListItem Value="15.50">15:30</asp:ListItem>
                                    <asp:ListItem Value="15.75">15:45</asp:ListItem>
                                    <asp:ListItem Value="16.00">16:00</asp:ListItem>
                                    <asp:ListItem Value="16.25">16:15</asp:ListItem>
                                    <asp:ListItem Value="16.50">16:30</asp:ListItem>
                                    <asp:ListItem Value="16.75">16:45</asp:ListItem>
                                    <asp:ListItem Value="17.00">17:00</asp:ListItem>
                                    <asp:ListItem Value="17.25">17:15</asp:ListItem>
                                    <asp:ListItem Value="17.50">17:30</asp:ListItem>
                                    <asp:ListItem Value="17.75">17:45</asp:ListItem>
                                    <asp:ListItem Value="18.00">18:00</asp:ListItem>
                                    <asp:ListItem Value="18.25">18:15</asp:ListItem>
                                    <asp:ListItem Value="18.50">18:30</asp:ListItem>
                                    <asp:ListItem Value="18.75">18:45</asp:ListItem>
                                    <asp:ListItem Value="19.00">19:00</asp:ListItem>
                                    <asp:ListItem Value="19.25">19:15</asp:ListItem>
                                    <asp:ListItem Value="19.50">19:30</asp:ListItem>
                                    <asp:ListItem Value="19.75">19:45</asp:ListItem>
                                    <asp:ListItem Value="20.00">20:00</asp:ListItem>
                                    <asp:ListItem Value="20.25">20:15</asp:ListItem>
                                    <asp:ListItem Value="20.50">20:30</asp:ListItem>
                                    <asp:ListItem Value="20.75">20:45</asp:ListItem>
                                    <asp:ListItem Value="21.00">21:00</asp:ListItem>
                                    <asp:ListItem Value="21.25">21:15</asp:ListItem>
                                    <asp:ListItem Value="21.50">21:30</asp:ListItem>
                                    <asp:ListItem Value="21.75">21:45</asp:ListItem>
                                    <asp:ListItem Value="22.00">22:00</asp:ListItem>
                                    <asp:ListItem Value="22.25">22:15</asp:ListItem>
                                    <asp:ListItem Value="22.50">22:30</asp:ListItem>
                                    <asp:ListItem Value="22.75">22:45</asp:ListItem>
                                    <asp:ListItem Value="23.00">23:00</asp:ListItem>
                                    <asp:ListItem Value="23.25">23:15</asp:ListItem>
                                    <asp:ListItem Value="23.50">23:30</asp:ListItem>
                                    <asp:ListItem Value="23.75">23:45</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <span>
                                    <asp:Label ID="lblSundayOr" runat="server" Text="Or"></asp:Label></span><asp:CheckBox
                                        runat="server" ID="chkDay7Sunday" /><asp:Label ID="lblSundayHours" runat="server"
                                            Text="24 Hours"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRealImage" runat="server" Text="Real Image"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:UpdatePanel ID="upRealImage" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="fuRealImage" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSave" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:HiddenField ID="hfRealImage" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Image ID="imgRealImage" Width="100px" runat="server" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </li>
        </ul>
        <div class="deliveryOptions clear">
            <asp:Button runat="server" ID="btnSave" ValidationGroup="delivery" OnClientClick="return validateRestaurantSettings();"
                CssClass="btn_orange_smaller" OnClick="btnSave_Click" Text="Save" />
            <asp:Label runat="server" ID="lblResult" ForeColor="red" EnableViewState="false" />
        </div>
    </div>
</div>
<RedSignal:FrameEnd runat="server" ID="FrameEnd3" />
