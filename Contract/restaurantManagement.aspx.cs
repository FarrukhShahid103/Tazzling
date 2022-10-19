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
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using ExactTargetAPI;
public partial class restaurantManagement : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLRestaurant objRes = new BLLRestaurant();
    
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;
    public string strIDs = "";
    public int start = 2;
    public string strtblHide = "none";
    public string strRestHide = "none";
    public string strGoogleAddress = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get & Fill Business Info into the GridView
            GetAllBusinessInfoAndFillGrid(0);                       
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

    protected string GetDetail(object objIsFaxPhone)
    {
        if (objIsFaxPhone.ToString() != "")
        {
            if (Convert.ToBoolean(objIsFaxPhone.ToString()))
            {
                return "Fax/Phone";
            }
            else
            {
                return "Printer";
            }
        }
        return "Not assigned yet.";
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        //string strQuery = "";
        try
        {
            if (this.txtSearchBusinessName.Text.Trim() != "" || this.txtSearchCity.Text.Trim() != "" || this.txtSearchZipCode.Text.Trim() != "")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;


                //strQuery = "Select";
                //strQuery += " restaurant.[restaurantId]";
                //strQuery += ",restaurant.[firstName]";
                //strQuery += ",restaurant.[lastName]";
                //strQuery += ",restaurant.[restaurantBusinessName]";
                //strQuery += ",restaurant.[restaurantAddress]";
                //strQuery += ",restaurant.[email]";
                //strQuery += ",restaurant.[phone]";
                //strQuery += ",restaurant.[fax]";
                //strQuery += ",restaurant.[zipCode]";
                //strQuery += ",restaurant.[restaurantPicture]";
                //strQuery += ",restaurant.[detail]";
                //strQuery += ",restaurant.[createdDate]";
                //strQuery += ",restaurant.[createdBy]";
                //strQuery += ",province.provinceName";
                //strQuery += ",city.cityName";
                //strQuery += ",userinfo.userID";
                //strQuery += ",restaurant.isActive";
                //strQuery += ",restaurant.userid";
                //strQuery += ",userinfo.userPassword";
                //strQuery += ",businessPaymentTitle";
                //strQuery += ",commission";
                //strQuery += " From restaurant";
                //strQuery += " INNER JOIN province on province.provinceId= restaurant.provinceId";
                //strQuery += " INNER JOIN city on city.cityId= restaurant.cityId";
                //strQuery += " inner join userinfo on userinfo.userid = restaurant.userid";

                //if (txtSearchBusinessName.Text.Trim() != "")
                //{
                //    strQuery += " and restaurantBusinessName like '%" + txtSearchBusinessName.Text.Trim() + "%' ";
                //}
                //if (txtSearchCity.Text.Trim() != "")
                //{
                //    strQuery += " and city.cityName like '%" + txtSearchCity.Text.Trim() + "%' ";
                //}
                //if (txtSearchZipCode.Text.Trim() != "")
                //{
                //    strQuery += " and zipCode like '%" + txtSearchZipCode.Text.Trim() + "%' ";
                //}

                //strQuery += " order by restaurant.restaurantId desc";

                pageGrid.PageIndex = 0;
                BindSearchedData("0");                
            }
            else
            {
                pageGrid.PageIndex = 0;
                GetAllBusinessInfoAndFillGrid(0);
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
            btnShowAll.Visible = true;
            string strQuery = "";

            DataTable dtUsercheck = (DataTable)Session["user"];
            if (dtUsercheck.Rows[0]["userTypeID"].ToString().Trim() == "4")
            {
                if (this.txtSearchBusinessName.Text.Trim() != "" || this.txtSearchCity.Text.Trim() != "" || this.txtSearchZipCode.Text.Trim() != "")
                {
                    strQuery = "Select * from (";
                    strQuery += " Select";
                    strQuery += " restaurant.[restaurantId]";
                    strQuery += ",restaurant.[firstName]";
                    strQuery += ",restaurant.[lastName]";
                    strQuery += ",restaurant.[restaurantBusinessName]";
                    strQuery += ",restaurant.[restaurantAddress]";
                    strQuery += ",restaurant.[phone]";
                    strQuery += ",restaurant.[createdDate]";
                    strQuery += ",restaurant.isActive";
                    strQuery += ",restaurant.userid";
                    strQuery += ",ROW_NUMBER() OVER(ORDER BY restaurantId desc) as RowNum";
                    strQuery += " From restaurant";
                    strQuery += " where restaurantBusinessName like '%" + txtSearchBusinessName.Text.Trim().Replace("'", "''") + "%' ";
                    strQuery += " and userid =" + dtUsercheck.Rows[0]["userId"].ToString().Trim();
                    if (txtSearchCity.Text.Trim() != "")
                    {
                        strQuery += " and restaurantAddress like '%" + txtSearchCity.Text.Trim().Replace("'", "''") + "%' ";
                    }
                    if (txtSearchZipCode.Text.Trim() != "")
                    {
                        strQuery += " and email like '%" + txtSearchZipCode.Text.Trim().Replace("'", "''") + "%' ";
                    }
                    strQuery += ") as DerivedTableName";
                    int strStartIndex = (Convert.ToInt32(strRowIndex) * pageGrid.PageSize) + 1;
                    int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * pageGrid.PageSize;
                    strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;
                    strQuery += "order by restaurantId desc ";
                    strQuery += " SELECT 'Return Value' =  COUNT(restaurantId) FROM restaurant";
                    strQuery += " where restaurantBusinessName like '%" + txtSearchBusinessName.Text.Trim().Replace("'", "''") + "%' ";
                    if (txtSearchCity.Text.Trim() != "")
                    {
                        strQuery += " and restaurantAddress like '%" + txtSearchCity.Text.Trim().Replace("'", "''") + "%' ";
                    }
                    if (txtSearchZipCode.Text.Trim() != "")
                    {
                        strQuery += " and email like '%" + txtSearchZipCode.Text.Trim().Replace("'", "''") + "%' ";
                    }
                    strQuery += " and userid =" + dtUsercheck.Rows[0]["userId"].ToString().Trim();
                }

            }
            else if (this.txtSearchBusinessName.Text.Trim() != "" || this.txtSearchCity.Text.Trim() != "" || this.txtSearchZipCode.Text.Trim() != "")
            {
                strQuery = "Select * from (";
                strQuery += " Select";
                strQuery += " restaurant.[restaurantId]";
                strQuery += ",restaurant.[firstName]";
                strQuery += ",restaurant.[lastName]";
                strQuery += ",restaurant.[restaurantBusinessName]";
                strQuery += ",restaurant.[restaurantAddress]";
                strQuery += ",restaurant.[phone]";
                strQuery += ",restaurant.[createdDate]";
                strQuery += ",restaurant.isActive";
                strQuery += ",restaurant.userid";
                strQuery += ",ROW_NUMBER() OVER(ORDER BY restaurantId desc) as RowNum";
                strQuery += " From restaurant";
                strQuery += " where restaurantBusinessName like '%" + txtSearchBusinessName.Text.Trim().Replace("'", "''") + "%' ";
                if (txtSearchCity.Text.Trim() != "")
                {
                    strQuery += " and restaurantAddress like '%" + txtSearchCity.Text.Trim().Replace("'", "''") + "%' ";
                }
                if (txtSearchZipCode.Text.Trim() != "")
                {
                    strQuery += " and email like '%" + txtSearchZipCode.Text.Trim().Replace("'", "''") + "%' ";
                }
                strQuery += ") as DerivedTableName";
                int strStartIndex = (Convert.ToInt32(strRowIndex) * pageGrid.PageSize) + 1;
                int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * pageGrid.PageSize;
                strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;
                strQuery += "order by restaurantId desc ";
                strQuery += " SELECT 'Return Value' =  COUNT(restaurantId) FROM restaurant";
                strQuery += " where restaurantBusinessName like '%" + txtSearchBusinessName.Text.Trim().Replace("'", "''") + "%' ";
                if (txtSearchCity.Text.Trim() != "")
                {
                    strQuery += " and restaurantAddress like '%" + txtSearchCity.Text.Trim().Replace("'", "''") + "%' ";
                }
                if (txtSearchZipCode.Text.Trim() != "")
                {
                    strQuery += " and email like '%" + txtSearchZipCode.Text.Trim().Replace("'", "''") + "%' ";
                }

            }
            else
            {
                pageGrid.PageIndex = 0;
                GetAllBusinessInfoAndFillGrid(0);
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

 

    protected void GetAllBusinessInfoAndFillGrid(int intPageNumber)
    {
        try
        {
            DataTable dtUsercheck = (DataTable)Session["user"];
            if (dtUsercheck != null && dtUsercheck.Rows.Count > 0)
            {

                if (dtUsercheck.Rows[0]["userTypeID"].ToString() == "1" || dtUsercheck.Rows[0]["userTypeID"].ToString() == "2" || dtUsercheck.Rows[0]["userTypeID"].ToString() == "6" || dtUsercheck.Rows[0]["userTypeID"].ToString() == "7")
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
                        dst = objRes.getAllResturantsForAdminWithIndexing(intPageNumber, pageGrid.PageSize);
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


                        //btnDeleteSelected.Enabled = true;
                        btnSearch.Enabled = true;
                        this.txtSearchBusinessName.Enabled = true;
                        this.txtSearchCity.Enabled = true;
                        this.txtSearchZipCode.Enabled = true;
                    }
                    else
                    {
                        pageGrid.DataSource = null;
                        pageGrid.DataBind();
                        //btnDeleteSelected.Enabled = false;
                        btnSearch.Enabled = false;
                        this.txtSearchBusinessName.Enabled = false;
                        this.txtSearchCity.Enabled = false;
                        this.txtSearchZipCode.Enabled = false;
                    }

                }



            /*---------------------------------------------------------- For Bussiness User-----------------------------------*/

                else if (dtUsercheck.Rows[0]["userTypeID"].ToString() == "4" )
                {
                    objRes.userID = Convert.ToInt16(dtUsercheck.Rows[0]["userId"].ToString().Trim());

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
                        dst = objRes.spGetAllResturantsForAdminWithIndexingByUserId(intPageNumber, pageGrid.PageSize);
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
                           pageGrid.Columns[6].Visible = false;
                            pageGrid.Columns[7].Visible = false;
                            pageGrid.Columns[8].Visible = false;
                            pageGrid.Columns[9].Visible = false;
                            pageGrid.Columns[10].Visible = true;
                           


                        
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


                        //btnDeleteSelected.Enabled = true;
                        btnSearch.Enabled = true;
                        this.txtSearchBusinessName.Enabled = true;
                        this.txtSearchCity.Enabled = true;
                        this.txtSearchZipCode.Enabled = true;
                        
                    }
                    else
                    {
                        pageGrid.DataSource = null;
                        pageGrid.DataBind();
                        //btnDeleteSelected.Enabled = false;
                        btnSearch.Enabled = false;
                        this.txtSearchBusinessName.Enabled = false;
                        this.txtSearchCity.Enabled = false;
                        this.txtSearchZipCode.Enabled = false;
                    }

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
            this.GetAllBusinessInfoAndFillGrid(intCurrentPage - 1);
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

    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();

            Label lblResttaurantID = (Label)pageGrid.Rows[e.RowIndex].FindControl("lblResttaurantID");
            if (lblResttaurantID != null)
            {
                objBLLRestaurant.restaurantId = Convert.ToInt32(lblResttaurantID.Text.Trim());
                result = objBLLRestaurant.deleteRestaurant();
            }

            if (result != 0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                GetAllBusinessInfoAndFillGrid(0);
                lblMessage.Text = "Business has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                pageGrid.PageIndex = 0;
                GetAllBusinessInfoAndFillGrid(0);
                lblMessage.Text = "Business could not be deleted because it contains the deals as well, please delete its deals first.";
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

    protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();

            objBLLRestaurant.restaurantId = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);

            Label lblStatus = (Label)pageGrid.Rows[e.NewEditIndex].FindControl("lblStatus");

            if (lblStatus != null)
            {
                if (lblStatus.Text.ToString() == "True")
                {
                    objBLLRestaurant.isActive = false;
                }
                else
                {
                    objBLLRestaurant.isActive = true;
                }
            }
            objBLLRestaurant.modifiedDate = DateTime.Now;

            objBLLRestaurant.modifiedBy = Convert.ToInt64(ViewState["userID"]);

            objBLLRestaurant.updateRestaurantStatusByResID();
            obj = new BLLUser();
            Label lblUserID = (Label)pageGrid.Rows[e.NewEditIndex].FindControl("lblUserID");
            obj.userId = Convert.ToInt32(lblUserID.Text.Trim());
            if (lblStatus != null)
            {
                if (lblStatus.Text.ToString() == "True")
                {
                    obj.isActive = false;
                }
                else
                {
                    obj.isActive = true;
                }
            }
            obj.changeUserStatus();

            ViewState["Query"] = null;
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            string strCurrentPage = txtPage.Text.Trim();
            GetAllBusinessInfoAndFillGrid( Convert.ToInt32(txtPage.Text)-1);
            txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = strCurrentPage;      
            lblMessage.Text = "Status has been changed successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;


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
            this.GetAllBusinessInfoAndFillGrid(0);
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
                this.GetAllBusinessInfoAndFillGrid(intCurrentPage - 1);
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
        GetAllBusinessInfoAndFillGrid(0);

        this.txtSearchBusinessName.Text = "";
        this.txtSearchCity.Text = "";
        this.txtSearchZipCode.Text = "";

    }
    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    {
        int UserCheck = 0;
        int check = 0;
        int result = 0;

        try
        {
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();

            for (int i = 0; i < pageGrid.Rows.Count; i++)
            {
                CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");

                if (chkSub.Checked)
                {
                    //Count the # of check boxes user selected
                    UserCheck++;

                    Label lblResttaurantID = (Label)pageGrid.Rows[i].FindControl("lblResttaurantID");
                    objBLLRestaurant.restaurantId = Convert.ToInt32(lblResttaurantID.Text);
                    result = objBLLRestaurant.deleteRestaurant();

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
                GetAllBusinessInfoAndFillGrid(0);
                lblMessage.Text = "Selected record(s) have not been deleted successfully because they conatain deal(s).";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else if ((UserCheck > 0) && (UserCheck == check))//All selected records are deleted successfully
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                GetAllBusinessInfoAndFillGrid(0);
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
                GetAllBusinessInfoAndFillGrid(0);
                lblMessage.Text = "Some records have been deleted successfully and some are not because they conatain deals.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
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
                dtUser = objRes.getAllResturantsForAdmin();
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
  
    protected void pageGrid_Login(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtUser = null;
        try
        {
            if (e.CommandName == "Login")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;
                obj.userId = Convert.ToInt32(e.CommandArgument);
                dtUser = obj.getUserByID();
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    obj.userName = dtUser.Rows[0]["userName"].ToString();
                    obj.userPassword = dtUser.Rows[0]["userPassword"].ToString();
                    dtUser = obj.validateUserNamePassword();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        Session.RemoveAll();
                        if (dtUser.Rows[0]["userTypeID"].ToString() == "1" || dtUser.Rows[0]["userTypeID"].ToString() == "2" || dtUser.Rows[0]["userTypeID"].ToString() == "6" || dtUser.Rows[0]["userTypeID"].ToString() == "7")
                        {
                            Session["user"] = dtUser;
                            Response.Redirect(ResolveUrl("~/admin/controlpanel.aspx"), false);
                        }
                        else
                        {
                            if (dtUser.Rows[0]["userTypeID"].ToString() == "4")
                            {
                                Session["member"] = dtUser;
                                Session.Remove("sale");
                                Session.Remove("restaurant");
                            }
                            else if (dtUser.Rows[0]["userTypeID"].ToString() == "3")
                            {
                                Session["restaurant"] = dtUser;
                                Session.Remove("member");
                                Session.Remove("sale");
                            }
                            else if (dtUser.Rows[0]["userTypeID"].ToString() == "5")
                            {
                                Session["sale"] = dtUser;
                                Session.Remove("member");
                                Session.Remove("restaurant");
                            }
                            Response.Redirect(ResolveUrl("~/Default.aspx"), false);
                        }
                    }
                    else
                    {

                        pnlGrid.Visible = true;
                        lblMessage.Text = "User could not login as this business user is deactive.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/error.png";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {

                    pnlGrid.Visible = true;
                    lblMessage.Text = "User could not login as this business user is deactive.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
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
         
  
}