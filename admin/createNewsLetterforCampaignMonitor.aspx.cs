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
using System.Collections.Generic;
using CampaignMonitorAPIWrapper;

public partial class createNewsLetterforCampaignMonitor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

        HtmlGenericControl myJs = new HtmlGenericControl();
        myJs.TagName = "script";
        myJs.Attributes.Add("type", "text/javascript");
        myJs.Attributes.Add("language", "javascript"); //don\"t need it usually but for cross browser.
        myJs.Attributes.Add("src", ResolveUrl("JS/CalendarControl.js"));
        Page.Header.Controls.Add(myJs);
   
        if (!IsPostBack)
        {
            string[] strCityIDs = ddlSearchCity.SelectedItem.Value.ToString().Split('|');
            txtdlStartDate.Text = DateTime.Now.AddDays(1).ToString("MM-dd-yyyy");
            ddlDLStartHH.SelectedValue = "00";
            ddlDLStartPortion.SelectedValue = "AM";
            ddlDLStartMM.SelectedValue = "00";
            FillDealsDroDownList(strCityIDs[1].Trim());
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

    protected void btnCampaignMonitor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            DateTime dtNewsLeterTime = DateTime.Parse(txtCampaignNewsLetterDate.Text.Trim() + " " + ((ddCampaingNewsLetterTimeSpan.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddCampaignNewsLetterHours.SelectedItem.Text.Trim()) + 12).ToString() : ddCampaignNewsLetterHours.SelectedItem.Text.Trim()).ToString() + ":" + ddCampaignNewsLetterMinutes.SelectedItem.Text + ":" + "00");
            if (dtNewsLeterTime > DateTime.Now)
            {
                string[] strCityIDs = ddlSearchCity.SelectedItem.Value.ToString().Split('|');                
                //Live Setting
                string API_ID = "544aa0620ffc18cb21229986fbf79123";
                string Client_ID = "b88559e05d78a8a5f93970f4b08c82d2";
                string ListID = strCityIDs[0].Trim();
                //string ListID = "501141ffbb664835f37aa728f226bb3d";

                //Demo Setting
                /*string API_ID = "bd0cdf364fb9f383479be6d0e58c6450";
                string Client_ID = "b8cbcc3a634d7fb67d6e65953459efc8";
                string ListID = "b9f01773f6badded2fa2b33e1f2b3795";*/
                


                List<string> list = new List<string>();
                list.Add(ListID);              

                List<ListSegment> Segment = new List<ListSegment>();
                ListSegment LS = new ListSegment(ListID, "Colin");
                Segment.Add(LS);


                CampaignMonitorAPIWrapper.Result<string> result = Campaign.Create(API_ID, Client_ID, txtCampaignNewsLetterName.Text.Trim(), txtCampaignNewsLetterSubject.Text.Trim(), "TastyGo", "news@tazzling.com", "news@tazzling.com",
                    "http://www.tazzling.com/default3.aspx?cid=" + strCityIDs[1].Trim() +"&extra="+Server.UrlEncode(txtExtraText.Text.Trim().Replace("\n","</br>"))+
                "&date=" + Convert.ToString(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00") + "&slot=" + ddlSearchDeal.SelectedIndex.ToString(),
                "http://www.tazzling.com/default3.aspx?cid=" + strCityIDs[1].Trim() +"&extra="+Server.UrlEncode(txtExtraText.Text.Trim().Replace("\n","</br>"))+
                "&date=" + Convert.ToString(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00") + "&slot=" + ddlSearchDeal.SelectedIndex.ToString()
                , list, Segment);
                if (result.Message.ToLower() == "success")
                {
                    result = Campaign.Send(API_ID, result.ReturnObject.ToString(), "colin@tazzling.com", dtNewsLeterTime);
                    //result = Campaign.Send(API_ID, result.ReturnObject.ToString(), "sher.azam@redsignal.biz", dtNewsLeterTime);
                    if (result.Message.ToLower() == "success")
                    {
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/checked.png";
                        lblMessage.ForeColor = System.Drawing.Color.Black;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Newsletter scheduled Successfully.";
                    }   
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/error.png";
                        lblMessage.Text = result.Code + " : " + result.Message.Trim();
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.Text = result.Code + " : " + result.Message.Trim();
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.Text = "Please enter future date for newsletter schedule.";
            }
           
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.Text = ex.Message;
        }       
                
    }

    protected void ddlSearchCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string[] strCityIDs = ddlSearchCity.SelectedItem.Value.ToString().Split('|');
            FillDealsDroDownList(strCityIDs[1].Trim());
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
            try
            {
                if (ddlSearchCity.SelectedItem.Text.Trim() == "Abbotsford" || ddlSearchCity.SelectedItem.Text.Trim() == "Surrey"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Vancouver" || ddlSearchCity.SelectedItem.Text.Trim() == "Victoria")
                {
                    ddCampaignNewsLetterHours.SelectedIndex = 6;
                }
                else if (ddlSearchCity.SelectedItem.Text.Trim() == "Calgary" || ddlSearchCity.SelectedItem.Text.Trim() == "Edmonton")
                {
                    ddCampaignNewsLetterHours.SelectedIndex = 5;
                }
                else if (ddlSearchCity.SelectedItem.Text.Trim() == "Brampton" || ddlSearchCity.SelectedItem.Text.Trim() == "Toronto"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Mississauga" || ddlSearchCity.SelectedItem.Text.Trim() == "Hamilton"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Halifax" || ddlSearchCity.SelectedItem.Text.Trim() == "York Region"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Oakville - Burlington" || ddlSearchCity.SelectedItem.Text.Trim() == "St. Catharines")
                {
                    ddCampaignNewsLetterHours.SelectedIndex = 3;
                }
                txtCampaignNewsLetterSubject.Text = ddlSearchDeal.SelectedItem.Text.Trim();
                txtCampaignNewsLetterName.Text = "!" + Convert.ToDateTime(txtdlStartDate.Text.Trim()).ToString("yyyyMMdd").ToString() + ddlSearchCity.SelectedItem.Text.Trim().Substring(0, 3);
                txtCampaignNewsLetterDate.Text = txtdlStartDate.Text.Trim();

                ViewState["CreatedDate"] = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            }
            catch (Exception ex)
            {
                ViewState["CreatedDate"] = objDeal.CreatedDate.ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLCities objCities = new BLLCities();
            BLLDeals objDeal = new BLLDeals();            
            objDeal.CreatedDate = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
            string[] strCityIDs = ddlSearchCity.SelectedItem.Value.ToString().Split('|');
            objDeal.cityId = Convert.ToInt32(strCityIDs[1].Trim());
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
                try
                {
                    if (ddlSearchCity.SelectedItem.Text.Trim() == "Abbotsford" || ddlSearchCity.SelectedItem.Text.Trim() == "Surrey"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Vancouver" || ddlSearchCity.SelectedItem.Text.Trim() == "Victoria")
                    {
                        ddCampaignNewsLetterHours.SelectedIndex = 6;
                    }
                    else if (ddlSearchCity.SelectedItem.Text.Trim() == "Calgary" || ddlSearchCity.SelectedItem.Text.Trim() == "Edmonton"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Hamilton")
                    {
                        ddCampaignNewsLetterHours.SelectedIndex = 5;
                    }
                    else if (ddlSearchCity.SelectedItem.Text.Trim() == "Brampton" || ddlSearchCity.SelectedItem.Text.Trim() == "Toronto"
                        || ddlSearchCity.SelectedItem.Text.Trim() == "Mississauga")
                    {
                        ddCampaignNewsLetterHours.SelectedIndex = 3;
                    }
                    txtCampaignNewsLetterSubject.Text = ddlSearchDeal.SelectedItem.Text.Trim();
                    txtCampaignNewsLetterName.Text = "!" + Convert.ToDateTime(txtdlStartDate.Text.Trim()).ToString("yyyyMMdd").ToString() + ddlSearchCity.SelectedItem.Text.Trim().Substring(0, 3);
                    txtCampaignNewsLetterDate.Text = txtdlStartDate.Text.Trim();
                }
                catch (Exception ex)
                { }
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
            ltDealDetail.Text += "<html><head><title>News Letter</title><style type=\"text/css\">body{padding: 0px;padding: 0px;color: Black;line-height: normal;background-color: #f3f3f3;font: 12px/150% Arial;}.Contant{padding: 0 auto;width: 680px;}.TopLink{font-size: 11px;color: #3fafef;cursor: pointer;text-decoration: none;}.ExternalClass{width:100% !important;}        .TopLink:hover{font-size: 11px;color: #3fafef;cursor: pointer;text-decoration: underline;}img{border: none;}.style1{width: 72px;}.style2{}.style3{height: 5px;}.style4{width: 100%;}.style6{height: 120px;}</style></head>";
            ltDealDetail.Text += "<body><center><table style=\"width: 680px; padding-top: 10px; padding-bottom: 10px;\"><tbody><tr><td>" + txtExtraText.Text.Trim().Replace("\n", "</br>") + "</td></tr><tr><td style=\"text-align: center;\">";
            
            ltDealDetail.Text += "<unsubscribe>Unsubscribe</unsubscribe>";
            ltDealDetail.Text += "<span style=\"color: Black;cursor: default;\">|</span>";
           // ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "\">Go to Tazzling.com </a><span style=\"color: Black; cursor: default;\">|</span>";
            
            ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/newsletter.aspx?cid=" + ddlSearchCity.SelectedValue.ToString().Trim() +
                "&date=" + Convert.ToString(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00") + "&slot=" + ddlSearchDeal.SelectedIndex.ToString() + "\">View this email in browser </a>";

            ltDealDetail.Text += "</td></tr><tr><td style=\"text-align: center;\">add <a class=\"TopLink\" href=\"mailto:news@tazzling.com\">news@tazzling.com</a> to your address book or safe sender list so our emails get to your inbox. <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/howItWorks.aspx\">Learn how</a></td></tr>";
            ltDealDetail.Text += "<tr><td style='width:100%;'><table style=\"clear: both; height: 95px; width: 100%; background-color: #12a4e0;border-bottom: 5px solid #ff72c7;\"><tr class=\"Contant\"><td style=\"padding-top: 25px; float: left; padding-left:15px;\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "\"><img style='height:56px; width:209px;' height='56' width='209' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/NewLogo_2.png\" alt=\"Tastygo Logo\" /></a></td>";
            ltDealDetail.Text += "<td style=\"padding-top: 25px; text-align: center; float: left; padding-left:25px;\"><span style=\"padding-left: 15px; padding-top: 25px; text-align: center; width: 250px;\"><span style=\"clear: both; font-size: 13px; color: White; font-weight: bold;\">Your DailyDeal For</span><br /><span style=\"clear: both; font-size: 25px; color: White; padding-top: 10px; padding-left: 5px;\">";

            ltDealDetail.Text += ddlSearchCity.SelectedItem.Text.Trim() + "</span> </span></td>";

            ltDealDetail.Text += "<td style=\"padding-top: 25px; text-align: right;\"><table><tr><td><span style=\"padding-top: 25px; width: 250px;\"><span style=\"clear: both; font-size: 13px;color: White; padding-top: 25px; font-weight: bold;\">";
            ltDealDetail.Text += Convert.ToDateTime(txtdlStartDate.Text.Trim()).ToString("dddd MMMM dd, yyyy") + "</span><br /></span></td></tr>";

            ltDealDetail.Text += "<tr><td><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='padding-right:5px;height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/twitter1.png\" alt=\"Tweet With Us on Twitter\" /></a><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/facebook2.png\" alt=\"Follow us on Facebook\" /></a></td></tr></table> </td></tr></table></td></tr>";
            ltDealDetail.Text += "<tr><td style=\"height: 35px; background-color: #5f5f5f;\"><table style=\"width: 100%\"><tr><td style=\"text-align: left;\" align=\"left\"><table align=\"left\"><tr><td style=\"font-family: Arial; font-size: 18.6px; font-weight: bold; padding: 11px 0 0 10px;color: White; width: 470px;\">";

            string shortTitle = dtDeals.Rows[ddlSearchDeal.SelectedIndex]["shortTitle"].ToString();
            string dealPagetitel = (dtDeals.Rows[ddlSearchDeal.SelectedIndex]["dealPageTitle"].ToString());
            string titel = dtDeals.Rows[ddlSearchDeal.SelectedIndex]["title"].ToString();
            string toptitel = dtDeals.Rows[ddlSearchDeal.SelectedIndex]["topTitle"].ToString();


            ltDealDetail.Text += (shortTitle.Trim() != "" ? shortTitle.Trim().Length > 50 ? shortTitle.Substring(0, 47) + "..." : shortTitle : dealPagetitel.Trim() == "" ? titel.Trim().Length > 50 ? titel.Substring(0, 47) + "..." : titel : dealPagetitel.Trim().Length > 50 ? dealPagetitel.Substring(0, 47) + "..." : dealPagetitel);

            ltDealDetail.Text += "</td></tr><tr><td style=\"color: White; font-family: Arial; font-size: 12.44px; padding-left: 10px;padding-top: 5px; text-align: left;\">";


            ltDealDetail.Text += toptitel.Trim().Length > 85 ? toptitel.Substring(0, 82) + "..." : toptitel;

            ltDealDetail.Text += "</td></tr></table></td>";

            ltDealDetail.Text += "<td style=\"float: left; text-align: right;\"><table align=\"right\"><tr><td align=\"right\" style=\"width: 180px;\"><table><tr><td style=\"clear: both; font-family: Arial; font-size: 12.44px; font-weight: bold;padding-bottom: -5px; color: White\" class=\"style1\" align=\"center\"></td><td style=\"clear: both; font-family: Arial; font-size: 12.44px; font-weight: bold;padding-bottom: -5px;\" rowspan=\"2\">";

            ltDealDetail.Text += "<a target=\"_blank\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["dealId"].ToString().Trim() + "\"><img style='height:41px; width:59px;' height='41' width='59' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Viewbtn.png\" alt=\"#\" /></a></td></tr><tr><td style=\"clear: both; color: White; font-family: Arial;text-align:center; font-size: 31.1px;font-weight: bold; line-height: normal; width:100px;\" class=\"style1\" width='100'>";

            ltDealDetail.Text += "<sup><span style='font-size:11px; font-wight:normal; vertical-align:top'>Only</span> $</sup>" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["sellingPrice"].ToString().Trim() + "</td></tr></table></td></tr></table></td></tr></table></td></tr> <tr class=\"Contant\" style=\"height: 20px; padding-top: 5px; font-family: Arial;font-size: 12.44px; font-weight: bold;\"><td style=\"width: 45%; float: left; padding-left: 110px; color: Black;\">";

            double dSellPrice = Convert.ToDouble(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["sellingPrice"].ToString().Trim());
            double dActualPrice = Convert.ToDouble(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["valuePrice"].ToString().Trim());
            double dDiscount = 0;
            if (dSellPrice == 0)
            {
                dDiscount = 100;
            }
            else
            {
                dDiscount = (100 / dActualPrice) * (dActualPrice - dSellPrice);
            }


            ltDealDetail.Text += "Reg: <del>$" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["valuePrice"].ToString().Trim() + "</del>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;You Save " + Convert.ToInt32(dDiscount).ToString() + "%";

            ltDealDetail.Text += "</td></tr><tr><td style=\"padding-top: 10px; text-align: center;\">";
            ltDealDetail.Text += " <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["dealId"].ToString().Trim() + "\">";
            ltDealDetail.Text += "<img  src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/dealfood/" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["restaurantId"].ToString().Trim() + "/" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["image1"].ToString().Trim() + "\" style='width:464px; height:333px;' height='333' width='464' /></a></td></tr><tr><td style=\"padding-top: 8px; color: Black;font-weight: bold; float:left; width:660px; height:40px;\" height='40' width='660'>";

            ltDealDetail.Text += StripHTML(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["description"].ToString().Trim()).Trim().Length > 200 ? StripHTML(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["description"].ToString().Trim()).Substring(0, 199) + "..." : StripHTML(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["description"].ToString().Trim());
            ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["dealId"].ToString().Trim() + "\">Read More</a>";

            ltDealDetail.Text += " </td></tr><tr><td style=\" background-color: #ff72c7;\" class=\"style3\"></td></tr>";


            dtDeals.Rows.RemoveAt(ddlSearchDeal.SelectedIndex);
            dtDeals.AcceptChanges();
            int dealsCount = dtDeals.Rows.Count;
            int loopCount = 0;
            if (dtDeals.Rows.Count > 0)
            {
                while (dealsCount > 0)
                {
                    if (loopCount >= 6)
                    {
                        break;
                    }

                    if (loopCount == 0)
                    {
                        ltDealDetail.Text += "<tr><td style=\"color: #019CFF; font-family: Arial; font-size: 16.77px; font-weight: bold;text-align: left;\" class=\"style2\"><table cellspacing=\"0\" class=\"style4\"><tr><td>more deals<font style=\"color: #019CFF; font-family: Arial; font-size: 16.77px;\"> for you</font></td><td align=\"right\"></td></tr></table></td></tr>";
                    }

                    string shortTitle1 = dtDeals.Rows[loopCount]["shortTitle"].ToString();
                    string dealPagetitel1 = (dtDeals.Rows[loopCount]["dealPageTitle"].ToString());
                    string titel1 = dtDeals.Rows[loopCount]["title"].ToString();
                    string toptitel1 = dtDeals.Rows[loopCount]["topTitle"].ToString();
                    ltDealDetail.Text += " <tr><td style=\"height: 35px; background-color: #5f5f5f;\"><table style=\"width: 100%\"><tr><td style=\"text-align: left;\" align=\"left\"><table align=\"left\"><tr><td style=\"font-family: Arial; font-size: 18.6px; font-weight: bold; padding: 11px 0 0 10px;color: White; width:470px;\">";
                    ltDealDetail.Text += (shortTitle1.Trim() != "" ? shortTitle1.Trim().Length > 50 ? shortTitle1.Substring(0, 47) + "..." : shortTitle1 : dealPagetitel1.Trim() == "" ? titel1.Trim().Length > 50 ? titel1.Substring(0, 47) + "..." : titel1 : dealPagetitel1.Trim().Length > 50 ? dealPagetitel1.Substring(0, 47) + "..." : dealPagetitel1);
                    ltDealDetail.Text += "</td></tr><tr><td style=\"color: White; font-family: Arial; font-size: 12.44px; padding-left: 10px;padding-top: 5px; text-align: left;\">";
                    ltDealDetail.Text += toptitel1.Trim().Length > 85 ? toptitel1.Substring(0, 82) + "..." : toptitel1;
                    ltDealDetail.Text += "</td></tr></table></td>";
                    ltDealDetail.Text += "<td style=\"float: left; text-align: right;\"><table align=\"right\"><tr><td align=\"right\" style=\"width: 180px;\"><table><tr><td style=\"clear: both; font-family: Arial; font-size: 12.44px; font-weight: bold;padding-bottom: -5px; color: White\" class=\"style1\" align=\"center\"></td><td style=\"clear: both; font-family: Arial; font-size: 12.44px; font-weight: bold;padding-bottom: -5px;\" rowspan=\"2\">";
                    ltDealDetail.Text += "<a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[loopCount]["dealId"].ToString().Trim() + "\"><img style='height:41px; width:59px;' height='41' width='59' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Viewbtn.png\"/></a></td></tr>";
                    ltDealDetail.Text += " <tr><td style=\"clear: both; color: White; font-family: Arial; text-align:center;font-size: 31.1px;font-weight: bold; line-height: normal; width:100px;\" width='100' class=\"style1\">";
                    ltDealDetail.Text += "<sup><span style='font-size:11px; font-wight:normal; vertical-align:top'>Only</span> $</sup>" + dtDeals.Rows[loopCount]["sellingPrice"].ToString().Trim() + "</td></tr></table></td></tr></table></td></tr></table></td></tr>";
                    ltDealDetail.Text += "<tr><td class=\"style2\"><table><tr class=\"Contant\"><td align=\"left\" class=\"style6\">";
                    ltDealDetail.Text += "<a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[loopCount]["dealId"].ToString().Trim() + "\">";
                    ltDealDetail.Text += "<img src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/dealfood/" + dtDeals.Rows[loopCount]["restaurantId"].ToString().Trim() + "/" + dtDeals.Rows[loopCount]["image1"].ToString().Trim() + "\" style='width:160px; height:100px' height='100' width='160'/></a></td><td class=\"style6\"><table><tr class=\"Contant\" style=\"font-family: Arial;font-size: 12.44px; font-weight: bold;\"><td colspan=\"2\" align=\"left\">";

                    dSellPrice = Convert.ToDouble(dtDeals.Rows[loopCount]["sellingPrice"].ToString().Trim());
                    dActualPrice = Convert.ToDouble(dtDeals.Rows[loopCount]["valuePrice"].ToString().Trim());
                    dDiscount = 0;
                    if (dSellPrice == 0)
                    {
                        dDiscount = 100;
                    }
                    else
                    {
                        dDiscount = (100 / dActualPrice) * (dActualPrice - dSellPrice);
                    }
                    ltDealDetail.Text += "Reg: <del>$" + dtDeals.Rows[loopCount]["valuePrice"].ToString().Trim() + "</del>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;You Save " + Convert.ToInt32(dDiscount).ToString() + "%</td></tr><tr><td align=\"left\" style='width:480px;' width='480'>";
                    ltDealDetail.Text += StripHTML(dtDeals.Rows[loopCount]["description"].ToString().Trim()).Trim().Length > 65 ? StripHTML(dtDeals.Rows[loopCount]["description"].ToString().Trim()).Substring(0, 62) + "..." : StripHTML(dtDeals.Rows[loopCount]["description"].ToString().Trim());
                    ltDealDetail.Text += " <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[loopCount]["dealId"].ToString().Trim() + "\">Read More</a></td></tr> </table></td></tr></table></td></tr>";
                    ltDealDetail.Text += "<tr><td style=\" width:680px; background-color: #ff72c7;\" width='680' class=\"style3\" colspan=\"2\"></td></tr>";

                    dealsCount--;
                    loopCount++;

                }
            }
            ltDealDetail.Text += "<tr><td><table style='width:680px;' width='680'><tr><td style='width:340px; text-align:center;' width='340' align=\"center\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/apps.aspx\"><img style='height:87px; width:263px;' height='87' width='263' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/MobileApps.png\"/></a></td>";
            ltDealDetail.Text += "<td style='width:340px; text-align:center;' width='340' align=\"center\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/affiliate.aspx\"><img style='height:87px; width:263px;' height='87' width='263' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Dollars.png\"/></a></td></tr></table>";
            ltDealDetail.Text += "</td></tr><tr><td align=\"left\" style=\"padding: 5px; color: White; font-size: medium; background-color: #000000; width:680px;\" width='680'>Question Or Coment? Contact Us</td></tr><tr><td align=\"left\" style=\"background-color: White; width: auto; height: 1px;\" width=\"2px\"></td></tr><tr><td style=\"background-color: #494949;\"><table><tr><td align=\"left\" valign=\"middle\" width=\"160px\"><table><tr><td width=\"165px\">";
            ltDealDetail.Text += "<img style='height:16px; width:157px;' height='16' width='157' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/FooterContactUs.png\"/>&nbsp;&nbsp;&nbsp;</td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Twitter2.png\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Fb.png\" alt=\"#\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/RSS.aspx\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/RSS2.png\" alt=\"#\" /></a></td></tr></table></td></tr>";
            ltDealDetail.Text += "<tr><td style=\"padding: 5px; background-color: #494949; color: White; float: left; width: 663px; height: 60px;\" align=\"left\">";
            ltDealDetail.Text += "This email was sent by: TastyGo Online Inc. 20-206 E 6th Ave Vancouver, BC, V5T 1J7, Canada. You are receiving this email because you signed up for the<br /> ";
            ltDealDetail.Text += "Daily Tastygo Deal alerts. If you prefer not to receive Daily Tastygo emails, you can always <unsubscribe>Unsubscribe</unsubscribe>.<br />";
            ltDealDetail.Text += "</td></tr></table></td></tr></tbody></table></center>  </body></html>";

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
