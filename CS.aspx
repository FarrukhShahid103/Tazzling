<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="CS.aspx.cs" Inherits="CS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<link href="CSS/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
<script src="JS/jquery-1.4.min.js" type="text/javascript"></script>
<script src="JS/jquery.autocomplete.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {
        $("#<%=txtSearch.ClientID%>").autocomplete('Search_CS.ashx');
    });       
</script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtSearch" Width="400px" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
