<%@ Page Title="Tastygo | Countact Us" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="orderComplete.aspx.cs" Inherits="orderComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_txtFriendName').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtEmail').tipsy({ gravity: 's' });

            $("#ctl00_ContentPlaceHolder1_btnEmailSubmit").click(function (e) {
                var isValidated = true;
                var value = $("#ctl00_ContentPlaceHolder1_txtFriendName").val();
                var valueEmail = $("#ctl00_ContentPlaceHolder1_txtEmail").val();

                if (value == '') {
                    $("#ctl00_ContentPlaceHolder1_txtFriendName").addClass("TextBoxError");
                    isValidated = false;

                }

                var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
                if (valueEmail == '' || !filter.test(valueEmail)) {
                    $("#ctl00_ContentPlaceHolder1_txtEmail").addClass("TextBoxError");
                    isValidated = false;
                }
                if (isValidated) {
                    return;
                }
                else {
                    return false;
                }


            });

        });    
    </script>
    <asp:Literal ID="ltFacebookSharing" runat="server"></asp:Literal>
    <center>
        <asp:UpdatePanel ID="udpnl" runat="server">
            <ContentTemplate>
                <div style="width: 980px;">
                    <div style="clear: both; padding-top: 20px">
                        <div class="DetailPageTopDiv">
                            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                                <div class="PageTopText" style="float: left;">
                                    Congratulations! You purchased taste of the day
                                </div>
                            </div>
                        </div>
                        <div class="DetailPage2ndDiv">
                            <div style="float: left; width: 980px; background-color: White;">
                                <div style="clear: both; width: 100%;">
                                    <div style="clear: both; width: 100%;">
                                        <div class="DetailTheDetailDiv" style="font-size: 30px; font-weight: bold; color: #103054;">
                                            <div style="float: left; padding: 10px 0px 0px 0px;">
                                                Detail of the order
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="margin-left: 15px;">
                                    <div>
                                        <div style="width: 950px; text-align: left; overflow: hidden;">
                                            <div style="clear: both; width: 100%; padding: 10px 0px 0px 0px;">
                                                <div class="DetailTheDetailDiv_OrderDetail" style="font-size: 15px; font-weight: bold;
                                                    color: #103054;">
                                                    <div style="float: left; padding: 10px 0px 0px 80px;">
                                                        Item Description
                                                    </div>
                                                    <div style="float: left; padding: 10px 0px 0px 400px;">
                                                        Quantity</div>
                                                    <div style="float: left; padding: 10px 0px 0px 80px;">
                                                        Price</div>
                                                    <div style="float: left; padding: 10px 0px 0px 80px;">
                                                        You Pay</div>
                                                </div>
                                            </div>
                                            <div style="clear: both; width: 100%; font-size: 13px; overflow: hidden; color: #5E636C;">
                                                <asp:GridView runat="server" ID="gridview1" DataKeyNames="productID" AllowPaging="false"
                                                    AutoGenerateColumns="false" CellPadding="2" CellSpacing="0" GridLines="None"
                                                    Width="100%" ShowHeader="false">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div style="clear: both; padding-top:10px;">
                                                                    <div style="float: left; padding-left: 10px;">
                                                                        <img id="imgCart" width="110px" src='<%# Eval("image") %>' /></div>
                                                                    <div style="float: left; padding-left: 10px; padding-top: 5px; width: 450px;">
                                                                        <asp:Label ID="lblGridDealTitle" runat="server" Text='<% #Eval("title") %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 50px; padding-top: 20px;">
                                                                        <asp:Label ID="Label9" runat="server" Text='<% #Eval("Qty") %>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 105px; padding-top: 20px; width: 90px;">
                                                                        <div style="float: left; font-size: 11px;">
                                                                            <sup>C$</sup></div>
                                                                        <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                                            <asp:Label ID="lblPriceGrid" runat="server" Text='<% #Eval("sellingPrice") %>'></asp:Label></div>
                                                                    </div>
                                                                    <div style="float: left; padding-left: 35px; padding-top: 20px; width: 80px;">
                                                                        <div style="float: left; font-size: 11px;">
                                                                            <sup>C$</sup></div>
                                                                        <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                                            <asp:Label ID="lblTotalPriceGrid" runat="server" Text='<% # (Convert.ToDouble(Eval("Qty"))*Convert.ToDouble(Eval("sellingPrice"))).ToString()%>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="divShipping" runat="server" style="clear: both; width: 100%; font-size: 13px;color:#919AA8;
                                                overflow: hidden; padding-top: 15px;" visible="false">
                                                <div style="float: left; padding: 10px 0px 0px 15px; width: 310px; font-weight: bold;">
                                                    <asp:Label ID="Label12" runat="server" Text="Shipping & Tax"></asp:Label>
                                                </div>
                                                <div style="float: right; padding: 10px 40px 0px 0px;color:#5E636C;">
                                                    <div style="float: left; font-size: 11px;">
                                                        <sup>C$</sup></div>
                                                    <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                        <asp:Label ID="lblShippingAndTax" runat="server" Text="0"></asp:Label></div>
                                                </div>
                                            </div>
                                            <div class="onPxStrip">
                                            </div>
                                            <div style="clear: both; width: 100%; font-size: 13px; overflow: hidden; padding-top: 10px;">
                                                <div style="clear: both; padding-left: 10px; padding-right: 10px; font-weight: bold;">
                                                    <div>
                                                        <div style="float: left; padding: 0px 0px 0px 5px; width: 310px; font-size: 13px; color:#919AA8;">
                                                            <asp:Label ID="lblTotalText" runat="server" Text="YOUR TOTAL"></asp:Label>
                                                        </div>
                                                        <div style="float: right; padding: 0px 40px 0px 0px; color:#5E636C;">
                                                            <div style="float: left; font-size: 11px;">
                                                                <sup>C$</sup></div>
                                                            <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                                <asp:Label ID="lblGrandTotal" runat="server" Text="99"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="clear: both; width: 100%; font-size: 13px; overflow: hidden; padding-top: 10px;">
                                                <div style="clear: both; padding-left: 10px; padding-right: 10px; font-weight: bold;">
                                                    <div>
                                                        <div style="float: left; padding: 0px 0px 0px 5px; width: 310px; font-size: 13px;color:#919AA8;">
                                                            <asp:Label ID="Label4" runat="server" Text="Tasty Credit Used"></asp:Label>
                                                        </div>
                                                        <div style="float: right; padding: 0px 40px 0px 0px;color:#5E636C;">
                                                            <div style="float: left; font-size: 11px;">
                                                                <sup>C$</sup></div>
                                                            <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                                <asp:Label ID="lblTastyCreditUsed" runat="server" Text="99"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="clear: both; width: 100%; font-size: 13px; overflow: hidden; padding-top: 10px;">
                                                <div style="clear: both; padding-left: 10px; padding-right: 10px; font-weight: bold;">
                                                    <div>
                                                        <div style="float: left; padding: 0px 0px 5px 5px; width: 310px; font-size: 13px;color:#919AA8;">
                                                            <asp:Label ID="Label1" runat="server" Text="Charge From Credit Card"></asp:Label>
                                                        </div>
                                                        <div style="float: right; padding: 0px 40px 5px 0px;color:#5E636C;">
                                                            <div style="float: left; font-size: 11px;">
                                                                <sup>C$</sup></div>
                                                            <div style="float: left; padding-left: 5px; padding-top: 2px; padding-bottom: 2px;">
                                                                <asp:Label ID="lblCreditCardUsed" runat="server" Text="99"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="onPxStrip">
                                            </div>
                                        </div>
                                        <div style="width: 900px; clear: both;">
                                            <div style="padding-top: 20px; clear: both; padding-right: 20px;">
                                                <div style="clear:both; width: 900px; background-color: #fffebc; height: 60px; text-align:center; font-size:13px;">
                                                    <div style="padding-left: 5px; padding-top: 13px; padding-right: 5px;">
                                                        You will receive a confirmation email shortly. To access your voucher, simply visit
                                                        www.tazzling.com and click member area! If you have any question, don't hesitate
                                                        to contact us at support@tazzling.com
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="clear: both; width: 100%;">
                                            <div style="clear: both; text-align: center; padding-top: 25px;">
                                                <div style="font-size: 17px; font-weight: bold;">
                                                    <asp:Label ID="lblShareDeal" runat="server" Width="445px" Text="Share this deal and receive <span style='color:#ff7800;'>$10</span> when you refer a new Tasty Customer!"></asp:Label>
                                                </div>
                                            </div>
                                            <div style="width: 700px;">
                                                <div style="clear: both; padding-top: 25px; text-align: center;">
                                                    <div style="clear: both; padding-left: 85px;">
                                                        <div style="float: left;">
                                                            <a id="linkFacebook1" runat="server" target="_blank">
                                                                <img id="img2" src="Images/ocFBShare.jpg" />
                                                            </a>
                                                        </div>
                                                        <div style="float: left; padding-left: 40px;">
                                                            <a id="linkTweeter1" runat="server" target="_blank">
                                                                <img id="img3" src="Images/ocTWShare.jpg" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="clear: both; padding-top: 25px;">
                                                    <div style="font-size: 17px; font-weight: bold; padding-left: 75px;">
                                                        Ready to share with friends?
                                                    </div>
                                                </div>
                                                <div style="clear: both; padding-top: 10px; padding-left: 75px;">
                                                    <div style="float: left;">
                                                        <div class="ItemHiding">
                                                            Name
                                                        </div>
                                                        <div style="margin-bottom: 10px;">
                                                            <asp:TextBox ID="txtFriendName" runat="server" onfocus="this.className='TextBox'"
                                                                CssClass="TextBox" ToolTip="Your friend name"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="float: left; padding-left: 10px;">
                                                        <div class="ItemHiding">
                                                            Email
                                                        </div>
                                                        <div style="margin-bottom: 10px;">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox" onfocus="this.className='TextBox'"
                                                                ToolTip="Your friend email"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="clear: both;">
                                                    <div style="float: right; padding-right: 60px;">
                                                        <asp:Button CssClass="button big primary" runat="server" OnClick="btnEmailSubmit_Click"
                                                            ID="btnEmailSubmit" Text="Submit" ValidationGroup="CreateUserWizard1" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="width: 700px; padding-left: 20px;">
                                                <div style="clear: both; padding-top: 10px; padding-left: 20px;">
                                                    <div style="font-size: 17px; font-weight: bold;">
                                                        Share your referral link
                                                    </div>
                                                </div>
                                                <div style="clear: both; padding-top: 10px;">
                                                    <div>
                                                        <asp:TextBox ID="txtShareLink" TextMode="MultiLine" Width="560px" Height="55px" runat="server"
                                                            CssClass="TextBox" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="clear: both; text-align: center; padding-top: 25px;">
                                                <div style="font-size: 17px; font-weight: bold;">
                                                    <asp:Label ID="Label3" runat="server" Width="535px" Text="Copy the link and send to your friend to receive <span style='color:#ff7800;'>$10</span> credits when they order $20 or more!"></asp:Label>
                                                </div>
                                            </div>
                                            <div style="clear: both; text-align: center; padding-top: 20px;">
                                                <div>
                                                    If you have any question, don't hesitate to contact us at <a href="mailto:support@tazzling.com"
                                                        style="color: Black;">support@tazzling.com</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both; height: 20px;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
