using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IPBlock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblBlockMessage.Text = "Your IP (" + Request.UserHostAddress.ToString() + ") is Blocked for 24 hours.";
    }
}