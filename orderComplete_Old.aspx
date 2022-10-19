<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="orderComplete_Old.aspx.cs"
    Inherits="orderComplete" Title="Untitled Page" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="JS/jquery.event.hover.js"></script>

    <script type="text/javascript" src="JS/jquery.easing.1.3.js"></script>

    <link href="CSS/tipsy.css" rel="stylesheet" type="text/css" />

    <script src="JS/tipsy.js"></script>

    <div id="fb-root">
    </div>

    <script src="http://connect.facebook.net/en_US/all.js"></script>

    <script type="text/javascript">
    $(document).ready(function() {
     $('#ctl00_ContentPlaceHolder1_txtFriendName').tipsy({ gravity: 's' });
      $('#ctl00_ContentPlaceHolder1_txtEmail').tipsy({ gravity: 's' });      
      
           $("#ctl00_ContentPlaceHolder1_btnEmailSubmit").click(function(e) {  
             var isValidated = true;
             var value = $("#ctl00_ContentPlaceHolder1_txtFriendName").val();      
             var valueEmail = $("#ctl00_ContentPlaceHolder1_txtEmail").val();               
                                        
            if(value=='')
            {
                  $("#ctl00_ContentPlaceHolder1_txtFriendName").addClass("DiscussionError");                                                                                     
                 isValidated = false;
                 
            }
            
            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
              if(valueEmail=='' || !filter.test(valueEmail)){                         
              $("#ctl00_ContentPlaceHolder1_txtEmail").addClass("DiscussionError");                                                                                     
                 isValidated = false;                                            
              }
              if(isValidated)
              
              {
             return; 
              }
             else
              {
               return false; 
              }
            
            
      });
      
    });
    
    
    
          
    
    
    </script>

    <asp:Literal ID="ltFacebookSharing" runat="server"></asp:Literal>
    <div style="background-color: #8ad3fe;">
        <div style="clear: both; padding-bottom: 20px;">
            <div>
                <img id="imgTop" src="Images/congrat_OrderComplete.jpg" />
            </div>
            <div style="color: #444444; font-family: Arial; font-size: 13px;">
                <div>
                    <div style="padding-left: 30px; padding-top: 30px; padding-bottom: 20px;">
                        <asp:Label ID="lblDealHeading" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="24px"
                            runat="server" Text="Detail of the order"></asp:Label>
                    </div>
                    <div style="float: left; padding-left: 30px;">
                        <div style="height: 121px; width: 168px; border: solid 2px #f99d1c; text-align: center;
                            vertical-align: middle;">
                            <asp:Image ID="dealImage" runat="server" Height="121px" Width="168px" ImageUrl="~/Images/checoutImg.jpg" />
                        </div>
                    </div>
                    <div style="float: left; padding-top: 65px; padding-left: 25px; line-height: normal;">
                        <asp:Label ID="lblDealTitle" Font-Bold="true" Font-Size="19px" Width="750px" runat="server"
                            Text="DEAL"></asp:Label>
                    </div>
                </div>
                <div style="padding-top: 16px; clear: both; padding-left: 30px;">
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="Label5" Font-Bold="true" Font-Size="40px" runat="server" Text="Deal"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="float: left; font-weight: bold; padding-left: 10px; font-family: Arial;
                            padding-top: 24px; font-size: 16px;">
                            <sup>C$</sup></div>
                        <div style="padding-top: 35px; float: left; padding-left: 64px; padding-bottom: 10px;">
                            <asp:Label ID="lblDealAmount" Font-Bold="true" Font-Size="48px" runat="server" Text="25.00"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 20px; padding: 1px;">
                    </div>
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="Label6" Font-Bold="true" Font-Size="40px" runat="server" Text="You Save"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="lblSavePercentage" Font-Bold="true" Font-Size="48px" runat="server"
                                Text="50%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div style="padding-top: 16px; clear: both; padding-left: 30px;">
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="Label7" Font-Bold="true" Font-Size="40px" runat="server" Text="Gift"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="float: left; font-weight: bold; padding-left: 10px; font-family: Arial;
                            padding-top: 24px; font-size: 16px;">
                            <sup>C$</sup></div>
                        <div style="padding-top: 35px; float: left; padding-left: 64px; padding-bottom: 10px;">
                            <asp:Label ID="lblGiftPrice" Font-Bold="true" Font-Size="48px" runat="server" Text="25.00"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 20px; padding: 1px;">
                    </div>
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="Label10" Font-Bold="true" Font-Size="40px" runat="server" Text="Total"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="float: left; font-weight: bold; padding-left: 10px; font-family: Arial;
                            padding-top: 24px; font-size: 16px;">
                            <sup>C$</sup></div>
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="lblOrderTotal" Font-Bold="true" Font-Size="48px" runat="server" Text="50.00"></asp:Label>
                        </div>
                    </div>
                </div>
                <div style="padding-top: 16px; clear: both; padding-left: 30px;">
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 15px; padding-bottom: 10px; line-height: 35px;">
                            <asp:Label ID="Label11" Font-Bold="true" Font-Size="30px" runat="server" Text="Credit Card Used"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="float: left; font-weight: bold; padding-left: 10px; font-family: Arial;
                            padding-top: 24px; font-size: 16px;">
                            <sup>C$</sup></div>
                        <div style="padding-top: 35px; float: left; padding-left: 64px; padding-bottom: 10px;">
                            <asp:Label ID="lblCreditCard" Font-Bold="true" Font-Size="48px" runat="server" Text="25.00"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 20px; padding: 1px;">
                    </div>
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 15px; padding-bottom: 10px; line-height: 35px;">
                            <asp:Label ID="Label13" Font-Bold="true" Font-Size="30px" runat="server" Text="Tasty Credit Used"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="float: left; font-weight: bold; padding-left: 10px; font-family: Arial;
                            padding-top: 24px; font-size: 16px;">
                            <sup>C$</sup></div>
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="lblTastyCredit" Font-Bold="true" Font-Size="48px" runat="server" Text="50.00"></asp:Label>
                        </div>
                    </div>
                </div>
                <div id="divShippingAndTax" runat="server" visible="false" style="padding-top: 16px;
                    clear: both; padding-left: 30px;">
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 15px; padding-bottom: 10px; line-height: 35px;">
                            <asp:Label ID="Label12" Font-Bold="true" Font-Size="30px" runat="server" Text="Shipping & Tax"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="float: left; font-weight: bold; padding-left: 10px; font-family: Arial;
                            padding-top: 24px; font-size: 16px;">
                            <sup>C$</sup></div>
                        <div style="padding-top: 35px; float: left; padding-left: 64px; padding-bottom: 10px;">
                            <asp:Label ID="lblShippingAndTax" Font-Bold="true" Font-Size="48px" runat="server"
                                Text="25.00"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 20px; padding: 1px;">
                    </div>
                    <div style="float: left; width: 195px; height: 90px; background-color: #5bb6f7; text-align: center;
                        color: White;">
                        <div style="padding-top: 24px; padding-bottom: 10px; line-height: 35px;">
                            <asp:Label ID="Label15" Font-Bold="true" Font-Size="30px" runat="server" Text="Grand Total"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 265px; height: 90px; background-color: #b5dffd; text-align: center;
                        color: #1690e7;">
                        <div style="float: left; font-weight: bold; padding-left: 10px; font-family: Arial;
                            padding-top: 24px; font-size: 16px;">
                            <sup>C$</sup></div>
                        <div style="padding-top: 35px; padding-bottom: 10px;">
                            <asp:Label ID="lblGrandTotal" Font-Bold="true" Font-Size="48px" runat="server" Text="50.00"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <%-- <div style="clear: both; padding-top: 20px;">
                <div style="height: 60px; background-color: #f99d1c; font-family: Arial; font-size: 24px;
                    text-align: center; font-weight: bold;">
                    <div style="padding-top: 12px; line-height: normal;">
                        <asp:Label ID="lblOrderDescription" runat="server" Width="950px" ForeColor="White"
                            Text="You will receive the deal when the deal count-down ends! Simply login and click ''member area'' on the top right corner. If you have any other questions, please contact us at support@tazzling.com"></asp:Label>
                    </div>
                </div>
            </div>--%>
            <div style="clear: both; padding-top: 20px;">
                <div style="height: 120px; background-color: #f99d1c; font-family: Arial; font-size: 24px;
                    text-align: center; font-weight: bold;">
                    <div style="padding-top: 12px; line-height: normal;">
                        <asp:Label ID="Label1" runat="server" Width="950px" ForeColor="White" Text="You will receive a confirmation email shortly. To access your voucher, simply visit www.tazzling.com and click member area! If you have any question, don't hesitate to contact us at support@tazzling.com"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="clear: both; padding-top: 20px;">
                <div style="float: left;">
                    <div style="padding-left: 40px;">
                        <img id="imgShareDealTop" src="Images/imgShareDealLink2.png" />
                    </div>
                    <div style="padding-top: 18px; padding-left: 40px; clear: both;">
                        <div style="float: left;">
                            <a id="linkFacebook1" runat="server" target="_blank">
                                <img id="img1" src="Images/imgFacebookShare2.jpg" />
                            </a>
                        </div>
                        <div style="float: left; padding-left: 20px;">
                            <a id="linkTweeter1" runat="server" target="_blank">
                                <img id="img2" src="Images/imgTweeterShare2.jpg" />
                            </a>
                        </div>
                    </div>
                    <div style="color: #444444; font-family: Arial; font-size: 13px; clear: both;">
                        <div style="padding-top: 15px; padding-left: 40px; padding-bottom: 15px;">
                            <asp:Label ID="Label2" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="24px" runat="server"
                                Text="Ready to share with friends?"></asp:Label>
                        </div>
                        <div style="padding-left: 40px;">
                            <div style="clear: both;">
                                <div style="float: left; padding-top: 8px;">
                                    <asp:Label ID="Label9" Font-Bold="true" ForeColor="#1690e7" Font-Size="24px" runat="server"
                                        Text="Name"></asp:Label></div>
                                <div style="float: left; padding-left: 8px; padding-right: 12px;">
                                    <asp:TextBox ID="txtFriendName" runat="server" onfocus="this.className='TextBoxDeal'"
                                        CssClass="TextBoxDeal" ToolTip="Your friend name"></asp:TextBox>
                                </div>
                                <div style="float: left; padding-top: 8px;">
                                    <asp:Label ID="Label3" Font-Bold="true" ForeColor="#1690e7" Font-Size="24px" runat="server"
                                        Text="Email"></asp:Label></div>
                                <div style="float: left; padding-left: 8px; padding-right: 12px;">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBoxDeal" onfocus="this.className='TextBoxDeal'"
                                        ToolTip="Your friend email"></asp:TextBox>
                                </div>
                                <div style="float: left;">
                                    <div style="padding-bottom: 10px;">
                                        <asp:ImageButton ID="btnEmailSubmit" runat="server" ImageUrl="~/Images/btnSubmit_Blue.png"
                                            OnClick="btnEmailSubmit_Click" /></div>
                                </div>
                            </div>
                        </div>
                        <div style="padding-left: 10px; clear: both; padding-left: 110px; padding-top: 5px;">
                            <div style="float: left;">
                                <asp:Label ID="lblEmailMessage" ForeColor="#0a3b5f" Font-Size="19px" Visible="false"
                                    runat="server" Text="Email has been sent successfully."></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="color: #444444; font-family: Arial; font-size: 13px; clear: both;">
                        <div style="padding-top: 15px; padding-left: 40px; padding-bottom: 15px;">
                            <asp:Label ID="Label4" Font-Bold="true" ForeColor="#0a3b5f" Font-Size="24px" runat="server"
                                Text="Share your referral link"></asp:Label>
                        </div>
                        <div style="padding-left: 110px;">
                            <div style="float: left;">
                                <asp:TextBox ID="txtShareLink" TextMode="MultiLine" Width="690px" Height="60px" runat="server"
                                    CssClass="TextBoxDeal" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div style="text-align: center; padding-top: 70px;">
                            <div style="padding-top: 10px; padding-bottom: 5px;">
                                <asp:Label ID="Label8" Font-Bold="true" ForeColor="#1690e7" Font-Size="22px" runat="server"
                                    Text="Copy the link and send to your friend to receive $10 credits when they order $20 or more!"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="text-align: center; clear: both; font-family: Arial; font-size: 24px;
                    color: #1690e7; padding-top: 20px;">
                    If you have any question, don't hesitate to contact us at <a href="mailto:support@tazzling.com"
                        style="color: white">support@tazzling.com</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
