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

public partial class createNewsLetter : System.Web.UI.Page
{
    BLLCampaign objcampaign = new BLLCampaign();
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
            txtdlStartDate.Text = DateTime.Now.AddDays(1).ToString("MM-dd-yyyy");
            ddlDLStartHH.SelectedValue = "00";
            ddlDLStartPortion.SelectedValue = "AM";
            ddlDLStartMM.SelectedValue = "00";
           // CreateNewsLetter();
            FillDealsDroDownList(ddlSearchCity.SelectedValue.Trim());
        }
    }

    protected void ddlSearchCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDealsDroDownList(this.ddlSearchCity.SelectedValue);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void FillDealsDroDownList(string strCityID)
    {
        try
        {

            BLLDeals objDeal = new BLLDeals();
            objDeal.CreatedDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            objDeal.cityId = Convert.ToInt32(strCityID);
            DataTable dtDeal = objDeal.getCurrentDealByCityID();
            if (dtDeal.Rows.Count > 0)
            {
                imgGridMessage.Visible = false;
                lblMessage.Visible = false;
                lblMessage.Text = "";
                ltDealDetail.Text = "";
                ddlSearchDeal.DataSource = dtDeal;
                ddlSearchDeal.DataValueField = "dealId";
                ddlSearchDeal.DataTextField = "title";
                ddlSearchDeal.DataBind();
                btnBindDeals(dtDeal);
            }
            else
            {
                ltDealDetail.Text = "";
                ddlSearchDeal.Items.Clear();
                ddlSearchDeal.DataSource = null;
                ddlSearchDeal.DataBind();
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.Text = "There is no active deal for city \"" + ddlSearchCity.SelectedItem.Text.ToString().Trim() + "\"";
            }           
        }
        catch (Exception ex)
        {

        }
    }

    static string StripHTML(string inputString)
    {
        return Regex.Replace
          (inputString, @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", string.Empty);
    }


    private void bindCityDD()
    {
        //ddlSearchCity.DataSource = Misc.getAllCitiesWithProvinceAndCountryInfoByCountryID(2);
        //ddlSearchCity.DataValueField = "cityId";
        //ddlSearchCity.DataTextField = "cityName";
        //ddlSearchCity.DataBind();
        //ddlSearchCity.SelectedValue = "337";
    }
        
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLCities objCities = new BLLCities();
            BLLDeals objDeal = new BLLDeals();
            objDeal.CreatedDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            objDeal.cityId = Convert.ToInt32(ddlSearchCity.SelectedValue.Trim());
            DataTable dtDeal = objDeal.getCurrentDealByCityID();
            if (dtDeal.Rows.Count > 0)
            {
                if (ViewState["CreatedDate"] != null && ViewState["CreatedDate"].ToString().Trim() != objDeal.CreatedDate.ToString().Trim())
                {
                    ViewState["CreatedDate"] = objDeal.CreatedDate.ToString().Trim();
                    ddlSearchDeal.DataSource = dtDeal;
                    ddlSearchDeal.DataValueField = "dealId";
                    ddlSearchDeal.DataTextField = "title";
                    ddlSearchDeal.DataBind();
                }
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;

                lblMessage.Text = "";
                ltDealDetail.Text = "";
                btnBindDeals(dtDeal);                
            }
            else
            {
                ltDealDetail.Text = "";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/checked.png";
                lblMessage.Text = "There is no active deal for city \"" + ddlSearchCity.SelectedItem.Text.ToString().Trim() + "\"";
            }
        }
        catch (Exception ex)
        { }
    }

    private void CreateNewsLetter()
    {
        try
        {
          
          /*  objcampaign.creationDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");          
            DataTable dtDeal = objcampaign.GetCurrentDealByDate();
            if (dtDeal.Rows.Count > 0)
            {
                lblMessage.Visible = false;
                lblMessage.Text = "";
                ltDealDetail.Text = "";
                btnBindDeals(dtDeal);
            }
            else
            {
                ltDealDetail.Text = "";
                lblMessage.Visible = true;
                lblMessage.Text = "There is no active deal for city \"" + ddlSearchCity.SelectedItem.Text.ToString().Trim() + "\"";
            }*/
        }
        catch (Exception ex)
        { }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {            
            string attachment = "attachment; filename=NewsLetter.html";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/HTML";
            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            ltDealDetail.RenderControl(htextw);
            Response.Write(stw.ToString());
            Response.End();      
        }
        catch (Exception ex)
        { }
    }

    private void ExportToUser(string phFileName, string saveFileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("content-disposition", "attachment;filename=\"" + saveFileName + "\"");
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(phFileName);
        Response.End();
    }

    protected void btnBindDeals(DataTable dtDeals)
    {
        try
        {

            string strCityName = "";
            if (ddlSearchCity.SelectedItem.Text.Trim() == "York Region")
            {
                strCityName = "York_Region";
            }
            else if (ddlSearchCity.SelectedItem.Text.Trim() == "Oakville - Burlington")
            {
                strCityName = "Oakville_Burlington";
            }
            else
            {
                strCityName = ddlSearchCity.SelectedItem.Text.Trim().Replace(" ", "");
            }

            ltDealDetail.Text += "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            ltDealDetail.Text += "<html><head><title>News Letter</title><style type=\"text/css\">  ";
            ltDealDetail.Text += @"   .ca-item
        {
            float: left;
            height: 150px;
            position: relative;
           
            width: 150px;
        }
        .ca-item-main
        {
           /* background-color: none repeat scroll 0 0 #FFFFFF;
            bottom: 5px;
            box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);
            left: 5px; right: 5px;
            top: 5px;*/
            overflow: hidden;
            position: absolute;
           
        }
        .mosaic-block
        {
          /*  background: url('../images/progress.gif') no-repeat scroll center center #111111;*/
          
            float: left;
            height: 149px;
            margin: 0;
            overflow: hidden;
            position: relative;
            width: 148px;
        }
        .bar3 .mosaic-overlay
        {
            bottom: 0;
            color: White;
            height: 25px;
            left: 0;
            opacity: 0.7;
            position: absolute;
            width: 150px;
            z-index: 6;
        }
        .mosaic-overlay
        {
            background-color: none repeat scroll 0 0 #111111;
            display: none;
            height: 100%;
            position: absolute;
            width: 100%;
            z-index: 5;
        }
        .details
        {
            margin: 0px;
        }
        .ca-item h4
        {
            font-family: Arial;
            font-size: 12px;
            font-style: italic;
            line-height: normal;
            position: relative;
            text-align: left;
        }
        .mosaic-backdrop
        {
            
          
            height: 100%;
            
           
            width: 100%;
        }   ";

            ltDealDetail.Text += "  body{padding: 0px;padding: 0px;color: Black;line-height: normal;background-color: #f3f3f3;font: 14px/150% Arial;}.Contant{padding: 0 auto;width: 680px;}.hyperlink{color:white; text-decoration:none;}   p{margin:0;} li.MsoNormal, div.MsoNormal{font-family: Arial;font-size: 12pt;margin: 0 0 0;}.MsoChpDefault{font-size: 10pt;}div.WordSection1{page: WordSection1;}  .TopLink{text-decoration:none;} .TopLink:hover{color: #3fafef;cursor: pointer;text-decoration: underline;}img{border: none;}.style1{width: 72px;}.style2{}.style3{height: 5px;}.style4{width: 100%;}.style6{height: 120px;}</style></head>";
            ltDealDetail.Text += "<body><center><table style=\"width: 680px; background-color: white; padding-top: 10px; padding-bottom: 10px;\"><tbody><tr><td style=\"text-align: center;\">";

            ltDealDetail.Text += "<unsubscribe>Unsubscribe</unsubscribe>";

            ltDealDetail.Text += "<span style=\"color: Black;cursor: default; margin:0 5px;\">|</span>";
            //ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "\">Go to Tazzling.com </a><span style=\"color: Black; cursor: default;\">|</span>";
            ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/newsletter.aspx?cid=" + ddlSearchCity.SelectedValue.ToString().Trim() +
                            "&date=" + Convert.ToString(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00") + "&slot=" + ddlSearchDeal.SelectedIndex.ToString() + "\">View this email in browser </a>";

            ltDealDetail.Text += "</td></tr><tr><td style=\"text-align: center;\">add <a class=\"TopLink\" href=\"mailto:news@tazzling.com\">news@tazzling.com</a> to your address book or safe sender list so our emails get to your inbox. <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/howItWorks.aspx\">Learn how</a></td></tr>";
            ltDealDetail.Text += "<tr><td style='width:100%;'><table style=\"clear: both; height: 95px; width: 100%;border-bottom: 1px solid #ff72c7;\"><tr class=\"Contant\"><td style=\"padding-top: 25px; float: left; padding-left:15px;\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "\"><img style='height:56px; width:209px;' height='56' width='209' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/NewLogo_2.png\" alt=\"Tastygo Logo\" /></a></td>";
            ltDealDetail.Text += "<td style=\"padding-top: 25px; text-align: center; display:none; float: left; padding-left:25px;\"><span style=\"padding-left: 15px; padding-top: 25px; text-align: center; width: 250px;\"><span style=\"clear: both; font-size: 13px;  font-weight: bold;\">Your DailyDeal For</span><br /><span style=\"clear: both; font-size: 25px; padding-top: 10px; padding-left: 5px;\">";


            ltDealDetail.Text += ddlSearchCity.SelectedItem.Text.Trim() + "</span> </span></td>";

            ltDealDetail.Text += "<td style=\"padding-top: 25px; float:right; margin-right:10px; text-align: right;\"><table><tr><td><span style=\"padding-top: 25px; width: 250px;\"><span style=\"clear: both;font-family: Arial; font-size: 13px;padding-top: 25px; font-weight: bold;\">";
            ltDealDetail.Text += Convert.ToDateTime(txtdlStartDate.Text.Trim()).ToString("dddd MMMM dd, yyyy") + "</span><br /></span></td></tr>";

            ltDealDetail.Text += "<tr><td><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='padding-right:5px;height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/twitter1.png\" alt=\"Tweet With Us on Twitter\" /></a><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/facebook2.png\" alt=\"Follow us on Facebook\" /></a></td></tr></table> </td></tr></table></td></tr>";
            ltDealDetail.Text += " <tr style='mso-yfti-irow: 3'>";
            ltDealDetail.Text += " <td colspan='2' style='padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='614' ";
            ltDealDetail.Text += " style='width: 498.5pt; margin-left:6px; mso-cellspacing: 0in; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " <tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes'> ";
            ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in; text-align:left;'> ";
            ltDealDetail.Text += " <p class='MsoNormal'> ";
            ltDealDetail.Text += " <b><span style=' font-family: Arial; mso-fareast-font-family: Arial; ";
            ltDealDetail.Text += " color: #363636'>New </span></b><em><b><span style='font-size: 16.5pt; font-family: Arial; ";
            ltDealDetail.Text += " mso-fareast-font-family: Arial; color: #DD0017; font-style: normal'> ";
            ltDealDetail.Text += " Sales</span></b></em><b><span style='font-size: 16.5pt; font-family:Arial; ";
            ltDealDetail.Text += " mso-fareast-font-family: Arial; color: #363636'><o:p></o:p></span></b></p> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td width='10' style='width: 7.5pt; padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td valign='top' style='padding: 0in 0in 0in 0in ;'> ";
            ltDealDetail.Text += " <div align='right'> ";
            ltDealDetail.Text += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='mso-cellspacing: 0in; ";
            ltDealDetail.Text += " mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " <tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes'> ";
            ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " <p class='MsoNormal' align='right' style='text-align: right'> ";
            ltDealDetail.Text += " <i><span style='font-family: Arial; mso-fareast-font-family: Arial; ";
            ltDealDetail.Text += " color: #363636'>Sales start</span></i><em><span style='font-family: Arial; ";
            ltDealDetail.Text += " mso-fareast-font-family:Arial; color: #DD0017'> today</span></em><i><span ";
            ltDealDetail.Text += " style='font-family:Arial; mso-fareast-font-family:Arial; ";
            ltDealDetail.Text += " color: #363636'> at </span></i><em><span style='font-family:Arial; mso-fareast-font-family:Arial; ";
            ltDealDetail.Text += " color: #DD0017'><span style='mso-field-code: ' HYPERLINK \0022\0022 ''><span class='MsoHyperlink'> ";
            ltDealDetail.Text += " <span style='font-family:Arial; color: #DD0017; text-decoration: none; ";
            ltDealDetail.Text += " text-underline: none'>11am ET / 8am PT</span></span></span></span></em><i><span style='font-family:Arial; ";
            ltDealDetail.Text += " mso-fareast-font-family:Arial; color: #363636'><o:p></o:p></span></i></p> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td width='10' style='width: 7.5pt; padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " <p class='MsoNormal'> ";
            ltDealDetail.Text += " <span style='mso-fareast-font-family:Arial'> ";
            ltDealDetail.Text += " <img border='0' width='49' height='35' id='_x0000_i1028' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/clock.gif\" ";
            ltDealDetail.Text += " style='display: block; padding-top: 8px'><o:p></o:p></span></p> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " </tr> ";
            ltDealDetail.Text += " </table> ";
            ltDealDetail.Text += " </div> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " </tr> ";
            ltDealDetail.Text += " </table> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " </tr> ";
            ltDealDetail.Text += " <tr style='mso-yfti-irow: 4'> ";
            ltDealDetail.Text += " <td colspan='2' style='padding: 0in 0in 0in 0in; display: inline-block'> ";
            ltDealDetail.Text += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='614' ";
            ltDealDetail.Text += " style='width: 500.5pt; margin-left:6px;border-bottom: 1px solid #FF72C7; margin-bottom:30px; mso-cellspacing: 0in; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in' ";
            ltDealDetail.Text += " height='300'> ";


            int loop = 0;


            for (int j = 0; j < dtDeals.Rows.Count; j++)
            {
                if (loop < dtDeals.Rows.Count)
                {

                    ltDealDetail.Text += "  <tr style='mso-yfti-irow: 0; margin-left:15px; mso-yfti-firstrow: yes'>";
                    for (int i = 0; i < 2; i++)
                    {

                        if (loop < dtDeals.Rows.Count)
                        {


                           // string shortTitle = dtDeals.Rows[loop]["campaignTitle"].ToString();
                            string shortTitle = (dtDeals.Rows[i]["dealPageTitle"].ToString());
                            //string titel = dtDeals.Rows[loop]["campaignShortDescription"].ToString();
                            string titel = dtDeals.Rows[i]["topTitle"].ToString();
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in ;'> ";
                            ltDealDetail.Text += "<table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='300' ";
                            ltDealDetail.Text += "style='width: 198.0pt; mso-cellspacing: 0in;  mso-yfti-tbllook: 1184; ";
                            ltDealDetail.Text += "mso-padding-alt: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "<tr height='45px' style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; height: 45px; background-color:#505050;clear: both;height: 45px;margin-left: 0;overflow: hidden;width: 300px;'> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 45px; '> ";
                            ltDealDetail.Text += "<table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='300' ";
                            ltDealDetail.Text += "style='mso-cellspacing: 0in;  mso-yfti-tbllook: 1184; ";
                            ltDealDetail.Text += "mso-padding-alt: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes; color:white; height: 45px ;'> ";
                            ltDealDetail.Text += "<td width='300'text-align: left; style=' padding: 0in 0in 0in 0in; height: 45px'> ";
                            ltDealDetail.Text += "<table class='MsoNormalTable' width='300' border='0' cellspacing='0' cellpadding='0' style='mso-cellspacing: 0in; ";
                            ltDealDetail.Text += "mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; height: 7.5pt'> ";
                            //ltDealDetail.Text += "<td width='10' style='width: 7.5pt; padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                            //ltDealDetail.Text += "</td> ";
                            //ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                            //ltDealDetail.Text += "</td> ";
                            //ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                            //ltDealDetail.Text += "</td> ";
                           // ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 1'> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in;text-align: left;color: white;'> ";
                            ltDealDetail.Text += "<span class='MsoNormal' style='text-align: left;color: white; padding-left:10px;'> ";
                            ltDealDetail.Text += "<b><span style='font-family:Arial; mso-fareast-font-family:Arial ";
                            ltDealDetail.Text += "color: white'>";
                            ltDealDetail.Text += (shortTitle.Trim() != "" ? shortTitle.Trim().Length > 20 ? shortTitle.Substring(0, 17) + "..." : shortTitle : titel.Trim() == "" ? titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel : titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel);
                            ltDealDetail.Text += "</span></b></span> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 2; height: 3.75pt'> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 3.75pt'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 3.75pt'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 3.75pt'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 3;text-align:left;'> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in;text-align:left;'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in;color: white;'> ";
                            ltDealDetail.Text += "<span class='MsoNormal' style='text-align:left;padding-left:10px;color: white;'> ";
                            ltDealDetail.Text += "<span style=' font-family:Arial;padding:left:10px; text-align:left; mso-fareast-font-family:Arial ";
                            ltDealDetail.Text += "color: white'>";
                            ltDealDetail.Text += titel.Trim().Length > 30 ? titel.Substring(0,27) + "..." : titel;
                            ltDealDetail.Text += "<o:p></o:p></span></span> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 4; mso-yfti-lastrow: yes; height: 7.5pt'> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td width='10' style='width: 7.5pt; padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "</table> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "</table> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "<td width='35' style='width: 20.25pt; padding: 0in 0in 0in 0in; height: 45px'> ";
                            ltDealDetail.Text += "<table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='mso-cellspacing: 0in; ";
                            ltDealDetail.Text += " mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes'> ";
                            ltDealDetail.Text += "<td style='padding: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "<span class='MsoNormal'> ";
                            ltDealDetail.Text += "<span style='mso-fareast-font-family:Arial'><a href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[loop]["restaurantId"].ToString().Trim() + "\" ";
                            ltDealDetail.Text += "title='";
                            ltDealDetail.Text += (shortTitle.Trim() != "" ? shortTitle.Trim().Length > 20 ? shortTitle.Substring(0, 17) + "..." : shortTitle : titel.Trim() == "" ? titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel : titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel);
                            ltDealDetail.Text += "'";
                            ltDealDetail.Text += "style='outline: none'><span style='border: none windowtext 1.0pt; mso-border-alt: none windowtext 0in; ";
                            ltDealDetail.Text += "padding: 0in; text-decoration: none; text-underline: none'> ";
                            ltDealDetail.Text += "<img border='0' id='_x0000_i1029' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/product-arow-right.png\" ";
                            ltDealDetail.Text += "style='border-bottom-width: 0in; border-left-width: 0in; border-right-width: 0in; ";
                            ltDealDetail.Text += "border-top-width: 0in; display: block' alt=' &gt; '></span></a><o:p></o:p></span></span> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "</table> ";
                            ltDealDetail.Text += "</td> ";
                            ltDealDetail.Text += "</tr> ";
                            ltDealDetail.Text += "<tr style='mso-yfti-irow: 1; mso-yfti-lastrow: yes; height: 2.5in'> ";
                            ltDealDetail.Text += "<td colspan='2' style='padding: 0in 0in 0in 0in; height: 2.5in'> ";
                            ltDealDetail.Text += "<p class='MsoNormal'> ";
                            ltDealDetail.Text += "<span style='mso-fareast-font-family:Arial'><a href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[loop]["restaurantId"].ToString().Trim() + "\" ";
                            ltDealDetail.Text += "title='";
                            ltDealDetail.Text += (shortTitle.Trim() != "" ? shortTitle.Trim().Length > 30 ? shortTitle.Substring(0, 27) + "..." : shortTitle : titel.Trim() == "" ? titel.Trim().Length > 30 ? titel.Substring(0, 27) + "..." : titel : titel.Trim().Length > 30 ? titel.Substring(0, 27) + "..." : titel);
                            ltDealDetail.Text += "'";
                            ltDealDetail.Text += "  style='outline: none'><span style='border: none windowtext 1.0pt; mso-border-alt: none windowtext 0in; ";
                            ltDealDetail.Text += "   padding: 0in; text-decoration: none; text-underline: none'> ";
                            ltDealDetail.Text += "   <img border='0' width='325' height='325'  src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/dealfood/" + dtDeals.Rows[loop]["restaurantId"].ToString().Trim() + "/" + dtDeals.Rows[loop]["image1"].ToString().Trim() + "\" ";
                            ltDealDetail.Text += "   style='display: block'></span></a><o:p></o:p></span></p> ";
                            ltDealDetail.Text += "    </td> ";
                            ltDealDetail.Text += "    </tr> ";
                            ltDealDetail.Text += "                                                </table> ";
                            ltDealDetail.Text += "                                                    </td> ";
                            ltDealDetail.Text += " ";
                            ltDealDetail.Text += "<td width='14' style='width: 13.5pt; background-color:white; padding: 0in 0in 0in 0in'> ";
                            ltDealDetail.Text += "  </td>";
                            loop++;
                        }
                    }

                    ltDealDetail.Text += "<td width='14' height='15px' style='width: 10.5pt; height:15px; padding: 0in 0in 0in 0in'> ";
                    ltDealDetail.Text += "    </td> ";
                    ltDealDetail.Text += "<tr height='15px' style='mso-yfti-irow: 1; mso-yfti-lastrow: yes; height: 11.25pt'> ";
                    ltDealDetail.Text += "<td colspan='3' height='15px' style='background-color: white; width:100%; padding: 0in 0in 0in 0in; height: 11.25pt'> ";
                    ltDealDetail.Text += "      </td> ";
                    ltDealDetail.Text += "  </tr>";
                }


            }

            ltDealDetail.Text += " </table> ";
            ltDealDetail.Text += " <p class='MsoNormal'> ";
            ltDealDetail.Text += " <span style='mso-fareast-font-family:Arial; display: none; mso-hide: all'> ";
            ltDealDetail.Text += " <o:p>&nbsp;</o:p> ";
            ltDealDetail.Text += " </span> ";
            ltDealDetail.Text += " </p> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            ltDealDetail.Text += " </td> ";
            ltDealDetail.Text += " </tr> ";
            //ltDealDetail.Text += " <tr style='mso-yfti-irow: 5'> ";
            //ltDealDetail.Text += " <td colspan='2' style='padding: 0in 0in 0in 0in'> ";
            //ltDealDetail.Text += " <p class='MsoNormal'> ";
            //ltDealDetail.Text += " <span style='mso-fareast-font-family: 'Times New Roman''> ";
            //ltDealDetail.Text += " <img border='0' id='_x0000_i1065' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/gap.gif\" ";
            //ltDealDetail.Text += " style='display: block'><o:p></o:p></span></p> ";
            //ltDealDetail.Text += " </td> ";
            //ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            //ltDealDetail.Text += " </td> ";
            //ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
            //ltDealDetail.Text += " </td> ";
            //ltDealDetail.Text += " </tr> ";
           /* objcampaign.creationDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            DataTable dtclosing = objcampaign.getEndingSoonCampaigns();
            if (dtclosing != null && dtclosing.Rows.Count > 0)
            {
                ltDealDetail.Text += " <tr style='mso-yfti-irow: 6; mso-yfti-lastrow: yes'> ";
                ltDealDetail.Text += " <td colspan='2' style='padding: 0in 0in 0in 0in'> ";
                ltDealDetail.Text += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='614' ";
                ltDealDetail.Text += " style='width: 460.5pt; text-align:left; margin-left:20px; mso-cellspacing: 0in; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";
                ltDealDetail.Text += " <tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes; height: 4.5pt'> ";
                ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in; height: 4.5pt'> ";
                ltDealDetail.Text += " </td> ";
                ltDealDetail.Text += " </tr> ";
                ltDealDetail.Text += " <tr style='mso-yfti-irow: 1'> ";
                ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
                ltDealDetail.Text += " <p class='MsoNormal'> ";
                ltDealDetail.Text += " <b><span style='font-size: 16.5pt; font-family:Arial; mso-fareast-font-family:Arial; ";
                ltDealDetail.Text += " color: #DD0017'>Ending</span></b><em><b><span style='font-size: 16.5pt; font-family:Arial; ";
                ltDealDetail.Text += " mso-fareast-font-family:Arial; color: #363636; font-style: normal'> ";
                ltDealDetail.Text += " Soon</span></b></em><span style='mso-fareast-font-family:Arial'><o:p></o:p></span></p> ";
                ltDealDetail.Text += " </td> ";
                ltDealDetail.Text += " </tr> ";
                ltDealDetail.Text += " <tr style='mso-yfti-irow: 2; mso-yfti-lastrow: yes; height: 9.75pt'> ";
                ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in; height: 9.75pt'> ";
                ltDealDetail.Text += " </td> ";
                ltDealDetail.Text += " </tr> ";
                ltDealDetail.Text += " </table> ";
                ltDealDetail.Text += " <p class='MsoNormal'> ";
                ltDealDetail.Text += " <span style='mso-fareast-font-family:Arial; display: none; mso-hide: all'> ";
                ltDealDetail.Text += " <o:p>&nbsp;</o:p> ";
                ltDealDetail.Text += " </span> ";
                ltDealDetail.Text += " </p> ";
                ltDealDetail.Text += " <table class='MsoNormalTable' border='0'  cellspacing='0' cellpadding='0' width='614' ";
                ltDealDetail.Text += " style='width: 500.5pt; margin-left:14px; background-color: white; border-bottom:1px solid #FF72C7; margin-bottom:30px; mso-cellspacing: 0in; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";
                ltDealDetail.Text += " <tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes'> ";
                ltDealDetail.Text += " <td width='614' valign='top' style='width: 502.5pt; background-color: white; padding: 0in 0in 0in 0in; display: inline-block; ";
                ltDealDetail.Text += " border: none!important; outline: none!important; text-decoration: none!important'> ";
                ltDealDetail.Text += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='mso-cellspacing: 0in; ";
                ltDealDetail.Text += " mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";




                int closingloopcount = 0;
                if (dtclosing != null && dtclosing.Rows.Count > 0)
                {
                    for (int k = 0; k < dtclosing.Rows.Count; k++)
                    {
                        ltDealDetail.Text += " <tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes'>";

                        for (int m = 0; m < 4; m++)
                        {
                            if (closingloopcount < dtclosing.Rows.Count)
                            {
                                string titel = dtDeals.Rows[closingloopcount]["campaignShortDescription"].ToString();
                                string shortTitle = dtDeals.Rows[closingloopcount]["campaignTitle"].ToString();
                                ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in; display: inline-block; border: none!important; ";
                                ltDealDetail.Text += " outline: none!important; text-decoration: none!important'> ";
                                ltDealDetail.Text += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' style='mso-cellspacing: 0in; ";
                                ltDealDetail.Text += " mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in'> ";



                                ltDealDetail.Text += " <tr style='mso-yfti-irow: 0; mso-yfti-firstrow: yes'> ";
                                ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in; border: none!important; text-align:left; outline: none!important; ";
                                ltDealDetail.Text += " text-decoration: none!important'> ";
                                ltDealDetail.Text += " <span class='MsoNormal'> ";
                                ltDealDetail.Text += " <span style='mso-fareast-font-family:Arial;text-align:left;'><a href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[closingloopcount]["restaurantId"].ToString().Trim() + "\" ";
                                ltDealDetail.Text += " title='";
                                ltDealDetail.Text += (shortTitle.Trim() != "" ? shortTitle.Trim().Length > 20 ? shortTitle.Substring(0, 17) + "..." : shortTitle : titel.Trim() == "" ? titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel : titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel);
                                ltDealDetail.Text += "'";
                                ltDealDetail.Text += "style='border: none!important; outline: none!important; ";
                                ltDealDetail.Text += "  text-decoration: none!important'><span style='text-decoration: none; text-underline: none;'> ";
                                ltDealDetail.Text += " <img border='0' width='150' height='150' id='_x0000_i1066' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/dealfood/dealfood/" + dtDeals.Rows[closingloopcount]["restaurantId"].ToString().Trim() + "/" + dtDeals.Rows[closingloopcount]["campaignpicture"].ToString().Trim() + "\" ";
                                ltDealDetail.Text += " style='border: none!important; outline: none!important; text-decoration: none!important; ";
                                ltDealDetail.Text += " display: block'></span></a><o:p></o:p></span></span> ";

                                ltDealDetail.Text += "<span>";
                                ltDealDetail.Text += "<p style='background-color: #505050;text-align:left; padding: 1.5pt 0in 0in 7.5pt; height: 25.5px; position:inherit; ' class='MsoNormal'> ";
                                ltDealDetail.Text += "  <span style='mso-fareast-font-family:Arial'><a class='hyperlink' href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[closingloopcount]["restaurantId"].ToString().Trim() + "\" ";
                                ltDealDetail.Text += " alt='' style='outline: none; text-decoration:none;'><span style=' font-family:Arial; ";
                                ltDealDetail.Text += " color: white; border: none windowtext 1.0pt; mso-border-alt: none windowtext 0in; text-align:left;";
                                ltDealDetail.Text += " padding: 0in; text-decoration: none; text-decoration: none'>";
                                ltDealDetail.Text += (shortTitle.Trim() != "" ? shortTitle.Trim().Length > 20 ? shortTitle.Substring(0, 17) + "..." : shortTitle : titel.Trim() == "" ? titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel : titel.Trim().Length > 20 ? titel.Substring(0, 17) + "..." : titel);
                                ltDealDetail.Text += "</span></a><o:p></o:p></span></p> ";
                                ltDealDetail.Text += "</span>";

                                ltDealDetail.Text += " </td> ";
                                ltDealDetail.Text += " </tr> ";


                                ltDealDetail.Text += " </table> ";
                                ltDealDetail.Text += " </td> ";
                                ltDealDetail.Text += " <td width='5' style='width: 20.75pt; padding: 0in 0in 0in 0in'> ";
                                ltDealDetail.Text += " </td>";
                                closingloopcount++;
                            }
                        }
                        ltDealDetail.Text += "</tr> ";
                        ltDealDetail.Text += "      <tr style='mso-yfti-irow: 1; mso-yfti-lastrow: yes; height: 7.5pt'> ";
                        ltDealDetail.Text += "        <td colspan='4' style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                        ltDealDetail.Text += "         </td> ";
                        ltDealDetail.Text += "        <td style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                        ltDealDetail.Text += "        </td> ";
                        ltDealDetail.Text += "     <td style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                        ltDealDetail.Text += "     </td> ";
                        ltDealDetail.Text += "     <td style='padding: 0in 0in 0in 0in; height: 7.5pt'> ";
                        ltDealDetail.Text += "     </td> ";
                        ltDealDetail.Text += "  </tr>";

                    }



                    ltDealDetail.Text += " </table> ";
                    ltDealDetail.Text += " </td> ";
                    ltDealDetail.Text += " </tr> ";
                    ltDealDetail.Text += " </table> ";
                    ltDealDetail.Text += " <p class='MsoNormal'> ";
                    ltDealDetail.Text += " <span style='mso-fareast-font-family:Arial; display: none; mso-hide: all'> ";
                    ltDealDetail.Text += " <o:p>&nbsp;</o:p> ";
                    ltDealDetail.Text += " </span> ";
                    ltDealDetail.Text += " </p> ";

                    ltDealDetail.Text += " </td> ";
                    ltDealDetail.Text += " <td width='33' style='width: 24.75pt; padding: 0in 0in 0in 0in'> ";
                    ltDealDetail.Text += " </td> ";
                    ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
                    ltDealDetail.Text += " </td> ";
                    ltDealDetail.Text += " </tr> ";
                    ltDealDetail.Text += " ";


                }

                else
                {
                    ltDealDetail.Text += "<tr style='color:black; font-family:Arial; font-size:16.5pt;float:center; font-size:20px; border-bottom:1 px solid red; '>";
                    ltDealDetail.Text += "<td >No Campaign ending soon within 1 Day</td>";



                }
            }*/


                //ltDealDetail.Text += " <tr style='mso-yfti-irow: 5'> ";
                //ltDealDetail.Text += " <td colspan='2' style='padding: 0in 0in 0in 0in'> ";
                //ltDealDetail.Text += " <p class='MsoNormal'> ";
                //ltDealDetail.Text += " <span style='mso-fareast-font-family: museo_sans_500regular'> ";
                //ltDealDetail.Text += " <img border='0' id='_x0000_i1065' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/gap.gif\" ";
                //ltDealDetail.Text += " style='display: block'><o:p></o:p></span></p> ";
                //ltDealDetail.Text += " </td> ";
                //ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
                //ltDealDetail.Text += " </td> ";
                //ltDealDetail.Text += " <td style='padding: 0in 0in 0in 0in'> ";
                //ltDealDetail.Text += " </td> ";
                //ltDealDetail.Text += " </tr> ";


                ltDealDetail.Text += "<tr><td><table style='width:680px;' width='680'><tr><td style='width:340px; text-align:center;' width='340' align=\"center\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/apps.aspx\"><img style='height:87px; width:263px;' height='87' width='263' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/MobileApps.png\"/></a></td>";
                ltDealDetail.Text += "<td style='width:340px; text-align:center;' width='340' align=\"center\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/affiliate.aspx\"><img style='height:87px; width:263px;' height='87' width='263' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Dollars.png\"/></a></td></tr></table>";
                ltDealDetail.Text += "</td></tr><tr><td align=\"left\" style=\"padding: 5px; color: White; font-size: medium; background-color:  #505050; width:680px;\" width='680'>Question Or Coment? Contact Us</td></tr><tr><td align=\"left\" style=\"background-color: White; width: auto; height: 1px;\" width=\"2px\"></td></tr><tr><td style=\"background-color: #505050;\"><table><tr><td align=\"left\" valign=\"middle\" width=\"160px\"><table><tr><td width=\"165px\">";
                ltDealDetail.Text += "<img style='height:16px; width:157px;' height='16' width='157' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/FooterContactUs.png\"/>&nbsp;&nbsp;&nbsp;</td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Twitter2.png\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Fb.png\" alt=\"#\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/RSS.aspx\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/RSS2.png\" alt=\"#\" /></a></td></tr></table></td></tr>";
                ltDealDetail.Text += "<tr><td style=\"padding: 5px; background-color:  #505050; color: White; float: left; width: 663px; height: 60px;\" align=\"left\">";
                ltDealDetail.Text += "This email was sent by: TastyGo Online Inc. 20-206 E 6th Ave Vancouver, BC, V5T 1J7, Canada. You are receiving this email because you signed up for the<br /> ";
                ltDealDetail.Text += "Daily Tastygo Deal alerts. If you prefer not to receive Daily Tastygo emails, you can always <unsubscribe>Unsubscribe</unsubscribe>.<br />";
                ltDealDetail.Text += "</td></tr></table></td></tr></tbody></table></center>  </body></html>";

            }
        
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }

    }
   
}
