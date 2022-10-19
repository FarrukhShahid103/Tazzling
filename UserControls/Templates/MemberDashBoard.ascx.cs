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

public partial class Takeout_UserControls_Templates_submenu : System.Web.UI.UserControl
{
    public string ClassName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ClassName = "";
        if (!IsPostBack)
        {
            try
            {
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name;

                string[] fullPageURL = Request.Url.ToString().ToLower().ToString().Split('/');
                DataTable dtUser = null;
                if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                    pnlMember.Visible = true;
                }
                else if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                    pnlMember.Visible = true;
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                    pnlMember.Visible = true;
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                    pnlMember.Visible = true;
                }
                else if (sRet.ToLower() == "member_used_gift_card.aspx")
                {
                    Response.Redirect(ResolveUrl("~/default.aspx"), false);
                    return;
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/default.aspx"), false);
                    return;
                }

                if (dtUser != null && dtUser.Rows.Count > 0
                    && dtUser.Rows[0]["isAffiliate"] != null
                    && dtUser.Rows[0]["isAffiliate"].ToString().Trim() != ""
                    && Convert.ToBoolean(dtUser.Rows[0]["isAffiliate"].ToString().Trim()))
                {
                    member_DashBOard.Visible = true;
                }

                if (sRet.ToLower() == "userdashboard.aspx")
                {
                    member_DashBOard.Attributes["class"] = "selected";

                }
                else if (sRet.ToLower() == "redemptions.aspx")
                {
                    member_Redemptions.Attributes["class"] = "selected";
                }

                else if (fullPageURL[fullPageURL.Length - 1].ToString().ToLower() == "frmbusdealorderinfo.aspx?mode=now")
                {
                    member_NowDeals.Attributes["class"] = "selected";
                }
                else if (fullPageURL[fullPageURL.Length -1].ToString().ToLower() == "frmbusdealorderinfo.aspx?mode=past")
                {
                    member_DailyDeals.Attributes["class"] = "selected";
                }
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
