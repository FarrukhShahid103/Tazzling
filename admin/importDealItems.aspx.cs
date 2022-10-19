using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

public partial class importDealItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            if (Session["user"] != null)
            {
                if (Request.QueryString["CID"] != null && Request.QueryString["CID"] != "" &&
                    Request.QueryString["CN"] != null && Request.QueryString["CN"] != "")
                {
                    DataTable dtUser = (DataTable)Session["user"];
                    ViewState["userID"] = dtUser.Rows[0]["userID"];
                    ViewState["userName"] = dtUser.Rows[0]["userName"];
                    ViewState["cuisineName"] = Request.QueryString["CN"].ToString();
                    ViewState["cuisineID"] = Request.QueryString["CID"].ToString();
                    // downloadExcelFile(Convert.ToInt64(ViewState["cuisineID"].ToString()));
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/admin/default.aspx"),false);
                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/admin/default.aspx"),false);
            }
        }
    }
}
