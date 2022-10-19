using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using System.IO;
using ExactTargetAPI;

public partial class Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            #region NewCode
            createEmail();
            //createSchedule();
            #endregion            
        }
        catch (Exception exc)
        {
            //Set Message
            lblMessage.Text += "<br/><br/><h3>ERROR</h3><br/>" + exc.Message;
        }

    }

    private void createSchedule()
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
        em.ID = 4212;//required //Available in the ET UI [Content > My Emails > Properties]
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
        esd.SendDefinitionList[0].List.ID = 445;  //This is Pub List Id  
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
        DateTime time = new DateTime(2012, 3, 1, 00, 00, 00, DateTimeKind.Unspecified).ToUniversalTime();

        sd.StartDateTime = time;
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

            //Display Results
            //lblMessage.Text += "Overall Status: " + cStatus;
            //lblMessage.Text += "<br/>";
            //lblMessage.Text += "Number of Results: " + cResults.Length;
            //lblMessage.Text += "<br/>";

            //Loop through each object returned and display the StatusMessage
            //foreach (CreateResult cr in cResults)
            //{
            //    lblMessage.Text += "Status Message: " + cr.StatusMessage;
            //    lblMessage.Text += "<br/>";
            //}
        }
        catch (Exception exCreate)
        {
            //Set Message
            lblMessage.Text += "<br/><br/>CREATE ERROR:<br/>" + exCreate.Message;
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

    protected void createEmail()
    {
        Email email = new Email();
        string requestID = "";
        string status = "";
        try
        {
            SoapClient client = new SoapClient();
            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();
            

            email.Subject = "Check out my way cool email";
            email.Name = "Dynamic Email_" + DateTime.Now.ToShortTimeString();
            email.HTMLBody = "<custom name=\"opencounter\" type=\"tracking\"><html><head><style>.hmmessage P{margin: 0px;padding: 0px;}body.hmmessage{font-size: 10pt;font-family: Tahoma;}</style></head><body class=\"hmmessage\"><div dir='ltr'><div><style>.ExternalClass .ecxhmmessage P{padding: 0px;}.ExternalClass body.ecxhmmessage{font-size: 9pt;}</style><div dir=\"ltr\"><div><style>.ExternalClass{height: 100%;padding: 0px;}.ExternalClass .ecxReadMsgBody{width: 100%;}.ExternalClass{width: 100%;}</style> <table style=\"width: 100%\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr><td valign=\"top\" align=\"center\"><table style=\"width: 1px\" width=\"1px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr><td valign=\"top\" align=\"center\"><table width=\"600px\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family: Arial, Helvetica, sans-serif;font-size: 10px; color: #676767; text-align: center\"><tr><td style=\"width: 120px;\"><a href=\"%%unsub_center_url%%\" alias=\"Unsubscribe\">Unsubscribe</a></td><td style=\"width: 20px;\">|</td><td style=\"width: 150px;\"><a href=\"http://www.tazzling.com/Calgary\" target=\"_blank\">Go to Tastygo</a></td><td style=\"width: 20px;\">|</td><td width=\"210px\">Your Calgary's Daily Deal</td></tr><tr><td colspan=\"5\" height=\"5\"></td></tr></table><table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family: Arial, Helvetica, sans-serif;font-size: 10px; color: #676767; text-align: center\"><tr><td>Be sure to add <a href=\"mailto:mail@e.tazzling.com\">mail@e.tazzling.com</a> to your address book or safe sender list so ouremails get to your inbox. <a href=\"http://www.tazzling.com/whitelist.aspx\" target=\"_blank\">Learn how</a></td></tr><tr><td height=\"10\"></td></tr></table><table width=\"660\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#0099ff\" style=\"border: 10px solid #d8e3f3;background-color:#0099ff;\"><tr><td valign=\"top\" style=\"padding-left:10px; padding-right:10px;\"><table width=\"620\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" ><tr><td valign=\"top\" style=\"height: 96px;\"><div style=\"float:left; padding-top:10px;\"><img src=\"http://www.tazzling.com/Images/logoForNewsLetter.png\" height=\"62px\" width=\"230px\" /></div> <div style=\"float:right;\"><table width=\"374\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"><tr><td width=\"218\" height=\"25\"></td><td colspan=\"4\"><span style=\"font-size: 12px; color: #FFF; font-family: Arial, Helvetica, sans-serif;padding-left: 30px;\">February 27, 2012</span></td></tr><tr><td rowspan=\"2\"><span style=\"font-size: 22px; color: #FFF; font-family: Arial, Helvetica, sans-serif;\">The Daily Deal For Calgary</span></td><td colspan=\"4\">&nbsp;</td></tr><tr><td width=\"68\"></td><td width=\"40\"><a href=\"http://www.twitter.com/tastygo\"><img src=\"http://www.tazzling.com/images/twHomeRight.jpg\" border=\"0\" /></a></td><td width=\"4\">&nbsp;</td><td width=\"44\"><a href=\"http://www.facebook.com/tastygo\"><img src=\"http://www.tazzling.com/images/fbHomeRight.jpg\" border=\"0\" /></a></td></tr></table></div></td></tr><tr><td valign=\"top\" bgcolor=\"#FFFFFF\"><table width=\"600\" border=\"0\" bgcolor=\"#e9e9e9\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"style=\"margin: 10px;\"><tr><td width=\"234\" height=\"10\"></td><td width=\"366\"></td></tr><tr><td height=\"44\" valign=\"top\" style=\"padding-left: 10px;\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #0081d3; text-decoration: underline\"><a style=\"color: #0081d3;\" href=\"http://www.tazzling.com/Calgary_502\">59% Off Roadmap to Success Book + CD</a></span></td><td rowspan=\"3\" align=\"center\"><table width=\"258\" bgcolor=\"#FFFFFF\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"style=\"padding: 6px; border: 1px solid #c0c0c0;\"><tr><td><a href=\"http://www.tazzling.com/Calgary_502\"><img src=\"http://www.tazzling.com/images/dealfood/375/3a4a850c-8238-47cc-949d-2a1e3d69f0ef.jpg\" width=\"244\" height=\"175\"></a></td></tr></table></td></tr><tr><td valign=\"top\" style=\"padding-left: 10px;padding-top:5px; padding-bottom:5px;\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #686868;\">The Happiness Motivator - Online</td></tr><tr><td height=\"132\" valign=\"top\" style=\"padding-left: 10px;\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #686868;\">\"The power of a smile is sufficient to ignite a lifetime of happiness,\" says Rozhy. If you are looking to be inspired and uplift your spirit, today's deal from The Happ...</span></td></tr><tr><td align=\"right\" style=\"padding-left: 10px;\"><a href=\"http://www.tazzling.com/Calgary_502\"><img src=\"http://www.tazzling.com/images/btn_dealviewSmall.png\" width=\"81\" height=\"18\" border=\"0\" /></a></td></tr><tr><td height=\"10\"></td><td></td></tr></table><table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><td colspan=\"2\" bgcolor=\"#0099ff\"><div style=\"padding:5px;\"><span style=\"font-size: 22px; color: #FFF; font-family: Arial, Helvetica, sans-serif;\">More Great Tasty Deals</span></div></td></tr><tr><td width=\"303\" align=\"left\"><table width=\"297\" border=\"0\" align=\"left\" cellpadding=\"0\" cellspacing=\"0\"><tr><td width=\"297\" height=\"178\" valign=\"top\" bgcolor=\"#e9e9e9\"><table width=\"297\" border=\"0\" background=\"images/img_bg.jpg\" height=\"169\" cellspacing=\"0\"cellpadding=\"0\"><tr><td height=\"169\" valign=\"top\" align=\"center\"><a target=\"_blank\" href=\"http://www.tazzling.com/Calgary_566\"><img src=\"http://www.tazzling.com/images/dealfood/421/95cfb2e6-dda3-46e3-a776-7caa59075d0f.jpg\" width=\"244\" height=\"175\" /></a></td></tr></table></td></tr><tr><td height=\"23\" valign=\"top\" bgcolor=\"#e9e9e9\" style=\"padding-left: 10px;\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #0081d3; text-decoration: underline\"><a target=\"_blank\" style=\"color: #0081d3;\" href=\"http://www.tazzling.com/Calgary_566\">52% Discount on iCamera Faux Camera iPhone 4/4s Protective Case</a></span></td></tr><tr><td height=\"25\" valign=\"bottom\" bgcolor=\"#e9e9e9\" style=\"padding-left: 10px;\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #686868;\">Goxprice.com</span></td></tr><tr><td height=\"41\" align=\"right\" valign=\"bottom\" bgcolor=\"#e9e9e9\" style=\"padding-right: 10px;\"><a target=\"_blank\" href=\"http://www.tazzling.com/Calgary_566\"><img src=\"http://www.tazzling.com/images/btn_dealviewSmall.png\" width=\"81\" height=\"18\" border=\"0\" /></a></td></tr><tr><td valign=\"top\" bgcolor=\"#e9e9e9\" align=\"right\" style=\"padding-right: 10px;\">&nbsp;</td></tr></table></td><td width=\"297\" valign=\"top\"><table width=\"297\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td width=\"297\" height=\"178\" valign=\"top\" bgcolor=\"#e9e9e9\"><table width=\"297\" border=\"0\" background=\"images/img_bg.jpg\" height=\"169\" cellspacing=\"0\"cellpadding=\"0\"><tr><td height=\"169\" valign=\"top\" align=\"center\"><a target=\"_blank\" href=\"http://www.tazzling.com/Calgary_523\"><img src=\"http://www.tazzling.com/images/dealfood/394/7a82f7bf-be96-45fe-93c3-547f15a965ba.jpg\" width=\"244\" height=\"175\" /></a></td></tr></table></td></tr><tr><td height=\"23\" valign=\"top\" bgcolor=\"#e9e9e9\" style=\"padding-left: 10px;\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold;color: #0081d3; text-decoration: underline\"><a target=\"_blank\" style=\"color: #0081d3;\" href=\"http://www.tazzling.com/Calgary_523\">77% Off Junk Removal Service</a></span></td></tr><tr><td height=\"25\" valign=\"bottom\" bgcolor=\"#e9e9e9\" style=\"padding-left: 10px;\"><span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #686868;\">1-888-JUNK-VAN - Vancouver, Toronto and Calgary</span></td></tr><tr><td height=\"41\" align=\"right\" valign=\"bottom\" bgcolor=\"#e9e9e9\" style=\"padding-right: 10px;\"><a target=\"_blank\" href=\"http://www.tazzling.com/Calgary_523\"><img src=\"http://www.tazzling.com/images/btn_dealviewSmall.png\" width=\"81\" height=\"18\" border=\"0\" /></a></td></tr><tr><td valign=\"top\" bgcolor=\"#e9e9e9\" align=\"right\" style=\"padding-right: 10px;\">&nbsp;</td></tr></table></td></tr><tr><td></td><td></td></tr><tr><td></td><td></td></tr></table></td></tr><tr><td valign=\"top\" bgcolor=\"#FFFFFF\">&nbsp;</td></tr><tr><td height=\"40\" align=\"center\" valign=\"middle\"><span style=\"font-family: Arial, Helvetica, sans-serif; color: #185961; font-size: 12px;\"><a target=\"_blank\" style=\"color: #185961;\" href=\"http://www.tazzling.com/contact-us.aspx\">Need help? Have feedback? Contact Us</a></span></td></tr></table></td></tr></table><table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family: Arial, Helvetica, sans-serif;font-size: 10px; color: #676767; text-align: center\"><tr><td height=\"20\"></td></tr><tr><td>You are receiving this email because %%emailaddr%% is signed up for the Daily Tastygo Deal alerts. If you prefer not to receive Daily Tastygo emails, you can always <a href=\"%%unsub_center_url%%\" alias=\"Unsubscribe\">Unsubscribe</a>. Please don\"t reply to this email--there are better ways to get in touch with us, like renting the billboard outside our office or emailing customer service <a href=\"mailto:info@tazzling.com\">here</a>.</td></tr><tr><td style=\"border-bottom: 1px solid #999\">&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td><table cellpadding=\"2\" cellspacing=\"0\" width=\"600\" ID=\"Table5\" Border=0><tr><td><font=\"verdana\" size=\"1\" color=\"#444444\">This email was sent by: <b>%%Member_Busname%%</b><br>%%Member_Addr%% %%Member_City%%, %%Member_State%%, %%Member_PostalCode%%, %%Member_Country%%<br><br></font></td></tr></table>To purchase the TastyGo voucher described in today\"s deal, you must press the Buy! Button and follow the instructions. Once you make the purchase, you will receivethe voucher via e-mail. This e-mail is not a valid TastyGo voucher, even if youbuy today\"s deal — the only valid voucher is the voucher you receive via Tazzling.com member area after you login after your purchase <a href=\"%%profile_center_url%%\" alias=\"Update Profile\">Update Profile</a>.</td></tr></table></td></tr></tbody></table></td></tr></tbody></table></div></div></div></div></body></html>";            
            email.IsHTMLPaste=true;
            email.IsHTMLPasteSpecified = true;
            email.TextBody = email.HTMLBody.ToString();
            email.CharacterSet = "UTF-8";
            email.CategoryID = 1845;  //This is the Folder where the email should be stored.  
            //You can get the Category ID by hovering over the folder in the account and looking at the CID= in the URL at the bottom.                    



            email.CategoryIDSpecified = true; //Not sure this is needed for PHP, but is needed for .Net  
            CreateResult[] results = client.Create(new CreateOptions(), new APIObject[] { email }, out requestID, out status);
        }
        catch (Exception ex)
        { }
    }
  
}


