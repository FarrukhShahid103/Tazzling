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

public partial class admin_Orderpayment : System.Web.UI.Page
{
    public string strIDs = "";
    public int start = 2;
    public int NewDealID;
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
                    GetAndSetRestaurantInfo();                    
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

    protected void gvViewDeals_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {          
            int iDealId = Convert.ToInt32(this.gvViewDeals.DataKeys[e.NewEditIndex].Value);

            Label lblCityIDHidden = (Label)gvViewDeals.Rows[e.NewEditIndex].FindControl("lblCityIDHidden");
            if (lblCityIDHidden != null)
            {
                hfCityID.Value = lblCityIDHidden.Text.Trim();
            }
            this.upItems.Visible = false;
            this.divSrchFields.Visible = false;

            //Show the Add New Deal here
            this.divAddNewDeal.Visible = true;

            //Get and Set the Deals Info
            GetAndShowDealInfoByDealId(iDealId);

            //Change the Image URL of the Save button
            this.btnImgSave.ImageUrl = "~/admin/images/btnUpdate.jpg";

            this.btnImgSave.ToolTip = "Update Deal Info";

            //Set the Deal ID for updating the Deal info
            this.hfDealId.Value = iDealId.ToString();

            //Show the All Deals
            this.lblDealInfoHeading.Text = "Update Deal Info";

            //Hide the Image and Text message
            this.imgGridMessage.Visible = false;
            this.lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            
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
            this.divAddNewDeal.Visible = false;

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

    private void GetAndShowDealInfoByDealId(int iDealId)
    {
        try
        {
            BLLDeals objBLLDeals = new BLLDeals();

            //Set the Deal Id here
            objBLLDeals.DealId = iDealId;
            try
            {
                objBLLDeals.cityId = Convert.ToInt32(hfCityID.Value.Trim());
            }
            catch (Exception ex)
            { }

            DataTable dtDeals = objBLLDeals.getDealForPaymentFormByDealId();

            if ((dtDeals != null) && (dtDeals.Rows.Count > 0))
            {
                lblBusinessName.Text= dtDeals.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblBusinessPaymentTitle.Text = dtDeals.Rows[0]["businessPaymentTitle"].ToString().Trim();
                lblBusinessPaymentAddress.Text = dtDeals.Rows[0]["restaurantpaymentAddress"].ToString().Trim();
                lblCommission.Text = dtDeals.Rows[0]["commission"].ToString().Trim();
                lblBusinessEmailAddress.Text = dtDeals.Rows[0]["email"].ToString().Trim();
                if (dtDeals.Rows[0]["TotalShipping"] != null 
                    && dtDeals.Rows[0]["TotalShipping"].ToString().Trim() != "" 
                    && Convert.ToDouble(dtDeals.Rows[0]["TotalShipping"].ToString().Trim())>0)
                {
                    trShippingAndTax.Visible = true;
                    lblShippingAndTax.Text = "$" + dtDeals.Rows[0]["TotalShipping"].ToString().Trim() + " CAD";
                }
                try
                {
                    lblDealEndedDate.Text = Convert.ToDateTime(dtDeals.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                { }
                //hfDealID.Value = dtDeals.Rows[0]["dealId"].ToString().Trim();
                hfBusinessId.Value = dtDeals.Rows[0]["restaurantId"].ToString().Trim();
                lblDealName.Text = dtDeals.Rows[0]["title"].ToString().Trim();
                lblDealSoldSummery.Text = "Successful Orders: " + dtDeals.Rows[0]["SuccessfulOrder"].ToString().Trim() + "<br>" + "Cancelled Orders:" + dtDeals.Rows[0]["CancelledOrder"].ToString().Trim() + "<br>Refunded Order" + dtDeals.Rows[0]["RefundedOrder"].ToString().Trim();
                ddlPaymentStatus.SelectedValue = dtDeals.Rows[0]["paymentStatus"].ToString().Trim();
                txtDealNote.Text = dtDeals.Rows[0]["dealNote"].ToString().Trim();                
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    #endregion

    #region"Update Deal Info By Deal ID"

    private void UpdateDealInfoByDealId(string strDealId)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }   
    #endregion

    protected void btnImgCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            //Show the Grid View
            this.upItems.Visible = true;
            this.divSrchFields.Visible = true;

            //Hide the Add New Deal here
            this.divAddNewDeal.Visible = false;

            //View All Deals
            this.lblDealInfoHeading.Text = "View All Deals";

            //Hide the Image and Text message
            this.imgGridMessage.Visible = false;
            this.lblMessage.Text = "";

        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
     
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
            strQuery += ",[deals].[finePrint]";
            strQuery += ",[deals].[dealHightlights]";
            strQuery += ",[deals].[description]";
            strQuery += ",[deals].[sellingPrice]";
            strQuery += ",[deals].[valuePrice]";
            strQuery += ",paymentStatus";
            strQuery += ",dealNote";            
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Successful') ,0) 'SuccessfulOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Cancelled') ,0) 'CancelledOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Pending') ,0) 'PendingOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Refunded') ,0) 'RefundedOrder'";
            strQuery += ",isnull((SELECT count(dealOrderDetail.detailID) FROM dealOrderDetail INNER JOIN dealOrders ON dealOrderDetail.dOrderID = dealOrders.dOrderID where dealOrders.dealid=[deals].dealid and markUsed=0 and dealOrders.status = 'Successful'),0) 'UsedCount'";
            strQuery += ",dealStartTimeC as 'dealStartTime'";
            strQuery += ",dealEndTimeC as 'dealEndTime'";
            strQuery += ",[deals].[images]";
            strQuery += ",[deals].[dealDelMinLmt]";
            strQuery += ",[deals].[dealDelMaxLmt]";
            strQuery += ",case when [deals].[dealStatus]=1 then 'Yes' else 'No' end as 'dealStatus'";
            strQuery += ",dealStatus as 'dealStatus1'";
            strQuery += ",[deals].[createdBy]";
            strQuery += ",[deals].[createdDate]";
            strQuery += ",[deals].[modifiedBy]";
            strQuery += ",[deals].[modifiedDate]";
            strQuery += ",[deals].[minOrdersPerUser]";
            strQuery += ",[deals].[maxOrdersPerUser]";
            strQuery += ",[restaurant].[restaurantBusinessName]";
            strQuery += ",[restaurant].[restaurantpaymentAddress]";
            strQuery += ",[city].[cityName]";
            strQuery += ",[city].[cityid]";
            strQuery += ",isnull(dealSlotC,1) as 'dealSlot'";
            strQuery += ",isnull([deals].affComm,0) affComm";
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
            strQuery += "INNER join dealcity On dealcity.[dealid]= deals.[dealid] ";
            strQuery += "INNER join city On dealCity.cityId= city.[cityId] ";
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
                strQuery += " and dealStartTimeC <= getdate() and dealEndTimeC >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "upcoming")
            {
                strQuery += " and dealStartTimeC >= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "expired")
            {
                strQuery += " and dealEndTimeC <= getdate()";
            }
            else if (this.ddlSearchStatus.SelectedValue == "all")
            {
                strQuery += "";
            }

            strQuery += " order by dealStartTimeC desc";

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
            this.divAddNewDeal.Visible = false;

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

    private void getDealDetailByID(string strDealID)
    {
        string strQuery = "";

        try
        {
            strQuery = "SELECT ";
            strQuery += " [dealId]";
            strQuery += ",[deals].[restaurantId]";
            strQuery += ",[deals].[title]";
            strQuery += ",[deals].[finePrint]";
            strQuery += ",paymentStatus";
            strQuery += ",dealNote";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Successful') ,0) 'SuccessfulOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Cancelled') ,0) 'CancelledOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Pending') ,0) 'PendingOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId in (select dealid from deals as dealsInner where dealsInner.dealid = [deals].dealId or dealsInner.parentdealid = [deals].dealId) and [dealOrders].[status]='Refunded') ,0) 'RefundedOrder'";
            strQuery += ",isnull((SELECT count(dealOrderDetail.detailID) FROM dealOrderDetail INNER JOIN dealOrders ON dealOrderDetail.dOrderID = dealOrders.dOrderID where dealOrders.dealid=[deals].dealid and markUsed=0 and dealOrders.status = 'Successful'),0) 'UsedCount'";            
            strQuery += ",[deals].[dealEndTime]";
            strQuery += ",case when [deals].[dealStatus]=1 then 'Yes' else 'No' end as 'dealStatus'";
            strQuery += ",dealStatus as 'dealStatus1'";
            strQuery += ",[restaurant].[restaurantBusinessName]";
            strQuery += ",[city].[cityName]";
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
            strQuery += "INNER join city On restaurant.[cityId]= city.[cityId] ";
            strQuery += "where Deals.[dealId]= "+ strDealID;            
            //ViewState["Query"] = strQuery;

            DataTable dtDeals = Misc.search(strQuery);

            if ((dtDeals != null) &&
                (dtDeals.Columns.Count > 0) &&
                (dtDeals.Rows.Count > 0))
            {
                this.gvViewDeals.DataSource = dtDeals;
                this.gvViewDeals.DataBind();
            }           
        }
        catch (Exception ex)
        {
            //Hide the Update Deal Info here
            this.divAddNewDeal.Visible = false;

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

    protected void btnImgSave_Click(object sender, ImageClickEventArgs e)
    {

        if (hfDealId.Value.ToString().Trim() != "" && hfDealId.Value.ToString().Trim() != "0")
        {
            BLLDeals objBLLDeals = new BLLDeals();

            //Set the Deal Id here
            objBLLDeals.DealId = Convert.ToInt64(hfDealId.Value.ToString().Trim());
            objBLLDeals.DealNote = txtDealNote.Text.Trim();
            objBLLDeals.updateDealNoteByDealId();

            if (hfBusinessId.Value.ToString().Trim() != "" && hfBusinessId.Value.ToString().Trim() != "0")
            {
                BLLRestaurant objRest = new BLLRestaurant();
                objRest.restaurantId = Convert.ToInt64(hfBusinessId.Value.ToString().Trim());
                if (ViewState["userID"] != null)
                {
                    objRest.modifiedBy = Convert.ToInt64(ViewState["userID"].ToString().Trim());
                }
                objRest.paymentStatus = ddlPaymentStatus.SelectedValue.ToString().Trim();
                objRest.updateRestaurantPaymentStatusByResID();
            }
            SearchhDealInfoByDifferentParams();
            this.divAddNewDeal.Visible = false;
            //Show All Deal Info into Grid View here
            this.upItems.Visible = true;
            this.divSrchFields.Visible = true;
            lblMessage.Text = "Deal update successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/checked.png";            
        }
    }

}