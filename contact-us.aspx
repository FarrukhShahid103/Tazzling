<%@ Page Title="Tastygo | Countact Us" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="contact-us.aspx.cs" Inherits="contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    $(document).ready(function() {
     $('#ctl00_ContentPlaceHolder1_txtFname').tipsy({ gravity: 's' });
      $('#ctl00_ContentPlaceHolder1_txtEmail').tipsy({ gravity: 's' });
      $('#ctl00_ContentPlaceHolder1_txtCity').tipsy({ gravity: 's' });
      $('#ctl00_ContentPlaceHolder1_txtReason').tipsy({ gravity: 's' });
      $('#ctl00_ContentPlaceHolder1_txtmessage').tipsy({ gravity: 's' });
            
      
      $("#ctl00_ContentPlaceHolder1_btnsubmit").click(function(e) {  
             var isValidated = true;
             var FriendName = $("#ctl00_ContentPlaceHolder1_txtFname").val();
             var City = $("#ctl00_ContentPlaceHolder1_txtCity").val();
             var Reason = $("#ctl00_ContentPlaceHolder1_txtReason").val();
             
             var Message = $("#ctl00_ContentPlaceHolder1_txtmessage").val();    
               
             var valueEmail = $("#ctl00_ContentPlaceHolder1_txtEmail").val();                         
             
            if (FriendName != "") {                  
               IsValid = true;
                return;        
            }
            else
            {
                  $("#ctl00_ContentPlaceHolder1_txtFname").addClass("TextBoxError");                                                                                     
                 IsValid = false;
                 
            }
            if (City != "") {                  
               IsValid = true;
                return;        
            }
            else
            {
                  $("#ctl00_ContentPlaceHolder1_txtCity").addClass("TextBoxError");                                                                                     
                 IsValid = false;
                 
            }
            
            if (Reason != "") {                  
               IsValid = true;
                return;        
            }
            else
            {
                  $("#ctl00_ContentPlaceHolder1_txtReason").addClass("TextBoxError");                                                                                     
                 IsValid = false;
                 
            }
            
            
            if (Message != "") {                  
               IsValid = true;
                return;        
            }
            else
            {
                  $("#ctl00_ContentPlaceHolder1_txtmessage").addClass("TextBoxError");                                                                                     
                 IsValid = false;
                 
            }
            
       
            var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;
              if(valueEmail=='' || !filter.test(valueEmail)){                         
              $("#ctl00_ContentPlaceHolder1_txtEmail").addClass("TextBoxError");                                                                                     
                 IsValid = false;
                
              
              
              }
              if(IsValid)
              
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

    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                        Contact Us
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 720px; background-color: White; min-height: 450px;">
                    <asp:UpdatePanel ID="udpnl" runat="server">
                        <ContentTemplate>
                            <div style="width: 100%; margin-left: 15px;">
                                <div style="margin-top: 30px;">
                                    <div style="float: left;">
                                        <div style="display: none;">
                                            <div class="ItemHiding">
                                                Name
                                            </div>
                                            <div>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtFname" Title="Please enter your name" runat="server" CssClass="TextBox"
                                                        onfocus="this.className='TextBox'"></asp:TextBox></div>
                                                <div style="float: left; padding-left: 5px; padding-top: 7px; font-size: 10px;">
                                                    * (Please enter full name)
                                                </div>
                                            </div>
                                            <div class="ItemHiding">
                                                Email Address
                                            </div>
                                            <div>
                                                <div style="float: left;">
                                                    <asp:TextBox Title="Please enter a valid Email ID" ID="txtEmail" runat="server" CssClass="TextBox"
                                                        onfocus="this.className='TextBox'"></asp:TextBox>
                                                </div>
                                                <div style="float: left; padding-left: 5px; padding-top: 7px; font-size: 10px;">
                                                    * (So we can get back to you)
                                                </div>
                                            </div>
                                            <div class="ItemHiding">
                                                City
                                            </div>
                                            <div style="margin-bottom: 10px;">
                                                <asp:TextBox Title="Please enter your City name" ID="txtCity" runat="server" CssClass="TextBox"
                                                    onfocus="this.className='TextBox'"></asp:TextBox>
                                            </div>
                                            <div class="ItemHiding">
                                                Reason for Contacting
                                            </div>
                                            <div style="margin-bottom: 10px;">
                                                <asp:TextBox Title="Please enter reason" ID="txtReason" runat="server" CssClass="TextBox"
                                                    onfocus="this.className='TextBox'"></asp:TextBox>
                                            </div>
                                            <div class="ItemHiding">
                                                Description
                                            </div>
                                            <div style="margin-bottom: 10px;">
                                                <div style="float: left;">
                                                    <asp:TextBox Width="333" Height="110px" Title="Please enter your message here" TextMode="MultiLine"
                                                        ID="txtmessage" runat="server" CssClass="TextBox" onfocus="this.className='TextBox'"></asp:TextBox>
                                                </div>
                                                <div style="float: left; padding-left: 5px; padding-top: 20px; font-size: 10px; width: 320px;">
                                                    * (Please enter the details of your request. A Tastygo customer representative will
                                                    get back to you within 48 hours)
                                                </div>
                                            </div>
                                            <div class="ItemHiding">
                                                <asp:CheckBox ID="chkUsingToday" runat="server" ForeColor="#0a3b5f" Text="This is regarding a Tastygo deal I'm planning to use today." />
                                            </div>
                                            <div style="clear: both;">
                                                <asp:Button CssClass="button big primary" runat="server" ID="btnsubmit" Text="Submit"
                                                    ValidationGroup="CreateUserWizard1" OnClick="btnsubmit_Click" />
                                            </div>
                                            <div style="clear: both;">
                                                <div style="float: left; padding-right: 5px">
                                                    <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="images/error.png" />
                                                </div>
                                                <div style="float: left;">
                                                    <asp:Label ID="lblErrorMessage" runat="server" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div style="clear: both; width: 100%; padding-top: 20px; padding-bottom: 10px; padding-right: 30px;">
                                                <div>
                                                    <div class="onPxStrip">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="ItemHeading">
                                                Question about today's deal?
                                            </div>
                                            <div class="ItemHiding">
                                                <div style="width: 690px;">
                                                    Tastygo reps are on hand to answer your questions. Simply click on "Deal Talk" and
                                                    join in on the discussion. You can also contact the business directly by following
                                                    their link in the "Company Info" section.
                                                </div>
                                            </div>
                                            <div style="clear: both; width: 100%; padding-top: 20px; padding-bottom: 10px; padding-right: 30px;">
                                                <div>
                                                    <div class="onPxStrip">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ItemHeading">
                                                Trouble accessing your Tastygo deals?
                                            </div>
                                            <div class="ItemHiding">
                                                <div style="width: 690px;">
                                                    Simply sign in on the top right corner of the page using your email address and
                                                    password. If you used Facebook Connect to make your purchase, sign in that way.
                                                    Once you are signed in, you will see your name at the top right corner of the page.
                                                    Click on your name and your account menu will drop down. Click on "My Tastygos"
                                                    to access your deals. Any Tastygo that you've purchased will show up here, so come
                                                    back as often as you'd like!
                                                </div>
                                            </div>
                                            <div style="clear: both; width: 100%; padding-top: 20px; padding-bottom: 10px; padding-right: 30px;">
                                                <div>
                                                    <div class="onPxStrip">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ItemHeading">
                                                Having trouble using Tastygo?
                                            </div>
                                            <div class="ItemHiding">
                                                <div style="width: 690px;">
                                                    Email us at <a href="mailto:support@tazzling.com" class="OrangeLink">support@tazzling.com</a>
                                                    or call <font class="OrangeLink">604 295 1777</font> or <font class="OrangeLink">1855
                                                        295 1771</font><br />
                                                    Live Agent Support: Monday – Fri 9:00am to 5:30pm PST Sat-Sun Email Support
                                                    <br />
                                                    <font style="font-style: italic">Pacific Standard Time</font>
                                                </div>
                                            </div>
                                            <div style="clear: both; width: 100%; padding-top: 20px; padding-bottom: 10px; padding-right: 30px;">
                                                <div>
                                                    <div class="onPxStrip">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ItemHeading">
                                                Our Headquarters
                                            </div>
                                            <div class="ItemHiding">
                                                <div style="width: 690px;">
                                                    #20-206 E.6th Ave Vancouver, BC V5T 1J7
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="float: left; padding-left: 10px;">
                    <div style="clear: both; width: 100%;">
                        <div style="clear: both; width: 100%; padding: 0px 0px 0px 0px;">
                            <div class="DetailTheDetailDiv" style="font-size: 13px; font-weight: bold;">
                                <div style="float: left; padding: 10px 0px 0px 15px;">
                                    Our Location
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%;">
                        <img id="Img1" src="Images/OurOfficeMap.png" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
