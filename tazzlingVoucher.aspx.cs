using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GecLibrary;
using System.IO;
using System.Configuration;

public partial class tazzlingVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | My Vouchers";
        if (!IsPostBack)
        {
            DataTable dtUser = null;
            if (Request.QueryString["pdf"] == null)
            {
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
                }
                else
                {
                    Response.Redirect("default.aspx");
                }
            }
            try
            {
                if (Request.QueryString["oid"] != null && Request.QueryString["oid"].Trim() != ""
                    && Request.QueryString["did"] != null && Request.QueryString["did"].Trim() != "")
                {
                    if (Request.QueryString["pdf"] != null && Request.QueryString["pdf"].ToString().ToUpper() == "TRUE")
                    {
                        imgBarcode.Visible = true;
                        lblBarcode.Visible = false;
                    }
                    BLLDealOrders objOrders = new BLLDealOrders();
                    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                    DataTable dtOrdersInfo = null;
                    DataTable dtOrdersdetail = null;
                    GECEncryption objEnc = new GECEncryption();
                    objOrders.dOrderID = Convert.ToInt64(Request.QueryString["oid"].Trim());
                    if (Convert.ToInt64(Request.QueryString["oid"].Trim()) < 237)
                    {
                        
                        dtOrdersInfo = objOrders.getDealOrderDetailByOrderID_New();
                    }
                    else
                    {
                        dtOrdersInfo = objOrders.getProductOrderDetailByOrderID();
                    }
                    //string strImage = "";
                    if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
                    {
                        if (Request.QueryString["pdf"] == null)
                        {
                            if (dtUser.Rows[0]["userid"].ToString().Trim() != dtOrdersInfo.Rows[0]["userid"].ToString().Trim())
                            {
                                Response.Redirect("default.aspx", true);
                                return;
                            }
                        }
                        objDetail.dOrderID = Convert.ToInt64(Request.QueryString["oid"].Trim());
                        dtOrdersdetail = objDetail.getAllDealOrderDetailsByOrderID();
                        if (dtOrdersdetail != null && dtOrdersdetail.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtOrdersdetail.Rows.Count; i++)
                            {
                                if (Request.QueryString["did"].Trim() == dtOrdersdetail.Rows[i]["detailID"].ToString().Trim())
                                {
                                    try
                                    {
                                        BLLRestaurantGoogleAddresses objResturant = new BLLRestaurantGoogleAddresses();
                                        objResturant.restaurantId = Convert.ToInt64(dtOrdersInfo.Rows[0]["restaurantId"].ToString().Trim());
                                        DataTable dtResturant = objResturant.getAllRestaurantGoogleAddressesByRestaurantID();
                                        if (dtResturant != null && dtResturant.Rows.Count > 0)
                                        {

                                            if (dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "online deal" || dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "online" || dtResturant.Rows[0]["restaurantGoogleAddress"].ToString().ToLower() == "deal online")
                                            {
                                                //lblRedeemAtLabel.Visible = false;
                                                //lblRedeemAt.Visible = false;
                                                //lblRedeemAt.Text = "Online Deal";
                                            }
                                            else
                                            {
                                                //lblRedeemAtLabel.Visible = true;
                                                //lblRedeemAt.Visible = true;
                                                //lblRedeemAt.Text = dtOrdersInfo.Rows[0]["Address"].ToString().Trim();
                                                FileInfo file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + dtOrdersInfo.Rows[0]["restaurantId"].ToString().Trim() + "\\" + dtOrdersInfo.Rows[0]["restaurantId"].ToString().Trim() + ".png");
                                                if (file.Exists)
                                                {
                                                    //imgGoogleMap.Visible = true;
                                                    //imgGoogleMap.ImageUrl = ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtOrdersInfo.Rows[0]["restaurantId"].ToString().Trim() + "/" + dtOrdersInfo.Rows[0]["restaurantId"].ToString().Trim() + ".png";
                                                }
                                                else
                                                {
                                                    //imgGoogleMap.Visible = false;
                                                }
                                            }
                                        }

                                        lblTastyGoNumber.Text = "# " + objEnc.DecryptData("deatailOrder", dtOrdersdetail.Rows[i]["dealOrderCode"].ToString());
                                        lblDealTitle.Text = dtOrdersInfo.Rows[0]["title"].ToString().Trim();
                                        lblSecurityCode2.Text = dtOrdersdetail.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                        lblSecurityCode.Text = dtOrdersdetail.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                        if (Convert.ToInt64(Request.QueryString["oid"].Trim()) < 237)
                                        {
                                            if (dtOrdersInfo.Rows[0]["voucherExpiryDate"] != null && dtOrdersInfo.Rows[0]["voucherExpiryDate"].ToString().Trim() != "")
                                            {
                                                lblVoucherDeatail.Text = "Promotion value EXPIRES " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["voucherExpiryDate"].ToString().Trim()).ToString("MMMMM dd,yyyy") + ".<br><b>Redeemable after " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("MMMMM dd,yyyy") + " at " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("hh tt") + "</b><br><span style='font-size:10px;'>Unless Specified Otherwise in Fine Print Below</span>";
                                            }
                                            else
                                            {
                                                lblVoucherDeatail.Text = "<b>Redeemable after " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("MMMMM dd,yyyy") + " at " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("hh tt") + "</b><br><span style='font-size:10px;'>Unless Specified Otherwise in Fine Print Below</span>";
                                            }
                                        }
                                        else
                                        {
                                            if (dtOrdersInfo.Rows[0]["voucherExpiryDate"] != null && dtOrdersInfo.Rows[0]["voucherExpiryDate"].ToString().Trim() != "")
                                            {
                                                lblVoucherDeatail.Text = "Promotion value EXPIRES " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["voucherExpiryDate"].ToString().Trim()).ToString("MMMMM dd,yyyy") + ".<br><b>Redeemable after " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["campaignEndTime"].ToString().Trim()).ToString("MMMMM dd,yyyy") + " at " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["campaignEndTime"].ToString().Trim()).ToString("hh tt") + "</b><br><span style='font-size:10px;'>Unless Specified Otherwise in Fine Print Below</span>";
                                            }
                                            else
                                            {
                                                lblVoucherDeatail.Text = "<b>Redeemable after " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["campaignEndTime"].ToString().Trim()).ToString("MMMMM dd,yyyy") + " at " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["campaignEndTime"].ToString().Trim()).ToString("hh tt") + "</b><br><span style='font-size:10px;'>Unless Specified Otherwise in Fine Print Below</span>";
                                            }
                                        }

                                        //lblVoucherDeatail.Text = "";
                                        //lblFinePrint.Text = dtOrdersInfo.Rows[0]["finePrint"].ToString().Trim();
                                        lblResturantName.Text = dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString().Trim();
                                        //lblHowToUse.Text = dtOrdersInfo.Rows[0]["howtouse"].ToString().Trim();
                                        lblRedemptionAndRefund.Text = "Redeemable only at \"" + dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString().Trim() + "\" for the goods or service listed above unless specifies an exception in fine print.  If the merchant refuses to honor this voucher, please contact Tastygo to arrange for refund. Visit us <a href='http://www.tazzling.com/faq.aspx' target='_blank' style='color:Black; text-decoration:none;'>http://www.tazzling.com/faq.aspx</a> for our refund policy.";
                                        lblRestText.Text = "• Not redeemable for cash unless required by law •  Taxes and gratuity not included unless specifies in the fine print •  Voucher cannot be combined with other additional discounts or offers •  Not reloadable. Unauthorized reproduction, modification, or resell are prohibited •  \"" + dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString().Trim() + "\" is the issuer of this voucher. Purchase, use , or acceptance of this voucher constitutes acceptance of these terms.";
                                    }
                                    catch (Exception ex)
                                    { }
                                }

                            }
                        }
                    }
                }
                else
                {
                    // Response.Redirect("~/member_MyTastygo.aspx", false);
                }

            }
            catch (Exception ex)
            {
                //lblMessage.Visible = true;
                //lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
                //imgGridMessage.Visible = true;
                //imgGridMessage.ImageUrl = "images/error.png";
                //lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}