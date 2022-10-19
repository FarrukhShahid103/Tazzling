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


public partial class MemberAffiliate : System.Web.UI.Page
{
    BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Affiliate Partner";
            if (!IsPostBack)
            {
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                {
                    LoadDropDownList();
                    DataTable dtUser = null;
                    if (Session["member"] != null)
                    {
                        dtUser = (DataTable)Session["member"];
                        ViewState["userID"] = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());

                        if (chkAffiliatePartnerOrNot(dtUser) == true)
                        {
                            //Get the Affiliate Total Amount
                            GetAndSetAffiliateCommCredits();

                            //Get the Affiliate Total Amount
                            GetAndSetTotalAffiliateEarned();

                            bindAffiliatePartnerCreditInfo();
                            //resturantTiers.Visible = false;          
                        }
                        else
                        {
                            Response.Redirect("member_affiliate_info.aspx", true);
                        }
                    }
                    else if (Session["restaurant"] != null)
                    {
                        dtUser = (DataTable)Session["restaurant"];

                        if (chkAffiliatePartnerOrNot(dtUser) == true)
                        {
                            ViewState["userID"] = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());

                            //Get the Affiliate Total Amount
                            GetAndSetAffiliateCommCredits();

                            //Get the Affiliate Total Amount
                            GetAndSetTotalAffiliateEarned();

                            bindAffiliatePartnerCreditInfo();
                            //resturantTiers.Visible = false;
                        }
                        else
                        {
                            Response.Redirect("member_affiliate_info.aspx", true);
                        }
                    }
                    else if (Session["sale"] != null)
                    {
                        dtUser = (DataTable)Session["sale"];

                        if (chkAffiliatePartnerOrNot(dtUser) == true)
                        {
                            ViewState["userID"] = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());

                            //Get the Affiliate Total Amount
                            GetAndSetAffiliateCommCredits();

                            //Get the Affiliate Total Amount
                            GetAndSetTotalAffiliateEarned();

                            bindAffiliatePartnerCreditInfo();
                            //bindReferralRestaurants();
                        }
                        else
                        {
                            Response.Redirect("member_affiliate_info.aspx", true);
                        }
                    }
                    else if (Session["user"] != null)
                    {
                        dtUser = (DataTable)Session["user"];

                        if (chkAffiliatePartnerOrNot(dtUser) == true)
                        {
                            ViewState["userID"] = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());

                            //Get the Affiliate Total Amount
                            GetAndSetAffiliateCommCredits();

                            //Get the Affiliate Total Amount
                            GetAndSetTotalAffiliateEarned();

                            bindAffiliatePartnerCreditInfo();
                            //bindReferralRestaurants();
                        }
                        else
                        {
                            Response.Redirect("member_affiliate_info.aspx", true);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    private bool chkAffiliatePartnerOrNot(DataTable dtUserInfo)
    {
        bool bStatus = false;

        try
        {
            if (dtUserInfo != null)
            {
                bStatus = dtUserInfo.Rows[0]["affiliateReq"] == DBNull.Value ? false : dtUserInfo.Rows[0]["affiliateReq"].ToString() == "approved" ? true : false;
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return bStatus;
    }

    private void GetAndSetAffiliateCommCredits()
    {
        try
        {
            objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(ViewState["userID"].ToString());
            DataTable dtRemainFoodCredit = objBLLAffiliatePartnerGained.getGetAffiliatePartnerGainedCreditsByUserID();
            if (dtRemainFoodCredit != null && dtRemainFoodCredit.Rows.Count > 0 && dtRemainFoodCredit.Rows[0][0].ToString() != "")
            {
                this.lblAffComBal.Text = "My Affiliate Commission Balance: " + "$" + dtRemainFoodCredit.Rows[0][0].ToString() + " CAD";
            }
            else
            {
                this.lblAffComBal.Text = "My Affiliate Commission Balance: " + "$0.00 CAD";
            }
        }
        catch (Exception ex)
        { }
    }

    private void GetAndSetTotalAffiliateEarned()
    {
        try
        {
            objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(ViewState["userID"].ToString());
            DataTable dtRemainFoodCredit = objBLLAffiliatePartnerGained.getGetAffiliatePartnerTotalEarnedByUserID();
            if (dtRemainFoodCredit != null && dtRemainFoodCredit.Rows.Count > 0 && dtRemainFoodCredit.Rows[0][0].ToString() != "")
            {
                this.lblAffComTotal.Text = "Total Commission Earned: " + "$" + dtRemainFoodCredit.Rows[0][0].ToString() + " CAD";
            }
            else
            {
                this.lblAffComTotal.Text = "Total Commission Earned: " + "$0.00 CAD";
            }
        }
        catch (Exception ex)
        { }
    }

    private void LoadDropDownList()
    {
        try
        {
            ddlYear.Items.Add(new ListItem("Select Year", "Select Year"));
            for (int year = DateTime.Now.AddYears(-10).Year; year <= DateTime.Now.Year; year++)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
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

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            bindAffiliatePartnerCreditInfo();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void bindAffiliatePartnerCreditInfo()
    {
        try
        {

            DateTime start = GetTimeframeStartDate(ddlMonth, ddlYear);
            DateTime end = GetTimeframeEndDate(ddlMonth, ddlYear);

            DataTable dtUser = null;

            if ((ViewState["Query"] == null) && (ViewState["userID"] != null))
            {
                objBLLAffiliatePartnerGained.UserId = int.Parse(ViewState["userID"].ToString());

                dtUser = objBLLAffiliatePartnerGained.getGetAffiliateGainedCreditsByUserID(start, end);
            }

            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                gridview1.DataSource = dtUser;
                gridview1.DataBind();
            }
            else
            {
                gridview1.DataSource = null;
                gridview1.DataBind();
            }

            SetTotalGainedInPeriod(dtUser);
        }
        catch (Exception ex)
        {
            lblHeaderMessage.Text = ex.ToString();
            lblHeaderMessage.Visible = true;
            lblHeaderMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void SetTotalGainedInPeriod(DataTable dtAffCredit)
    {
        try
        {
            if (dtAffCredit != null)
            {
                float fGainedCount = 0;

                for (int i = 0; i < dtAffCredit.Rows.Count; i++)
                    fGainedCount += float.Parse(dtAffCredit.Rows[i]["gainedAmount"].ToString().Trim());

                this.lblPeriodSales.Text = "$" + fGainedCount + " CAD";
            }
            else
            {
                this.lblPeriodSales.Text = "$0.00 CAD";
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected DateTime GetTimeframeStartDate(DropDownList ddlMonth, DropDownList ddlYear)
    {
        DateTime dtStartDate;

        if ((ddlMonth.SelectedValue.Trim() == "Select Month") || (ddlYear.SelectedValue.Trim() == "Select Year"))
            dtStartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
        else
            dtStartDate = Convert.ToDateTime(ddlMonth.SelectedValue + "/1/" + ddlYear.SelectedValue + " 00:00:00");

        return dtStartDate;
    }

    protected DateTime GetTimeframeEndDate(DropDownList ddlMonth, DropDownList ddlYear)
    {
        DateTime dtEndDate = GetTimeframeStartDate(ddlMonth, ddlYear);

        dtEndDate = Convert.ToDateTime(dtEndDate.ToShortDateString() + " 23:59:59");

        if ((ddlMonth.SelectedValue.Trim() != "Select Month") && (ddlYear.SelectedValue.Trim() != "Select Year"))
            dtEndDate = dtEndDate.AddMonths(1).AddDays(-1);

        return dtEndDate;
    }

    protected void lbViewHistory_Click(object sender, EventArgs e)
    {
        try
        {
            /*if (lbViewHistory.Text == "View Today")
            {
                lbViewHistory.Text = "View History";

                //Set the Header Message here
                this.lblHeaderMessage.Visible = false;

                //Set the Div Search area here
                strDivSrch = "none";

                //Show the Grid View Records
                Design_setup();
            }
            else
            {
                lbViewHistory.Text = "View Today";

                //Set the Header Message here
                this.lblHeaderMessage.Visible = true;

                //lblHeaderMessage.Text = ViewState["HeaderMessage"] == null ? ModDic.GetString("RewardsTransHistory") : ViewState["HeaderMessage"].ToString();
                lblHeaderMessage.Text = ViewState["HeaderMessage"] == null ? "Received Rewards Transaction History" : ViewState["HeaderMessage"].ToString();

                //Set the Div Search area here
                strDivSrch = "block";

                gridview1.DataBound += new EventHandler(gridview1_DataBound);
                gridview1_DataBind();
            }*/
        }
        catch (Exception ex)
        {
            lblHeaderMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblHeaderMessage.Visible = true;
        }
    }
}