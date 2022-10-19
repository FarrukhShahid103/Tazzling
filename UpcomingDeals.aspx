<%@ Page Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true" CodeFile="UpcomingDeals.aspx.cs"
    Inherits="UpcomingDeals" Title="Upcoming Deals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/Calander.css" rel="stylesheet" type="text/css" />
    <div id="tastygoWrapper" class='wrapperMesh'>
        <div id="contentWrapper">
            <div class="tastygoBorderSpace">
            </div>
            <div class="upcommingWrap upcommingMain">
                <h2>
                    <div style="color: #5e636c; font-size: 28.6px; float:left; margin-right:10px;  font-family: Helvetica;">
                        Upcoming</div>
                        <div style="color: #ff42e7; float:left; font-size: 28.6px;  font-family: Helvetica;">Sales
                        </div> </h2>
                <div class="upcomingSalesFstBlk" style="background-color: white; border-bottom: 10px solid white;">
                    <div class="fstDySale">
                        <asp:Literal ID="ltltomorow" runat="server"></asp:Literal>
                    </div>
                    <div class="secDySale">
                        <asp:Literal ID="ltlsecDySale" runat="server"></asp:Literal>
                    </div>
                    <div class="trdDySale">
                        <asp:Literal ID="ltltrdDySale" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="upcomingSalesSecBlk" style="background-color: white; border-bottom: 10px solid white;">
                    <div class="fstDySale">
                        <asp:Literal ID="ltlSecBlkfrst" runat="server"></asp:Literal>
                    </div>
                    <div class="secDySale">
                        <asp:Literal ID="ltlSecBlkscnd" runat="server"></asp:Literal>
                    </div>
                    <div class="trdDySale">
                        <asp:Literal ID="ltlSecBlkthrd" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="upcomingSalesTrdBlk" style="padding-bottom: 140px;background-color: white;">
                    <div class="fstDySale">
                        <asp:Literal ID="ltltrdBlk" runat="server"></asp:Literal>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
        <div id="fb-root">
        </div>
    </div>
</asp:Content>
