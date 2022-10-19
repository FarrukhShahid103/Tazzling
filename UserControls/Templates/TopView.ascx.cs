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
using GecLibrary;
using System.IO;
using System.Text;
using System.Net.Mail;
public partial class Takeout_UserControls_Templates_topview : System.Web.UI.UserControl
{
    BLLUser obj = new BLLUser();
    BLLDeals objDeal = new BLLDeals();
    BLLCities objCity = new BLLCities();
    public string strBusinessOwner = "none";
    public string UserName = "";
    public string strMySite = System.Configuration.ConfigurationSettings.AppSettings["YourSite"].ToString() + "/";
    public string strHomePageSite = System.Configuration.ConfigurationSettings.AppSettings["YourSite"].ToString() + "/default.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        TopNavigation();
        shoppinCart();
        BindCategoryMenu();
        
    }

    protected void BindCategoryMenu()
    {
        BLLCategories objCategory = new BLLCategories();
        DataTable dtCategory = objCategory.getAllActiveCategories();
        int newDealsTotal = objCategory.GetNewDealsCount();
        if (dtCategory != null && dtCategory.Rows.Count > 0)
        {
            lblNewDeals.Text = "New Deals" + " (" + newDealsTotal + ")";
            lblFashion.Text = dtCategory.Rows[0]["categoryName"].ToString().Trim() + " (" + dtCategory.Rows[0]["Total"].ToString().Trim() + ")";
            lblHomeDoctor.Text = dtCategory.Rows[1]["categoryName"].ToString().Trim() + " (" + dtCategory.Rows[1]["Total"].ToString().Trim() + ")";
            lblTechGadgets.Text = dtCategory.Rows[2]["categoryName"].ToString().Trim() + " (" + dtCategory.Rows[2]["Total"].ToString().Trim() + ")";
            lblPopularServices.Text = dtCategory.Rows[3]["categoryName"].ToString().Trim() + "(" + dtCategory.Rows[3]["Total"].ToString().Trim() + ")";
            lblHealth.Text = dtCategory.Rows[4]["categoryName"].ToString().Trim() + " (" + dtCategory.Rows[4]["Total"].ToString().Trim() + ")";
        }
    }
    
    protected void shoppinCart()
    {
        try
        {
            if (Session["dtProductCart"] == null)
            {
                hlCartText.Text = "(0)";              
            }
            else
            {
                DataTable dtUserCart = (DataTable)Session["dtProductCart"];
                if (dtUserCart != null)
                {
                    hlCartText.Text = "(" + dtUserCart.Rows.Count.ToString() + ")";
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void TopNavigation()
    {
        if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
        {
            DataTable dtUser = null;
            if (Session["member"] != null)
            {
                strBusinessOwner = "none";
                dtUser = (DataTable)Session["member"];
            }
            else if (Session["restaurant"] != null)
            {
                strBusinessOwner = "";
                dtUser = (DataTable)Session["restaurant"];
            }
            else if (Session["sale"] != null)
            {
                strBusinessOwner = "none";
                dtUser = (DataTable)Session["sale"];
            }
            else if (Session["user"] != null)
            {
                strBusinessOwner = "none";
                dtUser = (DataTable)Session["user"];
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                System.Web.HttpBrowserCapabilities browser = Request.Browser;

                PnlSignin.Visible = false;
                float browserversion = 0;
                float.TryParse(browser.Version, out browserversion);
                if (browser.Browser.Trim() == "IE" && browserversion != 0 && browserversion <= 7)
                {
                    pnlForOldBrowserUsers.Visible = true;                    
                    PnlUser.Visible = false;
                }
                else
                {
                    pnlForOldBrowserUsers.Visible = false;
                    PnlUser.Visible = true;
                }                                
                UserName = "Hi, " + dtUser.Rows[0]["firstName"].ToString().Trim() + " " + dtUser.Rows[0]["lastName"].ToString().Trim();

                if (dtUser.Rows[0]["profilePicture"] != null && dtUser.Rows[0]["profilePicture"].ToString().Trim() != "" && File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\images\\ProfilePictures\\" + dtUser.Rows[0]["profilePicture"].ToString().Trim()))
                {
                    imgLoginUser.ImageUrl = ConfigurationManager.AppSettings["YourSecureSite"].ToString().Trim() + "images/ProfilePictures/" + dtUser.Rows[0]["profilePicture"].ToString().Trim();
                }
                else if (dtUser.Rows[0]["FB_userID"] != null && dtUser.Rows[0]["FB_userID"].ToString().Trim() != "")
                {
                    imgLoginUser.ImageUrl = "https://graph.facebook.com/" + dtUser.Rows[0]["FB_userID"].ToString().Trim() + "/picture";
                }
                else
                {
                    imgLoginUser.ImageUrl = ConfigurationManager.AppSettings["YourSecureSite"].ToString().Trim() + "images/disImg.gif";
                }

            }
            else
            {
                pnlForOldBrowserUsers.Visible = false;
                PnlUser.Visible = false;
                PnlSignin.Visible = true;
            }
        }
        else
        {
            pnlForOldBrowserUsers.Visible = false;
            PnlUser.Visible = false;
            PnlSignin.Visible = true;
        }

    }
  
    #region "Remove the User Info Cookie"

    private void RemoveUserInfoCookie()
    {
        try
        {
            HttpCookie cookie = Request.Cookies["tastygo_ui"];
            //Remove the Cookie
            if (cookie != null)
            {
                //Response.Cookies.Remove("tastygo_ui");
                Response.Cookies["tastygo_ui"].Expires = DateTime.Now;
            }
            HttpCookie FB_cookie = Request.Cookies["fbsr_" + ConfigurationManager.AppSettings["Application_ID"].ToString()];
            if (FB_cookie != null)
            {
                Response.Cookies["fbsr_" + ConfigurationManager.AppSettings["Application_ID"].ToString()].Expires = DateTime.Now;
            }
            HttpCookie cookie2 = Request.Cookies["tastygoLogin"];
            //Remove the Cookie
            if (cookie2 != null)
            {
                //Response.Cookies.Remove("tastygo_ui");
                Response.Cookies["tastygoLogin"].Expires = DateTime.Now;
            }

        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    #endregion
 
    private string GetUserRefferalId()
    {
        string strRefId = "";

        try
        {
            HttpCookie cookie = Request.Cookies["tastygo_userID"];

            if (cookie != null)
            {
                strRefId = cookie.Values[0].ToString().Trim();
            }
        }
        catch (Exception ex)
        { }

        return strRefId;
    }

    protected void lnkBtnLogOut_Click(object sender, EventArgs e)
    {
        try
        {
            PnlUser.Visible = false;
            lnkBtnLogOut.Visible = false;
            strBusinessOwner = "none";
            Session.RemoveAll();

            //Remove the Cookie here
            RemoveUserInfoCookie();
            Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/Default.aspx", false);

        }
        catch (Exception ex)
        {

        }
    }
    
}
