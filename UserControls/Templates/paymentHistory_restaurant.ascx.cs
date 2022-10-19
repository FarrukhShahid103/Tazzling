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

public partial class Takeout_UserControls_Templates_paymentHistory_restaurant : System.Web.UI.UserControl
{    
    BLLWithdrawRequest objWithdraw = new BLLWithdrawRequest();
    BLLOrders objOrders = new BLLOrders();    
    BLLRestaurantFee objFee = new BLLRestaurantFee();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            resBalance = 0;
            if (!IsPostBack)
            {
                if (Session["restaurant"] != null)
                {
                    LoadDropDownList();
                    this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();                    
                    this.ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

                    DateTime start = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue), 1);
                    DateTime end = start.AddMonths(1).AddDays(-1);

                    DataTable dtUser = null;
                    if (Session["restaurant"] != null)
                    {
                        dtUser = (DataTable)Session["restaurant"];
                    }                  
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        ViewState["userID"] = dtUser.Rows[0]["userID"].ToString();
                        ViewState["restaurantId"] = dtUser.Rows[0]["restaurantId"].ToString();
                        BindGrid(end);
                    }                    
                }
                else
                {
                    Response.Redirect("opportunity.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("opportunity.aspx", false);
        }
    }

    private void LoadDropDownList()
    {
        //Clears the Drop Down List
        this.ddlYear.Items.Clear();
        this.ddlMonth.Items.Clear();

        //Year
        for (int year = 2009; year <= DateTime.Now.Year; year++)
        {
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }

        //Month
        for (int month = 1; month <= 12; month++)
        {
            ddlMonth.Items.Add(new ListItem(month.ToString(), month.ToString()));
        }

        this.btnSelect.Text = "Show";

    }

    protected void BindGrid(DateTime dtUserTime)
    {
        DataTable dtGivenMontHistory = new DataTable();

        dtGivenMontHistory.Columns.Add("Date", typeof(DateTime));
        dtGivenMontHistory.Columns.Add("Description");
        dtGivenMontHistory.Columns.Add("Amount");
        dtGivenMontHistory.Columns.Add("Balance");
        DataRow dtrow;
        double dCPSubTotal = 0;
        double dCPTotal = 0;
        double dCaSubTotal = 0;
        double dWidthdraw = 0;
        double dFeeAmount = 0;
        double dBalance = 0;
        objOrders.year = dtUserTime.AddMonths(-1).Year;
        objOrders.month = dtUserTime.AddMonths(-1).Month;
        objOrders.providerId = Convert.ToInt64(ViewState["restaurantId"].ToString());
        DataTable dtCredidCardOrdersSum = objOrders.getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDFromStartToGivenMonth();
        DataTable dtCashOrdersSum = objOrders.getTotalAndSubTotalOfCashOrdersByProviderIDFromStartToGivenMonth();
        
        objFee.year = dtUserTime.AddMonths(-1).Year;
        objFee.month = dtUserTime.AddMonths(-1).Month;
        objFee.restaurantId = Convert.ToInt64(ViewState["restaurantId"].ToString());
        DataTable dtfee = objFee.getTotalOfRestaurantFeeByRestaurantIDTillGivenMonth();

        if(dtfee!=null && dtfee.Rows.Count>0)
        {
            if (dtfee.Rows[0]["totalAmount"].ToString().Trim() != "")
            {
                dFeeAmount = Convert.ToDouble(dtfee.Rows[0]["totalAmount"].ToString());
            }
        }

        objWithdraw.year = dtUserTime.AddMonths(-1).Year;
        objWithdraw.month = dtUserTime.AddMonths(-1).Month;
        objWithdraw.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
        DataTable dtWithdraw = objWithdraw.getAllWithdrawmMoneyByUserIDTillGivenMonth();

        if (dtWithdraw != null && dtWithdraw.Rows.Count > 0)
        {
            if (dtWithdraw.Rows[0]["reqAmount"].ToString().Trim() != "")
            {
                dWidthdraw = Convert.ToDouble(dtWithdraw.Rows[0]["reqAmount"].ToString());
            }
        }


        if (dtCredidCardOrdersSum != null && dtCredidCardOrdersSum.Rows.Count > 0)
        {
            if (dtCredidCardOrdersSum.Rows[0]["totalAmount"].ToString() != "")
            {
                dCPTotal = Convert.ToDouble(dtCredidCardOrdersSum.Rows[0]["totalAmount"].ToString());
            }
            if (dtCredidCardOrdersSum.Rows[0]["totalAmount"].ToString() != "")
            {
                dCPSubTotal = Convert.ToDouble(dtCredidCardOrdersSum.Rows[0]["totalAmount"].ToString());
            }
        }
        if (dtCashOrdersSum != null && dtCashOrdersSum.Rows.Count > 0)
        {
            if (dtCashOrdersSum.Rows[0]["totalAmount"].ToString() != "")
            {
                dCaSubTotal = Convert.ToDouble(dtCashOrdersSum.Rows[0]["totalAmount"].ToString());
            }
        }

        DateTime start = new DateTime(objOrders.year, objOrders.month, 1);
        DateTime end = start.AddMonths(1).AddDays(-1);

        //resBalance = (dCPTotal - dCPSubTotal * .1) - (dCaSubTotal * .1) - dWidthdraw + dFeeAmount;
        resBalance = (dCPTotal - dCPSubTotal * .1)  - dWidthdraw + dFeeAmount;

        //dtrow = dtGivenMontHistory.NewRow();
        //dtrow["Date"] = end;
        //dtrow["Description"] = "Food Order";
        //dtrow["Amount"] = Convert.ToDouble((dCPTotal - dCPSubTotal * .1) - (dCaSubTotal * .1)).ToString("###.00");
        //dtrow["Balance"] = "";
        //dtGivenMontHistory.Rows.Add(dtrow);        
        
//        dBalance = (dCPTotal - dCPSubTotal * .1) - (dCaSubTotal * .1);

        objFee.restaurantId = Convert.ToInt64(ViewState["restaurantId"].ToString());
        objFee.year = dtUserTime.Year;
        objFee.month = dtUserTime.Month;
        DataTable dtResFee = objFee.getRestaurantFeeOfGivenMonthByRestaurantID();

        if (dtResFee != null && dtResFee.Rows.Count > 0)
        {
            for (int i = 0; i < dtResFee.Rows.Count; i++)
            {
                dtrow = dtGivenMontHistory.NewRow();
                dtrow["Date"] = Convert.ToDateTime(dtResFee.Rows[i]["creationDate"].ToString());
                if (float.Parse(dtResFee.Rows[i]["rfAmount"].ToString()) > 0)
                {
                    dtrow["Description"] = "Adjustment";
                }
                else
                {
                    dtrow["Description"] = "Monthly Fee";
                }
                dtrow["Amount"] = dtResFee.Rows[i]["rfAmount"].ToString();
                dtrow["Balance"] = "";
                dtGivenMontHistory.Rows.Add(dtrow);
            }
        }
        objWithdraw.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
        objWithdraw.month = dtUserTime.Month;
        objWithdraw.year = dtUserTime.Year;
        DataTable dtWithdra = objWithdraw.getWithdrawRequestForReturantOwnerByUserIDOfGivenMonth();
        if (dtWithdra != null && dtWithdra.Rows.Count > 0)
        {
            for (int i = 0; i < dtWithdra.Rows.Count; i++)
            {                
                dtrow = dtGivenMontHistory.NewRow();
                dtrow["Date"] = Convert.ToDateTime(dtWithdra.Rows[i]["creationDate"].ToString());
                dtrow["Description"] = "Withdraw";
                dtrow["Amount"] = dtWithdra.Rows[i]["requestAmount"].ToString();
                dtrow["Balance"] = "";
                dtGivenMontHistory.Rows.Add(dtrow);                
            }
        }

        dCPSubTotal = 0;
        dCPTotal = 0;
        dCaSubTotal = 0;
        objOrders.year = dtUserTime.Year;
        objOrders.month = dtUserTime.Month;
        objOrders.providerId = Convert.ToInt64(ViewState["restaurantId"].ToString());
        DataTable dtCredidCardOrdersSum2 = objOrders.getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDAndGivenMonth();
        DataTable dtCashOrdersSum2 = objOrders.getTotalAndSubTotalOfCashOrdersByProviderIDAndGivenMonth();

        if (dtCredidCardOrdersSum2 != null && dtCredidCardOrdersSum2.Rows.Count > 0)
        {
            if (dtCredidCardOrdersSum2.Rows[0]["totalAmount"].ToString() != "")
            {
                dCPTotal = Convert.ToDouble(dtCredidCardOrdersSum2.Rows[0]["totalAmount"].ToString());
            }
            if (dtCredidCardOrdersSum2.Rows[0]["totalAmount"].ToString() != "")
            {
                dCPSubTotal = Convert.ToDouble(dtCredidCardOrdersSum2.Rows[0]["totalAmount"].ToString());
            }
        }
        if (dtCashOrdersSum2 != null && dtCashOrdersSum2.Rows.Count > 0)
        {
            if (dtCashOrdersSum2.Rows[0]["totalAmount"].ToString() != "")
            {
                dCaSubTotal = Convert.ToDouble(dtCashOrdersSum2.Rows[0]["totalAmount"].ToString());
            }
        }

        
        dtrow = dtGivenMontHistory.NewRow();
        if (dtUserTime.Month == DateTime.Now.Month)
        {
            dtrow["Date"] = DateTime.Now;
        }
        else
        {
            dtrow["Date"] = dtUserTime;
        }
        
        dtrow["Description"] = "Food Order";
       // dtrow["Amount"] = Convert.ToDouble((dCPTotal - dCPSubTotal * .1) - (dCaSubTotal * .1)).ToString("###.00");
        dtrow["Amount"] = Convert.ToDouble(dCPTotal - dCPSubTotal * .1).ToString("###.00");
        dtrow["Balance"] = "";
        dtGivenMontHistory.Rows.Add(dtrow);

        DataView dv = new DataView(dtGivenMontHistory);
        dv.Sort = "Date ASC";
                
        gridview1.PageSize = Misc.clientPageSize;
        ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtGivenMontHistory.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
        gridview1.DataSource = dv;
        gridview1.DataBind();


        // double dBalance = (dCPTotal - dCPSubTotal * .1) - (dCaSubTotal * .1) - dWidthdraw + dFeeAmount;


        //objOrders.getTotalAmountOfGivenMonthByUserID();
        //obj.restaurantId = resturantID;
        //DataTable dtWithdraw = obj.getAllRFByRestaurantID();
        //gridview1.PageSize = Misc.clientPageSize;
        //ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtWithdraw.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
        //gridview1.DataSource = dtWithdraw.DefaultView;
        //gridview1.DataBind();

    }

    protected string GetExpirationDateString(object expirationDate)
    {
        if (expirationDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(expirationDate);
            return dt.ToString("MM-dd-yyyy");
        }
        return "";
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

            DateTime start = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue), 1);
            DateTime end = start.AddMonths(1).AddDays(-1);

            this.BindGrid(end);
        }
        catch (Exception ex)
        {

        }
    }
    public static double resBalance = 0;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //GridView gvSubItem = (GridView)e.Row.FindControl("gvSubItem");

                Label lblAmount2 = (Label)e.Row.FindControl("lblAmount2");
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                if (lblAmount2 != null && lblAmount2.Text.ToString() != "")
                {
                    if (lblBalance != null && lblDescription!=null)
                    {
                        if (lblDescription.Text.Trim() == "Withdraw")
                        {
                            lblBalance.Text = "$" + Convert.ToDouble(resBalance - Convert.ToDouble(lblAmount2.Text.Trim())).ToString("###.00");
                            resBalance = Convert.ToDouble(resBalance - Convert.ToDouble(lblAmount2.Text.Trim()));
                        }
                        else
                        {
                            lblBalance.Text = "$" + Convert.ToDouble(resBalance + Convert.ToDouble(lblAmount2.Text.Trim())).ToString("###.00");
                            resBalance = Convert.ToDouble(resBalance + Convert.ToDouble(lblAmount2.Text.Trim()));
                        }
                        
                    }
                }
            }


        }
        catch (Exception ex)
        {
            
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
                DateTime start = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue), 1);
                DateTime end = start.AddMonths(1).AddDays(-1);

                this.BindGrid(end);
            }
        }
        catch (Exception ex)
        {
            
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

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        DateTime start = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), Convert.ToInt32(this.ddlMonth.SelectedValue), 1);
        DateTime end = start.AddMonths(1).AddDays(-1);

        this.BindGrid(end);
    }
}
