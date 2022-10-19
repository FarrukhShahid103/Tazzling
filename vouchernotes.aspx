<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="vouchernotes.aspx.cs"
    Inherits="vouchernotes" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PagesBG" style="overflow: hidden; min-height: 400px;">
        <div class="DetailPageTopDiv">
            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                <div style="float: left; font-size: 15px;">
                    Voucher Note
                </div>
            </div>
        </div>
        <div style="height: 15px;">
        </div>
        <div style="background-color: White; overflow: hidden; min-height:400px;">
            <div id="innerDiv">
                <div style="clear: both; padding-left: 40px; padding-top: 15px;">
                    <div style="float: left; vertical-align: top;">
                        <asp:Label ID="Label1" runat="server" Text="Note:"
                            Font-Size="15px" Font-Bold="true"></asp:Label>
                    </div>
                    <div style="float: left; padding-left: 10px;">
                        <asp:TextBox ID="txtSubComment" ToolTip="Add Note" CssClass="TextBox"  Width="600px"
                            Height="100px" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div style="clear: both; padding-top: 10px; padding-left: 90px;">
                    <div style="float: left;">
                        <asp:Button runat="server" ID="btnSave" CssClass="button big primary" Text="Save"
                            OnClick="btnSave_Click" />
                    </div>
                    <div style="float: left; padding-left: 10px;">
                        <asp:Button runat="server" ID="Button1" PostBackUrl="frmBusDealOrderDetailInfo.aspx"
                            CssClass="button big primary" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
