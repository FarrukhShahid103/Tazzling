<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlDiscussion.ascx.cs"
    Inherits="UserControls_Discussion_ctrlDiscussion" %>
<%@ Register Assembly="Validators" Namespace="Sample.Web.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<link href="CSS/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
<link href="CSS/confirm.css" rel="stylesheet" type="text/css" />

<script src="JS/jquery-1.4.0.min.js" type="text/javascript"></script>

<script src="JS/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>

<script src="JS/jquery.simplemodal.js" type="text/javascript"></script>

<script src="JS/confirm.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    var _EmailAddress;
    function SaveUserNotes() {
        var Notes = escape($("#txtUserNotes").val());
       
        if (Notes == "") {
            alert("Please Enter Message to send email");
            return false;
        }
        else if ($("#txtUserNotes").val().length < 50) {
            alert("Your message must be grater then 50 character");
            return false;
        }

        $.ajax({
            type: "POST",
            url: "../getStateLocalTime.aspx?SendEmailToUser=" + _EmailAddress + "&Message=" + Notes,
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: false,
            success: function (msg) {
               if (msg == "true") {
                   return true;
                    
                }
               else {
                   return false;
                }
            }
        });

        return true;

    }
    function RunPopup(ButtonID, EmailAddress) {
    _EmailAddress = EmailAddress;
    $('#hfButtonID').val(ButtonID);
    RunPopup2(function () {
        if (SaveUserNotes()) {
            $("#" + ButtonID).click();
            return true;
        }
        else {
           
        }

    });
        return false;
    }
           
    function RunPopup2(callback) {
       
        $('#confirm').modal({
            closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
            position: ["20%", ],
            overlayId: 'confirm-overlay',
            containerId: 'confirm-container',
            onShow: function (dialog) {
                var modal = this;                
                
                 $('.RemoveWithoutEmail', dialog.data[1]).click(function () {
                    // call the callback                                                     
                           $("#" + $('#hfButtonID').val()).click();
                            modal.close();
                             return true;                                                                
                });
                
                // if the user clicks "yes"
                $('.yes', dialog.data[0]).click(function () {
                    // call the callback
                    if ($.isFunction(callback)) {
                        if (callback.apply()) {
                            modal.close();
                        }
                    }
                   
                });
            }
        });
    }	
</script>

<script type="text/javascript">


    function ConfirmAlert(Type, Message) {
        MyAlert(Type, Message);
        $('#BoxConfirmBtnOk').click(function (event) {
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
        $('#fancybox-close').click(function (event) {
            alert('Clicked');
            return false;
        });
    }


    $(document).ready(function () {
        $('#fancybox-close').click(function (event) {
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
<br />
<asp:Label ID="label2" runat="server" Font-Names="Arial,sans-serif" Text="Discussion"
    Font-Size="35px" Font-Bold="true" ForeColor="#97C717" Font-Underline="True"></asp:Label>
<br />
<br />
<input type="hidden" id="hfButtonID" />
<div style="padding-left: 4px; text-align: left;">
    <div style="float: left; padding-right: 5px">
        <asp:Image ID="imgGridMessage" runat="server" Visible="false" ImageUrl="Images/error.png" />
    </div>
    <div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Black" Visible="false" CssClass="fontStyle"></asp:Label>
    </div>
</div>
<br />
<div style="width: 783px; border: 1px solid #B7B7B7;" class="fontSpaceHeightRegular">
    <div style="height: 34px; border-bottom: solid 1px #B7B7B7; background-color: #F5F5F5;
        padding-left: 19px; padding-top: 11px; width: auto;">
        <asp:Label ID="label1" runat="server" Font-Names="Arial, Arial, sans-serif" Text="Edit Comment"
            Font-Bold="True" Font-Size="19px" ForeColor="#97C717"></asp:Label><asp:HiddenField
                ID="hfDealId" runat="server" />
    </div>
    <div style="height: 230px; border-bottom: solid 1px #B7B7B7; background-color: #F5F5F5;
        width: auto">
        <div style="width: 781px; padding-top: 15px;">
            <div style="width: 100px; float: left; padding-left: 41px;">
                <asp:Label ID="label3" runat="server" Font-Names="Arial,sans-serif" Text="Comment"
                    Font-Size="15px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>
            </div>
            <div style="width: 640px; float: right;">
                <asp:TextBox ID="txtComment" Width="575px" Height="103px" TextMode="MultiLine" runat="server"></asp:TextBox>
                <cc1:RequiredFieldValidator ID="rfvComments" SetFocusOnError="true" runat="server"
                    ControlToValidate="txtComment" ErrorMessage="Comments required!" ValidationGroup="vgComments"
                    Display="None">                            
                </cc1:RequiredFieldValidator>
                <cc2:ValidatorCalloutExtender ID="vcdComments" runat="server" TargetControlID="rfvComments">
                </cc2:ValidatorCalloutExtender>
            </div>
        </div>
        <div style="width: 781px;">
            <div style="width: 716px; float: left; text-align: right; padding-top: 26px;">
                <div style="float: right; padding-left: 15px;">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/admin/Images/btnConfirmCancel.gif"
                        ValidationGroup="vgComments" CausesValidation="false" OnClick="btnCancel_Click" />
                </div>
                <div style="float: right;">
                    <asp:ImageButton ID="btnPost" runat="server" ImageUrl="~/admin/Images/btnUpdate.jpg"
                        ValidationGroup="vgComments" CausesValidation="true" OnClick="btnPost_Click" />
                </div>
            </div>
            <div style="width: 63px; float: right;">
            </div>
        </div>
    </div>
    <asp:DataList ID="rptrDiscussion" RepeatColumns="1" RepeatDirection="Vertical" DataKeyField="discussionId"
        runat="server" CellPadding="0" OnItemDataBound="DataListItemDataBound" OnEditCommand="Edit_Command"
        OnDeleteCommand="Delete_Command" CellSpacing="0" Width="781px" GridLines="None"
        ShowHeader="false">
        <ItemTemplate>
            <div style="border-bottom: solid 1px #B7B7B7; background-color: #FFFFFF; width: auto;
                padding-top: 19px; padding-bottom: 19px; overflow: auto;">
                <div style="width: 141px; float: left; text-align: center">
                    <asp:Label ID="lbldiscussionId" runat="server" Visible="false" Text='<%# Eval("discussionId")%>'></asp:Label>
                    <asp:Image ID="imgDis" runat="server" BorderColor="#F99D1C" BorderWidth="2px" BorderStyle="Solid"
                        ImageUrl='<%# Eval("profilePicture") %>' Width="62px" Height="62px" />
                    <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("userId")%>' />
                </div>
                <div style="width: 640px; float: right; text-align: left;">
                    <div style="width: 640px; height: 26px;">
                        <div style="float: left; width: 550px; padding-right: 10px;">
                            <asp:Label ID="label5" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("Name")%>'
                                Font-Size="16px" ForeColor="#F99D1C" Font-Bold="True"></asp:Label>&nbsp;&nbsp;<asp:Label
                                    ID="label6" runat="server" Font-Names="Arial, Arial, sans-serif" Text='<%# "("+Eval("userName")+")" %>'
                                    Font-Bold="True" Font-Size="13px" ForeColor="#97C717"></asp:Label>
                        </div>
                        <div style="float: left; width: 80px;">
                            <div style="float: left; padding-right: 10px;">
                                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Edit Comment" CommandArgument='<%# Eval("discussionId") %>'
                                    CommandName="Edit" ImageUrl="~/admin/Images/edit.gif" />
                            </div>
                            <div style="float: left;">
                                <asp:HiddenField ID="hfEmail" runat="server" Value='<%# Eval("userName") %>' />
                                <asp:ImageButton ID="Delete" runat="server" ImageUrl="~/admin/Images/delete.gif"
                                    OnClientClick='return confirm("Are you sure you want to delete this comment?");'
                                    ToolTip="Delete Comment" />
                                <div style="display: none;">
                                    <asp:Button ID="BtnHidden" runat="server" CommandArgument='<%# Eval("discussionId") %>'
                                        CommandName="Delete" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 628px; padding-right: 12px;">
                        <asp:Label ID="label7" runat="server" Font-Names="Arial,sans-serif" Text='<%# Eval("comments")%>'
                            Font-Size="13px" ToolTip='<%# Eval("comments")%>' ForeColor="#7C7B7B"></asp:Label>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
</div>
<div id="confirm-container" class="simplemodal-container" style="position: fixed;
    display: none; height: auto !important; width: auto !important; z-index: 1002;
    left: 25% !important; top: 20%; overflow: hidden;">
    <a class="modal-close simplemodal-close" title="Close" href="#">x</a>
    <div tabindex="-1" style="outline: 0px none; border: 5px solid red; overflow: hidden;">
        <div id="confirm" class="simplemodal-data" style="display: block;">
            <div class="header">
                <span>Please Enter Text to Email</span></div>
            <p>
                <div style="margin: 13px;">
                    <textarea name="styled-textarea" id="txtUserNotes"></textarea>
                </div>
            </p>
            <div style="float: left !important;" class="buttons2">
                <div class="no simplemodal-close">
                    Cancel</div>
                <div class="no simplemodal-close RemoveWithoutEmail">
                    Remove</div>
                <div class="yes">
                    Send</div>
            </div>
        </div>
    </div>
</div>
<br />
