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

public partial class Takeout_GoogleMap : System.Web.UI.Page
{    
    public string locationOfVendor = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["gAdd"] != null && Request.QueryString["gAdd"].ToString().Trim() != "")
            {
                locationOfVendor = Request.QueryString["gAdd"].Trim()+", Canada";
            }

        }
    }

}
