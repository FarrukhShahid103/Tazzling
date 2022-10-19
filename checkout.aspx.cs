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
using GecLibrary;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net.Mail;
using com.optimalpayments.test.webservices;
public partial class checkout : System.Web.UI.Page
{
    BLLDeals objDeal = new BLLDeals();
    BLLCities objCities = new BLLCities();
    BLLUserCCInfo objCCInfo = new BLLUserCCInfo();
    BLLDealOrders objOrder = new BLLDealOrders();
    BLLUser objUser = new BLLUser();
    BLLGiftdealsInfo objGiftIt = new BLLGiftdealsInfo();
    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
    BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
    BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
    public static string strhideDive = "";
    public static string strHideDeliveryInfo = "";
    public static string DivShow = "False";
    public static string strhideShippingDiv = "";

    #region Top Area
    protected void Page_Load(object sender, EventArgs e)
    {
        //   Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // SendMailToAdmin("sher.azam@redsignal.biz", "Sher Azam");
        System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
        StringBuilder sbJS = new StringBuilder();

        sbJS.Append("if (typeof(Page_ClientValidate) == 'function') { ");

        sbJS.Append("if (Page_ClientValidate('CheckOut') == false) { return false; }} ");

        sbJS.Append("this.disabled = true;");

        sbJS.Append(Page.ClientScript.GetPostBackEventReference(btnCompleteOrder, ""));

        sbJS.Append(";return false;");

        btnCompleteOrder.Attributes.Add("onclick", sbJS.ToString());

        // ltUserIP.Text = "Anti-Fraud: " + Request.UserHostAddress;
        if (ViewState["title"] != null)
        {
            Page.Title = ViewState["title"].ToString();
        }
        string strCityName = "";

        string strCityID = "337";
        HttpCookie citycookie = Request.Cookies["yourCity"];
        if (citycookie != null)
        {
            strCityID = citycookie.Values[0].ToString().Trim();
        }
        BLLCities objCities = new BLLCities();
        objCities.cityId = Convert.ToInt32(strCityID);

        DataTable dtCity = objCities.getCityByCityId();
        if (dtCity.Rows.Count > 0)
        {
            strCityName = dtCity.Rows[0]["cityName"].ToString().Trim();
        }
        
        if (!IsPostBack)
        {
            try
            {
                if (Session["dtDealCartNew"] != null)
                {
                    Session.Remove("dtDealCartNew");
                }
                LoadDropDownList();
                CheckDealExpired();
                strhideShippingDiv = "";

                // bindProvinces();
                string sURL = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
                {
                    hlTermAndCondition.Visible = true;
                    cbAgree.Visible = true;
                    Label3.Text = "Create an account, or login to proceed on checkout. Your Deal will be available in the member area once the purchase completes. If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a>";
                    btnCompleteOrder.Enabled = true;
                }
                else
                {
                    hlTermAndCondition.Visible = false;
                    cbAgree.Visible = false;
                    Label3.Text = "Your Deal will be available in the member area once the purchase completes. If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a>";
                    lblErrorMessage.Text = "";
                    lblErrorMessage.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }

            DataTable dtDeal = null;
            double dSellPrice = 0;
            if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "")
            {
                objDeal = new BLLDeals();
                objDeal.DealId = Convert.ToInt64(Request.QueryString["did"].ToString());

                objDeal.cityId = Convert.ToInt32(strCityID);
                objDeal.cityId = Convert.ToInt32(strCityID);
                dtDeal = objDeal.getDealByDealAndCityID();
                if (dtDeal.Rows.Count > 0)
                {
                    dSellPrice = Convert.ToDouble(dtDeal.Rows[0]["sellingPrice"].ToString().Trim());
                }
            }
            
            Page.Title = strCityName + "'s Tasty Daily Deal";
            ViewState["title"] = Page.Title;                       
            DataTable dtUser = null;
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
            {
                divSignUpNew.Visible = false;
                RequiredFieldValidator1.Enabled = false;
                RequiredFieldValidator11.Enabled = false;
                PasswordLengthValid.Enabled = false;
                RequiredFieldValidator13.Enabled = false;
                RegularExpressionValidator2.Enabled = false;
                RequiredFieldValidator12.Enabled = false;
                cvConfirmPassword.Enabled = false;

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

                divSignUpNew.Visible = false;
                //Fill the Credit Card Info GridView
                if (dtUser.Rows.Count > 0)
                {
                    txtFirstname.Text = dtUser.Rows[0]["firstName"].ToString();
                    txtLastName.Text = dtUser.Rows[0]["lastName"].ToString();
                    txtBUserName.Text = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
                    txtEmail.Text = dtUser.Rows[0]["userName"].ToString();
                    txtCEmailAddress.Text = dtUser.Rows[0]["userName"].ToString();
                    txtCEmailAddress.ReadOnly = true;
                    txtEmail.ReadOnly = true;

                    ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();

                    //Get & Set the Remained Refferred Balance
                    getRemainedGainedBalByUserId(dtUser);


                    //Hide-Show the Credit Card Info in the function below                           
                    if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "")
                    {
                        if (dSellPrice == 0)
                        {
                            divDelivery1.Visible = false;
                            //Hide the Credit Card Grid here
                            this.divDeliveryGridCCI.Visible = false;
                            divRefBal.Visible = false;
                            //Show the Credit Card Personal Info here
                            //this.divDelivery2.Visible = true;
                            this.divDelivery3.Visible = true;


                            this.btnSave.Visible = false;
                            this.CancelButton.Visible = false;
                        }
                        else
                        {
                            GridDataBind(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                        }
                    }
                    else                    
                    {
                        GridDataBind(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                    }
                }
            }
            else
            {
                HttpCookie cookie = Request.Cookies["newslettersubscribe"];
                if (cookie != null)
                {
                    txtEmail.Text = cookie.Values[0].ToString().Trim();
                    txtCEmailAddress.Text = cookie.Values[0].ToString().Trim();
                }

                //Hide the Credit Card Grid here
                this.divDeliveryGridCCI.Visible = false;

                //Show the Credit Card Personal Info here
                this.divDelivery1.Visible = true;

                this.btnSave.Visible = false;
                this.CancelButton.Visible = false;
            }

        }
    }
      
    protected void CheckDealExpired()
    {
        try
        {
            if (Request.QueryString["did"] != null && Request.QueryString["did"].Trim() != "")
            {
                objCities.cityId = 337;

                HttpCookie yourCity = Request.Cookies["yourCity"];
                if (yourCity != null)
                {
                    objCities.cityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
                }

                DataTable dtCity = objCities.getCityByCityId();
                if (dtCity.Rows.Count > 0)
                {
                    objDeal.CreatedDate = Misc.getResturantLocalTime(Convert.ToInt32(dtCity.Rows[0]["provinceId"].ToString()));
                }
                DataTable dtDeal = null;
                objDeal.DealId = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
                objDeal.cityId = objCities.cityId;
                dtDeal = objDeal.getCurrentDealByDealID();
                if (dtDeal != null && dtDeal.Rows.Count > 0)
                {
                    DateTime dtTempEndTime = Convert.ToDateTime(dtDeal.Rows[0]["dealEndTime"].ToString().Trim());
                    TimeSpan ts = dtTempEndTime - objDeal.CreatedDate;

                    int intTotalOrders = 0;
                    if (dtDeal.Rows[0]["Orders"].ToString().Trim() != "")
                    {
                        intTotalOrders = Convert.ToInt32(dtDeal.Rows[0]["Orders"].ToString());
                    }

                    if (ts.Milliseconds < 0 || (dtDeal.Rows[0]["dealDelMaxLmt"] != null && dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim() != "0" && (intTotalOrders >= Convert.ToInt32(dtDeal.Rows[0]["dealDelMaxLmt"].ToString().Trim()))))
                    {
                        Response.Redirect("default.aspx");
                        return;
                    }

                    if (Session["dealAdd"] != null || Request.QueryString["dealAdd"] != null)
                    {
                        Session.Remove("dealAdd");
                        DataTable dtTopDealCart = null;
                        DataTable dtTemp = objDeal.getdealDetailForCheckout();
                        if (Session["dtTopDealCart"] == null)
                        {
                            dtTopDealCart = new DataTable("dtTopDealCart");
                            DataColumn dealID = new DataColumn("dealID");
                            DataColumn title = new DataColumn("title");
                            DataColumn image = new DataColumn("image");
                            DataColumn restaurantBusinessName = new DataColumn("restaurantBusinessName");
                            DataColumn dealPageTitle = new DataColumn("dealPageTitle");
                            DataColumn chieldDeals = new DataColumn("chieldDeals");

                            dtTopDealCart.Columns.Add(dealID);
                            dtTopDealCart.Columns.Add(title);
                            dtTopDealCart.Columns.Add(image);
                            dtTopDealCart.Columns.Add(restaurantBusinessName);
                            dtTopDealCart.Columns.Add(dealPageTitle);
                            dtTopDealCart.Columns.Add(chieldDeals);
                            Session["dtTopDealCart"] = dtTopDealCart;

                        }
                        else
                        {
                            dtTopDealCart = (DataTable)Session["dtTopDealCart"];
                            if (dtTopDealCart != null)
                            {
                                DataRow[] foundRows = dtTopDealCart.Select("dealID =" + dtTemp.Rows[0]["dealID"].ToString().Trim());
                                bool needToAdd = true;
                                if (foundRows.Length > 0)
                                {
                                    needToAdd = false;
                                }
                                if (needToAdd)
                                {
                                    DataRow dRow;
                                    dRow = dtTopDealCart.NewRow();
                                    dRow["dealID"] = dtTemp.Rows[0]["dealID"].ToString().Trim();
                                    dRow["title"] = dtTemp.Rows[0]["title"].ToString().Trim();
                                    dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtTemp.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtTemp.Rows[0]["image1"].ToString().Trim();
                                    dRow["restaurantBusinessName"] = dtTemp.Rows[0]["restaurantBusinessName"].ToString().Trim();
                                    dRow["dealPageTitle"] = dtTemp.Rows[0]["dealPageTitle"].ToString().Trim() == "" ? dtTemp.Rows[0]["title"].ToString().Trim() : dtTemp.Rows[0]["dealPageTitle"].ToString().Trim();
                                    dRow["chieldDeals"] = dtTemp.Rows.Count;                                                                       
                                    dtTopDealCart.Rows.Add(dRow);
                                    Session["dtTopDealCart"] = dtTopDealCart;
                                }                               
                            }
                        }
                        if (Session["dtDealCart"] == null)
                        {
                            DataTable dtDealCart = new DataTable("dtDealCart");
                            DataColumn dealID = new DataColumn("dealID");
                            DataColumn title = new DataColumn("title");
                            DataColumn valuePrice = new DataColumn("valuePrice");
                            DataColumn sellingPrice = new DataColumn("sellingPrice");
                            DataColumn image = new DataColumn("image");
                            DataColumn minOrdersPerUser = new DataColumn("minOrdersPerUser");
                            DataColumn maxOrdersPerUser = new DataColumn("maxOrdersPerUser");
                            DataColumn maxGiftsPerOrder = new DataColumn("maxGiftsPerOrder");
                            DataColumn shippingAndTax = new DataColumn("shippingAndTax");
                            DataColumn shippingAndTaxAmount = new DataColumn("shippingAndTaxAmount");
                            DataColumn Qty = new DataColumn("Qty", typeof(int));
                            DataColumn isGift = new DataColumn("isGift");
                            DataColumn parentdealId = new DataColumn("parentdealId");

                            dtDealCart.Columns.Add(dealID);
                            dtDealCart.Columns.Add(title);
                            dtDealCart.Columns.Add(valuePrice);
                            dtDealCart.Columns.Add(sellingPrice);
                            dtDealCart.Columns.Add(image);
                            dtDealCart.Columns.Add(minOrdersPerUser);
                            dtDealCart.Columns.Add(maxOrdersPerUser);
                            dtDealCart.Columns.Add(maxGiftsPerOrder);
                            dtDealCart.Columns.Add(shippingAndTax);
                            dtDealCart.Columns.Add(shippingAndTaxAmount);
                            dtDealCart.Columns.Add(Qty);
                            dtDealCart.Columns.Add(isGift);
                            dtDealCart.Columns.Add(parentdealId);
                                                        
                            DataRow dRow;
                            dRow = dtDealCart.NewRow();
                            dRow["dealID"] = dtDeal.Rows[0]["dealID"].ToString().Trim();
                            dRow["title"] = dtDeal.Rows[0]["title"].ToString().Trim();
                            dRow["valuePrice"] = dtDeal.Rows[0]["valuePrice"].ToString().Trim();
                            dRow["sellingPrice"] = dtDeal.Rows[0]["sellingPrice"].ToString().Trim();
                            dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtDeal.Rows[0]["image1"].ToString().Trim();
                            dRow["minOrdersPerUser"] = dtDeal.Rows[0]["minOrdersPerUser"].ToString().Trim();
                            dRow["maxOrdersPerUser"] = dtDeal.Rows[0]["maxOrdersPerUser"].ToString().Trim();
                            dRow["maxGiftsPerOrder"] = dtDeal.Rows[0]["maxGiftsPerOrder"].ToString().Trim();
                            dRow["shippingAndTax"] = dtDeal.Rows[0]["shippingAndTax"].ToString().Trim();
                            dRow["shippingAndTaxAmount"] = dtDeal.Rows[0]["shippingAndTaxAmount"].ToString().Trim();
                            dRow["Qty"] = "1";
                            dRow["isGift"] = "0";
                            dRow["parentdealId"] = dtTemp.Rows[0]["dealID"].ToString().Trim();
                            dtDealCart.Rows.Add(dRow);
                            Session["dtDealCart"] = dtDealCart;                            
                        }
                        else
                        {
                            DataTable dtDealCart = (DataTable)Session["dtDealCart"];
                            if (dtDealCart != null)
                            {
                                DataRow[] foundRows = dtDealCart.Select("dealID =" + dtDeal.Rows[0]["dealID"].ToString().Trim());
                                bool needToAdd = true;
                                if (foundRows.Length > 0)
                                {
                                    if (foundRows.Length == 2)
                                    {
                                        needToAdd = false;
                                    }
                                    else if (foundRows.Length == 1)
                                    {
                                        foreach (DataRow row in foundRows)
                                        {
                                            if (row["isGift"].ToString().Trim() == "0")
                                            {
                                                needToAdd = false;
                                            }
                                        }
                                    }
                                }
                                if (needToAdd)
                                {
                                    DataRow dRow;
                                    dRow = dtDealCart.NewRow();
                                    dRow["dealID"] = dtDeal.Rows[0]["dealID"].ToString().Trim();
                                    dRow["title"] = dtDeal.Rows[0]["title"].ToString().Trim();
                                    dRow["valuePrice"] = dtDeal.Rows[0]["valuePrice"].ToString().Trim();
                                    dRow["sellingPrice"] = dtDeal.Rows[0]["sellingPrice"].ToString().Trim();
                                    dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtDeal.Rows[0]["image1"].ToString().Trim();
                                    dRow["minOrdersPerUser"] = dtDeal.Rows[0]["minOrdersPerUser"].ToString().Trim();
                                    dRow["maxOrdersPerUser"] = dtDeal.Rows[0]["maxOrdersPerUser"].ToString().Trim();
                                    dRow["maxGiftsPerOrder"] = dtDeal.Rows[0]["maxGiftsPerOrder"].ToString().Trim();
                                    dRow["shippingAndTax"] = dtDeal.Rows[0]["shippingAndTax"].ToString().Trim();
                                    dRow["shippingAndTaxAmount"] = dtDeal.Rows[0]["shippingAndTaxAmount"].ToString().Trim();
                                    dRow["Qty"] = "1";
                                    dRow["isGift"] = "0";
                                    dRow["parentdealId"] =dtTemp.Rows[0]["dealID"].ToString().Trim();
                                    dtDealCart.Rows.Add(dRow);
                                    Session["dtDealCart"] = dtDealCart;
                                }
                               
                            }
                        }
                        gridview1.DataSource = dtTopDealCart.DefaultView;
                        gridview1.DataBind();
                    }
                    else if (Session["dtTopDealCart"] != null)
                    {
                        DataTable dtDealCart = (DataTable)Session["dtTopDealCart"];
                        gridview1.DataSource = dtDealCart.DefaultView;
                        gridview1.DataBind();
                    }
                    if (gridview1.Rows.Count == 0)
                    {
                        Response.Redirect("default.aspx");
                    }
                    resetAmounts();

                }
            }
            else if (Session["dtTopDealCart"] != null)
            {
                DataTable dtDealCart = (DataTable)Session["dtTopDealCart"];
                gridview1.DataSource = dtDealCart.DefaultView;
                gridview1.DataBind();

                if (gridview1.Rows.Count == 0)
                {
                    Response.Redirect("default.aspx");
                }
                resetAmounts();
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }
    
    protected void ddlQuntityGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlQuntityGrid = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlQuntityGrid.NamingContainer;            
            Label lblPriceGrid = (Label)row.FindControl("lblPriceGrid");
            Label lblTotalPriceGrid = (Label)row.FindControl("lblTotalPriceGrid");
            CheckBox cbIsGift = (CheckBox)row.FindControl("cbIsGift");            
            HiddenField hfDealID=(HiddenField)row.FindControl("hfDealID");
            string strCbValue = "0";
            if (cbIsGift.Checked)            
            {
                strCbValue = "1";
            }

            lblTotalPriceGrid.Text = (Convert.ToInt32(lblPriceGrid.Text.Trim()) * Convert.ToInt32(ddlQuntityGrid.SelectedValue.Trim())).ToString();
            if (Session["dtDealCart"] != null)
            {
                DataTable dtDealCart = (DataTable)Session["dtDealCart"];
                if (dtDealCart != null)
                {
                    for (int i = 0; i < dtDealCart.Rows.Count; i++)
                    {
                        if (hfDealID.Value.Trim() == dtDealCart.Rows[i]["dealID"].ToString().Trim() && strCbValue == dtDealCart.Rows[i]["isGift"].ToString().Trim())
                        {
                            dtDealCart.Rows[i]["Qty"] = ddlQuntityGrid.SelectedValue.Trim();
                            Session["dtDealCart"] = dtDealCart;
                            break;
                        }
                    }
                }
            }           
            resetAmounts();
            
        }
        catch (Exception ex)
        { }

    }

    protected void cbIsGift_CheckChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox cbIsGift = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbIsGift.NamingContainer;            
            HiddenField hfDealID = (HiddenField)row.FindControl("hfDealID");
            string strCbValue = "0";
            if (cbIsGift.Checked)
            {
                strCbValue = "1";
            }
            if (Session["dtDealCart"] != null)
            {
                DataTable dtDealCart = (DataTable)Session["dtDealCart"];
                if (dtDealCart != null)
                {
                    for (int i = 0; i < dtDealCart.Rows.Count; i++)
                    {
                        if (hfDealID.Value.Trim() == dtDealCart.Rows[i]["dealID"].ToString().Trim() && strCbValue != dtDealCart.Rows[i]["isGift"].ToString().Trim())
                        {
                            if (cbIsGift.Checked)
                            {
                                dtDealCart.Rows[i]["isGift"] = "1";
                            }
                            else
                            {
                                dtDealCart.Rows[i]["isGift"] = "0";
                            }
                            Session["dtDealCart"] = dtDealCart;
                            break;
                        }
                    }
                }
            }


        }
        catch (Exception ex)
        { }
    }

    protected void gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvSubItem = (GridView)e.Row.FindControl("gvSubItem");
                Label lblID = (Label)e.Row.FindControl("lblId");
                HiddenField hfchieldDeals = (HiddenField)e.Row.FindControl("hfchieldDeals");
                if (lblID != null && lblID.Text.ToString() != "")
                {
                    if (Session["dtDealCart"] != null)
                    {
                        DataTable dtDealCart = (DataTable)Session["dtDealCart"];
                        DataView dv = new DataView(dtDealCart);
                        dv.RowFilter = "parentdealId = '" + lblID.Text + "'";
                        gvSubItem.DataSource = dv;                      
                        gvSubItem.DataBind();
                    }
                }
                if (hfchieldDeals != null && hfchieldDeals.Value.Trim() != "" && Convert.ToInt32(hfchieldDeals.Value.Trim()) > 1)
                {
                    BindSubDeals(lblID.Text.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }
    }

    protected void BindSubDeals(string strDealID)
    {
        try
        {
            DataTable dtSubDealsInfo = null;
            objDeal.cityId = 337;
            HttpCookie yourCity = Request.Cookies["yourCity"];
            if (yourCity != null)
            {
                objDeal.cityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
            }
            objDeal.ParentDealId = Convert.ToInt64(strDealID);
            dtSubDealsInfo = objDeal.getCurrentSubDealInfoByParnetDealIDForDealPage();
            if ((dtSubDealsInfo != null) && (dtSubDealsInfo.Rows.Count > 1))
            {
                ltSubDeals.Text += " <div id='deal_ref_" + strDealID + "'><div style='padding-left: 10px; padding-bottom:10px;'><div><div style='clear: both;'><div style='float: left; padding-top: 5px; line-height: 20px;'>";
                ltSubDeals.Text += " <span style='font-family: Helvetica; font-size: 19px; font-weight: bold;'>Choose your deal:</span></div></div><div style='clear: both; padding-top: 11px;'><div><table cellspacing='0' border='0' style='width: 592px; border-collapse: collapse;'><tbody>";

                for (int i = 0; i < dtSubDealsInfo.Rows.Count; i++)
                {
                    ltSubDeals.Text += " <tr align='left' style='height: 26px;'><td><table width='100%' cellspacing='0' cellpadding='0' class='DetailRightTopDiv2' style='border: 1px solid #fc37b8;'><tbody><tr><td width='65%' style='padding-left: 8px;'>";
                    ltSubDeals.Text += " <a style='text-decoration: none; color: Black; font-size: 13px;' href='" + getSubDealURL(dtSubDealsInfo.Rows[i]["dealId"].ToString().Trim(), dtSubDealsInfo.Rows[i]["Orders"].ToString().Trim(), dtSubDealsInfo.Rows[i]["dealDelMaxLmt"].ToString().Trim()) + "'>";
                    ltSubDeals.Text += dtSubDealsInfo.Rows[i]["title"].ToString().Trim();
                    ltSubDeals.Text += "</a><br><div style='font-size: 13px; font-weight: bold;'>Value: <font style='font-weight: normal;'>C$ " + dtSubDealsInfo.Rows[i]["valuePrice"].ToString().Trim() + "</font> - Discount: <font style='font-weight: normal;'>";
                    ltSubDeals.Text += Convert.ToInt32(Convert.ToDouble(Convert.ToDouble(100 / Convert.ToDouble(dtSubDealsInfo.Rows[i]["valuePrice"].ToString().Trim())) * (Convert.ToDouble((dtSubDealsInfo.Rows[i]["valuePrice"].ToString().Trim())) - Convert.ToDouble(dtSubDealsInfo.Rows[i]["sellingPrice"].ToString().Trim())))).ToString() + "% off </font>- You Save: <font style='font-weight: normal;'>C$ " + (Convert.ToInt32(dtSubDealsInfo.Rows[i]["valuePrice"].ToString()) - Convert.ToInt32(dtSubDealsInfo.Rows[i]["sellingPrice"].ToString())).ToString() + "</font>";
                    ltSubDeals.Text += "</div></td><td style='padding-right: 5px; padding-left: 5px; width: 15%;'><div style='font-size: 13px; float: left;'>" + dtSubDealsInfo.Rows[i]["Orders"].ToString().Trim() + " bought</div>";
                    ltSubDeals.Text += "</td><td style='padding-right: 5px; padding-left: 40px;'><div style='width: 65px; text-align: center; color: White;'><div style='padding-top: 5px;'><font style='font-weight: bold; font-size: 19px;'><a style='text-decoration: none;color: White;' ";
                    ltSubDeals.Text += " href='" + getSubDealURL(dtSubDealsInfo.Rows[i]["dealId"].ToString().Trim(), dtSubDealsInfo.Rows[i]["Orders"].ToString().Trim(), dtSubDealsInfo.Rows[i]["dealDelMaxLmt"].ToString().Trim()) + "'>C$ " + dtSubDealsInfo.Rows[i]["sellingPrice"].ToString().Trim() + "</a></font></div></div></td></tr></tbody></table></td></tr>";
                }
                ltSubDeals.Text += "</tbody></table></div></div></div></div></div>";
            }
        }
        catch (Exception ex)
        {

        }
    }
    
    protected string getSubDealURL(string dealId, string Orders, string dealDelMaxLmt)
    {
        if (Convert.ToInt32(dealDelMaxLmt.ToString().Trim()) != 0 && Convert.ToInt32(Orders.ToString().Trim()) >= Convert.ToInt32(dealDelMaxLmt.ToString().Trim()))
        {
            return "#";
        }
        else
        {
            return ConfigurationManager.AppSettings["YourSecureSite"] + "checkout.aspx?dealAdd=1&did=" + dealId.ToString();
        }
    }
    
    protected void gvSubItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {               

                HiddenField hfisGift = (HiddenField)e.Row.FindControl("hfisGift");
                HiddenField hfminOrdersPerUser = (HiddenField)e.Row.FindControl("hfminOrdersPerUser");
                HiddenField hfmaxOrdersPerUser = (HiddenField)e.Row.FindControl("hfmaxOrdersPerUser");
                HiddenField hfmaxGiftsPerOrder = (HiddenField)e.Row.FindControl("hfmaxGiftsPerOrder");                
                HiddenField hfQty = (HiddenField)e.Row.FindControl("hfQty");
                DropDownList ddlQuntityGrid = (DropDownList)e.Row.FindControl("ddlQuntityGrid");
                Label lblPriceGrid = (Label)e.Row.FindControl("lblPriceGrid");
                Label lblTotalPriceGrid = (Label)e.Row.FindControl("lblTotalPriceGrid");                
                if (hfisGift != null && hfisGift.Value.Trim() == "1")
                {
                    for (int i = 0; i <= Convert.ToInt32(hfmaxGiftsPerOrder.Value); i++)
                    {
                        ddlQuntityGrid.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    lblTotalPriceGrid.Text = (Convert.ToInt32(lblPriceGrid.Text.Trim()) * Convert.ToInt32(hfQty.Value)).ToString();
                    ddlQuntityGrid.SelectedValue = hfQty.Value;
                }
                else
                {                                       
                    if (ddlQuntityGrid != null && hfminOrdersPerUser != null
                        && hfmaxOrdersPerUser != null && hfQty != null
                        && lblPriceGrid != null && lblTotalPriceGrid!=null)
                    {
                        int max = Convert.ToInt32(hfmaxOrdersPerUser.Value);
                        int min = Convert.ToInt32(hfminOrdersPerUser.Value);
                        ddlQuntityGrid.Items.Clear();                                                
                        if (max != 0)
                        {
                            for (int i = min; i <= max; i++)
                            {
                                ddlQuntityGrid.Items.Add(new ListItem(i.ToString(), i.ToString()));
                            }
                        }
                        else
                        {
                            for (int i = min; i <= 4; i++)
                            {
                                ddlQuntityGrid.Items.Add(new ListItem(i.ToString(), i.ToString()));
                            }
                        }
                        lblTotalPriceGrid.Text = (Convert.ToInt32(lblPriceGrid.Text.Trim()) * Convert.ToInt32(hfQty.Value)).ToString();
                        ddlQuntityGrid.SelectedValue = hfQty.Value;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }
    }

    protected void gvSubItem_Login(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {          
            string[] strIds = e.CommandArgument.ToString().Split('_');
            if (Session["dtDealCart"] != null)
            {
                DataTable dtDealCart = (DataTable)Session["dtDealCart"];
                DataTable dtTopDealCart = (DataTable)Session["dtTopDealCart"];
                if (dtDealCart != null)
                {
                    for (int i = 0; i < dtDealCart.Rows.Count; i++)
                    {
                        if (strIds[0].Trim() == dtDealCart.Rows[i]["dealID"].ToString().Trim())
                        {
                            string strParentdealID = dtDealCart.Rows[i]["parentdealId"].ToString().Trim();                            
                            dtDealCart.Rows.RemoveAt(i);                            
                            Session["dtDealCart"] = dtDealCart;
                            DataRow[] foundRows = dtDealCart.Select("parentdealId =" + strParentdealID.Trim());
                            if (foundRows.Length == 0)
                            {
                                foundRows = dtTopDealCart.Select("dealID =" + strParentdealID.Trim());
                                dtTopDealCart.Rows.Remove(foundRows[0]);
                                Session["dtTopDealCart"] = dtTopDealCart;
                            }
                            break;
                        }
                    }
                    if (dtTopDealCart.Rows.Count > 0)
                    {
                        gridview1.DataSource = dtTopDealCart.DefaultView;
                        gridview1.DataBind();
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }
                resetAmounts();
            }
            
        }
    }

 
    private bool SendMailForNewAccount(string strPassword, string strUserName, string strName)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string toAddress = strUserName;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailNewAccountCredentials"].ToString().Trim();
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + strName.Trim() + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your account has been recently created on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your account detail is following</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>User Name :  " + strUserName + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Password :" + strPassword.ToString().Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");


            message.Body = sb.ToString();
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            //lblAddressError.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            //lblAddressError.Visible = true;
            //imgGridMessage.Visible = true;
            //imgGridMessage.ImageUrl = "images/error.png";
            //lblAddressError.ForeColor = System.Drawing.Color.Red;
            return false;
        }
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
            if (dGainedCredit > 0)
            {
                ShowNotification((dGainedCredit).ToString());
            }
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

        objCCInfo = new BLLUserCCInfo();
        objCCInfo.userId = UserID;
        dtCCInfo = objCCInfo.getUserCCInfoByUserID();

        if ((dtCCInfo != null) && (dtCCInfo.Rows.Count > 0))
        {
            gvCustomers.DataSource = dtCCInfo;
            gvCustomers.DataBind();

            //Show the Credit Card Grid here
            this.divDeliveryGridCCI.Visible = true;

            //Hide the Credit Card Personal Info here
            this.divDelivery1.Visible = false;
            //this.divDelivery2.Visible = false;
            this.divDelivery3.Visible = false;
            
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
            this.divDelivery1.Visible = true;
            //this.divDelivery2.Visible = true;
            this.divDelivery3.Visible = true;

            this.btnSave.Visible = false;
            this.CancelButton.Visible = false;
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
        switch (e.CommandName)
        {
            //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
            case "EditCustomer":
                //Show the Credit Card Information                
                //this.divDelivery2.Visible = true;
                this.divDelivery3.Visible = true;
                GetAndSetCreditCardInfo(int.Parse(e.CommandArgument.ToString()));
                
                this.btnSave.Visible = true;
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
        shippingCheck();
    }

    private void GetAndSetCreditCardInfo(int iCCInfoID)
    {
        try
        {
            objCCInfo = new BLLUserCCInfo();
            objCCInfo.ccInfoID = Convert.ToInt64(iCCInfoID.ToString());
            //Get the Credit Card Info of User here
            DataTable dtUserCCInfo = objCCInfo.getUserCCInfoByID();
            hfPostalCode.Value = "0";
            if ((dtUserCCInfo != null) && (dtUserCCInfo.Rows.Count > 0))
            {
                //Set the Delivery Information
                this.hfCCInfoIdEdit.Value = dtUserCCInfo.Rows[0]["ccInfoID"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoID"].ToString().Trim();

                //Set the First Name
                this.txtFirstname.Text = dtUserCCInfo.Rows[0]["ccInfoDFirstName"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDFirstName"].ToString().Trim();
                //Set the Last Name
                this.txtLastName.Text = dtUserCCInfo.Rows[0]["ccInfoDLastName"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDLastName"].ToString().Trim();
                //Set the Email                
                this.txtEmail.Text = dtUserCCInfo.Rows[0]["ccInfoDEmail"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDEmail"].ToString().Trim();
                //Set the Confirm Email here
                this.txtCEmailAddress.Text = dtUserCCInfo.Rows[0]["ccInfoDEmail"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoDEmail"].ToString().Trim();

                GECEncryption objEnc = new GECEncryption();

                //Set the Billing Information here
                //Set the Business Name here
                this.txtBUserName.Text = dtUserCCInfo.Rows[0]["ccInfoBName"] == DBNull.Value ? "" : objEnc.DecryptData("colintastygochengusername", dtUserCCInfo.Rows[0]["ccInfoBName"].ToString().Trim());
                //Set the Business Address here
                this.txtBillingAddress.Text = dtUserCCInfo.Rows[0]["ccInfoBAddress"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                //Set the Business City here
                this.txtBCity.Text = dtUserCCInfo.Rows[0]["ccInfoBCity"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBCity"].ToString().Trim();
                //Set the Business Province name here
                try
                {
                   txtProvince.Text= dtUserCCInfo.Rows[0]["ccInfoBProvince"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBProvince"].ToString().Trim();
                }
                catch (Exception ex)
                { }
                //Set the Business Postal Code here
                this.txtPostalCode.Text = dtUserCCInfo.Rows[0]["ccInfoBPostalCode"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoBPostalCode"].ToString().Trim();

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
                this.ddlYear.SelectedValue = arrMMyy.Count > 1 ? arrMMyy[1].ToString() : "2011";
                //Set the CVN # here
                //this.txtCVNumber.Text = dtUserCCInfo.Rows[0]["ccInfoCCVNumber"] == DBNull.Value ? "" : objEnc.DecryptData("colintastygochengccv", dtUserCCInfo.Rows[0]["ccInfoCCVNumber"].ToString().Trim());
                this.hfCVNumber.Value = dtUserCCInfo.Rows[0]["ccInfoCCVNumber"] == DBNull.Value ? "" : dtUserCCInfo.Rows[0]["ccInfoCCVNumber"].ToString().Trim();
            }
        }
        catch (Exception ex)
        { }
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
    
    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public string GetPassword()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomNumber(10000, 99999));
        builder.Append(RandomNumber(10000, 99999));
        return builder.ToString();
    }

    private string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
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

    protected void btnApply_Click(object sender, EventArgs e)
    {
        resetAmounts();
        txtTastyCredit.MaxLength = lblRefBalanace.Text.Length;
    }

    private void resetAmounts()
    {
        try
        {
            if (Session["dtDealCart"] != null)
            {
                lblErrorMessage.Visible = false;
                lblErrorMessage.Text = "";
                DataTable dtDealCart = (DataTable)Session["dtDealCart"];
                int Qty = 0;                
                if (dtDealCart != null && dtDealCart.Rows.Count > 0)
                {
                    DataTable dtTemp = (DataTable)Session["dtTopDealCart"];
                    hlContinueShopping.Text = "You currently have " + dtTemp.Rows.Count.ToString() + " deal in your shopping cart. Continue shopping";
                    hlContinueShopping.NavigateUrl = ConfigurationManager.AppSettings["YourSite"].ToString() + "/default.aspx";

                    if (dtDealCart != null)
                    {
                        object sumObject;
                        sumObject = dtDealCart.Compute("Sum(Qty)", "Qty > 0");
                        int.TryParse(sumObject.ToString(), out Qty);
                        hfOrderQty.Value = Qty.ToString();
                    }
                    if (Qty == 0)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Select quantity.";
                        orderInProcess = false;
                        lblGrandTotal.Text = "0";
                        shippingCheck();
                        return;
                    }
                    double shippingAmount = 0;
                    double totalDealPrice = 0;
                    bool shippingAndTax = false;
                    for (int i = 0; i < dtDealCart.Rows.Count; i++)
                    {
                        if (dtDealCart.Rows[i]["shippingAndTax"].ToString().Trim().ToLower() == "true")
                        {
                            shippingAmount += Convert.ToDouble(dtDealCart.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtDealCart.Rows[i]["Qty"].ToString().Trim());
                            shippingAndTax = true;
                        }
                        totalDealPrice += Convert.ToDouble(dtDealCart.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtDealCart.Rows[i]["Qty"].ToString().Trim());
                    }
                    totalDealPrice += shippingAmount;
                    lblShippingAndTax.Text = Math.Round(shippingAmount, 2, MidpointRounding.AwayFromZero).ToString();
                    lblGrandTotal.Text = Math.Round(totalDealPrice, 2, MidpointRounding.AwayFromZero).ToString();
                    hfGrandTotal.Value = Math.Round(totalDealPrice, 2, MidpointRounding.AwayFromZero).ToString();
                    if (shippingAndTax)
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
                    }                    
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
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Place enter the quantity of your purchase.";
                        orderInProcess = false;
                        return;
                    }
                    double dShippingAndTax = Convert.ToDouble(lblShippingAndTax.Text.Trim());                    
                    double dDealTotal = 0;
                    dDealTotal = Convert.ToDouble(lblGrandTotal.Text);
                    double dRemail = dDealTotal - dUsed;                    
                    
                    if (dUsed > dDealTotal)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Place enter the quantity of your purchase.";
                        orderInProcess = false;
                        return;
                    }
                    hfPayFull.Value = dUsed.ToString();
                    lblGrandTotal.Text = Convert.ToDecimal(dRemail).ToString("###.00");
                    if (dRemail == 0)
                    {
                        this.hfMode.Value = "new";
                        strhideDive = "none";
                        cbShippingSame.Checked = false;
                        cbShippingSame.Visible = false;
                        lblSameAsBilling.Visible = false;
                        RequiredFieldValidator8.Enabled = false;
                        RequiredFieldValidator7.Enabled = false;
                        divDelivery1.Visible = false;
                        this.divDeliveryGridCCI.Visible = false;
                        this.divDelivery3.Visible = false;
                        this.btnSave.Visible = false;
                        this.CancelButton.Visible = false;
                    }
                    else if (gvCustomers.Rows.Count == 0)
                    {
                        this.hfMode.Value = "new";
                        strhideDive = "";

                        divDelivery1.Visible = true;
                        //Hide the Credit Card Grid here
                        this.divDeliveryGridCCI.Visible = false;
                        this.divDelivery3.Visible = true;
                        this.btnSave.Visible = false;
                        this.CancelButton.Visible = false;
                    }
                    else
                    {
                        strhideDive = "";
                        divDelivery1.Visible = false;
                        this.divDeliveryGridCCI.Visible = true;
                    }
                    shippingCheck();

                }
            }
        }
        catch (Exception ex)
        { }                          
    }

    private string GetUserRefferalId()
    {
        string strRefId = "";

        try
        {
            HttpCookie cookie = Request.Cookies["tastygo_userID"];

            if (cookie != null)
            {
                strRefId = cookie.Values[0].ToString().Trim();
            }
        }
        catch (Exception ex)
        { }

        return strRefId;
    }
    public bool orderInProcess = false;

    protected bool SignupSuccessfull()
    {
        try
        {
            if (Misc.validateEmailAddress(txtSignUpEmail.Text.Trim()))
            {
                if (txtSignUpFullName.Text.Trim() == "")
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Full Name required.";
                    return false;
                }
                if (txtSignUpPassword.Text.Trim() == "")
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Password required.";
                    return false;
                }
                if ((txtSignUpConfirmPassword.Text.Trim() == "") && (txtSignUpConfirmPassword.Text.Trim() != txtSignUpPassword.Text.Trim()))
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Confirm Password required.";
                    return false;
                }

                BLLUser obj = new BLLUser();

                DataTable dtUser = null;

                obj.userName = this.txtSignUpEmail.Text.Trim();

                obj.email = this.txtSignUpEmail.Text.Trim();

                obj.referralId = "";
                if (!obj.getUserByUserName())
                {
                    string[] strUserFullName = txtSignUpFullName.Text.Trim().Split(' ');
                    if (strUserFullName.Length > 1)
                    {
                        obj.firstName = strUserFullName[0].ToString().Trim();
                        obj.lastName = strUserFullName[1].ToString().Trim();
                    }
                    else
                    {
                        obj.firstName = txtSignUpFullName.Text.Trim();
                        obj.lastName = "";
                    }
                    obj.userName = this.txtSignUpEmail.Text.Trim();
                    obj.userPassword = this.txtSignUpPassword.Text.Trim();
                    obj.email = this.txtSignUpEmail.Text.Trim();

                    //For Customer 
                    obj.userTypeID = 4;
                    obj.isActive = true;

                    obj.referralId = "";
                    obj.countryId = 2;              
                    obj.provinceId = 3;              
                    obj.cityId = 337;
                    obj.friendsReferralId = GetUserRefferalId();
                    obj.howYouKnowUs = "Facebook";
                    obj.gender = true;
                    obj.age = "Select";
                    obj.zipcode = "";
                    obj.ipAddress = Request.UserHostAddress.ToString();
                    long result = obj.createUser();

                    HttpCookie yourCity = Request.Cookies["yourCity"];
                    string strCityid = "337";
                    if (yourCity != null)
                    {
                        strCityid = yourCity.Values[0].ToString().Trim();
                    }
                    Misc.addSubscriberEmail(txtSignUpEmail.Text.Trim(), strCityid);


                    if (result != 0)
                    {
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(result.ToString().Trim()));

                        GECEncryption oEnc = new GECEncryption();

                        string strEncryptUserID = Server.UrlEncode(oEnc.EncryptData("123456", result.ToString())).Replace("%", "_");

                        SendMailWithActiveCode(this.txtSignUpEmail.Text.Trim(), this.txtSignUpPassword.Text.Trim(), this.txtSignUpEmail.Text.Trim(), strEncryptUserID);

                        HttpCookie cookie = Request.Cookies["tastygoSignup"];
                        if (cookie == null)
                        {
                            cookie = new HttpCookie("tastygoSignup");
                        }
                        cookie.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(cookie);
                        cookie["tastygoSignup"] = this.txtSignUpEmail.Text.Trim();
                        try
                        {
                            //BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                            //bllKarma.userId = result;
                            //DataTable dtkarmaPoints = bllKarma.getKarmaTodayLoginPointsByUserId();
                            //if (dtkarmaPoints != null && dtkarmaPoints.Rows.Count == 0)
                            //{
                            //    bllKarma.userId = result;
                            //    bllKarma.karmaPoints = 250;
                            //    bllKarma.karmaPointsType = "Signup";
                            //    bllKarma.createdBy = result;
                            //    bllKarma.createdDate = DateTime.Now;
                            //    bllKarma.createKarmaPoints();
                            //}                           
                        }
                        catch (Exception ex)
                        { }

                        obj = new BLLUser();
                        obj.email = txtSignUpEmail.Text.Trim();
                        obj.userName = txtSignUpEmail.Text.Trim();
                        dtUser = obj.getUserDetailByEmail();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                            txtEmail.Text = txtSignUpEmail.Text.Trim();
                            txtFirstname.Text = dtUser.Rows[0]["firstName"].ToString().Trim();
                            txtLastName.Text = dtUser.Rows[0]["lastName"].ToString().Trim();
                            if (dtUser.Rows[0]["userTypeID"].ToString() == "4")
                            {
                                Session["member"] = dtUser;
                                Session.Remove("restaurant");
                                Session.Remove("sale");
                                Session.Remove("user");
                            }
                            else if (dtUser.Rows[0]["userTypeID"].ToString() == "3")
                            {
                                Session["restaurant"] = dtUser;
                                Session.Remove("member");
                                Session.Remove("sale");
                                Session.Remove("user");
                            }
                            else if (dtUser.Rows[0]["userTypeID"].ToString() == "5")
                            {
                                Session["sale"] = dtUser;
                                Session.Remove("member");
                                Session.Remove("restaurant");
                                Session.Remove("user");
                            }
                            else
                            {

                                Session["user"] = dtUser;
                                Session.Remove("member");
                                Session.Remove("restaurant");
                                Session.Remove("sale");
                            }
                        }
                        return true;
                    }
                    else
                    {
                        imgGridMessage.Visible = true;
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Sorry you could not register for right now please try again.";
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        return false;
                    }
                }
                else
                {
                    imgGridMessage.Visible = true;
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Email already exists, please login or <a href='login.aspx'>click here</a> for password retrieval.";
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            else
            {
                imgGridMessage.Visible = true;
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid email address.";
                lblErrorMessage.Visible = true;
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        catch (Exception ex)
        {
            imgGridMessage.Visible = true;
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = ex.Message;
            lblErrorMessage.Visible = true;
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }
    #endregion

    protected void btnCompleteOrder_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtDealCart"] == null)
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
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Your ip has been temporary blocked for 24 hours due to multiple attempts, please contact us at <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a> for further assistance.";
                    orderInProcess = false;
                    shippingCheck();
                    return;
                }
            }



            if (!orderInProcess)
            {
                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                if (hfOrderQty.Value.Trim() == "0")
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Select quantity.";
                    orderInProcess = false;
                    shippingCheck();
                    return;
                }

                if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
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
                }
                if (ViewState["userId"] == null || ViewState["userId"].ToString().Trim() == "")
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "<span style='color:#676767;'>Your session has been expired. Please Login to proceed checkout.</span>";
                    orderInProcess = false;
                    shippingCheck();
                    return;
                }
                orderInProcess = true;
                lblErrorMessage.Visible = false;
                lblErrorMessage.Text = "";

                if (Session["member"] == null && Session["restaurant"] == null && Session["sale"] == null && Session["user"] == null)
                {
                    Label3.Text = "Your Deal will be available in the member area once the purchase completes. If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com' style='color:blule;'>support@tazzling.com</a>";
                    btnCompleteOrder.Enabled = true;
                    orderInProcess = false;
                    shippingCheck();
                    return;
                }
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
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Place enter the quantity of your purchase.";
                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }

                    
                    double dDealTotalTemp = 0;
                    dDealTotalTemp = Convert.ToDouble(hfGrandTotal.Value.Trim());
                    double dRemail = dDealTotalTemp - dUsedTemp;

                    if (dUsedTemp > dDealTotalTemp)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Place enter the quantity of your purchase.";
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
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Please confirm your expirary date.";
                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }
                }
                else if (this.hfMode.Value.Trim() == "new" && (HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()) == ""))
                {

                }
                else if (divDelivery3.Visible)
                {
                    try
                    {

                        DateTime dtUserDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
                        if (dtUserDate < DateTime.Now)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "Please confirm your expirary date.";
                            orderInProcess = false;
                            shippingCheck();
                            return;
                        }


                        GECEncryption objEncTemp = new GECEncryption();

                        BLLUserCCInfo objCC = new BLLUserCCInfo();
                        hfPostalCode.Value = "0";
                        objCC.ccInfoID = Convert.ToInt64(this.hfCCInfoIdEdit.Value.ToString());
                        objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBillingAddress.Text.Trim());
                        objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtBCity.Text.Trim());
                        objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtPostalCode.Text.Trim());
                        objCC.ccInfoBProvince = txtProvince.Text.Trim();
                        if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
                        {
                            objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                            objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
                        }
                        else
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text = "<span style='color:#676767;'>Your session has been expired. Please Login to proceed checkout.</span>";
                            orderInProcess = false;
                            shippingCheck();
                            return;
                        }
                        objCC.ccInfoCCVNumber = objEncTemp.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim()));
                        objCC.ccInfoEdate = objEncTemp.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
                        //objCC.ccInfoNumber = this.hfCCN.Value.Trim();
                        objCC.ccInfoNumber = objEncTemp.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()));
                        objCC.ccInfoBName = objEncTemp.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBUserName.Text.Trim()));
                        objCC.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(this.txtEmail.Text.Trim());
                        string[] strUserName = txtBUserName.Text.Split(' ');
                        objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(strUserName[0].ToString());
                        if (strUserName.Length > 1)
                        {
                            objCC.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(strUserName[1].ToString());
                        }
                        objCC.updateUserCCInfoByID();
                    }
                    catch (Exception ex)
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                        orderInProcess = false;
                        shippingCheck();
                        return;
                    }
                }

               
                if (ViewState["userId"] == null || ViewState["userId"].ToString().Trim() == "")
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "<span style='color:#676767;'>Your session has been expired. Please Login to proceed checkout.</span>";
                    orderInProcess = false;
                    shippingCheck();
                    return;
                }
                else
                {
                    try
                    {
                        objUser.firstName = HtmlRemoval.StripTagsRegexCompiled(txtFirstname.Text.Trim());
                        objUser.lastName = HtmlRemoval.StripTagsRegexCompiled(txtLastName.Text.Trim());
                        objUser.userId = Convert.ToInt32(ViewState["userId"].ToString());
                        objUser.updateUserFirstAndLastNameByID();
                    }
                    catch (Exception ex)
                    { }
                }
                GECEncryption objEnc = new GECEncryption();

                long createdID = 0;
                long shippingInfoID = 0;

                objOrder = new BLLDealOrders();
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
                            billingDetails.firstName = txtBUserName.Text.Trim();
                            billingDetails.lastName = "";
                            billingDetails.street = txtBillingAddress.Text.Trim();
                            billingDetails.city = txtBCity.Text.Trim();
                            billingDetails.Item = (object)txtProvince.Text.Trim(); // California
                            billingDetails.country = CountryV1.CA; // United States
                            billingDetails.countrySpecified = true;
                            billingDetails.zip = txtPostalCode.Text.Trim();
                            billingDetails.phone = "";
                            billingDetails.email = HtmlRemoval.StripTagsRegexCompiled(txtEmail.Text.Trim());
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
                    objCCInfo.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBillingAddress.Text.Trim());
                    objCCInfo.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtBCity.Text.Trim());
                    objCCInfo.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtPostalCode.Text.Trim());
                    objCCInfo.ccInfoBProvince = txtProvince.Text.Trim();
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

                    objCCInfo.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBUserName.Text.Trim()));
                    objCCInfo.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(txtEmail.Text.Trim());
                    objCCInfo.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(txtFirstname.Text.Trim());
                    objCCInfo.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(txtLastName.Text.Trim());
                    createdID = objCCInfo.createUserCCInfo();
                    if (hfShippingEnabled.Value != "")
                    {
                        if (cbShippingSame.Checked)
                        {
                            BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                            objShippingInfo.Address = txtBillingAddress.Text.Trim();
                            objShippingInfo.City = txtBCity.Text.Trim();
                            objShippingInfo.State = txtProvince.Text.Trim();
                            objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                            objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                            objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                            objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.ZipCode = txtPostalCode.Text.Trim();
                            objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                            shippingInfoID = objShippingInfo.createUserShippingInfo();
                        }
                        else
                        {
                            BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                            objShippingInfo.Address = txtShippingAddress.Text.Trim();
                            objShippingInfo.City = txtShippingCity.Text.Trim();
                            objShippingInfo.State = txtShippingProvince.Text.Trim();
                            objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                            objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                            objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                            objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.ZipCode = txtShippingZipCode.Text.Trim();
                            objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                            shippingInfoID = objShippingInfo.createUserShippingInfo();
                        }
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

                        if (hfShippingEnabled.Value != "")
                        {
                            if (cbShippingSame.Checked)
                            {
                                try
                                {
                                    BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                    objShippingInfo.Address = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                                    objShippingInfo.City = dtCCinfo.Rows[0]["ccInfoBCity"].ToString().Trim();
                                    objShippingInfo.State = dtCCinfo.Rows[0]["ccInfoBProvince"].ToString().Trim();
                                    objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.Name = objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                                    objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                                    objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                    objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.ZipCode = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString().Trim();
                                    objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                                    shippingInfoID = objShippingInfo.createUserShippingInfo();

                                }
                                catch (Exception ex)
                                {
                                    BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                    objShippingInfo.Address = txtShippingAddress.Text.Trim();
                                    objShippingInfo.City = txtShippingCity.Text.Trim();
                                    objShippingInfo.State = txtShippingProvince.Text.Trim();
                                    objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                                    objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                                    objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                    objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.ZipCode = txtShippingZipCode.Text.Trim();
                                    objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                                    shippingInfoID = objShippingInfo.createUserShippingInfo();
                                }
                            }
                            else
                            {
                                BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                objShippingInfo.Address = txtShippingAddress.Text.Trim();
                                objShippingInfo.City = txtShippingCity.Text.Trim();
                                objShippingInfo.State = txtShippingProvince.Text.Trim();
                                objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                                objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                                objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.ZipCode = txtShippingZipCode.Text.Trim();
                                objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                                shippingInfoID = objShippingInfo.createUserShippingInfo();
                            }
                        }

                    }
                    else if (hfShippingEnabled.Value != "")
                    {
                        if (cbShippingSame.Checked)
                        {
                            try
                            {
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
                                    BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                    objShippingInfo.Address = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString().Trim();
                                    objShippingInfo.City = dtCCinfo.Rows[0]["ccInfoBCity"].ToString().Trim();
                                    objShippingInfo.State = dtCCinfo.Rows[0]["ccInfoBProvince"].ToString().Trim();
                                    objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                                    objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                                    objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                    objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.ZipCode = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString().Trim();
                                    objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                                    shippingInfoID = objShippingInfo.createUserShippingInfo();
                                }
                                else
                                {
                                    BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                    objShippingInfo.Address = txtShippingAddress.Text.Trim();
                                    objShippingInfo.City = txtShippingCity.Text.Trim();
                                    objShippingInfo.State = txtShippingProvince.Text.Trim();
                                    objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                                    objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                                    objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                    objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                    objShippingInfo.ZipCode = txtShippingZipCode.Text.Trim();
                                    objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                                    shippingInfoID = objShippingInfo.createUserShippingInfo();
                                }
                            }
                            catch (Exception ex)
                            {
                                BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                                objShippingInfo.Address = txtShippingAddress.Text.Trim();
                                objShippingInfo.City = txtShippingCity.Text.Trim();
                                objShippingInfo.State = txtShippingProvince.Text.Trim();
                                objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                                objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                                objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                                objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                                objShippingInfo.ZipCode = txtShippingZipCode.Text.Trim();
                                objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
                                shippingInfoID = objShippingInfo.createUserShippingInfo();
                            }
                        }
                        else
                        {
                            BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                            objShippingInfo.Address = txtShippingAddress.Text.Trim();
                            objShippingInfo.City = txtShippingCity.Text.Trim();
                            objShippingInfo.State = txtShippingProvince.Text.Trim();
                            objShippingInfo.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.Name = txtShippingFirstName.Text.Trim() + " " + txtShipppingName.Text.Trim();
                            objShippingInfo.Telephone = txtShippingPhone.Text.Trim();
                            objShippingInfo.shippingCountry = ddlShippingCountry.SelectedValue.ToString();
                            objShippingInfo.userID = Convert.ToInt64(ViewState["userId"].ToString());
                            objShippingInfo.ZipCode = txtShippingZipCode.Text.Trim();
                            objShippingInfo.shippingNote = txtShippingNote.Text.Trim();
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
                DataTable dtDealCart = (DataTable)Session["dtDealCart"];
                
               
                
                double dDealTotal = 0;
                double order_ccCreditUsed = Convert.ToDouble(lblGrandTotal.Text.Trim());
                long intOrderNumnbr = 0;
                bool firstOrder = false;
                hfPayFull.Value = dUsed.ToString();

                for (int a = 0; a < dtDealCart.Rows.Count; a++)
                {
                    

                    objOrder.dealId = Convert.ToInt64(dtDealCart.Rows[a]["dealId"].ToString().Trim());                    
                    double dccCreditUsed = 0;
                    double dOneItemPrice = 0;
                    double dShippingAndTax = 0;
                    double dDealTotalTemp=0;
                    int intQty = Convert.ToInt32(dtDealCart.Rows[a]["Qty"].ToString().Trim());
                    if (Convert.ToBoolean(dtDealCart.Rows[a]["shippingAndTax"].ToString().Trim()))
                    {
                        dShippingAndTax = Convert.ToDouble(dtDealCart.Rows[a]["shippingAndTaxAmount"].ToString().Trim());
                    }
                    dOneItemPrice = Convert.ToDouble(dtDealCart.Rows[a]["sellingPrice"].ToString().Trim()) + dShippingAndTax;
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
                    if (dtDealCart.Rows[a]["isGift"].ToString().Trim()!="1")
                    {
                        for (int i = 0; i < intQty; i++)
                        {
                            try
                            {
                                objOrder.Qty = 1;
                                objOrder.giftQty = 0;
                                objOrder.personalQty = 1;
                                objOrder.status = "Successful";
                                objOrder.userId = Convert.ToInt64(ViewState["userId"].ToString());

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
                                objOrder.addressId = this.hfAddressID.Value.Trim() == "" ? 0 : int.Parse(this.hfAddressID.Value.ToString());
                                objOrder.orderIPAddress = Request.UserHostAddress.Trim();
                                intOrderNumnbr = objOrder.createNewDealOrder();

                                objDetail = new BLLDealOrderDetail();
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
                    else
                    {
                        for (int i = 0; i < intQty; i++)
                        {
                            try
                            {
                                objOrder.Qty = 1;
                                objOrder.giftQty = 1;
                                objOrder.personalQty = 0;
                                objOrder.status = "Successful";
                                objOrder.userId = Convert.ToInt64(ViewState["userId"].ToString());

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
                                objOrder.addressId = this.hfAddressID.Value.Trim() == "" ? 0 : int.Parse(this.hfAddressID.Value.ToString());
                                objOrder.orderIPAddress = Request.UserHostAddress.Trim();
                                intOrderNumnbr = objOrder.createNewDealOrder();
                                objDetail = new BLLDealOrderDetail();
                                BLLSampleVouchers objSampleVouchers = new BLLSampleVouchers();
                                objSampleVouchers.dealId = objOrder.dealId;
                                DataTable dtUnUsedVoucher = objSampleVouchers.getTop1UnusedSampleVouchersByDealID();
                                if (dtUnUsedVoucher != null && dtUnUsedVoucher.Rows.Count > 0)
                                {
                                    objDetail.dealOrderCode = dtUnUsedVoucher.Rows[0]["dealOrderCode"].ToString().Trim();
                                    objDetail.voucherSecurityCode = dtUnUsedVoucher.Rows[0]["voucherSecurityCode"].ToString().Trim();
                                    objDetail.dOrderID = intOrderNumnbr;
                                    objDetail.isGift = true;
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
                                    objDetail.isGift = true;
                                    objDetail.isRedeemed = false;
                                    objDetail.createDealOrderDetail();
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
                    Misc.SendEmail(txtEmail.Text.Trim(), "", "", ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim(), ConfigurationManager.AppSettings["EmailSubjectForNewOrderForMember"].ToString().Trim(), strOrderEmailBody);
                }
                catch (Exception ex)
                { }                             
                orderInProcess = false;
                Session["dtDealCartNew"] =(DataTable) Session["dtDealCart"];
                Session.Remove("dtTopDealCart");
                Session.Remove("dtDealCart");
                Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/orderComplete.aspx?oid=" + intOrderNumnbr.ToString() + "&od=" + order_ccCreditUsed.ToString("###.00") + "_" + Order_tastyCreditUsed.ToString("###.00") + "_" + Order_dcomissionMoneyUsed.ToString("###.00"), false);
            }
        }
        catch (Exception ex)
        {
            orderInProcess = false;
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

    private void shippingCheck()
    {
        if (cbShippingSame.Checked)
        {
            strhideShippingDiv = "none";
            RequiredFieldValidator15.Enabled = false;
            RequiredFieldValidator19.Enabled = false;
            RequiredFieldValidator16.Enabled = false;
            RequiredFieldValidator17.Enabled = false;
        }
    }


    private void Notification(DataTable dtUser, string TastyCradits)
    {

        ltrlNotify.Text = "<script type='text/javascript'>";
       
        ltrlNotify.Text += "var chkPostBack = document.getElementById('"+hfShowDiv.ClientID+"').value;";              
        ltrlNotify.Text +="if (chkPostBack == 'false'){";
        ltrlNotify.Text += "document.getElementById('"+hfShowDiv.ClientID+"').value='true';";        
        ltrlNotify.Text += "var t=setTimeout('ShowNotification()',2000);}";
       
        ltrlNotify.Text += "function ShowNotification()";
        ltrlNotify.Text += "{";
        ltrlNotify.Text += "$(document).ready(function() {";
        ltrlNotify.Text += "$('#Notify').jGrowl('Dear <b>" + dtUser.Rows[0]["firstName"].ToString().Trim() + " " + dtUser.Rows[0]["lastName"].ToString().Trim() + "</b>,<br> You can also use the <b >Tasty Credits</b> to purchase this deal. <br>Your current Tasty Credits are <b >$" + (Convert.ToDouble(TastyCradits)).ToString("###.00") + " CAD</b>', {";
        ltrlNotify.Text += "sticky: true, ";
        ltrlNotify.Text += "glue: 'before',";
        ltrlNotify.Text += "speed: 2500,";
        ltrlNotify.Text += "easing: 'easeOutBounce',";
        ltrlNotify.Text += "animateOpen: { ";
        ltrlNotify.Text += "height: 'show',";
        ltrlNotify.Text += "width: 'show'";
        ltrlNotify.Text += "},";
        ltrlNotify.Text += "animateClose: { ";
        ltrlNotify.Text += "height: 'show',";
        ltrlNotify.Text += "width: 'show'";
        ltrlNotify.Text += "}";
        ltrlNotify.Text += "});";
        ltrlNotify.Text += "});";
        ltrlNotify.Text += "}";
        ltrlNotify.Text += "</script>";
        
        
        ltrlNotify.Text += "<div id='Notify'  class='bottom-right' style='position:fixed; bottom:0px; right:0px; font-size:11px !important;'></div>";

    }
       

    private void ShowNotification(string TastyCradits)
    {
        DataTable dt = null;
        if (TastyCradits.Trim() != "")
        {

            if (Session["member"] != null)
            {
               dt = (DataTable)Session["member"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    Notification(dt, TastyCradits);

                }
            }
            else if (Session["restaurant"] != null)
            {
                dt = (DataTable)Session["restaurant"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    Notification(dt, TastyCradits);

                }

            }
            else if (Session["sale"] != null)
            {
                dt = (DataTable)Session["sale"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    Notification(dt, TastyCradits);

                }
            }
            else if (Session["user"] != null)
            {
                dt = (DataTable)Session["user"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    Notification(dt, TastyCradits);

                }
            }
        }
    }

    public string RenderOrderDetailHTML(string OrderID, double order_ccCreditUsed, double Order_tastyCreditUsed, double Order_dcomissionMoneyUsed)
    {
        DataTable dtOrdersInfo;
        DataTable dtDealCart = null;
        BLLDealOrders objOrders = new BLLDealOrders();
        GECEncryption objEnc = new GECEncryption();
        objOrders.dOrderID = Convert.ToInt64(OrderID);
        dtOrdersInfo = objOrders.getDealOrderDetailByOrderID();
        if (Session["dtDealCart"] != null)
        {
            dtDealCart = (DataTable)Session["dtDealCart"];
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
            for (int i = 0; i < dtDealCart.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtDealCart.Rows[i]["Qty"].ToString().Trim()) > 0)
                {
                    sb.Append("<tr><td style='float: left; width: 300px;'>");
                    if (dtDealCart.Rows[i]["isGift"].ToString().Trim() == "0")
                    {
                        sb.Append(dtDealCart.Rows[i]["title"].ToString());
                    }
                    else
                    {
                        sb.Append(dtDealCart.Rows[i]["title"].ToString() + " (Deal For Gift)");
                    }
                    sb.Append("</td><td style='float: left; width: 100px;'>");
                    sb.Append(dtDealCart.Rows[i]["Qty"].ToString());
                    sb.Append("</td><td style='float: left; width: 100px;'>$");
                    sb.Append(dtDealCart.Rows[i]["sellingPrice"].ToString());
                    dTempTotalOrder += Convert.ToDouble(Convert.ToDouble(dtDealCart.Rows[i]["sellingPrice"].ToString()) * Convert.ToDouble(dtDealCart.Rows[i]["Qty"].ToString()));
                    sb.Append(" CAD</td><td style='float: left; width: 100px;'><strong>$");
                    sb.Append(Convert.ToDouble(Convert.ToDouble(dtDealCart.Rows[i]["sellingPrice"].ToString()) * Convert.ToDouble(dtDealCart.Rows[i]["Qty"].ToString())).ToString("###.00"));
                    sb.Append(" CAD</strong></td></tr>");
                }
            }
            
            sb.Append("<tr><td colspan='4' style='width:100%; border-top: solid 2px gray;'>&nbsp;</td></tr>");
            sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Total</strong></td><td style='float: left; width: 100px;'>");
            sb.Append("<strong>$" + dTempTotalOrder.ToString("###.00") + " CAD</strong></td></tr>");
            if (hfShippingEnabled.Value != "")
            {
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Shipping & Tax</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + lblShippingAndTax.Text.Trim() + " CAD</strong></td></tr>");
                sb.Append("<tr><td style='float: left; width: 300px;'>&nbsp;</td><td style='float: left; width: 100px;'>&nbsp;</td><td style='float: left; width: 100px;'><strong>Grand Total</strong></td><td style='float: left; width: 100px;'>");
                sb.Append("<strong>$" + Convert.ToDouble((Convert.ToDouble(lblShippingAndTax.Text.Trim()) + dTempTotalOrder)).ToString("###.00") + " CAD</strong></td></tr>");
            }
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
                objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
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

    #region Send Email for Forgot Password

    private bool SendMailWithActiveCode(string strEmailAddress, string strPassword, string strUserName, string strUserID)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
        try
        {

            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectActivation"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'>");
            mailBody.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 600'><title>Thank You for Registering with Tastygo</title>");
            mailBody.Append("<style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 480px)' type=text/css>*{line-height: normal !important;}</style></head>");
            mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            mailBody.Append("<table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='520' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='520' bgcolor='#FFFFFF' align='left'><div style='margin: 40px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'> <strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div><div style='margin: 0px 60px 15px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            mailBody.Append("<strong>Dear " + this.txtSignUpFullName.Text.Trim() + "</strong></div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("With the power of group ordering concept, Tastygo is excited to bring you amazing deals to experience, see, eat and buy at 50%~ 90% discount around your neighbourhood!");
            mailBody.Append("</div>");

            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("To activate your account, please click the follow the link below:<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "</a>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If clicking on the link doesn't work, try copy & paste it into your browser.");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Your Account detail:");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("User Name : " + strUserName);
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("Your Password : " + strPassword.ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com'>support@tazzling.com</a> any time, or call 1855-295-1771. Our office hours are from Mon – Sun 9am – 6pm");
            mailBody.Append("</div>");
            //mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            //mailBody.Append("We wish you enjoy our deal experience.");
            //mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("</td></tr></table></td></tr></table></td></tr></table></body></html>");
            message.Body = mailBody.ToString();
            try
            { Misc.SendEmail("superadmin@tazzling.com", "", "", fromAddress, Subject, message.Body); }
            catch (Exception ex)
            { }
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);

        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblErrorMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }
    #endregion


    #region Send Email for Forgot Password
    private bool SendMailToAdmin(string strEmailAddress, string strUserName)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string toAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectMemberRequest"].ToString().Trim();
            message.IsBodyHtml = true;
            //mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            //mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");
            //mailBody.Append("<h4>Dear Admin ");
            //mailBody.Append(",</h4>");
            //mailBody.Append("<font size='3'>You have received a member request on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></font>");
            //mailBody.Append("</font>");
            //mailBody.Append("<table><tr><td>We have sent an email to member's email address to activate his/her account.</td></tr>");
            //mailBody.Append("<tr><td>Following are the detail for the new member user</td></tr>");
            //mailBody.Append("<tr><td>Email : <a href='mailto:" + strEmailAddress + "'> " + strEmailAddress + " </a></td></tr>");
            //mailBody.Append("<tr><td>User Name :  " + strUserName + "</td></tr></table>");
            //mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");

            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear Admin,</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received a member request on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>We have sent an email to member's email address to activate his/her account.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Following are the detail for the new member user</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>User Name :  " + strUserName + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Email : <a href='mailto:" + strEmailAddress + "'> " + strEmailAddress + " </a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");


            message.Body = sb.ToString();
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion
      
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

    protected void btnSaveGiftInfo_Click(object sender, ImageClickEventArgs e)
    {

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

    protected void btnAddNewCCI_Click(object sender, EventArgs e)
    {
        try
        {            
            this.btnSave.Visible = false;
            this.CancelButton.Visible = true;

            //Set the Mode here
            this.hfMode.Value = "new";

            //Show the Credit Card Personal Info here
            this.divDelivery1.Visible = true;
            //this.divDelivery2.Visible = true;
            this.divDelivery3.Visible = true;

            //Refresh the Credit Card Values
            //Set the Delivery Information
            //Set the First Name
            // this.txtFirstname.Text = "";
            //Set the Last Name
            // this.txtLastName.Text = "";
            //Set the Email                
            // this.txtEmail.Text = "";
            //Set the Confirm Email here
            // this.txtCEmailAddress.Text = "";
            txtCVNumber.Text = "";
            //Set the Billing Information here
            //Set the Business Name here
            this.txtBUserName.Text = "";
            //Set the Business Address here
            this.txtBillingAddress.Text = "";
            //Set the Business City here
            this.txtBCity.Text = "";
            //Set the Business Province name here
            this.txtProvince.Text = "";
            //Set the Business Postal Code here
            this.txtPostalCode.Text = "";
            //Set the Credit Card Number here
            this.txtCCNumber.Text = "";

            this.divlblCCN.Visible = true;
            this.divtxtCCN.Visible = true;
            ddlMonth.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            //Set the Month here
            // this.ddlMonth.SelectedValue = "01";
            //Set the Year here
            //this.ddlYear.SelectedValue = "2010";
            //Set the CVN # here
            this.txtCVNumber.Text = "";
            hfPostalCode.Value = "0";
            //this.btnAddNewCCI.Attributes.Add("onclick", "checkPostCode();");
            //ClientScript.RegisterStartupScript(this.GetType(),"watermark", "alert('I was called from Content page!')", true);
            //   this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sherazam", "alert('Hello');");
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
                txtFirstname.Text = dtUser.Rows[0]["firstName"].ToString();
                txtLastName.Text = dtUser.Rows[0]["lastName"].ToString();
                txtBUserName.Text = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
                txtEmail.Text = dtUser.Rows[0]["userName"].ToString();
                txtCEmailAddress.Text = dtUser.Rows[0]["userName"].ToString();
                txtCEmailAddress.ReadOnly = true;
                txtEmail.ReadOnly = true;               
            }



            lblMessage.Visible = false;
            imgGridMessage.Visible = false;           
        }
        catch (Exception ex)
        {
            
        }
        shippingCheck();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dtUserDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
            if (dtUserDate < DateTime.Now)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please confirm your expirary date.";                
                return;
            }

            GECEncryption objEnc = new GECEncryption();

            BLLUserCCInfo objCC = new BLLUserCCInfo();
            hfPostalCode.Value = "0";
            objCC.ccInfoID = Convert.ToInt64(this.hfCCInfoIdEdit.Value.ToString());
            objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(txtBillingAddress.Text.Trim());
            objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(txtBCity.Text.Trim());
            objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(txtPostalCode.Text.Trim());
            objCC.ccInfoBProvince = txtProvince.Text.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            else
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "<span style='color:#676767;'>Your session has been expired. Please Login to proceed checkout.</span>";
                orderInProcess = false;
                return;
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(txtCVNumber.Text.Trim()));
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            //objCC.ccInfoNumber = this.hfCCN.Value.Trim();
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(txtCCNumber.Text.Trim()));
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(txtBUserName.Text.Trim()));
            objCC.ccInfoDEmail = HtmlRemoval.StripTagsRegexCompiled(this.txtEmail.Text.Trim());
            string[] strUserName = txtBUserName.Text.Split(' ');
            objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(strUserName[0].ToString());
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = HtmlRemoval.StripTagsRegexCompiled(strUserName[1].ToString());
            }
            objCC.updateUserCCInfoByID();

            //Hide the Credit Card Personal Info here
            this.divDelivery1.Visible = false;
            //this.divDelivery2.Visible = false;
            this.divDelivery3.Visible = false;
            if (ViewState["userId"] != null && ViewState["userId"].ToString().Trim() != "")
            {
                GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
            }
            lblMessage.Text = "Credit Card Info has been updated.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
            shippingCheck();
        }
        catch (Exception ex)
        { }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        //Show the Credit Card Personal Info here
        this.divDelivery1.Visible = false;
        hfMode.Value = "grid";
        //this.divDelivery2.Visible = false;
        this.divDelivery3.Visible = false;
        hfPostalCode.Value = "0";
        shippingCheck();
    }

    protected void CheckChanged(object sender, EventArgs e)
    {


        foreach (GridViewRow gvr in gvCustomers.Rows)
        {
            RadioButton rdb = (RadioButton)gvr.FindControl("MyRadioButton");
            //Button myButton = (Button)gvr.FindControl("b1");
            if (rdb.Checked)
            {
                //Hide the Credit Card Personal Info here
                this.divDelivery1.Visible = false;
                //this.divDelivery2.Visible = false;
                this.divDelivery3.Visible = false;
                this.hfMode.Value = "grid";
            }
        }
      
        shippingCheck();
        
    }
 
}