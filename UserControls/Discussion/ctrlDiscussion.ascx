<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlDiscussion.ascx.cs"
    Inherits="UserControls_Discussion_ctrlDiscussion" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<script type="text/javascript">
    $(document).ready(function() {
     $('#ctl00_ContentPlaceHolder1_ctrlDiscussion1_txtComment').tipsy({ gravity: 's' });
         
      $("#ctl00_ContentPlaceHolder1_ctrlDiscussion1_hLinkSignIn").click(function(e) {
            e.preventDefault();
            $("fieldset#signin_menu").toggle();
            $(".signin").toggleClass("menu-open");
            $('html, body').animate({scrollTop:0}, 'slow');
            return false;
        }); 
         });
         
            function EmptyFieldvalidate(oSrc, args) {        
            if (args.Value != "") {                  
                args.IsValid = true;
                return;        
            }
            else
            {
                 $("#"+oSrc.controltovalidate).addClass("DiscussionError");                                                                                          
                 args.IsValid = false;
                 return; 
            }
                       
        }
        
         function validateEmptyField(txtComment) {   
                        
            if ($("#"+txtComment).val() != "") {                                 
                return true;        
            }
            else
            {
                 $("#"+txtComment).addClass("DiscussionError");                                                                                                           
                 return false; 
            }
           }
           
            function hideShowDiv(divID,imgID) {                                
               var Commentclass = document.getElementById(divID).getAttribute("class");              
               if(Commentclass  == "hideComment")      
               {                     
                    $("#"+divID).show('slow');                 
                    var textAreaID=divID.replace("pnlFooter","txtSubComment");                         
                    //$(window).scrollTop($("#"+divID).offset().top-300); 
                    $('html, body').animate({ scrollTop: $("#"+divID).offset().top -300}, 'slow')
                    $("#"+textAreaID).focus();    
                    document.getElementById(divID).setAttribute("class","showComment")                
                    $("#"+imgID).attr("src", "images/hide_comment.png");

              }
              else
              {
                $("#"+divID).hide('slow'); 
                document.getElementById(divID).setAttribute("class","hideComment");                              
                $("#"+imgID).attr("src", "images/comment_reply.png");
              }              
           }
                                                           
        
</script>

<div style="width: 730px; border: 1px solid #B7B7B7; clear: both;" class="fontSpaceHeightRegular">
    <asp:HiddenField ID="hfDealId" runat="server" />
    <asp:UpdatePanel ID="upComment" runat="server">
        <ContentTemplate>
            <div>
                <asp:DataList ID="rptrDiscussion" RepeatColumns="1" RepeatDirection="Vertical" runat="server"
                    CellPadding="0" OnItemDataBound="DataListItemDataBound" CellSpacing="0" Width="730px"
                    GridLines="None" ShowHeader="false">
                    <ItemTemplate>
                        <div style="border-bottom: solid 1px #B7B7B7; background-color: #f0f8fe; width: auto;
                            padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                            <div style="width: 120px; float: left; text-align: center">
                                <asp:Image ID="imgDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px" BorderStyle="Solid"
                                    ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("userId")%>' />
                                <asp:HiddenField ID="hfDiscuessionID" runat="server" Value='<%# Eval("discussionId")%>' />
                            </div>
                            <div style="width: 540px; float: left; text-align: left;">
                                <div style="width: 540px; height: 26px;">
                                    <div style="float: left;">
                                        <asp:Label ID="label5" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text='<%# Eval("Name") %>'
                                            Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label></div>
                                    <div style="float: left; padding-left:10px;">
                                        <asp:Label ID="label6" runat="server" Font-Names="Arial, Helvetica, sans-serif" Text='<%# "Commented " + Eval("days") + " days, " + Eval("hour") + " hours and " + Eval("min") + " minutes ago" %>'
                                            Font-Bold="True" Font-Size="13px" ForeColor="#0976c3"></asp:Label></div>
                                    <div style="float: right;">
                                        <asp:Image ID="imgCommentReply" ToolTip="Click here to reply" Style="cursor: pointer;"
                                            runat="server" ImageUrl="~/Images/comment_reply.png" />
                                    </div>
                                </div>
                                <div style="width: 540px; padding-right: 12px;">
                                    <asp:Label ID="label7" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text='<%# Eval("comments")%>'
                                        Font-Size="13px" ForeColor="#7C7B7B"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both; background-color: #f0f8fe; width: auto;">
                            <div style="float: right;">
                                <asp:DataList ID="rptrSubDiscussion" RepeatColumns="1" RepeatDirection="Vertical"
                                    runat="server" CellPadding="0" OnItemDataBound="SubCommentDataListItemDataBound"
                                    CellSpacing="0" Width="730px" GridLines="None" OnItemCommand="rptrSubDiscussion_ItemCommand"
                                    ShowHeader="false">
                                    <ItemTemplate>
                                        <div style="border-bottom: solid 1px #B7B7B7; background-color: #f0f8fe; width: auto;
                                            padding-left: 100px; padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                                            <div style="width: 120px; float: left; text-align: center">
                                                <asp:Image ID="imgSubDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px"
                                                    BorderStyle="Solid" ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                                                <asp:HiddenField ID="hfSubCommentUserID" runat="server" Value='<%# Eval("userId")%>' />
                                                <asp:HiddenField ID="hfSubDiscuessionID" runat="server" Value='<%# Eval("discussionId")%>' />
                                            </div>
                                            <div style="width: 450px; float: left; text-align: left;">
                                                <div style="width: 450px; height: 26px;">
                                                    <asp:Label ID="sublabel5" runat="server" Font-Names="Arial,Helvetica,sans-serif"
                                                        Text='<%# Eval("Name") %>' Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                            ID="sublabel6" runat="server" Font-Names="Arial, Helvetica, sans-serif" Text='<%# "Commented " + Eval("days") + " days, " + Eval("hour") + " hours and " + Eval("min") + " minutes ago" %>'
                                                            Font-Bold="True" Font-Size="13px" ForeColor="#0976c3"></asp:Label></div>
                                                <div style="width: 450px; padding-right: 12px;">
                                                    <asp:Label ID="sublabel7" runat="server" Font-Names="Arial,Helvetica,sans-serif"
                                                        Text='<%# Eval("comments")%>' Font-Size="13px" ForeColor="#7C7B7B"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Panel ID="pnlFooter" runat="server" CssClass="hideComment" Style="display: none;">
                                            <div style="border-bottom: solid 1px #B7B7B7; background-color: #f0f8fe; width: auto;
                                                padding-top: 10px; padding-bottom: 10px; overflow: auto;">
                                                <div style="clear: both;">
                                                    <div style="width: 500px; float: right;">
                                                        <asp:TextBox ID="txtSubComment" title="Add Comments" onfocus="this.className=''"
                                                            Width="475px" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="clear: both; padding-top: 10px;">
                                                    <div style="float: right; padding-right: 23px;">
                                                        <asp:ImageButton ID="btnSubCommentPost" CommandName="addComment" CommandArgument='<%# Eval ("pdiscussionId") %>'
                                                            runat="server" ImageUrl="~/Images/post.gif" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </FooterTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="height: 230px; border-bottom: solid 1px #B7B7B7; background-color: #f5f5f5;
                width: auto">
                <div style="padding-left: 100px; clear: both; padding-top: 5px;">
                    <div style="float: left; padding-right: 5px;">
                        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="Images/error.png" />
                    </div>
                    <div>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" Visible="false" CssClass="fontStyle"></asp:Label>
                    </div>
                </div>
                <div style="padding-left: 20px; clear: both; padding-top: 10px; padding-bottom: 10px;">
                    <asp:ImageButton ID="hLinkSignIn" runat="server" ImageUrl="~/Images/btnSingInComment.jpg">
                    </asp:ImageButton></div>
                <div style="width: 730px; clear: both;">
                    <div style="width: 100px; float: left; padding-left: 20px;">
                        <div style="clear: both;">
                            <asp:Label ID="label3" runat="server" Font-Names="Arial,Helvetica,sans-serif" Text="Comment"
                                Font-Size="15px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>
                        </div>
                        <div style="clear: both; padding-top: 10px;">
                            <asp:Image ID="imgLoginUser" runat="server" BorderColor="#F99D1C" BorderWidth="2px"
                                BorderStyle="Solid" ImageUrl="~/Images/disImg.gif" Width="62px" Height="62px" />
                        </div>
                    </div>
                    <div style="width: 600px; float: right;">
                        <asp:TextBox ID="txtComment" title="Add Comments" onfocus="this.className=''" Width="575px"
                            Height="103px" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <cc1:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="EmptyFieldvalidate"
                            ControlToValidate="txtComment" Display="none" ValidateEmptyText="true" ValidationGroup="vgComments"
                            ErrorMessage="" SetFocusOnError="false"></cc1:CustomValidator>
                    </div>
                </div>
                <div style="width: 730px; clear: both;">
                    <div style="clear: both; padding-top: 20px;">
                        <div style="float: right; padding-right: 23px;">
                            <asp:ImageButton ID="btnPost" runat="server" ImageUrl="~/Images/post.gif" ValidationGroup="vgComments"
                                CausesValidation="true" OnClick="btnPost_Click" />
                        </div>
                    </div>
                    <div style="width: 63px; float: right;">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
