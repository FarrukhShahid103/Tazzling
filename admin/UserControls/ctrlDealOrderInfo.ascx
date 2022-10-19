<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlDealOrderInfo.ascx.cs"
    Inherits="admin_UserControls_ctrlDealOrderInfo" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!-- fix for 1.1em default font size in Jquery UI -->

<script type="text/javascript" language="javascript">
var UID;
    function clearFields() {
        document.getElementById('ctl00_ContentPlaceHolder1_ctrlDealOrderInfo1_txtUserName').value = '';        
        document.getElementById('ctl00_ContentPlaceHolder1_ctrlDealOrderInfo1_txtSearchDealName').value = ''; 
        document.getElementById('ctl00_ContentPlaceHolder1_ctrlDealOrderInfo1_txtVoucherNumber').value = ''; 
                               
        return false;
    }
    
    
     function checkPayBackStatus(comissionID,gainedId) {    
         if(comissionID>0)
         {
           return confirm("This order give affiliate comission. Are you sure you want to roll back payment?");
         }        
         else if(gainedId>0)
         {
            return confirm("This order give tasty credits. Are you sure you want to roll back payment?");           
         }        
         else 
         {
           return confirm("Are you sure you want to roll back payment?");
         }
    }
    
    function SaveUserNotes()
    {
      var Notes =  escape($("#txtUserNotes").val());
      var OrderID =  $("#dOrderIDHidden").val();
      $.ajax({
      type: "POST",
      url: "../getStateLocalTime.aspx?UID=" + UID + "&Notes=" + Notes + "&OrderID=" + OrderID,
      contentType: "application/json; charset=utf-8",
      async: true,
      cache: false,
      success: function(msg) {
     if (msg == "true") {
           return true;
          }
          else
          {
           return false;
          }
       }
      });

    }
function RunPopup(ButtonID,CompleteData,IsPayBack,PayBackButtonID)
{
    var SompleteString = CompleteData.split('|');
    
    
    
    var psi = SompleteString[0] + " \n";
    var DealTitle = SompleteString[1] + " \n";
    var Reason = SompleteString[2]+ " \n";
    var Userid = SompleteString[3]; 
    $("#dOrderIDHidden").val(SompleteString[4]);
    UID =   Userid;  
    $("#txtUserNotes").val(psi + DealTitle + Reason);
    RunPopup2(function () {
       SaveUserNotes();
            if(IsPayBack=="false")
            {
                $("#" + ButtonID).click();
            }
            else
            {
                $("#" + PayBackButtonID).click();
            }
	});
	return false;
}
function RunPopup2(callback) {
	$('#confirm').modal({
		closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
		position: ["20%",],
		overlayId: 'confirm-overlay',
		containerId: 'confirm-container', 
		onShow: function (dialog) {
			var modal = this; 
			// if the user clicks "yes"
			$('.yes', dialog.data[0]).click(function () {
				// call the callback
				if ($.isFunction(callback)) {
					callback.apply();
				}
				// close the dialog
				modal.close(); // or $.modal.close();
			});
		}
	});
}	
</script>

<script type="text/javascript">
                    
                    
                                    function ConfirmAlert(Type,Message)
                                    {
                                    MyAlert(Type,Message);
                                    $('#BoxConfirmBtnOk').click(function(event)
                                    {
                                   // return true;
                                    
//                                    StartLoading();
//                                    $.ajax({
//                                    type: "POST",
//                                    url: "AjaxCalls.aspx?BuyMoreCards=BuyMorebusinessCard",
//                                    contentType: "application/json; charset=utf-8",
//                                    async: true,
//                                    cache: false,
//                                    success: function(msg) {
//                                    if (msg == "Success") {
//                                    MyAlert('success', 'Please enter data for new business card.');
//                                    StopLoading();
//                                    $("#ctl00_ContentPlaceHolder1_DivBusinessCardData").hide('slow');
//                                    $("#ctl00_ContentPlaceHolder1_BtnBuyMore").show('slow');
//                                     }
                                    // }
                                     //});
                                     //$('#ctl00_ContentPlaceHolder1_BtnbuyMoreBusinessCards').click();
                                           
                                            
                                    });
                                   $('#fancybox-close').click(function(event)
                                     {
                                     alert('Clicked');
                                     return false;
                                     });
                                   }
                                   
                                   
                                    $(document).ready(function(){
                                        $('#fancybox-close').click(function(event)
                                         {
                                         alert('Clicked');
                                         return false;
                                         });
                                     
                                     });
</script>

<style type="text/css">
    textarea#txtUserNotes
    {
        width: 600px;
        height: 120px;
        border: 3px solid #cccccc;
        padding: 5px;
        font-family: Tahoma, sans-serif;
    }
</style>
<div>
<input type="hidden" id="dOrderIDHidden" />
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="searchBig">
                    <div style="clear: both;">
                        <div class="heading" style="float: left;">
                            <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
                        </div>
                        <div style="float: left; padding-left: 5px;">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading" style="float: left; padding-left: 20px;">
                            <asp:Label ID="lblFirstNameSearch" runat="server" Text="Deal Name"></asp:Label>
                        </div>
                        <div style="float: left; padding-left: 5px;">
                            <asp:TextBox ID="txtSearchDealName" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>
                        <div class="heading" style="float: left; padding-left: 20px;">
                            <asp:Label ID="Label2" runat="server" Text="Voucher Number"></asp:Label>
                        </div>
                        <div style="float: left; padding-left: 5px;">
                            <asp:TextBox ID="txtVoucherNumber" runat="server" CssClass="txtSearch"></asp:TextBox>
                        </div>                        
                    </div>
                    <div style="clear: both; padding-top: 7px;">
                        <div class="heading" style="float: left;">
                            <asp:Label ID="lblLastNameSearch" runat="server" Text="Order Status"></asp:Label>
                        </div>
                        <div style="float: left; padding-left: 5px;">
                            <asp:DropDownList ID="ddlSrchOrderStatus" Width="126px" runat="server">
                                <asp:ListItem Text="All" Selected="True" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Successful" Value="Successful"></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                <asp:ListItem Text="Declined" Value="Declined"></asp:ListItem>
                                <asp:ListItem Text="Refunded" Value="Refunded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; padding-left: 20px;">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                                OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                    ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                    TabIndex="2" />
                            <asp:ImageButton ID="btnShowAll" runat="server" ImageUrl="~/admin/images/btnShowAll.gif"
                                OnClick="btnShowAll_Click" />&nbsp;
                        </div>
                         <div style="float: left; padding-left: 10px; padding-top:5px;">
                            <b>Note:</b><span style="font-size:12px; font-weight:normal;"> Please enter complete voucher number no "#" sing required.</span>
                        </div>
                    </div>
                </div>
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/admin/images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:GridView ID="pageGrid" runat="server" DataKeyNames="dOrderID" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                        AllowSorting="false" OnRowEditing="pageGrid_RowEditing" OnRowCommand="pageGrid_RowCommand"
                        OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" OnRowDataBound="pageGrid_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div style="display: none">
                                        <asp:Label ID="lblDealID" runat="server" Text='<% # Eval("dealId") %>' Visible="true"></asp:Label>
                                        <asp:HiddenField ID="hfOrderTotal" runat="server" Value='<% # Eval("totalAmt") %>'>
                                        </asp:HiddenField>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle Width="2%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Psigate">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <a id="hlNewDetail" target="_blank" href='<%# "OrderDetails.aspx?OID="+ Eval("orderNo") %>'
                                        title="View Deal Order Detail">
                                        <asp:Label ID="lblpsigateTransaction" runat="server" Text='<%#Convert.ToString(Eval("psgTranNo")).Trim()==""?"View Detail":Eval("psgTranNo") %>'></asp:Label>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:Label ID="lblnsername" ForeColor="White" runat="server" Text="Username"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <a id="hlUserEdit" target="_blank" href='<%#"userManagement.aspx?uid="+Eval("userId") %>'
                                            title="View User Profile">
                                            <asp:Label ID="lblsUsername" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "username").ToString()) %>'
                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "username").ToString()) %>'></asp:Label>
                                        </a>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="11%" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDealNameHead" ForeColor="White" runat="server" Text="Deal"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lblDealName" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "DealName").ToString().Length > 21 ? DataBinder.Eval (Container.DataItem, "DealName").ToString().Substring(0,19) + "..." :DataBinder.Eval (Container.DataItem, "DealName").ToString()) %>'
                                            ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "DealName").ToString()) %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="12%" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" 
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:Label ID="lblQtyHead" ForeColor="White" runat="server" Text="Voucher #"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lblQty" Font-Bold="true" Text='<% # getDealCode(Eval("dealOrderCode")) %>' runat="server"></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Created Date"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lblcreationDateText" Text='<% # Eval("createdDate")!=DBNull.Value? Convert.ToDateTime(Eval("createdDate")).ToString("MM-dd-yyyy"): "" %>'
                                            runat="server"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <asp:Label ID="lblStatusHead" ForeColor="White" runat="server" Text="Status"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "status").ToString().Length > 22 ? DataBinder.Eval (Container.DataItem, "status").ToString().Substring(0,20) + "..." :DataBinder.Eval (Container.DataItem, "status").ToString()) %>'
                                            ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "status").ToString()) %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Detail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="btnViewDetail" runat="server" ImageUrl="~/admin/Images/detail.gif"
                                        Target="_blank" NavigateUrl='<%# "~/admin/OrderDetails.aspx?ID="+ Eval("dOrderID") %>'
                                        ToolTip="View Deal Order Detail" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manage" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink Target="_blank" ID="hlManageTables" runat="server" NavigateUrl='<%#"~/admin/dealOrderDetailInfo.aspx?oid="+ Eval("dOrderID") %>'
                                        Width="32px" ImageUrl="~/admin/Images/ViewAll.png" ToolTip="Manage Deal Order"></asp:HyperLink>
                                    <%--<asp:ImageButton ID="btnEdit" CommandName="Edit" runat="server" Width="32" ImageUrl="~/admin/Images/ViewAll.png"
                                        ToolTip="Manage Deal Order" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Card Info" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="ViewCreditCardInfo"
                                        CommandArgument='<%#Eval("ccInfoID")%>' CausesValidation="false" ToolTip="View Credit Card Info"
                                        ImageUrl="~/admin/Images/credit_card.jpg" />
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Make Payment" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnimgMakePayment" runat="server" CommandName="MakePayment"
                                        Visible='<%# getDetailStatus(Eval("status"),Eval("ccCreditUsed"))%>' Width="28"
                                        CommandArgument='<%#Eval("dOrderID")%>' CausesValidation="false" ToolTip="Process Pesigate Payment for this Order"
                                        OnClientClick="return confirm('Are you sure you want to process payment?');"
                                        ImageUrl="~/admin/Images/MakePayment.gif" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Pay Back" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnPayBack" runat="server" Visible='<%# getPayBackStatus(Eval("tracking"),Eval("dealEndTime"),Eval("status"),Eval("psgTranNo"),Eval("ccCreditUsed"),Eval("tastyCreditUsed"),Eval("comissionMoneyUsed"),Eval("createdDate"),Eval("affiliatePartnerId"),Eval("gainedId"))== true ? (Convert.ToInt32(Eval("RedeemCount"))>0) ? false :true :false %>'
                                        Width="28" CausesValidation="false" ToolTip="Roll back Payment for this Order"
                                        ImageUrl="~/admin/Images/RollBack.gif" />
                                    <div style="display: none;">
                                        <asp:Button ID="BtnHiddenPayBack" runat="server" CommandArgument='<%#Eval("dOrderID")%>'
                                            CommandName="PayBack" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfUserId" runat="server" Value='<%# Eval("userId") %>' />
                                    <asp:HiddenField ID="hfAffComm" runat="server" Value='<% # Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "affComm").ToString()) %>' />
                                    <asp:DropDownList ID="ddlStatus" Font-Size="9" runat="server">
                                        <asp:ListItem Text="Successful" Value="Successful"></asp:ListItem>
                                        <asp:ListItem Text="Refunded" Value="Refunded"></asp:ListItem>
                                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--OnClientClick='<%# "checkOrdercomission("+Eval("affiliatePartnerId")+","+Eval("gainedId")+");" %>'--%>
                                    <asp:ImageButton ID="btnLinkUpdate" href="#PopupDetails" ToolTip="Change Order Status"
                                        CausesValidation="false" runat="server" Visible='<%# getButtonStatus(Eval("tracking"),Eval("dealEndTime"),Eval("affiliatePartnerId"),Eval("gainedId"))%>'
                                        Width="28" ImageUrl="~/admin/Images/process.gif" />
                                    <div style="display: none;">
                                        <asp:Button ID="BtnHidden" runat="server" CommandName="Select" />
                                    </div>
                                    <asp:HiddenField ID="hfPrefilledData" Value='<%# Convert.ToString(Eval("psgTranNo")).Trim() + "|" + Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "DealName").ToString()) + "|" + Convert.ToString(Eval("userid")).Trim()+ "|" + Convert.ToString(Eval("dOrderID")).Trim() %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div id="emptyRowStyle" align="left">
                                <asp:Label ID="emptyText" Text="No records founds." runat="server"></asp:Label>
                            </div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridText" Height="27px" />
                        <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                        <PagerTemplate>
                            <div style="padding-top: 0px;">
                                <div id="pager">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family: Tahoma;
                                        font-size: 11px; color: #666666;">
                                        <tr>
                                            <td width="30%" align="left" style="padding-left: 2px;">
                                                <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                                                <asp:Label ID="lblTotal" Text=" results" runat="server"></asp:Label>
                                            </td>
                                            <td width="30%">
                                                <div class="floatRight">
                                                    <asp:DropDownList ID="ddlPage" runat="server" CssClass="fontStyle" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="floatRight" style="padding-top: 3px; padding-right: 6px;">
                                                    <asp:Label ID="lblRecordsPerPage" runat="server" Text="Record per Page"></asp:Label>
                                                </div>
                                            </td>
                                            <td width="40%" align="right" style="padding-right: 2px;">
                                                <table border="0" cellpadding="0" cellspacing="0" style="font-family: Tahoma; font-size: 11px;
                                                    color: #666666;">
                                                    <tr>
                                                        <td style="padding-right: 2px">
                                                            <div id="divPrevious">
                                                                <asp:ImageButton ID="btnPrev" Enabled='<%# displayPrevious %>' CommandName="Page"
                                                                    CommandArgument="Prev" runat="server" ImageUrl="~/admin/images/imgPrev.jpg" />
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="floatLeft" style="padding-top: 3px; padding-left: 8px;">
                                                                <asp:Label ID="lblpage1" runat="server" Text="Page"></asp:Label>
                                                            </div>
                                                            <div style="padding-left: 10px; padding-right: 10px; float: left">
                                                                <asp:TextBox ID="txtPage" CssClass="fontStyle" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"
                                                                    Style="padding-left: 12px;" Width="20px" Text="1" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="floatLeft" style="padding-top: 3px; padding-right: 4px;">
                                                                <asp:Label ID="lblOf" runat="server" Text="of"></asp:Label>
                                                                <asp:Label ID="lblPageCount" runat="server"></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td style="padding-left: 4px">
                                                            <div id="divNext">
                                                                <asp:ImageButton ID="btnNext" Enabled='<%# displayNext %>' CommandName="Page" CommandArgument="Next"
                                                                    runat="server" ImageUrl="~/admin/images/imgNext.jpg" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                        </PagerTemplate>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="confirm-container" class="simplemodal-container" style="position: fixed;
        display: none; height: auto !important; width: auto !important; z-index: 1002;
        left: 25% !important; top: 20%; overflow: hidden;">
        <a class="modal-close simplemodal-close" title="Close" href="#">x</a><div tabindex="-1"
            style="outline: 0px none; border: 5px solid red; overflow: hidden;">
            <div id="confirm" class="simplemodal-data" style="display: block;">
                <div class="header">
                    <span>Please Enter User Notes</span></div>
                <p>
                    <div style="margin: 13px;">
                        <textarea name="styled-textarea" id="txtUserNotes"></textarea>
                    </div>
                </p>
                <div style="float: left !important;" class="buttons">
                    <div class="no simplemodal-close">
                        Cancel</div>
                    <div class="yes">
                        Save</div>
                </div>
            </div>
        </div>
    </div>
</div>
