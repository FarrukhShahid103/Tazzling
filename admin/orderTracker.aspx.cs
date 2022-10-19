using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GecLibrary;
using System.Web.UI.HtmlControls;

using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Reflection;

public partial class orderTracker : System.Web.UI.Page
{
    BLLDealOrderDetail objBLLDealOrderDetail = new BLLDealOrderDetail();

    public bool displayPrevious = false;
    public bool displayNext = true;

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["did"] == null)
                {
                    Response.Redirect(ResolveUrl("~/admin/onlinedealManagement.aspx"));
                }              
                GetAllBusinessInfoAndFillGrid();
                //SetSubDealsInfo();

            }
            catch (Exception ex)
            {   
          
            }
        }
    }

        
    #region "Get and Set the Deal Order Info here"

    protected void GetAllBusinessInfoAndFillGrid()
    {
        try
        {
            DataTable dtUser;
            if (ViewState["Query"] == null)
            {
                dtUser = SearchhDealInfoByDifferentParams();
            }
            else
            {
                dtUser = Misc.search(ViewState["Query"].ToString());
            }
            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(ViewState["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = 10;
                ViewState["ddlPage"] = 10;
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {

                lblDealTitle.Text = "<b>Deal Title: </b>"+ dtUser.Rows[0]["title"].ToString().Trim();                
                pageGrid.DataSource = dtUser.DefaultView;
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

                TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
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

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

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
            ListItem objList = new ListItem("All", SearchhDealInfoByDifferentParams().Rows.Count.ToString());
            ddlPage.Items.Insert(5, objList);
            return ddlPage;
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
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
            this.GetAllBusinessInfoAndFillGrid();
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
    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
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
                dtUser = SearchhDealInfoByDifferentParams();
            }

            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(ViewState["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                ViewState["ddlPage"] = Misc.pageSize;
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
            ViewState["ddlPage"] = ddlPage.SelectedValue.ToString();
            setPageValueInCookie(ddlPage);
            this.GetAllBusinessInfoAndFillGrid();
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

    public string getImagePath(object resID, object imgName)
    {
        try
        {
            ArrayList arrImage = new ArrayList();
            arrImage.AddRange(imgName.ToString().Split(','));

            if (arrImage.Count > 0)
            {
                string strImageName = arrImage[0].ToString();

                string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + resID.ToString() + "\\" + strImageName;
                if (File.Exists(path))
                {
                    return "../Images/dealFood/" + resID.ToString() + "/" + strImageName;
                }
                else
                {
                    return "../Images/dealFood/noMenuImage.gif";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return "";
        }
        return "";
    }
   

    protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int idetailOrderID = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);
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
        }
        catch (Exception ex)
        { }
    }



    protected string getDealCode(object objCode)
    {
        if (objCode.ToString().Contains("# "))
        {
            return objCode.ToString();
        }
        else if (objCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return "# " + objEnc.DecryptData("deatailOrder", objCode.ToString());
        }
        return "";
    }
       
    protected string getDealCodeSrch(string strDealCode)
    {
        if (strDealCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.EncryptData("deatailOrder", strDealCode);
        }
        return "";
    }

    private DataTable SearchhDealInfoByDifferentParams()
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";            
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";
            strQuery += " ,products.title";            
            strQuery += " ,userInfo.userName";
            strQuery += " ,orderNo";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + rtrim(userInfo.lastName) as 'Name'";
            strQuery += " ,[dealOrders].[status]";            
            strQuery += ",userShippingInfo.Name as 'shippingName'";
            strQuery += ",dealOrders.shippingInfoId";
            strQuery += ",userShippingInfo.Address+', '+userShippingInfo.City+', '+userShippingInfo.State+', '+userShippingInfo.ZipCode +', '+userShippingInfo.shippingCountry as 'ShippingAddress'";
            strQuery += ",userShippingInfo.Telephone";
            strQuery += ",userShippingInfo.shippingNote";
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " ,[dealOrderDetail].[detailID]";
            strQuery += " ,[dealOrderDetail].[isRedeemed]";
            strQuery += " ,[dealOrderDetail].[trackingNumber]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";
            strQuery += " ,[dealOrderDetail].[markUsed]";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join products on (products.productID = dealOrders.dealId)";
            strQuery += " left outer join userShippingInfo on (userShippingInfo.shippingInfoId = dealOrders.shippingInfoId)";
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            if (Request.QueryString["order"] != null && Request.QueryString["order"].ToString().Trim() != ""
                && Request.QueryString["order"].Trim().ToLower() == "cancel")
            {
                strQuery += " where (dealOrders.status='Cancelled' OR  dealOrders.status='Refunded') and ";
            }
            else
            {
                strQuery += " where dealOrders.status='Successful' and ";
            }
            strQuery += " products.productID =" + Request.QueryString["did"].ToString() + " and dealOrders.isDeleted=0 and dealOrders.resendOrders=0";

            if (this.txtSrchDealCode.Text.Trim().Length > 0)
            {
                strQuery += " and [dealOrderDetail].[dealOrderCode] = '" + getDealCodeSrch(this.txtSrchDealCode.Text.Trim().ToUpper().Replace("#", "")).Trim() + "'";
            }
            if (this.txtUserEmail.Text.Trim().Length > 0)
            {
                strQuery += " and [userInfo].[userName] like '%" + txtUserEmail.Text.Trim() + "%'";
            }
            if (this.txtUserName.Text.Trim().Length > 0)
            {
                strQuery += " and (userInfo.firstname like '%" + txtUserName.Text.Trim() + "%' OR userInfo.lastname like '%" + txtUserName.Text.Trim() + "%') ";
            }
           
            //Get the Deal
            if (this.ddlShowMe.SelectedIndex == 0)
            {
                strQuery += " and ([dealOrderDetail].trackingNumber = '' or [dealOrderDetail].trackingNumber is null) ";
            }
            else if (this.ddlShowMe.SelectedIndex == 2)
            {
                strQuery += " and [dealOrderDetail].trackingNumber <> ''";
            }
            else if (this.ddlShowMe.SelectedIndex == 1)
            {
                strQuery += "";
            }
            strQuery += " Order by dealOrders.dOrderID desc";
          
            //Get & Set the DataTable here
            dtOrderDetailInfo = Misc.search(strQuery);            
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        return dtOrderDetailInfo;
    }
    
    private DataTable SortDataTable(DataTable dt)
    {
        try
        {
            GECEncryption objEnc = new GECEncryption();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["dealOrderCode"] = objEnc.EncryptData("deatailOrder", dt.Rows[i]["dealOrderCode"].ToString());
            }
            return dt;

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        return dt;
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            GetAllBusinessInfoAndFillGrid();

            //if (txtSrchDealCode.Text.Trim() != "")
            //{
            //    lblMessage.Visible = false;
            //    imgGridMessage.Visible = false;
            //    if (ViewState["dtUser"] != null)
            //    {
            //        DataTable dtUser = (DataTable)ViewState["dtUser"];
            //        DataView dv;
            //        if (dtUser != null && dtUser.Rows.Count > 0)
            //        {
            //            DataRow[] dtRow = dtUser.Select("dealOrderCode like '%" + txtSrchDealCode.Text.Trim() + "%'");
            //            DataTable dtSortTable = dtUser.Clone();
            //            foreach (DataRow dr in dtRow)
            //                dtSortTable.ImportRow(dr);

            //            pageGrid.DataSource = dtSortTable.DefaultView;
            //            pageGrid.DataBind();
            //            //GridView1.DataSource = dtSortTable.DefaultView;
            //            //GridView1.DataBind();

            //            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            //            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

            //            lblTotalRecords.Text = dtUser.Rows.Count.ToString();
            //            lblPageCount.Text = pageGrid.PageCount.ToString();

            //            DropDownList ddlPage = bindPageDropDown();
            //            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            //            {
            //                ddlPage.SelectedValue = ViewState["ddlPage"].ToString();
            //            }
            //            pageGrid.BottomPagerRow.Visible = true;

            //            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            //            if (pageGrid.PageIndex == 0)
            //            {
            //                displayPrevious = false;
            //            }
            //            else
            //            {
            //                displayPrevious = true;
            //                txtPage.Text = (pageGrid.PageIndex + 1).ToString();
            //            }
            //            if (pageGrid.PageIndex == pageGrid.PageCount - 1)
            //            {
            //                displayNext = false;
            //            }
            //            else
            //            {
            //                displayNext = true;
            //                txtPage.Text = (pageGrid.PageIndex + 1).ToString();
            //            }

            //            if (pageGrid.PageCount == 1)
            //            {
            //                ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
            //                ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

            //                imgNext.Enabled = false;
            //                imgPrev.Enabled = false;
            //            }
            //        }
            //        else
            //        {
            //            GetAllBusinessInfoAndFillGrid();
            //        }

            //    }
            //    else
            //    {
            //        GetAllBusinessInfoAndFillGrid();
            //    }
            //}
            //else
            //{
            //    GetAllBusinessInfoAndFillGrid();
            //}
        }
        catch (Exception ex)
        { }
    }
}
