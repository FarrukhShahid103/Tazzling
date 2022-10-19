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
using System.Data.SqlClient;
using System.Xml;
using System.Text;

public partial class rssReadTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlTextReader reader = null;

        try
        {

            reader = new XmlTextReader("http://localhost:12279/TastyGo/RSS.aspx?cid=337");

            DataSet ds = new DataSet();

            ds.ReadXml(reader);

            dlRSS.DataSource = ds.Tables["item"];

            dlRSS.DataBind();

        }

        catch (Exception ex)
        {

            //lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

        }

        finally
        {

            reader.Close();

        }
    }
}
