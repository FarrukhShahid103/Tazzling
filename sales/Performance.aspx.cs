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

public partial class sales_Performance : System.Web.UI.Page
{
    public bool displayPrevious = false;
    public bool displayNext = true;
    BLLDealOrders objBLLDealOrders = new BLLDealOrders();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    DataTable dtUser;
    DataView dv;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           bindProvinces();
           GetAllBusinessInfoAndFillGrid();
        }
    }
    protected void bindProvinces()
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlSearchProvince.DataSource = dt.DefaultView;
            ddlSearchProvince.DataTextField = "provinceName";
            ddlSearchProvince.DataValueField = "provinceId";
            ddlSearchProvince.DataBind();
            ddlSearchProvince.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlSearchProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlSearchProvince.SelectedValue);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    private void FillCitpDroDownList(string strProvinceId)
    {
        try
        {
            if (strProvinceId != "Select One")
            {
                BLLCities objBLLCities = new BLLCities();

                objBLLCities.provinceId = int.Parse(strProvinceId);

                DataTable dtCities = objBLLCities.getCitiesByProvinceId();

                ddlSearchCity.DataSource = dtCities;

                ddlSearchCity.DataTextField = "cityName";

                ddlSearchCity.DataValueField = "cityId";

                ddlSearchCity.DataBind();

                ddlSearchCity.Items.Insert(0, "Select One");
            }
            else
            {
                ddlSearchCity.Items.Clear();
                ddlSearchCity.DataSource = null;
                ddlSearchCity.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    DataTable dtSubscribedCityList, DataTableAllCities = new DataTable();
    protected void dlCities_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (dtSubscribedCityList != null)
        {
            System.Web.UI.WebControls.CheckBox chk;
            chk = (System.Web.UI.WebControls.CheckBox)(e.Item.FindControl("chkbxSelect"));
            Label lblCityID = (Label)(e.Item.FindControl("lblCityID"));
            Label lblCityName = (Label)(e.Item.FindControl("lblCity"));
            try
            {
                if (lblCityID != null && chk != null)
                {
                    for (int j = 0; j < dtSubscribedCityList.Rows.Count; j++)
                    {

                        if (lblCityID.Text.Trim() == dtSubscribedCityList.Rows[j]["cityid"].ToString().Trim())
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            }
        }

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
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
         string strQuery = "";
         try
         {
             if ( ddlSearchCity.SelectedValue.ToString().Trim() != "" || txtBusinessName.Text.Trim() != "" || txtStartDate.Text.Trim() != "" || txtEndDate.Text.Trim() != "" || txtSalesPersonAccountName.Text.Trim() != "")
             {

                 lblMessage.Visible = false;
                 imgGridMessage.Visible = false;


                 strQuery = "Select";
                 strQuery += " [deals].[dealId]";
                 strQuery += ",[deals].[restaurantId]";
                 strQuery += ",[deals].[title]";
                 strQuery += " ,[deals].[sellingPrice]";
                 strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].[status]='Successful' and [dealOrders].dealId = [deals].dealId),0) 'SuccessfulOrder'";
                 strQuery += ",dealStartTimeC as 'dealStartTime'";
                 strQuery += ",dealEndTimeC as 'dealEndTime'";
                 strQuery += " ,[deals].[createdBy]";
                 strQuery += ",[deals].[createdDate]";
                 strQuery += ",[restaurant].[restaurantBusinessName]";
                 strQuery += ",[deals].[salePersonAccountName]";
                 strQuery += " From [deals]  ";
                 strQuery += "  INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
                 strQuery += " INNER join dealcity On dealcity.[dealid]= deals.[dealid]";
                 strQuery += " INNER join city On dealCity.cityId= city.[cityId] ";

                 strQuery += "  where restaurant.[restaurantId]= deals.[restaurantId] ";

                 if (ddlSearchCity.SelectedValue.ToString().Trim() != "" && ddlSearchCity.SelectedValue.ToString().Trim() != "Select One")
                 {
                     strQuery += " and city.[cityid] like '%" + ddlSearchCity.SelectedValue.ToString().Trim() + "%' ";
                 }
                 if (txtBusinessName.Text.Trim() != "")
                 {
                     strQuery += " and [restaurant].[restaurantBusinessName] like '%" + txtBusinessName.Text.Trim() + "%' ";
                 }
                 if (txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "")
                 {
                     strQuery += " and dealOrders.createdDate between '%" + txtStartDate.Text.Trim() + "' AND '" + txtEndDate.Text.Trim() + "'";
                 }

                 if (txtSalesPersonAccountName.Text.Trim() != "")
                 {
                     strQuery += " and [deals].[salePersonAccountName] like '%" + txtSalesPersonAccountName.Text.Trim() + "%' ";
                 }



                 //if (Session["SalesPersonAccountName"] != null && Session["SalesPersonAccountName"].ToString().Trim() != "")
                 //{

                 //    strQuery += " and restaurant.[salePersonAccountName] ='" + Session["SalesPersonAccountName"].ToString().Trim() + "'";

                 //}

                 strQuery += " order by restaurant.restaurantId desc";

                 pageGrid.PageIndex = 0;
                 BindSearchedData(strQuery);
                 ViewState["Query"] = strQuery;


             }

             else
             {
                 pageGrid.PageIndex = 0;
                 GetAllBusinessInfoAndFillGrid();
                 ViewState["Query"] = null;
             }

         }
         catch { 
         

         }

    }
    protected void btnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlSearchProvince.SelectedIndex = 0;
        ddlSearchCity.Items.Clear();
        txtDealName.Text = "";
        txtBusinessName.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtSalesPersonAccountName.Text = "";


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
    protected void GetAllBusinessInfoAndFillGrid()
    {
        try
        {
           

            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(ViewState["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                ViewState["ddlPage"] = Misc.pageSize;
            }
           
            if (ViewState["Query"] == null)
            {
                dtUser = objBLLDealOrders.GetSalesPerformanceForSuperAdmin();
                dv = new DataView(dtUser);

                //dtUser =(DataTable) Session["salesSection"];

                //if (dtUser != null && dtUser.Rows.Count > 0)
                //{

                    //if (dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "2")
                    //{
                    //    dtUser = objBLLDealOrders.GetSalesPerformanceForSuperAdmin();
                    //    dv = new DataView(dtUser);
                    //}
                    //else
                    //{
                    //    Session["SalesPersonAccountName"] = dtUser.Rows[0]["userName"].ToString().Trim();
                    //    BLLUser objuser = new BLLUser();
                    //    dtUser = null;
                    //    dtUser = objBLLDealOrders.GetSalesPerformanceBySalesPersonEmail(Session["SalesPersonAccountName"].ToString().Trim());
                    //    if (dtUser != null && dtUser.Rows.Count > 0)
                    //    {
                    //        dv = new DataView(dtUser);
                    //    }

                    //}

                    
               // }
               
                
                
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
                if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = ViewState["ddlPage"].ToString();
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
    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
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
            ViewState["ddlPage"] = ddlPage.SelectedValue.ToString();
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
            ListItem objList = new ListItem("All", objBLLDealOrders.GetSalesPerformanceForSuperAdmin().Rows.Count.ToString());
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
                dtUser = objBLLDealOrders.GetSalesPerformanceForSuperAdmin();
                dv = new DataView(dtUser);

                //dtUser = (DataTable)Session["salesSection"];
                //if (dtUser != null && dtUser.Rows.Count > 0)
                //{

                //    if (dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "2")
                //    {
                //        dtUser = objBLLDealOrders.GetSalesPerformanceForSuperAdmin();

                //    }
                //    else
                //    {
                //        Session["SalesPersonAccountName"] = dtUser.Rows[0]["userName"].ToString().Trim();
                //        BLLUser objuser = new BLLUser();
                //        dtUser = null;
                //        dtUser = objBLLDealOrders.GetSalesPerformanceBySalesPersonEmail(Session["SalesPersonAccountName"].ToString().Trim());
                //        if (dtUser != null && dtUser.Rows.Count > 0)
                //        {
                //            dv = new DataView(dtUser);
                //        }

                //    }
                //}
            }

            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(ViewState["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = 5;
                ViewState["ddlPage"] = 5;
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
            dv = new DataView(dtUser);
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
                if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = ViewState["ddlPage"].ToString();
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
   
}
