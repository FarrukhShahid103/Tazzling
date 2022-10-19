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
using System.Text;
using System.Net.Mail;
using System.IO;
using GecLibrary;
using System.Collections.Generic;
using System.Threading;

public partial class member_profile : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLMemberDeliveryInfo objDeliveryInfo = new BLLMemberDeliveryInfo();
    BLLUserCCInfo objCC = new BLLUserCCInfo();
    BLLDealOrders objorders = new BLLDealOrders();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                if (ViewState["CanChangeUserName"] != null)
                {
                    if (Convert.ToBoolean(ViewState["CanChangeUserName"].ToString().Trim()))
                    {
                        txtUserName.Enabled = true;
                    }
                    else
                    {
                        txtUserName.Enabled = false;
                    }
                }
            }

            if (Request.QueryString["TabID"] != null && Request.QueryString["TabID"].ToString().Trim() != "")
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

                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    string Message = "Dear <b>" + dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString() + "</b>,<br>Your facebook account successfully configured with us and Tastygo purchases sharing enabled.";


                    string javascript = "";
                    javascript += "<script type='text/javascript' language='javascript'>";
                    javascript += "setTimeout(function () {";
                    javascript += "$('#Itemtab3').click();";
                    javascript += "$('.bottom-right').jGrowl('" + Message + "'),";
                    javascript += "{";
                    javascript += "sticky: false";
                    javascript += " };";
                    javascript += "}, 1000);";
                    javascript += "</script>";
                    ltFacebook.Text = javascript;
                }
            }
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Profile";
            if (!IsPostBack)
            {
                LoadDropDownList();
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"]!=null)
                {
                    //bindProvinces(2);
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
                        if (dtUserInfo.Rows[0]["modifiedDate"] != null && dtUserInfo.Rows[0]["modifiedDate"].ToString().Trim() != "")
                        {
                            try
                            {
                                if (DateTime.Now.Subtract(Convert.ToDateTime(dtUserInfo.Rows[0]["modifiedDate"])).Days == 0)
                                {
                                    ViewState["CanChangeUserName"] = false;
                                    txtUserName.Enabled = false;
                                }
                                else
                                {
                                    ViewState["CanChangeUserName"] = true;
                                }
                            }
                            catch (Exception ex)
                            { }
                        }
                        else if (dtUserInfo.Rows[0]["creationDate"] != null && dtUserInfo.Rows[0]["creationDate"].ToString().Trim() != "")
                        {
                            try
                            {
                                if (DateTime.Now.Subtract(Convert.ToDateTime(dtUserInfo.Rows[0]["creationDate"])).Days == 0)
                                {
                                    ViewState["CanChangeUserName"] = false;
                                    txtUserName.Enabled = false;
                                }
                                else
                                {
                                    ViewState["CanChangeUserName"] = true;
                                }
                            }
                            catch (Exception ex)
                            { }
                        }
                        GridDataBind(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));

                        txtUserName.Text = dtUserInfo.Rows[0]["userName"].ToString();
                        txtFirstName.Text = dtUserInfo.Rows[0]["firstName"].ToString();
                        txtLastName.Text = dtUserInfo.Rows[0]["lastName"].ToString();
                        if (dtUserInfo.Rows[0]["phoneNo"] != DBNull.Value)
                        {
                            string[] strphone = dtUserInfo.Rows[0]["phoneNo"].ToString().Split('-');
                            if (strphone.Length == 3)
                            {
                                txtPhone1.Text = strphone[0].ToString();
                                txtPhone2.Text = strphone[1].ToString();
                                txtPhone3.Text = strphone[2].ToString();
                            }
                        }


                        if (dtUserInfo.Rows[0]["FB_access_token"] != null && dtUserInfo.Rows[0]["FB_userID"] != null && dtUserInfo.Rows[0]["FB_Share"] != null && dtUserInfo.Rows[0]["FB_access_token"].ToString().Trim() != "" && dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() != "") 
                        {
                            pnlFacebookConnect.Visible = false;
                            pnlFacebookStuff.Visible = true;
                            
                            if (dtUserInfo.Rows[0]["FB_Share"].ToString().Trim() != "")
                            {                               
                                bool check = Convert.ToBoolean(dtUserInfo.Rows[0]["FB_Share"].ToString().Trim());
                                cbFBShare.Checked = check;
                                if (check)
                                {
                                   
                                    ViewState["PnlNoShare"] = "clear: both; display: none;";
                                    ViewState["pnlYesShare"] = "clear: both; display: block;";
                                    
                                }
                                else
                                {                                    
                                    ViewState["PnlNoShare"] = "clear: both; display: block;";
                                    ViewState["pnlYesShare"] = "clear: both; display: none;";                                                                                                           
                                }
                            }
                        }
                        else
                        {
                            pnlFacebookStuff.Visible = false;
                            pnlFacebookConnect.Visible = true;
                            cbFBShare.Checked = true;
                        }




                        if (dtUserInfo.Rows[0]["profilePicture"] != null && dtUserInfo.Rows[0]["profilePicture"].ToString().Trim() != "")
                        {
                            string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + dtUserInfo.Rows[0]["profilePicture"].ToString().Trim();
                            if (File.Exists(strFileName))
                            {
                                ViewState["PicName"] = dtUserInfo.Rows[0]["profilePicture"].ToString().Trim();
                                imgProfilePic.ImageUrl = "~/images/ProfilePictures/" + dtUserInfo.Rows[0]["profilePicture"].ToString().Trim();
                            }                            
                            else
                            {
                                imgProfilePic.ImageUrl = "~/images/NoPhotoAvailable.jpg";
                            }
                        }                      
                        else
                        {
                            imgProfilePic.ImageUrl = "~/images/NoPhotoAvailable.jpg";
                        }
                       
                        //ddlCountry.SelectedValue = //dtUserInfo.Rows[0]["countryId"].ToString();                                         
                        hfProvince.Value = dtUserInfo.Rows[0]["provinceID"].ToString();
                        try
                        {
                            /*if (dtUserInfo.Rows[0]["gender"] != null && dtUserInfo.Rows[0]["gender"].ToString().Trim() != "")
                            {
                                rbMale.Checked = Convert.ToBoolean(dtUserInfo.Rows[0]["gender"].ToString().Trim());
                            }
                            if (dtUserInfo.Rows[0]["age"] != null && dtUserInfo.Rows[0]["age"].ToString().Trim() != "")
                            {
                                dlAge.SelectedValue = dtUserInfo.Rows[0]["age"].ToString().Trim();
                            }*/



                            if (dtUserInfo.Rows[0]["countryId"] != null && dtUserInfo.Rows[0]["countryId"].ToString().Trim() != ""
                                && dtUserInfo.Rows[0]["provinceID"] != null && dtUserInfo.Rows[0]["provinceID"].ToString().Trim() != "")
                            {
                                try
                                {
                                    ddlCountry.SelectedValue = dtUserInfo.Rows[0]["countryId"].ToString().Trim();
                                    bindProvinces(Convert.ToInt32(dtUserInfo.Rows[0]["countryId"].ToString().Trim()));
                                    ddlProvinceLive.SelectedValue = dtUserInfo.Rows[0]["provinceID"].ToString().Trim();
                                    FillCitpDroDownList(dtUserInfo.Rows[0]["provinceID"].ToString());
                                }
                                catch (Exception ex)
                                { }
                            }                           
                            if (dtUserInfo.Rows[0]["cityId"] != null && dtUserInfo.Rows[0]["cityId"].ToString().Trim() != "" && dtUserInfo.Rows[0]["cityId"].ToString().Trim() != "0")
                            {
                                try
                                {
                                    if (ddlCity.Items.Count > 0)
                                    {
                                        ddlCity.SelectedValue = dtUserInfo.Rows[0]["cityId"].ToString();
                                    }
                                }
                                catch (Exception ex)
                                { }
                             
                            }                         
                            //if (dtUserInfo.Rows[0]["zipcode"] != null && dtUserInfo.Rows[0]["zipcode"].ToString().Trim() != "")
                            //{
                            //    txtZipCode.Text = dtUserInfo.Rows[0]["zipcode"].ToString().Trim();
                            //}                           
                            if (dtUserInfo.Rows[0]["howYouKnowUs"] != null && dtUserInfo.Rows[0]["howYouKnowUs"].ToString().Trim() != "")
                            {
                                dlhowyouHeared.SelectedValue = dtUserInfo.Rows[0]["howYouKnowUs"].ToString().Trim();
                            }
                        }
                        catch (Exception ex)
                        { }

                    }

                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {



            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur please email us at support@tazzling.com or call 1855-295-1771.' , 'Error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);

            




        }

       
    }

    protected void bindProvinces(int countryID)
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(countryID);
            ddlProvinceLive.DataSource = dt.DefaultView;
            ddlProvinceLive.DataTextField = "provinceName";
            ddlProvinceLive.DataValueField = "provinceId";
            ddlProvinceLive.DataBind();
            FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlProvinceLive_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlProvinceLive.SelectedValue);
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

                ddlCity.DataSource = dtCities;

                ddlCity.DataTextField = "cityName";

                ddlCity.DataValueField = "cityId";

                ddlCity.DataBind();

                ddlCity.Items.Insert(0, "Select One");
            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.DataSource = null;
                ddlCity.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected void txtUserName_Changed(object sender, EventArgs e)
    {
        try
        {

            if (!Misc.validateEmailAddress(txtUserName.Text.Trim()))
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter a valid email address.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);
                txtUserName.Focus();
                return;
            }
            
            if (txtUserName.Text.Trim() != "" && ViewState["userName"].ToString().Trim() != txtUserName.Text.Trim())
            {
                BLLUser objuser = new BLLUser();
                objuser.email = txtUserName.Text.Trim();
                DataTable dtuser = objuser.getUserDetailByEmail();
                if (dtuser != null && dtuser.Rows.Count > 0)
                {
                    


                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('This email already registered', 'Error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);
                }
                else
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Email Available You can use this email.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Javascript", jScript, false);
                }
            }
        }
        catch (Exception ex)
        { }
    }



    protected void BtnChangeUserPassword_Click(object sender, EventArgs e)
    {
        try
        {

            BLLUser objuser = new BLLUser();
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
            if (dtUser == null || dtUser.Rows.Count <= 0)
            {
                Response.Redirect("Login.aspx");
                Response.End();
            }


            if (Misc.validateEmailAddress(txtUserName.Text.Trim()))
            {
                if (txtUserName.Text.Trim() != "" && ViewState["userName"].ToString().Trim() != txtUserName.Text.Trim())
                {
                    if (ViewState["CanChangeUserName"] != null && !Convert.ToBoolean(ViewState["CanChangeUserName"].ToString().Trim()))
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('You could not change email today. Please try after 24hours.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);
                        return;
                    }
                    objuser.email = txtUserName.Text.Trim();
                    DataTable dtuser = objuser.getUserDetailByEmail();
                    if (dtuser != null && dtuser.Rows.Count > 0)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('This email already registered.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);                        
                        return;
                    }
                    if (OldPassword.Text.Trim() != "" && NewPassword.Text.Trim() != "" && ConfirmNewPassword.Text.Trim() != "")
                    {

                        objuser.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
                        objuser.userPassword = HtmlRemoval.StripTagsRegexCompiled(OldPassword.Text.Trim());
                        objuser.userName = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());
                        if (objuser.validateUserNamePassword().Rows.Count > 0)
                        {
                            objuser.userPassword = NewPassword.Text.Trim();
                            objuser.email = HtmlRemoval.StripTagsRegexCompiled(txtUserName.Text.Trim());
                            objuser.UpdateUserAccountInfo();
                            ViewState["userPassword"] = NewPassword.Text.Trim();
                            ViewState["userName"] = txtUserName.Text.Trim();
                            ViewState["CanChangeUserName"] = false;
                            txtUserName.Enabled = false;
                            string jScript;
                            jScript = "<script>";
                            jScript += "MessegeArea('Your account information has been successfully updated.','success');";
                            jScript += "</script>";
                            ScriptManager.RegisterClientScriptBlock(BtnChangeUserPassword, typeof(Button), "Javascript", jScript, false);
                        }
                        else
                        {
                            string jScript;
                            jScript = "<script>";                            
                            jScript += "MessegeArea('You enter wrong old password','Error');";
                            jScript += "</script>";
                            ScriptManager.RegisterClientScriptBlock(BtnChangeUserPassword, typeof(Button), "Javascript", jScript, false);
                            return;
                        }
                    }
                    else
                    {
                        objuser.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
                        objuser.userPassword = HtmlRemoval.StripTagsRegexCompiled(ViewState["userPassword"].ToString().Trim());
                        objuser.email = HtmlRemoval.StripTagsRegexCompiled(txtUserName.Text.Trim());
                        objuser.UpdateUserAccountInfo();
                        ViewState["userName"] = txtUserName.Text.Trim();
                        ViewState["CanChangeUserName"] = false;
                        txtUserName.Enabled = false;
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Your account information has been successfully updated.','success');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(BtnChangeUserPassword, typeof(Button), "Javascript", jScript, false);
                        
                    }

                }
                else if (OldPassword.Text.Trim() != "" && NewPassword.Text.Trim() != "" && ConfirmNewPassword.Text.Trim() != "")
                {

                    objuser.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
                    objuser.userPassword = HtmlRemoval.StripTagsRegexCompiled(OldPassword.Text.Trim());
                    objuser.userName = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());
                    if (objuser.validateUserNamePassword().Rows.Count > 0)
                    {
                        objuser.userPassword = NewPassword.Text.Trim();
                        objuser.email = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());
                        objuser.UpdateUserAccountInfo();
                        ViewState["userPassword"] = NewPassword.Text.Trim();
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Your account information has been successfully updated.','success');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(BtnChangeUserPassword, typeof(Button), "Javascript", jScript, false);
                    }
                    else
                    {
                        string jScript;
                        jScript = "<script>";                        
                        jScript += "MessegeArea('You enter wrong old password','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(BtnChangeUserPassword, typeof(Button), "Javascript", jScript, false);
                    }
                }
                else
                {                  
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Your account information has been successfully updated.','success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(BtnChangeUserPassword, typeof(Button), "Javascript", jScript, false);
                }
            }
            else
            {             
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter a valid email address.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);                
                txtUserName.Focus();
                return;
            }            
           
        }
        catch (Exception ex)
        { }
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (ViewState["IsCraditCard"] == null || ViewState["IsCraditCard"].ToString() == "false")
        {
            try
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

                if (dtUser == null || dtUser.Rows.Count <= 0)
                {
                    Response.Redirect("Login.aspx");
                    Response.End();
                }

                if (txtFirstName.Text.Trim() != "" && txtLastName.Text.Trim() != "")
                {
                    obj.firstName = HtmlRemoval.StripTagsRegexCompiled(txtFirstName.Text.Trim());
                    obj.lastName = HtmlRemoval.StripTagsRegexCompiled(txtLastName.Text.Trim());
                    obj.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
                    obj.userName = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());
                    obj.referralId = HtmlRemoval.StripTagsRegexCompiled(txtUserName.Text.Trim());
                    //obj.gender = rbMale.Checked;
                    // obj.zipcode = txtZipCode.Text.Trim();
                    // obj.age = dlAge.SelectedValue.ToString().Trim();
                    obj.howYouKnowUs = dlhowyouHeared.SelectedValue.ToString().Trim();

                    obj.cityId = Convert.ToInt32(ddlCity.SelectedValue.ToString().Trim());
                    obj.userPassword = ViewState["userPassword"].ToString().Trim();
                    obj.phoneNo = HtmlRemoval.StripTagsRegexCompiled(txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim());
                    obj.userName = HtmlRemoval.StripTagsRegexCompiled(txtUserName.Text.Trim());
                    obj.email = HtmlRemoval.StripTagsRegexCompiled(txtUserName.Text.Trim());
                    obj.modifiedBy = obj.userId;

                    obj.countryId = Convert.ToInt32(ddlCountry.SelectedValue.Trim());

                    obj.provinceId = Convert.ToInt32(ddlProvinceLive.SelectedValue.ToString().Trim());


                    if (fuUserProfilePic.HasFile)
                    {
                        string[] strExtension = fuUserProfilePic.FileName.Split('.');
                        string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                        string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "images\\temp\\" + fuUserProfilePic.FileName;
                        fuUserProfilePic.SaveAs(strSrcPath);
                        string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\";
                        string SrcFileName = fuUserProfilePic.FileName;
                        Misc.CreateThumbnail(strSrcPath, strthumbSave, "us", strUniqueID);
                        obj.profilePicture = strUniqueID;
                        imgProfilePic.ImageUrl = "~/images/ProfilePictures/" + strUniqueID;
                        ViewState["PicName"] = strUniqueID;
                    }
                    else if (ViewState["PicName"] != null)
                    {
                        obj.profilePicture = ViewState["PicName"].ToString();
                    }

                    if (obj.updateUserByID() != 0)
                    {
                        dtUser = obj.validateUserNamePassword();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            if (Session["member"] != null)
                            {
                                Session["member"] = dtUser;
                            }
                            else if (Session["restaurant"] != null)
                            {
                                Session["restaurant"] = dtUser;
                            }
                            else if (Session["sale"] != null)
                            {
                                Session["sale"] = dtUser;
                            }
                            else if (Session["user"] != null)
                            {
                                Session["user"] = dtUser;
                            }
                        }
                        string jScript;
                        jScript = "<script>";
                        jScript += "setTimeout(function () {Tab0();";
                        jScript += "Tab1();";
                        jScript += "$('#Itemtab2').click();";
                        jScript += "MessegeArea('Changes will take into effect on your next login','success');";
                        jScript += "}, 1000);</script>";
                        Page.RegisterStartupScript("regJSsss", jScript);
                        //ScriptManager.RegisterStartupScript(btnChange, typeof(Button), "randomlists", jScript.ToString(), true);
                        //ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);

                    }
                }
                else
                {
                    string jScript;
                    jScript = "<script>function pageLoad(){";
                    jScript += "setTimeout(function () {Tab0();";
                    jScript += "Tab1();alert('hi');";
                    jScript += "$('#Itemtab2').click();";
                    jScript += "MessegeArea('Please enter your first and last name.','Error');";
                    jScript += "}, 1000);}</script>";
                    ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);
                }
            }
            catch (Exception ex)
            {


                string jScript;
                jScript = "<script>function pageLoad(){";
                jScript += "setTimeout(function () {Tab0();";
                jScript += "Tab1();alert('hi');";
                jScript += "$('#Itemtab2').click();";
                jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.','Error');";
                jScript += "}, 1000);}</script>";
                ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);



            }
        }

        ViewState["IsCraditCard"] = "false";
    }

    protected string GetCardExplain(object objCCNumber)
    {
        if (objCCNumber.ToString().Trim() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString()).Length - 4);
            // objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString().Trim());
        }
        return "";
    }
    private void GridDataBind(long UserID)
    {
        objCC = new BLLUserCCInfo();
        objCC.userId = UserID;
        gvCustomers.DataSource = objCC.getUserCCInfoByUserID();
        gvCustomers.DataBind();
    }


    protected void btnAddCustomer_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
        imgGridMessage.Visible = false;
        ddlMonth.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ViewState["ccInfoID"] = null;
        ClearEditCustomerForm();	//In case using non-modal
        ViewState["IsCraditCard"] = "true";
        RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
    }


    private void ClearEditCustomerForm()
    {
        //Empty out text boxes
        var textBoxes = new List<Control>();
        FindControlsOfType(this.phrEditCustomer, typeof(TextBox), textBoxes);

        foreach (TextBox textBox in textBoxes)
            textBox.Text = "";

        //Clear validators
        var validators = new List<Control>();
        FindControlsOfType(this.phrEditCustomer, typeof(BaseValidator), validators);

        foreach (BaseValidator validator in validators)
            validator.IsValid = true;
    }


    static public void FindControlsOfType(Control root, Type controlType, List<Control> list)
    {
        if (root.GetType() == controlType || root.GetType().IsSubclassOf(controlType))
        {
            list.Add(root);
        }

        //Skip input controls
        if (!root.HasControls())
            return;

        foreach (Control control in root.Controls)
        {
            FindControlsOfType(control, controlType, list);
        }
    }


    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem == null)
            return;

      //  LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
       // btnEdit.OnClientClick = "openDialogAndBlock('Edit Credit Card', '" + btnEdit.ClientID + "')";
    }


    protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objCC = new BLLUserCCInfo();
        objCC.ccInfoID = Convert.ToInt64(e.CommandArgument);

        switch (e.CommandName)
        {
            //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
            case "EditCustomer":
                FillEditCustomerForm(Convert.ToInt64(e.CommandArgument));

                // this.EditCustomerID = customerID;
                //RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                RegisterStartupScript("jsShowDiv", "ShowAddCraditCard();");

                //Setting timer to test longer loading
                //Thread.Sleep(2000);
                ViewState["IsCraditCard"] = "true";

              

                break;

            case "DeleteCustomer":                               
                    objCC.deleteUserCCInfo();
                    GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
                    
                          


                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Record has been deleted successfully.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Javascript", jScript, false);



                break;

        }
    }

    private void FillEditCustomerForm(long ccinfoID)
    {
        try
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            GECEncryption objEnc = new GECEncryption();
            objCC = new BLLUserCCInfo();
            objCC.ccInfoID = ccinfoID;
            ViewState["ccInfoID"] = ccinfoID;
            DataTable dtCCinfo = objCC.getUserCCInfoByID();
            if (dtCCinfo != null && dtCCinfo.Rows.Count > 0)
            {
                txCardNumner.Text = "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
                txtBillingAddress.Text = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString();
                //txtCCVNumber.Text = objEnc.DecryptData("colintastygochengccv", dtCCinfo.Rows[0]["ccInfoCCVNumber"].ToString());
                txtCity.Text = dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
                txtFirstNameCC.Text = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
                txtPostalCode.Text = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
                if (dtCCinfo.Rows[0]["ccInfoDEmail"] != null && dtCCinfo.Rows[0]["ccInfoDEmail"].ToString() != "")
                {
                    ViewState["userName"] = dtCCinfo.Rows[0]["ccInfoDEmail"].ToString();
                }
                string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
                if (strDate.Length == 2)
                {
                    ddlMonth.SelectedValue = strDate[0].ToString();
                    ddlYear.SelectedValue = strDate[1].ToString();
                }
                try
                {
                    ddlState.SelectedValue = dtCCinfo.Rows[0]["ccInfoBProvince"].ToString();
                }
                catch (Exception ex)
                { }
            }
        }
        catch (Exception ex)
        { }
    }


    private void TriggerClientGridRefresh()
    {
        string script = "__doPostBack(\"" + btnRefreshGrid.ClientID + "\", \"\");";
        RegisterStartupScript("jsGridRefresh", script);
    }


    private void RegisterStartupScript(string key, string script)
    {
        ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((Convert.ToInt32(ddlMonth.SelectedValue) < DateTime.Now.Month) && (Convert.ToInt32(ddlYear.SelectedValue) <= DateTime.Now.Year))
        {
            lblDateError.Visible = true;
            lblDateError.Text = "*";
            return;
        }
        lblDateError.Visible = false;
        if (!Page.IsValid)
            return;

        //Customer customer;
        GECEncryption objEnc = new GECEncryption();
        if (ViewState["ccInfoID"] == null)
        {
            objCC = new BLLUserCCInfo();
            objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBillingAddress.Text.Trim());
            objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtCity.Text.Trim());
            objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtPostalCode.Text.Trim());
            objCC.ccInfoBProvince = ddlState.SelectedValue.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCCVNumber.Text.Trim()));
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txCardNumner.Text.Trim()));
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtFirstNameCC.Text.Trim()));
            objCC.ccInfoDEmail = ViewState["userName"].ToString();
            string[] strUserName = txtFirstNameCC.Text.Split(' ');
            objCC.ccInfoDFirstName = strUserName[0].ToString();
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = strUserName[1].ToString();
            }
            objCC.createUserCCInfo();
        }
        else
        {
            objCC = new BLLUserCCInfo();
            objCC.ccInfoID = Convert.ToInt64(ViewState["ccInfoID"].ToString());
            objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBillingAddress.Text.Trim());
            objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtCity.Text.Trim());
            objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtPostalCode.Text.Trim());
            objCC.ccInfoBProvince = ddlState.SelectedValue.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCCVNumber.Text.Trim()));
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txCardNumner.Text.Trim()));
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtFirstNameCC.Text.Trim()));
            objCC.ccInfoDEmail = ViewState["userName"].ToString();
            string[] strUserName = txtFirstNameCC.Text.Split(' ');
            objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(strUserName[0].ToString());
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = strUserName[1].ToString();
            }
            objCC.updateUserCCInfoByID();
        }
        HideEditCustomer();
        TriggerClientGridRefresh();
        GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
        if (ViewState["ccInfoID"] == null)
        {
            upnlCustomers.Update();
            
            string jScript2;
            jScript2 = "<script>";
            jScript2 += "HideAddCraditCard();";
            jScript2 += "MessegeArea('Record has been added successfully.', 'success');";
            jScript2 += "</script>";
            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Javascript", jScript2, false);


        }
        else
        {
            upnlCustomers.Update();
            


            



        }

        string jScript;
        jScript = "<script>";
        jScript += "HideAddCraditCard();";
        jScript += "MessegeArea('Record has been Updated successfully.' , 'success');";
       
        jScript += "</script>";
        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(Button), "Javascript", jScript, false);

        ViewState["IsCraditCard"] = "true";
    }

     protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindProvinces( Convert.ToInt32(ddlCountry.SelectedValue.ToString()));
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void LoadDropDownList()
    {
        try
        {
            //Clears the Drop Down List
            this.ddlYear.Items.Clear();

            //Year
            for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 9; year++)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();

            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlState.DataSource = dt.DefaultView;
            ddlState.DataTextField = "provinceName";
            ddlState.DataValueField = "provinceName";
            ddlState.DataBind();
        }
        catch (Exception ex)
        {

        }
    }


    private void HideEditCustomer()
    {
        ClearEditCustomerForm();

        RegisterStartupScript("jsCloseDialg", "closeDialog();");
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["ccInfoID"] = null;
        HideEditCustomer();
    }


    protected void btnRefreshGrid_Click(object sender, EventArgs e)
    {
        GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
    }




}
