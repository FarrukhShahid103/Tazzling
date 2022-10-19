<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrdersList.ascx.cs" Inherits="Takeout_UserControls_Order_OrdersList" %>
<div class="orderslist">
    <div class="ordersheader">
        <div class="height10">
        </div>
        <div class="viewhistory">
            <asp:LinkButton runat="server" ID="lbViewHistory" OnClick="lbViewHistory_Click" Visible="false"
                Text="View History"></asp:LinkButton></div>
        <div class="clear">
        </div>
        <div class="floatLeft" style="padding-top: 2px; padding-bottom: 7px; padding-left: 4px;">
            <div style="float: left; padding-right: 5px">
                <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/Checked.png" />
            </div>
            <div class="floatLeft">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Black" Visible="false" Text="Record has been updated successfully."
                    CssClass="fontStyle"></asp:Label>
            </div>
        </div>
        &nbsp;</div>
    <div style="padding: 5px; text-align: center;">
        <h4>
            <asp:Label ID="lblHeaderMessage" Visible="false" runat="server" /></h4>
    </div>
    <div style="color: #ed9702; font-size: 16px; font-weight: bold; padding-left: 20px;">
        <asp:Label ID="Label1" Text="Order Summary" runat="server"></asp:Label>
    </div>
    <div style="padding-top: 10px; padding-left: 25px">
        <div style="text-align: left; border: 1px solid #f99d1c; height: 30px; width: 1000px;
            background: #fbf0d1">
            <div class="memebreferral" style="float: left; width: 160px; height: 18px; background-color: #f99d1c;
                color: White; padding: 6px 6px 6px 15px;">
                <%= "<b>" + "Veiw Transcation by" + "</b> &nbsp;&nbsp;" %>
            </div>
            <div>
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="memebreferral" style="padding-left: 10px; padding-top: 5px; padding-bottom: 5px;">
                            <%= "Year" + ":&nbsp;"%>
                        </td>
                        <td style="padding-left: 10px; padding-top: 5px; padding-bottom: 5px;">
                            <asp:DropDownList runat="server" CssClass="memebreferral" ID="ddlYear" />
                        </td>
                        <td class="memebreferral" style="padding-top: 3px; padding-left: 10px;">
                            <%= "Month" + ":&nbsp;&nbsp;"%>
                        </td>
                        <td style="padding-top: 5px; padding-left: 10px; padding-bottom: 5px;">
                            <asp:DropDownList runat="server" CssClass="memebreferral" ID="ddlMonth">
                            </asp:DropDownList>
                        </td>
                        <td class="memebreferral" style="padding-top: 5px; padding-left: 10px; padding-bottom: 5px;">
                            <asp:Button Height="20px" runat="server" CssClass="loginsubmitbutton_new btnshow" ID="btnSelect"
                                Text="Show" OnClick="btnSelect_Click" CausesValidation="true" ValidationGroup="select" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="padding-top: 10px;">
    </div>
    <div style="padding-left: 25px;">
        <div class="gridborder" style="width: 1000px; padding-bottom: 10px;">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="GridHeader">
                <tr>
                    <td width="10%" style="padding-left:10px;">
                        Order Date &amp; Time
                    </td>
                    <td width="10%" style="text-align:center;">                    
                    Order Type
                    </td>
                    <td width="10%" style="text-align:center; ">
                        Ordered From
                    </td>
                    <td width="10%" style=" text-align:center;">
                        Pick Up / Delivery
                    </td>
                    
                    <td width="10%" style="text-align:center;">
                        Status
                    </td>
                    <td width="10%" style="text-align:center;" >
                        Gross
                    </td>
                   <td width="10%" style='display: <%= bfee  %>;text-align:center;'>
                        <%# (ConfigurationSettings.AppSettings["CommissionFee"].ToString()).ToString()%>10%FEE
                        <%#"Fee"%>
                    </td>
                    <td width="10%" style='display: <%= bnetamount %>;'>
                        Net Amount
                    </td>
                    <td width="8%" style='display: <%= bComm  %>; text-align:center;'>
                        Commission Used
                    </td>
                    <td width="8%" style='display: <%= bRedeem %>; text-align:center;' >
                        Food Credit Used
                    </td>
                </tr>
            </table>
            <asp:GridView runat="server" ID="gridview1" AllowPaging="false" AllowSorting="false"
                AutoGenerateColumns="false" CellPadding="5" CellSpacing="0" GridLines="None"
                Font-Size="Small" HorizontalAlign="Center" Width="1000px" ShowHeader="false"
                PageSize="15" RowStyle-HorizontalAlign="Center" OnPageIndexChanging="gridview1_PageIndexChanging">
                  <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td width="10%" >
                                    <asp:Label CssClass="textalign" width="160px" runat="server" ID="lblDate" Text='<%# GetOrderDate(Convert.ToDateTime(Eval("creationDate")), Eval("OrderId").ToString(), Eval("ProviderId").ToString()) %>' />
                                </td>
                                <td width="8%" style="text-align:left;">
                                    <asp:Label runat="server" ID="lblOrderType" ForeColor="#C15821" Font-Bold="true"
                                        Text='<%# ShowOrderType(Eval("OrderType").ToString()) %>' />
                                </td>
                                <td width="10%" style="text-align:left; padding-left:5px;">
                                    <asp:Label  runat="server" ID="lblOrderFrom" Text='<%#Eval("userName") %>' />
                                </td>
                                <td width="10%" style="text-align:left; padding-left:15px;">
                                    <asp:Label  runat="server" ID="lblShippingMethod" Text='<%# Eval("deliveryMethod").ToString() %>' />
                                </td>
                                <td  width="8%" style="text-align:right; padding-right:10px;">
                                    <asp:Label  runat="server" ID="lblStatus" ForeColor="#C15821" Font-Bold="true" Text='<%# Eval("orderStatus")%>' />
                                </td>
                                <td  width="10%" style="text-align:right; padding-right:20px;">
                                    <asp:Label runat="server" ID="lblAmount" Text='<%# ShowAmount(Eval("CurrencyCode"), Eval("TotalAmount")) %>' />
                                </td>
                                <td  width="10%" style='display: <%= bfee %>;text-align:right; padding-right:25px;'>
                                 <asp:Label runat="server" ID="lblFee" Text='<%# ShowCommissionAndNetAmount(Eval("OrderStatus"),Eval("OrderType"), Eval("CurrencyCode"), Eval("TotalAmount")) %>' />  
                                </td>
                                <td  width="12%" style='display: <%= bnetamount %>;text-align:center;'>
                                    <asp:Label runat="server" ID="lblNetAmount" Text='<%# GetNetAmount(Eval("OrderStatus"),Eval("OrderType"), Eval("subTotalAmount"), Eval("totalAmount")) %>' />
                                </td>
                                <td width="10%" style='display: <%= bComm %>; text-align:center;'>
                                    <asp:Label  runat="server" CssClass="valuealign " ID="Label2" Text='<%# Eval("commission")%>' />
                                </td>
                                <td width="10%" style='display: <%= bRedeem %>; text-align:center;'>
                                    <asp:Label  runat="server" ID="Label3" Text='<%# Eval("redeem")%>' />
                                </td>                            
                            </tr>
                        </table>
                        <div class="cutLineOrange"></div> 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                <EmptyDataTemplate>
                    <div class="emptydatarow">
                        There are no matching data in the system.
                    </div>
                </EmptyDataTemplate>
                <PagerTemplate>
                    <div class="pagerstyle">
                        <div>
                            <div>
                                <asp:LinkButton ID="lbPrev" runat="server" CssClass="pageraction" CausesValidation="False"
                                    CommandArgument="Prev" CommandName="Page" />
                                <asp:Label runat="server" ID="lblNumeric" />
                                <asp:LinkButton ID="lbNext" runat="server" CssClass="pageraction" CausesValidation="False"
                                    CommandArgument="Next" CommandName="Page" />
                            </div>
                        </div>
                    </div>
                </PagerTemplate>
                <PagerSettings PageButtonCount="10" Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>
            <div style="padding-left: 10px; padding-right: 10px;">
                <div class="ordersfooter" style="color: Black;">
                    <div style="float: left; width: 825px; text-align: right;">
                       <asp:Label ID="lblTotalB" runat="server" Text="This Period Balance:"></asp:Label> </div>
                    <div class="totalbalancebg" style="width: 100px; float: right; padding-top: 0px;">
                        <b>
                            <asp:Label runat="server" ID="lblPeriodSales"></asp:Label></b></div>
                </div>
            </div>
        </div>
    </div>
    <div style="height: 5px;">
    </div>
    <div style="padding: 5px 20px 10px 150px;">
        <div class="transanctionbottomalert">
            <div style="width: 46px; height: 42px; float: left; padding-top: 5px;">
                <asp:Image ID="imgqmark" runat="server" ImageUrl="~/Images/loginimages/imgforgotPW.png" />
            </div>
            <div style="padding-top: 15px; padding-left: 20px;">
                If you have any questions regarding to the order, please contact us at <a style="color: #f99d1c;
                    font-weight: bold; text-decoration: none;">1855-295-1771</a> or email at<a style="color: #f99d1c;
                        font-weight: bold;"> support@tazzling.com</a></div>
        </div>
    </div>
</div>
