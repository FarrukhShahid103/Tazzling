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
using System.Text;
using System.Text;
using System.Net.Mail;

public partial class Takeout_UserControls_Templates_withdraw : System.Web.UI.UserControl
{
    BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
    BLLConsumptionRecord objConsumbedRecord = new BLLConsumptionRecord();
    BLLWithdrawRequest objWithdraw = new BLLWithdrawRequest();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["member"] != null || Session["restaurant"] != null)
                {
                    DataTable dtUser = null;
                    if (Session["member"] != null)
                    {
                        dtUser = (DataTable)Session["member"];
                    }
                    else if (Session["sale"] != null)
                    {
                        dtUser = (DataTable)Session["sale"];
                    }
                    else if (Session["restaurant"]!=null)
                    {
                        dtUser = (DataTable)Session["restaurant"];
                    }
                    else if(Session["user"]!=null)
                    {
                        dtUser=(DataTable)Session["user"];
                    }
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        ViewState["userID"] = dtUser.Rows[0]["userId"].ToString();
                        ViewState["userName"] = dtUser.Rows[0]["userName"].ToString();
                        CalculateCommissionMoney();
                        BindGrid();
                    }
                }
                else
                {
                    Response.Redirect("default.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("default.aspx", false);
        }
    }

    protected void BindGrid()
    {
        objWithdraw.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
        DataTable dtWithdraw = objWithdraw.getWithdrawRequestByUserID();

        gridview1.PageSize = Misc.clientPageSize;
        ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtWithdraw.Rows.Count / gridview1.PageSize)).ToString();
        gridview1.DataSource = dtWithdraw;
        gridview1.DataBind();
    }

    protected void btnSendRequest_Click(object sender, EventArgs e)
    {
        try
        {
            objWithdraw.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
            objWithdraw.requestAction = "In Process";
            objWithdraw.requestAmount = float.Parse(txtAmount.Text.Trim());
            objWithdraw.requestUserType = "Member";
            objWithdraw.createWithdrawRequest();
            saveConsumptionRecord(float.Parse(txtAmount.Text));
            SendMailToAdminForDealStatus(txtAmount.Text.Trim());
            txtAmount.Text = "";
            CalculateCommissionMoney();
            BindGrid();
        }
        catch (Exception ex)
        {
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private bool SendMailToAdminForDealStatus(string strAmount)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
        try
        {
            string toAddress = "colin@tazzling.com";
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = "New Withdrawl Request";
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");
            mailBody.Append("<h4>Deal Admin.");
            mailBody.Append("</h4>");
            mailBody.Append("<p><font size='3'>You have received a new withdrawl request by user \"" + ViewState["userName"].ToString() + "\" for $" + strAmount + ".</p><br>");            
            mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();

            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {           
            return false;
        }
    }

    private void saveConsumptionRecord(float amount)
    {
        try
        {
            float remAmount = 0;
            objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
            objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(ViewState["userID"].ToString());
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
                            objBLLAffiliatePartnerGained.ModifiedBy = Convert.ToInt32(ViewState["userID"].ToString());
                            objBLLAffiliatePartnerGained.RemainAmount = remAmount - amount;
                            objBLLAffiliatePartnerGained.updateAffiliateRemainingUsableAmount();
                           
                            break;
                        }
                        else
                        {
                            objBLLAffiliatePartnerGained.AffiliatePartnerId = Convert.ToInt32(dt.Rows[i]["affiliatePartnerId"].ToString());
                            objBLLAffiliatePartnerGained.ModifiedBy = Convert.ToInt32(ViewState["userID"].ToString());
                            objBLLAffiliatePartnerGained.RemainAmount = 0;
                            objBLLAffiliatePartnerGained.updateAffiliateRemainingUsableAmount();
                            amount = amount - remAmount;
                           
                        }
                    }
                }
            }       
        }
        catch (Exception ex)
        {
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }


    private void CalculateCommissionMoney()
    {
        double dGainedCommission = 0;
                                             
        objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(ViewState["userID"].ToString());
        DataTable dtComissionMaoney = objBLLAffiliatePartnerGained.getGetAffiliatePartnerGainedCreditsByUserID();
        if (dtComissionMaoney != null && dtComissionMaoney.Rows.Count > 0 && dtComissionMaoney.Rows[0][0].ToString().Trim() != "")
        {           
            dGainedCommission = Convert.ToDouble(dtComissionMaoney.Rows[0][0].ToString());
        }

        lblFromBalance.Text = "My withdraw able money is <b>$<span id='cMoney'>" + (dGainedCommission).ToString("###.00") + "</span> CAD</b>";
        if ((dGainedCommission) >= 50.0)
        {
            panelWithdraw.Visible = true;
            lblWithdrawError.Visible = false;
            lblWithdrawError.Text =  Convert.ToDouble(dGainedCommission).ToString("###.00");
        }
        else
        {
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> Sorry, there is not enough funds available to withdraw. The minimum balance is <b>$50</b>.";
        }

    }

    protected string GetExpirationDateString(object expirationDate)
    {
        if (expirationDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(expirationDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }


    protected double GetRequestAmount(string requestAction, double requestAmount, bool isRestaurant)
    {
        if (requestAction == "Check Sent")
        {
            if (!isRestaurant)
                return requestAmount - 2.5;
            else
                return requestAmount;
        }
        return requestAmount;
    }

    public bool displayPrevious = false;
    public bool displayNext = true;
    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            if (e.NewPageIndex == gridview1.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gridview1.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.BindGrid();
        }
        catch (Exception ex)
        {
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    protected void gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
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

                this.gridview1.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.BindGrid();
            }
        }
        catch (Exception ex)
        {
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
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
}
