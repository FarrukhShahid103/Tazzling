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

public partial class dealVerificationManagement : System.Web.UI.Page
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

    protected void gvViewDeals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //priority
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfdealpayment = (HiddenField)e.Row.FindControl("hfdealpayment");
                if (hfdealpayment != null
                    && hfdealpayment.Value.Trim() != ""
                    && Convert.ToInt32(hfdealpayment.Value.Trim()) > 0)
                {
                    if (hfdealpayment.Value.Trim() == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (hfdealpayment.Value.Trim() == "2")
                    {
                        e.Row.BackColor = System.Drawing.Color.Orange;
                    }
                    else if (hfdealpayment.Value.Trim() == "3")
                    {
                        e.Row.BackColor = System.Drawing.Color.GreenYellow;
                    }
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
            if (e.CommandName.Trim() == "DownloadVoucers")
            {
                DataTable dtDealOrders = new DataTable("SampleVouchers");
                DataColumn dealOrderCode = new DataColumn("dealOrderCode");
                DataColumn voucherSecurityCode = new DataColumn("voucherSecurityCode");

                dtDealOrders.Columns.Add(dealOrderCode);
                dtDealOrders.Columns.Add(voucherSecurityCode);

                DataRow dRow;

                BLLSampleVouchers objSV = new BLLSampleVouchers();
                objSV.dealId = Convert.ToInt64(e.CommandArgument.ToString());
                DataTable dtExcelReport = objSV.getAllSampleVouchersByDealID();
                GECEncryption objEnc = new GECEncryption();
                if (dtExcelReport != null && dtExcelReport.Rows.Count > 0)
                {

                    for (int i = 0; i < dtExcelReport.Rows.Count; i++)
                    {
                        dRow = dtDealOrders.NewRow();
                        dRow["dealOrderCode"] = objEnc.DecryptData("deatailOrder", dtExcelReport.Rows[i]["dealOrderCode"].ToString().Trim());
                        dRow["voucherSecurityCode"] = dtExcelReport.Rows[i]["voucherSecurityCode"].ToString().Trim();
                        dtDealOrders.Rows.Add(dRow);
                    }
                    DataRow[] dtRow;
                    dtRow = dtDealOrders.Select(null, "dealOrderCode ASC");

                    // DataTable dtSortTable = dtDealOrders.Clone();
                    DataTable dtSortTable = new DataTable("SampleVouchers");
                    DataColumn Sr = new DataColumn("Sr.");
                    dealOrderCode = new DataColumn("Voucher Code");
                    voucherSecurityCode = new DataColumn("Voucher Security Code");
                    dtSortTable.Columns.Add(Sr);
                    dtSortTable.Columns.Add(dealOrderCode);
                    dtSortTable.Columns.Add(voucherSecurityCode);

                    for (int i = 0; i < dtRow.Length; i++)
                    {
                        dRow = dtSortTable.NewRow();
                        dRow["Sr."] = i + 1;
                        dRow["Voucher Code"] = dtRow[i]["dealOrderCode"].ToString();
                        dRow["Voucher Security Code"] = dtRow[i]["voucherSecurityCode"].ToString();
                        dtSortTable.Rows.Add(dRow);
                    }
                    ExportToExcel(dtSortTable, "SampleVouchers.xls");                    
                }
            }
            else if (e.CommandName.Trim() == "DownloadOrders")
            {
                string[] strData = e.CommandArgument.ToString().Split(',');
                DataTable dtUser = SearchhDealInfoByDifferentParams(strData[0].Trim());                
                DataTable dtDealOrders = new DataTable("dealOrders");
                if (dtUser != null && dtUser.Rows.Count > 0
                    && dtUser.Rows[0]["shippingAndTax"].ToString().Trim() != ""
                    && Convert.ToBoolean(dtUser.Rows[0]["shippingAndTax"].ToString()))
                {
                    DataColumn Customer = new DataColumn("Customer");
                    DataColumn dealOrderCode = new DataColumn("dealOrderCode");
                    DataColumn voucherSecurityCode = new DataColumn("voucherSecurityCode");
                    DataColumn Status = new DataColumn("Status");
                    DataColumn Telephone = new DataColumn("Telephone");
                    DataColumn Address = new DataColumn("Address");
                    DataColumn Note = new DataColumn("Note");
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
                            dRow["Customer"] = dtUser.Rows[i]["Name3"].ToString().Trim();
                            dRow["dealOrderCode"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                            dRow["voucherSecurityCode"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                            dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                            dRow["Telephone"] = dtUser.Rows[i]["Telephone"].ToString().Trim();
                            dRow["Address"] = dtUser.Rows[i]["Address"].ToString().Trim();
                            dRow["Note"] = dtUser.Rows[i]["shippingNote"].ToString().Trim();
                            dtDealOrders.Rows.Add(dRow);
                        }
                        DataRow[] dtRow;
                        dtRow = dtDealOrders.Select(null, "dealOrderCode ASC");

                        DataTable dtSortTable = new DataTable("dealOrders");
                        DataColumn Sr = new DataColumn("Sr.");
                        Customer = new DataColumn("Customer");
                        dealOrderCode = new DataColumn("Voucher Code");
                        voucherSecurityCode = new DataColumn("Voucher Security Code");
                        Status = new DataColumn("Status");
                        Telephone = new DataColumn("Telephone");
                        Address = new DataColumn("Address");
                        Note = new DataColumn("Note");
                        dtSortTable.Columns.Add(Sr);
                        dtSortTable.Columns.Add(Customer);
                        dtSortTable.Columns.Add(dealOrderCode);
                        dtSortTable.Columns.Add(voucherSecurityCode);
                        dtSortTable.Columns.Add(Status);
                        dtSortTable.Columns.Add(Telephone);
                        dtSortTable.Columns.Add(Address);
                        dtSortTable.Columns.Add(Note);

                        dRow = dtSortTable.NewRow();
                        dRow["Sr."] = "Title";
                        dRow["Customer"] = dtUser.Rows[0]["Title"].ToString().Trim();                        
                        dRow["Voucher Code"] = "";
                        dRow["Voucher Security Code"] = "";
                        dRow["Status"] = "";
                        dRow["Telephone"] = "";
                        dRow["Address"] = "";
                        dRow["Note"] = "";
                        dtSortTable.Rows.Add(dRow);

                        dRow = dtSortTable.NewRow();
                        dRow["Sr."] = "Business Name";
                        dRow["Customer"] = dtUser.Rows[0]["restaurantBusinessName"].ToString().Trim();                        
                        dRow["Voucher Code"] = "";
                        dRow["Voucher Security Code"] = "";
                        dRow["Status"] = "";
                        dRow["Telephone"] = "";
                        dRow["Address"] = "";
                        dRow["Note"] = "";
                        dtSortTable.Rows.Add(dRow);

                        try
                        {
                            dRow = dtSortTable.NewRow();
                            DateTime dttemp = DateTime.Now;
                            if (strData[1].Trim() != "" && DateTime.TryParse(strData[1].Trim(), out dttemp))
                            {
                                dRow["Sr."] = "Campaign Ended On:";
                                dRow["Customer"] = dttemp.ToString("MMM dd,yyyy");                                
                                dRow["Voucher Code"] = "Total Sold:";
                                dRow["Voucher Security Code"] = strData[2].Trim();
                            }
                            else
                            {
                                dRow["Sr."] = "Total Sold:";
                                dRow["Customer"] = strData[2].Trim();                               
                                dRow["Voucher Code"] = "";
                                dRow["Voucher Security Code"] = "";
                            }
                            dRow["Status"] = "";
                            dRow["Telephone"] = "";
                            dRow["Address"] = "";
                            dRow["Note"] = "";
                            dtSortTable.Rows.Add(dRow);
                        }
                        catch (Exception ex)
                        {
                        }

                        for (int i = 0; i < dtRow.Length; i++)
                        {
                            dRow = dtSortTable.NewRow();
                            dRow["Sr."] = i + 1;
                            dRow["Customer"] = dtRow[i]["Customer"].ToString();
                            dRow["Voucher Code"] = dtRow[i]["dealOrderCode"].ToString();
                            dRow["Voucher Security Code"] = dtRow[i]["voucherSecurityCode"].ToString();
                            dRow["Status"] = dtRow[i]["Status"].ToString();
                            dRow["Telephone"] = dtRow[i]["Telephone"].ToString();
                            dRow["Address"] = dtRow[i]["Address"].ToString();
                            dRow["Note"] = dtRow[i]["Note"].ToString().Trim();
                            dtSortTable.Rows.Add(dRow);
                        }
                        ExportToUser(dtSortTable, "DealOrders.xls");
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
                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn dealOrderCode = new DataColumn("dealOrderCode");
                        DataColumn voucherSecurityCode = new DataColumn("voucherSecurityCode");
                        DataColumn Status = new DataColumn("Status");
                        DataColumn Telephone = new DataColumn("Telephone");
                        DataColumn Address = new DataColumn("Address");
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
                                dRow["Customer"] = dtUser.Rows[i]["Name"].ToString().Trim();
                                dRow["dealOrderCode"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["voucherSecurityCode"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dRow["Telephone"] = dtUser.Rows[i]["phoneNo"].ToString().Trim();
                                dRow["Address"] = dtUser.Rows[i]["Address2"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            DataRow[] dtRow;
                            dtRow = dtDealOrders.Select(null, "dealOrderCode ASC");

                            DataTable dtSortTable = new DataTable("dealOrders");
                            DataColumn Sr = new DataColumn("Sr.");
                            Customer = new DataColumn("Customer");
                            dealOrderCode = new DataColumn("Voucher Code");
                            voucherSecurityCode = new DataColumn("Voucher Security Code");
                            Status = new DataColumn("Status");
                            Telephone = new DataColumn("Telephone");
                            Address = new DataColumn("Address");
                            dtSortTable.Columns.Add(Sr);
                            dtSortTable.Columns.Add(Customer);
                            dtSortTable.Columns.Add(dealOrderCode);
                            dtSortTable.Columns.Add(voucherSecurityCode);
                            dtSortTable.Columns.Add(Status);
                            dtSortTable.Columns.Add(Telephone);
                            dtSortTable.Columns.Add(Address);

                            dRow = dtSortTable.NewRow();
                            dRow["Sr."] = "Title: ";
                            dRow["Customer"] = dtUser.Rows[0]["Title"].ToString().Trim();                            
                            dRow["Voucher Code"] = "";
                            dRow["Voucher Security Code"] = "";
                            dRow["Status"] = "";
                            dRow["Telephone"] = "";
                            dRow["Address"] = "";
                            dtSortTable.Rows.Add(dRow);

                            dRow = dtSortTable.NewRow();
                            dRow["Sr."] = "Business Name: ";
                            dRow["Customer"] = dtUser.Rows[0]["restaurantBusinessName"].ToString().Trim();                            
                            dRow["Voucher Code"] = "";
                            dRow["Voucher Security Code"] = "";
                            dRow["Status"] = "";
                            dRow["Telephone"] = "";
                            dRow["Address"] = "";

                            try
                            {
                                dRow = dtSortTable.NewRow();
                                DateTime dttemp = DateTime.Now;
                                if (strData[1].Trim() != "" && DateTime.TryParse(strData[1].Trim(), out dttemp))
                                {
                                    dRow["Sr."] = "Campaign Ended On:";
                                    dRow["Customer"] = dttemp.ToString("MMM dd,yyyy");                                   
                                    dRow["Voucher Code"] = "Total Sold:";
                                    dRow["Voucher Security Code"] = strData[2].Trim();
                                }
                                else
                                {
                                    dRow["Sr."] = "Total Sold:";
                                    dRow["Customer"] = strData[2].Trim();                                   
                                    dRow["Voucher Code"] = "";
                                    dRow["Voucher Security Code"] = "";
                                }
                                dRow["Status"] = "";
                                dRow["Telephone"] = "";
                                dRow["Address"] = "";
                                dtSortTable.Rows.Add(dRow);
                            }
                            catch (Exception ex)
                            {
                            }

                           // dtSortTable.Rows.Add(dRow);

                            for (int i = 0; i < dtRow.Length; i++)
                            {
                                dRow = dtSortTable.NewRow();
                                dRow["Sr."] = i + 1;
                                dRow["Customer"] = dtRow[i]["Customer"].ToString();
                                dRow["Voucher Code"] = dtRow[i]["dealOrderCode"].ToString();
                                dRow["Voucher Security Code"] = dtRow[i]["voucherSecurityCode"].ToString();
                                dRow["Status"] = dtRow[i]["Status"].ToString();
                                dRow["Telephone"] = dtRow[i]["Telephone"].ToString();
                                dRow["Address"] = dtRow[i]["Address"].ToString();
                                dtSortTable.Rows.Add(dRow);
                            }
                            ExportToUser(dtSortTable, "DealOrders.xls");
                        }
                    }
                    else
                    {


                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn dealOrderCode = new DataColumn("dealOrderCode");
                        DataColumn voucherSecurityCode = new DataColumn("voucherSecurityCode");
                        DataColumn Status = new DataColumn("Status");
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
                                dRow["Customer"] = dtUser.Rows[i]["Name2"].ToString().Trim();
                                dRow["dealOrderCode"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["voucherSecurityCode"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            DataRow[] dtRow;
                            dtRow = dtDealOrders.Select(null, "dealOrderCode ASC");

                            DataTable dtSortTable = new DataTable("dealOrders");
                            DataColumn Sr = new DataColumn("Sr.");
                            Customer = new DataColumn("Customer");
                            dealOrderCode = new DataColumn("Voucher Code");
                            voucherSecurityCode = new DataColumn("Voucher Security Code");
                            Status = new DataColumn("Status");
                            dtSortTable.Columns.Add(Sr);
                            dtSortTable.Columns.Add(Customer);
                            dtSortTable.Columns.Add(dealOrderCode);
                            dtSortTable.Columns.Add(voucherSecurityCode);
                            dtSortTable.Columns.Add(Status);

                            dRow = dtSortTable.NewRow();
                            dRow["Sr."] = "Title: ";
                            dRow["Customer"] = dtUser.Rows[0]["Title"].ToString().Trim();                            
                            dRow["Voucher Code"] = "";
                            dRow["Voucher Security Code"] = "";
                            dRow["Status"] = "";
                            dtSortTable.Rows.Add(dRow);

                            dRow = dtSortTable.NewRow();
                            dRow["Sr."] = "Business Name: ";
                            dRow["Customer"] = dtUser.Rows[0]["restaurantBusinessName"].ToString().Trim();                            
                            dRow["Voucher Code"] = "";
                            dRow["Voucher Security Code"] = "";
                            dRow["Status"] = "";
                            dtSortTable.Rows.Add(dRow);

                            try
                            {
                                dRow = dtSortTable.NewRow();
                                DateTime dttemp = DateTime.Now;
                                if (strData[1].Trim() != "" && DateTime.TryParse(strData[1].Trim(), out dttemp))
                                {
                                    dRow["Sr."] = "Campaign Ended On:";
                                    dRow["Customer"] = dttemp.ToString("MMM dd,yyyy");
                                    dRow["Voucher Code"] = "Total Sold:";
                                    dRow["Voucher Security Code"] = strData[2].Trim();
                                }
                                else
                                {
                                    dRow["Sr."] = "Total Sold:";
                                    dRow["Customer"] = strData[2].Trim();
                                    dRow["Voucher Code"] = "";
                                    dRow["Voucher Security Code"] = "";
                                }
                                dRow["Status"] = "";                                
                                dtSortTable.Rows.Add(dRow);
                            }
                            catch (Exception ex)
                            {
                            }


                            for (int i = 0; i < dtRow.Length; i++)
                            {
                                dRow = dtSortTable.NewRow();
                                dRow["Sr."] = i + 1;
                                dRow["Customer"] = dtRow[i]["Customer"].ToString();
                                dRow["Voucher Code"] = dtRow[i]["dealOrderCode"].ToString();
                                dRow["Voucher Security Code"] = dtRow[i]["voucherSecurityCode"].ToString();
                                dRow["Status"] = dtRow[i]["Status"].ToString();
                                dtSortTable.Rows.Add(dRow);
                            }
                            ExportToUser(dtSortTable, "DealOrders.xls");
                        }
                    }
                }
               
            }
            else if (e.CommandName.Trim() == "EditDetail")
            {
                ResetControls();
                pnlDetail.Visible = true;
                pnlForm.Visible = false;
                GetAndShowDealInfoByDealId(e.CommandArgument.ToString());
            }
        }
        catch (Exception ex)
        { }
    }

    private void ExportToExcel(DataTable table, string strFileName)
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
 
    private void ExportToUser(DataTable table, string strFileName)
    {
        GridView gv = new GridView();
        gv.Font.Size = 12;
        gv.HeaderStyle.Font.Size = 13;
        gv.HeaderStyle.Font.Bold = true;

        gv.DataSource = table;
        gv.DataBind();

        gv.Rows[0].Style.Add("font-weight", "Bold");
        gv.Rows[1].Style.Add("font-weight", "Bold");
        gv.Rows[2].Style.Add("font-weight", "Bold");
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
        hlPreviewLink.Text = "";
        lblCellNumber.Text = "";
        lblDealEndTime.Text = "";
        lblDealName.Text = "";
        lblDealSoldSummery.Text = "";
        lblDealStartTime.Text = "";
        lblPhoneNumber.Text = "";
    }

    private void GetAndShowDealInfoByDealId(string strIDs)
    {
        try
        {
            BLLDeals objBLLDeals = new BLLDeals();


            objBLLDeals.DealId = Convert.ToInt64(strIDs);
            try
            {
                BLLDealCity objCity = new BLLDealCity();
                objCity.DealId = objBLLDeals.DealId;
                DataTable dtCityList = objCity.getDealCityListByDealId();
                if (dtCityList != null && dtCityList.Rows.Count > 0)
                {
                    objBLLDeals.cityId = Convert.ToInt32(dtCityList.Rows[0]["cityId"].ToString().Trim());
                }
            }
            catch (Exception ex)
            { }

            DataTable dtDeals = objBLLDeals.getDealForPaymentFormByDealId();

            if ((dtDeals != null) && (dtDeals.Rows.Count > 0))
            {
                lblBusinessName.Text = dtDeals.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblAlternateEmailAddress.Text = dtDeals.Rows[0]["alternativeEmail"].ToString().Trim();
                lblBusinessEmailAddress.Text = dtDeals.Rows[0]["email"].ToString().Trim();
                lblCellNumber.Text = dtDeals.Rows[0]["cellNumber"].ToString().Trim();
                lblPhoneNumber.Text = dtDeals.Rows[0]["phone"].ToString().Trim();
                try
                {
                    lblDealEndTime.Text = Convert.ToDateTime(dtDeals.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("yyyy-MM-dd");
                    lblDealStartTime.Text = Convert.ToDateTime(dtDeals.Rows[0]["dealStartTimeC"].ToString().Trim()).ToString("yyyy-MM-dd");

                }
                catch (Exception ex)
                { }
                //hfDealID.Value = dtDeals.Rows[0]["dealId"].ToString().Trim();
                lblBusinessName.Text = dtDeals.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblOwnerFirstName.Text = dtDeals.Rows[0]["firstName"].ToString().Trim();
                lblOwnerLastName.Text = dtDeals.Rows[0]["lastName"].ToString().Trim();
                hlPreviewLink.Text = ConfigurationManager.AppSettings["YourSite"] + "/Preview.aspx?sidedeal=" + dtDeals.Rows[0]["dealId"].ToString().Trim();
                hlPreviewLink.NavigateUrl = ConfigurationManager.AppSettings["YourSite"] + "/Preview.aspx?sidedeal=" + dtDeals.Rows[0]["dealId"].ToString().Trim();



                hfBusinessId.Value = dtDeals.Rows[0]["restaurantId"].ToString().Trim();
                lblDealName.Text = dtDeals.Rows[0]["title"].ToString().Trim();
                lblDealSoldSummery.Text = "Successful Orders: " + dtDeals.Rows[0]["SuccessfulOrder"].ToString().Trim() + "<br>" + "Cancelled Orders:" + dtDeals.Rows[0]["CancelledOrder"].ToString().Trim() + "<br>Refunded Order" + dtDeals.Rows[0]["RefundedOrder"].ToString().Trim();
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
            strQuery += " ,rtrim(userCCInfo.ccInfoDFirstName) +' ' + rtrim(userCCInfo.ccInfoDLastName) as 'Name'";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + rtrim(userInfo.lastName) as 'Name2'";
            strQuery += " ,rtrim(userShippingInfo.Name) as 'Name3'";
            strQuery += " ,orderNo";
            strQuery += " ,shippingAndTax";
            strQuery += " ,deals.restaurantId,Title,restaurantBusinessName";
            strQuery += " ,(userShippingInfo.Address+', '+userShippingInfo.City+', '+userShippingInfo.State+', '+userShippingInfo.ZipCode+', '+userShippingInfo.shippingCountry) as 'Address'";
            strQuery += " ,(userCCInfo.ccInfoBAddress+', '+userCCInfo.ccInfoBCity+', '+userCCInfo.ccInfoBProvince+', '+userCCInfo.ccInfoBPostalCode) as 'Address2',userinfo.phoneNo";
            strQuery += " ,userShippingInfo.Telephone";
            strQuery += " ,shippingNote";
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
            strQuery += " inner join restaurant on (restaurant.restaurantId = deals.restaurantId)"; 
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join userCCInfo on (userCCInfo.ccInfoID = dealOrders.ccInfoID)";
            strQuery += " left outer join userShippingInfo on (userShippingInfo.shippingInfoId = dealOrders.shippingInfoId)";
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
            BLLRestaurant objRest = new BLLRestaurant();
            objRest.restaurantId = Convert.ToInt64(hfBusinessId.Value);
            objRest.postDealVerification = ddlPostDealVerification.SelectedValue.ToString().Trim();
            objRest.preDealVerification = ddlPreDealVerification.SelectedValue.Trim();
            objRest.updateRestaurantDealVerificationInfoByResID();
            SearchhDealInfoByDifferentParams(0);
            lblMessage.Text = "Deal verification information has updated successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/checked.png";
            pnlDetail.Visible = false;
            pnlForm.Visible = true;
        }

        //if (hfDealId.Value.ToString().Trim() != "" && hfDealId.Value.ToString().Trim() != "0")
        //{
        //    BLLDeals objBLLDeals = new BLLDeals();

        //    //Set the Deal Id here
        //    objBLLDeals.DealId = Convert.ToInt64(hfDealId.Value.ToString().Trim());
        //    objBLLDeals.DealNote = txtDealNote.Text.Trim();
        //    objBLLDeals.updateDealNoteByDealId();

        //    if (hfBusinessId.Value.ToString().Trim() != "" && hfBusinessId.Value.ToString().Trim() != "0")
        //    {
        //        BLLRestaurant objRest = new BLLRestaurant();
        //        objRest.restaurantId = Convert.ToInt64(hfBusinessId.Value.ToString().Trim());
        //        if (ViewState["userID"] != null)
        //        {
        //            objRest.modifiedBy = Convert.ToInt64(ViewState["userID"].ToString().Trim());
        //        }
        //        objRest.paymentStatus = ddlPaymentStatus.SelectedValue.ToString().Trim();
        //        objRest.updateRestaurantPaymentStatusByResID();
        //    }
        //    SearchhDealInfoByDifferentParams();
        //    this.divAddNewDeal.Visible = false;
        //    //Show All Deal Info into Grid View here
        //    this.upItems.Visible = true;
        //    this.divSrchFields.Visible = true;
        //    lblMessage.Text = "Deal update successfully.";
        //    lblMessage.Visible = true;
        //    imgGridMessage.Visible = true;
        //    imgGridMessage.ImageUrl = "images/checked.png";
        //}
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
        //try
        //{


        //    //Show the Grid View
        //    this.upItems.Visible = true;
        //    this.divSrchFields.Visible = true;

        //    //Hide the Add New Deal here
        //    this.divAddNewDeal.Visible = false;

        //    //View All Deals
        //    this.lblDealInfoHeading.Text = "View All Deals";

        //    //Hide the Image and Text message
        //    this.imgGridMessage.Visible = false;
        //    this.lblMessage.Text = "";

        //}
        //catch (Exception ex)
        //{
        //    string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        //}
    }

    private DataSet SearchQuery(string strRowIndex)
    {
        DataSet dstDeals = null;
        string strQuery = "";
        try
        {
            strQuery = " SELECT * from(";
            strQuery += " SELECT ";
            strQuery += " [deals].[dealId]";
            strQuery += ",[deals].[dealpayment]";
            strQuery += ",[deals].[restaurantId]";
            strQuery += ",[deals].[title]";
            strQuery += ",[deals].[sellingPrice]";
            strQuery += ",[deals].[valuePrice]";            
            strQuery += ",[deals].[OurCommission]";
            strQuery += ",[deals].[dealStartTime]";
            strQuery += ",[deals].[dealEndTime]";
            strQuery += ",deals.voucherExpiryDate";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].[status]='Successful' and [dealOrders].dealId = [deals].dealId),0) 'SuccessfulOrder'";
            strQuery += ",isnull((SELECT count(vID) from sampleVouchers where sampleVouchers.dealId =[deals].dealId) ,0) 'TotalVoucher'";
            strQuery += ",[restaurant].[email]";
            strQuery += ",[restaurant].[phone]";            
            strQuery += ",[restaurant].[preDealVerification]";
            strQuery += ",[restaurant].[postDealVerification]";            
            strQuery += ",[restaurant].[restaurantBusinessName]";
            strQuery += ",ROW_NUMBER() OVER(ORDER BY dealStartTime desc) as RowNum";
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";            
            strQuery += "where restaurant.[restaurantId]= deals.[restaurantId] ";


            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [deals].[title] like '%" + txtSrchDealTitle.Text.Trim().Replace("'", "''") + "%' ";
            }

            if (txtSrchBusinessName.Text.Trim() != "")
            {
                strQuery += " and [restaurant].[restaurantBusinessName] like '%" + txtSrchBusinessName.Text.Trim().Replace("'", "''") + "%' ";
            }
            if (this.ddlSearchStatus.SelectedValue == "started")
            {
                strQuery += " and dealStartTime <= getdate() and dealEndTime >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "upcoming")
            {
                strQuery += " and dealStartTime >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "expired")
            {
                strQuery += " and dealEndTime <= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "all")
            {
                strQuery += "";
            }

          
            strQuery += ") as DerivedTableName";
            int strStartIndex = (Convert.ToInt32(strRowIndex) * gvViewDeals.PageSize) + 1;
            int strEndIndex = (Convert.ToInt32(strRowIndex) + 1) * gvViewDeals.PageSize;
            strQuery += " WHERE RowNum BETWEEN " + strStartIndex + " AND " + strEndIndex;
            strQuery += " order by dealStartTime desc ";

            strQuery += " SELECT 'Return Value' =  COUNT([deals].[dealId]) FROM [deals]";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";            
            strQuery += "where restaurant.[restaurantId]= deals.[restaurantId] ";
            
            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [deals].[title] like '%" + txtSrchDealTitle.Text.Trim().Replace("'", "''") + "%' ";
            }

            if (txtSrchBusinessName.Text.Trim() != "")
            {
                strQuery += " and [restaurant].[restaurantBusinessName] like '%" + txtSrchBusinessName.Text.Trim().Replace("'", "''") + "%' ";
            }
            if (txtSrchBusinessName.Text.Trim() != "")
            {
                strQuery += " and [restaurant].[restaurantBusinessName] like '%" + txtSrchBusinessName.Text.Trim().Replace("'", "''") + "%' ";
            }
            if (this.ddlSearchStatus.SelectedValue == "started")
            {
                strQuery += " and dealStartTime <= getdate() and dealEndTime >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "upcoming")
            {
                strQuery += " and dealStartTime >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "expired")
            {
                strQuery += " and dealEndTime <= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "all")
            {
                strQuery += "";
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
        txtSrchBusinessName.Text = "";
        ddlSearchStatus.SelectedValue = "all";        
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