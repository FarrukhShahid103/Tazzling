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
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net.Mail;
using com.optimalpayments.webservices;
public partial class checkoutgiftcard : System.Web.UI.Page
{
    
    BLLCities objCities = new BLLCities();
    BLLUserCCInfo objCCInfo = new BLLUserCCInfo();    
    BLLUser objUser = new BLLUser();        
    BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
    BLLGiftCard objGCDetail = new BLLGiftCard();
    BLLGiftCardOrder objGCOrder = new BLLGiftCardOrder();
    BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
    public static string strhideDive = "";
    public static string strHideDeliveryInfo = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = getCityName() + "'s Tasty Daily Deal | Buy Gift Card";
        ltUserIP.Text = "Anti-Fraud: " + Request.UserHostAddress;
        if (!IsPostBack)
        {
            LoadDropDownList();
            bindProvinces();
            try
            {
                string sURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"]==null)
                {
                    Label3.Text = "<span style='color:red;'>You must login to complete the checkout.</span> Tastygo voucher will be available and emailed to you once deal ends. If you have any question, please feel free to contact us <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a>";
                    lblErrorMessage.Text = "You must login to complete the checkout.";
                    lblErrorMessage.Visible = true;
                    btnCompleteOrder.Enabled = false;
                    string url = "";
                    
                }
                else
                {
                    Label3.Text = "Tastygo voucher will be available and emailed to you once deal ends. If you have any question, please feel free to contact us <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a>";
                    lblErrorMessage.Text = "";
                    lblErrorMessage.Visible = false;
                    btnCompleteOrder.Enabled = true;
                }
            }
            catch (Exception ex)
            {

            }

            //if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString() != "")
            //{

            if (Request.QueryString["lf"] != null && Request.QueryString["lf"].ToString() != "")
            {
                lblCompanyDetail.Text = "Invalid email or password.";
            }
            cbAgree.Checked = true;
            //cbInteract.Checked = false;
            cbMasterCard.Checked = true;

            DataTable dtUser2 = null;
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"]!=null)
            {
                if (Session["member"] != null)
                {
                    dtUser2 = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser2 = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser2 = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser2 = (DataTable)Session["user"];
                }
                txtFirstname.Text = dtUser2.Rows[0]["firstName"].ToString();
                txtLastName.Text = dtUser2.Rows[0]["lastName"].ToString();
                txtBUserName.Text = dtUser2.Rows[0]["firstName"].ToString() + " " + dtUser2.Rows[0]["lastName"].ToString();
                txtEmail.Text = dtUser2.Rows[0]["userName"].ToString();
                txtCEmailAddress.Text = dtUser2.Rows[0]["userName"].ToString();
                txtCEmailAddress.ReadOnly = true;
                txtEmail.ReadOnly = true;

                ViewState["userId"] = dtUser2.Rows[0]["userId"];
                divLogin.Visible = false;
                divFacebook.Visible = false;

                //Fill the Credit Card Info GridView
                if (dtUser2.Rows.Count > 0)
                {
                    //Get & Set the Remained Refferred Balance
                    getRemainedGainedBalByUserId(dtUser2);

                    ViewState["userId"] = dtUser2.Rows[0]["userId"].ToString();


                    //Hide-Show the Credit Card Info in the function below                           
                   
                        GridDataBind(Convert.ToInt64(dtUser2.Rows[0]["userId"].ToString()));

                }
            }
            else
            {
                HttpCookie cookie = Request.Cookies["newslettersubscribe"];
                if (cookie != null)
                {
                    txtEmail.Text = cookie.Values[0].ToString().Trim();
                    txtCEmailAddress.Text = cookie.Values[0].ToString().Trim();
                }

                divLogin.Visible = true;
                divFacebook.Visible = true;

                //Hide the Credit Card Grid here
                this.divDeliveryGridCCI.Visible = false;

                //Show the Credit Card Personal Info here
                this.divDelivery1.Visible = true;
                this.divDelivery2.Visible = false;
                this.divDelivery3.Visible = false;
                cbAgree.Visible = false;
                hlTermAndCondition.Visible = false;
                btnCompleteOrder.Visible = false;

                this.btnSave.Visible = false;
                this.CancelButton.Visible = false;
            }
           
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
            //lblAddressError.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            //lblAddressError.Visible = true;
            //imgGridMessage.Visible = true;
            //imgGridMessage.ImageUrl = "images/error.png";
            //lblAddressError.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }

    private void getRemainedGainedBalByUserId(DataTable dtUser)
    {
        try
        {
            double dGainedCredit = 0;
            BLLMemberUsedGiftCards objBLLMemberUsedGiftCards = new BLLMemberUsedGiftCards();
            objBLLMemberUsedGiftCards.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());

            DataTable dtUseAbleFoodCredit = null;
            dtUseAbleFoodCredit = objBLLMemberUsedGiftCards.getUseableFoodCreditRefferalByUserID();

            if (dtUseAbleFoodCredit != null && dtUseAbleFoodCredit.Rows.Count > 0 && dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString().Trim() != "")
            {
                hfTastyCredit.Value = dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString();
                dGainedCredit = Convert.ToDouble(dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString());
            }


            objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
            DataTable dtComissionMaoney = objBLLAffiliatePartnerGained.getGetAffiliatePartnerGainedCreditsByUserID();
            if (dtComissionMaoney != null && dtComissionMaoney.Rows.Count > 0 && dtComissionMaoney.Rows[0][0].ToString().Trim() != "")
            {
                hfComissionMoney.Value = dtComissionMaoney.Rows[0][0].ToString();
                dGainedCredit += Convert.ToDouble(dtComissionMaoney.Rows[0][0].ToString());
            }

            this.hfRefAmt.Value = (dGainedCredit).ToString();

            this.lblRefBal.Text = "($" + (dGainedCredit).ToString("###.00") + " CAD)";

            this.lblRefBalanace.Text = (dGainedCredit).ToString("###.00");
            txtTastyCredit.MaxLength = lblRefBalanace.Text.Length;
            if (dGainedCredit > 0)
                divRefBal.Visible = true;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void GridDataBind(long UserID)
    {
        DataTable dtCCInfo = null;

        objCCInfo = new BLLUserCCInfo();
        objCCInfo.userId = UserID;
        dtCCInfo = objCCInfo.getUserCCInfoByUserID();

        if ((dtCCInfo != null) && (dtCCInfo.Rows.Count > 0))
        {
            gvCustomers.DataSource = dtCCInfo;
            gvCustomers.DataBind();

            //Show the Credit Card Grid here
            this.divDeliveryGridCCI.Visible = true;

            //Hide the Credit Card Personal Info here
            this.divDelivery1.Visible = false;
            this.divDelivery2.Visible = false;
            this.divDelivery3.Visible = false;
            //Set the Mode here
            this.hfMode.Value = "grid";

            //Set the Default Select Radion button
            foreach (GridViewRow gvr in gvCustomers.Rows)
            {
                RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");
                rdb.Checked = true;
                break;
            }
        }
        else
        {
            //Hide the Credit Card Grid here
            this.divDeliveryGridCCI.Visible = false;

            //Show the Credit Card Personal Info here
            this.divDelivery1.Visible = true;
            this.divDelivery2.Visible = true;
            this.divDelivery3.Visible = true;

            this.btnSave.Visible = false;
            this.CancelButton.Visible = false;
        }
    }


    protected string GetCardExplain(object objCCNumber)
    {
        if (objCCNumber.ToString().Trim() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString()).Length - 4);
            // objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString().Trim());
        }
        return "";
    }

    protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
            case "EditCustomer":
                //Show the Credit Card Information                
                this.divDelivery2.Visible = true;
                this.divDelivery3.Visible = true;
                GetAndSetCreditCardInfo(int.Parse(e.CommandArgument.ToString()));
                this.btnSave.Visible = true;
                //Set the Mode here
                this.hfMode.Value = "grid";

                foreach (GridViewRow gvr in gvCustomers.Rows)
                {
                    RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");

                    LinkButton LnkbtnEdit = (LinkButton)gvr.FindControl("btnEdit");
                    //Button myButton = (Button)gvr.FindControl("b1");
                    if (LnkbtnEdit.CommandArgument.ToString() == e.CommandArgument.ToString())
                    {
                        rdb.Checked = true;
                    }
                    else { rdb.Checked = false; }
                }

                lblMessage.Visible = false;
                imgGridMessage.Visible = false;

                break;
        }
    }

    private void GetAndSetCreditCardInfo(int iCCInfoID)
    {
        try
        {
            objCCInfo = new BLLUserCCInfo();
            objCCInfo.ccInfoID = Convert.ToInt64(iCCInfoID.ToString());
            //Get the Credit Card Info of User here
            DataTable dtUserCCInfo = objCCInfo.getUserCCInfoByID();
            hfPostalCode.Value = "0";
            if ((dtUserCCInfo != null) && (dtUserCCInfo.Rows.Count > 0))
            {
                //Set the Delivery Information
                this.hfCCInfoIdEdit.Value = dtUserCCInfo.Rows[0]["ccInfoID"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoID"].ToString().Trim();

                //Set the First Name
                this.txtFirstname.Text = dtUserCCInfo.Rows[0]["ccInfoDFirstName"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDFirstName"].ToString().Trim();
                //Set the Last Name
                this.txtLastName.Text = dtUserCCInfo.Rows[0]["ccInfoDLastName"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDLastName"].ToString().Trim();
                //Set the Email                
                this.txtEmail.Text = dtUserCCInfo.Rows[0]["ccInfoDEmail"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDEmail"].ToString().Trim();
                //Set the Confirm Email here
                this.txtCEmailAddress.Text = dtUserCCInfo.Rows[0]["ccInfoDEmail"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDEmail"].ToString().Trim();

                GECEncryption objEnc = new GECEncryption();

                //Set the Billing Information here
                //Set the Business Name here
                this.txtBUserName.Text = dtUserCCInfo.Rows[0]["ccInfoBName"] == DBNull.Value ? "" : objEnc.DecryptData("colintastygochengusername", dtUserCCInfo.Rows[0]["ccInfoBName"].ToString().Trim());
                //Set the Business Address here
                this.txtBillingAddress.Text = dtUserCCInfo.Rows[0]["ccInfoBAddress"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                //Set the Business City here
                this.txtBCity.Text = dtUserCCInfo.Rows[0]["ccInfoBCity"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBCity"].ToString().Trim();
                //Set the Business Province name here
                try
                {
                    ddlProvince.SelectedValue = dtUserCCInfo.Rows[0]["ccInfoBProvince"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBProvince"].ToString().Trim();
                }
                catch (Exception ex)
                { }
                //Set the Business Postal Code here
                this.txtPostalCode.Text = dtUserCCInfo.Rows[0]["ccInfoBPostalCode"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBPostalCode"].ToString().Trim();

                //Set the Payment Information here
                //Set the Credit Card Number here
                this.txtCCNumber.Text = dtUserCCInfo.Rows[0]["ccInfoNumber"] == DBNull.Value ? "" : "XXXXXXXXXXXX" + objEnc.DecryptData("colintastygochengnumber", dtUserCCInfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtUserCCInfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4); ;
                // this.hfCCN.Value = dtUserCCInfo.Rows[0]["ccInfoNumber"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoNumber"].ToString();
                //this.divlblCCN.Visible = false;
                //this.divtxtCCN.Visible = false;
                txtCVNumber.Text = "";

                //Get the Selected Month & Year ccInfoEdate
                //string strMMyy = objEnc.DecryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
                string strMMyy = dtUserCCInfo.Rows[0]["ccInfoEdate"] == DBNull.Value ? "" : objEnc.DecryptData("colintastygochengexpirydate", dtUserCCInfo.Rows[0]["ccInfoEdate"].ToString().Trim());

                ArrayList arrMMyy = new ArrayList();
                arrMMyy.AddRange(strMMyy.Split('-'));

                //Set the Month here
                this.ddlMonth.SelectedValue = arrMMyy.Count > 1 ? arrMMyy[0].ToString() : "01";
                //Set the Year here
                this.ddlYear.SelectedValue = arrMMyy.Count > 1 ? arrMMyy[1].ToString() : "2010";
                //Set the CVN # here
                //this.txtCVNumber.Text = dtUserCCInfo.Rows[0]["ccInfoCCVNumber"] == DBNull.Value ? "" : objEnc.DecryptData("colintastygochengccv", dtUserCCInfo.Rows[0]["ccInfoCCVNumber"].ToString().Trim());
                this.hfCVNumber.Value = dtUserCCInfo.Rows[0]["ccInfoCCVNumber"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoCCVNumber"].ToString().Trim();
            }
        }
        catch (Exception ex)
        { }
    }

    private void LoadDropDownList()
    {
        try
        {
            //Clears the Drop Down List
            this.ddlYear.Items.Clear();

            //Year
            for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 7; year++)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();

        }
        catch (Exception ex)
        {
        }
    }
    protected void cbMasterCard_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //if (cbMasterCard.Checked)
            //{
            //    cbInteract.Checked = false;
            //    divDelivery3.Visible = true;
            //}
            //else
            //{
            //    cbInteract.Checked = true;
            //    divDelivery3.Visible = false;
            //}
        }
        catch (Exception ex)
        { }
    }

    protected void cbInteract_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //if (cbInteract.Checked)
            //{
            //    cbMasterCard.Checked = false;
            //    divDelivery3.Visible = false;
            //}
            //else
            //{
            //    cbMasterCard.Checked = true;
            //    divDelivery3.Visible = true;
            //}
        }
        catch (Exception ex)
        { }
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

    protected void btnApply_Click(object sender, ImageClickEventArgs e)
    {
        resetAmounts();
    }

    private void resetAmounts()
    {
        try
        {
            double dBalance = 0;
            double dUsed = 0;
            try
            {
                if (lblRefBalanace.Text.Trim() != "")
                {
                    dBalance = Convert.ToDouble(lblRefBalanace.Text.Trim());
                }
                if (txtTastyCredit.Text.Trim() != "")
                {
                    dUsed = Convert.ToDouble(txtTastyCredit.Text.Trim());
                }
            }
            catch (Exception ex)
            { }
            int int5Qty = Convert.ToInt32(ddl5Quntity.SelectedValue.ToString());
            int int10Qty = Convert.ToInt32(ddl10Quntity.SelectedValue.ToString());
            int int20Qty = Convert.ToInt32(ddl20Quntity.SelectedValue.ToString());
            int int50Qty = Convert.ToInt32(ddl50Quntity.SelectedValue.ToString());

            double dDealTotal = 0;
            hfPayFull.Value = dUsed.ToString();

            dDealTotal = (int5Qty * 5) + (int10Qty * 10) + (int20Qty * 20) + (int50Qty * 50);
            double dRemail = dDealTotal - dUsed;
            lbl5TotalPrice.Text = Convert.ToInt32(int5Qty * 5).ToString();
            lbl10TotalPrice.Text = Convert.ToInt32(int10Qty * 10).ToString();
            lbl20TotalPrice.Text = Convert.ToInt32(int20Qty * 20).ToString();
            lbl50TotalPrice.Text = Convert.ToInt32(int50Qty * 50).ToString();

            lblGrandTotal.Text = Convert.ToInt32(dRemail).ToString();
            if (dRemail == 0)
            {
                //hfPayFull.Value = "1";
                this.hfMode.Value = "new";
                Label8.Text = "";
                lblBUserName.Text = "* First and Last Name";

                lblBillingAddress.Text = "* Address";

                strhideDive = "none";
                RequiredFieldValidator8.Enabled = false;
                RequiredFieldValidator7.Enabled = false;

                divDelivery1.Visible = false;
                this.divDeliveryGridCCI.Visible = false;
                this.divDelivery2.Visible = true;
                this.divDelivery3.Visible = false;

                this.btnSave.Visible = false;
                this.CancelButton.Visible = false;
            }
            else
            {
                strhideDive = "";

                divDelivery1.Visible = false;
                //Hide the Credit Card Grid here
                this.divDeliveryGridCCI.Visible = true;
                //Show the Credit Card Personal Info here
                this.divDelivery2.Visible = false;
                this.divDelivery3.Visible = false;

            }

        }
        catch (Exception ex)
        { }
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

    protected void btnCompleteOrder_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblErrorMessage.Visible = false;
            lblErrorMessage.Text = "";

            if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"]==null)
            {
                Label3.Text = "<span style='color:red;'>You must login to complete the checkout.</span> Tastygo voucher will be available and emailed to you once deal ends. If you have any question, please feel free to contact us <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a>";
                lblErrorMessage.Text = "You must login to complete the checkout.";
                lblErrorMessage.Visible = true;
                btnCompleteOrder.Enabled = false;
                return;
            }

            if (divRefBal.Visible)
            {
                btnApply_Click(sender, e);
            }
            else
            {
                int int5Qty = Convert.ToInt32(ddl5Quntity.SelectedValue.ToString());
                int int10Qty = Convert.ToInt32(ddl10Quntity.SelectedValue.ToString());
                int int20Qty = Convert.ToInt32(ddl20Quntity.SelectedValue.ToString());
                int int50Qty = Convert.ToInt32(ddl50Quntity.SelectedValue.ToString());

                lbl5TotalPrice.Text = Convert.ToInt32(int5Qty * 5).ToString();
                lbl10TotalPrice.Text = Convert.ToInt32(int10Qty * 10).ToString();
                lbl20TotalPrice.Text = Convert.ToInt32(int20Qty * 20).ToString();
                lbl50TotalPrice.Text = Convert.ToInt32(int50Qty * 50).ToString();
                lblGrandTotal.Text = Convert.ToInt32((int5Qty * 5) + (int10Qty * 10) + (int20Qty * 20) + (int50Qty * 50)).ToString();
            }
            if (this.hfMode.Value.Trim() == "new")
            {
                DateTime dtUserDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
                if (dtUserDate < DateTime.Now)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Please confirm your expirary date.";
                    return;
                }
            }
            if (!cbAgree.Checked)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Accept terms & conditions.";
                return;
            }
            if (ddl10Quntity.SelectedValue.Trim() == "0" && ddl20Quntity.SelectedValue.Trim() == "0"
                && ddl50Quntity.SelectedValue.Trim() == "0" && ddl5Quntity.SelectedValue.Trim() == "0")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Select quantity.";
                return;
            }

            DataTable dtUser = null;
            if (ViewState["userId"] == null || ViewState["userId"].ToString() == "")
            {
                objUser = new BLLUser();
                objUser.email = txtEmail.Text.Trim();
                dtUser = objUser.getUserDetailByEmail();
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    try
                    {
                        objUser.firstName = txtFirstname.Text.Trim();
                        objUser.lastName = txtLastName.Text.Trim();
                        objUser.userId = Convert.ToInt32(ViewState["userId"].ToString());
                        objUser.updateUserFirstAndLastNameByID();
                    }
                    catch (Exception ex)
                    { }

                    ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                }
                else
                {
                    //Going to create new user
                    Random rand = new Random();
                    objUser.countryId = 2;
                    objUser.createdBy = 1;
                    objUser.email = txtEmail.Text.Trim();
                    objUser.firstName = txtFirstname.Text.Trim();
                    objUser.lastName = txtLastName.Text.Trim();
                    objUser.isActive = false;
                    objUser.provinceId = 3;

                    objUser.friendsReferralId = GetUserRefferalId();
                    objUser.userName = txtEmail.Text.Trim();
                    objUser.userTypeID = 4;
                    objUser.userPassword = GetPassword();
                    objUser.ipAddress = Request.UserHostAddress.ToString();
                    long result = objUser.createUser();
                    ViewState["userId"] = result;
                    string strCityID = "337";
                    HttpCookie yourCity = Request.Cookies["yourCity"];
                    if (yourCity != null)
                    {
                        strCityID = yourCity.Values[0].ToString().Trim();
                    }
                    Misc.addSubscriberEmail(txtEmail.Text.Trim(), strCityID);
                    if (result != 0)
                    {
                        //If exits then it will update into the User Info data table
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(result.ToString().Trim()));

                        //GECEncryption oEnc = new GECEncryption();
                        string strEncryptUserID =( Convert.ToInt64(result.ToString()) + 111111).ToString();
                        SendMailWithActiveCode(objUser.email, objUser.userPassword, objUser.email, strEncryptUserID);
                        SendMailToAdmin(objUser.email, objUser.email);
                    }
                }
            }
            else
            {
                try
                {
                    objUser.firstName = txtFirstname.Text.Trim();
                    objUser.lastName = txtLastName.Text.Trim();
                    objUser.userId = Convert.ToInt32(ViewState["userId"].ToString());
                    objUser.updateUserFirstAndLastNameByID();
                }
                catch (Exception ex)
                { }
            }
            GECEncryption objEnc = new GECEncryption();
            objGCOrder = new BLLGiftCardOrder();
            objGCDetail = new BLLGiftCard();
            long createdID = 0;           
            objGCOrder.creditCard = Convert.ToDouble(lblGrandTotal.Text.ToString().Trim());
            objGCOrder.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
           
            if (this.hfMode.Value.Trim() == "new")
            {
                if (objGCOrder.creditCard > 0)
                {

                    #region NetBack Payment Service Implementation
                    //Prepare the call to the Credit Card Web Service
                    CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
                    MerchantAccountV1 merchantAccount = new MerchantAccountV1();
                    merchantAccount.accountNum = ConfigurationManager.AppSettings["netBankAccountNum"].ToString().Trim();
                    merchantAccount.storeID = ConfigurationManager.AppSettings["netBankStoreID"].ToString().Trim();
                    merchantAccount.storePwd = ConfigurationManager.AppSettings["netBankStorePwd"].ToString().Trim();
                    ccAuthRequest.merchantAccount = merchantAccount;
                    ccAuthRequest.merchantRefNum = Guid.NewGuid().ToString();
                    ccAuthRequest.amount = objGCOrder.creditCard.ToString("###.00");

                    CardV1 card = new CardV1();
                    card.cardNum = txtCCNumber.Text.Trim();

                    CardExpiryV1 cardExpiry = new CardExpiryV1();
                    cardExpiry.month = Convert.ToInt32(ddlMonth.SelectedValue.ToString().Trim());
                    cardExpiry.year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                    card.cardExpiry = cardExpiry;
                    //card.cardType = CardTypeV1.VI;
                    card.cardTypeSpecified = true;
                    card.cvdIndicator = 1;
                    card.cvdIndicatorSpecified = true;
                    card.cvd = HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim());
                    ccAuthRequest.card = card;

                    BillingDetailsV1 billingDetails = new BillingDetailsV1();
                    billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
                    billingDetails.cardPayMethodSpecified = true;
                    billingDetails.firstName = txtBUserName.Text.Trim();
                    billingDetails.lastName = "";
                    billingDetails.street = txtBillingAddress.Text.Trim();
                    billingDetails.city = txtBCity.Text.Trim();
                    billingDetails.Item = (object) ddlProvince.SelectedValue.ToString().Trim(); // California
                    billingDetails.country = CountryV1.CA; // United States
                    billingDetails.countrySpecified = true;
                    billingDetails.zip = txtPostalCode.Text.Trim();
                    billingDetails.phone = "";
                    billingDetails.email = HtmlRemoval.StripTagsRegexCompiled(txtEmail.Text.Trim());
                    ccAuthRequest.billingDetails = billingDetails;
                    ccAuthRequest.customerIP = Request.UserHostAddress;
                    ccAuthRequest.productType = ProductTypeV1.M;

                    ccAuthRequest.productTypeSpecified = true;
                    // Perform the Web Services call for the purchase
                    CreditCardServiceV1 ccService = new CreditCardServiceV1();
                    CCTxnResponseV1 ccTxnResponse = ccService.ccPurchase(ccAuthRequest);
                    if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
                    {                        
                        objGCOrder.comments =  "Optimal : " + ccTxnResponse.code + " : " + ccTxnResponse.description + " : " + ccAuthRequest.merchantRefNum;
                        objGCOrder.orderIdReceived = ccTxnResponse.confirmationNumber;
                        objGCOrder.orderRefNo = "Optimal : " + ccTxnResponse.code + " : " + ccTxnResponse.description + " : " + ccAuthRequest.merchantRefNum;
                    }
                    else
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = ccTxnResponse.code + " : " + ccTxnResponse.description;                        
                        return;
                    }
                    #endregion   

                    #region Commented Code for Psigate payment
                    //System.Net.HttpWebRequest myWebReqeust = (HttpWebRequest)System.Net.WebRequest.Create(ConfigurationManager.AppSettings["psigateXMLlinq"].ToString());
                    //myWebReqeust.Timeout = 15000;
                    //StringBuilder myParams = new StringBuilder();
                    //myParams.Append("<?xml version='1.0' encoding='UTF-8'?>");
                    //myParams.Append("<Order>");
                    //myParams.Append("<StoreID>" + ConfigurationSettings.AppSettings["StoreID"].ToString() + "</StoreID>");
                    //myParams.Append("<Passphrase>" + ConfigurationSettings.AppSettings["Passphrase"].ToString() + "</Passphrase>");
                    //myParams.Append("<Bname>" + txtBUserName.Text.Trim() + "</Bname>");
                    //myParams.Append("<Bcompany></Bcompany>");
                    //myParams.Append("<Baddress1>" + txtBillingAddress.Text.Trim() + "</Baddress1>");
                    //myParams.Append("<Baddress2></Baddress2>");
                    //myParams.Append("<Bcity>" + txtBCity.Text.Trim() + "</Bcity>");
                    //myParams.Append("<Bprovince>" + ddlProvince.SelectedValue.ToString().Trim() + "</Bprovince>");
                    //myParams.Append("<Bpostalcode>" + txtPostalCode.Text.Trim() + "</Bpostalcode>");
                    //myParams.Append("<Bcountry>Canada</Bcountry>");
                    //myParams.Append("<Phone></Phone>");
                    //myParams.Append("<Fax></Fax>");
                    //myParams.Append("<Email>" + txtEmail.Text.Trim() + "</Email>");
                    //myParams.Append("<Comments></Comments>");
                    //myParams.Append("<Tax1>0.00</Tax1>");
                    //myParams.Append("<ShippingTotal>0.00</ShippingTotal>");
                    ////myParams.Append("<Subtotal>" + (Convert.ToDouble(dtorders.Rows[i]["sellingPrice"].ToString()) * Convert.ToDouble(dtorders.Rows[i]["Qty"].ToString())).ToString() + "</Subtotal>");
                    //myParams.Append("<Subtotal>" + objGCOrder.creditCard + "</Subtotal>");
                    //myParams.Append("<PaymentType>CC</PaymentType>");
                    //myParams.Append("<CardAction>0</CardAction>");
                    //myParams.Append("<CardNumber>" + txtCCNumber.Text.Trim() + "</CardNumber>");
                    //myParams.Append("<CardExpMonth>" + ddlMonth.SelectedValue.ToString() + "</CardExpMonth>");
                    //myParams.Append("<CardExpYear>" + ddlYear.SelectedValue.ToString().Substring(ddlYear.SelectedValue.ToString().Length - 2) + "</CardExpYear>");
                    //myParams.Append("<CardIDNumber>" + txtCVNumber.Text.Trim() + "</CardIDNumber>");
                    //myParams.Append("</Order>");
                    //myWebReqeust.Method = "POST";
                    //myWebReqeust.ContentLength = myParams.ToString().Length;
                    //myWebReqeust.ContentType = "application/x-www-form-urlencoded";
                    //myWebReqeust.KeepAlive = false;
                    //System.IO.StreamWriter myWriter;
                    //myWriter = new System.IO.StreamWriter(myWebReqeust.GetRequestStream());
                    //myWriter.Write(myParams.ToString());
                    //myWriter.Close();
                    //try
                    //{
                    //    System.Net.WebResponse myWebResponse = myWebReqeust.GetResponse();
                    //    StreamReader myStreamReader = new StreamReader(myWebResponse.GetResponseStream());
                    //    string myHTML = myStreamReader.ReadToEnd();
                    //    XmlDocument doc = new XmlDocument();
                    //    doc.LoadXml(myHTML);
                    //    XmlNode root = doc.DocumentElement;
                    //    if (root.HasChildNodes)
                    //    {
                    //        if (root.ChildNodes[3].InnerText.ToString().ToLower() == "approved")
                    //        {
                    //            objGCOrder.orderIdReceived = root.ChildNodes[1].InnerText.ToString();
                    //            objGCOrder.orderRefNo = root.ChildNodes[12].InnerText.ToString();
                    //            objGCOrder.comments = root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
                    //            // objGCOrder.createGiftCardOrderByUser();
                    //            myStreamReader.Close();

                    //        }
                    //        else if (root.ChildNodes[3].InnerText.ToString().ToLower() == "declined" || root.ChildNodes[3].InnerText.ToString().ToLower() == "error")
                    //        {
                    //            // objGCOrder.orderStatus = "Declined";
                    //            lblErrorMessage.Visible = true;
                    //            lblErrorMessage.Text = root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
                    //            myStreamReader.Close();
                    //            return;
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{ }
                    #endregion
                }
                objCCInfo = new BLLUserCCInfo();
                objCCInfo.ccInfoBAddress = txtBillingAddress.Text.Trim();
                objCCInfo.ccInfoBCity = txtBCity.Text.Trim();
                objCCInfo.ccInfoBPostalCode = txtPostalCode.Text.Trim();
                objCCInfo.ccInfoBProvince = ddlProvince.SelectedValue.ToString();
                if (ViewState["userId"] != null && ViewState["userId"].ToString() != "")
                {
                    objCCInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                    objCCInfo.userId = Convert.ToInt64(ViewState["userId"].ToString());
                }
                if (txtCVNumber.Text.Trim() == "")
                {
                    objCCInfo.ccInfoCCVNumber = "0";
                }
                else
                {
                    objCCInfo.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", txtCVNumber.Text.Trim());
                }
                objCCInfo.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
                if (txtCCNumber.Text.Trim() == "")
                {
                    objCCInfo.ccInfoNumber = "0";
                }
                else
                {
                    objCCInfo.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", txtCCNumber.Text.Trim());
                }
                objCCInfo.ccInfoBName = objEnc.EncryptData("colintastygochengusername", txtBUserName.Text.Trim());
                objCCInfo.ccInfoDEmail = txtEmail.Text.Trim();
                objCCInfo.ccInfoDFirstName = txtFirstname.Text.Trim();
                objCCInfo.ccInfoDLastName = txtLastName.Text.Trim();
                createdID = objCCInfo.createUserCCInfo();
            }
            else if (this.hfMode.Value.Trim() == "grid")
            {
                //Get the  CCInfoID
                foreach (GridViewRow gvr in gvCustomers.Rows)
                {
                    RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");

                    //Button myButton = (Button)gvr.FindControl("b1");
                    if (rdb.Checked)
                    {
                        HiddenField hfccInfoID = (HiddenField)gvr.FindControl("hfccInfoID");
                        createdID = long.Parse(hfccInfoID.Value.Trim());
                    }
                }
                objCCInfo = new BLLUserCCInfo();
                objCCInfo.ccInfoID = createdID;
                DataTable dtCCinfo = objCCInfo.getUserCCInfoByID();
                if (dtCCinfo != null && dtCCinfo.Rows.Count > 0)
                {
                    if (objGCOrder.creditCard > 0)
                    {
                        string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
                        if (strDate.Length == 2)
                        {
                            DateTime dtUserDate = new DateTime(Convert.ToInt32(strDate[1].ToString()), Convert.ToInt32(strDate[0].ToString()), 1);
                            if (dtUserDate < DateTime.Now)
                            {
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = "Please confirm your expirary date.";
                                return;
                            }
                        }
                        #region NetBack Payment Service Implementation
                        //Prepare the call to the Credit Card Web Service
                        CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
                        MerchantAccountV1 merchantAccount = new MerchantAccountV1();
                        merchantAccount.accountNum = ConfigurationManager.AppSettings["netBankAccountNum"].ToString().Trim();
                        merchantAccount.storeID = ConfigurationManager.AppSettings["netBankStoreID"].ToString().Trim();
                        merchantAccount.storePwd = ConfigurationManager.AppSettings["netBankStorePwd"].ToString().Trim();
                        ccAuthRequest.merchantAccount = merchantAccount;
                        ccAuthRequest.merchantRefNum = Guid.NewGuid().ToString();
                        ccAuthRequest.amount = objGCOrder.creditCard.ToString("###.00");

                        CardV1 card = new CardV1();
                        card.cardNum = objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString());
                        CardExpiryV1 cardExpiry = new CardExpiryV1();
                        if (strDate.Length == 2)
                        {
                            cardExpiry.month = Convert.ToInt32(strDate[0].ToString());
                            cardExpiry.year = Convert.ToInt32(strDate[1].ToString());
                        }
                        else
                        {
                            cardExpiry.month = DateTime.Now.Month;
                            cardExpiry.year = DateTime.Now.Year;
                        }
                        card.cardExpiry = cardExpiry;
                        //card.cardType = CardTypeV1.VI;
                        card.cardTypeSpecified = false;
                        card.cvdIndicator = 1;
                        card.cvdIndicatorSpecified = true;
                        card.cvd = objEnc.DecryptData("colintastygochengccv", dtCCinfo.Rows[0]["ccInfoCCVNumber"].ToString());
                        ccAuthRequest.card = card;

                        BillingDetailsV1 billingDetails = new BillingDetailsV1();
                        billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
                        billingDetails.cardPayMethodSpecified = true;
                        billingDetails.firstName = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
                        billingDetails.lastName = "";
                        billingDetails.street = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                        billingDetails.city = dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
                        billingDetails.Item = (object)dtCCinfo.Rows[0]["ccInfoBProvince"].ToString(); // California
                        billingDetails.country = CountryV1.CA; // United States
                        billingDetails.countrySpecified = true;
                        billingDetails.zip = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
                        billingDetails.phone = "";
                        billingDetails.email = dtCCinfo.Rows[0]["ccInfoDEmail"].ToString();
                        ccAuthRequest.billingDetails = billingDetails;
                        ccAuthRequest.customerIP = Request.UserHostAddress;
                        ccAuthRequest.productType = ProductTypeV1.M;
                        ccAuthRequest.dupeCheck = true;
                        ccAuthRequest.productTypeSpecified = true;
                        // Perform the Web Services call for the purchase
                        CreditCardServiceV1 ccService = new CreditCardServiceV1();
                        CCTxnResponseV1 ccTxnResponse = ccService.ccPurchase(ccAuthRequest);
                        if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
                        {
                            objGCOrder.comments = "Optimal : " + ccTxnResponse.code + " : " + ccTxnResponse.description + " : " + ccAuthRequest.merchantRefNum;
                            objGCOrder.orderIdReceived = ccTxnResponse.confirmationNumber;
                            objGCOrder.orderRefNo = "Optimal : " + ccTxnResponse.code + " : " + ccTxnResponse.description + " : " + ccAuthRequest.merchantRefNum;
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = ccTxnResponse.code + " : " + ccTxnResponse.description;
                            return;
                        }
                        #endregion

                        #region Pisgate Commented Code
                        //System.Net.HttpWebRequest myWebReqeust = (HttpWebRequest)System.Net.WebRequest.Create(ConfigurationManager.AppSettings["psigateXMLlinq"].ToString());
                        //myWebReqeust.Timeout = 15000;
                        //StringBuilder myParams = new StringBuilder();
                        //myParams.Append("<?xml version='1.0' encoding='UTF-8'?>");
                        //myParams.Append("<Order>");
                        //myParams.Append("<StoreID>" + ConfigurationSettings.AppSettings["StoreID"].ToString() + "</StoreID>");
                        //myParams.Append("<Passphrase>" + ConfigurationSettings.AppSettings["Passphrase"].ToString() + "</Passphrase>");
                        //myParams.Append("<Bname>" + objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString()) + "</Bname>");
                        //myParams.Append("<Bcompany></Bcompany>");
                        //myParams.Append("<Baddress1>" + dtCCinfo.Rows[0]["ccInfoBAddress"].ToString() + "</Baddress1>");
                        //myParams.Append("<Baddress2></Baddress2>");
                        //myParams.Append("<Bcity>" + dtCCinfo.Rows[0]["ccInfoBCity"].ToString() + "</Bcity>");
                        //myParams.Append("<Bprovince>" + dtCCinfo.Rows[0]["ccInfoBProvince"].ToString() + "</Bprovince>");
                        //myParams.Append("<Bpostalcode>" + dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString() + "</Bpostalcode>");
                        //myParams.Append("<Bcountry>Canada</Bcountry>");
                        //myParams.Append("<Phone></Phone>");
                        //myParams.Append("<Fax></Fax>");
                        //myParams.Append("<Email>" + dtCCinfo.Rows[0]["ccInfoDEmail"].ToString() + "</Email>");
                        //myParams.Append("<Comments></Comments>");
                        //myParams.Append("<Tax1>0.00</Tax1>");
                        //myParams.Append("<ShippingTotal>0.00</ShippingTotal>");
                        ////myParams.Append("<Subtotal>" + (Convert.ToDouble(dtorders.Rows[i]["sellingPrice"].ToString()) * Convert.ToDouble(dtorders.Rows[i]["Qty"].ToString())).ToString() + "</Subtotal>");
                        //myParams.Append("<Subtotal>" + objGCOrder.creditCard + "</Subtotal>");
                        //myParams.Append("<PaymentType>CC</PaymentType>");
                        //myParams.Append("<CardAction>0</CardAction>");
                        //myParams.Append("<CardNumber>" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()) + "</CardNumber>");
                        //if (strDate.Length == 2)
                        //{
                        //    myParams.Append("<CardExpMonth>" + strDate[0].ToString() + "</CardExpMonth>");
                        //    myParams.Append("<CardExpYear>" + strDate[1].ToString().Substring(strDate[1].ToString().Length - 2) + "</CardExpYear>");
                        //}
                        //else
                        //{
                        //    myParams.Append("<CardExpMonth>" + DateTime.Now.ToString("MM") + "</CardExpMonth>");
                        //    myParams.Append("<CardExpYear>" + DateTime.Now.ToString("yy") + "</CardExpYear>");
                        //}
                        //myParams.Append("<CardIDNumber>" + objEnc.DecryptData("colintastygochengccv", dtCCinfo.Rows[0]["ccInfoCCVNumber"].ToString()) + "</CardIDNumber>");
                        //myParams.Append("</Order>");
                        //myWebReqeust.Method = "POST";
                        //myWebReqeust.ContentLength = myParams.ToString().Length;
                        //myWebReqeust.ContentType = "application/x-www-form-urlencoded";
                        //myWebReqeust.KeepAlive = false;
                        //System.IO.StreamWriter myWriter;
                        //myWriter = new System.IO.StreamWriter(myWebReqeust.GetRequestStream());
                        //myWriter.Write(myParams.ToString());
                        //myWriter.Close();
                        //try
                        //{
                        //    System.Net.WebResponse myWebResponse = myWebReqeust.GetResponse();
                        //    StreamReader myStreamReader = new StreamReader(myWebResponse.GetResponseStream());
                        //    string myHTML = myStreamReader.ReadToEnd();
                        //    XmlDocument doc = new XmlDocument();
                        //    doc.LoadXml(myHTML);
                        //    XmlNode root = doc.DocumentElement;
                        //    if (root.HasChildNodes)
                        //    {
                        //        if (root.ChildNodes[3].InnerText.ToString().ToLower() == "approved")
                        //        {
                        //            objGCOrder.orderIdReceived = root.ChildNodes[1].InnerText.ToString();
                        //            objGCOrder.orderRefNo = root.ChildNodes[12].InnerText.ToString();
                        //            objGCOrder.comments = root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
                        //            // objGCOrder.createGiftCardOrderByUser();
                        //            myStreamReader.Close();

                        //        }
                        //        else if (root.ChildNodes[3].InnerText.ToString().ToLower() == "declined" || root.ChildNodes[3].InnerText.ToString().ToLower() == "error")
                        //        {
                        //            // objGCOrder.orderStatus = "Declined";
                        //            lblErrorMessage.Visible = true;
                        //            lblErrorMessage.Text = root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
                        //            myStreamReader.Close();
                        //            return;
                        //        }
                        //    }
                        //}
                        //catch (Exception ex)
                        //{ }
                        #endregion
                    }

                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                    return;
                }
            }
            if ((this.hfPayFull.Value.Trim().Length > 0 ? float.Parse(this.hfPayFull.Value.Trim()) : 0) > 0)
            {
                deductTastyCreditFromUserAccount(float.Parse(this.hfPayFull.Value.Trim()), int.Parse(ViewState["userId"].ToString()));
                objGCOrder.commission = Convert.ToDouble(this.hfComissionMoney.Value.Trim());
                objGCOrder.redeem = Convert.ToDouble(this.hfTastyCredit.Value.Trim());
            }
            else
            {
                objGCOrder.commission = 0;
                objGCOrder.redeem = 0;
            }           
            objGCOrder.subTotalAmount = objGCOrder.creditCard + objGCOrder.commission + objGCOrder.redeem;
            objGCOrder.orderStatus = "APPROVED";
            objGCOrder.ccInfoID = createdID;
            objGCDetail.giftCardOrderId = objGCOrder.createGiftCardOrderByUser();
            if (objGCDetail.giftCardOrderId != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    float intUnitPrice = 0;
                    int intQty = 0;
                    if (i == 0)
                    {
                        intUnitPrice = 5;
                        intQty = Convert.ToInt32(ddl5Quntity.SelectedValue.ToString());
                    }
                    else if (i == 1)
                    {
                        intUnitPrice = 10;
                        intQty = Convert.ToInt32(ddl10Quntity.SelectedValue.ToString());
                    }
                    else if (i == 2)
                    {
                        intUnitPrice = 20;
                        intQty = Convert.ToInt32(ddl20Quntity.SelectedValue.ToString());
                    }
                    else if (i == 3)
                    {
                        intUnitPrice = 50;
                        intQty = Convert.ToInt32(ddl50Quntity.SelectedValue.ToString());
                    }
                    for (int k = 0; k < intQty; k++)
                    {
                        objGCDetail.giftCardCode = GenerateId().ToString().Substring(0, 10);
                        objGCDetail.giftCardAmount = intUnitPrice;
                        objGCDetail.currencyCode = "CAD";
                        objGCDetail.expirationDate = DateTime.Now.AddMonths(6);
                        objGCDetail.creationDate = DateTime.Now;
                        objGCDetail.createdBy = objGCOrder.createdBy;
                        objGCDetail.createGiftCardByUser();
                    }
                }
            }
            try
            { Misc.SendEmail(txtEmail.Text.ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString(), "Gift Card purchase confirmation", RenderOrderHTML(createdID)); }
            catch (Exception ex)
            { }
            Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/ordergiftcardComplete.aspx", false);
        }
        catch (Exception ex)
        { }
    }


    public string RenderOrderHTML(long ccID)
    {
        try
        {
            objCCInfo = new BLLUserCCInfo();
            objCCInfo.ccInfoID = ccID;
            DataTable dtCCinfo = objCCInfo.getUserCCInfoByID();
            GECEncryption objEnc = new GECEncryption();
            StringBuilder sb = new StringBuilder();
            if (dtCCinfo != null && dtCCinfo.Rows.Count > 0)
            {
                sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
                sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
                sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                sb.Append("<strong>Dear " + dtCCinfo.Rows[0]["ccInfoBName"] == null ? "User" : objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString()) + ",</strong></div>");
                sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Thank for purchasing gift card on Tazzling.Com.</strong></div>");

                sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
                sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>How to use:</div>");
                sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1.	Login to Tazzling.com</div>");
                sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2.	Go to member area on the top right corner.</div>");
                sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3.	Click Gift Card tab, and you’ll have the option to send, print, or copy down the 10 digit gift card code.</div>");
                sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>4.	Have the recipient go to the same Gift card section, and deposit the gift card, using the code you gave them!</div>");

                sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>That’s it!</div>");
                sb.Append("<div style='margin: 0px 60px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><strong>Tastygo Online Inc. Invoice</strong></div>");
                sb.Append("<table style='margin: 0px 0px 5px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><tr><td style='width: 90px;'><strong>Deal Title:</strong></td>");
                sb.Append("<td>");
                sb.Append("Gift Card from Tazzling.com");
                sb.Append("</td></tr>");
                sb.Append("<tr><td style='float: left; width: 90px;'><strong>Status:</strong></td>");
                sb.Append("<td>");
                sb.Append("Successful");
                sb.Append("</td></tr>");
                sb.Append("<tr><td style='float: left; width: 90px;'><strong>Date & Time:</strong></td>");
                sb.Append("<td>");
                sb.Append(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
                sb.Append("</td></tr>");
                if (dtCCinfo.Rows[0]["ccInfoNumber"].ToString().Trim() != "0")
                {
                    sb.Append("<tr><td style='float: left; width: 90px;'><strong>Card Number:</strong></td>");
                    sb.Append("<td>");
                    sb.Append(dtCCinfo.Rows[0]["ccInfoNumber"] == "" ? "" : "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4));
                    sb.Append("</td></tr>");
                }
                sb.Append("<tr><td style='float: left; width: 90px;'><strong>Billing Name:</strong></td>");
                sb.Append("<td>");
                sb.Append(dtCCinfo.Rows[0]["ccInfoBName"] == "" ? "" : objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString()));
                sb.Append("</td></tr></table>");
                sb.Append("<div style='padding: 15px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 16px; line-height: 1.4em; clear: both;'><strong>Order Detail</strong></div>");
                sb.Append("<table style='margin: 0px 10px 10px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; clear: both;'><tr><td style='float: left; width: 300px;'><strong>Card Description</strong></td><td style='float: left; width: 100px;'><strong>Quantity</strong></td><td style='float: left; width: 100px;'><strong>Unit Price</strong></td><td style='float: left; width: 100px;'><strong>Total</strong></td></tr>");
                if (ddl5Quntity.SelectedIndex != 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>");
                    sb.Append("$5 Gift Card");
                    sb.Append("</td><td style='float: left; width: 100px;'>");
                    sb.Append(ddl5Quntity.SelectedValue.ToString());
                    sb.Append("</td><td style='float: left; width: 100px;'>$");
                    sb.Append("5");
                    sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                    sb.Append(Convert.ToDouble(Convert.ToDouble(ddl5Quntity.SelectedValue.ToString()) * Convert.ToDouble(5)).ToString("###.00"));
                    sb.Append(" CAD</strong></td></tr>");
                }
                if (ddl10Quntity.SelectedIndex != 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>");
                    sb.Append("$10 Gift Card");
                    sb.Append("</td><td style='float: left; width: 100px;'>");
                    sb.Append(ddl10Quntity.SelectedValue.ToString());
                    sb.Append("</td><td style='float: left; width: 100px;'>$");
                    sb.Append("10");
                    sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                    sb.Append(Convert.ToDouble(Convert.ToDouble(ddl10Quntity.SelectedValue.ToString()) * Convert.ToDouble(10)).ToString("###.00"));
                    sb.Append(" CAD</strong></td></tr>");
                }
                if (ddl20Quntity.SelectedIndex != 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>");
                    sb.Append("$20 Gift Card");
                    sb.Append("</td><td style='float: left; width: 100px;'>");
                    sb.Append(ddl20Quntity.SelectedValue.ToString());
                    sb.Append("</td><td style='float: left; width: 100px;'>$");
                    sb.Append("20");
                    sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                    sb.Append(Convert.ToDouble(Convert.ToDouble(ddl20Quntity.SelectedValue.ToString()) * Convert.ToDouble(20)).ToString("###.00"));
                    sb.Append(" CAD</strong></td></tr>");
                }
                if (ddl50Quntity.SelectedIndex != 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>");
                    sb.Append("$50 Gift Card");
                    sb.Append("</td><td style='float: left; width: 100px;'>");
                    sb.Append(ddl50Quntity.SelectedValue.ToString());
                    sb.Append("</td><td style='float: left; width: 100px;'>$");
                    sb.Append("50");
                    sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                    sb.Append(Convert.ToDouble(Convert.ToDouble(ddl50Quntity.SelectedValue.ToString()) * Convert.ToDouble(50)).ToString("###.00"));
                    sb.Append(" CAD</strong></td></tr>");
                }
                sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;</td></tr>");
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Total</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + objGCOrder.subTotalAmount.ToString() + " CAD</strong></td></tr>");
                if (objGCOrder.commission.ToString().Trim() != "" && Convert.ToDouble(objGCOrder.commission.ToString().Trim()) > 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px;padding-left:50px;'><strong>Charge From Commission</strong></td><td style='float: left; width: 100px;'>");
                    sb.Append("<strong>$" + objGCOrder.commission.ToString().Trim() + " CAD</strong></td></tr>");
                }
                if (objGCOrder.creditCard.ToString().Trim() != "" && Convert.ToDouble(objGCOrder.creditCard.ToString().Trim()) > 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px;padding-left:50px;'><strong>Charge From Credit Card</strong></td><td style='float: left; width: 100px;'>");
                    sb.Append("<strong>$" + objGCOrder.creditCard.ToString().Trim() + " CAD</strong></td></tr>");
                }
                if (objGCOrder.redeem.ToString().Trim() != "" && Convert.ToDouble(objGCOrder.redeem.ToString().Trim()) > 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px;padding-left:50px;'><strong>Charge From Tasty Credit</strong></td><td style='float: left; width: 100px;'>");
                    sb.Append("<strong>$" + objGCOrder.redeem.ToString().Trim() + " CAD</strong></td></tr>");
                }
                sb.Append("</table>");
                //sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;<td></tr>");
                // sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Balance</strong></td><td style='float: left; width: 100px;'><strong>$0 CAD</strong></td></tr></table>");            
                sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
                sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
                sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
                sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
                sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
                sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "";
        }

    }


    private long GenerateId()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }

    private void deductTastyCreditFromUserAccount(float amount, int iUserID)
    {
        float remAmount = 0;
        float tastyCreditUsed = 0;
        float tastyComissionUsed = 0;
        try
        {
            if (hfTastyCredit.Value.ToString().Trim() != "" && float.Parse(hfTastyCredit.Value.ToString()) > 0)
            {
                BLLMemberUsedGiftCards objUseableCard = new BLLMemberUsedGiftCards();
                objUseableCard.createdBy = iUserID;
                DataTable dt = objUseableCard.getAllRefferalTastyCreditsByUserID();
                float dTastyCredit = float.Parse(hfTastyCredit.Value);
                float deductAmount = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dTastyCredit <= 0 || amount <= 0)
                        { break; }
                        remAmount = float.Parse(dt.Rows[i]["remainAmount"].ToString());
                        float fOrignalRemainAmount = float.Parse(dt.Rows[i]["remainAmount"].ToString());
                        if (deductAmount > 0 && remAmount > 0)
                        {
                            if (remAmount > deductAmount)
                            {
                                remAmount = remAmount - deductAmount;
                                deductAmount = 0;
                            }
                            else
                            {
                                deductAmount = deductAmount - remAmount;
                                remAmount = 0;
                            }
                        }
                        if (remAmount > 0)
                        {
                            if (remAmount >= amount)
                            {
                                objUseableCard.gainedId = Convert.ToInt64(dt.Rows[i]["gainedId"].ToString());
                                objUseableCard.modifiedBy = iUserID;
                                objUseableCard.remainAmount = remAmount - amount;
                                objUseableCard.updateRemainingUsableAmount();
                                tastyCreditUsed += amount;
                                amount = 0;
                                break;
                            }
                            else
                            {
                                objUseableCard.gainedId = Convert.ToInt64(dt.Rows[i]["gainedId"].ToString());
                                objUseableCard.modifiedBy = iUserID;
                                objUseableCard.remainAmount = fOrignalRemainAmount - remAmount;
                                objUseableCard.updateRemainingUsableAmount();
                                amount = amount - remAmount;
                                dTastyCredit = dTastyCredit - remAmount;
                                tastyCreditUsed += remAmount;
                            }
                        }
                        else if (remAmount < 0)
                        {
                            deductAmount = deductAmount - remAmount;
                        }
                    }
                }
            }
            if (hfComissionMoney.Value.ToString().Trim() != "" && float.Parse(hfComissionMoney.Value.ToString()) > 0 && amount > 0)
            {
                objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
                objBLLAffiliatePartnerGained.UserId = iUserID;
                DataTable dt = objBLLAffiliatePartnerGained.getGetAllAffiliatePartnerGainedByUserID();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        remAmount = float.Parse(dt.Rows[i]["remainAmount"].ToString());
                        if (remAmount > 0)
                        {
                            if (remAmount >= amount)
                            {
                                objBLLAffiliatePartnerGained.AffiliatePartnerId = Convert.ToInt32(dt.Rows[i]["affiliatePartnerId"].ToString());
                                objBLLAffiliatePartnerGained.ModifiedBy = iUserID;
                                objBLLAffiliatePartnerGained.RemainAmount = remAmount - amount;
                                objBLLAffiliatePartnerGained.updateAffiliateRemainingUsableAmount();
                                tastyComissionUsed += amount;
                                break;
                            }
                            else
                            {
                                objBLLAffiliatePartnerGained.AffiliatePartnerId = Convert.ToInt32(dt.Rows[i]["affiliatePartnerId"].ToString());
                                objBLLAffiliatePartnerGained.ModifiedBy = iUserID;
                                objBLLAffiliatePartnerGained.RemainAmount = 0;
                                objBLLAffiliatePartnerGained.updateAffiliateRemainingUsableAmount();
                                amount = amount - remAmount;
                                tastyComissionUsed += remAmount;
                            }
                        }
                    }
                }
            }

            hfTastyCredit.Value = tastyCreditUsed.ToString();
            hfComissionMoney.Value = tastyComissionUsed.ToString();

        }
        catch (Exception ex)
        {
            
        }
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
            mailBody.Append("<strong>Dear " + txtFirstname.Text.Trim() + " " + txtLastName.Text.Trim() + "</strong></div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("With the power of group ordering concept, Tastygo brings amazing deal, from 50%~ 90% off  around your neighbourhood.");
            mailBody.Append("</div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("To activate your account, please click the follow the link below:<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "</a>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If clicking on the link doesn't work, try copy & paste it into your browser.");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Account detail:");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("User Name :  " + strUserName);
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Password :" + strPassword.ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com'>support@tazzling.com</a>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("We wish you enjoy our deal experience.");
            mailBody.Append("</div>");
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
            return false;
        }
    }    
    #endregion

    #region Send Email for Forgot Password
    private bool SendMailToAdmin(string strEmailAddress, string strUserName)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string toAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectMemberRequest"].ToString().Trim();
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear Admin,</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received a member request on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>We have sent an email to member's email address to activate his/her account.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Following are the detail for the new member user</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>User Name :  " + strUserName + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Email : <a href='mailto:" + strEmailAddress + "'> " + strEmailAddress + " </a></div>");
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
            return false;
        }
    }
    #endregion
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objUser = new BLLUser();
            objUser.userName = txtLoginEmailAddress.Text.Trim();
            objUser.userPassword = txtPassword.Text.ToString();

            DataTable dtUser = objUser.validateUserNamePassword();
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

                Response.Redirect("checkoutgiftcard.aspx", false);

            }
            else
            {
                // lblFailureText.Visible = true;
                // lblCompanyDetail.Text = "Invalid user name or password.";
                Response.Redirect("checkoutgiftcard.aspx?lf=1");
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnSaveGiftInfo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strScript = "uncheckOthers(" + ((RadioButton)e.Row.Cells[0].FindControl("MyRadioButton")).ClientID + ");";
                ((RadioButton)e.Row.Cells[0].FindControl("MyRadioButton")).Attributes.Add("onclick", strScript);
            }
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Error ');", true);
        }
    }

    protected void bindProvinces()
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlProvince.DataSource = dt.DefaultView;
            ddlProvince.DataTextField = "provinceName";
            ddlProvince.DataValueField = "provinceName";
            ddlProvince.DataBind();
            ddlProvince.SelectedIndex = 1;
            // ddlProvinceLive.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAddNewCCI_Click(object sender, EventArgs e)
    {
        try
        {
            this.btnSave.Visible = false;
            this.CancelButton.Visible = true;
            hfPostalCode.Value = "0";
            //Set the Mode here
            this.hfMode.Value = "new";

            //Show the Credit Card Personal Info here
            this.divDelivery1.Visible = true;
            this.divDelivery2.Visible = true;
            this.divDelivery3.Visible = true;
            strhideDive = "";
            //Refresh the Credit Card Values
            //Set the Delivery Information
            //Set the First Name
           // this.txtFirstname.Text = "";
            //Set the Last Name
           // this.txtLastName.Text = "";
            //Set the Email                
           // this.txtEmail.Text = "";
            //Set the Confirm Email here
           // this.txtCEmailAddress.Text = "";
            txtCVNumber.Text = "";
            //Set the Billing Information here
            //Set the Business Name here
            this.txtBUserName.Text = "";
            //Set the Business Address here
            this.txtBillingAddress.Text = "";
            //Set the Business City here
            this.txtBCity.Text = "";
            //Set the Business Province name here
            ddlProvince.SelectedIndex = 1;
            //Set the Business Postal Code here
            this.txtPostalCode.Text = "";
            //Set the Credit Card Number here
            this.txtCCNumber.Text = "";

            this.divlblCCN.Visible = true;
            this.divtxtCCN.Visible = true;
            ddlMonth.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            //Set the Month here
            // this.ddlMonth.SelectedValue = "01";
            //Set the Year here
            //this.ddlYear.SelectedValue = "2010";
            //Set the CVN # here
            this.txtCVNumber.Text = "";

            DataTable dtUser = null;
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"]!=null)
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
                txtFirstname.Text = dtUser.Rows[0]["firstName"].ToString();
                txtLastName.Text = dtUser.Rows[0]["lastName"].ToString();
                txtBUserName.Text = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
                txtEmail.Text = dtUser.Rows[0]["userName"].ToString();
                txtCEmailAddress.Text = dtUser.Rows[0]["userName"].ToString();
                txtCEmailAddress.ReadOnly = true;
                txtEmail.ReadOnly = true;
               // ViewState["userId"] = dtUser.Rows[0]["userId"];
                //divLogin.Visible = false;
                //divFacebook.Visible = false;

                //Fill the Credit Card Info GridView
                //if (dtUser.Rows.Count > 0)
                //{
                //    //Get & Set the Remained Refferred Balance
                //    getRemainedGainedBalByUserId(dtUser);

                //    ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();

                //    //Hide-Show the Credit Card Info in the function below
                //    GridDataBind(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                //}
            }

            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
        }
        catch (Exception ex)
        { }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if ((Convert.ToInt32(ddlMonth.SelectedValue) < DateTime.Now.Month) && (Convert.ToInt32(ddlYear.SelectedValue) <= DateTime.Now.Year))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please confirm your expirary date.";
                return;
            }
            hfPostalCode.Value = "0";
            GECEncryption objEnc = new GECEncryption();

            BLLUserCCInfo objCC = new BLLUserCCInfo();
            objCC.ccInfoID = Convert.ToInt64(this.hfCCInfoIdEdit.Value.ToString());
            objCC.ccInfoBAddress = txtBillingAddress.Text.Trim();
            objCC.ccInfoBCity = txtBCity.Text.Trim();
            objCC.ccInfoBPostalCode = txtPostalCode.Text.Trim();
            objCC.ccInfoBProvince = ddlProvince.SelectedValue.ToString();
            if (ViewState["userId"] != null && ViewState["userId"].ToString() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", txtCVNumber.Text.Trim());
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            //objCC.ccInfoNumber = this.hfCCN.Value.Trim();
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", txtCCNumber.Text.Trim());
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", txtBUserName.Text.Trim());
            objCC.ccInfoDEmail = this.txtEmail.Text.Trim();
            string[] strUserName = txtBUserName.Text.Split(' ');
            objCC.ccInfoDFirstName = strUserName[0].ToString();
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = strUserName[1].ToString();
            }
            objCC.updateUserCCInfoByID();

            //Hide the Credit Card Personal Info here
            this.divDelivery1.Visible = false;
            this.divDelivery2.Visible = false;
            this.divDelivery3.Visible = false;
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
            }
            lblMessage.Text = "Credit Card Info has been updated.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
        }
        catch (Exception ex)
        { }
    }

    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        //Show the Credit Card Personal Info here
        this.divDelivery1.Visible = false;
        this.divDelivery2.Visible = false;
        this.divDelivery3.Visible = false;
        hfPostalCode.Value = "0";
    }

    protected void CheckChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvCustomers.Rows)
        {
            RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");
            //Button myButton = (Button)gvr.FindControl("b1");
            if (rdb.Checked)
            {
                //Hide the Credit Card Personal Info here
                this.divDelivery1.Visible = false;
                this.divDelivery2.Visible = false;
                this.divDelivery3.Visible = false;
                this.hfMode.Value = "grid";
            }
        }

        if (divRefBal.Visible)
        {
            resetAmounts();
        }
        else
        {
            int int5Qty = Convert.ToInt32(ddl5Quntity.SelectedValue.ToString());
            int int10Qty = Convert.ToInt32(ddl10Quntity.SelectedValue.ToString());
            int int20Qty = Convert.ToInt32(ddl20Quntity.SelectedValue.ToString());
            int int50Qty = Convert.ToInt32(ddl50Quntity.SelectedValue.ToString());

            lbl5TotalPrice.Text = Convert.ToInt32(int5Qty * 5).ToString();
            lbl10TotalPrice.Text = Convert.ToInt32(int10Qty * 10).ToString();
            lbl20TotalPrice.Text = Convert.ToInt32(int20Qty * 20).ToString();
            lbl50TotalPrice.Text = Convert.ToInt32(int50Qty * 50).ToString();
            lblGrandTotal.Text = Convert.ToInt32((int5Qty * 5) + (int10Qty * 10) + (int20Qty * 20) + (int50Qty * 50)).ToString();
        }

    }


}