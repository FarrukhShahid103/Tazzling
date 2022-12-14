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

public partial class admin_Default : System.Web.UI.Page
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

            obj.userName = txtUserName.Text.Trim();
            obj.userPassword = txtPassword.Text.Trim();

            DataTable dtUser = obj.validateUserNamePassword();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                if (dtUser.Rows[0]["userTypeID"].ToString() == "1" || dtUser.Rows[0]["userTypeID"].ToString() == "2" || dtUser.Rows[0]["userTypeID"].ToString() == "5")
                {
                    Session["salesSection"] = dtUser;
                    Response.Redirect(ResolveUrl("~/sales/controlpanel.aspx"), false);
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
