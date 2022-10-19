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

public partial class member_gift_card : System.Web.UI.Page
{

    BLLGiftCard obj = new BLLGiftCard();
    BLLAdminGiftCard objAdminGiftCard = new BLLAdminGiftCard();
    BLLUser objUser = new BLLUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Gift Card";

            if (!IsPostBack)
            {
                DataTable dtUser = null;
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
                if (dtUser != null)
                {
                    ViewState["userID"] = dtUser.Rows[0]["userId"].ToString();
                    bindGrid(Convert.ToInt64(ViewState["userID"]));
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red; 
        }
    }

    #region Function to Bind Grid
    protected void bindGrid(long UserID)
    {
        try
        {
            DataTable dtGiftCards;
            DataView dv;
            obj.createdBy = UserID;
            dtGiftCards = obj.getGiftCardByUserID();
            dv = new DataView(dtGiftCards);
            gvGiftCards.PageSize = Misc.clientPageSize;
            ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtGiftCards.Rows.Count) / Convert.ToDouble(gvGiftCards.PageSize)).ToString();
            if (dtGiftCards != null && dtGiftCards.Rows.Count > 0)
            {
                gvGiftCards.DataSource = dv;
                gvGiftCards.DataBind();
            }
            else
            {
                gvGiftCards.DataSource = null;
                gvGiftCards.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red; 
        }
    }
    #endregion


    protected string GetGiftCardImage(string giftCardAmount)
    {
        DataTable dtGiftCard = null;        
        if (giftCardAmount != "")
        {
            objAdminGiftCard.cardAmount = float.Parse(giftCardAmount);
            dtGiftCard = objAdminGiftCard.getAdminGiftCardByPrice();
            if (dtGiftCard != null && dtGiftCard.Rows.Count > 0)
            {
                return "<img src='images/giftcard/" + dtGiftCard.Rows[0]["cardImage"] + "' width=\"82\" height=\"50\"/>";
            }         
        }
        return "";
    }

    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    protected string GetExpirationDateString(object expirationDate)
    {
        if (expirationDate.ToString()!="")
        {
            DateTime dt = Convert.ToDateTime(expirationDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    protected bool getCardStatus(string createdBy, string takenBy)
    {
        try
        {
            DataTable dtUser = null;
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
            string username = string.Empty;
            if (createdBy == dtUser.Rows[0]["userId"].ToString())
            {
                if (takenBy == "")
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected string GetCardExplain(string createdBy, string takenBy)
    {
        try
        {
            DataTable dtUser = null;
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
            string explain = "";
            string username = string.Empty;
            if (createdBy == dtUser.Rows[0]["userId"].ToString())
            {
                if (takenBy == "")
                    explain = "Not Been Redeemed";
                else
                {
                    DataTable dtTakenUser = null;
                    objUser.userId = Convert.ToInt32(takenBy);
                    dtTakenUser = objUser.getUserByID();
                    if (dtTakenUser != null && dtTakenUser.Rows.Count > 0)
                    {
                      //  username = dtTakenUser.Rows[0]["firstName"].ToString() + " " + dtTakenUser.Rows[0]["lastName"].ToString();
                        explain = "Redeemed by <br>"+ dtTakenUser.Rows[0]["firstName"].ToString() + " " + dtTakenUser.Rows[0]["lastName"].ToString();
                    }
                    //objUser.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
                    //dtUser = objUser.getUserByID();
                    //if (dtUser != null && dtUser.Rows.Count > 0 && (dtUser.Rows[0]["referralId"].ToString() == dtTakenUser.Rows[0]["friendsReferralId"].ToString()))
                    //{
                    //    explain = "Referral Connected" + "<br>" + username;
                    //}
                    //else
                    //{
                    //    explain = "<a href='#' title='Gift card has been redeemed. However, this person already had referral'>No Connected</a>" + "<br>" + username;
                    //}

                }
            }
            else
            {
                explain = "Given By " + dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
            }

            return explain;
        }
        catch (Exception ex)
        {
            return "";
        }
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("giftcard_step1.aspx", false);
    }

    public bool displayPrevious = false;
    public bool displayNext = true;
    protected void gvGiftCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex == 0)
            {
                displayPrevious = false;
            }
            else
            {
                displayPrevious = true;
            }
            if (e.NewPageIndex == gvGiftCards.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gvGiftCards.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.bindGrid(Convert.ToInt64(ViewState["userID"]));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red; 
        }
    }
    public string strIDs = "";
    public int start = 2;
    protected void gvGiftCards_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (start <= 9)
            {
                strIDs += "*ctl00_ContentPlaceHolder1_gvGiftCards_ctl0" + start + "_RowLevelCheckBox";
            }
            else
            {
                strIDs += "*ctl00_ContentPlaceHolder1_gvGiftCards_ctl" + start + "_RowLevelCheckBox";
            }

            start++;
            hiddenIds.Text = strIDs;
        }
        catch (Exception)
        { 
        
        }
        try
        {
            DataTable pageSize = new DataTable("pager");
            DataColumn pageNo = new DataColumn("pageNo");
            pageSize.Columns.Add(pageNo);

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Repeater rptrPager = (Repeater)e.Row.FindControl("rptrPage");

                if (ViewState["page"] != null)
                {
                    int count = Convert.ToInt32(ViewState["page"]);
                    for (int i = 0; i < count; i++)
                    {
                        DataRow drNewRow = pageSize.NewRow();
                        drNewRow["pageNo"] = (i + 1).ToString();
                        pageSize.Rows.Add(drNewRow);
                    }
                    rptrPager.DataSource = pageSize;
                    rptrPager.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red; 
        }
    }
    protected void lnkPage_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton pageLink = (LinkButton)sender;
            ViewState["pageText"] = pageLink.Text.ToString();
            if (pageLink.CommandName == "Page")
            {
                if (Convert.ToInt32(pageLink.CommandArgument) - 1 == 0)
                {
                    displayPrevious = false;
                }
                else
                {
                    displayPrevious = true;
                }
                if (Convert.ToInt32(pageLink.CommandArgument) == Convert.ToInt32(ViewState["page"]))
                {
                    displayNext = false;
                }
                else
                {
                    displayNext = true;
                }

                this.gvGiftCards.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.bindGrid(Convert.ToInt64(ViewState["userID"]));
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red; 
        }
    }
    protected System.Drawing.Color GetColor(object objPageNum)
    {
        string pageNum = objPageNum.ToString();
        string selectedPageNum = "";
        if (ViewState["pageText"] != null)
        {
            selectedPageNum = ViewState["pageText"].ToString();
        }
        else
        {
            ViewState["pageText"] = 1;
            selectedPageNum = 1.ToString();
        }

        if (pageNum == selectedPageNum)
        {
            return System.Drawing.Color.FromArgb(255, 163, 112);
        }
        else
        {
            return System.Drawing.Color.FromArgb(38, 145, 191);
        }
    }
    protected bool GetStatus(object objPageNum)
    {
        string pageNum = objPageNum.ToString();
        string selectedPageNum = "";
        if (ViewState["pageText"] != null)
        {
            selectedPageNum = ViewState["pageText"].ToString();
        }
        else
        {
            ViewState["pageText"] = 1;
            selectedPageNum = 1.ToString();
        }

        if (pageNum == selectedPageNum)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtUser = null;
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
            if (dtUser != null)
            {
                //HttpCookie yourCity = Request.Cookies["yourCity"];
                //string strCityid = "337";
                //if (yourCity != null)
                //{
                //    strCityid = yourCity.Values[0].ToString().Trim();
                //}
                //Misc.addSubscriberEmail(txtEmailTo.Text.Trim(), strCityid);


                string strFrom = dtUser.Rows[0]["userName"].ToString();
                System.Text.StringBuilder mailBody = new System.Text.StringBuilder();

                string toAddress = txtEmailTo.Text.Trim();
                string fromAddress = strFrom;
                string Subject = "You have received a gift card from Tazzling.Com";

                mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
                mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                mailBody.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
                mailBody.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                mailBody.Append("<strong>Dear " + txtFirstName.Text.Trim()+" "+txtLastName.Text.Trim() + ",</strong></div>");
                mailBody.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received a gift card(s) from " + dtUser.Rows[0]["firstName"].ToString().Trim() + " " + dtUser.Rows[0]["lastName"].ToString().Trim() + " at Tazzling.com.</strong></div>");
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Message From " + dtUser.Rows[0]["firstName"].ToString().Trim() + " " + dtUser.Rows[0]["lastName"].ToString().Trim() + ": " + txtMessage.Text.Trim() + " </div>");                
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>To start redeeming these gift cards,  you will need to:</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1.	Login on Tazzling.com</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2.	Go to the member area -> Gift Card</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3.	Deposit the gift card with the following numbers:</div>");

                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'><strong>Your gift card(s) detail as follow:</strong></div>");

                mailBody.Append("<table style='margin: 0px 10px 10px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; clear: both;'><tr><td style='float: left; width: 150px;'><strong>Sr.#</strong></td><td style='float: left; width: 150px;'><strong>Gift Card Number</strong></td><td style='float: left; width: 150px;'><strong>Card Amount</strong></td></tr>");

                if (gvGiftCards.Rows.Count > 0)
                {
                    int counter = 1;
                    for (int i = 0; i < gvGiftCards.Rows.Count; i++)
                    {
                        GridViewRow row = gvGiftCards.Rows[i];
                        CheckBox chk = ((CheckBox)row.FindControl("RowLevelCheckBox"));
                        if (chk.Checked)
                        {
                            mailBody.Append("<tr>");
                            mailBody.Append("<td style='float: left; width: 150px;'>" + counter.ToString() + "</td>");
                            mailBody.Append("<td style='float: left; width: 150px;'>" + ((Label)row.FindControl("lblBalance")).Text.ToString() + "</td>");
                            mailBody.Append("<td style='float: left; width: 150px;'>" + ((Label)row.FindControl("lblFunds")).Text.ToString() + "</td>");
                            mailBody.Append("</tr>");
                            counter++;
                        }
                    }
                }
                mailBody.Append("</table>");                                                
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");
                mailBody.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
                mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
                mailBody.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
                mailBody.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
                mailBody.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
                mailBody.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");

                if (Misc.SendEmail(toAddress, "", "", fromAddress, Subject, mailBody.ToString()))
                {
                    bindGrid(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                    lblMessage.Text = "Gift card(s) sent successfully.";
                    pnlMsg.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png"; 
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    bindGrid(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                    lblMessage.Text = "Gift card(s) count not sent.";
                    pnlMsg.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png"; 
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red; 
        }
    }
}
