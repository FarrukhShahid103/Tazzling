<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlSearch.ascx.cs" Inherits="Takeout_UserControls_Templates_ctrlSearch" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript">

    jQuery(document).ready(function() {
        jQuery('#lbAdvanceSearch').live('click',function() {
            jQuery('#advanceSearch').slideToggle();
        });

        jQuery('#a1').live('click', function() {
            jQuery('#div1').slideToggle();
            var image1 = document.getElementById('Img1').src;
            if (image1.indexOf("upBullet", 0) != -1) {
                jQuery('#Img1').attr("src", "images/downBullet.png");
            }
            else {
                jQuery('#Img1').attr("src", "images/upBullet.png");
            }
        });
        jQuery('#a2').live('click', function() {
            jQuery('#div2').slideToggle();
            var image2 = document.getElementById('Img2').src;
            if (image2.indexOf("upBullet", 0) != -1) {
                jQuery('#Img2').attr("src", "images/downBullet.png");
            }
            else {
                jQuery('#Img2').attr("src", "images/upBullet.png");
            }
        });
        jQuery('#a3').live('click', function() {
            jQuery('#div3').slideToggle();
            var image3 = document.getElementById('Img3').src;
            if (image3.indexOf("upBullet", 0) != -1) {
                jQuery('#Img3').attr("src", "images/downBullet.png");
            }
            else {
                jQuery('#Img3').attr("src", "images/upBullet.png");
            }
        });
    });


    function provinceChanged(obj, objLive, objCh) {
        var ctryID = obj

        //First it will null all the options of the ProvinceLive drop down list
        for (loop = document.getElementById(objLive).options.length - 1; loop > -1; loop--) {
            document.getElementById(objLive).options[loop] = null;
        }

        var count = 0;
        var ProConID
        //The it will get the required value from the Hidden drop down list and set it into the Live drop down list
        for (loop = document.getElementById(objCh).options.length - 1; loop > -1; loop--) {
            ProConID = document.getElementById(objCh).options[loop].value;
            //First Part contain the Province ID and Second Part contain the Country ID
            var ProConArray = ProConID.split(",");
            //Validate that Country ID in the country drop down list macthes with the country provinces into the hidden drop down list
            if (ProConArray[1] == ctryID) {
                var ProvinceName = document.getElementById(objCh).options[loop].text;
                document.getElementById(objLive).options[count] = new Option(ProvinceName);
                document.getElementById(objLive).options[count].value = ProConArray[0];
                count++;
            }
        }
        document.getElementById('ctl00_Search_hfCity').value = 0;
        return false;
    }


    function setCityTextOnProvinceChange() {
        var objDll = document.getElementById('ctl00_Search_ddlProState').selectedIndex;
        if (objDll != 0) {
            document.getElementById('ctl00_Search_hfCity').value = document.getElementById('ctl00_Search_ddlCity').options[document.getElementById('ctl00_Search_ddlCity').selectedIndex].text;
        }
        else {
            document.getElementById('ctl00_Search_hfCity').value = 0;
        }
    }

    function setCityTextOnCityChange() {
        var objDll = document.getElementById('ctl00_Search_ddlCity').selectedIndex;
        if (objDll != 0) {
            document.getElementById('ctl00_Search_hfCity').value = document.getElementById('ctl00_Search_ddlCity').options[objDll].text;
        }
        else {
            document.getElementById('ctl00_Search_hfCity').value = 0;
        }
    }
    
    
</script>

<div>
    <div style="float: left;">
       <asp:Image runat="server" ID="img0111" ImageUrl="~/images/Search/leftSearch.jpg" />
    </div>
    <div style="float: left" id="clientSearch">
        <asp:Panel runat="server" ID="pnlSearch" DefaultButton="btnSearch">
            <table cellpadding="0" border="0" cellspacing="0" width="100%" class="tblSearch">
                <tr>
                    <td style="padding-left: 5px; text-align: right">
                        <asp:Label ID="lblSearchResCos" runat="server" Text="Restaurant Name"></asp:Label>
                    </td>
                    <td style="padding-left: 13px; text-align: left">
                        <asp:TextBox ID="txtSearchRes" runat="server" CssClass="txtSearch"></asp:TextBox>
                        <cc2:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderRestaurant" runat="server"
                            TargetControlID="txtSearchRes" WatermarkText="Enter restaurant name" WatermarkCssClass="watermark" />
                    </td>
                    <td style="padding-left: 15px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="Postal Code"></asp:Label>
                    </td>
                    <td style="padding-left: 13px; text-align: left">
                        <asp:TextBox ID="txtSearchPostalCode" runat="server" CssClass="txtSearch" MaxLength="3" Width="220px"></asp:TextBox>
                        <cc2:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearchPostalCode"
                            WatermarkText="First part of postal code e.g. V5Z" WatermarkCssClass="watermark" />
                    </td>
                    <td style="padding-left: 8px;">
                        <asp:Button ID="btnSearch" Text="Search" runat="server" Font-Bold="true" CssClass="btn_yellow_search"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td style="padding-left: 12px;">
                        <a href="javascript:void(0)" id="lbAdvanceSearch" style="font-family: Arial; color: #ffffff;
                            font-size: 14px; font-weight: bold;">Advanced Filter</a>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="float: right">
      <asp:Image runat="server" ID="img12" ImageUrl="~/images/Search/rightSearch.jpg" /></div>
    <asp:Panel ID="pnlAdvanceSearch" runat="server" align="center" DefaultButton="btnInnerSearch">
        <div id="advanceSearch">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td align="left">
                        <div id="a1" class="searchLink">
                            <a href="javascript:void(0)">
                                <asp:Image runat="server" ImageUrl="~/images/upBullet.png" id="Img1" />
                                Location</a></div>
                        <div id="div1" class="searchContent">
                            <table border="0" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAddress" Text="Address" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchAddress" runat="server" Width="220px" CssClass="txtInnerSearch"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProState" Text="Province" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProState" runat="server" onchange="provinceChanged(this.value,'ctl00_Search_ddlCity','ctl00_Search_ddlCityPro'); setCityTextOnProvinceChange();"
                                            CssClass="txtInnerSearch">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCity" runat="server" Width="184px" onchange="setCityTextOnCityChange();">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlCityPro" runat="server" Style="display: none;">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfCity" Value="0" runat="server" />
                                    </td>
                                    <%--<td>
                                        <asp:TextBox ID="txtCity" runat="server" Width="220px" CssClass="txtInnerSearch"></asp:TextBox>
                                    </td>--%>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div id="a2" class="searchLink">
                            <a href="javascript:void(0)">
                                <asp:Image runat="server" ImageUrl="~/images/upBullet.png" id="Img2" />
                                Type of Cuisines</a></div>
                        <div id="div2" class="searchContent">
                            <asp:CheckBoxList ID="chkCuisines" runat="server" RepeatDirection="Horizontal" CellPadding="3"
                                CellSpacing="3">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div id="a3" class="searchLink">
                            <a href="javascript:void(0)">
                                <asp:Image runat="server" ImageUrl="~/images/upBullet.png" id="Img3" />
                                Options</a></a></div>
                        <div id="div3" class="searchContent">
                            <table border="0" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDelivery" Text="Delivery" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblDelivery" RepeatDirection="Horizontal" runat="server"
                                            CellPadding="3" CellSpacing="3">
                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDistance" Text="Filter Distance Within" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDistance" runat="server" CssClass="txtInnerSearch"></asp:TextBox>
                                        <asp:DropDownList ID="ddlDistance" runat="server" CssClass="txtInnerSearch">
                                            <asp:ListItem Selected="True" Value="KM" Text="KM"></asp:ListItem>
                                            <asp:ListItem Text="Miles" Value="Miles"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="innerSearchButton">
                            <asp:Button ID="btnInnerSearch" runat="server" Text="Filter" Font-Bold="true" CssClass="btn_yellow_search"
                                OnClick="btnSearch_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</div>
