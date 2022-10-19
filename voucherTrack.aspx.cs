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
using System.IO;

public partial class voucherTrack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | My Voucher Tracker";
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
                    dtOrdersInfo = objOrders.getDealOrderDetailByOrderID_New();
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
                                        lblTastyGoNumber.Text = "# " + objEnc.DecryptData("deatailOrder", dtOrdersdetail.Rows[i]["dealOrderCode"].ToString());
                                        lblDealTitle.Text = dtOrdersInfo.Rows[0]["title"].ToString().Trim();
                                        lblSecurityCode2.Text = dtOrdersdetail.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                        lblSecurityCode.Text = dtOrdersdetail.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                        lblFinePrint.Text = dtOrdersInfo.Rows[0]["finePrint"].ToString().Trim();
                                        if (dtOrdersInfo.Rows[0]["voucherExpiryDate"] != null && dtOrdersInfo.Rows[0]["voucherExpiryDate"].ToString().Trim() != "")
                                        {
                                            lblVoucherDeatail.Text = "Promotion value EXPIRES " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["voucherExpiryDate"].ToString().Trim()).ToString("MMMMM dd,yyyy") + ".<br><b>Redeemable after " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("MMMMM dd,yyyy") + " at " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("hh tt") + "</b><br><span style='font-size:10px;'>Unless Specified Otherwise in Fine Print Below</span>";
                                        }
                                        else
                                        {
                                            lblVoucherDeatail.Text = "<b>Redeemable after " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("MMMMM dd,yyyy") + " at " + Convert.ToDateTime(dtOrdersInfo.Rows[0]["dealEndTimeC"].ToString().Trim()).ToString("hh tt") + "</b><br><span style='font-size:10px;'>Unless Specified Otherwise in Fine Print Below</span>";
                                        }
                                        if (dtOrdersInfo.Rows[0]["shippingInfoId"] != null && dtOrdersInfo.Rows[0]["shippingInfoId"].ToString().Trim() != "")
                                        {
                                            DataTable dtShippingInfor = Misc.search("select * from userShippingInfo where shippingInfoId=" + dtOrdersInfo.Rows[0]["shippingInfoId"].ToString());
                                            if (dtShippingInfor != null && dtShippingInfor.Rows.Count > 0)
                                            {
                                                lblDescription.Text = dtShippingInfor.Rows[0]["shippingNote"].ToString().Trim();
                                            }
                                        }                                                                             

                                        //lblVoucherDeatail.Text = "";
                                        if (dtOrdersdetail.Rows[i]["trackingNumber"] != null && dtOrdersdetail.Rows[i]["trackingNumber"].ToString().Trim() != "")
                                        {
                                            TrackInfo.Visible = true;
                                            lblStatus.Text = "Sent";
                                            hlTrackingNumber.Visible = true;
                                            hlTrackingNumber.Text = dtOrdersdetail.Rows[i]["trackingNumber"].ToString().Trim();
                                            if (dtOrdersdetail.Rows[i]["trackerupdateDate"] != null && dtOrdersdetail.Rows[i]["trackerupdateDate"].ToString().Trim() != "")
                                            {
                                                lblEAT.Text = Convert.ToDateTime(dtOrdersdetail.Rows[i]["trackerupdateDate"].ToString()).AddDays(15).ToString("MM/dd/yyyy");
                                            }
                                        }
                                        else
                                        {
                                            lblStatus.Text = "Pending";
                                            hlTrackingNumber.Visible = false;
                                            TrackInfo.Visible = false;
                                        }
                                        
                                        lblResturantName.Text = dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString().Trim();                                     
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
