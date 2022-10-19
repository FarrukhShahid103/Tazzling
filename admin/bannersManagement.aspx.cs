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

public partial class bannersManagement : System.Web.UI.Page
{
    #region Global Variables
    BLLDealBanner obj = new BLLDealBanner();    
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
        ddlDealBannerCity.DataSource = objCities.getAllCities();
        ddlDealBannerCity.DataValueField = "cityId";
        ddlDealBannerCity.DataTextField = "cityName";
        ddlDealBannerCity.DataBind();

        ddlDealBannerCity.SelectedValue = "337";
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
                dtCities = obj.getAllDealBanner();
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
                txtDealBannerTitleSearch.Enabled = true;

            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnSearch.Enabled = false;                
                txtDealBannerTitleSearch.Enabled = false;
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
        ListItem objList = new ListItem("All", obj.getAllDealBanner().Rows.Count.ToString());
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
            obj.banner_ID = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value.ToString());
            if (obj.deleteDealBanner() == -1)
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
            dtCities = obj.getAllDealBanner();
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
			strQuery += "banner_ID,";			
            strQuery += "cityName,";                        
            strQuery += "banner_link, ";
            strQuery += "banner_image, ";            
            strQuery += "banner_startTime, ";
            strQuery += "banner_city, ";
            strQuery += "banner_endTime ";

            strQuery += "FROM dealBanners ";
            strQuery += "INNER join city On dealBanners.banner_city= city.[cityId] ";  
            

            if (txtDealBannerTitleSearch.Text.Trim() != "")
            {

                if (txtDealBannerTitleSearch.Text.Trim() != "")
                {
                    strQuery += " where banner_link LIKE '%" + txtDealBannerTitleSearch.Text.ToString().Trim() + "%'";
                }              
                strQuery += "order by banner_ID desc	";
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
            txtDealBannerTitleSearch.Text = "";            
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
                obj.banner_ID = Convert.ToInt64(ViewState["banner_ID"]);                
                obj.banner_link = txtLink.Text.Trim();
                obj.banner_startTime = Convert.ToDateTime(txtStartDate.Text.Trim());
                obj.banner_endTime = Convert.ToDateTime(txtEndDate.Text.Trim());                
                try
                {
                    obj.banner_city = Convert.ToInt32(ddlDealBannerCity.SelectedValue.Trim());
                }
                catch (Exception ex)
                { }
                //obj.dealId = Convert.ToInt32(ddlDeals.SelectedValue.Trim());                        
                if (fuBannerImage1.HasFile)
                {
                    //string strResID = this.ddlSelectRes.SelectedItem.Value.Trim();
                    string[] strExtension = fuBannerImage1.FileName.Split('.');
                    obj.banner_image = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                    string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\Banner\\";
                    if (!Directory.Exists(strthumbSave))
                    {
                        Directory.CreateDirectory(strthumbSave);
                    }
                    string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\Banner\\" + fuBannerImage1.FileName;
                    fuBannerImage1.SaveAs(strSrcPath);
                    string SrcFileName = fuBannerImage1.FileName;
                    Misc.CreateBannerImage(strSrcPath, strthumbSave, SrcFileName, obj.banner_image);
                    File.Delete(strSrcPath);
                }
                else
                {
                    if (ViewState["banner_image"] != null)
                    {
                        obj.banner_image = ViewState["banner_image"].ToString();
                    }
                }
                if (obj.updateDealBanner() == -1)
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
                dtCities = obj.getAllDealBanner();
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
            DataTable dtDealBanner = null;
            obj.banner_ID = Convert.ToInt64(pageGrid.SelectedDataKey.Value);
            dtDealBanner = obj.getDealBannerByID();
            ViewState["banner_ID"] = pageGrid.SelectedDataKey.Value.ToString();
            if (dtDealBanner != null && dtDealBanner.Rows.Count > 0)
            {                
                try
                {
                    if (dtDealBanner.Rows[0]["banner_city"] != null
                        && dtDealBanner.Rows[0]["banner_city"].ToString().Trim() != ""
                        && dtDealBanner.Rows[0]["banner_city"].ToString().Trim() != "0")
                    {
                        ddlDealBannerCity.SelectedValue = dtDealBanner.Rows[0]["banner_city"].ToString().Trim();
                    }
                }
                catch (Exception ex)
                {
 
                }                
                txtLink.Text = dtDealBanner.Rows[0]["banner_link"].ToString();
                try
                {
                    if (dtDealBanner.Rows[0]["banner_startTime"] != null && dtDealBanner.Rows[0]["banner_startTime"].ToString().Trim() != "")
                    {
                        txtStartDate.Text = Convert.ToDateTime(dtDealBanner.Rows[0]["banner_startTime"].ToString().Trim()).ToString("MM-dd-yyyy");
                    }
                    if (dtDealBanner.Rows[0]["banner_endTime"] != null && dtDealBanner.Rows[0]["banner_endTime"].ToString().Trim() != "")
                    {
                        txtEndDate.Text = Convert.ToDateTime(dtDealBanner.Rows[0]["banner_endTime"].ToString().Trim()).ToString("MM-dd-yyyy");
                    }
                }
                catch (Exception ex)
                { }
                imgUpload1.Visible = true;
                ViewState["banner_image"] = dtDealBanner.Rows[0]["banner_image"].ToString();
                imgUpload1.Src = "../Images/Banner/" + dtDealBanner.Rows[0]["banner_image"].ToString();
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
            txtLink.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            imgUpload1.Src = "";
            imgUpload1.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            ddlDealBannerCity.SelectedValue = "337";
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
                    obj.banner_ID = Convert.ToInt32(lblID.Text);
                    result = obj.deleteDealBanner();
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
                obj.banner_endTime = Convert.ToDateTime(txtEndDate.Text.Trim());
                obj.banner_startTime = Convert.ToDateTime(txtStartDate.Text.Trim());
                try
                {
                    obj.banner_city = Convert.ToInt32(ddlDealBannerCity.SelectedValue.Trim());
                }
                catch (Exception ex)
                {
                    obj.banner_city = 337;
                }                
                obj.banner_link = txtLink.Text.Trim();                
                if (fuBannerImage1.HasFile)
                {
                    //string strResID = this.ddlSelectRes.SelectedItem.Value.Trim();
                    string[] strExtension = fuBannerImage1.FileName.Split('.');
                    obj.banner_image = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                    string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\Banner\\";
                    if (!Directory.Exists(strthumbSave))
                    {
                        Directory.CreateDirectory(strthumbSave);
                    }
                    string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\Banner\\" + fuBannerImage1.FileName;
                    fuBannerImage1.SaveAs(strSrcPath);
                    string SrcFileName = fuBannerImage1.FileName;
                    Misc.CreateBannerImage(strSrcPath, strthumbSave, SrcFileName, obj.banner_image);
                    File.Delete(strSrcPath);
                }

                if (obj.createDealBanner() == -1)
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
