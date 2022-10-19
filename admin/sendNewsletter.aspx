<%@ Page Title="TastyGo | Admin | Send Newsletter" Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true" CodeFile="sendNewsletter.aspx.cs" Inherits="admin_sendNewsletter" %>


<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function clearFields() {
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearchTitle').value = '';
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

        function validateGridView(oSrc, args) {

            var Elements = document.getElementById("ctl00_ContentPlaceHolder1_hiddenIds").value;
            var list = Elements.split('*');
            for (i = 1; i <= list.length - 4; i++) {
                if (document.getElementById(list[i]).checked) {
                    args.IsValid = true;
                    return;
                } 
            }
            args.IsValid = false;
            return;
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGrid" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlGrid" runat="server" Visible="true">
                <div id="search">
                    <div class="heading">
                        <asp:Label ID="lblFirstNameSearch" runat="server" Text="Title"></asp:Label>
                    </div>
                    <div>
                        <asp:TextBox ID="txtSearchTitle" runat="server" CssClass="txtSearch"></asp:TextBox>
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
                <div id="gv">
                    <div style="width: 100%;" align="center">
                        <div id="element-box1">
                            <div class="t">
                                <div class="t">
                                    <div class="t">
                                    </div>
                                </div>
                            </div>
                            <div class="m">
                                <div class="m">
                                    <div>
                                        <div id="popHeader">
                                            <div style="float: left">
                                                <asp:HiddenField ID="hfBusinessId" runat="server" />
                                                <asp:Label ID="lblpopHead" Text="Send Newsletter" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <table id="tblRestuarant" border="0" cellpadding="3" cellspacing="2" width="720px"
                                            class="fontStyle">                                           
                                            <tr>
                                                <td align="right" class="colRight">
                                                    <asp:Label ID="lblProvince" runat="server" Text="Province" />
                                                </td>
                                                <td align="left" class="colLeft">
                                                    <%--<asp:DropDownList ID="ddlProvince" runat="server" CssClass="ddlForm1" Style="display: none;">
                                                    </asp:DropDownList>--%>
                                                    <asp:DropDownList ID="ddlProvinceLive" runat="server" CssClass="txtForm" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlProvinceLive_SelectedIndexChanged">
                                                        <%--<asp:ListItem Selected="True" Text="Select One" Value="0"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <cc1:RequiredFieldValidator ID="cvProvince" SetFocusOnError="true" InitialValue="Select One"
                                                        runat="server" ControlToValidate="ddlProvinceLive" ErrorMessage="Province required!"
                                                        ValidationGroup="userSend" Display="None">                            
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="vceProvince" TargetControlID="cvProvince" runat="server">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="colRight">
                                                    <asp:Label ID="lblCity" runat="server" Text="City" />
                                                </td>
                                                <td align="left" class="colLeft">
                                                    <asp:DropDownList ID="ddlSelectCity" runat="server" CssClass="txtForm">
                                                        <%--<asp:ListItem Selected="True" Text="Select One" Value="0"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" InitialValue="Select One"
                                                        runat="server" ControlToValidate="ddlSelectCity" ErrorMessage="Select City" ValidationGroup="userSend"
                                                        Display="None">                            
                                                    </cc1:RequiredFieldValidator>
                                                    <cc2:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator5"
                                                        runat="server">
                                                    </cc2:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="colRight" style="vertical-align: top;">
                                                    <asp:Label ID="Label1" runat="server" Width="126px" Text="Select Newsletter(s)" />
                                                </td>
                                                <td align="left" class="colLeft">
                                                    <table cellpadding="0" cellspacing="0" border="0" width="595px">
                                                        <tr>
                                                            <td width="592px">
                                                                <asp:UpdatePanel ID="gvUpdatepannel" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="pageGrid" runat="server" DataKeyNames="nlID" Width="592px" AutoGenerateColumns="False"
                                                                            AllowPaging="True" OnPageIndexChanging="pageGrid_PageIndexChanging" GridLines="None"
                                                                            OnRowDataBound="pageGrid_RowDataBound" AllowSorting="True" OnSorting="pageGrid_Sorting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="checkAll()" />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox runat="server" value='<% # Eval("nlID") %>' ID="RowLevelCheckBox" onclick="ChangeHeaderAsNeeded()" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="2%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <div style="display: none">
                                                                                            <asp:Label ID="lblgrdnlID" runat="server" Text='<% # Eval("nlID") %>' Visible="true"></asp:Label>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="2%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="72%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderTemplate>
                                                                                        <asp:LinkButton ID="lbtntitle" ForeColor="White" runat="server" Text="Title" CommandName="Sort"
                                                                                            CommandArgument="title"></asp:LinkButton>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <div>
                                                                                            <asp:Label ID="lbltitle" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString().Length > 112 ? DataBinder.Eval (Container.DataItem, "title").ToString().Substring(0,111) + "..." :DataBinder.Eval (Container.DataItem, "title").ToString()) %>'
                                                                                                ToolTip='<%# Server.HtmlEncode(DataBinder.Eval (Container.DataItem, "title").ToString()) %>'></asp:Label>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" Width="72%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="24%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                    <HeaderTemplate>
                                                                                        <asp:LinkButton ID="lblCardHeadcreationDate" ForeColor="White" runat="server" Text="Created Date"
                                                                                            CommandName="Sort" CommandArgument="creationDate"></asp:LinkButton>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <div>
                                                                                            <asp:Label ID="lblcreationDateText" Text='<% # GetDateString(Eval("creationDate")) %>'
                                                                                                runat="server"></asp:Label>
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
                                                                                    <div id="pagerSendEmail">
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family: Tahoma;
                                                                                            font-size: 11px; color: #666666;">
                                                                                            <tr>
                                                                                                <td width="30%" align="left" style="padding-left: 5px;">
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
                                                                                                        color: #666666; width: 100%">
                                                                                                        <tr>
                                                                                                            <td style="padding-right: 2px; padding-left: 6px;">
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
                                                                                                            <td style="padding-left: 4px; padding-right: 6px;">
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
                                                            </td>
                                                            <td width="3px" style="vertical-align:top;">
                                                                <asp:TextBox ID="hiddenIds" Width="0px" BorderWidth="0px" BackColor="#EFEFEF" ForeColor="#EFEFEF"
                                                                    Enabled="false" BorderStyle="None" runat="server">
                                                                </asp:TextBox>
                                                                <cc1:CustomValidator ID="cvGridView" runat="server" ControlToValidate="hiddenIds"
                                                                    ValidateEmptyText="true" ClientValidationFunction="validateGridView" SetFocusOnError="true"
                                                                    ValidationGroup="userSend" ErrorMessage="Please select the Newsletter(s) to send!"
                                                                    Display="None">
                                                                </cc1:CustomValidator>
                                                                <cc2:ValidatorCalloutExtender ID="vceGridView" runat="server" TargetControlID="cvGridView">
                                                                </cc2:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="colRight">
                                                </td>
                                                <td align="left" class="colLeft">
                                                 <asp:ImageButton ID="btnSend" ValidationGroup="userSend" runat="server" ImageUrl="~/admin/images/btnSend.gif"
                                                    OnClick="btnSend_Click" ToolTip="Send Email(s)" />&nbsp;
                                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="false" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                                                    OnClick="CancelButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="b">
                                <div class="b">
                                    <div class="b">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>

