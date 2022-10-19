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
using System.Net.Mail;
using GecLibrary;
using System.Threading;
public partial class UserControls_Discussion_ctrlDiscussion : System.Web.UI.UserControl
{
    BLLDeals objDeal = new BLLDeals();
    BLLCities objCities = new BLLCities();
    ArrayList arrayIDs = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["did"] != null)
        //{
        //    btnBack.PostBackUrl = ConfigurationManager.AppSettings["YourSite"] + "/Default.aspx?sidedeal=" + Request.QueryString["did"].ToString().Trim();
        //    btnBuy.PostBackUrl = ConfigurationManager.AppSettings["YourSecureSite"] + "checkout.aspx?did=" + Request.QueryString["did"].ToString().Trim();
        //}
        
        if (!Page.IsPostBack)
        {
            try
            {
                string strCityID = "337";

                if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() != "")
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

                if (Request.QueryString["cName"] == null || Request.QueryString["cName"].ToString().Trim() == "")
                {
                    HttpCookie yourCity = Request.Cookies["yourCity"];

                    if (yourCity != null)
                    {
                        strCityID = yourCity.Values[0].ToString().Trim();
                    }
                    else
                    {
                        yourCity = new HttpCookie("yourCity");
                    }
                    yourCity.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(yourCity);
                    yourCity["yourCity"] = strCityID;
                }
                else if (Request.QueryString["cName"] != null && Request.QueryString["cName"].ToString().Trim() != "")
                {
                    string[] strCName;
                    if (Request.QueryString["cName"].ToString().Trim().Contains('_'))
                    {
                        strCName = Request.QueryString["cName"].ToString().Trim().Split('_');
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
                    DataTable dtCityDetail = objCities.getCityDetailByName();
                    if (dtCityDetail != null && dtCityDetail.Rows.Count > 0)
                    {
                        HttpCookie citycookie = Request.Cookies["yourCity"];
                        if (citycookie == null)
                        {
                            citycookie = new HttpCookie("yourCity");
                        }
                        citycookie.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(citycookie);
                        citycookie["yourCity"] = dtCityDetail.Rows[0]["cityId"].ToString();
                        strCityID = dtCityDetail.Rows[0]["cityId"].ToString();
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
                        citycookie["yourCity"] = "0";
                        strCityID = "0";
                    }
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
                    citycookie["yourCity"] = strCityID;
                }

                objCities.cityId = Convert.ToInt32(strCityID);

                DataTable dtCity = objCities.getCityByCityId();

                if (dtCity.Rows.Count > 0)
                {
                    objDeal.CreatedDate = Misc.getResturantLocalTime(Convert.ToInt32(dtCity.Rows[0]["provinceId"].ToString()));
                }

                DataTable dtDeal = null;
                string strDealID = "";
                
                if (objDeal.DealId == 0)
                {
                    if (Request.QueryString["sidedeal"] != null && Request.QueryString["sidedeal"].ToString().Trim() != "")
                    {                        
                        strDealID = Request.QueryString["sidedeal"].ToString().Trim();                                                
                    }
                    else
                    {
                        objDeal.cityId = Convert.ToInt32(strCityID);
                        strDealID = objDeal.getCurrentDealByCityID().Rows[0]["dealid"].ToString().Trim();
                    }
                }
                else
                {
                    strDealID = objDeal.DealId.ToString();
                }


                int iDealId = int.Parse(strDealID);

                this.hfDealId.Value = strDealID;

                //Get And Set Posts By Deal Id
                CheckUserLoginInOrNot();
                GetAndSetPostsByDealId(iDealId, false, "","0");               

            }
            catch (Exception ex)
            {
                // Response.Redirect(ResolveUrl("Default.aspx"), false);
            }
        }
    }

    #region "Check user Is Logged In or not"

    private void CheckUserLoginInOrNot()
    {
        try
        {
            if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"]==null)
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
            AddNewPost(iDealId, iUserId,0, HtmlRemoval.StripTagsRegexCompiled(txtComment.Text.Trim()));

            //Get All the Posts here By Deal Id
            GetAndSetPostsByDealId(iDealId,true,HtmlRemoval.StripTagsRegexCompiled(txtComment.Text.Trim()),"0");

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
        DataTable dtUser =null;

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

            //BLLKarmaPoints bllKarma = new BLLKarmaPoints();
            //bllKarma.userId = iUserId;
            //bllKarma.karmaPoints = 1;
            //bllKarma.karmaPointsType = "Comment";
            //bllKarma.createdBy = iUserId;
            //bllKarma.createdDate = DateTime.Now;
            //bllKarma.createKarmaPoints();
            
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


    private bool SendMailForNewComment(string ToEmail, string ToName, string FromName, string strDealTitle,string strMessage)
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
                if (ViewState["userID"] == null || ViewState["userID"].ToString().Trim() == "" && imgCommentReply!=null)
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

                    int index= 0;
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
                    AddNewPost(iDealId, iUserId, Convert.ToInt64(e.CommandArgument),txtSubComment.Text);
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
}