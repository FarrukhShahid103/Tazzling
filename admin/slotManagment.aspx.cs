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

public partial class slotManagment : System.Web.UI.Page
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
            txtdlStartDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
            BLLCampaign objCamp = new BLLCampaign();
            objCamp.creationDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            DataTable dtCamp = objCamp.getCurrentCampaignByGivenDate();
            if (dtCamp!=null && dtCamp.Rows.Count > 0)
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
            DataTable dtCamp = objCamp.getCurrentCampaignByGivenDate();
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
            ltDealDetail.Text += "<ul id='test-list'>";
            for (int i = 0; i < dtDeals.Rows.Count; i++)
            {
                ltDealDetail.Text += " <li id='listItem_" + dtDeals.Rows[i]["campaignID"].ToString().Trim() + "'>";
                ltDealDetail.Text += " <div style='clear: both; height:30px;'>";
                ltDealDetail.Text += "<div style='float: left; padding-top:10px;'>";
                ltDealDetail.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/admin/Images/arrow.png' alt='move' width='16' height='16' class='handle' />";
                ltDealDetail.Text += "</div><div style='float: left; padding-right:10px;'>";
                ltDealDetail.Text += "<img src='" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/Images/dealfood/" + dtDeals.Rows[i]["restaurantId"].ToString().Trim() + "/mobile/" + dtDeals.Rows[i]["campaignpicture"].ToString().Trim() + "'";
                ltDealDetail.Text += " height='36' width='50'></img></div><div style='float: left; width:600px; text-align:left;'>";
                ltDealDetail.Text += "<strong>" + dtDeals.Rows[i]["campaignTitle"].ToString().Trim() + "</strong></div><div style='float: left; width:100px; text-align:left; padding-top:10px;'>";
                if (Convert.ToBoolean(dtDeals.Rows[i]["isFeatured"].ToString().Trim()))
                {
                    ltDealDetail.Text += "<input type='checkbox' onclick='javascript:changeFeaturedStatus(" + dtDeals.Rows[i]["campaignID"].ToString().Trim() + ");' checked='true' id='checkbox-" + dtDeals.Rows[i]["campaignID"].ToString().Trim() + "'/></div></div></li>";
                }
                else
                {
                    ltDealDetail.Text += "<input type='checkbox' onclick='javascript:changeFeaturedStatus(" + dtDeals.Rows[i]["campaignID"].ToString().Trim() + ");' id='checkbox-" + dtDeals.Rows[i]["campaignID"].ToString().Trim() + "'/></div></div></li>";
                }
            }
            ltDealDetail.Text += "</ul>";
            ltDealDetail.Text += "";
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
