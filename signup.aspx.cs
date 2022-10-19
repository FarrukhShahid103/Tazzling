using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;
using System.Net.Mail;
using GecLibrary;

public partial class SignUp : System.Web.UI.Page
{
    BLLUser objUser = new BLLUser();
    BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Signup";
            if (Request.QueryString["signup"] != null && Request.QueryString["signup"].Trim() != "" && Request.QueryString["signup"].ToLower().Trim() == "true")
            {               
                string jScript;
                jScript = "<script>";             
                jScript += "MessegeArea('Registration Complete! You may <a href='default.aspx'>click Here</a> to redirect to today's deal.' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                Response.AddHeader("REFRESH", "5;URL=Default.aspx");
                bindProvinces(0);
                return;
               
            }
            else if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {
                Response.Redirect("Default.aspx", false);
            }

            if (IsPostBack)
            {

                if (!(String.IsNullOrEmpty(txtPwd.Text.Trim())))
                {
                    txtPwd.Attributes["value"] = txtPwd.Text;
                }
                if (!(String.IsNullOrEmpty(txtConfirmPwd.Text.Trim())))
                {
                    txtConfirmPwd.Attributes["value"] = txtConfirmPwd.Text;
                }

                //CaptchaControl1.CaptchaMaxTimeout = 900;
            }
            if (!IsPostBack)
            {
                bindProvinces(0);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void bindProvinces(int CountryID)
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(CountryID);
            ddlProvinceLive.DataSource = dt.DefaultView;
            ddlProvinceLive.DataTextField = "provinceName";
            ddlProvinceLive.DataValueField = "provinceId";
            ddlProvinceLive.DataBind();
            ddlProvinceLive.Items.Insert(0, "Select Your Province/State");
            FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);

        }
        catch (Exception ex)
        {
        }
    }



    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindProvinces(Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
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

    protected void ddlProvinceLive_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);
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

    private void FillCitpDroDownList(string strProvinceId)
    {
        try
        {
            if (strProvinceId != "Select Your Province/State")
            {
                BLLCities objBLLCities = new BLLCities();

                objBLLCities.provinceId = int.Parse(strProvinceId);

                DataTable dtCities = objBLLCities.getCitiesByProvinceId();

                ddlCity.DataSource = dtCities;

                ddlCity.DataTextField = "cityName";

                ddlCity.DataValueField = "cityId";

                ddlCity.DataBind();

                ddlCity.Items.Insert(0, "Select Your City");
            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.DataSource = null;
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, "Select Your City");
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
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";

            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);




        }
        return bStatus;
    }



    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFullName.Text.Trim() == "")
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter your First Name', 'Error');";
                jScript += "</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;
            
            }

            if (txtLastName.Text.Trim() == "")
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter your Last Name', 'Error');";
                jScript += "</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;

            }


            if (txtUsernameSignUp.Text.Trim() == "")
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter your Email', 'Error');";
                jScript += "</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;

            }

            if (txtPwd.Text.Trim() == "")
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter your Password', 'Error');";
                jScript += "</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;

            }

            if (txtConfirmPwd.Text.Trim() == "")
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please confirm your Password', 'Error');";
                jScript += "</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;

            }

           
            if (ddlCountry.SelectedIndex == 0)
            {
                //string jScript;
                //jScript = "<script>";
                //jScript += "ErrorDialog('Field Required','Please select your Country');";
                //jScript += "</script>";
                //ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", jScript, false);

                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please select your Country', 'Error');";
                jScript += "</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;
              
                
            }
            if (ddlProvinceLive.SelectedIndex == 0)
            {
              


                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please select your Province/State.', 'Error');";
                jScript += "</script>";




                //ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", jScript, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;
            }
            if (ddlCity.SelectedIndex == 0)
            {
               


                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please select your City.', 'Error');";
                jScript += "</script>";

                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;
            }

            if (!cbSignUp.Checked)
            {

                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('PLease accept the Privacy Policy | Terms & Conditions', 'Error');";
                jScript += "</script>";

                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;
            
            }


            if (Misc.validateEmailAddress(txtUsernameSignUp.Text.Trim()))
            {
                if (cbSignUp.Checked)
                {
                    //ccJoin.ValidateCaptcha(txtCaptchaCode.Text.Trim());
                    //if (ccJoin.UserValidated)
                    //{
                    //Hide the SignUp Area
                    //this.divSignUp.Visible = true;

                    BLLUser obj = new BLLUser();


                   
                    obj.userName = this.txtUsernameSignUp.Text.Trim();

                    obj.email = this.txtUsernameSignUp.Text.Trim();

                    obj.referralId = "";
                    if (!obj.getUserByUserName())
                    {
                        obj.age = ddlAge.SelectedValue.ToString();
                        obj.gender = rbMale.Checked;

                        obj.firstName = txtFullName.Text.Trim();
                        obj.lastName = txtLastName.Text.Trim();
                        obj.userName = this.txtUsernameSignUp.Text.Trim();
                        obj.userPassword = this.txtPwd.Text.Trim();
                        obj.email = this.txtUsernameSignUp.Text.Trim();

                        //For Customer 
                        obj.userTypeID = 4;
                        obj.isActive = true;

                        obj.referralId = "";
                        obj.countryId = Convert.ToInt32(ddlCountry.SelectedValue.ToString());
                        //if (hfProvince.Value != "0")
                        //{
                        int provinceID = 3;
                        int.TryParse(ddlProvinceLive.SelectedValue.ToString(), out provinceID);
                        obj.provinceId = provinceID;
                        int cityid = 0;
                        int.TryParse(ddlCity.SelectedValue.ToString(), out cityid);
                        obj.cityId = cityid;
                        obj.friendsReferralId = GetUserRefferalId();
                        obj.howYouKnowUs = "";
                       // obj.gender = RBMale.Checked;
                        //obj.age = dlAge.SelectedValue.ToString();
                        //obj.zipcode = txtZipCode.Text.Trim();
                        obj.ipAddress = Request.UserHostAddress.ToString();
                        //obj.gender
                        long result = obj.createUser();


                        Misc.addSubscriberEmail(txtUsernameSignUp.Text.Trim(), cityid.ToString());


                        if (result != 0)
                        {

                            GetAndSetAffInfoFromCookieInUserInfo(int.Parse(result.ToString().Trim()));

                            GECEncryption oEnc = new GECEncryption();

                            string strEncryptUserID = Server.UrlEncode(oEnc.EncryptData("123456", result.ToString())).Replace("%", "_");

                            SendMailWithActiveCode(this.txtUsernameSignUp.Text.Trim(), this.txtPwd.Text.Trim(), this.txtUsernameSignUp.Text.Trim(), strEncryptUserID);

                            


                            HttpCookie cookie = Request.Cookies["tastygoSignup"];
                            if (cookie == null)
                            {
                                cookie = new HttpCookie("tastygoSignup");
                            }
                            cookie.Expires = DateTime.Now.AddMonths(1);
                            Response.Cookies.Add(cookie);
                            cookie["tastygoSignup"] = this.txtUsernameSignUp.Text.Trim();
                            loggedInUser();
                            
                            Response.Redirect("signup.aspx?signup=true",true);                         
                            
                        }
                        else
                        {
                           

                            string jScript;
                            jScript = "<script>";
                            jScript += "MessegeArea('Sorry you could not register for right now please try again..', 'Error');";
                            jScript += "</script>";



                            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                        }
                    }
                    else
                    {
                      
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Email already exists. Please choose another.', 'Error');";
                        jScript += "</script>";



                        ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                        
                    }
                    //}
                    //else
                    //{
                    //    imgGridMessage.Visible = true;
                    //    lblErrorMessage.Visible = true;
                    //    lblErrorMessage.Text = ccJoin.CustomValidatorErrorMessage;
                    //    lblErrorMessage.Visible = true;
                    //    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    //}
                }
                else
                {
                   

                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Subscribe needs to be checked.', 'Error');";
                    jScript += "</script>";



                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                }
            }
            else
            {
             


                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Invalid email address.', 'Error');";
                jScript += "</script>";


                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
            }
        }
        catch (Exception ex)
        {
          

            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('Oops...an Error occurred There is an Error occur, please email us at support@tazzling.com or call 1855-295-1771.', 'Error');";
            jScript += "</script>";


            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }
    }

    protected void loggedInUser()
    {
        try
        {
            BLLUser obj = new BLLUser();

            obj.userName = txtUsernameSignUp.Text.Trim();
            obj.userPassword = txtPwd.Text.Trim();
            DataTable dtUser = obj.validateUserNamePassword();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                try
                {
                    if (dtUser.Rows[0]["userId"] != null && dtUser.Rows[0]["userId"].ToString().Trim() != "")
                    {
                        int userID = 1;                        
                        int.TryParse(dtUser.Rows[0]["userId"].ToString().Trim(), out userID);
                        //BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                        //bllKarma.userId = userID;
                        //DataTable dtkarmaPoints = bllKarma.getKarmaTodayLoginPointsByUserId();
                        //if (dtkarmaPoints != null && dtkarmaPoints.Rows.Count == 0)
                        //{
                        //    bllKarma.userId = userID;
                        //    bllKarma.karmaPoints = 250;
                        //    bllKarma.karmaPointsType = "Signup";
                        //    bllKarma.createdBy = userID;
                        //    bllKarma.createdDate = DateTime.Now;
                        //    bllKarma.createKarmaPoints();
                        //}
                    }
                }
                catch (Exception ex)
                { }

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
                cookie["tastygoSignup"] = txtUsernameSignUp.Text.Trim();
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
                // Response.Redirect("default.aspx", true);
            }
            else
            {
               
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Invalid user name or password.', 'Error');";
                jScript += "</script>";

                ScriptManager.RegisterClientScriptBlock(btnSignUp, typeof(Button), "Javascript", jScript, false);
                return;
            }
        }
        catch (Exception ex)
        {
           

            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('Oops...an Error occurred There is an Error occur, please email us at support@tazzling.com or call 1855-295-1771.', 'Error');";
            jScript += "</script>";


            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }
    }

    protected void SendEmail(string strUserID, string strAmount)
    {
        try
        {

            BLLUser obj = new BLLUser();
            obj.userId = Convert.ToInt32(strUserID);
            DataTable dtUser = obj.getUserByID();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
                string toAddress = dtUser.Rows[0]["userName"].ToString().Trim();
                string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string Subject = "You have received $" + strAmount + " tasty credits from Tazzling.Com";

                mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
                mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                mailBody.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
                mailBody.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                mailBody.Append("<strong>Dear " + dtUser.Rows[0]["firstname"].ToString().Trim() + " " + dtUser.Rows[0]["lastname"].ToString().Trim() + ",</strong></div>");
                mailBody.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received $" + strAmount + " tasty credit from Tazzling.com.</strong></div>");
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Use them today on Tazzling.Com towards our amazing deals!</div>");
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>How to apply my Tasty Credits?</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1.	Login Tazzling.com</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2.	Choose the deal you want to purchase, on the checkout page,</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3.	Enter the amount of credits you want to apply</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>4.	Complete the checkout!</div>");


                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");
                mailBody.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
                mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
                mailBody.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
                mailBody.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
                mailBody.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
                mailBody.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
                Misc.SendEmail(toAddress, "", "", fromAddress, Subject, mailBody.ToString());
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

    private bool AddTastyGoCreditToReffer(int fromID, long OrderID, long lRefId)
    {
        bool bStatus = false;

        try
        {

            BLLMemberUsedGiftCards objUsedCard = new BLLMemberUsedGiftCards();

            //Add $10 Credit into the User Account
            objUsedCard.remainAmount = float.Parse("5.00");

            objUsedCard.createdBy = lRefId;

            objUsedCard.gainedAmount = float.Parse("5.00");

            //If user places the first order then he will get the $10
            objUsedCard.fromId = fromID;

            objUsedCard.targetDate = DateTime.Now.AddMonths(6);

            objUsedCard.currencyCode = "CAD";

            objUsedCard.gainedType = "Refferal";

            objUsedCard.orderId = OrderID;

            if (objUsedCard.createMemberUseableGiftCard() == -1)
            {
                bStatus = true;
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
        return bStatus;
    }

    private bool SendMailForNewAccount(string strPassword, string strUserName, string strName)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string toAddress = strUserName;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailNewAccountCredentials"].ToString().Trim();
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + strName.Trim() + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your account has been recently created on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your account detail is following</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>User Name :  " + strUserName + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Password :" + strPassword.ToString().Trim() + "</div>");
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
            

            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('Oops...an Error occurred There is an Error occur, please email us at support@tazzling.com or call 1855-295-1771.', 'Error');";
            jScript += "</script>";


            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
            return false;
        }
    }

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

    #region Send Email for Forgot Password

    private bool SendMailWithActiveCode(string strEmailAddress, string strPassword, string strUserName, string strUserID)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
        try
        {

            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectActivation"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'>");
            mailBody.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 600'><title>Thank You for Registering with Tastygo</title>");
            mailBody.Append("<style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 480px)' type=text/css>*{line-height: normal !important;}</style></head>");
            mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            mailBody.Append("<table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='520' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='520' bgcolor='#FFFFFF' align='left'><div style='margin: 40px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'> <strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div><div style='margin: 0px 60px 15px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            mailBody.Append("<strong>Dear " + this.txtFullName.Text.Trim() + "</strong></div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("With the power of group ordering concept, Tastygo is excited to bring you amazing deals to experience, see, eat and buy at 50%~ 90% discount around your neighbourhood!");
            mailBody.Append("</div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("To activate your account, please click the follow the link below:<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "</a>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If clicking on the link doesn't work, try copy & paste it into your browser.");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Your Account detail:");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("User Name : " + strUserName);
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Your Password : " + strPassword.ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com'>support@tazzling.com</a> any time, or call 1855-295-1771. Our office hours are from Mon – Sun 9am – 6pm");
            mailBody.Append("</div>");
            //mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            //mailBody.Append("We wish you enjoy our deal experience.");
            //mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("</td></tr></table></td></tr></table></td></tr></table></body></html>");
            message.Body = mailBody.ToString();
            try
            { Misc.SendEmail("superadmin@tazzling.com", "", "", fromAddress, Subject, message.Body); }
            catch (Exception ex)
            { }
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);

        }
        catch (Exception ex)
        {


            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('Oops...an Error occurred There is an Error occur, please email us at support@tazzling.com or call 1855-295-1771.', 'Error');";
            jScript += "</script>";


            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
            return false;
        }
    }

    #endregion
}