<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rssReadTest.aspx.cs" Inherits="rssReadTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:DataList ID="dlRSS" runat="server" Width="100%">

        <ItemTemplate>      

                    <div class="RSSTitle"><asp:HyperLink ID="TitleLink" runat="server" Text='<%# Eval("title") %>' NavigateUrl='<%# Eval("link") %>'/></div>

                    <div class="RSSSubtitle"><asp:Label ID="SubtitleLabel" runat="server" Text='<%# Eval("description") %>' /></div>

                    <div class="RSSInfo">

                        posted on <asp:Label ID="DateRSSedLabel" runat="server" Text='<%# Eval("pubDate", "{0:d} @ {0:t}") %>' />

                    </div>         

        </ItemTemplate>

    </asp:DataList>
    </div>
    </form>
</body>
</html>
