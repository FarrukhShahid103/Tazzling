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
using GecLibrary;
using System.Data.SqlClient;
using SQLHelper;

public partial class ResendOrders : System.Web.UI.Page
{
    #region Global Variables

    BLLDealOrderDetail objBLLDealOrderDetail = new BLLDealOrderDetail();
    BLLDealOrders obj = new BLLDealOrders();
    int detailID = 0;
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
            
            DataTable dtUser = null;
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                }
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "login.aspx");
            }

            if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "")
            {
                BLLProductSize bSize = new BLLProductSize();
                bSize.productID = Convert.ToInt64(Request.QueryString["did"].ToString());
                DataTable dtSize = bSize.getProductSizeByProductID();
                if (dtSize != null && dtSize.Rows.Count > 0)
                {
                    dSize.Visible = true;
                    ddlSize.DataSource = dtSize;                    
                    ddlSize.DataMember = "sizeID";
                    ddlSize.DataValueField = "sizeText";
                    ddlSize.DataBind();

                }
                    bindGrid();
            }
            hfdOrderID.Value = "";
        }
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
                obj.dealId = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
                dtCities = obj.getAllResendOrdersByProductID();
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
                txtSearchCustomerName.Enabled = true;
                

            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnSearch.Enabled = false;
                txtSearchCustomerName.Enabled = false;                
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
        obj.dealId = Convert.ToInt64(Request.QueryString["did"].Trim());
        ListItem objList = new ListItem("All", obj.getAllResendOrdersByProductID().Rows.Count.ToString());
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
            
            
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void pageGrid_Command(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edit")
        {
            if (e.CommandArgument.ToString() != string.Empty)
            {
                detailID = Convert.ToInt32(e.CommandArgument.ToString());
            }
            return;
        }

        if (e.CommandName.ToLower() == "select")
        {

           
        }
    }

    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            obj.dOrderID = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value.ToString());
            if (obj.DeleteOrderDetail())
            {
                resetFields();
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
            obj.dealId = Convert.ToInt64(Request.QueryString["did"].Trim());
            dtCities = obj.getAllResendOrdersByProductID();
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

            strQuery += "SELECT ";
            strQuery += "distinct [dealOrders].[dOrderID]";
            strQuery += ",detailID";
            strQuery += ",dealOrders.dealId";
            strQuery += ",sellingPrice";
            strQuery += ",subTitle as shortTitle";
            strQuery += ",[title]";
            strQuery += ",null as voucherExpiryDate";
            strQuery += ",orderNo";
            strQuery += ",[dealOrders].[Qty]";
            strQuery += ",dealOrders.createdDate";
            strQuery += ",[dealOrders].[status]";
            strQuery += ",dealOrderDetail.displayIt";
            strQuery += ",Cast( customerNote as NVarchar(Max)) as  customerNote ";
            strQuery += ",dealOrderDetail.dealOrderCode ";
            strQuery += ",trackingNumber ";
            strQuery += ",[dealOrders].[status] ";
            strQuery += ",Address ";
            strQuery += ",Telephone ";
            strQuery += ",userName ";
            strQuery += "FROM   [dealOrders] ";
            strQuery += "inner join products on products.productID = dealOrders.dealId ";
            strQuery += "inner join dealOrderDetail on dealOrderDetail.dOrderID = dealOrders.dOrderID ";
            strQuery += "inner join userShippingInfo on dealOrders.shippingInfoId = userShippingInfo.shippingInfoId ";
            strQuery += "inner join userInfo on userInfo.userId = dealOrders.userId ";
            strQuery += "WHERE dealId=" + Request.QueryString["did"].Trim();
            strQuery += " AND resendOrders=1 ";
            strQuery += "and [dealOrders].isDeleted = 0 ";

            if (txtSearchCustomerName.Text.Trim() != "")
            {
                strQuery += " and userName LIKE '%" + txtSearchCustomerName.Text.ToString().Trim().Replace("'", "''") + "%'";
            }

            strQuery += "order by dOrderID ASC";
            pageGrid.PageIndex = 0;
            BindSearchedData(strQuery);
            ViewState["Query"] = strQuery;
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
            txtSearchCustomerName.Text = "";
            
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

    protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (detailID != 0)
            {
                int idetailOrderID = detailID;
                TextBox txtTrackNumber = (TextBox)pageGrid.Rows[e.NewEditIndex].FindControl("txtTrackNumber");
                objBLLDealOrderDetail.detailID = idetailOrderID;
                objBLLDealOrderDetail.trackingNumber = txtTrackNumber.Text.Trim();
                objBLLDealOrderDetail.updateTrackingNumber();
                //this.GetAllBusinessInfoAndFillGrid();
                lblMessage.Text = "Track number has been saved successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                resetFields();
            }

            else
            {
                //BLLResendOrders obj = new BLLResendOrders();
                GECEncryption objEnc = new GECEncryption();
                int dOrderID = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);
                obj.dOrderID = dOrderID;
                DataTable dOrder = obj.EditOrderDetail();
                if (dOrder != null && dOrder.Rows.Count > 0)
                {
                    txtEmail.Text = dOrder.Rows[0]["email"].ToString();
                    txtCustomerName.Text = dOrder.Rows[0]["Name"].ToString();
                    txtVoucherNumber.Text = objEnc.DecryptData("deatailOrder", dOrder.Rows[0]["dealOrderCode"].ToString()); //objEnc.EncryptData(
                    txtTrackingNumber.Text = dOrder.Rows[0]["trackingNumber"].ToString();
                    txtTelephone.Text = dOrder.Rows[0]["Telephone"].ToString();
                    txtCity.Text = dOrder.Rows[0]["City"].ToString();
                    txtProvince.Text = dOrder.Rows[0]["State"].ToString();
                    txtZipCode.Text = dOrder.Rows[0]["ZipCode"].ToString();
                    txtAddress.Text = dOrder.Rows[0]["Address"].ToString();
                    txtAddress2.Text = dOrder.Rows[0]["Address2"].ToString();
                    ddlShippingCountry.SelectedValue = dOrder.Rows[0]["shippingCountry"].ToString();
                    txtNote.Text = dOrder.Rows[0]["shippingNote"].ToString();
                    hfdOrderID.Value = dOrder.Rows[0]["dOrderID"].ToString();
                    try
                    {
                        if (ddlSize.Items.Count > 0)
                        {
                            ddlSize.SelectedValue = dOrder.Rows[0]["size"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    btnSave.ImageUrl = "~/admin/images/btnUpdate.jpg";
                }
            }
             /*TextBox txtTrackNumber = (TextBox)pageGrid.Rows[e.NewEditIndex].FindControl("txtTrackNumber");
             obj.resendOrderID = idetailOrderID;
             obj.resendOrder_trackingNumber = txtTrackNumber.Text.Trim();
             obj.updateResendOrdersTrackingNumber();
             this.GetAllBusinessInfoAndFillGrid();
             lblMessage.Text = "Track number has been saved successfully.";
             lblMessage.Visible = true;
             imgGridMessage.Visible = true;
             imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
             lblMessage.ForeColor = System.Drawing.Color.Black;*/
        }
        catch (Exception ex)
        { }
    }

    public string voucherNumberDec(string sVal)
    {
        GECEncryption objEnc = new GECEncryption();
        return objEnc.DecryptData("deatailOrder", sVal);
    }

    

    #region Button Cancel Click Event
    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearForm();
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
    #endregion
    

    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try 
        {
            txtAddress.Text = "";
            txtCustomerName.Text = "";
            txtNote.Text = "";
            txtTelephone.Text = "";
            txtVoucherNumber.Text = "";            
            btnSave.Visible = true;            
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
                    obj.dOrderID = Convert.ToInt32(lblID.Text);
                    //result = obj.de.deleteResendOrders();
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
            
            
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private string LogoUploadHere(FileUpload fileUploadDealImg)
    {
        string strUniqueID = "";
        try
        {
            if (fileUploadDealImg.HasFile)
            {
                string[] strExtension = fileUploadDealImg.FileName.Split('.');
                strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\resendOrder\\" + strUniqueID;
                fileUploadDealImg.SaveAs(strSrcPath);
            }
        }
        catch (Exception ex)
        {
        }
        return strUniqueID;
    }

    protected void clearForm()
    {
        hfresendOrderID.Value = "0";
        txtAddress.Text = "";
        txtCustomerName.Text = "";
        txtNote.Text = "";
        txtSearchCustomerName.Text = "";
        txtTrackingNumber.Text = "";
        txtTelephone.Text = "";
        txtVoucherNumber.Text = "";
        
        btnSave.Visible = true;       
    }

    private bool IsValidFields()
    {
        bool isValid = true;
        if (!Misc.validateEmailAddress(txtEmail.Text.Trim()) || txtEmail.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtEmail.Focus();
            return isValid;
        }
        if (txtCustomerName.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtCustomerName.Focus();
            return isValid;
        }
        
        if (txtVoucherNumber.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtVoucherNumber.Focus();
            return isValid;
        }
        if (txtTelephone.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtTelephone.Focus();
            return isValid;
        }
        if (txtCity.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtCity.Focus();
            return isValid;
        }
        if (txtProvince.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtProvince.Focus();
            return isValid;
        }
        if (txtZipCode.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtZipCode.Focus();
            return isValid;
        }
        if (txtAddress.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtAddress.Focus();
            return isValid;
        }
        if (txtAddress2.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtAddress2.Focus();
            return isValid;
        }
        if (ddlShippingCountry.SelectedItem.Text == string.Empty)
        {
            isValid = false;
            ddlShippingCountry.Focus();
            return isValid;
        }
        if (txtNote.Text.Trim() == string.Empty)
        {
            isValid = false;
            txtNote.Focus();
            return isValid;
        }
        return isValid;
    }

    private void resetFields()
    {
        txtEmail.Text = string.Empty;
        txtCustomerName.Text = string.Empty;
        txtVoucherNumber.Text = string.Empty;
        txtTrackingNumber.Text = string.Empty;
        txtTelephone.Text = string.Empty;
        txtCity.Text= string.Empty;
        txtProvince.Text= string.Empty;
        txtZipCode.Text = string.Empty;
        txtAddress.Text= string.Empty;
        txtAddress2.Text = string.Empty;
        ddlShippingCountry.SelectedIndex = 0;
        if (ddlSize.Items.Count > 0)
        {
            ddlSize.SelectedIndex = 0;
        }
        txtNote.Text =string.Empty;
        hfdOrderID.Value = "";
        btnSave.ImageUrl = "~/admin/Images/btnSave.jpg";
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (IsValidFields())
        {
            if (hfdOrderID.Value != "")
            {

                GECEncryption objEnc = new GECEncryption();
                long result = 0;

                SqlParameter[] param = new SqlParameter[16];
                param[0] = new SqlParameter("@userId", Convert.ToInt64(ViewState["userId"].ToString()));
                param[1] = new SqlParameter("@dOrderID", hfdOrderID.Value);
                param[2] = new SqlParameter("@dealOrderCode", objEnc.EncryptData("deatailOrder", txtVoucherNumber.Text.Trim()));
                param[3] = new SqlParameter("@trackingNumber", txtTrackingNumber.Text.Trim());
                param[4] = new SqlParameter("@trackerupdateDate", DateTime.Now);
                param[5] = new SqlParameter("@Name", txtCustomerName.Text.Trim());
                param[6] = new SqlParameter("@Telephone", txtTelephone.Text.Trim());
                param[7] = new SqlParameter("@City", txtCity.Text.Trim());
                param[8] = new SqlParameter("@State", txtProvince.Text.Trim());
                param[9] = new SqlParameter("@ZipCode", txtZipCode.Text.Trim());
                param[10] = new SqlParameter("@Address", txtAddress.Text.Trim());
                param[11] = new SqlParameter("@Address2", txtAddress2.Text.Trim());
                param[12] = new SqlParameter("@shippingCountry", ddlShippingCountry.SelectedValue.ToString());
                param[13] = new SqlParameter("@shippingNote", txtNote.Text.Trim());
                param[14] = new SqlParameter("@modifiedDate", DateTime.Now);
                if (ddlSize.Items.Count > 0)
                {
                    param[15] = new SqlParameter("@size", ddlSize.SelectedValue.ToString());
                }
                else
                {
                    param[15] = new SqlParameter("@size", string.Empty);
                }
                //result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spUpdateOrderDetail", param).Tables[0].Rows[0][0]);
                result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateOrderDetail", param);
                if (result != 0)
                {
                    resetFields();
                    lblMessage.Visible = true;
                    lblMessage.Text = "Record has been updated successfully.";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                    imgGridMessage.ImageUrl = "images/checked.png";
                    imgGridMessage.Visible = true;
                    bindGrid();
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please enter correct customer email.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    imgGridMessage.ImageUrl = "images/error.png";
                    imgGridMessage.Visible = true;
                }
            }
            else
            {
                try
                {
                    BLLUser objuser = new BLLUser();
                    objuser.email = txtEmail.Text.Trim();
                    DataTable dtUser = objuser.getUserDetailByEmail();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        long createdID = 0;
                        long shippingInfoID = 0;
                        GECEncryption objEnc = new GECEncryption();
                        BLLUserCCInfo objCCInfo = new BLLUserCCInfo();
                        objCCInfo.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtAddress.Text.Trim());
                        objCCInfo.ccInfoBAddress2 = HtmlRemoval.StripTagsRegexCompiled(txtAddress2.Text.Trim());
                        objCCInfo.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtCity.Text.Trim());
                        objCCInfo.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtZipCode.Text.Trim());
                        objCCInfo.ccInfoBProvince = txtProvince.Text.Trim();
                        objCCInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                        objCCInfo.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()); //Convert.ToInt64(ViewState["userId"].ToString());
                        objCCInfo.ccInfoCCVNumber = "0";
                        objCCInfo.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", DateTime.Now.Month + "-" + DateTime.Now.Year);
                        objCCInfo.ccInfoNumber = "0";

                        objCCInfo.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtCustomerName.Text.Trim()));
                        objCCInfo.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(txtEmail.Text.Trim());
                        objCCInfo.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(txtCustomerName.Text.Trim());
                        objCCInfo.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(txtCustomerName.Text.Trim());
                        createdID = objCCInfo.createUserCCInfo();

                        BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                        objShippingInfo.Address = txtAddress.Text.Trim();
                        objShippingInfo.Address2 = txtAddress2.Text.Trim();
                        objShippingInfo.City = txtCity.Text.Trim();
                        objShippingInfo.State = txtProvince.Text.Trim();
                        objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                        objShippingInfo.Name = txtCustomerName.Text.Trim();
                        objShippingInfo.Telephone = txtTelephone.Text.Trim();
                        objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                        objShippingInfo.userID = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
                        objShippingInfo.ZipCode = txtZipCode.Text.Trim();
                        objShippingInfo.shippingNote = txtNote.Text.Trim();
                        shippingInfoID = objShippingInfo.createUserShippingInfo();

                        BLLDealOrders objOrder = new BLLDealOrders();
                        objOrder.orderNo = GenerateId().ToString().Substring(1, 7) + "_" + DateTime.Now.ToString("MMddyyHHmmss");
                        objOrder.dealId = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
                        objOrder.Qty = 1;
                        objOrder.giftQty = 0;
                        objOrder.personalQty = 1;
                        objOrder.status = "Successful";
                        objOrder.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
                        objOrder.createdBy = Convert.ToInt64(ViewState["userId"]);
                        if(ddlSize.Items.Count > 0)
                        {
                            objOrder.size = ddlSize.SelectedValue.ToString();
                        }
                        else
                        {
                            objOrder.size = "";
                        }
                        objOrder.ccCreditUsed = 0;
                        objOrder.tastyCreditUsed = 0;
                        objOrder.comissionMoneyUsed = 0;
                        objOrder.totalAmt = 0;
                        objOrder.shippingAndTaxAmount = 0;
                        objOrder.ccInfoID = createdID;
                        objOrder.shippingInfoId = shippingInfoID;
                        objOrder.orderIPAddress = Request.UserHostAddress.Trim();
                        objOrder.resendOrders = true;
                        objOrder.isDeleted = false;
                        long intOrderNumnbr = objOrder.createNewDealOrder();

                        BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                        objDetail.dealOrderCode = objEnc.EncryptData("deatailOrder", txtVoucherNumber.Text.Trim());
                        objDetail.trackingNumber = txtTrackingNumber.Text.Trim();
                        objDetail.trackingUpdateDate = DateTime.Now;
                        objDetail.voucherSecurityCode = GenerateId().ToString().Substring(1, 3) + "-" + GenerateId().ToString().Substring(1, 3);
                        objDetail.dOrderID = intOrderNumnbr;
                        objDetail.isGift = false;
                        objDetail.isRedeemed = false;
                        objDetail.createDealOrderDetail();
                        resetFields();
                        lblMessage.Visible = true;
                        lblMessage.Text = "Record has been inserted successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Black;
                        imgGridMessage.ImageUrl = "images/checked.png";
                        imgGridMessage.Visible = true;
                        bindGrid();


                        //obj = new BLLResendOrders();
                        //obj.resendOrder_Address = txtAddress.Text.Trim();
                        //obj.resendOrder_CustomerName = txtCustomerName.Text.Trim();

                        //if (fuImage.HasFile)
                        //{
                        //    obj.resendOrder_Image = LogoUploadHere(fuImage);
                        //}
                        //obj.resendOrder_trackingNumber = txtTrackingNumber.Text.Trim();
                        //obj.resendOrder_Note = txtNote.Text.Trim();
                        //obj.resendOrder_Telephone = txtTelephone.Text.Trim();
                        //obj.resendOrder_VoucherNumber = txtVoucherNumber.Text.Trim();
                        //obj.dealId = Convert.ToInt64(Request.QueryString["did"].Trim());
                        //if (obj.createResendOrders() == -1)
                        //{
                        //    bindGrid();                               
                        //    lblMessage.Text = "Records has been saved successfully.";
                        //    lblMessage.Visible = true;
                        //    imgGridMessage.Visible = true;
                        //    imgGridMessage.ImageUrl = "images/Checked.png";
                        //    lblMessage.ForeColor = System.Drawing.Color.Black;
                        //    clearForm();
                        //}
                        //else
                        //{
                        //    bindGrid();                                
                        //    lblMessage.Text = "Record could not be saved successfully.";
                        //    lblMessage.Visible = true;
                        //    imgGridMessage.Visible = true;
                        //    imgGridMessage.ImageUrl = "images/error.png";
                        //    lblMessage.ForeColor = System.Drawing.Color.Red;
                        //}
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please enter correct customer email.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        imgGridMessage.ImageUrl = "images/error.png";
                        imgGridMessage.Visible = true;
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
        }
        else
        {
            lblMessage.Text = "fields are cannot be empty.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            imgGridMessage.ImageUrl = "images/error.png";
            imgGridMessage.Visible = true;
        }
    }

    private long GenerateId()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }


    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
