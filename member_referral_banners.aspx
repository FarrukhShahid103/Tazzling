<%@ Page Title="Referral Tools" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="member_referral_banners.aspx.cs" Inherits="member_referral_banners" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <div style="padding-top: 50px; padding-bottom: 10px;">        
        <h3 style="padding-top: 15px; color: #ed9702">
            HTML for your referral code</h3>
        <div class="grayContainer">            
             <div style="padding-bottom: 15px; padding-top: 10px;">
                <h4 style="color: #ed9702">
                    Banner Tracker</h4>
                <p>
                   Copy and paste any one of the following HTML code to other websites, when someone
                has clicked this banner and registered to tazzling.com, you will become the referrer
                of this person.</p>                
            </div>
            <div>
                Text link with<br />
                transparent image<br />
                for tracking</div>
            <asp:GridView runat="server" ID="gridView_Banners" CellPadding="0" CellSpacing="0"
                Width="100%" CssClass="bannersTable" AutoGenerateColumns="false" AllowPaging="false"
                GridLines="None" ShowHeader="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# GetBannerHtmlCode(Convert.ToDouble(Eval("BannerWidth")), Convert.ToDouble(Eval("BannerHeight")), Eval("ImageFile").ToString(), Eval("HtmlCodeTemplate").ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
