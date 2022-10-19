﻿<%@ Page Title="TastyGo | Admin | Gift Card" Language="C#" MasterPageFile="~/admin/adminTastyGo.master"
    AutoEventWireup="true" CodeFile="giftcardManagment.aspx.cs" Inherits="giftcardManagment" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchCardAmount').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchCreatedBy').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchUsedBy').value = '';
            return false;
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

        function ImageValidation(oSrc, args) {
            var strFUImage = document.getElementById("ctl00_ContentPlaceHolder1_fuGiftcardImage").value;
            var strFUImageArray = strFUImage.split(".");
            var strFUImageExt = strFUImageArray[1];
            if (strFUImageExt == "jpg" || strFUImageExt == "JPG" || strFUImageExt == "gif" || strFUImageExt == "GIF" || strFUImageExt == "JPEG" ||
            strFUImageExt == "jpeg" || strFUImageExt == "png" || strFUImageExt == "PNG" || strFUImageExt == "ico" || strFUImageExt == "ICO"
            || strFUImageExt == "tif" || strFUImageExt == "TIF" || strFUImageExt == "tiff" || strFUImageExt == "TIFF" || strFUImageExt == "bmp"
            || strFUImageExt == "BMP") {
                args.IsValid = true;
                return;
            }
            else {
                args.IsValid = false;
                return;
            }

            //alert(strFUImageExt);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">                                         
                    <div class="heading">
                        <asp:Label ID="lblCousineNameSearch" runat="server" Text="Card Amount"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchCardAmount" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                     <div class="heading">
                        <asp:Label ID="Label5" runat="server" Text="Created By"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchCreatedBy" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
                     <div class="heading">
                        <asp:Label ID="Label6" runat="server" Text="Used By"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchUsedBy" runat="server" CssClass="txtSearch"></asp:TextBox>
                    </div>
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
                        <asp:ImageButton ID="btnDeleteSelected" runat="server" ImageUrl="~/admin/images/btnDeleteSelected.jpg"
                            OnClientClick="return preCheckSelected();" OnClick="btnDeleteSelected_Click" />
                    </div>
                    <div class="floatLeft">
                        &nbsp;<asp:ImageButton ID="btnAddNew" runat="server" ImageUrl="~/admin/images/btnAddNew.jpg"
                            OnClick="btnAddNew_Click" />
                    </div>
                </div>
                <div id="gv">
                    <asp:TextBox ID="hiddenIds" Style="display: none" runat="server"> </asp:TextBox>
                    <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="pageGrid" runat="server" DataKeyNames="giftCardId" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging"
                                GridLines="None" OnRowDataBound="pageGrid_RowDataBound" OnRowDeleting="pageGrid_RowDeleting"
                                OnSelectedIndexChanged="pageGrid_SelectedIndexChanged" AllowSorting="True" OnSorting="pageGrid_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" value='<% # Eval("giftCardId") %>' ID="RowLevelCheckBox"
                                                onclick="ChangeHeaderAsNeeded()" />
                                            <div style="display: none">
                                                <asp:Label ID="lblID1" runat="server" Text='<% # Eval("giftCardId") %>' Visible="true"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCousineHeadgiftCardId" ForeColor="White" runat="server" Text="Card Number"
                                                CommandName="Sort" CommandArgument="giftCardCode"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblgiftCardIdText" Text='<% # Eval("giftCardCode") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCardHeadCardAmount" ForeColor="White" runat="server" Text="Card Amount"
                                                CommandName="Sort" CommandArgument="giftCardAmount"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblCardAmountText" Text='<% # "$"+ Eval("giftCardAmount")+" CAD" %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Creation Date"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblcreationDateText" Text='<% # GetDateString(Eval("creationDate")) %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblCardHeadcreatedByUser" ForeColor="White" runat="server" Text="Created By"
                                                CommandName="Sort" CommandArgument="createdByUser"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblcreatedByUserText" Text='<% # Eval("createdByUser") %>' runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblHeadtakenBy" ForeColor="White" runat="server" Text="Used By"
                                                CommandName="Sort" CommandArgument="takenByUser"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblTakenByText" Text='<% # GetCardExplain(Eval("takenByUser").ToString()) %>'
                                                    runat="server"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="Edit" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip='<%#(Eval("takenByUser").ToString()=="" ? "Send it now!" : "Could not send it.") %> '  Enabled='<%#(Eval("takenByUser")==DBNull.Value ? true : false) %> '  ImageUrl='<%#(Eval("takenByUser")==DBNull.Value ? "~/admin/Images/edit.gif" : "~/admin/Images/edit-deavtive.gif") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%" HeaderText="Delete" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" Enabled='<%#(Eval("takenByUser")==DBNull.Value ? true : false) %>'  ImageUrl='<%#(Eval("takenByUser")==DBNull.Value ? "~/admin/Images/delete.gif" : "~/admin/Images/delete-deavtive.gif") %>'
                                                OnClientClick='return confirm("Are you sure you want to delete this item?");'
                                                ToolTip="Delete Gift Card" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div id="emptyRowStyle" align="left">
                                        <asp:Label ID="emptyText" Text="No data to display" runat="server"></asp:Label>
                                    </div>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="gridHeader" />
                                <RowStyle CssClass="gridText" Height="27px" />
                                <AlternatingRowStyle CssClass="AltgridText" Height="27px" />
                                <PagerTemplate>
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
                                                        <asp:Label ID="lblRecordsPerPage" runat="server" Text="Records per page"></asp:Label>
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
                                                                        Style="padding-left: 12px;" Width="20px" runat="server" Text="1"></asp:TextBox>
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
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfCountry" runat="server" Value="0" />
            <asp:HiddenField ID="hfProvince" runat="server" Value="0" />
            <asp:Panel ID="pnlForm" runat="server" Visible="false">
                <div style="width: 100%;" align="center">
                    <div id="popup">
                        <div id="popHeader">
                            <div style="float: left">
                                <asp:Label ID="lblpopHead" Text="Giftcard Managment" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table border="0" cellpadding="3" cellspacing="2" width="920px" class="fontStyle">
                            <tr>
                                <td align="right" style="width: 40%" class="colRight">                                    
                                </td>
                                <td align="left" class="colLeft" style="width: 40%">
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" Text="Your card number is not correct please enter again." />
                                </td>
                                <td align="left" class="colLeft" style="width: 30%">                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%" class="colRight">
                                    <asp:Label ID="lblUserType" runat="server" Text="Card Number" />
                                </td>
                                <td align="left" class="colLeft" style="width: 40%">
                                    <asp:TextBox runat="server" ID="txtCardNumber" CssClass="txtForm" />
                                    <cc1:RequiredFieldValidator ID="rfvCardnumber" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtCardNumber" ErrorMessage="Card number required!" ValidationGroup="Card"
                                        Display="None">                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="vcdCousineName" runat="server" 
                                        TargetControlID="rfvCardnumber">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                                <td align="left" class="colLeft" style="width: 30%">
                                    <asp:ImageButton ID="ibGenerateCode" runat="server" 
                                        ImageUrl="~/admin/Images/generateNumber.gif" 
                                        onclick="ibGenerateCode_Click" />
                                </td>
                            </tr>
                             <tr>
                                <td align="right" style="width: 40%" class="colRight">
                                    <asp:Label ID="Label1" runat="server" Text="Card Amount" />
                                </td>
                                <td align="left" class="colLeft" style="width: 40%">
                                    <asp:TextBox runat="server" ID="txtCardAmount" CssClass="txtForm" />
                                    <cc1:RequiredFieldValidator ID="rfvCardAmount" SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtCardAmount" ErrorMessage="Card amount required!" ValidationGroup="Card"
                                        Display="None">                                                               
                                    </cc1:RequiredFieldValidator>
                                <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                                        TargetControlID="rfvCardAmount">
                                </cc2:ValidatorCalloutExtender>
                                    <cc1:RangeValidator ID="rngItemPriceRange" runat="server" 
                                        ControlToValidate="txtCardAmount" Display="None" 
                                        ErrorMessage="Numeric value required!" MaximumValue="999999999" 
                                        MinimumValue="0" SetFocusOnError="true" Type="Currency" ValidationGroup="Card">                            
                                    </cc1:RangeValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                                        TargetControlID="rngItemPriceRange">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                                <td align="left" class="colLeft" style="width: 30%">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%" class="colRight">
                                    <asp:Label ID="Label3" runat="server" Text="Email To" />
                                </td>
                                <td align="left" class="colLeft" style="width: 40%">
                                    <asp:TextBox runat="server" ID="txtEmailTo" CssClass="txtForm" />
                                     <cc1:RegularExpressionValidator ID="revEmailAddress" runat="server" 
                                        ControlToValidate="txtEmailTo" Display="None" 
                                        ErrorMessage="Invalid email address!" SetFocusOnError="true" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        ValidationGroup="Card"></cc1:RegularExpressionValidator>
                                    <cc2:ValidatorCalloutExtender ID="vceREEmail" runat="server" 
                                        TargetControlID="revEmailAddress">
                                    </cc2:ValidatorCalloutExtender>
                                     <cc1:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                        SetFocusOnError="true" runat="server"
                                        ControlToValidate="txtEmailTo" ErrorMessage="Email required!" ValidationGroup="Card"
                                        Display="None">                                                               
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" 
                                        TargetControlID="RequiredFieldValidator2">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                                <td align="left" class="colLeft" style="width: 30%">                                    
                                </td>
                                </tr>
                                <tr>
                                <td align="right" class="colRight" style="width: 40%" valign="top">
                                    <asp:Label ID="Label2" runat="server" Text="Message" />
                                </td>
                                <td align="left" class="colLeft" style="width: 40%">
                                    <asp:TextBox ID="txtMessage" runat="server" CssClass="txtForm" Height="100px" 
                                        TextMode="MultiLine" />
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtMessage" Display="None" ErrorMessage="Message required!" 
                                        SetFocusOnError="true" ValidationGroup="Card">                            
                                    </cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                                        TargetControlID="RequiredFieldValidator1">
                                    </cc2:ValidatorCalloutExtender>
                                </td>
                                <td align="left" class="colLeft" style="width: 30%">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="width: 40%" class="colRight">
                                </td>
                                <td align="left" class="colLeft" style="width: 40%">
                                    <asp:ImageButton ID="btnSave" runat="server" ValidationGroup="Card" ImageUrl="~/admin/Images/btnSend.gif"
                                        OnClick="btnSave_Click" />
                                    <asp:ImageButton ID="btnUpdate" runat="server" ValidationGroup="Card" ImageUrl="~/admin/images/btnSend.gif"
                                        OnClick="btnUpdate_Click" Visible="False" />&nbsp;
                                    <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                        OnClick="CancelButton_Click" />
                                </td>
                                <td align="left" class="colLeft" style="width: 30%">
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
