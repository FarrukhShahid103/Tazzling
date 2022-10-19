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

public partial class contact_us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = getCityName() + "'s Tasty Daily Deal | Countact Us";
        if (!IsPostBack)
        {

            //DateTime dt = new DateTime(1984, 2, 19);
            //DateTime dtnow = DateTime.Now.AddDays(-10);
            //dtnow.Subtract(dt);

        }

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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        lblErrorMessage.Text = "";
        lblErrorMessage.Visible = false;
        imgGridMessage.Visible = false;


        MailMessage message = new MailMessage();

        StringBuilder sb = new StringBuilder();
        try
        {

            if (!Misc.validateEmailAddress(txtEmail.Text.Trim()))
            {
                lblErrorMessage.Text = "Email address is not valid.";
                lblErrorMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string fromAddress = "support@tazzling.com";
            string Subject = HtmlRemoval.StripTagsRegexCompiled(this.txtReason.Text.Trim());
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear Admin,</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received following query on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a>  from " + txtFname.Text.Trim() + "</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Query detail is following:</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Name : " + txtFname.Text.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Email : " + txtEmail.Text.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>City : " + txtCity.Text.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Reason for Contacting : " + txtReason.Text.Trim() + "</div>");
            if (chkUsingToday.Checked)
                sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>" + txtFname.Text.Trim() + " says : This is regarding a Tastygo deal I'm planning to use today</div>");

            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Message :  " + txtmessage.Text.Trim() + "</div>");

            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
            message.Body = sb.ToString();
            try
            {
                Misc.SendEmail(fromAddress, "", "", txtEmail.Text.Trim(), Subject, message.Body);
            }
            catch (Exception ex)
            {

            }
            message = new MailMessage();
            sb = new StringBuilder();
            message.IsBodyHtml = true;
            //mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            //mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");
            //mailBody.Append("<h4>Dear Customer,</h4>");
            //mailBody.Append("<font size='3'>You have sent following query on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></font>");
            //mailBody.Append("<table>");
            //mailBody.Append("<tr><td>Name : " + txtFname.Text.Trim() + "</td></tr>");
            //mailBody.Append("<tr><td>Email : " + txtEmail.Text.Trim() + "</td></tr>");
            //mailBody.Append("<tr><td>City : " + txtCity.Text.Trim() + "</td></tr>");
            //mailBody.Append("<tr><td>Reason for Contacting : " + txtReason.Text.Trim() + "</td></tr>");
            //if (chkUsingToday.Checked)
            //    mailBody.Append("<tr><td>You says : This is regarding a Tastygo I am planning to use today</td></tr>");
            //mailBody.Append("<tr><td>Message :  " + txtmessage.Text.Trim() + "</td></tr>");
            //mailBody.Append("<tr><td style='font-size:12px;'>This is an automated Message from www.Tazzling.Com</td></tr>");
            //mailBody.Append("<tr><td style='font-size:12px;color:Gray;'>*If we do not get back to you within 24hours, or your issue is urgent, please email to <a href='mailto:support@tazzling.com'>support@tazzling.com</a> or call 1855-295-1771.</td></tr>");
            //mailBody.Append("<tr><td><p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></td></tr></table></body></html>");


            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + txtFname.Text.Trim() + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have sent following query on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Query detail is following:</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Name : " + txtFname.Text.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Email : " + txtEmail.Text.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>City : " + txtCity.Text.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Reason for Contacting : " + txtReason.Text.Trim() + "</div>");
            if (chkUsingToday.Checked)
                sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>" + txtFname.Text.Trim() + " says : This is regarding a Tastygo I am planning to use today</div>");

            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Message :  " + txtmessage.Text.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>This is an automated Message from www.Tazzling.Com</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If we do not get back to you within 24hours, or your issue is urgent, please email to <a href='mailto:support@tazzling.com'>support@tazzling.com</a> or call 1855-295-1771.</div>");


            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");




            message.Body = sb.ToString();
            if (Misc.SendEmail(txtEmail.Text.Trim(), "", "", fromAddress, Subject, message.Body))
            {
                txtEmail.Text = "";
                txtmessage.Text = "";
                txtReason.Text = "";
                txtCity.Text = "";
                txtFname.Text = "";

                lblErrorMessage.Text = "Email sent successfully!";
                lblErrorMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/checked.png";
                lblErrorMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblErrorMessage.Text = "Email sending failed! Please try again.";
                lblErrorMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblErrorMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
        }

    }
}
