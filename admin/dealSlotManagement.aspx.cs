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

public partial class admin_dealSlotManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            //Get the Admin User Session here
           // bindCity();
            if (Session["user"] != null)
            {

                try
                {
                    GetAndSetRestaurantInfo();
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

    protected void bindCity()
    {
        try
        {

            //ddlSearchCity.DataSource = Misc.getAllCitiesWithProvinceAndCountryInfoByCountryID(2);
            //ddlSearchCity.DataTextField = "cityName";
            //ddlSearchCity.DataValueField = "cityId";
            //ddlSearchCity.DataBind();
            //ddlSearchCity.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        {
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
            strQuery += " [deals].[dealId]";
            strQuery += ",[deals].[title]";                                                
            strQuery += ",[deals].[restaurantId]";                                                
            strQuery += ",[deals].[sellingPrice]";
            strQuery += ",[deals].[valuePrice]";
            strQuery += ",dealStartTimeC";
            strQuery += ",dealEndTimeC";
            strQuery += ",voucherExpiryDate";        
            strQuery += ",[restaurant].[restaurantBusinessName]";
            strQuery += ",case when [deals].[dealStatus]=1 then 'Yes' else 'No' end as 'dealStatus'";
            strQuery += ",dealStatus as 'dealStatus1'";
            strQuery += ",[city].[cityName]";
            strQuery += ",[dealCity].[cityId]";
            strQuery += ",isnull(dealSlotC,1) dealSlot";            
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
            strQuery += "INNER join dealCity On dealCity.[dealId]= deals.[dealId] ";
            strQuery += "INNER join city On dealCity.[cityId]= city.[cityId] ";
            strQuery += "where restaurant.[restaurantId]= deals.[restaurantId] and [deals].parentdealId = 0 ";

            if (this.ddlSrchBusinessName.SelectedValue != "All")
            {
                strQuery += " and restaurant.restaurantId =" + this.ddlSrchBusinessName.SelectedValue.ToString().Trim();
            }

            if (ddlSearchCity.SelectedIndex > 0)
            {
                strQuery += " and dealCity.cityID=" + this.ddlSearchCity.SelectedValue.ToString().Trim();
            }
            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [deals].[title] like '%" + txtSrchDealTitle.Text.Trim() + "%' ";
            }

            //Get the Deal
            if (this.ddlSearchStatus.SelectedValue == "started")
            {
                strQuery += " and dealEndTimeC >= getdate()";
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

            strQuery += " order by dealSlotC";

            this.gvViewDeals.PageIndex = 0;

            ViewState["Query"] = strQuery;

            DataTable dtDeals = Misc.search(strQuery);

            if ((dtDeals != null) &&
                (dtDeals.Columns.Count > 0) &&
                (dtDeals.Rows.Count > 0))
            {
                this.gvViewDeals.DataSource = dtDeals;
                this.gvViewDeals.DataBind();
                try
                {
                    if (Convert.ToInt32(dtDeals.Rows[dtDeals.Rows.Count - 1]["dealSlot"]) == 150)
                    {
                        btnUpdateSlot.Enabled = false;
                    }
                    else
                    {
                        btnUpdateSlot.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
 
                }
            }
            else
            {
                this.gvViewDeals.PageIndex = 0;

                this.gvViewDeals.DataSource = null;
                this.gvViewDeals.DataBind();
                btnUpdateSlot.Enabled = false;               
            }
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
      
    protected void btnUpdateSlot_Click(object sender, EventArgs e)
    {
        if (ViewState["Query"] != null && ViewState["Query"].ToString().Trim() != "")
        {
            BLLDeals objDeal = new BLLDeals();
            DataTable dtResult = Misc.search(ViewState["Query"].ToString().Trim());            
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {                    
                    objDeal.DealId = Convert.ToInt64(dtResult.Rows[i]["DealId"].ToString().Trim());
                    objDeal.cityId = Convert.ToInt32(dtResult.Rows[i]["cityId"].ToString().Trim());
                    objDeal.DealSlot = Convert.ToInt32(dtResult.Rows[i]["DealSlot"].ToString().Trim()) + 1;
                    objDeal.updateDealSlotByDealId();
                }
            }
            SearchhDealInfoByDifferentParams();
            
        }
    }
}