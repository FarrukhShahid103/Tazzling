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

public partial class Takeout_UserControls_Templates_FrameStart : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public string ElementBoxId
    {
        get
        {
            if (ViewState["ElementBoxId"] == null)
                ViewState["ElementBoxId"] = "element-box";
            return (string)ViewState["ElementBoxId"];
        }
        set { ViewState["ElementBoxId"] = value; }
    }

    public string Styles
    {
        get
        {
            if (ViewState["Styles"] == null)
                ViewState["Styles"] = "";
            return (string)ViewState["Styles"];
        }
        set { ViewState["Styles"] = value; }
    }
}
