using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GecLibrary;
using System.Data;
using System.IO;
using System.Text;

public partial class MyAccountSetting : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLUserCCInfo objCC = new BLLUserCCInfo();
    string sHiddenPopupValue = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
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
                    //ViewState["email"] = dtUser.Rows[0]["email"].ToString();
                    ViewState["userPassword"] = dtUserInfo.Rows[0]["userPassword"].ToString();
                    if (dtUserInfo.Rows[0]["modifiedDate"] != null && dtUserInfo.Rows[0]["modifiedDate"].ToString().Trim() != "")
                    {
                        try
                        {
                            if (DateTime.Now.Subtract(Convert.ToDateTime(dtUserInfo.Rows[0]["modifiedDate"])).Days == 0)
                            {
                                ViewState["CanChangeUserName"] = false;
                                txtNewEmail.Enabled = false;
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
                                txtNewEmail.Enabled = false;
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

                    lblEmail.Text = dtUserInfo.Rows[0]["userName"].ToString();
                    lblYourName.Text = dtUserInfo.Rows[0]["firstName"].ToString() + " " + dtUserInfo.Rows[0]["lastName"].ToString();
                    if (dtUserInfo.Rows[0]["phoneNo"] != DBNull.Value)
                    {
                        lblPhoneNumber.Text = dtUserInfo.Rows[0]["phoneNo"].ToString();
                    }

                    if (dtUserInfo.Rows[0]["FB_access_token"] != null && dtUserInfo.Rows[0]["FB_userID"] != null && dtUserInfo.Rows[0]["FB_Share"] != null && dtUserInfo.Rows[0]["FB_access_token"].ToString().Trim() != "" && dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() != "")
                    {
                        //pnlFacebookConnect.Visible = false;
                        //pnlFacebookStuff.Visible = true;

                        if (dtUserInfo.Rows[0]["FB_Share"].ToString().Trim() != "")
                        {
                            bool check = Convert.ToBoolean(dtUserInfo.Rows[0]["FB_Share"].ToString().Trim());
                            //cbFBShare.Checked = check;
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
                        //pnlFacebookStuff.Visible = false;
                        //pnlFacebookConnect.Visible = true;
                        //cbFBShare.Checked = true;
                    }




                    if (dtUserInfo.Rows[0]["profilePicture"] != null && dtUserInfo.Rows[0]["profilePicture"].ToString().Trim() != "")
                    {
                        string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + dtUserInfo.Rows[0]["profilePicture"].ToString().Trim();
                        if (File.Exists(strFileName))
                        {
                            ViewState["PicName"] = dtUserInfo.Rows[0]["profilePicture"].ToString().Trim();
                            //imgProfilePic.ImageUrl = "~/images/ProfilePictures/" + dtUserInfo.Rows[0]["profilePicture"].ToString().Trim();
                        }
                        else
                        {
                            //imgProfilePic.ImageUrl = "~/images/NoPhotoAvailable.jpg";
                        }
                    }
                    else
                    {
                        //imgProfilePic.ImageUrl = "~/images/NoPhotoAvailable.jpg";
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
                                lblCountry.Text = ddlCountry.SelectedItem.Text.Trim();
                                bindProvinces(Convert.ToInt32(dtUserInfo.Rows[0]["countryId"].ToString().Trim()));                                
                                ddlProvinceLive.SelectedValue = dtUserInfo.Rows[0]["provinceID"].ToString().Trim();
                                lblProvinceState.Text = ddlProvinceLive.SelectedItem.Text.Trim();
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
                                    lblHomeCity.Text = ddlCity.SelectedItem.Text.Trim();
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
                            lblHowYouHear.Text = dtUserInfo.Rows[0]["howYouKnowUs"].ToString().Trim();
                            //dlhowyouHeared.SelectedValue = dtUserInfo.Rows[0]["howYouKnowUs"].ToString().Trim();
                        }
                    }
                    catch (Exception ex)
                    { }

                }

            }

        }
       
    }


    private void GridDataBind(long UserID)
    {
        objCC = new BLLUserCCInfo();
        objCC.userId = UserID;
        gvCustomers.DataSource = objCC.getUserCCInfoByUserID();
        gvCustomers.DataBind();
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

    private DataTable getSessionVal()
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
        return dtUser;
    }

    protected void imbEmailSave_Click(object sender, EventArgs e)
    {
        BLLUser objuser = new BLLUser();
        if (ViewState["CanChangeUserName"] != null && ViewState["CanChangeUserName"] != "false")
        {
            if (ViewState["IsCraditCard"] == null || ViewState["IsCraditCard"].ToString() == "false")
            {
                try
                {
                    DataTable dTable = getSessionVal();
                    if (Misc.validateEmailAddress(txtNewEmail.Text.Trim()) && txtNewEmail.Text != string.Empty)
                    {
                        objuser.email = txtNewEmail.Text.Trim();
                        DataTable dtuser = objuser.getUserDetailByEmail();
                        if (dtuser != null && dtuser.Rows.Count > 0)
                        {
                            divEmailUpdate.InnerHtml = "This email already registered";
                            
                            return;
                        }
                        int result;
                        obj = new BLLUser();
                        obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
                        obj.userName = txtNewEmail.Text.Trim();
                        obj.email = txtNewEmail.Text.Trim();
                        obj.modifiedDate = DateTime.Now;
                        obj.modifiedBy = obj.userId;
                        obj.isActive = true;
                        result = obj.updateUserFields();
                        if (result != 0)
                        {
                            //divEmailUpdate.InnerHtml = "Record is successfyly update";
                            string jScript;
                            jScript = "<script>";
                            jScript += "MessegeArea('Record is successfyly update','success');";
                            jScript += "</script>";
                            ScriptManager.RegisterClientScriptBlock(imbPasswordSave, typeof(Button), "Javascript", jScript, false);
                            ScriptManager.RegisterClientScriptBlock(lblEmail, typeof(Label), "script", "DisplayUpdated('" + lblEmail.ClientID + "', '" + txtNewEmail.Text.Trim() + "');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        if (ViewState["CanChangeUserName"] != null && !Convert.ToBoolean(ViewState["CanChangeUserName"].ToString().Trim()))
        {
            divEmailUpdate.InnerHtml = "You could not change email today. Please try after 24hours.";
            //string jScript;
            //jScript = "<script>";
            //jScript += "MessegeArea('You could not change email today. Please try after 24hours.','Error');";
            //jScript += "</script>";
            //ScriptManager.RegisterClientScriptBlock(btnChange, typeof(Button), "Javascript", jScript, false);
            //return;
        }
    }


    protected void imbPasswordSave_Click(object sender, EventArgs e)
    {
        try
        {
            BLLUser objuser = new BLLUser();

            DataTable dTable = getSessionVal();
            
            if (txtCurrentPassword.Text.Trim() != "" && txtNewPassword.Text.Trim() != "" && txtConfirmPassword.Text.Trim() != "")
            {
                objuser.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
                objuser.userPassword = HtmlRemoval.StripTagsRegexCompiled(txtCurrentPassword.Text.Trim());
                objuser.userName = HtmlRemoval.StripTagsRegexCompiled(dTable.Rows[0]["userName"].ToString().Trim());
                if (objuser.validateUserNamePassword().Rows.Count > 0)
                {
                    int result;
                    obj = new BLLUser();
                    obj.userPassword = txtNewPassword.Text.Trim();
                    obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
                    obj.modifiedDate = DateTime.Now;
                    obj.modifiedBy = obj.userId;
                    obj.isActive = true;
                    result = obj.updateUserFields();
                    ViewState["userPassword"] = txtNewPassword.Text.Trim();
                    if (result != 0)
                    {
                        //divPasswordUpdate.InnerHtml = "Password successfyly updated";
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Password has been changed','success');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(imbPasswordSave, typeof(Button), "Javascript", jScript, false);
                        return;
                    }
                    else
                    {
                        //divPasswordUpdate.InnerHtml = "Password not updated";
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Password not changed','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(imbPasswordSave, typeof(Button), "Javascript", jScript, false);
                        return;
                    }
                }
                else
                {
                    //divPasswordUpdate.InnerHtml = "You enter wrong old password";
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('You enter wrong old password','Error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(imbPasswordSave, typeof(Button), "Javascript", jScript, false);
                    return;
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void imbYourNameSave_Click(object sender, EventArgs e)
    {
        DataTable dTable = getSessionVal();
        if (txtFirstName.Text.Trim() != "" && txtLastName.Text.Trim() != "")
        {
            int result;
            obj = new BLLUser();
            obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
            obj.firstName = HtmlRemoval.StripTagsRegexCompiled(txtFirstName.Text.Trim());
            obj.lastName = HtmlRemoval.StripTagsRegexCompiled(txtLastName.Text.Trim());
            obj.modifiedDate = DateTime.Now;
            obj.modifiedBy = obj.userId;
            obj.isActive = true;
            result = obj.updateUserFields();
            if (result != 0)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Record is updated','success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                ScriptManager.RegisterClientScriptBlock(lblYourName, typeof(Label), "script", "DisplayUpdated('" + lblYourName.ClientID + "', '" + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + "');", true);
                return;
            }
        }
        else
        {

        }
    }

    protected void imbPhoneNumberSave_Click(object sender, EventArgs e)
    {
        DataTable dTable = getSessionVal();
        if (txtPhoneNumber.Text.Trim() != "")
        {
            int result;
            obj = new BLLUser();
            obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
            obj.phoneNo = HtmlRemoval.StripTagsRegexCompiled(txtPhoneNumber.Text.Trim());
            obj.modifiedDate = DateTime.Now;
            obj.modifiedBy = obj.userId;
            obj.isActive = true;
            result = obj.updateUserFields();
            if (result != 0)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Phone number is updated','success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                ScriptManager.RegisterClientScriptBlock(lblPhoneNumber, typeof(Label), "script", "DisplayUpdated('" + lblPhoneNumber.ClientID + "', '" + txtPhoneNumber.Text.Trim() + "');", true);
                return;
            }
        }
        else
        {

        }
    }

    protected void imbHowYouHearSave_Click(object sender, EventArgs e)
    {
        DataTable dTable = getSessionVal();
        if (txtHowYouHear.Text.Trim() != "")
        {
            int result;
            obj = new BLLUser();
            obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
            obj.howYouKnowUs = HtmlRemoval.StripTagsRegexCompiled(txtHowYouHear.Text.Trim());
            obj.modifiedDate = DateTime.Now;
            obj.modifiedBy = obj.userId;
            obj.isActive = true;
            result = obj.updateUserFields();
            if (result != 0)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Recoed is updated','success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                ScriptManager.RegisterClientScriptBlock(lblHowYouHear, typeof(Label), "script", "DisplayUpdated('" + lblHowYouHear.ClientID + "', '" + txtHowYouHear.Text.Trim() + "');", true);
                return;
            }
            else
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Recoed is not updated','error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;
            }
        }
        else
        {

        }
    }

    protected void imbCountrySave_Click(object sender, EventArgs e)
    {
        DataTable dTable = getSessionVal();
        if (ddlCountry.SelectedItem.Text != "")
        {
            int result;
            obj = new BLLUser();
            obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
            obj.countryId = Convert.ToInt32(ddlCountry.SelectedValue.Trim());
            obj.modifiedDate = DateTime.Now;
            obj.modifiedBy = obj.userId;
            obj.isActive = true;
            result = obj.updateUserFields();
            if (result != 0)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Country is changed','success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                ScriptManager.RegisterClientScriptBlock(lblCountry, typeof(Label), "script", "DisplayUpdated('" + lblCountry.ClientID + "', '" + ddlCountry.SelectedItem.Text + "');", true);
                return;
            }
        }
        else
        {

        }
    }

    protected void imbProvinceStateSave_Click(object sender, EventArgs e)
    {
        DataTable dTable = getSessionVal();
        if (ddlProvinceLive.SelectedItem.Text != "")
        {
            int result;
            obj = new BLLUser();
            obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
            obj.provinceId = Convert.ToInt32(ddlProvinceLive.SelectedValue.Trim());
            obj.modifiedDate = DateTime.Now;
            obj.modifiedBy = obj.userId;
            obj.isActive = true;
            result = obj.updateUserFields();
            if (result != 0)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Province is changed','success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                ScriptManager.RegisterClientScriptBlock(lblProvinceState, typeof(Label), "script", "DisplayUpdated('" + lblProvinceState.ClientID + "', '" + ddlProvinceLive.SelectedItem.Text + "');", true);
                return;
            }
        }
        else
        {

        }
    }

    protected void imbHomeCity_Click(object sender, EventArgs e)
    {
        DataTable dTable = getSessionVal();
        if (ddlProvinceLive.SelectedItem.Text != "")
        {
            int result;
            obj = new BLLUser();
            obj.userId = Convert.ToInt32(dTable.Rows[0]["userId"].ToString());
            obj.cityId = Convert.ToInt32(ddlCity.SelectedValue.Trim());
            obj.modifiedDate = DateTime.Now;
            obj.modifiedBy = obj.userId;
            obj.isActive = true;
            result = obj.updateUserFields();
            if (result != 0)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('City is changed','success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                ScriptManager.RegisterClientScriptBlock(lblHomeCity, typeof(Label), "script", "DisplayUpdated('" + lblHomeCity.ClientID + "', '" + ddlCity.SelectedItem.Text + "');", true);
                return;
            }
            else
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('City is not changed','error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                return;
            }
        }
        else
        {

        }
    }


    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindProvinces(Convert.ToInt32(ddlCountry.SelectedValue.ToString()));

        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
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
            lblProvinceState.Text = "";
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

    protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objCC = new BLLUserCCInfo();
        objCC.ccInfoID = Convert.ToInt64(e.CommandArgument);

        switch (e.CommandName)
        {
            //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
            case "EditCustomer":
                FillEditCustomerForm(Convert.ToInt64(e.CommandArgument));
                ViewState["IsCraditCard"] = "true";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "divPopup", "javascript:runPopup('" + sHiddenPopupValue + "');", true);
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
            GECEncryption objEnc = new GECEncryption();
            objCC = new BLLUserCCInfo();
            objCC.ccInfoID = ccinfoID;
            ViewState["ccInfoID"] = ccinfoID;
            DataTable dtCCinfo = objCC.getUserCCInfoByID();
            if (dtCCinfo != null && dtCCinfo.Rows.Count > 0)
            {
                /*
                hfDivPopupValue.Value = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
                hfDivPopupValue.Value += "|"; 
                hfDivPopupValue.Value += dtCCinfo.Rows[0]["ccInfoBAddress"].ToString();
                hfDivPopupValue.Value += "|";
                hfDivPopupValue.Value += "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
                hfDivPopupValue.Value += "|";
                string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
                if (strDate.Length == 2)
                {
                    hfDivPopupValue.Value += strDate[0].ToString();
                    hfDivPopupValue.Value += "|";
                    hfDivPopupValue.Value += strDate[1].ToString();
                    hfDivPopupValue.Value += "|";
                }
                hfDivPopupValue.Value += dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
                hfDivPopupValue.Value += "|";
                hfDivPopupValue.Value += dtCCinfo.Rows[0]["ccInfoBProvince"].ToString();
                hfDivPopupValue.Value += "|";
                hfDivPopupValue.Value += dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
                hfDivPopupValue.Value += "|";
                hfDivPopupValue.Value += ccinfoID;
                */

                sHiddenPopupValue = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
                sHiddenPopupValue += "|";
                sHiddenPopupValue += dtCCinfo.Rows[0]["ccInfoBAddress"].ToString();
                sHiddenPopupValue += "|";
                sHiddenPopupValue += "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
                sHiddenPopupValue += "|";
                string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
                if (strDate.Length == 2)
                {
                    sHiddenPopupValue += strDate[0].ToString();
                    sHiddenPopupValue += "|";
                    sHiddenPopupValue += strDate[1].ToString();
                    sHiddenPopupValue += "|";
                }
                sHiddenPopupValue += dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
                sHiddenPopupValue += "|";
                sHiddenPopupValue += dtCCinfo.Rows[0]["ccInfoBProvince"].ToString();
                sHiddenPopupValue += "|";
                sHiddenPopupValue += dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
                sHiddenPopupValue += "|";
                sHiddenPopupValue += ccinfoID;


                //txCardNumner.Text = "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
                //txtBillingAddress.Text = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString();
                //txtCCVNumber.Text = objEnc.DecryptData("colintastygochengccv", dtCCinfo.Rows[0]["ccInfoCCVNumber"].ToString());
                //txtCity.Text = dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
                //txtFirstNameCC.Text = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
                //txtPostalCode.Text = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
                //if (dtCCinfo.Rows[0]["ccInfoDEmail"] != null && dtCCinfo.Rows[0]["ccInfoDEmail"].ToString() != "")
                //{
                   // ViewState["userName"] = dtCCinfo.Rows[0]["ccInfoDEmail"].ToString();
                //}
                //string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
                //if (strDate.Length == 2)
                //{
                  //  ddlMonth.SelectedValue = strDate[0].ToString();
                    //ddlYear.SelectedValue = strDate[1].ToString();
                //}
                //try
                //{
                //    ddlState.SelectedValue = dtCCinfo.Rows[0]["ccInfoBProvince"].ToString();
                //}
                //catch (Exception ex)
                //{ }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void imbSaveCreditCardInfo_Click(object sender, EventArgs e)
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
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtSecurityCode.Text.Trim()));
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCardNumber.Text.Trim()));
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBillingName.Text.Trim()));
            objCC.ccInfoDEmail = ViewState["userName"].ToString();
            string[] strUserName = txtBillingName.Text.Split(' ');
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
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtSecurityCode.Text.Trim()));
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCardNumber.Text.Trim()));
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBillingName.Text.Trim()));
            objCC.ccInfoDEmail = ViewState["userName"].ToString();
            string[] strUserName = txtBillingName.Text.Split(' ');
            objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(strUserName[0].ToString());
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = strUserName[1].ToString();
            }
            objCC.updateUserCCInfoByID();
        }
       string jScript;
        jScript = "<script>";
        //jScript += "HideAddCraditCard();";
        jScript += "MessegeArea('Record has been Updated successfully.' , 'success');";

        jScript += "</script>";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Javascript", jScript, false);

        ViewState["IsCraditCard"] = "true";
    }

    [System.Web.Services.WebMethod(true)]
    public static DataTable FillDropDown(string countryID)
    {

        DataTable dTable = new DataTable();
        DataTable dt = Misc.getProvincesByCountryID(Convert.ToInt32(countryID));

        return dTable;
    }

}