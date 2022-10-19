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

public partial class subcategoriesManagement : System.Web.UI.Page
{
    #region Global Variables
    BLLCategories obj = new BLLCategories();
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
            if (Request.QueryString["vscid"] != null && Request.QueryString["vscid"].ToString().Trim() != "")
            {
                bindCategories();
                bindGrid();
            }
            else
            {
                Response.Redirect("categoriesManagement.aspx", true);
            }
        }
    }
    #endregion

    protected void bindCategories()
    {
        try
        {
            ddlCategories.DataSource = obj.getAllCategories().DefaultView;
            ddlCategories.DataTextField = "categoryName";
            ddlCategories.DataValueField = "categoryId";
            ddlCategories.DataBind();
            ddlCategories.Items.Insert(0, "Parent itself");
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur while bind Provinces Please contact you technical support";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region Function to Bind Grid
    protected void bindGrid()
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
            DataTable dtCard;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                if (Request.QueryString["vscid"] != null && Request.QueryString["vscid"].ToString() != "")
                {
                    obj.categoryParentID = Convert.ToInt64(Request.QueryString["vscid"].ToString());
                    dtCard = obj.getSubCategoryByParentID();
                }
                else
                {
                    dtCard = obj.getAllCategories();
                }
                dv = new DataView(dtCard);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = false;
            }
            else
            {
                dtCard = Misc.search(ViewState["Query"].ToString());
                dv = new DataView(dtCard);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = true;
            }
            if (dtCard != null && dtCard.Rows.Count > 0)
            {


                pageGrid.DataSource = dv;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtCard.Rows.Count.ToString();
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
                btnDeleteSelected.Enabled = true;
                btnSearch.Enabled = true;
                
                txtSearchcategoryNumber.Enabled = true;
                
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnDeleteSelected.Enabled = false;
                btnSearch.Enabled = false;
                txtSearchcategoryNumber.Enabled = false;                
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
        ListItem objList = new ListItem("All", obj.getAllCategories().Rows.Count.ToString());
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
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        bool result = false;
        try
        {
            obj.categoryId = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);
            Label lblStatus = (Label)pageGrid.Rows[e.NewEditIndex].FindControl("lblStatus");
            if (lblStatus != null)
            {
                if (lblStatus.Text.ToString() == "True")
                {
                    obj.status = false;
                }
                else
                {
                    obj.status = true;
                }
            }
            result = obj.changeCategoryStatus();
            if (result)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Status has been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;

            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Status has not been changed successfully.";
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
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            obj.categoryId = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value);
            result = obj.deleteCategory();
            if (result!=0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record could not be deleted.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }   
    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCard = null;
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            obj.categoryId = Convert.ToInt64(pageGrid.SelectedDataKey.Value);
           // obj.categoryId = 6;
            dtCard = obj.getCategoryByID();
            if ((dtCard != null) && (dtCard.Columns.Count > 0) && (dtCard.Rows.Count > 0))
            {
                ViewState["categoryId"] = dtCard.Rows[0]["categoryId"];
                txtCategoryName.Text = dtCard.Rows[0]["categoryName"].ToString().Trim();
                txtDescription.Text = dtCard.Rows[0]["categoryDescription"].ToString().Trim();
                if (dtCard.Rows[0]["status"] != null && dtCard.Rows[0]["status"].ToString() != "")
                {
                    cbStatus.Checked = Convert.ToBoolean(dtCard.Rows[0]["status"].ToString().Trim());
                }

                if (dtCard.Rows[0]["categoryParentID"] != null && dtCard.Rows[0]["categoryParentID"].ToString() != "")
                {
                    if (dtCard.Rows[0]["categoryParentID"].ToString().Trim() == "0")
                    {
                        ddlCategories.SelectedIndex = 0;
                    }
                    else
                    {
                        obj.categoryId= Convert.ToInt64(dtCard.Rows[0]["categoryParentID"].ToString().Trim());
                        DataTable dtParent=obj.getCategoryByID();
                        if (dtParent != null && dtParent.Rows.Count > 0)
                        {
                            if (dtParent.Rows[0]["categoryParentID"].ToString().Trim() == "0")
                            {
                                ddlCategories.SelectedValue = dtCard.Rows[0]["categoryParentID"].ToString().Trim();                               
                            }
                            else
                            {
                                ddlCategories.SelectedValue = dtParent.Rows[0]["categoryParentID"].ToString().Trim();
                                ddlCategories_SelectedIndexChanged(sender, e);
                                ddSubCategory.SelectedValue = dtCard.Rows[0]["categoryParentID"].ToString().Trim();                                
                            }
                        }
                    }
                }
                btnUpdate.Visible = true;
                btnSave.Visible = false;

                pnlForm.Visible = true;
                pnlGrid.Visible = false;
            }

        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
        DataTable dtCard = null;        
        if (ViewState["Query"] != null)
        {
            dtCard = Misc.search(ViewState["Query"].ToString());
        }
        else
        {
            dtCard = obj.getAllCategories();
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
        DataView dv = new DataView(dtCard);
        dv.Sort = sortExpression + direction;
        ViewState["Direction"] = sortExpression + direction;
        pageGrid.DataSource = dv;
        pageGrid.DataBind();

        TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
        txtPage.Text = (pageGrid.PageIndex + 1).ToString();
        if (dtCard != null && dtCard.Rows.Count > 0)
        {
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

            lblTotalRecords.Text = dtCard.Rows.Count.ToString();
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

            strQuery = "SELECT categoryId ,categoryDescription ,categoryName,categoryParentID,(select categoryName from categories where categoryID= " + Request.QueryString["vscid"].ToString() + ") as categoryParentname";
            strQuery += " ,status,creationDate,createdBy ,modifiedDate ,modifiedBy FROM categories where categoryParentID=" + Request.QueryString["vscid"].ToString();                          
            if (txtSearchcategoryNumber.Text != "")
            {
                strQuery += " and  categoryName like  '%" + txtSearchcategoryNumber.Text.ToString().Trim() + "%'";                
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
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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

            DataTable dtCard = Misc.search(Query);
            if ((dtCard != null) &&
                (dtCard.Columns.Count > 0) &&
                (dtCard.Rows.Count > 0))
            {
                pageGrid.DataSource = dtCard.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtCard.Rows.Count.ToString();
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
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
            txtSearchcategoryNumber.Text = "";
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

    #region Button Delete Selected Click Event
    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    {
        int check = 0;        
        try
        {
            for (int i = 0; i < pageGrid.Rows.Count; i++)
            {
                CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");
                if (chkSub.Checked)
                {
                    Label lblID = (Label)pageGrid.Rows[i].FindControl("lblID1");
                    obj.categoryId = Convert.ToInt32(lblID.Text);                   
                    if (obj.deleteCategory()!=0)
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
    #endregion

    #region Button Add New Click 
    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearFields();
            if (Request.QueryString["vscid"] != null && Request.QueryString["vscid"].ToString().Trim() != "")
            {
                ddlCategories.SelectedValue = Request.QueryString["vscid"].ToString();
                //obj.categoryParentID = Convert.ToInt64(Request.QueryString["vscid"].ToString());
                //DataTable dtParent = obj.getSubCategoryByParentID();
                //if (dtParent != null && dtParent.Rows.Count > 0)
                //{
                //    ddlCategories.SelectedValue = dtParent.Rows[0]["categoryParentID"].ToString().Trim();
                //    ddlCategories_SelectedIndexChanged(sender, e);
                //    ddSubCategory.SelectedValue = dtParent.Rows[0]["categoryId"].ToString().Trim();
                //}
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

    private void ClearFields()
    {
        txtCategoryName.Text = "";
        txtDescription.Text = "";
        cbStatus.Checked = true;
        ddlCategories.SelectedIndex = 0;
        ddSubCategory.Items.Clear();
        trSubCategory.Visible = false;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        pnlForm.Visible = true;
        pnlGrid.Visible = false;
       

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
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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

    #region Button Save Click Event
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];            
            obj.createdBy = Convert.ToInt64(dtUser.Rows[0]["userID"]);
            obj.categoryDescription = txtDescription.Text.Trim();
            obj.categoryName = txtCategoryName.Text.Trim();
            obj.status = cbStatus.Checked;
            if (ddSubCategory.Items.Count > 0)
            {
                obj.categoryParentID = Convert.ToInt64(ddSubCategory.SelectedValue.ToString());
            }
            else
            {
                if (ddlCategories.SelectedIndex == 0)
                {
                    obj.categoryParentID = 0;
                }
                else
                {
                    obj.categoryParentID = Convert.ToInt64(ddlCategories.SelectedValue.ToString());
                }
            }            
            if (obj.createCategory() != 0)
            {
                ViewState["Query"] = null;
                bindGrid();
                lblMessage.Text = "Record has been added successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
            }

        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }

    }
    #endregion

    #region Button Update Click Event
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            DataTable dtUser = (DataTable)Session["user"];
            obj.categoryId = Convert.ToInt64(ViewState["categoryId"].ToString());
            obj.modifiedBy = Convert.ToInt64(dtUser.Rows[0]["userID"]);
            obj.categoryDescription = txtDescription.Text.Trim();
            obj.categoryName = txtCategoryName.Text.Trim();
            obj.status = cbStatus.Checked;
            if (ddSubCategory.Items.Count > 0)
            {
                obj.categoryParentID = Convert.ToInt64(ddSubCategory.SelectedValue.ToString());
            }
            else
            {
                if (ddlCategories.SelectedIndex == 0)
                {
                    obj.categoryParentID = 0;
                }
                else
                {
                    obj.categoryParentID = Convert.ToInt64(ddlCategories.SelectedValue.ToString());
                }
            }

            if (obj.updateCategory() != 0)
            {
                ViewState["Query"] = null;
                bindGrid();
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
                lblMessage.Text = "Record has been updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;

            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;

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
            DataTable dtCard = null;
            if (ViewState["Query"] != null)
            {
                dtCard = Misc.search(ViewState["Query"].ToString());
            }
            else
            {
                dtCard = obj.getAllCategories();
            }
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
            lblTotalRecords.Text = dtCard.Rows.Count.ToString();
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
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategories.SelectedIndex == 0)
            {
                trSubCategory.Visible = false;
                ddSubCategory.Items.Clear();
                ddSubCategory.DataSource = null;
                ddSubCategory.DataBind();
            }
            else
            {
                obj.categoryParentID=Convert.ToInt64(ddlCategories.SelectedValue.ToString());
                DataTable dtSubCategory = obj.getSubCategoryByParentID();
                if (dtSubCategory != null && dtSubCategory.Rows.Count > 0)
                {
                    trSubCategory.Visible = true;
                    ddSubCategory.DataSource = dtSubCategory;
                    ddSubCategory.DataTextField = "categoryName";
                    ddSubCategory.DataValueField = "categoryId";
                    ddSubCategory.DataBind();
                }
                else
                {
                    trSubCategory.Visible = false;
                    ddSubCategory.Items.Clear();
                    ddSubCategory.DataSource = null;
                    ddSubCategory.DataBind();
                }

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
    }
    protected void pageGrid_Updating(object sender, GridViewUpdateEventArgs e)
    {
        //bool result = false;
        try
        {
            ClearFields();
            obj.categoryId = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value);
            DataTable dtParent = obj.getCategoryByID();
            if (dtParent != null && dtParent.Rows.Count > 0)
            {
                ddlCategories.SelectedValue = dtParent.Rows[0]["categoryParentID"].ToString().Trim();
                ddlCategories_SelectedIndexChanged(sender, e);
                ddSubCategory.SelectedValue = dtParent.Rows[0]["categoryId"].ToString().Trim();
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
