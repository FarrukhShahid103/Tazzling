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
using com.optimalpayments.webservices;
public partial class admin_UserControls_ctrlDealOrderInfo : System.Web.UI.UserControl
{
    BLLDealOrders objBLLDealOrders = new BLLDealOrders();

    public bool displayPrevious = false;
    public bool displayNext = true;

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
        if (!IsPostBack)
        {
            
              string struserid = "";
            DataTable dtUser = (DataTable)Session["user"];
            
            if ((dtUser != null) && (dtUser.Rows.Count > 0))
            {
                ViewState["userTypeID"] = dtUser.Rows[0]["userTypeID"].ToString().Trim();
            }

            GetAllBusinessInfoAndFillGrid(0);
        }
    }

    protected bool getDetailStatus(object status, object ccCreditUsed)
    {
        if (ccCreditUsed.ToString() == "" || Convert.ToDouble(ccCreditUsed.ToString()) == 0)
        {
            return false;
        }

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

    protected bool getButtonStatus(object tracking, object dealEndTime, object affiliatePartnerId, object gainedId)
    {
        try
        {
            if (Convert.ToBoolean(tracking))
            {
                if (dealEndTime.ToString() != "")
                {
                    DateTime dtDealEndTime = Convert.ToDateTime(dealEndTime);
                    if (dtDealEndTime <= DateTime.Now)
                    {
                        if (ViewState["userTypeID"] != null && (ViewState["userTypeID"].ToString().Trim() == "1"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (ViewState["userTypeID"] != null && (ViewState["userTypeID"].ToString().Trim() == "1"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
        catch (Exception ex)
        {
        }

        if (Convert.ToInt32(gainedId) > 0 || Convert.ToInt32(affiliatePartnerId) > 0)
        {
            if (ViewState["userTypeID"] != null && (ViewState["userTypeID"].ToString().Trim() == "1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
    
    protected bool getPayBackStatus(object tracking, object dealEndTime, object status, object psgTranNo, object ccCreditUsed, object tastyCreditUsed, object comissionMoneyUsed, object createdDate, object affiliatePartnerId, object gainedId)
    {
        try
        {
            try
            {
                if (Convert.ToBoolean(tracking))
                {
                    if (dealEndTime.ToString() != "")
                    {
                        DateTime dtDealEndTime = Convert.ToDateTime(dealEndTime);
                        if (dtDealEndTime <= DateTime.Now)
                        {
                            if ((status.ToString().ToLower().Trim() == "successful" || status.ToString().ToLower().Trim() == "cancelled") && psgTranNo.ToString().Trim() != "")
                            {
                                if (ViewState["userTypeID"] != null && (ViewState["userTypeID"].ToString().Trim() == "1"))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }

                           
                        }
                    }
                    else
                    {
                        if (ViewState["userTypeID"] != null && (ViewState["userTypeID"].ToString().Trim() == "1"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
            TimeSpan difference=DateTime.Now.Subtract(Convert.ToDateTime(createdDate));
            if (status.ToString() != "" && difference.Days<30)
            {
                if ((status.ToString().ToLower().Trim() == "successful" || status.ToString().ToLower().Trim() == "cancelled") && psgTranNo.ToString().Trim() != "")
                {
                    if ((tastyCreditUsed.ToString() != "") && (comissionMoneyUsed.ToString() != ""))
                    {
                        if (Convert.ToDouble(tastyCreditUsed.ToString()) > 0 || Convert.ToDouble(comissionMoneyUsed.ToString()) > 0)
                        {
                            return false;
                        }
                        else
                        {
                            if (Convert.ToInt32(gainedId) > 0 || Convert.ToInt32(affiliatePartnerId) > 0)
                            {
                                if (ViewState["userTypeID"] != null && (ViewState["userTypeID"].ToString().Trim() == "1"))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        catch (Exception ex)
        { return false; }
    }
    #region "Get and Set the Deal Order Info here"

    protected void btnShowAll_Click(object sender, ImageClickEventArgs e)
    {
        txtSearchDealName.Text = "";
        txtUserName.Text = "";
        ddlSrchOrderStatus.SelectedIndex = 0;
        ViewState["Query"] = null;        
        GetAllBusinessInfoAndFillGrid(0);
    }

    protected void GetAllBusinessInfoAndFillGrid(int intPageNumber)
    {
        try
        {
            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(ViewState["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = 5;
                ViewState["ddlPage"] = 5;
            }
            DataSet dstOrders=null;
            DataTable dtUser;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                btnShowAll.Visible = false;
                dstOrders = objBLLDealOrders.getAllDealOrdersByPageIndex(intPageNumber, pageGrid.PageSize);
                dtUser = dstOrders.Tables[0];
                dv = new DataView(dtUser);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
            }
            else
            {                
                BindSearchedData(intPageNumber.ToString());
                return;
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                pageGrid.DataSource = dv;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
                string strTotalOrders = "";
                if (dstOrders!=null && dstOrders.Tables[1] != null)
                {
                    strTotalOrders = dstOrders.Tables[1].Rows[0][0].ToString();
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
            this.GetAllBusinessInfoAndFillGrid(intCurrentPage-1);
            txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (intCurrentPage).ToString();            
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
                this.GetAllBusinessInfoAndFillGrid(intCurrentPage - 1);
                txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = intCurrentPage.ToString();
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
            DataSet dstOrders = null;
            DataTable dtUser = null;
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            if (ViewState["Query"] != null)
            {
                dtUser = Misc.search(ViewState["Query"].ToString());
            }
            else
            {
                dstOrders = objBLLDealOrders.getAllDealOrdersByPageIndex(1, pageGrid.PageSize);
                dtUser = dstOrders.Tables[0];
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
            DataView dv = new DataView(dtUser);
            dv.Sort = sortExpression + direction;
            ViewState["Direction"] = sortExpression + direction;
            pageGrid.DataSource = dv;
            pageGrid.DataBind();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
                string strTotalOrders = dstOrders.Tables[1].Rows[0][0].ToString();
                lblTotalRecords.Text = strTotalOrders;
                lblPageCount.Text = pageGrid.PageCount.ToString();

                DropDownList ddlPage = bindPageDropDown(strTotalOrders);
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
            this.GetAllBusinessInfoAndFillGrid(0);
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

    protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int idOrderID = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);

            Application["OrderId"] = idOrderID.ToString();

            Response.Redirect(ResolveUrl("~/admin/dealOrderDetailInfo.aspx"));

        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

   
    protected void pageGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfPrefilledData = (HiddenField)e.Row.FindControl("hfPrefilledData");
            string[] Text = hfPrefilledData.Value.ToString().Trim().Split('|');

            string psi = "Psigate : " +  Text[0] + "|";
            string DealTitle = "Deal Title : " + Text[1].Replace("\'","").Replace("\r\n","") + "|";
            string Reason = "Reason for Refund or Credits : " + "|";
            string Userid = Text[2] + "|";
            string dOrderID = Text[3] + "|";


            string CompleteData = psi + DealTitle + Reason + Userid + dOrderID;

            Button BtnHidden = (Button)e.Row.FindControl("BtnHidden");
            Button BtnHiddenPayBack = (Button)e.Row.FindControl("BtnHiddenPayBack");


            ImageButton btnLinkUpdate = (ImageButton)e.Row.FindControl("btnLinkUpdate");

            ImageButton btnPayBack = (ImageButton)e.Row.FindControl("btnPayBack");

            btnLinkUpdate.OnClientClick = "return RunPopup('" + BtnHidden.ClientID + "', '" + CompleteData + "','false','')";

            btnPayBack.OnClientClick = "return RunPopup('" + BtnHidden.ClientID + "', '" + CompleteData + "','true','" + BtnHiddenPayBack.ClientID + "')";
            
        }
    }  

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strQuery = "";
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            //strQuery += " Select * from (";	
            //strQuery += " SELECT ";
            //strQuery += " [dealOrders].[dOrderID]";
            //strQuery += " ,dealOrders.dealId	";
            //strQuery += " ,restaurant.[restaurantBusinessName]";
            //strQuery += " ,(select count(*) from dealOrderDetail where isRedeemed = 1 and dealOrderDetail.dOrderID = [dealOrders].dOrderID) as 'RedeemCount'";
            //strQuery += " ,deals.[title] as 'DealName'";
            //strQuery += " ,city.[CityName]";
            //strQuery += " ,deals.[images]as 'DealImage'";
            //strQuery += " ,orderNo,totalAmt";
            //strQuery += " ,psgTranNo";
            //strQuery += " ,[dealOrders].[Qty]";
            //strQuery += " ,dealOrders.createdDate";
            //strQuery += " ,[dealOrders].[status]";
            //strQuery += " ,restaurant.restaurantId";
            //strQuery += " ,[dealOrders].CCInfoID";
            //strQuery += " ,userType.UserType";
            //strQuery += " ,userInfo.[userName]";
            //strQuery += " ,ccCreditUsed";
            //strQuery += " ,tastyCreditUsed";
            //strQuery += " ,comissionMoneyUsed";
            //strQuery += " ,rtrim(userInfo.firstName) + ' ' + rtrim(userInfo.lastName) as 'Name'";
            //strQuery += " ,userInfo.[userName]";
            //strQuery += " ,userInfo.userId";
            //strQuery += " ,isnull(affComm,0) 'affComm'";
            //strQuery += " ,ROW_NUMBER() OVER(ORDER BY dealOrders.dorderid desc) as RowNum";
            //strQuery += " FROM ";
            //strQuery += " [dealOrders]";
            //strQuery += " inner join deals on deals.dealId = dealOrders.dealId";
            //strQuery += " inner join restaurant on restaurant.restaurantId = deals.restaurantId";
            //strQuery += " inner join city on city.cityid = restaurant.cityid";
            //strQuery += " inner join userInfo on userInfo.userId = dealOrders.userId";
            //strQuery += " inner join userType on userType.userTypeID = userInfo.userTypeID";

            //if (txtUserName.Text.Trim() != "")
            //{
            //    strQuery += " and userInfo.[userName] like '%" + txtUserName.Text.Trim() + "%' ";
            //}

            //if (txtSearchDealName.Text.Trim() != "")
            //{
            //    strQuery += " and deals.[title] like '%" + txtSearchDealName.Text.Trim() + "%' ";
            //}

            //if (ddlSrchOrderStatus.SelectedItem.Text.Trim() != "All")
            //{
            //    strQuery += " and [dealOrders].[status] = '" + ddlSrchOrderStatus.SelectedItem.Text.Trim() + "' ";
            //}
            //strQuery += " ) as DerivedTableName";            
            //strQuery += " WHERE RowNum BETWEEN (@startRowIndex*@maximumRows)+1 AND ((@startRowIndex+1)* @maximumRows)";
            pageGrid.PageIndex = 0;
            BindSearchedData("0");
            //ViewState["Query"] = strQuery;
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

    protected string getDealCode(object strDealCode)
    {
        if (strDealCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.DecryptData("deatailOrder", strDealCode.ToString());
        }
        return "";
    }
    

    #region Function to bind Search data in Grid
    private void BindSearchedData(string strRowIndex)
    {
        try
        {
            btnShowAll.Visible = true;
            string strQuery = "";                   
            strQuery += " Select * from (";	
            strQuery += " SELECT ";
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId ";            
            strQuery += " ,isnull((SELECT top 1 affiliatePartnerId from affiliatePartnerGained where affiliatePartnerGained.orderId = [dealOrders].dOrderID) ,0) AS 'affiliatePartnerId'";
            strQuery += " ,isnull((SELECT top 1 gainedId from dbo.gained where dbo.gained.gainedId = [dealOrders].dOrderID) ,0) AS 'gainedId'";	 			
            strQuery += " ,(select count(*) from dealOrderDetail where isRedeemed = 1 and dealOrderDetail.dOrderID = [dealOrders].dOrderID) as 'RedeemCount'";
            strQuery += " ,deals.[title] as 'DealName'";
            strQuery += " ,deals.dealEndTime";
            strQuery += " ,deals.tracking";
            strQuery += " ,dealOrderCode";
            strQuery += " ,orderNo,totalAmt";
            strQuery += " ,psgTranNo";
            strQuery += " ,[dealOrders].[Qty]";
            strQuery += " ,dealOrders.createdDate";
            strQuery += " ,[dealOrders].[status]";            
            strQuery += " ,[dealOrders].CCInfoID";            
            strQuery += " ,userInfo.[userName]";
            strQuery += " ,ccCreditUsed";
            strQuery += " ,tastyCreditUsed";
            strQuery += " ,comissionMoneyUsed";            
            strQuery += " ,userInfo.userId";
            strQuery += " ,isnull(affComm,0) 'affComm'";
            strQuery += " ,ROW_NUMBER() OVER(ORDER BY dealOrders.dorderid desc) as RowNum";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join deals on deals.dealId = dealOrders.dealId";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.dorderid = dealOrders.dorderid";
            strQuery += " inner join userInfo on userInfo.userId = dealOrders.userId";            

            if (txtUserName.Text.Trim() != "")
            {
                strQuery += " and userInfo.[userName] like '%" + txtUserName.Text.Trim() + "%' ";
            }

            if (txtVoucherNumber.Text.Trim() != "")
            {
                GECEncryption objEnc = new GECEncryption();                
                strQuery += " and dealOrderDetail.[dealOrderCode] = '" + objEnc.EncryptData("deatailOrder", txtVoucherNumber.Text.Trim()) + "' ";
            }

            if (txtSearchDealName.Text.Trim() != "")
            {
                strQuery += " and deals.[title] like '%" + txtSearchDealName.Text.Trim() + "%' ";
            }

            if (ddlSrchOrderStatus.SelectedItem.Text.Trim() != "All")
            {
                strQuery += " and [dealOrders].[status] = '" + ddlSrchOrderStatus.SelectedItem.Text.Trim() + "' ";
            }
            strQuery += " ) as DerivedTableName";
            int strStartIndex = (Convert.ToInt32(strRowIndex) * pageGrid.PageSize) + 1;
            int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * pageGrid.PageSize;
            strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;
            strQuery += " order by dorderid desc";

            strQuery += " SELECT 'Return Value' =  COUNT(dealOrders.dorderid) FROM [dealOrders] ";
            strQuery += " inner join deals on deals.dealId = dealOrders.dealId";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.dorderid = dealOrders.dorderid";
            strQuery += " inner join userInfo on userInfo.userId = dealOrders.userId";            

            if (txtUserName.Text.Trim() != "")
            {
                strQuery += " and userInfo.[userName] like '%" + txtUserName.Text.Trim() + "%' ";
            }

            if (txtVoucherNumber.Text.Trim() != "")
            {
                GECEncryption objEnc = new GECEncryption();
                strQuery += " and dealOrderDetail.[dealOrderCode] = '" + objEnc.EncryptData("deatailOrder", txtVoucherNumber.Text.Trim()) + "' ";
            }

            if (txtSearchDealName.Text.Trim() != "")
            {
                strQuery += " and deals.[title] like '%" + txtSearchDealName.Text.Trim() + "%' ";
            }

            if (ddlSrchOrderStatus.SelectedItem.Text.Trim() != "All")
            {
                strQuery += " and [dealOrders].[status] = '" + ddlSrchOrderStatus.SelectedItem.Text.Trim() + "' ";
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

            DataSet dst=Misc.searchDataSet(strQuery);
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

    #endregion
    
    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)pageGrid.Rows[pageGrid.SelectedIndex].FindControl("ddlStatus");

        HiddenField hfUserId = (HiddenField)pageGrid.Rows[pageGrid.SelectedIndex].FindControl("hfUserId");

        //Get the Affiliate Commission
        HiddenField hfAffComm = (HiddenField)pageGrid.Rows[pageGrid.SelectedIndex].FindControl("hfAffComm");

        String orderStatus = ddlStatus.SelectedValue.Trim();

        long iOrderId = Convert.ToInt32(pageGrid.SelectedDataKey.Value);

        int iUserID = int.Parse(hfUserId.Value.Trim());

        bool boolChangeORderstatus = false;

        if (ddlStatus.SelectedItem.Value == "Successful")
        {
            //Remove the Tastygo Refferal Credit functionality
            //AddTastyGoCreditToReffer(iUserID, iOrderId);

            Misc.createPDF(iOrderId.ToString());

            HiddenField hfOrderTotal = (HiddenField)pageGrid.Rows[pageGrid.SelectedIndex].FindControl("hfOrderTotal");
            
            //Add Commission to Reffer
            //AddAffiliateCommissionToReffer(iUserID, iOrderId, hfOrderTotal.Value, hfAffComm.Value.Trim());
            //CalculateAffiliateCommissionByOrderId(int.Parse(iOrderId.ToString()));           

            if (!AddTastyGoCreditToReffer(Convert.ToInt64(iOrderId.ToString())))
            {
                CalculateAffiliateCommissionByOrderId(int.Parse(iOrderId.ToString()));
            }

            boolChangeORderstatus = ChangeDealOrderStatus(iOrderId, orderStatus);
            objBLLDealOrders = new BLLDealOrders();
            objBLLDealOrders.dOrderID = iOrderId;
            DataTable dtorders = objBLLDealOrders.getDealOrderDetailByOrderID();
            Misc.SendEmail(dtorders.Rows[0]["username"].ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), ConfigurationManager.AppSettings["EmailSubjectForNewOrderForMember"].ToString().Trim(), RenderOrderDetailHTML(iOrderId.ToString()));

        }
        else
        {           
            RefundTastyGoCredit(iOrderId);
            CancelDeclinedAffiliateCommissionByOrderId(int.Parse(iOrderId.ToString()), "Cancelled");
            boolChangeORderstatus = ChangeDealOrderStatus(iOrderId, orderStatus);
            if (orderStatus.Trim() == "Refunded" || orderStatus.Trim() == "Cancelled")
            {
                objBLLDealOrders = new BLLDealOrders();
                objBLLDealOrders.dOrderID = iOrderId;
                DataTable dtorders = objBLLDealOrders.getDealOrderDetailByOrderID();
                if (dtorders != null && dtorders.Rows.Count > 0 && orderStatus.Trim() == "Refunded")
                {                    
                    Misc.SendEmail(dtorders.Rows[0]["username"].ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), "Your Order Status has changed", RenderOrderDetailHTMLForRefund(iOrderId.ToString()));
                   // if (dtorders.Rows[0]["tastyCreditUsed"] != null
                   //     && dtorders.Rows[0]["tastyCreditUsed"].ToString().Trim() != ""
                   //     && Convert.ToDouble(dtorders.Rows[0]["tastyCreditUsed"].ToString()) > 0)
                   // {
                   //     RefundTastyGoCreditToUser(Convert.ToInt64(dtorders.Rows[0]["userId"].ToString()), float.Parse(dtorders.Rows[0]["tastyCreditUsed"].ToString()));
                   // }

                   // if (dtorders.Rows[0]["comissionMoneyUsed"] != null
                   //&& dtorders.Rows[0]["comissionMoneyUsed"].ToString().Trim() != ""
                   //&& Convert.ToDouble(dtorders.Rows[0]["comissionMoneyUsed"].ToString()) > 0)
                   // {
                   //     RefundTastyGoComissionToUser(Convert.ToInt32(dtorders.Rows[0]["userId"].ToString()), float.Parse(dtorders.Rows[0]["comissionMoneyUsed"].ToString()));
                   // }
                }
            }

        }

        if (boolChangeORderstatus)
        {
            pageGrid.ShowFooter = true;
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            string strPage = txtPage.Text;
            GetAllBusinessInfoAndFillGrid(Convert.ToInt32(txtPage.Text)-1);
            txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = strPage;    
            lblMessage.Text = "Order status has been updated successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
    }

    private void RefundTastyGoCreditToUser(long userID, float amount)
    {
      

        try
        {

            BLLMemberUsedGiftCards objUsedCard = new BLLMemberUsedGiftCards();
            //Add $10 Credit into the User Account
            objUsedCard.remainAmount = amount;

            objUsedCard.createdBy = userID;

            objUsedCard.gainedAmount = amount;

            //If user places the first order then he will get the $10
            objUsedCard.fromId = 1;

            objUsedCard.targetDate = DateTime.Now.AddMonths(6);

            objUsedCard.currencyCode = "CAD";

            objUsedCard.gainedType = "Refferal";

            objUsedCard.orderId = 0;

            objUsedCard.createMemberUseableGiftCard();
            
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        
    }

    private void RefundTastyGoComissionToUser(int userID, float amount)
    {
        try
        {
            DataTable dtAdmin = (DataTable)Session["user"];
            if (dtAdmin != null)
            {
                BLLAffiliatePartnerGained objUsedCard = new BLLAffiliatePartnerGained();

                objUsedCard.UserId = userID;
                objUsedCard.GainedType = "Adjustment";
                objUsedCard.GainedAmount = amount;

                objUsedCard.RemainAmount = amount;

                objUsedCard.FromId = Convert.ToInt32(dtAdmin.Rows[0]["userId"].ToString());
                objUsedCard.CreatedBy = Convert.ToInt32(dtAdmin.Rows[0]["userId"].ToString());

                objUsedCard.createAffiliatePartnerGained();
                
            }
            else
            {
                Response.Redirect(ResolveUrl("~/Admin/Default.aspx"), false);
            }
        }
        catch (Exception ex)
        {
           
        }
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

    protected void pageGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
            case "ViewCreditCardInfo":
                Application["DealOrderID"] = e.CommandArgument.ToString();
                Response.Redirect(ResolveUrl("~/admin/dealOrderCCI.aspx"));
                //Setting timer to test longer loading
                //Thread.Sleep(2000);
                break;
            case "MakePayment":
                sendOrderToProcess(Convert.ToInt64(e.CommandArgument.ToString()));
                break;
            case "PayBack":

                PayBackToCustomer(Convert.ToInt64(e.CommandArgument.ToString()));
                break;
        }
    }

    private void PayBackToCustomer(long OrderID)
    {
        try
        {            
            objBLLDealOrders = new BLLDealOrders();
            objBLLDealOrders.dOrderID = OrderID;
            float fTastyCredit = 0;
            DataTable dtorders = objBLLDealOrders.getDealOrderDetailByOrderID();
            if (dtorders != null && dtorders.Rows.Count > 0)
            {
                if (dtorders.Rows[0]["psgError"].ToString().Contains("Optimal"))
                {
                    //Get the Tasty Credits of the Order if order have
                    try
                    {
                        System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                        fTastyCredit = dtorders.Rows[0]["tastyCreditUsed"] == DBNull.Value ? 0 : float.Parse(dtorders.Rows[0]["tastyCreditUsed"].ToString());

                        CCPostAuthRequestV1 ccPostAuthRequest = new CCPostAuthRequestV1();
                        ccPostAuthRequest.confirmationNumber = dtorders.Rows[0]["psgTranNo"].ToString().Trim();
                        MerchantAccountV1 merchantAccount = new MerchantAccountV1();
                        merchantAccount.accountNum = ConfigurationManager.AppSettings["netBankAccountNum"].ToString().Trim();
                        merchantAccount.storeID = ConfigurationManager.AppSettings["netBankStoreID"].ToString().Trim();
                        merchantAccount.storePwd = ConfigurationManager.AppSettings["netBankStorePwd"].ToString().Trim();
                        ccPostAuthRequest.merchantAccount = merchantAccount;
                        ccPostAuthRequest.merchantRefNum = Guid.NewGuid().ToString();
                        ccPostAuthRequest.amount = (dtorders.Rows[0]["ccCreditUsed"] == DBNull.Value ? "0.00" : dtorders.Rows[0]["ccCreditUsed"].ToString());
                        // Perform the Web Service call for the Settlement
                        CreditCardServiceV1 ccService = new CreditCardServiceV1();
                        CCTxnResponseV1 ccTxnResponse = ccService.ccCredit(ccPostAuthRequest);
                        if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
                        {
                            objBLLDealOrders.status = "Refunded";
                            int iUserID = int.Parse(dtorders.Rows[0]["userid"].ToString());
                            RefundTastyGoCredit(OrderID);
                            CancelDeclinedAffiliateCommissionByOrderId(int.Parse(OrderID.ToString()), "Cancelled");
                            Misc.SendEmail(dtorders.Rows[0]["username"].ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), "Your Order Status has changed", RenderOrderDetailHTMLForRefund(OrderID.ToString()));

                            if (dtorders.Rows[0]["tastyCreditUsed"] != null
                               && dtorders.Rows[0]["tastyCreditUsed"].ToString().Trim() != ""
                               && Convert.ToDouble(dtorders.Rows[0]["tastyCreditUsed"].ToString()) > 0)
                            {
                                RefundTastyGoCreditToUser(Convert.ToInt64(dtorders.Rows[0]["userId"].ToString()), float.Parse(dtorders.Rows[0]["tastyCreditUsed"].ToString()));
                            }

                            if (dtorders.Rows[0]["comissionMoneyUsed"] != null
                               && dtorders.Rows[0]["comissionMoneyUsed"].ToString().Trim() != ""
                               && Convert.ToDouble(dtorders.Rows[0]["comissionMoneyUsed"].ToString()) > 0)
                            {
                                RefundTastyGoComissionToUser(Convert.ToInt32(dtorders.Rows[0]["userId"].ToString()), float.Parse(dtorders.Rows[0]["comissionMoneyUsed"].ToString()));
                            }

                            lblMessage.Visible = true;
                            lblMessage.Text = "Payment rollback successfully";
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
                            lblMessage.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            objBLLDealOrders.status = dtorders.Rows[0]["status"].ToString().Trim();
                            lblMessage.Visible = true;
                            lblMessage.Text = ccTxnResponse.code + " : " + ccTxnResponse.description;                                            
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "~/admin/images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                        objBLLDealOrders.psgTranNo = dtorders.Rows[0]["psgTranNo"].ToString().Trim();
                        objBLLDealOrders.psgError = dtorders.Rows[0]["psgError"].ToString();
                        objBLLDealOrders.updateDealOrderStatusByOrderId();


                        TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                        GetAllBusinessInfoAndFillGrid(Convert.ToInt32(txtPage.Text) - 1);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/admin/images/error.png";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    System.Net.HttpWebRequest myWebReqeust = (HttpWebRequest)System.Net.WebRequest.Create(ConfigurationManager.AppSettings["psigateXMLlinq"].ToString());
                    myWebReqeust.Timeout = 9000;
                    StringBuilder myParams = new StringBuilder();
                    myParams.Append("<?xml version='1.0' encoding='UTF-8'?>");
                    myParams.Append("<Order>");
                    myParams.Append("<StoreID>" + ConfigurationSettings.AppSettings["StoreID"].ToString() + "</StoreID>");
                    myParams.Append("<Passphrase>" + ConfigurationManager.AppSettings["Passphrase"].ToString() + "</Passphrase>");
                    //myParams.Append("<Subtotal>" + (Convert.ToDouble(dtorders.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtorders.Rows[0]["Qty"].ToString())).ToString() + "</Subtotal>");
                    myParams.Append("<Subtotal>" + (dtorders.Rows[0]["ccCreditUsed"] == DBNull.Value ? "0" : dtorders.Rows[0]["ccCreditUsed"].ToString()) + "</Subtotal>");
                    myParams.Append("<PaymentType>CC</PaymentType>");
                    myParams.Append("<CardAction>3</CardAction>");
                    myParams.Append("<OrderID>" + dtorders.Rows[0]["psgTranNo"].ToString() + "</OrderID>");
                    myParams.Append("</Order>");
                    myWebReqeust.Method = "POST";
                    myWebReqeust.ContentLength = myParams.ToString().Length;
                    myWebReqeust.ContentType = "application/x-www-form-urlencoded";
                    myWebReqeust.KeepAlive = false;
                    System.IO.StreamWriter myWriter;
                    myWriter = new System.IO.StreamWriter(myWebReqeust.GetRequestStream());
                    myWriter.Write(myParams.ToString());
                    myWriter.Close();

                    //Get the Tasty Credits of the Order if order have
                    fTastyCredit = dtorders.Rows[0]["tastyCreditUsed"] == DBNull.Value ? 0 : float.Parse(dtorders.Rows[0]["tastyCreditUsed"].ToString());

                    try
                    {
                        System.Net.WebResponse myWebResponse = myWebReqeust.GetResponse();
                        StreamReader myStreamReader = new StreamReader(myWebResponse.GetResponseStream());
                        string myHTML = myStreamReader.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(myHTML);
                        XmlNode root = doc.DocumentElement;
                        if (root.HasChildNodes)
                        {
                            objBLLDealOrders = new BLLDealOrders();
                            objBLLDealOrders.dOrderID = OrderID;
                            if(doc.GetElementsByTagName("Approved")[0].InnerText.ToString().ToLower().Trim() == "approved")
                            {
                                objBLLDealOrders.status = "Refunded";

                                //Refund TastyGo Credit
                                //RefundTastyGoCredit(OrderID);

                                //Get the UserId of the Order
                                int iUserID = int.Parse(dtorders.Rows[0]["userid"].ToString());

                                //If Deal Order Contains the tasty Credits -- On colin demand 40--15
                                /*if (fTastyCredit > 0)
                                    reverseFoodAmount(fTastyCredit, iUserID);*/

                                //Reverse the Commision here    
                                //Refund the Affiliate 7% credit here
                                // RefundAffiliatePartnerCredits(OrderID);
                                RefundTastyGoCredit(OrderID);
                                CancelDeclinedAffiliateCommissionByOrderId(int.Parse(OrderID.ToString()), "Cancelled");
                                Misc.SendEmail(dtorders.Rows[0]["username"].ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), "Your Order Status has changed", RenderOrderDetailHTMLForRefund(OrderID.ToString()));

                                if (dtorders.Rows[0]["tastyCreditUsed"] != null
                                   && dtorders.Rows[0]["tastyCreditUsed"].ToString().Trim() != ""
                                   && Convert.ToDouble(dtorders.Rows[0]["tastyCreditUsed"].ToString()) > 0)
                                {
                                    RefundTastyGoCreditToUser(Convert.ToInt64(dtorders.Rows[0]["userId"].ToString()), float.Parse(dtorders.Rows[0]["tastyCreditUsed"].ToString()));
                                }

                                if (dtorders.Rows[0]["comissionMoneyUsed"] != null
                                   && dtorders.Rows[0]["comissionMoneyUsed"].ToString().Trim() != ""
                                   && Convert.ToDouble(dtorders.Rows[0]["comissionMoneyUsed"].ToString()) > 0)
                                {
                                    RefundTastyGoComissionToUser(Convert.ToInt32(dtorders.Rows[0]["userId"].ToString()), float.Parse(dtorders.Rows[0]["comissionMoneyUsed"].ToString()));
                                }

                            }
                            else
                            {
                                objBLLDealOrders.status = dtorders.Rows[0]["status"].ToString().Trim();
                            }
                            //else if (root.ChildNodes[3].InnerText.ToString().ToLower() == "declined" || root.ChildNodes[3].InnerText.ToString().ToLower() == "error")
                            //{
                            //    objBLLDealOrders.status = dtorders.Rows[0]["status"].ToString().Trim();
                            //}
                            //else
                            //{
                            //    objBLLDealOrders.status = root.ChildNodes[3].InnerText.ToString();
                            //}
                            objBLLDealOrders.psgTranNo = root.ChildNodes[1].InnerText.ToString();
                            objBLLDealOrders.psgError = root.ChildNodes[12].InnerText.ToString() + ": " + root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
                            objBLLDealOrders.updateDealOrderStatusByOrderId();
                            // string strOrderEmailBody = RenderOrderDetailHTML(OrderID.ToString());

                            // Misc.SendEmail(dtorders.Rows[0]["ccInfoDEmail"].ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), ConfigurationManager.AppSettings["EmailSubjectForNewOrderForMember"].ToString().Trim(), strOrderEmailBody);

                            // string strApproved = root.ChildNodes[3].InnerText.ToString();
                            // string strOrderID = root.ChildNodes[1].InnerText.ToString();
                        }
                        myStreamReader.Close();
                        TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                        GetAllBusinessInfoAndFillGrid(Convert.ToInt32(txtPage.Text) - 1);
                        if (root.ChildNodes[3].InnerText.ToString().ToLower() == "approved")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Payment rollback successfully";
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
                            lblMessage.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = root.ChildNodes[3].InnerText.ToString() + ": " + root.ChildNodes[5].InnerText.ToString();
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "~/admin/images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }

                    }
                    catch (Exception ex)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/admin/images/error.png";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private bool RefundTastyGoCredit(long orderID)
    {
        bool bStatus = false;
        try
        {
            BLLMemberUsedGiftCards objBLLMemberUsedGiftCards = new BLLMemberUsedGiftCards();
            objBLLMemberUsedGiftCards.orderId = orderID;
            DataTable dtgained = objBLLMemberUsedGiftCards.getTastyCreditsByOrderID();
            if (dtgained != null && dtgained.Rows.Count > 0)
            {
                objBLLMemberUsedGiftCards.remainAmount = float.Parse("-10.00");

                objBLLMemberUsedGiftCards.createdBy = Convert.ToInt64(dtgained.Rows[0]["createdBy"].ToString().Trim());

                objBLLMemberUsedGiftCards.gainedAmount = float.Parse("-10.00");
                //If user places the first order then he will get the $10
                objBLLMemberUsedGiftCards.fromId = Convert.ToInt64(dtgained.Rows[0]["fromId"].ToString().Trim());

                objBLLMemberUsedGiftCards.targetDate = DateTime.Now.AddMonths(6);

                objBLLMemberUsedGiftCards.currencyCode = "CAD";

                objBLLMemberUsedGiftCards.gainedType = "Refunded Order";

                objBLLMemberUsedGiftCards.orderId = orderID;
                
                //objBLLMemberUsedGiftCards.remainAmount = float.Parse(dtgained.Rows[0]["remainAmount"].ToString().Trim()) - float.Parse(dtgained.Rows[0]["gainedAmount"].ToString().Trim());
                //int iChk = objBLLMemberUsedGiftCards.updateCreditByOrderId();
                int iChk = objBLLMemberUsedGiftCards.createMemberUseableGiftCard();
                if (iChk != 0)
                    bStatus = true;
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return bStatus;
    }

    private bool AddTastyGoCreditToReffer(long OrderID)
    {
        bool bStatus = false;

        try
        {
            BLLMemberUsedGiftCards objUsedCard = new BLLMemberUsedGiftCards();
            //Add $10 Credit into the User Account
            objUsedCard.remainAmount = float.Parse("10.00");
            objUsedCard.orderId = OrderID;
            DataTable dtOrder = objUsedCard.getTastyCreditsByOrderID();
            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                if (objUsedCard.updateCreditByOrderId() == -1)
                {
                    bStatus = true;
                    SendEmail(dtOrder.Rows[0]["createdBy"].ToString(), "10");
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    protected void SendEmail(string strUserID, string strAmount)
    {
        try
        {

            BLLUser obj = new BLLUser();
            obj.userId = Convert.ToInt32(strUserID);
            DataTable dtUser = obj.getUserByID();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
                string toAddress = dtUser.Rows[0]["userName"].ToString().Trim();
                string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string Subject = "You have received $" + strAmount + " tasty credits from Tazzling.Com";

                mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
                mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                mailBody.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
                mailBody.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                mailBody.Append("<strong>Dear " + dtUser.Rows[0]["firstname"].ToString().Trim() + " " + dtUser.Rows[0]["lastname"].ToString().Trim() + ",</strong></div>");
                mailBody.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received $" + strAmount + " tasty credit from Tazzling.com.</strong></div>");
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Use them today on Tazzling.Com towards our amazing deals!</div>");
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>How to apply my Tasty Credits?</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1.	Login Tazzling.com</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2.	Choose the deal you want to purchase, on the checkout page,</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3.	Enter the amount of credits you want to apply</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>4.	Complete the checkout!</div>");


                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");
                mailBody.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
                mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
                mailBody.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
                mailBody.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;");
                mailBody.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
                mailBody.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
                Misc.SendEmail(toAddress, "", "", fromAddress, Subject, mailBody.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private bool ChkDealOrderFirstOrderOfUser(int iUserId)
    {
        bool bStatus = false;

        try
        {
            BLLDealOrders objBLLDealOrders = new BLLDealOrders();

            objBLLDealOrders.userId = iUserId;

            DataTable dtDealOrderCount = objBLLDealOrders.getDealOrdersCountByUserId();

            if ((dtDealOrderCount != null) && (dtDealOrderCount.Rows.Count > 0))
            {
                if (int.Parse(dtDealOrderCount.Rows[0][0].ToString().Trim()) == 0)
                {
                    //Means that its the first Order
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return bStatus;
    }

    private long GetRefferalIdByUserID(int iUserId)
    {
        long lRefId = 0;

        try
        {
            BLLUser objBLLUser = new BLLUser();

            objBLLUser.userId = iUserId;

            DataTable dtUserInfo = objBLLUser.getUserByID();

            if ((dtUserInfo != null) && (dtUserInfo.Rows.Count > 0))
            {
                lRefId = dtUserInfo.Rows[0]["friendsReferralId"] == DBNull.Value ? 0 : dtUserInfo.Rows[0]["friendsReferralId"].ToString().Trim().Length > 0 ? long.Parse(dtUserInfo.Rows[0]["friendsReferralId"].ToString().Trim()) : 0;
            }
        }
        catch (Exception Ex)
        {
            string strException = Ex.Message;
        }

        return lRefId;
    }

    protected void sendOrderToProcess(long OrderID)
    {
        try
        {
            GECEncryption objEnc = new GECEncryption();
            objBLLDealOrders = new BLLDealOrders();
            objBLLDealOrders.dOrderID = OrderID;
            DataTable dtorders = objBLLDealOrders.getDealOrderDetailByOrderID();
           // float fTastyCredit = 0;
            if (dtorders != null && dtorders.Rows.Count > 0)
            {
                try
                {

                    objBLLDealOrders.status = "Successful";

                    Misc.createPDF(OrderID.ToString());


                    if (!AddTastyGoCreditToReffer(Convert.ToInt64(OrderID.ToString())))
                    {                                                
                        CalculateAffiliateCommissionByOrderId(int.Parse(OrderID.ToString()));
                    }
                    
                    

                    objBLLDealOrders.changeDealOrderStatus();

                    try
                    {
                        string strOrderEmailBody = RenderOrderDetailHTML(OrderID.ToString());
                        Misc.SendEmail(dtorders.Rows[0]["username"].ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), ConfigurationManager.AppSettings["EmailSubjectForNewOrderForMember"].ToString().Trim(), strOrderEmailBody);
                    }
                    catch (Exception ex)
                    { }

                    TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                    GetAllBusinessInfoAndFillGrid(Convert.ToInt32(txtPage.Text) - 1);
                    


                    lblMessage.Visible = true;
                    lblMessage.Text = "Payment made successfully";
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;

                }
                catch (Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/admin/images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private bool checkAffiliateDeductionByOrderId()
    {
        bool bStatus = false;

        try
        { }
        catch (Exception ex)
        { }

        return bStatus;
    }

    private bool CalculateAffiliateCommissionByOrderId(int OrderID)
    {
        bool bStatus = false;

        try
        {
            DataTable dtAdmin = (DataTable)Session["user"];

            DataTable dtAffiliateCommInfo = null;

            BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();

            objBLLAffiliatePartnerGained.OrderId = OrderID;

            dtAffiliateCommInfo = objBLLAffiliatePartnerGained.getGetAffiliatePartnerCommisionInfoByOrderID();

            if ((dtAffiliateCommInfo != null) && (dtAffiliateCommInfo.Rows.Count > 0))
            {
                string strTotalAmount = dtAffiliateCommInfo.Rows[0]["totalAmt"] == DBNull.Value ? "0" : dtAffiliateCommInfo.Rows[0]["totalAmt"].ToString().Trim();
                string strAffComm = dtAffiliateCommInfo.Rows[0]["affCommPer"] == DBNull.Value ? "0" : dtAffiliateCommInfo.Rows[0]["affCommPer"].ToString().Trim();

                objBLLAffiliatePartnerGained.GainedType = "Confirmed";

                //Add $1.85 % Amount of Total Amount into the User Account
                objBLLAffiliatePartnerGained.GainedAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm)/100));
                objBLLAffiliatePartnerGained.RemainAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm)/100));

                objBLLAffiliatePartnerGained.ModifiedBy = Convert.ToInt32(dtAdmin.Rows[0]["userId"].ToString());

                objBLLAffiliatePartnerGained.OrderId = int.Parse(OrderID.ToString());

                if (objBLLAffiliatePartnerGained.updateAffiliateCommisionByOrderId() == -1)
                {
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        { }

        return bStatus;
    }

    private bool CancelDeclinedAffiliateCommissionByOrderId(int OrderID,string strGainedType)
    {
        bool bStatus = false;

        try
        {
            DataTable dtAdmin = (DataTable)Session["user"];

            DataTable dtAffiliateCommInfo = null;

            BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();

            objBLLAffiliatePartnerGained.OrderId = OrderID;

            dtAffiliateCommInfo = objBLLAffiliatePartnerGained.getGetAffiliatePartnerCommisionInfoByOrderID();

            if ((dtAffiliateCommInfo != null) && (dtAffiliateCommInfo.Rows.Count > 0) && (dtAffiliateCommInfo.Rows[0]["gainedType"].ToString().Trim() == "Confirmed"))
            {

                //objBLLAffiliatePartnerGained.GainedType = "Cancelled";
                objBLLAffiliatePartnerGained.GainedType = strGainedType;

                //Add $1.85 % Amount of Total Amount into the User Account
                //objBLLAffiliatePartnerGained.GainedAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.GainedAmount = -1* Convert.ToDouble(dtAffiliateCommInfo.Rows[0]["gainedAmount"].ToString());
                //objBLLAffiliatePartnerGained.RemainAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.RemainAmount = -1 * Convert.ToDouble(dtAffiliateCommInfo.Rows[0]["gainedAmount"].ToString());

                objBLLAffiliatePartnerGained.ModifiedBy = Convert.ToInt32(dtAdmin.Rows[0]["userId"].ToString());

                objBLLAffiliatePartnerGained.FromId = Convert.ToInt32(dtAffiliateCommInfo.Rows[0]["fromId"].ToString());
                objBLLAffiliatePartnerGained.CreatedBy = Convert.ToInt32(dtAffiliateCommInfo.Rows[0]["fromId"].ToString());
                objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(dtAffiliateCommInfo.Rows[0]["UserId"].ToString());
                objBLLAffiliatePartnerGained.AffCommPer = float.Parse(dtAffiliateCommInfo.Rows[0]["AffCommPer"].ToString());
                objBLLAffiliatePartnerGained.OrderId = int.Parse(OrderID.ToString());

                if (objBLLAffiliatePartnerGained.createAffiliatePartnerGained() == -1)
                {
                    bStatus = true;
                }
            }
           
        }
        catch (Exception ex)
        { }

        return bStatus;
    }

    private bool AddAffiliateCommissionToReffer(int fromID, long OrderID, string strTotalAmount, string strAffComm)
    {
        bool bStatus = false;

        try
        {
            BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();

            DataTable dtAdmin = (DataTable)Session["user"];

            long lCommissionerId = GetAffiliateCommissionerIdByUserID(fromID);

            if (lCommissionerId != 0)
            {
                objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(lCommissionerId.ToString());
                objBLLAffiliatePartnerGained.GainedType = "Confirmed";

                //Add $1.85 % Amount of Total Amount into the User Account
                objBLLAffiliatePartnerGained.GainedAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm)));
                objBLLAffiliatePartnerGained.RemainAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm)));

                objBLLAffiliatePartnerGained.FromId = fromID;
                objBLLAffiliatePartnerGained.CreatedBy = Convert.ToInt32(dtAdmin.Rows[0]["userId"].ToString());

                objBLLAffiliatePartnerGained.OrderId = int.Parse(OrderID.ToString());

                //Set the Affiliate Commission
                objBLLAffiliatePartnerGained.AffCommPer = float.Parse(strAffComm);

                if (objBLLAffiliatePartnerGained.createAffiliatePartnerGained() == -1)
                {
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    private long GetAffiliateCommissionerIdByUserID(int iUserId)
    {
        long lCommissionerId = 0;

        try
        {
            BLLUser objBLLUser = new BLLUser();

            objBLLUser.userId = iUserId;

            DataTable dtUserInfo = objBLLUser.getUserByID();

            if ((dtUserInfo != null) && (dtUserInfo.Rows.Count > 0))
            {
                if ((dtUserInfo.Rows[0]["affComID"] != DBNull.Value) && (dtUserInfo.Rows[0]["affComID"].ToString().Trim().Length > 0))
                {
                    if ((dtUserInfo.Rows[0]["affComEndDate"] != DBNull.Value) && (DateTime.Parse(dtUserInfo.Rows[0]["affComEndDate"].ToString().Trim()) > DateTime.Now))
                        lCommissionerId = dtUserInfo.Rows[0]["affComID"] == DBNull.Value ? 0 : dtUserInfo.Rows[0]["affComID"].ToString().Trim().Length > 0 ? long.Parse(dtUserInfo.Rows[0]["affComID"].ToString().Trim()) : 0;
                }
            }
        }
        catch (Exception Ex)
        {
            string strException = Ex.Message;
        }

        return lCommissionerId;
    }

    private void reverseFoodAmount(float amount, int iUserId)
    {
        float remAmount = 0;
        float totalAmount = 0;
        float updatedAmount = amount;
        try
        {
            BLLMemberUsedGiftCards objUseableCard = new BLLMemberUsedGiftCards();
            objUseableCard.createdBy = iUserId;
            DataTable dt = objUseableCard.getAllRefferalTastyCreditsByUserID();

            //Get the Admin User ID
            int iAdminUserId = 0;
            if (Session["user"] != null)
            {
                DataTable dtUser = (DataTable)Session["user"];
                if ((dtUser != null) && (dtUser.Rows.Count > 0))
                {
                    iAdminUserId = int.Parse(dtUser.Rows[0]["userID"].ToString());
                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; (i + 1) > 0; i--)
                {
                    remAmount = float.Parse(dt.Rows[i]["remainAmount"].ToString());
                    totalAmount = float.Parse(dt.Rows[i]["gainedAmount"].ToString());
                    updatedAmount += remAmount;
                    if (updatedAmount <= totalAmount)
                    {
                        objUseableCard.gainedId = Convert.ToInt64(dt.Rows[i]["gainedId"].ToString());
                        objUseableCard.modifiedBy = iAdminUserId;
                        objUseableCard.remainAmount = updatedAmount;
                        objUseableCard.updateRemainingUsableAmount();
                        break;
                    }
                    else
                    {
                        objUseableCard.gainedId = Convert.ToInt64(dt.Rows[i]["gainedId"].ToString());
                        objUseableCard.modifiedBy = iAdminUserId;
                        objUseableCard.remainAmount = totalAmount;
                        objUseableCard.updateRemainingUsableAmount();
                        updatedAmount = updatedAmount - totalAmount;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    private bool RefundAffiliatePartnerCredits(long OrderID)
    {
        bool bStatus = false;

        try
        {
            int iaffiliatePartnerId = 0;

            BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
            objBLLAffiliatePartnerGained.OrderId = int.Parse(OrderID.ToString());
            DataTable dtCommision = objBLLAffiliatePartnerGained.getGetAffiliatePartnerCommisionInfoByOrderID();

            //True Means that commision has been deducted against this order & if yes then we will roll back that commision
            if ((dtCommision != null) && (dtCommision.Rows.Count > 0))
            {
                float amountGained = float.Parse(dtCommision.Rows[0]["gainedAmount"].ToString());
                float amountRemain = float.Parse(dtCommision.Rows[0]["RemainAmount"].ToString());

                iaffiliatePartnerId = int.Parse(dtCommision.Rows[0]["affiliatePartnerId"].ToString());

                //Get the Admin User ID
                int iAdminUserId = 0;
                if (Session["user"] != null)
                {
                    DataTable dtUser = (DataTable)Session["user"];
                    if ((dtUser != null) && (dtUser.Rows.Count > 0))
                    {
                        iAdminUserId = int.Parse(dtUser.Rows[0]["userID"].ToString());
                    }
                }

                objBLLAffiliatePartnerGained.AffiliatePartnerId = iaffiliatePartnerId;

                objBLLAffiliatePartnerGained.RemainAmount = (amountRemain - amountGained);

                objBLLAffiliatePartnerGained.ModifiedBy = iAdminUserId;

                int iChk = objBLLAffiliatePartnerGained.updateAffiliateRemainingUsableAmount();

                if (iChk != 0)
                    bStatus = true;
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    public string RenderOrderDetailHTML(string OrderID)
    {
        DataTable dtOrdersInfo;
        BLLDealOrders objOrders = new BLLDealOrders();
        GECEncryption objEnc = new GECEncryption();
        objOrders.dOrderID = Convert.ToInt64(OrderID);
        dtOrdersInfo = objOrders.getDealOrderDetailByOrderID();
        StringBuilder sb = new StringBuilder();
        if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
        {
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "User" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString()) + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Thank you for purchasing this amazing deal on Tazzling.com.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>The deal is on and we are all excited to get this deal!</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>How to Redeem This Deal:</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1. Login onto Tazzling.com</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2. Click the member Area on the top right corner.</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3. Click your deal, and print out the voucher.</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>4. Or go green! Use your smart phone!</div>");
            sb.Append("<div style='margin: 0px 0px 15px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>5. Wait until the deal ends, and redeem them on location!</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your purchase receipt as follow:</div>");
            sb.Append("<div style='margin: 0px 60px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><strong>Tastygo Online Inc. Invoice</strong></div>");
            sb.Append("<table style='margin: 0px 0px 5px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><tr><td style='width: 90px;'><strong>Deal Title:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["title"] == null ? "" : dtOrdersInfo.Rows[0]["title"].ToString());
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Status:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["status"] == null ? "" : dtOrdersInfo.Rows[0]["status"].ToString());
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Date & Time:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["createdDate"] == null ? "" : dtOrdersInfo.Rows[0]["createdDate"].ToString());
            sb.Append("</td></tr>");
            if (dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString().Trim() != "0")
            {
                sb.Append("<tr><td style='float: left; width: 90px;'><strong>Card Number:</strong></td>");
                sb.Append("<td>");
                sb.Append(dtOrdersInfo.Rows[0]["ccInfoNumber"] == null ? "" : "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4));
                sb.Append("</td></tr>");
            }
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Billing Name:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString()));
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Province/State:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["ccInfoBProvince"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBProvince"].ToString().Trim());
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Billing City:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["ccInfoBCity"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBCity"].ToString().Trim());
            sb.Append("</td></tr></table>");
            sb.Append("<div style='padding: 15px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 16px; line-height: 1.4em; clear: both;'><strong>Deal Detail</strong></div>");
            sb.Append("<table style='margin: 0px 10px 10px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; clear: both;'><tr><td style='float: left; width: 300px;'><strong>Deal Title</strong></td><td style='float: left; width: 100px;'><strong>Quantity</strong></td><td style='float: left; width: 100px;'><strong>Unit Price</strong></td><td style='float: left; width: 100px;'><strong>Total</strong></td></tr>");
            if (dtOrdersInfo.Rows[0]["personalQty"] != null && Convert.ToInt32(dtOrdersInfo.Rows[0]["personalQty"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>");
                sb.Append(dtOrdersInfo.Rows[0]["title"].ToString());
                sb.Append("</td><td style='float: left; width: 100px;'>");
                sb.Append(dtOrdersInfo.Rows[0]["personalQty"].ToString());
                sb.Append("</td><td style='float: left; width: 100px;'>$");
                sb.Append(dtOrdersInfo.Rows[0]["sellingPrice"].ToString());
                sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                sb.Append(Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["personalQty"].ToString())).ToString("###.00"));
                sb.Append(" CAD</strong></td></tr>");
            }
            if (dtOrdersInfo.Rows[0]["giftQty"] != null && Convert.ToInt32(dtOrdersInfo.Rows[0]["giftQty"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>");
                sb.Append("Deals for gift.");
                sb.Append("</td><td style='float: left; width: 100px;'>");
                sb.Append(dtOrdersInfo.Rows[0]["giftQty"].ToString());
                sb.Append("</td><td style='float: left; width: 100px;'>$");
                sb.Append(dtOrdersInfo.Rows[0]["sellingPrice"].ToString());
                sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                sb.Append(Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["giftQty"].ToString())).ToString("###.00"));
                sb.Append(" CAD</strong></td></tr>");
            }
            sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;</td></tr>");
            sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Total</strong></td><td style='float: left; width: 100px;'>");
            sb.Append("<strong>$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["Qty"].ToString())).ToString("###.00") + " CAD</strong></td></tr>");

            if (dtOrdersInfo.Rows[0]["shippingAndTaxAmount"] != null
                && dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim() != ""
                && Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Shipping & Tax</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["Qty"].ToString())).ToString("###.00") + " CAD</strong></td></tr>");
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Grand Total</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + Convert.ToDouble((Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim()) + (Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()))) * Convert.ToDouble(dtOrdersInfo.Rows[0]["Qty"].ToString().Trim())).ToString("###.00") + " CAD</strong></td></tr>");
            }

            if (dtOrdersInfo.Rows[0]["tastyCreditUsed"] != null && dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px; padding-left:50px;'><strong>Charge From Tasty Credit</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString() + " CAD</strong></td></tr>");
            }
            if (dtOrdersInfo.Rows[0]["ccCreditUsed"] != null && dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px;padding-left:50px;'><strong>Charge From Credit Card</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString() + " CAD</strong></td></tr>");
            }
            if (dtOrdersInfo.Rows[0]["comissionMoneyUsed"] != null && dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px; padding-left:50px;'><strong>Charge From Commission</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString() + " CAD</strong></td></tr>");
            }

            sb.Append("</table>");
            //sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;<td></tr>");
            // sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Balance</strong></td><td style='float: left; width: 100px;'><strong>$0 CAD</strong></td></tr></table>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'>If you bought this deal as gift, please login to <a href='http://www.tazzling.com'>www.tazzling.com</a>. Click Member area, then Send gift. You will be able to print off the voucher as gift!<div>");

            sb.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>Don’t forget:</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>-	Refer friends, earn $10 when your friends orders with you! <a href='http://www.tazzling.com/member_referral.aspx' target='_blank'>Click here</a></div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>-	Buy the deal for a friend! If the deal is not on yet, tell them about it so we can all get it!</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>-	Click \"like\" on our <a href='http://www.facebook.com/tastygo' target='_blank'>facebook</a> or \"follow us\" on <a href='http://www.twitter.com/tastygovan' target='_blank'>twitter</a> to win awesome prizes</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Want us to negotiate a deal for you? <a href='http://www.tazzling.com/suggestBusiness.aspx' target='_blanck'>Suggest a business here</a></div>");
            sb.Append("<div style='margin: 0px 0px 15px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Tastygo provides 30 days buyer's protection. <a href='http://www.tazzling.com/faq.aspx' target='_blanck'>Click Here?</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");

            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");

        }
        return sb.ToString();
    }

    public string RenderOrderDetailHTMLForRefund(string OrderID)
    {
        DataTable dtOrdersInfo;
        BLLDealOrders objOrders = new BLLDealOrders();
        GECEncryption objEnc = new GECEncryption();
        objOrders.dOrderID = Convert.ToInt64(OrderID);
        dtOrdersInfo = objOrders.getDealOrderDetailByOrderID();
        StringBuilder sb = new StringBuilder();
        if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
        {
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Your Order Status has changed!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + (dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "User" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString())) + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Your order status has changed. Details are shown bellows:</strong></div>");                                   
            sb.Append("<div style='margin: 0px 60px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><strong>Tastygo Online Inc.</strong></div>");
            sb.Append("<table style='margin: 0px 0px 5px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><tr><td style='width: 90px;'><strong>Deal Title:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["title"] == null ? "" : dtOrdersInfo.Rows[0]["title"].ToString());
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Status:</strong></td>");
            sb.Append("<td>");
            //sb.Append(dtOrdersInfo.Rows[0]["status"] == null ? "" : dtOrdersInfo.Rows[0]["status"].ToString());
            sb.Append("Cancelled");
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Date & Time:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["modifiedDate"] == null ? "" : dtOrdersInfo.Rows[0]["modifiedDate"].ToString());
            sb.Append("</td></tr>");
            if (dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString().Trim() != "0")
            {
                sb.Append("<tr><td style='float: left; width: 90px;'><strong>Card Number:</strong></td>");
                sb.Append("<td>");
                sb.Append(dtOrdersInfo.Rows[0]["ccInfoNumber"] == null ? "" : "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4));
                sb.Append("</td></tr>");
            }
            sb.Append("<tr><td style='float: left; width: 90px;'><strong>Name:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString()));
            sb.Append("</td></tr></table>");
            sb.Append("<div style='padding: 15px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 16px; line-height: 1.4em; clear: both;'><strong>Deal Detail</strong></div>");
            sb.Append("<table style='margin: 0px 10px 10px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; clear: both;'><tr><td style='float: left; width: 300px;'><strong>Deal Title</strong></td><td style='float: left; width: 100px;'><strong>Quantity</strong></td><td style='float: left; width: 100px;'><strong>Unit Price</strong></td><td style='float: left; width: 100px;'><strong>Total</strong></td></tr>");
            if (dtOrdersInfo.Rows[0]["personalQty"] != null && Convert.ToInt32(dtOrdersInfo.Rows[0]["personalQty"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>");
                sb.Append(dtOrdersInfo.Rows[0]["title"].ToString());
                sb.Append("</td><td style='float: left; width: 100px;'>");
                sb.Append(dtOrdersInfo.Rows[0]["personalQty"].ToString());
                sb.Append("</td><td style='float: left; width: 100px;'>$");
                sb.Append(dtOrdersInfo.Rows[0]["sellingPrice"].ToString());
                sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                sb.Append(Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["personalQty"].ToString())).ToString("###.00"));
                sb.Append(" CAD</strong></td></tr>");
            }
            if (dtOrdersInfo.Rows[0]["giftQty"] != null && Convert.ToInt32(dtOrdersInfo.Rows[0]["giftQty"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>");
                sb.Append("Deals for gift.");
                sb.Append("</td><td style='float: left; width: 100px;'>");
                sb.Append(dtOrdersInfo.Rows[0]["giftQty"].ToString());
                sb.Append("</td><td style='float: left; width: 100px;'>$");
                sb.Append(dtOrdersInfo.Rows[0]["sellingPrice"].ToString());
                sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                sb.Append(Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["giftQty"].ToString())).ToString("###.00"));
                sb.Append(" CAD</strong></td></tr>");
            }
            sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;</td></tr>");
            sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Total</strong></td><td style='float: left; width: 100px;'>");
            sb.Append("<strong>$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["Qty"].ToString())).ToString("###.00") + " CAD</strong></td></tr>");

            if (dtOrdersInfo.Rows[0]["shippingAndTaxAmount"] != null
                && dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim() != ""
                && Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Shipping & Tax</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["Qty"].ToString())).ToString("###.00") + " CAD</strong></td></tr>");
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Grand Total</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + Convert.ToDouble((Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString().Trim()) + (Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()))) * Convert.ToDouble(dtOrdersInfo.Rows[0]["Qty"].ToString().Trim())).ToString("###.00") + " CAD</strong></td></tr>");
            }
            if (dtOrdersInfo.Rows[0]["tastyCreditUsed"] != null && dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px; padding-left:50px;'><strong>Tasty Credit Returned</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString() + " CAD</strong></td></tr>");
            }
            if (dtOrdersInfo.Rows[0]["ccCreditUsed"] != null && dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px;padding-left:50px;'><strong>Credit Card Returned</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString() + " CAD</strong></td></tr>");
            }
            if (dtOrdersInfo.Rows[0]["comissionMoneyUsed"] != null && dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString()) > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px; padding-left:50px;'><strong>Commission Returned</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString() + " CAD</strong></td></tr>");
            }

            sb.Append("</table>");
            //sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;<td></tr>");
            // sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Balance</strong></td><td style='float: left; width: 100px;'><strong>$0 CAD</strong></td></tr></table>");                              
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact us at <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");

            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");

        }
        return sb.ToString();
    }

}
