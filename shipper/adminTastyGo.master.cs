using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class admin_adminTastyGo : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
            return;
        }                 
        lblFooter.Text = "Copyrights © " + System.DateTime.Now.Year.ToString() + " TastyGo";
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Remove("user");
            Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
        }
        catch (Exception ex)
        {

        }
    }
}
