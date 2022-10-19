<%@ Page Title="Tastygo | Business | Features" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="frmBusinessInfo.aspx.cs" Inherits="frmBusinessInfo" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div style="clear: both; padding-top: 20px">
            <div class="DetailPage2ndDiv">
                <div style="float: left; width: 100%; background-color: White; min-height: 450px;
                    border: 1px solid #ACAFB0;">
                    <div style="background-color: White; overflow: hidden;">
                        <div id="tblBusinessInfo" runat="server" style="width: 100%; height: 663px;">
                            <div id="divSignUp" style="width: 100%;">
                                <div>
                                    <div style="clear: both;">
                                        <div class="DetailPageTopDiv">
                                            <div style="clear: both; padding-top: 7px; padding-left: 15px;">
                                                <div style="float: left; font-size: 15px;">
                                                    Get Featured
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-top:15px;">
                                        <table style="width: 100%; height: 63px;" cellpadding="0"
                                            cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 50%; padding-left: 19px;">
                                                    <asp:Label ID="Label1" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                                                        Text="Fill out the following form and we'll contact you shortly"></asp:Label>
                                                </td>
                                                <td style="width: 50%; padding-left: 19px; display:none;">
                                                    <asp:Label ID="Label19" Font-Bold="true" ForeColor="#636363" Font-Size="16px" runat="server"
                                                        Text="Asterik <font style='color:RED;'>(*)</font> represents required field"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                        <div style="width: 980px; padding-top: 20px; clear: both;">
                                            <div style="width: 490px; float: left; min-height: 50px;">
                                            <center>
                                                <div align="center" style="width: 265px; padding-left: 20px;">
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label20" ForeColor="#636363" Font-Size="13px" runat="server" Text="Business Name *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtBusinessName" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                                                ControlToValidate="txtBusinessName" ErrorMessage="Business Name required!" ValidationGroup="businessReg"
                                                                Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RequiredFieldValidator9">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label21" ForeColor="#636363" Font-Size="13px" runat="server" Text="First Name *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtFirstName" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true"
                                                                runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name required!"
                                                                ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator12">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label22" ForeColor="#636363" Font-Size="13px" runat="server" Text="Address 1 *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtAddress1" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator14" SetFocusOnError="true"
                                                                runat="server" ControlToValidate="txtAddress1" ErrorMessage="Address 1 required!"
                                                                ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RequiredFieldValidator14">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label23" ForeColor="#636363" Font-Size="13px" runat="server" Text="State / Province *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:DropDownList ID="ddlProvince" runat="server" CssClass="TextBox" Width="267px"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <cc1:RequiredFieldValidator ID="cvProvince" SetFocusOnError="true" InitialValue="Select One"
                                                                runat="server" ControlToValidate="ddlProvince" ErrorMessage="Province required!"
                                                                ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="Validatorcalloutextender15" TargetControlID="cvProvince"
                                                                runat="server">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label25" ForeColor="#636363" Font-Size="13px" runat="server" Text="Zip Code *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtZip" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator16" SetFocusOnError="true"
                                                                runat="server" ControlToValidate="txtZip" ErrorMessage="Zip Code required!" ValidationGroup="businessReg"
                                                                Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" TargetControlID="RequiredFieldValidator16">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label26" ForeColor="#636363" Font-Size="13px" runat="server" Text="Phone Number *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtPhoneNo" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator17" SetFocusOnError="true"
                                                                runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Phone Number required!"
                                                                ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" TargetControlID="RequiredFieldValidator17">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label27" ForeColor="#636363" Font-Size="13px" runat="server" Text="Pick a Category *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:DropDownList ID="ddlPickCategory" runat="server" CssClass="TextBox" Width="267px"
                                                                AutoPostBack="false">
                                                                <asp:ListItem Selected="True" Text="Select One" Value="Select One"></asp:ListItem>
                                                                <asp:ListItem Text="Food" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Beauty" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Property" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator18" SetFocusOnError="true"
                                                                InitialValue="Select One" runat="server" ControlToValidate="ddlPickCategory"
                                                                ErrorMessage="Category required!" ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" TargetControlID="RequiredFieldValidator18"
                                                                runat="server">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <div class="ItemHiding">
                                                            <asp:Label ID="Label28" ForeColor="#636363" Font-Size="13px" runat="server" Text="Where do you want your tastygo to run? *"></asp:Label>
                                                        </div>
                                                        <div style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtWhereRunTastyGo" Width="263px" runat="server" MaxLength="100"
                                                                CssClass="TextBox"></asp:TextBox>
                                                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator19" SetFocusOnError="true"
                                                                runat="server" ControlToValidate="txtWhereRunTastyGo" ErrorMessage="Where do you want your tastygo to run field is required!"
                                                                ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                            <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" TargetControlID="RequiredFieldValidator19">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                </center>
                                            </div>
                                            <div style="width: 490px;text-align:center; float: right; min-height: 50px;">
                                            <center>
                                                 <div align="center" style="width: 265px;">
                                                <div>
                                                    <div class="ItemHiding">
                                                        <asp:Label ID="Label30" ForeColor="#636363" Font-Size="13px" runat="server" Text="Email Address *"></asp:Label>
                                                    </div>
                                                    <div style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtEmail" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator20" SetFocusOnError="true"
                                                            runat="server" ControlToValidate="txtEmail" ErrorMessage="Email Address required!"
                                                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" TargetControlID="RequiredFieldValidator20">
                                                        </cc2:ValidatorCalloutExtender>
                                                        <cc1:RegularExpressionValidator ID="Regularexpressionvalidator1" SetFocusOnError="true"
                                                            runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email address!"
                                                            ValidationGroup="businessReg" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></cc1:RegularExpressionValidator>
                                                        <cc2:ValidatorCalloutExtender ID="Validatorcalloutextender21" runat="server" TargetControlID="RequiredFieldValidator20">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="ItemHiding">
                                                        <asp:Label ID="Label31" ForeColor="#636363" Font-Size="13px" runat="server" Text="Last Name *"></asp:Label>
                                                    </div>
                                                    <div style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtLastName" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator21" SetFocusOnError="true"
                                                            runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name required!"
                                                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" TargetControlID="RequiredFieldValidator21">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="ItemHiding">
                                                        <asp:Label ID="Label32" ForeColor="#636363" Font-Size="13px" runat="server" Text="Address 2"></asp:Label>
                                                    </div>
                                                    <div style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtAddress2" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="ItemHiding">
                                                        <asp:Label ID="Label33" ForeColor="#636363" Font-Size="13px" runat="server" Text="City *"></asp:Label>
                                                    </div>
                                                    <div style="margin-bottom: 5px;">
                                                        <asp:DropDownList ID="ddlSelectCity" runat="server" Width="267px" CssClass="TextBox">
                                                        </asp:DropDownList>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator22" SetFocusOnError="true"
                                                            InitialValue="Select One" runat="server" ControlToValidate="ddlSelectCity" ErrorMessage="Select City"
                                                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator22"
                                                            runat="server">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="ItemHiding">
                                                        <asp:Label ID="Label11" ForeColor="#636363" Font-Size="13px" runat="server" Text="Website *"></asp:Label>
                                                    </div>
                                                    <div style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtWebsite" Width="263px" runat="server" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                                                        <cc1:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true"
                                                            runat="server" ControlToValidate="txtWebsite" ErrorMessage="Website field is required!"
                                                            ValidationGroup="businessReg" Display="None"></cc1:RequiredFieldValidator>
                                                        <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RequiredFieldValidator10">
                                                        </cc2:ValidatorCalloutExtender>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="ItemHiding">
                                                        <asp:Label ID="Label13" ForeColor="#636363" Font-Size="13px" runat="server" Text="Review Link(s)"></asp:Label><br />
                                                        <asp:Label ID="Label24" Font-Bold="true" ForeColor="#636363" Font-Size="11px" runat="server"
                                                            Text="e.g. Yelp, City, Search etc"></asp:Label>
                                                    </div>
                                                    <div style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtReviewLink" Width="263px" runat="server" MaxLength="500" TextMode="MultiLine"
                                                            Height="126" CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                </div>
                                                
                                                </center>
                                            </div>
                                            <div style="margin-left: 52px;">
                                                <div style="padding-left:69px">
                                                    <div class="ItemHiding">
                                                        <asp:Label ID="Label29" ForeColor="#636363" Font-Size="13px" runat="server" Text="Tell us a little bit about your business"></asp:Label>
                                                    </div>
                                                    <div style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtBusinessInfo" Width="740px" runat="server" MaxLength="100" TextMode="MultiLine"
                                                            Height="92"  CssClass="TextBox"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="margin-bottom: 5px; float: right; margin-right: 115px; padding-top:10px; padding-bottom:10px;">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="businessReg"
                                                        OnClick="btnSubmit_Click" CssClass="button big primary" />
                                                </div>
                                            </div>
                                            <div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="tblEmailInfo" runat="server" visible="false" style="width: 100%; border: 1px solid #373737;
                            background: #F5F5F5; height: 92px;">
                            <div style="float: left; padding-left: 26px; padding-top: 26px;">
                                <asp:Image ID="imgGridMessage" runat="server" ImageUrl="~/images/error.png" />
                                <asp:Label ID="lblErrorMessage" runat="server" Font-Size="17px"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
