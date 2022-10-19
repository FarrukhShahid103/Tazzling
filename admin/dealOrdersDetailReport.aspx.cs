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

public partial class dealOrdersDetailReport : System.Web.UI.Page
{
    BLLDealOrders objBLLDealOrders = new BLLDealOrders();

    public bool displayPrevious = false;
    public bool displayNext = true;

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllBusinessInfoAndFillGrid();
        }

        if (Request.QueryString["did"] != null)
        {
            try
            {
                if (int.Parse(Request.QueryString["did"].ToString().Trim()) == 0)
                    Response.Redirect("dealOrdersMgmtByUsers.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Redirect("dealOrdersMgmtByUsers.aspx", false);
            }
        }
        else
            Response.Redirect("dealOrdersMgmtByUsers.aspx", false);
    }

    protected bool getDetailStatus(object status)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "successful" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "refunded")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }

    protected bool getPayBackStatus(object status, object psgTranNo)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "successful" && psgTranNo.ToString().Trim() != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    #region "Get and Set the Deal Order Info here"

    protected void GetAllBusinessInfoAndFillGrid()
    {
        try
        {
            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(Session["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                Session["ddlPage"] = Misc.pageSize;
            }
            DataTable dtUser;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                dtUser = dtSearchhDealInfoByDifferentParams();
                dv = new DataView(dtUser);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
            }
            else
            {
                dtUser = Misc.search(ViewState["Query"].ToString());
                dv = new DataView(dtUser);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                pageGrid.DataSource = dv;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtUser.Rows.Count.ToString();
                lblPageCount.Text = pageGrid.PageCount.ToString();

                DropDownList ddlPage = bindPageDropDown();
                if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = Session["ddlPage"].ToString();
                }
                pageGrid.BottomPagerRow.Visible = true;
                if (pageGrid.PageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }

                btnSearch.Enabled = true;
                this.txtUserAcc.Enabled = true;
                this.ddlPayment.Enabled = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
            }

        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private DropDownList bindPageDropDown()
    {
        try
        {
            DropDownList ddlPage = (DropDownList)pageGrid.BottomPagerRow.Cells[0].FindControl("ddlPage");

            ddlPage.Items.Insert(0, "5");
            ddlPage.Items.Insert(1, "10");
            ddlPage.Items.Insert(2, "20");
            ddlPage.Items.Insert(3, "30");
            ddlPage.Items.Insert(4, "50");
            ListItem objList = new ListItem("All", dtSearchhDealInfoByDifferentParams().Rows.Count.ToString());
            ddlPage.Items.Insert(5, objList);
            return ddlPage;
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return null;
        }
    }

    protected void pageGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            if (e.NewPageIndex == 0)
            {
                displayPrevious = false;
            }
            else
            {
                displayPrevious = true;
            }
            if (e.NewPageIndex == pageGrid.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.pageGrid.PageIndex = e.NewPageIndex;
            this.GetAllBusinessInfoAndFillGrid();
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (e.NewPageIndex + 1).ToString();
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #endregion

    #region Event to take to required page
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            int intPageindex = 0;
            if (txtPage.Text != null && txtPage.Text.ToString() != "")
            {
                intPageindex = Convert.ToInt32(txtPage.Text);
                if (intPageindex > 0)
                {
                    intPageindex--;
                }
            }

            if (intPageindex < pageGrid.PageCount && intPageindex > 0)
            {
                pageGrid.PageIndex = intPageindex;
            }
            else
            {
                pageGrid.PageIndex = 0;
            }


            txtPage.Text = (pageGrid.PageIndex + 1).ToString();

            if (pageGrid.PageIndex == pageGrid.PageCount - 1)
            {
                displayNext = false;
                displayPrevious = true;
            }

            else if (pageGrid.PageIndex == 0)
            {
                displayPrevious = false;
                displayNext = true;
            }
            else
            {
                displayPrevious = true;
                displayNext = true;
            }
            this.GetAllBusinessInfoAndFillGrid();
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    #region Function to Sort Grid

    protected void pageGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, DESCENDING);
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, ASCENDING);
        }
    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            DataTable dtUser = null;
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            if (ViewState["Query"] != null)
            {
                dtUser = Misc.search(ViewState["Query"].ToString());
            }
            else
            {
                dtUser = dtSearchhDealInfoByDifferentParams();
            }

            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(Session["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                Session["ddlPage"] = Misc.pageSize;
            }

            if (pageGrid.PageIndex == 0)
            {
                displayPrevious = false;
            }
            else
            {
                displayPrevious = true;
                txtPage.Text = (pageGrid.PageIndex + 1).ToString();
            }
            if (pageGrid.PageIndex == pageGrid.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
                txtPage.Text = (pageGrid.PageIndex + 1).ToString();
            }
            DataView dv = new DataView(dtUser);
            dv.Sort = sortExpression + direction;
            ViewState["Direction"] = sortExpression + direction;
            pageGrid.DataSource = dv;
            pageGrid.DataBind();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtUser.Rows.Count.ToString();
                lblPageCount.Text = pageGrid.PageCount.ToString();

                DropDownList ddlPage = bindPageDropDown();
                if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = Session["ddlPage"].ToString();
                }
                pageGrid.BottomPagerRow.Visible = true;
                if (pageGrid.PageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    #region Event of dropdown to take to selected page

    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            pageGrid.PageIndex = 0;
            DropDownList ddlPage = (DropDownList)pageGrid.BottomPagerRow.Cells[0].FindControl("ddlPage");
            Session["ddlPage"] = ddlPage.SelectedValue.ToString();
            setPageValueInCookie(ddlPage);
            this.GetAllBusinessInfoAndFillGrid();
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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

    #endregion

    public string getImagePath(object resID, object imgName)
    {
        try
        {
            ArrayList arrImage = new ArrayList();
            arrImage.AddRange(imgName.ToString().Split(','));

            if (arrImage.Count > 0)
            {
                string strImageName = arrImage[0].ToString();

                string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + resID.ToString() + "\\" + strImageName;
                if (File.Exists(path))
                {
                    return "../Images/dealFood/" + resID.ToString() + "/" + strImageName;
                }
                else
                {
                    return "../Images/dealFood/noMenuImage.gif";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return "";
        }
        return "";
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        lblMessage.Visible = false;
        imgGridMessage.Visible = false;

        ViewState["Query"] = null;

        GetAllBusinessInfoAndFillGrid();

    }


    protected void imgbtnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {


        DataTable dtDealOrders = new DataTable("dtDealOrders");
        DataColumn Sr = new DataColumn("Sr.");
        DataColumn userEmail = new DataColumn("User Email");
        DataColumn Qty = new DataColumn("Qty");
        DataColumn CreditCardAmount = new DataColumn("Credit Card Amount");
        DataColumn Comission = new DataColumn("Comission");
        DataColumn Credits = new DataColumn("Credits");
        DataColumn Total = new DataColumn("Total");
        DataColumn ComissionEarned = new DataColumn("Comission Earned");
        DataColumn TastyCreditEarned = new DataColumn("Tasty Credit Earned");
        DataColumn OrderedDate = new DataColumn("Ordered Date");
        DataColumn Payment = new DataColumn("Payment");

        dtDealOrders.Columns.Add(Sr);
        dtDealOrders.Columns.Add(userEmail);
        dtDealOrders.Columns.Add(Qty);
        dtDealOrders.Columns.Add(CreditCardAmount);
        dtDealOrders.Columns.Add(Comission);
        dtDealOrders.Columns.Add(Credits);
        dtDealOrders.Columns.Add(Total);
        dtDealOrders.Columns.Add(ComissionEarned);
        dtDealOrders.Columns.Add(TastyCreditEarned);
        dtDealOrders.Columns.Add(OrderedDate);
        dtDealOrders.Columns.Add(Payment);

        DataRow dRow;

        DataTable dtExcelReport = dtSearchhDealInfoByDifferentParams();

        if (dtExcelReport != null && dtExcelReport.Rows.Count>0)
        {

            for (int i = 0; i < dtExcelReport.Rows.Count; i++)
            {
                dRow = dtDealOrders.NewRow();
                dRow["Sr."] = i + 1;
                dRow["User Email"] = dtExcelReport.Rows[i]["userName"].ToString().Trim();
                dRow["Qty"] = dtExcelReport.Rows[i]["Qty"].ToString().Trim();
                dRow["Credit Card Amount"] = dtExcelReport.Rows[i]["ccCreditUsed"].ToString().Trim();
                dRow["Comission"] = dtExcelReport.Rows[i]["comissionMoneyUsed"].ToString().Trim();
                dRow["Credits"] = dtExcelReport.Rows[i]["tastyCreditUsed"].ToString().Trim();
                dRow["Total"] = dtExcelReport.Rows[i]["totalAmt"].ToString().Trim();
                dRow["Comission Earned"] = dtExcelReport.Rows[i]["comission"].ToString().Trim();
                dRow["Tasty Credit Earned"] = dtExcelReport.Rows[i]["TastyCredit"].ToString().Trim();
                dRow["Ordered Date"] = dtExcelReport.Rows[i]["createdDate"].ToString().Trim();
                dRow["Payment"] = dtExcelReport.Rows[i]["status"].ToString().Trim();
                dtDealOrders.Rows.Add(dRow);
            }
            ExportToUser(dtDealOrders, "Summery.xls");
            //string strPath = AppDomain.CurrentDomain.BaseDirectory + "Admin\\Images\\";
            //if (File.Exists(strPath + "excel.xls"))
            //{
            //    File.Copy(strPath + "excel.xls", strPath + "download.xls", true);
            //    ExcelProcessor.DataTableToExcel(dtDealOrders, strPath + "download.xls");                
            //    ExportToUser(strPath + "download.xls", "download.xls");
            //}
        }
     
    }

    private void ExportToUser(DataTable table, string strFileName)
    {
        GridView gv = new GridView();
        gv.DataSource = table;
        gv.DataBind();
        string attachment = "attachment; filename=" + strFileName;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }

    

    private bool ChangeDealOrderStatus(long dOrderId, string strStatus)
    {
        bool bStatus = false;
        try
        {
            BLLDealOrders objBLLDealOrders = new BLLDealOrders();

            objBLLDealOrders.dOrderID = dOrderId;

            objBLLDealOrders.status = strStatus;

            bStatus = objBLLDealOrders.changeDealOrderStatus();
        }
        catch (Exception ex)
        { }
        return bStatus;
    }

    private DataTable dtSearchhDealInfoByDifferentParams()
    {
        DataTable dtDeals = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT ";
            strQuery += " tblDeals.[title] as 'DealName'";
            strQuery += " ,dOrderID,tblDealOrders.dealId,ccCreditUsed,tastyCreditUsed,comissionMoneyUsed,totalAmt,Qty";            
            strQuery += " ,tblDealOrders.createdDate";
            strQuery += " ,[tblDealOrders].[status]";            
            strQuery += " ,tblUserInfo.[userName]";
            strQuery += " ,rtrim(tblUserInfo.firstName) + ' ' + rtrim(tblUserInfo.lastName) as 'Name'";
            strQuery += " ,isnull(tblUserInfo.phoneNo,'') as 'phoneNo'";
            strQuery += " ,tblUserInfo.[userId]";
            strQuery += " ,tblUserInfo.[isActive]";
            strQuery += " ,isnull(tblAffiliatePartnerGained.gainedAmount,0) as 'comission'";
            strQuery += " ,isnull((SELECT gained.gainedAmount FROM  gained where gained.gainedType='Refferal' and gained.orderId = tblDealOrders.dOrderID),0) 'TastyCredit'";
            strQuery += " FROM ";
            strQuery += " tblDealOrders";            
            strQuery += " inner join tblDeals on tblDeals.dealId = tblDealOrders.dealId";
            strQuery += " inner join tblUserInfo on tblUserInfo.userId = tblDealOrders.userId";
            strQuery += " inner join tblUserType on userType.userTypeID = userInfo.userTypeID";
            strQuery += " left outer join tblAffiliatePartnerGained on tblAffiliatePartnerGained.orderId = tblDealOrders.dOrderID";
            strQuery += " left outer join gained on gained.orderId = tblDealOrders.dOrderID";
            strQuery += " where ";

            if (Request.QueryString["did"] != null)
            {
                try
                {
                    if (int.Parse(Request.QueryString["did"].ToString().Trim()) > 0)
                    {
                        strQuery += " tblDealOrders.dealId = " + int.Parse(Request.QueryString["did"].ToString().Trim());
                    }
                }
                catch (Exception ex)
                { }
            }

            if (txtUserAcc.Text.Trim() != "")
            {
                strQuery += " and tblUserInfo.[userName] like '%" + txtUserAcc.Text.Trim() + "%' ";
            }

            //Get the Deal

            if (ddlPayment.SelectedIndex > 0)
            {
                strQuery += " and [tblDealOrders].[status] = '" + ddlPayment.SelectedValue.ToString() + "'";
            }           

            strQuery += " order by tblDealOrders.createdDate desc";

            ViewState["Query"] = strQuery;

            dtDeals = Misc.search(strQuery);

            if (dtDeals != null && dtDeals.Rows.Count > 0)
            {
                string strCreditCard = "";
                object sumObject;
                sumObject = dtDeals.Compute("Sum(ccCreditUsed)", "ccCreditUsed > 0");
                if (sumObject.ToString() != "")
                {
                    strCreditCard += "Total Credit Card=$" + String.Format("{0:0,0}", sumObject);
                }
                else
                {
                    strCreditCard += "Total Credit Card=$0";
                }
                sumObject = dtDeals.Compute("Sum(comissionMoneyUsed)", "comissionMoneyUsed > 0");
                if (sumObject.ToString() != "")
                {
                    strCreditCard += "<br>Total Comission Money=$" + String.Format("{0:0,0}", sumObject);
                }
                else
                {
                    strCreditCard += "<br>Total Comission Money=$0";
                }
                sumObject = dtDeals.Compute("Sum(tastyCreditUsed)", "tastyCreditUsed > 0");
                if (sumObject.ToString() != "")
                {
                    strCreditCard += "<br>Total Tasty Credit=$" + String.Format("{0:0,0}", sumObject);
                }
                else
                {
                    strCreditCard += "<br>Total Tasty Credit=$0";
                }
                sumObject = dtDeals.Compute("Sum(comission)", "comission > 0");
                if (sumObject.ToString() != "")
                {
                    strCreditCard += "<br>Total Comission Earned=$" + String.Format("{0:0,0}", sumObject);
                }
                else
                {
                    strCreditCard += "<br>Total Comission Earned=$0";
                }
                sumObject = dtDeals.Compute("Sum(TastyCredit)", "TastyCredit > 0");
                if (sumObject.ToString() != "")
                {
                    strCreditCard += "<br>Total Tasty Credit Earned=$" + String.Format("{0:0,0}", sumObject);
                }
                else
                {
                    strCreditCard += "<br>Total Tasty Credit Earned=$0";
                }
                lblSummery.Text = strCreditCard.Trim();
            }
            else
            {
                lblSummery.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        return dtDeals;
    }

 
}