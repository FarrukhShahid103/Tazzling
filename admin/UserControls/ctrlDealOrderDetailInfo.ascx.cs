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

public partial class admin_UserControls_ctrlDealOrderDetailInfo : System.Web.UI.UserControl
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
                if (Request.QueryString["oid"] != null && Request.QueryString["oid"].Trim()!="")
                {
                    this.hfOrderId.Value = Request.QueryString["oid"].ToString().Trim();

                    GetAllBusinessInfoAndFillGrid();
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/admin/dealOrderInfo.aspx"), false);
                }
            }
            catch (Exception ex)
            {
                string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            }
        }
    }

    #region "Get and Set the Deal Order Info here"

    protected void GetAllBusinessInfoAndFillGrid()
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
                //Get the Deal Order Details By Order Id
                int iOrderId = int.Parse(this.hfOrderId.Value);
                objBLLDealOrderDetail.dOrderID = iOrderId;                
                dtUser = objBLLDealOrderDetail.getDealOrderDetailsByOrderID();

                dv = new DataView(dtUser);

                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
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
                this.ddlShowMe.Enabled = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();

                //btnSearch.Enabled = false;
                //this.ddlShowMe.Enabled = false;                
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
            int iOrderId = int.Parse(this.hfOrderId.Value);
            objBLLDealOrderDetail.dOrderID = iOrderId;
            ListItem objList = new ListItem("All", objBLLDealOrderDetail.getDealOrderDetailsByOrderID().Rows.Count.ToString());
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
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
        set
        {
            ViewState["sortDirection"] = value;
        }
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
                int iOrderId = int.Parse(this.hfOrderId.Value);

                objBLLDealOrderDetail.dOrderID = iOrderId;

                dtUser = objBLLDealOrderDetail.getDealOrderDetailsByOrderID();
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
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
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
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return "";
        }
        return "";
    }

    protected string getDealCode(object objCode)
    {
        if (objCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return "# " + objEnc.DecryptData("deatailOrder", objCode.ToString());
        }
        return "";
    }

    protected void pageGrid_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" || e.CommandArgument.ToString().ToLower().Trim() == "prev")
        {
            return;
        }
        bool result = false;
        try
        {
            objBLLDealOrderDetail.detailID = Convert.ToInt64(e.CommandArgument);
            if (((LinkButton)e.CommandSource).Text.Trim() == "Display for Customer")
            {
                objBLLDealOrderDetail.displayIt = true;               
            }
            else
            {
                objBLLDealOrderDetail.displayIt = false;               
            }
            result = objBLLDealOrderDetail.changeOrderDetailDisplayStatus();
            if (result)
            {

                GetAllBusinessInfoAndFillGrid();
                lblMessage.Text = "Status has been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/Images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;               
            }
            else
            {
                GetAllBusinessInfoAndFillGrid();               
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

    }

    //protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    int idetailOrderID = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);

    //    objBLLDealOrderDetail.detailID = idetailOrderID;
        

    //    objBLLDealOrderDetail.updateDealOrderDetailsByDetailID();

    //    lblMessage.Text = "Deal info has been redeemed successfully.";
        
    //    lblMessage.Visible = true;

    //    imgGridMessage.Visible = true;

    //    imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
        
    //    lblMessage.ForeColor = System.Drawing.Color.Black;

    //    this.GetAllBusinessInfoAndFillGrid();
    //}

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            SearchhDealInfoByDifferentParams();
        }
        catch (Exception ex)
        { }
    }

    private void SearchhDealInfoByDifferentParams()
    {
        string strQuery = "";

        try
        {
            strQuery = "SELECT ";
            strQuery += "deals.[title] as 'DealName'";
            strQuery += ",deals.[images]as 'DealImage'";
            strQuery += ",[dealOrderDetail].[detailID]";
            strQuery += ",[dealOrderDetail].[dOrderID]";
            strQuery += ",[dealOrderDetail].[receiverEmail]";
            strQuery += ",[dealOrderDetail].[isRedeemed]";
            strQuery += ",[dealOrderDetail].[redeemedDate]";
            strQuery += ",[dealOrderDetail].[dealOrderCode]";
            strQuery += ",[dealOrderDetail].[isGift]";
            strQuery += ",[dealOrderDetail].[markUsed] ";
            strQuery += ",psgTranNo ";
            strQuery += ",[userinfo].username ";
            strQuery += " FROM ";
            strQuery += " dealOrderDetail";
            strQuery += " inner join dealOrders on [dealOrders].[dOrderID] = [dealOrderDetail].[dOrderID]";
            strQuery += " inner join deals on deals.dealId = dealOrders.dealId";
            strQuery += " inner join userinfo on userinfo.userId = dealOrders.userId";
            strQuery += " where ";
            strQuery += " [dealOrderDetail].[dOrderID] = " + this.hfOrderId.Value.ToString();

            //Get the Deal
            if (this.ddlShowMe.SelectedValue == "available")
            {
                strQuery += " and isRedeemed = 0";
            }
            else if (this.ddlShowMe.SelectedValue == "used")
            {
                strQuery += " and isRedeemed = 1";
            }
            else if (this.ddlShowMe.SelectedValue == "all")
            {
                strQuery += "";
            }

            this.pageGrid.PageIndex = 0;

            //ViewState["Query"] = strQuery;

            DataTable dtDeals = Misc.search(strQuery);

            if ((dtDeals != null) &&
                (dtDeals.Columns.Count > 0) &&
                (dtDeals.Rows.Count > 0))
            {
                this.pageGrid.DataSource = dtDeals;
                this.pageGrid.DataBind();
            }
            else
            {
                this.pageGrid.PageIndex = 0;

                this.pageGrid.DataSource = null;
                this.pageGrid.DataBind();

                ViewState["Query"] = null;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}