using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Index2 : System.Web.UI.Page
{
    BLLCities objCity = new BLLCities();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
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

            //HttpCookie cookie = Request.Cookies["newslettersubscribe"];
            //if (cookie != null)
            //{
            //    Response.Redirect("default.aspx", true);
            //}
            //string sURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            //string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            //if (!(sURL.ToLower().Contains("://www.")))
            //{
            //    sURL = sURL.Replace("://", "://www.");
            //    Response.Redirect(sURL, false);
            //    Response.End();
            //}
        }
        catch (Exception ex)
        {

        }
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

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {

            try
            {
                DataTable dt = objCity.getAllCitiesWithProvinceAndCountryInfo();
                ListItem item;

                string ddlCityGroupScript = "";
                string CityName = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ListItem(dt.Rows[i]["cityName"].ToString(), dt.Rows[i]["cityId"].ToString());
                    item.Attributes["Country"] = dt.Rows[i]["CountryName"].ToString();
                    ddlCity.Items.Add(item);
                    if (CityName == "" || CityName != dt.Rows[i]["CountryName"].ToString())
                    {
                        ddlCityGroupScript += "$(\"#ddlCity option[Country='" + dt.Rows[i]["CountryName"].ToString() + "']\").wrapAll(\"<optgroup label='" + dt.Rows[i]["CountryName"].ToString() + "'>\");";
                        CityName = dt.Rows[i]["CountryName"].ToString();
                    }

                }

                string CallJavaScript = " <script type=\"text/javascript\">";
                CallJavaScript += ddlCityGroupScript;
                CallJavaScript += "TraceIP();";
                CallJavaScript += "</script>";

                ltRunScript.Text = CallJavaScript;

                //HttpCookie Mycookie = Request.Cookies["newslettersubscribe"];
                //if (Mycookie != null)
                //{
                //    Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/" + ddlCity.SelectedItem.Text.ToString().Trim().Replace(' ', '.'));
                //    Response.End();
                //}

            }
            catch (Exception ex)
            {

            }
        }
    }
}