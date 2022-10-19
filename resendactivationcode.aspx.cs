using System;
using System.Text;
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
using GecLibrary;
using System.Net;
using System.Net.Mail;

public partial class resendactivationcode : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (!IsPostBack)
            {
                DataTable dtUsers = obj.getAllInActiveUsers();
                int emailCount=0;
                if (dtUsers != null && dtUsers.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUsers.Rows.Count; i++)
                    {

                        if (SendMailWithActiveCode(dtUsers.Rows[0]["email"].ToString(), dtUsers.Rows[0]["userPassword"].ToString(), dtUsers.Rows[0]["userName"].ToString(), (Convert.ToInt64(dtUsers.Rows[0]["userID"].ToString())+111111).ToString(), dtUsers.Rows[0]["firstName"].ToString() + " " + dtUsers.Rows[0]["lastName"].ToString()))
                        {
                            emailCount++;
                        }
                    }

                }
                lblMessage.Visible = true;
                lblMessage.Text = "Email with activation code is send to " + emailCount.ToString() + " users";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
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
            string Subject = ConfigurationManager.AppSettings["EmailSubjectActivation"].ToString().Trim();
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
            mailBody.Append("To activate your account, please click the follow the link below:<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "</a>");
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
}
