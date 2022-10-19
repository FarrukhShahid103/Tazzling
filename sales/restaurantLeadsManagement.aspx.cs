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

public partial class restaurantLeadsManagement : System.Web.UI.Page
{
    BLLRestaurantLeads obj = new BLLRestaurantLeads();

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
            bindProvince();
            bindGrid();
            if (Request.QueryString["msg"] != null && Request.QueryString["msg"].ToString().Trim() == "2")
            {
                lblMessage.Text = "Restaurant Information has been updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else if (Request.QueryString["msg"] != null && Request.QueryString["msg"].ToString().Trim() == "1")
            {
                lblMessage.Text = "Restaurant Information has been added successfully.";
                lblMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/checked.png"; 
                imgGridMessage.Visible = true;                
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
    }


    protected void bindProvince()
    {
        try
        {
            ddSearchProvince.DataSource = Misc.getProvincesByCountryID(2).DefaultView;
            ddSearchProvince.DataTextField = "provinceName";
            ddSearchProvince.DataValueField = "provinceId";
            ddSearchProvince.DataBind();
            ddSearchProvince.Items.Insert(0, "All");
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
            if (ddlFaxPhone.SelectedIndex != 0 || txtSearchResturantName.Text.Trim() != "" || txtSearchUserName.Text.Trim() != "" || ddSearchProvince.SelectedIndex!=0)
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;


                strQuery = "Select";
                strQuery += " restaurantLeadID ,restaurantLeadName ,restaurantLeadOwnerName ,restaurantLeadAddress";
                strQuery += ",restaurantLeadPhoneNumber,restaurantAssignto,restaurantLeadOwnerPhoneNumber,restaurantLeadStatus, restaurantLeads.provinceId, restaurantLeads.cityId";   
			    strQuery += ",restaurantLeadSignUpID ,restaurantLeads.creationDate ,restaurantLeads.createdBy   ,userName,priority";
	            strQuery += " FROM restaurantLeads";
                strQuery += " Inner join userInfo on userInfo.userId=restaurantLeads.createdBy";                
                
                if (txtSearchResturantName.Text.Trim() != "")
                {
                    strQuery += " where restaurantLeadName like '%" + txtSearchResturantName.Text.Trim() + "%' ";
                }
                if (txtSearchUserName.Text.Trim() != "" && strQuery.Contains("where"))
                {
                    strQuery += " and restaurantLeadOwnerName like '%" + txtSearchUserName.Text.Trim() + "%' ";
                }
                else if (txtSearchUserName.Text.Trim() != "")
                {
                    strQuery += " where restaurantLeadOwnerName like '%" + txtSearchUserName.Text.Trim() + "%' ";
                }
                if (ddlFaxPhone.SelectedIndex != 0)
                {
                    if (strQuery.Contains("where"))
                    {
                        strQuery += " and restaurantLeadStatus like '%" + ddlFaxPhone.SelectedValue.ToString().Trim() + "%' ";
                    }
                    else
                    {
                        strQuery += " where restaurantLeadStatus like '%" + ddlFaxPhone.SelectedValue.ToString().Trim() + "%' ";
                    }
                }
                if (ddSearchProvince.SelectedIndex != 0)
                {
                    if (strQuery.Contains("where"))
                    {
                        strQuery += " and restaurantLeads.provinceId=" + ddSearchProvince.SelectedValue.ToString().Trim() + " ";
                    }
                    else
                    {
                        strQuery += " where restaurantLeads.provinceId=" + ddSearchProvince.SelectedValue.ToString().Trim() + " ";
                    }
                   
                    if(ddSearchCity.Items.Count>0)
                    {
                        strQuery += " and restaurantLeads.cityId=" + ddSearchCity.SelectedValue.ToString().Trim() + " ";
                    }
                }
                strQuery += " Order by restaurantLeadID desc";
                pageGrid.PageIndex = 0;
                BindSearchedData(strQuery);
                ViewState["Query"] = strQuery;
            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid();
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
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
            DataTable dtUser;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                dtUser = obj.getAllRestaurantLeads();
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
              //  btnDeleteSelected.Enabled = true;
                btnSearch.Enabled = true;
                txtSearchResturantName.Enabled = true;
                ddlFaxPhone.Enabled = true;
                txtSearchUserName.Enabled = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                //btnDeleteSelected.Enabled = false;
                btnSearch.Enabled = false;
                txtSearchResturantName.Enabled = false;
                ddlFaxPhone.Enabled = false;
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

            //priority

            if (e.Row.RowType == DataControlRowType.DataRow)
	        {
	            // find the label control
	            Label priority =(Label) e.Row.FindControl("priority");
	 
	            // read the value from the datasoure

                if (priority.Text.Trim() != "")
                {
                    if (Convert.ToBoolean(priority.Text.Trim()))
                    {
                        // get the cell where that label contains

                        // to change the row color by setting
                        e.Row.BackColor = System.Drawing.Color.Orange;

                        // to change the text color like this
                        // lblPrice.ForeColor = System.Drawing.Color.Green;
                    }
                }
	        }
 

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
    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            Label lblUserID = (Label)pageGrid.Rows[e.RowIndex].FindControl("lblUserID");
            if (lblUserID != null)
            {
                obj.restaurantLeadID = Convert.ToInt32(lblUserID.Text.Trim());
                result = obj.deleteRestaurantLeads();
            }

            if (result!=0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record could not be deleted.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
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
            this.bindGrid();
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
        bindGrid();
        txtSearchUserName.Text = "";
        ddlFaxPhone.SelectedIndex = 0;
        txtSearchResturantName.Text = "";    
    }
    //protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    //{
    //    int check = 0;
    //    bool result = false;

    //    try
    //    {
    //        for (int i = 0; i < pageGrid.Rows.Count; i++)
    //        {
    //            CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");
    //            if (chkSub.Checked)
    //            {
    //                Label lblUserID = (Label)pageGrid.Rows[i].FindControl("lblUserID");
    //                obj.userId = Convert.ToInt32(lblUserID.Text);
    //                result = obj.deleteUser();
    //                if (result)
    //                {
    //                    check++;
    //                }
    //            }
    //        }
    //        if (check != 0)
    //        {
    //            ViewState["Query"] = null;
    //            pageGrid.PageIndex = 0;
    //            bindGrid();
    //            lblMessage.Text = "Selected records have been deleted successfully.";
    //            lblMessage.Visible = true;
    //            imgGridMessage.Visible = true;
    //            imgGridMessage.ImageUrl = "images/Checked.png";
    //            lblMessage.ForeColor = System.Drawing.Color.Black;
    //        }
    //    }
    //    catch (Exception ex)
    //    {           
    //        pnlGrid.Visible = true;
    //        lblMessage.Text = ex.ToString();
    //        lblMessage.Visible = true;
    //        imgGridMessage.Visible = true;
    //        imgGridMessage.ImageUrl = "images/error.png"; 
    //        lblMessage.ForeColor = System.Drawing.Color.Red;
    //    }
    //}   
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
                dtUser = obj.getAllRestaurantLeads();
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
            ListItem objList = new ListItem("All", obj.getAllRestaurantLeads().Rows.Count.ToString());
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
    protected void ddSearchProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddSearchProvince.SelectedIndex == 0)
            {
                divCityLabel.Visible = false;
                divCityDD.Visible = false;
                ddSearchCity.Items.Clear();
                ddSearchCity.DataSource = null;
                ddSearchCity.DataBind();
            }
            else
            {
                DataTable dtCities = Misc.getCitiesByProvinceID(Convert.ToInt32(ddSearchProvince.SelectedValue.ToString()));
                if (dtCities != null && dtCities.Rows.Count > 0)
                {
                    divCityLabel.Visible = true;
                    divCityDD.Visible = true;
                    ddSearchCity.DataSource = dtCities;
                    ddSearchCity.DataTextField = "cityName";
                    ddSearchCity.DataValueField = "cityId";
                    ddSearchCity.DataBind();
                }
                else
                {
                    divCityLabel.Visible = false;
                    divCityDD.Visible = false;
                    ddSearchCity.Items.Clear();
                    ddSearchCity.DataSource = null;
                    ddSearchCity.DataBind();
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
    protected void btnPriority_Click(object sender, EventArgs e)
    {
        try
        {
            string strQuery = "";
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;


            strQuery = "Select";
            strQuery += " restaurantLeadID ,restaurantLeadName ,restaurantLeadOwnerName ,restaurantLeadAddress";
            strQuery += ",restaurantLeadPhoneNumber,restaurantAssignto,restaurantLeadOwnerPhoneNumber,restaurantLeadStatus, restaurantLeads.provinceId, restaurantLeads.cityId";
            strQuery += ",restaurantLeadSignUpID ,restaurantLeads.creationDate ,restaurantLeads.createdBy   ,userName,priority";
            strQuery += " FROM restaurantLeads";
            strQuery += " Inner join userInfo on userInfo.userId=restaurantLeads.createdBy";
            strQuery += " where priority=1 Order by restaurantLeadID desc";
            pageGrid.PageIndex = 0;
            BindSearchedData(strQuery);
            ViewState["Query"] = strQuery;

        }
        catch (Exception ex)
        { }
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        bindProvince();
        bindGrid();
    }
}
