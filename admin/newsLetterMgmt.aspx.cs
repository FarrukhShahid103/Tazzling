using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_newsLetterMgmt : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();
    BLLMemberMenu objMMenu = new BLLMemberMenu();
    BLLMemberMenuItems objMMenuItems = new BLLMemberMenuItems();
    BLLRestaurantMenu objRMenu = new BLLRestaurantMenu();
    BLLRestaurantMenuItems objRMenuItms = new BLLRestaurantMenuItems();

    BLLOrders objOrders = new BLLOrders();
    BLLRestaurantFee objFee = new BLLRestaurantFee();
    BLLConsumptionRecord objCon = new BLLConsumptionRecord();

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
            //Get & Fill Business Info into the GridView
            GetAllNewsLetterAndFillGrid();            
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    private void GetAndSetUserID()
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];

            if ((dtUser != null) && (dtUser.Rows.Count > 0))
            {
                ViewState["userID"] = dtUser.Rows[0]["userID"];
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }



    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strQuery = "";
        try
        {
            if (this.txtSearchTitle.Text.Trim() != "")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;

                strQuery = "SELECT ";
                strQuery += " [nlID]";
                strQuery += " ,[title]";
                strQuery += " ,[newsLetter]";
                strQuery += " ,[createdBy]";
                strQuery += " ,[creationDate]";
                strQuery += " ,[modifiedBy]";
                strQuery += " ,[modifiedDate]";
                strQuery += " FROM ";
                strQuery += " [newsLetters]";

                if (txtSearchTitle.Text.Trim() != "")
                {
                    strQuery += " where title like '%" + txtSearchTitle.Text.Trim() + "%' ";
                }

                strQuery += " order by nlID desc";

                pageGrid.PageIndex = 0;
                BindSearchedData(strQuery);
                ViewState["Query"] = strQuery;
            }
            else
            {
                pageGrid.PageIndex = 0;
                GetAllNewsLetterAndFillGrid();
                ViewState["Query"] = null;
            }
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    
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

            DataTable dtUser = Misc.search(Query);

            if ((dtUser != null) &&
                (dtUser.Columns.Count > 0) &&
                (dtUser.Rows.Count > 0))
            {
                pageGrid.DataSource = dtUser.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtUser.Rows.Count.ToString();
                pageGrid.PageIndex = 0;

                ViewState["Query"] = Query;
                DropDownList ddlPage = bindPageDropDown();
                if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = Session["ddlPage"].ToString();
                }
                lblPageCount.Text = pageGrid.PageCount.ToString();
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
            upBusinessMgmtForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    protected void GetAllNewsLetterAndFillGrid()
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
                dtUser = objBLLNewsLetters.getAllNewsLetter();

                dv = new DataView(dtUser);
                
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                
                btnShowAll.Visible = false;
            }
            else
            {
                dtUser = Misc.search(ViewState["Query"].ToString());

                dv = new DataView(dtUser);

                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }

                btnShowAll.Visible = true;
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
                btnDeleteSelected.Enabled = true;
                btnSearch.Enabled = true;
                this.txtSearchTitle.Enabled = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnDeleteSelected.Enabled = false;
                btnSearch.Enabled = false;
                this.txtSearchTitle.Enabled = false;                
            }
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
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
            //Save the Deal Info
            if (this.btnSave.ToolTip == "Add New Newsletter")
            {                
                //Add New Deal Info
                AddNewNewsLetter();

                //Hide the Update Deal Info here
                this.upBusinessMgmtForm.Visible = false;

                //Show All Deal Info into Grid View here
                this.pnlGrid.Visible = true;

                //Show the All Deals
                //this.lblDealInfoHeading.Text = "View All Deals";

                //Show the Add New & Delete buttons
                this.btnAddNew.Visible = true;
                this.btnDeleteSelected.Visible = true;

                //Get All Latest Deal Info Grid Info
                GetAllNewsLetterAndFillGrid();
            }
            //Update the Deal Info
            else if (this.btnSave.ToolTip == "Update Newsletter Info")
            {
                //Update the Deal info by Deal Id
                UpdateBusinessInfo(hfNewsletterId.Value);

                //Hide the Update Deal Info here
                this.upBusinessMgmtForm.Visible = false;

                //Show All Deal Info into Grid View here
                this.pnlGrid.Visible = true;

                //Show the All Deals
                //this.lblDealInfoHeading.Text = "View All Deals";

                //Get All Latest Deal Info Grid Info
                GetAllNewsLetterAndFillGrid();
            }
        }
        catch (Exception ex)
        { }
    }

    protected void UpdateBusinessInfo(string strNewsletterID)
    {
        try
        {
            BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();
            objBLLNewsLetters.nlId = int.Parse(strNewsletterID);
            objBLLNewsLetters.title = this.txtTitle.Text.Trim();
            objBLLNewsLetters.newsLetter = this.txtNewsletter.Text.Trim();
            objBLLNewsLetters.modifiedBy = Convert.ToInt64(ViewState["userID"]);
            objBLLNewsLetters.modifiedDate = DateTime.Now;


            int iChk = objBLLNewsLetters.updateNewsLetterById();

            if (iChk != 0)
            {
                lblMessage.Text = "Newsletter has been updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblMessage.Text = "Newsletter has not been updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
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
            this.GetAllNewsLetterAndFillGrid();
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (e.NewPageIndex + 1).ToString();
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
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
            upBusinessMgmtForm.Visible = false;
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
        int result = 0;
        try
        {
            BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();

            Label lblgrdnlID = (Label)pageGrid.Rows[e.RowIndex].FindControl("lblgrdnlID");
            if (lblgrdnlID != null)
            {
                objBLLNewsLetters.nlId = Convert.ToInt32(lblgrdnlID.Text.Trim());
                result = objBLLNewsLetters.deleteNewsLetterById();
            }

            if (result != 0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                GetAllNewsLetterAndFillGrid();
                lblMessage.Text = "Newsletter has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                pageGrid.PageIndex = 0;
                GetAllNewsLetterAndFillGrid();
                lblMessage.Text = "Newsletter has not been deleted.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    
    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblAddressError.Visible = false;
            lblAddressError.Text = "";
            upBusinessMgmtForm.Visible = true;
            pnlGrid.Visible = false;

            //Change the Image URL of the Save button
            this.btnSave.ImageUrl = "~/admin/images/btnUpdate.jpg";

            this.btnSave.ToolTip = "Update Newsletter Info";

            //Initilize the BLLRestaurant here
            BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();

            //Initilize the DataTable here
            DataTable dtNewsLetter = null;

            this.hfNewsletterId.Value = pageGrid.SelectedDataKey.Value.ToString();

            objBLLNewsLetters.nlId = Convert.ToInt32(pageGrid.SelectedDataKey.Value);

            dtNewsLetter = objBLLNewsLetters.getNewsLetterInfoById();

            if ((dtNewsLetter != null) && (dtNewsLetter.Rows.Count > 0))
            {
                this.txtTitle.Text = DBNull.Value.Equals(dtNewsLetter.Rows[0]["title"]) ? "" : dtNewsLetter.Rows[0]["title"].ToString().Trim();

                this.txtNewsletter.Text = DBNull.Value.Equals(dtNewsLetter.Rows[0]["newsLetter"]) ? "" : dtNewsLetter.Rows[0]["newsLetter"].ToString().Trim();                
            }
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
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
            this.GetAllNewsLetterAndFillGrid();
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
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
            this.GetAllNewsLetterAndFillGrid();
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
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
        GetAllNewsLetterAndFillGrid();

        this.txtSearchTitle.Text = "";

    }
    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    {
        int UserCheck = 0;
        int check = 0;
        int result = 0;

        try
        {
            BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();

            for (int i = 0; i < pageGrid.Rows.Count; i++)
            {
                CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");

                if (chkSub.Checked)
                {
                    //Count the # of check boxes user selected
                    UserCheck++;

                    Label lblgrdnlID = (Label)pageGrid.Rows[i].FindControl("lblgrdnlID");
                    objBLLNewsLetters.nlId = Convert.ToInt32(lblgrdnlID.Text);
                    result = objBLLNewsLetters.deleteNewsLetterById();

                    if (result != 0)
                    {
                        check++;
                    }
                }
            }

            //Means no record has been deleted
            if (check == 0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                GetAllNewsLetterAndFillGrid();
                lblMessage.Text = "Selected record(s) have not been deleted.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else if ((UserCheck > 0) && (UserCheck == check))//All selected records are deleted successfully
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                GetAllNewsLetterAndFillGrid();
                lblMessage.Text = "Selected records have been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else//Some are delete and some are not
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                GetAllNewsLetterAndFillGrid();
                lblMessage.Text = "Some records have been deleted successfully and some are not.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
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
                dtUser = objBLLNewsLetters.getAllNewsLetter();
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
            upBusinessMgmtForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
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
            ListItem objList = new ListItem("All", objBLLNewsLetters.getAllNewsLetter().Rows.Count.ToString());
            ddlPage.Items.Insert(5, objList);
            return ddlPage;
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return null;
        }
    }
    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (pageGrid.Rows.Count > 0)
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
                DataTable dtUser = null;
                if (ViewState["Query"] != null)
                {
                    dtUser = Misc.search(ViewState["Query"].ToString());
                }
                else
                {
                    dtUser = objBLLNewsLetters.getAllNewsLetter();
                }
                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
                lblTotalRecords.Text = dtUser.Rows.Count.ToString();
                lblPageCount.Text = pageGrid.PageCount.ToString();
            }

            pnlGrid.Visible = true;
            upBusinessMgmtForm.Visible = false;
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
        }
        catch (Exception ex)
        {
            upBusinessMgmtForm.Visible = false;
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

            //Hide the message area
            lblAddressError.Visible = false;
            lblAddressError.Text = "";

            //Show the Input form
            upBusinessMgmtForm.Visible = true;

            //Hide the GridView
            pnlGrid.Visible = false;

            //Change the Image URL of the Save button
            this.btnSave.ImageUrl = "~/admin/images/btnSave.jpg";

            this.btnSave.ToolTip = "Add New Newsletter";

            //Clear All the fields
            this.txtTitle.Text = "";
            this.txtNewsletter.Text = "";                        
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }


    #region"Save New Business Info here"

    private void AddNewNewsLetter()
    {
        try
        {
            BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();
            objBLLNewsLetters.title = this.txtTitle.Text.Trim();
            objBLLNewsLetters.newsLetter = this.txtNewsletter.Text.Trim();
            objBLLNewsLetters.createdBy = Convert.ToInt64(ViewState["userID"]);
            objBLLNewsLetters.createdDate = DateTime.Now;

            int iChk = objBLLNewsLetters.createNewsLetter();

            lblMessage.Text = "Newsletter has been added successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #endregion
}
