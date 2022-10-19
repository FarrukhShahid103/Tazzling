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
using System.IO;

public partial class admin_newsletterSubsMgmt : System.Web.UI.Page
{
    BLLNewsLetterSubscriber objBLLNewsLetterSubscriber = new BLLNewsLetterSubscriber();

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

            //Get & Set the Total # Subscribers
            GetCountOfSubscribers();
            
            //Get & Fill Business Info into the GridView
            GetAllNewsLetterAndFillGrid();
        }
        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    protected void pageGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        bool result = false;
        try
        {
            objBLLNewsLetterSubscriber.SId = Convert.ToInt64(pageGrid.DataKeys[e.NewEditIndex].Value);
            Label lblStatus = (Label)pageGrid.Rows[e.NewEditIndex].FindControl("lblStatus");
            if (lblStatus != null)
            {
                if (lblStatus.Text.ToString() == "True")
                {
                    objBLLNewsLetterSubscriber.Status = false;
                }
                else
                {
                    objBLLNewsLetterSubscriber.Status = true;
                }
            }
            result = objBLLNewsLetterSubscriber.changeSubscriberStatus();
            if (result)
            {
                ViewState["Query"] = null;
                pageGrid.PageIndex = 0;
                GetCountOfSubscribers();
                //Get & Fill Business Info into the GridView
                GetAllNewsLetterAndFillGrid();
                lblMessage.Text = "Status has been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;

            }
            else
            {
                pageGrid.PageIndex = 0;
                GetCountOfSubscribers();
                //Get & Fill Business Info into the GridView
                GetAllNewsLetterAndFillGrid();
                lblMessage.Text = "Status has not been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {            
        }
    }

    private void GetCountOfSubscribers()
    {
        try
        {
            string strQry = "SELECT count(*) FROM newsletterSubscribers";

            DataTable dtSubscribers = Misc.search(strQry);

            if ((dtSubscribers != null) && (dtSubscribers.Rows.Count > 0))
            {
                //Get the Total no of Subscribers
                this.lblTotalSubs.Text = "Total Subscribers : " + dtSubscribers.Rows[0][0].ToString();
            }
            else
            {
                this.lblTotalSubs.Text = "Total Subscribers : 0";
            }
        }
        catch (Exception ex)
        { }
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

            this.pageGrid.DataSource = null;
            this.pageGrid.DataBind();
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
            if (strProvinceId != "Select One")
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
            else
            {
                ddlSelectCity.DataSource = null;
                ddlSelectCity.DataBind();
            }
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
        
        try
        {
            //Hide the Message at the Top of the Page
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            GetCountOfSubscribers();
            //Get & Fill Business Info into the GridView
            GetAllNewsLetterAndFillGrid();
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


    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        if ((this.ddlSelectCity.SelectedIndex != 0) && ((this.ddlSelectCity.SelectedIndex != -1)))
        {
            DataTable dtEmails = null;
            objBLLNewsLetterSubscriber = new BLLNewsLetterSubscriber();
            objBLLNewsLetterSubscriber.Email = this.txtSearchEmail.Text.Trim();

            if ((this.ddlSelectCity.SelectedIndex != 0) && ((this.ddlSelectCity.SelectedIndex != -1)))
            {
                //Set the city Id here
                objBLLNewsLetterSubscriber.CityId = int.Parse(this.ddlSelectCity.SelectedItem.Value.ToString());

                dtEmails = objBLLNewsLetterSubscriber.getNewsLetterSubscriberByCityId();
                //dtEmails = Misc.search("select * from subscriber");
            }

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + DateTime.Now.ToString("MMddyyyyHHmmss") + "_" + this.ddlSelectCity.SelectedItem.Text.Trim() + ".txt";
            FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            string strEmail = "";
            if (dtEmails != null && dtEmails.Rows.Count > 0)
            {
                for (int i = 0; i < dtEmails.Rows.Count; i++)
                {
                    //strEmail += dtEmails.Rows[i][0].ToString() + ";";
                    strEmail += dtEmails.Rows[i]["email"].ToString() + ";";
                }
            }
            m_streamWriter.WriteLine(strEmail);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            try
            {
                string contentType = "";
                //Get the physical path to the file.
                // string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf";

                //Set the appropriate ContentType.
                contentType = "Application/txt";

                //Set the appropriate ContentType.

                Response.ContentType = contentType;
                Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo(DateTime.Now.ToString("MMddyyyyHHmmss") + "_" + this.ddlSelectCity.SelectedItem.Text.Trim() + ".txt")).Name);

                //Write the file directly to the HTTP content output stream.
                Response.WriteFile(FilePath);
                Response.End();
            }
            catch
            {
                //To Do
            }
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
                btnDeleteSelected.Visible = true;
                btnDownload.Visible=true;

            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();

                //Hide the Send button
                btnDeleteSelected.Visible = false;
                btnDownload.Visible = false;
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
                //Set the email here
                objBLLNewsLetterSubscriber.Email = this.txtSearchEmail.Text.Trim();

                if ((this.ddlSelectCity.SelectedIndex != 0) && ((this.ddlSelectCity.SelectedIndex != -1)))
                {
                    //Set the city Id here
                    objBLLNewsLetterSubscriber.CityId = int.Parse(this.ddlSelectCity.SelectedItem.Value.ToString());

                    dtUser = objBLLNewsLetterSubscriber.getNewsLetterSubscriberByEmailCityId();
                }
                else
                {
                    //City is not selected
                    dtUser = null;
                }
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

                //Show the Send button
                btnDeleteSelected.Visible = true;
                btnDownload.Visible = true;
            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();

                //Hide the Send button
                btnDeleteSelected.Visible = false;
                btnDownload.Visible = false;
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
            GetCountOfSubscribers();
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
            GetCountOfSubscribers();
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
            GetCountOfSubscribers();
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
                //Set the city Id here
                objBLLNewsLetterSubscriber.CityId = int.Parse(this.ddlSelectCity.SelectedItem.Value.ToString());
                objBLLNewsLetterSubscriber.Email = this.txtSearchEmail.Text.Trim();

                dtUser = objBLLNewsLetterSubscriber.getNewsLetterSubscriberByEmailCityId();
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

            //Set the City Id here
            objBLLNewsLetterSubscriber.CityId = int.Parse(this.ddlSelectCity.SelectedItem.Value.ToString());
            //Set the Email Id here
            objBLLNewsLetterSubscriber.Email = this.txtSearchEmail.Text.Trim();

            ListItem objList = new ListItem("All", objBLLNewsLetterSubscriber.getNewsLetterSubscriberByEmailCityId().Rows.Count.ToString());
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
            //Manage Subscriber Mode
            ManageSubscriberMode();
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

    private void ManageSubscriberMode()
    {
        try
        {
            //Hide the Search portion here
            this.divSrch.Visible = true;

            //Hide the Add New button
            this.btnAddNew.Visible = true;

            //Set the Main heading here
            this.lblpopHead.Text = "Manage Newsletter Subscribers";

            //Hide Grid View here
            this.tblGrid.Visible = true;

            //Set the Subscriber Email here
            this.lblSubscriber.Text = "Subscriber(s)";

            //Set the Auto-Post-Back event to False
            this.ddlSelectCity.AutoPostBack = true;

            //Show the Subscriber email
            this.txtSubscriberEmail.Visible = false;

            //Hide Delete button here
            this.btnDeleteSelected.Visible = true;
            btnDownload.Visible = true;
            //Show the Save button
            this.btnSave.Visible = false;

            //Show the Cancel button
            this.CancelButton.Visible = false;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
       
    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Set the Main heading here
            this.lblpopHead.Text = "Add Newsletter Subscriber";

            ChangeItToNewUpdateMode();

            //Change the Image URL of the Save button
            this.btnSave.ImageUrl = "~/admin/images/btnSave.jpg";
            this.btnSave.ToolTip = "Add New Subscriber";

            //Hide the Message at the Top of the Page
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
        }
        catch (Exception ex)
        {
            
        }
    }

    private void ChangeItToNewUpdateMode()
    {
        try
        {
            //Hide the Search portion here
            this.divSrch.Visible = false;

            //Hide the Add New button
            this.btnAddNew.Visible = false;

            //Hide Grid View here
            this.tblGrid.Visible = false;

            //Set the Subscriber Email here
            this.lblSubscriber.Text = "Subscriber Email";

            //Set the Auto-Post-Back event to False
            this.ddlSelectCity.AutoPostBack = false;

            //Show the Subscriber email
            this.txtSubscriberEmail.Visible = true;
            this.txtSubscriberEmail.Text = "";

            //Hide Delete button here
            this.btnDeleteSelected.Visible = false;
            btnDownload.Visible = false;
            //Show the Save button
            this.btnSave.Visible = true;

            //Show the Cancel button
            this.CancelButton.Visible = true;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (this.btnSave.ToolTip == "Add New Subscriber")
            {
                AddNewSubscriberEmail();

                //Manage Subscriber Mode
                ManageSubscriberMode();

                //Get & Fill Business Info into the GridView
                GetAllNewsLetterAndFillGrid();
            }
            else if (this.btnSave.ToolTip == "Update Subscriber")
            {
                UpdateSubscriberEmail();

                //Manage Subscriber Mode
                ManageSubscriberMode();

                //Get & Fill Business Info into the GridView
                GetAllNewsLetterAndFillGrid();
            }
            GetCountOfSubscribers();
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

    private bool AddNewSubscriberEmail()
    {
        bool bStatus = false;

        try
        {
            Misc.addSubscriberEmailForAdmin(this.txtSubscriberEmail.Text.Trim(), this.ddlSelectCity.SelectedValue.ToString().Trim());
            //objBLLNewsLetterSubscriber.Email = this.txtSubscriberEmail.Text.Trim();
            //objBLLNewsLetterSubscriber.CityId = int.Parse(this.ddlSelectCity.SelectedValue.ToString().Trim());
            //objBLLNewsLetterSubscriber.CreatedDate = DateTime.Now;
            //objBLLNewsLetterSubscriber.Status = true;
          //  int iChk = objBLLNewsLetterSubscriber.createNewsLetterSubscriber();
           
                bStatus = true;
                lblMessage.Visible = true;
                lblMessage.Text = "New Subscriber has been added successfully.";
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {  }

        return bStatus;
    }

    private bool UpdateSubscriberEmail()
    {
        bool bStatus = false;

        try
        {

            BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
            obj.Email = this.txtSubscriberEmail.Text.Trim();
            obj.CityId = Convert.ToInt64(this.ddlSelectCity.SelectedValue.ToString().Trim());
            DataTable dtEmail = obj.getNewsLetterSubscriberByEmailCityId();
            int iChk = 0;
            if (dtEmail != null && dtEmail.Rows.Count == 0)
            {
                objBLLNewsLetterSubscriber.SId = int.Parse(this.hfSId.Value.Trim());
                objBLLNewsLetterSubscriber.Email = this.txtSubscriberEmail.Text.Trim();
                objBLLNewsLetterSubscriber.CityId = int.Parse(this.ddlSelectCity.SelectedValue.ToString().Trim());
                objBLLNewsLetterSubscriber.ModifiedDate = DateTime.Now;
                objBLLNewsLetterSubscriber.Status = true;
                iChk = objBLLNewsLetterSubscriber.updateNewsLetterSubscriberById();
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "This email is already Subscribe for selected city.";
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            if (iChk != 0)
            {
                bStatus = true;

                lblMessage.Visible = true;
                lblMessage.Text = "Subscriber Info has been updated successfully.";
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Subscriber has not been update.";
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {  }

        return bStatus;
    }

    private bool DeleteSubscriberEmailBySId()
    {
        bool bStatus = false;

        try
        {
            objBLLNewsLetterSubscriber.SId = int.Parse(this.hfSId.Value.Trim());

            int iChk = objBLLNewsLetterSubscriber.deleteNewsLetterSubscriberById();

            if (iChk != 0)
            {
                bStatus = true;

                lblMessage.Visible = true;
                lblMessage.Text = "Subscriber has been deleted successfully.";
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Subscriber has not been deleted.";
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            
        }

        return bStatus;
    }

    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    {
        int UserCheck = 0;
        int check = 0;
        int result = 0;

        try
        {
            for (int i = 0; i < pageGrid.Rows.Count; i++)
            {
                CheckBox chkSub = (CheckBox)pageGrid.Rows[i].FindControl("RowLevelCheckBox");

                if (chkSub.Checked)
                {
                    //Count the # of check boxes user selected
                    UserCheck++;

                    Label lblgrdsID = (Label)pageGrid.Rows[i].FindControl("lblgrdsID");
                    objBLLNewsLetterSubscriber.SId = Convert.ToInt32(lblgrdsID.Text);
                    result = objBLLNewsLetterSubscriber.deleteNewsLetterSubscriberById();

                    if (result != 0)
                    {
                        check++;
                    }
                }
            }

            //Means no record has been deleted
            if (check == 0)
            {                
                pageGrid.PageIndex = 0;
             
                //Get & Fill Business Info into the GridView
                GetAllNewsLetterAndFillGrid();
                GetCountOfSubscribers();
                lblMessage.Text = "Selected record(s) have not been deleted.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else if ((UserCheck > 0) && (UserCheck == check))//All selected records are deleted successfully
            {             
                pageGrid.PageIndex = 0;
                
                //Get & Fill Business Info into the GridView
                GetAllNewsLetterAndFillGrid();
                GetCountOfSubscribers();
                lblMessage.Text = "Selected records have been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else//Some are delete and some are not
            {
                pageGrid.PageIndex = 0;
                
                //Get & Fill Business Info into the GridView
                GetAllNewsLetterAndFillGrid();
                GetCountOfSubscribers();
                lblMessage.Text = "Some records have been deleted successfully and some are not.";
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

    protected void ddlSelectCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            //Get & Fill Business Info into the GridView
            GetAllNewsLetterAndFillGrid();
            GetCountOfSubscribers();
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

    protected void pageGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {  //Set the Main heading here
            this.lblpopHead.Text = "Update Newsletter Subscriber";

            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            ChangeItToNewUpdateMode();

            //Change the Image URL of the Update button
            this.btnSave.ImageUrl = "~/admin/images/btnUpdate.jpg";
            this.btnSave.ToolTip = "Update Subscriber";

            //Initilize the DataTable here
            
            this.hfSId.Value = pageGrid.SelectedDataKey.Value.ToString();

            Label lblEmail = (Label)pageGrid.Rows[pageGrid.SelectedIndex].FindControl("lblEmail");           
            this.txtSubscriberEmail.Text = lblEmail.ToolTip;
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
        try
        {
            this.hfSId.Value = pageGrid.DataKeys[e.RowIndex].Value.ToString();

            DeleteSubscriberEmailBySId();

            //Get & Fill Business Info into the GridView
            GetAllNewsLetterAndFillGrid();
            GetCountOfSubscribers();
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
}