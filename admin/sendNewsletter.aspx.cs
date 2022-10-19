using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Threading;
using System.Collections;
using GecLibrary;
using System.IO;

public partial class admin_sendNewsletter : System.Web.UI.Page
{
    BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();
        
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
            bindProvinces();   

            //Get & Fill Business Info into the GridView
            GetAllNewsLetterAndFillGrid();
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    protected void bindProvinces()
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlProvinceLive.DataSource = dt.DefaultView;
            ddlProvinceLive.DataTextField = "provinceName";
            ddlProvinceLive.DataValueField = "provinceId";
            ddlProvinceLive.DataBind();
            ddlProvinceLive.Items.Insert(0, "Select One");
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

    protected void ddlProvinceLive_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void FillCitpDroDownList(string strProvinceId)
    {
        try
        {
            BLLCities objBLLCities = new BLLCities();

            objBLLCities.provinceId = int.Parse(strProvinceId);

            DataTable dtCities = objBLLCities.getCitiesByProvinceId();

            ddlSelectCity.DataSource = dtCities;

            ddlSelectCity.DataTextField = "cityName";

            ddlSelectCity.DataValueField = "cityId";

            ddlSelectCity.DataBind();

            ddlSelectCity.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
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

    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }



    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strQuery = "";
        try
        {
            if (this.txtSearchTitle.Text.Trim() != "")
            {
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;

                strQuery = "SELECT ";
                strQuery += " [nlID]";
                strQuery += " ,[title]";
                strQuery += " ,[newsLetter]";
                strQuery += " ,[createdBy]";
                strQuery += " ,[creationDate]";
                strQuery += " ,[modifiedBy]";
                strQuery += " ,[modifiedDate]";
                strQuery += " FROM ";
                strQuery += " [newsLetters]";

                if (txtSearchTitle.Text.Trim() != "")
                {
                    strQuery += " where title like '%" + txtSearchTitle.Text.Trim() + "%' ";
                }

                strQuery += " order by nlID desc";

                pageGrid.PageIndex = 0;
                BindSearchedData(strQuery);
                ViewState["Query"] = strQuery;
            }
            else
            {
                pageGrid.PageIndex = 0;
                GetAllNewsLetterAndFillGrid();
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

            DataTable dtUser = Misc.search(Query);

            if ((dtUser != null) &&
                (dtUser.Columns.Count > 0) &&
                (dtUser.Rows.Count > 0))
            {
                pageGrid.DataSource = dtUser.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtUser.Rows.Count.ToString();
                pageGrid.PageIndex = 0;

                ViewState["Query"] = Query;
                DropDownList ddlPage = bindPageDropDown();
                if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = Session["ddlPage"].ToString();
                }
                lblPageCount.Text = pageGrid.PageCount.ToString();
                pageGrid.BottomPagerRow.Visible = true;
                if (pageGrid.PageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }

                //Show the Send button
                btnSend.Visible = true;

            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();

                //Hide the Send button
                btnSend.Visible = false;
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

    protected void GetAllNewsLetterAndFillGrid()
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
                dtUser = objBLLNewsLetters.getAllNewsLetter();

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
                //btnSearch.Enabled = true;
                //this.txtSearchTitle.Enabled = true;

                //Show the Send button
                btnSend.Visible = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();                
                //btnSearch.Enabled = false;
                //this.txtSearchTitle.Enabled = false;

                //Hide the Send button
                btnSend.Visible = false;
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
            this.GetAllNewsLetterAndFillGrid();
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (e.NewPageIndex + 1).ToString();
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
            this.GetAllNewsLetterAndFillGrid();
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
            this.GetAllNewsLetterAndFillGrid();
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
                dtUser = objBLLNewsLetters.getAllNewsLetter();
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
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
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
            ListItem objList = new ListItem("All", objBLLNewsLetters.getAllNewsLetter().Rows.Count.ToString());
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
    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect(ResolveUrl("~/admin/controlpanel.aspx"), false);
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

    protected void btnSend_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iCityId = int.Parse(this.ddlSelectCity.SelectedItem.Value.ToString());

            int iNewsLetterId = 0;
            
            for (int i = 0; i < this.pageGrid.Rows.Count; i++)
            {
                CheckBox RowLevelCheckBox = (CheckBox)this.pageGrid.Rows[i].FindControl("RowLevelCheckBox");

                if (RowLevelCheckBox.Checked)
                {
                    Label lblgrdnlID = (Label)this.pageGrid.Rows[i].FindControl("lblgrdnlID");

                    iNewsLetterId = int.Parse(lblgrdnlID.Text);

                    //Send email
                    ThreadStart starter = delegate { GetSubcribersAndSendEmail(iCityId, iNewsLetterId); };
                    new Thread(starter).Start();
                }
            }

            lblMessage.Text = "Email(s) have been sent successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
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

    private void GetSubcribersAndSendEmail(int iCityId, int iNewsLetterId)
    {
        try
        {
            BLLNewsLetters objBLLNewsLetters = new BLLNewsLetters();
            DataTable dtNewsLetterInfo = null;

            objBLLNewsLetters.nlId = iNewsLetterId;

            //Get Newsletter Info By Newsletter ID
            dtNewsLetterInfo = objBLLNewsLetters.getNewsLetterInfoById();

            string strEmailSubject = dtNewsLetterInfo.Rows[0]["title"].ToString();
            
            StringBuilder strEmailBody = new StringBuilder();
            strEmailBody.Append(dtNewsLetterInfo.Rows[0]["newsLetter"].ToString());

            //NewsLetter Subscriber Info
            DataTable dtSubscribers = null;

            BLLNewsLetterSubscriber objBLLNewsLetterSubscriber = new BLLNewsLetterSubscriber();

            //City Id
            objBLLNewsLetterSubscriber.CityId = iCityId;

            //Get News Letter Subscriber's List By City Id
            dtSubscribers = objBLLNewsLetterSubscriber.getNewsLetterSubscriberByCityId();

            int iSubId = 0;
            string strAddress = "";

            if ((dtSubscribers != null) && (dtSubscribers.Rows.Count > 0))
            {
                for (int i = 0; i < dtSubscribers.Rows.Count; i++)
                {
                    try
                    {
                        iSubId = int.Parse(dtSubscribers.Rows[i]["sID"].ToString().Trim());
                        strAddress = dtSubscribers.Rows[i]["email"].ToString().Trim();
                        
                        string strEncryptUserID = "";
                        strEncryptUserID = (Convert.ToInt64(iSubId.ToString()) + 111111).ToString();
                        string strNewEmailBody = "";
                        strNewEmailBody = strEmailBody.ToString();
                        strNewEmailBody = strNewEmailBody.Replace("unsubscribe.aspx", "unsubscribe.aspx?c=" + strEncryptUserID);
                        Misc.SendEmail(strAddress, "", "", "", strEmailSubject, strNewEmailBody.ToString());
                        //Save NewsLetter History By Subscriber Id
                        SaveNewsLetterSendHistoryBySubscriber(iSubId, iNewsLetterId);
                    }
                    catch (Exception ex)
                    { }
                }
            }
        }
        catch(Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void SaveNewsLetterSendHistoryBySubscriber(int iSubId, int inlId)
    {
        try
        {
            BLLNewsLetterSendHistory objBLLNewsLetterSendHistory = new BLLNewsLetterSendHistory();

            objBLLNewsLetterSendHistory.SId = iSubId;

            objBLLNewsLetterSendHistory.NlId = inlId;

            objBLLNewsLetterSendHistory.SendBy = int.Parse(ViewState["userID"].ToString());

            objBLLNewsLetterSendHistory.SendDate = DateTime.Now;

            //Create Newsletter Send History
            objBLLNewsLetterSendHistory.createNewsLetterSentHistory();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
}