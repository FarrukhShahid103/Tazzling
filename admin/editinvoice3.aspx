<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editinvoice3.aspx.cs" Inherits="editinvoice3" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="../CSS/fonts.css" type="text/css" charset="utf-8" />
    <link href="CSS/adminTastyGo.css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">     
         function numbersonly(myfield, e, dec) {
            var key;
            var keychar;

            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            
            keychar = String.fromCharCode(key);

            // control keys
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27))
                return true;
            // numbers
            else if ((("0123456789").indexOf(keychar) > -1)) {                                  
                return true;
            }
            // decimal point jump            
            else
            {              
                return false;
            }
        }
        
        
         function calculation() {
               var fristPayment = document.getElementById("txtFirstPaymentPer").value;               
               var fristPaymentTotal = document.getElementById("lblFirstPaymentAmount");               
               var secondPayment = document.getElementById("txtSecondPaymentPer").value;  
               var secondPaymentTotal = document.getElementById("lblSecondPaymentAmount");               
               var thirdPayment = document.getElementById("txtThirdPaymentPercent").value;               
               var thirdPaymentTotal = document.getElementById("lblThirdPaymentAmount");  
               var totalPayout = document.getElementById("hfTotalPaymout").value;  
                                             
               fristPaymentTotal.innerHTML = Math.round(((fristPayment/100) *  totalPayout)*100)/100;
               secondPaymentTotal.innerHTML =Math.round(((secondPayment/100)*  totalPayout)*100)/100;
               thirdPaymentTotal.innerHTML =Math.round(((thirdPayment/100)*  totalPayout)*100)/100;
        }  
        
          function totalpercentcheck() {
               var fristPayment = document.getElementById("txtFirstPaymentPer").value;                                
               var secondPayment = document.getElementById("txtSecondPaymentPer").value;                 
               var thirdPayment = document.getElementById("txtThirdPaymentPercent").value;               
               
                if((parseFloat(fristPayment)+parseFloat(secondPayment)+parseFloat(thirdPayment))==100)
                {
                    return true;
                }
                else
                {
                    alert("Your did not enter correct values in % fields.");
                    return false;
                }                                                                                                       
            }  
        
        
                                   
    </script>

</head>
<body style="font-family: Arial;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" AsyncPostBackTimeout="96000" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/JS/webkit.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="floatRight">
        <asp:UpdateProgress ID="upTasty" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div class="lightbox">
                    <div style="background-color: White; padding: 20px; width: 120px; border: solid 2px #e1e1e1;">
                        <asp:Image ImageUrl="~/admin/Images/_load.gif" ToolTip="Processing..." runat="server"
                            ID="imgLoad" />
                        <br />
                        <br />
                        <asp:Label ID="lblLoad" runat="server" Text="Processing..."></asp:Label>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfTotalPaymout" runat="server" Value="0" />
            <div id="pagezone">
                <div style="clear: both; padding-top: 40px;">
                    <div style="width: 450px; float: left;">
                        <div style="clear: both; font-size: 22px; font-weight: bold;">
                            <asp:Label ID="lblInvoiceNumber" runat="server" Text="INVOICE #1259901"></asp:Label>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 15px; font-weight: bold;">
                            TO:
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtBusinessName" runat="server"
                                    Width="350px" Text="COMPANY NAME HERE" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblBusinessName" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveBName" OnClick="btnSaveBName_Click" runat="server" Visible="false"
                                    ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseBName" OnClick="btnCloseBName_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditBName" OnClick="btnEditBName_Click" runat="server" ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtBusinessAddress" runat="server"
                                    Width="350px" Text="5555 Street Address, City, State 55555" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblBusinessAddress" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveBAddress" OnClick="btnSaveBAddress_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseBAddress" OnClick="btnCloseBAddress_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditBAddress" OnClick="btnEditBAddress_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtBusinessPhone" runat="server"
                                    Width="350px" Text="Phone: 555.555.5555" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblBusinessPhone" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveBPhone" OnClick="btnSaveBPhone_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseBPhone" OnClick="btnCloseBPhone_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditBPhone" OnClick="btnEditBPhone_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 25px; font-weight: bold;">
                            CHEQUE SENT TO:
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" Visible="false" ID="txtBusinessPaymentTitle"
                                    runat="server" Width="350px" Text="PAYMENT TITLE HERE"></asp:TextBox>
                                <asp:Label ID="lblBusinessPaymentTitle" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveCName" OnClick="btnSaveCName_Click" runat="server" Visible="false"
                                    ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseCName" OnClick="btnCloseCName_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditCName" OnClick="btnEditCName_Click" runat="server" ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" Visible="false" ID="txtBusinessPaymentAddress"
                                    runat="server" Width="350px" Text="Payment address, City, State 55555"></asp:TextBox>
                                <asp:Label ID="lblBusinessPaymentAddress" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveCAddress" OnClick="btnSaveCAddress_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseCAddress" OnClick="btnCloseCAddress_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditCAddress" OnClick="btnEditCAddress_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" Visible="false" ID="txtBusinessPaymentPhone"
                                    runat="server" Width="350px" Text="Phone: 555.555.5555"></asp:TextBox>
                                <asp:Label ID="lblBusinessPaymentPhone" runat="server"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveCPhone" OnClick="btnSaveCPhone_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseCPhone" OnClick="btnCloseCPhone_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditCPhone" OnClick="btnEditCPhone_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                    </div>
                    <div style="width: 450px; float: right; text-align: right;">
                        <div style="clear: both; font-size: 14px; font-weight: bold;">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="INVOICE DATE: yyyy/mm/dd"></asp:Label>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 15px; font-weight: bold;">
                            <img src="Images/invoiceLogo.png" />
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 40px; font-weight: bold;">
                            From: Tastygo Online Inc.
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <asp:Label ID="lblTastyGOAddress" runat="server" Text="20-206 East 6th Ave Vancouver BC V5T-1J7"></asp:Label>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <asp:Label ID="lblTastyGoPhone" runat="server" Text="Toll Free: 1-855-295-1771 | Fax: 1-888-717-7073"></asp:Label>
                        </div>
                        <div style="clear: both; font-size: 14px; padding-top: 5px;">
                            <asp:Label ID="lblTastygoSite" runat="server" Text="www.tazzling.com"></asp:Label>
                        </div>
                    </div>
                    <div style="width: 100%; border-bottom: 1px solid black; height: 10px; clear: both;">
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px;">
                        <div style="width: 650px; float: left; text-align: center;">
                            Deal Title
                        </div>
                        <div style="width: 250px; float: left; text-align: center;">
                            Campaign Duration
                        </div>
                    </div>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <asp:Literal ID="ltDealsDetails" runat="server" Text=""></asp:Literal>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <div style="width: 100%; clear: both; font-size: 16px; padding-top: 20px; padding-left: 15px;
                        font-weight: bold;">
                        Service
                    </div>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; height: 25px; padding-top: 5px;">
                        <div style="width: 435px; float: left; text-align: left; padding-left: 15px;">
                            Description
                        </div>
                        <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                            Unit Price
                        </div>
                        <div style="width: 150px; float: left; text-align: left;">
                            Line Total
                        </div>
                    </div>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <asp:Literal ID="ltServiceFeeDetail" runat="server" Text=""></asp:Literal>
                    <asp:GridView ID="pageGrid" runat="server" DataKeyNames="poID" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="false" ShowHeader="false" ShowFooter="false" GridLines="None" OnRowDeleting="pageGrid_RowDeleting">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="440px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div style="font-weight: bold; padding-left: 10px;">
                                        <asp:Label ID="lblpoType" runat="server" Text='<% # Eval("poType") %>'> </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div style="font-weight: bold; padding-left: 10px;">
                                        <asp:Label ID="lblpoDescription" runat="server" Text='<% # Eval("poDescription") %>'> </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <div style="padding-left: 5px;">
                                        <asp:Label ID="lblpoAmount" runat="server" Text='<% # "$" + Eval("poAmount") %>'> </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                        ImageUrl="~/admin/Images/delete.gif" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div id="emptyRowStyle" align="left">
                                <asp:Label ID="emptyText" Text="No data to display" runat="server"></asp:Label>
                            </div>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="InvoiceGridAdjustSecondLine" />
                        <AlternatingRowStyle CssClass="InvoiceGridAdjustFirstLine" />
                    </asp:GridView>
                    <asp:Literal ID="ltAdjustmentTotal" runat="server" Text=""></asp:Literal>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <div style="width: 100%; clear: both; font-size: 16px; padding-top: 50px; padding-left: 15px;
                        font-weight: bold;" id="divAdjustment" runat="server">
                        <div style="width: 435px; float: left; text-align: left; padding-left: 15px; font-weight: bold;">
                            <div>
                                Adjustment</div>
                            <div style="font-size: 12px; font-weight: normal;">
                                positive amount for adding more money to vendor, negative for deducting money from
                                vendor</div>
                        </div>
                        <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                            <div style="clear: both;">
                                Description
                            </div>
                            <div style="clear: both; padding-top: 5px;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtAdjustmentDescription" runat="server"
                                    Text="" Width="80%" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                                    ControlToValidate="txtAdjustmentDescription" ErrorMessage="Please enter Description!"
                                    ValidationGroup="Adjustment" Display="None">                                                                           
                                &nbsp;&nbsp;                                                                           
                                </cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div style="width: 150px; float: left; text-align: left;">
                            <div style="clear: both; font-weight: bold;">
                                Amount
                            </div>
                            <div style="clear: both; padding-top: 5px;">
                                <asp:TextBox ID="txtAdjustmentAmount" runat="server" Width="75px" Style="border: 1px solid #666666;"></asp:TextBox>
                                <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                    ControlToValidate="txtAdjustmentAmount" ErrorMessage="Please enter Amount!" ValidationGroup="Adjustment"
                                    Display="None">                                                                           
                                &nbsp;&nbsp;                                                                           
                                </cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                                </cc2:ValidatorCalloutExtender>
                                <cc1:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true"
                                    runat="server" ControlToValidate="txtAdjustmentAmount" ErrorMessage="Only Numeric value required"
                                    ValidationGroup="Adjustment" Display="None" ValidationExpression="(^-{0,1}\d*\.{0,1}\d+$)"></cc1:RegularExpressionValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RegularExpressionValidator1">
                                </cc2:ValidatorCalloutExtender>
                            </div>
                            <div style="clear: both; padding-top: 5px;">
                                <asp:ImageButton ID="btnAdjustment" runat="server" ValidationGroup="Adjustment" ImageUrl="~/admin/Images/btnAdjustment.png"
                                    OnClick="btnAdjustment_Click" />
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%; clear: both; font-size: 16px; padding-top: 20px; padding-left: 15px;
                        font-weight: bold;">
                        Payment Schedule
                    </div>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; height: 25px; padding-top: 5px;">
                        <div style="width: 435px; float: left; text-align: center; padding-left: 15px;">
                            Date
                        </div>
                        <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                            % Payment
                        </div>
                        <div style="width: 150px; float: left; text-align: left;">
                            Line Total
                        </div>
                    </div>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;
                        min-height: 20px;">
                        <div style="width: 440px; float: left; text-align: center; padding-left: 10px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtFirstPaymentDateTime" runat="server"
                                    Visible="false"></asp:TextBox>
                                <asp:Label ID="lblFirstPaymentDateTime" runat="server"></asp:Label></div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveFirstPayment" OnClick="btnSaveFirstPayment_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseFirstPayment" OnClick="btnCloseFirstPayment_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditFirstPayment" OnClick="btnEditFirstPayment_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                            <div style="float: left;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtFirstPaymentPer" Width="20px"
                                    MaxLength="2" runat="server" Visible="false" onKeyPress="return numbersonly(this, event,true);"></asp:TextBox>
                                <asp:Label ID="lblFirstPaymentPer" runat="server"></asp:Label>% First Payment</div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveFirstPer" OnClick="btnSaveFirstPer_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseFirstPer" OnClick="btnCloseFirstPer_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditFirstPer" OnClick="btnEditFirstPer_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="width: 150px; float: left; text-align: left; font-weight: normal;">
                            $<asp:Label ID="lblFirstPaymentAmount" runat="server"></asp:Label></div>
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;">
                        <div style="width: 440px; float: left; text-align: center; padding-left: 10px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtSecondPaymentDateTime" runat="server"
                                    Visible="false"></asp:TextBox>
                                <asp:Label ID="lblSecondPaymentDateTime" runat="server"></asp:Label></div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveSecondPayment" OnClick="btnSaveSecondPayment_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseSecondPayment" OnClick="btnCloseSecondPayment_Click"
                                    runat="server" Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditSecondPayment" OnClick="btnEditSecondPayment_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                            <div style="float: left;">
                                <asp:TextBox Style="border: 1px solid #666666;" Visible="false" ID="txtSecondPaymentPer"
                                    Width="20px" MaxLength="2" runat="server" onKeyPress="return numbersonly(this, event,true);"></asp:TextBox>
                                <asp:Label ID="lblSecondPaymentPer" runat="server"></asp:Label>% Second Payment</div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveSecondPer" OnClick="btnSaveSecondPer_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseSecondPer" OnClick="btnCloseSecondPer_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditSecondPer" OnClick="btnEditSecondPer_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="width: 150px; float: left; text-align: left; font-weight: normal;">
                            $<asp:Label ID="lblSecondPaymentAmount" runat="server"></asp:Label></div>
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;
                        min-height: 20px;">
                        <div style="width: 440px; float: left; text-align: center; padding-left: 10px;">
                            <div style="float: left; width: 350px;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtThirdPaymentDateTime" runat="server"
                                    Visible="false"></asp:TextBox>
                                <asp:Label ID="lblThirdPaymentDateTime" runat="server"></asp:Label></div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveThirdPayment" OnClick="btnSaveThirdPayment_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseThirdPayment" OnClick="btnCloseThirdPayment_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditThirdPayment" OnClick="btnEditThirdPayment_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="width: 300px; float: left; text-align: left; font-weight: bold;">
                            <div style="float: left;">
                                <asp:TextBox Style="border: 1px solid #666666;" ID="txtThirdPaymentPercent" Width="20px"
                                    MaxLength="2" runat="server" Visible="false" onKeyPress="return numbersonly(this, event,true);"></asp:TextBox>
                                <asp:Label ID="lblThirdPaymentPercent" runat="server"></asp:Label>% Final Payment</div>
                            <div style="float: left; padding-left: 5px;">
                                <asp:ImageButton ID="btnSaveThirdPer" OnClick="btnSaveThirdPer_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/Checked.png" />
                                <asp:ImageButton ID="btnCloseThirdPer" OnClick="btnCloseThirdPer_Click" runat="server"
                                    Visible="false" ImageUrl="~/admin/Images/cancelorders.png" />
                                <asp:ImageButton ID="btnEditThirdPer" OnClick="btnEditThirdPer_Click" runat="server"
                                    ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                        </div>
                        <div style="width: 150px; float: left; text-align: left; font-weight: normal;">
                            $<asp:Label ID="lblThirdPaymentAmount" runat="server"></asp:Label></div>
                    </div>
                    <div style="width: 100%; border-bottom: 1px solid black; clear: both;">
                    </div>
                    <div style="width: 100%; clear: both; font-size: 32px; padding-top: 40px; text-align: center;">
                        Thank you for your Business!
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; text-align: center; padding-top: 20px;">
                        Interest running another deal? Contact Your Account Manager Today!
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; text-align: center; padding-top: 5px;
                        font-weight: bold;">
                        <asp:Label ID="lblAccountRepDetail" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="width: 100%; clear: both; font-size: 12px; text-align: center; padding-top: 30px;">
                        For Billing Inquiries, contact us 1-855-295-1771
                    </div>
                    <asp:Literal ID="ltRefundOrders" runat="server" Text=""></asp:Literal>
                    <div style="width: 100%; clear: both; font-size: 16px; padding-top: 50px; padding-left: 15px;
                        font-weight: bold;">
                        <asp:Label ID="lblLablePayments" runat="server" Text="Payments"></asp:Label>
                    </div>
                    <div style="width: 100%; clear: both; font-size: 16px; padding-top: 10px; padding-left: 15px;">
                        <div style="float: left; padding-left: 15px;">
                            <asp:ImageButton ID="btnSendFistPayment" runat="server" ImageUrl="~/admin/Images/BtnSendThirdPayment.png"
                                OnClick="btnSendFistPayment_Click" OnClientClick="return confirm('Are you sure you want to proceed this payment?');" />
                        </div>
                    </div>
                    <div style="width: 100%; clear: both; font-size: 16px; padding-top: 10px; padding-left: 15px;">
                        <div style="padding-left: 15px; clear: both;">
                            <div style="float: left; padding-left: 15px;">
                                <asp:Label ID="lblTopTitle" runat="server" Text="Deal Note<br>(Max 500 characters):"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 15px;">
                                <asp:TextBox ID="txtDealNote" runat="server" TextMode="MultiLine" Width="600px" Height="200px"
                                    Style="border: 1px solid #666666;" MaxLength="500"></asp:TextBox>
                            </div>
                            <div style="padding-left: 190px; clear: both; padding-top: 10px;">
                                <div style="float: left; padding-left: 10px;">
                                    <asp:ImageButton ID="btnPaymentToBusiness" runat="server" ImageUrl="~/admin/Images/btnSave.jpg"
                                        OnClick="btnPaymentToBusiness_Click" /></div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%; clear: both; font-size: 16px; padding-top: 50px; padding-bottom: 50px;
                        padding-left: 15px; font-weight: bold;">
                        <asp:UpdatePanel ID="updownloadExcel" runat="server">
                            <ContentTemplate>
                                <div style="clear: both;">
                                    <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                                        <div style="float: left; padding-right: 5px">
                                            <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                                        </div>
                                        <div class="floatLeft">
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="fontStyle"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both; padding-top: 10px;">
                                    <div style="float: left;">
                                        <asp:ImageButton ID="btnSaveAndDownlaodPDF" runat="server" ImageUrl="~/admin/images/btnDownloadInvoice.png"
                                            OnClick="btnSaveAndDownlaodPDF_Click" /></div>
                                    <div style="float: left; padding-left: 10px;">
                                        <asp:ImageButton ID="btnSaveRefund" runat="server" ImageUrl="~/admin/images/btnDownloadRefund.png"
                                            OnClick="btnSaveRefund_Click" /></div>
                                    <div style="float: left; padding-left: 10px;">
                                        <asp:ImageButton ID="btnDownloadCheck" runat="server" ImageUrl="~/admin/images/BtnDownloadChequePDF.png"
                                            OnClick="btnDownloadCheck_Click" /></div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveAndDownlaodPDF" />
                                <asp:PostBackTrigger ControlID="btnSaveRefund" />
                                <asp:PostBackTrigger ControlID="btnDownloadCheck" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
