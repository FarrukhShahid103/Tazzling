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
using GecLibrary;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;

public partial class admin_dealOrdersDetailsByUsers : System.Web.UI.Page
{
    BLLDealOrders objBLLDealOrders = new BLLDealOrders();

    public bool displayPrevious = false;
    public bool displayNext = true;

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllBusinessInfoAndFillGrid();
        }

        if (Request.QueryString["did"] != null)
        {
            try
            {
                if (int.Parse(Request.QueryString["did"].ToString().Trim()) == 0)
                    Response.Redirect("dealOrdersMgmtByUsers.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Redirect("dealOrdersMgmtByUsers.aspx", false);
            }
        }
        else
            Response.Redirect("dealOrdersMgmtByUsers.aspx", false);
    }

    protected bool getDetailStatus(object status)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "successful" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "refunded")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }

    protected bool getPayBackStatus(object status, object psgTranNo)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "successful" && psgTranNo.ToString().Trim() != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
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
                dtUser = dtSearchhDealInfoByDifferentParams();
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
                this.txtUserAcc.Enabled = true;
                this.ddlPayment.Enabled = true;
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
            ListItem objList = new ListItem("All", dtSearchhDealInfoByDifferentParams().Rows.Count.ToString());
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
                dtUser = dtSearchhDealInfoByDifferentParams();
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

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        lblMessage.Visible = false;
        imgGridMessage.Visible = false;

        ViewState["Query"] = null;

        GetAllBusinessInfoAndFillGrid();

    }

    private bool ChangeDealOrderStatus(long dOrderId, string strStatus)
    {
        bool bStatus = false;
        try
        {
            BLLDealOrders objBLLDealOrders = new BLLDealOrders();

            objBLLDealOrders.dOrderID = dOrderId;

            objBLLDealOrders.status = strStatus;

            bStatus = objBLLDealOrders.changeDealOrderStatus();
        }
        catch (Exception ex)
        { }
        return bStatus;
    }

    protected void pageGrid_Login(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtUser = null;
        try
        {
            if (e.CommandName == "Login")
            {
                BLLUser obj = new BLLUser();
                obj.userId = Convert.ToInt32(e.CommandArgument);
                dtUser = obj.getUserByID();
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
                    lblMessage.Text = "User could not login as user is deactive.";
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

    private DataTable dtSearchhDealInfoByDifferentParams()
    {
        DataTable dtDeals = null;
        string strQuery = "";
        try
        {
            strQuery = "SELECT ";
            strQuery += " tblDeals.[title] as 'DealName'";
            strQuery += " ,[tblDealOrderDetail].[detailID]";
            strQuery += " ,[tblDealOrderDetail].[dOrderID]";
            strQuery += " ,[tblDealOrderDetail].[isRedeemed]";
            strQuery += " ,[tblDealOrderDetail].[redeemedDate]";
            strQuery += " ,[tblDealOrderDetail].[dealOrderCode]";
            strQuery += " ,[tblDealOrderDetail].[isGift]";
            strQuery += " ,[tblDealOrderDetail].[markUsed]";
            strQuery += " ,tblDealOrders.dealId";
            strQuery += " ,tblDealOrders.psgTranNo";            
            strQuery += " ,(select count(*) from tblDealOrderDetail where isRedeemed = 1 and tblDealOrderDetail.dOrderID = [tblDealOrders].dOrderID) as 'RedeemCount' ";
            strQuery += " ,tblDealOrders.createdDate";
            strQuery += " ,[tblDealOrders].[status]";
            strQuery += " ,case when tblUserType.UserType = 'retaurant' then 'Business' else 'Member' end as 'UserType'";
            strQuery += " ,tblUserInfo.[userName]";
            strQuery += " ,rtrim(tblUserInfo.firstName) + ' ' + rtrim(tblUserInfo.lastName) as 'Name'";
            strQuery += " ,isnull(tblUserInfo.phoneNo,'') as 'phoneNo'";
            strQuery += " ,tblUserInfo.[userId]";
            strQuery += " ,tblUserInfo.[isActive]";
            strQuery += " FROM ";
            strQuery += " tblDealOrderDetail";
            strQuery += " inner join tblDealOrders on [tblDealOrders].[dOrderID] = [tblDealOrderDetail].[dOrderID]";
            strQuery += " inner join tblDeals on tblDeals.dealId = tblDealOrders.dealId";
            strQuery += " inner join tblUserInfo on tblUserInfo.userId = tblDealOrders.userId";
            strQuery += " inner join tblUserType on tblUserType.userTypeID = tblUserInfo.userTypeID";
            strQuery += " where tblDealOrderDetail.[dOrderID]= tblDealOrders.[dOrderID]";

            if (Request.QueryString["did"] != null)
            {
                try
                {
                    if (int.Parse(Request.QueryString["did"].ToString().Trim()) > 0)
                    {
                        strQuery += " and tblDealOrders.dealId = " + int.Parse(Request.QueryString["did"].ToString().Trim());
                    }
                }
                catch (Exception ex)
                { }
            }

            if (this.txtVoucher.Text.Trim().Length > 0)
            {
                strQuery += " and [tblDealOrderDetail].[dealOrderCode] = '" + getDealCodeSrch(this.txtVoucher.Text.Trim().Replace("#", "")) + "'";
            }

            if (txtUserAcc.Text.Trim() != "")
            {
                strQuery += " and tblUserInfo.[userName] like '%" + txtUserAcc.Text.Trim() + "%' ";
            }

            if (ddlPayment.SelectedIndex > 0)
            {
                strQuery += " and [tblDealOrders].[status] = '" + ddlPayment.SelectedValue.ToString() + "'";
            }     


            strQuery += " order by tblDealOrders.createdDate desc";

            ViewState["Query"] = strQuery;

            dtDeals = Misc.search(strQuery);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        return dtDeals;
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

    protected string getDealCode(object objCode, object status)
    {
        if (objCode.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending")
            {
                return "# *******";
            }
            else
            {
                GECEncryption objEnc = new GECEncryption();
                return "# " + objEnc.DecryptData("deatailOrder", objCode.ToString());
            }
        }
        return "";
    }
}