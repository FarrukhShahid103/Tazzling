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
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;


public partial class addEditdealManagement : System.Web.UI.Page
{
    public string strIDs = "";
    public int start = 2;
    public int NewDealID;
    public static bool notExist = true;
    BLLDealOrders objOrders = new BLLDealOrders();
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            //Get the Admin User Session here
            BindSaleAccounts();
            if (Session["user"] != null)
            {
                if ((Request.QueryString["Mode"] != null) && (Request.QueryString["resID"] != null))
                {
                    try
                    {
                        if ((Request.QueryString["Mode"].ToString().Trim().ToLower() == "new") && (int.Parse(Request.QueryString["resID"].ToString()) > 0))
                        {
                            ResetAllValuesForNewEntry();                                                      
                            BindCities(0);
                        }
                        else if ((Request.QueryString["Mode"].ToString().Trim().ToLower() == "edit") && (int.Parse(Request.QueryString["resID"].ToString()) > 0) && (int.Parse(Request.QueryString["did"].ToString()) > 0))
                        {
                            ResetAllValuesForNewEntry();
                            BindCities(Convert.ToInt64(Request.QueryString["did"].ToString()));
                            GetAndShowDealInfoByDealId(Convert.ToInt64(Request.QueryString["did"].ToString()));
                            this.btnImgSave.ImageUrl = "~/admin/images/btnUpdate.jpg";

                            this.btnImgSave.ToolTip = "Update Deal Info";

                            this.hfDealId.Value = Request.QueryString["did"].ToString();

                            this.lblDealInfoHeading.Text = "Update Deal Info";

                            this.imgGridMessage.Visible = false;
                            this.lblMessage.Text = "";
                        }
                        else
                        {
                            Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), true);
                        }
                    }
                    catch (Exception ex)
                    {                       
                        Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), true);
                    }
                }
                else//If any one of "Mode" and "ResId" is Null then it will redirects you towards the Business Form
                {
                    Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), true);
                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/admin/default.aspx"), true);
            }
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    protected void BindSaleAccounts()
    {
        try
        {
            if (Request.QueryString["resID"] != null && Request.QueryString["resID"].ToString().Trim() != "")
            {
                DataTable dtResturnat = Misc.search("Select url from restaurant where restaurantId=" + Request.QueryString["resID"].ToString().Trim());
                if (dtResturnat != null && dtResturnat.Rows.Count > 0)
                {
                    lblBusinessURL.Text = dtResturnat.Rows[0][0].ToString();
                }
                else
                {
                    lblBusinessULRTitle.Visible = false;
                    lblBusinessURL.Visible = false;
                }

            }

            BLLUser obj = new BLLUser();
            DataTable dt = null;
            dt = obj.GetAllSalesAccountNames();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSalePersonAccountName.DataSource = dt;
                ddlSalePersonAccountName.DataTextField = "email";
                ddlSalePersonAccountName.DataValueField = "email";
                ddlSalePersonAccountName.DataBind();
                ddlSalePersonAccountName.Items.Insert(0, "Please Select");
            }


        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur while bind Sales Account Please contact you technical support";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

    }

    private void GetAndShowDealInfoByDealId(long iDealId)
    {
        try
        {
            BLLDeals objBLLDeals = new BLLDeals();

            //Set the Deal Id here
            objBLLDeals.DealId = iDealId;

            DataTable dtDeals = objBLLDeals.getDealByDealId();

            if ((dtDeals != null) && (dtDeals.Rows.Count > 0))
            {
                hfResturnatID.Value = dtDeals.Rows[0]["restaurantId"].ToString().Trim();

                //Set the Search Drop Down List                
                if (dtDeals.Rows[0]["shippingAndTax"] != null
                    && dtDeals.Rows[0]["shippingAndTax"].ToString().Trim() != ""
                    && Convert.ToBoolean(dtDeals.Rows[0]["shippingAndTax"].ToString()))
                {
                    cbShippingAndTax.Checked = true;
                    rfvShippingAndTax.Enabled = true;
                    txtShippingAndTax.Text = dtDeals.Rows[0]["shippingAndTaxAmount"].ToString();
                }
                else
                {
                    cbShippingAndTax.Checked = false;
                    txtShippingAndTax.Text = dtDeals.Rows[0]["shippingAndTaxAmount"].ToString();
                }
                if (dtDeals.Rows[0]["yelpRate"] != null && dtDeals.Rows[0]["yelpRate"].ToString().Trim() != "")
                {
                    try
                    {
                        ddlReviewRate.SelectedValue = dtDeals.Rows[0]["yelpRate"].ToString().Trim();
                    }
                    catch (Exception ex)
                    { }
                }
                try
                {
                    if (dtDeals.Rows[0]["doublePoints"] != null && dtDeals.Rows[0]["doublePoints"].ToString().Trim() != "")
                    {
                        cbDoublePoints.Checked = Convert.ToBoolean(dtDeals.Rows[0]["doublePoints"].ToString().Trim());
                    }
                    if (dtDeals.Rows[0]["tracking"] != null && dtDeals.Rows[0]["tracking"].ToString().Trim() != "")
                    {
                        cbTracking.Checked = Convert.ToBoolean(dtDeals.Rows[0]["tracking"].ToString().Trim());
                    }
                }
                catch (Exception ex)
                { }

                cbYelpReviews.Checked = Convert.ToBoolean(dtDeals.Rows[0]["reviewExist"].ToString().Trim());
                txtReviewLink.Text = dtDeals.Rows[0]["yelpLink"].ToString().Trim();
                txtReviewText.Text = dtDeals.Rows[0]["yelpText"].ToString().Trim();

                this.txtTitle.Text = dtDeals.Rows[0]["Title"].ToString().Trim();
                this.txtURLTitle.Text = dtDeals.Rows[0]["urlTitle"].ToString().Trim();
                this.txtTopTitle.Text = dtDeals.Rows[0]["topTitle"].ToString().Trim();
                txtShortTitle.Text = dtDeals.Rows[0]["shortTitle"].ToString().Trim();
                this.txtFinePrint.Text = dtDeals.Rows[0]["FinePrint"].ToString().Trim().Replace("<br>", "\n");
                this.txtHowToUse.Text = dtDeals.Rows[0]["howtouse"].ToString().Trim().Replace("<br>", "\n");

                this.txtDealHightlights.Text = dtDeals.Rows[0]["DealHightlights"].ToString().Trim().Replace("<br>", "\n"); ;
                this.elm1.Text = dtDeals.Rows[0]["Description"].ToString().Trim();
                this.txtDealPrice.Text = dtDeals.Rows[0]["SellingPrice"].ToString().Trim();
                this.txtActualPrice.Text = dtDeals.Rows[0]["ValuePrice"].ToString().Trim();

                //Get the Starting Date of the Deal here              
                if (dtDeals.Rows[0]["voucherExpiryDate"] != null && dtDeals.Rows[0]["voucherExpiryDate"].ToString().Trim() != "")
                {
                    txtVoucherExpiryDate.Text = DateTime.Parse(dtDeals.Rows[0]["voucherExpiryDate"].ToString().Trim()).ToString("MM-dd-yyyy");
                    rfvExpiryDate.Enabled = true;
                }
                else
                {
                    txtVoucherExpiryDate.Text = "";
                    cbNoExpiryDate.Checked = true;
                    rfvExpiryDate.Enabled = false;
                }

                this.txtMinNoOfOrders.Text = dtDeals.Rows[0]["DealDelMinLmt"].ToString().Trim();
                this.txtMaxNoOfOrders.Text = dtDeals.Rows[0]["DealDelMaxLmt"].ToString().Trim();

                this.ddlStatus.SelectedValue = DBNull.Value.Equals(dtDeals.Rows[0]["DealStatus"]) ? "No" : (bool.Parse(dtDeals.Rows[0]["DealStatus"].ToString()) == true ? "Yes" : "No");


                this.txtMaxOrdersPerUser.Text = dtDeals.Rows[0]["MaxOrdersPerUser"].ToString().Trim();

                this.txtMaxGiftsPerOrder.Text = dtDeals.Rows[0]["maxGiftsPerOrder"] == DBNull.Value ? "0" : dtDeals.Rows[0]["maxGiftsPerOrder"].ToString().Trim() == "" ? "0" : dtDeals.Rows[0]["maxGiftsPerOrder"].ToString().Trim();

                this.txtOurComission.Text = dtDeals.Rows[0]["OurCommission"] == DBNull.Value ? "0" : dtDeals.Rows[0]["OurCommission"].ToString().Trim() == "" ? "0" : dtDeals.Rows[0]["OurCommission"].ToString().Trim();
                try
                {
                    this.ddlSalePersonAccountName.SelectedValue = dtDeals.Rows[0]["salePersonAccountName"] == DBNull.Value ? "1" : dtDeals.Rows[0]["salePersonAccountName"].ToString();
                }
                catch (Exception ex)
                { }

                //Set the Deal Slot here
                // this.ddlSideDeal.SelectedValue = dtDeals.Rows[0]["dealSlot"] == DBNull.Value ? "1" : dtDeals.Rows[0]["dealSlot"].ToString();

                //Set the Affiliate Commission here


                string strImageNames = dtDeals.Rows[0]["images"].ToString().Trim();

                ArrayList arrImages = new ArrayList();
                arrImages.AddRange(strImageNames.Split(','));

                string strRedID = dtDeals.Rows[0]["restaurantId"].ToString().Trim();

                if (arrImages.Count > 0)
                {
                    string strImageName = arrImages[0].ToString();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strRedID + "\\" + strImageName;

                    if (File.Exists(path))
                    {
                        this.rfvDealImage1.ValidationGroup = "";

                        //Set the First Image here
                        this.imgUpload1.Src = "../Images/dealFood/" + strRedID + "/" + strImageName;
                        this.imgUpload1.Visible = true;
                    }
                }
                else
                {
                    imgUpload1.Src = "";
                }

                if (arrImages.Count > 1)
                {
                    string strImageName = arrImages[1].ToString();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strRedID + "\\" + strImageName;

                    if (File.Exists(path))
                    {
                        //Set the Second Image here
                        this.imgUpload2.Src = "../Images/dealFood/" + strRedID + "/" + strImageName;
                        this.imgUpload2.Visible = true;
                        imgUpload2Remove.Visible = true;
                    }
                }
                else
                {
                    imgUpload2.Src = "";
                }

                if (arrImages.Count > 2)
                {
                    string strImageName = arrImages[2].ToString();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strRedID + "\\" + strImageName;

                    if (File.Exists(path))
                    {
                        //Set the Third Image here
                        this.imgUpload3.Src = "../Images/dealFood/" + strRedID + "/" + strImageName;
                        this.imgUpload3.Visible = true;
                        imgUpload3Remove.Visible = true;
                    }
                }
                else
                {
                    imgUpload3.Src = "";
                }

                //Means that no Sub-Deal Exists into the database
                if (dtDeals.Rows.Count == 1)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = "0";
                    hfDealOrder2.Value = "0";
                    hfDealOrder3.Value = "0";
                    hfDealOrder4.Value = "0";
                    hfDealOrder5.Value = "0";
                    hfDealOrder6.Value = "0";
                    hfDealOrder7.Value = "0";
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";
                    
                    //Hide the First Sub Deal
                    this.divFirstSubDeal.Visible = false;
                    //Hide the Second Sub Deal
                    this.divSecondSubDeal.Visible = false;
                    //Hide the Third Sub Deal
                    this.divThirdSubDeal.Visible = false;
                    //Hide the Forth sub Deal
                    this.divForthSubDeal.Visible = false;

                    this.divFifthSubDeal.Visible = false;

                    this.divSixthSubDeal.Visible = false;

                    this.divSeventhSubDeal.Visible = false;

                    this.divEightSubDeal.Visible = false;

                    this.divNinthSubDeal.Visible = false;

                    this.divTenthSubDeal.Visible = false;


                    //Show the Add New Sub Deal button
                    this.lBtn0AddThirdDiv.Visible = true;

                    //Set the First sub deal Hidden Field Deal ID
                    this.hfDealId1.Value = "0";
                    this.hfDealId2.Value = "0";
                    this.hfDealId3.Value = "0";
                    this.hfDealId4.Value = "0";
                    this.hfDealId5.Value = "0";
                    this.hfDealId6.Value = "0";
                    this.hfDealId7.Value = "0";
                    this.hfDealId8.Value = "0";
                    this.hfDealId9.Value = "0";
                    this.hfDealId10.Value = "0";

                }

                //Show the First Sub-Deal info here
                if (dtDeals.Rows.Count > 1)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = "0"; 
                    hfDealOrder3.Value = "0";
                    hfDealOrder4.Value = "0";
                    hfDealOrder5.Value = "0";
                    hfDealOrder6.Value = "0";
                    hfDealOrder7.Value = "0";
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";

                    this.lBtn0AddThirdDiv.Visible = false;

                    this.divFirstSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId1.Value = dtDeals.Rows[1]["dealId"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle1.Text = dtDeals.Rows[1]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice1.Text = dtDeals.Rows[1]["SellingPrice"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice1.Text = dtDeals.Rows[1]["ValuePrice"].ToString().Trim();

                    this.txtMaxNoOfOrders1.Text = dtDeals.Rows[1]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Deal Page Title here
                    this.txtTitleMain.Text = dtDeals.Rows[0]["dealPageTitle"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[1]["shippingAndTax"] != null
                        && dtDeals.Rows[1]["shippingAndTax"].ToString().Trim() != ""
                        && Convert.ToBoolean(dtDeals.Rows[1]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax1.Checked = true;
                        rfvShippingAndTax1.Enabled = true;
                        txtShippingAndTax1.Text = dtDeals.Rows[1]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax1.Checked = false;
                        txtShippingAndTax1.Text = dtDeals.Rows[1]["shippingAndTaxAmount"].ToString();
                    }

                    this.lBtn1AddThirdDiv.Visible = true;                    
                }

                //Show the Second Sub-Deal info here
                if (dtDeals.Rows.Count > 2)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = "0"; 
                    hfDealOrder4.Value = "0";
                    hfDealOrder5.Value = "0";
                    hfDealOrder6.Value = "0";
                    hfDealOrder7.Value = "0";
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";

                    this.lBtn1AddThirdDiv.Visible = false;
                    this.lBtn1DeleteSecondDiv.Visible = false;

                    this.divSecondSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId2.Value = dtDeals.Rows[2]["dealId"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle2.Text = dtDeals.Rows[2]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice2.Text = dtDeals.Rows[2]["SellingPrice"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice2.Text = dtDeals.Rows[2]["ValuePrice"].ToString().Trim();

                    this.txtMaxNoOfOrders2.Text = dtDeals.Rows[2]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[2]["shippingAndTax"] != null
                       && dtDeals.Rows[2]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[2]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax2.Checked = true;
                        rfvShippingAndTax2.Enabled = true;
                        txtShippingAndTax2.Text = dtDeals.Rows[2]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax2.Checked = false;
                        txtShippingAndTax2.Text = dtDeals.Rows[2]["shippingAndTaxAmount"].ToString();
                    }

                    this.lBtn2AddThirdDiv.Visible = true;                    
                }

                //Show the Third Sub-Deal info here
                if (dtDeals.Rows.Count > 3)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = "0"; 
                    hfDealOrder5.Value = "0";
                    hfDealOrder6.Value = "0";
                    hfDealOrder7.Value = "0";
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";
                    if (hfDealOrder3.Value.Trim() == "0")
                    {
                        this.lBtn3DeleteSecondDiv.Visible = true;
                    }
                    else
                    {
                        this.lBtn3DeleteSecondDiv.Visible = false;
                    }
                    //this.lBtn3DeleteSecondDiv.Visible = true;
                    this.lBtn3AddThirdDiv.Visible = true;
                    this.lBtn2AddThirdDiv.Visible = false;
                    this.lBtn2DeleteSecondDiv.Visible = false;

                    this.divThirdSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId3.Value = dtDeals.Rows[3]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice3.Text = dtDeals.Rows[3]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle3.Text = dtDeals.Rows[3]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice3.Text = dtDeals.Rows[3]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders3.Text = dtDeals.Rows[3]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[3]["shippingAndTax"] != null
                       && dtDeals.Rows[3]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[3]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax3.Checked = true;
                        rfvShippingAndTax3.Enabled = true;
                        txtShippingAndTax3.Text = dtDeals.Rows[3]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax3.Checked = false;
                        txtShippingAndTax3.Text = dtDeals.Rows[3]["shippingAndTaxAmount"].ToString();
                    }
                }

                //Show the Forth Sub-Deal info here
                if (dtDeals.Rows.Count > 4)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = dtDeals.Rows[4]["Order"].ToString().Trim();
                    hfDealOrder5.Value = "0";
                    hfDealOrder6.Value = "0";
                    hfDealOrder7.Value = "0";
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";
                   
                    //this.lBtn4DeleteSecondDiv.Visible = true;
                    lBtn4AddThirdDiv.Visible = true;
                    this.lBtn3DeleteSecondDiv.Visible = false;
                    this.lBtn3AddThirdDiv.Visible = false;

                    this.divForthSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId4.Value = dtDeals.Rows[4]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice4.Text = dtDeals.Rows[4]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle4.Text = dtDeals.Rows[4]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice4.Text = dtDeals.Rows[4]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders4.Text = dtDeals.Rows[4]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[4]["shippingAndTax"] != null
                       && dtDeals.Rows[4]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[4]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax4.Checked = true;
                        rfvShippingAndTax4.Enabled = true;
                        txtShippingAndTax4.Text = dtDeals.Rows[4]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax4.Checked = false;
                        txtShippingAndTax4.Text = dtDeals.Rows[4]["shippingAndTaxAmount"].ToString();
                    }
                }
                if (dtDeals.Rows.Count > 5)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = dtDeals.Rows[4]["Order"].ToString().Trim();
                    hfDealOrder5.Value = dtDeals.Rows[5]["Order"].ToString().Trim();
                    hfDealOrder6.Value = "0";
                    hfDealOrder7.Value = "0";
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";
                    
                    lBtn5AddThirdDiv.Visible = true;
                    this.lBtn4DeleteSecondDiv.Visible = false;
                    this.lBtn4AddThirdDiv.Visible = false;

                    this.divFifthSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId5.Value = dtDeals.Rows[5]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice5.Text = dtDeals.Rows[5]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle5.Text = dtDeals.Rows[5]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice5.Text = dtDeals.Rows[5]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders5.Text = dtDeals.Rows[5]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[5]["shippingAndTax"] != null
                       && dtDeals.Rows[5]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[5]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax5.Checked = true;
                        rfvShippingAndTax5.Enabled = true;
                        txtShippingAndTax5.Text = dtDeals.Rows[5]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax5.Checked = false;
                        txtShippingAndTax5.Text = dtDeals.Rows[5]["shippingAndTaxAmount"].ToString();
                    }
                }
                if (dtDeals.Rows.Count > 6)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = dtDeals.Rows[4]["Order"].ToString().Trim();
                    hfDealOrder5.Value = dtDeals.Rows[5]["Order"].ToString().Trim();
                    hfDealOrder6.Value = dtDeals.Rows[6]["Order"].ToString().Trim();
                    hfDealOrder7.Value = "0";
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";
                   
                    lBtn6AddThirdDiv.Visible = true;
                    this.lBtn5DeleteSecondDiv.Visible = false;
                    this.lBtn5AddThirdDiv.Visible = false;

                    this.divSixthSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId6.Value = dtDeals.Rows[6]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice6.Text = dtDeals.Rows[6]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle6.Text = dtDeals.Rows[6]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice6.Text = dtDeals.Rows[6]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders6.Text = dtDeals.Rows[6]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[6]["shippingAndTax"] != null
                       && dtDeals.Rows[6]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[6]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax6.Checked = true;
                        rfvShippingAndTax6.Enabled = true;
                        txtShippingAndTax6.Text = dtDeals.Rows[6]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax6.Checked = false;
                        txtShippingAndTax6.Text = dtDeals.Rows[6]["shippingAndTaxAmount"].ToString();
                    }
                }
                if (dtDeals.Rows.Count > 7)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = dtDeals.Rows[4]["Order"].ToString().Trim();
                    hfDealOrder5.Value = dtDeals.Rows[5]["Order"].ToString().Trim();
                    hfDealOrder6.Value = dtDeals.Rows[6]["Order"].ToString().Trim();
                    hfDealOrder7.Value = dtDeals.Rows[7]["Order"].ToString().Trim();
                    hfDealOrder8.Value = "0";
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";
                   
                    lBtn7AddThirdDiv.Visible = true;
                    this.lBtn6DeleteSecondDiv.Visible = false;
                    this.lBtn6AddThirdDiv.Visible = false;

                    this.divSeventhSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId7.Value = dtDeals.Rows[7]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice7.Text = dtDeals.Rows[7]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle7.Text = dtDeals.Rows[7]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice7.Text = dtDeals.Rows[7]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders7.Text = dtDeals.Rows[7]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[7]["shippingAndTax"] != null
                       && dtDeals.Rows[7]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[7]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax7.Checked = true;
                        rfvShippingAndTax7.Enabled = true;
                        txtShippingAndTax7.Text = dtDeals.Rows[7]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax7.Checked = false;
                        txtShippingAndTax7.Text = dtDeals.Rows[7]["shippingAndTaxAmount"].ToString();
                    }
                }
                if (dtDeals.Rows.Count > 8)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = dtDeals.Rows[4]["Order"].ToString().Trim();
                    hfDealOrder5.Value = dtDeals.Rows[5]["Order"].ToString().Trim();
                    hfDealOrder6.Value = dtDeals.Rows[6]["Order"].ToString().Trim();
                    hfDealOrder7.Value = dtDeals.Rows[7]["Order"].ToString().Trim();
                    hfDealOrder8.Value = dtDeals.Rows[8]["Order"].ToString().Trim();
                    hfDealOrder9.Value = "0";
                    hfDealOrder10.Value = "0";
                   
                    lBtn8AddThirdDiv.Visible = true;
                    this.lBtn7DeleteSecondDiv.Visible = false;
                    this.lBtn7AddThirdDiv.Visible = false;

                    this.divEightSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId8.Value = dtDeals.Rows[8]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice8.Text = dtDeals.Rows[8]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle8.Text = dtDeals.Rows[8]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice8.Text = dtDeals.Rows[8]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders8.Text = dtDeals.Rows[8]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[8]["shippingAndTax"] != null
                       && dtDeals.Rows[8]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[8]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax8.Checked = true;
                        rfvShippingAndTax8.Enabled = true;
                        txtShippingAndTax8.Text = dtDeals.Rows[8]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax8.Checked = false;
                        txtShippingAndTax8.Text = dtDeals.Rows[8]["shippingAndTaxAmount"].ToString();
                    }
                }
                if (dtDeals.Rows.Count > 9)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = dtDeals.Rows[4]["Order"].ToString().Trim();
                    hfDealOrder5.Value = dtDeals.Rows[5]["Order"].ToString().Trim();
                    hfDealOrder6.Value = dtDeals.Rows[6]["Order"].ToString().Trim();
                    hfDealOrder7.Value = dtDeals.Rows[7]["Order"].ToString().Trim();
                    hfDealOrder8.Value = dtDeals.Rows[8]["Order"].ToString().Trim();
                    hfDealOrder9.Value = dtDeals.Rows[9]["Order"].ToString().Trim();
                    hfDealOrder10.Value = "0";
                    
                    lBtn9AddThirdDiv.Visible = true;
                    this.lBtn8DeleteSecondDiv.Visible = false;
                    this.lBtn8AddThirdDiv.Visible = false;

                    this.divNinthSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId9.Value = dtDeals.Rows[9]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice9.Text = dtDeals.Rows[9]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle9.Text = dtDeals.Rows[9]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice9.Text = dtDeals.Rows[9]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders9.Text = dtDeals.Rows[9]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[9]["shippingAndTax"] != null
                       && dtDeals.Rows[9]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[9]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax9.Checked = true;
                        rfvShippingAndTax9.Enabled = true;
                        txtShippingAndTax9.Text = dtDeals.Rows[9]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax9.Checked = false;
                        txtShippingAndTax9.Text = dtDeals.Rows[9]["shippingAndTaxAmount"].ToString();
                    }
                }
                if (dtDeals.Rows.Count > 10)
                {
                    hfDealOrder0.Value = dtDeals.Rows[0]["Order"].ToString().Trim();
                    hfDealOrder1.Value = dtDeals.Rows[1]["Order"].ToString().Trim();
                    hfDealOrder2.Value = dtDeals.Rows[2]["Order"].ToString().Trim();
                    hfDealOrder3.Value = dtDeals.Rows[3]["Order"].ToString().Trim();
                    hfDealOrder4.Value = dtDeals.Rows[4]["Order"].ToString().Trim();
                    hfDealOrder5.Value = dtDeals.Rows[5]["Order"].ToString().Trim();
                    hfDealOrder6.Value = dtDeals.Rows[6]["Order"].ToString().Trim();
                    hfDealOrder7.Value = dtDeals.Rows[7]["Order"].ToString().Trim();
                    hfDealOrder8.Value = dtDeals.Rows[8]["Order"].ToString().Trim();
                    hfDealOrder9.Value = dtDeals.Rows[9]["Order"].ToString().Trim();
                    hfDealOrder10.Value = dtDeals.Rows[10]["Order"].ToString().Trim();
                    
                    this.lBtn9DeleteSecondDiv.Visible = false;
                    this.lBtn9AddThirdDiv.Visible = false;

                    this.divTenthSubDeal.Visible = true;

                    //Set the Sub-Deal Id into the Hidden Field Deal ID
                    this.hfDealId10.Value = dtDeals.Rows[10]["dealId"].ToString().Trim();

                    //Set the Value Price here
                    this.txtActualPrice10.Text = dtDeals.Rows[10]["ValuePrice"].ToString().Trim();

                    //Set the Sub-Title here
                    this.txtTitle10.Text = dtDeals.Rows[10]["Title"].ToString().Trim();

                    //Set the Selling Price here
                    this.txtDealPrice10.Text = dtDeals.Rows[10]["SellingPrice"].ToString().Trim();

                    this.txtMaxNoOfOrders10.Text = dtDeals.Rows[10]["DealDelMaxLmt"].ToString().Trim();

                    //Set the Affiliate Commission here


                    if (dtDeals.Rows[10]["shippingAndTax"] != null
                       && dtDeals.Rows[10]["shippingAndTax"].ToString().Trim() != ""
                       && Convert.ToBoolean(dtDeals.Rows[10]["shippingAndTax"].ToString()))
                    {
                        cbShippingAndTax10.Checked = true;
                        rfvShippingAndTax10.Enabled = true;
                        txtShippingAndTax10.Text = dtDeals.Rows[10]["shippingAndTaxAmount"].ToString();
                    }
                    else
                    {
                        cbShippingAndTax10.Checked = false;
                        txtShippingAndTax10.Text = dtDeals.Rows[10]["shippingAndTaxAmount"].ToString();
                    }
                }

                if (hfDealOrder0.Value.Trim() != "0" || hfDealOrder1.Value.Trim() != "0" || hfDealOrder2.Value.Trim() != "0"
                        || hfDealOrder3.Value.Trim() != "0" || hfDealOrder4.Value.Trim() != "0" || hfDealOrder5.Value.Trim() != "0"
                        || hfDealOrder6.Value.Trim() != "0" || hfDealOrder7.Value.Trim() != "0" || hfDealOrder8.Value.Trim() != "0"
                        || hfDealOrder9.Value.Trim() != "0" || hfDealOrder10.Value.Trim() != "0")
                {
                    this.lBtn1DeleteSecondDiv.Visible = false;
                    this.lBtn2DeleteSecondDiv.Visible = false;
                    this.lBtn3DeleteSecondDiv.Visible = false;
                    this.lBtn4DeleteSecondDiv.Visible = false;
                    this.lBtn5DeleteSecondDiv.Visible = false;
                    this.lBtn6DeleteSecondDiv.Visible = false;
                    this.lBtn7DeleteSecondDiv.Visible = false;
                    this.lBtn8DeleteSecondDiv.Visible = false;
                    this.lBtn9DeleteSecondDiv.Visible = false;
                    this.lBtn10DeleteSecondDiv.Visible = false;
                }
                else
                {
                    this.lBtn1DeleteSecondDiv.Visible = true;
                    this.lBtn2DeleteSecondDiv.Visible = true;
                    this.lBtn3DeleteSecondDiv.Visible = true;
                    this.lBtn4DeleteSecondDiv.Visible = true;
                    this.lBtn5DeleteSecondDiv.Visible = true;
                    this.lBtn6DeleteSecondDiv.Visible = true;
                    this.lBtn7DeleteSecondDiv.Visible = true;
                    this.lBtn8DeleteSecondDiv.Visible = true;
                    this.lBtn9DeleteSecondDiv.Visible = true;
                    this.lBtn10DeleteSecondDiv.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtURLTitle_Changed(object sender, EventArgs e)
    {
        try
        {          

            checkExpiryCheck();
            for (int i = 0; i < dlCities.Items.Count; i++)
            {
                try
                {
                    CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                    Panel pnlDealTimeDetail = (Panel)dlCities.Items[i].FindControl("pnlDealTimeDetail");
                    if (chk != null && chk.Checked)
                    {
                        if (pnlDealTimeDetail != null)
                        {
                            pnlDealTimeDetail.CssClass = "showTimeDetail";
                        }
                    }
                }
                catch (Exception ex)
                { }
            }

            BLLDeals objdeals = new BLLDeals();
            objdeals.urlTitle = txtURLTitle.Text.Trim();

            DataTable dtDeal = objdeals.getDealinfoByURLTitle();
            if (dtDeal != null && dtDeal.Rows.Count > 0)
            {
                if (hfDealId.Value.Trim() != dtDeal.Rows[0]["dealId"].ToString().Trim())
                {
                    notExist = false;
                    lblMessage.Text = "Deal with this Title already exist Please change your URL Title";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    notExist = true;
                    lblMessage.Text = "";
                    lblMessage.Visible = false;
                    imgGridMessage.Visible = false;
                }
            }
            else
            {
                notExist = true;
                lblMessage.Text = "";
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }

    private void checkExpiryCheck()
    {
        if (cbNoExpiryDate.Checked)
        {
            rfvExpiryDate.Enabled = false;
        }
        else
        {
            rfvExpiryDate.Enabled = true;
        }

        if (hfDealOrder0.Value.Trim() != "0" || hfDealOrder1.Value.Trim() != "0" || hfDealOrder2.Value.Trim() != "0"
                        || hfDealOrder3.Value.Trim() != "0" || hfDealOrder4.Value.Trim() != "0" || hfDealOrder5.Value.Trim() != "0"
                        || hfDealOrder6.Value.Trim() != "0" || hfDealOrder7.Value.Trim() != "0" || hfDealOrder8.Value.Trim() != "0"
                        || hfDealOrder9.Value.Trim() != "0" || hfDealOrder10.Value.Trim() != "0")
        {
            this.lBtn1DeleteSecondDiv.Visible = false;
            this.lBtn2DeleteSecondDiv.Visible = false;
            this.lBtn3DeleteSecondDiv.Visible = false;
            this.lBtn4DeleteSecondDiv.Visible = false;
            this.lBtn5DeleteSecondDiv.Visible = false;
            this.lBtn6DeleteSecondDiv.Visible = false;
            this.lBtn7DeleteSecondDiv.Visible = false;
            this.lBtn8DeleteSecondDiv.Visible = false;
            this.lBtn9DeleteSecondDiv.Visible = false;
            this.lBtn10DeleteSecondDiv.Visible = false;
        }
        else
        {
            this.lBtn1DeleteSecondDiv.Visible = true;
            this.lBtn2DeleteSecondDiv.Visible = true;
            this.lBtn3DeleteSecondDiv.Visible = true;
            this.lBtn4DeleteSecondDiv.Visible = true;
            this.lBtn5DeleteSecondDiv.Visible = true;
            this.lBtn6DeleteSecondDiv.Visible = true;
            this.lBtn7DeleteSecondDiv.Visible = true;
            this.lBtn8DeleteSecondDiv.Visible = true;
            this.lBtn9DeleteSecondDiv.Visible = true;
            this.lBtn10DeleteSecondDiv.Visible = true;
        }
    }

    DataTable dtSubscribedCityList, DataTableAllCities = new DataTable();

    private void BindCities(long dealID)
    {
        if (dealID != 0)
        {
            BLLDealCity objSub = new BLLDealCity();
            objSub.DealId = Convert.ToInt32(dealID);
            dtSubscribedCityList = objSub.getDealCityListByDealId();
        }
        else
        {
            dtSubscribedCityList = null;
        }
        BLLCities objCities = new BLLCities();
        DataRow dRow;
        DataTableAllCities = new DataTable();
        DataColumn cityId = new DataColumn("cityId");
        DataColumn cityname = new DataColumn("cityname");
        DataTableAllCities.Columns.Add(cityId);
        DataTableAllCities.Columns.Add(cityname);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "8129";
        dRow["cityname"] = "National Deal";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1710";
        dRow["cityname"] = "Abbotsford";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1720";
        dRow["cityname"] = "Brampton";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1376";
        dRow["cityname"] = "Calgary";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1709";
        dRow["cityname"] = "Edmonton";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1716";
        dRow["cityname"] = "Halifax";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1722";
        dRow["cityname"] = "Hamilton";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1726";
        dRow["cityname"] = "Mississauga";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1727";
        dRow["cityname"] = "Oakville - Burlington";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1729";
        dRow["cityname"] = "St. Catharines";
        DataTableAllCities.Rows.Add(dRow);


        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1712";
        dRow["cityname"] = "Surrey";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "337";
        dRow["cityname"] = "Vancouver";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1713";
        dRow["cityname"] = "Victoria";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "338";
        dRow["cityname"] = "Toronto";
        DataTableAllCities.Rows.Add(dRow);

        dRow = DataTableAllCities.NewRow();
        dRow["cityId"] = "1733";
        dRow["cityname"] = "York Region";
        DataTableAllCities.Rows.Add(dRow);

        


        //DataTableAllCities = Misc.getAllCitiesWithProvinceAndCountryInfoByCountryID(2);
        //DataRow dRow = DataTableAllCities.NewRow();
        //dRow["cityID"] = "1";
        //dRow["CityName"] = "National Deal";
        //DataTableAllCities.Rows.InsertAt(dRow, 0);    
        dlCities.DataSource = DataTableAllCities;
        dlCities.DataBind();

        if (dlCities != null && dlCities.Items.Count > 0)
        {
            DropDownList ddlDLSideDeal = (DropDownList)dlCities.Items[0].FindControl("ddlDLSideDeal");
            Label lblDLSelectSlot = (Label)dlCities.Items[0].FindControl("lblDLSelectSlot");

            if (ddlDLSideDeal != null && lblDLSelectSlot != null)
            {
                ddlDLSideDeal.Visible = false;
                lblDLSelectSlot.Visible = false;
            }
        }

    }

    protected void dlCities_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        TextBox txtdlStartDate = (TextBox)(e.Item.FindControl("txtdlStartDate"));
        if (txtdlStartDate != null)
        {
            txtdlStartDate.Attributes.Add("onclick", "OpenCalendar('" + txtdlStartDate.ClientID + "');");
        }
        TextBox txtDLEndDate = (TextBox)(e.Item.FindControl("txtDLEndDate"));
        if (txtDLEndDate != null)
        {
            txtDLEndDate.Attributes.Add("onclick", "OpenCalendar('" + txtDLEndDate.ClientID + "');");
        }

        System.Web.UI.WebControls.CheckBox chk;
        chk = (System.Web.UI.WebControls.CheckBox)(e.Item.FindControl("chkbxSelect"));
        Panel pnlDealTimeDetail = (Panel)e.Item.FindControl("pnlDealTimeDetail");
        if (chk != null)
        {

            if (pnlDealTimeDetail != null)
            {
                chk.Attributes.Add("onclick", "hideShowDiv('" + pnlDealTimeDetail.ClientID + "');");
                if (dtSubscribedCityList == null)
                { pnlDealTimeDetail.CssClass = "hideTimeDetail"; }
            }
        }

        if (dtSubscribedCityList != null)
        {
            Label lblCityID = (Label)(e.Item.FindControl("lblCityID"));
            Label lblCityName = (Label)(e.Item.FindControl("lblCity"));
            try
            {
                if (lblCityID != null && chk != null)
                {
                    for (int j = 0; j < dtSubscribedCityList.Rows.Count; j++)
                    {

                        if (lblCityID.Text.Trim() == dtSubscribedCityList.Rows[j]["cityid"].ToString().Trim())
                        {
                            pnlDealTimeDetail.CssClass = "showTimeDetail";
                            chk.Checked = true;
                            DropDownList ddlDLSideDeal = (DropDownList)(e.Item.FindControl("ddlDLSideDeal"));
                            if (ddlDLSideDeal != null &&
                                dtSubscribedCityList.Rows[j]["dealSlotC"] != null &&
                                dtSubscribedCityList.Rows[j]["dealSlotC"].ToString().Trim() != "")
                            {
                                try
                                {
                                    ddlDLSideDeal.SelectedValue = dtSubscribedCityList.Rows[j]["dealSlotC"].ToString().Trim();
                                }
                                catch (Exception ex)
                                { }
                            }
                            if (dtSubscribedCityList.Rows[j]["dealStartTimeC"] != null
                                && dtSubscribedCityList.Rows[j]["dealStartTimeC"].ToString().Trim() != "")
                            {
                                try
                                {
                                    string strStartDate = dtSubscribedCityList.Rows[j]["dealStartTimeC"].ToString().Trim();

                                    DropDownList ddlDLStartHH = (DropDownList)(e.Item.FindControl("ddlDLStartHH"));
                                    DropDownList ddlDLStartMM = (DropDownList)(e.Item.FindControl("ddlDLStartMM"));
                                    DropDownList ddlDLStartPortion = (DropDownList)(e.Item.FindControl("ddlDLStartPortion"));
                                    if (ddlDLStartHH != null
                                        && ddlDLStartMM != null
                                        && ddlDLStartPortion != null)
                                    {
                                        txtdlStartDate.Text = DateTime.Parse(strStartDate).ToString("MM-dd-yyyy");
                                        if (DateTime.Parse(strStartDate).Hour < 12)
                                        {
                                            ddlDLStartHH.SelectedValue = DateTime.Parse(strStartDate).Hour.ToString();
                                            ddlDLStartPortion.SelectedValue = "AM";
                                        }
                                        else
                                        {
                                            ddlDLStartHH.SelectedValue = (DateTime.Parse(strStartDate).Hour - 12).ToString();
                                            ddlDLStartPortion.SelectedValue = "PM";
                                        }
                                        ddlDLStartMM.SelectedValue = DateTime.Parse(strStartDate).Minute.ToString();
                                    }

                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            if (dtSubscribedCityList.Rows[j]["dealEndTimeC"] != null
                                && dtSubscribedCityList.Rows[j]["dealEndTimeC"].ToString().Trim() != "")
                            {
                                try
                                {
                                    string strEndDate = dtSubscribedCityList.Rows[j]["dealEndTimeC"].ToString().Trim();

                                    DropDownList ddlDLEndHH = (DropDownList)(e.Item.FindControl("ddlDLEndHH"));
                                    DropDownList ddlDLEndMM = (DropDownList)(e.Item.FindControl("ddlDLEndMM"));
                                    DropDownList ddlDLEndPortion = (DropDownList)(e.Item.FindControl("ddlDLEndPortion"));
                                    if (ddlDLEndHH != null
                                        && ddlDLEndMM != null
                                        && ddlDLEndPortion != null)
                                    {

                                        txtDLEndDate.Text = DateTime.Parse(strEndDate).ToString("MM-dd-yyyy");
                                        if (DateTime.Parse(strEndDate).Hour < 12)
                                        {
                                            ddlDLEndHH.SelectedValue = DateTime.Parse(strEndDate).Hour.ToString();
                                            ddlDLEndPortion.SelectedValue = "AM";
                                        }
                                        else
                                        {
                                            ddlDLEndHH.SelectedValue = (DateTime.Parse(strEndDate).Hour - 12).ToString();
                                            ddlDLEndPortion.SelectedValue = "PM";
                                        }
                                        ddlDLEndMM.SelectedValue = DateTime.Parse(strEndDate).Minute.ToString();
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            break;
                        }
                        else
                        {
                            pnlDealTimeDetail.CssClass = "hideTimeDetail";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            }
        }

    }

    private void GetAndSetUserID()
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];

            if ((dtUser != null) && (dtUser.Rows.Count > 0))
            {
                ViewState["userID"] = dtUser.Rows[0]["userID"];
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #region "Get All Deal Info & Fill the GridView"

    public string getImagePath(object resID, object imgName)
    {
        try
        {
            ArrayList arrImage = new ArrayList();
            arrImage.AddRange(imgName.ToString().Split(','));

            if (arrImage.Count > 0)
            {
                string strImageName = arrImage[0].ToString();

                string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + resID.ToString() + "\\" + strImageName;
                if (File.Exists(path))
                {
                    return "../Images/dealFood/" + resID.ToString() + "/" + strImageName;
                }
                else
                {
                    return "../Images/dealFood/noMenuImage.gif";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return "";
        }
        return "";
    }

    #endregion

    private bool AlreadyDealExistsWithSameStartEndTimeWithCityName(int iResId, string strCurrentDealID)
    {
        if (!cbForDesign.Checked)
        {            

            bool bStatus = true;
            BLLDealCity objCity = new BLLDealCity();
            int checkedCity = 0;
            int dealExist = 0;

            for (int i = 0; i < dlCities.Items.Count; i++)
            {
                try
                {
                    CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                    Panel pnlDealTimeDetail = (Panel)dlCities.Items[i].FindControl("pnlDealTimeDetail");
                    if (chk != null && chk.Checked)
                    {
                        if (pnlDealTimeDetail != null)
                        {
                            pnlDealTimeDetail.CssClass = "showTimeDetail";
                        }
                    }
                }
                catch (Exception ex)
                { }
            }

            for (int i = 1; i < dlCities.Items.Count; i++)
            {
                try
                {
                    CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                    Label lblCityID = (Label)dlCities.Items[i].FindControl("lblCityID");
                    Label lblCity = (Label)dlCities.Items[i].FindControl("lblCity");
                    Panel pnlDealTimeDetail = (Panel)dlCities.Items[i].FindControl("pnlDealTimeDetail");
                    if (chk != null && lblCityID != null && lblCity != null && lblCityID.Text.Trim() != "" && chk.Checked)
                    {
                        if (pnlDealTimeDetail != null)
                        {
                            pnlDealTimeDetail.CssClass = "showTimeDetail";
                        }
                        checkedCity++;
                        try
                        {
                            TextBox txtdlStartDate = (TextBox)(dlCities.Items[i].FindControl("txtdlStartDate"));
                            TextBox txtDLEndDate = (TextBox)(dlCities.Items[i].FindControl("txtDLEndDate"));
                            DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartHH"));
                            DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartMM"));
                            DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartPortion"));
                            DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndHH"));
                            DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndMM"));
                            DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndPortion"));
                            DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[i].FindControl("ddlDLSideDeal"));

                            if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                                && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                                && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                            {

                                if (txtdlStartDate.Text.Trim() == "")
                                {
                                    bStatus = true;
                                    lblMessage.Text = "Please select start time for city <b>" + lblCity.Text.Trim() + "</b>";
                                    lblMessage.Visible = true;
                                    imgGridMessage.Visible = true;
                                    imgGridMessage.ImageUrl = "images/error.png";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    return true;
                                }
                                if (txtDLEndDate.Text.Trim() == "")
                                {
                                    bStatus = true;
                                    lblMessage.Text = "Please select end time for city <b>" + lblCity.Text.Trim() + "</b>";
                                    lblMessage.Visible = true;
                                    imgGridMessage.Visible = true;
                                    imgGridMessage.ImageUrl = "images/error.png";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    return true;
                                }
                                string strStartDate = txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
                                string strEndDate = txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
                                BLLDeals objBLLDeals = new BLLDeals();
                                objBLLDeals.RestaurantId = iResId;
                                objBLLDeals.DealStartTime = DateTime.Parse(strStartDate);
                                objBLLDeals.DealEndTime = DateTime.Parse(strEndDate);
                                objBLLDeals.DealSlot = int.Parse(ddlDLSideDeal.SelectedValue.Trim());
                                objBLLDeals.cityId = Convert.ToInt32(lblCityID.Text.ToString().Trim());
                                DataTable dtDealsInfo;
                                //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                                dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTimeWithCityID();
                                if ((dtDealsInfo != null) && (dtDealsInfo.Rows.Count > 0))
                                {
                                    //For Update Mode
                                    if (strCurrentDealID != "")
                                    {
                                        for (int j = 0; j < dtDealsInfo.Rows.Count; j++)
                                        {
                                            if (strCurrentDealID != dtDealsInfo.Rows[j]["dealId"].ToString().Trim())
                                            {
                                                dealExist++;
                                                bStatus = true;
                                                lblMessage.Text = "Already deal exists in selected slot & current time period for city \"" + lblCity.Text.Trim() + "\". '" + dtDealsInfo.Rows[j]["restaurantBusinessName"] + "' has deal '" + dtDealsInfo.Rows[j]["title"] + "' between " + dtDealsInfo.Rows[j]["dealStartTimeC"] + " and " + dtDealsInfo.Rows[j]["dealEndTimeC"];
                                                lblMessage.Visible = true;
                                                imgGridMessage.Visible = true;
                                                imgGridMessage.ImageUrl = "images/error.png";
                                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                                return true;

                                            }
                                            else
                                            {
                                                bStatus = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //For Add New Mode
                                        dealExist++;
                                        bStatus = true;
                                        lblMessage.Text = "Already deal exists in selected slot & current time period for city \"" + lblCity.Text.Trim() + "\". '" + dtDealsInfo.Rows[0]["restaurantBusinessName"] + "' has deal '" + dtDealsInfo.Rows[0]["title"] + "' between " + dtDealsInfo.Rows[0]["dealStartTimeC"] + " and " + dtDealsInfo.Rows[0]["dealEndTimeC"];
                                        lblMessage.Visible = true;
                                        imgGridMessage.Visible = true;
                                        imgGridMessage.ImageUrl = "images/error.png";
                                        lblMessage.ForeColor = System.Drawing.Color.Red;
                                        return true;

                                        //dealExist++;
                                        //bStatus = true;
                                        //lblMessage.Text = "There is some problem.";
                                        //lblMessage.Visible = true;
                                        //imgGridMessage.Visible = true;
                                        //imgGridMessage.ImageUrl = "images/error.png";
                                        //lblMessage.ForeColor = System.Drawing.Color.Red;
                                        //return true;
                                    }
                                }
                            }
                            else
                            {
                                lblMessage.Text = "There is some problem with city data list binding. Please contact your developer.";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "images/error.png";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            lblMessage.Visible = true;
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    //else
                    //{
                    //    return true;
                    //}
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (dealExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void bumpUpSlot()
    {
        try
        {
            BLLDeals objDeal = new BLLDeals();
            DataTable dtResult = Misc.search("SELECT  [deals].[dealId],[dealCity].[cityId],isnull(dealSlotC,1) dealSlot FROM [deals] INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] INNER join dealCity On dealCity.[dealId]= deals.[dealId] INNER join city On dealCity.[cityId]= city.[cityId] where restaurant.[restaurantId]= deals.[restaurantId] and dealSlotC<150 and [deals].parentdealId = 0  and dealEndTimeC >= getdate() order by dealSlotC");
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    objDeal.DealId = Convert.ToInt64(dtResult.Rows[i]["DealId"].ToString().Trim());
                    objDeal.cityId = Convert.ToInt32(dtResult.Rows[i]["cityId"].ToString().Trim());
                    objDeal.DealSlot = Convert.ToInt32(dtResult.Rows[i]["DealSlot"].ToString().Trim()) + 1;
                    objDeal.updateDealSlotByDealId();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnImgSave_Click(object sender, ImageClickEventArgs e)
    {
        checkExpiryCheck();
        if (!notExist)
        {
            lblMessage.Text = "Deal with this Title already exist. Please change your URL Title";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        string strImageName = "";

        int iResID = 0;
        int checkedcounter = 0;
        bool NationalDeal = false;
        CheckBox checkNationalDeals = (CheckBox)dlCities.Items[0].FindControl("chkbxSelect");
        if (checkNationalDeals != null && checkNationalDeals.Checked)
        {
            NationalDeal = true;
            Panel pnlDealTimeDetail = (Panel)dlCities.Items[0].FindControl("pnlDealTimeDetail");
            if (pnlDealTimeDetail != null)
            {
                pnlDealTimeDetail.CssClass = "showTimeDetail";
            }
        }
        if (!cbForDesign.Checked)
        {
            for (int i = 0; i < dlCities.Items.Count; i++)
            {
                try
                {
                    CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                    if (chk != null && chk.Checked)
                    {
                        checkedcounter++;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                    return;
                }
            }
            if (checkedcounter == 0)
            {
                lblMessage.Text = "Please select a city";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                return;
            }
        }

        try
        {
            iResID = int.Parse(Request.QueryString["resID"].ToString().Trim());
            iResID = iResID > 0 ? iResID : 0;
        }
        catch (Exception ex)
        { iResID = 0; }

        if (iResID != 0)
        {
            //Save the Deal Info
            if (this.btnImgSave.ToolTip == "Save Deal Info")
            {
                //Check In the Current Period of Deal, already Deal exists or not
                //if (AlreadyDealExistsWithSameStartEndTime(iResID, strStartDate, strEndDate, "") == false)

                if (NationalDeal)
                {                    

                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[0].FindControl("txtdlStartDate"));
                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[0].FindControl("txtDLEndDate"));
                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartHH"));
                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartMM"));
                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartPortion"));
                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndHH"));
                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndMM"));
                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndPortion"));
                    DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[0].FindControl("ddlDLSideDeal"));

                    if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                        && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                        && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                    {

                        if (txtdlStartDate.Text.Trim() == "")
                        {
                            lblMessage.Text = "Please select start time";
                            lblMessage.Visible = true;
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        if (txtDLEndDate.Text.Trim() == "")
                        {
                            lblMessage.Text = "Please select end time";
                            lblMessage.Visible = true;
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }


                    if (fpDealImage1.HasFile)
                    {
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
                    }

                    //Add New Deal Info & first tie it will be the parnet
                    int iDealId = AddNewDealInfo(strImageName, iResID, 0, 0);
                    NewDealID = iDealId;

                    long subdealID1 = 0;
                    long subdealID2 = 0;
                    long subdealID3 = 0;
                    long subdealID4 = 0;
                    long subdealID5 = 0;
                    long subdealID6 = 0;
                    long subdealID7 = 0;
                    long subdealID8 = 0;
                    long subdealID9 = 0;
                    long subdealID10 = 0;

                    if (iDealId != 0)
                    {
                        //Check First Sub Deal Info is provided or not
                        if (this.divFirstSubDeal.Visible == true)
                        {
                            subdealID1 = AddNewDealInfo(strImageName, iResID, iDealId, 1);
                        }
                        //Check Second Sub Deal Info is provided or not
                        if (this.divSecondSubDeal.Visible == true)
                        {
                            subdealID2 = AddNewDealInfo(strImageName, iResID, iDealId, 2);
                        }
                        //Check Third Sub Deal Info is provided or not
                        if (this.divThirdSubDeal.Visible == true)
                        {
                            subdealID3 = AddNewDealInfo(strImageName, iResID, iDealId, 3);
                        }
                        if (this.divForthSubDeal.Visible == true)
                        {
                            subdealID4 = AddNewDealInfo(strImageName, iResID, iDealId, 4);
                        }
                        if (this.divFifthSubDeal.Visible == true)
                        {
                            subdealID5 = AddNewDealInfo(strImageName, iResID, iDealId, 5);
                        }
                        if (this.divSixthSubDeal.Visible == true)
                        {
                            subdealID6 = AddNewDealInfo(strImageName, iResID, iDealId, 6);
                        }
                        if (this.divSeventhSubDeal.Visible == true)
                        {
                            subdealID7 = AddNewDealInfo(strImageName, iResID, iDealId, 7);
                        }
                        if (this.divEightSubDeal.Visible == true)
                        {
                            subdealID8 = AddNewDealInfo(strImageName, iResID, iDealId, 8);
                        }
                        if (this.divNinthSubDeal.Visible == true)
                        {
                            subdealID9 = AddNewDealInfo(strImageName, iResID, iDealId, 9);
                        }
                        if (this.divTenthSubDeal.Visible == true)
                        {
                            subdealID10 = AddNewDealInfo(strImageName, iResID, iDealId, 10);
                        }
                    }

                    BLLDealCity objCity = new BLLDealCity();
                    try
                    {
                        string strStartDate = txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
                        string strEndDate = txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.RestaurantId = iResID;
                        objBLLDeals.DealStartTime = DateTime.Parse(strStartDate);
                        objBLLDeals.DealEndTime = DateTime.Parse(strEndDate);
                        objBLLDeals.DealSlot = int.Parse(ddlDLSideDeal.SelectedValue.Trim());
                        bumpUpSlot();
                        for (int i = 0; i < dlCities.Items.Count; i++)
                        {
                            Label lblCityID = (Label)(dlCities.Items[i].FindControl("lblCityID"));
                            if (lblCityID != null && lblCityID.Text.Trim() != "")
                            {
                                objBLLDeals.cityId = Convert.ToInt32(lblCityID.Text);
                                objCity.CityId = Convert.ToInt32(lblCityID.Text);
                                DataTable dtDealsInfo;
                                //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                                dtDealsInfo = objBLLDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                                if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                                {
                                    int dealSlot = 1;
                                    for (int a = 1; a <= 150; a++)
                                    {
                                        DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                        if (foundRows.Length == 0)
                                        {
                                            dealSlot = a;
                                            break;
                                        }
                                    }
                                    objCity.DealSlotC = dealSlot;
                                }
                                else
                                {
                                    objCity.DealSlotC = 1;
                                }
                                objCity.DealStartTimeC = DateTime.Parse(strStartDate);
                                objCity.DealEndTimeC = DateTime.Parse(strEndDate);

                                objCity.DealId = iDealId;

                                objCity.createDealCity();

                                if (iDealId != 0)
                                {
                                    //Check First Sub Deal Info is provided or not
                                    if (subdealID1 > 0)
                                    {
                                        objCity.DealId = subdealID1;
                                        objCity.createDealCity();
                                    }
                                    //Check Second Sub Deal Info is provided or not
                                    if (subdealID2 > 0)
                                    {
                                        objCity.DealId = subdealID2;
                                        objCity.createDealCity();
                                    }
                                    //Check Third Sub Deal Info is provided or not
                                    if (subdealID3 > 0)
                                    {
                                        objCity.DealId = subdealID3;
                                        objCity.createDealCity();
                                    }

                                    if (subdealID4 > 0)
                                    {
                                        objCity.DealId = subdealID4;
                                        objCity.createDealCity();
                                    }

                                    if (subdealID5 > 0)
                                    {
                                        objCity.DealId = subdealID5;
                                        objCity.createDealCity();
                                    }

                                    if (subdealID6 > 0)
                                    {
                                        objCity.DealId = subdealID6;
                                        objCity.createDealCity();
                                    }

                                    if (subdealID7 > 0)
                                    {
                                        objCity.DealId = subdealID7;
                                        objCity.createDealCity();
                                    }

                                    if (subdealID8 > 0)
                                    {
                                        objCity.DealId = subdealID8;
                                        objCity.createDealCity();
                                    }

                                    if (subdealID9 > 0)
                                    {
                                        objCity.DealId = subdealID9;
                                        objCity.createDealCity();
                                    }

                                    if (subdealID10 > 0)
                                    {
                                        objCity.DealId = subdealID10;
                                        objCity.createDealCity();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    Response.Redirect("dealManagement.aspx?Res=Add&Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), false);                  
                    
                }
                else if (AlreadyDealExistsWithSameStartEndTimeWithCityName(iResID, "") == false)
                {
                    //If Image 1 exists
                    if (fpDealImage1.HasFile)
                    {
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
                    }

                    //Add New Deal Info & first tie it will be the parnet
                    int iDealId = AddNewDealInfo(strImageName, iResID, 0, 0);
                    NewDealID = iDealId;

                    long subdealID1 = 0;
                    long subdealID2 = 0;
                    long subdealID3 = 0;
                    long subdealID4 = 0;
                    long subdealID5 = 0;
                    long subdealID6 = 0;
                    long subdealID7 = 0;
                    long subdealID8 = 0;
                    long subdealID9 = 0;
                    long subdealID10 = 0;


                    if (iDealId != 0)
                    {
                        //Check First Sub Deal Info is provided or not
                        if (this.divFirstSubDeal.Visible == true)
                        {
                            subdealID1 = AddNewDealInfo(strImageName, iResID, iDealId, 1);
                        }
                        //Check Second Sub Deal Info is provided or not
                        if (this.divSecondSubDeal.Visible == true)
                        {
                            subdealID2 = AddNewDealInfo(strImageName, iResID, iDealId, 2);
                        }
                        //Check Third Sub Deal Info is provided or not
                        if (this.divThirdSubDeal.Visible == true)
                        {
                            subdealID3 = AddNewDealInfo(strImageName, iResID, iDealId, 3);
                        }
                        if (this.divForthSubDeal.Visible == true)
                        {
                            subdealID4 = AddNewDealInfo(strImageName, iResID, iDealId, 4);
                        }
                        if (this.divFifthSubDeal.Visible == true)
                        {
                            subdealID5 = AddNewDealInfo(strImageName, iResID, iDealId, 5);
                        }
                        if (this.divSixthSubDeal.Visible == true)
                        {
                            subdealID6 = AddNewDealInfo(strImageName, iResID, iDealId, 6);
                        }
                        if (this.divSeventhSubDeal.Visible == true)
                        {
                            subdealID7 = AddNewDealInfo(strImageName, iResID, iDealId, 7);
                        }
                        if (this.divEightSubDeal.Visible == true)
                        {
                            subdealID8 = AddNewDealInfo(strImageName, iResID, iDealId, 8);
                        }
                        if (this.divNinthSubDeal.Visible == true)
                        {
                            subdealID9 = AddNewDealInfo(strImageName, iResID, iDealId, 9);
                        }
                        if (this.divTenthSubDeal.Visible == true)
                        {
                            subdealID10 = AddNewDealInfo(strImageName, iResID, iDealId, 10);
                        }
                    }

                    BLLDealCity objCity = new BLLDealCity();
                    if (!cbForDesign.Checked)
                    {
                        for (int i = 1; i < dlCities.Items.Count; i++)
                        {
                            try
                            {
                                CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                                Label lblCityID = (Label)dlCities.Items[i].FindControl("lblCityID");
                                if (chk != null && lblCityID != null && lblCityID.Text.Trim() != "" && chk.Checked)
                                {
                                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[i].FindControl("txtdlStartDate"));
                                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[i].FindControl("txtDLEndDate"));
                                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartHH"));
                                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartMM"));
                                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartPortion"));
                                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndHH"));
                                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndMM"));
                                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndPortion"));
                                    DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[i].FindControl("ddlDLSideDeal"));

                                    if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                                        && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                                        && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                                    {
                                        objCity.DealStartTimeC = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealEndTimeC = DateTime.Parse(txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealSlotC = Convert.ToInt32(ddlDLSideDeal.SelectedValue.Trim());
                                    }
                                    objCity.DealId = iDealId;
                                    objCity.CityId = Convert.ToInt32(lblCityID.Text.Trim());
                                    objCity.createDealCity();

                                    if (iDealId != 0)
                                    {
                                        //Check First Sub Deal Info is provided or not
                                        if (subdealID1 > 0)
                                        {
                                            objCity.DealId = subdealID1;
                                            objCity.createDealCity();
                                        }
                                        //Check Second Sub Deal Info is provided or not
                                        if (subdealID2 > 0)
                                        {
                                            objCity.DealId = subdealID2;
                                            objCity.createDealCity();
                                        }
                                        //Check Third Sub Deal Info is provided or not
                                        if (subdealID3 > 0)
                                        {
                                            objCity.DealId = subdealID3;
                                            objCity.createDealCity();
                                        }
                                        if (subdealID4 > 0)
                                        {
                                            objCity.DealId = subdealID4;
                                            objCity.createDealCity();
                                        }
                                        if (subdealID5 > 0)
                                        {
                                            objCity.DealId = subdealID5;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID6 > 0)
                                        {
                                            objCity.DealId = subdealID6;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID7 > 0)
                                        {
                                            objCity.DealId = subdealID7;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID8 > 0)
                                        {
                                            objCity.DealId = subdealID8;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID9 > 0)
                                        {
                                            objCity.DealId = subdealID9;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID10 > 0)
                                        {
                                            objCity.DealId = subdealID10;
                                            objCity.createDealCity();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        try
                        {

                            objCity.DealStartTimeC = DateTime.Now.AddMonths(-3);
                            objCity.DealEndTimeC = DateTime.Now.AddMonths(-2);
                            objCity.DealId = iDealId;
                            objCity.CityId = 337;

                            BLLDeals objBLLDeals = new BLLDeals();
                            objBLLDeals.DealStartTime = objCity.DealStartTimeC;
                            objBLLDeals.DealEndTime = objCity.DealEndTimeC;
                            objBLLDeals.cityId = 337;
                            DataTable dtDealsInfo;
                            //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                            dtDealsInfo = objBLLDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                            if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                            {
                                int dealSlot = 1;
                                for (int a = 1; a <= 150; a++)
                                {
                                    DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                    if (foundRows.Length == 0)
                                    {
                                        dealSlot = a;
                                        break;
                                    }
                                }
                                objCity.DealSlotC = dealSlot;
                            }
                            else
                            {
                                objCity.DealSlotC = 1;
                            }

                            objCity.createDealCity();

                            if (iDealId != 0)
                            {
                                //Check First Sub Deal Info is provided or not
                                if (subdealID1 > 0)
                                {
                                    objCity.DealId = subdealID1;
                                    objCity.createDealCity();
                                }
                                //Check Second Sub Deal Info is provided or not
                                if (subdealID2 > 0)
                                {
                                    objCity.DealId = subdealID2;
                                    objCity.createDealCity();
                                }
                                //Check Third Sub Deal Info is provided or not
                                if (subdealID3 > 0)
                                {
                                    objCity.DealId = subdealID3;
                                    objCity.createDealCity();
                                }

                                if (subdealID4 > 0)
                                {
                                    objCity.DealId = subdealID4;
                                    objCity.createDealCity();
                                }
                                if (subdealID5 > 0)
                                {
                                    objCity.DealId = subdealID5;
                                    objCity.createDealCity();
                                }

                                if (subdealID6 > 0)
                                {
                                    objCity.DealId = subdealID6;
                                    objCity.createDealCity();
                                }

                                if (subdealID7 > 0)
                                {
                                    objCity.DealId = subdealID7;
                                    objCity.createDealCity();
                                }

                                if (subdealID8 > 0)
                                {
                                    objCity.DealId = subdealID8;
                                    objCity.createDealCity();
                                }

                                if (subdealID9 > 0)
                                {
                                    objCity.DealId = subdealID9;
                                    objCity.createDealCity();
                                }

                                if (subdealID10 > 0)
                                {
                                    objCity.DealId = subdealID10;
                                    objCity.createDealCity();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    //Validate that DealId Should not be "0"


                    //Hide the Update Deal Info here
                    

                    
                    //If adds successfully then redirects towars the Business form
                    Response.Redirect("dealManagement.aspx?Res=Add&Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), false);
                }
            }
            //Update the Deal Info
            else if (this.btnImgSave.ToolTip == "Update Deal Info")
            {                 
                 iResID = int.Parse(hfResturnatID.Value);

                if (NationalDeal)
                {
                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[0].FindControl("txtdlStartDate"));
                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[0].FindControl("txtDLEndDate"));
                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartHH"));
                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartMM"));
                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartPortion"));
                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndHH"));
                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndMM"));
                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndPortion"));
                    DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[0].FindControl("ddlDLSideDeal"));

                    if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                        && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                        && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                    {

                        if (txtdlStartDate.Text.Trim() == "")
                        {
                            lblMessage.Text = "Please select start time";
                            lblMessage.Visible = true;
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        if (txtDLEndDate.Text.Trim() == "")
                        {
                            lblMessage.Text = "Please select end time";
                            lblMessage.Visible = true;
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }

                    //If Image 1 exists
                    if (fpDealImage1.HasFile)
                    {
                        if (this.imgUpload1.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload1.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                    }
                    else
                    {
                        strImageName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        if (this.imgUpload2.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload2.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        if (this.imgUpload3.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload3.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }

                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //Update the Deal info by Deal Id
                    UpdateDealInfoByDealId(strImageName, iResID, hfDealId.Value, 0);
                    long subDealID1 = 0;
                    long subDealID2 = 0;
                    long subDealID3 = 0;
                    long subDealID4 = 0;
                    long subDealID5 = 0;
                    long subDealID6 = 0;
                    long subDealID7 = 0;
                    long subDealID8 = 0;
                    long subDealID9 = 0;
                    long subDealID10 = 0;


                    //Check First Sub Deal Info is provided or not
                    if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId1.Value.Trim(), 1);
                        subDealID1 = Convert.ToInt64(hfDealId1.Value);
                        this.hfDealId1.Value = "0";
                    }
                    else if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() == "0"))
                    {
                        subDealID1 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 1);
                        this.hfDealId1.Value = "0";
                    }
                    else if ((this.divFirstSubDeal.Visible == false) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId1.Value.Trim());
                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId1.Value = "0";
                        subDealID1 = 0;
                    }

                    // 
                    //Check Second Sub Deal Info is provided or not
                    if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId2.Value.Trim(), 2);
                        subDealID2 = Convert.ToInt64(hfDealId2.Value);
                        this.hfDealId2.Value = "0";
                    }
                    else if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() == "0"))
                    {
                        subDealID2 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 2);
                        this.hfDealId2.Value = "0";
                    }
                    else if ((this.divSecondSubDeal.Visible == false) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId2.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId2.Value = "0";
                        subDealID2 = 0;
                    }

                    //Check Third Sub Deal Info is provided or not
                    if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId3.Value.Trim(), 3);
                        subDealID3 = Convert.ToInt64(hfDealId3.Value);
                        this.hfDealId3.Value = "0";
                    }
                    else if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() == "0"))
                    {
                        subDealID3 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 3);
                        this.hfDealId3.Value = "0";
                    }
                    else if ((this.divThirdSubDeal.Visible == false) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId3.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId3.Value = "0";
                        subDealID3 = 0;
                    }

                    //Check Forth Sub Deal info is prvided or not
                    if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId4.Value.Trim(), 4);
                        subDealID4 = Convert.ToInt64(hfDealId4.Value);
                        this.hfDealId4.Value = "0";
                    }
                    else if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() == "0"))
                    {
                        subDealID4 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 4);
                        this.hfDealId4.Value = "0";
                    }
                    else if ((this.divForthSubDeal.Visible == false) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId4.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId4.Value = "0";
                        subDealID4 = 0;
                    }

                    //Check Fifth Sub Deal info is prvided or not
                    if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId5.Value.Trim(), 5);
                        subDealID5 = Convert.ToInt64(hfDealId5.Value);
                        this.hfDealId5.Value = "0";
                    }
                    else if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() == "0"))
                    {
                        subDealID5 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 5);
                        this.hfDealId5.Value = "0";
                    }
                    else if ((this.divFifthSubDeal.Visible == false) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId5.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId5.Value = "0";
                        subDealID5 = 0;
                    }

                    //Check Sixth Sub Deal info is prvided or not
                    if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId6.Value.Trim(), 6);
                        subDealID6 = Convert.ToInt64(hfDealId6.Value);
                        this.hfDealId6.Value = "0";
                    }
                    else if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() == "0"))
                    {
                        subDealID6 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 6);
                        this.hfDealId6.Value = "0";
                    }
                    else if ((this.divSixthSubDeal.Visible == false) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId6.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId6.Value = "0";
                        subDealID6 = 0;
                    }

                    //Check Seven Sub Deal info is prvided or not
                    if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId7.Value.Trim(), 7);
                        subDealID7 = Convert.ToInt64(hfDealId7.Value);
                        this.hfDealId7.Value = "0";
                    }
                    else if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() == "0"))
                    {
                        subDealID7 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 7);
                        this.hfDealId7.Value = "0";
                    }
                    else if ((this.divSeventhSubDeal.Visible == false) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId7.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId7.Value = "0";
                        subDealID7 = 0;
                    }

                    //Check Eight Sub Deal info is prvided or not
                    if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId8.Value.Trim(), 8);
                        subDealID8 = Convert.ToInt64(hfDealId8.Value);
                        this.hfDealId8.Value = "0";
                    }
                    else if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() == "0"))
                    {
                        subDealID8 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 8);
                        this.hfDealId8.Value = "0";
                    }
                    else if ((this.divEightSubDeal.Visible == false) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId8.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId8.Value = "0";
                        subDealID8 = 0;
                    }

                    //Check Ninth Sub Deal info is prvided or not
                    if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId9.Value.Trim(), 9);
                        subDealID9 = Convert.ToInt64(hfDealId9.Value);
                        this.hfDealId9.Value = "0";
                    }
                    else if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() == "0"))
                    {
                        subDealID9 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 9);
                        this.hfDealId9.Value = "0";
                    }
                    else if ((this.divNinthSubDeal.Visible == false) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId9.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId9.Value = "0";
                        subDealID9 = 0;
                    }


                    //Check Ninth Sub Deal info is prvided or not
                    if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId10.Value.Trim(), 10);
                        subDealID10 = Convert.ToInt64(hfDealId10.Value);
                        this.hfDealId10.Value = "0";
                    }
                    else if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() == "0"))
                    {
                        subDealID10 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 10);
                        this.hfDealId10.Value = "0";
                    }
                    else if ((this.divTenthSubDeal.Visible == false) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId10.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId10.Value = "0";
                        subDealID10 = 0;
                    }


                    //Hide the Update Deal Info here
                    
                    
                    //Get All Latest Deal Info Grid Info
                    //GetAllDealInfoAndFillGrid();
                    // SearchhDealInfoByDifferentParams();

                    //If adds successfully then redirects towars the Business form
                    //Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx?Res=Update"), false);

                    //Show the All Deals
                    

                    BLLDealCity objCity = new BLLDealCity();
                    objCity.DealId = Convert.ToInt64(hfDealId.Value);
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID1;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID2;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID3;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID4;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID5;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID6;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID7;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID8;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID9;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID10;
                    objCity.deleteDealCityByDealID();

                    string strStartDate = txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
                    string strEndDate = txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
                    BLLDeals objDeals = new BLLDeals();
                    objDeals.DealStartTime = DateTime.Parse(strStartDate);
                    objDeals.DealEndTime = DateTime.Parse(strEndDate);
                    bumpUpSlot();
                    for (int i = 0; i < dlCities.Items.Count; i++)
                    {
                        try
                        {
                            Label lblCityID = (Label)(dlCities.Items[i].FindControl("lblCityID"));
                            if (lblCityID != null && lblCityID.Text.Trim() != "")
                            {
                                objDeals.cityId = Convert.ToInt32(lblCityID.Text);
                                objCity.CityId = Convert.ToInt32(lblCityID.Text);
                                DataTable dtDealsInfo;
                                //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                                dtDealsInfo = objDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                                if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                                {
                                    int dealSlot = 1;
                                    for (int a = 1; a <= 150; a++)
                                    {
                                        DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                        if (foundRows.Length == 0)
                                        {
                                            dealSlot = a;
                                            break;
                                        }
                                    }
                                    objCity.DealSlotC = dealSlot;
                                }
                                else
                                {
                                    objCity.DealSlotC = 1;
                                }

                                objCity.DealStartTimeC = DateTime.Parse(strStartDate);
                                objCity.DealEndTimeC = DateTime.Parse(strEndDate);
                                objCity.DealId = Convert.ToInt64(hfDealId.Value.Trim());
                                objCity.createDealCity();

                                if (subDealID1 > 0)
                                {
                                    objCity.DealId = subDealID1;
                                    objCity.createDealCity();
                                }
                                //Check Second Sub Deal Info is provided or not
                                if (subDealID2 > 0)
                                {
                                    objCity.DealId = subDealID2;
                                    objCity.createDealCity();
                                }
                                //Check Third Sub Deal Info is provided or not
                                if (subDealID3 > 0)
                                {
                                    objCity.DealId = subDealID3;
                                    objCity.createDealCity();
                                }
                                //Check Forth Sub Deal Info is provided or not
                                if (subDealID4 > 0)
                                {
                                    objCity.DealId = subDealID4;
                                    objCity.createDealCity();
                                }
                                if (subDealID5 > 0)
                                {
                                    objCity.DealId = subDealID5;
                                    objCity.createDealCity();
                                }

                                if (subDealID6 > 0)
                                {
                                    objCity.DealId = subDealID6;
                                    objCity.createDealCity();
                                }

                                if (subDealID7 > 0)
                                {
                                    objCity.DealId = subDealID7;
                                    objCity.createDealCity();
                                }

                                if (subDealID8 > 0)
                                {
                                    objCity.DealId = subDealID8;
                                    objCity.createDealCity();
                                }

                                if (subDealID9 > 0)
                                {
                                    objCity.DealId = subDealID9;
                                    objCity.createDealCity();
                                }

                                if (subDealID10 > 0)
                                {
                                    objCity.DealId = subDealID10;
                                    objCity.createDealCity();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    Response.Redirect("dealManagement.aspx?Res=Update&Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), false);
                }
                else if (AlreadyDealExistsWithSameStartEndTimeWithCityName(iResID, hfDealId.Value) == false)
                {
                    //If Image 1 exists
                    if (fpDealImage1.HasFile)
                    {
                        if (this.imgUpload1.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload1.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                    }
                    else
                    {
                        strImageName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        if (this.imgUpload2.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload2.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        if (this.imgUpload3.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload3.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }

                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //Update the Deal info by Deal Id
                    UpdateDealInfoByDealId(strImageName, iResID, hfDealId.Value, 0);
                    long subDealID1 = 0;
                    long subDealID2 = 0;
                    long subDealID3 = 0;
                    long subDealID4 = 0;
                    long subDealID5 = 0;
                    long subDealID6 = 0;
                    long subDealID7 = 0;
                    long subDealID8 = 0;
                    long subDealID9 = 0;
                    long subDealID10 = 0;

                    //Check First Sub Deal Info is provided or not
                    if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId1.Value.Trim(), 1);
                        subDealID1 = Convert.ToInt64(hfDealId1.Value);
                        this.hfDealId1.Value = "0";
                    }
                    else if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() == "0"))
                    {
                        subDealID1 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 1);
                        this.hfDealId1.Value = "0";
                    }
                    else if ((this.divFirstSubDeal.Visible == false) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId1.Value.Trim());
                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId1.Value = "0";
                        subDealID1 = 0;
                    }

                    // 
                    //Check Second Sub Deal Info is provided or not
                    if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId2.Value.Trim(), 2);
                        subDealID2 = Convert.ToInt64(hfDealId2.Value);
                        this.hfDealId2.Value = "0";
                    }
                    else if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() == "0"))
                    {
                        subDealID2 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 2);
                        this.hfDealId2.Value = "0";
                    }
                    else if ((this.divSecondSubDeal.Visible == false) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId2.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId2.Value = "0";
                        subDealID2 = 0;
                    }

                    //Check Third Sub Deal Info is provided or not
                    if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId3.Value.Trim(), 3);
                        subDealID3 = Convert.ToInt64(hfDealId3.Value);
                        this.hfDealId3.Value = "0";
                    }
                    else if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() == "0"))
                    {
                        subDealID3 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 3);
                        this.hfDealId3.Value = "0";
                    }
                    else if ((this.divThirdSubDeal.Visible == false) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId3.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId3.Value = "0";
                        subDealID3 = 0;
                    }

                    //Check Forth Sub Deal info is prvided or not
                    if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId4.Value.Trim(), 4);
                        subDealID4 = Convert.ToInt64(hfDealId4.Value);
                        this.hfDealId4.Value = "0";
                    }
                    else if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() == "0"))
                    {
                        subDealID4 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 4);
                        this.hfDealId4.Value = "0";
                    }
                    else if ((this.divForthSubDeal.Visible == false) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId4.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId4.Value = "0";
                        subDealID4 = 0;
                    }


                    //Check Fifth Sub Deal info is prvided or not
                    if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId5.Value.Trim(), 5);
                        subDealID5 = Convert.ToInt64(hfDealId5.Value);
                        this.hfDealId5.Value = "0";
                    }
                    else if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() == "0"))
                    {
                        subDealID5 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 5);
                        this.hfDealId5.Value = "0";
                    }
                    else if ((this.divFifthSubDeal.Visible == false) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId5.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId5.Value = "0";
                        subDealID5 = 0;
                    }

                    //Check Sixth Sub Deal info is prvided or not
                    if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId6.Value.Trim(), 6);
                        subDealID6 = Convert.ToInt64(hfDealId6.Value);
                        this.hfDealId6.Value = "0";
                    }
                    else if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() == "0"))
                    {
                        subDealID6 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 6);
                        this.hfDealId6.Value = "0";
                    }
                    else if ((this.divSixthSubDeal.Visible == false) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId6.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId6.Value = "0";
                        subDealID6 = 0;
                    }

                    //Check Seven Sub Deal info is prvided or not
                    if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId7.Value.Trim(), 7);
                        subDealID7 = Convert.ToInt64(hfDealId7.Value);
                        this.hfDealId7.Value = "0";
                    }
                    else if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() == "0"))
                    {
                        subDealID7 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 7);
                        this.hfDealId7.Value = "0";
                    }
                    else if ((this.divSeventhSubDeal.Visible == false) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId7.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId7.Value = "0";
                        subDealID7 = 0;
                    }

                    //Check Eight Sub Deal info is prvided or not
                    if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId8.Value.Trim(), 8);
                        subDealID8 = Convert.ToInt64(hfDealId8.Value);
                        this.hfDealId8.Value = "0";
                    }
                    else if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() == "0"))
                    {
                        subDealID8 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 8);
                        this.hfDealId8.Value = "0";
                    }
                    else if ((this.divEightSubDeal.Visible == false) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId8.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId8.Value = "0";
                        subDealID8 = 0;
                    }

                    //Check Ninth Sub Deal info is prvided or not
                    if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId9.Value.Trim(), 9);
                        subDealID9 = Convert.ToInt64(hfDealId9.Value);
                        this.hfDealId9.Value = "0";
                    }
                    else if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() == "0"))
                    {
                        subDealID9 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 9);
                        this.hfDealId9.Value = "0";
                    }
                    else if ((this.divNinthSubDeal.Visible == false) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId9.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId9.Value = "0";
                        subDealID9 = 0;
                    }


                    //Check Ninth Sub Deal info is prvided or not
                    if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId10.Value.Trim(), 10);
                        subDealID10 = Convert.ToInt64(hfDealId10.Value);
                        this.hfDealId10.Value = "0";
                    }
                    else if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() == "0"))
                    {
                        subDealID10 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 10);
                        this.hfDealId10.Value = "0";
                    }
                    else if ((this.divTenthSubDeal.Visible == false) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId10.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId10.Value = "0";
                        subDealID10 = 0;
                    }



                    //Hide the Update Deal Info here
                    
                    
                    //Get All Latest Deal Info Grid Info
                    //GetAllDealInfoAndFillGrid();
                    // SearchhDealInfoByDifferentParams();

                    //If adds successfully then redirects towars the Business form
                    //Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx?Res=Update"), false);

                    //Show the All Deals
                    

                    BLLDealCity objCity = new BLLDealCity();
                    objCity.DealId = Convert.ToInt64(hfDealId.Value);
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID1;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID2;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID3;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID4;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID5;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID6;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID7;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID8;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID9;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID10;
                    objCity.deleteDealCityByDealID();

                    if (!cbForDesign.Checked)
                    {
                        for (int i = 1; i < dlCities.Items.Count; i++)
                        {
                            try
                            {
                                CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                                Label lblCityID = (Label)dlCities.Items[i].FindControl("lblCityID");
                                if (chk != null && lblCityID != null && lblCityID.Text.Trim() != "" && chk.Checked)
                                {
                                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[i].FindControl("txtdlStartDate"));
                                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[i].FindControl("txtDLEndDate"));
                                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartHH"));
                                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartMM"));
                                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartPortion"));
                                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndHH"));
                                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndMM"));
                                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndPortion"));
                                    DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[i].FindControl("ddlDLSideDeal"));

                                    if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                                        && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                                        && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                                    {
                                        objCity.DealStartTimeC = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealEndTimeC = DateTime.Parse(txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealSlotC = Convert.ToInt32(ddlDLSideDeal.SelectedValue.Trim());
                                    }
                                    objCity.CityId = Convert.ToInt32(lblCityID.Text.Trim());
                                    objCity.DealId = Convert.ToInt64(hfDealId.Value.Trim());
                                    objCity.createDealCity();

                                    if (subDealID1 > 0)
                                    {
                                        objCity.DealId = subDealID1;
                                        objCity.createDealCity();
                                    }
                                    //Check Second Sub Deal Info is provided or not
                                    if (subDealID2 > 0)
                                    {
                                        objCity.DealId = subDealID2;
                                        objCity.createDealCity();
                                    }
                                    //Check Third Sub Deal Info is provided or not
                                    if (subDealID3 > 0)
                                    {
                                        objCity.DealId = subDealID3;
                                        objCity.createDealCity();
                                    }
                                    //Check Forth Sub Deal Info is provided or not
                                    if (subDealID4 > 0)
                                    {
                                        objCity.DealId = subDealID4;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID5 > 0)
                                    {
                                        objCity.DealId = subDealID5;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID6 > 0)
                                    {
                                        objCity.DealId = subDealID6;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID7 > 0)
                                    {
                                        objCity.DealId = subDealID7;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID8 > 0)
                                    {
                                        objCity.DealId = subDealID8;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID9 > 0)
                                    {
                                        objCity.DealId = subDealID9;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID10 > 0)
                                    {
                                        objCity.DealId = subDealID10;
                                        objCity.createDealCity();
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            objCity.DealStartTimeC = DateTime.Now.AddMonths(-3);
                            objCity.DealEndTimeC = DateTime.Now.AddMonths(-2);
                            objCity.DealId = Convert.ToInt64(hfDealId.Value.Trim()); ;
                            objCity.CityId = 337;

                            BLLDeals objBLLDeals = new BLLDeals();
                            objBLLDeals.DealStartTime = objCity.DealStartTimeC;
                            objBLLDeals.DealEndTime = objCity.DealEndTimeC;
                            objBLLDeals.cityId = 337;
                            DataTable dtDealsInfo;
                            //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                            dtDealsInfo = objBLLDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                            if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                            {
                                int dealSlot = 1;
                                for (int a = 1; a <= 150; a++)
                                {
                                    DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                    if (foundRows.Length == 0)
                                    {
                                        dealSlot = a;
                                        break;
                                    }
                                }
                                objCity.DealSlotC = dealSlot;
                            }
                            else
                            {
                                objCity.DealSlotC = 1;
                            }


                            objCity.createDealCity();

                            if (subDealID1 > 0)
                            {
                                objCity.DealId = subDealID1;
                                objCity.createDealCity();
                            }
                            //Check Second Sub Deal Info is provided or not
                            if (subDealID2 > 0)
                            {
                                objCity.DealId = subDealID2;
                                objCity.createDealCity();
                            }
                            //Check Third Sub Deal Info is provided or not
                            if (subDealID3 > 0)
                            {
                                objCity.DealId = subDealID3;
                                objCity.createDealCity();
                            }
                            //Check Forth Sub Deal Info is provided or not
                            if (subDealID4 > 0)
                            {
                                objCity.DealId = subDealID4;
                                objCity.createDealCity();
                            }

                            if (subDealID5 > 0)
                            {
                                objCity.DealId = subDealID5;
                                objCity.createDealCity();
                            }

                            if (subDealID6 > 0)
                            {
                                objCity.DealId = subDealID6;
                                objCity.createDealCity();
                            }

                            if (subDealID7 > 0)
                            {
                                objCity.DealId = subDealID7;
                                objCity.createDealCity();
                            }

                            if (subDealID8 > 0)
                            {
                                objCity.DealId = subDealID8;
                                objCity.createDealCity();
                            }

                            if (subDealID9 > 0)
                            {
                                objCity.DealId = subDealID9;
                                objCity.createDealCity();
                            }

                            if (subDealID10 > 0)
                            {
                                objCity.DealId = subDealID10;
                                objCity.createDealCity();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    Response.Redirect("dealManagement.aspx?Res=Update&Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), false);
                   
                }
            }            
        }
        else
        {
            //In case of Exception it will redirect you towrads the Business page
            //Usually exception comes in that case where if "Mode" or "resID" contains invalid value
            // Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), false);
        }
    }

    private string ImageUploadHere(FileUpload fileUploadDealImg, int strResID)
    {
        string strUniqueID = "";

        try
        {

            if (fileUploadDealImg.HasFile)
            {
                //string strResID = this.ddlSelectRes.SelectedItem.Value.Trim();

                string[] strExtension = fileUploadDealImg.FileName.Split('.');

                strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];

                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\";

                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }
                string filename = Path.GetFileName(fileUploadDealImg.PostedFile.FileName);
                string targetPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strUniqueID;
                Stream strm = fileUploadDealImg.PostedFile.InputStream;
                var targetFile = targetPath;
                //Based on scalefactor image size will vary
                Misc.GenerateThumbnails(strm, targetFile, AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\", strUniqueID);
            }
        }
        catch (Exception ex)
        {

        }

        return strUniqueID;
    }

    protected void btnChange_Click(object sender, EventArgs e)
    { }

    #region"Save New Deal Info here"

    //if iDealRefNo is "0", then it will pick the default values
    //if iDealRefNo is "1", then it will pick the first Sub Deal Info
    //if iDealRefNo is "2", then it will pick the second Sub Deal Info
    //if iDealRefNo is "3", then it will pick the third Sub Deal Info

    private int AddNewDealInfo(string strImageNames, int iResId, int iParnetDealId, int iDealRefNo)
    {

        int iDealId = 0;

        try
        {
            if (ddlSalePersonAccountName.SelectedIndex == 0)
            {
                lblMessage.Text = "Please select a sale's person account.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return 0;
            }



            BLLDeals objBLLDeals = new BLLDeals();
            //objBLLDeals.RestaurantId = int.Parse(this.ddlSelectRes.SelectedItem.Value.Trim());
            objBLLDeals.RestaurantId = iResId;

            //Set the Deal Title according to the DealRefNo
            if (iDealRefNo == 0)
            {
                objBLLDeals.Title = this.txtTitle.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }

            }
            else if (iDealRefNo == 1)
            {
                objBLLDeals.Title = this.txtTitle1.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice1.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice1.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax1.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders1.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders1.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax1.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 2)
            {
                objBLLDeals.Title = this.txtTitle2.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice2.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice2.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax2.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders2.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders2.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax2.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 3)
            {
                objBLLDeals.Title = this.txtTitle3.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice3.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice3.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax3.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders3.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders3.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax3.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 4)
            {
                objBLLDeals.Title = this.txtTitle4.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice4.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice4.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax4.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders4.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders4.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax4.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 5)
            {
                objBLLDeals.Title = this.txtTitle5.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice5.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice5.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax5.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders5.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders5.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax5.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 6)
            {
                objBLLDeals.Title = this.txtTitle6.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice6.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice6.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax6.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders6.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders6.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax6.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 7)
            {
                objBLLDeals.Title = this.txtTitle7.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice7.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice7.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax7.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders7.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders7.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax7.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 8)
            {
                objBLLDeals.Title = this.txtTitle8.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice8.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice8.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax8.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders8.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders8.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax8.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 9)
            {
                objBLLDeals.Title = this.txtTitle9.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice9.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice9.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax9.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders9.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders9.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax9.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 10)
            {
                objBLLDeals.Title = this.txtTitle10.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice10.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice10.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax10.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders10.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders10.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax10.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            try
            {
                objBLLDeals.yelpRate = float.Parse(ddlReviewRate.SelectedValue.ToString());
            }
            catch (Exception ex)
            { }

            objBLLDeals.tracking = cbTracking.Checked;
            objBLLDeals.doublePoints = cbDoublePoints.Checked;

            objBLLDeals.reviewExist = cbYelpReviews.Checked;
            objBLLDeals.yelpLink = txtReviewLink.Text.Trim();
            objBLLDeals.yelpText = txtReviewText.Text.Trim();
            objBLLDeals.urlTitle = txtURLTitle.Text.Trim();
            objBLLDeals.FinePrint = this.txtFinePrint.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.howtouse = this.txtHowToUse.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.DealHightlights = this.txtDealHightlights.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.Description = this.elm1.Text.Trim();

            string strStartDate = "";
            string strEndDate = "";

            for (int i = 1; i < dlCities.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                if (chk != null && chk.Checked)
                {
                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[i].FindControl("txtdlStartDate"));
                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[i].FindControl("txtDLEndDate"));
                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartHH"));
                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartMM"));
                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartPortion"));
                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndHH"));
                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndMM"));
                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndPortion"));

                    strStartDate = txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
                    strEndDate = txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
                    break;
                }
            }

            try
            {
                objBLLDeals.DealStartTime = Convert.ToDateTime(strStartDate);
                objBLLDeals.DealEndTime = Convert.ToDateTime(strEndDate);
            }
            catch (Exception ex)
            {
                objBLLDeals.DealStartTime = DateTime.Now;
                objBLLDeals.DealEndTime = DateTime.Now;
            }
            DateTime dt = DateTime.Now;
            if (cbNoExpiryDate.Checked)
            {
                objBLLDeals.voucherExpiryDateAvailable = false;
            }
            if (DateTime.TryParse(txtVoucherExpiryDate.Text.Trim(), out dt))
            {
                objBLLDeals.voucherExpiryDate = dt;
            }

            objBLLDeals.Images = strImageNames;

            //Set the Minimum no. of Orders
            objBLLDeals.DealDelMinLmt = int.Parse(this.txtMinNoOfOrders.Text.Trim() == "" ? "0" : this.txtMinNoOfOrders.Text.Trim());



            //Set the Minimum Orders Per User 
            objBLLDeals.MinOrdersPerUser = 0;// int.Parse(this.txtMinOrdersPerUser.Text.Trim());

            //Set the Max Orders Per User 
            objBLLDeals.MaxOrdersPerUser = int.Parse(this.txtMaxOrdersPerUser.Text.Trim() == "" ? "0" : this.txtMaxOrdersPerUser.Text.Trim());

            //Set the Maximum Gifts Per Order 
            objBLLDeals.MaxGiftsPerOrder = int.Parse(this.txtMaxGiftsPerOrder.Text.Trim() == "" ? "0" : this.txtMaxGiftsPerOrder.Text.Trim());


            //Set the Default Page Title
            objBLLDeals.DealPageTitle = ((divFirstSubDeal.Visible == true) && (iDealRefNo == 0)) ? this.txtTitleMain.Text.Trim() : "";

            objBLLDeals.DealStatus = this.ddlStatus.SelectedValue == "Yes" ? true : false;

            objBLLDeals.CreatedBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();

            objBLLDeals.CreatedDate = DateTime.Now;

            // objBLLDeals.DealSlot = int.Parse(this.ddlSideDeal.SelectedValue.Trim());

            objBLLDeals.ParentDealId = iParnetDealId;

            objBLLDeals.topTitle = txtTopTitle.Text.Trim();
            objBLLDeals.shortTitle = txtShortTitle.Text.Trim();
            objBLLDeals.ourCommission = float.Parse(txtOurComission.Text.ToString().Trim());



            objBLLDeals.SalePersonAccountName = ddlSalePersonAccountName.SelectedItem.ToString().Trim();


            iDealId = objBLLDeals.AddNewDeal();


            lblMessage.Text = "New Deal has been added successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return iDealId;
    }

    private int AddNewDealInfoForPreview(string strImageNames, int iResId, int iParnetDealId, int iDealRefNo)
    {
        int iDealId = 0;

        try
        {
            BLLDeals objBLLDeals = new BLLDeals();
            //objBLLDeals.RestaurantId = int.Parse(this.ddlSelectRes.SelectedItem.Value.Trim());
            objBLLDeals.RestaurantId = iResId;

            //Set the Deal Title according to the DealRefNo
            if (iDealRefNo == 0)
            {
                objBLLDeals.Title = this.txtTitle.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 1)
            {
                objBLLDeals.Title = this.txtTitle1.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice1.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice1.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax1.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders1.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders1.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax1.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 2)
            {
                objBLLDeals.Title = this.txtTitle2.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice2.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice2.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax2.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders2.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders2.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax2.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 3)
            {
                objBLLDeals.Title = this.txtTitle3.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice3.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice3.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax3.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders3.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders3.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax3.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 4)
            {
                objBLLDeals.Title = this.txtTitle4.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice4.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice4.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax4.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders4.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders4.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax4.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 5)
            {
                objBLLDeals.Title = this.txtTitle5.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice5.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice5.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax5.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders5.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders5.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax5.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 6)
            {
                objBLLDeals.Title = this.txtTitle6.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice6.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice6.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax6.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders6.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders6.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax6.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 7)
            {
                objBLLDeals.Title = this.txtTitle7.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice7.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice7.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax7.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders7.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders7.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax7.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 8)
            {
                objBLLDeals.Title = this.txtTitle8.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice8.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice8.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax8.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders8.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders8.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax8.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 9)
            {
                objBLLDeals.Title = this.txtTitle9.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice9.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice9.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax9.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders9.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders9.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax9.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 10)
            {
                objBLLDeals.Title = this.txtTitle10.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice10.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice10.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax10.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders10.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders10.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax10.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            try
            {
                objBLLDeals.yelpRate = float.Parse(ddlReviewRate.SelectedValue.ToString());
            }
            catch (Exception ex)
            { }

            objBLLDeals.tracking = cbTracking.Checked;
            objBLLDeals.doublePoints = cbDoublePoints.Checked;

            objBLLDeals.reviewExist = cbYelpReviews.Checked;
            objBLLDeals.yelpLink = txtReviewLink.Text.Trim();
            objBLLDeals.yelpText = txtReviewText.Text.Trim();

            objBLLDeals.urlTitle = txtURLTitle.Text.Trim();
            objBLLDeals.FinePrint = this.txtFinePrint.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.howtouse = this.txtHowToUse.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.DealHightlights = this.txtDealHightlights.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.Description = this.elm1.Text.Trim();
            objBLLDeals.DealStartTime = DateTime.Now;
            objBLLDeals.DealEndTime = DateTime.Now;
            DateTime dt = DateTime.Now;
            if (cbNoExpiryDate.Checked)
            {
                objBLLDeals.voucherExpiryDateAvailable = false;
            }
            if (DateTime.TryParse(txtVoucherExpiryDate.Text.Trim(), out dt))
            {
                objBLLDeals.voucherExpiryDate = dt;
            }

            objBLLDeals.Images = strImageNames;

            //Set the Minimum no. of Orders
            objBLLDeals.DealDelMinLmt = int.Parse(this.txtMinNoOfOrders.Text.Trim() == "" ? "0" : this.txtMinNoOfOrders.Text.Trim());

            //Set the Minimum Orders Per User 
            objBLLDeals.MinOrdersPerUser = 0; //int.Parse(this.txtMinOrdersPerUser.Text.Trim());

            //Set the Max Orders Per User 
            objBLLDeals.MaxOrdersPerUser = int.Parse(this.txtMaxOrdersPerUser.Text.Trim() == "" ? "0" : this.txtMaxOrdersPerUser.Text.Trim());

            //Set the Maximum Gifts Per Order 
            objBLLDeals.MaxGiftsPerOrder = int.Parse(this.txtMaxGiftsPerOrder.Text.Trim() == "" ? "0" : this.txtMaxGiftsPerOrder.Text.Trim());

            //Set the Default Page Title
            objBLLDeals.DealPageTitle = ((divFirstSubDeal.Visible == true) && (iDealRefNo == 0)) ? this.txtTitleMain.Text.Trim() : "";

            objBLLDeals.DealStatus = false;

            objBLLDeals.CreatedBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();

            objBLLDeals.CreatedDate = DateTime.Now;

            //objBLLDeals.DealSlot = int.Parse(this.ddlSideDeal.SelectedValue.Trim());

            objBLLDeals.ParentDealId = iParnetDealId;

            objBLLDeals.topTitle = txtTopTitle.Text.Trim();
            objBLLDeals.shortTitle = txtShortTitle.Text.Trim();
            objBLLDeals.ourCommission = float.Parse(txtOurComission.Text.ToString().Trim());
            try
            {
                if (ddlSalePersonAccountName.SelectedIndex != 0)
                {
                    objBLLDeals.SalePersonAccountName = ddlSalePersonAccountName.SelectedItem.ToString().Trim();
                }
            }
            catch (Exception ex)
            { }

            iDealId = objBLLDeals.AddNewDeal();

            lblMessage.Text = "New Deal has been added successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return iDealId;
    }

    #endregion

    #region"Update Deal Info By Deal ID"

    private void UpdateDealInfoByDealId(string strImageNames, int iResId, string strDealId, int iDealRefNo)
    {
        try
        {
            BLLDeals objBLLDeals = new BLLDeals();
            objBLLDeals.DealId = int.Parse(strDealId);
            //objBLLDeals.RestaurantId = int.Parse(this.ddlSelectRes.SelectedItem.Value.Trim());
            objBLLDeals.RestaurantId = iResId;

            //Set the Deal Title according to the DealRefNo
            if (iDealRefNo == 0)
            {
                objBLLDeals.Title = this.txtTitle.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 1)
            {
                objBLLDeals.Title = this.txtTitle1.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice1.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice1.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax1.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders1.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders1.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax1.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 2)
            {
                objBLLDeals.Title = this.txtTitle2.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice2.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice2.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax2.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders2.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders2.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax2.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 3)
            {
                objBLLDeals.Title = this.txtTitle3.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice3.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice3.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax3.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders3.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders3.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax3.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 4)
            {
                objBLLDeals.Title = this.txtTitle4.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice4.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice4.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax4.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders4.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders4.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax4.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 5)
            {
                objBLLDeals.Title = this.txtTitle5.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice5.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice5.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax5.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders5.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders5.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax5.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 6)
            {
                objBLLDeals.Title = this.txtTitle6.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice6.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice6.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax6.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders6.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders6.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax6.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 7)
            {
                objBLLDeals.Title = this.txtTitle7.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice7.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice7.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax7.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders7.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders7.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax7.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 8)
            {
                objBLLDeals.Title = this.txtTitle8.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice8.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice8.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax8.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders8.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders8.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax8.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 9)
            {
                objBLLDeals.Title = this.txtTitle9.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice9.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice9.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax9.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders9.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders9.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax9.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 10)
            {
                objBLLDeals.Title = this.txtTitle10.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice10.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice10.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax10.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders10.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders10.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax10.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }

            try
            {
                objBLLDeals.yelpRate = float.Parse(ddlReviewRate.SelectedValue.ToString());
            }
            catch (Exception ex)
            { }

            objBLLDeals.tracking = cbTracking.Checked;
            objBLLDeals.doublePoints = cbDoublePoints.Checked;

            objBLLDeals.reviewExist = cbYelpReviews.Checked;
            objBLLDeals.yelpLink = txtReviewLink.Text.Trim();
            objBLLDeals.yelpText = txtReviewText.Text.Trim();

            objBLLDeals.urlTitle = txtURLTitle.Text.Trim();
            objBLLDeals.FinePrint = this.txtFinePrint.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.howtouse = txtHowToUse.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.DealHightlights = this.txtDealHightlights.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.Description = this.elm1.Text.Trim();
            string strStartDate = "";
            string strEndDate = "";

            for (int i = 1; i < dlCities.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                if (chk != null && chk.Checked)
                {
                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[i].FindControl("txtdlStartDate"));
                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[i].FindControl("txtDLEndDate"));
                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartHH"));
                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartMM"));
                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartPortion"));
                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndHH"));
                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndMM"));
                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndPortion"));

                    strStartDate = txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
                    strEndDate = txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
                    break;
                }
            }
            try
            {
                objBLLDeals.DealStartTime = Convert.ToDateTime(strStartDate);
                objBLLDeals.DealEndTime = Convert.ToDateTime(strEndDate);
            }
            catch (Exception ex)
            {
                objBLLDeals.DealStartTime = DateTime.Now;
                objBLLDeals.DealEndTime = DateTime.Now;
            }

            //objBLLDeals.DealStartTime = DateTime.Now;
            //objBLLDeals.DealEndTime = DateTime.Now;
            DateTime dt = DateTime.Now;
            if (cbNoExpiryDate.Checked)
            {
                objBLLDeals.voucherExpiryDateAvailable = false;
            }
            if (DateTime.TryParse(txtVoucherExpiryDate.Text.Trim(), out dt))
            {
                objBLLDeals.voucherExpiryDate = dt;
            }
            objBLLDeals.Images = strImageNames;

            objBLLDeals.DealStatus = this.ddlStatus.SelectedValue == "Yes" ? true : false;

            objBLLDeals.CreatedBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();
            objBLLDeals.CreatedDate = DateTime.Now;
            //objBLLDeals.DealSlot = int.Parse(this.ddlSideDeal.SelectedValue.Trim());

            //Set the Minimum no. of Orders
            objBLLDeals.DealDelMinLmt = int.Parse(this.txtMinNoOfOrders.Text.Trim() == "" ? "0" : this.txtMinNoOfOrders.Text.Trim());


            //Set the Minimum Orders Per User 
            objBLLDeals.MinOrdersPerUser = 0;//int.Parse(this.txtMinOrdersPerUser.Text.Trim());

            //Set the Max Orders Per User 
            objBLLDeals.MaxOrdersPerUser = int.Parse(this.txtMaxOrdersPerUser.Text.Trim() == "" ? "0" : this.txtMaxOrdersPerUser.Text.Trim());

            //Set the Maximum Gifts Per Order 
            objBLLDeals.MaxGiftsPerOrder = int.Parse(this.txtMaxGiftsPerOrder.Text.Trim() == "" ? "0" : this.txtMaxGiftsPerOrder.Text.Trim());

            //Set the Default Page Title
            objBLLDeals.DealPageTitle = ((divFirstSubDeal.Visible == true) && (iDealRefNo == 0)) ? this.txtTitleMain.Text.Trim() : "";
            objBLLDeals.topTitle = txtTopTitle.Text.Trim();
            objBLLDeals.shortTitle = txtShortTitle.Text.Trim();
            objBLLDeals.ourCommission = float.Parse(txtOurComission.Text.Trim());


            if (ddlSalePersonAccountName.SelectedIndex != 0)
            {
                objBLLDeals.SalePersonAccountName = ddlSalePersonAccountName.SelectedItem.ToString().Trim();
            }


            int iChk = objBLLDeals.updateDealInfoByDealId();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private void UpdateDealInfoByDealIdForPreview(string strImageNames, int iResId, string strDealId, int iDealRefNo)
    {
        try
        {
            BLLDeals objBLLDeals = new BLLDeals();
            objBLLDeals.DealId = int.Parse(strDealId);
            //objBLLDeals.RestaurantId = int.Parse(this.ddlSelectRes.SelectedItem.Value.Trim());
            objBLLDeals.RestaurantId = iResId;

            //Set the Deal Title according to the DealRefNo
            if (iDealRefNo == 0)
            {
                objBLLDeals.Title = this.txtTitle.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 1)
            {
                objBLLDeals.Title = this.txtTitle1.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice1.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice1.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax1.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders1.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders1.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax1.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 2)
            {
                objBLLDeals.Title = this.txtTitle2.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice2.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice2.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax2.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders2.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders2.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax2.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 3)
            {
                objBLLDeals.Title = this.txtTitle3.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice3.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice3.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax3.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders3.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders3.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax3.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 4)
            {
                objBLLDeals.Title = this.txtTitle4.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice4.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice4.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax4.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders4.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders4.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax4.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 5)
            {
                objBLLDeals.Title = this.txtTitle5.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice5.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice5.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax5.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders5.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders5.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax5.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 6)
            {
                objBLLDeals.Title = this.txtTitle6.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice6.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice6.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax6.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders6.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders6.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax6.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 7)
            {
                objBLLDeals.Title = this.txtTitle7.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice7.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice7.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax7.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders7.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders7.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax7.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 8)
            {
                objBLLDeals.Title = this.txtTitle8.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice8.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice8.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax8.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders8.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders8.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax8.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 9)
            {
                objBLLDeals.Title = this.txtTitle9.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice9.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice9.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax9.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders9.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders9.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax9.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            else if (iDealRefNo == 10)
            {
                objBLLDeals.Title = this.txtTitle10.Text.Trim();
                objBLLDeals.SellingPrice = float.Parse(this.txtDealPrice10.Text.Trim());
                objBLLDeals.ValuePrice = float.Parse(this.txtActualPrice10.Text.Trim());
                objBLLDeals.AffComm = 10;
                objBLLDeals.shippingAndTax = cbShippingAndTax10.Checked;

                //Set the Maximum no. of Orders
                objBLLDeals.DealDelMaxLmt = int.Parse(this.txtMaxNoOfOrders10.Text.Trim() == "" ? "0" : this.txtMaxNoOfOrders10.Text.Trim());

                float flShippingPrice = 0;
                if (float.TryParse(txtShippingAndTax10.Text.Trim(), out flShippingPrice))
                {
                    objBLLDeals.shippingAndTaxAmount = flShippingPrice;
                }
            }
            try
            {
                objBLLDeals.yelpRate = float.Parse(ddlReviewRate.SelectedValue.ToString());
            }
            catch (Exception ex)
            { }

            objBLLDeals.tracking = cbTracking.Checked;
            objBLLDeals.doublePoints = cbDoublePoints.Checked;

            objBLLDeals.reviewExist = cbYelpReviews.Checked;
            objBLLDeals.yelpLink = txtReviewLink.Text.Trim();
            objBLLDeals.yelpText = txtReviewText.Text.Trim();

            objBLLDeals.urlTitle = txtURLTitle.Text.Trim();
            objBLLDeals.FinePrint = this.txtFinePrint.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.howtouse = txtHowToUse.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.DealHightlights = this.txtDealHightlights.Text.Trim().Replace("\n", "<br>");
            objBLLDeals.Description = this.elm1.Text.Trim();


            objBLLDeals.DealStartTime = DateTime.Now;
            objBLLDeals.DealEndTime = DateTime.Now;
            DateTime dt = DateTime.Now;
            if (cbNoExpiryDate.Checked)
            {
                objBLLDeals.voucherExpiryDateAvailable = false;
            }
            if (DateTime.TryParse(txtVoucherExpiryDate.Text.Trim(), out dt))
            {
                objBLLDeals.voucherExpiryDate = dt;
            }
            objBLLDeals.Images = strImageNames;

            objBLLDeals.DealStatus = this.ddlStatus.SelectedValue == "Yes" ? true : false;

            objBLLDeals.CreatedBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();
            objBLLDeals.CreatedDate = DateTime.Now;

            //objBLLDeals.DealSlot = int.Parse(this.ddlSideDeal.SelectedValue.Trim());

            //Set the Minimum no. of Orders
            objBLLDeals.DealDelMinLmt = int.Parse(this.txtMinNoOfOrders.Text.Trim() == "" ? "0" : this.txtMinNoOfOrders.Text.Trim());


            //Set the Minimum Orders Per User 
            objBLLDeals.MinOrdersPerUser = 0;//int.Parse(this.txtMinOrdersPerUser.Text.Trim());

            //Set the Max Orders Per User 
            objBLLDeals.MaxOrdersPerUser = int.Parse(this.txtMaxOrdersPerUser.Text.Trim() == "" ? "0" : this.txtMaxOrdersPerUser.Text.Trim());

            //Set the Maximum Gifts Per Order 
            objBLLDeals.MaxGiftsPerOrder = int.Parse(this.txtMaxGiftsPerOrder.Text.Trim() == "" ? "0" : this.txtMaxGiftsPerOrder.Text.Trim());

            //Set the Default Page Title
            objBLLDeals.DealPageTitle = ((divFirstSubDeal.Visible == true) && (iDealRefNo == 0)) ? this.txtTitleMain.Text.Trim() : "";
            objBLLDeals.topTitle = txtTopTitle.Text.Trim();
            objBLLDeals.shortTitle = txtShortTitle.Text.Trim();
            objBLLDeals.ourCommission = float.Parse(txtOurComission.Text.Trim());
            int iChk = objBLLDeals.updateDealInfoByDealId();
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #endregion

    private void ResetAllValuesForNewEntry()
    {
        try
        {
            //Add New Deal Info
                        
            //Show the Add New Deal here            
            //Reset the Fields
            hfResturnatID.Value = "0";
            this.txtTitle.Text = "";
            this.txtTopTitle.Text = "";
            txtShortTitle.Text = "";
            this.txtFinePrint.Text = "";
            txtURLTitle.Text = "";
            cbForDesign.Checked = false;
            cbNoExpiryDate.Checked = false;
            cbShippingAndTax.Checked = false;
            cbShippingAndTax1.Checked = false;
            cbShippingAndTax2.Checked = false;
            cbShippingAndTax3.Checked = false;
            cbShippingAndTax4.Checked = false;
            cbShippingAndTax5.Checked = false;
            cbShippingAndTax6.Checked = false;
            cbShippingAndTax7.Checked = false;
            cbShippingAndTax8.Checked = false;
            cbShippingAndTax9.Checked = false;
            cbShippingAndTax10.Checked = false;

            txtShippingAndTax.Text = "";
            txtShippingAndTax1.Text = "";
            txtShippingAndTax2.Text = "";
            txtShippingAndTax3.Text = "";
            txtShippingAndTax4.Text = "";
            txtShippingAndTax5.Text = "";
            txtShippingAndTax6.Text = "";
            txtShippingAndTax7.Text = "";
            txtShippingAndTax8.Text = "";
            txtShippingAndTax9.Text = "";
            txtShippingAndTax10.Text = "";


            rfvShippingAndTax.Enabled = false;
            rfvShippingAndTax1.Enabled = false;
            rfvShippingAndTax2.Enabled = false;
            rfvShippingAndTax3.Enabled = false;
            rfvShippingAndTax4.Enabled = false;
            rfvShippingAndTax5.Enabled = false;
            rfvShippingAndTax6.Enabled = false;
            rfvShippingAndTax7.Enabled = false;
            rfvShippingAndTax8.Enabled = false;
            rfvShippingAndTax9.Enabled = false;
            rfvShippingAndTax10.Enabled = false;

            rfvReviewText.Enabled = false;
            rfvReviewLink.Enabled = false;
            reReviewLink.Enabled = false;

            cbTracking.Checked = false;
            cbDoublePoints.Checked = false;            

            cbYelpReviews.Checked = false;
            ddlReviewRate.SelectedIndex = 0;
            txtReviewText.Text = "";
            txtReviewLink.Text = "";

            txtHowToUse.Text = "";
            this.txtDealHightlights.Text = "";
            this.elm1.Text = "";
            this.txtDealPrice.Text = "";
            this.txtActualPrice.Text = "";
            // this.txtStartDate.Text = "";
            //this.txtEndDate.Text = "";
            this.txtVoucherExpiryDate.Text = "";

            //this.ddlStartPortion.SelectedValue = "AM";
            // this.ddlStartHH.SelectedValue = "0";
            // this.ddlStartMM.SelectedValue = "0";

            // this.ddlEndPortion.SelectedValue = "AM";
            // this.ddlEndHH.SelectedValue = "0";
            // this.ddlEndMM.SelectedValue = "0";

            this.imgUpload1.Visible = false;
            this.imgUpload2.Visible = false;
            imgUpload2Remove.Visible = false;
            imgUpload3Remove.Visible = false;
            this.imgUpload3.Visible = false;

            this.txtMinNoOfOrders.Text = "";
            this.txtMaxNoOfOrders.Text = "";
            this.txtMaxNoOfOrders1.Text = "";
            this.txtMaxNoOfOrders2.Text = "";
            this.txtMaxNoOfOrders3.Text = "";
            this.txtMaxNoOfOrders4.Text = "";
            this.txtMaxNoOfOrders5.Text = "";
            this.txtMaxNoOfOrders6.Text = "";
            this.txtMaxNoOfOrders7.Text = "";
            this.txtMaxNoOfOrders8.Text = "";
            this.txtMaxNoOfOrders9.Text = "";
            this.txtMaxNoOfOrders10.Text = "";


            this.txtMaxOrdersPerUser.Text = "";
            imgUpload1.Src = "";
            imgUpload2.Src = "";
            imgUpload3.Src = "";


            txtOurComission.Text = "";

            ddlSalePersonAccountName.SelectedIndex = 0;


        }
        catch (Exception ex)
        { }
    }

    protected bool getDetailStatus(object status)
    {
        try
        {
            if (status.ToString() != "")
            {
                if (Convert.ToInt32(status.ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #region Add SubDeal Funcitons
    protected void lBtn0AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the
            this.lBtn0AddThirdDiv.Visible = false;
            this.lBtn1DeleteSecondDiv.Visible = true;
            //Show the First Div here
            this.divFirstSubDeal.Visible = true;

            //Refresh all the First Deal Info
            this.txtTitle1.Text = "";
            this.txtDealPrice1.Text = "";
            this.txtActualPrice1.Text = "";
            txtMaxNoOfOrders1.Text = "";
            txtDealPrice1.Text = "";
            txtActualPrice1.Text = "";
            cbShippingAndTax1.Checked = false;
            txtShippingAndTax1.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void imgUpload2Remove_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            imgUpload2.Src = "";
            imgUpload2.Visible = false;
            imgUpload2Remove.Visible = false;
        }
        catch (Exception ex)
        { }
    }

    protected void imgUpload3Remove_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            imgUpload3.Src = "";
            imgUpload3.Visible = false;
            imgUpload3Remove.Visible = false;
        }
        catch (Exception ex)
        { }
    }

    protected void lBtn1AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn1AddThirdDiv.Visible = false;
            this.lBtn1DeleteSecondDiv.Visible = false;
            this.lBtn2DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divSecondSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle2.Text = "";
            this.txtDealPrice2.Text = "";
            this.txtActualPrice2.Text = "";
            txtMaxNoOfOrders2.Text = "";
            txtDealPrice2.Text = "";
            txtActualPrice2.Text = "";
            cbShippingAndTax2.Checked = false;
            txtShippingAndTax2.Text = "";
        }
        catch (Exception ex)
        { }
    }

    protected void lBtn1DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Show the First Div button

        this.lBtn0AddThirdDiv.Visible = true;

        //Hide the First Div here
        this.divFirstSubDeal.Visible = false;
    }

    protected void lBtn2AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Third Add New Button
            this.lBtn2AddThirdDiv.Visible = false;
            this.lBtn2DeleteSecondDiv.Visible = false;
            this.lBtn3DeleteSecondDiv.Visible = true;
            //Show the Third Div here
            this.divThirdSubDeal.Visible = true;

            //Refresh all the Third Deal Info
            this.txtTitle3.Text = "";
            this.txtDealPrice3.Text = "";
            this.txtActualPrice3.Text = "";
            txtMaxNoOfOrders3.Text = "";
            txtDealPrice3.Text = "";
            txtActualPrice3.Text = "";
            cbShippingAndTax3.Checked = false;
            txtShippingAndTax3.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn3AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn3AddThirdDiv.Visible = false;
            this.lBtn3DeleteSecondDiv.Visible = false;
            this.lBtn4DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divForthSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle4.Text = "";
            this.txtDealPrice4.Text = "";
            this.txtActualPrice4.Text = "";
            txtMaxNoOfOrders4.Text = "";
            txtDealPrice4.Text = "";
            txtActualPrice4.Text = "";
            cbShippingAndTax4.Checked = false;
            txtShippingAndTax4.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn3DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn2AddThirdDiv.Visible = true;

        if (hfDealId2.Value.Trim() == "0")
        {
            this.lBtn2DeleteSecondDiv.Visible = true;
        }
        //Show the Third Div here
        this.divThirdSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void lBtn4AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn4AddThirdDiv.Visible = false;
            this.lBtn4DeleteSecondDiv.Visible = false;
            this.lBtn5DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divFifthSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle5.Text = "";
            this.txtDealPrice5.Text = "";
            this.txtActualPrice5.Text = "";
            txtMaxNoOfOrders5.Text = "";
            txtDealPrice5.Text = "";
            txtActualPrice5.Text = "";
            cbShippingAndTax5.Checked = false;
            txtShippingAndTax5.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn4DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn3AddThirdDiv.Visible = true;

        if (hfDealId3.Value.Trim() == "0")
        {
            this.lBtn3DeleteSecondDiv.Visible = true;
        }
        //Show the Third Div here
        this.divForthSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void lBtn2DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn1AddThirdDiv.Visible = true;

        if (hfDealId1.Value.Trim() == "0")
        {
            this.lBtn1DeleteSecondDiv.Visible = true;
        }
        //Show the Third Div here
        this.divSecondSubDeal.Visible = false;
    }

    protected void lBtn5AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn5AddThirdDiv.Visible = false;
            this.lBtn5DeleteSecondDiv.Visible = false;
            this.lBtn6DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divSixthSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle6.Text = "";
            this.txtDealPrice6.Text = "";
            this.txtActualPrice6.Text = "";
            txtMaxNoOfOrders6.Text = "";
            txtDealPrice6.Text = "";
            txtActualPrice6.Text = "";
            cbShippingAndTax6.Checked = false;
            txtShippingAndTax6.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn5DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn4AddThirdDiv.Visible = true;

        if (hfDealId4.Value.Trim() == "0")
        {
            this.lBtn4DeleteSecondDiv.Visible = true;
        }
        //Show the Third Div here
        this.divFifthSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void lBtn6AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn6AddThirdDiv.Visible = false;
            this.lBtn6DeleteSecondDiv.Visible = false;
            this.lBtn7DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divSeventhSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle7.Text = "";
            this.txtDealPrice7.Text = "";
            this.txtActualPrice7.Text = "";
            txtMaxNoOfOrders7.Text = "";
            txtDealPrice7.Text = "";
            txtActualPrice7.Text = "";
            cbShippingAndTax7.Checked = false;
            txtShippingAndTax7.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn6DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn5AddThirdDiv.Visible = true;

        if (hfDealId5.Value.Trim() == "0")
        {
            this.lBtn5DeleteSecondDiv.Visible = true;
        }
        //Show the Third Div here
        this.divSixthSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void lBtn7AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn7AddThirdDiv.Visible = false;
            this.lBtn7DeleteSecondDiv.Visible = false;
            this.lBtn8DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divEightSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle8.Text = "";
            this.txtDealPrice8.Text = "";
            this.txtActualPrice8.Text = "";
            txtMaxNoOfOrders8.Text = "";
            txtDealPrice8.Text = "";
            txtActualPrice8.Text = "";
            cbShippingAndTax8.Checked = false;
            txtShippingAndTax8.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn7DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn6AddThirdDiv.Visible = true;

        if (hfDealId6.Value.Trim() == "0")
        {
            this.lBtn6DeleteSecondDiv.Visible = true;
        }
        //Show the Third Div here
        this.divSeventhSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void lBtn8AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn8AddThirdDiv.Visible = false;
            this.lBtn8DeleteSecondDiv.Visible = false;
            this.lBtn9DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divNinthSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle9.Text = "";
            this.txtDealPrice9.Text = "";
            this.txtActualPrice9.Text = "";
            txtMaxNoOfOrders9.Text = "";
            txtDealPrice9.Text = "";
            txtActualPrice9.Text = "";
            cbShippingAndTax9.Checked = false;
            txtShippingAndTax9.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn8DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn7AddThirdDiv.Visible = true;
        if (hfDealId7.Value.Trim() == "0")
        {
            this.lBtn7DeleteSecondDiv.Visible = true;
        }
        

        //Show the Third Div here
        this.divEightSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void lBtn9AddThirdDiv_Click(object sender, EventArgs e)
    {
        try
        {
            checkExpiryCheck();
            //Hide the Second Add New & Delete Button
            this.lBtn9AddThirdDiv.Visible = false;
            this.lBtn9DeleteSecondDiv.Visible = false;
            this.lBtn10DeleteSecondDiv.Visible = true;
            //Show the Second Div here
            this.divTenthSubDeal.Visible = true;

            //Refresh all the Second Deal Info
            this.txtTitle10.Text = "";
            this.txtDealPrice10.Text = "";
            this.txtActualPrice10.Text = "";
            txtMaxNoOfOrders10.Text = "";
            txtDealPrice10.Text = "";
            txtActualPrice10.Text = "";
            cbShippingAndTax10.Checked = false;
            txtShippingAndTax10.Text = "";

        }
        catch (Exception ex)
        { }
    }

    protected void lBtn9DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn8AddThirdDiv.Visible = true;
        if (hfDealId8.Value.Trim() == "0")
        {
            this.lBtn8DeleteSecondDiv.Visible = true;
        }

        //Show the Third Div here
        this.divNinthSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void lBtn10DeleteSecondDiv_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        //Hide the Third Add New Button
        this.lBtn9AddThirdDiv.Visible = true;
        if (hfDealId9.Value.Trim() == "0")
        {
            this.lBtn9DeleteSecondDiv.Visible = true;
        }

        //Show the Third Div here
        this.divTenthSubDeal.Visible = false;
        //Show the Third Add New $ DeleteButton
    }

    protected void BtnPreview_Click(object sender, EventArgs e)
    {
        checkExpiryCheck();
        string strImageName = "";
        int iResID = 0;
        int checkedcounter = 0;
        bool NationalDeal = false;
        CheckBox checkNationalDeals = (CheckBox)dlCities.Items[0].FindControl("chkbxSelect");
        if (checkNationalDeals != null && checkNationalDeals.Checked)
        {
            NationalDeal = true;
            Panel pnlDealTimeDetail = (Panel)dlCities.Items[0].FindControl("pnlDealTimeDetail");
            if (pnlDealTimeDetail != null)
            {
                pnlDealTimeDetail.CssClass = "showTimeDetail";
            }
        }
        if (!cbForDesign.Checked)
        {
            for (int i = 0; i < dlCities.Items.Count; i++)
            {
                try
                {
                    CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                    if (chk != null && chk.Checked)
                    {
                        checkedcounter++;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                    return;
                }
            }
            if (checkedcounter == 0)
            {
                lblMessage.Text = "Please select a city";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                return;
            }
        }

        try
        {
            iResID = int.Parse(Request.QueryString["resID"].ToString().Trim());

            iResID = iResID > 0 ? iResID : 0;
        }
        catch (Exception ex)
        { iResID = 0; }

        if (iResID != 0)
        {
            //string strStartDate = this.txtStartDate.Text.Trim() + " " + ((this.ddlStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(this.ddlStartHH.SelectedItem.Text.Trim()) + 12).ToString() : this.ddlStartHH.SelectedItem.Text.Trim()).ToString() + ":" + this.ddlStartMM.SelectedItem.Text + ":" + "00";
            //string strEndDate = this.txtEndDate.Text.Trim() + " " + ((this.ddlEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(this.ddlEndHH.SelectedItem.Text.Trim()) + 12).ToString() : this.ddlEndHH.SelectedItem.Text.Trim()).ToString() + ":" + this.ddlEndMM.SelectedItem.Text + ":" + "00";

            //Save the Deal Info
            if (this.btnImgSave.ToolTip == "Save Deal Info")
            {
                //Check In the Current Period of Deal, already Deal exists or not
                //                if (AlreadyDealExistsWithSameStartEndTime(iResID, strStartDate, strEndDate, "") == false)
                if (NationalDeal)
                {                    
                    //If Image 1 exists
                    if (fpDealImage1.HasFile)
                    {
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                        this.rfvDealImage1.ValidationGroup = "";

                        //Set the First Image here
                        this.imgUpload1.Src = "../Images/dealFood/" + iResID.ToString() + "/" + strImageName;
                        this.imgUpload1.Visible = true;
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        //upload the Image here
                        string strImageLocal = ImageUploadHere(fpDealImage2, iResID);
                        strImageName += "," + strImageLocal;

                        //Set the First Image here
                        this.imgUpload2.Src = "../Images/dealFood/" + iResID.ToString() + "/" + strImageLocal;
                        this.imgUpload2.Visible = true;
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        //upload the Image here
                        string strImageLocal = ImageUploadHere(fpDealImage3, iResID);
                        strImageName += "," + strImageLocal;

                        this.imgUpload3.Src = "../Images/dealFood/" + iResID.ToString() + "/" + strImageLocal;
                        this.imgUpload3.Visible = true;
                    }

                    //Add New Deal Info & first tie it will be the parnet
                    int iDealId = AddNewDealInfo(strImageName, iResID, 0, 0);
                    NewDealID = iDealId;

                    long subdealID1 = 0;
                    long subdealID2 = 0;
                    long subdealID3 = 0;
                    long subdealID4 = 0;
                    long subdealID5 = 0;
                    long subdealID6 = 0;
                    long subdealID7 = 0;
                    long subdealID8 = 0;
                    long subdealID9 = 0;
                    long subdealID10 = 0;

                    if (iDealId != 0)
                    {
                        //Check First Sub Deal Info is provided or not
                        if (this.divFirstSubDeal.Visible == true)
                        {
                            subdealID1 = AddNewDealInfo(strImageName, iResID, iDealId, 1);
                        }
                        //Check Second Sub Deal Info is provided or not
                        if (this.divSecondSubDeal.Visible == true)
                        {
                            subdealID2 = AddNewDealInfo(strImageName, iResID, iDealId, 2);
                        }
                        //Check Third Sub Deal Info is provided or not
                        if (this.divThirdSubDeal.Visible == true)
                        {
                            subdealID3 = AddNewDealInfo(strImageName, iResID, iDealId, 3);
                        }
                        if (this.divForthSubDeal.Visible == true)
                        {
                            subdealID4 = AddNewDealInfo(strImageName, iResID, iDealId, 4);
                        }
                        if (this.divFifthSubDeal.Visible == true)
                        {
                            subdealID5 = AddNewDealInfo(strImageName, iResID, iDealId, 5);
                        }
                        if (this.divSixthSubDeal.Visible == true)
                        {
                            subdealID6 = AddNewDealInfo(strImageName, iResID, iDealId, 6);
                        }
                        if (this.divSeventhSubDeal.Visible == true)
                        {
                            subdealID7 = AddNewDealInfo(strImageName, iResID, iDealId, 7);
                        }
                        if (this.divEightSubDeal.Visible == true)
                        {
                            subdealID8 = AddNewDealInfo(strImageName, iResID, iDealId, 8);
                        }
                        if (this.divNinthSubDeal.Visible == true)
                        {
                            subdealID9 = AddNewDealInfo(strImageName, iResID, iDealId, 9);
                        }
                        if (this.divTenthSubDeal.Visible == true)
                        {
                            subdealID10 = AddNewDealInfo(strImageName, iResID, iDealId, 10);
                        }
                    }

                    BLLDealCity objCity = new BLLDealCity();
                    try
                    {
                        TextBox txtdlStartDate = (TextBox)(dlCities.Items[0].FindControl("txtdlStartDate"));
                        TextBox txtDLEndDate = (TextBox)(dlCities.Items[0].FindControl("txtDLEndDate"));
                        DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartHH"));
                        DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartMM"));
                        DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartPortion"));
                        DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndHH"));
                        DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndMM"));
                        DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndPortion"));
                        DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[0].FindControl("ddlDLSideDeal"));

                        if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                            && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                            && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                        {

                            if (txtdlStartDate.Text.Trim() == "")
                            {
                                lblMessage.Text = "Please select start time";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "images/error.png";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            if (txtDLEndDate.Text.Trim() == "")
                            {
                                lblMessage.Text = "Please select end time";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "images/error.png";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                            string strStartDate = txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
                            string strEndDate = txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
                            BLLDeals objBLLDeals = new BLLDeals();
                            objBLLDeals.RestaurantId = iResID;
                            objBLLDeals.DealStartTime = DateTime.Parse(strStartDate);
                            objBLLDeals.DealEndTime = DateTime.Parse(strEndDate);
                            objBLLDeals.DealSlot = int.Parse(ddlDLSideDeal.SelectedValue.Trim());
                            bumpUpSlot();
                            for (int i = 0; i < dlCities.Items.Count; i++)
                            {
                                Label lblCityID = (Label)(dlCities.Items[i].FindControl("lblCityID"));
                                if (lblCityID != null && lblCityID.Text.Trim() != "")
                                {
                                    objBLLDeals.cityId = Convert.ToInt32(lblCityID.Text);
                                    objCity.CityId = Convert.ToInt32(lblCityID.Text);
                                    DataTable dtDealsInfo;
                                    //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                                    dtDealsInfo = objBLLDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                                    if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                                    {
                                        int dealSlot = 1;
                                        for (int a = 1; a <= 150; a++)
                                        {
                                            DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                            if (foundRows.Length == 0)
                                            {
                                                dealSlot = a;
                                                break;
                                            }
                                        }
                                        objCity.DealSlotC = dealSlot;
                                    }
                                    else
                                    {
                                        objCity.DealSlotC = 1;
                                    }
                                    objCity.DealStartTimeC = DateTime.Parse(strStartDate);
                                    objCity.DealEndTimeC = DateTime.Parse(strEndDate);

                                    objCity.DealId = iDealId;

                                    objCity.createDealCity();

                                    if (iDealId != 0)
                                    {
                                        //Check First Sub Deal Info is provided or not
                                        if (subdealID1 > 0)
                                        {
                                            objCity.DealId = subdealID1;
                                            objCity.createDealCity();
                                        }
                                        //Check Second Sub Deal Info is provided or not
                                        if (subdealID2 > 0)
                                        {
                                            objCity.DealId = subdealID2;
                                            objCity.createDealCity();
                                        }
                                        //Check Third Sub Deal Info is provided or not
                                        if (subdealID3 > 0)
                                        {
                                            objCity.DealId = subdealID3;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID4 > 0)
                                        {
                                            objCity.DealId = subdealID4;
                                            objCity.createDealCity();
                                        }
                                        if (subdealID5 > 0)
                                        {
                                            objCity.DealId = subdealID5;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID6 > 0)
                                        {
                                            objCity.DealId = subdealID6;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID7 > 0)
                                        {
                                            objCity.DealId = subdealID7;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID8 > 0)
                                        {
                                            objCity.DealId = subdealID8;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID9 > 0)
                                        {
                                            objCity.DealId = subdealID9;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID10 > 0)
                                        {
                                            objCity.DealId = subdealID10;
                                            objCity.createDealCity();
                                        }

                                    }
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (AlreadyDealExistsWithSameStartEndTimeWithCityName(iResID, "") == false)
                {
                    //If Image 1 exists
                    if (fpDealImage1.HasFile)
                    {
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                        this.rfvDealImage1.ValidationGroup = "";

                        //Set the First Image here
                        this.imgUpload1.Src = "../Images/dealFood/" + iResID.ToString() + "/" + strImageName;
                        this.imgUpload1.Visible = true;
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        //upload the Image here
                        string strImageLocal = ImageUploadHere(fpDealImage2, iResID);
                        strImageName += "," + strImageLocal;

                        //Set the First Image here
                        this.imgUpload2.Src = "../Images/dealFood/" + iResID.ToString() + "/" + strImageLocal;
                        this.imgUpload2.Visible = true;
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        //upload the Image here
                        string strImageLocal = ImageUploadHere(fpDealImage3, iResID);
                        strImageName += "," + strImageLocal;

                        this.imgUpload3.Src = "../Images/dealFood/" + iResID.ToString() + "/" + strImageLocal;
                        this.imgUpload3.Visible = true;
                    }
                    //Add New Deal Info & first tie it will be the parnet
                    int iDealId = AddNewDealInfoForPreview(strImageName, iResID, 0, 0);
                    NewDealID = iDealId;
                    hfDealId.Value = iDealId.ToString();
                    BLLDealCity objCity = new BLLDealCity();


                    long subdealID1 = 0;
                    long subdealID2 = 0;
                    long subdealID3 = 0;
                    long subdealID4 = 0;
                    long subdealID5 = 0;
                    long subdealID6 = 0;
                    long subdealID7 = 0;
                    long subdealID8 = 0;
                    long subdealID9 = 0;
                    long subdealID10 = 0;


                    if (iDealId != 0)
                    {
                        //Check First Sub Deal Info is provided or not
                        if (this.divFirstSubDeal.Visible == true)
                        {
                            subdealID1 = AddNewDealInfo(strImageName, iResID, iDealId, 1);
                        }
                        //Check Second Sub Deal Info is provided or not
                        if (this.divSecondSubDeal.Visible == true)
                        {
                            subdealID2 = AddNewDealInfo(strImageName, iResID, iDealId, 2);
                        }
                        //Check Third Sub Deal Info is provided or not
                        if (this.divThirdSubDeal.Visible == true)
                        {
                            subdealID3 = AddNewDealInfo(strImageName, iResID, iDealId, 3);
                        }
                        //Check Forth Sub Deal Info is provided or not
                        if (this.divForthSubDeal.Visible == true)
                        {
                            subdealID4 = AddNewDealInfo(strImageName, iResID, iDealId, 4);
                        }
                        if (this.divFifthSubDeal.Visible == true)
                        {
                            subdealID5 = AddNewDealInfo(strImageName, iResID, iDealId, 5);
                        }
                        if (this.divSixthSubDeal.Visible == true)
                        {
                            subdealID6 = AddNewDealInfo(strImageName, iResID, iDealId, 6);
                        }
                        if (this.divSeventhSubDeal.Visible == true)
                        {
                            subdealID7 = AddNewDealInfo(strImageName, iResID, iDealId, 7);
                        }
                        if (this.divEightSubDeal.Visible == true)
                        {
                            subdealID8 = AddNewDealInfo(strImageName, iResID, iDealId, 8);
                        }
                        if (this.divNinthSubDeal.Visible == true)
                        {
                            subdealID9 = AddNewDealInfo(strImageName, iResID, iDealId, 9);
                        }
                        if (this.divTenthSubDeal.Visible == true)
                        {
                            subdealID10 = AddNewDealInfo(strImageName, iResID, iDealId, 10);
                        }

                    }
                    if (!cbForDesign.Checked)
                    {
                        for (int i = 1; i < dlCities.Items.Count; i++)
                        {
                            try
                            {
                                CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                                Label lblCityID = (Label)dlCities.Items[i].FindControl("lblCityID");
                                if (chk != null && lblCityID != null && lblCityID.Text.Trim() != "" && chk.Checked)
                                {
                                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[i].FindControl("txtdlStartDate"));
                                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[i].FindControl("txtDLEndDate"));
                                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartHH"));
                                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartMM"));
                                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartPortion"));
                                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndHH"));
                                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndMM"));
                                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndPortion"));
                                    DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[i].FindControl("ddlDLSideDeal"));

                                    if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                                        && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                                        && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                                    {
                                        objCity.DealStartTimeC = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealEndTimeC = DateTime.Parse(txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealSlotC = Convert.ToInt32(ddlDLSideDeal.SelectedValue.Trim());
                                    }
                                    objCity.DealId = iDealId;
                                    objCity.CityId = Convert.ToInt32(lblCityID.Text.Trim());
                                    objCity.createDealCity();

                                    if (iDealId != 0)
                                    {
                                        //Check First Sub Deal Info is provided or not
                                        if (subdealID1 > 0)
                                        {
                                            objCity.DealId = subdealID1;
                                            objCity.createDealCity();
                                        }
                                        //Check Second Sub Deal Info is provided or not
                                        if (subdealID2 > 0)
                                        {
                                            objCity.DealId = subdealID2;
                                            objCity.createDealCity();
                                        }
                                        //Check Third Sub Deal Info is provided or not
                                        if (subdealID3 > 0)
                                        {
                                            objCity.DealId = subdealID3;
                                            objCity.createDealCity();
                                        }
                                        if (subdealID4 > 0)
                                        {
                                            objCity.DealId = subdealID4;
                                            objCity.createDealCity();
                                        }
                                        if (subdealID5 > 0)
                                        {
                                            objCity.DealId = subdealID5;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID6 > 0)
                                        {
                                            objCity.DealId = subdealID6;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID7 > 0)
                                        {
                                            objCity.DealId = subdealID7;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID8 > 0)
                                        {
                                            objCity.DealId = subdealID8;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID9 > 0)
                                        {
                                            objCity.DealId = subdealID9;
                                            objCity.createDealCity();
                                        }

                                        if (subdealID10 > 0)
                                        {
                                            objCity.DealId = subdealID10;
                                            objCity.createDealCity();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        try
                        {

                            objCity.DealStartTimeC = DateTime.Now.AddMonths(-3);
                            objCity.DealEndTimeC = DateTime.Now.AddMonths(-2);
                            objCity.DealId = iDealId;
                            objCity.CityId = 337;

                            BLLDeals objBLLDeals = new BLLDeals();
                            objBLLDeals.DealStartTime = objCity.DealStartTimeC;
                            objBLLDeals.DealEndTime = objCity.DealEndTimeC;
                            objBLLDeals.cityId = 337;
                            DataTable dtDealsInfo;
                            //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                            dtDealsInfo = objBLLDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                            if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                            {
                                int dealSlot = 1;
                                for (int a = 1; a <= 150; a++)
                                {
                                    DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                    if (foundRows.Length == 0)
                                    {
                                        dealSlot = a;
                                        break;
                                    }
                                }
                                objCity.DealSlotC = dealSlot;
                            }
                            else
                            {
                                objCity.DealSlotC = 1;
                            }
                            objCity.createDealCity();

                            if (iDealId != 0)
                            {
                                //Check First Sub Deal Info is provided or not
                                if (subdealID1 > 0)
                                {
                                    objCity.DealId = subdealID1;
                                    objCity.createDealCity();
                                }
                                //Check Second Sub Deal Info is provided or not
                                if (subdealID2 > 0)
                                {
                                    objCity.DealId = subdealID2;
                                    objCity.createDealCity();
                                }
                                //Check Third Sub Deal Info is provided or not
                                if (subdealID3 > 0)
                                {
                                    objCity.DealId = subdealID3;
                                    objCity.createDealCity();
                                }
                                //Check Forth Sub Deal Info is provided or not
                                if (subdealID4 > 0)
                                {
                                    objCity.DealId = subdealID4;
                                    objCity.createDealCity();
                                }
                                if (subdealID5 > 0)
                                {
                                    objCity.DealId = subdealID5;
                                    objCity.createDealCity();
                                }

                                if (subdealID6 > 0)
                                {
                                    objCity.DealId = subdealID6;
                                    objCity.createDealCity();
                                }

                                if (subdealID7 > 0)
                                {
                                    objCity.DealId = subdealID7;
                                    objCity.createDealCity();
                                }

                                if (subdealID8 > 0)
                                {
                                    objCity.DealId = subdealID8;
                                    objCity.createDealCity();
                                }

                                if (subdealID9 > 0)
                                {
                                    objCity.DealId = subdealID9;
                                    objCity.createDealCity();
                                }

                                if (subdealID10 > 0)
                                {
                                    objCity.DealId = subdealID10;
                                    objCity.createDealCity();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    //Validate that DealId Should not be "0"



                    //
                    //
                    //
                    //
                    //
                    //

                }
            }
            //Update the Deal Info
            else if (this.btnImgSave.ToolTip == "Update Deal Info")
            {
                string strResID = hfResturnatID.Value;

                iResID = int.Parse(strResID);

                if (NationalDeal)
                {
                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[0].FindControl("txtdlStartDate"));
                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[0].FindControl("txtDLEndDate"));
                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartHH"));
                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartMM"));
                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLStartPortion"));
                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndHH"));
                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndMM"));
                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[0].FindControl("ddlDLEndPortion"));
                    DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[0].FindControl("ddlDLSideDeal"));

                    if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                        && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                        && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                    {

                        if (txtdlStartDate.Text.Trim() == "")
                        {
                            lblMessage.Text = "Please select start time";
                            lblMessage.Visible = true;
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        if (txtDLEndDate.Text.Trim() == "")
                        {
                            lblMessage.Text = "Please select end time";
                            lblMessage.Visible = true;
                            imgGridMessage.Visible = true;
                            imgGridMessage.ImageUrl = "images/error.png";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }

                    //If Image 1 exists
                    if (fpDealImage1.HasFile)
                    {
                        if (this.imgUpload1.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload1.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                    }
                    else
                    {
                        strImageName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        if (this.imgUpload2.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload2.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        if (this.imgUpload3.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload3.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }

                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //Update the Deal info by Deal Id
                    UpdateDealInfoByDealId(strImageName, iResID, hfDealId.Value, 0);
                    long subDealID1 = 0;
                    long subDealID2 = 0;
                    long subDealID3 = 0;
                    long subDealID4 = 0;
                    long subDealID5 = 0;
                    long subDealID6 = 0;
                    long subDealID7 = 0;
                    long subDealID8 = 0;
                    long subDealID9 = 0;
                    long subDealID10 = 0;

                    //Check First Sub Deal Info is provided or not
                    if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId1.Value.Trim(), 1);
                        subDealID1 = Convert.ToInt64(hfDealId1.Value);
                    }
                    else if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() == "0"))
                    {
                        subDealID1 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 1);
                    }
                    else if ((this.divFirstSubDeal.Visible == false) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId1.Value.Trim());
                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId1.Value = "0";
                        subDealID1 = 0;
                    }

                    // 
                    //Check Second Sub Deal Info is provided or not
                    if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId2.Value.Trim(), 2);
                        subDealID2 = Convert.ToInt64(hfDealId2.Value);
                    }
                    else if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() == "0"))
                    {
                        subDealID2 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 2);
                    }
                    else if ((this.divSecondSubDeal.Visible == false) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId2.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId2.Value = "0";
                        subDealID2 = 0;
                    }

                    //Check Third Sub Deal Info is provided or not
                    if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId3.Value.Trim(), 3);
                        subDealID3 = Convert.ToInt64(hfDealId3.Value);
                    }
                    else if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() == "0"))
                    {
                        subDealID3 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 3);
                    }
                    else if ((this.divThirdSubDeal.Visible == false) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId3.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId3.Value = "0";
                        subDealID3 = 0;
                    }

                    //Check Forth Sub Deal info is prvided or not
                    if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId4.Value.Trim(), 4);
                        subDealID4 = Convert.ToInt64(hfDealId4.Value);
                    }
                    else if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() == "0"))
                    {
                        subDealID4 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 4);
                    }
                    else if ((this.divForthSubDeal.Visible == false) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId4.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId4.Value = "0";
                        subDealID4 = 0;
                    }

                    //Check Fifth Sub Deal info is prvided or not
                    if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId5.Value.Trim(), 5);
                        subDealID5 = Convert.ToInt64(hfDealId5.Value);
                    }
                    else if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() == "0"))
                    {
                        subDealID5 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 5);
                    }
                    else if ((this.divFifthSubDeal.Visible == false) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId5.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId5.Value = "0";
                        subDealID5 = 0;
                    }

                    //Check Sixth Sub Deal info is prvided or not
                    if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId6.Value.Trim(), 6);
                        subDealID6 = Convert.ToInt64(hfDealId6.Value);
                    }
                    else if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() == "0"))
                    {
                        subDealID6 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 6);
                    }
                    else if ((this.divSixthSubDeal.Visible == false) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId6.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId6.Value = "0";
                        subDealID6 = 0;
                    }

                    //Check Seven Sub Deal info is prvided or not
                    if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId7.Value.Trim(), 7);
                        subDealID7 = Convert.ToInt64(hfDealId7.Value);
                    }
                    else if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() == "0"))
                    {
                        subDealID7 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 7);
                    }
                    else if ((this.divSeventhSubDeal.Visible == false) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId7.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId7.Value = "0";
                        subDealID7 = 0;
                    }

                    //Check Eight Sub Deal info is prvided or not
                    if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId8.Value.Trim(), 8);
                        subDealID8 = Convert.ToInt64(hfDealId8.Value);
                    }
                    else if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() == "0"))
                    {
                        subDealID8 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 8);
                    }
                    else if ((this.divEightSubDeal.Visible == false) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId8.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId8.Value = "0";
                        subDealID8 = 0;
                    }

                    //Check Ninth Sub Deal info is prvided or not
                    if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId9.Value.Trim(), 9);
                        subDealID9 = Convert.ToInt64(hfDealId9.Value);
                    }
                    else if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() == "0"))
                    {
                        subDealID9 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 9);
                    }
                    else if ((this.divNinthSubDeal.Visible == false) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId9.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId9.Value = "0";
                        subDealID9 = 0;
                    }


                    //Check Tenth Sub Deal info is prvided or not
                    if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId10.Value.Trim(), 10);
                        subDealID10 = Convert.ToInt64(hfDealId10.Value);
                    }
                    else if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() == "0"))
                    {
                        subDealID10 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 10);
                    }
                    else if ((this.divTenthSubDeal.Visible == false) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId10.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId10.Value = "0";
                        subDealID10 = 0;
                    }


                                        
                    //Get All Latest Deal Info Grid Info
                    //GetAllDealInfoAndFillGrid();
                    // SearchhDealInfoByDifferentParams();

                    //If adds successfully then redirects towars the Business form
                    //Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx?Res=Update"), false);

                    
                    

                    BLLDealCity objCity = new BLLDealCity();
                    objCity.DealId = Convert.ToInt64(hfDealId.Value);
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID1;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID2;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID3;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID4;
                    objCity.deleteDealCityByDealID();


                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID5;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID6;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID7;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID8;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID9;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID10;
                    objCity.deleteDealCityByDealID();


                    string strStartDate = txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
                    string strEndDate = txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
                    BLLDeals objDeals = new BLLDeals();
                    objDeals.DealStartTime = DateTime.Parse(strStartDate);
                    objDeals.DealEndTime = DateTime.Parse(strEndDate);
                    bumpUpSlot();
                    for (int i = 0; i < dlCities.Items.Count; i++)
                    {
                        try
                        {
                            Label lblCityID = (Label)(dlCities.Items[i].FindControl("lblCityID"));
                            if (lblCityID != null && lblCityID.Text.Trim() != "")
                            {
                                objDeals.cityId = Convert.ToInt32(lblCityID.Text);
                                objCity.CityId = Convert.ToInt32(lblCityID.Text);
                                DataTable dtDealsInfo;
                                //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                                dtDealsInfo = objDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                                if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                                {
                                    int dealSlot = 1;
                                    for (int a = 1; a <= 150; a++)
                                    {
                                        DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                        if (foundRows.Length == 0)
                                        {
                                            dealSlot = a;
                                            break;
                                        }
                                    }
                                    objCity.DealSlotC = dealSlot;
                                }
                                else
                                {
                                    objCity.DealSlotC = 1;
                                }

                                objCity.DealStartTimeC = DateTime.Parse(strStartDate);
                                objCity.DealEndTimeC = DateTime.Parse(strEndDate);
                                objCity.DealId = Convert.ToInt64(hfDealId.Value.Trim());
                                objCity.createDealCity();

                                if (subDealID1 > 0)
                                {
                                    objCity.DealId = subDealID1;
                                    objCity.createDealCity();
                                }
                                //Check Second Sub Deal Info is provided or not
                                if (subDealID2 > 0)
                                {
                                    objCity.DealId = subDealID2;
                                    objCity.createDealCity();
                                }
                                //Check Third Sub Deal Info is provided or not
                                if (subDealID3 > 0)
                                {
                                    objCity.DealId = subDealID3;
                                    objCity.createDealCity();
                                }
                                //Check Forth Sub Deal Info is provided or not
                                if (subDealID4 > 0)
                                {
                                    objCity.DealId = subDealID4;
                                    objCity.createDealCity();
                                }

                                if (subDealID5 > 0)
                                {
                                    objCity.DealId = subDealID5;
                                    objCity.createDealCity();
                                }

                                if (subDealID6 > 0)
                                {
                                    objCity.DealId = subDealID6;
                                    objCity.createDealCity();
                                }

                                if (subDealID7 > 0)
                                {
                                    objCity.DealId = subDealID7;
                                    objCity.createDealCity();
                                }

                                if (subDealID8 > 0)
                                {
                                    objCity.DealId = subDealID8;
                                    objCity.createDealCity();
                                }

                                if (subDealID9 > 0)
                                {
                                    objCity.DealId = subDealID9;
                                    objCity.createDealCity();
                                }

                                if (subDealID10 > 0)
                                {
                                    objCity.DealId = subDealID10;
                                    objCity.createDealCity();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    lblMessage.Text = "Deal Info has been updated successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
                else if (AlreadyDealExistsWithSameStartEndTimeWithCityName(iResID, hfDealId.Value) == false)
                {
                    //If Image 1 exists
                    if (fpDealImage1.HasFile)
                    {
                        if (this.imgUpload1.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload1.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName = ImageUploadHere(fpDealImage1, iResID);
                    }
                    else
                    {
                        strImageName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 2 exists
                    if (fpDealImage2.HasFile)
                    {
                        if (this.imgUpload2.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload2.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //If Image 3 exists
                    if (fpDealImage3.HasFile)
                    {
                        if (this.imgUpload3.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strImgName;

                            if (File.Exists(path))
                            {
                                try
                                {
                                    this.imgUpload3.Src = "";

                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }

                        //upload the Image here
                        strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
                    }
                    else
                    {
                        //upload the Image here
                        strImageName += "," + this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));
                    }

                    //Update the Deal info by Deal Id
                    NewDealID = Convert.ToInt32(hfDealId.Value);
                    UpdateDealInfoByDealId(strImageName, iResID, hfDealId.Value, 0);

                    //Check First Sub Deal Info is provided or not
                    long subdealID1 = 0;
                    long subdealID2 = 0;
                    long subdealID3 = 0;
                    long subdealID4 = 0;
                    long subDealID5 = 0;
                    long subDealID6 = 0;
                    long subDealID7 = 0;
                    long subDealID8 = 0;
                    long subDealID9 = 0;
                    long subDealID10 = 0;

                    if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId1.Value.Trim(), 1);
                        subdealID1 = Convert.ToInt64(this.hfDealId1.Value);
                    }
                    else if ((this.divFirstSubDeal.Visible == true) && (this.hfDealId1.Value.Trim() == "0"))
                    {
                        subdealID1 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 1);
                    }
                    else if ((this.divFirstSubDeal.Visible == false) && (this.hfDealId1.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId1.Value.Trim());
                        NewDealID = iDealId;

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId1.Value = "0";
                        subdealID1 = 0;
                    }
                    // 
                    //Check Second Sub Deal Info is provided or not
                    if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId2.Value.Trim(), 2);
                        subdealID2 = Convert.ToInt64(hfDealId2.Value);
                    }
                    else if ((this.divSecondSubDeal.Visible == true) && (this.hfDealId2.Value.Trim() == "0"))
                    {
                        subdealID2 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 2);
                    }
                    else if ((this.divSecondSubDeal.Visible == false) && (this.hfDealId2.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId2.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId2.Value = "0";
                        subdealID2 = 0;
                    }
                    //Check Third Sub Deal Info is provided or not
                    if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId3.Value.Trim(), 3);
                        subdealID3 = Convert.ToInt64(hfDealId3.Value);
                    }
                    else if ((this.divThirdSubDeal.Visible == true) && (this.hfDealId3.Value.Trim() == "0"))
                    {
                        subdealID3 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 3);
                    }
                    else if ((this.divThirdSubDeal.Visible == false) && (this.hfDealId3.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId3.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId3.Value = "0";
                        subdealID3 = 0;
                    }
                    //Check Forth Sub Deal Info is provided or not
                    if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId4.Value.Trim(), 4);
                        subdealID4 = Convert.ToInt64(hfDealId4.Value);
                    }
                    else if ((this.divForthSubDeal.Visible == true) && (this.hfDealId4.Value.Trim() == "0"))
                    {
                        subdealID4 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 4);
                    }
                    else if ((this.divForthSubDeal.Visible == false) && (this.hfDealId4.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId4.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId4.Value = "0";
                        subdealID4 = 0;
                    }
                    //Check Fifth Sub Deal info is prvided or not
                    if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId5.Value.Trim(), 5);
                        subDealID5 = Convert.ToInt64(hfDealId5.Value);
                    }
                    else if ((this.divFifthSubDeal.Visible == true) && (this.hfDealId5.Value.Trim() == "0"))
                    {
                        subDealID5 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 5);
                    }
                    else if ((this.divFifthSubDeal.Visible == false) && (this.hfDealId5.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId5.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId5.Value = "0";
                        subDealID5 = 0;
                    }

                    //Check Sixth Sub Deal info is prvided or not
                    if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId6.Value.Trim(), 6);
                        subDealID6 = Convert.ToInt64(hfDealId6.Value);
                    }
                    else if ((this.divSixthSubDeal.Visible == true) && (this.hfDealId6.Value.Trim() == "0"))
                    {
                        subDealID6 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 6);
                    }
                    else if ((this.divSixthSubDeal.Visible == false) && (this.hfDealId6.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId6.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId6.Value = "0";
                        subDealID6 = 0;
                    }

                    //Check Seven Sub Deal info is prvided or not
                    if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId7.Value.Trim(), 7);
                        subDealID7 = Convert.ToInt64(hfDealId7.Value);
                    }
                    else if ((this.divSeventhSubDeal.Visible == true) && (this.hfDealId7.Value.Trim() == "0"))
                    {
                        subDealID7 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 7);
                    }
                    else if ((this.divSeventhSubDeal.Visible == false) && (this.hfDealId7.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId7.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId7.Value = "0";
                        subDealID7 = 0;
                    }

                    //Check Eight Sub Deal info is prvided or not
                    if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId8.Value.Trim(), 8);
                        subDealID8 = Convert.ToInt64(hfDealId8.Value);
                    }
                    else if ((this.divEightSubDeal.Visible == true) && (this.hfDealId8.Value.Trim() == "0"))
                    {
                        subDealID8 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 8);
                    }
                    else if ((this.divEightSubDeal.Visible == false) && (this.hfDealId8.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId8.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId8.Value = "0";
                        subDealID8 = 0;
                    }

                    //Check Ninth Sub Deal info is prvided or not
                    if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId9.Value.Trim(), 9);
                        subDealID9 = Convert.ToInt64(hfDealId9.Value);
                    }
                    else if ((this.divNinthSubDeal.Visible == true) && (this.hfDealId9.Value.Trim() == "0"))
                    {
                        subDealID9 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 9);
                    }
                    else if ((this.divNinthSubDeal.Visible == false) && (this.hfDealId9.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId9.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId9.Value = "0";
                        subDealID9 = 0;
                    }


                    //Check Ninth Sub Deal info is prvided or not
                    if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        UpdateDealInfoByDealId(strImageName, iResID, this.hfDealId10.Value.Trim(), 10);
                        subDealID10 = Convert.ToInt64(hfDealId10.Value);
                    }
                    else if ((this.divTenthSubDeal.Visible == true) && (this.hfDealId10.Value.Trim() == "0"))
                    {
                        subDealID10 = AddNewDealInfo(strImageName, iResID, int.Parse(hfDealId.Value), 10);
                    }
                    else if ((this.divTenthSubDeal.Visible == false) && (this.hfDealId10.Value.Trim() != "0"))
                    {
                        int iDealId = Convert.ToInt32(this.hfDealId10.Value.Trim());

                        BLLDeals objBLLDeals = new BLLDeals();
                        objBLLDeals.DealId = iDealId;
                        int iDeal = objBLLDeals.deleteDealByDealId();
                        this.hfDealId10.Value = "0";
                        subDealID10 = 0;
                    }


                    //Hide the Update Deal Info here
                    //

                    //Show All Deal Info into Grid View here
                    //
                    // 

                    //Get All Latest Deal Info Grid Info
                    //GetAllDealInfoAndFillGrid();
                    

                    //If adds successfully then redirects towars the Business form
                    //Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx?Res=Update"), false);

                    //Show the All Deals
                    

                    BLLDealCity objCity = new BLLDealCity();
                    objCity.DealId = Convert.ToInt64(hfDealId.Value);
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subdealID1;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subdealID2;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subdealID3;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subdealID4;
                    objCity.deleteDealCityByDealID();


                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID5;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID6;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID7;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID8;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID9;
                    objCity.deleteDealCityByDealID();

                    objCity = new BLLDealCity();
                    objCity.DealId = subDealID10;
                    objCity.deleteDealCityByDealID();

                    if (!cbForDesign.Checked)
                    {
                        for (int i = 1; i < dlCities.Items.Count; i++)
                        {
                            try
                            {
                                CheckBox chk = (CheckBox)dlCities.Items[i].FindControl("chkbxSelect");
                                Label lblCityID = (Label)dlCities.Items[i].FindControl("lblCityID");
                                if (chk != null && lblCityID != null && lblCityID.Text.Trim() != "" && chk.Checked)
                                {
                                    TextBox txtdlStartDate = (TextBox)(dlCities.Items[i].FindControl("txtdlStartDate"));
                                    TextBox txtDLEndDate = (TextBox)(dlCities.Items[i].FindControl("txtDLEndDate"));
                                    DropDownList ddlDLStartHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartHH"));
                                    DropDownList ddlDLStartMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartMM"));
                                    DropDownList ddlDLStartPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLStartPortion"));
                                    DropDownList ddlDLEndHH = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndHH"));
                                    DropDownList ddlDLEndMM = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndMM"));
                                    DropDownList ddlDLEndPortion = (DropDownList)(dlCities.Items[i].FindControl("ddlDLEndPortion"));
                                    DropDownList ddlDLSideDeal = (DropDownList)(dlCities.Items[i].FindControl("ddlDLSideDeal"));

                                    if (txtdlStartDate != null && txtDLEndDate != null && ddlDLSideDeal != null
                                        && ddlDLStartHH != null && ddlDLStartMM != null && ddlDLStartPortion != null
                                        && ddlDLEndHH != null && ddlDLEndMM != null && ddlDLEndPortion != null)
                                    {
                                        objCity.DealStartTimeC = DateTime.Parse(txtdlStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealEndTimeC = DateTime.Parse(txtDLEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00");
                                        objCity.DealSlotC = Convert.ToInt32(ddlDLSideDeal.SelectedValue.Trim());
                                    }
                                    objCity.CityId = Convert.ToInt32(lblCityID.Text.Trim());
                                    objCity.DealId = Convert.ToInt64(hfDealId.Value.ToString());
                                    objCity.createDealCity();

                                    if (subdealID1 > 0)
                                    {
                                        objCity.DealId = subdealID1;
                                        objCity.createDealCity();
                                    }
                                    //Check Second Sub Deal Info is provided or not
                                    if (subdealID2 > 0)
                                    {
                                        objCity.DealId = subdealID2;
                                        objCity.createDealCity();
                                    }
                                    //Check Third Sub Deal Info is provided or not
                                    if (subdealID3 > 0)
                                    {
                                        objCity.DealId = subdealID3;
                                        objCity.createDealCity();
                                    }
                                    //Check Forth Sub Deal Info is provided or not
                                    if (subdealID4 > 0)
                                    {
                                        objCity.DealId = subdealID4;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID5 > 0)
                                    {
                                        objCity.DealId = subDealID5;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID6 > 0)
                                    {
                                        objCity.DealId = subDealID6;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID7 > 0)
                                    {
                                        objCity.DealId = subDealID7;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID8 > 0)
                                    {
                                        objCity.DealId = subDealID8;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID9 > 0)
                                    {
                                        objCity.DealId = subDealID9;
                                        objCity.createDealCity();
                                    }

                                    if (subDealID10 > 0)
                                    {
                                        objCity.DealId = subDealID10;
                                        objCity.createDealCity();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            objCity.DealStartTimeC = DateTime.Now.AddMonths(-3);
                            objCity.DealEndTimeC = DateTime.Now.AddMonths(-2);
                            objCity.DealSlotC = 1;
                            objCity.DealId = Convert.ToInt64(hfDealId.Value.Trim()); ;
                            objCity.CityId = 337;

                            BLLDeals objBLLDeals = new BLLDeals();
                            objBLLDeals.DealStartTime = objCity.DealStartTimeC;
                            objBLLDeals.DealEndTime = objCity.DealEndTimeC;
                            objBLLDeals.cityId = 337;
                            DataTable dtDealsInfo;
                            //dtDealsInfo = objBLLDeals.getDealInfoByDealStartEndTime();
                            dtDealsInfo = objBLLDeals.getActiveDealSlotByDealStartEndTimeWithCityID();

                            if (dtDealsInfo != null && dtDealsInfo.Rows.Count > 0)
                            {
                                int dealSlot = 1;
                                for (int a = 1; a <= 150; a++)
                                {
                                    DataRow[] foundRows = dtDealsInfo.Select("dealSlotC =" + a.ToString());
                                    if (foundRows.Length == 0)
                                    {
                                        dealSlot = a;
                                        break;
                                    }
                                }
                                objCity.DealSlotC = dealSlot;
                            }
                            else
                            {
                                objCity.DealSlotC = 1;
                            }

                            objCity.createDealCity();

                            if (subdealID1 > 0)
                            {
                                objCity.DealId = subdealID1;
                                objCity.createDealCity();
                            }
                            //Check Second Sub Deal Info is provided or not
                            if (subdealID2 > 0)
                            {
                                objCity.DealId = subdealID2;
                                objCity.createDealCity();
                            }
                            //Check Third Sub Deal Info is provided or not
                            if (subdealID3 > 0)
                            {
                                objCity.DealId = subdealID3;
                                objCity.createDealCity();
                            }
                            //Check Forth Sub Deal Info is provided or not
                            if (subdealID4 > 0)
                            {
                                objCity.DealId = subdealID4;
                                objCity.createDealCity();
                            }

                            if (subDealID5 > 0)
                            {
                                objCity.DealId = subDealID5;
                                objCity.createDealCity();
                            }

                            if (subDealID6 > 0)
                            {
                                objCity.DealId = subDealID6;
                                objCity.createDealCity();
                            }

                            if (subDealID7 > 0)
                            {
                                objCity.DealId = subDealID7;
                                objCity.createDealCity();
                            }

                            if (subDealID8 > 0)
                            {
                                objCity.DealId = subDealID8;
                                objCity.createDealCity();
                            }

                            if (subDealID9 > 0)
                            {
                                objCity.DealId = subDealID9;
                                objCity.createDealCity();
                            }

                            if (subDealID10 > 0)
                            {
                                objCity.DealId = subDealID10;
                                objCity.createDealCity();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    lblMessage.Text = "Deal Info has been updated successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
            }

        }
        else
        {
            //In case of Exception it will redirect you towrads the Business page
            //Usually exception comes in that case where if "Mode" or "resID" contains invalid value
            // Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), false);
        }
        this.btnImgSave.ToolTip = "Update Deal Info";
        string jScript;
        if (cbForDesign.Checked)
        {
            jScript = "<script>myWindow=window.open('" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Preview.aspx?sidedeal=" + NewDealID + "&fordesign=true','','width=760,height=700,toolbar=no,status=no, menubar=no, scrollbars=yes,resizable=no');";
        }
        else
        {
            jScript = "<script>myWindow=window.open('" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Preview.aspx?sidedeal=" + NewDealID + "','','width=760,height=700,toolbar=no,status=no, menubar=no, scrollbars=yes,resizable=no');";
        }
        jScript += "myWindow.focus();";
        jScript += " </script>";
        Page.RegisterClientScriptBlock("keyClientBlock", jScript);

    }


    protected void btnImgCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToString().Trim().ToLower() == "new")
            {
                Response.Redirect("restaurantManagement.aspx", true);
            }
            else if (Request.QueryString["resID"] != null && Request.QueryString["resID"].ToString().Trim() != "")
            {
                Response.Redirect("dealManagement.aspx?Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), true);
            }
            else
            {
                Response.Redirect("restaurantManagement.aspx", true);
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
  
    #endregion

  
}