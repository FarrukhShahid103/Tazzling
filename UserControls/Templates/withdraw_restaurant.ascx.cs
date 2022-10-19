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

public partial class Takeout_UserControls_Templates_withdraw_restaurant : System.Web.UI.UserControl
{
    
    BLLConsumptionRecord objConsumbedRecord = new BLLConsumptionRecord();
    BLLWithdrawRequest objWithdraw = new BLLWithdrawRequest();
    BLLOrders objOrders = new BLLOrders();
    BLLRestaurantFee objFee = new BLLRestaurantFee();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["restaurant"] != null)
                {
                    DataTable dtUser = null;
                    if (Session["restaurant"] != null)
                    {
                        dtUser = (DataTable)Session["restaurant"];
                    }                  
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        ViewState["userID"] = dtUser.Rows[0]["userId"].ToString();
                        CalculateOrderMoney(dtUser);
                        BindGrid();
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

    protected void BindGrid()
    {
        objWithdraw.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
        DataTable dtWithdraw = objWithdraw.getWithdrawRequestForReturantOwnerByUserID();
        gridview1.PageSize = Misc.clientPageSize;
        ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtWithdraw.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
        gridview1.DataSource = dtWithdraw.DefaultView;
        gridview1.DataBind();
    }

    protected void btnSendRequest_Click(object sender, EventArgs e)
    {
        try
        {
            objWithdraw.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
            objWithdraw.requestAction = "In Process";
            objWithdraw.requestAmount = float.Parse(txtAmount.Text.Trim());
            objWithdraw.requestUserType = "Restaurant";
            
            objWithdraw.createWithdrawRequest();
            saveConsumptionRecord(txtAmount.Text, true);//true for commission 
            txtAmount.Text = "";

            DataTable dtUser = (DataTable)Session["restaurant"];
            CalculateOrderMoney(dtUser);
            BindGrid();
        }
        catch (Exception ex)
        {
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void saveConsumptionRecord(string amount, bool cType)
    {
        try
        {
            objConsumbedRecord.orderId = 0;
            objConsumbedRecord.consumptionAmount = float.Parse(amount);
            objConsumbedRecord.consumptionType = cType;
            objConsumbedRecord.createdBy = Convert.ToInt64(ViewState["userID"]);
            objConsumbedRecord.currencyCode = "CAD";
            objConsumbedRecord.isOrder = true;
            objConsumbedRecord.createConsumptionRecord();            
        }
        catch (Exception ex)
        {
            panelWithdraw.Visible = false;
            lblWithdrawError.Visible = true;
            lblWithdrawError.Text = "<img src='images/ico_alert.gif'> There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }


    private void CalculateOrderMoney(DataTable dtUser)
    {
        if (dtUser.Rows[0]["restaurantId"].ToString() != "")
        {
            double dCPSubTotal = 0;
            double dCPTotal = 0;
            double dCaSubTotal = 0;
            double dWidthdraw = 0;
            double dFeeAmount = 0;   
            objOrders.providerId = Convert.ToInt64(dtUser.Rows[0]["restaurantId"].ToString());
            DataTable dtCredidCardOrdersSum = objOrders.getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderID();
            DataTable dtCashOrdersSum = objOrders.getTotalAndSubTotalOfCashOrdersByProviderID();
            objConsumbedRecord.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
            DataTable dtConsumed = objConsumbedRecord.getAllWithdrawmMoneyByUserID();
            objFee.restaurantId = Convert.ToInt64(dtUser.Rows[0]["restaurantId"].ToString());
            DataTable dtFee = objFee.getTotalOfRestaurantFeeByRestaurantID();
            if (dtFee != null && dtFee.Rows.Count > 0)
            {
                if (dtFee.Rows[0]["totalAmount"].ToString().Trim() != "")
                {
                    dFeeAmount = Convert.ToDouble(dtFee.Rows[0]["totalAmount"].ToString());
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
            if (dtConsumed != null && dtConsumed.Rows.Count > 0)
            {
                if (dtConsumed.Rows[0][0].ToString() != "")
                {
                    dWidthdraw = Convert.ToDouble(dtConsumed.Rows[0][0].ToString());
                }
            }

            double dOrderBalance = (dCPTotal - dCPSubTotal * .1) - dWidthdraw + dFeeAmount; 

            if (dOrderBalance >0)
            {
                lblFromBalance.Text = "Your withdraw able money is $<span id='cMoney'>" + dOrderBalance.ToString("###.00") + "</span> CAD";
                panelWithdraw.Visible = true;
                lblWithdrawError.Visible = false;                
            }
            else
            {
                panelWithdraw.Visible = false;
                lblWithdrawError.Visible = true;
                lblWithdrawError.Text = "<img src='images/ico_alert.gif'> Sorry, there is not enough funds available to withdraw.";
            }
        }
        else
        {
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
