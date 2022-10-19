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


public partial class trackingdealManagement : System.Web.UI.Page
{
    public string strIDs = "";
    public int start = 2;
    public int NewDealID;    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {            
            //Get the Admin User Session here
            if (Session["user"] != null)
            {

                try
                {
                    //Fill the Restaurant Info dropdownlist
                    GetAndSetRestaurantInfo();
                
                    SearchhDealInfoByDifferentParams();

                }
                catch (Exception ex)
                {                    
                 
                }

            }
            else
            {
                Response.Redirect(ResolveUrl("~/shipper/default.aspx"), false);
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

                ddlSrchBusinessName.SelectedValue = "31";
            }
        }
        catch (Exception ex)
        { string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771."; }
    }

    #endregion
  
    #region "Grid View Events"   
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
 
    private void SearchhDealInfoByDifferentParams()
    {
        string strQuery = "";

        try
        {
            strQuery = "SELECT ";
            strQuery += " [products].[productID],";
            strQuery += "[campaign].[restaurantId],";
            strQuery += "[products].[title],";
            strQuery += "restaurant.restaurantBusinessName,";
            strQuery += "[campaign].campaignEndTime,";
            strQuery += "[campaign].campaignStartTime";
            strQuery += ",isnull((SELECT count(dealOrderDetail.detailID) FROM dealOrderDetail INNER JOIN dealOrders ON dealOrderDetail.dOrderID = dealOrders.dOrderID where (trackingNumber='' OR trackingNumber is null) and dealOrders.status = 'Successful' and dealOrders.dealid = [products].productID and dealOrders.isDeleted=0 and dealOrders.resendOrders=0),0) 'RemainTrackingOrders'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [products].productID and [dealOrders].[status]='Successful' and dealOrders.isDeleted=0 and dealOrders.resendOrders=0) ,0) 'SuccessfulOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [products].productID and [dealOrders].[status]='Successful' and dealOrders.isDeleted=0 and dealOrders.resendOrders=1) ,0) 'ResendOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [products].productID and [dealOrders].[status]<>'Successful'  and dealOrders.isDeleted=0 and dealOrders.resendOrders=0) ,0) 'RefundedOrder'";
            strQuery += ",case when [products].isActive=1 then 'Yes' else 'No' end as 'dealStatus'";            
            strQuery += ",[products].[createdDate]";            
            strQuery += " FROM ";
            strQuery += "[products] ";

            strQuery += "INNER join campaign On campaign.[campaignID]= [products].[campaignID] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= campaign.[restaurantId] ";
            strQuery += "where restaurant.[restaurantId]= campaign.[restaurantId] and [products].tracking = 1 ";

            if (this.ddlSrchBusinessName.SelectedValue != "All")
            {
                strQuery += " and restaurant.restaurantId =" + this.ddlSrchBusinessName.SelectedValue.ToString().Trim();
            }

            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [products].[title] like '%" + txtSrchDealTitle.Text.Trim() + "%' ";
            }

            //Get the Deal
            if (this.ddlSearchStatus.SelectedValue == "started")
            {
                strQuery += " and campaignStartTime <= getdate() and campaignEndTime >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "upcoming")
            {
                strQuery += " and campaignStartTime >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "expired")
            {
                strQuery += " and campaignEndTime <= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "all")
            {
                strQuery += "";
            }

            strQuery += " order by campaignEndTime desc";

            this.gvViewDeals.PageIndex = 0;
            
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
        if (e.CommandName.Trim() == "DownloadExcel")
        {
            DataTable dtUser = searchForExcelFileDownload(e.CommandArgument.ToString());
            DataTable dtDealOrders = new DataTable("dealOrders");
            DataColumn Sr = new DataColumn("Sr.");
            DataColumn Title = new DataColumn("Title");
            DataColumn Customer = new DataColumn("Customer");
            DataColumn DealCode = new DataColumn("Deal Code");
            DataColumn Telephone = new DataColumn("Telephone");
            DataColumn Note = new DataColumn("Note");
            DataColumn Address = new DataColumn("Address");
            DataColumn Track = new DataColumn("Track #");
            
            dtDealOrders.Columns.Add(Sr);
            dtDealOrders.Columns.Add(Title);
            dtDealOrders.Columns.Add(Customer);
            dtDealOrders.Columns.Add(DealCode);
            dtDealOrders.Columns.Add(Telephone);
            dtDealOrders.Columns.Add(Note);
            dtDealOrders.Columns.Add(Address);
            dtDealOrders.Columns.Add(Track);

            DataRow dRow;
            GECEncryption objEnc = new GECEncryption();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    dRow = dtDealOrders.NewRow();
                    dRow["Sr."] = i + 1;
                    dRow["Title"] = dtUser.Rows[i]["title"].ToString().Trim();
                    dRow["Customer"] = dtUser.Rows[i]["Name"].ToString().Trim();
                    dRow["Deal Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                    dRow["Telephone"] = dtUser.Rows[i]["Telephone"].ToString().Trim();
                    dRow["Note"] = dtUser.Rows[i]["shippingNote"].ToString().Trim();
                    dRow["Address"] = dtUser.Rows[i]["ShippingAddress"].ToString().Trim();                    
                    dRow["Track #"] = dtUser.Rows[i]["trackingNumber"].ToString().Trim();
                    dtDealOrders.Rows.Add(dRow);
                }
                ExportToUser(dtDealOrders, "DealTrackingOrders.xls");
            }

        }
    }

    private DataTable searchForExcelFileDownload(string dealid)
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";
            //strQuery += " ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber'";
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";
            strQuery += " ,products.title";
            strQuery += " ,userInfo.userName";
            //strQuery += " ,dealOrders.createdDate";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + rtrim(userInfo.lastName) as 'Name'";
            strQuery += " ,[dealOrders].[status]";
            strQuery += ",userShippingInfo.Name as 'shippingName'";
            strQuery += ",dealOrders.shippingInfoId";
            strQuery += ",userShippingInfo.Address+', '+userShippingInfo.City+', '+userShippingInfo.State+', '+userShippingInfo.ZipCode+', '+userShippingInfo.shippingCountry as 'ShippingAddress'";
            strQuery += ",userShippingInfo.Telephone";
            strQuery += ",userShippingInfo.shippingNote";
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " ,[dealOrderDetail].[detailID]";
            strQuery += " ,[dealOrderDetail].[isRedeemed]";
            strQuery += " ,[dealOrderDetail].[trackingNumber]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";
            strQuery += " ,[dealOrderDetail].[markUsed]";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join products on (products.productID = dealOrders.dealId)";
            strQuery += " left outer join userShippingInfo on (userShippingInfo.shippingInfoId = dealOrders.shippingInfoId)";
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            strQuery += " where (trackingNumber='' OR trackingNumber is null) and dealOrders.status='Successful' and dealOrders.isDeleted=0 and dealOrders.resendOrders=0 and ";
            strQuery += " products.productID =" + dealid ;
            strQuery += " Order by dealOrders.dOrderID asc";
            // this.pageGrid.PageIndex = 0;
            // GridView1.PageIndex = 0;

            //Get & Set the DataTable here
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

    private void ExportToUser(DataTable table, string strFileName)
    {
        GridView gv = new GridView();
        gv.Font.Size = 12;
        gv.HeaderStyle.Font.Size = 13;
        gv.HeaderStyle.Font.Bold = true;

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
                             
}