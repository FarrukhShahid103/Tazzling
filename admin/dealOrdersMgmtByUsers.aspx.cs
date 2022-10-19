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

public partial class admin_dealOrdersMgmtByUsers : System.Web.UI.Page
{
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
                bindProvinces();
                SearchhDealInfoByDifferentParams();
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
    protected void bindProvinces()
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlSearchProvince.DataSource = dt.DefaultView;
            ddlSearchProvince.DataTextField = "provinceName";
            ddlSearchProvince.DataValueField = "provinceId";
            ddlSearchProvince.DataBind();
            ddlSearchProvince.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlSearchProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlSearchProvince.SelectedValue);
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

                ddlSearchCity.DataSource = dtCities;

                ddlSearchCity.DataTextField = "cityName";

                ddlSearchCity.DataValueField = "cityId";

                ddlSearchCity.DataBind();

                ddlSearchCity.Items.Insert(0, "Select One");
            }
            else
            {
                ddlSearchCity.Items.Clear();
                ddlSearchCity.DataSource = null;
                ddlSearchCity.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
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
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].[status]='Successful' and [dealOrders].dealId = [deals].dealId),0) 'SuccessfulOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].[status]='Cancelled' and [dealOrders].dealId = [deals].dealId),0) 'CancelledOrder'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [deals].dealId),0) 'TotalOrders'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].[status]='Refunded' and [dealOrders].dealId = [deals].dealId),0) 'RefundedOrder'";           
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
            strQuery += ",[city].[cityName]";                       
            strQuery += " FROM ";
            strQuery += "[deals] ";
            strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
            strQuery += "INNER join dealcity On dealcity.[dealid]= deals.[dealid] ";
            strQuery += "INNER join city On dealCity.cityId= city.[cityId] ";
            strQuery += "where restaurant.[restaurantId]= deals.[restaurantId] ";


            if (txtSrchDealTitle.Text.Trim() != "")
            {
                strQuery += " and [deals].[title] like '%" + txtSrchDealTitle.Text.Trim() + "%' ";
            }

            if (ddlSearchCity.SelectedIndex > 0)
            {
                strQuery += " and dealCity.cityId="+ ddlSearchCity.SelectedValue.ToString().Trim();
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