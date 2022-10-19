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
public partial class Default10 : System.Web.UI.Page
{
    BLLDeals objDeal = new BLLDeals();
    BLLCities objCities = new BLLCities();

    ArrayList arrayIDs = new ArrayList();

    // BLLDealOrders objOrders = new BLLDealOrders();

    public string strFBString = "";
    public string strShareLink = "";
    public string strPedding = "";
    public string strpaddingCause = "";
    public string strCurrentCityName = "";
    public string MarginTop = "0";
    public string strCheckOutLink = "";
    public string strShareReferal = "";
    public string strImageOnOff = "";
    public string strimgGoogle = "";
    public string strimgGoogleToolTip = "";
    public string strGoogleLink = "";
    public string strSellingPrice = "";
    public string strCauseImage = "";
    public string strCauseLink = "";
    public string strNewarByImagedisplay = "";
    public string strReferralLink = "";

    public string strRSSLink = "";
    public string strDealTitle = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                //For Side Deal Implementation
                if (objDeal.DealId == 0)
                {
                    if (Request.QueryString["sidedeal"] != null && Request.QueryString["sidedeal"].ToString().Trim() != "")
                    {
                        //Get the Current Side Deal Info on Click of View Detail button of Side Deal
                        objDeal.cityId = Convert.ToInt32(strCityID);
                        objDeal.DealId = Convert.ToInt32(Request.QueryString["sidedeal"].ToString().Trim());
                        dtDeal = objDeal.getCurrentDealByDealIDForDealPage2();
                    }
                    else
                    {
                        
                        objDeal.cityId = Convert.ToInt32(strCityID);
                        dtDeal = objDeal.getCurrentDealByCityIDForPage2();
                    }
                }
                else
                {
                    objDeal.cityId = Convert.ToInt32(strCityID);
                    dtDeal = objDeal.getCurrentDealByDealIDForDealPage2();
                }



                if (dtDeal != null && dtDeal.Rows.Count > 0)
                {
                    //Check Deal is Expired or not

                    this.hfDealId.Value = dtDeal.Rows[0]["dealId"].ToString().Trim();

                    //Get And Set Posts By Deal Id
                    CheckUserLoginInOrNot();
                    GetAndSetPostsByDealId(Convert.ToInt32(hfDealId.Value), false, "", "0");

                    BLLDealCause objCause = new BLLDealCause();
                    objCause.cause_startTime = objDeal.CreatedDate;
                    objCause.cause_city = Convert.ToInt32(strCityID);
                    DataTable dtDealCause = objCause.getDealCauseByStartTime();
                    if (dtDealCause != null && dtDealCause.Rows.Count > 0)
                    {
                        pnlDealCause.Visible = true;
                        strCauseImage = ConfigurationManager.AppSettings["YourSite"] + "/Images/Cause/" + dtDealCause.Rows[0]["cause_image"].ToString().Trim();
                        strpaddingCause = "padding:5px;";
                        strCauseLink = dtDealCause.Rows[0]["cause_link"].ToString().Trim();
                        lblCauseTitle.Text = dtDealCause.Rows[0]["cause_title"].ToString().Trim();
                        lblCauseShortDescription.Text = dtDealCause.Rows[0]["cause_shortDescription"].ToString().Trim();
                        lblCauseLongDescription.Text = dtDealCause.Rows[0]["cause_longDescription"].ToString().Trim();
                    }
                    else
                    {
                        strpaddingCause = "";
                        pnlDealCause.Visible = false;
                    }
                    if (strCityID != "337")
                    {

                        HttpCookie colorBoxClose = Request.Cookies["colorBoxClose"];
                        if (colorBoxClose == null)
                        {
                            colorBoxClose = new HttpCookie("colorBoxClose");
                        }
                        colorBoxClose.Expires = DateTime.Now.AddHours(20);
                        Response.Cookies.Add(colorBoxClose);
                        colorBoxClose["colorBoxClose"] = "True";
                    }
                    bool dealNotExpired = true;
                    string strDealExpiredText = "";
                    DateTime dtTempEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealEndTime"].ToString().Trim());
                    TimeSpan ts = dtTempEndTime - objDeal.CreatedDate;

                    this.hfCurrentDealId.Value = dtDeal.Rows[0]["dealId"].ToString();

                    //objOrders.dealId = Convert.ToInt64(dtDeal.Rows[0]["dealId"].ToString());

                    //DataTable dtOrders = objOrders.getTotalDealOrdersCountByDealId();

                    int intTotalOrders = 0;
                    if (dtDeal.Rows[0]["Orders"] != null && dtDeal.Rows[0]["Orders"].ToString().Trim() != "")
                    {
                        intTotalOrders = Convert.ToInt32(dtDeal.Rows[0]["Orders"].ToString());
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
                        strCheckOutLink = ConfigurationManager.AppSettings["YourSecureSite"] + "checkout.aspx?did=" + dtDeal.Rows[0]["dealId"].ToString();
                    }
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
                            ltPhotos.Text = "<div style='clear:both;'><img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' alt='' height='176px' width='244px' /></div>";
                            Literal lit = (Literal)Master.FindControl("linkID");

                            if (lit != null)
                            {
                                lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' >";
                            }
                            ltSlideShow.Text += "<img style='position: absolute;top: 0px;left: 268px;z-index: 8;  z-index: 10;opacity: 1.0;' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' alt='Slideshow Image 1'/>";
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
                                    ltPhotos.Text = "<div style='clear:both;'><img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[0] + "' alt='' height='176px' width='244px' /></div>";
                                    //ltSlideShow.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' alt='Slideshow Image 1' class='active' />";

                                    ltSlideShow.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' class='active'>";
                                }
                                else if (strDealImages[i].ToString().Trim() != "")
                                {
                                    ltSlideShow.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'>";
                                    // ltSlideShow.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' alt='Slideshow Image " + (1 + 1).ToString() + "' />";
                                    ltPhotos.Text += "<div style='clear:both; padding-top:10px;'><img src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "' alt='' height='176px' width='244px' /></div>";
                                }
                            }
                            ltSlideShow.Text += "</div>";
                        }
                    }
                    //Set the Page Title
                    if (dtDeal.Rows[0]["topTitle"] != null && dtDeal.Rows[0]["topTitle"].ToString().Trim() != "")
                    {
                        lblTopTitle.Visible = true;
                        lblTopTitle.Text = dtDeal.Rows[0]["topTitle"].ToString().Trim();
                    }
                    else
                    {
                        lblTopTitle.Visible = false;
                        lblTopTitle.Text = "";
                    }
                    Page.Title = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtDeal.Rows[0]["title"].ToString().Trim();
                    ViewState["title"] = strCurrentCityName.ToString() + "'s Tasty Daily Deal | " + dtDeal.Rows[0]["title"].ToString().Trim();

                    strDealTitle = dtDeal.Rows[0]["title"].ToString().Trim();

                    // Get the Sub Deal Info by Deal ID
                    GetandSetSubDealsInfoIntoGrid(int.Parse(dtDeal.Rows[0]["dealId"].ToString()), dealNotExpired, strDealExpiredText, strCityID);


                    strShareReferal = ConfigurationManager.AppSettings["YourSite"] + "/member_referral.aspx?did=" + dtDeal.Rows[0]["dealId"].ToString();
                    strFBString = Server.UrlEncode(ConfigurationManager.AppSettings["YourSite"].ToString() + "/" + strCurrentCityName + "_" + dtDeal.Rows[0]["dealId"].ToString());

                    strReferralLink = ConfigurationManager.AppSettings["YourSite"] + "/member_referral.aspx?did=" + dtDeal.Rows[0]["dealId"].ToString();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        strShareLink = HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                             + "/r/" + Convert.ToString(Convert.ToInt32(dtUser.Rows[0]["userId"].ToString().Trim()) + 111111) + "_" + dtDeal.Rows[0]["dealId"].ToString()));
                    }
                    else
                    {
                        strShareLink = HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
                                                     + "/" + strCurrentCityName + "_" + dtDeal.Rows[0]["dealId"].ToString()));
                    }


                    //For RSS Feed
                    strRSSLink = ConfigurationManager.AppSettings["YourSite"].ToString() + "/RSS.aspx?cid=" + strCityID;

                    //Set the Deal Id into the Hidden Field


                    if (dtDeal.Rows[0]["Orders"] != null && dtDeal.Rows[0]["Orders"].ToString().Trim() != "")
                    {
                        intTotalOrders = Convert.ToInt32(dtDeal.Rows[0]["Orders"].ToString());

                        if (dtDeal.Rows[0]["dealDelMaxLmt"] != null && dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim() != "0")
                        {
                            if (intTotalOrders >= Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()))
                            {
                                lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                lblDealTotal2.Text = "Sold out";
                                strImageOnOff = "Images/DealOff.png";
                            }
                            else if (dealNotExpired)
                            {
                                if (intTotalOrders < Convert.ToInt32(dtDeal.Rows[0]["dealDelMinLmt"].ToString().Trim()))
                                {

                                    //lblLeftDeals.Text = "There are only <b>" + (Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()) - intTotalOrders).ToString() + " deals left</b>";
                                    lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                    lblDealTotal2.Text = "Deal almost On";
                                    strImageOnOff = "Images/DealOff.png";
                                }
                                else
                                {
                                    // lblLeftDeals.Text = "There are only <b>" + (Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()) - intTotalOrders).ToString() + " deals left</b>";
                                    lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                    lblDealTotal2.Text = "Deal is On";
                                    strImageOnOff = "Images/DealOn.png";
                                }
                            }
                            else
                            {
                                lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                lblDealTotal2.Text = "Deal Expired";
                                strImageOnOff = "Images/DealOff.png";
                            }
                        }
                        else
                        {
                            if (dealNotExpired)
                            {
                                if (intTotalOrders == 0)
                                {
                                    // lblLeftDeals.Text = "Be the first <b>to buy!</b>";
                                    lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                    lblDealTotal2.Text = "Deal almost On";
                                    strImageOnOff = "Images/DealOff.png";
                                }
                                else if (intTotalOrders < Convert.ToInt32(dtDeal.Rows[0]["dealDelMinLmt"].ToString().Trim()))
                                {
                                    // lblLeftDeals.Text = " <b>" + intTotalOrders.ToString() + " Deals Sold</b>";
                                    lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                    lblDealTotal2.Text = "Deal almost On";
                                    strImageOnOff = "Images/DealOff.png";
                                }
                                else
                                {
                                    // lblLeftDeals.Text = "Don’t miss out <b>on this deal!</b>";
                                    lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                    lblDealTotal2.Text = "Deal is On";
                                    strImageOnOff = "Images/DealOn.png";
                                }
                            }
                            else
                            {
                                lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                                lblDealTotal2.Text = "Deal Expired";
                                strImageOnOff = "Images/DealOff.png";
                            }
                        }
                    }
                    else
                    {
                        // lblLeftDeals.Text = "Be the first <b>to buy!</b>";
                        lblDealTotal.Text = intTotalOrders.ToString() + " bought";
                        lblDealTotal2.Text = "Deal almost On";
                        strImageOnOff = "Images/DealOff.png";
                    }

                    DateTime dtEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealEndTime"].ToString().Trim());
                    //if (strCityID == "338")
                    //{
                    //    dtEndTime = dtEndTime.AddHours(-3);
                    //}
                    if (!dealNotExpired)
                    {
                        dtEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealStartTime"].ToString().Trim());
                    }
                    ltCountDown.Text = "<script type='text/javascript'>";
                    ltCountDown.Text += "$(function () {";
                    ltCountDown.Text += "var austDay = new Date();";
                    ltCountDown.Text += "austDay = new Date(" + dtEndTime.Year + "," + (dtEndTime.Month - 1) + "," + dtEndTime.Day + "," + dtEndTime.Hour + "," + dtEndTime.Minute + ",0);";
                    ltCountDown.Text += "$('#defaultCountdown').countdown({until: austDay, serverSync: serverTime});";
                    ltCountDown.Text += "$('#year').text(austDay.getFullYear());";
                    ltCountDown.Text += "});</script>";

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
                    ltCountDown.Text += "}    </script>";


                    ltCountDown2.Text = "<script type='text/javascript'>";
                    ltCountDown2.Text += "$(function () {";
                    ltCountDown2.Text += "var austDay = new Date();";
                    ltCountDown2.Text += "austDay = new Date(" + dtEndTime.Year + "," + (dtEndTime.Month - 1) + "," + dtEndTime.Day + "," + dtEndTime.Hour + "," + dtEndTime.Minute + ",0);";
                    ltCountDown2.Text += "$('#defaultCountdown2').countdown({until: austDay, serverSync: serverTime});";
                    ltCountDown2.Text += "$('#year').text(austDay.getFullYear());";
                    ltCountDown2.Text += "});</script>";

                    ltCountDown2.Text += "<script type='text/javascript'>";
                    ltCountDown2.Text += "function serverTime() { ";
                    ltCountDown2.Text += "var time = null; ";
                    ltCountDown2.Text += "$.ajax({url: '" + System.Configuration.ConfigurationManager.AppSettings["YourSite"].ToString() + "/getStateLocalTime.aspx?sid=" + dtDeal.Rows[0]["provinceId"].ToString().Trim() + "', ";
                    ltCountDown2.Text += "async: false, dataType: 'text', ";
                    ltCountDown2.Text += "success: function(text) { ";
                    ltCountDown2.Text += "time = new Date(text); ";
                    ltCountDown2.Text += "}, error: function(http, message, exc) { ";
                    ltCountDown2.Text += "time = new Date(); ";
                    ltCountDown2.Text += "}}); ";
                    ltCountDown2.Text += "return time; ";
                    ltCountDown2.Text += "}    </script>";




                    if (intTotalOrders == 0)
                    {
                        lblDealPurchaseDetailBottom.Text = "Be the First to Buy this Deal";
                        //lblDealPurchaseDetailBottom.Text = " " + dtDeal.Rows[0]["dealDelMinLmt"].ToString().Trim() + " more buys to activate this deal";
                        ltCountDown.Text += "<script>";
                        ltCountDown.Text += "jQuery(function() {";
                        ltCountDown.Text += "jQuery( '#progressbar' ).progressbar({";
                        ltCountDown.Text += "value: 0";
                        ltCountDown.Text += "});";
                        ltCountDown.Text += "});	</script>";

                        //ltProgressBarImage.Text = "<div style='padding-left: 0px;'>";
                        //ltProgressBarImage.Text += "<img id='imgProgress' src='Images/progressBarTop.jpg' />";
                        //ltProgressBarImage.Text += "</div>";
                    }
                    else if (intTotalOrders >= Convert.ToInt32(dtDeal.Rows[0]["dealDelMinLmt"].ToString().Trim()))
                    {
                        if (dtDeal.Rows[0]["dealDelMaxLmt"] != null && dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim() != "0")
                        {
                            if (intTotalOrders >= Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()))
                            {
                                lblDealPurchaseDetailBottom.Text = " " + intTotalOrders.ToString() + " Deals Sold.";
                            }
                            else if (intTotalOrders <= Convert.ToInt32(dtDeal.Rows[0]["dealDelMinLmt"].ToString().Trim()))
                            {
                                lblDealPurchaseDetailBottom.Text = "Limited Qty " + (Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()) - intTotalOrders).ToString() + " Deals Left.";
                            }
                            else
                            {
                                lblDealPurchaseDetailBottom.Text = "Limited Qty " + (Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()) - intTotalOrders).ToString() + " Deals Left.";
                            }
                        }
                        else
                        {
                            if (dealNotExpired)
                            {
                                if (intTotalOrders == 0)
                                {
                                    lblDealPurchaseDetailBottom.Text = "Be the first to buy!";
                                }
                                else
                                {
                                    lblDealPurchaseDetailBottom.Text = "Don’t miss out on this deal!";
                                }
                            }
                            else
                            {
                                lblDealPurchaseDetailBottom.Text = "Sorry you miss this deal.";
                            }
                        }

                        // lblDealPurchaseDetailBottom.Text = "Limited time offer, don't miss it!";                        
                        //lblDealsPurchasedDetali.Text = intTotalOrders.ToString() + " Deals have been sold.";
                        ltCountDown.Text += "<script>";
                        ltCountDown.Text += "jQuery(function() {";
                        ltCountDown.Text += "jQuery( '#progressbar' ).progressbar({";
                        ltCountDown.Text += "value: 100";
                        ltCountDown.Text += "});";
                        ltCountDown.Text += "});	</script>";

                        //ltProgressBarImage.Text = "<div style='padding-left: 195px;'>";
                        //ltProgressBarImage.Text += "<img id='imgProgress' src='Images/progressBarTop.jpg' />";
                        //ltProgressBarImage.Text += "</div>";
                    }
                    else
                    {
                        lblDealPurchaseDetailBottom.Text = " " + (Convert.ToInt32(dtDeal.Rows[0]["dealDelMinLmt"].ToString().Trim()) - intTotalOrders).ToString() + " more buys to activate this deal";
                        //lblDealsPurchasedDetali.Text = intTotalOrders.ToString() + " Deals have been sold.";
                        double dMinOrder = Convert.ToDouble(dtDeal.Rows[0]["dealDelMinLmt"].ToString().Trim());
                        double dOrders = intTotalOrders;
                        double dPercent = (100 / dMinOrder) * dOrders;
                        ltCountDown.Text += "<script>";
                        ltCountDown.Text += "jQuery(function() {";
                        ltCountDown.Text += "jQuery( '#progressbar' ).progressbar({";
                        ltCountDown.Text += "value: " + Convert.ToInt32(dPercent);
                        ltCountDown.Text += "});";
                        ltCountDown.Text += "});	</script>";

                        //ltProgressBarImage.Text = "<div style='padding-left: " + (Convert.ToInt32(dPercent) * 2 - 5).ToString() + "px;'>";
                        //ltProgressBarImage.Text += "<img id='imgProgress' src='Images/progressBarTop.jpg' />";
                        //ltProgressBarImage.Text += "</div>";
                    }



                    if (dealNotExpired)
                    {
                        lblDealPrice.Text = "$" + dtDeal.Rows[0]["sellingPrice"].ToString().Trim();
                        strSellingPrice = "$" + dtDeal.Rows[0]["sellingPrice"].ToString().Trim();
                    }
                    else
                    {
                        lblDealPrice.Text = strDealExpiredText;
                        lblDealPrice.Font.Size = FontUnit.XLarge;
                        strSellingPrice = strDealExpiredText;
                    }
                    lblValue.Text = "$" + dtDeal.Rows[0]["valuePrice"].ToString().Trim();
                    if (Convert.ToInt32(dtDeal.Rows[0]["valuePrice"].ToString()) > 999)
                    {
                        lblDiscount.Font.Size = 18;
                        lblValue.Font.Size = 18;
                        lblSave.Font.Size = 18;
                    }
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

                    lblDiscount.Text = Convert.ToInt32(dDiscount).ToString() + "%";
                    lblSave.Text = "$" + (Convert.ToInt32(dActualPrice - dSellPrice)).ToString();


                    lblFinePrintText.Text = dtDeal.Rows[0]["finePrint"].ToString().Trim();
                    lblHighlightsText.Text = dtDeal.Rows[0]["dealHightlights"].ToString().Trim();
                    lblDealDiscription.Text = dtDeal.Rows[0]["description"].ToString().Trim();
                    //                        lblCompanyDetail.Text = dtDeal.Rows[0]["firstName"].ToString().Trim() + " " + dtDeal.Rows[0]["lastName"].ToString().Trim()+"<br>";
                    lblCompanyDetail.Text = dtDeal.Rows[0]["restaurantBusinessName"].ToString().Trim() + "<br>" + dtDeal.Rows[0]["phone"].ToString().Trim() + "<br>" + dtDeal.Rows[0]["restaurantAddress"].ToString().Trim() + "<br>";

                    if (dtDeal.Rows[0]["url"] != null && dtDeal.Rows[0]["url"].ToString().Trim() != "")
                    {
                        hlBusinessURL.Visible = true;
                        hlBusinessURL.Text = dtDeal.Rows[0]["url"].ToString().Trim();
                        hlBusinessURL.NavigateUrl = dtDeal.Rows[0]["url"].ToString().Trim();
                    }
                    try
                    {
                        BLLRestaurantGoogleAddresses objResturant = new BLLRestaurantGoogleAddresses();
                        objResturant.restaurantId = Convert.ToInt64(dtDeal.Rows[0]["restaurantId"].ToString().Trim());
                        DataTable dtResturant = objResturant.getAllRestaurantGoogleAddressesByRestaurantID();
                        if (dtResturant != null && dtResturant.Rows.Count > 0)
                        {

                            if (dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "online deal" || dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "online" || dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "deal online")
                            {
                                DivGoogleMap.Visible = false;
                                DivGoogleMapHeader.Visible = false;
                                MarginTop = "-27px";
                            }
                            else
                            {
                                strimgGoogle = ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + ".png";
                                strimgGoogleToolTip = "";
                                for (int i = 0; i < dtResturant.Rows.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        strimgGoogleToolTip += "(A) " + dtResturant.Rows[i]["restaurantGoogleAddress"].ToString() + "</br>";
                                    }
                                    else if (i == 1)
                                    {
                                        strimgGoogleToolTip += "(B) " + dtResturant.Rows[i]["restaurantGoogleAddress"].ToString() + "</br>";
                                    }
                                    else if (i == 2)
                                    {
                                        strimgGoogleToolTip += "(C) " + dtResturant.Rows[i]["restaurantGoogleAddress"].ToString() + "</br>";
                                    }
                                    else
                                    {
                                        strimgGoogleToolTip += "(Address) " + dtResturant.Rows[i]["restaurantGoogleAddress"].ToString() + "</br>";
                                    }

                                }
                                strGoogleLink = "http://maps.google.com/maps?f=d&daddr=" + dtResturant.Rows[0]["restaurantGoogleAddress"].ToString();
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

                    //if (!executeGetDealByCity)
                    //{
                        objDeal.cityId = Convert.ToInt32(strCityID);
                        dtDeal = objDeal.getCurrentDealByCityIDForPage3();
                    //}
                    if (dtDeal.Rows.Count > 1 || (objDeal.DealId != 0 && Convert.ToInt32(dtDeal.Rows[0]["DealId"].ToString()) != objDeal.DealId))
                    {
                        strPedding = "padding-top:10px;";
                        GetAndFillDealsInfo(dtDeal);
                        //SlideShow(dtDeal);
                    }
                    else
                    {
                        strPedding = "";
                        strNewarByImagedisplay = "none";
                    }
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/nodeal.aspx?cid=" + strCityID, false);
                }
            }
        }
        catch (Exception ex)
        { }
    }


 
    protected string getDiscountPer(object oSellPrice, object oActualPrice)
    {
        string strRet = "";

        try
        {
            double dSellPrice = Convert.ToDouble(oSellPrice.ToString().Trim());
            double dActualPrice = Convert.ToDouble(oActualPrice.ToString().Trim());
            double dDiscount = 0;
            if (dSellPrice == 0)
            {
                dDiscount = 100;
            }
            else
            {
                dDiscount = (100 / dActualPrice) * (dActualPrice - dSellPrice);
            }

            strRet = Convert.ToInt32(dDiscount).ToString() + "% Off";
        }
        catch (Exception ex)
        {
            return "100% Off";
        }

        return strRet;
    }



    private void GetAndFillDealsInfo(DataTable dtDeals)
    {
        try
        {

            for (int i = 0; i < dtDeals.Rows.Count; i++)
            {
                if (this.hfCurrentDealId.Value.Trim() == dtDeals.Rows[i]["dealId"].ToString().Trim())
                {
                    dtDeals.Rows[i].Delete();
                }
            }
            this.gridDeals.DataSource = dtDeals;
            this.gridDeals.DataBind();
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

    public void ClearApplicationCache()
    {
        try
        {
            List<string> keys = new List<string>();
            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                Cache.Remove(keys[i]);
            }
        }
        catch (Exception ex)
        { }
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
    private void SaveUserIdInCookie(string strUserId)
    {
        HttpCookie cookie = Request.Cookies["tastygo_userID"];

        if (cookie == null)
        {
            cookie = new HttpCookie("tastygo_userID");
        }
        else
        {
            Response.Cookies.Clear();
        }

        cookie.Expires = DateTime.Now.AddMonths(1);

        Response.Cookies.Add(cookie);

        cookie["tastygo_userID"] = strUserId;
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
            cookie.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(cookie);
            cookie["tastygo_affiliate_userID"] = strUserId;

            //Save the Affiliate Visit Date
            HttpCookie cookieDate = Request.Cookies["tastygo_affiliate_date"];
            cookieDate = new HttpCookie("tastygo_affiliate_date");
            cookieDate.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(cookieDate);
            cookieDate["tastygo_affiliate_date"] = DateTime.Now.AddDays(2).ToString();
        }
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
                strDealTitle = dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim() == "" ? dtSubDealsInfo.Rows[0]["title"].ToString().Trim() : dtSubDealsInfo.Rows[0]["dealPageTitle"].ToString().Trim();

                //Set the Rows Count here
                this.hfPopUpRowsCount.Value = dtSubDealsInfo.Rows.Count.ToString();


                if (dealNotExpired)
                {
                    lblDealPrice.Text = "$" + dtSubDealsInfo.Rows[0]["sellingPrice"].ToString().Trim();
                    strSellingPrice = "$" + dtSubDealsInfo.Rows[0]["sellingPrice"].ToString().Trim();
                    strCheckOutLink = "javascript:ShowAddressPopUp();";
                }
                else
                {
                    lblDealPrice.Text = strDealExpiredText;
                    lblDealPrice.Font.Size = FontUnit.XLarge;
                    strSellingPrice = strDealExpiredText;
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


    #region Discuession Code
    #region "Check user Is Logged In or not"

    private void CheckUserLoginInOrNot()
    {
        try
        {
            if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
            {
                this.txtComment.Enabled = false;

                this.btnPost.Visible = false;

                this.hLinkSignIn.Visible = true;
            }
            else
            {
                this.txtComment.Enabled = true;

                this.btnPost.Visible = true;

                this.hLinkSignIn.Visible = false;
                GetUserIdFromSession();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #endregion

    protected void btnPost_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iDealId = int.Parse(this.hfDealId.Value);

            int iUserId = GetUserIdFromSession();

            //Add New Post here
            AddNewPost(iDealId, iUserId, 0, HtmlRemoval.StripTagsRegexCompiled(txtComment.Text.Trim()));

            //Get All the Posts here By Deal Id
            GetAndSetPostsByDealId(iDealId, true, HtmlRemoval.StripTagsRegexCompiled(txtComment.Text.Trim()), "0");

            this.txtComment.Text = "";
            //

        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private int GetUserIdFromSession()
    {
        int iUserid = 0;
        DataTable dtUser = null;

        try
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

            iUserid = int.Parse(dtUser.Rows[0]["userID"].ToString());
            ViewState["userID"] = dtUser.Rows[0]["userID"].ToString();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return iUserid;
    }

    private bool AddNewPost(int iDealId, int iUserId, long iParentID, string strComment)
    {
        bool bStatus = false;

        try
        {
            BLLDealDiscussion objBLLDealDiscussion = new BLLDealDiscussion();

            objBLLDealDiscussion.DealId = iDealId;

            objBLLDealDiscussion.UserId = iUserId;

            objBLLDealDiscussion.Comments = strComment.Trim().Replace("\n", "<br>");

            objBLLDealDiscussion.CmtDatetime = DateTime.Now;

            objBLLDealDiscussion.pdiscussionId = iParentID;

            objBLLDealDiscussion.AddNewDealDiscussion();

            lblMessage.Visible = true;
            lblMessage.Text = "Your post has been added successfully.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/Checked.png";

            BLLKarmaPoints bllKarma = new BLLKarmaPoints();
            bllKarma.userId = iUserId;
            bllKarma.karmaPoints = 1;
            bllKarma.karmaPointsType = "Comment";
            bllKarma.createdBy = iUserId;
            bllKarma.createdDate = DateTime.Now;
            bllKarma.createKarmaPoints();

            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        return bStatus;
    }

    private void GetAndSetPostsByDealId(int iDealId, bool sendEmail, string strMessage, string iParentID)
    {
        try
        {
            BLLDealDiscussion objBLLDealDiscussion = new BLLDealDiscussion();
            objBLLDealDiscussion.DealId = iDealId;
            DataTable dtPosts = objBLLDealDiscussion.getDealDiscussionByDealId();
            if ((dtPosts != null) && (dtPosts.Rows.Count > 0))
            {
                this.rptrDiscussion.DataSource = dtPosts;
                this.rptrDiscussion.DataBind();
            }
            if (sendEmail)
            {
                if (iParentID != "0")
                {
                    objBLLDealDiscussion = new BLLDealDiscussion();
                    objBLLDealDiscussion.pdiscussionId = Convert.ToInt32(iParentID);
                    dtPosts = objBLLDealDiscussion.getAllDealDiscussionByParentID();
                }
                ThreadStart starter = delegate { SendCommentToAll(dtPosts, strMessage, iParentID); };
                new Thread(starter).Start();
            }
            if (ViewState["userID"] != null && ViewState["userID"].ToString().Trim() != "")
            {
                for (int i = 0; i < rptrDiscussion.Items.Count; i++)
                {
                    Image imgCommentReply = (Image)rptrDiscussion.Items[i].FindControl("imgCommentReply");
                    if (imgCommentReply != null)
                    {
                        imgCommentReply.Attributes.Add("onclick", "hideShowDiv('" + arrayIDs[i].ToString() + "','" + imgCommentReply.ClientID + "');");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void SendCommentToAll(DataTable dtPosts, string strMessage, string iParentID)
    {
        string strUserName = "";
        string strEmail = "";
        DataTable dtUser = null;
        ArrayList arrUsers = new ArrayList();
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
            strUserName = dtUser.Rows[0]["FirstName"].ToString().Trim() + " " + dtUser.Rows[0]["LastName"].ToString().Trim().Remove(1, dtUser.Rows[0]["LastName"].ToString().Trim().Length - 1);
            strEmail = dtUser.Rows[0]["userName"].ToString().Trim();
            arrUsers.Add(dtUser.Rows[0]["userName"].ToString().Trim());
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                SendMailForNewComment("support@tazzling.com", "Admin", strUserName, dtPosts.Rows[0]["title"].ToString().Trim(), strMessage);
            }
        }
        if (iParentID.Trim() != "0")
        {
            for (int i = 0; i < dtPosts.Rows.Count; i++)
            {
                if (!arrUsers.Contains(dtPosts.Rows[i]["userName"].ToString().Trim()))
                {
                    arrUsers.Add(dtPosts.Rows[i]["userName"].ToString().Trim());
                    SendMailForNewComment(dtPosts.Rows[i]["userName"].ToString().Trim(), dtPosts.Rows[i]["Name"].ToString().Trim(), strUserName, dtPosts.Rows[i]["title"].ToString().Trim(), strMessage);
                }
            }
        }

    }


    private bool SendMailForNewComment(string ToEmail, string ToName, string FromName, string strDealTitle, string strMessage)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string toAddress = ToEmail;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = FromName + " add his/her comment on deal " + strDealTitle;
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear \"" + ToName.Trim() + "\",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>\"" + FromName + "\" just add his/her comments on deal \"" + strDealTitle + "\"</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>His/Her comment is following</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>" + strMessage.Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");

            message.Body = sb.ToString();
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            //lblAddressError.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            //lblAddressError.Visible = true;
            //imgGridMessage.Visible = true;
            //imgGridMessage.ImageUrl = "images/error.png";
            //lblAddressError.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }

    BLLUser objUser = new BLLUser();

    protected void DataListItemDataBound(Object src, DataListItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image imgDis = (Image)e.Item.FindControl("imgDis");
                Image imgCommentReply = (Image)e.Item.FindControl("imgCommentReply");
                if (ViewState["userID"] == null || ViewState["userID"].ToString().Trim() == "" && imgCommentReply != null)
                {
                    imgCommentReply.Visible = false;
                }

                //if (Session["FBImage"] == null)
                //{
                HiddenField hfUserID = (HiddenField)e.Item.FindControl("hfUserID");
                objUser.userId = Convert.ToInt32(hfUserID.Value);
                DataTable dtUserInfo = objUser.getUserByID();

                string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + imgDis.ImageUrl.Trim().Trim();
                if (File.Exists(strFileName))
                {
                    ViewState["PicName"] = imgDis.ImageUrl.Trim().Trim();
                    imgDis.ImageUrl = "~/images/ProfilePictures/" + imgDis.ImageUrl.Trim().Trim();

                }
                else if (dtUserInfo != null && dtUserInfo.Rows.Count > 0 && (dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() != ""))
                {
                    imgDis.ImageUrl = "https://graph.facebook.com/" + dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() + "/picture";
                }
                else
                {
                    imgDis.ImageUrl = "~/Images/disImg.gif";
                }
                if (ViewState["userID"] != null && Convert.ToInt32(ViewState["userID"]) == objUser.userId)
                {
                    imgLoginUser.ImageUrl = imgDis.ImageUrl;
                }
                else
                {
                    imgLoginUser.ImageUrl = "~/Images/disImg.gif";
                }

                try
                {
                    DataList rptrSubDiscussion = (DataList)e.Item.FindControl("rptrSubDiscussion");
                    String strDiscutionID = String.Empty;
                    if (null != rptrSubDiscussion)
                    {
                        HiddenField hfDiscuessionID = (HiddenField)e.Item.FindControl("hfDiscuessionID");
                        if (hfDiscuessionID != null)
                        {

                            strDiscutionID = hfDiscuessionID.Value.Trim();
                            ViewState["DiscutionID"] = hfDiscuessionID.Value.Trim();
                            ViewState["itemIndex"] = e.Item.ItemIndex.ToString();
                            BLLDealDiscussion objDis = new BLLDealDiscussion();
                            objDis.pdiscussionId = Convert.ToInt64(strDiscutionID);
                            DataTable data = objDis.getDealDiscussionByParentID();
                            rptrSubDiscussion.DataSource = data;
                            rptrSubDiscussion.DataBind();
                        }

                    }
                }
                catch (Exception ex)
                { }

            }
        }
        catch (Exception ex)
        { }
    }

    protected void SubCommentDataListItemDataBound(Object src, DataListItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image imgDis = (Image)e.Item.FindControl("imgSubDis");

                //if (Session["FBImage"] == null)
                //{
                HiddenField hfUserID = (HiddenField)e.Item.FindControl("hfSubCommentUserID");
                objUser.userId = Convert.ToInt32(hfUserID.Value);
                DataTable dtUserInfo = objUser.getUserByID();

                string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + imgDis.ImageUrl.Trim().Trim();
                if (File.Exists(strFileName))
                {
                    ViewState["PicName"] = imgDis.ImageUrl.Trim().Trim();
                    imgDis.ImageUrl = "~/images/ProfilePictures/" + imgDis.ImageUrl.Trim().Trim();

                }
                else if (dtUserInfo != null && dtUserInfo.Rows.Count > 0 && (dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() != ""))
                {
                    imgDis.ImageUrl = "https://graph.facebook.com/" + dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() + "/picture";
                }
                else
                {
                    imgDis.ImageUrl = "~/Images/disImg.gif";
                }
                //if (ViewState["userID"] != null && Convert.ToInt32(ViewState["userID"]) == objUser.userId)
                //{
                //    imgLoginUser.ImageUrl = imgDis.ImageUrl;
                //}
                //else
                //{
                //    imgLoginUser.ImageUrl = "~/Images/disImg.gif";
                //}               

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                if (ViewState["userID"] == null || ViewState["userID"].ToString().Trim() == "")
                {
                    Panel pnlFooter = (Panel)e.Item.FindControl("pnlFooter");
                    if (pnlFooter != null)
                    {
                        pnlFooter.Visible = false;
                    }
                }
                else
                {

                    int index = 0;
                    int.TryParse(ViewState["itemIndex"].ToString().Trim(), out index);

                    Panel pnlFooter = (Panel)e.Item.FindControl("pnlFooter");
                    //Image imgDis = (Image)rptrDiscussion.Items[rptrDiscussion.Items.Count].FindControl("imgDis");
                    //if (imgDis != null && pnlFooter != null)
                    //{
                    //    imgDis.Attributes.Add("onclick", "hideShowDiv('" + pnlFooter.ClientID + "');");
                    //}

                    arrayIDs.Add(pnlFooter.ClientID);

                    TextBox txtComment = (TextBox)e.Item.FindControl("txtSubComment");
                    ImageButton btnSubCommentPost = (ImageButton)e.Item.FindControl("btnSubCommentPost");
                    if (txtComment != null && btnSubCommentPost != null)
                    {
                        btnSubCommentPost.Attributes.Add("onclick", "return validateEmptyField('" + txtComment.ClientID + "');");
                        if (ViewState["DiscutionID"] != null)
                        {
                            btnSubCommentPost.CommandArgument = ViewState["DiscutionID"].ToString();
                        }
                        ViewState["DiscutionID"] = null;
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void rptrSubDiscussion_ItemCommand(object source, DataListCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "addComment")
            {
                TextBox txtSubComment = (TextBox)e.Item.FindControl("txtSubComment");
                if (txtSubComment != null)
                {
                    int iDealId = int.Parse(this.hfDealId.Value);
                    int iUserId = GetUserIdFromSession();
                    //Add New Post here
                    AddNewPost(iDealId, iUserId, Convert.ToInt64(e.CommandArgument), txtSubComment.Text);
                    //Get All the Posts here By Deal Id
                    GetAndSetPostsByDealId(iDealId, true, txtSubComment.Text, e.CommandArgument.ToString());
                    txtSubComment.Text = "";
                }
            }

        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }


    }
    #endregion
}
