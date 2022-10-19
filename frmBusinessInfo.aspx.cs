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

public partial class frmBusinessInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Features";
        if (!Page.IsPostBack)
        {
            //Bind the Provinces/State Info
            bindProvinces();
        }
    }

    protected void bindProvinces()
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(2);
            this.ddlProvince.DataSource = dt.DefaultView;
            this.ddlProvince.DataTextField = "provinceName";
            this.ddlProvince.DataValueField = "provinceId";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        { }
    }

    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlProvince.SelectedValue);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void FillCitpDroDownList(string strProvinceId)
    {
        try
        {
            BLLCities objBLLCities = new BLLCities();

            objBLLCities.provinceId = int.Parse(strProvinceId);

            DataTable dtCities = objBLLCities.getCitiesByProvinceId();

            ddlSelectCity.DataSource = dtCities;

            ddlSelectCity.DataTextField = "cityName";

            ddlSelectCity.DataValueField = "cityId";

            ddlSelectCity.DataBind();

            ddlSelectCity.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //it will send the email
            if (SendMailWithActiveCode())
            {
                this.tblBusinessInfo.Visible = false;
                this.tblEmailInfo.Visible = true;

                lblErrorMessage.Text = "We have received your business information and we will contact with you soon.";
                lblErrorMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblErrorMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                this.tblBusinessInfo.Visible = false;
                this.tblEmailInfo.Visible = true;

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
            string Subject = ConfigurationManager.AppSettings["EmailSubjectActivation"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<html><head><title>New Business Info</title></head><body><h4>We have received the new request for business");
            mailBody.Append(",</h4>");            
            mailBody.Append("<table>");
            mailBody.Append("<tr><td>Business detail is following</td></tr>");
            mailBody.Append("<tr><td><b>Business Name :</b>  " + this.txtBusinessName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Email Address :</b>  " + this.txtEmail.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>First Name :</b>  " + this.txtFirstName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Last Name :</b>  " + this.txtLastName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Address 1 :</b>  " + this.txtAddress1.Text.Trim() + "</td></tr>");
            if (this.txtAddress2.Text.Trim().Length > 0)
                mailBody.Append("<tr><td><b>Address 2 :</b>  " + this.txtAddress2.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>State / Province :</b>  " + this.ddlProvince.SelectedItem.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>City :</b>  " + this.ddlSelectCity.SelectedItem.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Zip Code :</b>  " + this.txtZip.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Website :</b>  " + this.txtWebsite.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Phone Number :</b>  " + this.txtPhoneNo.Text.Trim() + "</td></tr>");
            if (this.txtReviewLink.Text.Trim().Length > 0)
                mailBody.Append("<tr><td><b>Review Link(s) :</b>  " + this.txtReviewLink.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Pick a Category :</b>  " + this.ddlPickCategory.SelectedItem.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Where do you want your tastygo to run? :</b>  " + this.txtWhereRunTastyGo.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Tell us a little bit about your business :</b>  " + this.txtBusinessInfo.Text.Trim() + "</td></tr>");
            mailBody.Append("</table>");
            mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();

            //It will send the email to Administrator
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            this.tblBusinessInfo.Visible = false;
            this.tblEmailInfo.Visible = true;

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