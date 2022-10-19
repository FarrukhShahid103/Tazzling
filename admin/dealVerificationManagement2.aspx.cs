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

public partial class dealVerificationManagement2 : System.Web.UI.Page
{
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public bool displayPrevious = false;
    public bool displayNext = true;
    public string strIDs = "";
    public int start = 2;
    BLLDealOrders objOrders = new BLLDealOrders();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            //Get the Admin User Session here
            if (Session["user"] != null)
            {
                SearchhDealInfoByDifferentParams(0);
            }
            else
            {
                Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
            }
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    protected void gvViewDeals_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            int intPageCount = 0;
            TextBox txtPage = (TextBox)gvViewDeals.BottomPagerRow.Cells[0].FindControl("txtPage");
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
            this.SearchhDealInfoByDifferentParams(intCurrentPage-1);
            txtPage = (TextBox)gvViewDeals.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (intCurrentPage).ToString();            
            
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

    #region Event of dropdown to take to selected page
    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            gvViewDeals.PageIndex = 0;
            DropDownList ddlPage = (DropDownList)gvViewDeals.BottomPagerRow.Cells[0].FindControl("ddlPage");
            Session["ddlPage"] = ddlPage.SelectedValue.ToString();
            setPageValueInCookie(ddlPage);
            this.SearchhDealInfoByDifferentParams(0);
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

    #region Event to take to required page

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            TextBox txtPage = (TextBox)gvViewDeals.BottomPagerRow.Cells[0].FindControl("txtPage");

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
                this.SearchhDealInfoByDifferentParams(intCurrentPage - 1);
                txtPage = (TextBox)gvViewDeals.BottomPagerRow.Cells[0].FindControl("txtPage");
                txtPage.Text = intCurrentPage.ToString();
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            gvViewDeals.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }   
    #endregion

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

    private void GetAndSetUserID()
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];

            if ((dtUser != null) && (dtUser.Rows.Count > 0))
            {
                ViewState["userID"] = dtUser.Rows[0]["userID"];
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void gvViewDeals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            //priority

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // find the label control

                Label lblPreDealVerification = (Label)e.Row.FindControl("lblPreDealVerification");
                Label lblPostDealVerification = (Label)e.Row.FindControl("lblPostDealVerification");

                // read the value from the datasoure

                if ((lblPreDealVerification.Text.Trim() == "0% Data Entered" || lblPreDealVerification.Text.Trim() == "20% Fine Print Verified" || lblPreDealVerification.Text.Trim() == "40% Date Verified") && (lblPostDealVerification.Text.Trim() == "0% Deal Ended"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#348781");
                }
                else if (lblPreDealVerification.Text.Trim() == "60% Design Verified" && lblPostDealVerification.Text.Trim() == "0% Deal Ended")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#7D1B7E");
                }
                else if ((lblPreDealVerification.Text.Trim() == "80% Pregen List sent, email confirmed" || lblPreDealVerification.Text.Trim() == "100% Called Business, All Staff ready for deal") && (lblPostDealVerification.Text.Trim() == "0% Deal Ended"))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                }
                else if (lblPostDealVerification.Text.Trim() == "0% Deal Ended" || lblPostDealVerification.Text.Trim() == "50% Email Notification, sent customer's List")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#1569C7");
                }
                else if (lblPostDealVerification.Text.Trim() == "75% Phone Notification")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#8D38C9");
                }
                else if (lblPostDealVerification.Text.Trim() == "100% Confirmed Received Customer List")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FF00");
                }

                

                //if (lblPreDealVerification.Text.Trim() == "0% Data Entered" && lblPostDealVerification.Text.Trim() == "0% Deal Ended")
                //{
                //    e.Row.BackColor = System.Drawing.Color.Purple;
                //}
                //else if (lblPreDealVerification.Text.Trim() == "100% Called Business, All Staff ready for deal" && lblPostDealVerification.Text.Trim() == "100% Confirmed Received Customer List")
                //{
                //    e.Row.BackColor = System.Drawing.Color.YellowGreen;
                //}
                //else
                //{
                //    //e.Row.BackColor = System.Drawing.Color.Yellow;
                //}

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

    #region "Get All Deal Info & Fill the GridView"

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

    #endregion

    bool searchClick = false;
    
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            searchClick = true;
            SearchhDealInfoByDifferentParams(0);
        }
        catch (Exception ex)
        { }
    }

    protected bool getDetailStatus(object status)
    {
        try
        {
            if (status.ToString() != "")
            {
                if (Convert.ToInt32(status.ToString()) > 0)
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
        catch (Exception ex)
        {
            return false;
        }
    }

    protected void gvViewDeals_Login(object sender, GridViewCommandEventArgs e)
    {

        try
        {
           
            if (e.CommandName.Trim() == "EditDetail")
            {
                ResetControls();
                pnlDetail.Visible = true;
                pnlForm.Visible = false;
                GetAndShowDealInfoByDealId(e.CommandArgument.ToString());
                hfBusinessId.Value = e.CommandArgument.ToString();
                lblCommentMessage.Text = "";
                GetAndSetPostsByDealId(Convert.ToInt32(hfBusinessId.Value));
                SetFeilds(0);
            }
        }
        catch (Exception ex)
        { }
    }

    private void ResetControls()
    {
        ddlPostDealVerification.SelectedIndex = 0;
        ddlPreDealVerification.SelectedIndex = 0;
        lblAlternateEmailAddress.Text = "";
        lblBusinessEmailAddress.Text = "";
        lblBusinessName.Text = "";
        lblBusinessName.Text = "";
        lblOwnerFirstName.Text = "";
        lblOwnerLastName.Text = "";
        lblCellNumber.Text = "";
        lblDealModifyBy.Text = "";
        lblDealModifyTime.Text = "";
        lblDealName.Text = "";
        lblPhoneNumber.Text = "";
    }

    private void GetAndShowDealInfoByDealId(string strIDs)
    {
        try
        {
            BLLUpcommingDeals objBLLDeals = new BLLUpcommingDeals();

            //Set the Deal Id here            

            objBLLDeals.updealId = Convert.ToInt64(strIDs);
            DataTable dtDeals = objBLLDeals.getupCommingDealForDealId();

            if ((dtDeals != null) && (dtDeals.Rows.Count > 0))
            {
                lblBusinessName.Text = dtDeals.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblAlternateEmailAddress.Text = dtDeals.Rows[0]["alternativeEmail"].ToString().Trim();
                lblBusinessEmailAddress.Text = dtDeals.Rows[0]["email"].ToString().Trim();
                lblCellNumber.Text = dtDeals.Rows[0]["cellNumber"].ToString().Trim();
                lblPhoneNumber.Text = dtDeals.Rows[0]["phone"].ToString().Trim();
                try
                {
                    lblDealModifyBy.Text = dtDeals.Rows[0]["userName"].ToString().Trim();
                    lblDealModifyTime.Text = Convert.ToDateTime(dtDeals.Rows[0]["modifiedDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                { }
                //hfDealID.Value = dtDeals.Rows[0]["dealId"].ToString().Trim();
                lblBusinessName.Text = dtDeals.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblOwnerFirstName.Text = dtDeals.Rows[0]["firstName"].ToString().Trim();
                lblOwnerLastName.Text = dtDeals.Rows[0]["lastName"].ToString().Trim();

                hfBusinessId.Value = dtDeals.Rows[0]["updealId"].ToString().Trim();
                lblDealName.Text = dtDeals.Rows[0]["title"].ToString().Trim();
                if (dtDeals.Rows[0]["preDealVerification"] != null && dtDeals.Rows[0]["preDealVerification"].ToString().Trim() != "")
                {
                    try
                    {
                        ddlPreDealVerification.SelectedValue = dtDeals.Rows[0]["preDealVerification"].ToString().Trim();
                    }
                    catch (Exception ex)
                    { }
                }
                if (dtDeals.Rows[0]["postDealVerification"] != null && dtDeals.Rows[0]["postDealVerification"].ToString().Trim() != "")
                {
                    try
                    {
                        ddlPostDealVerification.SelectedValue = dtDeals.Rows[0]["postDealVerification"].ToString().Trim();
                    }
                    catch (Exception ex)
                    { }
                }

            }

        }
        catch (Exception ex)
        {

        }
    }

    private DataTable SearchhDealInfoByDifferentParams(string strDealID)
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";
            strQuery += " ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber'";
            strQuery += " ,[dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + (case when (len(rtrim(userInfo.lastName))>1) then upper(substring(rtrim(userInfo.lastName),1,1)) else '' end )as 'Name'";
            strQuery += " ,orderNo";
            strQuery += " ,dealOrders.createdDate";
            strQuery += " ,[dealOrders].[status]";
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " ,[dealOrderDetail].[detailID]";
            strQuery += " ,[dealOrderDetail].[receiverEmail]";
            strQuery += " ,[dealOrderDetail].[isRedeemed]";
            strQuery += " ,[dealOrderDetail].[redeemedDate]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";
            strQuery += " ,[dealOrderDetail].[isGift]";
            strQuery += " ,[dealOrderDetail].[markUsed]";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join deals on (deals.dealId = dealOrders.dealId)";
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            strQuery += " where ";
            strQuery += " dealOrders.dealId =" + strDealID.Trim() + " and [dealOrders].[status]='Successful'";
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

    protected void btnImgSave_Click(object sender, ImageClickEventArgs e)
    {

        if (hfBusinessId.Value.Trim() != "")
        {
            BLLUpcommingDeals objuDeals = new BLLUpcommingDeals();
            objuDeals.updealId = Convert.ToInt64(hfBusinessId.Value);
            objuDeals.postDealVerification = ddlPostDealVerification.SelectedValue.ToString().Trim();
            objuDeals.preDealVerification = ddlPreDealVerification.SelectedValue.Trim();
            objuDeals.modifiedBy = Convert.ToInt64(ViewState["userID"]);
            objuDeals.updateUpcommingDeals();
            SearchhDealInfoByDifferentParams(0);
            lblMessage.Text = "Deal verification information has updated successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/checked.png";
            pnlDetail.Visible = false;
            pnlForm.Visible = true;
        }

      
    }

    protected void btnImgCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlForm.Visible = true;
            pnlDetail.Visible = false;
        }
        catch (Exception ex)
        {

        }
      
    }

    protected void gvViewDeals_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            BLLUpcommingDeals obj = new BLLUpcommingDeals();
            obj.updealId = Convert.ToInt32(gvViewDeals.DataKeys[e.RowIndex].Value.ToString());
                if (obj.deleteUpcommingDeals() == -1)
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

            SearchhDealInfoByDifferentParams(0);
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlDetail.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

    }

    private DataSet SearchQuery(string strRowIndex)
    {
        DataSet dstDeals = null;
        string strQuery = "";
        try
        {
            strQuery = " SELECT * from(";
            strQuery += " SELECT ";
            strQuery += " [upcommingDeals].[updealId]";
            strQuery += ",[upcommingDeals].[restaurantId]";
            strQuery += ",[upcommingDeals].[title]";
            strQuery += ",[restaurant].[email]";
            strQuery += ",[restaurant].[phone]";
            strQuery += ",[upcommingDeals].[preDealVerification]";
            strQuery += ",[upcommingDeals].[postDealVerification]";
            strQuery += ",[restaurant].[restaurantBusinessName]";
            strQuery += ",ROW_NUMBER() OVER(ORDER BY updealId desc) as RowNum";
            strQuery += " FROM ";
            strQuery += "[upcommingDeals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= upcommingDeals.[restaurantId] ";
            strQuery += "where restaurant.[restaurantId]= upcommingDeals.[restaurantId] ";
            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [upcommingDeals].[title] like '%" + txtSrchDealTitle.Text.Trim().Replace("'", "''") + "%' ";
            }
            strQuery += ") as DerivedTableName";
            int strStartIndex = (Convert.ToInt32(strRowIndex) * gvViewDeals.PageSize) + 1;
            int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * gvViewDeals.PageSize;
            strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;
            strQuery += " order by updealId desc ";

            strQuery += " SELECT 'Return Value' =  COUNT([upcommingDeals].[updealId]) FROM [upcommingDeals]";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= upcommingDeals.[restaurantId] ";
            strQuery += "where restaurant.[restaurantId]= upcommingDeals.[restaurantId] ";


            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [upcommingDeals].[title] like '%" + txtSrchDealTitle.Text.Trim().Replace("'", "''") + "%' ";
            }
            if (searchClick)
            {
                ViewState["Query"] = strQuery;
            }

            dstDeals = Misc.searchDataSet(strQuery);
        }
        catch (Exception ex)
        { }
        return dstDeals;
    }

    private void SearchhDealInfoByDifferentParams(int intPageNumber)
    {
        try
        {
            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                gvViewDeals.PageSize = Convert.ToInt32(Session["ddlPage"]);
            }
            else
            {
                gvViewDeals.PageSize = 20;
                Session["ddlPage"] = 20;
            }
            DataSet dst = null;
            DataTable dtDeals;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                dst = SearchQuery(intPageNumber.ToString());
                dtDeals = dst.Tables[0];
                dv = new DataView(dtDeals);
                if (searchClick)
                {
                    btnShowAll.Visible = true;
                }
                else
                {
                    btnShowAll.Visible = false;
                }
            }
            else
            {
                dst = SearchQuery(intPageNumber.ToString());
                dtDeals = dst.Tables[0];
                dv = new DataView(dtDeals);
                btnShowAll.Visible = true;
            }

            if (dtDeals != null && dtDeals.Rows.Count > 0)
            {
               gvViewDeals.DataSource = dv;
               gvViewDeals.DataBind();

               Label lblPageCount = (Label)gvViewDeals.BottomPagerRow.FindControl("lblPageCount");
               Label lblTotalRecords = (Label)gvViewDeals.BottomPagerRow.FindControl("lblTotalRecords");
                string strTotalOrders = "";
                if (dst != null && dst.Tables[1] != null)
                {
                    strTotalOrders = dst.Tables[1].Rows[0][0].ToString();
                }
                else
                {
                    strTotalOrders = dtDeals.Rows.Count.ToString();
                }
                lblTotalRecords.Text = strTotalOrders;
                int intpageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(strTotalOrders) / gvViewDeals.PageSize));
                lblPageCount.Text = intpageCount.ToString();
                ViewState["PageCount"] = intpageCount.ToString();
                DropDownList ddlPage = bindPageDropDown(strTotalOrders);
                if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = Session["ddlPage"].ToString();
                }
                gvViewDeals.BottomPagerRow.Visible = true;
                if (intpageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)gvViewDeals.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)gvViewDeals.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }
                btnSearch.Enabled = true;                
            }
            else
            {
                gvViewDeals.DataSource = null;
                gvViewDeals.DataBind();                                
                //txtSearchFirstName.Enabled = false;
                //txtSearchLastName.Enabled = false;
                //txtSearchUserName.Enabled = false;
            }

        }
        catch (Exception ex)
        {

            //Show All Deal Info into Grid View here
            this.upItems.Visible = true;
            this.divSrchFields.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnShowAll_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Query"] = null;
        lblMessage.Visible = false;
        imgGridMessage.Visible = false;
        txtSrchDealTitle.Text = "";
        txtSearchBusinessName.Text = "";
        txtSaveBusinessName.Text = "";
        txtSaveDealTitle.Text = "";
        gvViewDeals.PageIndex = 0;
        SearchhDealInfoByDifferentParams(0);       
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtResturant = Misc.search("select restaurantId from restaurant where restaurantBusinessName='" + txtSaveBusinessName.Text.Trim().Replace("'","''") + "'");
            if (dtResturant != null && dtResturant.Rows.Count > 0)
            {
                BLLUpcommingDeals objupcommingdeals = new BLLUpcommingDeals();
                if (ViewState["userID"] != null)
                {
                    objupcommingdeals.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                    objupcommingdeals.modifiedBy = Convert.ToInt64(ViewState["userID"].ToString());
                }
                objupcommingdeals.restaurantId = Convert.ToInt64(dtResturant.Rows[0]["restaurantId"].ToString());
                objupcommingdeals.title = txtSaveDealTitle.Text.Trim();
                objupcommingdeals.createUpcommingDeals();
                SearchhDealInfoByDifferentParams(0);
                lblMessage.Text = "Deal has been saved successful.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/checked.png";
                txtSaveBusinessName.Text = "";
                txtSaveDealTitle.Text = "";
                txtSearchBusinessName.Text = "";
                txtSrchDealTitle.Text = "";
            }
            else
            {
                lblMessage.Text = "Restaurant with name \"" + txtSaveBusinessName.Text.Trim() + "\" does not exist.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private DropDownList bindPageDropDown(string strTotalRecords)
    {
        try
        {
            DropDownList ddlPage = (DropDownList)gvViewDeals.BottomPagerRow.Cells[0].FindControl("ddlPage");

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
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return null;
        }
    }

    #region Discuession Code
    #region "Check user Is Logged In or not"

    private void SetFeilds(int set)
    {
        try
        {
            this.txtComment.Enabled = true;
            this.btnPost.Visible = true;
            this.btnCancel.Visible = true;            
            lblCommentMessage.Text = "";
            //if (set == 1)
            //{
            //    this.txtComment.Enabled = true;

            //    this.btnPost.Visible = true;

            //    this.btnCancel.Visible = true;
            //}
            //else
            //{
            //    ViewState["DiscussionId"] = null;
            //    txtComment.Text = "";
            //    this.txtComment.Enabled = false;

            //    this.btnPost.Visible = false;

            //    this.btnCancel.Visible = false;
            //}
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
            BLLUpcommingDealDiscussion obj = new BLLUpcommingDealDiscussion();
            obj.Comments = txtComment.Text.Trim().Replace("\n", "<br>");
            obj.UpdealId = Convert.ToInt64(hfBusinessId.Value.Trim());
            obj.UserId = Convert.ToInt64(ViewState["userID"].ToString());
            obj.AddNewUpcommingDealDiscussion();
            SetFeilds(0);
            //Get All the Posts here By Deal Id
            GetAndSetPostsByDealId(Convert.ToInt32(this.hfBusinessId.Value));
            lblCommentMessage.Visible = true;
            lblCommentMessage.Text = "Comment save successfully.";            
            txtComment.Text = "";
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

            BLLUpcommingDealDiscussion objBLLUpcommingDealDiscussion = new BLLUpcommingDealDiscussion();

            objBLLUpcommingDealDiscussion.UpdealId = iDealId;

            DataTable dtPosts = objBLLUpcommingDealDiscussion.getUpcommingDealDiscussionByUpdealId();

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

    protected void Edit_Command(Object sender, DataListCommandEventArgs e)
    {
    //    BLLUpcommingDealDiscussion obj = new BLLUpcommingDealDiscussion();
    //    obj.DiscussionId = Convert.ToInt64(e.CommandArgument.ToString());
    //    DataTable dtDiscuess = obj.get();
    //    if (dtDiscuess != null && dtDiscuess.Rows.Count > 0)
    //    {
    //        SetFeilds(1);
    //        ViewState["DiscussionId"] = e.CommandArgument.ToString();
    //        txtComment.Text = dtDiscuess.Rows[0]["comments"].ToString().Replace("<br>", "\n");
    //    }
    }

    protected void Delete_Command(Object sender, DataListCommandEventArgs e)
    {
        //try
        //{
        //    BLLUpcommingDealDiscussion obj = new BLLUpcommingDealDiscussion();
        //    obj.DiscussionId = Convert.ToInt64(e.CommandArgument.ToString());
        //    obj.deleteUpcommingDealDiscussionByDiscussionId();
        //    GetAndSetPostsByDealId(Convert.ToInt32(this.hfBusinessId.Value));

        //    SetFeilds(0);
        //    lblMessage.Visible = true;
        //    lblMessage.Text = "Comment delete successfully.";

        //    imgGridMessage.Visible = true;
        //    imgGridMessage.ImageUrl = "~/Images/checked.png";

        //}
        //catch (Exception ex)
        //{
        //    lblMessage.Visible = true;
        //    lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

        //    imgGridMessage.Visible = true;
        //    imgGridMessage.ImageUrl = "~/Images/error.png";

        //    lblMessage.ForeColor = System.Drawing.Color.Red;
        //}
    }
    #endregion

}