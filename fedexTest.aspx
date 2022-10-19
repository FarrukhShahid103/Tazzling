<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fedexTest.aspx.cs" Inherits="fedexTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fedex</title>
</head>
<body style="font-family: Helvetica; font-size: 12px; color: Black;">
    <form id="form1" runat="server">
    <div style="clear: both;">
        <div style="clear: both">
            <div style="float: left; width: 45%;">
                <div>
                    <div style="clear: both; font-size: 16px; font-weight: bold;">
                        Sender Info</div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Sender Name</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderName" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Company Name</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderCompanyName" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Phone Number</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderPhoneNumber" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Address Lane 1</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderAddressLane1" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            City</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderCity" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            State/Province Code</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderStateOrProvinceCode" runat="server" Width="200px" MaxLength="2"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            PostalCode</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderPostalCode" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Country Code</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtSenderCountryCode" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                </div>
            </div>
            <div style="float: left; width: 45%; padding-left: 5%;">
                <div>
                    <div style="clear: both; font-size: 16px; font-weight: bold;">
                        Recipient Info</div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Recipient Name</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientName" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Company Name</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientCompanyName" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Phone Number</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientPhoneNumber" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Address Lane 1</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientAddress" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            City</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientCity" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            State/Province Code</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientState" runat="server" Width="200px" MaxLength="2"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            PostalCode</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientPostalCode" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                    <div style="clear: both; padding-top: 10px;">
                        <div style="float: left; width: 150px;">
                            Country Code</div>
                        <div style="float: left">
                            <asp:TextBox ID="txtRecipientCountryCode" runat="server" Width="200px"></asp:TextBox></div>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both; padding-top:20px;">
        <div style="float: left; width: 45%;">
            <div>
                <div style="clear: both; font-size: 16px; font-weight: bold;">
                    Package Detail</div>
                <div style="clear: both; padding-top: 10px;">
                    <div style="float: left; width: 150px;">
                        Weight in LBs</div>
                    <div style="float: left">
                        <asp:TextBox ID="txtWeight" runat="server" Width="200px"></asp:TextBox></div>
                </div>
                <div style="clear: both; padding-top: 10px;">
                    <div style="float: left; width: 150px;">
                        Length in Inches</div>
                    <div style="float: left">
                        <asp:TextBox ID="txtLength" runat="server" Width="200px"></asp:TextBox></div>
                </div>
                <div style="clear: both; padding-top: 10px;">
                    <div style="float: left; width: 150px;">
                        Width in Inches</div>
                    <div style="float: left">
                        <asp:TextBox ID="txtWidth" runat="server" Width="200px"></asp:TextBox></div>
                </div>
                <div style="clear: both; padding-top: 10px;">
                    <div style="float: left; width: 150px;">
                        Height in Inches</div>
                    <div style="float: left">
                        <asp:TextBox ID="txtHeight" runat="server" Width="200px"></asp:TextBox></div>
                </div>
                <div style="clear: both; padding-top: 10px; padding-left:150px;">
                    <asp:Button ID="btnShippCalculation" runat="server" Text="Process" 
                        onclick="btnShippCalculation_Click" />
                 </div>                
            </div>
            </div>
             <div style="float: left; width: 45%; padding-left: 5%;">
             <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
             </div>
        </div>
    </div>
    </form>
</body>
</html>
