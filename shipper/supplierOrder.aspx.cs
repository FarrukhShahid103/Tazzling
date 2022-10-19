using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GecLibrary;
using System.Web.UI.HtmlControls;

using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Reflection;

public partial class supplierOrder : System.Web.UI.Page
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
                    SearchhDealInfoByDifferentParams();
                }
                catch (Exception ex)
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
            strQuery += ",sizeText";
            strQuery += ",sizeID";
            strQuery += ",isnull((SELECT sum(supplierOrdersQty) FROM  supplierOrders where supplierOrders.productID = [products].productID and (supplierOrders.sizeID=productSize.sizeID or supplierOrders.sizeID is null)) ,0) 'SupplierOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [products].productID and [dealOrders].[status]='Successful' and (dealOrders.size=productSize.sizeText or dealOrders.size is null or dealOrders.size='') and dealOrders.isDeleted=0) ,0) 'SuccessfulOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [products].productID and [dealOrders].[status]<>'Successful' and (dealOrders.size=productSize.sizeText or dealOrders.size is null or dealOrders.size='') and dealOrders.isDeleted=0) ,0) 'RefundedOrder'";
            strQuery += ",case when [products].isActive=1 then 'Yes' else 'No' end as 'dealStatus'";            
            strQuery += ",[products].[createdDate]";            
            strQuery += " FROM ";
            strQuery += "[products] ";

            strQuery += "left outer join productSize On productSize.productID= [products].productID ";
            strQuery += "INNER join campaign On campaign.[campaignID]= [products].[campaignID] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= campaign.[restaurantId] ";
            strQuery += "where restaurant.[restaurantId]= campaign.[restaurantId] and [products].tracking = 1 ";
            strQuery += " and restaurant.restaurantId = 31";
            
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

    protected void gvViewDeals_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            BLLSupplierOrders objSupplier = new BLLSupplierOrders();
            //int idetailOrderID = Convert.ToInt32(pageGrid.DataKeys[e.NewEditIndex].Value);
            TextBox txtSendingOrdered = (TextBox)gvViewDeals.Rows[e.NewEditIndex].FindControl("txtSendingOrdered");
            Label lblProductID = (Label)gvViewDeals.Rows[e.NewEditIndex].FindControl("lblProductID");
            Label lblSizeID = (Label)gvViewDeals.Rows[e.NewEditIndex].FindControl("lblSizeID");
            objSupplier.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
            objSupplier.productID = Convert.ToInt64(lblProductID.Text.Trim().ToString());
            long sizeID=0;
            long.TryParse(lblSizeID.Text.Trim(),out sizeID);
            objSupplier.sizeID = sizeID;
            objSupplier.supplierOrdersQty = Convert.ToInt64(txtSendingOrdered.Text.Trim());
            objSupplier.createSupplierOrders();
            SearchhDealInfoByDifferentParams();
            lblMessage.Text = "Orders has been saved successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        { }
    }
                             
}
