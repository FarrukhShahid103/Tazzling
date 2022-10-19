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

public partial class giftcardManagment : System.Web.UI.Page
{
    #region Global Variables
    BLLGiftCard obj = new BLLGiftCard();
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
            bindGrid();
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

    protected string GetExpirationDateString(object expirationDate)
    {
        if (expirationDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(expirationDate);

            if (DateTime.Compare(DateTime.Now, dt) > 0)
                return "Gift Card Expired";
            else
                return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    protected string GetCardExplain(string takenBy)
    {
        try
        {

            string explain = takenBy;   
         
                if (takenBy == "")
                  return  explain = "Not Been Redeemed";
               
            return explain;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

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
            DataTable dtCard;
            DataView dv;
            if (ViewState["Query"] == null)
            {
                dtCard = obj.getAllApprovedGiftCards();
                dv = new DataView(dtCard);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = false;
            }
            else
            {
                dtCard = Misc.search(ViewState["Query"].ToString());
                dv = new DataView(dtCard);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }
                btnShowAll.Visible = true;
            }
            if (dtCard != null && dtCard.Rows.Count > 0)
            {


                pageGrid.DataSource = dv;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtCard.Rows.Count.ToString();
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
                btnDeleteSelected.Enabled = true;
                btnSearch.Enabled = true;
                txtSearchCardAmount.Enabled = true;
                txtSearchCreatedBy.Enabled = true;
                txtSearchUsedBy.Enabled = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                btnDeleteSelected.Enabled = false;
                btnSearch.Enabled = false;

                txtSearchCardAmount.Enabled = false;
                txtSearchCreatedBy.Enabled = false;
                txtSearchUsedBy.Enabled = false;
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
        ListItem objList = new ListItem("All", obj.getAllApprovedGiftCards().Rows.Count.ToString());
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
            pnlForm.Visible = false;
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
            pnlForm.Visible = false;
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
            obj.giftCardId = Convert.ToInt32(pageGrid.DataKeys[e.RowIndex].Value);
            result = obj.updateGiftCardSetUnApproved();
            if (result!=0)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                pageGrid.PageIndex = 0;
                bindGrid();
                lblMessage.Text = "Record could not be deleted.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }   
    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCard = null;
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            obj.giftCardId = Convert.ToInt32(pageGrid.SelectedDataKey.Value);
            dtCard = obj.getGiftCardByID();
            if ((dtCard != null) && (dtCard.Columns.Count > 0) && (dtCard.Rows.Count > 0))
            {
                ibGenerateCode.Visible = false;
                txtCardNumber.ReadOnly = true;
                txtCardAmount.ReadOnly = true;                
                txtCardNumber.Text = dtCard.Rows[0]["giftCardCode"].ToString();
                txtCardAmount.Text = dtCard.Rows[0]["giftCardAmount"].ToString();
                txtMessage.Text = dtCard.Rows[0]["comments"].ToString();
                txtEmailTo.Text = "";
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                pnlForm.Visible = true;
                pnlGrid.Visible = false;                
            }

        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
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
            pnlForm.Visible = false;
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
        DataTable dtCard = null;        
        if (ViewState["Query"] != null)
        {
            dtCard = Misc.search(ViewState["Query"].ToString());
        }
        else
        {
            dtCard = obj.getAllApprovedGiftCards();
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
        DataView dv = new DataView(dtCard);
        dv.Sort = sortExpression + direction;
        ViewState["Direction"] = sortExpression + direction;
        pageGrid.DataSource = dv;
        pageGrid.DataBind();

        TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
        txtPage.Text = (pageGrid.PageIndex + 1).ToString();
        if (dtCard != null && dtCard.Rows.Count > 0)
        {
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

            lblTotalRecords.Text = dtCard.Rows.Count.ToString();
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



            strQuery = "SELECT giftCardId,comments,giftCardCode,giftCardAmount,currencyCode,takenBy";
            strQuery += " ,expirationDate,giftCard.creationDate ,giftCard.createdBy,giftCard.modifiedDate,giftCard.modifiedBy";
            strQuery += " ,tb.userName as 'takenByUser' ,cb.userName as 'createdByUser' FROM giftCard";
            strQuery += " left outer join userInfo tb on tb.userId=giftCard.takenBy";
            strQuery += " inner join userInfo cb on cb.userId=giftCard.createdBy Where OrderStatus='APPROVED' ";            
            if (txtSearchCardAmount.Text.Trim() != "")
            {
                strQuery += " and giftCardAmount = '" + txtSearchCardAmount.Text.ToString().Trim() + "'";                                                             
            }
            else if (txtSearchCreatedBy.Text.Trim() != "")
            {
                strQuery += " and cb.userName like '%" + txtSearchCreatedBy.Text.ToString().Trim() + "%'";                                                             
            }
            else if (txtSearchUsedBy.Text.Trim() != "")
            {
                strQuery += " and tb.userName like '%" + txtSearchUsedBy.Text.ToString().Trim() + "%'";                                                                             
            }
            strQuery += " order by giftCardId desc";
            if (txtSearchCardAmount.Text.Trim() != "" || txtSearchCreatedBy.Text.Trim()!="" || txtSearchUsedBy.Text.Trim()!="")
            {
                pageGrid.PageIndex = 0;
                BindSearchedData(strQuery);
                ViewState["Query"] = strQuery;
            }
            else
            {
                ViewState["Query"] = null;
                this.pageGrid.PageIndex = 0;
                bindGrid();
            }
          
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
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

            DataTable dtCard = Misc.search(Query);
            if ((dtCard != null) &&
                (dtCard.Columns.Count > 0) &&
                (dtCard.Rows.Count > 0))
            {
                pageGrid.DataSource = dtCard.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtCard.Rows.Count.ToString();
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
            pnlForm.Visible = false;
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

            txtSearchCardAmount.Text = "";
            txtSearchCreatedBy.Text = "";
            txtSearchUsedBy.Text = "";
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    #region Button Delete Selected Click Event
    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    {
        int check = 0;        
        try
        {
            for (int i = 0; i < pageGrid.Rows.Count; i++)
            {
                CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");
                if (chkSub.Checked)
                {
                    Label lblID = (Label)pageGrid.Rows[i].FindControl("lblID1");
                    obj.giftCardId = Convert.ToInt32(lblID.Text);                   
                    if (obj.updateGiftCardSetUnApproved()!=0)
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
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    #region Button Add New Click 
    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtCardNumber.Text = "";
            txtCardAmount.Text = "";
            txtEmailTo.Text = "";
            txtMessage.Text = "";
            txtCardAmount.ReadOnly = false;
            txtCardNumber.ReadOnly = false;
            ibGenerateCode.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            pnlForm.Visible = true;
            pnlGrid.Visible = false;            
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
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
            pnlForm.Visible = false;
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


    #region Button Save Click Event
    BLLGiftCardOrder objGCOrder = new BLLGiftCardOrder();
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];
            obj.createdBy = Convert.ToInt64(dtUser.Rows[0]["userID"].ToString());

            string strComments = txtMessage.Text.ToString();
            string strGiftCardCode = txtCardNumber.Text.Trim();
            string strGiftCardAmt = txtCardAmount.Text.Trim();
            string strCurrencyCode = "CAD";
            DateTime dtExpireDate = DateTime.Now.AddMonths(6);
            DateTime dtCreateDate = DateTime.Now;
            //Get & Set the Order Ref No
            string strOrderRefNo = Guid.NewGuid().ToString();

            objGCOrder.comments = strComments;
            objGCOrder.orderRefNo = strOrderRefNo;
            objGCOrder.redeem = 0;
            objGCOrder.commission = 0;
            objGCOrder.redeem = 0;
            objGCOrder.creditCard = 0;
            
            objGCOrder.subTotalAmount = Convert.ToDouble(strGiftCardAmt);
            objGCOrder.createdBy = Convert.ToInt64(dtUser.Rows[0]["userID"].ToString());
            obj.giftCardOrderId = objGCOrder.createGiftCardOrderByUser();

            if (obj.giftCardOrderId != 0)
            {
                strGiftCardAmt = txtCardAmount.Text.ToString().Trim();
                obj.comments = strComments;
                obj.giftCardCode = strGiftCardCode;
                obj.giftCardAmount = float.Parse(strGiftCardAmt);
                obj.currencyCode = strCurrencyCode;
                obj.expirationDate = dtExpireDate;
                obj.creationDate = dtCreateDate;
                obj.orderRefNoSent = strOrderRefNo;
                
                obj.createdBy = Convert.ToInt64(dtUser.Rows[0]["userID"].ToString());
                obj.createGiftCardByUser();

            }

            objGCOrder.orderRefNo = strOrderRefNo;
            objGCOrder.orderStatus = "APPROVED";
            objGCOrder.orderIdReceived = "ADMIN-ID-" + DateTime.Now.Millisecond;

            int iStatus = objGCOrder.updateGiftCardOrderByOrderRefNoSent();

            System.Text.StringBuilder sbEmail = new System.Text.StringBuilder();
            //Append the Gift Card Expire Date & Comment            
            string strFrom = dtUser.Rows[0]["userName"].ToString();
            System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
            string toAddress = txtEmailTo.Text.Trim();
            string fromAddress = strFrom;
            
            string Subject = "You have received a gift card from Tazzling.Com";
            mailBody.Append("<html><head><title></title></head><body><h4>Dear " + txtEmailTo.Text.Trim() + " , </h4>");
            mailBody.Append("<font size='3'>You have received a gift card from TastyGo Administrator.<br /> ");
            mailBody.Append("In order to use this gift card, you can start register with Tastygo by clicking the link below. ");
            mailBody.Append("Ordering Deal is only clicks away.<br />");
            mailBody.Append("<a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></font><br />");

            mailBody.Append("<table>");
            mailBody.Append("<tr><td style='text-align:left'>Giftcard Amount: </td>");
            mailBody.Append("<td style='text-align:left'> $" + txtCardAmount.Text.Trim() + " CAD</td></tr>");
            mailBody.Append("<tr><td style='text-align:left'>Giftcard Code: </td>");
            mailBody.Append("<td style='text-align:left'> " + txtCardNumber.Text.Trim() + " </td></tr>");
            mailBody.Append("<tr><td style='text-align:left'>Expiry Date: </td>");
            mailBody.Append("<td style='text-align:left'> " + DateTime.Now.AddMonths(6).ToString("MMMM dd yyyy") + " </td></tr>");
            mailBody.Append("<tr><td style='text-align:left'>Message: </td>");
            mailBody.Append("<td style='text-align:left'> " + txtMessage.Text.Trim() + " </td></tr>");
            mailBody.Append("</table><p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");

            if (Misc.SendEmail(toAddress, "", "", fromAddress, Subject, mailBody.ToString()))
            {
                bindGrid();
                lblMessage.Text = "Gift card sent successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
            }
            else
            {
                bindGrid();
                lblMessage.Text = "Gift card could not be sent.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }

    }
    #endregion

    #region Button Update Click Event
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];
            System.Text.StringBuilder sbEmail = new System.Text.StringBuilder();            
            //Append the Gift Card Expire Date & Comment            
            string strFrom = dtUser.Rows[0]["userName"].ToString();
            System.Text.StringBuilder mailBody = new System.Text.StringBuilder();

            string toAddress = txtEmailTo.Text.Trim();
            string fromAddress = strFrom;
            string Subject = "You have received a gift card from Tazzling.Com";
            mailBody.Append("<html><head><title></title></head><body><h4>Dear "+txtEmailTo.Text.Trim()+", </h4>");
            mailBody.Append("<font size='3'>You have received a gift card from TastyGo Administrator.<br /> ");
            mailBody.Append("In order to use this gift card, you can start register with Tastygo by clicking the link below. ");
            mailBody.Append("Ordering deal is only clicks away.<br />");
            mailBody.Append("<a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></font><br />");

            mailBody.Append("<table>");
            mailBody.Append("<tr><td style='text-align:left'>Giftcard Amount: </td>");
            mailBody.Append("<td style='text-align:left'> $" + txtCardAmount.Text.Trim() + " CAD</td></tr>");
            mailBody.Append("<tr><td style='text-align:left'>Giftcard Code: </td>");
            mailBody.Append("<td style='text-align:left'> " + txtCardNumber.Text.Trim() + " </td></tr>");
            mailBody.Append("<tr><td style='text-align:left'>Expiry Date: </td>");
            mailBody.Append("<td style='text-align:left'> " + DateTime.Now.AddMonths(6).ToString("MMMM dd yyyy") + " </td></tr>");
            mailBody.Append("<tr><td style='text-align:left'>Message: </td>");
            mailBody.Append("<td style='text-align:left'> " + txtMessage.Text.Trim() + " </td></tr>");
            mailBody.Append("</table><p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");

            if (Misc.SendEmail(toAddress, "", "", fromAddress, Subject, mailBody.ToString()))
            {
                bindGrid();
                lblMessage.Text = "Gift card sent successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
            }
            else
            {
                bindGrid();
                lblMessage.Text = "Gift card count not sent.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                pnlForm.Visible = false;
                pnlGrid.Visible = true;
            }
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;

        }
    }
    #endregion

    #region Button Cancel Click Event
    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
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
            DataTable dtCard = null;
            if (ViewState["Query"] != null)
            {
                dtCard = Misc.search(ViewState["Query"].ToString());
            }
            else
            {
                dtCard = obj.getAllApprovedGiftCards();
            }
            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");
            lblTotalRecords.Text = dtCard.Rows.Count.ToString();
            lblPageCount.Text = pageGrid.PageCount.ToString();
            pnlGrid.Visible = true;
            pnlForm.Visible = false;
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    protected void ibGenerateCode_Click(object sender, ImageClickEventArgs e)
    {
        txtCardNumber.Text = GenerateId().ToString().Substring(0, 10);
    }


    private long GenerateId()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }
}
