<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="Default5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblTranscationNumber" runat="server" Text="Label"></asp:Label>
    </div>
    <div>
        <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
    </div>
    <div>
        <asp:Label ID="lblReferralNumber" runat="server" Text="Label"></asp:Label>
    </div>
    <div>
        <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:Panel ID="pnlHideGrid" runat="server" Visible="false">
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="detailID" Width="70%" CellPadding="0"
            CellSpacing="0" AutoGenerateColumns="false" AllowPaging="false" ShowFooter="false"
            GridLines="None" AllowSorting="true" Visible="true">
            <Columns>
                <asp:TemplateField ItemStyle-Width="8%" HeaderText="Sr." ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'> </asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
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
                        <asp:Label ID="lblBusniessName" runat="server" Text='<%# "# "+ Eval("dealOrderCode")%>'
                            ToolTip='<%# Eval("dealOrderCode")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Security Code" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblSecurityCode" runat="server" Text='<%# Eval("voucherSecurityCode") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Status" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Customer Address" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
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
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
    </form>
</body>
</html>
