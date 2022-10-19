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


public partial class campaignManagement : System.Web.UI.Page
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
                                lblMessage.Text = "New Campaign has been added successfully.";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "images/Checked.png";
                                lblMessage.ForeColor = System.Drawing.Color.Black;
                            }
                            if (Request.QueryString["Res"].ToString().Trim() == "Update")
                            {
                                lblMessage.Text = "Campaign Info has been updated successfully.";
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
            strQuery += " campaignID,restaurantId,campaignTitle,campaignStartTime,campaignEndTime";
            strQuery += " ,creationDate,shipCanada,shipUSA";    
            strQuery += " FROM ";
            strQuery += "campaign ";
            
            if (this.ddlSrchBusinessName.SelectedValue != "All")
            {
                strQuery += " where campaign.restaurantId =" + this.ddlSrchBusinessName.SelectedValue.ToString().Trim();
            }

            if (txtSrchCampaignTitle.Text.Trim() != "")
            {
                if (strQuery.Contains("where"))
                {
                    strQuery += " and [campaign].[campaignTitle] like '%" + txtSrchCampaignTitle.Text.Trim() + "%' ";
                }
                else
                {
                    strQuery += " where [campaign].[campaignTitle] like '%" + txtSrchCampaignTitle.Text.Trim() + "%' ";
                }
            }

            //Get the Deal
            if (this.ddlSearchStatus.SelectedValue == "started")
            {
                if (strQuery.Contains("where"))
                {
                    strQuery += " and campaignStartTime <= getdate() and campaignEndTime >= getdate()";
                }
                else
                {
                    strQuery += " where campaignStartTime <= getdate() and campaignEndTime >= getdate()";
                }                
            }
            else if (this.ddlSearchStatus.SelectedValue == "upcoming")
            {
                if (strQuery.Contains("where"))
                {
                    strQuery += " and campaignStartTime >= getdate()";
                }
                else
                {
                    strQuery += " where campaignStartTime >= getdate()";
                }        
            }
            else if (this.ddlSearchStatus.SelectedValue == "expired")
            {
                if (strQuery.Contains("where"))
                {
                    strQuery += " and campaignEndTime <= getdate()";
                }
                else
                {
                    strQuery += " where campaignEndTime <= getdate()";
                }                                        
            }
            else if (this.ddlSearchStatus.SelectedValue == "all")
            {
                strQuery += "";
            }

            strQuery += " order by campaignStartTime desc";

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

  
}