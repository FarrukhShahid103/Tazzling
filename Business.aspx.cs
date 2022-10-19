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
public partial class Business : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Page.Title = getCityName() + "'s Tasty Daily Deal | Login";
                //if (Session["sale"] != null || Session["user"] != null)
                //{
                //    Response.Redirect("Default.aspx", false);
                //}
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected string getCityName()
    {
        BLLCities objCity = new BLLCities();
        objCity.cityId = 337;
        HttpCookie yourCity = Request.Cookies["yourCity"];
        if (yourCity != null)
        {
            objCity.cityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
        }
        DataTable dtCity = objCity.getCityByCityId();
        if (dtCity != null && dtCity.Rows.Count > 0)
        {
            return dtCity.Rows[0]["cityName"].ToString().Trim();
        }
        return "";
    }

    protected void btnSignin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmail.Text.Trim() == "" || txtPwd.Text.Trim() == "")
            {
                string jScript;
                jScript = "<script>";
                jScript += "$(\"#messages\").removeClass(\"successMessage\").addClass(\"errorMessage\");";
                jScript += "$(\"#messages\").html('Enter Email and password').slideDown(\"slow\");";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSignin, typeof(Button), "Javascript", jScript, false);

                //  ltForMessages.Text = "<div class='errorMessage'>Enter Email and password</div>";
                return;
            }
            if (!Misc.validateEmailAddress(txtEmail.Text.Trim()))
            {
                string jScript;
                jScript = "<script>";
                jScript += "$(\"#messages\").removeClass(\"successMessage\").addClass(\"errorMessage\");";
                jScript += "$(\"#messages\").html('Invalid email address.').slideDown(\"slow\");";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSignin, typeof(Button), "Javascript", jScript, false);
                return;
            }

            BLLUser obj = new BLLUser();

            obj.userName = txtEmail.Text.Trim();
            obj.userPassword = txtPwd.Text.Trim();
            DataTable dtUser = obj.validateUserNamePassword();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {


                if (dtUser.Rows[0]["userTypeID"].ToString() == "4")
                {
                    Session["member"] = dtUser;
                    Session.Remove("restaurant");
                    Session.Remove("sale");
                    Session.Remove("user");
                    //Get the AffiliateInfo from Cookie 
                    //If exits then it will update into the User Info data table
                    GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                }
                else if (dtUser.Rows[0]["userTypeID"].ToString() == "3")
                {
                    Session["restaurant"] = dtUser;
                    Session.Remove("member");
                    Session.Remove("sale");
                    Session.Remove("user");
                    //Get the AffiliateInfo from Cookie 
                    //If exits then it will update into the User Info data table
                    GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                }
                else if (dtUser.Rows[0]["userTypeID"].ToString() == "5")
                {
                    Session["sale"] = dtUser;
                    Session.Remove("member");
                    Session.Remove("restaurant");
                    Session.Remove("user");
                    //Get the AffiliateInfo from Cookie 
                    //If exits then it will update into the User Info data table
                    GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                }
                else
                {
                    Session["user"] = dtUser;
                    Session.Remove("member");
                    Session.Remove("restaurant");
                    Session.Remove("sale");

                    GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                }
                HttpCookie cookie = Request.Cookies["tastygoSignup"];
                if (cookie == null)
                {
                    cookie = new HttpCookie("tastygoSignup");
                }
                cookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cookie);
                cookie["tastygoSignup"] = txtEmail.Text.Trim();
                HttpCookie cookie2 = Request.Cookies["tastygoLogin"];
                if (cookie2 == null)
                {
                    cookie2 = new HttpCookie("tastygoLogin");
                }
                cookie2.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(cookie2);
                cookie2["tastygoLogin"] = "true";

                HttpCookie colorBoxClose = Request.Cookies["colorBoxClose"];
                if (colorBoxClose == null)
                {
                    colorBoxClose = new HttpCookie("colorBoxClose");
                }
                colorBoxClose.Expires = DateTime.Now.AddHours(20);
                Response.Cookies.Add(colorBoxClose);
                colorBoxClose["colorBoxClose"] = "true";

                Response.Redirect(ConfigurationManager.AppSettings["YourSite"].ToString() + "/default.aspx", true);
            }
            else
            {

                string jScript;
                jScript = "<script>";
                jScript += "$(\"#messages\").removeClass(\"successMessage\").addClass(\"errorMessage\");";
                jScript += "$(\"#messages\").html('Invalid user name or password.').slideDown(\"slow\");";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSignin, typeof(Button), "Javascript", jScript, false);
                //ltForMessages.Text = "<div class='errorMessage'>Invalid user name or password.</div>";                           

            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "$(\"#messages\").removeClass(\"successMessage\").addClass(\"errorMessage\");";
            jScript += "$(\"#messages\").html('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.').slideDown(\"slow\");";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(btnSignin, typeof(Button), "Javascript", jScript, false);
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

                        GECEncryption objDecrypt = new GECEncryption();

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


}
