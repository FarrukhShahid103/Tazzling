<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tastypdfdownload.aspx.cs"
    Inherits="tastypdfdownload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="Images/favicon.ico" />
    <title>Untitled Page</title>
    <link rel="stylesheet" href="CSS/fonts.css" type="text/css" charset="utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="pagezone">
        <div class="divTop">
            <div style="float: left; padding-left: 10px; padding-top: 10px; width: 310px;">
                <img id="tgLogo" src="Images/logoForNewsLetter.png" />
            </div>
        </div>
        <div class="divMiddle">
            <div style="clear: both; text-align: center; padding-top:10px;">
                <asp:GridView ID="pageGrid" runat="server" DataKeyNames="detailID" Width="100%" CellPadding="0"
                    CellSpacing="0" AutoGenerateColumns="false" AllowPaging="false" GridLines="None">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Sr." ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'> </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Customer" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Deal Code" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# getDealCode(Eval("dealOrderCode"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="lblIsRedeemed" runat="server" Text='<%# Eval("status")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Redeemed Date" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblRedeemDate" Visible='<%# Convert.ToBoolean(Eval("isRedeemed"))==false ? false :true %>'
                                    runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "redeemedDate").ToString()) %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Security Code" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("voucherSecurityCode") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div id="emptydatarow" align="left">
                            <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                        </div>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="memberAreaTable" />
                    <RowStyle Height="41px" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
        <div class="divBottom">
            <div style="padding-top: 30px; padding-left: 5px; padding-right: 5px;">
                We're here to help! | 1-(855)-295-1771 | Mon-Fri 9:00am – 5:30pm PST | <a href="mailto:support@tazzling.com"
                    style="color: White; text-decoration: none;">support@tazzling.com</a> |
            </div>
        </div>
    </div>
    </form>
</body>
</html>
