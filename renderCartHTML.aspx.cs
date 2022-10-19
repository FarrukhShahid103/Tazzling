using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class renderCartHTML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                double dOrderSubTotal = 0;
                double dOrderSubTotalWithoutDiscoutn = 0;
                double dOrderShippingCharges = 0;
                if (Session["dtProductCart"] != null)
                {
                    DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                    if (dtProductCart != null && dtProductCart.Rows.Count > 0)
                    {
                        string ltShoppingCart = "<div style='float: left;'>";
                        for (int i = 0; i < dtProductCart.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dtProductCart.Rows[i]["enableSize"].ToString().Trim()))
                            {
                                ltShoppingCart += "<div id='div-" + dtProductCart.Rows[i]["Size"].ToString().Trim() + dtProductCart.Rows[i]["productID"].ToString().Trim() + "' style='clear: both; width: 640px; background-color:white;'><div style='clear: both; width: 640px; padding-top: 5px;'></div>";

                                ltShoppingCart += "<div style='clear: both; font-size: 25px; color: black; font-weight: 700; width: 100%; padding-top: 10px;'><div style='float: left; padding-left:15px;'>";
                                if (Convert.ToBoolean(dtProductCart.Rows[i]["enableSize"].ToString().Trim()))
                                {
                                    ltShoppingCart += dtProductCart.Rows[i]["title"].ToString().Trim() + "</div><div style='float: right; padding-top: 5px; padding-right:15px;' class='cartRemove' onclick='javascript:RemoveFromCartWithSize(" + dtProductCart.Rows[i]["productID"].ToString() + ",\"" + dtProductCart.Rows[i]["Size"].ToString() + "\");'>";
                                }
                                else
                                {
                                    ltShoppingCart += dtProductCart.Rows[i]["title"].ToString().Trim() + "</div><div style='float: right; padding-top: 5px; padding-right:15px;' class='cartRemove' onclick='javascript:RemoveFromCart(" + dtProductCart.Rows[i]["productID"].ToString() + ");'>";
                                }

                                ltShoppingCart += "</div></div>";

                                ltShoppingCart += "<div style='clear: both; width: 640px; padding-top: 15px;'><div style='float: left; padding-right: 25px;'>";
                            }
                            else
                            {
                                ltShoppingCart += "<div id='div-" + dtProductCart.Rows[i]["productID"].ToString().Trim() + "' style='clear: both; width: 640px; background-color:white;'><div style='clear: both; width: 640px; padding-top: 5px;'></div>";

                                ltShoppingCart += "<div style='clear: both; font-size: 25px; color: black; font-weight: 700; width: 100%; padding-top: 10px;'><div style='float: left; padding-left:15px;'>";
                                if (Convert.ToBoolean(dtProductCart.Rows[i]["enableSize"].ToString().Trim()))
                                {
                                    ltShoppingCart += dtProductCart.Rows[i]["title"].ToString().Trim() + "</div><div style='float: right; padding-top: 5px; padding-right:15px;' class='cartRemove' onclick='javascript:RemoveFromCartWithSize(" + dtProductCart.Rows[i]["productID"].ToString() + ",\"" + dtProductCart.Rows[i]["Size"].ToString() + "\");'>";
                                }
                                else
                                {
                                    ltShoppingCart += dtProductCart.Rows[i]["title"].ToString().Trim() + "</div><div style='float: right; padding-top: 5px; padding-right:15px;' class='cartRemove' onclick='javascript:RemoveFromCart(" + dtProductCart.Rows[i]["productID"].ToString() + ");'>";
                                }

                                ltShoppingCart += "</div></div>";

                                ltShoppingCart += "<div style='clear: both; width: 640px; padding-top: 15px;'><div style='float: left; padding-right: 25px;'>";
                            }
                            ltShoppingCart += "<img src='" + dtProductCart.Rows[i]["image"].ToString().Trim() + "' width='216px' height='157px' /></div>";
                            ltShoppingCart += "<div style='float: left; text-align: left; width:384px; padding-right:15px;'>";



                            ltShoppingCart += "<div style='clear: both; font-size: 13px; padding-top: 10px; font-weight: 300;'><div style='width: 110px; float: left; text-align: left; color: #313131;'>";
                            ltShoppingCart += "Estimated Arrival</div><div style='padding-left: 10px; float: left; text-align: left; color: #313131;'>" + dtProductCart.Rows[i]["estimatedArivalTime"].ToString().Trim() + "</div></div>";
                            ltShoppingCart += "<div style='border-bottom: 1px solid #F3F3F3; width: 100%; clear: both; padding-top: 10px;'></div><div style='clear: both; font-size: 13px; padding-top: 5px; font-weight: 300;'><div style='width: 110px; float: left; text-align: left; color: #313131; vertical-align: top;'>Return Policy</div><div style='padding-left: 10px; float: left; text-align: left; color: #313131; width: 260px;'>";
                            ltShoppingCart += dtProductCart.Rows[i]["returnPolicy"].ToString().Trim() + "</div></div><div style='border-bottom: 1px solid #F3F3F3; width: 100%; clear: both; padding-top: 15px;'></div>";
                            ltShoppingCart += "<div style='clear: both; font-size: 13px; padding-top: 5px; font-weight: 300;'><div style='width: 110px; float: left; text-align: left; color: #313131; vertical-align: top;'>Availablity</div><div style='padding-left: 10px; float: left; text-align: left; color: #313131; width: 260px;'>";
                            if (Convert.ToBoolean(dtProductCart.Rows[i]["shipCanada"].ToString().Trim())
                              && Convert.ToBoolean(dtProductCart.Rows[i]["shipUSA"].ToString().Trim()))
                            {
                                ltShoppingCart += "Ship To US and Canada Only";
                            }
                            else if (Convert.ToBoolean(dtProductCart.Rows[i]["shipCanada"].ToString().Trim()))
                            {
                                ltShoppingCart += "Ship To Canada Only";
                            }
                            else if (Convert.ToBoolean(dtProductCart.Rows[i]["shipUSA"].ToString().Trim()))
                            {
                                ltShoppingCart += "Ship To US Only";
                            }
                            ltShoppingCart += "</div></div><div style='border-bottom: 1px solid #F3F3F3; width: 100%; clear: both; padding-top: 5px;'></div><div style='clear: both; width:384px; padding-right:15px; text-align: right; padding-top: 10px; font-weight: 700;color: black; font-size: 16px;'>$";
                            ltShoppingCart += Convert.ToDecimal(dtProductCart.Rows[i]["sellingPrice"].ToString()).ToString("0.###").Trim() + "</div>";
                            ltShoppingCart += "<div style='clear: both; text-align: left; color: #313131; padding-top: 10px; width:384px; padding-right:15px;'><div style='float: right; font-size: 15px;'>";
                            if (Convert.ToBoolean(dtProductCart.Rows[i]["enableSize"].ToString().Trim()))
                            {
                                ltShoppingCart += BindSize(dtProductCart.Rows[i]["productID"].ToString().Trim(), dtProductCart.Rows[i]["Size"].ToString().Trim(), dtProductCart.Rows[i]["Qty"].ToString().Trim());

                            }
                            else
                            {
                                ltShoppingCart += BindQuantityDropDown(Convert.ToInt32(dtProductCart.Rows[i]["minQty"].ToString().Trim()), Convert.ToInt32(dtProductCart.Rows[i]["maxQty"].ToString().Trim()), Convert.ToInt32(dtProductCart.Rows[i]["Qty"].ToString().Trim()), dtProductCart.Rows[i]["productID"].ToString().Trim());
                                ltShoppingCart += "</div><div style='float: right; font-size: 15px; padding-top: 3px; padding-right: 5px;padding-left: 10px;'>Quantity</div><div style='float: right; font-size: 15px;'>";
                                ltShoppingCart += "";
                                ltShoppingCart += "</div><div style='float: right; font-size: 15px; padding-top: 3px; padding-right: 5px;'>";
                                ltShoppingCart += "";
                            }
                            ltShoppingCart += "</div></div>";
                            ltShoppingCart += "<div style='clear: both; width:384px; padding-right:15px; text-align: right; padding-top: 10px; font-weight: 700;color: black; font-size: 19px;'>$";
                            ltShoppingCart += (Convert.ToDouble(dtProductCart.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtProductCart.Rows[i]["Qty"].ToString().Trim())).ToString() + "</div></div></div><div style='clear: both; width: 640px; border-bottom: 5px solid #F3F3F3; padding-top: 15px;'></div></div>";
                            dOrderSubTotal += Convert.ToDouble(dtProductCart.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtProductCart.Rows[i]["Qty"].ToString().Trim());
                            dOrderSubTotalWithoutDiscoutn += Convert.ToDouble(dtProductCart.Rows[i]["valuePrice"].ToString().Trim()) * Convert.ToDouble(dtProductCart.Rows[i]["Qty"].ToString().Trim());

                        }
                        ltShoppingCart += "</div>";

                        ltShoppingCart += @"<div style='float: left; padding-left: 40px;'>
                                                    <div  id='divCart' style='clear:both; position:fixed; width:317px; height:365px; background-color:white; overflow:hidden;font-size: 15px; top:253px; box-shadow: 0px 3px 0px #888888'>
                                                        <div style='background-image:url(images/SubContentDot.png); background-repeat:no-repeat; width:100%; margin-left:5px; margin-top:10px; height:14px; text-align:center;'>
                                                        </div>
                                                        <div style='clear:both; float:left; overflow:hidden; padding:15px; width:90%;'>
                                                            <div style='float:left; overflow:hidden;'>
                                                                <div style='float:left; font-size:25px; font-weight:bold; padding-top:10px;'>Your Order</div>
                                                            </div>
                                                            <div style='float:right;'><img src='Images/YourOrderLogo.png' alt='' /></div>
                                                        </div>
                                                        <div style='clear:both; float:left; padding:0px 20px; overflow:hidden; width:87%;'>
                                                            <div style='overflow:hidden; float:left; padding:5px 0px; border-top:1px solid #F3F3F3; width:100%;'>
                                                                <div style='float:left; width:150px; color:#313131;'>Order Subtotal</div>
                                                                <div style='float:left; color:#313131;'>$" + dOrderSubTotal.ToString() + @"</div>
                                                            </div>
                                                            <div style='clear:both; float:left; border-top:1px solid #F3F3F3; width:100%; padding:5px 0px;'>
                                                                <div style='float:left; overflow:hidden; width:150px;'>
                                                                    <div style='float:left; color:#313131;'>Shipping</div>
                                                                    <div style='float:left; padding-left:5px;'>
                                                                        <img src='Images/qustion-icon.png' alt='' />
                                                                    </div>
                                                                </div>
                                                                <div style='float:left; text-align:left; color:#313131;'>$" + dOrderShippingCharges.ToString() + @"</div>
                                                            </div>
                                                            <div style='clear:both; border-top:1px solid #F3F3F3; color:black; font-size:17px; font-weight:700; width:100%; padding:15px 0px;'>
                                                                <div style='float:left; width:150px;'>Total</div>
                                                                <div style='float:left;'>$" + (dOrderSubTotal + dOrderShippingCharges).ToString() + @"</div>
                                                            </div>
                                                            <div style='clear:both; background-color:#F4F4F5; border-radius:5px; width:100%; text-align:center; margin-top:30px; padding:5px 0px; color:#DD0016;'>
                                                                Your are saving: $" + (dOrderSubTotalWithoutDiscoutn - (dOrderSubTotal + dOrderShippingCharges)).ToString() + @"
                                                            </div>
                                                            <div style='clear:both; border-bottom:1px solid #F3F3F3; padding-top:15px;'></div>
                                                            <div style='clear:both; padding-top:20px;'><a href='payment.aspx'><img src='Images/CheckOut_cart.png' /></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>";
                        


                        Response.Write(ltShoppingCart);
                        Response.End();
                    }
                    else
                    {
                       /* Response.Write(ltShoppingCart);
                        Response.End();*/
                    }
                }
                else
                {
                    /*Response.Write(ltShoppingCart);
                    Response.End();  */
                }
            }
            catch (Exception ex)
            {

            }
        }
    }


    protected string BindSize(string strProductID, string strSize, string strQty)
    {

        string strSize1 = "</div><div style='float: right; font-size: 15px; padding-top: 3px; padding-right: 5px;padding-left: 10px;'>Quantity</div><div style='float: right; font-size: 15px;'>";
        string strSize2 = "</div><div style='float: right; font-size: 15px; padding-top: 3px; padding-right: 5px;'>Size";
        string strSize4 = "<select id='series" + strSize + strProductID + "' class='detailDropDown' onchange='javascript:updateCart(" + strProductID + ",\"" + strSize + "\");'>";
        string strSize3 = "<select id='mark" + strSize + strProductID + "'  class='detailDropDown' disabled='disabled'>";
        BLLProductSize objSize = new BLLProductSize();
        objSize.productID = Convert.ToInt64(strProductID);
        DataTable dtSize = objSize.getProductSizeByProductID();
        if (dtSize != null && dtSize.Rows.Count > 0)
        {
            for (int i = 0; i < dtSize.Rows.Count; i++)
            {
                if (dtSize.Rows[i]["sizeText"].ToString().Trim() == strSize.Trim())
                {
                    strSize3 += "<option selected='selected' value='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "</option>";
                    for (int a = 1; a <= Convert.ToInt32(dtSize.Rows[i]["quantity"].ToString().Trim()); a++)
                    {
                        if (a.ToString() == strQty)
                        {
                            strSize4 += "<option selected='selected' value='" + a.ToString() + "' class='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + a.ToString() + "</option>";
                        }
                        else
                        {
                            strSize4 += "<option value='" + a.ToString() + "' class='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + a.ToString() + "</option>";
                        }
                    }
                }
            }
            strSize4 += "</select>";
            strSize3 += "</select>";
        }
        return strSize4 + strSize1 + strSize3 + strSize2;

    }

    protected string BindQuantityDropDown(int min, int Qty, int Select, string ProductID)
    {
        string strQty = "<select id='seriessher" + ProductID + "' class='detailDropDown' onchange='javascript:updateCart(" + ProductID + ",\"sher\");'>";
        try
        {
            for (int i = min; i <= Qty; i++)
            {
                if (i == Select)
                {
                    strQty += "<option value='" + i.ToString() + "' selected='selected'>" + i.ToString() + "</option>";
                }
                else
                {
                    strQty += "<option value='" + i.ToString() + "'>" + i.ToString() + "</option>";
                }
            }
        }
        catch (Exception ex)
        {
        }
        return strQty += "</select>";
    }
}