using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Contract_createContract : System.Web.UI.Page
{
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;
    public string strImageName = "";
    public string strIDs = "";
    public int start = 2;
    public string HideBit = "";
    public string AHideBit = "";
    public string aliHideBit = "";
    public string restaurantId = "";
    public string contractid = "";
    BLLContractDtail objcontract = new BLLContractDtail();
    BLLRestaurant objRes = new BLLRestaurant();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["resId"] != null && Request.QueryString["resId"] != "")
            {
                DataTable dtUsercheck = (DataTable)Session["user"];
                if (dtUsercheck != null && dtUsercheck.Rows.Count > 0)
                {

                    if (dtUsercheck.Rows[0]["userTypeID"].ToString() == "4")
                    {

                        DataTable dtTemp = Misc.search("select userinfo.userid from userinfo inner join restaurant on (restaurant.userid = userInfo.userid) where userinfo.userid=" + dtUsercheck.Rows[0]["userid"].ToString().Trim() + " and restaurant.restaurantId=" + Request.QueryString["resId"].ToString().Trim());
                        if (dtTemp != null && dtTemp.Rows.Count == 0)
                        {
                            Response.Redirect(ResolveUrl("~/contract/restaurantManagement.aspx"));
                        }
                    }


                    restaurantId = Request.QueryString["resId"].ToString().Trim();
                    FillgridWithCreateContract(0);
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/contract/youadminlogin.aspx"));
 
                }
            }
        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["resId"] != null && Request.QueryString["resId"] != "")
        { 
            DataTable dtUsercheck = (DataTable)Session["user"];
            



            BLLContractDtail objContract = new BLLContractDtail();
            objContract.restaurantId = Convert.ToInt16(Request.QueryString["resId"].ToString().Trim());
           
            if (dtUsercheck != null)
            {
                objContract.userID = Convert.ToInt16(dtUsercheck.Rows[0]["userId"].ToString().Trim());
            }
           

            objContract.weight = txtWeight.Text.ToString().Trim();
            objContract.width = txtWidth.Text.ToString().Trim();
            objContract.haight = txtHeight.Text.ToString().Trim();
            objContract.price = txtPrice.Text.ToString().Trim();
            objContract.length = txtLength.Text.ToString().Trim();
            objContract.itemName = txtItem.Text.ToString().Trim();
          

            if (fpBusinessImg.HasFile)
            {
                //upload the Image here
                strImageName = ImageUploadHere(fpBusinessImg);
            }
            objContract.image = strImageName;

           

            int Result = objContract.CreateContractDetail();
            if (Result != 0)
            {

                lblAddressError.Text = "Contract has been added successfully.";
                lblAddressError.Visible = true;
                ImgAddError.Visible = true;
                ImgAddError.ImageUrl = "images/Checked.png"; lblAddressError.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblAddressError.Text = "Error. Contract Not Added.";
                lblAddressError.Visible = true;
                ImgAddError.Visible = true;
                ImgAddError.ImageUrl = "images/error.png"; lblAddressError.ForeColor = System.Drawing.Color.Black;
            }

            FillgridWithCreateContract(0);
            clearForm();


        }
    
    }
  
    private string ImageUploadHere(FileUpload fileUploadDealImg)
    {
        string strUniqueID = "";

        try
        {

            if (fileUploadDealImg.HasFile)
            {

                string[] strExtension = fileUploadDealImg.FileName.Split('.');

                strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];

                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\createContract\\" + fileUploadDealImg.FileName;

                fileUploadDealImg.SaveAs(strSrcPath);

                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\createContract\\";

                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }

                string SrcFileName = fileUploadDealImg.FileName;

                Misc.CreateThumbnailForBusinessOwner(strSrcPath, strthumbSave, SrcFileName, strUniqueID);

                File.Delete(strSrcPath);
            }
        }
        catch (Exception ex)
        {

        }

        return strUniqueID;
    }
    protected void FillgridWithCreateContract(int intPageNumber)
    {
        DataTable dtUsercheck = (DataTable)Session["user"];
        if (dtUsercheck != null && dtUsercheck.Rows.Count > 0)
        { 
            DataTable dtUser;
            DataSet dst = null;
            DataView dv;
            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(Session["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                Session["ddlPage"] = Misc.pageSize;
            }


            if (ViewState["Query"] == null)
            {
                objcontract.restaurantId = Convert.ToInt16(Request.QueryString["resId"].ToString().Trim());
                dst = objcontract.GetContractDetail(intPageNumber, pageGrid.PageSize);
                dtUser = dst.Tables[0];
                dv = new DataView(dtUser);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }

            }
            else
            {
                return;
            }

           // DataTable dtUser = (DataTable)Session["user"];
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
              //  btnSearch.Enabled = true;
               // this.txtSearchBusinessName.Enabled = true;
               // this.txtSearchCity.Enabled = true;
               // this.txtSearchZipCode.Enabled = true;
            }

















          
            //BLLContractDtail objRes = new BLLContractDtail();
            //objRes.userID = Convert.ToInt16(dtUsercheck.Rows[0]["userId"].ToString().Trim());
            //objcontract.restaurantId = Convert.ToInt16(Request.QueryString["resId"].ToString().Trim());


            //dtUser = objcontract.GetContractDetail(intPageNumber, pageGrid.PageSize);
            //    dv = new DataView(dtUser);
            //    pageGrid.DataSource = dv;

            //    pageGrid.DataBind();
           
        }
    }

 
    protected void pageGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HP = ((HyperLink)e.Row.FindControl("HPImage"));

                Literal LT = ((Literal)e.Row.FindControl("LTRLScript"));
                if (HP != null)
                {
                    LT.Text = " <script type='text/javascript'>";
                    LT.Text += "$(document).ready(function() {";
                    LT.Text += "var HerfID ='" + HP.ClientID + "';";
                    LT.Text += "$('a#' + HerfID).fancybox({";
                    LT.Text += "'overlayShow'	: false,";
                    LT.Text += "'transitionIn'	: 'elastic',";
                    LT.Text += "'transitionOut'	: 'elastic'";
                    LT.Text += " });";
                    LT.Text += " });";
                    LT.Text += "</script>";
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

            
        }
    }


    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

        contractid = Convert.ToString(pageGrid.SelectedDataKey.Value);
        hfcontractID.Value = Convert.ToString(pageGrid.SelectedDataKey.Value);
        DataTable dtcontractDetail = null;
        objcontract.contractid = Convert.ToInt16(hfcontractID.Value);
        dtcontractDetail = objcontract.GetCintractDetailByResId();

        if (dtcontractDetail != null && dtcontractDetail.Rows.Count > 0)
        {

            txtHeight.Text = dtcontractDetail.Rows[0]["height"].ToString().Trim();
            txtItem.Text = dtcontractDetail.Rows[0]["itemName"].ToString().Trim();
            txtLength.Text = dtcontractDetail.Rows[0]["length"].ToString().Trim();
            txtPrice.Text = dtcontractDetail.Rows[0]["Price"].ToString().Trim();

            txtWeight.Text = dtcontractDetail.Rows[0]["weight"].ToString().Trim();
            txtWidth.Text = dtcontractDetail.Rows[0]["width"].ToString().Trim();
            this.imgUpload1.Src = "../Images/createContract/" + dtcontractDetail.Rows[0]["image"].ToString().Trim();
            this.imgUpload1.Visible = true;
            rfvDealImage1.ValidationGroup = "";
            btnUpdate.Visible = true;
            btncanclupdate.Visible = true;
            btnSave.Visible = false;
            CancelButton.Visible = false;
            lblpopHead.Visible = false;
            lbleditContract.Visible = true;


        }

    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objcontract.length = txtLength.Text.ToString().Trim();
            objcontract.itemName = txtItem.Text.ToString().Trim();
            objcontract.weight = txtWeight.Text.ToString().Trim();
            objcontract.width = txtWidth.Text.ToString().Trim();
            objcontract.price = txtPrice.Text.ToString().Trim();
            objcontract.haight = txtHeight.Text.ToString().Trim();
            DataTable dtUsercheck = (DataTable)Session["user"];
            objcontract.userID = Convert.ToInt16(dtUsercheck.Rows[0]["userID"].ToString().Trim());
            objcontract.contractid = Convert.ToInt16(hfcontractID.Value);
            if (fpBusinessImg.HasFile)
            {
                //upload the Image here
                strImageName = ImageUploadHere(fpBusinessImg);
                objcontract.image = strImageName;
            }
            else
            {
                
                DataTable dtcontractDetail = null;
                dtcontractDetail = objcontract.GetCintractDetailByResId();
                if (dtcontractDetail != null && dtcontractDetail.Rows.Count > 0)
                {
                    strImageName = dtcontractDetail.Rows[0]["image"].ToString().Trim();
                    objcontract.image = strImageName;
                }
            }

            rfvDealImage1.ValidationGroup = "user";

            int updateContract = objcontract.UpdateContractDetail();

            if (updateContract == -1)
            {
                lblAddressError.Text = "Contract has been updated successfully.";
                lblAddressError.Visible = true;
                ImgAddError.Visible = true;
                ImgAddError.ImageUrl = "images/Checked.png"; lblAddressError.ForeColor = System.Drawing.Color.Black;
                btncanclupdate.Visible = false;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                CancelButton.Visible = true;
                lblpopHead.Visible = true;
                lbleditContract.Visible = false;
            }
            else
            {
                lblAddressError.Text = "Contract could not be updated successfully.";
                lblAddressError.Visible = true;
                ImgAddError.Visible = true;
                ImgAddError.ImageUrl = "images/error.png";
                lblAddressError.ForeColor = System.Drawing.Color.Red;
            }

        }
        catch (Exception ex)
        {

            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
        FillgridWithCreateContract(0);
        clearForm();


    }
    protected void clearForm()
    {
        hfcontractID.Value = "";
        txtHeight.Text = "";
        txtItem.Text = "";
        txtLength.Text = "";
        txtPrice.Text = "";
        txtWeight.Text = "";
        txtWidth.Text = "";

        imgUpload1.Src = "";
        imgUpload1.Visible = false;
       
       
       
    }

    #region Event to take to required page
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblAddressError.Visible = false;
            ImgAddError.Visible = false;

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
                this.FillgridWithCreateContract(intCurrentPage - 1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = intCurrentPage.ToString();
            }
        }
        catch (Exception ex)
        {

            //pnlGrid.Visible = true;
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
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

           // pnlGrid.Visible = true;
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
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

           // pnlGrid.Visible = true;
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
            return null;
        }
    }

    protected void pageGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblAddressError.Visible = false;
            ImgAddError.Visible = false;
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
            this.FillgridWithCreateContract(intCurrentPage - 1);
            txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (intCurrentPage).ToString();
        }
        catch (Exception ex)
        {

            //pnlGrid.Visible = true;
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblAddressError.Visible = false;
            ImgAddError.Visible = false;
            pageGrid.PageIndex = 0;
            DropDownList ddlPage = (DropDownList)pageGrid.BottomPagerRow.Cells[0].FindControl("ddlPage");
            Session["ddlPage"] = ddlPage.SelectedValue.ToString();
            setPageValueInCookie(ddlPage);
            this.FillgridWithCreateContract(0);
        }
        catch (Exception ex)
        {

           // pnlGrid.Visible = true;
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
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
}