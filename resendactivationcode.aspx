<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resendactivationcode.aspx.cs" Inherits="resendactivationcode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="image_src" href="http://www.tazzling.com/Images/dealfood/31/d06ddc79-0ebf-458b-b52d-54643b55f3c3.jpg?ranid=c9af59f2-a1fe-46ca-8428-868dd6d799bc">
<meta name="description" content="$0 Deal - Entering a Draw for IPAD 16GB GIVEAWAY!!!" />
<head runat="server">
    <title>Emails send to inactive accounts</title>
    <link href="CSS/takeout_style.css" rel="stylesheet" type="text/css" />
        
</head>
<body style="background-color:#8AD3FE;">
  <div class="PagesBG" style="min-height: 400px; margin-left:25px;">
   
    
            <div style="padding-top: 25px; padding-bottom: 26px;  word-spacing: 3px;" class="yellowandbold">
                <asp:Label ID="label1" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Emails send to inactive accounts"
                    Font-Size="29px" Font-Bold="true" ForeColor="#0A3B5F" Font-Underline="False"></asp:Label>
                    </div>
            <div style="color:White;">
                <asp:Image ID="imgGridMessage" runat="server" ImageUrl="images/Checked.png" />
                <asp:Label ID="lblMessage" Font-Size="17px"  runat="server"></asp:Label>
            </div>
    
    </div>
</body>
</html>
