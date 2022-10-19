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

public partial class admin_userManagement : System.Web.UI.Page
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindCountry();
            //bindProvinces(2);
           // bindCountries();
            bindCousines();
            bindGrid(0);
            if ((Request.QueryString["uid"] != null) && (Request.QueryString["uid"].Trim() != ""))
            {
                editUser();
            }
        }
    }

    protected void editUser()
    {
        DataTable dtUser = null;
        try
        {
            lblDError.Visible = false;
            imgAddError.Visible = false;
            obj.userId = Convert.ToInt32(Request.QueryString["uid"].Trim());
            dtUser = obj.getUserByID();

            rfvUserType.Enabled = true;
            rfvFirstName.Enabled = true;
            rfvLastName.Enabled = true;            
            rfvPassword.Enabled = true;
            rfvEmail.Enabled = true;            
            cvPhone.Enabled = true;
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            txtPhone3.Text = "";

            if ((dtUser != null) && (dtUser.Columns.Count > 0) && (dtUser.Rows.Count > 0))
            {
                ViewState["userID"] = dtUser.Rows[0]["userID"];

                txtFirstName.Text = dtUser.Rows[0]["firstName"].ToString();
                txtLastName.Text = dtUser.Rows[0]["lastName"].ToString();              
                ViewState["userName"] = dtUser.Rows[0]["userName"].ToString();
                txtPassword.Text = dtUser.Rows[0]["userPassword"].ToString();
                ddlUserType.SelectedValue = dtUser.Rows[0]["userTypeID"].ToString();

                try
                {
                    ddlCountry.SelectedValue = dtUser.Rows[0]["countryID"].ToString();
                    bindProvinces(Convert.ToInt32(dtUser.Rows[0]["countryID"]));
                    ddlProvinceLive.SelectedValue = dtUser.Rows[0]["provinceID"].ToString();
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
                    strtblHide = "none";
                    strRestHide = "none";
                    cvPhone.Enabled = true;
                }
                else
                {
                    strtblHide = "none";
                    strRestHide = "none";                   
                    if (dtUser.Rows[0]["userTypeID"].ToString() == "4" || dtUser.Rows[0]["userTypeID"].ToString() == "5")
                    {
                        cvPhone.Enabled = true;                    
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

        GetAndSetPostsByDealId(Convert.ToInt32(ViewState["userID"]));
        SetFeilds(0);
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
    private void BindSearchedData(string strRowIndex)
    {
        try
        {          
            string strQuery = "";
            if (txtSearchFirstName.Text.Trim() != "" || txtSearchLastName.Text.Trim() != "" || txtSearchUserName.Text.Trim() != "")
            {
                strQuery = "Select * from (";
                strQuery += " SELECT ";
                strQuery += " userInfo.userID, ";
                strQuery += " userType.userTypeID, ";
                strQuery += " userType.userType, ";                
                strQuery += " userInfo.userName, ";
                strQuery += " userInfo.ipAddress, ";                                
                strQuery += " dbo.InitCap(userInfo.firstName) as firstName, ";
                strQuery += " dbo.InitCap(userInfo.lastName) as lastName, ";                                
                strQuery += " userInfo.creationDate, ";                                
                strQuery += " userInfo.isActive,isAffiliate, ";
                strQuery += " ROW_NUMBER() OVER(ORDER BY userInfo.userID desc) as RowNum";
                strQuery += " FROM userInfo ";
                strQuery += " inner join userType on userType.userTypeID = userInfo.userTypeID ";
                strQuery += " where isDeleted = 0 and userInfo.userTypeId <> 1 ";

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

                strQuery += " SELECT 'Return Value' =  COUNT(userID) FROM userInfo";
                strQuery += " inner join userType on userType.userTypeID = userInfo.userTypeID ";
                strQuery += " where isDeleted = 0 and userInfo.userTypeId <> 1 ";

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
                dst = obj.getAllUsersWithIndexing(intPageNumber, pageGrid.PageSize);
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
    protected void bindCountry()
    {
        try
        {
            DataTable dt = Misc.getAllCountries();
            ddlCountry.DataSource = dt.DefaultView;
            ddlCountry.DataTextField = "countryName";
            ddlCountry.DataValueField = "countryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "Select One");
            //FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);
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
    protected void bindProvinces(int counryID)
    {
        try 
        {
            DataTable dt = Misc.getProvincesByCountryID(counryID);
            ddlProvinceLive.DataSource = dt.DefaultView;
            ddlProvinceLive.DataTextField = "provinceName";
            ddlProvinceLive.DataValueField = "provinceId";
            ddlProvinceLive.DataBind();
            FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);
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
 
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindProvinces(Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

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

           /* if (ddlUserType.SelectedIndex == 2)
            {
                string strAddress = txtResAddress.Text.Trim() + ", " + txtCity.Text.Trim() + ", " + ddlProvinceLive.SelectedItem.Text.Trim() + ", " + txtZipCode.Text.Trim() + ", Canada";
                if (!Misc.validateAddress(strAddress))
                {
                    lblDError.Text = "Your given address is not correct.";                    
                    lblDError.Visible = true;
                    imgAddError.Visible = true;
                    if (ddlUserType.SelectedIndex == 1)
                    {
                        //rfvHowYouKnowUs.Enabled = false;
                        // cvCountry.Enabled = false;
                       // rfvRefID.Enabled = false;
                        //cvProvince.Enabled = false;
                        strtblHide = "block";
                        strRestHide = "none";
                    }
                    else
                    {
                        if (ddlUserType.SelectedIndex == 2)
                        {
                            rfvResName.Enabled = true;
                            rfvResBusinessName.Enabled = true;
                            rfvResAddress.Enabled = true;
                            rfvCity.Enabled = true;
                            rfvFax.Enabled = true;
                            rfvCousineType.Enabled = true;
                            rfvResDetail.Enabled = true;
                            cvPhone.Enabled = true;
                            rfvZipCode.Enabled = true;
                            strtblHide = "block";
                            strRestHide = "block";
                        }
                        else
                        {
                            rfvResName.Enabled = false;
                            rfvResBusinessName.Enabled = false;
                            rfvResAddress.Enabled = false;
                            rfvCity.Enabled = false;
                            rfvFax.Enabled = false;
                            rfvCousineType.Enabled = false;
                            rfvResDetail.Enabled = false;
                            cvPhone.Enabled = false;
                            rfvZipCode.Enabled = false;
                            strtblHide = "block";
                            strRestHide = "none";
                        }
                        //rfvHowYouKnowUs.Enabled = true;
                        //cvCountry.Enabled = true;
                       // rfvRefID.Enabled = true;
                        //cvProvince.Enabled = true;
                       
                    }
                    return;
                }
            }  */          
            DataTable dtUser = (DataTable)Session["user"];

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
                if (ddlUserType.SelectedIndex == 0)
                {
                    obj.userTypeID = 4;
                }
                else
                {
                    obj.userTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
                }
                
                obj.isActive = Convert.ToBoolean(chkIsActive.Checked);
                //if (ddlUserType.SelectedValue == "3" || ddlUserType.SelectedValue == "4" || ddlUserType.SelectedValue == "5")
                //{
                obj.countryId = Convert.ToInt32(ddlCountry.SelectedValue.Trim());
                obj.provinceId = Convert.ToInt32(ddlProvinceLive.SelectedValue);
                obj.cityId = Convert.ToInt32(ddlSelectCity.SelectedValue);
                obj.howYouKnowUs = "";
                obj.phoneNo = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();
                obj.ipAddress = Request.UserHostAddress.ToString();

                if (fuUserProfilePic.HasFile)
                {
                    string[] strExtension = fuUserProfilePic.FileName.Split('.');
                    string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                    string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "images\\temp\\" + fuUserProfilePic.FileName;
                    fuUserProfilePic.SaveAs(strSrcPath);
                    string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\";
                    string SrcFileName = fuUserProfilePic.FileName;
                    Misc.CreateThumbnail(strSrcPath, strthumbSave, "us", strUniqueID);
                    obj.profilePicture = strUniqueID;                                        
                }
                
                long result = obj.createUser();
                Misc.addSubscriberEmail(txtEmail.Text.Trim(), ddlSelectCity.SelectedValue);
                
                if (result > 0 && ddlUserType.SelectedValue == "3")
                {
                    /*objRes.userId = result;
                    objRes.restaurantName = txtResName.Text.Trim();
                    objRes.restaurantAddress = txtResAddress.Text.Trim();
                    objRes.restaurantBusinessName = txtResBusinessName.Text.Trim();
                    objRes.city = txtCity.Text.Trim();
                    objRes.phone = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();
                    objRes.fax = txtFax.Text.Trim();
                    objRes.cuisineId = Convert.ToInt32(ddlCousineType.SelectedValue);
                    objRes.detail = txtResDetail.Text.Trim();
                    objRes.zipCode = txtZipCode.Text.Trim();
                    objRes.createdBy = Convert.ToInt32(dtUser.Rows[0]["userID"]);
                    objRes.createRestaurant();
                    //syncMenu(Convert.ToInt64(ddlCousineType.SelectedValue), result);*/
                }
                bindGrid(0);
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

                if (ddlUserType.SelectedIndex == 1)
                {
                    //cvProvince.Enabled = false;
                    strtblHide = "block";
                    strRestHide = "none";
                }
                else
                {
                    if (ddlUserType.SelectedIndex == 2)
                    {
                        //rfvResName.Enabled = true;
                        //rfvResBusinessName.Enabled = true;
                        //rfvResAddress.Enabled = true;
                        //rfvCity.Enabled = true;
                        //rfvFax.Enabled = true;
                        //rfvCousineType.Enabled = true;
                        //rfvResDetail.Enabled = true;
                        cvPhone.Enabled = true;
                        //rfvZipCode.Enabled = true;
                        strtblHide = "block";
                        strRestHide = "block";
                    }
                    else
                    {
                        //rfvResName.Enabled = false;
                        //rfvResBusinessName.Enabled = false;
                        //rfvResAddress.Enabled = false;
                        //rfvCity.Enabled = false;
                        //rfvFax.Enabled = false;
                        //rfvCousineType.Enabled = false;
                        //rfvResDetail.Enabled = false;
                        cvPhone.Enabled = false;
                        //rfvZipCode.Enabled = false;
                        strtblHide = "block";
                        strRestHide = "none";
                    }
                    //rfvHowYouKnowUs.Enabled = true;
                    //cvCountry.Enabled = true;
                    //rfvRefID.Enabled = true;
                    //cvProvince.Enabled = true;
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
                Misc.addSubscriberEmail(txtEmail.Text.Trim(), ddlSelectCity.SelectedValue);
            }

            obj.isActive = chkIsActive.Checked;
            obj.modifiedBy = Convert.ToInt32(dtUser.Rows[0]["userID"]);
            obj.userId = Convert.ToInt32(ViewState["userID"]);
            obj.firstName = txtFirstName.Text.Trim();
            obj.lastName = txtLastName.Text.Trim();
            obj.userName = txtEmail.Text.Trim();
            obj.userPassword = txtPassword.Text.Trim();
            obj.email = txtEmail.Text.Trim();
            if (ddlUserType.SelectedIndex == 0)
            {
                obj.userTypeID = 4;
            }
            else
            {
                obj.userTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
            }
            obj.countryId = Convert.ToInt32(ddlCountry.SelectedValue.Trim());                
            obj.provinceId = Convert.ToInt32(ddlProvinceLive.SelectedValue);
            obj.cityId = Convert.ToInt32(ddlSelectCity.SelectedValue);
            obj.phoneNo = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();

            if (fuUserProfilePic.HasFile)
            {
                string[] strExtension = fuUserProfilePic.FileName.Split('.');
                string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "images\\temp\\" + fuUserProfilePic.FileName;
                fuUserProfilePic.SaveAs(strSrcPath);
                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\";
                string SrcFileName = fuUserProfilePic.FileName;
                Misc.CreateThumbnail(strSrcPath, strthumbSave, "us", strUniqueID);
                obj.profilePicture = strUniqueID;
            }
            else if (ViewState["PicName"] != null)
            {
                obj.profilePicture = ViewState["PicName"].ToString();
                ViewState["PicName"] = null;
            }

            int result = obj.updateUser();



            if (result == -1)
            {
                TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                int intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());
                bindGrid(intCurrentPage - 1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = (intCurrentPage).ToString();
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
        ViewState["userID"] = null; 
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
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            int intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());
            
            obj.userId = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value);
            result = obj.deleteUser();
            if (result)
            {
                ViewState["Query"] = null;
                bindGrid(intCurrentPage-1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = (intCurrentPage).ToString();   
                lblMessage.Text = "Record has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; 
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                bindGrid(intCurrentPage-1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = (intCurrentPage).ToString();   
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
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            int intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());

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
                bindGrid(intCurrentPage - 1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = (intCurrentPage).ToString();   
                lblMessage.Text = "Status has been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; 
                lblMessage.ForeColor = System.Drawing.Color.Black;

            }
            else
            {
                bindGrid(intCurrentPage - 1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = (intCurrentPage).ToString();   
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

            rfvUserType.Enabled = true;
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
                ddlUserType.SelectedValue = dtUser.Rows[0]["userTypeID"].ToString();

                try
                {
                    ddlCountry.SelectedValue = dtUser.Rows[0]["countryID"].ToString();
                    bindProvinces(Convert.ToInt32(dtUser.Rows[0]["countryID"]));
                    ddlProvinceLive.SelectedValue = dtUser.Rows[0]["provinceID"].ToString();
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
                imgProfilePic.Visible = true;
                if (dtUser.Rows[0]["profilePicture"] != null && dtUser.Rows[0]["profilePicture"].ToString().Trim() != "")
                {
                    string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + dtUser.Rows[0]["profilePicture"].ToString().Trim();
                    if (File.Exists(strFileName))
                    {
                        ViewState["PicName"] = dtUser.Rows[0]["profilePicture"].ToString().Trim();
                        imgProfilePic.ImageUrl = "~/images/ProfilePictures/" + dtUser.Rows[0]["profilePicture"].ToString().Trim();
                    }
                    else if (Session["FBImage"] != null)
                    {
                        imgProfilePic.ImageUrl = Session["FBImage"].ToString();
                    }
                    else
                    {
                        imgProfilePic.ImageUrl = "~/images/NoPhotoAvailable.jpg";
                    }
                }
                else if (Session["FBImage"] != null)
                {
                    imgProfilePic.ImageUrl = Session["FBImage"].ToString();
                }
                else
                {
                    imgProfilePic.ImageUrl = "~/images/NoPhotoAvailable.jpg";
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

        GetAndSetPostsByDealId(Convert.ToInt32(ViewState["userID"]));
        SetFeilds(0);
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
        bindGrid(0);
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
                bindGrid(0);
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
            ddlUserType.SelectedIndex = 0;
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
                dtUser = obj.getAllUsersWithIndexing(0,pageGrid.PageSize).Tables[0];
            }            
            pnlGrid.Visible = true;
            pnlForm.Visible = false;
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            ViewState["userID"] = null; 
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
        ViewState["userID"] = null; 
    }
    protected void pageGrid_Login(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtUser = null;
        try
        {
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
                    if (dtUser.Rows[0]["userTypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["userTypeID"].ToString().Trim() == "2" || dtUser.Rows[0]["userTypeID"].ToString().Trim() == "6" || dtUser.Rows[0]["userTypeID"].ToString().Trim() == "7")
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
                    TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                    int intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());
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
                        bindGrid(intCurrentPage-1);
                        txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                        txtPage.Text = intCurrentPage.ToString();
                        lblMessage.Text = "Status has been changed successfully.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/Checked.png";
                        lblMessage.ForeColor = System.Drawing.Color.Black;

                    }
                    else
                    {
                        bindGrid(intCurrentPage - 1);
                        txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                        txtPage.Text = intCurrentPage.ToString();
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

    #region Discuession Code
    #region "Check user Is Logged In or not"

    private void SetFeilds(int set)
    {
        try
        {
            this.txtComment.Enabled = true;
            txtComment.Text = "";
            this.btnPost.Visible = true;
            this.btnCancel.Visible = true;            
            lblCommentMessage.Text = "";         
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #endregion

    protected void btnPost_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["userID"] != null)
            {
                DataTable dtUser = (DataTable)Session["user"];

                BLLUserComments obj = new BLLUserComments();
                obj.comment = txtComment.Text.Trim().Replace("\n", "<br>");
                obj.userId = Convert.ToInt64(ViewState["userID"]);
                obj.commentby = Convert.ToInt64(dtUser.Rows[0]["userID"].ToString());
                obj.AddNewUserComments();
                SetFeilds(0);
                //Get All the Posts here By Deal Id
                GetAndSetPostsByDealId(Convert.ToInt32(ViewState["userID"]));
                lblCommentMessage.Visible = true;
                lblCommentMessage.Text = "Comment save successfully.";
                txtComment.Text = "";
            }
            else
            {
                lblCommentMessage.Text = "Please add user first.";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SetFeilds(0);            
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void GetAndSetPostsByDealId(int iDealId)
    {
        try
        {

            BLLUserComments objBLLUserComments = new BLLUserComments();

            objBLLUserComments.userId = iDealId;

            DataTable dtPosts = objBLLUserComments.getUserCommentsByUserId();

            if ((dtPosts != null) && (dtPosts.Rows.Count > 0))
            {
                this.rptrDiscussion.DataSource = dtPosts;
                this.rptrDiscussion.DataBind();
            }
            else
            {
                this.rptrDiscussion.DataSource = null;
                this.rptrDiscussion.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    BLLUser objUser = new BLLUser();

    protected void DataListItemDataBound(Object src, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgDis = (Image)e.Item.FindControl("imgDis");

            //if (Session["FBImage"] == null)
            //{
            HiddenField hfUserID = (HiddenField)e.Item.FindControl("hfUserID");
            objUser.userId = Convert.ToInt32(hfUserID.Value);
            DataTable dtUserInfo = objUser.getUserByID();

            string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + imgDis.ImageUrl.Trim().Trim();
            if (File.Exists(strFileName))
            {
                ViewState["PicName"] = imgDis.ImageUrl.Trim().Trim();
                imgDis.ImageUrl = "~/images/ProfilePictures/" + imgDis.ImageUrl.Trim().Trim();
            }
            else if (dtUserInfo != null && dtUserInfo.Rows.Count > 0 && (dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() != ""))
            {
                imgDis.ImageUrl = "https://graph.facebook.com/" + dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() + "/picture";
            }
            else
            {
                imgDis.ImageUrl = "~/Images/disImg.gif";
            }



        }
    }
   
    #endregion
}
