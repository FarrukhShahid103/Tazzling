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
using SQLHelper;
using createsend_dotnet;
using System.Collections.Generic;
using GecLibrary;

public partial class Default5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //IEnumerable<BasicClient> clients = General.Clients();




            //foreach (BasicClient c in clients)
            //{

            //    Console.WriteLine(string.Format("ID: {0}; Name: {1}", c.ClientID, c.Name));
            //}
            //Console.ReadLine();
            //try
            //{
            //    Subscriber subsc1 = new Subscriber("fb74a3b3fa0adad757a3ea8dcb10c85d");
            //    bool str = subsc1.Unsubscribe("sher.azam@redsignal.biz");
            //}
            //catch (Exception ex)
            //{ }
            //try
            //{
            //    Subscriber subsc = new Subscriber("fb74a3b3fa0adad757a3ea8dcb10c85d");
            //    string strin = subsc.Add("waqas@redsignal.biz", "Waqas", null, false);
            //}
            //catch (Exception ex)
            //{ }

            //try
            //{
            //    Subscriber subsc2 = new Subscriber("fb74a3b3fa0adad757a3ea8dcb10c85d");
            //    SubscriberDetail detail = subsc2.Get("sher.azam@redsignal.biz");                
            //}
            //catch (Exception ex)
            //{ }

            //try
            //{
            //    Subscriber subsc2 = new Subscriber("fb74a3b3fa0adad757a3ea8dcb10c85d");
            //    IEnumerable<HistoryItem> history = subsc2.GetHistory("sher.azam@redsignal.biz");
            //    foreach (HistoryItem c in history)
            //    {

            //       // Console.WriteLine(string.Format("ID: {0}; Name: {1}", c..ClientID, c.Name));
            //    }
            //    Console.ReadLine();
            //}
            //catch (Exception ex)
            //{ }
            //string strQuery = "SELECT [dealOrderDetail].[detailID] , [dealOrderDetail].[dealOrderCode] FROM  [dealOrders]";
            //strQuery += " inner join deals on (deals.dealId = dealOrders.dealId) ";
            //strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            //strQuery += " where  dealOrders.dealId =425";

            //DataTable dtExisitingOrders = Misc.search(strQuery);
            //if (dtExisitingOrders != null && dtExisitingOrders.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtExisitingOrders.Rows.Count; i++)
            //    {
            //        BLLSampleVouchers objSampleVouchers = new BLLSampleVouchers();
            //        objSampleVouchers.dealId = 424;
            //        DataTable dtUnUsedVoucher = objSampleVouchers.getTop1UnusedSampleVouchersByDealID();
            //        if (dtUnUsedVoucher != null && dtUnUsedVoucher.Rows.Count > 0)
            //        {
            //            int result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, "update dealOrderDetail set dealOrderCode='" + dtUnUsedVoucher.Rows[0]["dealOrderCode"].ToString().Trim() + "' where detailID=" + dtExisitingOrders.Rows[i]["detailID"].ToString().Trim());
            //            objSampleVouchers.detailID = Convert.ToInt64(dtExisitingOrders.Rows[i]["detailID"].ToString().Trim());
            //            objSampleVouchers.isUsed = true;
            //            objSampleVouchers.vID = Convert.ToInt32(dtUnUsedVoucher.Rows[0]["vID"].ToString().Trim());
            //            objSampleVouchers.updateSampleVouchers();
            //        }
                    
            //    }
            //}

            DataTable dtSampleVouchers = Misc.search("select * from sampleVouchers where dealId=550 and isUsed=0 order by vID asc");
            GECEncryption objEnc = new GECEncryption();
            for (int i = 0; i < dtSampleVouchers.Rows.Count; i++)
            {
                dtSampleVouchers.Rows[i]["dealOrderCode"] = objEnc.DecryptData("deatailOrder", dtSampleVouchers.Rows[i]["dealOrderCode"].ToString().Trim());
            }
            GridView gv = new GridView();
            gv.DataSource = dtSampleVouchers;
            gv.DataBind();
            string attachment = "attachment; filename=SampleVoucehrs.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            gv.RenderControl(htextw);
            Response.Write(stw.ToString());
            Response.End();

            //DataTable dtDeals = Misc.search("select dealId from deals order by dealId asc");
            //if (dtDeals != null && dtDeals.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtDeals.Rows.Count; i++)
            //    {
            //        DataTable dtCityDeals = Misc.search("select dealStartTimeC,dealEndTimeC from dealCity where dealid=" + dtDeals.Rows[i]["dealId"].ToString().Trim());
            //        if (dtCityDeals != null && dtCityDeals.Rows.Count > 0)
            //        {
            //            SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, "update deals set dealStartTime='" + dtCityDeals.Rows[0]["dealStartTimeC"].ToString().Trim() + "',dealEndTime='" + dtCityDeals.Rows[0]["dealEndTimeC"].ToString().Trim() + "'  where dealId=" + dtDeals.Rows[i]["dealId"].ToString().Trim());
            //        }
            //    }
            //}
            //GridView gv = new GridView();
            //gv.DataSource = dtSampleVouchers;
            //gv.DataBind();
            //string attachment = "attachment; filename=SampleVoucehrs.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/ms-excel";
            //StringWriter stw = new StringWriter();
            //HtmlTextWriter htextw = new HtmlTextWriter(stw);
            //gv.RenderControl(htextw);
            //Response.Write(stw.ToString());
            //Response.End();


            //PaymentBack();
            //Payment();

            //givereferalAmount("148");

            //if (Request.QueryString["did"] != null
            //    && Request.QueryString["did"].ToString().Trim() != ""
            //    && Request.QueryString["cid"] != null
            //    && Request.QueryString["cid"].ToString().Trim() != "")
            //if (Request.QueryString["did"] != null
            // && Request.QueryString["did"].ToString().Trim() != "")
            //{
            //    DownloadExcelFile();
            //}
        }
    }

    private void DownloadExcelFile()
    {
        string strQuery = "SELECT ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber' ,[dealOrders].[dOrderID] ,dealOrders.dealId ,";
        strQuery += " rtrim(userInfo.firstname) +' ' + (case when (len(rtrim(userInfo.lastName))>1) then upper(substring(rtrim(userInfo.lastName),1,1))";
        strQuery += " else '' end )as 'Name' ,orderNo ,dealOrders.createdDate ,[dealOrders].[status] ,";
        //strQuery += " (ccInfoBAddress+', '+ccInfoBCity+', '+ccInfoBProvince+' '+ccInfoBPostalCode) as 'Address',";
        strQuery += " (psgTranNo) as 'Address',";
        strQuery += " userInfo.userName as 'voucherSecurityCode' ,[dealOrderDetail].[detailID] ,[dealOrderDetail].[receiverEmail] ,";
        strQuery += " [dealOrderDetail].[isRedeemed] ,[dealOrderDetail].[redeemedDate] ,[dealOrderDetail].[dealOrderCode] ,";
        strQuery += " [dealOrderDetail].[isGift] ,[dealOrderDetail].[markUsed] FROM  [dealOrders] inner join deals on (deals.dealId = dealOrders.dealId)";
        strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId)";
        strQuery += " inner join userCCInfo on (userCCInfo.ccInfoID = dealOrders.ccInfoID)";
        strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
        strQuery += " where (dealOrders.dealId =" + Request.QueryString["did"].ToString() + ")";

        DataTable dtSearch = Misc.search(strQuery);
        if (dtSearch != null && dtSearch.Rows.Count > 0)
        {
            GECEncryption objEnc = new GECEncryption();
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                dtSearch.Rows[i]["dealOrderCode"] = objEnc.DecryptData("deatailOrder", dtSearch.Rows[i]["dealOrderCode"].ToString());
            }
            DataView dv;
            dv = new DataView(dtSearch);
           // dv.Sort = "Address ASC";
            GridView1.DataSource = dv;
            GridView1.DataBind();


            string attachment = "attachment; filename=dealcodes.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            GridView1.RenderControl(htextw);
            Response.Write(stw.ToString());
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    
    #region Give Payment Function
    private void givereferalAmount(string strDealid)
    {
        ArrayList arrl = new ArrayList();
        string strQuery = "select username,firstname, LastName,dorderid,dealOrders.status,Qty, dealOrders.userId from dealOrders inner join userInfo on userInfo.userId=dealOrders.userId where dealId=" + strDealid + " and status='Successful'";
        DataTable dtuserList = Misc.search(strQuery);
        int a = 0;
        if (dtuserList != null && dtuserList.Rows.Count > 0)
        {
            for (int i = 0; i < dtuserList.Rows.Count; i++)
            {
                if (!arrl.Contains(dtuserList.Rows[i]["userId"].ToString().Trim()))
                {
                    arrl.Add(dtuserList.Rows[i]["userId"].ToString().Trim());
                    AddTastyGoCreditToReffer(1, 0, Convert.ToInt64(dtuserList.Rows[i]["userId"].ToString().Trim()));
                    SendEmail(dtuserList.Rows[i]["userId"].ToString().Trim(), "5");

                   // SendNotificationEmail(dtuserList.Rows[i]["firstname"].ToString().Trim() + " " + dtuserList.Rows[i]["LastName"].ToString().Trim(), dtuserList.Rows[i]["username"].ToString().Trim());
                    Thread.Sleep(6000);
                    a++;
                }
            }
        }

        lblTranscationNumber.Visible = true;
        lblTranscationNumber.Text = "$5 given to " + a.ToString();


    }

    private void AddTastyGoCreditToReffer(int fromID, long OrderID, long lRefId)
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

            objUsedCard.createMemberUseableGiftCard();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        
    }

    protected void SendNotificationEmail(string strUserName, string strEmail)
    {
        try
        {

            System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
            string toAddress = strEmail;
            string fromAddress = "info@tazzling.com";
            string Subject = "BC Dance Zumba Class Changes";

            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>BC Dance Zumba Class Changes</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            mailBody.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            mailBody.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            mailBody.Append("<strong>Dear " + strUserName + ",</strong></div>");
            mailBody.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial; color: #000000; font-size: 18px;line-height: 1.3em;'><strong>There has been a change of location on Thursday’s class. Please see the email in the bottom. </strong></div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px;line-height: 1.4em;'>If you have any questions, you may contact BC dance at 604-685-2846, or you can contact Tastygo at support@tazzling.com (604)-295-1777</div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial; color: #333333; font-size: 14px;line-height: 1.4em; font-weight: bold;'>Hi everyone! This is BC Dance Studio</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial; color: #333333; font-size: 14px;line-height: 1.4em;'>Yesterday we received news that Africana dance studio in Tinseltown is being sold to a new owner; It will take 2 to 3 weeks before the new owners will tell us if we can get back in this location. In the meantime, we are moving the Thursday Zumba class to Rhodes College (information as follows):</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial; color: #333333; font-size: 14px;line-height: 1.4em; font-weight: bold;'>Thursdays (Starting tomorrow)</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Zumba (all levels) 6:30 - 7:30 Pm<br>Location: Rhodes College 280 - 1125 Howe St @ Helmeken (DT) Buzzer at Helmeken St<br>Unfortunately, this studio is smaller than tinseltown, so \"space is limited\". Please arrive early to ensure that we can accommodate as many people as possible in this new location.</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>• I just purchase a commercial fan to have the air circulating in this studio!</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>To serve you better, we are also adding an extra Zumba class on Sundays at Robson market beginning September 18th from 2:30pm-3:20pm and will be beginning another Zumba Class in West Vancouver on Saturday mornings starting October 1st. Please see class details below:</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; border-top: 1px solid #eeeeee;font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial; color: #333333; font-size: 14px;line-height: 1.4em; font-weight: bold;'>Saturdays:</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Class: Zumba (all levels) 10:00 - 11:00 Am (This class will <b>start on October 1</b>)<br>Location: West Van. Masonic Hall<br>1763 Bellevue Avenue @ 17 St<br>West Vancouver<br>Lots of free parking</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; border-top: 1px solid #eeeeee;font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial; color: #333333; font-size: 14px;line-height: 1.4em; font-weight: bold;'>Sundays:</div>");
            mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Class: Zumba (all levels) 11:45am - 12: 30pm<br>Class: Zumba (all levels) 2:30 – 3:20 <b>Starting September 18 (new)</b><br>* I recently purchased a commercial fan to have the air circulating in this studio!<br>Robson Market (At the Yoga studio) 2Nd floor # 208 - 1610 Robson Street.<br>(2 hrs free underground parking)</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; border-top: 1px solid #eeeeee;font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            mailBody.Append("<div style='margin: 0px 0px 0px 20px; font-family: Arial; color: #333333; font-size: 11px;line-height: 1.4em;'>Please use your coupon as drop in pass in any of these 3 locations<br>\"Classes are on going for any coupon holder\"<br>For more information, please visit <a href='http://www.bcdance.com'>www.bcdance.com</a> or call 604-685-2846</div>");
            mailBody.Append("<div style='margin: 0px 0px 15px 20px; font-family: Arial; color: #333333; font-size: 14px;line-height: 1.4em; font-weight: bold;'>We apologize for the inconvenience and understand that this a major change. We are sorry that we were unable to notify you until now but unfortunately we were also notified last minute. We are trying to do all we can to make this transition easier and to try to continue the class temporarily in this new location and will let you know of any further changes to come.<br><br>Thank you so much for understanding!</div>");                      
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");
            mailBody.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            mailBody.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            mailBody.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            mailBody.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            mailBody.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
            Misc.SendEmail(toAddress, "", "", fromAddress, Subject, mailBody.ToString());
        }
        catch (Exception ex)
        {
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
        }
    }

    #endregion

    private static void PaymentBack()
    {
        try
        {
            //System.Net.HttpWebRequest myWebReqeust = (HttpWebRequest)System.Net.WebRequest.Create("https://secure.psigate.com:7934/Messenger/XMLMessenger");
            //myWebReqeust.Timeout = 10000;
            //StringBuilder myParams = new StringBuilder();
            //myParams.Append("<?xml version='1.0' encoding='UTF-8'?>");
            //myParams.Append("<Order>");
            //myParams.Append("<StoreID>tastygoCAD</StoreID>");
            //myParams.Append("<Passphrase>Cheng1984</Passphrase>");
            //myParams.Append("<Subtotal></Subtotal>");
            //myParams.Append("<PaymentType>CC</PaymentType>");
            //myParams.Append("<CardAction>3</CardAction>");
            //myParams.Append("<OrderID></OrderID>");
            //myParams.Append("</Order>");
            //myWebReqeust.Method = "POST";
            //myWebReqeust.ContentLength = myParams.ToString().Length;
            //myWebReqeust.ContentType = "application/x-www-form-urlencoded";
            //myWebReqeust.KeepAlive = false;
            //System.IO.StreamWriter myWriter;
            //myWriter = new System.IO.StreamWriter(myWebReqeust.GetRequestStream());
            //myWriter.Write(myParams.ToString());
            //myWriter.Close();

            ////Get the Tasty Credits of the Order if order have           
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

            //        string strOrderStatus = "";
            //        if (root.ChildNodes[3].InnerText.ToString().ToLower() == "approved")
            //        {
            //            strOrderStatus = "Successful";

            //            //AddTastyGoCreditToReffer(iUserID, OrderID);
            //            //Misc.createPDF(OrderID.ToString());
            //        }
            //        else if (root.ChildNodes[3].InnerText.ToString().ToLower() == "declined" || root.ChildNodes[3].InnerText.ToString().ToLower() == "error")
            //        {
            //            strOrderStatus = "Declined";

            //            //If Deal Order Contains the tasty Credits                       
            //        }
            //        else
            //        {
            //            strOrderStatus = root.ChildNodes[3].InnerText.ToString();
            //        }
            //        string strTranscationNumber = root.ChildNodes[1].InnerText.ToString();
            //        string Error = root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
            //        //  string strApproved = root.ChildNodes[3].InnerText.ToString();
            //        // string strOrderID = root.ChildNodes[1].InnerText.ToString();
            //    }
            //    myStreamReader.Close();
            //}
            //catch (Exception ex)
            //{

            //}
        }
        catch (Exception ex)
        {

        }
    }
    
    private static void Payment()
    {
        try
        {
            System.Net.HttpWebRequest myWebReqeust = (HttpWebRequest)System.Net.WebRequest.Create("https://secure.psigate.com:7934/Messenger/XMLMessenger");
            myWebReqeust.Timeout = 10000;
            StringBuilder myParams = new StringBuilder();
            myParams.Append("<?xml version='1.0' encoding='UTF-8'?>");
            myParams.Append("<Order>");
            myParams.Append("<StoreID>tastygoCAD</StoreID>");
            myParams.Append("<Passphrase>Cheng1984</Passphrase>");
            myParams.Append("<Bname>shaffina a hirji</Bname>");
            myParams.Append("<Bcompany></Bcompany>");
            myParams.Append("<Baddress1>102 3183 esmond ave</Baddress1>");
            myParams.Append("<Baddress2></Baddress2>");
            myParams.Append("<Bcity>burnaby</Bcity>");
            myParams.Append("<Bprovince>BC</Bprovince>");
            myParams.Append("<Bpostalcode>v5g-4v6</Bpostalcode>");
            myParams.Append("<Bcountry>Canada</Bcountry>");
            myParams.Append("<Phone></Phone>");
            myParams.Append("<Fax></Fax>");
            myParams.Append("<Email>lbaker_21@hotmail.com</Email>");
            myParams.Append("<Comments></Comments>");
            myParams.Append("<Tax1>0.00</Tax1>");
            myParams.Append("<ShippingTotal>0.00</ShippingTotal>");
            //myParams.Append("<Subtotal>" + (Convert.ToDouble(dtorders.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtorders.Rows[0]["Qty"].ToString())).ToString() + "</Subtotal>");
            myParams.Append("<Subtotal>34</Subtotal>");
            myParams.Append("<PaymentType>CC</PaymentType>");
            myParams.Append("<CardAction>0</CardAction>");
            myParams.Append("<CardNumber></CardNumber>");

            myParams.Append("<CardExpMonth>09</CardExpMonth>");
            myParams.Append("<CardExpYear>13</CardExpYear>");

            myParams.Append("<CardIDNumber></CardIDNumber>");
            myParams.Append("</Order>");
            myWebReqeust.Method = "POST";
            myWebReqeust.ContentLength = myParams.ToString().Length;
            myWebReqeust.ContentType = "application/x-www-form-urlencoded";
            myWebReqeust.KeepAlive = false;
            System.IO.StreamWriter myWriter;
            myWriter = new System.IO.StreamWriter(myWebReqeust.GetRequestStream());
            myWriter.Write(myParams.ToString());
            myWriter.Close();

            //Get the Tasty Credits of the Order if order have           
            try
            {
                System.Net.WebResponse myWebResponse = myWebReqeust.GetResponse();
                StreamReader myStreamReader = new StreamReader(myWebResponse.GetResponseStream());
                string myHTML = myStreamReader.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(myHTML);
                XmlNode root = doc.DocumentElement;
                if (root.HasChildNodes)
                {

                    string strOrderStatus = "";
                    if (root.ChildNodes[3].InnerText.ToString().ToLower() == "approved")
                    {
                        strOrderStatus = "Successful";

                        //AddTastyGoCreditToReffer(iUserID, OrderID);
                        //Misc.createPDF(OrderID.ToString());
                    }
                    else if (root.ChildNodes[3].InnerText.ToString().ToLower() == "declined" || root.ChildNodes[3].InnerText.ToString().ToLower() == "error")
                    {
                        strOrderStatus = "Declined";

                        //If Deal Order Contains the tasty Credits                       
                    }
                    else
                    {
                        strOrderStatus = root.ChildNodes[3].InnerText.ToString();
                    }
                    string strTranscationNumber = root.ChildNodes[1].InnerText.ToString();
                    string Error = root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
                    //  string strApproved = root.ChildNodes[3].InnerText.ToString();
                    // string strOrderID = root.ChildNodes[1].InnerText.ToString();
                }
                myStreamReader.Close();
            }
            catch (Exception ex)
            {

            }
        }
        catch (Exception ex)
        {

        }
    }
    
    #region Code to support Download Things

    public bool displayPrevious = false;
    public bool displayNext = true;

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";     

    protected string getDealCode(object objCode)
    {
        if (objCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return "# " + objEnc.DecryptData("deatailOrder", objCode.ToString());
        }
        return "";
    }
    
    protected string getDealStatus(object objRedeem)
    {
        if (objRedeem.ToString() != "")
        {
            if (Convert.ToBoolean(objRedeem.ToString()))
            {
                return "Yes";
            }
            else
                return "No";
        }
        return "No";
    }

    protected bool getDealStatus2(object objRedeem)
    {
        if (objRedeem.ToString() != "")
        {
            if (Convert.ToBoolean(objRedeem.ToString()))
            {
                return false;
            }
            else
                return true;
        }
        return true;
    }
    
    protected string getDealDate(object objDate)
    {
        try
        {
            if (objDate.ToString() != "")
            {
                DateTime dt = Convert.ToDateTime(objDate);
                return dt.ToString("MM-dd-yyyy H.mm tt");
            }
            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    
    protected string getDealCodeSrch(string strDealCode)
    {
        if (strDealCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.EncryptData("deatailOrder", strDealCode);
        }
        return "";
    }
    
    private DataTable SortDataTable(DataTable dt)
    {
        try
        {
            GECEncryption objEnc = new GECEncryption();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["dealOrderCode"] = objEnc.EncryptData("deatailOrder", dt.Rows[i]["dealOrderCode"].ToString());
            }
            return dt;

        }
        catch (Exception ex)
        {         
        }

        return dt;
    }
    
    private int GetDealIdFromApplication()
    {
        int iUserid = 0;

        try
        {
            if (Application["DealId"] != null)
            {
                iUserid = int.Parse(Application["DealId"].ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return iUserid;
    }

    #endregion

}