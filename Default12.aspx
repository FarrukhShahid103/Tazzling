<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default12.aspx.cs" Inherits="Default12" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="JS/jquery-1.4.min.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            setInterval(function () {
                var test = __doPostBack('upNewUpdating', '');
            }, 2000);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="upNewUpdating" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblTest" runat="server"  Text="<%#sTest %>"></asp:Label>
                <asp:Button ID="btnTest" runat="server" Text="Test" OnClick="btnTest_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
