using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Rewards : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Rewards";
    }
}