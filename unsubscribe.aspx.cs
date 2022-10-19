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
using System.Text;
using System.Net.Mail;
using GecLibrary;
using System.Threading;
public partial class unsubscribe : System.Web.UI.Page
{
    BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Tastygo | Unsubscribe NewsLetter";
            if (!IsPostBack)
            {
                imgGridMessage.Visible = false;
                lblMessage.Visible = false;
                GECEncryption oEnc = new GECEncryption();
                if (Request.QueryString["c"] != null && Request.QueryString["c"] != "")
                {
                    trEmail.Visible = false;
                    trButton.Visible = false;
                    obj.SId = Convert.ToInt32(oEnc.DecryptData("123456", Server.UrlDecode(Request.QueryString["c"].ToString().Trim().Replace("_", "%")).Replace(" ", "+")));
                    obj.Status = false;
                    obj.changeSubscriberStatus();
                    imgGridMessage.Visible = true;
                    lblMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.Text = "Your email have been unsubscribe successfully.";
                    Response.AddHeader("REFRESH", "3;URL=Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.Text = "Your URL is not correct. Please try again with correct URL.<br>There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }


    private bool SendMailForNewAccount(string strUserID)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = "Unsubscribe your email";
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You will not be bugged with 50%~90% OFF Amazing Daily Deals!.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>If you want to unsubscribe, click the following link <br><a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/unsubscribe.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/unsubscribe.aspx?c=" + strUserID + "</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>If your link did not work please copy past it in your browser.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
            message.Body = sb.ToString();
            return Misc.SendEmail(txtUserName.Text.Trim(), "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            //lblAddressError.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            //lblAddressError.Visible = true;
            //imgGridMessage.Visible = true;
            //imgGridMessage.ImageUrl = "images/error.png";
            //lblAddressError.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }
    protected void BtnUnsubscribe_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Misc.validateEmailAddress(txtUserName.Text.Trim()))
            {
                Response.Write("Please enter a valid Email ID");
                Response.End();
                return;
            }

            obj = new BLLNewsLetterSubscriber();
            HttpCookie yourCity = Request.Cookies["yourCity"];
            obj.CityId = 337;
            if (yourCity != null)
            {
                obj.CityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
            }
            if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() != "")
            {
                obj.CityId = Convert.ToInt32(Request.QueryString["cid"].ToString());
            }
            obj.Email = txtUserName.Text.Trim();
            DataTable dtEmail = obj.getNewsLetterSubscriberByEmailCityId2();
            if (dtEmail != null && dtEmail.Rows.Count > 0)
            {
                string iSubId = dtEmail.Rows[0]["sID"].ToString().Trim();
                GECEncryption oEnc = new GECEncryption();
                string strEncryptUserID = "";
                strEncryptUserID = Server.UrlEncode(oEnc.EncryptData("123456", iSubId.ToString())).Replace("%", "_");
                if (SendMailForNewAccount(strEncryptUserID))
                {
                    imgGridMessage.Visible = true;
                    lblMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.Text = "An email is sent to your email address. Please check your inbox for unsubscribe link.";
                }
                else
                {
                    imgGridMessage.Visible = true;
                    lblMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.Text = "Email sending failed. Please try again later.";
                }
            }
        }
        catch (Exception ex)
        { }
    }
}
