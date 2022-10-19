<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IPBlock.aspx.cs" Inherits="IPBlock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your IP is Blocked</title>
    <style type="text/css">
    div.centered{
    display:block;
    position:absolute;
    left:35%;
    width:350px;
  }

</style>
</head>
<body style="background-color:Black;">
    <form id="form1" runat="server">
    <div class="centered">
    <div>
    <img src="Images/hacker.gif" />
    </div>
    <div style="font-size:18px; color:White;">
    <center><asp:Label ID="lblBlockMessage" runat="server" ForeColor="White"></asp:Label></center>
    </div>
    
    </div>
    </form>
</body>
</html>
