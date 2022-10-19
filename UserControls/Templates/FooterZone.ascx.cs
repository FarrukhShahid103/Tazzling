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

public partial class FooterZone : System.Web.UI.UserControl
{    
    BLLCities objCities = new BLLCities();
    public string strRssLink = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/RSS.aspx?cid=";
    protected void Page_Load(object sender, EventArgs e)
    {        
        HttpCookie yourCity = Request.Cookies["yourCity"];
        if (yourCity != null)
        {
            strRssLink += yourCity.Values[0].ToString().Trim();
        }
        else
        {
            strRssLink += "337";
        }
        //strRssLink+=
        if (!IsPostBack)
        {

        }
    }
}
