<%@ Page Title="Tastygo | Countact Us" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="suggestBusiness.aspx.cs" Inherits="suggestBusiness" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPageTopDiv">
                <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                    <div class="PageTopText" style="float: left;">
                        Suggest your favorite businesses
                    </div>
                </div>
            </div>
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 720px; background-color: White; min-height: 450px;">
                    <asp:UpdatePanel ID="udpnl" runat="server">
                        <ContentTemplate>
                            <div style="width: 100%; margin-left: 15px;">
                                <div style="margin-top: 30px;">
                                    <div style="float: left;">
                                        <div style="clear: both; font-size: 13px; font-weight: bold; padding: 0px 0px 10px 0px;">
                                            Suggest your favorite businesses and we'll try to get them featured on Tastygo!
                                            No need to mention national chains; they are already on our radar. Thanks!
                                        </div>
                                        <div class="ItemHiding">
                                            Owner Name
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtOwnerName" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvEmailRequired" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtOwnerName" ErrorMessage="Owner Name required!" ValidationGroup="CreateUserWizard1"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="vceEmailRequired" runat="server" TargetControlID="rfvEmailRequired">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div class="ItemHiding">
                                            Business Name
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtBusinessName" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtBusinessName" ErrorMessage="Business Name required!" ValidationGroup="CreateUserWizard1"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div class="ItemHiding">
                                            Business Website
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtBusWebsite" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtBusWebsite" ErrorMessage="Business Website required!" ValidationGroup="CreateUserWizard1"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div class="ItemHiding">
                                            City
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtCity" ErrorMessage="City required!" ValidationGroup="CreateUserWizard1"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator4">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div class="ItemHiding">
                                            Type of Business
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtTypeBusiness" runat="server" CssClass="TextBox"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtTypeBusiness" ErrorMessage="Type of Business required!"
                                                ValidationGroup="CreateUserWizard1" Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>
                                        <div class="ItemHiding">
                                            Why should we feature them? Give us your review!
                                        </div>
                                        <div style="margin-bottom: 10px;">
                                            <asp:TextBox Width="333" Height="110px" TextMode="MultiLine" ID="txtFeaturedViews"
                                                runat="server" CssClass="TextBox"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtFeaturedViews" ErrorMessage="Your review required!" ValidationGroup="CreateUserWizard1"
                                                Display="None"></cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator5">
                                            </cc2:ValidatorCalloutExtender>
                                        </div>                                       
                                        <div style="clear: both;">
                                            <asp:Button CssClass="button big primary" runat="server" ID="btnsubmit" Text="Submit" ValidationGroup="CreateUserWizard1"
                                                OnClick="btnsubmit_Click" />
                                        </div>
                                        <div style="clear: both;">
                                            <div style="float: left; padding-right: 5px">
                                                <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="images/error.png" />
                                            </div>
                                            <div style="float: left;">
                                                <asp:Label ID="lblErrorMessage" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                         <div style="clear: both; height:20px;">
                                         </div>                                    
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="float: left; padding-left: 10px;">
                    
                    <div style="clear: both; width: 100%;">
                        <div style="clear: both; width: 100%; padding: 0px 0px 0px 0px;">
                            <div class="DetailTheDetailDiv" style="font-size: 13px; font-weight: bold;">
                                <div style="float: left; padding: 10px 0px 0px 15px;">
                                    Our Location
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; width: 100%;">
                        <img id="Img1" src="Images/OurOfficeMap.png" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
