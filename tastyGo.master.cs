using System;
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
using System.Net;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;
using GecLibrary;
using System.Net.Mail;
using System.Threading;
using System.Text.RegularExpressions;

public partial class Takeout_tastyGo : System.Web.UI.MasterPage
{
    BLLUser objUser = new BLLUser();
    BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
    public string strImage = "Images/slideOpen.png";
    public string strState = "Show";
    public string strDisplay = "none";
    public string strTopBG = "TopArea";
    protected void Page_Init(object sender, EventArgs e)
    {

        HttpCookie fullsite = Request.Cookies["fullsite"];
        if (Request.QueryString["fullsite"] != null)
        {
            if (fullsite == null)
            {
                fullsite = new HttpCookie("fullsite");
            }
            fullsite.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(fullsite);
            fullsite["fullsite"] = "true";
        }
        else if (fullsite == null)
        {
            if (HttpContext.Current.Request.UserAgent.ToLower().Contains("iphone"))
            {
                if (isMobile())
                {
                    Response.Redirect("http://www.mobile.tazzling.com", false);
                }
            }
            else if (HttpContext.Current.Request.UserAgent.ToLower().Contains("ipad"))
            {
                //iPad is the requested client. Write logic here which is specific to iPad.
            }
            else if (isMobile())
            {
                Response.Redirect("http://www.mobile.tazzling.com", false);
            }
        }
        HttpContext.Current.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
        
        if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString().Trim() != "")
        {
            HttpCookie cookie = Request.Cookies["newslettersubscribe"];
            if (cookie == null)
            {
                cookie = new HttpCookie("newslettersubscribe");
                cookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cookie);
                cookie["newslettersubscribe"] = "";
            }
        }
        //else if (Request.QueryString["cName"] != null
        //    && Request.QueryString["cName"].ToString().Trim() != "")
        //{
        //    HttpCookie cookie = Request.Cookies["newslettersubscribe"];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie("newslettersubscribe");
        //        cookie.Expires = DateTime.Now.AddMonths(1);
        //        Response.Cookies.Add(cookie);
        //        cookie["newslettersubscribe"] = "";
        //    }
        //}
        //else
        //{
        //    HttpCookie cookie = Request.Cookies["newslettersubscribe"];
        //    if (cookie == null)
        //    {
        //        Response.Redirect("Index.aspx");
        //        Response.End();
        //    }
        //}

        
        try
        {
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {
                DataTable dtUser = null;
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }

                HttpCookie cookie = Request.Cookies["tastygoSignup"];
                if (cookie == null)
                {
                    cookie = new HttpCookie("tastygoSignup");
                }
                if (dtUser.Rows.Count > 0)
                {
                   
                    if (dtUser.Rows[0]["firstName"].ToString().Length > 9)
                    {
                        userName.Text = dtUser.Rows[0]["firstName"].ToString().Trim().Substring(0, 6) + "...";
                    }
                    else
                    {
                        userName.Text = dtUser.Rows[0]["firstName"].ToString().Trim();
                    }
                }
                cookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cookie);
                cookie["tastygoSignup"] = dtUser.Rows[0]["username"].ToString();
                HttpCookie cookie2 = Request.Cookies["tastygoLogin"];
                if (cookie2 == null)
                {
                    cookie2 = new HttpCookie("tastygoLogin");
                }
                cookie2.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(cookie2);
                cookie2["tastygoLogin"] = "true";
            }

            string sURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            //if (!(sURL.ToLower().Contains("://www.")))
            //{
            //    sURL = sURL.Replace("://", "://www.");
            //    Response.Redirect(sURL, false);
            //}


            
            string sRet = oInfo.Name;
            if (sRet.ToLower() == "shop.aspx")
            {
                strTopBG = "TopArea2";
            }
            
            //if ((sRet.ToLower() == "checkout.aspx" || sRet.ToLower() == "login.aspx" || sRet.ToLower() == "signup.aspx")
            //    && (sURL.ToLower().Contains("http:")))
            //{
            //    string sRedirectPath = sURL.Replace(ConfigurationSettings.AppSettings["http:"].ToString(), ConfigurationSettings.AppSettings["https:"].ToString());
            //    Response.Redirect(sRedirectPath, false);
            //}
            //else if (!((sRet.ToLower() == "checkout.aspx") || (sRet.ToLower() == "login.aspx") || (sRet.ToLower() == "signup.aspx")) && (sURL.ToLower().Contains("https:")))
            //{
            //    Response.Redirect(sURL.Replace(ConfigurationSettings.AppSettings["https:"].ToString(), ConfigurationSettings.AppSettings["http:"].ToString()), false);
            //}


           // string sRet = oInfo.Name;
            if (!((sRet.ToLower() == "login.aspx") || (sRet.ToLower() == "signup.aspx") || (sRet.ToLower() == "tastylogin.aspx")))
            {
                if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
                {
                 //   Response.Redirect("TastyLogin.aspx", false);
                }
            }        

        }
        catch (Exception ex)
        { }
    }

    #region "Get User Info From Cookie If exists then Logged-in into the System"
    private void getUserInfoFromCookieIfExists()
    {
        try
        {
            string strUserName = "";
            string strPwd = "";

            HttpCookie cookie = Request.Cookies["tastygo_ui"];

            //Remove the Cookie
            if (cookie != null)
            {
                strUserName = cookie.Values[0].Substring(0, cookie.Values[0].IndexOf(":::"));
                strPwd = cookie.Value.Substring((cookie.Value.IndexOf(":::") + 3), (cookie.Value.Length - (cookie.Value.IndexOf(":::") + 3)));

                GECEncryption objDecrypt = new GECEncryption();

                strUserName = objDecrypt.DecryptData("t7a7s0T7y7g8o", strUserName);
                strPwd = objDecrypt.DecryptData("t7a7s0T7y7g8o", strPwd);

                //Logged In User
                LoggedInUser(strUserName, strPwd);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void LoggedInUser(string strUserName, string strPwd)
    {
        try
        {
            BLLUser obj = new BLLUser();

            obj.userName = strUserName;
            obj.userPassword = strPwd;

            DataTable dtUser = obj.validateUserNamePassword();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                if (dtUser.Rows[0]["userTypeID"].ToString() == "4")
                {
                    Session["member"] = dtUser;
                    Session.Remove("restaurant");
                    Session.Remove("sale");
                    Session.Remove("user");
                }
                else if (dtUser.Rows[0]["userTypeID"].ToString() == "3")
                {
                    Session["restaurant"] = dtUser;
                    Session.Remove("member");
                    Session.Remove("sale");
                    Session.Remove("user");
                }
                else if (dtUser.Rows[0]["userTypeID"].ToString() == "5")
                {
                    Session["sale"] = dtUser;
                    Session.Remove("member");
                    Session.Remove("restaurant");
                    Session.Remove("user");
                }
                else
                {
                    Session["user"] = dtUser;
                    Session.Remove("member");
                    Session.Remove("restaurant");
                    Session.Remove("sale");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpCookie topslide = Request.Cookies["topslide"];
            if (topslide != null)
            {
                if (topslide.Value.Trim() == "topslide=Hide")
                {
                    strImage = "Images/slideColse.png";
                    strState = "Hide";
                    strDisplay = "";
                }
                else
                {
                    strImage = "Images/slideOpen.png";
                    strState = "Show";
                    strDisplay = "none";
                }
                //topslide.Value;
            }

            if (!IsPostBack)
            {
                if (Session["dtProductCart"] != null && Session["dtProductCart"].ToString().Trim() != string.Empty)
                {
                    DataTable dCartValue = (DataTable)Session["dtProductCart"];
                    if (dCartValue != null && dCartValue.Rows.Count > 0)
                    {
                        ltCart.Text = @"<script type='text/javascript'>
                                    $(document).ready(function () { 
                                        setTimeout(function () { 
                                            updateShopingCart(" + dCartValue.Rows.Count.ToString() + @");
                                        },1000);
                                    });
                                </script>";
                    }
                }
                if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
                {
                    pnlUserDropDown.Visible = false;
                    pnlSignupLogin.Visible = true;
                    getUserInfoFromCookieIfExists();
                    string url = "";
                }
                else
                {
                    pnlUserDropDown.Visible = true;
                    pnlSignupLogin.Visible = false;
                }

            }
        }
        catch (Exception ex)
        { }
    }

    public static Boolean isMobile()
    {
        HttpContext curcontext = HttpContext.Current;

        string user_agent = curcontext.Request.ServerVariables["HTTP_USER_AGENT"];
        user_agent = user_agent.ToLower();


        // Checks the user-agent  
        if (user_agent != null)
        {
            // Checks if its a Windows browser but not a Windows Mobile browser  
            if (user_agent.Contains("windows") && !user_agent.Contains("windows ce"))
            {
                return false;
            }

            // Checks if it is a mobile browser  
            string pattern = "up.browser|up.link|windows ce|iphone|iemobile|mini|mmp|symbian|midp|wap|phone|pocket|mobile|pda|psp";
            MatchCollection mc = Regex.Matches(user_agent, pattern, RegexOptions.IgnoreCase);
            if (mc.Count > 0)
                return true;

            // Checks if the 4 first chars of the user-agent match any of the most popular user-agents  
            string popUA = "|acs-|alav|alca|amoi|audi|aste|avan|benq|bird|blac|blaz|brew|cell|cldc|cmd-|dang|doco|eric|hipt|inno|ipaq|java|jigs|kddi|keji|leno|lg-c|lg-d|lg-g|lge-|maui|maxo|midp|mits|mmef|mobi|mot-|moto|mwbp|nec-|newt|noki|opwv|palm|pana|pant|pdxg|phil|play|pluc|port|prox|qtek|qwap|sage|sams|sany|sch-|sec-|send|seri|sgh-|shar|sie-|siem|smal|smar|sony|sph-|symb|t-mo|teli|tim-|tosh|tsm-|upg1|upsi|vk-v|voda|w3c |wap-|wapa|wapi|wapp|wapr|webc|winw|winw|xda|xda-|";
            if (popUA.Contains("|" + user_agent.Substring(0, 4) + "|"))
                return true;
        }

        // Checks the accept header for wap.wml or wap.xhtml support  
        string accept = curcontext.Request.ServerVariables["HTTP_ACCEPT"];
        if (accept != null)
        {
            if (accept.Contains("text/vnd.wap.wml") || accept.Contains("application/vnd.wap.xhtml+xml"))
            {
                return true;
            }
        }

        // Checks if it has any mobile HTTP headers  

        string x_wap_profile = curcontext.Request.ServerVariables["HTTP_X_WAP_PROFILE"];
        string profile = curcontext.Request.ServerVariables["HTTP_PROFILE"];
        string opera = curcontext.Request.Headers["HTTP_X_OPERAMINI_PHONE_UA"];

        if (x_wap_profile != null || profile != null || opera != null)
        {
            return true;
        }

        return false;
    }

    //public void MessageDialog(bool IsErrorDialog, string Title, string Message)
    //{

    //    string jScript;
    //    if (IsErrorDialog)
    //    {
    //        jScript = "<script>";
    //        //jScript += "function pageLoad(){";
    //        jScript += "ErrorDialog('" + Title + "','" + Message + "');";
    //        // jScript += "}";
    //        jScript += "</script>";
    //    }
    //    else
    //    {
    //        jScript = "<script>";
    //        //jScript += "function pageLoad(){";
    //        jScript += "SuccessDialog('" + Title + "','" + Message + "');";
    //        // jScript += "}";
    //        jScript += "</script>";
    //    }
    //    ltScript.Text = jScript;
    //}

    protected void lnkSignOut_Click(object sender, EventArgs e)
    {
        Session.Remove("member");
        Session.Remove("restaurant");
        Session.Remove("sale");
        Session.Remove("user");
        Session.Remove("dtProductCart");
        Response.Redirect("~/default.aspx");
    }

    protected void lnkReferral_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/referral.aspx");
    }

    protected void lnkSubscriptions_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/member_SubscribeCities.aspx");
    }


    protected void lnkPreferences_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/member_preference.aspx");
    }


    protected void lnkAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MyAccountSetting.aspx");
    }


    protected void lnkGifts_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/member_MyGiftTastygo.aspx");
    }


    protected void lnkTazzling_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/MyOrder.aspx");
        Response.Redirect("~/MyOrder.aspx");
    }


}
