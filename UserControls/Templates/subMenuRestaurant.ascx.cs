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

public partial class Takeout_UserControls_Templates_subMenuRestaurant : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["restaurant"] != null)
                {
                    pnlRestaurant.Visible = true;
                }
                else
                {
                    if (Session["member"] == null)
                    {
                        Response.Redirect(ResolveUrl("~/opportunity.aspx"));
                    }
                }
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name;
                if (sRet.ToLower() == "restaurant_banlance.aspx")
                {
                    restaurant_Banlance.Attributes["class"] = "current";
                }
                else if (sRet.ToLower() == "restaurant_setup.aspx")
                {
                    restaurant_Setup.Attributes["class"] = "current";
                }
                else if (sRet.ToLower() == "restaurant_paymenthistory.aspx")
                {
                    restaurant_Bank_Account.Attributes["class"] = "current";
                }
                else if (sRet.ToLower() == "restaurant_comments.aspx")
                {
                    restaurant_Comments.Attributes["class"] = "current";
                }
                else if (sRet.ToLower() == "restaurant_orders.aspx")
                {
                    restaurant_Orders.Attributes["class"] = "current";
                }
                else if (sRet.ToLower() == "restaurant_profile.aspx")
                {
                    restaurant_Profile.Attributes["class"] = "current";
                }
                else if (sRet.ToLower() == "restaurant_statistics.aspx")
                {
                    restaurant_Statistics.Attributes["class"] = "current";
                }
                else if (sRet.ToLower() == "restaurant_package.aspx")
                {
                    restaurant_Package.Attributes["class"] = "current";
                }
                

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
