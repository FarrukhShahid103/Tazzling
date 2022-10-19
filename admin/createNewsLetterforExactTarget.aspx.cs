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
using ExactTargetAPI;
public partial class createNewsLetterforExactTarget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl myJs = new HtmlGenericControl();
        myJs.TagName = "script";
        myJs.Attributes.Add("type", "text/javascript");
        myJs.Attributes.Add("language", "javascript"); //don\"t need it usually but for cross browser.
        myJs.Attributes.Add("src", ResolveUrl("JS/CalendarControl.js"));
        Page.Header.Controls.Add(myJs);
   
        if (!IsPostBack)
        {
            txtdlStartDate.Text = DateTime.Now.AddDays(1).ToString("MM-dd-yyyy");
            ddlDLStartHH.SelectedValue = "00";
            ddlDLStartPortion.SelectedValue = "AM";
            ddlDLStartMM.SelectedValue = "00";

            FillDealsDroDownList(ddlSearchCity.SelectedValue.Trim());
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
            try
            {
                if (ddlSearchCity.SelectedItem.Text.Trim() == "Abbotsford" || ddlSearchCity.SelectedItem.Text.Trim() == "Surrey"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Vancouver" || ddlSearchCity.SelectedItem.Text.Trim() == "Victoria")
                {
                    ddNewsLetterHours.SelectedIndex = 6;
                }
                else if (ddlSearchCity.SelectedItem.Text.Trim() == "Calgary" || ddlSearchCity.SelectedItem.Text.Trim() == "Edmonton")
                {
                    ddNewsLetterHours.SelectedIndex = 5;
                }
                else if (ddlSearchCity.SelectedItem.Text.Trim() == "Brampton" || ddlSearchCity.SelectedItem.Text.Trim() == "Toronto"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Mississauga" || ddlSearchCity.SelectedItem.Text.Trim() == "Hamilton"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Halifax" || ddlSearchCity.SelectedItem.Text.Trim() == "York Region"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Oakville - Burlington")
                {
                    ddNewsLetterHours.SelectedIndex = 3;
                }
                txtNewsLetterSubject.Text = ddlSearchDeal.SelectedItem.Text.Trim();
                txtNewsLetterName.Text = "!" + Convert.ToDateTime(txtdlStartDate.Text.Trim()).ToString("yyyyMMdd").ToString() + ddlSearchCity.SelectedItem.Text.Trim().Substring(0, 3);
                txtNewsLetterDate.Text = txtdlStartDate.Text.Trim();

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
    
    protected void imgbtnSend_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNewsLetterDate.Text.Trim() != "")
        {
            DateTime dtNewsLeterTime = DateTime.Parse(txtNewsLetterDate.Text.Trim() + " " + ((ddNewsLetterTimeSpan.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddNewsLetterHours.SelectedItem.Text.Trim()) + 12).ToString() : ddNewsLetterHours.SelectedItem.Text.Trim()).ToString() + ":" + ddNewsLetterMinutes.SelectedItem.Text + ":" + "00").AddHours(8);
            if (dtNewsLeterTime > DateTime.Now)
            {
                int intEmail = createEmail();
                if (intEmail > 0)
                {
                    createSchedule(intEmail, dtNewsLeterTime);
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
        else
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.Text = "Please enter future date for newsletter schedule.";
        }

    }

    #region Code To generate Email and schedule it on exactTarget
    protected int createEmail()
    {
        Email email = new Email();
        string requestID = "";
        string status = "";
        int intEmailID = 0;
        try
        {
            SoapClient client = new SoapClient();
            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

            
            email.Subject = txtNewsLetterSubject.Text.Trim();
            email.Name = txtNewsLetterName.Text.Trim();
            email.HTMLBody = ltDealDetail.Text.Trim();
            email.IsHTMLPaste = true;
            email.IsHTMLPasteSpecified = true;
            email.TextBody = email.HTMLBody.ToString();
            email.CharacterSet = "UTF-8";
            email.CategoryID = 1845;  //This is the Folder where the email should be stored.  
            //You can get the Category ID by hovering over the folder in the account and looking at the CID= in the URL at the bottom.                    
            email.CategoryIDSpecified = true; //Not sure this is needed for PHP, but is needed for .Net  
            CreateResult[] results = client.Create(new CreateOptions(), new APIObject[] { email }, out requestID, out status);
            if (status.ToUpper().Trim() == "OK")
            {
                intEmailID = results[0].NewID;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.Text =  results[0].StatusMessage;
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
        return intEmailID;
    }

    private void createSchedule(int emailID,DateTime dtScheduleTime)
    {
        SoapClient client = new SoapClient();
        client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
        client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

        string strSchedule2 = Guid.NewGuid().ToString();

        EmailSendDefinition esd = new EmailSendDefinition();
        esd.Name = strSchedule2;//required but you can create your own unique name
        esd.CustomerKey = strSchedule2;//required key for send definition, but you can change to your own value if you do not want to use


        //Create a GUID for ESD to ensure a unique name and customer key
        //string strGUID = System.Guid.NewGuid().ToString(); //comment this line if you do not want to utilize the strGUID functionality

        //Create Email object to refer to pre-create Email
        Email em = new Email();
        em.ID = emailID;//required //Available in the ET UI [Content > My Emails > Properties]
        em.IDSpecified = true;//required


        //Apply Email object to the EmailSendDefiniton object
        esd.Email = em;//required

        //Create SendClassification

        esd.SendClassification = retrieveSendClassifications(client); //required

        DataExtension de = new DataExtension();
        de.ObjectID = Guid.NewGuid().ToString();

        //Set Send Definition List for the EmailSendDefinition
        esd.SendDefinitionList = new SendDefinitionList[1];//You can have many SendDefinitonLists of different SendDefinitionListTypes
        esd.SendDefinitionList[0] = new SendDefinitionList();
        esd.SendDefinitionList[0].SendDefinitionListType = SendDefinitionListTypeEnum.SourceList;
        esd.SendDefinitionList[0].SendDefinitionListTypeSpecified = true;
        //esd.SendDefinitionList[0].CustomObjectID = de.ObjectID;// "0A4B0038-7963-42E0-9324-811688C4EBBA";//jek data extension  // You will need to do a retrieve on the DataExtension Object in order to find this value
        //esd.SendDefinitionList[0].DataSourceTypeID = DataSourceTypeEnum.CustomObject;//This specifies a Data Extention

        esd.SendDefinitionList[0].List = new List();
        if (this.ddlSearchCity.SelectedValue.Trim() == "337")
        {
            esd.SendDefinitionList[0].List.ID = 215;  //This is Pub List Id  
        }
        else if (this.ddlSearchCity.SelectedValue.Trim() == "338")
        {
            esd.SendDefinitionList[0].List.ID = 613;  //This is Pub List Id  
        }
        else 
        {
            esd.SendDefinitionList[0].List.ID = Convert.ToInt32(ddlSearchCity.SelectedValue.Trim());  //This is Pub List Id  
        }
        
        esd.SendDefinitionList[0].DataSourceTypeID = DataSourceTypeEnum.List;
        esd.SendDefinitionList[0].DataSourceTypeIDSpecified = true;
        esd.SendDefinitionList[0].List.IDSpecified = true;
        esd.IsWrapped = true;
        esd.IsWrappedSpecified = true;


        string strSchedule = Guid.NewGuid().ToString();

        ScheduleDefinition sd = new ScheduleDefinition();
        sd.Name = strSchedule;
        sd.CustomerKey = strSchedule;
        sd.Description = strSchedule;
        sd.RecurrenceType = RecurrenceTypeEnum.Daily;
        sd.RecurrenceTypeSpecified = true;
        sd.RecurrenceRangeType = RecurrenceRangeTypeEnum.EndAfter;
        sd.RecurrenceRangeTypeSpecified = true;
        sd.Occurrences = 1;
        sd.OccurrencesSpecified = true;
        //DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(1).Hour, DateTime.Now.AddMinutes(10).Minute, 00, DateTimeKind.Unspecified).ToUniversalTime();
        //DateTime time = new DateTime(2012, 3, 1, 00, 00, 00, DateTimeKind.Unspecified).ToUniversalTime();

        sd.StartDateTime = dtScheduleTime;
        sd.StartDateTimeSpecified = true;

        DailyRecurrence dr = new DailyRecurrence();
        dr.DailyRecurrencePatternType = DailyRecurrencePatternTypeEnum.Interval;
        dr.DailyRecurrencePatternTypeSpecified = true;
        dr.DayInterval = 1;
        dr.DayIntervalSpecified = true;
        sd.Recurrence = dr;
        String o1;
        String o2;
        String r;

        try
        {
            string cRequestID = String.Empty;
            string cStatus = String.Empty;

            //Call the Create method on the Subscriber object
            CreateResult[] cResults = client.Create(new CreateOptions(), new APIObject[] { esd }, out cRequestID, out cStatus);

            ScheduleResult[] s = client.Schedule(new ScheduleOptions(), "start", sd, new APIObject[] { esd }, out o1, out o2, out r);

            if (o1.ToUpper().Trim() == "OK")
            {
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/checked.png";
                lblMessage.Visible = true;
                lblMessage.Text = "Newsletter scheduled Successfully.";
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.Text = s[0].StatusMessage.ToString();
            }
          
        }
        catch (Exception exCreate)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.Text = exCreate.Message;           
        }
    }

    protected SendClassification retrieveSendClassifications(SoapClient client)
    {
        String[] props = {
                                 "ObjectID", "Name", "Description", "CustomerKey", "SenderProfile.CustomerKey",
                                 "Client.ID"
                                 , "Client.PartnerClientKey", "PartnerKey"
                             };

        RetrieveRequest request = new RetrieveRequest();
        request.ObjectType = "SendClassification";
        request.Properties = props;

        //// Use Cleind-Id object if you are acting on Sub-account.
        //ClientID clientID = new ClientID();
        ////clientID.PartnerClientKey = "partnerkey_subaccount_2"; //Same as Account.PartnerKey
        //clientID.ID = 225115;
        //clientID.IDSpecified = true;
        //ClientID[] clientIDs = { clientID };
        //request.ClientIDs = clientIDs;

        //Default Transactional
        SimpleFilterPart filterPart = new SimpleFilterPart();
        //By default account will come with 2 classifications, one: Commerical two: Transactional 
        String[] filterValues = { "Default Commercial" };
        filterPart.Property = "Name";
        filterPart.SimpleOperator = SimpleOperators.equals;
        filterPart.Value = filterValues;
        request.Filter = filterPart;


        String requestId = null;
        APIObject[] results;

        client.Retrieve(request, out requestId, out results);
        SendClassification classification = null;
        if (results != null)
        {
            classification = (SendClassification)results[0];
            Console.Write("Results ::: " + results.Length);
        }
        return classification;
    }

    #endregion

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
                try
                {
                    if (ddlSearchCity.SelectedItem.Text.Trim() == "Abbotsford" || ddlSearchCity.SelectedItem.Text.Trim() == "Surrey"
                       || ddlSearchCity.SelectedItem.Text.Trim() == "Vancouver" || ddlSearchCity.SelectedItem.Text.Trim() == "Victoria")
                    {
                        ddNewsLetterHours.SelectedIndex = 6;
                    }
                    else if (ddlSearchCity.SelectedItem.Text.Trim() == "Calgary" || ddlSearchCity.SelectedItem.Text.Trim() == "Edmonton")
                    {
                        ddNewsLetterHours.SelectedIndex = 5;
                    }
                    else if (ddlSearchCity.SelectedItem.Text.Trim() == "Brampton" || ddlSearchCity.SelectedItem.Text.Trim() == "Toronto"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Mississauga" || ddlSearchCity.SelectedItem.Text.Trim() == "Hamilton"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Halifax" || ddlSearchCity.SelectedItem.Text.Trim() == "York Region"
                    || ddlSearchCity.SelectedItem.Text.Trim() == "Oakville - Burlington" || ddlSearchCity.SelectedItem.Text.Trim() == "St. Catharines")
                    {
                        ddNewsLetterHours.SelectedIndex = 3;
                    }
                    txtNewsLetterSubject.Text = ddlSearchDeal.SelectedItem.Text.Trim();
                    txtNewsLetterName.Text = "!" + Convert.ToDateTime(txtdlStartDate.Text.Trim()).ToString("yyyyMMdd").ToString() + ddlSearchCity.SelectedItem.Text.Trim().Substring(0, 3);
                    txtNewsLetterDate.Text = txtdlStartDate.Text.Trim();
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

            ltDealDetail.Text += "<custom name=\"opencounter\" type=\"tracking\"><!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            ltDealDetail.Text += "<html><head><title>News Letter</title><style type=\"text/css\">body{padding: 0px;padding: 0px;color: Black;line-height: normal;background-color: #f3f3f3;font: 12px/150% Arial;}.Contant{padding: 0 auto;width: 680px;}.TopLink{font-size: 11px;color: #3fafef;cursor: pointer;text-decoration: none;}.ExternalClass{width:100% !important;}        .TopLink:hover{font-size: 11px;color: #3fafef;cursor: pointer;text-decoration: underline;}img{border: none;}.style1{width: 72px;}.style2{}.style3{height: 5px;}.style4{width: 100%;}.style6{height: 120px;}</style></head>";
            ltDealDetail.Text += "<body><center><table style=\"width: 680px; padding-top: 10px; padding-bottom: 10px;\"><tbody><tr><td>"+txtExtraText.Text.Trim().Replace("\n","</br>")+"</td></tr><tr><td style=\"text-align: center;\">";
            ltDealDetail.Text += " <a style=\"color:#0792bd;\" href=\"%%unsub_center_url%%\" alias=\"Unsubscribe\">Unsubscribe</a>";
            ltDealDetail.Text += "<span style=\"color: Black;cursor: default;\">|</span>";
            ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/newsletter.aspx?cid=" + ddlSearchCity.SelectedValue.ToString().Trim() +
                "&date=" + Convert.ToString(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00") + "&slot=" + ddlSearchDeal.SelectedIndex.ToString()+"\">View this email in browser </a>";
            
            //ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() +"\">Go to Tazzling.com </a><span style=\"color: Black; cursor: default;\">|</span>";
            //ltDealDetail.Text += " <a style=\"color:#0792bd;\" href=\"%%unsub_center_url%%\" alias=\"Unsubscribe\">Unsubscribe</a>";
            ltDealDetail.Text += "</td></tr><tr><td style=\"text-align: center;\">add <a class=\"TopLink\" href=\"mailto:mail@e.tazzling.com\">mail@e.tazzling.com</a> to your address book or safe sender list so our emails get to your inbox. <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() +"/howItWorks.aspx\">Learn how</a></td></tr>";
            ltDealDetail.Text += "<tr><td style='width:100%;'><table style=\"clear: both; height: 95px; width: 100%; background-color: #12a4e0;border-bottom: 5px solid #ff72c7;\"><tr class=\"Contant\"><td style=\"padding-top: 25px; float: left; padding-left:15px;\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "\"><img style='height:56px; width:209px;' height='56' width='209' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/NewLogo_2.png\" alt=\"Tastygo Logo\" /></a></td>";
            ltDealDetail.Text += "<td style=\"padding-top: 25px; text-align: center; float: left;padding-left:25px;\"><span style=\"padding-left: 15px; padding-top: 25px; text-align: center; width: 250px;\"><span style=\"clear: both; font-size: 13px; color: White; font-weight: bold;\">Your DailyDeal For</span><br /><span style=\"clear: both; font-size: 25px; color: White; padding-top: 10px; padding-left: 5px;\">";

            ltDealDetail.Text += ddlSearchCity.SelectedItem.Text.Trim() + "</span> </span></td>";

            ltDealDetail.Text += "<td style=\"padding-top: 25px; text-align: right;\"><table><tr><td><span style=\"padding-top: 25px; width: 250px;\"><span style=\"clear: both; font-size: 13px;color: White; padding-top: 25px; font-weight: bold;\">";
            ltDealDetail.Text += Convert.ToDateTime(txtdlStartDate.Text.Trim()).ToString("dddd MMMM dd, yyyy") + "</span><br /></span></td></tr>";

            ltDealDetail.Text += "<tr><td><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='padding-right:5px;height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/twitter1.png\" alt=\"Tweet With Us on Twitter\" /></a><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/facebook2.png\" alt=\"Follow us on Facebook\" /></a></td></tr></table> </td></tr></table></td></tr>";
            ltDealDetail.Text += "<tr><td style=\"height: 40px; padding-top: 5px; background-color: #5f5f5f;\"><table style=\"width: 100%\"><tr><td style=\"text-align: left;\" align=\"left\"><table align=\"left\"><tr><td style=\"font-family: Arial; font-size: 18.6px; font-weight: bold; padding: 11px 0 0 10px;color: White; width: 470px;\">";

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
            ltDealDetail.Text += "<img  src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/dealfood/" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["restaurantId"].ToString().Trim() + "/" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["image1"].ToString().Trim() + "\" style='width:464px; height:333px;' height='333' width='464' /></a></td></tr><tr><td style=\"padding-top: 8px; text-align: left; color: Black;font-weight: bold; width:680px;height:40px;\" height='40' width='680'>";

            ltDealDetail.Text += StripHTML(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["description"].ToString().Trim()).Trim().Length > 200 ? StripHTML(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["description"].ToString().Trim()).Substring(0, 199) + "..." : StripHTML(dtDeals.Rows[ddlSearchDeal.SelectedIndex]["description"].ToString().Trim());
            ltDealDetail.Text += " <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName + "_" + dtDeals.Rows[ddlSearchDeal.SelectedIndex]["dealId"].ToString().Trim() + "\">Read More</a>";

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
                    ltDealDetail.Text += " <tr><td style=\"height: 40px; padding-top: 5px; background-color: #5f5f5f;\"><table style=\"width: 100%\"><tr><td style=\"text-align: left;\" align=\"left\"><table align=\"left\"><tr><td style=\"font-family: Arial; font-size: 18.6px; font-weight: bold; padding: 11px 0 0 10px;color: White; width:470px;\">";
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
                    ltDealDetail.Text += "Reg: <del>$" + dtDeals.Rows[loopCount]["valuePrice"].ToString().Trim() + "</del>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;You Save " + Convert.ToInt32(dDiscount).ToString() + "%</td></tr><tr><td style='width:480px;' width='480' align=\"left\">";
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
            ltDealDetail.Text += "<img style='height:16px; width:157px;' height='16' width='157' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/FooterContactUs.png\"/>&nbsp;&nbsp;&nbsp;</td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Twitter2.png\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Fb.png\" alt=\"#\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/RSS.aspx\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/RSS2.png\" /></a></td></tr></table></td></tr>";
            ltDealDetail.Text += "<tr><td style=\"padding: 5px; background-color: #494949; color: White; float: left; width: 620px;height:140px;\" align=\"left\">";
            ltDealDetail.Text += "You are receiving this email because<span style=\"color:#0792bd;\"> %%emailaddr%% </span>is signed up for the Daily Tastygo Deal alerts. If you prefer not to receive Daily Tastygo emails, you can always <a  href=\"%%unsub_center_url%%\" alias=\"Unsubscribe\" style=\"color:#0792bd;\">Unsubscribe</a>.";
                                    
            ltDealDetail.Text += "<table cellpadding=\"2\" cellspacing=\"0\" width=\"600px\" ID=\"Table5\" Border=0><tr><td width\"100%\"><span style=\"color:white;\">";
            ltDealDetail.Text += "This email was sent by: <b>%%Member_Busname%%</b><br>%%Member_Addr%% %%Member_City%%, %%Member_State%%, %%Member_PostalCode%%, %%Member_Country%%<br><br></span></td></tr></table>";

            ltDealDetail.Text += "<a style=\"color:#0792bd;\" href=\"%%profile_center_url%%\" alias=\"Update Profile\">Update Profile</a>.";
            ltDealDetail.Text += "</td></tr></table></td></tr></tbody></table></td></tr></td></tr></table></td></tr> </tbody></table></center>  </body></html>";


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
