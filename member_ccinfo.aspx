<%@ Page Title="TastyGo | Member | Credit Cards" Language="C#" MasterPageFile="~/tastyGo.master"
    AutoEventWireup="true" CodeFile="member_ccinfo.aspx.cs" Inherits="member_ccinfo" %>

<%@ Register TagName="subMenu" TagPrefix="usrCtrl" Src="~/UserControls/Templates/subMenuMember.ascx" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/jquery-ui.css" type="text/css" rel="Stylesheet" />
    
    <script type="text/javascript" src="JS/jquery-ui.min.js"></script>

    <script type="text/javascript" src="JS/jquery.blockUI.js"></script>

    <!-- fix for 1.1em default font size in Jquery UI -->
    <style type="text/css">
        .ui-widget
        {
            font-size: 11px !important;
        }
        .ui-state-error-text
        {
            margin-left: 10px;
        }
    </style>

    <script type="text/javascript">
		$(document).ready(function() {
			$("#divEditCustomer").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 'auto',
				width: 'auto',
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divEditCustomerDlgContainer");
				},
			});
		});


		function closeDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divEditCustomer").dialog('close');
		}
		
		function openDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = 400;
			
			
			$("#divEditCustomer").dialog("option", "title", title);
			$("#divEditCustomer").dialog("option", "position", [left, top]);
			
			$("#divEditCustomer").dialog('open');
		}



		function openDialogAndBlock(title, linkID) {
		   // alert("Title: "+ title+" linkID: "+linkID);
			openDialog(title, linkID);

			//block it to clean out the data
			$("#divEditCustomer").block({
				message: '<img src="~/images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}

		
		function unblockDialog() {
			$("#divEditCustomer").unblock();
		}

		function onTest() {
			$("#divEditCustomer").block({
				message: '<h1>Processing</h1>',
				css: { border: '3px solid #a00' },
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
		
		
		       function luhn(oSrc, args) {                
            if (args.Value != "") {
                var num = args.Value;
                num = (num + '').replace(/\D+/g, '').split('').reverse();
                if (!num.length) {
                    args.IsValid = false;
                    return;
                }
                var total = 0, i;
                for (i = 0; i < num.length; i++) {
                    num[i] = parseInt(num[i])
                    total += i % 2 ? 2 * num[i] - (num[i] > 4 ? 9 : 0) : num[i];
                }
                if ((total % 10) == 0 || (total % 10) == 5) {
                    args.IsValid = true;
                    return;
                }
                else {
                    args.IsValid = false;
                    return;
                }
            }
        }
		 
		   function isPostCodeLocal(oSrc, args) {

            // checks cdn codes only          

            entry = args.Value;
            //alert(entry);


            strlen = entry.length;
            if (strlen != 7) {
                args.IsValid = false;
                return;
            }
            entry = entry.toUpperCase(); //in case of lowercase
            //Check for legal characters,index starts at zero
            s1 = 'ABCEGHJKLMNPRSTVXY'; s2 = s1 + 'WZ'; d3 = '0123456789';


            if (s1.indexOf(entry.charAt(0)) < 0) {
                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(1)) < 0) {
                args.IsValid = false;
                return;
            }
            if (s2.indexOf(entry.charAt(2)) < 0) {
                args.IsValid = false;
                return;
            }
            if (entry.charAt(3) != '-') {

                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(4)) < 0) {
                args.IsValid = false;
                return;
            }
            if (s2.indexOf(entry.charAt(5)) < 0) {
                args.IsValid = false;
                return;
            }
            if (d3.indexOf(entry.charAt(6)) < 0) {
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
            return;
        } 

    </script>

    <usrCtrl:subMenu ID="subMenu1" runat="server" />
    <div style="width: auto; padding-bottom: 10px;" align="center">
        <div style="padding-top: 50px; width: 820px; padding-bottom: 30px;">
            <div id="divEditCustomerDlgContainer">
                <div id="divEditCustomer" style="display: none">
                    <asp:UpdatePanel ID="upnlEditCustomer" runat="server">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="phrEditCustomer" runat="server">
                                <table cellpadding="3" cellspacing="1" width="450px">
                                    <tr>
                                        <td>
                                            *Billing Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFirstName" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="vtxtFirstName" runat="server" 
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*"
                                                ControlToValidate="txtFirstName" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            *Billing Address:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBillingAddress" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="vtxtLastName" runat="server" 
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*"
                                                ControlToValidate="txtBillingAddress" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            *Card Number:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txCardNumner" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvCCnumber" runat="server" 
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*"
                                                ControlToValidate="txCardNumner" />
                                            <asp:CustomValidator ID="cvCreditCard" runat="server" ControlToValidate="txCardNumner"
                                                ValidateEmptyText="true" ClientValidationFunction="luhn" SetFocusOnError="true"
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*">                                       
                                            </asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            *Security Code:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCCVNumber" Columns="20" MaxLength="20" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*"
                                                ControlToValidate="txtCCVNumber" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Expiration Date:
                                        </td>
                                        <td>
                                            <div>
                                                <div style="float: left;">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" Height="20">
                                                        <asp:ListItem Value="01" Selected="True">01</asp:ListItem>
                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                        <asp:ListItem Value="09">09</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div style="float: left; padding-left: 10px;">
                                                    <asp:DropDownList ID="ddlYear" runat="server" Height="20">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            *City:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCity" Columns="40" MaxLength="50" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*"
                                                ControlToValidate="txtCity" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            State:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlState" runat="server" Height="20">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            *Postal Code:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPostalCode" Columns="20" MaxLength="20" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                CssClass="ui-state-error-text" Display="Dynamic" ValidationGroup="CC" ErrorMessage="*"
                                                ControlToValidate="txtPostalCode" />
                                            <cc1:CustomValidator ID="revPostalCode" SetFocusOnError="true" runat="server" ControlToValidate="txtPostalCode"
                                                ClientValidationFunction="isPostCodeLocal" ErrorMessage="*" ValidationGroup="CC"
                                                Display="Dynamic"></cc1:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" ValidationGroup="CC" Text="Save"
                                                runat="server" CausesValidation="true" />
                                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" OnClientClick="closeDialog()"
                                                CausesValidation="false" Text="Cancel" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="height15 clear" align="left" style="text-align: left; font-family: Arial;
                        font-size: 16px; padding-bottom: 10px;">
                        <asp:Image ID="imgGridMessage" runat="server" align="texttop" Visible="false" ImageUrl="images/error.png" />
                        <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                    <div style="float: left; padding-bottom: 20px;">
                        <asp:LinkButton ID="btnAddCustomer" Text="Add Credit Card" Font-Bold="true" Font-Size="24px"
                            Font-Names="Arial" runat="server" OnClientClick="openDialogAndBlock('Add New Credit Card', 'ctl00_ContentPlaceHolder1_btnAddCustomer')"
                            CausesValidation="false" OnClick="btnAddCustomer_Click"></asp:LinkButton>
                    </div>
                    <div style="clear: both;">
                        <table cellpadding="0" cellspacing="0" class="memberAreaTable">
                            <tr>
                                <td class="cellFirst1" style="padding-left: 200px;">
                                    <b>
                                        <asp:Label ID="Label4" runat="server" Text="Creadit Cards"></asp:Label></b>
                                </td>
                                <td class="cellSecond1" style="padding-left: 40px;">
                                    <b>
                                        <asp:Label ID="Label1" runat="server" Text="Action"></asp:Label></b>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: left; border: solid 1px #cfcfcf; border-top: none; width: 818px;">
                        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="10"
                            CellSpacing="0" OnRowDataBound="gvCustomer_RowDataBound" GridLines="None" Width="810px"
                            ShowHeader="false" OnRowCommand="gvCustomers_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Your Credit Cards">
                                    <ItemTemplate>
                                        <div style="font-family: Arial; font-size: 21px; width: 425px; text-align: left;
                                            padding-left: 20px;">
                                            <%# GetCardExplain(Eval("ccInfoNumber"))%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div style="font-family: Arial; font-size: 21px; text-align: left; padding-left: 40px;">
                                            <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="EditCustomer" CausesValidation="false"
                                                CommandArgument='<%#Eval("ccInfoID")%>' runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" Text="Delete" OnClientClick='return confirm("Are you sure you want to delete this info?");'
                                                CommandName="DeleteCustomer" CausesValidation="false" CommandArgument='<%#Eval("ccInfoID")%>'
                                                runat="server"></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:LinkButton ID="btnRefreshGrid" CausesValidation="false" OnClick="btnRefreshGrid_Click"
                        Style="display: none" runat="server"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
