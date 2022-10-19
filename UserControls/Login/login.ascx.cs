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

public partial class Takeout_UserControls_Login_login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {           

        }       
    } 
    private bool GetAndSetAffInfoFromCookieInUserInfo(int iUserId)
    {
        bool bStatus = false;

        try
        {
            string strAffiliateRefId = "";
            string strAffiliateDate = "";

            HttpCookie cookieAffId = Request.Cookies["tastygo_affiliate_userID"];
            HttpCookie cookieAddDate = Request.Cookies["tastygo_affiliate_date"];

            //Remove the Cookie
            if ((cookieAffId != null) && (cookieAddDate != null))
            {
                if ((cookieAffId.Values.Count > 0) && (cookieAddDate.Values.Count > 0))
                {
                    //It should not be the same user
                    if (int.Parse(cookieAffId.Values[0].ToString()) != iUserId)
                    {
                        strAffiliateRefId = cookieAffId.Values[0].ToString();
                        strAffiliateDate = cookieAddDate.Values[0].ToString();

                        GECEncryption objDecrypt = new GECEncryption();

                        BLLUser objBLLUser = new BLLUser();
                        objBLLUser.userId = iUserId;
                        objBLLUser.affComID = int.Parse(strAffiliateRefId);
                        objBLLUser.affComEndDate = DateTime.Parse(strAffiliateDate);
                        objBLLUser.updateUserAffCommIDByUserId();

                        cookieAffId.Values.Clear();
                        cookieAddDate.Values.Clear();
                        cookieAffId.Expires = DateTime.Now;
                        cookieAddDate.Expires = DateTime.Now;

                        Response.Cookies.Add(cookieAffId);
                        Response.Cookies.Add(cookieAddDate);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }
 
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        try
        {
            //For the functionality of Div --Show/Hide
            //Display the Login Area
            if (cbSignUp.Checked)
            {                
                //Hide the SignUp Area
                this.divSignUp.Visible = true;

                BLLUser obj = new BLLUser();

                imgGridMessage.Visible = false;
                lblErrorMessage.Visible = false;
                lblErrorMessage.Text = "";
                DataTable dtUser = null;

                obj.userName = this.txtUsernameSignUp.Text.Trim();

                obj.email = this.txtUsernameSignUp.Text.Trim();

                obj.referralId = "";
                if (!obj.getUserByUserName())
                {
                    string[] strUserName = this.txtFullName.Text.Trim().Split(' ');
                    if (strUserName.Length > 1)
                    {
                        obj.firstName = strUserName[0].ToString();
                        obj.lastName = strUserName[1].ToString();
                    }
                    else
                    {
                        obj.firstName = this.txtFullName.Text.Trim();
                        obj.lastName = "";
                    }
                    obj.userName = this.txtUsernameSignUp.Text.Trim();
                    obj.userPassword = this.txtPwd.Text.Trim();
                    obj.email = this.txtUsernameSignUp.Text.Trim();

                    //For Customer 
                    obj.userTypeID = 4;
                    obj.isActive = false;

                    obj.referralId = "";
                    obj.countryId = 2;
                    //if (hfProvince.Value != "0")
                    //{
                    obj.provinceId = 3;
                    //}
                    obj.friendsReferralId = GetUserRefferalId();
                    obj.howYouKnowUs = "";
                    obj.ipAddress = Request.UserHostAddress.ToString();
                    long result = obj.createUser();

                    //HttpCookie yourCity = Request.Cookies["yourCity"];
                    //string strCityid = "337";
                    //if (yourCity != null)
                    //{
                    //    strCityid = yourCity.Values[0].ToString().Trim();
                    //}
                    //Misc.addSubscriberEmail(txtUsernameSignUp.Text.Trim(), strCityid);


                    if (result != 0)
                    {
                        //If exits then it will update into the User Info data table
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(result.ToString().Trim()));

                        GECEncryption oEnc = new GECEncryption();

                        string strEncryptUserID = Server.UrlEncode(oEnc.EncryptData("123456", result.ToString())).Replace("%", "_");

                        SendMailWithActiveCode(this.txtUsernameSignUp.Text.Trim(), this.txtPwd.Text.Trim(), this.txtUsernameSignUp.Text.Trim(), strEncryptUserID);

                        /*divCreate.Visible = false;
                    
                        divMessage.Visible = true;*/


                        this.lblErrorMessage.Visible = true;
                        this.lblErrorMessage.ForeColor = System.Drawing.Color.Black;
                        lblErrorMessage.Text = "Thank you for registering with Tazzling.com. Please check your email inbox for your welcome email.If there is no link in your inbox, or junk box email, please contact us at support@tazzling.com or call 1855-295-1771.";

                        this.imgGridMessage.Visible = true;
                        this.imgGridMessage.ImageUrl = "~/images/Checked.png";

                        this.tblSignUp.Visible = false;
                        HttpCookie cookie = Request.Cookies["tastygoSignup"];
                        if (cookie == null)
                        {
                            cookie = new HttpCookie("tastygoSignup");
                        }
                        cookie.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(cookie);
                        cookie["tastygoSignup"] = this.txtUsernameSignUp.Text.Trim();
                    }
                    else
                    {
                        imgGridMessage.Visible = true;
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Sorry you could not register for right now please try again.";
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    imgGridMessage.Visible = true;
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Email already exists. Please choose another.";
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                imgGridMessage.Visible = true;
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please accept terms and agreement.";
                lblErrorMessage.Visible = true;
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

    private string GetUserRefferalId()
    {
        string strRefId = "";

        try
        {
            HttpCookie cookie = Request.Cookies["tastygo_userID"];

            if (cookie != null)
            {
                strRefId = cookie.Values[0].ToString().Trim();
            }
        }
        catch (Exception ex)
        { }

        return strRefId;
    }

    #region Send Email for Forgot Password

    private bool SendMailWithActiveCode(string strEmailAddress, string strPassword, string strUserName, string strUserID)
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
            mailBody.Append("<strong>Dear " + this.txtFullName.Text.Trim() + "</strong></div>");

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
            lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblErrorMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }

    #endregion
}