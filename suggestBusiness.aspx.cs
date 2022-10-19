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

public partial class suggestBusiness : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Suggest a Business";
        if (!IsPostBack)
        {
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        MailMessage message = new MailMessage();

        StringBuilder mailBody = new StringBuilder();

        try
        {
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = "Suggest a Business";

            message.IsBodyHtml = true;
            mailBody.Append("<html><head><title>Suggest a Business</title></head><body><h4>Dear Admin");
            mailBody.Append(",</h4>");
            // mailBody.Append("<font size='3'>You have received following query on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a>");
            mailBody.Append("<font size='3'>You have received following query on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a>");
            mailBody.Append(" from " + txtOwnerName.Text.Trim() + "</font>");
            mailBody.Append("<table>");
            mailBody.Append("<tr><td>Query detail is following: </td></tr>");
            mailBody.Append("<tr><td><b>Owner Name :</b> " + txtOwnerName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Business Name :</b> " + txtBusinessName.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Business Website :</b> " + txtBusWebsite.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>City :</b> " + txtCity.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Type of Business :</b> " + txtTypeBusiness.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><b>Why should We feature them? Give us your review :</b>  " + txtFeaturedViews.Text.Trim() + "</td></tr>");
            mailBody.Append("<tr><td><p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></td></tr></table></body></html>");
            message.Body = mailBody.ToString();

            if (Misc.SendEmail(fromAddress, "", "", txtBusinessName.Text.Trim(), Subject, message.Body))
            {
                txtOwnerName.Text = "";
                txtBusinessName.Text = "";
                txtBusWebsite.Text = "";
                txtCity.Text = "";
                txtTypeBusiness.Text = "";
                txtFeaturedViews.Text = "";

                lblErrorMessage.Text = "We have received your suggestion.";
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
