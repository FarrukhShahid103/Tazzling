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

public partial class ResendOrdersManagement : System.Web.UI.Page
{
    #region Global Variables
    BLLResendOrders obj = new BLLResendOrders();
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
            if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "")
            {
                bindGrid();
            }
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
                dtCities = obj.getResendOrdersByDealId();
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
        ListItem objList = new ListItem("All", obj.getResendOrdersByDealId().Rows.Count.ToString());
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

    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            obj.resendOrderID = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value.ToString());
            if (obj.deleteResendOrders() == -1)
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
            dtCities = obj.getResendOrdersByDealId();
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
            strQuery += "[resendOrderID]";
            strQuery += ",[resendOrder_CustomerName]";
            strQuery += ",[resendOrder_VoucherNumber]";
            strQuery += ",[resendOrder_Telephone]";
            strQuery += ",[resendOrder_Address]";
            strQuery += ",[resendOrder_Note]";
            strQuery += ",[resendOrder_Image]";
            strQuery += ",[dealId]";
            strQuery += ",[resendOrder_trackingNumber]";
            strQuery += "FROM resendOrders Where dealId=" + Request.QueryString["did"].Trim();

            if (txtSearchCustomerName.Text.Trim() != "")
            {
                strQuery += " and resendOrder_CustomerName LIKE '%" + txtSearchCustomerName.Text.ToString().Trim().Replace("'", "''") + "%'";
            }

            strQuery += "order by resendOrderID desc";
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
            obj = new BLLResendOrders();
            int idetailOrderID = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);
            TextBox txtTrackNumber = (TextBox)pageGrid.Rows[e.NewEditIndex].FindControl("txtTrackNumber");
            obj.resendOrderID = idetailOrderID;
            obj.resendOrder_trackingNumber = txtTrackNumber.Text.Trim();
            obj.updateResendOrdersTrackingNumber();
            //this.GetAllBusinessInfoAndFillGrid();
            lblMessage.Text = "Track number has been saved successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        { }
    }

    #region Button Update Click Event
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            obj = new BLLResendOrders();
            obj.resendOrder_Address = txtAddress.Text.Trim();
            obj.resendOrder_CustomerName = txtCustomerName.Text.Trim();
            try
            {
                if (fuImage.HasFile)
                {
                    if (this.imglogo.Src.ToString().Length > 2)
                    {
                        string strImgName = "";

                        strImgName = this.imglogo.Src.ToString().Substring(this.imglogo.Src.ToString().LastIndexOf("/") + 1, (this.imglogo.Src.ToString().Length - (this.imglogo.Src.ToString().LastIndexOf("/") + 1)));

                        string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\resendOrder\\" + strImgName;

                        this.imglogo.Src = "";

                        if (File.Exists(path))
                        {
                            try
                            {
                                //Delete the File
                                File.Delete(path);
                            }
                            catch (Exception ex) { }
                        }
                    }
                    //upload the Image here
                    obj.resendOrder_Image = LogoUploadHere(fuImage);
                }
                else
                {
                    obj.resendOrder_Image = this.imglogo.Src.ToString().Substring(this.imglogo.Src.ToString().LastIndexOf("/") + 1, (this.imglogo.Src.ToString().Length - (this.imglogo.Src.ToString().LastIndexOf("/") + 1)));
                }
            }
            catch (Exception ex)
            { }
            obj.resendOrder_trackingNumber = txtTrackingNumber.Text.Trim();
            obj.resendOrder_Note = txtNote.Text.Trim();
            obj.resendOrder_Telephone = txtTelephone.Text.Trim();
            obj.resendOrder_VoucherNumber = txtVoucherNumber.Text.Trim();
            obj.resendOrderID = Convert.ToInt64(hfresendOrderID.Value);

            if (obj.updateResendOrders() == -1)
            {
                lblMessage.Text = "Record has been updated successfully.";
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                clearForm();
            }
            else
            {
                lblMessage.Text = "Record could not be updated successfully.";
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            DataTable dtCity = null;
            obj.resendOrderID = Convert.ToInt32(pageGrid.SelectedDataKey.Value);
            dtCity = obj.getResendOrdersById();
            hfresendOrderID.Value = pageGrid.SelectedDataKey.Value.ToString();
            txtAddress.Text = dtCity.Rows[0]["resendOrder_Address"].ToString();
            txtCustomerName.Text = dtCity.Rows[0]["resendOrder_CustomerName"].ToString();
            txtNote.Text = dtCity.Rows[0]["resendOrder_Note"].ToString();
            txtTelephone.Text = dtCity.Rows[0]["resendOrder_Telephone"].ToString();
            txtVoucherNumber.Text = dtCity.Rows[0]["resendOrder_VoucherNumber"].ToString();
            txtTrackingNumber.Text = dtCity.Rows[0]["resendOrder_trackingNumber"].ToString();
            imglogo.Src = "../Images/resendOrder/" + dtCity.Rows[0]["resendOrder_Image"].ToString();
            imglogo.Visible = true;
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\resendOrder\\" + dtCity.Rows[0]["resendOrder_Image"].ToString();           
            GetAndSetPostsByDealId(Convert.ToInt64(hfresendOrderID.Value));
            SetFeilds(0);

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

    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAddress.Text = "";
            txtCustomerName.Text = "";
            txtNote.Text = "";
            txtTelephone.Text = "";
            txtVoucherNumber.Text = "";
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            SetFeilds(0);
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
                    obj.resendOrderID = Convert.ToInt32(lblID.Text);
                    result = obj.deleteResendOrders();
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
        imglogo.Src = "";
        imglogo.Visible = false;
        btnSave.Visible = true;       
        btnUpdate.Visible = false;
        SetFeilds(0);
        this.rptrDiscussion.DataSource = null;
        this.rptrDiscussion.DataBind();
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            obj = new BLLResendOrders();
            obj.resendOrder_Address = txtAddress.Text.Trim();
            obj.resendOrder_CustomerName = txtCustomerName.Text.Trim();

            if (fuImage.HasFile)
            {
                obj.resendOrder_Image = LogoUploadHere(fuImage);
            }
            obj.resendOrder_trackingNumber = txtTrackingNumber.Text.Trim();
            obj.resendOrder_Note = txtNote.Text.Trim();
            obj.resendOrder_Telephone = txtTelephone.Text.Trim();
            obj.resendOrder_VoucherNumber = txtVoucherNumber.Text.Trim();
            obj.dealId = Convert.ToInt64(Request.QueryString["did"].Trim());
            if (obj.createResendOrders() == -1)
            {
                bindGrid();
                lblMessage.Text = "Records has been saved successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                clearForm();
            }
            else
            {
                bindGrid();
                lblMessage.Text = "Record could not be saved successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
            if (hfresendOrderID.Value.Trim() != "0")
            {
                DataTable dtUser = (DataTable)Session["user"];

                BLLResendOrderComments obj = new BLLResendOrderComments();
                obj.comment = txtComment.Text.Trim().Replace("\n", "<br>");
                obj.resendOrderID = Convert.ToInt64(hfresendOrderID.Value.Trim());
                obj.commentby = Convert.ToInt64(dtUser.Rows[0]["userID"].ToString());
                obj.createResendOrderComments();
                SetFeilds(0);
                //Get All the Posts here By Deal Id
                GetAndSetPostsByDealId(Convert.ToInt64(hfresendOrderID.Value.Trim()));
                lblCommentMessage.Visible = true;
                lblCommentMessage.Text = "Comment save successfully.";
                txtComment.Text = "";
            }
            else
            {
                lblCommentMessage.Text = "Please add order first.";
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

    private void GetAndSetPostsByDealId(long iResendOrderID)
    {
        try
        {

            BLLResendOrderComments objBLLResendOrderComments = new BLLResendOrderComments();

            objBLLResendOrderComments.resendOrderID = iResendOrderID;

            DataTable dtPosts = objBLLResendOrderComments.getResendOrderCommentsByResendOrderId();

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
