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
using SQLHelper;

public partial class dealSampleVouchersManagement : System.Web.UI.Page
{
    public string strIDs = "";
    public bool displayPrevious = false;
    public bool displayNext = true;
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
                try
                {
                    SearchhDealInfoByDifferentParams();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
            }
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }
      
    DataTable DataTableAllCities = new DataTable();
 
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
   

    #region "Grid View Events"

    protected void gvViewDeals_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataTable dtAdmin = (DataTable)Session["user"];
            if (dtAdmin != null)
            {

                HiddenField hfDealID = (HiddenField)gvViewDeals.Rows[e.NewEditIndex].FindControl("hfDealID");               
                TextBox txtSampleVouchers = (TextBox)gvViewDeals.Rows[e.NewEditIndex].FindControl("txtSampleVouchers");
                if (hfDealID != null && txtSampleVouchers != null)                    
                {
                    GECEncryption objEnc = new GECEncryption();                                
                    int intSampleVoucersToCreate=0;
                    Int32.TryParse(txtSampleVouchers.Text.Trim(),out intSampleVoucersToCreate);
                    DataTable dtDetail = SearchhDealInfoByDealID(hfDealID.Value);
                    if (dtDetail != null && dtDetail.Rows.Count > 0)
                    {
                        int TotalOrders = 0;
                        int unUsedVoucher = 0;
                        int UsedVoucher = 0;
                        int TotalVoucher = 0;
                        DataTable dtPlacesOrders = null;
                        Int32.TryParse(dtDetail.Rows[0]["TotalOrders"].ToString(), out TotalOrders);
                        Int32.TryParse(dtDetail.Rows[0]["unUsedVoucher"].ToString(), out unUsedVoucher);
                        Int32.TryParse(dtDetail.Rows[0]["UsedVoucher"].ToString(), out UsedVoucher);
                        Int32.TryParse(dtDetail.Rows[0]["TotalVoucher"].ToString(), out TotalVoucher);
                        if (TotalOrders == 0 && TotalVoucher==0)
                        {
                            for (int i = 0; i < intSampleVoucersToCreate; i++)
                            {
                                bool notExist = true;
                                BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                                BLLSampleVouchers sampleVoucher = new BLLSampleVouchers();
                                while (notExist)
                                {
                                    objDetail.dealOrderCode = objEnc.EncryptData("deatailOrder", GenerateId().ToString().Substring(1, 7) + gerrateAlpabit().ToUpper());
                                    DataTable dtDeal = objDetail.getAllDealOrderDetailByDealOrderCode();
                                    sampleVoucher.dealOrderCode = objDetail.dealOrderCode;
                                    DataTable dtSampleVoucher = sampleVoucher.getSampleVoucherByDealOrderCode();
                                    if (dtDeal != null && dtSampleVoucher!=null 
                                        && dtDeal.Rows.Count == 0 && dtSampleVoucher.Rows.Count==0)
                                    {
                                        notExist = false;
                                    }
                                }
                                
                                sampleVoucher.dealId = Convert.ToInt64(hfDealID.Value);
                                sampleVoucher.dealOrderCode = objDetail.dealOrderCode;
                                sampleVoucher.isUsed = false;
                                sampleVoucher.voucherSecurityCode = GenerateId().ToString().Substring(1, 3) + "-" + GenerateId().ToString().Substring(1, 3);
                                sampleVoucher.createSampleVouchers();
                            }
                        }
                        else if (TotalOrders > 0 && TotalVoucher == 0 && intSampleVoucersToCreate > TotalOrders)
                        {
                            dtPlacesOrders = SearchhDealInfoByDifferentParams(hfDealID.Value.Trim());
                            if (dtPlacesOrders != null && dtPlacesOrders.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtPlacesOrders.Rows.Count; i++)
                                {                                    
                                    BLLSampleVouchers sampleVoucher = new BLLSampleVouchers();
                                    sampleVoucher.dealId = Convert.ToInt64(hfDealID.Value);
                                    sampleVoucher.dealOrderCode = dtPlacesOrders.Rows[i]["dealOrderCode"].ToString().Trim();
                                    sampleVoucher.isUsed = true;
                                    sampleVoucher.voucherSecurityCode = dtPlacesOrders.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                    sampleVoucher.detailID = Convert.ToInt64(dtPlacesOrders.Rows[i]["detailID"].ToString().Trim());                                    
                                    sampleVoucher.createSampleVouchers();
                                } 
                            }
                            for (int i = 0; i < intSampleVoucersToCreate-dtPlacesOrders.Rows.Count; i++)
                            {
                                bool notExist = true;
                                BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                                BLLSampleVouchers sampleVoucher = new BLLSampleVouchers();
                                while (notExist)
                                {
                                    objDetail.dealOrderCode = objEnc.EncryptData("deatailOrder", GenerateId().ToString().Substring(1, 7) + gerrateAlpabit().ToUpper());
                                    DataTable dtDeal = objDetail.getAllDealOrderDetailByDealOrderCode();
                                    sampleVoucher.dealOrderCode = objDetail.dealOrderCode;
                                    DataTable dtSampleVoucher = sampleVoucher.getSampleVoucherByDealOrderCode();
                                    if (dtDeal != null && dtSampleVoucher != null
                                        && dtDeal.Rows.Count == 0 && dtSampleVoucher.Rows.Count == 0)
                                    {
                                        notExist = false;
                                    }
                                }
                                sampleVoucher.dealId = Convert.ToInt64(hfDealID.Value);
                                sampleVoucher.dealOrderCode = objDetail.dealOrderCode;
                                sampleVoucher.isUsed = false;
                                sampleVoucher.voucherSecurityCode = GenerateId().ToString().Substring(1, 3) + "-" + GenerateId().ToString().Substring(1, 3);
                                sampleVoucher.createSampleVouchers();
                            } 
                        }
                        else if (TotalVoucher > 0 && TotalOrders == 0 && intSampleVoucersToCreate > TotalVoucher)
                        {
                            for (int i = 0; i < intSampleVoucersToCreate - TotalVoucher; i++)
                            {
                                bool notExist = true;
                                BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                                BLLSampleVouchers sampleVoucher = new BLLSampleVouchers();
                                while (notExist)
                                {
                                    objDetail.dealOrderCode = objEnc.EncryptData("deatailOrder", GenerateId().ToString().Substring(1, 7) + gerrateAlpabit().ToUpper());
                                    DataTable dtDeal = objDetail.getAllDealOrderDetailByDealOrderCode();
                                    sampleVoucher.dealOrderCode = objDetail.dealOrderCode;
                                    DataTable dtSampleVoucher = sampleVoucher.getSampleVoucherByDealOrderCode();
                                    if (dtDeal != null && dtSampleVoucher != null
                                        && dtDeal.Rows.Count == 0 && dtSampleVoucher.Rows.Count == 0)
                                    {
                                        notExist = false;
                                    }
                                }                                
                                sampleVoucher.dealId = Convert.ToInt64(hfDealID.Value);
                                sampleVoucher.dealOrderCode = objDetail.dealOrderCode;
                                sampleVoucher.isUsed = false;
                                sampleVoucher.voucherSecurityCode = GenerateId().ToString().Substring(1, 3) + "-" + GenerateId().ToString().Substring(1, 3);
                                sampleVoucher.createSampleVouchers();
                            }
                        }
                        else if (intSampleVoucersToCreate < TotalVoucher && unUsedVoucher >= (TotalVoucher - intSampleVoucersToCreate))
                        {
                            string strQuery = "delete from sampleVouchers where vID in (select top " + (TotalVoucher - intSampleVoucersToCreate).ToString() + " vID from sampleVouchers where isUsed=0 and dealid="+hfDealID.Value.Trim()+")";
                            SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strQuery);
                        }
                        
                        SearchhDealInfoByDifferentParams();
                        lblMessage.Visible = true;
                        lblMessage.Text = "Your Sample Vouchers has been updated successfuly.";
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/checked.png";
                       
                    }
                }
             
            }
            else
            {
                Response.Redirect(ResolveUrl("~/Admin/Default.aspx"), false);
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

    protected void gvViewDeals_Login(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName.Trim() == "DownloadExcel")
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

                    ExportToUser(dtSortTable);
                }
            }
        }
        catch (Exception ex)
        { }
    }

    private void ExportToUser(DataTable table)
    {
        GridView gv = new GridView();
        gv.DataSource = table;
        gv.DataBind();
        string attachment = "attachment; filename=SampleVouchers.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }

    private DataTable SearchhDealInfoByDealID(string strDealid)
    {
        string strQuery = "";
        DataTable dtDeals = null;
        try
        {
            strQuery = "SELECT ";
            strQuery += "isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = " + strDealid + ")) ,0) 'TotalOrders'";
            strQuery += ",isnull((SELECT count(vID) from sampleVouchers where sampleVouchers.dealId in (select dealid from deals as dealsInner where dealsInner.dealid = " + strDealid + ") and sampleVouchers.isused=0) ,0) 'unUsedVoucher'";
            strQuery += ",isnull((SELECT count(vID) from sampleVouchers where sampleVouchers.dealId in (select dealid from deals as dealsInner where dealsInner.dealid = " + strDealid + ") and sampleVouchers.isused=1) ,0) 'UsedVoucher'";
            strQuery += ",isnull((SELECT count(vID) from sampleVouchers where sampleVouchers.dealId in (select dealid from deals as dealsInner where dealsInner.dealid = " + strDealid + ")) ,0) 'TotalVoucher'";           
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "where [deals].dealid ="+strDealid;                                              
            dtDeals = Misc.search(strQuery);            
        }
        catch (Exception ex)
        {          
        }
        return dtDeals;
    }

    private DataTable SearchhDealInfoByDifferentParams( string strDealID)
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";            
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";                                                
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " ,[dealOrderDetail].[detailID]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";                        
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join deals on (deals.dealId = dealOrders.dealId)";            
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            strQuery += " where ";
            strQuery += " (dealOrders.dealId =" + strDealID + ")";                               
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
 
    private long GenerateId()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }

    private string gerrateAlpabit()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random();
        var result = new string(
            Enumerable.Repeat(chars, 3)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());
        return result.ToString();
    }

    private string GenerateStringID()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }
 
    protected void gvViewDeals_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int iDealId = Convert.ToInt32(gvViewDeals.SelectedDataKey.Value);

            Label lblStatus = (Label)gvViewDeals.Rows[gvViewDeals.SelectedIndex].FindControl("lblStatus");

            BLLDeals objBLLDeals = new BLLDeals();

            objBLLDeals.DealId = iDealId;

            if (lblStatus != null)
            {
                if (lblStatus.Text.ToString() == "True")
                {
                    objBLLDeals.DealStatus = false;
                }
                else
                {
                    objBLLDeals.DealStatus = true;
                }
            }

            objBLLDeals.ModifiedBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();
            objBLLDeals.ModifiedDate = DateTime.Now;

            //Update the Deal Status By Deal Id
            objBLLDeals.updateDealStatusByDealId();

            //GetAllDealInfoAndFillGrid();            
            SearchhDealInfoByDifferentParams();

            lblMessage.Text = "Status has been changed successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            //Hide the Grid View
            this.upItems.Visible = true;
            this.divSrchFields.Visible = false;

            //Show the Add New Deal here

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gvViewDeals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (start <= 9)
            {           //ctl00_ContentPlaceHolder1_gvViewDeals_ctl03_RowLevelCheckBox
                strIDs += "*ctl00_ContentPlaceHolder1_gvViewDeals_ctl0" + start + "_RowLevelCheckBox";
            }
            else
            {
                strIDs += "*ctl00_ContentPlaceHolder1_gvViewDeals_ctl" + start + "_RowLevelCheckBox";
            }

            start++;
            hiddenIds.Text = strIDs;
        }
        catch (Exception ex)
        {
            pnlForm.Visible = false;
            upItems.Visible = true;
            this.divSrchFields.Visible = false;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #endregion

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            SearchhDealInfoByDifferentParams();
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

    private void SearchhDealInfoByDifferentParams()
    {


        if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
        {
            gvViewDeals.PageSize = Convert.ToInt32(Session["ddlPage"]);
        }
        else
        {
            gvViewDeals.PageSize = Misc.pageSize;
            Session["ddlPage"] = Misc.pageSize;
        }
       
        string strQuery = "";

        try
        {
            strQuery = "SELECT ";
            strQuery += " [deals].[dealId]";
            strQuery += ",[deals].[title]";
            strQuery += ",[deals].[restaurantId]";
            strQuery += ",[deals].[sellingPrice]";
            strQuery += ",[deals].[valuePrice]";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId)) ,0) 'TotalOrders'";
            strQuery += ",isnull((SELECT count(vID) from sampleVouchers where sampleVouchers.dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId) and sampleVouchers.isused=0) ,0) 'unUsedVoucher'";
            strQuery += ",isnull((SELECT count(vID) from sampleVouchers where sampleVouchers.dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId) and sampleVouchers.isused=1) ,0) 'UsedVoucher'";
            strQuery += ",isnull((SELECT count(vID) from sampleVouchers where sampleVouchers.dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId)) ,0) 'TotalVoucher'";
            strQuery += ",[restaurant].[restaurantBusinessName]";
            strQuery += ",case when [deals].[dealStatus]=1 then 'Yes' else 'No' end as 'dealStatus'";
            strQuery += ",dealStatus as 'dealStatus1'";
            //strQuery += ",[city].[cityName]";
            //strQuery += ",[dealCity].[cityId]";
            strQuery += ",isnull(dealSlot,1) dealSlot";
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
            //strQuery += "INNER join dealCity On dealCity.[dealId]= deals.[dealId] ";
            //strQuery += "INNER join city On dealCity.[cityId]= city.[cityId] ";
            strQuery += "where restaurant.[restaurantId]= deals.[restaurantId] ";

            if (txtTitleSearch.Text.Trim() != "")
            {
                strQuery += " and deals.title like '%" + txtTitleSearch.Text.Trim() + "%'";
            }
           
            //Get the Deal
            if (this.ddlSearchStatus.SelectedValue == "started")
            {
                strQuery += " and dealEndTime >= getdate()";
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

            strQuery += " order by dealSlot";

           // this.gvViewDeals.PageIndex = 0;

            ViewState["Query"] = strQuery;

            DataTable dtDeals = Misc.search(strQuery);

           // this.gvViewDeals.PageIndex = 0;
            this.gvViewDeals.DataSource = dtDeals;
            this.gvViewDeals.DataBind();


            Label lblPageCount = (Label)gvViewDeals.BottomPagerRow.FindControl("lblPageCount");
            Label lblTotalRecords = (Label)gvViewDeals.BottomPagerRow.FindControl("lblTotalRecords");
            string strTotalOrders = "";
            strTotalOrders = dtDeals.Rows.Count.ToString();
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
        catch (Exception ex)
        {
            //Hide the Update Deal Info here

            //Show All Deal Info into Grid View here
            this.upItems.Visible = true;
            this.divSrchFields.Visible = false;


            lblMessage.Text = ex.ToString();
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

    protected void gvViewDeals_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            if (e.NewPageIndex == gvViewDeals.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gvViewDeals.PageIndex = e.NewPageIndex;
            this.SearchhDealInfoByDifferentParams();
            TextBox txtPage = (TextBox)gvViewDeals.BottomPagerRow.Cells[0].FindControl("txtPage");
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
    #region Event of dropdown to take to selected page

    #region Event to take to required page
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            TextBox txtPage = (TextBox)gvViewDeals.BottomPagerRow.Cells[0].FindControl("txtPage");
            int intPageindex = 0;
            if (txtPage.Text != null && txtPage.Text.ToString() != "")
            {
                intPageindex = Convert.ToInt32(txtPage.Text);
                if (intPageindex > 0)
                {
                    intPageindex--;
                }
            }

            if (intPageindex < gvViewDeals.PageCount && intPageindex > 0)
            {
                gvViewDeals.PageIndex = intPageindex;
            }
            else
            {
                gvViewDeals.PageIndex = 0;
            }




            if (gvViewDeals.PageIndex == gvViewDeals.PageCount - 1)
            {
                displayNext = false;
                displayPrevious = true;
            }

            else if (gvViewDeals.PageIndex == 0)
            {
                displayPrevious = false;
                displayNext = true;
            }
            else
            {
                displayPrevious = true;
                displayNext = true;
            }
            SearchhDealInfoByDifferentParams();
            TextBox txtPage2 = (TextBox)gvViewDeals.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage2.Text = (gvViewDeals.PageIndex + 1).ToString();

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
            SearchhDealInfoByDifferentParams();
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
  
  
}