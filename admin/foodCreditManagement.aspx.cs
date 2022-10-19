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
using System.Data.SqlClient;
using SQLHelper;

public partial class admin_foodCreditManagement : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
        
    BLLMemberUsedGiftCards objUsedCard = new BLLMemberUsedGiftCards();
    BLLMemberUsedGiftCards objUseableCard = new BLLMemberUsedGiftCards();
    
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
            bindGrid(0);
        }
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            if (txtSearchFirstName.Text.Trim() != "" || txtSearchLastName.Text.Trim() != "" || txtSearchUserName.Text.Trim() != "" || txtTastyCredit.Text.Trim() != "")
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
            if (txtSearchFirstName.Text.Trim() != "" || txtSearchLastName.Text.Trim() != "" || txtSearchUserName.Text.Trim() != "" || txtTastyCredit.Text.Trim() != "")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;
                /*
                strQuery = "Select * from ( SELECT * FROM dbo.vw_getMemberResturantAndSalesUsers ";
                strQuery += @"where isDeleted = 0 
                            and isActive = 1 
                            and ( userTypeId = 2 
                                  or userTypeId = 3 
                                  or userTypeId = 4 
                                  or userTypeId = 5 
                                  or userTypeId = 6 
                                  or userTypeId = 7 
                                )";
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
                if (txtTastyCredit.Text.Trim() != "")
                {
                    strQuery += " and (isnull((SELECT sum(remainAmount) as remainAmount FROM gained where createdBy=userId and (gainedType = 'Refferal' OR gainedType = 'Gift Card')),0)) " + this.ddlTastyCredit.SelectedValue.ToString() + txtTastyCredit.Text.Trim();
                }
                strQuery += ") as DerivedTableName";
                int strStartIndex = (Convert.ToInt32(strRowIndex) * pageGrid.PageSize) + 1;
                int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * pageGrid.PageSize;
                strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;

                strQuery += " SELECT	'Return Value' =  COUNT(userID) ";
                strQuery += "FROM dbo.vw_getMemberResturantAndSalesUsers ";
                strQuery += @"where isDeleted = 0 
                            and isActive = 1 
                            and ( userTypeId = 2 
                                  or userTypeId = 3 
                                  or userTypeId = 4 
                                  or userTypeId = 5 
                                  or userTypeId = 6 
                                  or userTypeId = 7 
                                )";
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
                if (txtTastyCredit.Text.Trim() != "")
                {
                    strQuery += " and (isnull((SELECT sum(remainAmount) as remainAmount FROM gained where createdBy=userId and (gainedType = 'Refferal' OR gainedType = 'Gift Card')),0)) " + this.ddlTastyCredit.SelectedValue.ToString() + txtTastyCredit.Text.Trim();
                }
                 */ 
                strQuery = "Select * from ( SELECT ";
                strQuery += " userID, ";
                strQuery += " tblUserType.userTypeID, ";
                strQuery += " tblUserType.userType, ";
                strQuery += " userPassword, ";
                strQuery += " userName, ";
                strQuery += " email, ";
                strQuery += " referralId, ";
                strQuery += " dbo.InitCap(firstname) as firstName, ";
                strQuery += " dbo.InitCap(lastName) as lastName, ";
                strQuery += " friendsReferralId, ";
                strQuery += " countryId, ";
                strQuery += " provinceId, ";
                strQuery += " howYouKnowUs, ";
                strQuery += " creationDate, ";
                strQuery += " createdBy, ";
                strQuery += " modifiedDate, ";
                strQuery += " modifiedBy, ";
                strQuery += " isActive,ROW_NUMBER() OVER(ORDER BY tblUserInfo.userID desc) as RowNum ";
                strQuery += " FROM tblUserInfo ";
                strQuery += " inner join tblUserType on tblUserType.userTypeID = tblUserInfo.userTypeID ";
                strQuery += " where isDeleted = 0 and isActive = 1 and (tblUserType.userTypeId = 2 or tblUserType.userTypeId = 3 or tblUserType.userTypeId = 4 or tblUserType.userTypeId = 5 or tblUserType.userTypeId = 6 or tblUserType.userTypeId = 7) ";

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
                if (txtTastyCredit.Text.Trim() != "")
                {
                    strQuery += " and (isnull((SELECT sum(remainAmount) as remainAmount FROM gained where createdBy=[tblUserInfo].userId and (gainedType = 'Refferal' OR gainedType = 'Gift Card')),0)) " + this.ddlTastyCredit.SelectedValue.ToString() + txtTastyCredit.Text.Trim();
                }

                strQuery += ") as DerivedTableName";
                int strStartIndex = (Convert.ToInt32(strRowIndex) * pageGrid.PageSize) + 1;
                int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * pageGrid.PageSize;
                strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;
                strQuery += " order by userID desc ";

                strQuery += " SELECT	'Return Value' =  COUNT(userID) ";                
                strQuery += " FROM tblUserInfo ";
                strQuery += " inner join tblUserType on tblUserType.userTypeID = tblUserInfo.userTypeID ";
                strQuery += " where isDeleted = 0 and isActive = 1 and (tblUserType.userTypeId = 2 or tblUserType.userTypeId = 3 or tblUserType.userTypeId = 4 or tblUserType.userTypeId = 5 or tblUserType.userTypeId = 6 or tblUserType.userTypeId = 7) ";

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
                if (txtTastyCredit.Text.Trim() != "")
                {
                    strQuery += " and (isnull((SELECT sum(remainAmount) as remainAmount FROM gained where createdBy=[tblUserInfo].userId and (gainedType = 'Refferal' OR gainedType = 'Gift Card')),0)) " + this.ddlTastyCredit.SelectedValue.ToString() + txtTastyCredit.Text.Trim();
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
                dst = getMemberResturantAndSalesUsers(intPageNumber, pageGrid.PageSize);
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected DataSet getMemberResturantAndSalesUsers(int intStartIndex, int intMaxRecords)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@startRowIndex", intStartIndex);
            param[1] = new SqlParameter("@maximumRows", intMaxRecords);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spgetMemberResturantAndSalesUsers", param);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }

    protected void pageGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblID1 = (Label)e.Row.FindControl("lblID1");                                 
                string Reason = "Reason for Adding Tasty Credits : " + "|";
                string Userid = lblID1.Text.Trim() + "|";
                string CompleteData = Reason + Userid;
                Button BtnHidden = (Button)e.Row.FindControl("BtnHidden");
                TextBox txtFoodCredit = (TextBox)e.Row.FindControl("txtFoodCredit");
                ImageButton ibLogin = (ImageButton)e.Row.FindControl("ibLogin");
                ibLogin.OnClientClick = "return RunPopup('" + BtnHidden.ClientID + "', '" + CompleteData + "','" + txtFoodCredit.ClientID + "','')";                
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
                bindGrid(intCurrentPage - 1);
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
                bindGrid(intCurrentPage - 1);
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
            
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; 
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
            this.bindGrid(intCurrentPage - 1);
            txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (intCurrentPage).ToString();            
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return null;
        }
    }
    
    protected void pageGrid_Login(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataTable dtAdmin = (DataTable)Session["user"];
            if (dtAdmin != null)
            {
                TextBox txtFoodCredit = (TextBox)pageGrid.Rows[e.NewEditIndex].FindControl("txtFoodCredit");
                if (txtFoodCredit.Text.Trim() != "")
                {
                    float amount = 0;
                    float.TryParse(txtFoodCredit.Text.Trim(),out amount);
                    objUsedCard.remainAmount = amount;
                    objUsedCard.createdBy = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);
                    objUsedCard.gainedAmount = amount;
                    objUsedCard.fromId = Convert.ToInt64(dtAdmin.Rows[0]["userId"].ToString());
                    objUsedCard.targetDate = DateTime.Now.AddMonths(6);
                    objUsedCard.currencyCode = "CAD";
                    objUsedCard.gainedType = "Refferal";

                    if (objUsedCard.createMemberUseableGiftCard() == -1)
                    {
                        SendEmail(objUsedCard.createdBy.ToString(), objUsedCard.remainAmount.ToString());
                        TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                        int intCurrentPage = Convert.ToInt32(txtPage.Text.Trim());
                        bindGrid(intCurrentPage - 1);
                        txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                        txtPage.Text = intCurrentPage.ToString();
                    }
                    else
                    {
                        lblMessage.Text = "Amount could not be added to user account.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/error.png";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Please enter amount.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/Admin/Default.aspx"),false);
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

    protected void SendEmail(string strUserID, string strAmount)
    {
        try
        {
                
                obj = new BLLUser();
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

    protected string getAvailableFoodCredit(object userID)
    {
        string strFoodCredit = "";
        try
        {
            objUseableCard.createdBy = Convert.ToInt64(userID);
            DataTable dtRemainFoodCredit = objUseableCard.getUseableFoodCreditRefferalByUserID();
            if (dtRemainFoodCredit != null && dtRemainFoodCredit.Rows.Count > 0 && dtRemainFoodCredit.Rows[0][0].ToString() != "")
            {
                strFoodCredit = "$" + dtRemainFoodCredit.Rows[0][0].ToString() + " CAD";
            }
            else
            {
                strFoodCredit = "$0.00 CAD";
            }
        }
        catch (Exception ex)
        { }
        return strFoodCredit;
    }
    
}