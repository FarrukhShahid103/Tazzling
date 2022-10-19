using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using System.IO;

using System.Net;
using System.Xml;
using System.Threading;
using System.Net.Mail;
using System.Text;

public partial class _Preview : System.Web.UI.Page
{
    BLLDeals objDeal = new BLLDeals();
    BLLCities objCities = new BLLCities();
    public string strShareLink = "";
    public string strFBString = "";
    public string strDealExpiredText = "Buy Now";
    public string strCheckOutLink = "";
    public string strDealDiscuessionLink = "";
    public string strCurrentCityName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtUser = null;

        if (ViewState["title"] != null)
        {
            Page.Title = ViewState["title"].ToString();
        }
        if (!IsPostBack)
        {


            //Save UserID for the Referral Banner into the Cookies
            objDeal = new BLLDeals();
            GECEncryption objEnc = new GECEncryption();
            if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString().Trim() != "")
            {
                //SaveUserIdInCookie(objEnc.DecryptData("userID", Server.UrlDecode(Request.QueryString["uid"].ToString().Trim().Replace("_", "%")).Replace(" ", "+")));
                int userID = 0;
                string[] strUserInfo = Request.QueryString["uid"].Split('_');
                if (strUserInfo.Length == 2 && Int32.TryParse(strUserInfo[0].Trim(), out userID))
                {
                    userID -= 111111;
                    long dealID = 0;
                    if (Int64.TryParse(strUserInfo[1].Trim(), out dealID))
                    {
                        objDeal.DealId = dealID;
                    }
                    if (userID > 0)
                    {
                        SaveAffiliateUserIdInCookie(userID.ToString());
                    }
                }
                else if (Int32.TryParse(Request.QueryString["uid"].Trim(), out userID))
                {
                    userID -= 111111;
                    if (userID > 0)
                    {
                        SaveAffiliateUserIdInCookie(userID.ToString());
                    }
                }
                else
                {
                    SaveAffiliateUserIdInCookie(objEnc.DecryptData("userID", Server.UrlDecode(Request.QueryString["uid"].ToString().Trim().Replace("_", "%")).Replace(" ", "+")));
                }
            }

            //Save UserID for the Affiliate Banner into the Cookies
            if (Request.QueryString["uid_ab"] != null && Request.QueryString["uid_ab"].ToString().Trim() != "")
            {
                SaveAffiliateUserIdInCookie(objEnc.DecryptData("userID", Server.UrlDecode(Request.QueryString["uid_ab"].ToString().Trim().Replace("_", "%")).Replace(" ", "+")));
            }

            //Save the Value In Cookie in the Case of Referral Process
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {


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
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    //Get the AffiliateInfo from Cookie 
                    //If exits then it will update into the User Info data table
                    ViewState["userId"] = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString().Trim()) + 111111;
                    GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                }
            }
            string strCityID = "337";
            DataTable dtCity = null;
            if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() != "" && Request.QueryString["cid"].ToString().Trim() != "0")
            {
                HttpCookie citycookie = Request.Cookies["yourCity"];
                if (citycookie == null)
                {
                    citycookie = new HttpCookie("yourCity");
                }
                citycookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(citycookie);
                citycookie["yourCity"] = Request.QueryString["cid"].ToString().Trim();
                strCityID = Request.QueryString["cid"].ToString().Trim();
            }
            else if (Request.QueryString["cName"] != null && Request.QueryString["cName"].ToString().Trim() != "")
            {
                string[] strCName;
                if (Request.QueryString["cName"].ToString().Trim().Contains('_'))
                {
                    strCName = Request.QueryString["cName"].ToString().Trim().Split('_');
                    if (strCName[0].Trim() == "St.Catharines")
                    {
                        objCities.cityName = "St. Catharines";
                        objDeal.DealId = Convert.ToInt64(strCName[1].Trim());
                    }
                    else
                    {
                        objCities.cityName = strCName[0].Trim().Replace('.', ' ');
                        objDeal.DealId = Convert.ToInt64(strCName[1].Trim());
                    }
                }
                else
                {
                    if (Request.QueryString["cName"].ToString().Trim() == "St.Catharines")
                    {
                        objCities.cityName = "St. Catharines";
                    }
                    else
                    {
                        objCities.cityName = Request.QueryString["cName"].ToString().Trim().Replace('.', ' ');
                    }
                }
                // objCities.cityName = Request.QueryString["cName"].ToString().Trim().Replace('.', ' ');
                dtCity = objCities.getCityDetailByName();
                if (dtCity != null && dtCity.Rows.Count > 0)
                {
                    HttpCookie citycookie = Request.Cookies["yourCity"];
                    if (citycookie == null)
                    {
                        citycookie = new HttpCookie("yourCity");
                    }
                    citycookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(citycookie);
                    citycookie["yourCity"] = dtCity.Rows[0]["cityId"].ToString();
                    strCityID = dtCity.Rows[0]["cityId"].ToString();
                }
                else
                {
                    HttpCookie citycookie = Request.Cookies["yourCity"];
                    if (citycookie == null)
                    {
                        citycookie = new HttpCookie("yourCity");
                    }
                    citycookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(citycookie);
                    citycookie["yourCity"] = "337";
                    strCityID = "337";
                }
            }
            else
            {
                HttpCookie citycookie = Request.Cookies["yourCity"];
                if (citycookie == null)
                {
                    try
                    {
                        string sPath = "http://api.ipinfodb.com/v3/ip-city/?key=e196d3461698a1fd4f6c8c5c5ac1c29ea5e73f0c1ab6620791469c63786458fa&ip=" + Request.UserHostAddress + "&format=xml";
                        WebClient client = new WebClient();
                        string ipDetail = client.DownloadString(sPath).ToString();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(ipDetail);
                        if (doc.GetElementsByTagName("statusCode").Item(0).InnerText.ToString().Trim().ToUpper() == "OK")
                        {
                            if (doc.GetElementsByTagName("countryCode").Item(0).InnerText.ToString().Trim().ToUpper() == "CA")
                            {
                                if (doc.GetElementsByTagName("regionName").Item(0).InnerText.ToString().Trim().ToUpper() == "BRITISH COLUMBIA")
                                {
                                    citycookie = new HttpCookie("yourCity");
                                    citycookie.Expires = DateTime.Now.AddMonths(1);
                                    Response.Cookies.Add(citycookie);
                                    citycookie["yourCity"] = "337";
                                    strCityID = "337";
                                }
                                else if (doc.GetElementsByTagName("regionName").Item(0).InnerText.ToString().Trim().ToUpper() == "ALBERTA")
                                {
                                    citycookie = new HttpCookie("yourCity");
                                    citycookie.Expires = DateTime.Now.AddMonths(1);
                                    Response.Cookies.Add(citycookie);
                                    citycookie["yourCity"] = "1376";
                                    strCityID = "1376";
                                }
                                else if (doc.GetElementsByTagName("regionName").Item(0).InnerText.ToString().Trim().ToUpper() == "ONTARIO")
                                {
                                    citycookie = new HttpCookie("yourCity");
                                    citycookie.Expires = DateTime.Now.AddMonths(1);
                                    Response.Cookies.Add(citycookie);
                                    citycookie["yourCity"] = "338";
                                    strCityID = "338";
                                }
                                else
                                {
                                    citycookie = new HttpCookie("yourCity");
                                    citycookie.Expires = DateTime.Now.AddMonths(1);
                                    Response.Cookies.Add(citycookie);
                                    citycookie["yourCity"] = strCityID;
                                }
                            }
                            else
                            {
                                citycookie = new HttpCookie("yourCity");
                                citycookie.Expires = DateTime.Now.AddMonths(1);
                                Response.Cookies.Add(citycookie);
                                citycookie["yourCity"] = strCityID;
                            }
                        }
                        else
                        {
                            citycookie = new HttpCookie("yourCity");
                            citycookie.Expires = DateTime.Now.AddMonths(1);
                            Response.Cookies.Add(citycookie);
                            citycookie["yourCity"] = strCityID;
                        }
                    }
                    catch (Exception ex)
                    {
                        citycookie = new HttpCookie("yourCity");
                        citycookie.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(citycookie);
                        citycookie["yourCity"] = strCityID;
                    }
                }
                else
                {
                    strCityID = citycookie.Values[0].ToString().Trim();
                }
            }
            if (strCityID == "0")
            {
                strCityID = "337";
                HttpCookie citycookie = new HttpCookie("yourCity");
                citycookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(citycookie);
                citycookie["yourCity"] = strCityID;
            }
            if (dtCity != null && dtCity.Rows.Count > 0)
            {
                objDeal.CreatedDate = Misc.getResturantLocalTime(Convert.ToInt32(dtCity.Rows[0]["provinceId"].ToString()));
                strCurrentCityName = dtCity.Rows[0]["cityName"].ToString().Trim().Replace(' ', '.');
            }
            else
            {
                objCities.cityId = Convert.ToInt32(strCityID);
                dtCity = objCities.getCityByCityId();
                if (dtCity.Rows.Count > 0)
                {
                    objDeal.CreatedDate = Misc.getResturantLocalTime(Convert.ToInt32(dtCity.Rows[0]["provinceId"].ToString()));
                    strCurrentCityName = dtCity.Rows[0]["cityName"].ToString().Trim().Replace(' ', '.');
                }
            }

            DataTable dtDeal = null;
            bool executeGetDealByCity = false;
            //For Side Deal Implementation
            if (objDeal.DealId == 0)
            {
                if (Request.QueryString["sidedeal"] != null && Request.QueryString["sidedeal"].ToString().Trim() != "")
                {
                    int dealid = 0;
                    int.TryParse(Request.QueryString["sidedeal"].ToString().Trim(), out dealid);
                    objDeal.cityId = Convert.ToInt32(strCityID);
                    objDeal.DealId = dealid;
                    dtDeal = objDeal.getDealinfoByDealID();
                }
                else if (Request.QueryString["dealTitle"] != null && Request.QueryString["dealTitle"].ToString().Trim() != "")
                {
                    objDeal.cityId = Convert.ToInt32(strCityID);
                    objDeal.urlTitle = Request.QueryString["dealTitle"].Trim();
                    dtDeal = objDeal.getCurrentDealByURLTitleForDealPage();
                }
                else
                {
                    executeGetDealByCity = true;
                    objDeal.cityId = Convert.ToInt32(strCityID);
                    dtDeal = objDeal.getCurrentDealByCityIDForPage();
                }
            }
            else
            {
                objDeal.cityId = Convert.ToInt32(strCityID);
                dtDeal = objDeal.getDealinfoByDealID();
            }

            if (dtDeal != null && dtDeal.Rows.Count > 0)
            {

                BLLDealBanner objBanners = new BLLDealBanner();
                objBanners.banner_startTime = objDeal.CreatedDate;
                objBanners.banner_city = Convert.ToInt32(strCityID);
                DataTable dtDealbanners = objBanners.getDealBannerByStartTime();
                if (dtDealbanners != null && dtDealbanners.Rows.Count > 0)
                {
                    ltBanner.Text = "<a id=\"LnkBanner\" href=\"" + dtDealbanners.Rows[0]["banner_link"].ToString() + "\" target=\"_blank\"><img src=\"Images/Banner/" + dtDealbanners.Rows[0]["banner_image"].ToString() + "\" /></a>";

                }
                else
                {

                }
                bool dealNotExpired = true;
                ViewState["MainDealID"] = dtDeal.Rows[0]["dealid"].ToString().Trim();
                DateTime dtTempEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealEndTime"].ToString().Trim());
                TimeSpan ts = dtTempEndTime - objDeal.CreatedDate;
                int intTotalOrders = 0;
                if (dtDeal.Rows[0]["Orders"] != null && dtDeal.Rows[0]["Orders"].ToString().Trim() != "")
                {
                    intTotalOrders = Convert.ToInt32(dtDeal.Rows[0]["Orders"].ToString().ToString());
                    lblDealsSold.Text = dtDeal.Rows[0]["Orders"].ToString().ToString();
                    lblDealsSold2.Text = "Buys : " + dtDeal.Rows[0]["Orders"].ToString().ToString();
                }

                BLLDealDiscussion objBLLDealDiscussion = new BLLDealDiscussion();
                objBLLDealDiscussion.DealId = Convert.ToInt64(dtDeal.Rows[0]["dealid"].ToString().Trim());
                DataTable dtPosts = objBLLDealDiscussion.getLatestDealDiscussionCommentByDealId();
                if ((dtPosts != null) && (dtPosts.Rows.Count > 0))
                {
                    if (dtPosts.Rows[0]["profilePicture"].ToString().Trim() != "")
                    {
                        string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + dtPosts.Rows[0]["profilePicture"].ToString().Trim();
                        if (File.Exists(strFileName))
                        {
                            imgCommentUserImage.ImageUrl = "~/images/ProfilePictures/" + dtPosts.Rows[0]["profilePicture"].ToString().Trim();
                        }
                        else
                        {
                            imgCommentUserImage.ImageUrl = "~/Images/disImg.gif";
                        }
                    }
                    lblTotalPosts.Text = "Posts : " + dtPosts.Rows[0]["TotalComments"].ToString().Trim();

                    lblDealDiscuessionComment.Text = dtPosts.Rows[0]["comments"].ToString().Trim().Length > 65 ? dtPosts.Rows[0]["comments"].ToString().Trim().Substring(0, 64) + "..." : dtPosts.Rows[0]["comments"].ToString().Trim();
                    string strLastName = "";
                    if (dtPosts.Rows[0]["LastName"].ToString().Trim().Length > 0)
                    {
                        strLastName = dtPosts.Rows[0]["LastName"].ToString().Trim().Remove(1, dtPosts.Rows[0]["LastName"].ToString().Trim().Length - 1);
                    }
                    strLastName = dtPosts.Rows[0]["FirstName"].ToString().Trim() + " " + strLastName;
                    lblCommentUserName.Text = strLastName;

                    lblDealDiscuessionComment.ToolTip = "<b>" + strLastName + "</b> says,<br>" + dtPosts.Rows[0]["comments"].ToString().Trim();
                    // imgCommentUserImage.ToolTip = "<b>" + strLastName + "</b><br>" + lblDealsSold2.Text.Trim() + " <br>" + lblTotalPosts.Text.Trim();
                }
                else
                {
                    lblCommentUserName.Text = "";
                    lblCommentUserName.Visible = false;
                    lblDealDiscuessionComment.Text = "Be the first to comment.";
                }

                if (dtDeal.Rows[0]["dealDelMaxLmt"] != null && dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim() != "0" && (intTotalOrders >= Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim())))
                {
                    strDealExpiredText = "Sold Out";
                    dealNotExpired = false;
                    strCheckOutLink = "";
                }
                else if (ts.Milliseconds < 0)
                {
                    strDealExpiredText = "Deal Expired";
                    dealNotExpired = false;
                    strCheckOutLink = "";
                }
                else
                {
                   // strCheckOutLink = ConfigurationManager.AppSettings["YourSecureSite"] + "checkout.aspx?did=" + dtDeal.Rows[0]["dealId"].ToString();
                }
                strDealDiscuessionLink = ConfigurationManager.AppSettings["YourSite"] + "/deal_discussion.aspx?did=" + dtDeal.Rows[0]["dealId"].ToString();
                string[] strDealImages = dtDeal.Rows[0]["images"].ToString().Split(',');
                if (strDealImages.Length > 0)
                {
                    if ((strDealImages.Length == 1) || (strDealImages[1].Trim() == "" && strDealImages[2].Trim() == ""))
                    {
                        if (dealNotExpired)
                        {
                            ltSlideShow.Text = "<div style='cursor:pointer;' onclick=\"document.location.href='" + ConfigurationManager.AppSettings["YourSecureSite"] + "checkout.aspx?did=" + dtDeal.Rows[0]["dealId"].ToString() + "'\">";
                        }
                        else
                        {
                            ltSlideShow.Text = "<div>";
                        }
                        Literal lit = (Literal)Master.FindControl("linkID");

                        if (lit != null)
                        {
                            lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' >";
                        }
                        ltSlideShow.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' alt='Slideshow Image 1'/>";
                        ltSlideShow.Text += "</div>";
                    }
                    else
                    {
                        ltSlideShow.Text += "<div id=\"slideshow\">";
                        for (int i = 0; i < strDealImages.Length; i++)
                        {
                            if (i == 0)
                            {
                                Literal lit = (Literal)Master.FindControl("linkID");
                                if (lit != null)
                                {
                                    lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' >";
                                }

                                ltSlideShow.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' class='active'>";
                            }
                            else if (strDealImages[i].ToString().Trim() != "")
                            {
                                ltSlideShow.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'>";
                            }
                        }
                        ltSlideShow.Text += "</div>";
                    }
                }
                //Set the Page Title


                if (dtDeal.Rows[0]["topTitle"] != null && dtDeal.Rows[0]["topTitle"].ToString().Trim() != "")
                {
                    lblDealTopTitle.Text = dtDeal.Rows[0]["topTitle"].ToString().Trim();
                }

                Page.Title = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtDeal.Rows[0]["title"].ToString().Trim();
                ViewState["title"] = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtDeal.Rows[0]["title"].ToString().Trim();

                lblDealTitle.Text = dtDeal.Rows[0]["title"].ToString().Trim();

                // Get the Sub Deal Info by Deal ID
                GetandSetSubDealsInfoIntoGrid(int.Parse(dtDeal.Rows[0]["dealId"].ToString()), dealNotExpired, strDealExpiredText, strCityID);

                if (ViewState["userId"] != null)
                {
                    strShareLink = HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                         + "/r/" + ViewState["userId"].ToString() + "_" + dtDeal.Rows[0]["dealId"].ToString()));
                }
                else
                {
                    strShareLink = HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                           + "/" + strCurrentCityName + "_" + dtDeal.Rows[0]["dealId"].ToString()));
                }

                strFBString = Server.UrlEncode(ConfigurationManager.AppSettings["YourSite"].ToString() + "/" + strCurrentCityName + "_" + dtDeal.Rows[0]["dealId"].ToString());

                if (dtDeal.Rows[0]["Orders"] != null && dtDeal.Rows[0]["Orders"].ToString().Trim() != "")
                {
                    intTotalOrders = Convert.ToInt32(dtDeal.Rows[0]["Orders"].ToString());

                    if (dtDeal.Rows[0]["dealDelMaxLmt"] != null && dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim() != "0")
                    {
                        if (intTotalOrders >= Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()))
                        {
                            strDealExpiredText = "Sold out";
                        }
                        else if (!dealNotExpired)
                        {
                            strDealExpiredText = "Deal Expired";
                        }
                    }
                    else if (!dealNotExpired)
                    {
                        strDealExpiredText = "Deal Expired";
                    }
                }
                lblBusinessName.Text = dtDeal.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblBusinessPhone.Text = dtDeal.Rows[0]["phone"].ToString().Trim();
                if (dtDeal.Rows[0]["url"] != null && dtDeal.Rows[0]["url"].ToString().Trim() != "" || dtDeal.Rows[0]["url"].ToString().Trim().ToLower() != "n/a")
                {
                    HPBusiness.NavigateUrl = dtDeal.Rows[0]["url"].ToString().Trim();
                    HPBusiness.Text = "Website";
                    HPBusiness.Target = "_blank";
                }
                else
                {
                    HPBusiness.Visible = false;
                }
                DateTime dtEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealEndTime"].ToString().Trim());


                if (Convert.ToBoolean(dtDeal.Rows[0]["reviewExist"].ToString().Trim()))
                {

                    string Reviews = "<div style='clear:both;'>";
                    if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 0)
                    {
                        Reviews += "<img src='Images/rate/0Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 1)
                    {
                        Reviews += "<img src='Images/rate/1Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 1.5)
                    {
                        Reviews += "<img src='Images/rate/15Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 2)
                    {
                        Reviews += "<img src='Images/rate/2Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 2.5)
                    {
                        Reviews += "<img src='Images/rate/25Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 3)
                    {
                        Reviews += "<img src='Images/rate/3Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 3.5)
                    {
                        Reviews += "<img src='Images/rate/35Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 4)
                    {
                        Reviews += "<img src='Images/rate/4Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 4.5)
                    {
                        Reviews += "<img src='Images/rate/45Star.png'/>";
                    }
                    else if (float.Parse(dtDeal.Rows[0]["yelpRate"].ToString().Trim()) == 5)
                    {
                        Reviews += "<img src='Images/rate/5Star.png'/>";
                    }
                    Reviews += "</div>";
                    Reviews += "<div style='clear: both; font-size: 13px; font-weight: bold; color: #096bc7; padding-top: 5px;'>";
                    Reviews += "<a target='_blank' href='" + dtDeal.Rows[0]["yelpLink"].ToString().Trim() + "' >";
                    Reviews += dtDeal.Rows[0]["yelpText"].ToString().Trim();
                    Reviews += "</a></div>";
                    ltYelpStart.Text = Reviews;
                }
                else
                {
                    ltYelpStart.Text = "";
                    ltYelpStart.Visible = false;
                }



                //if (strCityID == "338")
                //{
                //    dtEndTime = dtEndTime.AddHours(-3);
                //}
                if (!dealNotExpired)
                {
                    dtEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealStartTime"].ToString().Trim());
                }


                ltCountDown.Text += "<script type='text/javascript'>";
                ltCountDown.Text += "function serverTime() { ";
                ltCountDown.Text += "var time = null; ";
                ltCountDown.Text += "$.ajax({url: '" + System.Configuration.ConfigurationManager.AppSettings["YourSite"].ToString() + "/getStateLocalTime.aspx?sid=" + dtDeal.Rows[0]["provinceId"].ToString().Trim() + "', ";
                ltCountDown.Text += "async: false, dataType: 'text', ";
                ltCountDown.Text += "success: function(text) { ";
                ltCountDown.Text += "time = new Date(text); ";
                ltCountDown.Text += "}, error: function(http, message, exc) { ";
                ltCountDown.Text += "time = new Date(); ";
                ltCountDown.Text += "}}); ";
                ltCountDown.Text += "return time; ";
                ltCountDown.Text += "}";


                ltCountDown.Text += "$(function () {";
                ltCountDown.Text += "var austDay = new Date();";
                ltCountDown.Text += "austDay = new Date(" + dtEndTime.Year + "," + (dtEndTime.Month - 1) + "," + dtEndTime.Day + "," + dtEndTime.Hour + "," + dtEndTime.Minute + ",0);";
                ltCountDown.Text += "$('#defaultCountdown').countdown({until: austDay,compact: true, serverSync: serverTime});";
                ltCountDown.Text += "$('#year').text(austDay.getFullYear());";
                ltCountDown.Text += "});</script>";

                lblDealPrice.Text = "$" + dtDeal.Rows[0]["sellingPrice"].ToString().Trim();
                lblValuePrice.Text = "<b>Value:</b> $" + dtDeal.Rows[0]["valuePrice"].ToString().Trim();
                double dSellPrice = Convert.ToDouble(dtDeal.Rows[0]["sellingPrice"].ToString().Trim());
                double dActualPrice = Convert.ToDouble(dtDeal.Rows[0]["valuePrice"].ToString().Trim());
                double dDiscount = 0;
                if (dSellPrice == 0)
                {
                    dDiscount = 100;
                }
                else
                {
                    dDiscount = (100 / dActualPrice) * (dActualPrice - dSellPrice);
                }

                lblDealDiscount.Text = "<b>Discount:</b> " + Convert.ToInt32(dDiscount).ToString() + "%";

                lblFinePrint.Text = dtDeal.Rows[0]["finePrint"].ToString().Trim();
                lblHighlights.Text = dtDeal.Rows[0]["dealHightlights"].ToString().Trim();
                lblDealDetail.Text = dtDeal.Rows[0]["description"].ToString().Trim();

                //lblCompanyDetail.Text = dtDeal.Rows[0]["restaurantBusinessName"].ToString().Trim() + "<br>" + dtDeal.Rows[0]["phone"].ToString().Trim() + "<br>" + dtDeal.Rows[0]["restaurantAddress"].ToString().Trim() + "<br>";

                //if (dtDeal.Rows[0]["url"] != null && dtDeal.Rows[0]["url"].ToString().Trim() != "")
                //{
                //    hlBusinessURL.Visible = true;
                //    hlBusinessURL.Text = dtDeal.Rows[0]["url"].ToString().Trim();
                //    hlBusinessURL.NavigateUrl = dtDeal.Rows[0]["url"].ToString().Trim();
                //}
                try
                {
                    BLLRestaurantGoogleAddresses objResturant = new BLLRestaurantGoogleAddresses();
                    objResturant.restaurantId = Convert.ToInt64(dtDeal.Rows[0]["restaurantId"].ToString().Trim());
                    DataTable dtResturant = objResturant.getAllRestaurantGoogleAddressesByRestaurantID();
                    if (dtResturant != null && dtResturant.Rows.Count > 0)
                    {

                        if (dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "online deal" || dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "online" || dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "deal online")
                        {
                            imgGoogleMap.Visible = false;
                            dlGooglePaths.Visible = false;
                        }
                        else
                        {
                            imgGoogleMap.ImageUrl = ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + ".png";
                            dlGooglePaths.DataSource = dtResturant.DefaultView;
                            dlGooglePaths.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                { }
                Page.Title = Page.Title = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtDeal.Rows[0]["title"].ToString().Trim();
                ViewState["title"] = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtDeal.Rows[0]["title"].ToString().Trim();

                Literal litMeta = (Literal)Master.FindControl("Meta1");
                if (litMeta != null)
                {
                    if (dtDeal.Rows[0]["dealHightlights"].ToString().Trim() != "")
                    {
                        litMeta.Text = "<meta name='description' content='" + StripHTML(dtDeal.Rows[0]["dealHightlights"].ToString()).Replace('"', '\"') + "' />";
                    }
                    else
                    {
                        litMeta.Text = "<meta name='description' content='" + dtDeal.Rows[0]["title"].ToString().Replace('"', '\"') + "' />";
                    }
                }


                if (!executeGetDealByCity)
                {
                    objDeal.cityId = Convert.ToInt32(strCityID);
                    dtDeal = objDeal.getCurrentDealByCityIDForPage();
                }
                if (dtDeal.Rows.Count > 1 || (objDeal.DealId != 0 && Convert.ToInt32(dtDeal.Rows[0]["DealId"].ToString()) != objDeal.DealId))
                {
                    GetAndFillMoreDeals(dtDeal);
                }

            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/nodeal.aspx?cid=" + strCityID, false);
            }
        }
    }
    protected string getSubDealURL(object dealId, object Orders, object dealDelMaxLmt)
    {
        if (Convert.ToInt32(dealDelMaxLmt.ToString().Trim()) != 0 && Convert.ToInt32(Orders.ToString().Trim()) >= Convert.ToInt32(dealDelMaxLmt.ToString().Trim()))
        {
            return "#";
        }
        else
        {
            return ConfigurationManager.AppSettings["YourSecureSite"] + "checkout.aspx?did=" + dealId.ToString();
        }
    }
    protected string getSubDealPrice(object dealDelMaxLmt, object Orders, object sellingPrice)
    {
        if (Convert.ToInt32(dealDelMaxLmt.ToString().Trim()) != 0 && Convert.ToInt32(Orders.ToString().Trim()) >= Convert.ToInt32(dealDelMaxLmt.ToString().Trim()))
        {
            return "Sold Out";
        }
        else
        {
            return "CS " + sellingPrice.ToString().Trim();
        }
        return "CS " + sellingPrice.ToString().Trim();
    }
    private void GetandSetSubDealsInfoIntoGrid(int iDealId, bool dealNotExpired, string strDealExpiredText, string strCityID)
    {
        try
        {
            DataTable dtSubDealsInfo = null;
            objDeal.cityId = Convert.ToInt32(strCityID);
            objDeal.ParentDealId = iDealId;

            dtSubDealsInfo = objDeal.getCurrentSubDealInfoByParnetDealIDForDealPage();

            if ((dtSubDealsInfo != null) && (dtSubDealsInfo.Rows.Count > 1))
            {
                this.grdViewPrices.DataSource = dtSubDealsInfo;
                this.grdViewPrices.DataBind();

                //Set the Page Title
                Page.Title = Page.Title = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim() == "" ? dtSubDealsInfo.Rows[0]["title"].ToString().Trim() : dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim();
                ViewState["title"] = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim() == "" ? dtSubDealsInfo.Rows[0]["title"].ToString().Trim() : dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim();
                lblDealTitle.Text = dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim() == "" ? dtSubDealsInfo.Rows[0]["title"].ToString().Trim() : dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim();

                //Set the Rows Count here
                this.hfPopUpRowsCount.Value = dtSubDealsInfo.Rows.Count.ToString();


                if (dealNotExpired)
                {
                    lblDealPrice.Text = "$" + dtSubDealsInfo.Rows[0]["sellingPrice"].ToString().Trim();
                    lblValuePrice.Text = "<b>Value:</b>  $" + dtSubDealsInfo.Rows[0]["sellingPrice"].ToString().Trim();
                   // strCheckOutLink = "javascript:ShowAddressPopUp();";
                }
                else
                {
                }
                //Hide and Show the Buy Deal Image Button                
            }
            else
            {
                //Set the Rows Count here
                this.hfPopUpRowsCount.Value = "0";
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    const string HTML_TAG_PATTERN = "<.*?>";
    string StripHTML(string inputString)
    {
        return Regex.Replace
          (inputString, HTML_TAG_PATTERN, string.Empty);
    }
    private void GetAndFillMoreDeals(DataTable dtDeals)
    {
        try
        {

            for (int i = 0; i < dtDeals.Rows.Count; i++)
            {
                if (ViewState["MainDealID"].ToString().Trim() == dtDeals.Rows[i]["dealId"].ToString().Trim())
                {
                    dtDeals.Rows[i].Delete();
                    dtDeals.AcceptChanges();
                }
            }

            if (dtDeals.Rows.Count > 0)
            {
                this.gridDeals.DataSource = dtDeals;
                this.gridDeals.DataBind();

                /*  string HTML = "";

                  for (int i = 0; i < dtDeals.Rows.Count; i++)
                  {

                      HTML += "<div  onclick=\"window.location.href='detail.aspx?did=" + dtDeals.Rows[i]["dealId"].ToString().Trim() + "' ;\"  class='SubDeal_SliderElement MoreDealImage'>";
                      HTML += "<div>";
                      HTML += "<sohail herf='javascript:void(0);'>";
                      HTML += "<strong>";
                      HTML += "<div style='margin: -45px 5px 5px; font-size:15px; font-weight:normal;'>";
                      HTML += dtDeals.Rows[i]["shortTitle"].ToString().Trim().Length > 160 ? dtDeals.Rows[i]["shortTitle"].ToString().Trim().Substring(0, 157) + " ..." : dtDeals.Rows[i]["shortTitle"].ToString().Trim();
                      HTML += "</div>";
                      HTML += "<div style='cursor:pointer;margin: 15px 5px 5px;' onclick='javascript:window.location ='\"detail.aspx?did=" + dtDeals.Rows[i]["dealid"] + " ; class='button pill'>View this deal</div>";
                      HTML += "</strong>";
                      HTML += "<img src='Images/dealfood/" + dtDeals.Rows[i]["restaurantId"].ToString().Trim() + '/' + dtDeals.Rows[i]["image1"].ToString().Trim() + "' height='145px' width='285px' />";
                      HTML += "</div>";
                      HTML += "<div class='SubDeal_Bottom'>";
                      HTML += "<div class='SubDeal_Text'>";
                      HTML += dtDeals.Rows[i]["shortTitle"].ToString().Trim() != "" ? dtDeals.Rows[i]["shortTitle"].ToString().Trim().Length < 30 ? dtDeals.Rows[i]["shortTitle"].ToString().Trim() : dtDeals.Rows[i]["shortTitle"].ToString().Trim().Substring(0, 29) + "..." : dtDeals.Rows[i]["dealPageTitle"].ToString().Trim() != "" ? dtDeals.Rows[i]["dealPageTitle"].ToString().Trim().Length < 30 ? dtDeals.Rows[i]["dealPageTitle"].ToString().Trim() : dtDeals.Rows[i]["dealPageTitle"].ToString().Trim().Substring(0, 29) + "..." : dtDeals.Rows[i]["title"].ToString().Trim().Length < 30 ? dtDeals.Rows[i]["title"].ToString().Trim() : dtDeals.Rows[i]["title"].ToString().Trim().Substring(0, 29) + "...";
                      HTML += "</div>";
                      HTML += " <div class='SubDeal_PriceTag'>";
                      HTML += "<div class='SubDeal_PriceTagText shadowText'>";
                      HTML += Convert.ToInt32((100 / float.Parse(dtDeals.Rows[i]["valuePrice"].ToString())) * (float.Parse(dtDeals.Rows[i]["valuePrice"].ToString()) - float.Parse(dtDeals.Rows[i]["sellingPrice"].ToString()))).ToString() + "% OFF";
                      //HTML +="$" + dtDeals.Rows[i]["SellingPrice"].ToString().Trim();
                      HTML += "</div>";
                      HTML += "</div>";
                      HTML += " </div>";
                      HTML += " </sohail>";
                      HTML += "</div>";



                  }
                  */
                //  ltMoreDeals.Text = HTML;

            }

        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private bool GetAndSetAffInfoFromCookieInUserInfo(int iUserId)
    {
        bool bStatus = false;

        try
        {
            string strAffiliateRefId = "";
            string strAffiliateDate = "";

            HttpCookie cookieAffId = Request.Cookies["tastygo_affiliate_userID"];
            HttpCookie cookieAddDate = Request.Cookies["tastygo_affiliate_date"];

            //Remove the Cookie
            if ((cookieAffId != null) && (cookieAddDate != null))
            {
                if ((cookieAffId.Values.Count > 0) && (cookieAddDate.Values.Count > 0))
                {
                    //It should not be the same user
                    if (int.Parse(cookieAffId.Values[0].ToString()) != iUserId)
                    {
                        strAffiliateRefId = cookieAffId.Values[0].ToString();
                        strAffiliateDate = cookieAddDate.Values[0].ToString();

                        BLLUser objBLLUser = new BLLUser();
                        objBLLUser.userId = iUserId;
                        objBLLUser.affComID = int.Parse(strAffiliateRefId);
                        objBLLUser.affComEndDate = DateTime.Parse(strAffiliateDate);
                        objBLLUser.updateUserAffCommIDByUserId();

                        cookieAffId.Values.Clear();
                        cookieAddDate.Values.Clear();
                        cookieAffId.Expires = DateTime.Now;
                        cookieAddDate.Expires = DateTime.Now;

                        Response.Cookies.Add(cookieAffId);
                        Response.Cookies.Add(cookieAddDate);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    //Save the Value In Cookie in the Case of Referral Process
    private void SaveAffiliateUserIdInCookie(string strUserId)
    {
        int intuserID = 0;
        if (int.TryParse(strUserId, out intuserID))
        {
            //Save the Affiliate User ID
            HttpCookie cookie = Request.Cookies["tastygo_affiliate_userID"];
            cookie = new HttpCookie("tastygo_affiliate_userID");
            cookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(cookie);
            cookie["tastygo_affiliate_userID"] = strUserId;

            //Save the Affiliate Visit Date
            HttpCookie cookieDate = Request.Cookies["tastygo_affiliate_date"];
            cookieDate = new HttpCookie("tastygo_affiliate_date");
            cookieDate.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(cookieDate);
            cookieDate["tastygo_affiliate_date"] = DateTime.Now.AddHours(1).ToString();
        }
    }

}
