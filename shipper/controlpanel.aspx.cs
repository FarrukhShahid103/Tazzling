using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class controlpanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["user"] == null)
            {
                Response.Redirect(ResolveUrl("~/shipper/default.aspx"), false);
                return;
            }
          
        }
        catch (Exception ex)
        { }
    }
}
