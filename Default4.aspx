<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="image_src" href="http://www.tazzling.com/Images/dealfood/31/d06ddc79-0ebf-458b-b52d-54643b55f3c3.jpg?ranid=c9af59f2-a1fe-46ca-8428-868dd6d799bc">
<meta name="description" content="$0 Deal - Entering a Draw for IPAD 16GB GIVEAWAY!!!" />
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" src="JS/jquery-1.4.min.js"></script>

    <script language="javascript" type="text/javascript" src="JS/Functions.js"></script>

    <link rel="stylesheet" type="text/css" href="CSS/jquery-ui.css" />

    <script language="javascript" src="JS/cjs.js" type="text/javascript"></script>

    <script language="javascript" src="JS/general.js" type="text/javascript"></script>

    <script src="JS/jquery.maskedinput-1.2.2.js"></script>

    <script type="text/javascript">
    $(document).ready(function()
{
    //set the originals
    var originalWinWidth = $(window).width();
 
    //set the original font size
    var originalFontSize = 30;
 
    //set the ratio of change for each size change
    var ratioOfChange = 50;
 
    //set the font size using jquery
    $("#container").css("font-size", originalFontSize);
 
    $(window).resize(function()
    {
        //get the width and height as the window resizes
        var winWidth = $(window).width();
 
        //get the difference in width
        var widthDiff = winWidth - originalWinWidth;
 
        //check if the window is larger or smaller than the original
        if(widthDiff > 0)
        {
            //our window is larger than the original so increase font size
            var pixelsToIncrease = Math.round(widthDiff / ratioOfChange);
 
            //calculate the new font size
            var newFontSize = originalFontSize + pixelsToIncrease;
 
            //set new font size
            $("#container").css("font-size", newFontSize);
        }
        else
        {
            //our window is smaller than the original so decrease font size
            var pixelsToDecrease = Math.round(Math.abs(widthDiff) / ratioOfChange);
 
            //calculate the new font size
            var newFontSize = originalFontSize - pixelsToDecrease;
 
            //set the new font size
            $("#container").css("font-size", newFontSize);
        }
    })
});
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <p>
           <asp:Label ID="lblMessage" runat="server" ></asp:Label>
            
        </p>
    </div>
    <div>
        <img src="Images/logo.gif" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    <p>
        <asp:Button ID="btnSendEmail" runat="server" OnClick="btnSendEmail_Click" Text="TestEmail" />
    </p>
    <asp:AdRotator ID="AdRotator1" runat="server" />
    
    
    
 
    
    </form>
</body>
</html>
