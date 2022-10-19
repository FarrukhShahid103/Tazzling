<%@ Page Language="C#" MasterPageFile="~/admin/adminTastyGo.master" AutoEventWireup="true"
    CodeFile="createNewsLetterforCampaignMonitor.aspx.cs" Inherits="createNewsLetterforCampaignMonitor"
    Title="Create News Letter For Campaign Monitor" %>

<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="JS/jquery-1.4.0.min.js"></script>

    <link href="CSS/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function pageLoad() {            
     $("#ctl00_ContentPlaceHolder1_txtdlStartDate").click(function(){
            var Cal = window.document.getElementById("ctl00_ContentPlaceHolder1_txtdlStartDate");
            showCalendarControl(Cal);                                          
    });
    
     $("#ctl00_ContentPlaceHolder1_txtCampaignNewsLetterDate").click(function(){
            var Cal = window.document.getElementById("ctl00_ContentPlaceHolder1_txtCampaignNewsLetterDate");
            showCalendarControl(Cal);                                          
    });           
    }
    </script>

    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlForm" runat="server" Visible="true">
                <div style="clear: both;">
                    <div style="float: left; padding-right: 5px">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="~/Images/error.png" />
                    </div>
                    <div class="floatLeft">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div id="divSrchFields" runat="server">
                    <div id="searchBig">
                        <div style="clear: both;">
                            <div class="heading" style="float: left;">
                                <asp:Label ID="Label3" runat="server" Text="NewsLetter Name"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 10px;">
                                <asp:DropDownList ID="ddlSearchCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCity_SelectedIndexChanged"
                                    Width="192px">                                                
                                    <asp:ListItem Text="Abbotsford" Value="98dda76b7a23901888451f2a87081b28|1710"></asp:ListItem>
                                    <asp:ListItem Text="Surrey" Value="ea499736c8dfc227a81511c30fa16ca6|1712"></asp:ListItem>
                                    <asp:ListItem Text="Victoria" Value="71c5249d1aaa13c696b10aebbde7dddd|1713"></asp:ListItem>
                                    <asp:ListItem Selected="True" Text="Vancouver" Value="5ded7bafacc4ec6e0c58eae5429fb0ae|337"></asp:ListItem>                                                                        
                                    <asp:ListItem Text="Calgary" Value="57b1c18efcd4a489be956cef28df7ab6|1376"></asp:ListItem>
                                    <asp:ListItem Text="Edmonton" Value="140556b11bfa3f7075df5db55ce97749|1709"></asp:ListItem>                                    
                                    <asp:ListItem Text="Halifax" Value="142c46265b5cf54d50faeb1cb93a58b6|1716"></asp:ListItem>                                    
                                    <asp:ListItem Text="Brampton" Value="257ae9d7c53ced27772e0f7468eb8c56|1720"></asp:ListItem>
                                    <asp:ListItem Text="Hamilton" Value="8db2753c4cf483b0dd5ee29ef7619d3a|1722"></asp:ListItem>
                                    <asp:ListItem Text="Mississauga" Value="4f1c53c0ab2572319474d78d570070b3|1726"></asp:ListItem>
                                    <asp:ListItem Text="Oakville - Burlington" Value="d15ee73cf8443219031eb3d9c3ccab47|1727"></asp:ListItem>
                                    <asp:ListItem Text="St. Catharines" Value="d829cb465bf554dac084d767d155506e|1729"></asp:ListItem>                                                                                                           
                                    <asp:ListItem Text="Toronto" Value="1d1cf4cc7d49c20fd89eff3493c6f1ce|338"></asp:ListItem>
                                    <asp:ListItem Text="York Region" Value="0a628e9eda46d1be9c06d2cd08b7f499|1733"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </div>
                            <div class="heading" style="float: left; padding-left: 10px;">
                                <asp:Label ID="Label1" runat="server" Text="Select Deal" Width="92px"></asp:Label>
                            </div>
                            <div style="float: left; padding-left: 10px;">
                                <asp:DropDownList ID="ddlSearchDeal" runat="server" Width="192px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="clear: both; padding-top: 8px;">
                            <div style="float: left;">
                                <div style="float: left;">
                                    <asp:Label ID="lbldlStartTime" runat="server" Text="Select Time"></asp:Label>
                                </div>
                                <div style="padding-left: 10px; float: left;">
                                    <asp:TextBox ID="txtdlStartDate" runat="server" CssClass="txtForm" Width="92px" MaxLength="12"></asp:TextBox>
                                    <asp:DropDownList ID="ddlDLStartHH" runat="server">
                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDLStartMM" runat="server">
                                        <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                        <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                        <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                        <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                        <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                        <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                        <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                        <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                        <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                        <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                        <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                        <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                        <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                        <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                        <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                        <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                        <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                        <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                        <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                        <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                        <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                        <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                        <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                        <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                        <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                        <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDLStartPortion" runat="server">
                                        <asp:ListItem Selected="True" Text="AM" Value="AM"></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="float: left; padding-left: 10px;">
                                <div style="float: left;">
                                    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/admin/Images/btnUpdate.jpg"
                                        OnClick="btnSearch_Click" TabIndex="1" />
                                </div>
                                <div style="float: left; padding-left: 10px;">
                                    <asp:UpdatePanel ID="updownloadExcel" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/admin/Images/btnSave.jpg"
                                                OnClick="btnSave_Click" TabIndex="2" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="imgbtnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both; padding-top: 5px;">
                    &nbsp;</div>            
                <div id="searchBig" style="background-repeat:repeat-y repeat-x; height:100px;">
                    <div style="clear: both;">                       
                        <div class="heading" style="float: left;">
                            <asp:Label ID="Label6" runat="server" Text="Name"></asp:Label>
                        </div>
                        <div style="float: left; padding-left: 10px;">
                            <asp:TextBox ID="txtCampaignNewsLetterName" runat="server" TextMode="MultiLine" Height="60px" Width="270px"></asp:TextBox>
                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCampaignNewsLetterName"
                                ErrorMessage="News letter name required." ValidationGroup="CampaignNewsLetter" Display="None"
                                SetFocusOnError="true"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3">
                            </cc2:ValidatorCalloutExtender>
                        </div>
                        <div class="heading" style="float: left; padding-left: 10px;">
                            <asp:Label ID="Label7" runat="server" Text="Subject"></asp:Label>
                        </div>
                        <div style="float: left; padding-left: 10px;">
                            <asp:TextBox ID="txtCampaignNewsLetterSubject" TextMode="MultiLine" Height="60px"  runat="server" Width="270px"></asp:TextBox>
                            <cc1:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCampaignNewsLetterSubject"
                                ErrorMessage="News letter subject required." ValidationGroup="CampaignNewsLetter" Display="None"
                                SetFocusOnError="true"></cc1:RequiredFieldValidator>
                            <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator4">
                            </cc2:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div style="clear: both; padding-top: 8px;">
                        <div style="float: left;">
                            <div style="float: left;">
                                <asp:Label ID="Label8" runat="server" Text="Select Time"></asp:Label>
                            </div>
                            <div style="padding-left: 10px; float: left;">
                                <asp:TextBox ID="txtCampaignNewsLetterDate" runat="server" CssClass="txtForm" Width="92px"
                                    MaxLength="12"></asp:TextBox>
                                <div style="float: left; padding-left: 10px;">
                                    <cc1:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCampaignNewsLetterDate"
                                        ErrorMessage="News letter subject required." ValidationGroup="CampaignNewsLetter" Display="None"
                                        SetFocusOnError="true"></cc1:RequiredFieldValidator>
                                    <cc2:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator5">
                                    </cc2:ValidatorCalloutExtender>
                                </div>
                                <asp:DropDownList ID="ddCampaignNewsLetterHours" runat="server">
                                    <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddCampaignNewsLetterMinutes" runat="server">
                                    <asp:ListItem Selected="True" Text="00" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                    <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                    <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                    <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                    <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                    <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                    <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                    <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                    <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                    <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                    <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                    <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                    <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                    <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                    <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                    <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                    <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                    <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                    <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                    <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                    <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                    <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddCampaingNewsLetterTimeSpan" runat="server">
                                    <asp:ListItem Selected="True" Text="AM" Value="AM"></asp:ListItem>
                                    <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="float: left; padding-left: 10px;">
                            <div style="float: left;">
                                <asp:ImageButton ID="btnCampaignMonitor" runat="server" CausesValidation="true" ValidationGroup="CampaignNewsLetter"
                                    ImageUrl="~/admin/Images/btnSend.gif" OnClick="btnCampaignMonitor_Click" TabIndex="1" />
                            </div>
                        </div>
                    </div>
                </div>
                 <div style="clear: both; padding-top: 5px;">
                    &nbsp;</div>
                <div style="clear: both;">
                    <div class="heading" style="float: left;">
                        <asp:Label ID="Label2" runat="server" Text="Announcement"></asp:Label>
                    </div>
                    <div style="float: left; padding-left: 10px;">
                        <asp:TextBox ID="txtExtraText" runat="server" TextMode="MultiLine" Width="700px"
                            Height="140px"></asp:TextBox>
                    </div>
                </div>                
                <div class="floatLeft" style="padding-top: 15px; padding-left: 4px;">
                    <div style="clear: both;" align="center">
                        <asp:Literal ID="ltDealDetail" runat="server" Text=""></asp:Literal>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
