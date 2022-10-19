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

public partial class admin_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        
        }

    }
    protected void BtnPreview_Click(object sender, EventArgs e)
    {
        string jScript;
        //jScript = "<script>myWindow=window.open('http://www.google.com','','width=800,height=600,toolbar=no,status=no, menubar=no, scrollbars=yes,resizable=yes');</script>";
        jScript = "<script>alert('Hello');</script>";
        Page.RegisterClientScriptBlock("keyClientBlock", jScript);
        Page.RegisterClientScriptBlock("ThisIsKay", "<script>alert('hi');</script>");
    }
}
