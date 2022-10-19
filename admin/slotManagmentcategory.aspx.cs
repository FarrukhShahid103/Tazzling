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
using System.IO;
using System.Text.RegularExpressions;

public partial class slotManagmentcategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl myJs = new HtmlGenericControl();
        myJs.TagName = "script";
        myJs.Attributes.Add("type", "text/javascript");
        myJs.Attributes.Add("language", "javascript"); //don't need it usually but for cross browser.
        myJs.Attributes.Add("src", ResolveUrl("JS/CalendarControl.js"));
        Page.Header.Controls.Add(myJs);

        if (!IsPostBack)
        {
            BindCategories();
            txtdlStartDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
            BLLCampaign objCamp = new BLLCampaign();
            objCamp.creationDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            DataTable dtCamp = objCamp.getCurrentProductsByGivenDateAndCategory();
            if (dtCamp.Rows.Count > 0)
            {
                lblMessage.Visible = false;
                lblMessage.Text = "";
                ltDealDetail.Text = "";
                btnBindDeals(dtCamp);
            }
            else
            {
                ltDealDetail.Text = "";
                lblMessage.Visible = true;
                lblMessage.Text = "There is no active deal for this date";
            }    
        }
    }

    protected void BindCategories()
    {
        BLLCategories objCategory = new BLLCategories();
        DataTable dtCategory = objCategory.getAllActiveCategoriesAndSubCategories();
        if (dtCategory != null && dtCategory.Rows.Count > 0)
        {
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataValueField = "categoryId";
            ddlCategory.DataTextField = "categoryName";
            ddlCategory.DataBind();
        }

    }

    static string StripHTML(string inputString)
    {
        return Regex.Replace
          (inputString, @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", string.Empty);
    }
    
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLCampaign objCamp = new BLLCampaign();
            objCamp.creationDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            objCamp.campaignCategory = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            DataTable dtCamp = objCamp.getCurrentProductsByGivenDateAndCategory();
            if (dtCamp.Rows.Count > 0)
            {               
                lblMessage.Visible = false;
                lblMessage.Text = "";
                ltDealDetail.Text = "";
                btnBindDeals(dtCamp);
            }
            else
            {
                ltDealDetail.Text = "";
                lblMessage.Visible = true;
                lblMessage.Text = "There is no active deal for this date";
            }
        }
        catch (Exception ex)
        { }
    }

    protected void btnBindDeals(DataTable dtDeals)
    {
        try
        {
            string strDeals = "";
            strDeals += "<ul id='test-list'>";
            for (int i = 0; i < dtDeals.Rows.Count; i++)
            {
                strDeals += " <li id='listItem_" + dtDeals.Rows[i]["productID"].ToString().Trim() + "'>";
                strDeals += " <div style='clear: both; height:30px;'>";
                strDeals += "<div style='float: left; padding-top:10px;'>";
                strDeals += "<img src='" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/admin/Images/arrow.png' alt='move' width='16' height='16' class='handle' />";
                strDeals += "</div><div style='float: left; padding-right:10px;'>";
                strDeals += "<img src='" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/Images/dealfood/" + dtDeals.Rows[i]["restaurantId"].ToString().Trim() + "/mobile/" + dtDeals.Rows[i]["images"].ToString().Trim().Split(',')[0] + "'";
                strDeals += " height='36' width='50'></img></div><div style='float: left; width:250px; text-align:left;'>";
                strDeals += "<strong>" + dtDeals.Rows[i]["title"].ToString().Trim() + "</strong></div>";
                strDeals += "<div style='float: left; width:60px; padding-left:50px; text-align:left;'>$" + dtDeals.Rows[i]["sellingPrice"].ToString().Trim() + "</div>";
                strDeals += "<div style='float: left; width:60px; padding-left:30px; text-align:left;'>$" + dtDeals.Rows[i]["valuePrice"].ToString().Trim() + "</div>";
                string strVal = Convert.ToBoolean(dtDeals.Rows[i]["isActive"]) ? "Yes" : "No";

                strDeals += "<div style='float: left; width:60px; padding-left:30px; text-align:left;'>$" + strVal + "</div>";
                strDeals += "<div style='float: left; width:60px; padding-left:30px; text-align:left;'>$" + Convert.ToDateTime(dtDeals.Rows[i]["createdDate"].ToString().Trim()).ToString("MM-dd-yyyy") + "</div>";
                //strDeals += "<div style='float: left; width:60px; padding-left:100px; text-align:left;'><a href='" + "addEditProductManagement.aspx?Mode=edit&resID=" + dtDeals.Rows[i]["restaurantId"].ToString().Trim() + "&cid=" + dtDeals.Rows[i]["campaignID"].ToString().Trim() + "&did=" + dtDeals.Rows[i]["productID"].ToString().Trim() + "'><img src='" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/admin/Images/edit.gif'/></a></div>";
                strDeals += "</div></li>";
            }
            strDeals += "</ul>";            
            ltDealDetail.Text = strDeals;
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }

    }
}
