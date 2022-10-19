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

public partial class admin_UserControls_Order_OrdersList : System.Web.UI.UserControl
{
    public bool displayPrevious = false;
    public bool displayNext = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #region Grid View Processor

    private void gridview1_DataBind()
    {
        if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
        {
            this.gridview1.PageSize = Convert.ToInt32(Session["ddlPage"]);
        }
        else
        {
            this.gridview1.PageSize = Misc.pageSize;
            Session["ddlPage"] = Misc.pageSize;
        }

        DateTime start = GetTimeframeStartDate(ddlMonth, ddlYear);
        DateTime end = GetTimeframeEndDate(ddlMonth, ddlYear);

        DataTable dtOrders = null;

        BLLOrders objBLLOrders = new BLLOrders();

        //Get the Order's Info by Created By Date
        dtOrders = objBLLOrders.getAllOrdersByCreationDate(start, end);

        //Get and Set the Total Period Balance
        GetAndSetTotalPeriodBal(dtOrders);

        if ((dtOrders != null) && (dtOrders.Rows.Count > 0))
        {
            gridview1.DataSource = dtOrders;
            gridview1.DataBind();


            Label lblPageCount = (Label)gridview1.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)gridview1.BottomPagerRow.FindControl("lblTotalRecords");

            lblTotalRecords.Text = dtOrders.Rows.Count.ToString();
            lblPageCount.Text = gridview1.PageCount.ToString();

            DropDownList ddlPage = bindPageDropDown(lblTotalRecords.Text);
            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                ddlPage.SelectedValue = Session["ddlPage"].ToString();
            }
            gridview1.BottomPagerRow.Visible = true;
            if (gridview1.PageCount == 1)
            {
                ImageButton imgPrev = (ImageButton)gridview1.BottomPagerRow.FindControl("btnPrev");
                ImageButton imgNext = (ImageButton)gridview1.BottomPagerRow.FindControl("btnNext");

                imgNext.Enabled = false;
                imgPrev.Enabled = false;
            }
        }
        else
        {
            gridview1.DataSource = dtOrders;
            gridview1.DataBind();
        }
    }

    private DropDownList bindPageDropDown(string strTotalCount)
    {
        DropDownList ddlPage = null;

        try
        {
            ddlPage = (DropDownList)gridview1.BottomPagerRow.Cells[0].FindControl("ddlPage");
            ddlPage.Items.Insert(0, "5");
            ddlPage.Items.Insert(1, "10");
            ddlPage.Items.Insert(2, "20");
            ddlPage.Items.Insert(3, "30");
            ddlPage.Items.Insert(4, "50");
            ListItem objList = new ListItem("All", strTotalCount);
            ddlPage.Items.Insert(5, objList);            
        }
        catch (Exception ex)
        {
            return null;
        }

        return ddlPage;
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
    }

    #region Timeframe Filter

    protected DateTime GetTimeframeStartDate(DropDownList ddlMonth, DropDownList ddlYear)
    {
        DateTime dtStartDate;

        dtStartDate = Convert.ToDateTime(ddlMonth.SelectedValue + "/1/" + ddlYear.SelectedValue + " 00:00:00");

        return dtStartDate;
    }

    protected DateTime GetTimeframeEndDate(DropDownList ddlMonth, DropDownList ddlYear)
    {
        DateTime dtEndDate = GetTimeframeStartDate(ddlMonth, ddlYear);

        dtEndDate = Convert.ToDateTime(dtEndDate.ToShortDateString() + " 23:59:59");

        dtEndDate = dtEndDate.AddMonths(1).AddDays(-1);

        return dtEndDate;
    }

    #endregion

    #region Pager Row Process

    private void PageIndexChanging(int newPageIndex)
    {
        // check to prevent form the NewPageIndex out of the range
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= gridview1.PageCount ? gridview1.PageCount - 1 : newPageIndex;

        // specify the NewPageIndex
        gridview1.PageIndex = newPageIndex;

        gridview1_DataBind();
    }

    #endregion

    protected string GetOrderDate(DateTime createdOn, string orderId, string providerID)
    {
        string url = "orderManagement.aspx";

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

    protected string ShowCommissionAndNetAmount(object orderStatus, object currencyCode, object value)
    {
        double totalCom = 0;

        try
        {
            if (orderStatus.ToString() == "Payment Unsuccessful" || orderStatus.ToString() == "Cancelled")
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
            if (orderStatus.ToString() == "Payment Unsuccessful" || orderStatus.ToString() == "Cancelled")
            {
                return "0.00";
            }
            else if (orderStatus.ToString() == "Confirmed" && orderType.ToString() == "Cash")
            {
                double subTotal = Convert.ToDouble(totalAmt);

                double CommRate = Convert.ToDouble(ConfigurationSettings.AppSettings["CommissionFee"].ToString());

                subTotalCom = -(subTotal * CommRate) / 100;

            }
            else
            {

                double subTotal = Convert.ToDouble(totalAmt);

                double CommRate = Convert.ToDouble(ConfigurationSettings.AppSettings["CommissionFee"].ToString());

                subTotalCom = Convert.ToDouble(totalAmt) - ((subTotal * CommRate) / 100);
            }
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
            double CommRate = Convert.ToDouble(ConfigurationSettings.AppSettings["CommissionFee"].ToString());
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
            lblPeriodSales.Text = "$" + String.Format("{0:0.00}", bTotalPeriodBal - bTotalCash).ToString() + " CAD";


        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gridview1.PageIndex = 0;
            DropDownList ddlPage = (DropDownList)this.gridview1.BottomPagerRow.Cells[0].FindControl("ddlPage");
            Session["ddlPage"] = ddlPage.SelectedValue.ToString();
            setPageValueInCookie(ddlPage);
            this.gridview1_DataBind();
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
        }
    }

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPage = (TextBox)this.gridview1.BottomPagerRow.Cells[0].FindControl("txtPage");
            int intPageindex = 0;
            if (txtPage.Text != null && txtPage.Text.ToString() != "")
            {
                intPageindex = Convert.ToInt32(txtPage.Text);
                if (intPageindex > 0)
                {
                    intPageindex--;
                }
            }

            if (intPageindex < this.gridview1.PageCount && intPageindex > 0)
            {
                this.gridview1.PageIndex = intPageindex;
            }
            else
            {
                this.gridview1.PageIndex = 0;
            }


            txtPage.Text = (this.gridview1.PageIndex + 1).ToString();

            if (this.gridview1.PageIndex == this.gridview1.PageCount - 1)
            {
                displayNext = false;
                displayPrevious = true;
            }

            else if (this.gridview1.PageIndex == 0)
            {
                displayPrevious = false;
                displayNext = true;
            }
            else
            {
                displayPrevious = true;
                displayNext = true;
            }
            this.gridview1_DataBind();

            //Save the Grid Goto Textbos value into the Cache
            Cache["txtPage"] = txtPage.Text.Trim();
        }
        catch (Exception ex)
        {
        }
    }

    private void setPageValueInCookie(DropDownList ddlPage)
    {
        HttpCookie cookie = Request.Cookies["ddlPage"];
        if (cookie == null)
        {
            cookie = new HttpCookie("ddlPage");
        }
        cookie.Expires = DateTime.Now.AddYears(1);
        Response.Cookies.Add(cookie);
        cookie["ddlPage"] = ddlPage.SelectedValue.ToString();
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
            if (e.NewPageIndex == this.gridview1.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gridview1.PageIndex = e.NewPageIndex;
            this.gridview1_DataBind();
            TextBox txtPage = (TextBox)this.gridview1.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (e.NewPageIndex + 1).ToString();

            //Save the Grid Goto Textbos value into the Cache
            Cache["txtPage"] = txtPage.Text.Trim();
        }
        catch (Exception ex)
        { }
    }
    protected void btnSelect_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //gridview1.DataBound += new EventHandler(gridview1_DataBound);
            gridview1_DataBind();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
}