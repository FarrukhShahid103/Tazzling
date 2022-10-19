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
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net.Sockets;


public partial class admin_dealManagement : System.Web.UI.Page
{
    public string strIDs = "";
    public int start = 2;
    public int NewDealID;
    public static bool notExist = true;
    BLLDealOrders objOrders = new BLLDealOrders();
    
    protected void Page_Load(object sender, EventArgs e)
    {
              
        if (!IsPostBack)
        {
            notExist = true;           
            //Get the Admin User Session here
            if (Session["user"] != null)
            {
                if ((Request.QueryString["Mode"] != null) && (Request.QueryString["resID"] != null))
                {
                    try
                    {
                        if (Request.QueryString["Res"] != null && Request.QueryString["Res"].ToString().Trim() != "")
                        {
                            if (Request.QueryString["Res"].ToString().Trim() == "Add")
                            {
                                lblMessage.Text = "New Deal has been added successfully.";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "images/Checked.png";
                                lblMessage.ForeColor = System.Drawing.Color.Black;
                            }
                            if (Request.QueryString["Res"].ToString().Trim() == "Update")
                            {
                                lblMessage.Text = "Deal Info has been updated successfully.";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "images/Checked.png";
                                lblMessage.ForeColor = System.Drawing.Color.Black;
                            }
                        }                    
                        if ((Request.QueryString["Mode"] == "All") && (int.Parse(Request.QueryString["resID"].ToString()) > 0))
                        {
                            //Fill the Restaurant Info dropdownlist
                            GetAndSetRestaurantInfo();
                            //Set the Restaurant Id here
                            this.ddlSrchBusinessName.SelectedValue = Request.QueryString["resID"].ToString();
                            //GetAllDealInfoAndFillGrid();
                            SearchhDealInfoByDifferentParams();
                        }                            
                        else
                        {
                            //In case of Exception it will redirect you towrads the Business page
                            //Usually exception comes in that case where if "Mode" or "redID" contains invalid value
                            Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), false);
                        }
                    }
                    catch (Exception ex)
                    {
                        //In case of Exception it will redirect you towrads the Business page
                        //Usually exception comes in that case where if "Mode" or "redID" contains invalid value
                        Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), false);
                    }
                }
                else//If any one of "Mode" and "ResId" is Null then it will redirects you towards the Business Form
                {
                    Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), false);
                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
            }
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
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
    

    #region "Get & Set Restaurant Management"

    private void GetAndSetRestaurantInfo()
    {
        try
        {
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();

            DataTable dtResInfo = objBLLRestaurant.getAllResturantsForAdmin();

            if ((dtResInfo != null) && (dtResInfo.Rows.Count > 0))
            {
                DataView dv = new DataView(dtResInfo);
                dv.Sort = "restaurantBusinessName asc";
              
                //For the Search Restaurant Drop Down List                
                this.ddlSrchBusinessName.DataTextField = "restaurantBusinessName";
                this.ddlSrchBusinessName.DataValueField = "restaurantId";
                this.ddlSrchBusinessName.DataSource = dv;
                this.ddlSrchBusinessName.DataBind();
                this.ddlSrchBusinessName.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        { string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771."; }
    }

    #endregion
  
    #region "Grid View Events"
   
    protected void gvViewDeals_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int iDealId = Convert.ToInt32(this.gvViewDeals.DataKeys[e.RowIndex].Value);

            BLLDeals objBLLDeals = new BLLDeals();

            objBLLDeals.DealId = iDealId;

            int iDeal = objBLLDeals.deleteDealByDealId();

            //Get All Latest Deal Info Grid Info
            //GetAllDealInfoAndFillGrid();
            SearchhDealInfoByDifferentParams();

            lblMessage.Text = "Deal has been deleted successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            //
            gvViewDeals.PageIndex = 0;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
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
            this.divSrchFields.Visible = true;

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
            this.divSrchFields.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #endregion


    protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void btnDeleteSelected_Click(object sender, ImageClickEventArgs e)
    { }

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
        string strQuery = "";

        try
        {
            strQuery = "SELECT ";
            strQuery += " [deals].[dealId]";
            strQuery += ",[deals].[restaurantId]";
            strQuery += ",[deals].[title]";
            
            strQuery += ",[deals].[sellingPrice]";
            strQuery += ",[deals].[valuePrice]";
            strQuery += ",deals.voucherExpiryDate";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Successful') ,0) 'SuccessfulOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]<>'Successful') ,0) 'CancelledOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Pending') ,0) 'PendingOrder'";
            strQuery += ",isnull((SELECT count(dealOrderDetail.detailID) FROM dealOrderDetail INNER JOIN dealOrders ON dealOrderDetail.dOrderID = dealOrders.dOrderID where dealOrders.dealid=[deals].dealid and markUsed=0 and dealOrders.status = 'Successful'),0) 'UsedCount'";
            //strQuery += ",dealStartTimeC as 'dealStartTime'";
            //strQuery += ",dealEndTimeC as 'dealEndTime'";
            //strQuery += ",[deals].[images]";
            //strQuery += ",[deals].[dealDelMinLmt]";
            //strQuery += ",[deals].[dealDelMaxLmt]";
            strQuery += ",case when [deals].[dealStatus]=1 then 'Yes' else 'No' end as 'dealStatus'";
            strQuery += ",dealStatus as 'dealStatus1'";
            //strQuery += ",[deals].[createdBy]";
            strQuery += ",[deals].[createdDate]";
            //strQuery += ",[deals].[modifiedBy]";
            //strQuery += ",[deals].[modifiedDate]";
            //strQuery += ",[deals].[minOrdersPerUser]";
            //strQuery += ",[deals].[maxOrdersPerUser]";
            //strQuery += ",[restaurant].[restaurantBusinessName]";
            //strQuery += ",[city].[cityName]";
            //strQuery += ",isnull(dealSlotC,1) as 'dealSlot'";
            //strQuery += ",isnull([deals].affComm,0) affComm";
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";            
            //strQuery += "INNER join city On dealCity.cityId= city.[cityId] ";
            strQuery += "where restaurant.[restaurantId]= deals.[restaurantId] and [deals].parentdealId = 0 ";

            if (this.ddlSrchBusinessName.SelectedValue != "All")
            {
                strQuery += " and restaurant.restaurantId =" + this.ddlSrchBusinessName.SelectedValue.ToString().Trim();
            }

            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [deals].[title] like '%" + txtSrchDealTitle.Text.Trim() + "%' ";
            }

            //Get the Deal
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

            strQuery += " order by dealStartTime desc";

            this.gvViewDeals.PageIndex = 0;

            //ViewState["Query"] = strQuery;

            DataTable dtDeals = Misc.search(strQuery);

            if ((dtDeals != null) &&
                (dtDeals.Columns.Count > 0) &&
                (dtDeals.Rows.Count > 0))
            {
                this.gvViewDeals.DataSource = dtDeals;
                this.gvViewDeals.DataBind();
            }
            else
            {
                this.gvViewDeals.PageIndex = 0;

                this.gvViewDeals.DataSource = null;
                this.gvViewDeals.DataBind();

                ViewState["Query"] = null;
            }
        }
        catch (Exception ex)
        {
            //Hide the Update Deal Info here
            

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

    protected void gvViewDeals_Login(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName.Trim() == "Payment")
            {
                GECEncryption objEnc = new GECEncryption();
                objOrders = new BLLDealOrders();
                objOrders.dealId = Convert.ToInt64(e.CommandArgument);
                DataTable dtorders = objOrders.getTopTenOrdersWithCCDetailByDealID();

                if (dtorders != null && dtorders.Rows.Count > 0)
                {
                    for (int i = 0; i < dtorders.Rows.Count; i++)
                    {
                        try
                        {

                            objOrders = new BLLDealOrders();
                            objOrders.dOrderID = Convert.ToInt64(dtorders.Rows[i]["dOrderID"].ToString());
                            objOrders.status = "Successful";
                            //AddTastyGoCreditToReffer(Convert.ToInt32(dtorders.Rows[i]["userid"].ToString()), objOrders.dOrderID);                        
                            objOrders.changeDealOrderStatus();
                            try
                            {
                                string strOrderEmailBody = RenderOrderDetailHTML(dtorders.Rows[i]["dOrderID"].ToString());
                                Misc.SendEmail(dtorders.Rows[i]["ccInfoDEmail"].ToString(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), ConfigurationManager.AppSettings["EmailSubjectForNewOrderForMember"].ToString().Trim(), strOrderEmailBody);
                                Thread.Sleep(700);
                            }
                            catch (Exception ex)
                            { }
                            //If ordered amount is greater than "0"
                            if (!AddTastyGoCreditToReffer(Convert.ToInt64(dtorders.Rows[i]["dOrderID"].ToString())))
                            {
                                float fOrderTotal = (dtorders.Rows[i]["totalAmt"] == DBNull.Value) ? 0 : float.Parse(dtorders.Rows[i]["totalAmt"].ToString());
                                if (fOrderTotal > 0)
                                    CalculateAffiliateCommissionByOrderId(int.Parse(objOrders.dOrderID.ToString()));
                            }
                            //}
                            Misc.createPDF(dtorders.Rows[i]["dOrderID"].ToString());
                        }
                        catch (Exception ex)
                        { }
                    }

                    SearchhDealInfoByDifferentParams();

                    lblMessage.Text = "Orders have been processed.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
            }
            else if (e.CommandName.Trim() == "SendReminder")
            {

                ThreadStart starter = delegate { DealExpiryReminder(e.CommandArgument.ToString()); };
                new Thread(starter).Start();
                lblMessage.Text = "Reminder has been sent to all.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            else if (e.CommandName.Trim() == "DownloadExcel")
            {
                string[] strData = e.CommandArgument.ToString().Split(',');
                DataTable dtUser = SearchhDealInfoByDifferentParams(strData[0].Trim());
                DataTable dtDealOrders = new DataTable("dealOrders");
                if (dtUser != null && dtUser.Rows.Count > 0
                    && dtUser.Rows[0]["shippingAndTax"].ToString().Trim() != ""
                    && Convert.ToBoolean(dtUser.Rows[0]["shippingAndTax"].ToString()))
                {
                    DataColumn Sr = new DataColumn("Sr.");
                    DataColumn Date = new DataColumn("Date");
                    DataColumn Customer = new DataColumn("Customer");
                    DataColumn Email = new DataColumn("Email");
                    DataColumn dealOrderCode = new DataColumn("Voucher Code");
                    DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                    DataColumn Status = new DataColumn("Status");
                    DataColumn Telephone = new DataColumn("Telephone");
                    DataColumn Address = new DataColumn("Address");
                    DataColumn Note = new DataColumn("Note");
                    dtDealOrders.Columns.Add(Sr);
                    dtDealOrders.Columns.Add(Date);
                    dtDealOrders.Columns.Add(Customer);
                    dtDealOrders.Columns.Add(Email);
                    dtDealOrders.Columns.Add(dealOrderCode);
                    dtDealOrders.Columns.Add(voucherSecurityCode);
                    dtDealOrders.Columns.Add(Status);
                    dtDealOrders.Columns.Add(Telephone);
                    dtDealOrders.Columns.Add(Address);
                    dtDealOrders.Columns.Add(Note);
                    DataRow dRow;
                    dRow = dtDealOrders.NewRow();
                    dRow["Sr."] = "Title:";
                    dRow["Date"] = dtUser.Rows[0]["Title"].ToString().Trim();
                    dRow["Customer"] = "";
                    dRow["Email"] = "";
                    dRow["Voucher Code"] = "";
                    dRow["Voucher Security Code"] = "";
                    dRow["Status"] = "";
                    dRow["Telephone"] = "";
                    dRow["Address"] = "";
                    dRow["Note"] = "";
                    dtDealOrders.Rows.Add(dRow);

                    dRow = dtDealOrders.NewRow();
                    dRow["Sr."] = "Business Name:";
                    dRow["Date"] = dtUser.Rows[0]["restaurantBusinessName"].ToString().Trim();
                    dRow["Customer"] = "";
                    dRow["Email"] = "";
                    dRow["Voucher Code"] = "";
                    dRow["Voucher Security Code"] = "";
                    dRow["Status"] = "";
                    dRow["Telephone"] = "";
                    dRow["Address"] = "";
                    dRow["Note"] = "";
                    dtDealOrders.Rows.Add(dRow);

                    try
                    {
                        dRow = dtDealOrders.NewRow();
                        DateTime dttemp = DateTime.Now;
                        if (strData[1].Trim() != "" && DateTime.TryParse(strData[1].Trim(), out dttemp))
                        {
                            dRow["Sr."] = "Campaign Ended On:";
                            dRow["Date"] = dttemp.ToString("MMM dd,yyyy");
                            dRow["Customer"] = "";
                            dRow["Email"] = "";
                            dRow["Voucher Code"] = "Total Sold:";
                            dRow["Voucher Security Code"] = strData[2].Trim();
                        }
                        else
                        {
                            dRow["Sr."] = "Total Sold:";
                            dRow["Date"] = strData[2].Trim();
                            dRow["Customer"] = "";
                            dRow["Email"] = "";
                            dRow["Voucher Code"] = "";
                            dRow["Voucher Security Code"] = "";
                        }
                        dRow["Status"] = "";
                        dRow["Telephone"] = "";
                        dRow["Address"] = "";
                        dRow["Note"] = "";
                        dtDealOrders.Rows.Add(dRow);
                    }
                    catch (Exception ex)
                    {
                    }
                    GECEncryption objEnc = new GECEncryption();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtUser.Rows.Count; i++)
                        {
                            dRow = dtDealOrders.NewRow();
                            dRow["Sr."] = i + 1;
                            try
                            {
                                dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                            }
                            catch (Exception ex)
                            {
                                dRow["Date"] = "";
                            }
                            dRow["Customer"] = dtUser.Rows[i]["Name3"].ToString().Trim();
                            dRow["Email"] = dtUser.Rows[i]["userName"].ToString().Trim();
                            dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                            dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                            dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                            dRow["Telephone"] = dtUser.Rows[i]["Telephone"].ToString().Trim();
                            dRow["Address"] = dtUser.Rows[i]["Address"].ToString().Trim();
                            dRow["Note"] = dtUser.Rows[i]["shippingNote"].ToString().Trim();
                            dtDealOrders.Rows.Add(dRow);
                        }
                        ExportToUser(dtDealOrders, "DealOrders.xls");
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
                        DataColumn Sr = new DataColumn("Sr.");
                        DataColumn Date = new DataColumn("Date");
                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn Email = new DataColumn("Email");
                        DataColumn dealOrderCode = new DataColumn("Voucher Code");
                        DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                        DataColumn Status = new DataColumn("Status");
                        DataColumn Telephone = new DataColumn("Telephone");
                        DataColumn Address = new DataColumn("Address");
                        dtDealOrders.Columns.Add(Sr);
                        dtDealOrders.Columns.Add(Date);
                        dtDealOrders.Columns.Add(Customer);
                        dtDealOrders.Columns.Add(Email);
                        dtDealOrders.Columns.Add(dealOrderCode);
                        dtDealOrders.Columns.Add(voucherSecurityCode);
                        dtDealOrders.Columns.Add(Status);
                        dtDealOrders.Columns.Add(Telephone);
                        dtDealOrders.Columns.Add(Address);

                        DataRow dRow;
                        dRow = dtDealOrders.NewRow();
                        dRow["Sr."] = "Title:";
                        dRow["Date"] = dtUser.Rows[0]["Title"].ToString().Trim();
                        dRow["Customer"] = "";
                        dRow["Email"] = "";
                        dRow["Voucher Code"] = "";
                        dRow["Voucher Security Code"] = "";
                        dRow["Status"] = "";
                        dRow["Telephone"] = "";
                        dRow["Address"] = "";
                        dtDealOrders.Rows.Add(dRow);

                        dRow = dtDealOrders.NewRow();
                        dRow["Sr."] = "Business Name:";
                        dRow["Date"] = dtUser.Rows[0]["restaurantBusinessName"].ToString().Trim();
                        dRow["Customer"] = "";
                        dRow["Email"] = "";
                        dRow["Voucher Code"] = "";
                        dRow["Voucher Security Code"] = "";
                        dRow["Status"] = "";
                        dRow["Telephone"] = "";
                        dRow["Address"] = "";
                        dtDealOrders.Rows.Add(dRow);

                        try
                        {
                            dRow = dtDealOrders.NewRow();
                            DateTime dttemp = DateTime.Now;
                            if (strData[1].Trim() != "" && DateTime.TryParse(strData[1].Trim(), out dttemp))
                            {
                                dRow["Sr."] = "Campaign Ended On:";
                                dRow["Date"] = dttemp.ToString("MMM dd,yyyy");
                                dRow["Customer"] = "";
                                dRow["Email"] = "";
                                dRow["Voucher Code"] = "Total Sold:";
                                dRow["Voucher Security Code"] = strData[2].Trim();
                            }
                            else
                            {
                                dRow["Sr."] = "Total Sold:";
                                dRow["Date"] = strData[2].Trim();
                                dRow["Customer"] = "";
                                dRow["Email"] = "";
                                dRow["Voucher Code"] = "";
                                dRow["Voucher Security Code"] = "";
                            }
                            dRow["Status"] = "";
                            dRow["Telephone"] = "";
                            dRow["Address"] = "";
                            dtDealOrders.Rows.Add(dRow);
                        }
                        catch (Exception ex)
                        {
                        }
                        GECEncryption objEnc = new GECEncryption();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtUser.Rows.Count; i++)
                            {
                                dRow = dtDealOrders.NewRow();
                                dRow["Sr."] = i + 1;
                                try
                                {
                                    dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                                }
                                catch (Exception ex)
                                {
                                    dRow["Date"] = "";
                                }
                                dRow["Customer"] = dtUser.Rows[i]["Name"].ToString().Trim();
                                dRow["Email"] = dtUser.Rows[i]["userName"].ToString().Trim();
                                dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dRow["Telephone"] = dtUser.Rows[i]["phoneNo"].ToString().Trim();
                                dRow["Address"] = dtUser.Rows[i]["Address2"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            ExportToUser(dtDealOrders, "DealOrders.xls");
                        }
                    }
                    else
                    {
                        DataColumn Sr = new DataColumn("Sr.");
                        DataColumn Date = new DataColumn("Date");
                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn Email = new DataColumn("Email");
                        DataColumn dealOrderCode = new DataColumn("Voucher Code");
                        DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                        DataColumn Status = new DataColumn("Status");

                        dtDealOrders.Columns.Add(Sr);
                        dtDealOrders.Columns.Add(Date);
                        dtDealOrders.Columns.Add(Customer);
                        dtDealOrders.Columns.Add(Email);
                        dtDealOrders.Columns.Add(dealOrderCode);
                        dtDealOrders.Columns.Add(voucherSecurityCode);
                        dtDealOrders.Columns.Add(Status);
                        DataRow dRow;
                        dRow = dtDealOrders.NewRow();
                        dRow["Sr."] = "Title: ";
                        dRow["Date"] = dtUser.Rows[0]["Title"].ToString().Trim();
                        dRow["Customer"] = "";
                        dRow["Email"] = "";
                        dRow["Voucher Code"] = "";
                        dRow["Voucher Security Code"] = "";
                        dRow["Status"] = "";
                        dtDealOrders.Rows.Add(dRow);

                        dRow = dtDealOrders.NewRow();
                        dRow["Sr."] = "Business Name: ";
                        dRow["Date"] = dtUser.Rows[0]["restaurantBusinessName"].ToString().Trim();
                        dRow["Customer"] = "";
                        dRow["Email"] = "";
                        dRow["Voucher Code"] = "";
                        dRow["Voucher Security Code"] = "";
                        dRow["Status"] = "";
                        dtDealOrders.Rows.Add(dRow);

                        try
                        {
                            dRow = dtDealOrders.NewRow();
                            DateTime dttemp = DateTime.Now;
                            if (strData[1].Trim() != "" && DateTime.TryParse(strData[1].Trim(), out dttemp))
                            {
                                dRow["Sr."] = "Campaign Ended On:";
                                dRow["Date"] = dttemp.ToString("MMM dd,yyyy");
                                dRow["Customer"] = "";
                                dRow["Email"] = "";
                                dRow["Voucher Code"] = "Total Sold:";
                                dRow["Voucher Security Code"] = strData[2].Trim();
                            }
                            else
                            {
                                dRow["Sr."] = "Total Sold:";
                                dRow["Date"] = strData[2].Trim();
                                dRow["Customer"] = "";
                                dRow["Email"] = "";
                                dRow["Voucher Code"] = "";
                                dRow["Voucher Security Code"] = "";
                            }
                            dRow["Status"] = "";
                            dtDealOrders.Rows.Add(dRow);
                        }
                        catch (Exception ex)
                        {
                        }

                        GECEncryption objEnc = new GECEncryption();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtUser.Rows.Count; i++)
                            {
                                dRow = dtDealOrders.NewRow();
                                dRow["Sr."] = i + 1;
                                try
                                {
                                    dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                                }
                                catch (Exception ex)
                                {
                                    dRow["Date"] = "";
                                }
                                dRow["Customer"] = dtUser.Rows[i]["Name2"].ToString().Trim();
                                dRow["Email"] = dtUser.Rows[i]["userName"].ToString().Trim();
                                dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            ExportToUser(dtDealOrders, "DealOrders.xls");
                        }
                    }
                }

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


    private DataTable SearchhDealInfoByDifferentParams(string strDealID)
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";
            strQuery += " ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber'";
            strQuery += " ,userInfo.userName";
            strQuery += " ,dealOrders.createdDate";
            strQuery += " ,rtrim(userCCInfo.ccInfoDFirstName) +' ' + rtrim(userCCInfo.ccInfoDLastName) as 'Name'";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + rtrim(userInfo.lastName) as 'Name2'";
            strQuery += " ,rtrim(userShippingInfo.Name) as 'Name3'";
            strQuery += " ,shippingAndTax";
            strQuery += " ,deals.restaurantId";
            strQuery += " ,(userShippingInfo.Address+', '+userShippingInfo.City+', '+userShippingInfo.State+', '+userShippingInfo.ZipCode +', '+userShippingInfo.shippingCountry) as 'Address'";
            strQuery += " ,(userCCInfo.ccInfoBAddress+', '+userCCInfo.ccInfoBCity+', '+userCCInfo.ccInfoBProvince+', '+userCCInfo.ccInfoBPostalCode) as 'Address2',userinfo.phoneNo";
            strQuery += " ,userShippingInfo.Telephone,Title,restaurantBusinessName";
            strQuery += ",userShippingInfo.shippingNote";
            strQuery += " ,[dealOrders].[status]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join deals on (deals.dealId = dealOrders.dealId)";
            strQuery += " inner join restaurant on (restaurant.restaurantId = deals.restaurantId)";
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join userCCInfo on (userCCInfo.ccInfoID = dealOrders.ccInfoID)";
            strQuery += " left outer join userShippingInfo on (userShippingInfo.shippingInfoId = dealOrders.shippingInfoId)";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            strQuery += " where ";
            strQuery += " (deals.dealId =" + strDealID.Trim() + " OR deals.parentDealId=" + strDealID.Trim();
            strQuery += ") and [dealOrders].[status]='Successful' Order by dealOrders.dOrderID asc";
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
     
    private bool CancelDeclinedAffiliateCommissionByOrderId(int OrderID, string strGainedType)
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

                //objBLLAffiliatePartnerGained.GainedType = "Cancelled";
                objBLLAffiliatePartnerGained.GainedType = strGainedType;

                //Add $1.85 % Amount of Total Amount into the User Account
                //objBLLAffiliatePartnerGained.GainedAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.GainedAmount = 0;
                //objBLLAffiliatePartnerGained.RemainAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.RemainAmount = 0;

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
                objBLLAffiliatePartnerGained.GainedAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.RemainAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));

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

                mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Your Deal Is Here!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
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

    public string RenderOrderDetailHTML(string OrderID)
    {
        DataTable dtOrdersInfo;
        objOrders = new BLLDealOrders();
        GECEncryption objEnc = new GECEncryption();
        objOrders.dOrderID = Convert.ToInt64(OrderID);
        dtOrdersInfo = objOrders.getDealOrderDetailByOrderID();
        StringBuilder sb = new StringBuilder();
        if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
        {
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Your Deal Is Here!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
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
            sb.Append("<div style='margin: 0px 0px 15px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>5. Go to the establishment, and redeem your voucher!</div>");
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
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");           
        }
        return sb.ToString();
    }

    public void DealExpiryReminder(string dealID)
    {


        objOrders = new BLLDealOrders();
        objOrders.dealId = Convert.ToInt64(dealID);
        DataTable dtorders = objOrders.getAllSuccessfulOrdersByDealID();
        if (dtorders != null && dtorders.Rows.Count > 0)
        {
            for (int i = 0; i < dtorders.Rows.Count; i++)
            {               
                DataTable dtOrdersInfo;
                objOrders = new BLLDealOrders();
                GECEncryption objEnc = new GECEncryption();
                objOrders.dOrderID = Convert.ToInt64(dtorders.Rows[i]["dOrderID"].ToString().Trim());
                dtOrdersInfo = objOrders.getVouchersDetailByOrderID();
                StringBuilder sb = new StringBuilder();
                if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0) &&( Convert.ToInt32(dtOrdersInfo.Rows[1][1].ToString())>0))
                {    
                    DateTime dtExpiryDate=DateTime.Now;
                    string strExpiryDate="";
                    if (DateTime.TryParse(dtorders.Rows[i]["voucherExpiryDate"].ToString(), out dtExpiryDate))
                    {
                        strExpiryDate = "before \"<b>" + dtExpiryDate.ToString("MM-dd-yyyy") + "</b>\"";
                    }
                    else
                    {
                        strExpiryDate = "soon";
                    }
                    sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Reminder from Tastygo</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
                    sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                    sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
                    sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                    sb.Append("<strong>Dear " + dtorders.Rows[i]["ccInfoDFirstName"].ToString().Trim() + " " + dtorders.Rows[i]["ccInfoDLastName"] + ",</strong></div>");
                    sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have " + dtOrdersInfo.Rows[1][1].ToString() + " un-used vouchers in your account.</strong></div>");
                    sb.Append("<div style='margin: 0px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>We would like to remind you that your \"<b>" + dtorders.Rows[i]["title"] + "</b>\" Voucher expires " + strExpiryDate + ".</div>");

                    sb.Append("<div style='margin: 5px 0px 5px 15px;border-top: 1px solid #eeeeee; font-family: Arial; font-size: 12px; line-height: 1.4em;'><div style='padding-top:10px;'>If you have used your voucher already, simply mark your voucher as used in your member area and you will not receive this notification again.</div></div>");
                    sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; font-size: 12px; line-height: 1.4em;'>Don't forget to send your vouchers to your friend if you haven't. Simply login -> member area and click \"My Gift\".</div>");
                    sb.Append("<div style='margin: 0px 0px 0px 15px; font-family: Arial; border-top: 1px solid #eeeeee;font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
                    sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;font-size: 12px; line-height: 1.3em;'>Here is the Business info for your voucher:</div>");                    
                    sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>" + dtorders.Rows[i]["restaurantBusinessName"] + "</div>");
                    sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>" + dtorders.Rows[i]["restaurantAddress"] + ", " + dtorders.Rows[i]["cityName"] + ", " + dtorders.Rows[i]["provinceName"] + ", " + dtorders.Rows[i]["zipCode"] + "</div>");
                    sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>" + dtorders.Rows[i]["phone"] + "</div>");                    
                    sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
                    sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
                    sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
                    sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;");
                    sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
                    sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
                    Misc.SendEmail(dtorders.Rows[i]["ccInfoDEmail"].ToString().Trim(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), "Reminder from Tastygo", sb.ToString());
                }                             
            }
        }
    }
  
}