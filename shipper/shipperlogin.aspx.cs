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

public partial class shipperlogin : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try 
        {
            if (!IsPostBack)
            {
               
            }
        }
        catch (Exception ex)
        {
          
        }
        lblFooter.Text = "Copyrights © "+ System.DateTime.Now.Year.ToString() +" TastyGo";
    }
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            obj.userName = HtmlRemoval.StripTagsRegexCompiled(txtUserName.Text.Trim());
            obj.userPassword = HtmlRemoval.StripTagsRegexCompiled( txtPassword.Text.Trim());        
            DataTable dtUser = obj.validateUserNamePassword();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                if (dtUser.Rows[0]["userTypeID"].ToString() == "8")
                {
                    Session["user"] = dtUser;
                    Response.Redirect(ResolveUrl("~/shipper/controlpanel.aspx"), false);
                }
                else
                {
                    lblMessage.Text = "You are not allowed to access this area";
                    pnlError.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = "Invalid Username / Password";
                pnlError.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
}
