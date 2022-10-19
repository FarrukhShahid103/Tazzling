using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;
using System.Configuration;

public partial class affiliateform : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = getCityName() + "'s Tasty Daily Deal | Affliate Partner | Request";
        if ((Session["member"] != null) || (Session["restaurant"] != null) || (Session["sale"] != null) || (Session["user"]!=null))
        {
            //If User is Affiliate Partner then it will redirect to the Affiliate Banner page
            if (chkAffliatePartner())
                Response.Redirect("affiliateBanners.aspx", false);
        }
        else
            Response.Redirect("Default.aspx");

        if (!Page.IsPostBack)
        {}
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

    private bool chkAffliatePartner()
    {
        bool bStatus = false;

        try
        {
            DataTable dtUser = null;

            if (Session["member"] != null)
                dtUser = (DataTable)Session["member"];
            else if (Session["restaurant"] != null)
                dtUser = (DataTable)Session["restaurant"];
            else if (Session["sale"] != null)
                dtUser = (DataTable)Session["sale"];
            else if (Session["user"] != null)
                dtUser = (DataTable)Session["user"];

            if ((dtUser != null) && (dtUser.Rows.Count > 0))
            {
                if (dtUser.Rows[0]["affiliateReq"] != DBNull.Value)
                {
                    if (dtUser.Rows[0]["affiliateReq"].ToString().Trim() == "approved")
                        bStatus = true;
                }
            }
        }
        catch (Exception ex)
        { }

        return bStatus;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //it will send the email
            if (SendMailWithActiveCode())
            {
                BLLUser objBLLUser = new BLLUser();

                objBLLUser.userId = GetUserIdFromSession();
                objBLLUser.affiliateReq = "new";

                int iAffliateReq = objBLLUser.updateUserAffiliateReqByUserId();

                lblErrorMessage.Text = "We have received your affiliation partner request and we will contact with you soon.";
                lblErrorMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblErrorMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                lblErrorMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #region Send Email for Forgot Password

    private bool SendMailWithActiveCode()
    {
        MailMessage message = new MailMessage();

        StringBuilder mailBody = new StringBuilder();

        try
        {
            string toAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectAffiliatePartner"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            mailBody.Append("<head><title>New Affiliate Partner Request Info</title></head><body style='font-family: Century;'><h4>We have received the new request for  Affiliate Partner");
            mailBody.Append(",</h4>");
            mailBody.Append("<table>");

            mailBody.Append("<tr><td><h4>Site Information</h4></td></tr>");
            mailBody.Append("<tr><td><b>Web site or neesletter name :</b>  " + this.txtWebSiteName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Web site URL :</b>  " + this.txtWebSiteURL.Text.Trim() + "</td></tr>");
            if (this.txtWebsiteInfo.Text.Trim().Length>0)
                mailBody.Append("<tr><td><b>Tell us a little bit about your website :</b>  " + this.txtWebsiteInfo.Text.Trim() + "</td></tr>"); ;
            
            mailBody.Append("<tr><td><br /><h4>Contact Information</h4></td></tr>");            
            mailBody.Append("<tr><td><b>Title/Function in Organization :</b>  " + this.txtTitleInOrg.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Email Address :</b>  " + this.txtEmail.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>First Name :</b>  " + this.txtFirstName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Last Name :</b>  " + this.txtLastName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Phone Number :</b>  " + this.txtPhoneNo.Text.Trim() + "</td></tr>");

            mailBody.Append("<tr><td><br /><h4>Company Information</h4></td></tr>");            
            mailBody.Append("<tr><td><b>Organization Name (Your name if none):</b>  " + this.txtOrgName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Address 1 :</b>  " + this.txtAddress1.Text.Trim() + "</td></tr>");
            if (this.txtAddress2.Text.Trim().Length > 0)
                mailBody.Append("<tr><td><b>Address 2 :</b>  " + this.txtAddress2.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>City :</b>  " + this.txtCity.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Province :</b>  " + this.txtProvince.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Zip Code :</b>  " + this.txtZip.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Country :</b>  " + this.txtCountry.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Organization Phone :</b>  " + this.txtOrgPhone.Text.Trim() + "</td></tr>");
            if (this.txtOrgFax.Text.Trim().Length > 0)
                mailBody.Append("<tr><td><b>Organization Fax:</b>  " + this.txtOrgFax.Text.Trim() + "</td></tr>");
            mailBody.Append("</table>");
            mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();

            //It will send the email to Administrator
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

    private int GetUserIdFromSession()
    {
        int iUserid = 0;
        DataTable dtUser = null;

        try
        {
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

            iUserid = int.Parse(dtUser.Rows[0]["userID"].ToString());
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return iUserid;
    }

    #endregion
}