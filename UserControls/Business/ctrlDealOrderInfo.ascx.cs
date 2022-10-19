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
using System.Diagnostics;
using System.Configuration;
public partial class UserControls_Business_ctrlDealOrderInfo : System.Web.UI.UserControl
{
    BLLDealOrders objBLLDealOrders = new BLLDealOrders();

    public bool displayPrevious = false;
    public bool displayNext = true;

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["restaurant"] == null)
        {
            Response.Redirect("Default.aspx", false);
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["rid"] != null && Request.QueryString["rid"].Trim()!="")
            {
                DataTable dtuserinfo = (DataTable)Session["restaurant"];
                if (dtuserinfo != null && dtuserinfo.Rows.Count > 0 && dtuserinfo.Rows[0]["userid"] != null && dtuserinfo.Rows[0]["userid"].ToString().Trim() != "")
                {
                    DataTable dtTemp = Misc.search("select userinfo.userid from userinfo inner join restaurant on (restaurant.userid = userInfo.userid) inner join deals on (deals.restaurantId = restaurant.restaurantId) where userinfo.userid=" + dtuserinfo.Rows[0]["userid"].ToString().Trim() + " and restaurant.restaurantId=" + Request.QueryString["rid"].Trim());
                    if (dtTemp != null && dtTemp.Rows.Count == 0)
                    {
                        Response.Redirect(ResolveUrl("~/UserDashBoard.aspx"));
                    }
                }
                GetAllBusinessInfoAndFillGrid();
            }
            else if (Request.QueryString["mode"] != null && Request.QueryString["mode"].Trim()!="")
            {
                if(Request.QueryString["mode"].Trim().ToLower() == "past")
                {
                    ddlSearchStatus.SelectedIndex = 2;
                    ddlSearchStatus.Visible = false;
                    btnSearch.Visible = false;
                }
                else if(Request.QueryString["mode"].Trim().ToLower() == "now") 
                {
                    ddlSearchStatus.SelectedIndex = 1;
                    ddlSearchStatus.Visible = false;
                    btnSearch.Visible = false;
                }
                DataTable dtuserinfo = (DataTable)Session["restaurant"];
                if (dtuserinfo != null && dtuserinfo.Rows.Count > 0 && dtuserinfo.Rows[0]["userid"] != null && dtuserinfo.Rows[0]["userid"].ToString().Trim() != "")
                {
                    DataTable dtTemp = Misc.search("select userinfo.userid from userinfo inner join restaurant on (restaurant.userid = userInfo.userid) inner join deals on (deals.restaurantId = restaurant.restaurantId) where userinfo.userid=" + dtuserinfo.Rows[0]["userid"].ToString().Trim());
                    if (dtTemp != null && dtTemp.Rows.Count == 0)
                    {
                        Response.Redirect(ResolveUrl("~/UserDashBoard.aspx"));
                    }
                }
                GetAllBusinessInfoAndFillGrid();
            }
            else
            {
                Response.Redirect(ResolveUrl("~/UserDashBoard.aspx"));
            }
        }
    }

    private int GetUserIdFromSession()
    {
        int iUserid = 0;
        DataTable dtUser = null;

        try
        {
            if (Session["restaurant"] != null)
            {
                dtUser = (DataTable)Session["restaurant"];
            }
            
            iUserid = int.Parse(dtUser.Rows[0]["userID"].ToString());
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return iUserid;
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
                dtUser = SearchhDealInfoByDifferentParams();
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
                    LinkButton imgPrev = (LinkButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    LinkButton imgNext = (LinkButton)pageGrid.BottomPagerRow.FindControl("btnNext");

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

            int iUserId = GetUserIdFromSession();
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


    private DataTable SearchhDealInfoByDifferentParams()
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
          

            strQuery = "SELECT ";
            //strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].[status]='Successful' and [dealOrders].dealId = [deals].dealId),0) 'Qty Ordered',";
            strQuery += " isnull((SELECT count(dealOrderDetail.detailID) FROM dealOrderDetail INNER JOIN dealOrders ON dealOrderDetail.dOrderID = dealOrders.dOrderID where dealOrders.dealid=[deals].dealid and isRedeemed=1 and dealOrders.status = 'Successful'),0) 'UsedCount',";
            strQuery += " isnull((SELECT count(dealOrderDetail.detailID) FROM dealOrderDetail INNER JOIN dealOrders ON dealOrderDetail.dOrderID = dealOrders.dOrderID where dealOrders.dealid=[deals].dealid and isRedeemed=0 and dealOrders.status = 'Successful'),0) 'UnUsedCount',";
            strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId)) ,0) 'Qty Ordered',";
            strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId and [dealOrders].[status]='Cancelled')) ,0) 'Cancelled Ordered',";
            strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId and [dealOrders].[status]='Refunded')) ,0) 'Refunded Ordered',";
            strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId and [dealOrders].[status]='Successful')) ,0) 'Successful Ordered',";
            strQuery += "  deals.dealId,deals.[restaurantId], [deals].[title]";
            strQuery += " ,[deals].[sellingPrice]";
            strQuery += " ,[deals].[valuePrice]";            
            strQuery += " ,[deals].[images]";
            strQuery += " FROM ";
            strQuery += " [deals]";
            strQuery += " INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
            strQuery += " inner join userInfo on userInfo.userId = restaurant.userId";
            strQuery += " INNER join dealcity On dealcity.[dealid]= deals.[dealid] ";
            strQuery += " INNER join city On dealCity.cityId= city.[cityId] ";
            strQuery += " where dealStatus = 1 ";
            if (Request.QueryString["rid"] != null && Request.QueryString["rid"].ToString().Trim() != "")
            {
                strQuery += " and restaurant.[restaurantId] = " + Request.QueryString["rid"].Trim();
            }
            else
            {
                strQuery += " and userInfo.userId = " + GetUserIdFromSession().ToString(); 
            }            
            if (this.ddlSearchStatus.SelectedValue == "started")
            {
                strQuery += " and dealStartTimeC <= getdate() and dealEndTimeC >= getdate()";
              
            }
            else if (this.ddlSearchStatus.SelectedValue == "expired")
            {
                strQuery += " and dealEndTimeC <= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "all")
            {
               // strQuery += " and dealStartTimeC <= getdate()";
            }
            strQuery += " group by deals.dealId,deals.[restaurantId], [deals].[title] ,[deals].[sellingPrice] ,[deals].[valuePrice] ,[deals].[images] ";
            strQuery += " order by dealId desc";



            //this.pageGrid.PageIndex = 0;

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
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            GetAllBusinessInfoAndFillGrid();
        }
        catch (Exception ex)
        { }
    }


    private DataTable SearchhDealInfoByDifferentParamsForDownload(string strDealID)
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";
            //strQuery += " ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber'";
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";
            strQuery += " ,dealOrders.createdDate";
            strQuery += " ,rtrim(userCCInfo.ccInfoDFirstName) +' ' + rtrim(userCCInfo.ccInfoDLastName) as 'Name'";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + rtrim(userInfo.lastName) as 'Name2'";
            strQuery += " ,rtrim(userShippingInfo.Name) as 'Name3'";
            strQuery += " ,shippingAndTax";
            strQuery += " ,[dealOrders].[status]";
            strQuery += " ,restaurantId";
            strQuery += " ,(userShippingInfo.Address+', '+userShippingInfo.City+', '+userShippingInfo.State+', '+userShippingInfo.ZipCode) as 'Address'";
            strQuery += " ,(userCCInfo.ccInfoBAddress+', '+userCCInfo.ccInfoBCity+', '+userCCInfo.ccInfoBProvince+', '+userCCInfo.ccInfoBPostalCode) as 'Address2',userinfo.phoneNo";
            strQuery += " ,userShippingInfo.Telephone";
            strQuery += ",userShippingInfo.shippingNote";
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " ,[dealOrderDetail].[detailID]";
            strQuery += " ,[dealOrderDetail].[isRedeemed]";
            strQuery += " ,[dealOrderDetail].[redeemedDate]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";
            strQuery += " ,[dealOrderDetail].[markUsed]";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join deals on (deals.dealId = dealOrders.dealId)";
            strQuery += " left outer join userShippingInfo on (userShippingInfo.shippingInfoId = dealOrders.shippingInfoId)";
            strQuery += " inner join userCCInfo on (userCCInfo.ccInfoID = dealOrders.ccInfoID)";
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            strQuery += " where dealOrders.dealId =" + strDealID + "";
            strQuery += " Order by dOrderID asc";            
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

    private void ExportToUser(DataTable table, string strFileName)
    {
        GridView gv = new GridView();
        gv.DataSource = table;
        gv.DataBind();
        string attachment = "attachment; filename=" + strFileName;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }

    private int GetDealIdFromApplication()
    {
        int iUserid = 0;

        try
        {
            if (Request.QueryString["did"] != null)
            {
                iUserid = int.Parse(Request.QueryString["did"].ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return iUserid;
    }

    protected void pageGrid_download(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Trim() == "DownloadExcel")
        {
            try
            {
                DataTable dtUser = SearchhDealInfoByDifferentParamsForDownload(e.CommandArgument.ToString());
                DataTable dtDealOrders = new DataTable("dealOrders");
                if (dtUser != null && dtUser.Rows.Count > 0
                    && dtUser.Rows[0]["shippingAndTax"].ToString().Trim() != ""
                    && Convert.ToBoolean(dtUser.Rows[0]["shippingAndTax"].ToString()))
                {
                    DataColumn Sr = new DataColumn("Sr.");
                    DataColumn Date = new DataColumn("Date");
                    DataColumn Customer = new DataColumn("Customer");
                    DataColumn dealOrderCode = new DataColumn("Voucher Code");
                    DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                    DataColumn Status = new DataColumn("Status");
                    DataColumn Telephone = new DataColumn("Telephone");
                    DataColumn Address = new DataColumn("Address");
                    DataColumn Note = new DataColumn("Note");
                    dtDealOrders.Columns.Add(Sr);
                    dtDealOrders.Columns.Add(Date);
                    dtDealOrders.Columns.Add(Customer);
                    dtDealOrders.Columns.Add(dealOrderCode);
                    dtDealOrders.Columns.Add(voucherSecurityCode);
                    dtDealOrders.Columns.Add(Status);
                    dtDealOrders.Columns.Add(Telephone);
                    dtDealOrders.Columns.Add(Address);
                    dtDealOrders.Columns.Add(Note);
                    DataRow dRow;
                    GECEncryption objEnc = new GECEncryption();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtUser.Rows.Count; i++)
                        {
                            dRow = dtDealOrders.NewRow();
                            dRow["Sr."] = i + 1;
                            try
                            {
                                dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                            }
                            catch (Exception ex)
                            {
                                dRow["Date"] = "";
                            }
                            dRow["Customer"] = dtUser.Rows[i]["Name3"].ToString().Trim();
                            dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                            dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                            dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                            dRow["Telephone"] = dtUser.Rows[i]["Telephone"].ToString().Trim();
                            dRow["Address"] = dtUser.Rows[i]["Address"].ToString().Trim();
                            dRow["Note"] = dtUser.Rows[i]["shippingNote"].ToString().Trim();
                            dtDealOrders.Rows.Add(dRow);
                        }
                        ExportToUser(dtDealOrders, "DealOrders.xls");
                    }
                }
                else
                {
                    BLLRestaurantGoogleAddresses objGoogle = new BLLRestaurantGoogleAddresses();
                    objGoogle.restaurantId = Convert.ToInt64(dtUser.Rows[0]["restaurantId"].ToString().Trim());
                    DataTable dtAddress = objGoogle.getAllRestaurantGoogleAddressesByRestaurantID();
                    if (dtAddress != null && dtAddress.Rows.Count > 0
                        && dtAddress.Rows[0]["restaurantGoogleAddress"] != null && dtAddress.Rows[0]["restaurantGoogleAddress"].ToString().Trim().ToLower() == "online")
                    {
                        DataColumn Sr = new DataColumn("Sr.");
                        DataColumn Date = new DataColumn("Date");
                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn dealOrderCode = new DataColumn("Voucher Code");
                        DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                        DataColumn Status = new DataColumn("Status");
                        DataColumn Telephone = new DataColumn("Telephone");
                        DataColumn Address = new DataColumn("Address");
                        dtDealOrders.Columns.Add(Sr);
                        dtDealOrders.Columns.Add(Date);
                        dtDealOrders.Columns.Add(Customer);
                        dtDealOrders.Columns.Add(dealOrderCode);
                        dtDealOrders.Columns.Add(voucherSecurityCode);
                        dtDealOrders.Columns.Add(Status);
                        dtDealOrders.Columns.Add(Telephone);
                        dtDealOrders.Columns.Add(Address);
                        DataRow dRow;
                        GECEncryption objEnc = new GECEncryption();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtUser.Rows.Count; i++)
                            {
                                dRow = dtDealOrders.NewRow();
                                dRow["Sr."] = i + 1;
                                try
                                {
                                    dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                                }
                                catch (Exception ex)
                                {
                                    dRow["Date"] = "";
                                }
                                dRow["Customer"] = dtUser.Rows[i]["Name"].ToString().Trim();
                                dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dRow["Telephone"] = dtUser.Rows[i]["phoneNo"].ToString().Trim();
                                dRow["Address"] = dtUser.Rows[i]["Address2"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            ExportToUser(dtDealOrders, "DealOrders.xls");
                        }
                    }
                    else
                    {

                        DataColumn Sr = new DataColumn("Sr.");
                        DataColumn Date = new DataColumn("Date");
                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn dealOrderCode = new DataColumn("Voucher Code");
                        DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                        DataColumn Status = new DataColumn("Status");
                        dtDealOrders.Columns.Add(Sr);
                        dtDealOrders.Columns.Add(Date);
                        dtDealOrders.Columns.Add(Customer);
                        dtDealOrders.Columns.Add(dealOrderCode);
                        dtDealOrders.Columns.Add(voucherSecurityCode);
                        dtDealOrders.Columns.Add(Status);
                        DataRow dRow;
                        GECEncryption objEnc = new GECEncryption();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtUser.Rows.Count; i++)
                            {
                                dRow = dtDealOrders.NewRow();
                                dRow["Sr."] = i + 1;
                                try
                                {
                                    dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                                }
                                catch (Exception ex)
                                {
                                    dRow["Date"] = "";
                                }
                                dRow["Customer"] = dtUser.Rows[i]["Name2"].ToString().Trim();
                                dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            ExportToUser(dtDealOrders, "DealOrders.xls");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


        if (e.CommandName.Trim() == "DownloadPdf")
        {
            try
            {
                string url = ConfigurationManager.AppSettings["YourSite"] + "/tastypdfdownload.aspx?pdf=TRUE&did=" + e.CommandArgument.ToString();
                Process PDFProcess = new Process();
                string FileName = @" " + AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\invoice.pdf";
                PDFProcess.StartInfo.UseShellExecute = false;
                PDFProcess.StartInfo.CreateNoWindow = true;
                PDFProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"bin\wkhtmltopdf.exe";
                PDFProcess.StartInfo.Arguments = url + FileName;
                PDFProcess.StartInfo.RedirectStandardOutput = true;
                PDFProcess.StartInfo.RedirectStandardError = true;
                PDFProcess.Start();
                PDFProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                string xx = ex.Message.ToString();
                Response.Write("<br>" + xx);
            }

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\invoice.pdf";
            try
            {
                string contentType = "";
                contentType = "Application/pdf";
                Response.ContentType = contentType;
                Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo("invoice.pdf")).Name);
                Response.WriteFile(FilePath);
                Response.End();
            }
            catch
            {
                //To Do
            }
        }

        if (e.CommandName.Trim() == "checkdealstatus")
        {
            Response.Redirect("dashboarddealstatus.aspx?sidedealId=" + e.CommandArgument.ToString());
            Response.End();
        }
 
    }



}