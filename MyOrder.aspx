<%@ Page Title="My Order" Language="C#" MasterPageFile="~/tastyGo.master" AutoEventWireup="true"
    CodeFile="MyOrder.aspx.cs" Inherits="MyOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Src="~/UserControls/Templates/Total-Funds.ascx" TagPrefix="RedSgnal"
    TagName="TotalFunds" %>
<%@ Register Src="~/UserControls/Templates/Total-Referral.ascx" TagPrefix="RedSgnal"
    TagName="TotalComission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="CSS/coda-slider-2.0.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="CSS/gh-buttons.css" type="text/css" media="screen" />
    <script type="text/javascript" src="JS/jquery.coda-slider-2.0.js"></script>
    <link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <link href="CSS/confirm.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <script src="JS/jquery.simplemodal.js" type="text/javascript"></script>
    <script src="JS/confirm.js" type="text/javascript"></script>
    <style type="text/css">
        .clear
        {
            clear: both;
        }
        
        textarea#txtUserNotes
        {
            width: 350px;
            height: 130px;
            border: 3px solid #cccccc;
            padding: 5px;
            font-family: Tahoma, sans-serif;
        }
        .VAlign
        {
            vertical-align: top;
        }
        
        .GridBottomSaperate
        {
            clear: both;
            background-color: #DEDEDE;
            min-height: 10px;
            vertical-align: bottom;
        }
        
        .PageHeading
        {
            float: left;
            width: 535px;
            font-size: 24px;
            font-weight: bold;
            color: #363636;
            border: 0px;
            padding-top: 10px;
        }
        
        .gotQuestion
        {
            float: left;
            width: 150px;
            font-size: 18px;
            font-weight: bold;
            margin-right: 10px;
            padding-top: 13px;
        }
        
        .EmailPhone
        {
            float: left;
            font-size: 15px;
            padding-top: 10px;
        }
        
        .BeforeGridDiv
        {
            clear: both;
            overflow: hidden; /*float:left;*/
            background-color: White;
        }
        
        .GridFirstColumn
        {
            float: left;
            min-width: 135px;
            min-height: 67px;
            padding: 0px 15px;
        }
        
        .GridSecondColumn
        {
            float: left;
            overflow: hidden;
            min-width: 350px;
        }
        
        .SecondColumnHeading
        {
            font-size: 18px;
            font-weight: bold;
            width: 325px;
            padding: 5px 0px;
            float: left;
            border-bottom: 1px solid #D1D6DC;
        }
        
        .ItemsNumber
        {
            float: left;
            font-size: 12px;
            padding-right: 5px;
            color: #ABABAB;
            text-transform: uppercase;
        }
        
        .ItemsNumberVal
        {
            float: left;
            font-size: 12px;
            padding: 0px 15px;
        }
        
        .ContactUs
        {
            float: left;
            font-size: 12px;
            padding: 0px 5px;
            text-decoration: underline;
        }
        
        .ButtonOuterDiv
        {
            clear: both;
            float: left;
            padding-top: 7px;
            overflow: hidden;
        }
        
        .ColumnButton
        {
            min-height: 20px;
            min-width: 94px;
            background-color: #E4E4E4;
            float: left;
            overflow: hidden;
            font-size: 10px;
            color: #797979;
            text-align: center;
            border-radius: 15px;
        }
        
        .ColumnButton a
        {
            color: #797979;
        }
        
        .GridThirdColumn
        {
            float: left;
            overflow: hidden;
            width: 220px;
        }
        
        .ThirdColumnDetail
        {
            font-size: 11px;
            float: left;
            padding: 2px 0px;
            color: #4F4F4F;
        }
        
        .FourthColumnTextArea
        {
            float: left;
            padding: 5px;
            border: 1px solid #D1D6DC;
            height: 56px;
            width: 216px;
        }
        
        .TextAreaOfFourthColumn
        {
            border-style: none;
            height: 56px;
            width: 216px;
        }
        
        .FourthColumnButton
        {
        }
    </style>
    <div>
        <div class="DetailPage2ndDiv">
            <div style="width: 100%; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                </div>
                <div style="min-height: 450px;">
                    <div style="padding-top: 20px; overflow: hidden; height: 55px;">
                        <div style="padding-left: 15px;">
                            <div class="PageHeading">
                                My <span style="color: #DD0016;">Orders</span>
                            </div>
                            <div class="gotQuestion">
                                Got Questions?
                            </div>
                            <div class="EmailPhone">
                                <img src="Images/EmailIcon.png" alt="" style="padding: 0px 10px;" /><a href="mailto:info@tazzling.com">Email to</a></div>
                            <div class="EmailPhone">
                                <img src="Images/PhoneIcon.png" alt="" style="padding: 0px 10px 0px 15px;" />1-855-295-1771</div>
                        </div>
                    </div>
                    <div class="BeforeGridDiv">
                        <asp:GridView ID="gvOrderItems" runat="server" AutoGenerateColumns="false" GridLines="None"
                            DataKeyNames="dOrderID" AllowPaging="true" CellPadding="0" CellSpacing="0" PageSize="5"
                            ShowHeader="false" OnPageIndexChanging="gvOrderItems_PageIndexChanging" OnRowDataBound="gvOrderItems_RowDataBound"
                            OnRowEditing="gvOrderItems_Edit" OnRowCommand="gvOrderItems_Command">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="VAlign">
                                    <ItemTemplate>
                                        <div style="padding-bottom: 10px; padding-top: 10px; overflow: hidden; width: 100%;
                                            border-bottom: 10px solid #F4F4F4;">
                                            <div class="GridFirstColumn">
                                                <asp:Label ID="lblId" runat="server" Text='<%#Eval("dOrderID")%>' Visible="false"></asp:Label>
                                                <img src='<%# imagePath(Eval("images"),Eval("restaurantId")) %>' height="93px" width="130px"
                                                    alt="" />
                                            </div>
                                            <div class="GridSecondColumn">
                                                <div class="SecondColumnHeading" title='<%# Convert.ToString(Eval("title")).ToString().Trim()%>'>
                                                    <%# Eval("title").ToString().Trim().Length > 30 ? Convert.ToString(Eval("title")).ToString().Trim().Substring(0, 27) + "..." : Convert.ToString(Eval("title")).ToString().Trim()%>
                                                </div>
                                                <div style="clear: both; float: left; padding: 5px 5px 0px 0px; overflow: hidden;">
                                                    <div class="ItemsNumber">
                                                        item number</div>
                                                    <div class="ItemsNumberVal">
                                                        <asp:Label ID="lblDetailID" runat="server" Text='' Visible="false"></asp:Label>
                                                        <%# getDealCode(Eval("dealOrderCode"),Eval("status"),Eval("displayIt"))%>
                                                    </div>
                                                    <div class="ContactUs"><a href="contact-us.aspx">
                                                        Contact Us</a></div>
                                                </div>
                                                <div class="ButtonOuterDiv">
                                                    <div style="padding-right: 5px; float: left; overflow: hidden;">
                                                        <div class="ColumnButton">
                                                            <asp:HyperLink ID="hlViewDetail" runat="server" Text="View Detail" Target="_blank"
                                                                Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>' NavigateUrl='<%# "~/tazzlingVoucher.aspx?oid="+(Eval("dOrderID") + "&did=" + Eval("detailID"))%>'></asp:HyperLink>
                                                        </div>
                                                    </div>
                                                    <div style="padding: 0px 5px; float: left; overflow: hidden;">
                                                        <div class="ColumnButton">
                                                            <asp:UpdatePanel ID="upDownloadFile" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="lnSavePDF" runat="server" Text="Save as PDF" ForeColor="#797979"
                                                                        CommandName="download" Visible='<%# getDetailStatus(Eval("status"),Eval("displayIt"))%>'
                                                                        CommandArgument='<%# (Eval("detailID") + "," + Eval("dOrderID")+","+Eval("dealOrderCode"))%>'></asp:LinkButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="lnSavePDF" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    <div style="padding: 0px 5px; float: left; overflow: hidden;">
                                                        <div class="ColumnButton">
                                                            <asp:LinkButton ID="lnOrderReceipt" runat="server" Text="Order Reciept" ForeColor="#797979"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="GridThirdColumn">
                                                <div class="ThirdColumnDetail clear">
                                                    Shipped to:
                                                    <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></div>
                                                <div class="ThirdColumnDetail clear">
                                                    SHIPPING TYPE:
                                                    <asp:Label ID="lblShipingType" runat="server" Text="Standard"></asp:Label>
                                                </div>
                                                <div class="ThirdColumnDetail clear">
                                                    Tracking No: <span style="text-decoration: underline; text-transform: uppercase;">
                                                        <%#Eval("trackingNumber") %>
                                                    </span>
                                                </div>
                                            </div>
                                            <div style="float: left; overflow: hidden; width: 245px;">
                                                <div class="FourthColumnTextArea">
                                                    <asp:TextBox ID="txtTextArea" runat="server" TextMode="MultiLine" CssClass="TextAreaOfFourthColumn"
                                                        Text='<%#Eval("customerNote")%>'></asp:TextBox>
                                                </div>
                                                <div style="padding: 5px 0px; float: left; overflow: hidden; clear: both;">
                                                    <div class="ColumnButton">
                                                        <div style="float: left;">
                                                            <img src="Images/SaveIcon.png" alt="" style="padding: 5px 7px 5px 10px;" />
                                                        </div>
                                                        <div style="float: left; padding-top: 2px;">
                                                            <asp:LinkButton ID="lnSaveNote" runat="server" Text="Save Note" CommandName="Edit"
                                                                ForeColor="#797979"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="emptydatarow">
                                    <asp:Label ID="lblNoData" runat="server" Text="There is no deal codes to be shown."></asp:Label>
                                </div>
                            </EmptyDataTemplate>
                            <PagerTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr runat="server" id="topPager">
                                        <td>
                                            <div style="padding-top: 35px;">
                                                <div>
                                                    <div>
                                                        <div>
                                                            <div class="button-group">
                                                                <asp:LinkButton ID="lnkTopPrev" CssClass="button icon arrowleft   green" Enabled='<%# displayPrevious %>'
                                                                    CommandName="Page" CommandArgument="Prev" runat="server" Text="Previous"></asp:LinkButton>
                                                                <asp:Repeater ID="rptrPage" runat="server">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton Style="min-width: 0px !important;" ID="lnkPage" CssClass='<%# Convert.ToInt32((gvOrderItems.PageIndex + 1)) == Convert.ToInt32((Eval("pageNo"))) ? "button   CityNameButton" : "button  " %>'
                                                                            OnClick="lnkPage_Click" Font-Underline="false" CommandName="Page" Enabled='<%# GetStatus(Eval("pageNo").ToString()) %>'
                                                                            CommandArgument='<%# Eval("pageNo") %>' runat="server" Text='<%# Eval("pageNo") %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                                <asp:LinkButton Style="width: 35px;" ID="lnkTopNext" runat="server" Enabled='<%# displayNext %>'
                                                                    CommandName="Page" CommandArgument="Next" CssClass="button icon arrowright   green"
                                                                    Text="Next"></asp:LinkButton>
                                                            </div>
                                                            <div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div class="backtotop">
                                    <a href="#">
                                        <asp:Label ID="lblbackToTop" runat="server" Text="Back To Top"></asp:Label><img src="images/totop.gif" /></a>
                                </div>
                            </PagerTemplate>
                            <PagerSettings PageButtonCount="10" Position="Bottom" />
                            <PagerStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        <div style="float: left;">
                        </div>
                        <div style="float: left;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
