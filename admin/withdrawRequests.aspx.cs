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

public partial class withdrawRequests : System.Web.UI.Page
{
    #region Global Variables
    BLLWithdrawRequest obj = new BLLWithdrawRequest();
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
            if (Session["user"] != null)
            {
                DataTable dtUser = (DataTable)Session["user"];
                if(dtUser!=null && dtUser.Rows.Count>0)
                {
                    ViewState["userID"] = dtUser.Rows[0]["userId"].ToString();
                }

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
            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(Session["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                Session["ddlPage"] = Misc.pageSize;
            }
            DataTable dtWithdraw;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                dtWithdraw = obj.getAllWithdrawRequests();
                dv = new DataView(dtWithdraw);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = false;
            }
            else
            {
                dtWithdraw = Misc.search(ViewState["Query"].ToString());
                dv = new DataView(dtWithdraw);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = true;
            }
            if (dtWithdraw != null && dtWithdraw.Rows.Count > 0)
            {


                pageGrid.DataSource = dv;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtWithdraw.Rows.Count.ToString();
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
                btnApproveSelected.Enabled = true;
                btnSearch.Enabled = true;
                
                
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnApproveSelected.Enabled = false;
                btnSearch.Enabled = false;
                
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
        ListItem objList = new ListItem("All", obj.getAllWithdrawRequests().Rows.Count.ToString());
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            obj.requestId = Convert.ToInt64(pageGrid.DataKeys[e.RowIndex].Value);
            Button btnAction = (Button) pageGrid.Rows[e.RowIndex].FindControl("btnAction");

            obj.modifiedBy = Convert.ToInt64(ViewState["userID"].ToString());
            if (btnAction != null && btnAction.Text == "Send")
            {
                obj.requestAction = "Check Sent";
            }
            else
            {
                obj.requestAction = "Disapproved";
                try
                {
                    HiddenField hfUserID = (HiddenField)pageGrid.Rows[e.RowIndex].FindControl("hfUserID");
                    HiddenField hfRequestAmount = (HiddenField)pageGrid.Rows[e.RowIndex].FindControl("hfRequestAmount");
                    if (hfRequestAmount != null && hfUserID != null
                        && hfRequestAmount.Value.Trim() != "" && hfUserID.Value.Trim() != "")
                    {
                        BLLAffiliatePartnerGained objUsedCard = new BLLAffiliatePartnerGained();

                        objUsedCard.UserId = Convert.ToInt32(hfUserID.Value.Trim());
                        objUsedCard.GainedType = "Adjustment";
                        objUsedCard.GainedAmount = float.Parse(hfRequestAmount.Value.Trim());

                        objUsedCard.RemainAmount = float.Parse(hfRequestAmount.Value.Trim());

                        objUsedCard.FromId = Convert.ToInt32(ViewState["userID"].ToString());
                        objUsedCard.CreatedBy = Convert.ToInt32(ViewState["userID"].ToString());

                        if (objUsedCard.createAffiliatePartnerGained() == -1)
                        {

                        }
                    }
                }
                catch (Exception ex)
                { }

            }
            if (obj.updateWithdrawRequestByAdmin()!=0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record has been updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record could not be updated.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {           
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
            pnlGrid.Visible = true;
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
        DataTable dtWithdraw = null;        
        if (ViewState["Query"] != null)
        {
            dtWithdraw = Misc.search(ViewState["Query"].ToString());
        }
        else
        {
            dtWithdraw = obj.getAllWithdrawRequests();
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
        DataView dv = new DataView(dtWithdraw);
        dv.Sort = sortExpression + direction;
        ViewState["Direction"] = sortExpression + direction;
        pageGrid.DataSource = dv;
        pageGrid.DataBind();

        TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
        txtPage.Text = (pageGrid.PageIndex + 1).ToString();
        if (dtWithdraw != null && dtWithdraw.Rows.Count > 0)
        {
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

            lblTotalRecords.Text = dtWithdraw.Rows.Count.ToString();
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
            int i = 0;
            strQuery = "Select requestId,userName,requestAction,requestUserType,requestAmount,requestCurrencyCode,withdrawRequest.creationDate";
            strQuery += ",withdrawRequest.createdBy,PreferredMethod ,withdrawRequest.modifiedDate, withdrawRequest.modifiedBy from withdrawRequest";
            strQuery += " inner join userInfo on userInfo.userId=withdrawRequest.createdBy ";
            if (txtSearchUserName.Text.Trim() != "")
            {
                i = 1;
                strQuery += " where userName like '%" + txtSearchUserName.Text.Trim() + "%'";
            }
            if (ddlRequesterType.SelectedIndex != 0)
            {
                if (i == 1)
                {
                    i = 2;
                    strQuery += " and  requestUserType='" + ddlRequesterType.SelectedValue.ToString() + "'";
                }
                else
                {
                    i = 2;
                    strQuery += " where requestUserType = '" + ddlRequesterType.SelectedValue.ToString() + "'";
                }
            }
            if (ddlRequestAction.SelectedIndex != 0)
            {
                if (i != 0)
                {
                    strQuery += " and  requestAction='" + ddlRequestAction.SelectedValue.ToString() + "'";
                }
                else
                {
                    strQuery += " where requestAction = '" + ddlRequestAction.SelectedValue.ToString() + "'";
                }
            }
            strQuery += " order by requestId desc";
            pageGrid.PageIndex = 0;
            BindSearchedData(strQuery);
            ViewState["Query"] = strQuery;
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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

            DataTable dtWithdraw = Misc.search(Query);
            if ((dtWithdraw != null) &&
                (dtWithdraw.Columns.Count > 0) &&
                (dtWithdraw.Rows.Count > 0))
            {
                pageGrid.DataSource = dtWithdraw.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtWithdraw.Rows.Count.ToString();
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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

    #region Button Approve Selected Click Event
    protected void btnApproveSelected_Click(object sender, ImageClickEventArgs e)
    {       
            int check = 0;
            try
            {
                for (int i = 0; i < pageGrid.Rows.Count; i++)
                {
                    CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");
                    if (chkSub.Checked && chkSub.Enabled)
                    {
                        Label lblID = (Label)pageGrid.Rows[i].FindControl("lblID1");
                        Button btnAction = (Button)pageGrid.Rows[i].FindControl("btnAction");
                        obj.requestId = Convert.ToInt64(lblID.Text);                        
                        obj.modifiedBy = Convert.ToInt64(ViewState["userID"].ToString());
                        if (btnAction != null && btnAction.Text == "Send")
                        {
                            obj.requestAction = "Check Sent";
                            if (obj.updateWithdrawRequestByAdmin() != 0)
                            {
                                check++;
                            }
                        }
                                                                       
                    }
                }
                if (check != 0)
                {
                    ViewState["Query"] = null;
                    pageGrid.PageIndex = 0;
                    bindGrid();
                    lblMessage.Text = "Selected records have been updated successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    pnlGrid.Visible = true;
                    lblMessage.Text = "Selected records are already approved.";
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
   
    protected string GetExpirationDateString(object expirationDate)
    {
        if (expirationDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(expirationDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    protected string GetButtonText(object expirationDate)
    {
        if (expirationDate.ToString() == "In Process")
        {
            return "Send";
        }
        else if (expirationDate.ToString() == "Disapproved")
        {
            return "PayBack";
        }
        return "Rollback";
    }
        

}
