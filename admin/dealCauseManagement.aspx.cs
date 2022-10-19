using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_dealCauseManagement : System.Web.UI.Page
{
    #region Global Variables
    BLLDealCause obj = new BLLDealCause();
    BLLDeals objDeals = new BLLDeals();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;
    public string strIDs = "";
    public int start = 2;
    #endregion

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindCityDD();
            bindGrid();
        }
    }


    private void bindCityDD()
    {
        BLLCities objCities = new BLLCities();
        ddlDealCauseCity.DataSource = objCities.getAllCities();
        ddlDealCauseCity.DataValueField = "cityId";
        ddlDealCauseCity.DataTextField = "cityName";
        ddlDealCauseCity.DataBind();

        ddlDealCauseCity.SelectedValue = "337";
    }

    #endregion
   
    #region Function to Bind Grid
    protected void bindGrid()
    {
        try
        {
            trErrorMsg.Visible = false;
            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(Session["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                Session["ddlPage"] = Misc.pageSize;
            }
            DataTable dtCities;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                dtCities = obj.getAllDealCause();
                dv = new DataView(dtCities);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = false;
            }
            else
            {
                dtCities = Misc.search(ViewState["Query"].ToString());
                dv = new DataView(dtCities);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = true;
            }
            if (dtCities != null && dtCities.Rows.Count > 0)
            {


                pageGrid.DataSource = dv;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtCities.Rows.Count.ToString();
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
                txtDealCauseTitleSearch.Enabled = true;

            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnSearch.Enabled = false;                
                txtDealCauseTitleSearch.Enabled = false;
            }

        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Bind Page Drop Down
    private DropDownList bindPageDropDown()
    {
        DropDownList ddlPage = (DropDownList)pageGrid.BottomPagerRow.Cells[0].FindControl("ddlPage");

        ddlPage.Items.Insert(0, "5");
        ddlPage.Items.Insert(1, "10");
        ddlPage.Items.Insert(2, "20");
        ddlPage.Items.Insert(3, "30");
        ddlPage.Items.Insert(4, "50");
        ListItem objList = new ListItem("All", obj.getAllDealCause().Rows.Count.ToString());
        ddlPage.Items.Insert(5, objList);
        return ddlPage;
    }
    #endregion

    #region Page Grid View Events
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
            this.bindGrid();
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (e.NewPageIndex + 1).ToString();
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
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
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            obj.cause_ID = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value.ToString());
            if (obj.deleteDealCause() == -1)
            {
                lblMessage.Text = "Record has been deleted successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                imgGridMessage.ImageUrl = "images/checked.png";
            }
            else
            {
                lblMessage.Text = "Record could not be deleted.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                imgGridMessage.ImageUrl = "images/error.png";
            }
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
           
            bindGrid();
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
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
            this.bindGrid();
            TextBox txtPage2 = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage2.Text = (pageGrid.PageIndex + 1).ToString();
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

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
        DataTable dtCities = null;
        if (ViewState["Query"] != null)
        {
            dtCities = Misc.search(ViewState["Query"].ToString());
        }
        else
        {
            dtCities = obj.getAllDealCause();
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
        }
        if (pageGrid.PageIndex == pageGrid.PageCount - 1)
        {
            displayNext = false;
        }
        else
        {
            displayNext = true;
        }
        DataView dv = new DataView(dtCities);
        dv.Sort = sortExpression + direction;
        ViewState["Direction"] = sortExpression + direction;
        pageGrid.DataSource = dv;
        pageGrid.DataBind();

        TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
        txtPage.Text = (pageGrid.PageIndex + 1).ToString();
        if (dtCities != null && dtCities.Rows.Count > 0)
        {
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

            lblTotalRecords.Text = dtCities.Rows.Count.ToString();
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
    #endregion

    #region button Search Click Event
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strQuery = "";
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            strQuery += "Select ";
			strQuery += "cause_ID,";
			strQuery += "cause_title,";
            strQuery += "cityName,";            
            strQuery += "cause_shortDescription,";
            strQuery += "cause_longDescription, ";
            strQuery += "cause_link, ";
            strQuery += "cause_image, ";
            strQuery += "dealId, ";
            strQuery += "cause_startTime, ";
            strQuery += "cause_city, ";
            strQuery += "cause_endTime ";

            strQuery += "FROM dealCause ";
            strQuery += "INNER join city On dealCause.cause_city= city.[cityId] ";  
            

            if (txtDealCauseTitleSearch.Text.Trim() != "")
            {

                if (txtDealCauseTitleSearch.Text.Trim() != "")
                {
                    strQuery += " where cause_title LIKE '%" + txtDealCauseTitleSearch.Text.ToString().Trim() + "%'";
                }              
                strQuery += "order by cause_ID desc	";
                pageGrid.PageIndex = 0;
                BindSearchedData(strQuery);
                ViewState["Query"] = strQuery;
            }
            else
            {
                ViewState["Query"] = null;
                this.pageGrid.PageIndex = 0;
                bindGrid();
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    #region Function to bind Search data in Grid
    private void BindSearchedData(string Query)
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

            DataTable dtCities = Misc.search(Query);
            if ((dtCities != null) &&
                (dtCities.Columns.Count > 0) &&
                (dtCities.Rows.Count > 0))
            {
                pageGrid.DataSource = dtCities.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtCities.Rows.Count.ToString();
                lblPageCount.Text = pageGrid.PageCount.ToString();



                pageGrid.PageIndex = 0;

                ViewState["Query"] = Query;
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
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();

            }
            btnShowAll.Visible = true;

        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    #region Button Show All Click
    protected void btnShowAll_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["Query"] = null;
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            pageGrid.PageIndex = 0;
            bindGrid();
            txtDealCauseTitleSearch.Text = "";            
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
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
            this.bindGrid();
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
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

    #region Button Update Click Event
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtEndDate.Text.Trim()) > Convert.ToDateTime(txtStartDate.Text.Trim()))
            {
                obj.cause_ID = Convert.ToInt64(ViewState["cause_ID"]);
                obj.cause_title = txtCauseTitle.Text.Trim();
                obj.cause_link = txtLink.Text.Trim();
                obj.cause_startTime = Convert.ToDateTime(txtStartDate.Text.Trim());
                obj.cause_endTime = Convert.ToDateTime(txtEndDate.Text.Trim());
                obj.cause_shortDescription = txtShortDescription.Text.Trim().Replace("\r\n", "<br>");
                obj.cause_longDescription = txtLongDescription.Text.Trim().Replace("\r\n", "<br>");
                try
                {
                    obj.cause_city = Convert.ToInt32(ddlDealCauseCity.SelectedValue.Trim());
                }
                catch (Exception ex)
                { }
                //obj.dealId = Convert.ToInt32(ddlDeals.SelectedValue.Trim());                        
                if (fuCauseImage1.HasFile)
                {
                    //string strResID = this.ddlSelectRes.SelectedItem.Value.Trim();
                    string[] strExtension = fuCauseImage1.FileName.Split('.');
                    obj.cause_image = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                    string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\Cause\\";
                    if (!Directory.Exists(strthumbSave))
                    {
                        Directory.CreateDirectory(strthumbSave);
                    }
                    string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\Cause\\" + fuCauseImage1.FileName;
                    fuCauseImage1.SaveAs(strSrcPath);
                    string SrcFileName = fuCauseImage1.FileName;
                    Misc.CreateSmallThumbnailDealFood(strSrcPath, strthumbSave, SrcFileName, obj.cause_image);
                    File.Delete(strSrcPath);
                }
                else
                {
                    if (ViewState["cause_image"] != null)
                    {
                        obj.cause_image = ViewState["cause_image"].ToString();
                    }
                }
                if (obj.updateDealCause() == -1)
                {
                    lblMessage.Text = "Record has been updated successfully.";
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    lblMessage.Text = "Record could not be updated successfully.";
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                pnlGrid.Visible = true;
                pnlForm.Visible = false;
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                bindGrid();
            }
            else
            {
                trErrorMsg.Visible = true;
                lblErrorMsg.Text = "End date should be greater than start date.";
                lblErrorMsg.Visible = true;
                Image1.Visible = true;
                Image1.ImageUrl = "images/error.png";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        
    }
    #endregion

    #region Button Cancel Click Event
    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
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
            DataTable dtCities = null;
            if (ViewState["Query"] != null)
            {
                dtCities = Misc.search(ViewState["Query"].ToString());
            }
            else
            {
                dtCities = obj.getAllDealCause();
            }

            trErrorMsg.Visible = false;
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
            lblTotalRecords.Text = dtCities.Rows.Count.ToString();
            lblPageCount.Text = pageGrid.PageCount.ToString();
            pnlGrid.Visible = true;
            pnlForm.Visible = false;
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            cleanForm();
            DataTable dtDealCause = null;
            obj.cause_ID = Convert.ToInt64(pageGrid.SelectedDataKey.Value);
            dtDealCause = obj.getDealCauseByID();
            ViewState["cause_ID"] = pageGrid.SelectedDataKey.Value.ToString();
            if (dtDealCause != null && dtDealCause.Rows.Count > 0)
            {
                txtCauseTitle.Text = dtDealCause.Rows[0]["cause_title"].ToString();
                try
                {
                    if (dtDealCause.Rows[0]["cause_city"] != null
                        && dtDealCause.Rows[0]["cause_city"].ToString().Trim() != ""
                        && dtDealCause.Rows[0]["cause_city"].ToString().Trim() != "0")
                    {
                        ddlDealCauseCity.SelectedValue = dtDealCause.Rows[0]["cause_city"].ToString().Trim();
                    }
                }
                catch (Exception ex)
                {
 
                }
                txtShortDescription.Text = dtDealCause.Rows[0]["cause_shortDescription"].ToString().Replace("<br>", "\r\n");
                txtLongDescription.Text = dtDealCause.Rows[0]["cause_longDescription"].ToString().Replace("<br>", "\r\n");

                txtLink.Text = dtDealCause.Rows[0]["cause_link"].ToString();
                try
                {
                    if (dtDealCause.Rows[0]["cause_startTime"] != null && dtDealCause.Rows[0]["cause_startTime"].ToString().Trim() != "")
                    {
                        txtStartDate.Text = Convert.ToDateTime(dtDealCause.Rows[0]["cause_startTime"].ToString().Trim()).ToString("MM-dd-yyyy");
                    }
                    if (dtDealCause.Rows[0]["cause_endTime"] != null && dtDealCause.Rows[0]["cause_endTime"].ToString().Trim() != "")
                    {
                        txtEndDate.Text = Convert.ToDateTime(dtDealCause.Rows[0]["cause_endTime"].ToString().Trim()).ToString("MM-dd-yyyy");
                    }
                }
                catch (Exception ex)
                { }
                imgUpload1.Visible = true;
                ViewState["cause_image"] = dtDealCause.Rows[0]["cause_image"].ToString();
                imgUpload1.Src = "../Images/Cause/" + dtDealCause.Rows[0]["cause_image"].ToString();
            }
            pnlForm.Visible = true;
            pnlGrid.Visible = false;
            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try 
        {
            cleanForm();
            pnlForm.Visible = true;
            pnlGrid.Visible = false;
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void cleanForm()
    {
        try
        {
            txtCauseTitle.Text = "";
            txtShortDescription.Text = "";
            txtLongDescription.Text = "";
            txtLink.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            imgUpload1.Src = "";
            imgUpload1.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            ddlDealCauseCity.SelectedValue = "337";
        }
        catch (Exception ex)
        { }
    }
    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    {
        int check = 0;
        int result = 0;

        try
        {
            for (int i = 0; i < pageGrid.Rows.Count; i++)
            {
                CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");
                if (chkSub.Checked)
                {
                    Label lblID = (Label)pageGrid.Rows[i].FindControl("lblID1");
                    obj.cause_ID = Convert.ToInt32(lblID.Text);
                    result = obj.deleteDealCause();
                    if (result == -1)
                    {
                        check++;
                    }
                }
            }
            if (check != 0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Selected records have been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtEndDate.Text.Trim()) > Convert.ToDateTime(txtStartDate.Text.Trim()))
            {
                obj.cause_title = txtCauseTitle.Text.Trim();
                obj.cause_endTime = Convert.ToDateTime(txtEndDate.Text.Trim());
                obj.cause_startTime = Convert.ToDateTime(txtStartDate.Text.Trim());
                try
                {
                    obj.cause_city = Convert.ToInt32(ddlDealCauseCity.SelectedValue.Trim());
                }
                catch (Exception ex)
                {
                    obj.cause_city = 337;
                }
                obj.cause_shortDescription = txtShortDescription.Text.Trim().Replace("\r\n", "<br>");
                obj.cause_link = txtLink.Text.Trim();
                obj.cause_longDescription = txtLongDescription.Text.Trim().Replace("\r\n", "<br>");
                if (fuCauseImage1.HasFile)
                {
                    //string strResID = this.ddlSelectRes.SelectedItem.Value.Trim();
                    string[] strExtension = fuCauseImage1.FileName.Split('.');
                    obj.cause_image = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                    string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\Cause\\";
                    if (!Directory.Exists(strthumbSave))
                    {
                        Directory.CreateDirectory(strthumbSave);
                    }
                    string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\Cause\\" + fuCauseImage1.FileName;
                    fuCauseImage1.SaveAs(strSrcPath);
                    string SrcFileName = fuCauseImage1.FileName;
                    Misc.CreateSmallThumbnailDealFood(strSrcPath, strthumbSave, SrcFileName, obj.cause_image);
                    File.Delete(strSrcPath);
                }

                if (obj.createDealCause() == -1)
                {
                    bindGrid();
                    pnlForm.Visible = false;
                    pnlGrid.Visible = true;
                    lblMessage.Text = "Records has been saved successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    bindGrid();
                    pnlForm.Visible = false;
                    pnlGrid.Visible = true;
                    lblMessage.Text = "Record could not be saved successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                trErrorMsg.Visible = true;
                lblErrorMsg.Text = "End date should be greater than start date.";
                lblErrorMsg.Visible = true;
                Image1.Visible = true;
                Image1.ImageUrl = "images/error.png";
                lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
