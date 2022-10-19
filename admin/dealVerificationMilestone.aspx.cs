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

public partial class dealVerificationMilestone : System.Web.UI.Page
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

                if (lblPreDealVerification.Text.Trim() == "0% Data Entered" && lblPostDealVerification.Text.Trim() == "0% Deal Ended")
                {                    
                        e.Row.BackColor = System.Drawing.Color.Orange;                    
                }
                else if (lblPreDealVerification.Text.Trim() == "100% Called Business, All Staff ready for deal" && lblPostDealVerification.Text.Trim() == "100% Confirmed Received Customer List")
                {
                    e.Row.BackColor = System.Drawing.Color.Lime;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
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

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {                        
            DataTable dtResturant=Misc.search("select restaurantId from restaurant where restaurantBusinessName='"+txtSaveBusinessName.Text.Trim()+"'");
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
            }
            else
            {
                lblMessage.Text = "Restaurant with name \""+txtSaveBusinessName.Text.Trim()+"\" does not exist.";
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
 
    private void ExportToUser(DataTable table, string strFileName)
    {
        GridView gv = new GridView();
        gv.DataSource = table;
        gv.DataBind();
        string attachment = "attachment; filename="+strFileName;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
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
            strQuery += ",[restaurant].[preDealVerification]";
            strQuery += ",[restaurant].[postDealVerification]";            
            strQuery += ",[restaurant].[restaurantBusinessName]";
            strQuery += ",ROW_NUMBER() OVER(ORDER BY updealId desc) as RowNum";
            strQuery += " FROM ";
            strQuery += "[upcommingDeals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= upcommingDeals.[restaurantId] ";            
            strQuery += "where restaurant.[restaurantId]= upcommingDeals.[restaurantId] ";
            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [upcommingDeals].[title] like '%" + txtSrchDealTitle.Text.Trim() + "%' ";
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
                strQuery += " and [upcommingDeals].[title] like '%" + txtSrchDealTitle.Text.Trim() + "%' ";
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
                btnSearch.Enabled = false;
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
        gvViewDeals.PageIndex = 0;
        SearchhDealInfoByDifferentParams(0);       
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
}