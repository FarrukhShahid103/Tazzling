using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using GecLibrary;
using System.Text;
using System.Data;

public partial class TastyLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    
                    if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                    {
                        Response.Redirect("Default.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public string GetPassword()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomNumber(10000, 99999));
        builder.Append(RandomNumber(10000, 99999));
        return builder.ToString();
    }

    private string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
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
    protected void btnSignin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmail.Text.Trim() == "" || txtPwd.Text.Trim() == "")
            {

                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Enter Email and password";
                return;

            }


            if (!Misc.validateEmailAddress(txtEmail.Text.Trim()))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Invalid email address.";

                return;
            }

            BLLUser obj = new BLLUser();

            obj.userName = txtEmail.Text.Trim();
            obj.userPassword = txtPwd.Text.Trim();
            DataTable dtUser = obj.validateUserNamePassword();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                //if (dtUser.Rows[0]["userId"] != null && dtUser.Rows[0]["userId"].ToString().Trim() != "")
                //{
                //    try
                //    {
                //        BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                //        bllKarma.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                //        DataTable dtkarmaPoints = bllKarma.getKarmaTodayLoginPointsByUserId();
                //        if (dtkarmaPoints != null && dtkarmaPoints.Rows.Count == 0)
                //        {
                //            bllKarma.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                //            bllKarma.karmaPoints = 2;
                //            bllKarma.karmaPointsType = "Login";
                //            bllKarma.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                //            bllKarma.createdDate = DateTime.Now;
                //            bllKarma.createKarmaPoints();
                //        }
                //        else if (dtkarmaPoints != null
                //            && dtkarmaPoints.Rows.Count > 0
                //            && Convert.ToDateTime(dtkarmaPoints.Rows[0]["createdDate"].ToString()).ToString("MM/dd/yyyy") != DateTime.Now.ToString("MM/dd/yyyy"))
                //        {
                //            bllKarma.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                //            bllKarma.karmaPoints = 2;
                //            bllKarma.karmaPointsType = "Login";
                //            bllKarma.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                //            bllKarma.createdDate = DateTime.Now;
                //            bllKarma.createKarmaPoints();
                //        }
                //    }
                //    catch (Exception ex)
                //    { }
                //}

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
               
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Invalid user name or password.";


                return;
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
}