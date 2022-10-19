<%@ Page Title="Withdraw Request" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="member_withdraw.aspx.cs" Inherits="member_withdraw" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/UserControls/Templates/withdraw.ascx" TagPrefix="RedSgnal" TagName="withDraw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div>
        <div class="DetailPage2ndDiv">
            <div style="width: 980px; float: left;">
                <div>
                    <div style="overflow: hidden;">
                        <usrCtrl:subMenu ID="subMenu1" runat="server" />
                    </div>
                    <div style="clear: both; padding-top: 10px; margin-bottom: 10px;">
                        <div class="DetailPageTopDiv">
                            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                                <div id="PageTitle" class="PageTopText" style="float: left;">
                                    My Withdraw Funds
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="background-color: White; min-height: 450px;">
                    <div style="padding-top: 40px;">
                        <div class="MemberArea_PageHeading" style="padding-left: 15px;">
                            Withdraw Funds</div>
                        <div style="font-size: 15px; padding-left: 15px; padding-top: 10px;">
                            Withdraw With Cheque
                        </div>
                        
                    </div>
                    <div style="clear: both;">
                        <RedSgnal:withDraw ID="withDraw1" runat="server" />
                    </div>
                </div>
            </div>
        </div>
      
    </div>   
</asp:Content>
