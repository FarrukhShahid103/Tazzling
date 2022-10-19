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

public partial class detail : System.Web.UI.Page
{
    BLLDeals objDeal = new BLLDeals();
    BLLCities objCities = new BLLCities();
    ArrayList arrayIDs = new ArrayList();
    public string strShareLink = "";
    public string strFBString = "";
    public string strDealExpiredText = "Buy Now";
    public string strCheckOutLink = "";
    public string strCurrentCityName = "";
    public string DivID="";
    public string ImageID = "";
    public static string IsLogedIn ="display:none;";
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (ViewState["title"] != null)
        {
            Page.Title = ViewState["title"].ToString();
        }    

        if (!IsPostBack)
        {            
            DataTable dtDeal = null;
            string strCityID = "337";
            objCities.cityId = 337;
            HttpCookie yourCity = Request.Cookies["yourCity"];
            if (yourCity != null)
            {
                strCityID = yourCity.Values[0].ToString().Trim();
                objCities.cityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
            }

            if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "")
            {                
                DataTable dtCity = objCities.getCityByCityId();
                if (dtCity.Rows.Count > 0)
                {
                    objDeal.CreatedDate = Misc.getResturantLocalTime(Convert.ToInt32(dtCity.Rows[0]["provinceId"].ToString()));
                    strCurrentCityName = dtCity.Rows[0]["cityName"].ToString().Trim().Replace(' ', '.');
                }

                objDeal.cityId = Convert.ToInt32(strCityID);
                objDeal.DealId = Convert.ToInt32(Request.QueryString["did"].ToString().Trim());
                dtDeal = objDeal.getCurrentDealByDealIDForDealPage();

                       

                if (dtDeal != null && dtDeal.Rows.Count > 0)
                {                                                         
                    bool dealNotExpired = true;
                    hfDealId.Value = dtDeal.Rows[0]["dealid"].ToString();
                    CheckUserLoginInOrNot();
                    GetAndSetPostsByDealId(Convert.ToInt32(dtDeal.Rows[0]["dealid"].ToString()), false, "", "0");   

                    DateTime dtTempEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealEndTime"].ToString().Trim());
                    TimeSpan ts = dtTempEndTime - objDeal.CreatedDate;
                    int intTotalOrders = 0;
                    if (dtDeal.Rows[0]["Orders"] != null && dtDeal.Rows[0]["Orders"].ToString().Trim() != "")
                    {
                        intTotalOrders = Convert.ToInt32(dtDeal.Rows[0]["Orders"].ToString().ToString());
                       lblDealsSold.Text = dtDeal.Rows[0]["Orders"].ToString().ToString();
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
                    if (dtDeal.Rows[0]["shortTitle"] != null && dtDeal.Rows[0]["shortTitle"].ToString().Trim() != "")
                    {
                        lblDealShortTitle.Text = dtDeal.Rows[0]["shortTitle"].ToString().Trim();
                    }
                    else
                    {
                        lblDealShortTitle.Visible = false;
                        lblDealShortTitle.Text = "";
                    }

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
                    ltCountDown.Text += "$('#defaultCountdown').countdown({until: austDay,compact: true, serverSync: serverTime});";
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

                    lblDealDiscount.Text = "<b>Discount:</b> "+ Convert.ToInt32(dDiscount).ToString() + "%";
                                                                                
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
                   
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/nodeal.aspx?cid=" + strCityID, false);
                }               
            }
            else
            {
                Response.Redirect("Default.aspx", true);
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
            return "C$ " + sellingPrice.ToString().Trim();
        }
        return "C$ " + sellingPrice.ToString().Trim();
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
                    strCheckOutLink = "javascript:ShowAddressPopUp();";
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

    #region Discuession Code
    #region "Check user Is Logged In or not"

    private void CheckUserLoginInOrNot()
    {
        try
        {
            if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
            {
                this.txtComment.Enabled = false;
                IsLogedIn = "display:none;";
                this.btnPost.Visible = false;

                this.hLinkSignIn.Visible = true;
            }
            else
            {
                this.txtComment.Enabled = true;
                IsLogedIn = "display:block;";
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

    protected void btnPost_Click(object sender, EventArgs e)
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
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                       
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
            ViewState["userId"] = dtUser.Rows[0]["userID"].ToString();
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

            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('Your post has been added successfully.' , 'success');";                         
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                
          
           /* BLLKarmaPoints bllKarma = new BLLKarmaPoints();
            bllKarma.userId = iUserId;
            bllKarma.karmaPoints = 1;
            bllKarma.karmaPointsType = "Comment";
            bllKarma.createdBy = iUserId;
            bllKarma.createdDate = DateTime.Now;
            bllKarma.createKarmaPoints();*/
            
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                               
        }
        CheckUserLoginInOrNot();
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
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                for (int i = 0; i < rptrDiscussion.Items.Count; i++)
                {
                    LinkButton BtnReply = (LinkButton)rptrDiscussion.Items[i].FindControl("BtnReply");


                    if (BtnReply != null)
                    {
                        BtnReply.Attributes.Add("onclick", "return hideShowDiv('" + arrayIDs[i].ToString() + "');");
                        
                        
                    }
                }
            }

        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                       
        }
    }

    private void SendCommentToAll(DataTable dtPosts, string strMessage, string iParentID)
    {
        try
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
                string strLastName = "";
                if (dtUser.Rows[0]["LastName"].ToString().Trim().Length > 0)
                {
                    strLastName = dtUser.Rows[0]["LastName"].ToString().Trim().Remove(1, dtUser.Rows[0]["LastName"].ToString().Trim().Length - 1);
                }
                strUserName = dtUser.Rows[0]["FirstName"].ToString().Trim() + " " + strLastName;
                strEmail = dtUser.Rows[0]["userName"].ToString().Trim();
                arrUsers.Add(dtUser.Rows[0]["userName"].ToString().Trim());
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    SendMailForNewComment("info@tazzling.com", "Admin", strUserName, dtPosts.Rows[0]["title"].ToString().Trim(), strMessage);
                }
            }
            //if (iParentID.Trim() != "0")
            //{
            //    for (int i = 0; i < dtPosts.Rows.Count; i++)
            //    {
            //        if (!arrUsers.Contains(dtPosts.Rows[i]["userName"].ToString().Trim()))
            //        {
            //            arrUsers.Add(dtPosts.Rows[i]["userName"].ToString().Trim());
            //            SendMailForNewComment(dtPosts.Rows[i]["userName"].ToString().Trim(), dtPosts.Rows[i]["Name"].ToString().Trim(), strUserName, dtPosts.Rows[i]["title"].ToString().Trim(), strMessage);
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
 
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
                if (ViewState["userId"] != null && Convert.ToInt32(ViewState["userId"]) == objUser.userId)
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
                //if (ViewState["userId"] != null && Convert.ToInt32(ViewState["userId"]) == objUser.userId)
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
                if (ViewState["userId"] == null || ViewState["userId"].ToString().Trim() == "")
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
                    LinkButton btnSubCommentPost = (LinkButton)e.Item.FindControl("btnSubCommentPost");
                    LinkButton LinkButtonCancel = (LinkButton)e.Item.FindControl("BtnCancel");

                    

                    LinkButtonCancel.Attributes.Add("onclick", "return CancelComment('" + pnlFooter.ClientID + "')");

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
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                       
        }


    }
    #endregion
  
}
