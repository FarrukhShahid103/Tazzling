using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using GecLibrary;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;

public partial class confirmDeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((Request.QueryString["u"] != null) && (Request.QueryString["dc"] != null) && (Request.QueryString["sid"] != null) && (Request.QueryString["uName"] != null))
            {
                string strUsername = DecryptUserName(Request.QueryString["u"].ToString().Trim().Replace(' ', '+'));

                string strDealCode = Request.QueryString["dc"].ToString().Trim().Replace(' ', '+');

                string strSenderEmailID = DecryptUserName(Request.QueryString["sid"].ToString().Trim().Replace(' ', '+'));
                string strUname = Request.QueryString["uName"].ToString().Trim();
                //Check User Exists or not
                SignUpNewUser(strUsername, strDealCode, strSenderEmailID, strUname);
            }
            else
            {
                //If User Already exists
                Response.Redirect("Default.aspx", false);
            }
        }
        catch (Exception ex)
        { }
    }

    private DataTable ChkDealExistsIfExistsThenUpdate(string strDealCode, int iUserID)
    {
        try
        {
            BLLDealOrderDetail objBLLDealOrderDetail = new BLLDealOrderDetail();

            objBLLDealOrderDetail.dealOrderCode = strDealCode;

            objBLLDealOrderDetail.isGiftCapturedId = iUserID;

            return objBLLDealOrderDetail.updateDealOrderDetailCapIdByDealCode();
        }
        catch (Exception ex)
        { }
        return null;
    }

    protected string DecryptDealCode(string strDealCode)
    {
        if (strDealCode.Trim() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.DecryptData("deatailOrder", strDealCode);
        }
        return "";
    }

    protected string DecryptUserName(string strUserName)
    {
        if (strUserName.Trim() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.DecryptData("userName", strUserName);
        }
        return "";
    }

    private void SignUpNewUser(string strUserName, string strDealCode, string strSenderEmailID, string strUname)
    {
        try
        {
            //For the functionality of Div --Show/Hide
            BLLUser obj = new BLLUser();

            DataTable dtUser = (DataTable)Session["user"];

            obj.userName = strUserName;

            obj.email = strUserName;

            obj.referralId = "";
            string[] strArryName = strUname.Split(' ');
            if (!obj.getUserByUserName())
            {
                try
                {
                    obj.firstName = strArryName[0].ToString();
                    obj.lastName = strArryName[1].ToString();
                }
                catch (Exception ex)
                { }
                obj.userName = strUserName;
                obj.userPassword = GetPassword();
                obj.email = strUserName;

                //For Customer 
                obj.userTypeID = 4;
                obj.isActive = true;
                                
                obj.countryId = 2;
                //if (hfProvince.Value != "0")
                //{
                obj.provinceId = 3;
                //}
                obj.friendsReferralId = GetSenderUserIDByEmail(strSenderEmailID);
                obj.howYouKnowUs = "";
                obj.ipAddress = Request.UserHostAddress.ToString();
                long result = obj.createUser();

                if (result != 0)
                {
                    SendMailWithUserInfo(strUserName, obj.userPassword, strUserName);

                    obj.userId = Convert.ToInt32(result);

                    dtUser = obj.getUserByID();

                    obj.userName = dtUser.Rows[0]["userName"].ToString();

                    obj.userPassword = dtUser.Rows[0]["userPassword"].ToString();

                    dtUser = obj.validateUserNamePassword();

                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        if (dtUser.Rows[0]["userTypeID"].ToString() == "4")
                        {
                            Session["member"] = dtUser;
                            Session.Remove("sale");
                            Session.Remove("restaurant");
                        }
                        Response.Redirect("member_MyTastygo.aspx", false);
                    }

                    //Update Deal Order Detail -- IsCapurtedId 
                    DataTable dtDetail = ChkDealExistsIfExistsThenUpdate(strDealCode, obj.userId);
                    if (dtDetail != null && dtDetail.Rows.Count > 0 && dtDetail.Rows[0][0].ToString().Trim()!="0")
                    {
                        Misc.createPDFForGift(dtDetail.Rows[0]["detailID"].ToString(), dtDetail.Rows[0]["dOrderID"].ToString(), strUname);
                    }

                }
                else
                {
                    //If user is not created
                    Response.Redirect("Default.aspx", false);
                }
            }
            else
            {
                //If User Already exists
                BLLUser objBLLUser=new BLLUser();
                
                objBLLUser.userName = strUserName;

                int iUserId = objBLLUser.getUserIdByUserName();

                //Update Deal Order Detail -- IsCapurtedId 
                DataTable dtDetail = ChkDealExistsIfExistsThenUpdate(strDealCode, iUserId);
                if (dtDetail != null && dtDetail.Rows.Count > 0 && dtDetail.Rows[0][0].ToString().Trim() != "0")
                {
                    Misc.createPDFForGift(dtDetail.Rows[0]["detailID"].ToString(), dtDetail.Rows[0]["dOrderID"].ToString(), strUname);
                }

                //If User Already exists
                Response.Redirect("Default.aspx", false);
            }
        }
        catch (Exception ex)
        {}
    }

    private string GetSenderUserIDByEmail(string strEmail)
    {
        string strUserId = "";

        try
        {
            BLLUser objUser = new BLLUser();
            objUser.email = strEmail;

            DataTable dtUser = objUser.getUserDetailByEmail();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                strUserId = dtUser.Rows[0]["userId"].ToString();
            }
        }
        catch (Exception ex)
        { }

        return strUserId;
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

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    #region Send Email for Forgot Password

    private void SendMailWithUserInfo(string strEmailAddress, string strPassword, string strUserName)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
        try
        {
            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailNewAccountCredentials"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");
            mailBody.Append("<h4>Dear " + strUserName);
            mailBody.Append(",</h4>");
            mailBody.Append("<font size='3'>Your account has been recently created on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></font>");
            mailBody.Append("<table><tr><td>Your account detail is following</td></tr>");
            mailBody.Append("<tr><td>User Name :  " + strEmailAddress + "</td></tr>");
            mailBody.Append("<tr><td>Password :" + strPassword.ToString().Trim() + "</td></tr></table>");
            mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();
            //mailBody.Append("<html><head><title></title></head><body><h4>Dear User");
            //mailBody.Append(",</h4>");
            //mailBody.Append("<font size='3'>You account has been created on tastygo.");            
            //mailBody.Append("</font><br />");
            //mailBody.Append("<table>");
            //mailBody.Append("<tr><td>Your account detail is following</td></tr>");
            //mailBody.Append("<tr><td>User Name :  " + strUserName + "</td></tr>");
            //mailBody.Append("<tr><td>Password :" + strPassword.ToString().Trim() + "</td></tr></table>");
            //mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();

            Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {}
    }

    #endregion
}