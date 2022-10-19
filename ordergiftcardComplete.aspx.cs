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
using System.IO;
using GecLibrary;
using System.Text;
using System.Net.Mail;
using System.Drawing;

public partial class ordergiftcardComplete : System.Web.UI.Page
{
   
    BLLDealOrders objOrder = new BLLDealOrders();
    BLLUser objUser = new BLLUser();   
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | You have purchase gift card successfully";
        #region Commest
        //        if (!IsPostBack)
//        {
//            lblEmailMessage.Visible = false;
//            if (Request.QueryString["oid"] != null && Request.QueryString["oid"].ToString() != "")
//            {

//                objOrder = new BLLDealOrders();
//                objOrder.dOrderID = Convert.ToInt64(Request.QueryString["oid"].ToString());
//                DataTable dtOrderinfo = objOrder.getDealOrderDetailByOrderID();

//                if (dtOrderinfo != null && dtOrderinfo.Rows.Count > 0)
//                {
//                    GECEncryption objEnc = new GECEncryption();
//                    ViewState["userId"] = dtOrderinfo.Rows[0]["userId"].ToString().Trim();
//                    string strUid =  Server.UrlEncode(objEnc.EncryptData("userID", dtOrderinfo.Rows[0]["userId"].ToString().Trim()));
//                    linkFacebook1.HRef = "http://www.facebook.com/sharer.php?u="
//                    + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
//                    + "/Default.aspx?uid=" + strUid + "&guid=" + Guid.NewGuid().ToString()));
//                    linkTweeter1.HRef = "http://twitter.com/share?url="
//                    + HttpUtility.UrlEncode(ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
//                    + "/Default.aspx?uid=" + strUid + "&guid=" + Guid.NewGuid().ToString()));
//                    Page.Title = dtOrderinfo.Rows[0]["title"].ToString().Trim();
//                    lblDealTitle.Text = dtOrderinfo.Rows[0]["title"].ToString().Trim();
//                    ViewState["dealId"] = dtOrderinfo.Rows[0]["dealId"].ToString().Trim();
//                    lblDealAmount.Text = Convert.ToDouble(Convert.ToDouble(dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim()) * Convert.ToInt32(dtOrderinfo.Rows[0]["personalQty"].ToString().Trim())).ToString("###.00");
//                    lblGiftPrice.Text = Convert.ToDouble(Convert.ToDouble(dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim()) * Convert.ToInt32(dtOrderinfo.Rows[0]["giftQty"].ToString().Trim())).ToString("###.00");
//                    lblSavePercentage.Text = dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim();
//                    double dSellPrice = Convert.ToDouble(dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim());
//                    double dActualPrice = Convert.ToDouble(dtOrderinfo.Rows[0]["valuePrice"].ToString().Trim());
//                    double dDiscount = 0;
//                    if (dSellPrice == 0)
//                    {
//                        dDiscount = 100;
//                    }
//                    else
//                    {
//                        dDiscount = (100 / dActualPrice) * (dActualPrice - dSellPrice);
//                    }
//                    lblSavePercentage.Text = Convert.ToInt32(dDiscount).ToString() + "%";
//                    lblOrderTotal.Text = Convert.ToDouble(Convert.ToDouble(dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim()) * Convert.ToInt32(dtOrderinfo.Rows[0]["Qty"].ToString().Trim())).ToString("###.00");
//                    string[] strDealImages = dtOrderinfo.Rows[0]["images"].ToString().Split(',');
//                    txtShareLink.Text = ResolveUrl(ConfigurationManager.AppSettings["YourSite"].ToString()
//                    + "/Default.aspx?uid=" + strUid+ "&guid=" + Guid.NewGuid().ToString());

//                    int i = 0;
//                    bool imageFound = false;
//                    if (strDealImages.Length > 0)
//                    {
//                        for (i = 0; i < strDealImages.Length; i++)
//                        {
//                            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strDealImages[i]))
//                            {
//                                imageFound = true;
//                                break;
//                            }
//                        }
//                    }
//                    if (imageFound)
//                    {
////                        watermark(AppDomain.CurrentDomain.BaseDirectory+"Images\\dealfood\\"+ dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strDealImages[i]
//                        string strNewFileName="123"+strDealImages[i];
//                        watermark.waterMark(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strDealImages[i], AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strNewFileName);
//                        dealImage.ImageUrl = "~/Images/dealfood/" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "/" + strNewFileName;
//                    }
//                    else
//                    {
//                        dealImage.ImageUrl = "~/Images/imageNotFound.jpg";
//                    }



//                    //lblTotalPrice.Text = (Convert.ToInt32(dtOrderinfo.Rows[0]["minOrdersPerUser"].ToString().Trim()) * Convert.ToInt32(dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim())).ToString();
//                    //lblGrandTotal.Text = (Convert.ToInt32(dtOrderinfo.Rows[0]["minOrdersPerUser"].ToString().Trim()) * Convert.ToInt32(dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim())).ToString();
//                    //double dSellPrice = Convert.ToDouble(dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim());
//                    //double dActualPrice = Convert.ToDouble(dtOrderinfo.Rows[0]["valuePrice"].ToString().Trim());
//                    //double dDiscount = (100 / dActualPrice) * dSellPrice;
//                    //string[] strDealImages = dtOrderinfo.Rows[0]["images"].ToString().Split(',');
//                    //int i = 0;
//                    //bool imageFound = false;
//                    //if (strDealImages.Length > 0)
//                    //{
//                    //    for (i = 0; i < strDealImages.Length; i++)
//                    //    {
//                    //        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + strDealImages[i]))
//                    //        {
//                    //            imageFound = true;
//                    //            break;
//                    //        }
//                    //    }
//                    //}
//                    //if (imageFound)
//                    //{
//                    //    dealImage.ImageUrl = "~/Images/dealfood/" + dtOrderinfo.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i];
//                    //}
//                    //else
//                    //{
//                    //    dealImage.ImageUrl = "~/Images/imageNotFound.jpg";
//                    //}
//                    //lblDealTitle.Text = dtOrderinfo.Rows[0]["title"].ToString().Trim();

//                    //lblDealDetail.Text = "<span style='color:#F99D1C;font-family: Arial;font-size: 19px;font-weight:bold'>Price:&nbsp;</span>";
//                    //lblDealDetail.Text += "<span style='color:black;font-family: Arial;font-size: 19px;font-weight:bold'>C$" + dtOrderinfo.Rows[0]["sellingPrice"].ToString().Trim() + "&nbsp;&nbsp;&nbsp;</span>";
//                    //lblDealDetail.Text += "<span style='color:#97C717;font-family: Arial;font-size: 19px;font-weight:bold'>|&nbsp;&nbsp;&nbsp;</span>";
//                    //lblDealDetail.Text += "<span style='color:#F99D1C;font-family: Arial;font-size: 19px;font-weight:bold'>You Save:&nbsp;</span>";
//                    //lblDealDetail.Text += "<span style='color:black;font-family: Arial;font-size: 19px;font-weight:bold'>" + Convert.ToInt32(dDiscount).ToString() + "%</span>";
//                    //DataTable dtUser = null;
//                    //if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null)
//                    //{
//                    //    if (Session["member"] != null)
//                    //    {
//                    //        dtUser = (DataTable)Session["member"];
//                    //    }
//                    //    else if (Session["restaurant"] != null)
//                    //    {
//                    //        dtUser = (DataTable)Session["restaurant"];
//                    //    }
//                    //    else if (Session["sale"] != null)
//                    //    {
//                    //        dtUser = (DataTable)Session["sale"];
//                    //    }
//                    //    txtFirstname.Text = dtUser.Rows[0]["firstName"].ToString();
//                    //    txtLastName.Text = dtUser.Rows[0]["lastName"].ToString();
//                    //    txtBUserName.Text = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
//                    //    txtEmail.Text = dtUser.Rows[0]["userName"].ToString();
//                    //    txtCEmailAddress.Text = dtUser.Rows[0]["userName"].ToString();

//                    //    ViewState["userId"] = dtUser.Rows[0]["userId"];
//                    //    divLogin.Visible = false;
//                    //    divFacebook.Visible = false;
                   
//                }
//            }
        //        }
        #endregion
    }

    #region Comment Functions may use in future
    //protected void btnJoinDiscuession_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        if (ViewState["dealId"] != null)
    //        {
    //            Response.Redirect("frmdiscussion.aspx?did=" + ViewState["dealId"].ToString(), true);
    //        }
    //        else
    //        {
    //            Response.Redirect("frmdiscussion.aspx", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
 
    //    }

    //}    
    ///*************************************************/
    //protected void btnEmailSubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        //watermark.waterMark("str");

    //        //return;
    //        if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null)
    //        {
    //            DataTable dtUser = null;
    //            if (Session["member"] != null)
    //            {
    //                dtUser = (DataTable)Session["member"];
    //            }
    //            else if (Session["restaurant"] != null)
    //            {
    //                dtUser = (DataTable)Session["restaurant"];
    //            }
    //            else if (Session["sale"] != null)
    //            {
    //                dtUser = (DataTable)Session["sale"];
    //            }
    //            GECEncryption objEnc = new GECEncryption();
    //            string strUid = objEnc.EncryptData("userID", dtUser.Rows[0]["userId"].ToString().Trim());
    //            string strUserName = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
    //            if (SendMailWithActiveCode(txtEmail.Text.Trim(), strUserName, strUid))
    //            {
    //                txtEmail.Text = "";
    //                lblEmailMessage.Visible = true;
    //                lblEmailMessage.Text = "Email has been sent successfully.";
    //            }
    //            else
    //            {
    //                lblEmailMessage.Visible = true;
    //                lblEmailMessage.Text = "Email sent failed.";
    //                lblEmailMessage.ForeColor = System.Drawing.Color.Red;
    //            }

    //        }
    //        else if (ViewState["userName"] != null)
    //        {
    //            GECEncryption objEnc = new GECEncryption();
    //            string strUid = objEnc.EncryptData("userID", ViewState["userId"].ToString().Trim());
    //            if (SendMailWithActiveCode(txtEmail.Text.Trim(), ViewState["userName"].ToString(), strUid))
    //            {
    //                txtEmail.Text = "";
    //                lblEmailMessage.Visible = true;
    //                lblEmailMessage.Text = "Email has been sent successfully.";
    //            }
    //            else
    //            {
    //                lblEmailMessage.Visible = true;
    //                lblEmailMessage.Text = "Email sent failed.";
    //                lblEmailMessage.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //        else
    //        {
    //            objUser = new BLLUser();
    //            objUser.userId = Convert.ToInt32(ViewState["userId"].ToString().Trim());
    //            DataTable dtUser = objUser.getUserByID();
    //            if (dtUser != null && dtUser.Rows.Count > 0)
    //            {
    //                ViewState["userName"] = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
    //                GECEncryption objEnc = new GECEncryption();
    //                string strUid = objEnc.EncryptData("userID", ViewState["userId"].ToString().Trim());
    //                if (SendMailWithActiveCode(txtEmail.Text.Trim(), ViewState["userName"].ToString(), strUid))
    //                {
    //                    txtEmail.Text = "";
    //                    lblEmailMessage.Visible = true;
    //                    lblEmailMessage.Text = "Email has been sent successfully.";
    //                }
    //                else
    //                {
    //                    lblEmailMessage.Visible = true;
    //                    lblEmailMessage.Text = "Email sent failed.";
    //                    lblEmailMessage.ForeColor = System.Drawing.Color.Red;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    { }
    //}
   
    //#region Send Email for Forgot Password
    //private bool SendMailWithActiveCode(string strEmailAddress, string strUserName, string strUserID)
    //{
    //    MailMessage message = new MailMessage();
    //    StringBuilder mailBody = new StringBuilder();
    //    try
    //    {
    //        strUserID = Server.UrlEncode(strUserID)+"&guid="+Guid.NewGuid().ToString();

    //        string toAddress = strEmailAddress;
    //        string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
    //        string Subject = ConfigurationManager.AppSettings["EmailSubjectForReferral"].ToString().Trim();
    //        message.IsBodyHtml = true;
    //        mailBody.Append("<html><head><title></title></head><body><h4>Dear " + strEmailAddress);
    //        mailBody.Append(",</h4>");
    //        mailBody.Append("<font size='3'>You friend "+ strUserName +" has just purchased a deal on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() +"/Default.aspx?uid=" +strUserID+"'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a>");
    //        mailBody.Append("<br>He/She wants that you also check this deal as its great.<br>Please click on the following link to go on our site. :<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Default.aspx?uid=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Default.aspx.aspx?uid=" + strUserID + "</a></font>");
    //        mailBody.Append("</font>");
    //        mailBody.Append("<table><tr><td>(If clicking on the link doesn't work, try copying and pasting it into your browser.)</td></tr>");
    //        mailBody.Append("</table>");            
    //        mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
    //        message.Body = mailBody.ToString();

    //        return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}
    //#endregion
    #endregion

}
