using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GecLibrary;
using System.Data;

public partial class Takeout_UserControls_Order_OrderDetails : System.Web.UI.UserControl
{
    BLLRestaurantSetting objResSetting = new BLLRestaurantSetting();
    BLLOrders objOrder = new BLLOrders();
    BLLOrderItem objOItem = new BLLOrderItem();
    BLLRestaurant objRes = new BLLRestaurant();
    BLLTaxRate objTax = new BLLTaxRate();
    BLLCommission objCommission = new BLLCommission();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtUser = null;

        if (!IsPostBack)
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            if (Request.QueryString["ID"] != null)
            {
                GetAndSetOrderDetailByOrderID();
            }
            bindItemsGrid();
        }
    }

    private void GetAndSetOrderDetailByOrderID()
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            GECEncryption objGECEncryption = new GECEncryption();

            string strOrderID = objGECEncryption.DecryptData("Order_ID_01", Request.QueryString["ID"].ToString().Replace(" ","+"));

            DataTable dtOrdersInfo;

            BLLOrders objBLLOrders = new BLLOrders();

            objBLLOrders.orderId = Convert.ToInt64(strOrderID);

            dtOrdersInfo = objBLLOrders.getOrderDetilByOrderID();

            if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
            {
                ViewState["createdBy"] = dtOrdersInfo.Rows[0]["createdBy"].ToString();
                ViewState["subTotalAmount"] = dtOrdersInfo.Rows[0]["subTotalAmount"].ToString();
                //Set the Order Number
                this.lblOrderNo.Text = dtOrdersInfo.Rows[0]["orderNumber"] == null ? "" : dtOrdersInfo.Rows[0]["orderNumber"].ToString();

                //Set the Status
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string url = oInfo.Name;

                //if (url.ToString().ToLower().Trim().Equals("restaurant_orders.aspx"))
                //{
                //    this.lblStatus.Visible = false;

                //    this.ddlStatus.Visible = true;

                //    this.btnUpdate.Visible = true;

                //    this.ddlStatus.SelectedValue = dtOrdersInfo.Rows[0]["orderStatus"] == null ? "" : dtOrdersInfo.Rows[0]["orderStatus"].ToString();
                //}
                //else
                //{
                this.lblStatus.Text = dtOrdersInfo.Rows[0]["orderStatus"] == null ? "" : dtOrdersInfo.Rows[0]["orderStatus"].ToString();

                this.ddlStatus.Visible = false;

                this.btnUpdate.Visible = false;
                //}

                //Set the Order Type
                if (dtOrdersInfo.Rows[0]["orderType"] != null && dtOrdersInfo.Rows[0]["orderType"].ToString().ToLower() == "cash")
                {
                    this.lblOrderType.Text = dtOrdersInfo.Rows[0]["orderType"] == null ? "" : dtOrdersInfo.Rows[0]["orderType"].ToString();
                }
                else if (dtOrdersInfo.Rows[0]["orderType"] != null && dtOrdersInfo.Rows[0]["orderType"]!="")
                {
                    this.lblOrderType.Text = "Prepaid";
                }
                else
                {
                    this.lblOrderType.Text = "";
                }

                ViewState["Payment"] = dtOrdersInfo.Rows[0]["orderType"] == null ? "" : dtOrdersInfo.Rows[0]["orderType"].ToString();
                //Set the Order Creation Date Time
                this.lblDateTime.Text = dtOrdersInfo.Rows[0]["creationDate"] == null ? "" : dtOrdersInfo.Rows[0]["creationDate"].ToString();

                //Set the Restaurant Name
                this.lblRestaurantName.Text = dtOrdersInfo.Rows[0]["restaurantName"] == null ? "" : dtOrdersInfo.Rows[0]["restaurantName"].ToString();


                this.lblPickUpDelivery.Text = dtOrdersInfo.Rows[0]["PickUpDelivery"] == null ? "" : dtOrdersInfo.Rows[0]["PickUpDelivery"].ToString();

                //Set the Pick Up / Delivery

                if (!this.lblPickUpDelivery.Text.ToUpper().Equals("PICK UP"))
                {
                    //Set the Delivery Address
                    this.lblDeliveryAddress.Text = dtOrdersInfo.Rows[0]["shipToAddress"] == null ? "" : dtOrdersInfo.Rows[0]["shipToAddress"].ToString();
                    this.lblDeliveryPhone.Text = dtOrdersInfo.Rows[0]["shipPhone"] == null ? "" : dtOrdersInfo.Rows[0]["shipPhone"].ToString();
                    this.lblBillingPhone.Text = dtOrdersInfo.Rows[0]["billPhone"] == null ? "" : dtOrdersInfo.Rows[0]["billPhone"].ToString();
                    lblBillingAddress.Text = dtOrdersInfo.Rows[0]["billToAddress"] == null ? "" : dtOrdersInfo.Rows[0]["billToAddress"].ToString();
                    this.lblOrderFrom.Text = dtOrdersInfo.Rows[0]["ShippingName"] == null ? "" : dtOrdersInfo.Rows[0]["ShippingName"].ToString();
                }
                else
                {
                    this.lbllblDeliveryAddress.Visible = false;
                    this.lblDeliveryAddress.Visible = false;
                    this.lblDeliveryPhone.Visible = false;
                    lbllblDeliveryPhone.Visible = false;
                    lblBillingAddress.Text = dtOrdersInfo.Rows[0]["billToAddress"] == null ? "" : dtOrdersInfo.Rows[0]["billToAddress"].ToString();
                    this.lblBillingPhone.Text = dtOrdersInfo.Rows[0]["billPhone"] == null ? "" : dtOrdersInfo.Rows[0]["billPhone"].ToString();
                    this.lblOrderFrom.Text = dtOrdersInfo.Rows[0]["BillingName"] == null ? "" : dtOrdersInfo.Rows[0]["BillingName"].ToString();
                }

                //Set the Order From

               

               // this.lblOrderFrom.Text = dtOrdersInfo.Rows[0]["OrderFrom"] == null ? "" : dtOrdersInfo.Rows[0]["OrderFrom"].ToString();

                
                

                //Set the Pick Up / Delivery Time -- Order Date
                this.lblPickUpDeliveryTime.Text = dtOrdersInfo.Rows[0]["orderDate"] == null ? "" : dtOrdersInfo.Rows[0]["orderDate"].ToString();

                //Set the Special Request
                this.lblSpecialRequest.Text = dtOrdersInfo.Rows[0]["specialRequest"] == null ? "" : dtOrdersInfo.Rows[0]["specialRequest"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnGoToCheckout_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddFavorate_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnRefund_Click(object sender, EventArgs e)
    {

    }

    private void bindItemsGrid()
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            GECEncryption objEnc = new GECEncryption();
            string strID = "";
          
                if (Request.QueryString["id"] != null)
                    strID = Request.QueryString["id"].ToString().Replace(" ","+");
                else
                    Response.Redirect(ResolveUrl("~/member_orders.aspx"), false);
          
            //strID = "26";
            string strOrderID = objEnc.DecryptData("Order_ID_01", strID);
            objOItem.orderId = Convert.ToInt64(strOrderID);
            DataTable dtMenuItems = objOItem.getAllOrderItemByOrderID();

            objOrder.orderId = Convert.ToInt64(strOrderID);
            DataTable dtOrderDetail = objOrder.getOrderDetilByOrderID();

            if (dtMenuItems != null && dtMenuItems.Rows.Count > 0 && dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
            {
                GridView_Sub.DataSource = dtMenuItems.DefaultView;
                GridView_Sub.DataBind();

                //double dTotal = 0.0;
                //double dDelCharge = 0.0;

                //for (int i = 0; i < dtMenuItems.Rows.Count; i++)
                //{
                //    dTotal += (Convert.ToDouble(dtMenuItems.Rows[i]["unitPrice"] == null ? "0" : dtMenuItems.Rows[i]["unitPrice"]) * Convert.ToDouble(dtMenuItems.Rows[i]["quantity"] == null ? "0" : dtMenuItems.Rows[i]["quantity"]));
                //}
                calculateTotal(dtOrderDetail);
                if (dtOrderDetail.Rows[0]["deliveryMethod"].ToString() == "Delivery")
                {
                    lblTime.Text = "<b>Delivery Time</b>: " + Convert.ToDateTime(dtOrderDetail.Rows[0]["orderDate"].ToString()).ToString("MM-dd-yyyy HH:mm tt");
                }
                else
                {
                    lblTime.Text = "<b>Pick Up Time</b>: " + Convert.ToDateTime(dtOrderDetail.Rows[0]["orderDate"].ToString()).ToString("MM-dd-yyyy HH:mm tt");
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
 
    private void calculateTotal(DataTable dtOrderDetail)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            double freeDistance = 0;
            double chargeDistance = 0;
            double exceededLimitChargeUnit = 0;

            ltTotal.Text = "";
            ltTotal.Text += "<table><tbody>";
            ltTotal.Text += "<tr><td><b>Total Amount:</b></td><td style='text-align: right;'>$" + dtOrderDetail.Rows[0]["subTotalAmount"].ToString() + " CAD</td></tr>";
            if (dtOrderDetail.Rows[0]["deliveryAmount"].ToString() != "" && dtOrderDetail.Rows[0]["deliveryMethod"].ToString() != "Pick up")
            {
                objResSetting.restaurantId = Convert.ToInt64(dtOrderDetail.Rows[0]["providerId"]);
                DataTable dtSetting = objResSetting.getRestaurantSettingByResturantID();

                if (dtSetting != null && dtSetting.Rows.Count > 0)
                {
                    double deliveryCharge = Convert.ToDouble(dtSetting.Rows[0]["deliveryCharge"].ToString());
                    if (dtSetting.Rows[0]["freeDeliveryDistance"].ToString() != "")
                    {
                        freeDistance = Convert.ToDouble(dtSetting.Rows[0]["freeDeliveryDistance"].ToString());
                    }
                    if (dtSetting.Rows[0]["exceededLimitChargeUnit"].ToString() != "")
                    {
                        exceededLimitChargeUnit = Convert.ToDouble(dtSetting.Rows[0]["exceededLimitChargeUnit"].ToString());
                    }

                    double distance = dtOrderDetail.Rows[0]["shippingDistance"].ToString() != "" ? Convert.ToDouble(dtOrderDetail.Rows[0]["shippingDistance"].ToString()) : 0.0;

                    if (distance > freeDistance)
                    {
                        chargeDistance = distance - freeDistance;
                    }
                    double deliveryCharges = deliveryCharge + chargeDistance * exceededLimitChargeUnit;

                    if (dtOrderDetail.Rows[0]["belowLimtAmount"] != null && dtOrderDetail.Rows[0]["belowLimtAmount"].ToString() != "")
                    {
                        deliveryCharges += Convert.ToDouble(dtOrderDetail.Rows[0]["belowLimtAmount"].ToString());
                    }

                    if (chargeDistance == 0)
                    {
                        ltTotal.Text += "<tr><td><b>Shipping Flat:</b></td><td style='text-align: right;'>$" + deliveryCharges.ToString("###.00") + "&nbsp;CAD</td></tr>";
                    }
                    else
                    {
                        ltTotal.Text += "<tr><td><b>Shipping Distance (" + distance.ToString("###.00") + "km):</b></td><td style='text-align: right;'>$" + deliveryCharges.ToString("###.00") + "&nbsp;CAD</td></tr>";
                    }
                }
                //ltTotal.Text += "<tr><td><b>Shipping Amount:</b></td><td style='text-align: right;'>$" + dtOrderDetail.Rows[0]["deliveryAmount"].ToString() + "&nbsp;CAD</td></tr>";
            }
            if (dtOrderDetail.Rows[0]["providerId"] != null && dtOrderDetail.Rows[0]["providerId"].ToString() != "")
            {

                objRes.restaurantId = Convert.ToInt64(dtOrderDetail.Rows[0]["providerId"].ToString());
                DataTable dtResturant = objRes.getRestaurantInfoByID();
                if (dtResturant != null && dtResturant.Rows.Count > 0 && dtResturant.Rows[0]["provinceId"].ToString() != "")
                {
                    objTax.provinceId = Convert.ToInt64(dtResturant.Rows[0]["provinceId"].ToString());
                    DataTable dtTax = objTax.getProvinceTaxRateByProvinceID();
                    if (dtTax != null && dtTax.Rows.Count > 0 && dtTax.Rows[0]["taxRates"].ToString() != "")
                    {
                        ltTotal.Text += "<tr><td><b>Tax(" + dtTax.Rows[0]["taxRates"].ToString() + "%):</b></td><td style='text-align: right;'>$" + dtOrderDetail.Rows[0]["taxAmount"].ToString() + "&nbsp;CAD</td></tr>";
                    }
                }

            }

            if (dtOrderDetail.Rows[0]["tips"] != null && dtOrderDetail.Rows[0]["tips"].ToString() != "" && Convert.ToDouble(dtOrderDetail.Rows[0]["tips"].ToString()) != 0)
            {
                ltTotal.Text += "<tr><td><b>Tip:</b></td><td style='text-align: right;'>$" + dtOrderDetail.Rows[0]["tips"].ToString() + "&nbsp;CAD</td></tr>";
            }
            ltTotal.Text += "<tr><td style='border-top:solid 1px #e1e1e1;'><b>Grand Total:</b></td><td style='text-align: right; border-top:solid 1px #e1e1e1;' id='grandTotal'>$" + Convert.ToDouble(dtOrderDetail.Rows[0]["totalAmount"].ToString()).ToString("###.00") + "&nbsp;CAD</td></tr>";
            ltTotal.Text += "</tbody></table>";           
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            GECEncryption objGECEncryption = new GECEncryption();
            string strOrderID = objGECEncryption.DecryptData("Order_ID_01", Request.QueryString["ID"].ToString().Replace(" ", "+"));
            BLLOrders objBLLOrders = new BLLOrders();
            objBLLOrders.orderId = Convert.ToInt64(strOrderID);
            objBLLOrders.orderStatus = this.ddlStatus.SelectedValue.ToString().Trim();
            if (ddlStatus.SelectedIndex != 0)
            {
                objCommission.orderId = Convert.ToInt64(strOrderID);
                objCommission.deleteCommission();
            }
            else
            {
                if (ViewState["Payment"].ToString() != "Mix Payment")
                {
                    assignCommission(Convert.ToInt32(ViewState["createdBy"].ToString()), Convert.ToInt64(strOrderID));
                }
            }
            objBLLOrders.updateOrderStatusByOrderID();
            Session["OrderStatus"] = "Updated";
            Response.Redirect(ResolveUrl("~/restaurant_orders.aspx"), false);
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    private void assignCommission(int userID,long orderID)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            BLLUser objUser = new BLLUser();
            BLLOrders objOrder = new BLLOrders();
            objOrder.orderId = orderID;
            objOrder.subTotalAmount =float.Parse(ViewState["subTotalAmount"].ToString());
            objCommission = new BLLCommission();
            objCommission.orderId = orderID;
            objUser.userId = userID;
            DataTable dtUser = objUser.getUserByID();

            objUser.referralId = dtUser.Rows[0]["friendsReferralId"].ToString();
            dtUser = objUser.getUserByReferralIdForPayment();

            double dCommission = 0d;

            for (int i = 0; i < 7; i++)
            {
                if (dtUser != null && dtUser.Rows.Count > 0 && objUser.referralId != "")
                {
                    objOrder.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());

                    if (objOrder.getTotalAmountOfGivenMonthByUserID())
                    {
                        objCommission.isDoubled = true;
                        switch (i)
                        {
                            case 0:
                                dCommission = (objOrder.subTotalAmount / 100) * 1;
                                break;
                            case 1:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.15;
                                break;
                            case 2:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.15;
                                break;
                            case 3:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.15;
                                break;
                            case 4:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.15;
                                break;
                            case 5:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.1;
                                break;
                            case 6:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.1;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        objCommission.isDoubled = false;
                        switch (i)
                        {
                            case 0:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.5;
                                break;
                            case 1:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.075;
                                break;
                            case 2:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.075;
                                break;
                            case 3:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.075;
                                break;
                            case 4:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.075;
                                break;
                            case 5:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.05;
                                break;
                            case 6:
                                dCommission = (objOrder.subTotalAmount / 100) * 0.05;
                                break;
                            default:
                                break;
                        }
                    }

                    objCommission.cMoney = dCommission;
                    objCommission.orderId = objOrder.orderId;
                    objCommission.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
                    objCommission.refreeId = Convert.ToInt64(ViewState["createdBy"].ToString());
                    objCommission.isSalesComission = false;
                    objCommission.createCommission();
                    objUser.referralId = dtUser.Rows[0]["friendsReferralId"].ToString();
                    dtUser = objUser.getUserByReferralIdForPayment();
                }
                else
                {
                    break;
                }
            }
            objOrder.createdBy = Convert.ToInt64(ViewState["createdBy"].ToString());
            if (objOrder.getTotalAmountOfGivenMonthByUserID())
            {
                objCommission.updateCommission();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
