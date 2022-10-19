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

public partial class sales_SalesManagement : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLRestaurant objRes = new BLLRestaurant();
    BLLMemberMenu objMMenu = new BLLMemberMenu();
    BLLMemberMenuItems objMMenuItems = new BLLMemberMenuItems();
    BLLRestaurantMenu objRMenu = new BLLRestaurantMenu();
    BLLRestaurantMenuItems objRMenuItms = new BLLRestaurantMenuItems();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;
    public string strIDs = "";
    public int start = 2;
    public string strtblHide = "none";
    public string strRestHide = "none";
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["salesSection"] == null)
            {
                Response.Redirect(ResolveUrl("~/sales/default.aspx"), false);
            }
            else
            {
                DataTable dt;
                dt = (DataTable)Session["salesSection"];
                if (dt != null && dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["userTypeID"].ToString() == "1" || dt.Rows[0]["userTypeID"].ToString() == "2")
                    {

                        bindProvinces();
                        // bindCountries();
                        bindCousines();
                        bindGrid();

                    }
                    else
                    {
                        SearchButtons.Visible = false;
                        SearchDiv.Visible = false;
                        gv.Visible = false;
                        imgGridMessage.Visible = true;
                        lblMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/error.png";
                        lblMessage.Text = "You are not authorized to view this area. :-) ";
                        lblMessage.ForeColor = System.Drawing.Color.Red;

                    }

                }

            }
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strQuery = "";
        try
        {
            if (txtSearchFirstName.Text.Trim() != "" || txtSearchLastName.Text.Trim() != "" || txtSearchUserName.Text.Trim() != "")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;
                strQuery = "SELECT ";
                strQuery += " userInfo.userID, ";
                strQuery += " userType.userTypeID, ";
                strQuery += " userType.userType, ";
                strQuery += " userPassword, ";
                strQuery += " userInfo.userName, ";
                strQuery += " userInfo.email, ";
                strQuery += " referralId, ";
                strQuery += " dbo.InitCap(userInfo.firstName) as firstName, ";
                strQuery += " dbo.InitCap(userInfo.lastName) as lastName, ";
                strQuery += " friendsReferralId, ";
                strQuery += " userInfo.countryId, ";
                strQuery += " userInfo.provinceId, ";
                strQuery += " howYouKnowUs, ";
                strQuery += " userInfo.creationDate, ";
                strQuery += " userInfo.createdBy, ";
                strQuery += " userInfo.modifiedDate, ";
                strQuery += " userInfo.modifiedBy, ";
                strQuery += " userInfo.isActive,isAffiliate ";
                strQuery += " FROM userInfo ";
                strQuery += " inner join userType on userType.userTypeID = userInfo.userTypeID ";
                strQuery += " where isDeleted = 0 and userType.userTypeId = 5 ";

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
            pnlForm.Visible = false;
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
                dtUser = obj.GetAllSalesUsers();
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
                txtSearchFirstName.Enabled = true;
                txtSearchLastName.Enabled = true;
                txtSearchUserName.Enabled = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnDeleteSelected.Enabled = false;
                btnSearch.Enabled = false;
                txtSearchFirstName.Enabled = false;
                txtSearchLastName.Enabled = false;
                txtSearchUserName.Enabled = false;
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

    protected void bindProvinces()
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlProvinceLive.DataSource = dt.DefaultView;
            ddlProvinceLive.DataTextField = "provinceName";
            ddlProvinceLive.DataValueField = "provinceId";
            ddlProvinceLive.DataBind();
            ddlProvinceLive.Items.Insert(0, "Select One");
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
    protected void bindCousines()
    {
        try
        {
            BLLCuisine objCousine = new BLLCuisine();
            DataTable dt = objCousine.getAllCuisine();
            ddlCousineType.DataSource = dt.DefaultView;
            ddlCousineType.DataTextField = "cuisineName";
            ddlCousineType.DataValueField = "cuisineId";
            ddlCousineType.DataBind();
            ddlCousineType.Items.Insert(0, "Select One");
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
    protected void bindProvincesByCountryID(int countryID)
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(countryID);
            ddlProvinceLive.DataSource = dt.DefaultView;
            ddlProvinceLive.DataTextField = "provinceName";
            ddlProvinceLive.DataValueField = "provinceID";
            ddlProvinceLive.DataBind();
            ddlProvinceLive.Items.Insert(0, "Select One");
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
    //protected void bindCountries()
    //{
    //    try
    //    {
    //        DataTable dt = Misc.getAllCountries();
    //        ddlCountry.DataSource = dt.DefaultView;
    //        ddlCountry.DataTextField = "countryName";
    //        ddlCountry.DataValueField = "countryID";
    //        ddlCountry.DataBind();
    //        ddlCountry.Items.Insert(0, "Select One");
    //    }
    //    catch (Exception ex)
    //    {
    //        pnlForm.Visible = false;
    //        pnlGrid.Visible = true;
    //        lblMessage.Text = ex.ToString();
    //        lblMessage.Visible = true;
    //        imgGridMessage.Visible = true;
    //        imgGridMessage.ImageUrl = "images/error.png";
    //        lblMessage.ForeColor = System.Drawing.Color.Red;
    //    }
    //}

    protected void ddlProvinceLive_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);
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

                ddlSelectCity.DataSource = dtCities;

                ddlSelectCity.DataTextField = "cityName";

                ddlSelectCity.DataValueField = "cityId";

                ddlSelectCity.DataBind();

                ddlSelectCity.Items.Insert(0, "Select One");
            }
            else
            {
                ddlSelectCity.Items.Clear();
                ddlSelectCity.DataSource = null;
                ddlSelectCity.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            DataTable dtUser = (DataTable)Session["salesSection"];

            obj.userName = txtEmail.Text.Trim();
            obj.email = txtEmail.Text.Trim();

            if (!obj.getUserByEmail() && !obj.getUserByUserName())
            {
                obj.createdBy = Convert.ToInt32(dtUser.Rows[0]["userID"]);
                obj.firstName = txtFirstName.Text.Trim();
                obj.lastName = txtLastName.Text.Trim();
                obj.userName = txtEmail.Text.Trim();
                obj.userPassword = txtPassword.Text.Trim();
                obj.email = txtEmail.Text.Trim();
                obj.userTypeID = 5;
                obj.isActive = Convert.ToBoolean(chkIsActive.Checked);
                //if (ddlUserType.SelectedValue == "3" || ddlUserType.SelectedValue == "4" || ddlUserType.SelectedValue == "5")
                //{
                obj.countryId = 2;
                obj.provinceId = Convert.ToInt32(ddlProvinceLive.SelectedValue);
                obj.cityId = Convert.ToInt32(ddlSelectCity.SelectedValue);
                obj.howYouKnowUs = "";
                obj.phoneNo = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();
                // }
                long result = obj.createUser();
                Misc.addSubscriberEmail(txtEmail.Text.Trim(), ddlSelectCity.SelectedValue);
                bindGrid();
                lblMessage.Text = "Record has been added successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
            }
            else
            {
                lblDError.Text = "Username / Email already exists. Please choose another.";
                lblDError.Visible = true;
                imgAddError.Visible = true;
                cvPhone.Enabled = false;
                strtblHide = "block";
                strRestHide = "none";

               

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

    #region Function to Sync Menu
    private void syncMenu(long intCuisineID, long intUserID)
    {
        try
        {
            DataTable dtResturantMenu = null;
            objRMenu.cuisineID = intCuisineID;
            dtResturantMenu = objRMenu.getRestaurantMenuByCuisineID();
            if (dtResturantMenu != null && dtResturantMenu.Rows.Count > 0)
            {
                for (int i = 0; i < dtResturantMenu.Rows.Count; i++)
                {
                    objMMenu.foodType = dtResturantMenu.Rows[i]["foodType"].ToString();
                    objMMenu.cuisineID = Convert.ToInt64(dtResturantMenu.Rows[i]["cuisineID"].ToString());
                    objMMenu.foodImage = dtResturantMenu.Rows[i]["foodImage"].ToString();
                    objMMenu.createdBy = intUserID;
                    long intMenuID = objMMenu.createMemberMenu();
                    if (intMenuID != 0)
                    {
                        objRMenuItms.foodTypeId = Convert.ToInt64(dtResturantMenu.Rows[i]["foodTypeID"].ToString());
                        DataTable dtMenuItems = null;
                        dtMenuItems = objRMenuItms.getRestaurantMenuItemsByFoodTypeID();
                        if (dtMenuItems != null && dtMenuItems.Rows.Count > 0)
                        {
                            for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                            {
                                objMMenuItems.createdBy = intUserID;
                                objMMenuItems.foodTypeId = intMenuID;
                                objMMenuItems.itemName = dtMenuItems.Rows[a]["itemName"].ToString();
                                objMMenuItems.itemSubname = dtMenuItems.Rows[a]["itemSubname"].ToString();
                                objMMenuItems.itemDescription = dtMenuItems.Rows[a]["itemDescription"].ToString();
                                objMMenuItems.itemPrice = float.Parse(dtMenuItems.Rows[a]["itemPrice"].ToString());
                                objMMenuItems.createMemberMenuItems();
                            }
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion

    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            DataTable dtUser = (DataTable)Session["user"];
            obj.email = txtEmail.Text.Trim();
            obj.userName = txtEmail.Text.Trim();
            if (!txtEmail.Text.Trim().ToLower().Equals(ViewState["email"].ToString().ToLower()))
            {
                if (obj.getUserByEmail())
                {
                    lblDError.Text = "Email already exists. Please choose another.";
                    lblDError.Visible = true;
                    imgAddError.Visible = true;
                    return;
                }
            }

            obj.isActive = chkIsActive.Checked;
            obj.modifiedBy = Convert.ToInt32(dtUser.Rows[0]["userID"]);
            obj.userId = Convert.ToInt32(ViewState["userID"]);
            obj.firstName = txtFirstName.Text.Trim();
            obj.lastName = txtLastName.Text.Trim();
            obj.userName = txtEmail.Text.Trim();
            obj.userPassword = txtPassword.Text.Trim();
            obj.email = txtEmail.Text.Trim();
            obj.userTypeID = 5;
            obj.countryId = 2;
            obj.provinceId = Convert.ToInt32(ddlProvinceLive.SelectedValue);
            obj.cityId = Convert.ToInt32(ddlSelectCity.SelectedValue);
            obj.phoneNo = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();
            int result = obj.updateUser();



            if (result == -1)
            {
                bindGrid();
                lblMessage.Text = "Record has been update successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
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
        bool result = false;
        try
        {
            obj.userId = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value);
            result = obj.deleteUser();
            if (result)
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
                lblMessage.Text = "User could not be deleted.";
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
    protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        bool result = false;
        try
        {
            obj.userId = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);
            Label lblStatus = (Label)pageGrid.Rows[e.NewEditIndex].FindControl("lblStatus");
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
            result = obj.changeUserStatus();
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
    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtUser = null;
        try
        {
            lblDError.Visible = false;
            imgAddError.Visible = false;
            obj.userId = Convert.ToInt32(pageGrid.SelectedDataKey.Value);
            dtUser = obj.getUserByID();

            
            rfvFirstName.Enabled = true;
            rfvLastName.Enabled = true;
            // rfvUserName.Enabled = true;
            rfvPassword.Enabled = true;
            rfvEmail.Enabled = true;
            //rfvRefID.Enabled = true;
            //cvProvince.Enabled = true;
            // rfvHowYouKnowUs.Enabled = true;



            //rfvResName.Enabled = true;
            //rfvResBusinessName.Enabled = true;
            //rfvResAddress.Enabled = true;
            //rfvCity.Enabled = true;
            cvPhone.Enabled = true;
            //rfvFax.Enabled = true;
            //rfvZipCode.Enabled = true;
            //rfvCousineType.Enabled = true;
            //rfvResDetail.Enabled = true;

            txtPhone1.Text = "";
            txtPhone2.Text = "";
            txtPhone3.Text = "";

            if ((dtUser != null) && (dtUser.Columns.Count > 0) && (dtUser.Rows.Count > 0))
            {
                ViewState["userID"] = dtUser.Rows[0]["userID"];

                txtFirstName.Text = dtUser.Rows[0]["firstName"].ToString();
                txtLastName.Text = dtUser.Rows[0]["lastName"].ToString();
                // txtUsername.Text = dtUser.Rows[0]["username"].ToString();
                ViewState["userName"] = dtUser.Rows[0]["userName"].ToString();
                txtPassword.Text = dtUser.Rows[0]["userPassword"].ToString();
                bindProvincesByCountryID(Convert.ToInt32(dtUser.Rows[0]["countryID"]));

                ddlProvinceLive.SelectedValue = dtUser.Rows[0]["provinceID"].ToString();
                try
                {
                    FillCitpDroDownList(dtUser.Rows[0]["provinceID"].ToString());
                    if (ddlSelectCity.Items.Count > 0)
                    {
                        ddlSelectCity.SelectedValue = dtUser.Rows[0]["cityId"].ToString();
                    }
                }
                catch (Exception ex)
                { }
                hfProvince.Value = dtUser.Rows[0]["provinceID"].ToString();

                if (dtUser.Rows[0]["phoneNo"].ToString() != "" && dtUser.Rows[0]["phoneNo"].ToString() != null)
                {
                    string[] str = dtUser.Rows[0]["phoneNo"].ToString().Split('-');
                    if (str.Length == 3)
                    {
                        txtPhone1.Text = str[0];
                        txtPhone2.Text = str[1];
                        txtPhone3.Text = str[2];
                    }
                }


                if (dtUser.Rows[0]["userTypeID"].ToString() == "2")
                {
                    strtblHide = "none";
                    //  ddlProvinceLive.SelectedIndex = -1;
                    // ddlHowYouKnowUs.SelectedIndex = 0;
                    strtblHide = "none";
                    strRestHide = "none";

                    //rfvRefID.Enabled = false;
                    //cvProvince.Enabled = false;
                    //  rfvHowYouKnowUs.Enabled = false;

                    //rfvResName.Enabled = false;
                    //rfvResBusinessName.Enabled = false;
                    //rfvResAddress.Enabled = false;
                    //rfvCity.Enabled = false;
                    cvPhone.Enabled = true;
                    //rfvFax.Enabled = false;
                    //rfvZipCode.Enabled = false;
                    //rfvCousineType.Enabled = false;
                    //rfvResDetail.Enabled = false;
                }
                else
                {
                    strtblHide = "none";
                    strRestHide = "none";

                    //ddlHowYouKnowUs.SelectedValue = dtUser.Rows[0]["howYouKnowUs"].ToString();

                    if (dtUser.Rows[0]["userTypeID"].ToString() == "3")
                    {
                        //strRestHide = "block";
                        //objRes.userId = Convert.ToInt64(dtUser.Rows[0]["userID"]);
                        //DataTable dtRes = objRes.getRestaurantByUserID();
                        //if (dtRes != null && dtRes.Rows.Count > 0)
                        //{
                        //    ViewState["restaurantId"] = dtRes.Rows[0]["restaurantId"].ToString();
                        //    txtResName.Text = dtRes.Rows[0]["restaurantName"].ToString();
                        //    txtResBusinessName.Text = dtRes.Rows[0]["restaurantBusinessName"].ToString();
                        //    txtResAddress.Text = dtRes.Rows[0]["restaurantAddress"].ToString();
                        //    txtZipCode.Text = dtRes.Rows[0]["zipCode"].ToString();
                        //    txtCity.Text = dtRes.Rows[0]["city"].ToString();
                        //    txtFax.Text = dtRes.Rows[0]["fax"].ToString();
                        //    txtResDetail.Text = dtRes.Rows[0]["detail"].ToString();
                        //    if (dtRes.Rows[0]["phone"].ToString() != "" && dtRes.Rows[0]["phone"].ToString() != null)
                        //    {
                        //        string[] str = dtRes.Rows[0]["phone"].ToString().Split('-');
                        //        txtPhone1.Text = str[0];
                        //        txtPhone2.Text = str[1];
                        //        txtPhone3.Text = str[2];
                        //    }
                        //    else
                        //    {
                        //        txtPhone1.Text = "";
                        //        txtPhone2.Text = "";
                        //        txtPhone3.Text = "";
                        //    }
                        //    ddlCousineType.SelectedValue = dtRes.Rows[0]["cuisineId"].ToString();
                        //    ViewState["cuisineId"] = dtRes.Rows[0]["cuisineId"].ToString();
                        //}
                    }
                    else if (dtUser.Rows[0]["userTypeID"].ToString() == "4" || dtUser.Rows[0]["userTypeID"].ToString() == "5")
                    {
                        //rfvResName.Enabled = false;
                        //rfvResBusinessName.Enabled = false;
                        //rfvResAddress.Enabled = false;
                        //rfvCity.Enabled = false;
                        cvPhone.Enabled = true;
                        //rfvFax.Enabled = false;
                        //rfvZipCode.Enabled = false;
                        //rfvCousineType.Enabled = false;
                        //rfvResDetail.Enabled = false;
                    }
                }

                txtEmail.Text = dtUser.Rows[0]["userName"].ToString();

                ViewState["email"] = dtUser.Rows[0]["userName"].ToString();

                chkIsActive.Checked = Convert.ToBoolean(dtUser.Rows[0]["isActive"]);

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
    protected void btnShowAll_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Query"] = null;
        lblMessage.Visible = false;
        imgGridMessage.Visible = false;
        pageGrid.PageIndex = 0;
        bindGrid();
        txtSearchUserName.Text = "";
        txtSearchFirstName.Text = "";
        txtSearchLastName.Text = "";
    }
    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    {
        int check = 0;
        bool result = false;

        try
        {
            for (int i = 0; i < pageGrid.Rows.Count; i++)
            {
                CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");
                if (chkSub.Checked)
                {
                    Label lblID = (Label)pageGrid.Rows[i].FindControl("lblID1");
                    obj.userId = Convert.ToInt32(lblID.Text);
                    result = obj.deleteUser();
                    if (result)
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
    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            // txtUsername.Text = "";
            txtEmail.Text = "";
            //txtFriendsRerralId.Text = "";            
            //txtFriendsRerralId.ReadOnly = false;
            //txtFriendsRerralId.Text = "";
            txtResBusinessName.Text = "";
            txtZipCode.Text = "";
            // ddlCountry.SelectedIndex = 0;
            ddlProvinceLive.SelectedIndex = 0;
            ddlSelectCity.DataSource = null;
            ddlSelectCity.DataBind();
            // ddlHowYouKnowUs.SelectedIndex = 0;
            hfCountry.Value = "0";
            hfProvince.Value = "0";
            chkIsActive.Checked = false;

            txtResName.Text = "";
            txtResAddress.Text = "";
            txtCity.Text = "";
            txtFax.Text = "";
            txtResDetail.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            txtPhone3.Text = "";
            ddlCousineType.SelectedIndex = 0;

            //rfvResName.Enabled = false;
            //rfvResAddress.Enabled = false;
            //rfvCity.Enabled = false;
            //rfvFax.Enabled = false;
            //rfvCousineType.Enabled = false;
            //rfvResDetail.Enabled = false;

            strtblHide = "none";
            strRestHide = "none";

            //rfvHowYouKnowUs.Enabled = false;
            //cvCountry.Enabled = false;
            //rfvRefID.Enabled = false;
            //cvProvince.Enabled = false;


            lblDError.Visible = false;
            imgAddError.Visible = false;
            btnSave.Visible = true;
            btnUpdate.Visible = false;

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
                dtUser = obj.GetAllSalesUsers();
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
            ListItem objList = new ListItem("All", obj.getAllUsers().Rows.Count.ToString());
            ddlPage.Items.Insert(5, objList);
            return ddlPage;
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
            return null;
        }
    }

    protected void showUserDetails(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("showUserDetails.aspx");
    }
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
            DataTable dtUser = null;
            if (ViewState["Query"] != null)
            {
                dtUser = Misc.search(ViewState["Query"].ToString());
            }
            else
            {
                dtUser = obj.getAllUsers();
            }
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
            lblTotalRecords.Text = dtUser.Rows.Count.ToString();
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


    protected void pageGrid_Login(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtUser = null;
        try
        {
            if (e.CommandName == "Detail")
            {
                //obj.userId = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                //int rIndex = row.RowIndex;
                ////Label lblAffiliate = (Label)pageGrid.Rows[rIndex].FindControl("lblAffiliate");
                //string email = pageGrid.Rows[rIndex].Cells[3].Text;

                obj.userId = Convert.ToInt32(e.CommandArgument);
                //dtUser = obj.getUserByID();
                //Session["member"] = dtUser;                
                //Response.Redirect("showUserDetails.aspx");

            }
            if (e.CommandName == "Login")
            {
                lblDError.Visible = false;
                imgAddError.Visible = false;
                obj.userId = Convert.ToInt32(e.CommandArgument);
                dtUser = obj.getUserByID();
                obj.userName = dtUser.Rows[0]["userName"].ToString();
                obj.userPassword = dtUser.Rows[0]["userPassword"].ToString();
                dtUser = obj.validateUserNamePassword();
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    Session.RemoveAll();
                    if (dtUser.Rows[0]["userTypeID"].ToString() == "1" || dtUser.Rows[0]["userTypeID"].ToString() == "2")
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
                    pnlForm.Visible = false;
                    pnlGrid.Visible = true;
                    lblMessage.Text = "User could not login as user is deactive.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (e.CommandName == "Affiliate")
            {
                try
                {
                    obj.userId = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    int rIndex = row.RowIndex;
                    Label lblAffiliate = (Label)pageGrid.Rows[rIndex].FindControl("lblAffiliate");
                    if (lblAffiliate != null)
                    {
                        if (lblAffiliate.Text.ToString() == "True")
                        {
                            obj.isAffiliate = false;
                        }
                        else
                        {
                            obj.isAffiliate = true;
                        }
                    }
                    bool result = obj.changeUserAffiliateStatus();
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
