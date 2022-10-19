<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="admin_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div>
    <asp:UpdatePanel ID="UPDNL" runat="server" UpdateMode="Always">
    <ContentTemplate>
    <asp:Button ID="BtnPreview" OnClick="BtnPreview_Click" runat="server" Text="Preview" />
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="BtnPreview" />
    </Triggers>
    </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
