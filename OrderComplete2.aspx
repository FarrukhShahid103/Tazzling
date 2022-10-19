<%@ Page Title="Order Complete" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="OrderComplete2.aspx.cs" Inherits="OrderComplete2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
        .textBoxBG
        {
            background-image: url('images/textBoxBG.png');
            border-radius: 5px;
            height: 28px;
            border: 1px solid #D1D6DC;
        }
        .textBoxError
        {
            /*background-image: url('images/textBoxBG.png');*/
            border-radius: 5px;
            height: 28px;
            border: 2px solid #FF0000;
            background-color:#F6CCDA;
        }
</style>

    <script type="text/javascript">
        $(document).ready(function () {
            //$('#ctl00_ContentPlaceHolder1_txtFriendName').tipsy({ gravity: 's' });
            $('#ctl00_ContentPlaceHolder1_txtEmail').tipsy({ gravity: 's' });

            $("#ctl00_ContentPlaceHolder1_lnkSendInvitation").click(function (e) {
                var isValidated = true;
                //var value = $("#ctl00_ContentPlaceHolder1_txtFriendName").val();
                var valueEmail = $("#ctl00_ContentPlaceHolder1_txtEmail").val();

                //if (value == '') {
                //   $("#ctl00_ContentPlaceHolder1_txtFriendName").addClass("TextBoxError");
                //   isValidated = false;

                //}

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

        function checkEmailEmpty() {
            var isValid = true;
            if ($("input[id$='_txtEmail']").val() == "") {
                $("input[id$='_txtEmail']").removeClass("textBoxBG").addClass("textBoxError");
                IsValid = false;
            }
            
            return isValid;
        }

    </script>

    <asp:Literal ID="ltFacebookSharing" runat="server"></asp:Literal>
    <center>
        <asp:UpdatePanel ID="udpnl" runat="server">
            <ContentTemplate>
                <div style="clear: both; width: 100%; text-align: left;
                    padding-top: 40px;">
                    <img src="Images/PlaceOrder.png" />
                </div>
                <div style="clear: both; width: 100%; padding-top: 30px;">
                </div>
                <div style="width: 980px;">
                    <div style="clear: both;">
                        <div class="DetailPageTopDiv">
                            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                                <div class="PageTopText" style="float: left;">
                                    Congratulations! You purchased taste of the day
                                </div>
                            </div>
                        </div>
                        <div class="DetailPage2ndDiv">
                            <div style="float: left; width: 980px;">
                                <div style="clear: both; width: 100%; padding-bottom:15px;">
                                    <div style="clear: both; width: 100%;">
                                        <div class="DetailTheDetailDiv" style="font-size: 30px; font-weight: bold; color: #363636;">
                                            <div style="float: left; padding: 15px 0px 0px 20px;">
                                                Detail of the order
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <div style="width: 98%; text-align: left; overflow: hidden; background-color:White; padding:10px;">
                                            <div style="clear: both; width: 100%;">
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
                                                            <asp:Label ID="Label4" runat="server" Text="Tazzling Credit Used"></asp:Label>
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
                                        <div style="width: 900px; clear: both; padding-bottom:15px;">
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
                                        <%--<div style="clear: both; width: 100%;">
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
                                        </div>--%>
                                    </div>
                                </div>
                                <div style="clear:both; float:left; overflow:hidden; width:100%;">
                                    <div style="float:left; padding:10px; background-color:White; overflow:hidden; width:400px; height:200px;">
                                        <div style="float:left; font-size:26px; padding:10px 0px;">Your Special <span style="color:#DD0016;">Invite Link</span></div>
                                        <div style="clear:both; float:left; font-size:13px; padding:10px 0px 20px 0px;">Copy paste the URL below into <b>Twitter, Facebook</b> or in an <b>Email</b></div>
                                        <div style="clear:both; float:left;"><asp:TextBox ID="txtShareLink" ReadOnly="true" runat="server" Width="215px" CssClass="textBoxBG"></asp:TextBox></div>
                                        <div style="float:left; padding:8px 0px 0px 15px;"> Share </div>
                                        <div style="float:left; padding:10px 0px 0px 12px;">
                                            <a id="linkTweeter1" runat="server" target="_blank">
                                                <img id="img3" src="Images/twitter.png" alt="" />
                                            </a>
                                        </div>
                                        <div style="float:left; padding:10px 0px 0px 5px;">
                                            <a id="linkFacebook1" runat="server" target="_blank">
                                                <img id="img2" src="images/facebook-icn.png" alt="" />
                                            </a>
                                        </div>
                                    </div>
                                    <div style="float:left; width:10px;">&nbsp;</div>
                                    <div style="float:left; background-color:White; overflow:hidden; width:530px; height:200px; padding:10px;">
                                        <div style="float:left; font-size:26px; padding:10px 0px 20px;"><span style="color:#DD0016;">Invite</span> More People</div>
                                        <div style="clear:both; float:left; overflow:hidden; border-bottom:1px solid #DEDEDE; width:100%; font-size:17px;">
                                            <div style="float:left; border-top:1px solid #DEDEDE; border-left:1px solid #DEDEDE; border-right:1px solid #DEDEDE; padding:8px 12px; margin-right:5px; color:white; background-color:#DD0016;">Email</div>
                                        </div>
                                        <div style="clear:both; font-size:12px; padding:10px 0px; text-align:left;">Add friend's email addresses</div>
                                        <div style="clear:both; float:left; overflow:hidden;">
                                            <div style="float:left;"><asp:TextBox ID="txtEmail" runat="server" Width="410px" CssClass="textBoxBG"></asp:TextBox></div>
                                            <div style="float:left; padding-left:5px;"><asp:ImageButton ID="lnkSendInvitation" runat="server" ImageUrl="images/SendInvitation.png" OnClientClick="return checkEmailEmpty();" OnClick="lnkSendInvitation_Click" /></div>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear:both; float:left; padding:20px 10px 10px 10px; font-size:26px;">Interested Earning <span style="color:#DD0016;">Cash </span>Instead?</div>
                                <div style="clear:both; font-size:13px; padding:10px; text-align:left;">Click <a href="#">HERE </a>to Join our Affiliate Program</div>
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

