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


public partial class Takeout_UserControls_Login_PasswordRecover : System.Web.UI.UserControl
{
    BLLUser obj = new BLLUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            pnlError.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            pnlError.Visible = false;
            if (!Misc.validateEmailAddress(txtEmail.Text.Trim()))
            {
                pnlError.Visible = true;
                imgGridMessage.ImageUrl = "~/Images/error.png";
                lblMessage.Text = "Invalid email address.";             
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            obj.email = txtEmail.Text.Trim();
            if (obj.getUserByEmail())
            {
                DataTable dtUser = obj.getUserDetailByEmail();
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtUser.Rows[0]["isActive"].ToString()))
                    {

                        if (SendMailForPassword(dtUser.Rows[0]["email"].ToString(), dtUser.Rows[0]["userPassword"].ToString(), dtUser.Rows[0]["userName"].ToString(), dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString()))
                        {
                            pnlError.Visible = true;
                            imgGridMessage.ImageUrl = "~/Images/Checked.png";
                            lblMessage.Text = "Your account information is sent to your email address.";
                            lblMessage.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            imgGridMessage.ImageUrl = "~/Images/error.png";
                            pnlError.Visible = true;
                            lblMessage.Text = "Email sending failed. Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                         GECEncryption objenc = new GECEncryption();
                         string strUserID = (Convert.ToInt32(dtUser.Rows[0]["userID"].ToString().Trim()) + 111111).ToString();
                         if (SendMailWithActiveCode(dtUser.Rows[0]["email"].ToString(), dtUser.Rows[0]["userPassword"].ToString(), dtUser.Rows[0]["userName"].ToString(), strUserID, dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString()))
                        {
                            pnlError.Visible = true;
                            imgGridMessage.ImageUrl = "~/Images/Checked.png";
                            lblMessage.Text = "Your account information is sent to your email address.";
                            lblMessage.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            imgGridMessage.ImageUrl = "~/Images/error.png";
                            pnlError.Visible = true;
                            lblMessage.Text = "Email sending failed. Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            else
            {
                imgGridMessage.ImageUrl = "~/Images/error.png";
                pnlError.Visible = true;
                lblMessage.Text = "This email address does not exist.";
                lblMessage.ForeColor = System.Drawing.Color.Red;                                
            }
        }
        catch (Exception ex)
        {
            pnlError.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    private bool SendMailWithActiveCode(string strEmailAddress, string strPassword, string strUserName, string strUserID, string strUserFullName)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
        try
        {

            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectForgetPassword"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'>");
            mailBody.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 600'><title>Thank You for Registering with Tastygo</title>");
            mailBody.Append("<style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 480px)' type=text/css>*{line-height: normal !important;}</style></head>");
            mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            mailBody.Append("<table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='520' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='520' bgcolor='#FFFFFF' align='left'><div style='margin: 40px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'> <strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div><div style='margin: 0px 60px 15px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            mailBody.Append("<strong>Dear " + strUserFullName + "</strong></div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("With the power of group ordering concept, Tastygo brings amazing deal, from 50%~ 90% off  around your neighbourhood.");
            mailBody.Append("</div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("First to activate your account, please click the follow the link below:<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "</a>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If clicking on the link doesn't work, try copy & paste it into your browser.");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Account detail:");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("User Name :  " + strUserName);
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Password :" + strPassword.ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com'>support@tazzling.com</a>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("We wish you enjoy our deal experience.");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("</td></tr></table></td></tr></table></td></tr></table></body></html>");
            message.Body = mailBody.ToString();
            try
            { Misc.SendEmail("superadmin@tazzling.com", "", "", fromAddress, Subject, message.Body); }
            catch (Exception ex)
            { }
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #region Send Email for Forgot Password
    private bool SendMailForPassword(string strEmailAddress, string strPassword, string strUserName, string strOrignalName)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
        try
        {
            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectForgetPassword"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");            
            mailBody.Append("<h4>Dear "+strOrignalName);
            mailBody.Append(",</h4>");
            mailBody.Append("<font size='3'>Find your login information on: <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></font>");
            
            mailBody.Append("<table><tr><td>Your login details are as follows:</td></tr>");
            mailBody.Append("<tr><td>Your Email : <a href='mailto:" + strEmailAddress + "'> " + strEmailAddress + " </a></td></tr>");
            mailBody.Append("<tr><td>User Name :  " + strUserName + "</td></tr>");
            mailBody.Append("<tr><td>Password :" + strPassword.ToString().Trim() + "</td></tr></table>");
            mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();

            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

}
