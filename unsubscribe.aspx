<%@ Page Title="Unsubscribe NewsLetter" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="unsubscribe.aspx.cs" Inherits="unsubscribe" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
  <div class="PagesBG" style="min-height: 400px;">
  <div style="padding-top: 25px; padding-bottom: 26px; word-spacing: 3px;" class="yellowandbold">
<script type="text/javascript">

 
$(document).ready(function() {
  $('#ctl00_ContentPlaceHolder1_BtnUnsubscribe').click(function() {
 var isValidated = true;
         // validate Email
         var filter = /^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/;       
            if ($("#ctl00_ContentPlaceHolder1_txtUserName").val() == '' || !filter.test($("#ctl00_ContentPlaceHolder1_txtUserName").val())) {
             $("#ctl00_ContentPlaceHolder1_txtUserName").removeClass("TextBoxDeal").addClass("TextBoxDealError");  
             isValidated = false;
                }  
                
                            
                if (isValidated){            
                return;
                }
                else
                {
                return false;
                }

                    });
                 
  });
 



</script>
  
    <table border="0" cellpadding="6" cellspacing="0" width="100%">
        <tr>
            <td style="height: 8px;">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 0px; width:100px;" colspan="2">
                  <asp:Label ID="label2" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Unsubscribe Newsletters"
                Font-Size="29px" Font-Bold="true" ForeColor="#0a3b5f"></asp:Label>
            </td>            
        </tr>
        <tr id="trEmail" runat="server">
            <td class="fieldLoginUsername" width="250px" style="text-align: right; padding-top:20px">
                <asp:Label ID="UserNameLabel" Font-Bold="true" ForeColor="#636363" Font-Size="16px"
                    runat="server" Text="Email You Want to Unsubscribe:"></asp:Label>
            </td>
            <td style="padding-bottom: 10px; padding-left: 10px; padding-top:28px">
                <asp:TextBox ID="txtUserName" onfocus="this.className='TextBoxDeal'" Width="245px" runat="server" CssClass="TextBoxDeal"></asp:TextBox>
              </td>
        </tr>
         <tr id="trButton" runat="server">
           <td></td>
           <td style="padding-left:10px;">
                   <asp:Button runat="server" ID="BtnUnsubscribe" 
                   CssClass="loginsubmitbutton_new" Text="Unsubscribe" 
                   OnClick="BtnUnsubscribe_Click" ></asp:Button>
                   
                   
                   </td>
        </tr>
        <tr>
            <td style="padding-left: 41px; text-align: left; height: 30px; padding-top: 7px;
                padding-bottom: 2px; padding-right: 25px;" colspan="2">
                <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="images/Checked.png" />
                <asp:Label ID="lblMessage" Font-Size="17px" Visible="false" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    
    
    
    
    
   
  
  </div>
  </div>
 
</asp:Content>
