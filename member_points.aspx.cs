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

public partial class member_points : System.Web.UI.Page
{
    BLLKarmaPoints objkarmaPoints = new BLLKarmaPoints();

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Points";
            if (!IsPostBack)
            {
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
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
                    if (dtUser != null && dtUser.Rows.Count > 0 && dtUser.Rows[0]["userId"].ToString().Trim()!="")
                    {
                        ViewState["userID"] = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
                        //Get the Affiliate Total Amount
                        GetTotalPoints();

                        bindPoints(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                    }
                    else
                    {
                        lblHeaderMessage.Text = "There is some issue";
                        lblHeaderMessage.Visible = true;
                        lblHeaderMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    //resturantTiers.Visible = false;                              
                }
            }
        }
        catch (Exception ex)
        {
            lblHeaderMessage.Text = ex.ToString();
            lblHeaderMessage.Visible = true;
            lblHeaderMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnWithdrawPoints_Click(object sender, EventArgs e)
    {
        try
        {
            int intPoints = 0;
            int.TryParse(hfPoints.Value.Trim(), out intPoints);            
            if (intPoints >= 1000)
            {
                try
                {
                    BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                    bllKarma.userId = long.Parse(ViewState["userID"].ToString().Trim());
                    bllKarma.karmaPoints =  -1000;
                    bllKarma.karmaPointsType = "Redeem Points";
                    bllKarma.createdBy = long.Parse(ViewState["userID"].ToString().Trim());
                    bllKarma.createdDate = DateTime.Now;
                    bllKarma.createKarmaPoints();

                    BLLMemberUsedGiftCards objUsedCard = new BLLMemberUsedGiftCards();
                    objUsedCard.remainAmount = 10;
                    objUsedCard.createdBy = Convert.ToInt32(ViewState["userID"].ToString().Trim());
                    objUsedCard.gainedAmount = 10;
                    objUsedCard.fromId = Convert.ToInt64(ViewState["userID"].ToString().Trim());
                    objUsedCard.targetDate = DateTime.Now.AddMonths(6);
                    objUsedCard.currencyCode = "CAD";
                    objUsedCard.gainedType = "Redeem Points";
                    objUsedCard.createMemberUseableGiftCard();

                    GetTotalPoints();
                    bindPoints(Convert.ToInt64(ViewState["userID"].ToString().Trim()));
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Your 1000 points have been converted successfully.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(btnWithdrawPoints, typeof(Button), "Javascript", jScript, false);
                }
                catch (Exception ex)
                { }
            }
            else
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('You need atleast 1000 points to convert.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnWithdrawPoints, typeof(Button), "Javascript", jScript, false);
            }

        }
        catch (Exception ex)
        { }
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

                this.bindPoints(Convert.ToInt64(ViewState["userID"].ToString().Trim()));
                
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
            this.bindPoints(Convert.ToInt64(ViewState["userID"].ToString().Trim()));
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab1').click();</script>", false);
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
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }
    }
  
    private void GetTotalPoints()
    {
        try
        {
            if (ViewState["userID"] != null && ViewState["userID"].ToString().Trim() != "")
            {
                objkarmaPoints.userId = Convert.ToInt32(ViewState["userID"].ToString());
                DataTable dtKarmaPoints = objkarmaPoints.getKarmaPointsTotalByUserId();
                if (dtKarmaPoints != null && dtKarmaPoints.Rows.Count > 0 && dtKarmaPoints.Rows[0][0].ToString() != "")
                {
                    this.lblAffComBal.Text = "Current Points Balance: " + dtKarmaPoints.Rows[0][0].ToString();
                    hfPoints.Value = dtKarmaPoints.Rows[0][0].ToString();
                }
                else
                {
                    hfPoints.Value = "0";
                    this.lblAffComBal.Text = "Current Points Balance: 0";
                }
            }
            else
            {
                hfPoints.Value = "0";
                this.lblAffComBal.Text = "Current Points Balance: 0";
            }
        }
        catch (Exception ex)
        {
            lblHeaderMessage.Text = ex.ToString();
            lblHeaderMessage.Visible = true;
            lblHeaderMessage.ForeColor = System.Drawing.Color.Red;
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

    protected void bindPoints(long userID)
    {
        try
        {                       
            DataTable dtPoints=null;
            objkarmaPoints.userId = userID;
            dtPoints = objkarmaPoints.getKarmaPointsByUserId();
            ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtPoints.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
            if (dtPoints != null && dtPoints.Rows.Count > 0)
            {                
                gridview1.DataSource = dtPoints;
                gridview1.DataBind();
            }
            else
            {
                gridview1.DataSource = null;
                gridview1.DataBind();
            }            
        }
        catch (Exception ex)
        {
            lblHeaderMessage.Text = ex.ToString();
            lblHeaderMessage.Visible = true;
            lblHeaderMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
   
}