using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Text;

public partial class referral : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Referral";

        if (!IsPostBack)
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
                    if (dtUser.Rows.Count > 0)
                    {
                        string strUid = (Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim()) + 111111).ToString();
                        if (Request.QueryString["did"] != null)
                        {
                            linkFacebook1.HRef = "http://www.facebook.com/sharer.php?u="
                            + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                            + "/r/" + strUid + "_" + Request.QueryString["did"].ToString().Trim()));
                            linkTweeter1.HRef = "http://twitter.com/share?url="
                            + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                            + "/r/" + strUid + "_" + Request.QueryString["did"].ToString().Trim()));
                            txtShareLink.Text = ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                            + "/r/" + strUid + "_" + Request.QueryString["did"].ToString().Trim());
                        }
                        else
                        {
                            linkFacebook1.HRef = "http://www.facebook.com/sharer.php?u="
                                                       + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                                                       + "/r/" + strUid));
                            linkTweeter1.HRef = "http://twitter.com/share?url="
                            + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                            + "/r/" + strUid));
                            txtShareLink.Text = ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                              + "/r/" + strUid);
                        }


                    }

                }
                else
                {
                    if (Request.QueryString["did"] != null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "login.aspx?did=" + Request.QueryString["did"].ToString(), false);
                    }
                    else
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "login.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }

    protected void lnkSendInvitation_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmail.Text != string.Empty)
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
                    //if (dtUser != null && dtUser.Rows.Count > 0)
                    //{
                    //    BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                    //    bllKarma.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                    //    bllKarma.karmaPoints = 1;
                    //    bllKarma.karmaPointsType = "Share";
                    //    bllKarma.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                    //    bllKarma.createdDate = DateTime.Now;
                    //    bllKarma.createKarmaPoints();
                    //}
                    //GECEncryption objEnc = new GECEncryption();
                    string strUid = (Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim()) + 111111).ToString();
                    string strUserName = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
                    if (SendMailWithActiveCode(txtEmail.Text.Trim(), strUserName, strUid))
                    {

                        //BLLReferralTracker track = new BLLReferralTracker();
                        //track.email = txtEmail.Text.Trim();
                        //DataTable dtTrack = track.getReferralTrackerByEmail();
                        //if (dtTrack != null && dtTrack.Rows.Count == 0)
                        //{
                        //    track.isSignup = false;
                        //    track.trackBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                        //    track.trackerDate = DateTime.Now;
                        //    track.trackerName = txtFriendName.Text.Trim();
                        //    track.createReferralTracker();
                        //}

                        //else if (dtTrack != null && dtTrack.Rows[0]["trackerID"].ToString().Trim()!="")
                        txtEmail.Text = "";
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Email has been sent successfully.' , 'success');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(lnkSendInvitation, typeof(Button), "Javascript", jScript, false);
                    }
                    else
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Email sent failed.' , 'error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(lnkSendInvitation, typeof(Button), "Javascript", jScript, false);
                    }
                }
            }
            else
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter email address.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(lnkSendInvitation, typeof(Button), "Javascript", jScript, false);
            }
        }
        catch (Exception ex)
        { }
    }

    private bool SendMailWithActiveCode(string strEmailAddress, string strUserName, string strUserID)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            if (Request.QueryString["did"] != null)
            {
                strUserID = strUserID + "_" + Request.QueryString["did"].ToString().Trim();
            }
            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = "You got a message from \"" + strUserName + "\"";
            message.IsBodyHtml = true;

            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logo_new.png' alt='tazzling' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            //sb.Append("<strong>Dear " + txtFriendName.Text.Trim() + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Your friend " + strUserName + " has purchase amazing deal on Tazzling.com.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>I just found a great daily deal site. They offer huge discounts on food fares, spa and other great outdoor adventures for more than 50% off! Best of all, signup is free! To check one of their their today’s deal <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/r/" + strUserID + "'>click here </a>.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Check it out <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/r/" + strUserID + "'>http://www.tazzling.com</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(strUserName + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
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
}