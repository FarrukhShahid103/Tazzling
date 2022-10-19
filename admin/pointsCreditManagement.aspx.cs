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
using System.Data.SqlClient;
using SQLHelper;

public partial class admin_pointsCreditManagement : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLMemberPoints objPints = new BLLMemberPoints();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;
    public string strIDs = "";
    public int start = 2;
    public string strtblHide = "none";
    public string strRestHide = "none";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid(0);
        }
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            if (txtSearchFirstName.Text.Trim() != "" || txtSearchLastName.Text.Trim() != "" || txtSearchUserName.Text.Trim() != "")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;
                pageGrid.PageIndex = 0;
                BindSearchedData("0");
            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid(0);
                ViewState["Query"] = null;
            }
        }
        catch (Exception ex)
        {

            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
   
    #region Function to bind Search data in Grid
    private void BindSearchedData(string strRowIndex)
    {
        try
        {
            string strQuery = "";
            if (txtSearchFirstName.Text.Trim() != "" || txtSearchLastName.Text.Trim() != "" || txtSearchUserName.Text.Trim() != "")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;
                strQuery = "Select * from( SELECT ";
                strQuery += " userID, ";
                strQuery += " tblUserType.userTypeID, ";
                strQuery += " tblUserType.userType, ";
                strQuery += " userPassword, ";
                strQuery += " userName, ";
                strQuery += " email, ";
                strQuery += " referralId, ";
                strQuery += " dbo.InitCap(firstname) as firstName, ";
                strQuery += " dbo.InitCap(lastName) as lastName, ";
                strQuery += " friendsReferralId, ";
                strQuery += " countryId, ";
                strQuery += " provinceId, ";
                strQuery += " howYouKnowUs, ";
                strQuery += " creationDate, ";
                strQuery += " createdBy, ";
                strQuery += " modifiedDate, ";
                strQuery += " modifiedBy, ";
                strQuery += " isActive,ROW_NUMBER() OVER(ORDER BY tblUserInfo.userID desc) as RowNum ";
                strQuery += " FROM tblUserInfo ";
                strQuery += " inner join tblUserType on tblUserType.userTypeID = tblUserInfo.userTypeID ";
                strQuery += " where isDeleted = 0 and isActive = 1 and (tblUserType.userTypeId = 3 or tblUserType.userTypeId = 4 or tblUserType.userTypeId =5) ";

                if (txtSearchFirstName.Text.Trim() != "")
                {
                    strQuery += " and firstName like '%" + txtSearchFirstName.Text.Trim() + "%' ";
                }
                if (txtSearchLastName.Text.Trim() != "")
                {
                    strQuery += " and lastName like '%" + txtSearchLastName.Text.Trim() + "%' ";
                }
                if (txtSearchUserName.Text.Trim() != "")
                {
                    strQuery += " and userName like '%" + txtSearchUserName.Text.Trim() + "%' ";
                }

                strQuery += ") as DerivedTableName";
                int strStartIndex = (Convert.ToInt32(strRowIndex) * pageGrid.PageSize) + 1;
                int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * pageGrid.PageSize;
                strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;
                strQuery += " order by userID desc ";

                strQuery += " SELECT	'Return Value' =  COUNT(userID) ";
                strQuery += " FROM userInfo ";
                strQuery += " inner join userType on userType.userTypeID = userInfo.userTypeID ";
                strQuery += " where isDeleted = 0 and isActive = 1 and (userType.userTypeId = 2 or userType.userTypeId = 3 or userType.userTypeId = 4 or userType.userTypeId = 5 or userType.userTypeId = 6 or userType.userTypeId = 7) ";

                if (txtSearchFirstName.Text.Trim() != "")
                {
                    strQuery += " and firstName like '%" + txtSearchFirstName.Text.Trim() + "%' ";
                }
                if (txtSearchLastName.Text.Trim() != "")
                {
                    strQuery += " and lastName like '%" + txtSearchLastName.Text.Trim() + "%' ";
                }
                if (txtSearchUserName.Text.Trim() != "")
                {
                    strQuery += " and userName like '%" + txtSearchUserName.Text.Trim() + "%' ";
                }

            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid(0);
                ViewState["Query"] = null;
                return;
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

            DataSet dst = Misc.searchDataSet(strQuery);

            DataTable dtUser = dst.Tables[0];

            if ((dtUser != null) &&
                (dtUser.Columns.Count > 0) &&
                (dtUser.Rows.Count > 0))
            {
                pageGrid.DataSource = dtUser.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                string strTotalOrders = dst.Tables[1].Rows[0][0].ToString();
                int intpageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(strTotalOrders) / pageGrid.PageSize));
                lblPageCount.Text = intpageCount.ToString();

                ViewState["PageCount"] = intpageCount.ToString();

                lblTotalRecords.Text = strTotalOrders;

                ViewState["Query"] = strQuery;
                DropDownList ddlPage = bindPageDropDown(strTotalOrders);

                if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = ViewState["ddlPage"].ToString();
                }
                pageGrid.BottomPagerRow.Visible = true;

                if (intpageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
            }
            btnShowAll.Visible = true;
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    protected void bindGrid(int intPageNumber)
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
            DataSet dst = null;
            DataTable dtUser;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                dst = getMemberResturantAndSalesUsers(intPageNumber, pageGrid.PageSize);
                dtUser = dst.Tables[0];
                dv = new DataView(dtUser);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = false;
            }
            else
            {
                BindSearchedData(intPageNumber.ToString());
                btnShowAll.Visible = true;
                return;
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                pageGrid.DataSource = dv;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
                string strTotalOrders = "";
                if (dst != null && dst.Tables[1] != null)
                {
                    strTotalOrders = dst.Tables[1].Rows[0][0].ToString();
                }
                else
                {
                    strTotalOrders = dtUser.Rows.Count.ToString();
                }
                lblTotalRecords.Text = strTotalOrders;
                int intpageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(strTotalOrders) / pageGrid.PageSize));
                lblPageCount.Text = intpageCount.ToString();
                ViewState["PageCount"] = intpageCount.ToString();
                DropDownList ddlPage = bindPageDropDown(strTotalOrders);
                if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = Session["ddlPage"].ToString();
                }
                pageGrid.BottomPagerRow.Visible = true;
                if (intpageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }                
                btnSearch.Enabled = true;
                txtSearchFirstName.Enabled = true;
                txtSearchLastName.Enabled = true;
                txtSearchUserName.Enabled = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();                
                btnSearch.Enabled = false;
                txtSearchFirstName.Enabled = false;
                txtSearchLastName.Enabled = false;
                txtSearchUserName.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected DataSet getMemberResturantAndSalesUsers(int intStartIndex, int intMaxRecords)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@startRowIndex", intStartIndex);
            param[1] = new SqlParameter("@maximumRows", intMaxRecords);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spgetMemberResturantAndSalesUsers", param);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }


    protected void pageGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (start <= 9)
            {
                strIDs += "*ctl00_ContentPlaceHolder1_pageGrid_ctl0" + start + "_RowLevelCheckBox";
            }
            else
            {
                strIDs += "*ctl00_ContentPlaceHolder1_pageGrid_ctl" + start + "_RowLevelCheckBox";
            }

            start++;
            hiddenIds.Text = strIDs;
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    protected void pageGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            int intPageCount = 0;
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            int intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());
            if (ViewState["PageCount"] != null)
            {
                intPageCount = Convert.ToInt32(ViewState["PageCount"].ToString());
            }
            intCurrentPage += e.NewPageIndex;
            if (intCurrentPage == 1)
            {
                displayPrevious = false;
            }
            else
            {
                displayPrevious = true;
            }
            if (intCurrentPage == intPageCount)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            if (intCurrentPage == 0)
            {
                intCurrentPage = 1;
            }
            this.bindGrid(intCurrentPage - 1);
            txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (intCurrentPage).ToString();
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

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
            this.bindGrid(0);
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
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

    #region Event to take to required page
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");

            if (txtPage.Text != null && txtPage.Text.ToString() != "")
            {
                int intPageCount = 0;
                int intCurrentPage = 0;
                try
                {
                    intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());
                }
                catch (Exception ex)
                {
                    intCurrentPage = 1;
                }
                if (ViewState["PageCount"] != null)
                {
                    intPageCount = Convert.ToInt32(ViewState["PageCount"].ToString());
                }
                if (intCurrentPage == 1)
                {
                    displayPrevious = false;
                }
                else
                {
                    displayPrevious = true;
                }
                if (intCurrentPage == intPageCount)
                {
                    displayNext = false;
                }
                else
                {
                    displayNext = true;
                }
                if (intCurrentPage == 0)
                {
                    intCurrentPage = 1;
                }
                this.bindGrid(intCurrentPage - 1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = intCurrentPage.ToString();
            }
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
    protected void btnShowAll_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Query"] = null;
        lblMessage.Visible = false;
        imgGridMessage.Visible = false;
        pageGrid.PageIndex = 0;
        bindGrid(0);
        txtSearchUserName.Text = "";
        txtSearchFirstName.Text = "";
        txtSearchLastName.Text = "";
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
                dtUser = obj.getAllUsers();
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

                DropDownList ddlPage = bindPageDropDown("0");
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
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
    private DropDownList bindPageDropDown(string strTotalRecords)
    {
        try
        {
            DropDownList ddlPage = (DropDownList)pageGrid.BottomPagerRow.Cells[0].FindControl("ddlPage");

            ddlPage.Items.Insert(0, "5");
            ddlPage.Items.Insert(1, "10");
            ddlPage.Items.Insert(2, "20");
            ddlPage.Items.Insert(3, "30");
            ddlPage.Items.Insert(4, "50");
            ListItem objList = new ListItem("All", strTotalRecords);
            ddlPage.Items.Insert(5, objList);
            return ddlPage;
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return null;
        }
    }

    
    protected void pageGrid_Login(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataTable dtAdmin = (DataTable)Session["user"];
            if (dtAdmin != null)
            {
                TextBox txtFoodCredit = (TextBox)pageGrid.Rows[e.NewEditIndex].FindControl("txtFoodCredit");
                TextBox txtDescription = (TextBox)pageGrid.Rows[e.NewEditIndex].FindControl("txtDescription");                
                objPints.createdBy = Convert.ToInt64(dtAdmin.Rows[0]["userId"].ToString());

                BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                bllKarma.userId = Convert.ToInt64(pageGrid.DataKeys[e.NewEditIndex].Value);
                bllKarma.karmaPoints = Convert.ToInt32(txtFoodCredit.Text.Trim());
                bllKarma.karmaPointsType = txtDescription.Text.Trim();
                bllKarma.createdBy = Convert.ToInt64(pageGrid.DataKeys[e.NewEditIndex].Value);
                bllKarma.createdDate = DateTime.Now;
                bllKarma.createKarmaPoints();
                               
                TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                int intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());
                bindGrid(intCurrentPage - 1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = intCurrentPage.ToString();

                lblMessage.Text = "Points are added to user account.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                Response.Redirect(ResolveUrl("~/Admin/Default.aspx"), false);
            }
        }
        catch (Exception ex)
        {

            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
        
    protected string getAvailableFoodCredit(object userID)
    {
        string strFoodCredit = "0";
        try
        {
            BLLKarmaPoints objkarmaPoints = new BLLKarmaPoints();
            objkarmaPoints.userId = Convert.ToInt32(userID.ToString());
            DataTable dtKarmaPoints = objkarmaPoints.getKarmaPointsTotalByUserId();
            if (dtKarmaPoints != null && dtKarmaPoints.Rows.Count > 0 && dtKarmaPoints.Rows[0][0].ToString() != "")
            {
               return dtKarmaPoints.Rows[0][0].ToString();                
            }
            else
            {
                return "0";
            }
         
        }
        catch (Exception ex)
        {         
        }
        return strFoodCredit;
    }
}
