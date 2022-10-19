<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="foodCreditManagement.aspx.cs" Inherits="admin_foodCreditManagement"
    Title="TastyGo | Admin | Food Credit Management" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchFirstName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchLastName').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchUserName').value = '';
            
            
            return false;
        }

        function countrychanged(obj, objLive, objCh) {
            //alert(obj);
            var ctryID = obj;

            //First it will null all the options of the ProvinceLive drop down list
            for (loop = document.getElementById(objLive).options.length - 1; loop > -1; loop--) {
                document.getElementById(objLive).options[loop] = null;
            }

            var count = 0;

            //The it will get the required value from the Hidden drop down list and set it into the Live drop down list
            document.getElementById(objLive).options[count] = new Option("Select One");
            count++;
            for (loop = document.getElementById(objCh).options.length - 1; loop > -1; loop--) {
                var ProConID = document.getElementById(objCh).options[loop].value;
                //First Part contain the Province ID and Second Part contain the Country ID
                var ProConArray = ProConID.split(",");
                //Validate that Country ID in the country drop down list macthes with the country provinces into the hidden drop down list

                if (ProConArray[1] == ctryID) {

                    var ProvinceName = document.getElementById(objCh).options[loop].Text.Trim();
                    document.getElementById(objLive).options[count] = new Option(ProvinceName);
                    document.getElementById(objLive).options[count].value = ProConArray[0];
                    count++;
                }
            }
        }

        function checkAll() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            if (document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked) {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = true;
                }
            }
            else {
                for (i = 1; i <= list.length - 4; i++) {
                    document.getElementById(list[i]).checked = false;
                }
            }
        }

        function ChangeHeaderAsNeeded() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (!document.getElementById(list[i]).checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = false;
                    return;
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_pageGrid_ctl01_HeaderLevelCheckBox").checked = true;
        }

        function preCheckSelected() {
            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (document.getElementById(list[i]).checked) {
                    return (confirm("Are you sure you want to delete the selected record(s)?"));
                }
            }
            alert("Please select the record(s) to delete!");
            return false;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <link href="CSS/confirm.css" rel="stylesheet" type="text/css" />

    <script src="JS/jquery-1.4.0.min.js" type="text/javascript"></script>

    <script src="JS/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>

    <script src="JS/jquery.simplemodal.js" type="text/javascript"></script>

    <script src="JS/confirm.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    var UID;
    function SaveUserNotes()
    {
      var Notes =  escape($("#txtUserNotes").val());
      $.ajax({
      type: "POST",
      url: "../getStateLocalTime.aspx?UID=" + UID + "&Notes=" + Notes + "&OrderID=0",
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
function RunPopup(ButtonID,CompleteData,TextBoxID,PayBackButtonID)
{        
    var textBoxValue= $("#"+TextBoxID).val();
     var filterNumber=/^[-+]?[0-9]\d{0,2}(\.\d{1,2})?%?$/;
     if(textBoxValue=='' || !filterNumber.test(textBoxValue))
     {
       alert("Please enter valid Numeric value e.g.(1, 2.5)");
       return false;
     }
    var SompleteString = CompleteData.split('|');                    
    var Reason = SompleteString[0]+ " \n";
    var Userid = SompleteString[1]; 
    UID =   Userid;  
    $("#txtUserNotes").val(Reason);
    RunPopup2(function () {
       SaveUserNotes();
           
     $("#" + ButtonID).click();                      
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
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblFirstNameSearch" runat="server" Text="First Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchFirstName" runat="server" Width="75px" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="lblLastNameSearch" runat="server" Text="Last Name"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchLastName" runat="server" Width="75px" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="lblUsernameSearch" runat="server" Text="Username"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchUserName" runat="server" Width="70px" CssClass="txtSearch"></asp:TextBox>
                    </div>
                    <div class="heading">
                        <asp:Label ID="Label1" runat="server" Text="Tasty Credit"></asp:Label>
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlTastyCredit" Height="24" Font-Size="11pt" runat="server">
                            <asp:ListItem Value="=" Text="="></asp:ListItem>
                            <asp:ListItem Value=">" Text=">"></asp:ListItem>
                            <asp:ListItem Value="<" Text="<"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:TextBox ID="txtTastyCredit" Width="63px" runat="server" CssClass="txtSearch"></asp:TextBox></div>
                    <div>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnSearch.png"
                            OnClick="btnSearch_Click" TabIndex="1" />&nbsp;<asp:ImageButton ID="btnClear" runat="server"
                                ImageUrl="~/admin/Images/btnClear.png" OnClientClick="return clearFields();"
                                TabIndex="2" />
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
                <div class="searchButtons">
                    <div class="floatLeft">
                        <asp:ImageButton ID="btnShowAll" runat="server" ImageUrl="~/admin/images/btnShowAll.gif"
                            OnClick="btnShowAll_Click" />&nbsp;
                    </div>
                    <div class="floatLeft">
                        <asp:ImageButton Visible="false" ID="btnDeleteSelected" runat="server" ImageUrl="~/admin/images/btnDeleteSelected.jpg"
                            OnClientClick="return preCheckSelected();" OnClick="btnDeleteSelected_Click" />
                    </div>
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server">
                    </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="userID" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                                OnRowDataBound="pageGrid_RowDataBound" OnRowEditing="pageGrid_Login" AllowSorting="True"
                                OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                            runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("userID") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID1" runat="server" Text='<% # Eval("userID") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <div style="padding-left: 12px;">
                                                <asp:LinkButton ID="lblUserHeadFName" ForeColor="White" runat="server" Text="First Name"
                                                    CommandName="Sort" CommandArgument="firstName"></asp:LinkButton>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="padding-left: 12px;">
                                                <asp:Label ID="lblUserFirstNameText" Text='<% # Eval("firstName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" SortExpression="lastName"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblUserHeadLName" ForeColor="White" runat="server" Text="Last Name"
                                                CommandName="Sort" CommandArgument="lastName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserLastNameText" Text='<% # Eval("lastName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblUserHeadUName" ForeColor="White" runat="server" Text="Username"
                                                CommandName="Sort" CommandArgument="userName"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblUserUserNameText" Text='<% # Eval("userName") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblFoodCreditHead" ForeColor="White" runat="server" Text="Food Money"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblFoodCreditText" Text='<% # getAvailableFoodCredit(Eval("userID")) %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFoodCredit" runat="server" Width="80px"></asp:TextBox>
                                            <cc1:RequiredFieldValidator ID="rfvFoodCredit" SetFocusOnError="true" runat="server"
                                                ControlToValidate="txtFoodCredit" ValidationGroup='<% # "user"+Eval("userID") %>'
                                                ErrorMessage="Value required!" Display="None">
                                            </cc1:RequiredFieldValidator>
                                            <cc2:ValidatorCalloutExtender ID="vceFoodCredit1" runat="server" TargetControlID="rfvFoodCredit">
                                            </cc2:ValidatorCalloutExtender>
                                            <asp:RangeValidator runat="server" ID="rxFoodCredit" ValidationGroup='<% # "user"+Eval("userID") %>'
                                                ControlToValidate="txtFoodCredit" Type="Currency" ErrorMessage="Value must be numeric."
                                                MinimumValue="-999999999" MaximumValue="999999999" Display="None"></asp:RangeValidator>
                                            <cc2:ValidatorCalloutExtender ID="vceFoodCredit" runat="server" TargetControlID="rxFoodCredit">
                                            </cc2:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Send Amount" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibLogin" ValidationGroup='<% # "user"+Eval("userID") %>' ImageUrl="~/admin/Images/btnSend.gif"
                                                runat="server" />
                                            <div style="display: none;">
                                                <asp:Button ID="BtnHidden" runat="server" CommandName="Edit" />
                                            </div>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
</asp:Content>
