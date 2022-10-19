using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using GecLibrary;
using System.Text;
using com.optimalpayments.test.webservices;
using System.Collections;

public partial class MyPayment : System.Web.UI.Page
{
    public static string strhideShippingDiv = "";
    BLLUser objUser = new BLLUser();
    BLLUserCCInfo objCCInfo = new BLLUserCCInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtUser = null;
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {
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
                    if (Session["dtProductCart"] != null)
                    {
                        DataTable dtCart = (DataTable)Session["dtProductCart"];
                        if (dtCart != null && dtCart.Rows.Count > 0)
                        {
                            ViewState["userName"] = dtUser.Rows[0]["userName"].ToString();
                            ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                            LoadDropDownList();
                            getRemainedGainedBalByUserId(dtUser);
                            GridDataBind(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                            resetAmounts();

                        }
                        else
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["YourSite"].ToString() + "/default.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["YourSite"].ToString() + "/default.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "login.aspx");
            }
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

        }
        catch (Exception ex)
        {
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

    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strScript = "uncheckOthers(" + ((RadioButton)e.Row.Cells[0].FindControl("MyRadioButton")).ClientID + ");";
                ((RadioButton)e.Row.Cells[0].FindControl("MyRadioButton")).Attributes.Add("onclick", strScript);
            }
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Error ');", true);
        }
    }

    protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
            case "EditCustomer":
                //Show the Credit Card Information
                clearBillingFields();
                this.divBilling.Visible = true;
                divSaveCancelArea.Visible = true;
                GetAndSetCreditCardInfo(int.Parse(e.CommandArgument.ToString()));
                //Set the Mode here
                this.hfMode.Value = "grid";
                foreach (GridViewRow gvr in gvCustomers.Rows)
                {
                    RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");

                    LinkButton LnkbtnEdit = (LinkButton)gvr.FindControl("btnEdit");
                    //Button myButton = (Button)gvr.FindControl("b1");
                    if (LnkbtnEdit.CommandArgument.ToString() == e.CommandArgument.ToString())
                    {
                        rdb.Checked = true;
                    }
                    else { rdb.Checked = false; }
                }
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;

                break;

            case "DeleteCustomer":
                BLLUserCCInfo objCC = new BLLUserCCInfo();
                objCC.ccInfoID = Convert.ToInt64(e.CommandArgument);
                objCC.deleteUserCCInfo();
                GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
                lblMessage.Text = "Record has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                //}
                break;
        }
    }

    private void checkIPAddress()
    {
        try
        {
            BLLRequestChecker objRequestChecker = new BLLRequestChecker();
            objRequestChecker.requestIP = Request.UserHostAddress.ToString();
            DataTable dtCheck = objRequestChecker.AddRequestToRequestChecker();
            if (dtCheck != null && dtCheck.Rows[0]["Check"] != null
                && dtCheck.Rows[0]["Check"].ToString().Trim() != "" && dtCheck.Rows[0]["Check"].ToString().Trim().ToLower() == "true")
            {
                Response.Redirect("IPBlock.aspx");
                Response.End();
            }
        }
        catch (Exception ex)
        { }
    }

    private void GetAndSetCreditCardInfo(int iCCInfoID)
    {
        try
        {

            divSaveCancelArea.Visible = true;
            divBilling.Visible = true;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            btnUpdate.ValidationGroup = "Update";
            RequiredFieldValidator1.ValidationGroup = "Update";
            RequiredFieldValidator2.ValidationGroup = "Update";
            RequiredFieldValidator3.ValidationGroup = "Update";
            RequiredFieldValidator4.ValidationGroup = "Update";
            RequiredFieldValidator5.ValidationGroup = "Update";
            RequiredFieldValidator6.ValidationGroup = "Update";
            RequiredFieldValidator7.ValidationGroup = "Update";
            RequiredFieldValidator8.ValidationGroup = "Update";
            cvCreditCard.ValidationGroup = "Update";


            BLLUserCCInfo objCCInfo = new BLLUserCCInfo();
            objCCInfo.ccInfoID = Convert.ToInt64(iCCInfoID.ToString());
            //Get the Credit Card Info of User here
            DataTable dtUserCCInfo = objCCInfo.getUserCCInfoByID();
            if ((dtUserCCInfo != null) && (dtUserCCInfo.Rows.Count > 0))
            {
                //Set the Delivery Information
                this.hfCCInfoIdEdit.Value = dtUserCCInfo.Rows[0]["ccInfoID"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoID"].ToString().Trim();

                //Set the First Name
                this.txtBFirstName.Text = dtUserCCInfo.Rows[0]["ccInfoDFirstName"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDFirstName"].ToString().Trim();
                //Set the Last Name
                this.txtBLastName.Text = dtUserCCInfo.Rows[0]["ccInfoDLastName"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDLastName"].ToString().Trim();

                GECEncryption objEnc = new GECEncryption();

                //Set the Business Address here
                this.txtBAddress1.Text = dtUserCCInfo.Rows[0]["ccInfoBAddress"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                this.txtBAddress2.Text = dtUserCCInfo.Rows[0]["ccInfoBAddress2"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBAddress2"].ToString().Trim();
                //Set the Business City here
                this.txtBCity.Text = dtUserCCInfo.Rows[0]["ccInfoBCity"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBCity"].ToString().Trim();
                //Set the Business Province name here
                try
                {
                    txtBProvince.Text = dtUserCCInfo.Rows[0]["ccInfoBProvince"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBProvince"].ToString().Trim();
                }
                catch (Exception ex)
                { }
                //Set the Business Postal Code here
                this.txtBPostalCode.Text = dtUserCCInfo.Rows[0]["ccInfoBPostalCode"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBPostalCode"].ToString().Trim();

                //Set the Payment Information here
                //Set the Credit Card Number here
                this.txtCCNumber.Text = dtUserCCInfo.Rows[0]["ccInfoNumber"] == DBNull.Value ? "" : "XXXXXXXXXXXX" + objEnc.DecryptData("colintastygochengnumber", dtUserCCInfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtUserCCInfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4); ;
                // this.hfCCN.Value = dtUserCCInfo.Rows[0]["ccInfoNumber"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoNumber"].ToString();
                //this.divlblCCN.Visible = false;
                //this.divtxtCCN.Visible = false;
                txtCVNumber.Text = "";

                //Get the Selected Month & Year ccInfoEdate
                //string strMMyy = objEnc.DecryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
                string strMMyy = dtUserCCInfo.Rows[0]["ccInfoEdate"] == DBNull.Value ? "" : objEnc.DecryptData("colintastygochengexpirydate", dtUserCCInfo.Rows[0]["ccInfoEdate"].ToString().Trim());

                ArrayList arrMMyy = new ArrayList();
                arrMMyy.AddRange(strMMyy.Split('-'));

                //Set the Month here
                this.ddlMonth.SelectedValue = arrMMyy.Count > 1 ? arrMMyy[0].ToString() : "01";
                //Set the Year here
                this.ddlYear.SelectedValue = arrMMyy.Count > 1 ? arrMMyy[1].ToString() : DateTime.Now.Year.ToString();
                //Set the CVN # here
                //this.txtCVNumber.Text = dtUserCCInfo.Rows[0]["ccInfoCCVNumber"] == DBNull.Value ? "" : objEnc.DecryptData("colintastygochengccv", dtUserCCInfo.Rows[0]["ccInfoCCVNumber"].ToString().Trim());
                this.hfCVNumber.Value = dtUserCCInfo.Rows[0]["ccInfoCCVNumber"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoCCVNumber"].ToString().Trim();
            }
        }
        catch (Exception ex)
        { }
    }

    private void getRemainedGainedBalByUserId(DataTable dtUser)
    {
        try
        {
            double dGainedCredit = 0;
            BLLMemberUsedGiftCards objBLLMemberUsedGiftCards = new BLLMemberUsedGiftCards();
            objBLLMemberUsedGiftCards.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());

            DataTable dtUseAbleFoodCredit = null;
            dtUseAbleFoodCredit = objBLLMemberUsedGiftCards.getUseableFoodCreditRefferalByUserID();

            if (dtUseAbleFoodCredit != null && dtUseAbleFoodCredit.Rows.Count > 0 && dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString().Trim() != "")
            {
                hfTastyCredit.Value = dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString();
                dGainedCredit = Convert.ToDouble(dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString());
            }

            BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
            objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
            DataTable dtComissionMaoney = objBLLAffiliatePartnerGained.getGetAffiliatePartnerGainedCreditsByUserID();
            if (dtComissionMaoney != null && dtComissionMaoney.Rows.Count > 0 && dtComissionMaoney.Rows[0][0].ToString().Trim() != "")
            {
                hfComissionMoney.Value = dtComissionMaoney.Rows[0][0].ToString();
                dGainedCredit += Convert.ToDouble(dtComissionMaoney.Rows[0][0].ToString());
            }

            this.hfRefAmt.Value = (dGainedCredit).ToString();

            this.lblRefBal.Text = "($" + (dGainedCredit).ToString("###.00") + " CAD)";

            this.lblRefBalanace.Text = (dGainedCredit).ToString("###.00");
            txtTastyCredit.MaxLength = lblRefBalanace.Text.Length;
            if (dGainedCredit > 0)
                divRefBal.Visible = true;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void GridDataBind(long UserID)
    {
        DataTable dtCCInfo = null;

        BLLUserCCInfo objCCInfo = new BLLUserCCInfo();
        objCCInfo.userId = UserID;
        dtCCInfo = objCCInfo.getUserCCInfoByUserID();
        if ((dtCCInfo != null) && (dtCCInfo.Rows.Count > 0))
        {
            gvCustomers.DataSource = dtCCInfo;
            gvCustomers.DataBind();

            //Show the Credit Card Grid here
            this.divDeliveryGridCCI.Visible = true;

            //Hide the Credit Card Personal Info here
            this.divBilling.Visible = false;
            divSaveCancelArea.Visible = false;
            //Set the Mode here
            this.hfMode.Value = "grid";

            //Set the Default Select Radion button
            foreach (GridViewRow gvr in gvCustomers.Rows)
            {
                RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");
                rdb.Checked = true;
                break;
            }
        }
        else
        {
            //Hide the Credit Card Grid here
            this.divDeliveryGridCCI.Visible = false;
            //Show the Credit Card Personal Info here
            this.divBilling.Visible = true;
            divSaveCancelArea.Visible = false;
        }
    }

    public string RenderOrderDetailHTML(string OrderID, double order_ccCreditUsed, double Order_tastyCreditUsed, double Order_dcomissionMoneyUsed)
    {
        DataTable dtOrdersInfo;
        DataTable dtProductCart = null;
        BLLDealOrders objOrders = new BLLDealOrders();
        GECEncryption objEnc = new GECEncryption();
        objOrders.dOrderID = Convert.ToInt64(OrderID);
        dtOrdersInfo = objOrders.getProductOrderDetailByOrderID();
        if (Session["dtProductCart"] != null)
        {
            dtProductCart = (DataTable)Session["dtProductCart"];
        }
        StringBuilder sb = new StringBuilder();
        if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
        {
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/NewLogo_2.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "User" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString()) + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Thank you for purchasing this amazing deal on Tazzling.com.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>The deal is on and we are all excited to get this deal!</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>How to Redeem This Deal:</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1. Login onto Tazzling.com</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2. Click My Tastygo for personal vouchers, My Gift for gift vouchers.</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3. Click print under each deal, or go green with our mobile apps.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>4. Redeem on location. Read the fine print before you do. Give the business a call before redeeming to make sure they are open and confirm your visit.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'><strong>If your purchase is an online deal or product:</strong></div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1. Read the fine print of the deal. Usually the product is directly ship to you.</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2. Some deals, you’ll have to redeem on a separate site and use the voucher code found in \"My Tastygo\"</div>");
            sb.Append("<div style='margin: 0px 0px 15px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3. If you have any questions, feel free to contact us <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a>.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your purchase receipt as follow:</div>");
            sb.Append("<div style='margin: 0px 60px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><strong>Tastygo Online Inc. Invoice</strong></div>");
            sb.Append("<table style='margin: 0px 0px 5px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            //sb.Append("<tr><td style='width: 130px;'><strong>Deal Title:</strong></td><td>");
            //sb.Append(dtOrdersInfo.Rows[0]["title"] == null ? "" : dtOrdersInfo.Rows[0]["title"].ToString());
            //sb.Append("</td></tr>");
            //sb.Append("<tr><td style='float: left; width: 130px;'><strong>Business Name:</strong></td>");
            //sb.Append("<td>");
            //sb.Append(dtOrdersInfo.Rows[0]["status"] == null ? "" : dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString());
            //sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 130px;'><strong>Status:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["status"] == null ? "" : dtOrdersInfo.Rows[0]["status"].ToString());
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 130px;'><strong>Date & Time:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["createdDate"] == null ? "" : dtOrdersInfo.Rows[0]["createdDate"].ToString());
            sb.Append("</td></tr>");
            if (dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString().Trim() != "0")
            {
                sb.Append("<tr><td style='float: left; width: 130px;'><strong>Card Number:</strong></td>");
                sb.Append("<td>");
                sb.Append(dtOrdersInfo.Rows[0]["ccInfoNumber"] == null ? "" : "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4));
                sb.Append("</td></tr>");
            }
            sb.Append("<tr><td style='float: left; width: 130px;'><strong>Billing Name:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString()));
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 130px;'><strong>Province/State:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["ccInfoBProvince"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBProvince"].ToString().Trim());
            sb.Append("</td></tr>");
            sb.Append("<tr><td style='float: left; width: 130px;'><strong>Billing City:</strong></td>");
            sb.Append("<td>");
            sb.Append(dtOrdersInfo.Rows[0]["ccInfoBCity"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBCity"].ToString().Trim());
            sb.Append("</td></tr></table>");
            sb.Append("<div style='padding: 15px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 16px; line-height: 1.4em; clear: both;'><strong>Deal Detail</strong></div>");
            sb.Append("<table style='margin: 0px 10px 10px 15px; width:100%; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; clear: both;'><tr><td style='float: left; width: 300px;'><strong>Deal Title</strong></td><td style='float: left; width: 100px;'><strong>Quantity</strong></td><td style='float: left; width: 100px;'><strong>Unit Price</strong></td><td style='float: left; width: 100px;'><strong>Total</strong></td></tr>");
            double dTempTotalOrder = 0;
            for (int i = 0; i < dtProductCart.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtProductCart.Rows[i]["Qty"].ToString().Trim()) > 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>");
                    sb.Append(dtProductCart.Rows[i]["title"].ToString());
                    /* if (dtProductCart.Rows[i]["isGift"].ToString().Trim() == "0")
                     {
                         sb.Append(dtProductCart.Rows[i]["title"].ToString());
                     }
                     else
                     {
                         sb.Append(dtProductCart.Rows[i]["title"].ToString() + " (Deal For Gift)");
                     }*/
                    sb.Append("</td><td style='float: left; width: 100px;'>");
                    sb.Append(dtProductCart.Rows[i]["Qty"].ToString());
                    sb.Append("</td><td style='float: left; width: 100px;'>$");
                    sb.Append(dtProductCart.Rows[i]["sellingPrice"].ToString());
                    dTempTotalOrder += Convert.ToDouble(Convert.ToDouble(dtProductCart.Rows[i]["sellingPrice"].ToString()) * Convert.ToDouble(dtProductCart.Rows[i]["Qty"].ToString()));
                    sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                    sb.Append(Convert.ToDouble(Convert.ToDouble(dtProductCart.Rows[i]["sellingPrice"].ToString()) * Convert.ToDouble(dtProductCart.Rows[i]["Qty"].ToString())).ToString("###.00"));
                    sb.Append(" CAD</strong></td></tr>");
                }
            }

            sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;</td></tr>");
            sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Total</strong></td><td style='float: left; width: 100px;'>");
            sb.Append("<strong>$" + dTempTotalOrder.ToString("###.00") + " CAD</strong></td></tr>");
            /* if (hfShippingEnabled.Value != "")
             {
                 sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Shipping & Tax</strong></td><td style='float: left; width: 100px;'>");
                 sb.Append("<strong>$" + lblShippingAndTax.Text.Trim() + " CAD</strong></td></tr>");
                 sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Grand Total</strong></td><td style='float: left; width: 100px;'>");
                 sb.Append("<strong>$" + Convert.ToDouble((Convert.ToDouble(lblShippingAndTax.Text.Trim()) + dTempTotalOrder)).ToString("###.00") + " CAD</strong></td></tr>");
             }*/
            if (Order_tastyCreditUsed > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px; padding-left:50px;'><strong>Charge From Tasty Credit</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + Order_tastyCreditUsed.ToString("###.00") + " CAD</strong></td></tr>");
            }
            if (order_ccCreditUsed > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px;padding-left:50px;'><strong>Charge From Credit Card</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + order_ccCreditUsed.ToString("###.00") + " CAD</strong></td></tr>");
            }
            if (Order_dcomissionMoneyUsed > 0)
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td colspan='2' style='float: left; width: 200px; padding-left:50px;'><strong>Charge From Commission</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + Order_dcomissionMoneyUsed.ToString("###.00") + " CAD</strong></td></tr>");
            }

            sb.Append("</table>");
            //sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;<td></tr>");
            // sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Balance</strong></td><td style='float: left; width: 100px;'><strong>$0 CAD</strong></td></tr></table>");
            //            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'>If you bought this deal as gift, please login to <a href='http://www.tazzling.com'>www.tazzling.com</a>. Click Member area, then Send gift. You will be able to print off the voucher as gift!<div>");

            sb.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>Don’t forget:</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>-	Refer friends, earn $10 when your friends orders with you! <a href='http://www.tazzling.com/member_referral.aspx' target='_blank'>Click here</a></div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>-	Buy the deal for a friend! If the deal is not on yet, tell them about it so we can all get it!</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>-	Click \"like\" on our <a href='http://www.facebook.com/tastygo' target='_blank'>facebook</a> or \"follow us\" on <a href='http://www.twitter.com/tastygovan' target='_blank'>twitter</a> to win awesome prizes</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Want us to negotiate a deal for you? <a href='http://www.tazzling.com/suggestBusiness.aspx' target='_blanck'>Suggest a business here</a></div>");
            sb.Append("<div style='margin: 0px 0px 15px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Tastygo provides 30 days buyer's protection. <a href='http://www.tazzling.com/faq.aspx' target='_blanck'>Click Here?</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");

            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");

        }
        return sb.ToString();
    }

    #region Affilate Stuff

    private bool GetAndSetAffInfoFromCookieInUserInfo(int iUserId)
    {
        bool bStatus = false;

        try
        {
            string strAffiliateRefId = "";
            string strAffiliateDate = "";

            HttpCookie cookieAffId = Request.Cookies["tastygo_affiliate_userID"];
            HttpCookie cookieAddDate = Request.Cookies["tastygo_affiliate_date"];

            //Remove the Cookie
            if ((cookieAffId != null) && (cookieAddDate != null))
            {
                if ((cookieAffId.Values.Count > 0) && (cookieAddDate.Values.Count > 0))
                {
                    //It should not be the same user
                    if (int.Parse(cookieAffId.Values[0].ToString()) != iUserId)
                    {
                        strAffiliateRefId = cookieAffId.Values[0].ToString();
                        strAffiliateDate = cookieAddDate.Values[0].ToString();



                        BLLUser objBLLUser = new BLLUser();
                        objBLLUser.userId = iUserId;
                        objBLLUser.affComID = int.Parse(strAffiliateRefId);
                        objBLLUser.affComEndDate = DateTime.Parse(strAffiliateDate);
                        objBLLUser.updateUserAffCommIDByUserId();

                        cookieAffId.Values.Clear();
                        cookieAddDate.Values.Clear();
                        cookieAffId.Expires = DateTime.Now;
                        cookieAddDate.Expires = DateTime.Now;

                        Response.Cookies.Add(cookieAffId);
                        Response.Cookies.Add(cookieAddDate);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    private bool AddAffiliateCommissionToReffer(int fromID, long OrderID, string strTotalAmount, string strAffComm, long lCommissionerId)
    {
        bool bStatus = false;

        try
        {
            BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();

            if (lCommissionerId != 0)
            {
                objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(lCommissionerId.ToString());
                objBLLAffiliatePartnerGained.GainedType = "Confirmed";

                //Add $1.85 % Amount of Total Amount into the User Account
                objBLLAffiliatePartnerGained.GainedAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.RemainAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));

                objBLLAffiliatePartnerGained.FromId = fromID;
                objBLLAffiliatePartnerGained.CreatedBy = fromID;

                objBLLAffiliatePartnerGained.OrderId = int.Parse(OrderID.ToString());

                //Set the Affiliate Commission
                objBLLAffiliatePartnerGained.AffCommPer = float.Parse(strAffComm);

                if (objBLLAffiliatePartnerGained.createAffiliatePartnerGained() == -1)
                {
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    private long GetAffiliateCommissionerIdByUserID(int iUserId)
    {
        long lCommissionerId = 0;

        try
        {
            BLLUser objBLLUser = new BLLUser();

            objBLLUser.userId = iUserId;

            DataTable dtUserInfo = objBLLUser.getUserByID();

            if ((dtUserInfo != null) && (dtUserInfo.Rows.Count > 0))
            {
                if ((dtUserInfo.Rows[0]["affComID"] != DBNull.Value) && (dtUserInfo.Rows[0]["affComID"].ToString().Trim().Length > 0))
                {
                    if ((dtUserInfo.Rows[0]["affComEndDate"] != DBNull.Value) && (DateTime.Parse(dtUserInfo.Rows[0]["affComEndDate"].ToString().Trim()) > DateTime.Now))
                        lCommissionerId = dtUserInfo.Rows[0]["affComID"] == DBNull.Value ? 0 : dtUserInfo.Rows[0]["affComID"].ToString().Trim().Length > 0 ? long.Parse(dtUserInfo.Rows[0]["affComID"].ToString().Trim()) : 0;
                }
            }
        }
        catch (Exception Ex)
        {
            string strException = Ex.Message;
        }

        return lCommissionerId;
    }

    private long GenerateId()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }

    private string gerrateAlpabit()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random();
        var result = new string(
            Enumerable.Repeat(chars, 3)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());
        return result.ToString();
    }

    private string GenerateStringID()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    private void deductTastyCreditFromUserAccount(float amount, int iUserID)
    {
        float remAmount = 0;
        float tastyCreditUsed = 0;
        float tastyComissionUsed = 0;
        try
        {
            if (hfTastyCredit.Value.ToString().Trim() != "" && float.Parse(hfTastyCredit.Value.ToString()) > 0)
            {
                BLLMemberUsedGiftCards objUseableCard = new BLLMemberUsedGiftCards();
                objUseableCard.createdBy = iUserID;
                DataTable dt = objUseableCard.getAllRefferalTastyCreditsByUserID();
                float dTastyCredit = float.Parse(hfTastyCredit.Value);
                float deductAmount = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dTastyCredit <= 0 || amount <= 0)
                        { break; }
                        remAmount = float.Parse(dt.Rows[i]["remainAmount"].ToString());
                        float fOrignalRemainAmount = float.Parse(dt.Rows[i]["remainAmount"].ToString());
                        if (deductAmount > 0 && remAmount > 0)
                        {
                            if (remAmount > deductAmount)
                            {
                                remAmount = remAmount - deductAmount;
                                deductAmount = 0;
                            }
                            else
                            {
                                deductAmount = deductAmount - remAmount;
                                remAmount = 0;
                            }
                        }
                        if (remAmount > 0)
                        {
                            if (remAmount >= amount)
                            {
                                objUseableCard.gainedId = Convert.ToInt64(dt.Rows[i]["gainedId"].ToString());
                                objUseableCard.modifiedBy = iUserID;
                                objUseableCard.remainAmount = remAmount - amount;
                                objUseableCard.updateRemainingUsableAmount();
                                tastyCreditUsed += amount;
                                amount = 0;
                                break;
                            }
                            else
                            {
                                objUseableCard.gainedId = Convert.ToInt64(dt.Rows[i]["gainedId"].ToString());
                                objUseableCard.modifiedBy = iUserID;
                                objUseableCard.remainAmount = fOrignalRemainAmount - remAmount;
                                objUseableCard.updateRemainingUsableAmount();
                                amount = amount - remAmount;
                                dTastyCredit = dTastyCredit - remAmount;
                                tastyCreditUsed += remAmount;
                            }
                        }
                        else if (remAmount < 0)
                        {
                            deductAmount = deductAmount - remAmount;
                        }
                    }
                }
            }
            if (hfComissionMoney.Value.ToString().Trim() != "" && float.Parse(hfComissionMoney.Value.ToString()) > 0 && amount > 0)
            {
                BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
                objBLLAffiliatePartnerGained.UserId = iUserID;
                DataTable dt = objBLLAffiliatePartnerGained.getGetAllAffiliatePartnerGainedByUserID();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        remAmount = float.Parse(dt.Rows[i]["remainAmount"].ToString());
                        if (remAmount > 0)
                        {
                            if (remAmount >= amount)
                            {
                                objBLLAffiliatePartnerGained.AffiliatePartnerId = Convert.ToInt32(dt.Rows[i]["affiliatePartnerId"].ToString());
                                objBLLAffiliatePartnerGained.ModifiedBy = iUserID;
                                objBLLAffiliatePartnerGained.RemainAmount = remAmount - amount;
                                objBLLAffiliatePartnerGained.updateAffiliateRemainingUsableAmount();
                                tastyComissionUsed += amount;
                                break;
                            }
                            else
                            {
                                objBLLAffiliatePartnerGained.AffiliatePartnerId = Convert.ToInt32(dt.Rows[i]["affiliatePartnerId"].ToString());
                                objBLLAffiliatePartnerGained.ModifiedBy = iUserID;
                                objBLLAffiliatePartnerGained.RemainAmount = 0;
                                objBLLAffiliatePartnerGained.updateAffiliateRemainingUsableAmount();
                                amount = amount - remAmount;
                                tastyComissionUsed += remAmount;
                            }
                        }
                    }
                }
            }

            hfTastyCredit.Value = tastyCreditUsed.ToString();
            hfComissionMoney.Value = tastyComissionUsed.ToString();

        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    protected void CheckChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvCustomers.Rows)
        {
            RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");
            if (rdb.Checked)
            {
                this.hfMode.Value = "grid";
            }
        }
    }

    protected void btnAddNewCCInfo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearBillingFields();
            divSaveCancelArea.Visible = true;
            divBilling.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnSave.ValidationGroup = "AddNew";
            RequiredFieldValidator1.ValidationGroup = "AddNew";
            RequiredFieldValidator2.ValidationGroup = "AddNew";
            RequiredFieldValidator3.ValidationGroup = "AddNew";
            RequiredFieldValidator4.ValidationGroup = "AddNew";
            RequiredFieldValidator5.ValidationGroup = "AddNew";
            RequiredFieldValidator6.ValidationGroup = "AddNew";
            RequiredFieldValidator7.ValidationGroup = "AddNew";
            RequiredFieldValidator8.ValidationGroup = "AddNew";
            cvCreditCard.ValidationGroup = "AddNew";
        }
        catch (Exception ex)
        { }
    }

    protected void clearBillingFields()
    {
        try
        {
            txtBAddress1.Text = "";
            txtBAddress2.Text = "";
            txtBCity.Text = "";
            txtBFirstName.Text = "";
            txtBLastName.Text = "";
            txtBPostalCode.Text = "";
            txtBProvince.Text = "";
            txtCCNumber.Text = "";
            txtCVNumber.Text = "";
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DateTime dtUserDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
            if (dtUserDate < DateTime.Now)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please confirm your expirary date.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                return;
            }


            if (txtBAddress1.Text.Trim() == "" || txtBCity.Text.Trim() == "" || txtBFirstName.Text.Trim() == ""
                || txtBLastName.Text.Trim() == "" || txtBPostalCode.Text.Trim() == "" || txtBProvince.Text.Trim() == "" || txtCCNumber.Text.Trim() == ""
                || txtCVNumber.Text.Trim() == "")
            {

                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter all required fields.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                return;
            }

            GECEncryption objEnc = new GECEncryption();
            BLLUserCCInfo objCC = new BLLUserCCInfo();
            objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBAddress1.Text.Trim());
            objCC.ccInfoBAddress2 = HtmlRemoval.StripTagsRegexCompiled(txtBAddress2.Text.Trim());
            objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtBCity.Text.Trim());
            objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtBPostalCode.Text.Trim());
            objCC.ccInfoBProvince = txtBProvince.Text.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            else
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Your session has been expired. Please Login to proceed checkout.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                return;
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim()));
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            //objCC.ccInfoNumber = this.hfCCN.Value.Trim();
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()));
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim() + " " + txtBLastName.Text.Trim()));
            objCC.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());

            objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim());

            objCC.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(txtBLastName.Text.Trim());
            objCC.createUserCCInfo();

            //Hide the Credit Card Personal Info here
            divBilling.Visible = false;
            divSaveCancelArea.Visible = false;
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
            }
            clearBillingFields();
            string jScript2;
            jScript2 = "<script>";
            jScript2 += "MessegeArea('Credit Card Info has been saved.','success');";
            jScript2 += "</script>";
            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript2, false);
        }
        catch (Exception ex)
        { }
    }

    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DateTime dtUserDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
            if (dtUserDate < DateTime.Now)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please confirm your expirary date.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                return;
            }


            if (txtBAddress1.Text.Trim() == "" || txtBCity.Text.Trim() == "" || txtBFirstName.Text.Trim() == ""
                || txtBLastName.Text.Trim() == "" || txtBPostalCode.Text.Trim() == "" || txtBProvince.Text.Trim() == "" || txtCCNumber.Text.Trim() == ""
                || txtCVNumber.Text.Trim() == "")
            {

                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Please enter all required fields.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                return;
            }

            GECEncryption objEnc = new GECEncryption();
            BLLUserCCInfo objCC = new BLLUserCCInfo();
            objCC.ccInfoID = Convert.ToInt64(this.hfCCInfoIdEdit.Value.ToString());
            objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBAddress1.Text.Trim());
            objCC.ccInfoBAddress2 = HtmlRemoval.StripTagsRegexCompiled(txtBAddress2.Text.Trim());
            objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtBCity.Text.Trim());
            objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtBPostalCode.Text.Trim());
            objCC.ccInfoBProvince = txtBProvince.Text.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            else
            {

                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Your session has been expired. Please Login to proceed checkout.','Error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                return;
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim()));
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            //objCC.ccInfoNumber = this.hfCCN.Value.Trim();
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()));
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim() + " " + txtBLastName.Text.Trim()));
            objCC.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());

            objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim());

            objCC.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(txtBLastName.Text.Trim());
            objCC.updateUserCCInfoByID();

            //Hide the Credit Card Personal Info here
            divBilling.Visible = false;
            divSaveCancelArea.Visible = false;
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
            }
            string jScript2;
            jScript2 = "<script>";
            jScript2 += "MessegeArea('Credit Card Info has been updated.','success');";
            jScript2 += "</script>";
            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript2, false);
        }
        catch (Exception ex)
        { }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        clearBillingFields();
        divSaveCancelArea.Visible = false;
        divBilling.Visible = false;
    }

    private void shippingCheck()
    {
        if (cbShippingSame.Checked)
        {
            strhideShippingDiv = "none";
            RequiredFieldValidator10.Enabled = false;
            RequiredFieldValidator12.Enabled = false;
            RequiredFieldValidator13.Enabled = false;
            RequiredFieldValidator14.Enabled = false;
        }
    }

    protected void btnApply_Click(object sender, ImageClickEventArgs e)
    {
        resetAmounts();
        txtTastyCredit.MaxLength = lblRefBalanace.Text.Length;
    }

    private void resetAmounts()
    {
        try
        {
            if (Session["dtProductCart"] != null)
            {
                DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                int Qty = 0;
                if (dtProductCart != null && dtProductCart.Rows.Count > 0)
                {
                    int isVoucher = 0;
                    object sumIsVoucher;
                    sumIsVoucher = dtProductCart.Compute("Sum(isVoucherProduct)", "isVoucherProduct > 0");
                    int.TryParse(sumIsVoucher.ToString(), out isVoucher);
                    if (isVoucher == dtProductCart.Rows.Count)
                    {
                        divShippingAddress.Visible = false;
                    }


                    if (dtProductCart != null)
                    {
                        object sumObject;
                        sumObject = dtProductCart.Compute("Sum(Qty)", "Qty > 0");
                        int.TryParse(sumObject.ToString(), out Qty);
                        hfOrderQty.Value = Qty.ToString();
                    }
                    if (Qty == 0)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Your cart is empty.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                        Response.Redirect("cart.aspx");
                        /*shippingCheck();
                        return;*/
                    }
                    double shippingAmount = 0;
                    double totalDealPrice = 0;
                    bool shippingAndTax = false;
                    for (int i = 0; i < dtProductCart.Rows.Count; i++)
                    {
                        //if (dtProductCart.Rows[i]["shippingAndTax"].ToString().Trim().ToLower() == "true")
                        //{
                        //    shippingAmount += Convert.ToDouble(dtProductCart.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtProductCart.Rows[i]["Qty"].ToString().Trim());
                        //    shippingAndTax = true;
                        //}
                        totalDealPrice += Convert.ToDouble(dtProductCart.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtProductCart.Rows[i]["Qty"].ToString().Trim());
                    }
                    totalDealPrice += shippingAmount;
                    //lblShippingAndTax.Text = Math.Round(shippingAmount, 2, MidpointRounding.AwayFromZero).ToString();
                    lblGrandTotal.Text = Math.Round(totalDealPrice, 2, MidpointRounding.AwayFromZero).ToString();
                    hfGrandTotal.Value = Math.Round(totalDealPrice, 2, MidpointRounding.AwayFromZero).ToString();
                    /* if (shippingAndTax)
                     {
                         hfShippingEnabled.Value = "true";
                         hfShippingAndTax.Value = shippingAmount.ToString();
                         divShipping.Visible = true;
                         divShippingAddress.Visible = true;
                     }
                     else
                     {
                         hfShippingEnabled.Value = "";
                         hfShippingAndTax.Value = shippingAmount.ToString();
                         divShippingAddress.Visible = false;
                         divShipping.Visible = false;
                     }*/
                    double dBalance = 0;
                    double dUsed = 0;
                    try
                    {
                        if (lblRefBalanace.Text.Trim() != "")
                        {
                            dBalance = Convert.ToDouble(lblRefBalanace.Text.Trim());
                        }
                        if (txtTastyCredit.Text.Trim() != "")
                        {
                            dUsed = Convert.ToDouble(txtTastyCredit.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    { }
                    if (dBalance < dUsed)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Place enter the quantity of your purchase.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                        return;
                    }
                    // double dShippingAndTax = Convert.ToDouble(lblShippingAndTax.Text.Trim());
                    double dDealTotal = 0;
                    dDealTotal = Convert.ToDouble(hfGrandTotal.Value);
                    double dRemail = dDealTotal - dUsed;

                    if (dUsed > dDealTotal)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Place enter the quantity of your purchase.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                        return;
                    }
                    hfPayFull.Value = dUsed.ToString();
                    lblGrandTotal.Text = Convert.ToDecimal(dRemail).ToString("###.00");
                    if (dRemail == 0)
                    {
                        this.hfMode.Value = "new";
                        cbShippingSame.Checked = false;
                        cbShippingSame.Visible = false;
                        lblSameAsBilling.Visible = false;
                        RequiredFieldValidator8.Enabled = false;
                        RequiredFieldValidator7.Enabled = false;
                        divBilling.Visible = false;
                        this.divDeliveryGridCCI.Visible = false;
                        divSaveCancelArea.Visible = false;
                    }
                    else if (gvCustomers.Rows.Count == 0)
                    {
                        this.hfMode.Value = "new";
                        divBilling.Visible = true;
                        //Hide the Credit Card Grid here
                        this.divDeliveryGridCCI.Visible = false;
                        this.divSaveCancelArea.Visible = false;
                    }
                    else
                    {
                        divBilling.Visible = false;
                        this.divDeliveryGridCCI.Visible = true;
                    }
                    shippingCheck();
                }
            }
        }
        catch (Exception ex)
        { }
    }

    private bool ChkDealOrderFirstOrderOfUser(int iUserId)
    {
        bool bStatus = false;

        try
        {
            BLLDealOrders objBLLDealOrders = new BLLDealOrders();

            objBLLDealOrders.userId = iUserId;

            DataTable dtDealOrderCount = objBLLDealOrders.getDealOrdersCountByUserId();

            if ((dtDealOrderCount != null) && (dtDealOrderCount.Rows.Count > 0))
            {
                if (int.Parse(dtDealOrderCount.Rows[0][0].ToString().Trim()) == 1)
                {
                    //Means that its the first Order
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return bStatus;
    }

    private long GetRefferalIdByUserID(int iUserId)
    {
        long lRefId = 0;

        try
        {
            BLLUser objBLLUser = new BLLUser();

            objBLLUser.userId = iUserId;

            DataTable dtUserInfo = objBLLUser.getUserByID();

            if ((dtUserInfo != null) && (dtUserInfo.Rows.Count > 0))
            {
                if (dtUserInfo.Rows[0]["affComEndDate"] != null)
                {
                    try
                    {
                        DateTime dtAffiliateReqDate = Convert.ToDateTime(dtUserInfo.Rows[0]["affComEndDate"].ToString());
                        if (dtAffiliateReqDate > DateTime.Now)
                        {
                            lRefId = dtUserInfo.Rows[0]["affComID"] == DBNull.Value ? 0 : dtUserInfo.Rows[0]["affComID"].ToString().Trim().Length > 0 ? long.Parse(dtUserInfo.Rows[0]["affComID"].ToString().Trim()) : 0;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        catch (Exception Ex)
        {
            string strException = Ex.Message;
        }

        return lRefId;
    }

    private bool AddTastyGoCreditToReffer(int fromID, long OrderID, long lRefId)
    {
        bool bStatus = false;

        try
        {

            BLLMemberUsedGiftCards objUsedCard = new BLLMemberUsedGiftCards();

            //Add $10 Credit into the User Account
            objUsedCard.remainAmount = float.Parse("10.00");

            if (lRefId != 0)
            {

                //Get the Refferal ID By UserID of the Order
                objUsedCard.createdBy = lRefId;

                objUsedCard.gainedAmount = float.Parse("10.00");

                //If user places the first order then he will get the $10
                objUsedCard.fromId = fromID;

                objUsedCard.targetDate = DateTime.Now.AddMonths(6);

                objUsedCard.currencyCode = "CAD";

                objUsedCard.gainedType = "Refferal";

                objUsedCard.orderId = OrderID;

                if (objUsedCard.createMemberUseableGiftCard() == -1)
                {
                    bStatus = true;
                    SendEmail(lRefId.ToString(), "10");
                }

            }

        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    protected void SendEmail(string strUserID, string strAmount)
    {
        try
        {

            BLLUser obj = new BLLUser();
            obj.userId = Convert.ToInt32(strUserID);
            DataTable dtUser = obj.getUserByID();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                System.Text.StringBuilder mailBody = new System.Text.StringBuilder();
                string toAddress = dtUser.Rows[0]["userName"].ToString().Trim();
                string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string Subject = "You have received $" + strAmount + " tasty credits from Tazzling.Com";

                mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
                mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                mailBody.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
                mailBody.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                mailBody.Append("<strong>Dear " + dtUser.Rows[0]["firstname"].ToString().Trim() + " " + dtUser.Rows[0]["lastname"].ToString().Trim() + ",</strong></div>");
                mailBody.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received $" + strAmount + " tasty credit from Tazzling.com.</strong></div>");
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Use them today on Tazzling.Com towards our amazing deals!</div>");
                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>How to apply my Tasty Credits?</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>1.	Login Tazzling.com</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>2.	Choose the deal you want to purchase, on the checkout page,</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>3.	Enter the amount of credits you want to apply</div>");
                mailBody.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>4.	Complete the checkout!</div>");


                mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");
                mailBody.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
                mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
                mailBody.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
                mailBody.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
                mailBody.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
                mailBody.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
                Misc.SendEmail(toAddress, "", "", fromAddress, Subject, mailBody.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnContinue_Click(object sender, ImageClickEventArgs e)
    {
        bool orderInProcess = false;
        try
        {

            if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Your session has been expired. Please Login to proceed checkout.','Error');";
                jScript += "</script>";
                orderInProcess = false;
                ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                shippingCheck();
                return;
            }

            if (Session["dtProductCart"] == null)
            {
                Response.Redirect("default.aspx");
            }

            resetAmounts();
            BLLRequestChecker objRequestChecker = new BLLRequestChecker();
            objRequestChecker.requestIP = Request.UserHostAddress.ToString();
            DataTable dt = objRequestChecker.CheckIPIsBlocked();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Result"].ToString().Trim() == "True")
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Your ip has been temporary blocked for 24 hours due to multiple attempts, please contact us at <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a> for further assistance.','Error');";
                    jScript += "</script>";
                    orderInProcess = false;
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                    shippingCheck();
                    return;
                }
            }

            if (!orderInProcess)
            {
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                /*if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
                {
                    if (!cbAgree.Checked)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Accept terms & conditions.";
                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }
                    if (!SignupSuccessfull())
                    {
                        shippingCheck();
                        return;
                    }
                }*/
                if (ViewState["userId"] == null || ViewState["userId"].ToString().Trim() == "")
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Your session has been expired. Please Login to proceed checkout.','Error');";
                    jScript += "</script>";
                    orderInProcess = false;
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                    shippingCheck();
                    return;
                }
                orderInProcess = true;

                if (divRefBal.Visible)
                {
                    double dBalanceTemp = 0;
                    double dUsedTemp = 0;
                    DataTable dtUserTemp = null;
                    if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                    {
                        if (Session["member"] != null)
                        {
                            dtUserTemp = (DataTable)Session["member"];
                        }
                        else if (Session["restaurant"] != null)
                        {
                            dtUserTemp = (DataTable)Session["restaurant"];
                        }
                        else if (Session["sale"] != null)
                        {
                            dtUserTemp = (DataTable)Session["sale"];
                        }
                        else if (Session["user"] != null)
                        {
                            dtUserTemp = (DataTable)Session["user"];
                        }
                        getRemainedGainedBalByUserId(dtUserTemp);
                    }
                    try
                    {
                        if (lblRefBalanace.Text.Trim() != "")
                        {
                            dBalanceTemp = Convert.ToDouble(lblRefBalanace.Text.Trim());
                        }
                        if (txtTastyCredit.Text.Trim() != "")
                        {
                            dUsedTemp = Convert.ToDouble(txtTastyCredit.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    { }
                    if (dBalanceTemp < dUsedTemp)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Place enter the quantity of your purchase.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }


                    double dDealTotalTemp = 0;
                    dDealTotalTemp = Convert.ToDouble(hfGrandTotal.Value.Trim());
                    double dRemail = dDealTotalTemp - dUsedTemp;

                    if (dUsedTemp > dDealTotalTemp)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Place enter the quantity of your purchase.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }
                    txtTastyCredit.MaxLength = lblRefBalanace.Text.Length;
                }
                if (this.hfMode.Value.Trim() == "new" && (HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()) != ""))
                {
                    DateTime dtUserDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
                    if (dtUserDate < DateTime.Now)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Please confirm your expirary date.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);

                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }
                }
                else if (this.hfMode.Value.Trim() == "new" && (HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()) == ""))
                {

                }
                else if (divBilling.Visible)
                {
                    try
                    {

                        DateTime dtUserDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
                        if (dtUserDate < DateTime.Now)
                        {
                            string jScript;
                            jScript = "<script>";
                            jScript += "MessegeArea('Please confirm your expirary date.','Error');";
                            jScript += "</script>";
                            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);

                            orderInProcess = false;
                            shippingCheck();
                            return;
                        }


                        GECEncryption objEncTemp = new GECEncryption();

                        BLLUserCCInfo objCC = new BLLUserCCInfo();
                        objCC.ccInfoID = Convert.ToInt64(this.hfCCInfoIdEdit.Value.ToString());
                        objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBAddress1.Text.Trim());
                        objCC.ccInfoBAddress2 = HtmlRemoval.StripTagsRegexCompiled(txtBAddress2.Text.Trim());
                        objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtBCity.Text.Trim());
                        objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtBPostalCode.Text.Trim());
                        objCC.ccInfoBProvince = txtBProvince.Text.Trim();
                        if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
                        {
                            objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                            objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
                        }
                        else
                        {
                            string jScript;
                            jScript = "<script>";
                            jScript += "MessegeArea('Your session has been expired. Please Login to proceed checkout.','Error');";
                            jScript += "</script>";
                            ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                            orderInProcess = false;
                            shippingCheck();
                            return;
                        }
                        objCC.ccInfoCCVNumber = objEncTemp.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim()));
                        objCC.ccInfoEdate = objEncTemp.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
                        //objCC.ccInfoNumber = this.hfCCN.Value.Trim();
                        objCC.ccInfoNumber = objEncTemp.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()));
                        objCC.ccInfoBName = objEncTemp.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim() + " " + txtBLastName.Text.Trim()));
                        objCC.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());
                        objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim());
                        objCC.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(txtBLastName.Text.Trim());
                        objCC.updateUserCCInfoByID();
                    }
                    catch (Exception ex)
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.','Error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }
                }


                if (ViewState["userId"] == null || ViewState["userId"].ToString().Trim() == "")
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Your session has been expired. Please Login to proceed checkout.','Error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(btnSave, typeof(ImageButton), "Javascript", jScript, false);
                    orderInProcess = false;
                    shippingCheck();
                    return;
                }
                else
                {
                    try
                    {
                        /*objUser.firstName = HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim());
                        objUser.lastName = HtmlRemoval.StripTagsRegexCompiled(txtLastName.Text.Trim());
                        objUser.userId = Convert.ToInt32(ViewState["userId"].ToString());
                        objUser.updateUserFirstAndLastNameByID();*/
                    }
                    catch (Exception ex)
                    { }
                }
                GECEncryption objEnc = new GECEncryption();

                long createdID = 0;
                long shippingInfoID = 0;

                BLLDealOrders objOrder = new BLLDealOrders();
                objOrder.ccCreditUsed = float.Parse(lblGrandTotal.Text.Trim());
                if (this.hfMode.Value.Trim() == "new")
                {
                    if (objOrder.ccCreditUsed > 0)
                    {
                        try
                        {
                            #region NetBack Payment Service Implementation
                            //Prepare the call to the Credit Card Web Service
                            CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
                            MerchantAccountV1 merchantAccount = new MerchantAccountV1();
                            merchantAccount.accountNum = ConfigurationManager.AppSettings["netBankAccountNum"].ToString().Trim();
                            merchantAccount.storeID = ConfigurationManager.AppSettings["netBankStoreID"].ToString().Trim();
                            merchantAccount.storePwd = ConfigurationManager.AppSettings["netBankStorePwd"].ToString().Trim();
                            ccAuthRequest.merchantAccount = merchantAccount;
                            ccAuthRequest.merchantRefNum = Guid.NewGuid().ToString();
                            ccAuthRequest.amount = objOrder.ccCreditUsed.ToString("###.00");



                            CardV1 card = new CardV1();
                            card.cardNum = txtCCNumber.Text.Trim();

                            CardExpiryV1 cardExpiry = new CardExpiryV1();
                            cardExpiry.month = Convert.ToInt32(ddlMonth.SelectedValue.ToString().Trim());
                            cardExpiry.year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                            card.cardExpiry = cardExpiry;
                            //card.cardType = CardTypeV1.VI;

                            card.cardTypeSpecified = false;
                            card.cvdIndicator = 1;
                            card.cvdIndicatorSpecified = true;
                            card.cvd = HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim());
                            ccAuthRequest.card = card;

                            BillingDetailsV1 billingDetails = new BillingDetailsV1();
                            billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
                            billingDetails.cardPayMethodSpecified = true;
                            billingDetails.firstName = txtBFirstName.Text.Trim();
                            billingDetails.lastName = txtBLastName.Text.Trim();
                            billingDetails.street = txtBAddress1.Text.Trim();
                            billingDetails.city = txtBCity.Text.Trim();
                            billingDetails.Item = (object)txtBProvince.Text.Trim(); // California
                            if (ddlBillingCountry.SelectedIndex == 0)
                            {
                                billingDetails.country = CountryV1.CA; // United States
                            }
                            else
                            {
                                billingDetails.country = CountryV1.US;
                            }
                            billingDetails.countrySpecified = true;
                            billingDetails.zip = txtBPostalCode.Text.Trim();
                            billingDetails.phone = "";
                            billingDetails.email = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());
                            ccAuthRequest.billingDetails = billingDetails;
                            ccAuthRequest.customerIP = Request.UserHostAddress;
                            ccAuthRequest.productType = ProductTypeV1.M;

                            ccAuthRequest.productTypeSpecified = true;
                            // Perform the Web Services call for the purchase
                            CreditCardServiceV1 ccService = new CreditCardServiceV1();
                            CCTxnResponseV1 ccTxnResponse = ccService.ccPurchase(ccAuthRequest);
                            if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
                            {
                                objOrder.psgTranNo = ccTxnResponse.confirmationNumber;
                                objOrder.psgError = "Optimal : " + ccTxnResponse.code + " : " + ccTxnResponse.description + " : " + ccAuthRequest.merchantRefNum;
                            }
                            else
                            {
                                checkIPAddress();
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = ccTxnResponse.code + " : " + ccTxnResponse.description;
                                orderInProcess = false;
                                shippingCheck();
                                return;
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            checkIPAddress();
                            objOrder.psgError = ex.Message;
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = ex.Message + " There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.<br>" + ex.Message;
                            orderInProcess = false;
                            shippingCheck();
                            return;
                        }

                    }
                    objCCInfo = new BLLUserCCInfo();
                    objCCInfo.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBAddress1.Text.Trim());
                    objCCInfo.ccInfoBAddress2 = HtmlRemoval.StripTagsRegexCompiled(txtBAddress2.Text.Trim());
                    objCCInfo.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtBCity.Text.Trim());
                    objCCInfo.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtBPostalCode.Text.Trim());
                    objCCInfo.ccInfoBProvince = txtBProvince.Text.Trim();
                    if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
                    {
                        objCCInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                        objCCInfo.userId = Convert.ToInt64(ViewState["userId"].ToString());
                    }
                    if (txtCVNumber.Text.Trim() == "")
                    {
                        objCCInfo.ccInfoCCVNumber = "0";
                    }
                    else
                    {
                        objCCInfo.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim()));
                    }

                    objCCInfo.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
                    if (txtCCNumber.Text.Trim() == "")
                    {
                        objCCInfo.ccInfoNumber = "0";
                    }
                    else
                    {
                        objCCInfo.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()));
                    }

                    objCCInfo.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim() + " " + txtBLastName.Text.Trim()));
                    objCCInfo.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(ViewState["userName"].ToString().Trim());
                    objCCInfo.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(txtBFirstName.Text.Trim());
                    objCCInfo.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(txtBLastName.Text.Trim());
                    createdID = objCCInfo.createUserCCInfo();

                    if (cbShippingSame.Checked)
                    {
                        BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                        objShippingInfo.Address = txtBAddress1.Text.Trim();
                        objShippingInfo.Address2 = txtBAddress2.Text.Trim();
                        objShippingInfo.City = txtBCity.Text.Trim();
                        objShippingInfo.State = txtBProvince.Text.Trim();
                        objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                        objShippingInfo.Name = txtSFirstName.Text.Trim() + " " + txtSLastName.Text.Trim();
                        objShippingInfo.Telephone = txtSCellNumber.Text.Trim();
                        objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                        objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                        objShippingInfo.ZipCode = txtBPostalCode.Text.Trim();
                        objShippingInfo.shippingNote = txtSNote.Text.Trim();
                        shippingInfoID = objShippingInfo.createUserShippingInfo();
                    }
                    else
                    {
                        BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                        objShippingInfo.Address = txtSAddress1.Text.Trim();
                        objShippingInfo.Address2 = txtSAddress2.Text.Trim();
                        objShippingInfo.City = txtSCity.Text.Trim();
                        objShippingInfo.State = txtSProvince.Text.Trim();
                        objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                        objShippingInfo.Name = txtSFirstName.Text.Trim() + " " + txtSLastName.Text.Trim();
                        objShippingInfo.Telephone = txtSCellNumber.Text.Trim();
                        objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                        objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                        objShippingInfo.ZipCode = txtSPostalCode.Text.Trim();
                        objShippingInfo.shippingNote = txtSNote.Text.Trim();
                        shippingInfoID = objShippingInfo.createUserShippingInfo();
                    }
                }
                else if (this.hfMode.Value.Trim() == "grid")
                {
                    DataTable dtCCinfo = null;
                    if (objOrder.ccCreditUsed > 0)
                    {
                        //Get the  CCInfoID
                        foreach (GridViewRow gvr in gvCustomers.Rows)
                        {
                            RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");

                            //Button myButton = (Button)gvr.FindControl("b1");
                            if (rdb.Checked)
                            {
                                HiddenField hfccInfoID = (HiddenField)gvr.FindControl("hfccInfoID");
                                createdID = long.Parse(hfccInfoID.Value.Trim());
                            }
                        }

                        objCCInfo = new BLLUserCCInfo();
                        objCCInfo.ccInfoID = createdID;
                        dtCCinfo = objCCInfo.getUserCCInfoByID();
                        if (dtCCinfo != null && dtCCinfo.Rows.Count > 0)
                        {

                            string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
                            if (strDate.Length == 2)
                            {
                                DateTime dtUserDate = new DateTime(Convert.ToInt32(strDate[1].ToString()), Convert.ToInt32(strDate[0].ToString()), 1).AddMonths(1).AddDays(-1);
                                if (dtUserDate < DateTime.Now)
                                {
                                    lblErrorMessage.Visible = true;
                                    lblErrorMessage.Text = "Please confirm your expirary date.";
                                    orderInProcess = false;
                                    shippingCheck();
                                    return;
                                }
                            }
                            try
                            {
                                #region NetBack Payment Service Implementation

                                //Prepare the call to the Credit Card Web Service
                                CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
                                MerchantAccountV1 merchantAccount = new MerchantAccountV1();
                                merchantAccount.accountNum = ConfigurationManager.AppSettings["netBankAccountNum"].ToString().Trim();
                                merchantAccount.storeID = ConfigurationManager.AppSettings["netBankStoreID"].ToString().Trim();
                                merchantAccount.storePwd = ConfigurationManager.AppSettings["netBankStorePwd"].ToString().Trim();
                                ccAuthRequest.merchantAccount = merchantAccount;
                                ccAuthRequest.merchantRefNum = Guid.NewGuid().ToString();
                                ccAuthRequest.amount = objOrder.ccCreditUsed.ToString("###.00");

                                CardV1 card = new CardV1();
                                card.cardNum = objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString());
                                CardExpiryV1 cardExpiry = new CardExpiryV1();
                                if (strDate.Length == 2)
                                {
                                    cardExpiry.month = Convert.ToInt32(strDate[0].ToString());
                                    cardExpiry.year = Convert.ToInt32(strDate[1].ToString());
                                }
                                else
                                {
                                    cardExpiry.month = DateTime.Now.Month;
                                    cardExpiry.year = DateTime.Now.Year;
                                }
                                card.cardExpiry = cardExpiry;
                                //card.cardType = CardTypeV1.VI;
                                card.cardTypeSpecified = false;
                                card.cvdIndicator = 1;
                                card.cvdIndicatorSpecified = true;
                                card.cvd = objEnc.DecryptData("colintastygochengccv", dtCCinfo.Rows[0]["ccInfoCCVNumber"].ToString());
                                ccAuthRequest.card = card;

                                BillingDetailsV1 billingDetails = new BillingDetailsV1();
                                billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
                                billingDetails.cardPayMethodSpecified = true;
                                billingDetails.firstName = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
                                billingDetails.lastName = "";
                                billingDetails.street = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                                billingDetails.city = dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
                                billingDetails.Item = (object)dtCCinfo.Rows[0]["ccInfoBProvince"].ToString(); // California
                                billingDetails.country = CountryV1.CA; // United States
                                billingDetails.countrySpecified = true;
                                billingDetails.zip = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
                                billingDetails.phone = "";
                                billingDetails.email = dtCCinfo.Rows[0]["ccInfoDEmail"].ToString();
                                ccAuthRequest.billingDetails = billingDetails;
                                ccAuthRequest.customerIP = Request.UserHostAddress;
                                ccAuthRequest.productType = ProductTypeV1.M;
                                ccAuthRequest.dupeCheck = true;
                                ccAuthRequest.productTypeSpecified = true;
                                // Perform the Web Services call for the purchase
                                CreditCardServiceV1 ccService = new CreditCardServiceV1();
                                CCTxnResponseV1 ccTxnResponse = ccService.ccPurchase(ccAuthRequest);
                                if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
                                {
                                    objOrder.psgTranNo = ccTxnResponse.confirmationNumber;
                                    objOrder.psgError = "Optimal : " + ccTxnResponse.code + " : " + ccTxnResponse.description + " : " + ccAuthRequest.merchantRefNum;
                                }
                                else
                                {
                                    checkIPAddress();
                                    lblErrorMessage.Visible = true;
                                    lblErrorMessage.Text = ccTxnResponse.code + " : " + ccTxnResponse.description;
                                    orderInProcess = false;
                                    shippingCheck();
                                    return;
                                }
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                checkIPAddress();
                                objOrder.psgError = ex.Message;
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = "Time out on payment gateway, please try again in 1 minute.";
                                orderInProcess = false;
                                shippingCheck();
                                return;
                            }
                        }
                        else
                        {
                            checkIPAddress();
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                            orderInProcess = false;
                            shippingCheck();
                            return;
                        }

                        if (cbShippingSame.Checked)
                        {
                            try
                            {
                                BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                objShippingInfo.Address = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                                objShippingInfo.Address2 = dtCCinfo.Rows[0]["ccInfoBAddress2"].ToString().Trim();// txtSAddress2.Text.Trim();
                                objShippingInfo.City = dtCCinfo.Rows[0]["ccInfoBCity"].ToString().Trim();
                                objShippingInfo.State = dtCCinfo.Rows[0]["ccInfoBProvince"].ToString().Trim();
                                objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.Name = txtSFirstName.Text.Trim() + " " + txtSLastName.Text.Trim();
                                objShippingInfo.Telephone = txtSCellNumber.Text.Trim();
                                objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.ZipCode = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString().Trim();
                                objShippingInfo.shippingNote = txtSNote.Text.Trim();
                                shippingInfoID = objShippingInfo.createUserShippingInfo();

                            }
                            catch (Exception ex)
                            {
                                BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                objShippingInfo.Address = txtSAddress1.Text.Trim();
                                objShippingInfo.Address2 = txtSAddress2.Text.Trim();
                                objShippingInfo.City = txtSCity.Text.Trim();
                                objShippingInfo.State = txtSProvince.Text.Trim();
                                objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.Name = txtSFirstName.Text.Trim() + " " + txtSLastName.Text.Trim();
                                objShippingInfo.Telephone = txtSCellNumber.Text.Trim();
                                objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.ZipCode = txtSPostalCode.Text.Trim();
                                objShippingInfo.shippingNote = txtSNote.Text.Trim();
                                shippingInfoID = objShippingInfo.createUserShippingInfo();
                            }
                        }
                        else
                        {
                            BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                            objShippingInfo.Address = txtSAddress1.Text.Trim();
                            objShippingInfo.Address2 = txtSAddress2.Text.Trim();
                            objShippingInfo.City = txtSCity.Text.Trim();
                            objShippingInfo.State = txtSProvince.Text.Trim();
                            objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.Name = txtSFirstName.Text.Trim() + " " + txtSLastName.Text.Trim();
                            objShippingInfo.Telephone = txtSCellNumber.Text.Trim();
                            objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                            objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.ZipCode = txtSPostalCode.Text.Trim();
                            objShippingInfo.shippingNote = txtSNote.Text.Trim();
                            shippingInfoID = objShippingInfo.createUserShippingInfo();
                        }
                    }
                }
                if (objOrder.ccCreditUsed > 0 && objOrder.psgTranNo.Trim() == "")
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                    orderInProcess = false;
                    shippingCheck();
                    return;
                }
                try
                {
                    BLLRequestChecker objCheck = new BLLRequestChecker();
                    objCheck.requestIP = Request.UserHostAddress.ToString();
                    objCheck.DeleteFromRequestChecker();
                }
                catch (Exception ex)
                {

                }


                objOrder.ccInfoID = createdID;
                objOrder.shippingInfoId = shippingInfoID;
                //Deduct the Tasty Credit from the User Account
                double dtastyCreditUsed = 0;
                double dcomissionMoneyUsed = 0;
                double Order_tastyCreditUsed = 0;
                double Order_dcomissionMoneyUsed = 0;
                if ((this.hfPayFull.Value.Trim().Length > 0 ? float.Parse(this.hfPayFull.Value.Trim()) : 0) > 0)
                {
                    deductTastyCreditFromUserAccount(float.Parse(this.hfPayFull.Value.Trim()), int.Parse(ViewState["userId"].ToString()));
                    dtastyCreditUsed = float.Parse(hfTastyCredit.Value);
                    dcomissionMoneyUsed = float.Parse(hfComissionMoney.Value);
                    Order_tastyCreditUsed = dtastyCreditUsed;
                    Order_dcomissionMoneyUsed = dcomissionMoneyUsed;
                }
                objOrder.createdBy = Convert.ToInt64(ViewState["userId"]);

                double dUsed = Convert.ToDouble(hfPayFull.Value.Trim());
                DataTable dtProductCart = (DataTable)Session["dtProductCart"];



                double dDealTotal = 0;
                double order_ccCreditUsed = Convert.ToDouble(lblGrandTotal.Text.Trim());
                long intOrderNumnbr = 0;
                bool firstOrder = false;
                hfPayFull.Value = dUsed.ToString();
                for (int a = 0; a < dtProductCart.Rows.Count; a++)
                {
                    objOrder.dealId = Convert.ToInt64(dtProductCart.Rows[a]["productID"].ToString().Trim());
                    double dccCreditUsed = 0;
                    double dOneItemPrice = 0;
                    double dShippingAndTax = 0;
                    double dDealTotalTemp = 0;
                    int intQty = Convert.ToInt32(dtProductCart.Rows[a]["Qty"].ToString().Trim());
                    dOneItemPrice = Convert.ToDouble(dtProductCart.Rows[a]["sellingPrice"].ToString().Trim()) + dShippingAndTax;
                    dDealTotal = 0;
                    dDealTotalTemp = intQty * dOneItemPrice;

                    dccCreditUsed = 0;

                    if (dUsed >= dDealTotalTemp)
                    {
                        dUsed -= dDealTotalTemp;
                    }
                    else
                    {
                        dccCreditUsed = float.Parse((dDealTotalTemp - dUsed).ToString());
                        dUsed = 0;
                    }
                    objOrder.orderNo = GenerateId().ToString().Substring(1, 7) + "_" + DateTime.Now.ToString("MMddyyHHmmss");
                    for (int i = 0; i < intQty; i++)
                    {
                        try
                        {
                            objOrder.Qty = 1;
                            objOrder.giftQty = 0;
                            objOrder.personalQty = 1;
                            objOrder.status = "Successful";
                            objOrder.userId = Convert.ToInt64(ViewState["userId"].ToString());
                            objOrder.size = dtProductCart.Rows[a]["Size"].ToString().Trim();
                            if (dccCreditUsed >= dOneItemPrice)
                            {
                                objOrder.ccCreditUsed = float.Parse(dOneItemPrice.ToString());
                                dccCreditUsed -= dOneItemPrice;
                                objOrder.tastyCreditUsed = 0;
                                objOrder.comissionMoneyUsed = 0;
                            }
                            else if (dtastyCreditUsed >= dOneItemPrice)
                            {
                                objOrder.ccCreditUsed = 0;
                                objOrder.tastyCreditUsed = float.Parse(dOneItemPrice.ToString());
                                dtastyCreditUsed -= dOneItemPrice;
                                objOrder.comissionMoneyUsed = 0;
                            }
                            else if (dcomissionMoneyUsed >= dOneItemPrice)
                            {
                                objOrder.ccCreditUsed = 0;
                                objOrder.tastyCreditUsed = 0;
                                objOrder.comissionMoneyUsed = float.Parse(dOneItemPrice.ToString());
                                dcomissionMoneyUsed -= dOneItemPrice;
                            }
                            else if ((dccCreditUsed + dtastyCreditUsed) >= dOneItemPrice)
                            {
                                objOrder.ccCreditUsed = float.Parse(dccCreditUsed.ToString());
                                objOrder.tastyCreditUsed = float.Parse((dOneItemPrice - dccCreditUsed).ToString());
                                dtastyCreditUsed = (dOneItemPrice - dccCreditUsed) - dtastyCreditUsed;
                                dccCreditUsed = 0;
                                objOrder.comissionMoneyUsed = 0;
                            }
                            else if ((dccCreditUsed + dcomissionMoneyUsed) >= dOneItemPrice)
                            {
                                objOrder.ccCreditUsed = float.Parse(dccCreditUsed.ToString());
                                objOrder.tastyCreditUsed = 0;
                                objOrder.comissionMoneyUsed = float.Parse((dOneItemPrice - dccCreditUsed).ToString());
                                dcomissionMoneyUsed = (dOneItemPrice - dccCreditUsed) - dcomissionMoneyUsed;
                                dccCreditUsed = 0;
                            }
                            else if (dtastyCreditUsed + dcomissionMoneyUsed >= dOneItemPrice)
                            {
                                objOrder.ccCreditUsed = 0;
                                objOrder.tastyCreditUsed = float.Parse(dtastyCreditUsed.ToString());
                                objOrder.comissionMoneyUsed = float.Parse((dOneItemPrice - dtastyCreditUsed).ToString());
                                dcomissionMoneyUsed = (dOneItemPrice - dtastyCreditUsed) - dcomissionMoneyUsed;
                                dtastyCreditUsed = 0;
                            }
                            else if (dtastyCreditUsed + dcomissionMoneyUsed + dccCreditUsed >= dOneItemPrice)
                            {
                                objOrder.ccCreditUsed = float.Parse(dccCreditUsed.ToString());
                                objOrder.tastyCreditUsed = float.Parse(dtastyCreditUsed.ToString());
                                objOrder.comissionMoneyUsed = float.Parse(dcomissionMoneyUsed.ToString());
                                dtastyCreditUsed = 0;
                                dcomissionMoneyUsed = 0;
                                dccCreditUsed = 0;
                            }
                            //Total Amount he has to pay -- Total Deal ordered Amount
                            objOrder.totalAmt = float.Parse(dOneItemPrice.ToString());
                            objOrder.shippingAndTaxAmount = float.Parse(dShippingAndTax.ToString());
                            //Set teh Deal Address 
                            //objOrder.addressId = this.hfAddressID.Value.Trim() == "" ? 0 : int.Parse(this.hfAddressID.Value.ToString());
                            objOrder.orderIPAddress = Request.UserHostAddress.Trim();
                            intOrderNumnbr = objOrder.createNewDealOrder();

                            BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                            BLLSampleVouchers objSampleVouchers = new BLLSampleVouchers();
                            objSampleVouchers.dealId = objOrder.dealId;
                            DataTable dtUnUsedVoucher = objSampleVouchers.getTop1UnusedSampleVouchersByDealID();
                            if (dtUnUsedVoucher != null && dtUnUsedVoucher.Rows.Count > 0)
                            {
                                objDetail.dealOrderCode = dtUnUsedVoucher.Rows[0]["dealOrderCode"].ToString().Trim();
                                objDetail.voucherSecurityCode = dtUnUsedVoucher.Rows[0]["voucherSecurityCode"].ToString().Trim();
                                objDetail.dOrderID = intOrderNumnbr;
                                objDetail.isGift = false;
                                objDetail.isRedeemed = false;
                                objSampleVouchers.detailID = objDetail.createDealOrderDetail();
                                objSampleVouchers.isUsed = true;
                                objSampleVouchers.vID = Convert.ToInt32(dtUnUsedVoucher.Rows[0]["vID"].ToString().Trim());
                                objSampleVouchers.updateSampleVouchers();
                            }
                            else
                            {
                                bool notExist = true;
                                while (notExist)
                                {
                                    objDetail.dealOrderCode = objEnc.EncryptData("deatailOrder", GenerateId().ToString().Substring(1, 7) + gerrateAlpabit().ToUpper());
                                    DataTable dtDeal = objDetail.getAllDealOrderDetailByDealOrderCode();
                                    objSampleVouchers.dealOrderCode = objDetail.dealOrderCode;
                                    DataTable dtSampleVoucher = objSampleVouchers.getSampleVoucherByDealOrderCode();
                                    if (dtDeal != null && dtSampleVoucher != null
                                        && dtDeal.Rows.Count == 0 && dtSampleVoucher.Rows.Count == 0)
                                    {
                                        notExist = false;
                                    }
                                }
                                objDetail.voucherSecurityCode = GenerateId().ToString().Substring(1, 3) + "-" + GenerateId().ToString().Substring(1, 3);
                                objDetail.dOrderID = intOrderNumnbr;
                                objDetail.isGift = false;
                                objDetail.isRedeemed = false;
                                objDetail.createDealOrderDetail();
                            }
                            if (i == 0)
                            {
                                if (ChkDealOrderFirstOrderOfUser(Convert.ToInt32(ViewState["userId"].ToString())))
                                {
                                    firstOrder = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = ex.Message;
                            orderInProcess = false;
                            return;
                        }
                    }

                }
                //Add the Affiliate Commission to Refferer
                if (intOrderNumnbr != 0)
                {
                    try
                    {

                        long lRefId = GetRefferalIdByUserID(Convert.ToInt32(ViewState["userId"].ToString()));
                        if (lRefId != 0)
                        {
                            bool tastyCreditSend = false;

                            BLLUser affUser = new BLLUser();
                            affUser.userId = Convert.ToInt32(lRefId.ToString());
                            DataTable dtUserInfo = affUser.getUserByID();
                            if (dtUserInfo != null && dtUserInfo.Rows.Count > 0 &&
                                dtUserInfo.Rows[0]["isAffiliate"] != null &&
                                !Convert.ToBoolean(dtUserInfo.Rows[0]["isAffiliate"].ToString()) && firstOrder && dDealTotal >= 20)
                            {
                                tastyCreditSend = AddTastyGoCreditToReffer(Convert.ToInt32(ViewState["userId"].ToString()), intOrderNumnbr, lRefId);
                            }
                            else if (Convert.ToBoolean(dtUserInfo.Rows[0]["isAffiliate"].ToString()) && !(Order_tastyCreditUsed + Order_dcomissionMoneyUsed > 0))
                            {
                                AddAffiliateCommissionToReffer(Convert.ToInt32(ViewState["userId"].ToString()), intOrderNumnbr, dDealTotal.ToString(), "10", lRefId);
                            }
                        }
                    }
                    catch (Exception ex)
                    { }

                }
                try
                {
                    string strOrderEmailBody = RenderOrderDetailHTML(intOrderNumnbr.ToString(), order_ccCreditUsed, Order_tastyCreditUsed, Order_dcomissionMoneyUsed);
                    Misc.SendEmail(ViewState["userName"].ToString().Trim(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), ConfigurationManager.AppSettings["EmailSubjectForNewOrderForMember"].ToString().Trim(), strOrderEmailBody);
                }
                catch (Exception ex)
                { }
                orderInProcess = false;
                Session["dtProductCartNew"] = (DataTable)Session["dtProductCart"];
                Session.Remove("dtProductCart");
                Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/orderComplete2.aspx?oid=" + intOrderNumnbr.ToString() + "&od=" + order_ccCreditUsed.ToString("###.00") + "_" + Order_tastyCreditUsed.ToString("###.00") + "_" + Order_dcomissionMoneyUsed.ToString("###.00"), false);
            }
        }
        catch (Exception ex)
        {
            orderInProcess = false;
        }
    }

}