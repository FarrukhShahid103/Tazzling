using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using GecLibrary;

public partial class Takeout_UserControls_Order_OrdersList : System.Web.UI.UserControl
{
    protected string strDivSrch = "";
    public string bComm = "";
    public string bRedeem = "";
    public string bnetamount = "";
    public string bfee = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["restaurant"] == null) && (Session["member"] == null) && (Session["sale"] == null))
            Response.Redirect("opportunity.aspx");

        if (!Page.IsPostBack)
        {
            //Set the Div Search to visible
           // strDivSrch = "none";

            //Set the Month and Year Drop Down Lists
            LoadDropDownList();
            
            //Show the Grid View Records
            Design_setup();

            //Show the Message
            ShowMessage();
        }
    }

    private void Design_setup()
    {
        //Set the Current Year here
        this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();

        //Set the Current Month here
        this.ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

        //gridview1.AllowPaging = true;
        gridview1.DataBound += new EventHandler(gridview1_DataBound);
        gridview1_DataBind();
    }

    private void ShowMessage()
    {
        try
        {
            if (Session["OrderStatus"] != null)
            {
                if (Session["OrderStatus"].ToString() == "Updated")
                {
                    imgGridMessage.Visible = true;
                    lblMessage.Visible = true;
                }

                Session.Remove("OrderStatus");
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region Grid View Processor

    private void gridview1_DataBind()
    {
        try
        {
            DateTime start = GetTimeframeStartDate(ddlMonth, ddlYear);
            DateTime end = GetTimeframeEndDate(ddlMonth, ddlYear);

            DataTable dtOrders = null;

            BLLOrders objBLLOrders = new BLLOrders();

            DataTable dtRestaurant = null;

            if (Session["restaurant"] != null)
            {
                dtRestaurant = (DataTable)Session["restaurant"];

                if ((Session["Ctrl_Member"] != null) && (Session["Ctrl_Member"].ToString().Trim() == "m"))
                {
                    //In the members area
                    objBLLOrders.createdBy = long.Parse(dtRestaurant.Rows[0]["userID"].ToString());

                    //Get the Order's Info by Provider ID
                    dtOrders = objBLLOrders.getAllOrdersByCreatedByCreationDate(start, end);
                    //gridview1.Columns[6].Visible = false;
                    //gridview1.Columns[7].Visible = false;                
                    //                
                    bfee = "none";
                    bnetamount = "none";
                    lblTotalB.Text = "This Period Order:";

                    //Remove the Session of Control Memeber

                }
                else
                {
                    objBLLOrders.providerId = long.Parse(dtRestaurant.Rows[0]["restaurantId"].ToString());

                    //Get the Order's Info by Provider ID
                    dtOrders = objBLLOrders.getAllOrdersByProviderIDCreationDate(start, end);
                    //gridview1.Columns[8].Visible = false;
                    //gridview1.Columns[9].Visible = false;
                    lblTotalB.Text = "This Period Balance:";
                    bComm = "none";
                    bRedeem = "none";
                }
            }
            else if (Session["member"] != null || Session["sale"] != null)
            {
                if (Session["member"] != null)
                {
                    dtRestaurant = (DataTable)Session["member"];
                }
                else
                {
                    dtRestaurant = (DataTable)Session["sale"];
                }

                //Set the Orders Created By date to UserID
                objBLLOrders.createdBy = long.Parse(dtRestaurant.Rows[0]["userID"].ToString());

                //Get the Order's Info by Created By Date
                dtOrders = objBLLOrders.getAllOrdersByCreatedByCreationDate(start, end);
                //gridview1.Columns[6].Visible = false;
                //gridview1.Columns[7].Visible = false;            
                lblTotalB.Text = "This Period Order:";
                bfee = "none";
                bnetamount = "none";
                //bGross = "none";

            }

            //Get and Set the Total Period Balance
            GetAndSetTotalPeriodBal(dtOrders);

            if ((dtOrders != null) && (dtOrders.Rows.Count > 0))
            {
                gridview1.DataSource = dtOrders;
                gridview1.DataBind();
            }
            else
            {
                gridview1.DataSource = dtOrders;
                gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void LoadDropDownList()
    {
        try
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
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region Timeframe Filter

    protected DateTime GetTimeframeStartDate(DropDownList ddlMonth, DropDownList ddlYear)
    {
        DateTime dtStartDate;

        if (strDivSrch == "none")
            dtStartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
        else
            dtStartDate = Convert.ToDateTime(ddlMonth.SelectedValue + "/1/" + ddlYear.SelectedValue + " 00:00:00");

        return dtStartDate;
    }

    protected DateTime GetTimeframeEndDate(DropDownList ddlMonth, DropDownList ddlYear)
    {
        DateTime dtEndDate = GetTimeframeStartDate(ddlMonth, ddlYear);

        dtEndDate = Convert.ToDateTime(dtEndDate.ToShortDateString() + " 23:59:59");

        if (strDivSrch != "none")
            dtEndDate = dtEndDate.AddMonths(1).AddDays(-1);

        return dtEndDate;
    }

    #endregion

    protected void lbViewHistory_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbViewHistory.Text == "View Today")
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
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region Pager Row Process

    private void PageIndexChanging(int newPageIndex)
    {
        try
        {
            // check to prevent form the NewPageIndex out of the range
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= gridview1.PageCount ? gridview1.PageCount - 1 : newPageIndex;

            // specify the NewPageIndex
            gridview1.PageIndex = newPageIndex;

            gridview1_DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void CreateNumericControls(Control container, GridView gridView1)
    {
        try
        {
            int pbc = gridView1.PagerSettings.PageButtonCount;
            int pi = gridView1.PageIndex;
            int pc = gridView1.PageCount;

            CreateNumericControls(container, pbc, pi, pc);
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void CreateNumericControls(Control container, int pbc, int pi, int pc)
    {
        try
        {
            container.Controls.Clear();

            int pstar = 0;
            int pend = pc - 1;
            if (pbc > 0 && pbc < pc)
            {
                int ps = pbc / 2;
                pstar = (pi > ps) ? ((pi + pbc - ps > pc) ? pc - pbc : pi - ps) : 0;
                pend = pstar + pbc - 1;
            }
            if (pstar > 0)
            {
                container.Controls.Add(new LiteralControl("<span>&nbsp;...&nbsp;</span>"));
            }

            for (int i = pstar; i <= pend; i++)
            {
                int pageno = i + 1;
                if (i == pi)
                    container.Controls.Add(new LiteralControl("<span class=\"pagernumeric\">" + pageno + "</span>"));
                else
                {
                    LinkButton lbNumeric = new LinkButton();
                    lbNumeric.ID = container.ID + "_lbNumeric" + i;
                    lbNumeric.CommandArgument = i.ToString();
                    lbNumeric.Command += new CommandEventHandler(lbNumeric_Command);
                    lbNumeric.CausesValidation = false;
                    lbNumeric.Text = pageno.ToString();
                    container.Controls.Add(lbNumeric);
                }
                container.Controls.Add(new LiteralControl("&nbsp;"));
            }
            if (pend < pc - 1) container.Controls.Add(new LiteralControl("<span>&nbsp;...&nbsp;</span>"));
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    void lbNumeric_Command(object sender, CommandEventArgs e)
    {
        PageNumeric(e);
    }

    protected void PageNumeric(CommandEventArgs e)
    {
        PageIndexChanging(Convert.ToInt32(e.CommandArgument));
    }

    void gridview1_DataBound(object sender, EventArgs e)
    {
        if (gridview1.AllowPaging)
        {
            CreatePagerRow(this.gridview1.TopPagerRow);
            CreatePagerRow(this.gridview1.BottomPagerRow);
        }
    }

    private void CreatePagerRow(GridViewRow pagerRow)
    {
        if (pagerRow != null)
        {
            LinkButton lbPrev = pagerRow.FindControl("lbPrev") as LinkButton;
            LinkButton lbNext = pagerRow.FindControl("lbNext") as LinkButton;
            Label lblNumeric = pagerRow.FindControl("lblNumeric") as Label;

            lbPrev.Text = "Previous";
            lbNext.Text = "Next";

            if (this.gridview1.PageIndex <= 0)
            {
                lbPrev.Enabled = false;
            }
            else
                lbPrev.Enabled = true;

            if (this.gridview1.PageIndex >= gridview1.PageCount - 1)
                lbNext.Enabled = false;
            else
                lbNext.Enabled = true;

            CreateNumericControls(lblNumeric, this.gridview1);
        }
    }

    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        PageIndexChanging(e.NewPageIndex);
    }

    #endregion

    protected string GetOrderDate(DateTime createdOn, string orderId, string providerID)
    {

        //string url = "";
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string url = oInfo.Name;
        //if (Session["member"] != null)        
        //    url = "member_orders.aspx";
        
        //else
        //    url = "restaurant_orders.aspx";

        string date = FormatDateTime(createdOn, "yyyy-MM-dd H.mm tt");

        GECEncryption objGECEncryption = new GECEncryption();

        orderId = objGECEncryption.EncryptData("Order_ID_01", orderId);

        return "<a href='" + url + "?CtrlID=OrderDetails.ascx&ID=" + orderId + "'>" + date + "</a>";
    }

    protected string FormatDateTime(DateTime date, string format)
    {
        System.IFormatProvider provider = new System.Globalization.CultureInfo("en-US", true);
        return date.ToString(format, provider);
    }

    protected string ShowAmount(object currencyCode, object value)
    {
        return value.ToString();
    }

    protected string ShowOrderType(string OrderType)
    {
        if (OrderType.Trim().ToLower() == "cash")
        {
            return OrderType;
        }
        else
        {
            return "Prepaid";
        }        
    }


    

    protected string ShowCommissionAndNetAmount(object orderStatus,object OrderType, object currencyCode, object value)
    {
        double totalCom = 0;

        try
        {
            if (orderStatus.ToString() == "Payment Unsuccessful" || orderStatus.ToString() == "Cancelled" || OrderType.ToString()=="Cash")
            {
                return "0.00";
            }
            double subTotal = Convert.ToDouble(value);

            double CommRate = Convert.ToDouble(ConfigurationSettings.AppSettings["CommissionFee"].ToString());

            totalCom = (subTotal * CommRate) / 100;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return String.Format("{0:0.00}", totalCom).ToString();
    }

    #endregion

    protected string GetNetAmount(object orderStatus, object orderType, object sTotal, object totalAmt)
    {
        double subTotalCom = 0;

        try
        {
            if (orderStatus.ToString() == "Payment Unsuccessful" || orderStatus.ToString() == "Cancelled" || orderType.ToString() == "Cash")
            {
                return "0.00";
            }
            //else if (orderStatus.ToString() == "Confirmed" && orderType.ToString() == "Cash")
            //{
            //    double subTotal = Convert.ToDouble(totalAmt);

            //    double CommRate = Convert.ToDouble(ConfigurationSettings.AppSettings["CommissionFee"].ToString());

            //    subTotalCom = -(subTotal * CommRate) / 100;
              
            //}
            //else
            //{

                double subTotal = Convert.ToDouble(totalAmt);

                double CommRate = Convert.ToDouble(ConfigurationSettings.AppSettings["CommissionFee"].ToString());

                subTotalCom = Convert.ToDouble(totalAmt) - ((subTotal * CommRate) / 100);
            //}
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return String.Format("{0:0.00}", subTotalCom).ToString();
    }

    private void GetAndSetTotalPeriodBal(DataTable dtOrderListing)
    {
        try
        {
            //Initialize the Total Period Balance
            double CommRate = Convert.ToDouble(ConfigurationSettings.AppSettings["CommissionFee"].ToString());
            if ((Session["Ctrl_Member"] != null) && (Session["Ctrl_Member"].ToString().Trim() == "m"))
            {
                double bTotalPeriodBal = 0;
                for (int i = 0; i < dtOrderListing.Rows.Count; i++)
                {
                    bTotalPeriodBal += Convert.ToDouble(dtOrderListing.Rows[i]["totalAmount"] == null ? "0" : dtOrderListing.Rows[i]["totalAmount"].ToString().Trim());
                }

                //Set the Total Period balance
                lblPeriodSales.Text = "$" + String.Format("{0:0.00}", bTotalPeriodBal).ToString() + " CAD";
                Session.Remove("Ctrl_Member");
            }
            else
            {
                double bTotalPeriodBal = 0;
                double bTotalCash = 0;
                for (int i = 0; i < dtOrderListing.Rows.Count; i++)
                {
                    if (dtOrderListing.Rows[i]["OrderStatus"].ToString() == "Confirmed" && dtOrderListing.Rows[i]["OrderType"].ToString() == "Cash")
                    {
                        bTotalCash += (Convert.ToDouble(dtOrderListing.Rows[i]["totalAmount"] == null ? "0" : dtOrderListing.Rows[i]["totalAmount"].ToString().Trim()) * CommRate) / 100;
                    }
                    else if (dtOrderListing.Rows[i]["OrderStatus"].ToString() == "Confirmed")
                    {
                        bTotalPeriodBal += (Convert.ToDouble(dtOrderListing.Rows[i]["totalAmount"] == null ? "0" : dtOrderListing.Rows[i]["totalAmount"].ToString().Trim()) - ((Convert.ToDouble(dtOrderListing.Rows[i]["totalAmount"] == null ? "0" : dtOrderListing.Rows[i]["totalAmount"].ToString().Trim()) * CommRate) / 100));
                    }
                }

                //Set the Total Period balance
                lblPeriodSales.Text = "$" + String.Format("{0:0.00}", bTotalPeriodBal).ToString() + " CAD";
            }

            
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            //Set the Search Area here
            strDivSrch = "block";

            gridview1.DataBound += new EventHandler(gridview1_DataBound);
            gridview1_DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
