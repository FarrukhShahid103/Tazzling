<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlClientMenu.ascx.cs"
    Inherits="Takeout_UserControls_Menu_ctrlClientMenu" %>
<div style="border: solid 1px #d1d0d0; padding:3px; height:51px;">
    <div id="menuZone">
        <%-- <div class="menuLeft">
    </div>--%>
        <div id="menuh" class="menuMiddle">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="menuHover">
                        <ul>
                            <li><a href="Default.aspx" onmouseover="mopen('m1')" style="text-decoration: none;"
                                onmouseout="mclosetime()" class="top_parent">Daily Deal </a>
                                <ul id="sddm" class="sddm">
                                    <li>
                                        <div style="clear: both; width: 200px;display:none;" id="m1" onmouseover="mcancelclosetime()"
                                            onmouseout="mclosetime()">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%"  style="margin-top:4px; background-color: #c96201;">                                               
                                                <tr>                                                   
                                                    <td style="background-color: #c96201; padding: 8px 0px 0px 0px;" >
                                                        <asp:Literal ID="ltProvinces" runat="server"></asp:Literal>
                                                    </td>                                                    
                                                </tr>                                               
                                            </table>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </td>
                    <td>
                        <div class="menu_s">
                        </div>
                    </td>
                    <td class="menuHover">
                        <ul>
                            <li><a href="howItWorks.aspx" onmouseover="mopen('Div1')" style="text-decoration: none;"
                                onmouseout="mclosetime()" class="top_parent">How it works</a>
                                <ul id="Ul1" class="sddm">
                                    <li>
                                        <div style="clear: both; width: 200px;display:none;" id="Div1" onmouseover="mcancelclosetime()"
                                            onmouseout="mclosetime()">
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin-top:4px;background-color: #c96201;">
                                               
                                                <tr>                                                   
                                                    <td style="background-color: #c96201; padding: 8px 0px 0px 0px;">
                                                        <asp:Literal ID="ltCuisine" runat="server"></asp:Literal>
                                                    </td>                                                   
                                                </tr>                                              
                                            </table>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </td>
                    <td>
                        <div class="menu_s">
                        </div>
                    </td>
                    <td class="menuHover">
                        <ul>
                            <li><a href="app.aspx" onmouseover="mopen('Div2')" style="text-decoration: none;"
                                onmouseout="mclosetime()" class="top_parent">App </a>
                                <ul id="Ul2" class="sddm">
                                    <li>
                                        <div style="clear: both; width: 200px;display:none;" id="Div2" onmouseover="mcancelclosetime()"
                                            onmouseout="mclosetime()">
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin-top:4px;background-color: #c96201;">                                              
                                                <tr>
                                                   
                                                    <td style="background-color: #c96201; padding: 8px 0px 0px 0px;">                                                        
                                                    </td>
                                                    
                                                </tr>
                                               
                                            </table>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </td>
                    <td>
                        <div class="menu_s">
                        </div>
                    </td>
                    <td class="menuHover">
                        <ul>
                            <li><a href="GiftCard.aspx" onmouseover="mopen('Div3')" style="text-decoration: none;"
                                onmouseout="mclosetime()" class="top_parent">Gift Card </a>
                                <ul id="Ul3" class="sddm">
                                    <li>
                                        <div style="clear: both; width: 200px;display:none;" id="Div3" onmouseover="mcancelclosetime()"
                                            onmouseout="mclosetime()">
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin-top:4px;background-color: #c96201;">
                                                
                                                <tr>                                                   
                                                    <td style="background-color: #c96201; padding: 8px 0px 0px 0px;">                                                        
                                                    </td>                                                   
                                                </tr>                                               
                                            </table>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </td>
                    <td>
                        <div class="menu_s">
                        </div>
                    </td>
                    <td class="menuHover">
                        <ul>
                            <li><a href="pastDeals.aspx" onmouseover="mopen('Div4')" style="text-decoration: none;"
                                onmouseout="mclosetime()" class="top_parent">Past Deals </a>
                                <ul id="Ul4" class="sddm">
                                    <li>
                                        <div style="clear: both; width: 200px;display:none;" id="Div4" onmouseover="mcancelclosetime()"
                                            onmouseout="mclosetime()">
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin-top:4px;background-color: #c96201;">
                                               
                                                <tr>
                                                   
                                                    <td style="background-color: #c96201; padding: 8px 0px 0px 0px;">
                                                      
                                                    </td>
                                                  
                                                </tr>                                                
                                            </table>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </td>                   
                    <td>
                        <div class="menu_s">
                        </div>
                    </td>
                    <td class="menuHover">
                        <ul>
                            <li><a href="javascript:void(0)" onmouseover="mopen('Div5')" style="text-decoration: none;"
                                onmouseout="mclosetime()" class="top_parent">About </a>
                                <ul id="Ul5" class="sddm">
                                    <li>
                                        <div style="clear: both; width: 138px;" id="Div5" onmouseover="mcancelclosetime()"
                                            onmouseout="mclosetime()">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 4px;
                                                background-color: #c96201;">
                                                <tr>
                                                    <td style="background-color: #c96201; padding: 8px 0px 0px 0px;">
                                                        <a href="<%=strMySite %>/contact-us.aspx" style='color: white; font-family: Arial;
                                                            font-size: 12px; font-weight: bold; text-align: left; border-bottom: 1px solid #b45801;
                                                            width: 138px;'>&nbsp;&nbsp;Contact</a> 
                                                         <a href="<%=strMySite %>/jobs.aspx" style='color: white;
                                                                font-family: Arial; font-size: 12px; font-weight: bold; text-align: left; border-bottom: 1px solid #b45801;
                                                                width: 138px;'>&nbsp;&nbsp;Jobs</a> 
                                                                 <a href="<%=strMySite %>/faq.aspx" style='color: white;
                                                                font-family: Arial; font-size: 12px; font-weight: bold; text-align: left; border-bottom: 1px solid #b45801;
                                                                width: 138px;'>&nbsp;&nbsp;FAQ</a> 
                                                          <a href="<%=strMySite %>/affiliate.aspx" style='color: white;
                                                                    font-family: Arial; font-size: 12px; font-weight: bold; text-align: left; border-bottom: 1px solid #b45801;
                                                                    width: 138px;'>&nbsp;&nbsp;Affiliate</a> 
                                                          <a href="<%=strMySite %>/featureBusiness.aspx"
                                                                        style='color: white; font-family: Arial; font-size: 12px; font-weight: bold;
                                                                        text-align: left; border-bottom: 1px solid #b45801; width: 138px;'>&nbsp;&nbsp;Feature
                                                                        Business</a> 
                                                          <a href="<%=strMySite %>/suggestBusiness.aspx" style='color: white;
                                                                            font-family: Arial; font-size: 12px; font-weight: bold; text-align: left; border-bottom: 1px solid #b45801;
                                                                            width: 138px;'>&nbsp;&nbsp;Suggest a Business</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </td>
                </tr>
            </table>
        </div>
        <%--<div class="menuRight">
    </div>--%>
    </div>
</div>
