using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using GecLibrary;
using System.Text;
using System.Net.Mail;
using System.Drawing;
using System.Text.RegularExpressions;
public partial class orderComplete : System.Web.UI.Page
{
    BLLDealOrders objOrder = new BLLDealOrders();
    BLLUser objUser = new BLLUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["title"] != null)
        {
            Page.Title = ViewState["title"].ToString();
        }

        if (!IsPostBack)
        {            
            if (Request.QueryString["oid"] != null && Request.QueryString["oid"].ToString() != ""
                && Request.QueryString["od"] != null && Request.QueryString["od"].ToString() != "")
            {

                string[] strODetail = Request.QueryString["od"].ToString().Split('_');
                if (strODetail.Length == 3)
                {
                    objOrder = new BLLDealOrders();
                    objOrder.dOrderID = Convert.ToInt64(Request.QueryString["oid"].ToString());
                    DataTable dtOrderinfo = objOrder.getProductOrderDetailByOrderID();
                    if (dtOrderinfo != null && dtOrderinfo.Rows.Count > 0)
                    {
                        lblTastyCreditUsed.Text = (Convert.ToDouble(strODetail[1].Trim()) + Convert.ToDouble(strODetail[2].Trim())).ToString();
                        lblCreditCardUsed.Text = Convert.ToDouble(strODetail[0].Trim()).ToString();
                        if (Session["dtProductCartNew"] != null)
                        {
                            try
                            {
                                DataTable dtProductCartNew = (DataTable)Session["dtProductCartNew"];
                                gridview1.DataSource = dtProductCartNew.DefaultView;
                                gridview1.DataBind();
                                resetAmounts();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                  
                        ViewState["userId"] = dtOrderinfo.Rows[0]["userId"].ToString().Trim();
                        string strUid = (Convert.ToInt64(dtOrderinfo.Rows[0]["userId"].ToString().Trim()) + 111111).ToString();
                        linkFacebook1.HRef = "http://www.facebook.com/sharer.php?u="
                        + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                        + "/r/" + strUid + "_" + dtOrderinfo.Rows[0]["dealId"].ToString().Trim()));
                        linkTweeter1.HRef = "http://twitter.com/share?url="
                        + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                        + "/r/" + strUid + "_" + dtOrderinfo.Rows[0]["dealId"].ToString().Trim()));

                        Page.Title = getCityName() + "'s Tasty Daily Deal";
                        ViewState["title"] = Page.Title;                        
                        ViewState["dealId"] = dtOrderinfo.Rows[0]["dealId"].ToString().Trim();                       
                                              
                        string[] strDealImages = dtOrderinfo.Rows[0]["images"].ToString().Split(',');
                        txtShareLink.Text = ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                        + "/r/" + strUid + "_" + dtOrderinfo.Rows[0]["dealId"].ToString().Trim());

                        int i = 0;
                        bool imageFound = false;
                        string strFBIMage = "";
                        if (strDealImages.Length > 0)
                        {
                            for (i = 0; i < strDealImages.Length; i++)
                            {
                                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strDealImages[i]))
                                {
                                    imageFound = true;
                                    break;
                                }
                            }
                        }
                        if (imageFound)
                        {
                            strFBIMage = ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i];
                            string strNewFileName = "123" + strDealImages[i];
                            watermark.waterMark(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strDealImages[i], AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strNewFileName);
                           
                        }
                        else
                        {
                            strFBIMage = ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/logo.png";
                            
                        }

                        if (dtOrderinfo.Rows[0]["FB_userID"].ToString().Trim() != "" && Convert.ToBoolean(dtOrderinfo.Rows[0]["FB_Share"].ToString().Trim()) == true)
                        {

                            ltFacebookSharing.Text += " <script>";
                            ltFacebookSharing.Text += "FB.init({appId: '" + ConfigurationManager.AppSettings["Application_ID"].ToString() + "', status: true,";
                            ltFacebookSharing.Text += "cookie: true, xfbml: true,oauth : true});";
                            ltFacebookSharing.Text += "FB.api('/" + dtOrderinfo.Rows[0]["FB_userID"].ToString() + "/feed', 'post', { message: 'I have Purchased this Amazing Deal on Tazzling.com'";
                            ltFacebookSharing.Text += ",name:'" + dtOrderinfo.Rows[0]["title"].ToString().Trim() + "'";
                            ltFacebookSharing.Text += ",description:'" + StripHTML(dtOrderinfo.Rows[0]["description"].ToString()).Replace('\r', ' ').Replace('\n', ' ').Replace('"', '\"') + "'";

                            ltFacebookSharing.Text += ",link:'" + ConfigurationManager.AppSettings["YourSite"].ToString()
                            + "/r/" + strUid + "'";
                            ltFacebookSharing.Text += ",picture:'" + strFBIMage + "'}, function(response) {";
                            ltFacebookSharing.Text += "if (!response || response.error) {";
                            //ltFacebookSharing.Text += "alert('Error occured');";
                            ltFacebookSharing.Text += "} else {";
                            //ltFacebookSharing.Text += "alert('Post ID: ' + response.id);";
                            ltFacebookSharing.Text += "}});";
                            ltFacebookSharing.Text += "</script>";
                        }

                    }

                }
            }
        }
               
    }

    private void resetAmounts()
    {
        try
        {
            if (Session["dtProductCartNew"] != null)
            {               
                DataTable dtProductCartNew = (DataTable)Session["dtProductCartNew"];
                int Qty = 0;
                if (dtProductCartNew != null && dtProductCartNew.Rows.Count > 0)
                {
                    if (dtProductCartNew != null)
                    {
                        object sumObject;
                        sumObject = dtProductCartNew.Compute("Sum(Qty)", "Qty > 0");
                        int.TryParse(sumObject.ToString(), out Qty);                       
                    }                    
                    double shippingAmount = 0;
                    double totalDealPrice = 0;
                    bool shippingAndTax = false;
                    for (int i = 0; i < dtProductCartNew.Rows.Count; i++)
                    {
                       /* if (dtProductCartNew.Rows[i]["shippingAndTax"].ToString().Trim().ToLower() == "true")
                        {
                            shippingAmount += Convert.ToDouble(dtProductCartNew.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtProductCartNew.Rows[i]["Qty"].ToString().Trim());
                            shippingAndTax = true;
                        }*/
                        totalDealPrice += Convert.ToDouble(dtProductCartNew.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtProductCartNew.Rows[i]["Qty"].ToString().Trim());
                    }
                    totalDealPrice += shippingAmount;
                    lblShippingAndTax.Text = Math.Round(shippingAmount, 2, MidpointRounding.AwayFromZero).ToString();
                    lblGrandTotal.Text = Math.Round(totalDealPrice, 2, MidpointRounding.AwayFromZero).ToString();                   
                    if (shippingAndTax)
                    {                                               
                        divShipping.Visible = true;                        
                    }
                    else
                    {                       
                        divShipping.Visible = false;
                    }                    
                }
            }
        }
        catch (Exception ex)
        { }
    }

    const string HTML_TAG_PATTERN = "<.*?>";

    string StripHTML(string inputString)
    {
        return Regex.Replace
          (inputString, HTML_TAG_PATTERN, string.Empty);
    }

    protected string getCityName()
    {
        BLLCities objCity = new BLLCities();
        objCity.cityId = 337;
        HttpCookie yourCity = Request.Cookies["yourCity"];
        if (yourCity != null)
        {
            objCity.cityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
        }
        DataTable dtCity = objCity.getCityByCityId();
        if (dtCity != null && dtCity.Rows.Count > 0)
        {
            return dtCity.Rows[0]["cityName"].ToString().Trim();
        }
        return "";
    }

    protected void btnEmailSubmit_Click(object sender, EventArgs e)
    {
        try
        {           

            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {

                DataTable dtUser = null;
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }

                string strUid = (Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim()) + 111111).ToString();
                string strUserName = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
                if (SendMailWithActiveCode(txtEmail.Text.Trim(), strUserName, strUid))
                {
                    txtEmail.Text = "";
                    txtFriendName.Text = "";
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Email has been sent successfully.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(btnEmailSubmit, typeof(Button), "Javascript", jScript, false);                                                    
                  
                }
                else
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Email sent failed.' , 'error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(btnEmailSubmit, typeof(Button), "Javascript", jScript, false);                                                    
                }

            }
            else if (ViewState["userName"] != null)
            {

                string strUid = (Convert.ToInt64(ViewState["userId"].ToString().Trim()) + 111111).ToString();
                if (SendMailWithActiveCode(txtEmail.Text.Trim(), ViewState["userName"].ToString(), strUid))
                {
                    txtEmail.Text = "";
                    txtFriendName.Text = "";
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Email has been sent successfully.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(btnEmailSubmit, typeof(Button), "Javascript", jScript, false);                                                                        
                }
                else
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Email sent failed.' , 'error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(btnEmailSubmit, typeof(Button), "Javascript", jScript, false);                                                    
                }
            }
            else
            {
                objUser = new BLLUser();
                objUser.userId = Convert.ToInt32(ViewState["userId"].ToString().Trim());
                DataTable dtUser = objUser.getUserByID();
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    ViewState["userName"] = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
                    string strUid = (Convert.ToInt64(ViewState["userId"].ToString().Trim()) + 111111).ToString();
                    if (SendMailWithActiveCode(txtEmail.Text.Trim(), ViewState["userName"].ToString(), strUid))
                    {
                        txtEmail.Text = "";
                        txtFriendName.Text = "";
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Email has been sent successfully.' , 'success');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnEmailSubmit, typeof(Button), "Javascript", jScript, false);                                                                        
                    }
                    else
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Email sent failed.' , 'error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnEmailSubmit, typeof(Button), "Javascript", jScript, false);                                                    
                    }
                }
            }

        }
        catch (Exception ex)
        { }
    }

    #region Send Email for Forgot Password

    private bool SendMailWithActiveCode(string strEmailAddress, string strUserName, string strUserID)
    {
        MailMessage message = new MailMessage();

        StringBuilder sb = new StringBuilder();

        try
        {

            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = "You got a message from \"" + strUserName + "\"";
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + txtFriendName.Text.Trim() + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Your friend " + strUserName + " has purchase amazing deal on Tazzling.com.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>I just found a great daily deal site. They offer huge discounts on food fares, spa and other great outdoor adventures for more than 50% off! Best of all, signup is free! One of their today’s deal is  \"<a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/r/" + strUserID + "'>" + ViewState["title"].ToString() + "</a>\".</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Check it out <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/r/" + strUserID + "'>http://www.tazzling.com</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(strUserName + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");

            message.Body = sb.ToString();

            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion
}
