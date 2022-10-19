using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExactTargetAPI;
using System.Configuration;
using System.Collections;

public partial class member_preference : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {
                try
                {

                    fillMonthList(ddMonth);
                    fillDayList(ddDate);
                    fillYearList(ddYEar);



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

                       
                        txtFirstName.Text = dtUserInfo.Rows[0]["Firstname"].ToString().Trim();
                        if (dtUserInfo.Rows[0]["lastName"] != null && dtUserInfo.Rows[0]["Firstname"].ToString().Trim() != "")
                        {
                            txtLastName.Text = dtUserInfo.Rows[0]["LastName"].ToString().Trim();
                        }
                        if (dtUserInfo.Rows[0]["ZipCode"] != null && dtUserInfo.Rows[0]["ZipCode"].ToString().Trim() != "")
                        {
                            txtZipCode.Text = dtUserInfo.Rows[0]["ZipCode"].ToString().Trim();
                        }

                        if (dtUserInfo.Rows[0]["DealsPreferFor"] != null && dtUserInfo.Rows[0]["DealsPreferFor"].ToString().Trim() != "")
                        {
                            ddlDealsFor.SelectedValue = dtUserInfo.Rows[0]["DealsPreferFor"].ToString().Trim();
                        }

                        if (dtUserInfo.Rows[0]["DateOfBirth"] != null && dtUserInfo.Rows[0]["DateOfBirth"].ToString().Trim() != "")
                        {
                            DateTime DateOfBirth = Convert.ToDateTime(dtUserInfo.Rows[0]["DateOfBirth"].ToString().Trim());

                            ddYEar.SelectedValue = DateOfBirth.Year.ToString();
                            ddMonth.SelectedValue = DateOfBirth.ToString("MM");
                            ddDate.SelectedValue = DateOfBirth.Day.ToString();
                        }


                        DealTags(Convert.ToInt32(dtUser.Rows[0]["userId"].ToString()));
                       
                    }
                }
                catch (Exception ex)
                {

                    lblerror.Text = ex.Message.ToString();
                }

            }
        }
    }
 

    public void fillMonthList(DropDownList ddlList)
    {
        ddlList.Items.Add(new ListItem("Month", "Month"));
        ddlList.SelectedIndex = 0;

        DateTime month = Convert.ToDateTime("1/1/2000");
        for (int intLoop = 0; intLoop < 12; intLoop++)
        {
            DateTime NextMont = month.AddMonths(intLoop);
            //ddlList.Items.Add(new ListItem(NextMont.ToString("MMMM"), NextMont.Month.ToString()));
            ddlList.Items.Add(new ListItem(NextMont.ToString("MM"), NextMont.ToString("MM")));
        }
    }
    public void fillDayList(DropDownList ddlList)
    {
        ddlList.Items.Add(new ListItem("Day", "Day"));
        ddlList.SelectedIndex = 0;

        int totalDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        for (int intLoop = 1; intLoop <= totalDays; intLoop++)
        {
            ddlList.Items.Add(new ListItem(intLoop.ToString(), intLoop.ToString()));
        }
    }
    public void fillYearList(DropDownList ddlList)
    {
        ddlList.Items.Add(new ListItem("Year", "Year"));
        ddlList.SelectedIndex = 0;

        int intYearName = 1900;
        for (int intLoop = intYearName; intLoop <= Convert.ToInt32(DateTime.Now.Year); intLoop++)
        {
            ddlList.Items.Add(new ListItem(intLoop.ToString(), intLoop.ToString()));
        }
    }

    private void DealTags(int UserID)
    {
        try
        {
            BLLDealCatagories ObjDealCatagories = new BLLDealCatagories();
            DataTable dt = ObjDealCatagories.GetAllDealCatagories();
            DataTable dtSubDeals = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                //categoryID
                string strHTML = "";
                string strHTMLMyDealTags = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    strHTML += "<div id='MainCatagory' " + dt.Rows[i]["DealCategoryID"].ToString().Trim() + " style='float:left; clear: both;color: black;padding-left:15px;'>";
                    strHTML += "<span style='font-size:15px;'>";
                    strHTML += dt.Rows[i]["DealCatagoryName"].ToString().Trim();
                    strHTML += "</span>";
                    strHTML += "<div style='margin-bottom: 15px; padding-top:15px;'>";

                    ObjDealCatagories.DealCategoryid = Convert.ToInt64(dt.Rows[i]["DealCategoryID"].ToString().Trim());
                    dtSubDeals = ObjDealCatagories.GetAllDealCategoriesbyDealCategoryID();

                    if (dtSubDeals != null && dtSubDeals.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtSubDeals.Rows.Count; j++)
                        {
                            strHTML += "<div id='MainDivDealTag" + dtSubDeals.Rows[j]["SubCategoryID"].ToString().Trim() + "' style='margin-bottom:5px;clear:both;'>";
                            strHTML += "<div class='button-group'>";
                            strHTML += "<div id='Tag" + dtSubDeals.Rows[j]["SubCategoryID"].ToString().Trim() + "' style=' cursor: default !important;font-family: sans-serif;font-size: 11px;height: 14px;' class='DealTag pill primary icon like'>";
                            strHTML += dtSubDeals.Rows[j]["SubCatagoryName"].ToString().Trim();
                            strHTML += "</div>";
                            strHTML += "<a onClick='javascript:AddToLoveList(" + dtSubDeals.Rows[j]["SubCategoryID"].ToString().Trim() + ")' style='cursor: pointer; border-color: #3072B3 #3072B3 #2A65A0; height:14px; font-size:11px; font-family: sans-serif; border-style: solid; border-width: 1px;'  class='button green icon add pill '>Add</a>";
                            strHTML += "</div>";
                            strHTML += "</div>";



                            strHTML += "<div id='MainDivDoneTag" + dtSubDeals.Rows[j]["SubCategoryID"].ToString().Trim() + "' style='margin-bottom: 5px; clear:both;display:none;'>";
                            strHTML += "<div class='button-group'>";
                            strHTML += "<div id='DoneTag" + dtSubDeals.Rows[j]["SubCategoryID"].ToString().Trim() + "' style='cursor: cursor: default !important;font-family: sans-serif;font-size: 11px;height: 14px;' class='DealTagDone pill primary icon like'>";
                            strHTML += dtSubDeals.Rows[j]["SubCatagoryName"].ToString().Trim();
                            strHTML += "</div>";
                            strHTML += "<a onClick='javascript:RemoveFromLoveList(" + dtSubDeals.Rows[j]["SubCategoryID"].ToString().Trim() + ")' style='cursor: pointer; border-color: #3072B3 #3072B3 #2A65A0; height:14px; font-size:11px; font-family: sans-serif; border-style: solid; border-width: 1px;'  class='button danger icon remove pill '>Remove</a>";
                            strHTML += "</div>";
                            strHTML += "</div>";

                        }

                    }
                    strHTML += "</div>";
                    strHTML += "</div>";

                }
                string Javascript = "";
                ObjDealCatagories.Userid = UserID;
                dt = ObjDealCatagories.GetUserFavoriteDealCategoriesByUserID();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int b = 0; b < dt.Rows.Count; b++)
                    {
                        Javascript += "<script type='text/javascript'>";
                        Javascript += "$(document).ready(function() {";
                        Javascript += "$('#MainDivDoneTag" + dt.Rows[b]["DealSubCategoryID"].ToString().Trim() + "').show();";
                        Javascript += "$('#MainDivDealTag" + dt.Rows[b]["DealSubCategoryID"].ToString().Trim() + "').hide();";
                        Javascript += "});";
                        Javascript += "</script>";
                    }
                }

               

                ltMyDealtags.Text = "<div style='clear:both; padding-left:15px;'>" + strHTMLMyDealTags + "</div>";
                ltDealTags.Text = strHTML;
                ltJavascript.Text = Javascript;
               

            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message.ToString();
        }
    }

    private void BindCities(string userid)
    {
        DataTable dtSubscribedCityList = null;
        BLLNewsLetterSubscriber objSub = new BLLNewsLetterSubscriber();
        objSub.userId = Convert.ToInt32(userid);
        dtSubscribedCityList = objSub.getAllSubscribesCityByUserID();
        string Javascript = "";
        string script2 = "";
        string script = "<div id='manage_subscription_section'>";
        
        
        DataTable dtCountry = Misc.getAllCountries();
        DataTable dtCities = null;


        if (dtCountry != null && dtCountry.Rows.Count > 0)
        {
            for (int a = 0; a < dtCountry.Rows.Count; a++)
            {
                //Countries

               

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
                            

                            for (int j = 0; j < dtCities.Rows.Count; j++)
                            {
                                

                                //dtSubscribedCityList

                                if (dtSubscribedCityList != null && dtSubscribedCityList.Rows.Count > 0)
                                {

                                    for (int b = 0; b < dtSubscribedCityList.Rows.Count; b++)
                                    {

                                        if (Convert.ToInt32(dtCities.Rows[j]["cityid"].ToString().Trim()) == Convert.ToInt32(dtSubscribedCityList.Rows[b]["cityid"].ToString().Trim()))
                                        {
                                            
                                            Javascript += "<script type='text/javascript'>";
                                            Javascript += "$(document).ready(function() {";
                                            //Javascript += "$('#sub_check_container_" + dtCities.Rows[j]["cityid"].ToString().Trim()  + "').show();";
                                            Javascript += "$('#sub_check_container_" + dtCities.Rows[j]["cityid"].ToString().Trim() + "').show().find('input[name=\"geoId\"]').attr('checked', 'checked').change();";
                                            Javascript += "});";
                                            Javascript += "</script>";

                                        }
                                    }

                                }


                                script2 += "<div class='sub_container' id='sub_check_container_" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' style='display: none'>";
                                script2 += "<div class='sub_loader' id='" + dtCities.Rows[j]["cityid"].ToString().Trim() + "_prog'>";
                                script2 += "<img src='images/ajax.gif' class='icon' style='color:#4297D7 !important;' alt='' width='16' height='16'>";
                                script2 += "Saving...";
                                script2 += "</div>";
                                script2 += "<div class='sub_loader' style='color:#4297D7 !important;' id='" + dtCities.Rows[j]["cityid"].ToString().Trim() + "_done'>";
                                script2 += "<img src='images/check_icon.gif' alt='' />'";
                                script2 += "Saved";
                                script2 += "</div>";
                                script2 += "<div class='sub_check' id='" + dtCities.Rows[j]["cityid"].ToString().Trim() + "_check'>";
                               
                                //script2 += "<input onclick=javascript:Unsubscribe(" + dtCities.Rows[j]["cityid"].ToString().Trim() + "); style='float:left; display:none;' id='" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' name='geoId' value='" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' type='checkbox'>";
                                //script2 += " <input  id='hid" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' value='" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' type='hidden'>";
                                //script2 += " <label style='float:left; font-size: 18px; font-weight:bold; text-shadow:0 1px 0 black; color: #3289B2;' class='city' for='" + dtCities.Rows[j]["cityid"].ToString().Trim() + "'>";
                                
                                //script2 += dtCities.Rows[j]["cityName"].ToString().Trim();
                                //script2 += "</label>";
                                //script2 += " <label onclick=javascript:Unsubscribe(" + dtCities.Rows[j]["cityid"].ToString().Trim() + "); style='float:left; font-size: 10px;  color: #3289B2;' class='city' for='Remove'>";
                                //script2 += "Remove";
                                //script2 += "</label>";

                                
                                script2 += "<div style='margin-bottom:1px;'>";
                                script2 += "<div class='button-group'>";
                                script2 += "<div id='Left" + dtCities.Rows[j]["cityid"].ToString().Trim() + "' style='cursor:default !important; height:14px !important;' class='CityNameButton primary'>" + dtCities.Rows[j]["cityName"].ToString().Trim() + "</div>";
                                script2 += "<a href=javascript:Unsubscribe(" + dtCities.Rows[j]["cityid"].ToString().Trim() + "); style=' border-color: #3072B3 #3072B3 #2A65A0;border-style: solid;border-width: 1px; height:14px !important; font:11px sans-serif !important; ' class='button danger icon remove'>Unsubscribe</a>";
                                script2 += "</div>";
                                script2 += "</div>";
                                script2 += "</div>";
                                script2 += "</div>";


                            }
                        }

                    }

                }

            }

            
            script += "";

            script += " <div id='your_subscriptions' class='sub_section'>";
            script += "<div style='color:#0a3b5f;font-size:20px;float:left;clear:both;font-weight:bold;' class='sub_col_title'>";
            script += "My Subscriptions:";
            script += "</div>";

            //2nd div
            script += "<div style='clear:both;' class='sub_column3 sub_col_secondary'>";

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



            TabScript.Text = "<script type='text/javascript'>";
            TabScript.Text += "$(document).ready(function() {";
            TabScript.Text += "$('#coda-slider-1').codaSlider({";
            TabScript.Text += "dynamicArrows: false";
            TabScript.Text += " });";
            TabScript.Text += "$('#Itemtab1').hide();";
            TabScript.Text += "$('#Itemtab2').hide();";
            TabScript.Text += "$('#Itemtab3').hide();";
            TabScript.Text += "});";
            TabScript.Text += "</script>";

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


    protected void ddMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDynamicDays();
    }
    private void BindDynamicDays()
    {
        ddDate.Items.Clear();
        ddDate.Items.Add(new ListItem("Day", "Day"));
        ddDate.SelectedIndex = 0;
        int year;
        int month;
        if (ddYEar.SelectedValue.ToString().Trim() == "Year")
        {
            year = DateTime.Now.Year;
        }
        else
        {
            year = Convert.ToInt32(ddYEar.SelectedValue.ToString().Trim());
        }


        if (ddMonth.SelectedValue.ToString().Trim() == "Month")
        {
            month = DateTime.Now.Month;
        }
        else
        {
            month = Convert.ToInt32(ddMonth.SelectedValue.ToString().Trim());
        }
        int totalDays = DateTime.DaysInMonth(year, month);
        for (int intLoop = 1; intLoop <= totalDays; intLoop++)
        {
            ddDate.Items.Add(new ListItem(intLoop.ToString(), intLoop.ToString()));
        }
    }
    protected void ddYEar_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDynamicDays();
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        int Result = 0;
        
        
        BLLUser objUser = new BLLUser();


        DateTime BirthdayDate = new DateTime(Convert.ToInt32(ddYEar.SelectedItem.ToString().Trim()), Convert.ToInt32(ddMonth.SelectedItem.ToString().Trim()), Convert.ToInt32(ddDate.SelectedItem.ToString().Trim()));
                

        objUser.userId = Convert.ToInt32(ViewState["userId"].ToString().Trim());
        objUser.firstName = txtFirstName.Text.ToString().Trim();
        objUser.lastName = txtLastName.Text.ToString().Trim();
        objUser.DealsPreferfor = ddlDealsFor.SelectedValue.ToString().Trim();
        objUser.zipcode = txtZipCode.Text.ToString().Trim();
        objUser.DateOfBirth = BirthdayDate;
        Result = objUser.UserUpdateWhoTab();
        if (Result != 0)
        {

            string jScript2;
            jScript2 = "<script>";
            //jScript += "$(\"#messages\").removeClass(\"successMessage\").addClass(\"errorMessage\");";                           
            //jScript += "$(\"#messages\").html('Enter Email and password').slideDown(\"slow\");";
            jScript2 += "MessegeArea('Profile has been saved successfully.' , 'success');";            
            jScript2 += "</script>";
            ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript2, false);

            //lblErrorMessageProfile.Text = "Profile has been saved successfully.";
            //lblErrorMessageProfile.Visible = true;
            //imgGridMessageProfile.Visible = true;
            //imgGridMessageProfile.ImageUrl = "images/Checked.png";
            //lblErrorMessageProfile.ForeColor = System.Drawing.Color.Black;
            string jScript;
            jScript = "<script>";
            jScript += "$(document).ready(function() {";
            jScript += " setTimeout(function () {";
            jScript += " $('#Itemtab3').click();";
            jScript += " }, 0);";
            jScript += "";
            jScript += "});";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);

        }
        
    }


}