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

public partial class admin_cityManagement : System.Web.UI.Page
{
    #region Global Variables
    BLLCities obj = new BLLCities();
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
            bindGrid();
            bindSearchProvinces();
        }
    }
    #endregion

    protected void bindSearchProvinces()
    {
        try 
        {
            ddlProvinceSearch.DataSource = Misc.getProvincesByCountryID(2).DefaultView;
            ddlProvinceSearch.DataTextField = "provinceName";
            ddlProvinceSearch.DataValueField = "provinceId";
            ddlProvinceSearch.DataBind();

            ddlProvinceSearch.Items.Insert(0, "Select One");
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

    protected void bindProvinces()
    {
        try
        {
            ddlProvince.DataSource = Misc.getProvincesByCountryID(2).DefaultView;
            ddlProvince.DataTextField = "provinceName";
            ddlProvince.DataValueField = "provinceId";
            ddlProvince.DataBind();

            ddlProvince.Items.Insert(0, "Select One");
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

    protected string GetTaxRates(object taxRates)
    {
        if (taxRates == null || taxRates.ToString().Trim() == "")
        {
            return "0";
        }
        return taxRates.ToString();
    }

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
                dtCities = obj.getAllCitiesForAdmin();
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
                ddlProvinceSearch.Enabled = true;
                txtCityNameSearch.Enabled = true;

            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnSearch.Enabled = false;
                ddlProvinceSearch.Enabled = false;
                txtCityNameSearch.Enabled = false;
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
        ListItem objList = new ListItem("All", obj.getAllCitiesForAdmin().Rows.Count.ToString());
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
            obj.cityId = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value.ToString());
            if (obj.deleteCity() == -1)
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
            dtCities = obj.getAllCitiesForAdmin();
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
			strQuery += "cityId,";
			strQuery += "cityName,";		
			strQuery += "city.provinceId,";
			strQuery += "provinceName, ";
            strQuery += "cityDescription ";							
		    strQuery += "FROM city ";
		    strQuery += "inner join province on province.provinceId = city.provinceId ";

            if (ddlProvinceSearch.SelectedIndex != 0 || txtCityNameSearch.Text.Trim() != "")
            {

                if (txtCityNameSearch.Text.Trim() != "")
                {
                    strQuery += " where cityName LIKE '%" + txtCityNameSearch.Text.ToString().Trim().Replace("'", "''") + "%'";
                }

                if (ddlProvinceSearch.SelectedIndex != 0)
                {
                    if (strQuery.Contains("where"))
                    {
                        strQuery += " and province.provinceId ='" + ddlProvinceSearch.SelectedValue.ToString() + "' ";
                    }
                    else
                    {
                        strQuery += " where province.provinceId ='" + ddlProvinceSearch.SelectedValue.ToString() + "' ";
                    }

                }

                strQuery += "order by cityName asc	";
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
            txtCityNameSearch.Text = "";
            ddlProvinceSearch.SelectedIndex = 0;
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
            obj.cityId = Convert.ToInt32(ViewState["cityId"]);
            obj.cityName = txtCity.Text.Trim();
            obj.cityDescription = txtDescription.Text.Trim();
            obj.provinceId = Convert.ToInt32(ddlProvince.SelectedValue.Trim());
            if (lblCityNameToUpdate.Text.ToString()!=txtCity.Text.ToString() && obj.getCityByName())
            {
                trErrorMsg.Visible = true;
                lblErrorMsg.Text = "City with this name in this province already exists.";
                return;
            }
            if (obj.updateCity() == -1)
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
                dtCities = obj.getAllCitiesForAdmin();
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
            bindProvinces();

            DataTable dtCity = null;
            obj.cityId = Convert.ToInt32(pageGrid.SelectedDataKey.Value);
            dtCity = obj.getCityByCityId();
            ViewState["cityId"] = pageGrid.SelectedDataKey.Value.ToString(); 
            txtCity.Text = dtCity.Rows[0]["cityName"].ToString();
            txtDescription.Text = dtCity.Rows[0]["cityDescription"].ToString();
            ddlProvince.SelectedValue = dtCity.Rows[0]["provinceId"].ToString();
            lblCityNameToUpdate.Text = dtCity.Rows[0]["cityName"].ToString();

            pnlForm.Visible = true;
            pnlGrid.Visible = false;
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            trUpdate.Visible = true;
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
            bindProvinces();
            txtCity.Text = "";
            txtDescription.Text = "";
            ddlProvince.SelectedIndex = 0;
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            trUpdate.Visible = false;

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
                    obj.cityId = Convert.ToInt32(lblID.Text);
                    result = obj.deleteCity();
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
            obj.cityName = txtCity.Text.Trim();
            obj.provinceId = Convert.ToInt32(ddlProvince.SelectedValue.ToString());
            obj.cityDescription = txtDescription.Text.Trim();
            if (obj.getCityByName())
            {
                trErrorMsg.Visible = true;
                lblErrorMsg.Text = "City with this name in this province already exists.";
                return;
            }
            
            if (obj.createCity() == -1)
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
