using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Takeout_UserControls_restaurant_deliveryOptions : System.Web.UI.UserControl
{
    BLLRestaurantSetting objSet = new BLLRestaurantSetting();
    BLLRestaurantAvailableTime objTime = new BLLRestaurantAvailableTime();
    BLLRestaurant objRes = new BLLRestaurant();
    public string strHide = "block";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["restaurant"] != null)
                {
                    DataTable dtUser = (DataTable)Session["restaurant"];
                    ViewState["userID"] = dtUser.Rows[0]["userId"].ToString();
                    bindResaturantSettings(Convert.ToInt64(dtUser.Rows[0]["userId"]));
                    lblMsg.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                lblMsg.Visible = true;
            }
        }
    }

    protected void bindResaturantSettings(long userId)
    {
        try 
        {
            objSet.createdBy = userId;
            DataTable dtResSettings = objSet.getRestaurantSettingByResturantOwnerID();
            if (dtResSettings != null && dtResSettings.Rows.Count > 0)
            {
                if (dtResSettings.Rows[0]["isDelivery"].ToString()!="" && Convert.ToBoolean(dtResSettings.Rows[0]["isDelivery"]))
                {
                    chkDelivery.Checked = Convert.ToBoolean(dtResSettings.Rows[0]["isDelivery"]);
                    if (dtResSettings.Rows[0]["deliveryCharge"].ToString() != "0.00")
                    {
                        txtInsideMoney.Text = dtResSettings.Rows[0]["deliveryCharge"].ToString();
                        chkFree.Checked = false;
                        strHide = "block";
                    }
                    else
                    {
                        chkFree.Checked = true;
                        strHide = "none";
                    }
                    txtInsideKM.Text = dtResSettings.Rows[0]["freeDeliveryDistance"].ToString();
                    if (Convert.ToBoolean(((dtResSettings.Rows[0]["exceededLimitsOK"] == null) || (dtResSettings.Rows[0]["exceededLimitsOK"].ToString() == "")) ? 0 : dtResSettings.Rows[0]["exceededLimitsOK"]))                    
                    {
                        chkDeliveryOK1.Checked = Convert.ToBoolean(dtResSettings.Rows[0]["exceededLimitsOK"]);
                        txtPer.Text = dtResSettings.Rows[0]["exceededLimitCharge"].ToString();
                        txtKM.Text = (float.Parse(dtResSettings.Rows[0]["exceededLimitCharge"].ToString()) / float.Parse(dtResSettings.Rows[0]["exceededLimitChargeUnit"].ToString())).ToString();
                    }
                    
                    //If Delivery Amount is less than minimum Amount
                    if (Convert.ToBoolean(((dtResSettings.Rows[0]["belowLimitOK"] == null) || (dtResSettings.Rows[0]["belowLimitOK"].ToString() == "")) ? 0 : dtResSettings.Rows[0]["belowLimitOK"]))
                    {
                        chkDelMinAmt.Checked = Convert.ToBoolean(dtResSettings.Rows[0]["belowLimitOK"] == null ? 0 : dtResSettings.Rows[0]["belowLimitOK"]);
                        txtDelMinAmtPer.Text = dtResSettings.Rows[0]["belowLimitCharge"].ToString();                        
                    }
                }
                if (dtResSettings.Rows[0]["minimumOrderAmount"].ToString() != "")
                {
                    txtMinDelOrderAmnt.Text = dtResSettings.Rows[0]["minimumOrderAmount"].ToString();
                }
                if (dtResSettings.Rows[0]["realMenuImage"].ToString()!="")
                {
                    imgRealImage.ImageUrl = "~/images/MenuRealImages/" + dtResSettings.Rows[0]["realMenuImage"].ToString();
                    imgRealImage.Visible = true;
                    hfRealImage.Value = dtResSettings.Rows[0]["realMenuImage"].ToString();
                }

                objTime.settingID = Convert.ToInt64(dtResSettings.Rows[0]["settingID"]);
                ViewState["settingID"] = dtResSettings.Rows[0]["settingID"];
                DataTable dtAvailableTime = objTime.getRestaurantAvailableTimeBySettingID();
                if (dtAvailableTime != null && dtAvailableTime.Rows.Count > 0)
                {

                    if (Convert.ToBoolean(dtAvailableTime.Rows[0]["isDay"]))
                    {
                        chkDay1Monday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[0]["isDay"]);
                        ddlClose1_1.SelectedIndex = 0;
                        ddlClose1_2.SelectedIndex = 0;
                        ddlOpen1_1.SelectedIndex = 0;
                        ddlOpen1_2.SelectedIndex = 0;
                    }
                    else
                    {
                        chkDay1Monday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[0]["isDay"]);

                        ddlClose1_1.SelectedValue = returnTime(dtAvailableTime.Rows[0]["close1"].ToString());
                        ddlClose1_2.SelectedValue = returnTime(dtAvailableTime.Rows[0]["close2"].ToString());
                        ddlOpen1_1.SelectedValue = returnTime(dtAvailableTime.Rows[0]["open1"].ToString());
                        ddlOpen1_2.SelectedValue = returnTime(dtAvailableTime.Rows[0]["open2"].ToString());
                    }


                    if (Convert.ToBoolean(dtAvailableTime.Rows[1]["isDay"]))
                    {
                        chkDay2Tuesday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[1]["isDay"]);
                        ddlClose2_1.SelectedIndex = 0;
                        ddlClose2_2.SelectedIndex = 0;
                        ddlOpen2_1.SelectedIndex = 0;
                        ddlOpen2_2.SelectedIndex = 0;
                    }
                    else
                    {
                        chkDay2Tuesday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[1]["isDay"]);

                        ddlClose2_1.SelectedValue = returnTime(dtAvailableTime.Rows[1]["close1"].ToString());
                        ddlClose2_2.SelectedValue = returnTime(dtAvailableTime.Rows[1]["close2"].ToString());
                        ddlOpen2_1.SelectedValue = returnTime(dtAvailableTime.Rows[1]["open1"].ToString());
                        ddlOpen2_2.SelectedValue = returnTime(dtAvailableTime.Rows[1]["open2"].ToString());
                    }

                    if (Convert.ToBoolean(dtAvailableTime.Rows[2]["isDay"]))
                    {
                        chkDay3Wednesday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[2]["isDay"]);
                        ddlClose3_1.SelectedIndex = 0;
                        ddlClose3_2.SelectedIndex = 0;
                        ddlOpen3_1.SelectedIndex = 0;
                        ddlOpen3_2.SelectedIndex = 0;
                    }
                    else
                    {
                        chkDay3Wednesday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[2]["isDay"]);

                        ddlClose3_1.SelectedValue = returnTime(dtAvailableTime.Rows[2]["close1"].ToString());
                        ddlClose3_2.SelectedValue = returnTime(dtAvailableTime.Rows[2]["close2"].ToString());
                        ddlOpen3_1.SelectedValue = returnTime(dtAvailableTime.Rows[2]["open1"].ToString());
                        ddlOpen3_2.SelectedValue = returnTime(dtAvailableTime.Rows[2]["open2"].ToString());
                    }

                    if (Convert.ToBoolean(dtAvailableTime.Rows[3]["isDay"]))
                    {
                        chkDay4Thursday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[3]["isDay"]);
                        ddlClose4_1.SelectedIndex = 0;
                        ddlClose4_2.SelectedIndex = 0;
                        ddlOpen4_1.SelectedIndex = 0;
                        ddlOpen4_2.SelectedIndex = 0;
                    }
                    else
                    {
                        chkDay4Thursday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[3]["isDay"]);

                        ddlClose4_1.SelectedValue = returnTime(dtAvailableTime.Rows[3]["close1"].ToString());
                        ddlClose4_2.SelectedValue = returnTime(dtAvailableTime.Rows[3]["close2"].ToString());
                        ddlOpen4_1.SelectedValue = returnTime(dtAvailableTime.Rows[3]["open1"].ToString());
                        ddlOpen4_2.SelectedValue = returnTime(dtAvailableTime.Rows[3]["open2"].ToString());
                    }
                    if (Convert.ToBoolean(dtAvailableTime.Rows[4]["isDay"]))
                    {
                        chkDay5Friday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[4]["isDay"]);
                        ddlClose5_1.SelectedIndex = 0;
                        ddlClose5_2.SelectedIndex = 0;
                        ddlOpen5_1.SelectedIndex = 0;
                        ddlOpen5_2.SelectedIndex = 0;
                    }
                    else
                    {
                        chkDay5Friday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[4]["isDay"]);

                        ddlClose5_1.SelectedValue = returnTime(dtAvailableTime.Rows[4]["close1"].ToString());
                        ddlClose5_2.SelectedValue = returnTime(dtAvailableTime.Rows[4]["close2"].ToString());
                        ddlOpen5_1.SelectedValue = returnTime(dtAvailableTime.Rows[4]["open1"].ToString());
                        ddlOpen5_2.SelectedValue = returnTime(dtAvailableTime.Rows[4]["open2"].ToString());
                    }

                    if (Convert.ToBoolean(dtAvailableTime.Rows[5]["isDay"]))
                    {
                        chkDay6Saturday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[5]["isDay"]);
                        ddlClose6_1.SelectedIndex = 0;
                        ddlClose6_2.SelectedIndex = 0;
                        ddlOpen6_1.SelectedIndex = 0;
                        ddlOpen6_2.SelectedIndex = 0;
                    }
                    else
                    {
                        chkDay6Saturday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[5]["isDay"]);

                        ddlClose6_1.SelectedValue = returnTime(dtAvailableTime.Rows[5]["close1"].ToString());
                        ddlClose6_2.SelectedValue = returnTime(dtAvailableTime.Rows[5]["close2"].ToString());
                        ddlOpen6_1.SelectedValue = returnTime(dtAvailableTime.Rows[5]["open1"].ToString());
                        ddlOpen6_2.SelectedValue = returnTime(dtAvailableTime.Rows[5]["open2"].ToString());
                    }
                    if (Convert.ToBoolean(dtAvailableTime.Rows[6]["isDay"]))
                    {
                        chkDay7Sunday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[6]["isDay"]);
                        ddlClose7_1.SelectedIndex = 0;
                        ddlClose7_2.SelectedIndex = 0;
                        ddlOpen7_1.SelectedIndex = 0;
                        ddlOpen7_2.SelectedIndex = 0;
                    }
                    else
                    {
                        chkDay7Sunday.Checked = Convert.ToBoolean(dtAvailableTime.Rows[6]["isDay"]);

                        ddlClose7_1.SelectedValue = returnTime(dtAvailableTime.Rows[6]["close1"].ToString());
                        ddlClose7_2.SelectedValue = returnTime(dtAvailableTime.Rows[6]["close2"].ToString());
                        ddlOpen7_1.SelectedValue = returnTime(dtAvailableTime.Rows[6]["open1"].ToString());
                        ddlOpen7_2.SelectedValue = returnTime(dtAvailableTime.Rows[6]["open2"].ToString());
                    }
                }
             
                btnSave.Text = "Update";
            }
            else
            {
                btnSave.Text = "Save";
                imgRealImage.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMsg.Visible = true;
        }
    }

    private string returnTime(string strTime)
    {
        string strReplace = "";
        try 
        {
            if (strTime != "")
            {
                if (strTime.StartsWith("0"))
                {
                    strTime = strTime.Remove(0, 1);
                }
                if (strTime.Contains(":15"))
                {
                    strReplace = strTime.Replace(":15", ".25");
                }
                else if (strTime.Contains(":30"))
                {
                    strReplace = strTime.Replace(":30", ".50");
                }
                else if (strTime.Contains(":45"))
                {
                    strReplace = strTime.Replace(":45", ".75");
                }
                else if (strTime.Contains(":00"))
                {
                    strReplace = strTime.Replace(":00", ".00");
                }
            }
            return strReplace;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SaveSettings();
        }
        else
        {
            UpdateSettings();
        }
        bindResaturantSettings(Convert.ToInt32(ViewState["userID"]));
    }

    private void SaveSettings()
    {/*
        try
        {
            objRes.userId = Convert.ToInt32(ViewState["userID"]);
            DataTable dtRes = objRes.getRestaurantByUserID();
            if (chkDelivery.Checked)
            {
                objSet.isDelivery = chkDelivery.Checked;
                if (txtInsideKM.Text.ToString() != "")
                {
                    objSet.freeDeliveryDistance = float.Parse(txtInsideKM.Text.Trim());
                }
                if (txtInsideMoney.Text.ToString() != "")
                {
                    objSet.deliveryCharge = float.Parse(txtInsideMoney.Text.Trim());
                }
                objSet.exceededLimitsOK = chkDeliveryOK1.Checked;
                if (chkDeliveryOK1.Checked)
                {
                    objSet.exceededLimitCharge = float.Parse(txtPer.Text.Trim());
                    objSet.exceededLimitChargeUnit = float.Parse(txtPer.Text.Trim()) / float.Parse(txtKM.Text.Trim());
                }

                //If Delivery Amount is less than minimum Amount
                objSet.belowLimitsOK = chkDelMinAmt.Checked;
                if (chkDelMinAmt.Checked)
                {
                    objSet.belowLimitCharge = float.Parse(txtDelMinAmtPer.Text.Trim());                    
                }
            }

            if (txtMinDelOrderAmnt.Text.Trim() != "")
            {
                objSet.minimumOrderAmount = float.Parse(txtMinDelOrderAmnt.Text.Trim());
            }

            objSet.createdBy = Convert.ToInt64(ViewState["userID"]);
            if (fuRealImage.HasFile)
            {
                string[] strExtension = fuRealImage.FileName.Split('.');
                string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\Images\\MenuRealImages\\" + strUniqueID;
                fuRealImage.SaveAs(strSrcPath);

                objSet.realMenuImage = strUniqueID;
            }

            objSet.restaurantId = Convert.ToInt64(dtRes.Rows[0]["restaurantId"]);
            long SettingID = objSet.createRestaurantSetting();

            //////////////////////////////////////Available Time ///////////////////////////////////////////////

            objTime.settingID = SettingID;
            if (chkDay1Monday.Checked)
            {
                objTime.isDay = true;
                objTime.open1 = "";
                objTime.close1 = "";
                objTime.open2 = "";
                objTime.close2 = "";
            }
            else
            {
                objTime.isDay = false;
                objTime.open1 = ddlOpen1_1.SelectedItem.ToString();
                objTime.close1 = ddlClose1_1.SelectedItem.ToString();

                if (ddlOpen1_2.SelectedIndex != 0 || ddlClose1_2.SelectedIndex != 0)
                {
                    objTime.open2 = ddlOpen1_2.SelectedItem.ToString();
                    objTime.close2 = ddlClose1_2.SelectedItem.ToString();
                }
            }
            objTime.dayOfWeek = 1;
            objTime.createdBy = Convert.ToInt64(ViewState["userID"]);
            objTime.createRestaurantAvailableTime();
            //////////////////////////////////Monday Settings Ends///////////////////////////////////////////////

            objTime.settingID = SettingID;
            if (chkDay2Tuesday.Checked)
            {
                objTime.isDay = true;
                objTime.open1 = "";
                objTime.close1 = "";
                objTime.open2 = "";
                objTime.close2 = "";
            }
            else
            {
                objTime.isDay = false;
                objTime.open1 = ddlOpen2_1.SelectedItem.ToString();
                objTime.close1 = ddlClose2_1.SelectedItem.ToString();

                if (ddlOpen2_2.SelectedIndex != 0 || ddlClose2_2.SelectedIndex != 0)
                {
                    objTime.open2 = ddlOpen2_2.SelectedItem.ToString();
                    objTime.close2 = ddlClose2_2.SelectedItem.ToString();
                }
            }
            objTime.dayOfWeek = 2;
            objTime.createdBy = Convert.ToInt64(ViewState["userID"]);
            objTime.createRestaurantAvailableTime();
            //////////////////////////////////Tuesday Settings Ends///////////////////////////////////////////////

            objTime.settingID = SettingID;
            if (chkDay3Wednesday.Checked)
            {
                objTime.isDay = true;
                objTime.open1 = "";
                objTime.close1 = "";
                objTime.open2 = "";
                objTime.close2 = "";
            }
            else
            {
                objTime.isDay = false;
                objTime.open1 = ddlOpen3_1.SelectedItem.ToString();
                objTime.close1 = ddlClose3_1.SelectedItem.ToString();

                if (ddlOpen3_2.SelectedIndex != 0 || ddlClose3_2.SelectedIndex != 0)
                {
                    objTime.open2 = ddlOpen3_1.SelectedItem.ToString();
                    objTime.close2 = ddlClose3_2.SelectedItem.ToString();
                }
            }
            objTime.dayOfWeek = 3;
            objTime.createdBy = Convert.ToInt64(ViewState["userID"]);
            objTime.createRestaurantAvailableTime();
            //////////////////////////////////Wednesday Settings Ends///////////////////////////////////////////////

            objTime.settingID = SettingID;
            if (chkDay4Thursday.Checked)
            {
                objTime.isDay = true;
                objTime.open1 = "";
                objTime.close1 = "";
                objTime.open2 = "";
                objTime.close2 = "";
            }
            else
            {
                objTime.isDay = false;
                objTime.open1 = ddlOpen4_1.SelectedItem.ToString();
                objTime.close1 = ddlClose4_1.SelectedItem.ToString();

                if (ddlOpen4_2.SelectedIndex != 0 || ddlClose4_2.SelectedIndex != 0)
                {
                    objTime.open2 = ddlOpen4_1.SelectedItem.ToString();
                    objTime.close2 = ddlClose4_2.SelectedItem.ToString();
                }
            }
            objTime.dayOfWeek = 4;
            objTime.createdBy = Convert.ToInt64(ViewState["userID"]);
            objTime.createRestaurantAvailableTime();
            //////////////////////////////////Thursday Settings Ends///////////////////////////////////////////////

            objTime.settingID = SettingID;
            if (chkDay5Friday.Checked)
            {
                objTime.isDay = true;
                objTime.open1 = "";
                objTime.close1 = "";
                objTime.open2 = "";
                objTime.close2 = "";
            }
            else
            {
                objTime.isDay = false;
                objTime.open1 = ddlOpen5_1.SelectedItem.ToString();
                objTime.close1 = ddlClose5_1.SelectedItem.ToString();

                if (ddlOpen5_2.SelectedIndex != 0 || ddlClose5_2.SelectedIndex != 0)
                {
                    objTime.open2 = ddlOpen5_1.SelectedItem.ToString();
                    objTime.close2 = ddlClose5_2.SelectedItem.ToString();
                }
            }
            objTime.dayOfWeek = 5;
            objTime.createdBy = Convert.ToInt64(ViewState["userID"]);
            objTime.createRestaurantAvailableTime();
            //////////////////////////////////Friday Settings Ends///////////////////////////////////////////////

            objTime.settingID = SettingID;
            if (chkDay6Saturday.Checked)
            {
                objTime.isDay = true;
                objTime.open1 = "";
                objTime.close1 = "";
                objTime.open2 = "";
                objTime.close2 = "";
            }
            else
            {
                objTime.isDay = false;
                objTime.open1 = ddlOpen6_1.SelectedItem.ToString();
                objTime.close1 = ddlClose6_1.SelectedItem.ToString();

                if (ddlOpen6_2.SelectedIndex != 0 || ddlClose6_2.SelectedIndex != 0)
                {
                    objTime.open2 = ddlOpen6_1.SelectedItem.ToString();
                    objTime.close2 = ddlClose6_2.SelectedItem.ToString();
                }
            }
            objTime.dayOfWeek = 6;
            objTime.createdBy = Convert.ToInt64(ViewState["userID"]);
            objTime.createRestaurantAvailableTime();
            //////////////////////////////////Saturday Settings Ends///////////////////////////////////////////////

            objTime.settingID = SettingID;
            if (chkDay7Sunday.Checked)
            {
                objTime.isDay = true;
                objTime.open1 = "";
                objTime.close1 = "";
                objTime.open2 = "";
                objTime.close2 = "";
            }
            else
            {
                objTime.isDay = false;
                objTime.open1 = ddlOpen7_1.SelectedItem.ToString();
                objTime.close1 = ddlClose7_1.SelectedItem.ToString();

                if (ddlOpen7_2.SelectedIndex != 0 || ddlClose7_2.SelectedIndex != 0)
                {
                    objTime.open2 = ddlOpen7_1.SelectedItem.ToString();
                    objTime.close2 = ddlClose7_2.SelectedItem.ToString();
                }
            }
            objTime.dayOfWeek = 7;
            objTime.createdBy = Convert.ToInt64(ViewState["userID"]);
            objTime.createRestaurantAvailableTime();
            //////////////////////////////////Sunday Settings Ends///////////////////////////////////////////////

            lblMsg.Text = "Settings have been saved successfully.";
            lblMsg.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMsg.Visible = true;
        }*/
    }
    private void UpdateSettings()
    {
        /*try
        {
            objRes.userId = Convert.ToInt32(ViewState["userID"]);
            DataTable dtRes = objRes.getRestaurantByUserID();
            if (chkDelivery.Checked)
            {
                objSet.isDelivery = chkDelivery.Checked;
                if (!chkFree.Checked)
                {
                    if (txtInsideMoney.Text.ToString() != "")
                    {
                        objSet.deliveryCharge = float.Parse(txtInsideMoney.Text.Trim());
                        objSet.deliveryChargeUnit = float.Parse(txtInsideMoney.Text.Trim()) / float.Parse(txtInsideKM.Text.Trim());
                    }
                }
                if (txtInsideKM.Text.ToString() != "")
                {
                    objSet.freeDeliveryDistance = float.Parse(txtInsideKM.Text.Trim());
                }
                
                objSet.exceededLimitsOK = chkDeliveryOK1.Checked;
                if (chkDeliveryOK1.Checked)
                {
                    objSet.exceededLimitCharge = float.Parse(txtPer.Text.Trim());
                    objSet.exceededLimitChargeUnit = float.Parse(txtPer.Text.Trim()) / float.Parse(txtKM.Text.Trim());
                }


                //If Delivery Amount is less than minimum Amount
                objSet.belowLimitsOK = chkDelMinAmt.Checked;
                if (chkDelMinAmt.Checked)
                {
                    objSet.belowLimitCharge = float.Parse(txtDelMinAmtPer.Text.Trim());                    
                }
                
                objSet.modifiedBy = Convert.ToInt64(ViewState["userID"]);
            }

            if (txtMinDelOrderAmnt.Text.Trim() != "")
            {
                objSet.minimumOrderAmount = float.Parse(txtMinDelOrderAmnt.Text.Trim());
            }

            if (fuRealImage.HasFile)
            {
                string[] strExtension = fuRealImage.FileName.Split('.');
                string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\Images\\MenuRealImages\\" + strUniqueID;
                fuRealImage.SaveAs(strSrcPath);

                objSet.realMenuImage = strUniqueID;
            }
            else
            {
                objSet.realMenuImage = hfRealImage.Value.ToString();
            }

            objSet.settingID = Convert.ToInt64(ViewState["settingID"]);
            objSet.updateRestaurantSetting();

            //////////////////////////////////////Available Time ///////////////////////////////////////////////

            string query = "select timeId from restaurantAvailableTime where settingID = " + ViewState["settingID"].ToString();
            DataTable dtIDs = Misc.search(query);

            if (dtIDs != null && dtIDs.Rows.Count > 0)
            {
                if (chkDay1Monday.Checked)
                {
                    objTime.isDay = true;
                    objTime.open1 = "";
                    objTime.close1 = "";
                    objTime.open2 = "";
                    objTime.close2 = "";
                }
                else
                {
                    objTime.isDay = false;
                    if (ddlOpen1_1.SelectedIndex != 0)
                    {
                        objTime.open1 = ddlOpen1_1.SelectedItem.ToString();
                        objTime.close1 = ddlClose1_1.SelectedItem.ToString();
                    }
                    if (ddlOpen1_2.SelectedIndex != 0 && ddlClose1_2.SelectedIndex != 0)
                    {
                        objTime.open2 = ddlOpen1_2.SelectedItem.ToString();
                        objTime.close2 = ddlClose1_2.SelectedItem.ToString();
                    }
                }
                objTime.timeId = Convert.ToInt64(dtIDs.Rows[0]["timeId"]);
                objTime.modifiedBy = Convert.ToInt64(ViewState["userID"]);
                objTime.updateRestaurantAvailableTime();
                //////////////////////////////////Monday Settings Ends///////////////////////////////////////////////

                if (chkDay2Tuesday.Checked)
                {
                    objTime.isDay = true;
                    objTime.open1 = "";
                    objTime.close1 = "";
                    objTime.open2 = "";
                    objTime.close2 = "";
                }
                else
                {
                    objTime.isDay = false;
                    if (ddlOpen2_1.SelectedIndex != 0)
                    {
                        objTime.open1 = ddlOpen2_1.SelectedItem.ToString();
                        objTime.close1 = ddlClose2_1.SelectedItem.ToString();
                    }
                    if (ddlOpen2_2.SelectedIndex != 0 && ddlClose2_2.SelectedIndex != 0)
                    {
                        objTime.open2 = ddlOpen2_2.SelectedItem.ToString();
                        objTime.close2 = ddlClose2_2.SelectedItem.ToString();
                    }
                }
                objTime.timeId = Convert.ToInt64(dtIDs.Rows[1]["timeId"]);
                objTime.modifiedBy = Convert.ToInt64(ViewState["userID"]);
                objTime.updateRestaurantAvailableTime();
                //////////////////////////////////Tuesday Settings Ends///////////////////////////////////////////////

                if (chkDay3Wednesday.Checked)
                {
                    objTime.isDay = true;
                    objTime.open1 = "";
                    objTime.close1 = "";
                    objTime.open2 = "";
                    objTime.close2 = "";
                }
                else
                {
                    objTime.isDay = false;
                    if (ddlOpen3_1.SelectedIndex != 0)
                    {
                        objTime.open1 = ddlOpen3_1.SelectedItem.ToString();
                        objTime.close1 = ddlClose3_1.SelectedItem.ToString();
                    }
                    if (ddlOpen3_2.SelectedIndex != 0 && ddlClose3_2.SelectedIndex != 0)
                    {
                        objTime.open2 = ddlOpen3_2.SelectedItem.ToString();
                        objTime.close2 = ddlClose3_2.SelectedItem.ToString();
                    }
                }
                objTime.timeId = Convert.ToInt64(dtIDs.Rows[2]["timeId"]);
                objTime.modifiedBy = Convert.ToInt64(ViewState["userID"]);
                objTime.updateRestaurantAvailableTime();
                //////////////////////////////////Wednesday Settings Ends///////////////////////////////////////////////

                if (chkDay4Thursday.Checked)
                {
                    objTime.isDay = true;
                    objTime.open1 = "";
                    objTime.close1 = "";
                    objTime.open2 = "";
                    objTime.close2 = "";
                }
                else
                {
                    objTime.isDay = false;
                    if (ddlOpen4_1.SelectedIndex != 0)
                    {
                        objTime.open1 = ddlOpen4_1.SelectedItem.ToString();
                        objTime.close1 = ddlClose4_1.SelectedItem.ToString();
                    }
                    if (ddlOpen4_2.SelectedIndex != 0 && ddlClose4_2.SelectedIndex != 0)
                    {
                        objTime.open2 = ddlOpen4_2.SelectedItem.ToString();
                        objTime.close2 = ddlClose4_2.SelectedItem.ToString();
                    }
                }
                objTime.timeId = Convert.ToInt64(dtIDs.Rows[3]["timeId"]);
                objTime.modifiedBy = Convert.ToInt64(ViewState["userID"]);
                objTime.updateRestaurantAvailableTime();
                //////////////////////////////////Thursday Settings Ends///////////////////////////////////////////////

                if (chkDay5Friday.Checked)
                {
                    objTime.isDay = true;
                    objTime.open1 = "";
                    objTime.close1 = "";
                    objTime.open2 = "";
                    objTime.close2 = "";
                }
                else
                {
                    objTime.isDay = false;
                    if (ddlOpen5_1.SelectedIndex != 0)
                    {
                        objTime.open1 = ddlOpen5_1.SelectedItem.ToString();
                        objTime.close1 = ddlClose5_1.SelectedItem.ToString();
                    }
                    if (ddlOpen5_2.SelectedIndex != 0 && ddlClose5_2.SelectedIndex != 0)
                    {
                        objTime.open2 = ddlOpen5_2.SelectedItem.ToString();
                        objTime.close2 = ddlClose5_2.SelectedItem.ToString();
                    }
                }
                objTime.timeId = Convert.ToInt64(dtIDs.Rows[4]["timeId"]);
                objTime.modifiedBy = Convert.ToInt64(ViewState["userID"]);
                objTime.updateRestaurantAvailableTime();
                //////////////////////////////////Friday Settings Ends///////////////////////////////////////////////

                if (chkDay6Saturday.Checked)
                {
                    objTime.isDay = true;
                    objTime.open1 = "";
                    objTime.close1 = "";
                    objTime.open2 = "";
                    objTime.close2 = "";
                }
                else
                {
                    if (ddlOpen6_1.SelectedIndex != 0)
                    {
                        objTime.open1 = ddlOpen6_1.SelectedItem.ToString();
                        objTime.close1 = ddlClose6_1.SelectedItem.ToString();
                    }
                    if (ddlOpen6_2.SelectedIndex != 0 && ddlClose6_2.SelectedIndex != 0)
                    {
                        objTime.open2 = ddlOpen6_2.SelectedItem.ToString();
                        objTime.close2 = ddlClose6_2.SelectedItem.ToString();
                    }
                }
                objTime.timeId = Convert.ToInt64(dtIDs.Rows[5]["timeId"]);
                objTime.modifiedBy = Convert.ToInt64(ViewState["userID"]);
                objTime.updateRestaurantAvailableTime();
                //////////////////////////////////Saturday Settings Ends///////////////////////////////////////////////

                if (chkDay7Sunday.Checked)
                {
                    objTime.isDay = true;
                    objTime.open1 = "";
                    objTime.close1 = "";
                    objTime.open2 = "";
                    objTime.close2 = "";
                }
                else
                {
                    objTime.isDay = false;
                    if (ddlOpen7_1.SelectedIndex != 0)
                    {
                        objTime.open1 = ddlOpen7_1.SelectedItem.ToString();
                        objTime.close1 = ddlClose7_1.SelectedItem.ToString();
                    }
                    if (ddlOpen7_2.SelectedIndex != 0 && ddlClose7_2.SelectedIndex != 0)
                    {
                        objTime.open2 = ddlOpen7_2.SelectedItem.ToString();
                        objTime.close2 = ddlClose7_2.SelectedItem.ToString();
                    }
                }
                objTime.timeId = Convert.ToInt64(dtIDs.Rows[6]["timeId"]);
                objTime.modifiedBy = Convert.ToInt64(ViewState["userID"]);
                objTime.updateRestaurantAvailableTime();
                //////////////////////////////////Sunday Settings Ends///////////////////////////////////////////////
                lblMsg.Text = "Settings have been updated successfully.";
                lblMsg.Visible = true;
                bindResaturantSettings(Convert.ToInt64(ViewState["userID"]));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMsg.Visible = true;
        }*/
    }
}
