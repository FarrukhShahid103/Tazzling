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
using ExactTargetAPI;

public partial class member_SubscribeCities : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {          
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Subscription Cities";
            if (!IsPostBack)
            {                
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                {

                    DataTable dtUser = null;
                    if (Session["member"] != null)
                    {
                        dtUser = (DataTable)Session["member"];
                    }
                    else if (Session["restaurant"] != null)
                    {
                        dtUser = (DataTable)Session["restaurant"];
                    }
                    else if (Session["sale"] != null)
                    {
                        dtUser = (DataTable)Session["sale"];
                    }
                    else if (Session["user"] != null)
                    {
                        dtUser = (DataTable)Session["user"];
                    }

                    obj.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
                    DataTable dtUserInfo = null;
                    dtUserInfo = obj.getUserByID();
                    if (dtUserInfo != null && dtUserInfo.Rows.Count > 0)
                    {
                        ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                        ViewState["userName"] = dtUser.Rows[0]["userName"].ToString();
                        ViewState["userPassword"] = dtUserInfo.Rows[0]["userPassword"].ToString();
                        manageExactTargetSubscriberList(dtUser.Rows[0]["userName"].ToString());
                        BindCities(ViewState["userId"].ToString());
                    }

                }
                else
                {
                    //Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {
          
        }
    }


    private void manageExactTargetSubscriberList(string strEmail)
    {
        try
        {
            SoapClient client = new SoapClient();
            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

            SimpleFilterPart sfp = new SimpleFilterPart();
            sfp.Property = "SubscriberKey";
            sfp.SimpleOperator = SimpleOperators.equals;
            sfp.Value = new string[] { strEmail };

            // Create the RetrieveRequest ListSubscriber objects
            RetrieveRequest rr = new RetrieveRequest();
            rr.ObjectType = "ListSubscriber";
            rr.Properties = new string[] { "ListID", "SubscriberKey", "Status" };
            rr.Filter = sfp;

            // Execute the Retrieve call
            APIObject[] results;
            string requestID;
            //Change integrationFramework to the name of your own development environment
            string status = client.Retrieve(rr, out requestID, out results);
            if (status.Trim().ToLower() == "ok")
            {
                BLLNewsLetterSubscriber objSub = new BLLNewsLetterSubscriber();
                objSub.Email = strEmail;
                objSub.unsubscribeAllCityByEmail();
            }
            // Iterate over the results
            // Console.WriteLine("List Subscriber Details:\tList ID\tSubscriberKey\tStatus");
            ArrayList checklist = new ArrayList();
            for (int i = 0; i < results.Length; i++)
            {
                ListSubscriber ls = (ListSubscriber)results[i];
                checklist.Add(ls.ListID);
                if (ls.ListID == 215)
                {
                    subscriberCheck(strEmail, ls, 337);
                }
                else if (ls.ListID == 613)
                {
                    subscriberCheck(strEmail, ls, 338);
                }
                else
                {
                    subscriberCheck(strEmail, ls, ls.ListID);
                }
                // Console.WriteLine("List Subscriber Details:\t{0}\t{1}\t{2}", ls.ListID, ls.SubscriberKey, ls.Status);
            }
            ViewState["checklist"] = checklist;
        }
        catch (Exception ex)
        {
 
        }
    }

    private static void subscriberCheck(string strEmail, ListSubscriber ls, int cityID)
    {
        BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
        obj.Email = strEmail;
        obj.CityId = cityID;
        DataTable dtEmail = obj.getNewsLetterSubscriberByEmailCityId2();
        if (dtEmail != null && dtEmail.Rows.Count == 0)
        {
            obj.Status = true;
            obj.friendsReferralId = "";
            obj.createNewsLetterSubscriber();
        }
        else if (ls.Status == SubscriberStatus.Active)
        {
            obj.Status = true;
            obj.SId = Convert.ToInt64(dtEmail.Rows[0]["SId"].ToString().Trim());
            obj.changeSubscriberStatus();
        }
        else
        {
            obj.Status = false;
            obj.SId = Convert.ToInt64(dtEmail.Rows[0]["SId"].ToString().Trim());
            obj.changeSubscriberStatus();
        }
    }

    DataTable dtSubscribedCityList, DataTableAllCities = new DataTable();

    private void BindCities(string userid)
    {
        BLLNewsLetterSubscriber objSub = new BLLNewsLetterSubscriber();
        objSub.userId = Convert.ToInt32(userid);
        dtSubscribedCityList = objSub.getAllSubscribesCityByUserID();
        string Javascript = "";
        string Checked = "";
        string script2 = "";
        string script = "<div id='manage_subscription_section'>";
        script += "<div class='section_loader'>";
        script += "<div class='loader_saving'>";
        script += "<img src='images/ajax.gif' width='16' height='16'>Saving...</div>";
        script += "<div class='loader_saved'>";
        script += "<img src='account_files2/icon-checkmark.png' width='16' height='18'>Your subscription";
        script += " settings have been saved.</div>";
        script += "</div>";
        script += " <div style='margin-top:30px;padding-bottom:5px;' id='available_subscriptions'>";
        script += "<div class='MemberArea_PageHeading'>";
        script += " Available Subscriptions</div>";

        script += "<div style='clear:both; overflow-x:hidden;' class='sub_column'>";
        DataTable dtCountry = Misc.getAllCountries();
        DataTable dtCities = null;
        

        if (dtCountry != null && dtCountry.Rows.Count > 0)
        {
            for (int a = 0; a < dtCountry.Rows.Count; a++)
            {
                //Countries

                if (a == 0)
                {
                    script += "<div style='margin-bottom:15px; height:20px !important; line-height:20px;font-size:18px;font-weight:bold !important;margin-left:-10px !important;margin-right:-10px !important;width:100%;' class='CityNameButton'>";
                    script += dtCountry.Rows[a]["countryName"].ToString().Trim();
                    script += "</div>";
                }
                else
                {
                    script += "<div style='margin-bottom:15px;font-size:18px;font-weight:bold !important;margin-left:-10px !important;margin-right:-10px !important;width:100%; margin-top:15px;' class='CityNameButton'>";
                    script += dtCountry.Rows[a]["countryName"].ToString().Trim();
                    script += "</div>";
                }

                DataTable dtProvince = Misc.getAllProvincesByCountryID(Convert.ToInt32(dtCountry.Rows[a]["countryid"].ToString().Trim()));

                if (dtProvince != null && dtProvince.Rows.Count > 0)
                {

                    for (int i = 0; i < dtProvince.Rows.Count; i++)
                    {

                        //Cities
                        dtCities = Misc.getCitiesByProvinceID(Convert.ToInt32(dtProvince.Rows[i]["provinceId"].ToString().Trim()));

                        if (dtCities != null && dtCities.Rows.Count > 0)
                        {


                            //ProvinceName
                            script += " <div style='font-size:15px;padding-top:20px;padding-bottom:5px;' >";
                            script += dtProvince.Rows[i]["provinceName"].ToString().Trim();
                            script += "</div>";

                            for (int j = 0; j < dtCities.Rows.Count; j++)
                            {
                                //City Names for Subscribe
                                //script += "<a style='text-decoration:none;' class='ui-state' href=javascript:enableSub(" + dtCities.Rows[j]["cityid"].ToString().Trim() + ")>" + dtCities.Rows[j]["cityName"].ToString().Trim() + "</a>";




                                script += "<div style='width:100%' style='margin-bottom:1px;'>";
                                script += "<div style='width:100%;' class='button-group'>";
                                script += "<div id='Left" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' style='cursor:default !important;width:165px !important; width:285px !important;' class='CityNameButton primary'>" + dtCities.Rows[j]["cityName"].ToString().Trim() + "</div>";
                                script += "<a href=javascript:enableSub(" + dtCities.Rows[j]["cityid"].ToString().Trim() + "); style=' border-color: #3072B3 #3072B3 #2A65A0;border-style: solid;border-width: 1px; font: 11px sans-serif; height:15px; ' class='button green icon approve'>Subscribe</a>";
                                script += "</div>";
                                script += "</div>";



                                //dtSubscribedCityList

                                if (dtSubscribedCityList != null && dtSubscribedCityList.Rows.Count > 0)
                                {

                                    for (int b = 0; b < dtSubscribedCityList.Rows.Count; b++)
                                    {

                                        if (Convert.ToInt32(dtCities.Rows[j]["cityid"].ToString().Trim()) == Convert.ToInt32(dtSubscribedCityList.Rows[b]["cityid"].ToString().Trim()))
                                        {
                                            Checked = "";

                                            Javascript += "<script type='text/javascript'>";
                                            Javascript += "$(document).ready(function() {";
                                            Javascript += "$('#sub_check_container_" + dtCities.Rows[j]["cityid"].ToString().Trim() + "').show();";
                                            Javascript += "$('#TG" + dtCities.Rows[j]["cityid"].ToString().Trim() + "').show();";
                                            Javascript += "});";
                                            Javascript += "</script>";
                                        }
                                    }
                                }
                                script2 += "<div class='sub_container' id='sub_check_container_" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' style='display: none'>";
                                script2 += "<div style='width:100%' style='margin-bottom:1px;'>";
                                script2 += "<div style='width:100%; display:none; height:25px;' id=TG" + dtCities.Rows[j]["cityid"].ToString().Trim() + " class='button-group'>";
                                script2 += "<div id='Right" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' style='cursor:default !important;width:290px !important;' class='CityNameButton primary'>" + dtCities.Rows[j]["cityName"].ToString().Trim() + "</div>";
                                script2 += "<a href=javascript:Unsubscribe(" + dtCities.Rows[j]["cityid"].ToString().Trim() + "); style=' border-color: #3072B3 #3072B3 #2A65A0;border-style: solid;border-width: 1px;  font: 11px sans-serif  !important; height:15px !important; ' class='button danger icon remove'>Unsubscribe</a>";
                                script2 += "</div>";
                                script2 += "</div>";
                                script2 += "</div>";
                            }
                        }

                    }

                }

            }
  
                script += "</div>";
                script += " </div>";
                script += "";
                script += " <div style='margin-top:30px;' id='your_subscriptions'>";
                script += "<div class='MemberArea_PageHeading'>";
                script += "My Subscriptions";
                script += "</div>";
                //2nd div
               script += "<div id='SubscribedDiv' style='clear:both; overflow-x:hidden;padding-top:5px !important;' class='sub_column sub_col_secondary'>";
               script += "<div class='actions button-container'>";
               script += script2;
               script += "</div>";
               script += "</div>";
               script += " </div>";
               script += " <div style='clear: both;'>";
               script += " </div>";
                script += "</div>";
                script += "";
                script += "";
                script += Javascript;
                ltCities.Text = script;
               
        }
    }

    private void unSubscribeUser(string strEmail, int listID)
    {
        SoapClient client = new SoapClient();
        client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
        client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

        Subscriber sub = new Subscriber();
        sub.SubscriberKey = strEmail;//required //may not be active in all accounts //use EmailAddress instead  
        sub.EmailAddress = strEmail;//required  

        sub.Status = SubscriberStatus.Active;
        sub.StatusSpecified = true;

        //Create an Array of Lists  
        sub.Lists = new SubscriberList[1];//If a list is not specified the Subscriber will be added to the "All Subscribers" List  

        sub.Lists[0] = new SubscriberList();
        sub.Lists[0].ID = listID;//Available in the UI via List Properties  
        sub.Lists[0].IDSpecified = true;
        sub.Lists[0].Status = SubscriberStatus.Unsubscribed;
        sub.Lists[0].StatusSpecified = true;
        sub.Lists[0].Action = "update";
        sub.Attributes = new ExactTargetAPI.Attribute[1];

        sub.Attributes[0] = new ExactTargetAPI.Attribute();
        sub.Attributes[0].Name = "Status";
        sub.Attributes[0].Value = "Unsubscribed";
        try
        {
            string uRequestID = String.Empty;
            string uStatus = String.Empty;
            UpdateResult[] uResults = client.Update(new UpdateOptions(), new APIObject[] { sub }, out uRequestID, out uStatus);
        }
        catch (Exception ex)
        {
        }
    }

    private void callJS()
    {
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "lololo", "RenderCityList();", true);
    }
   
}
