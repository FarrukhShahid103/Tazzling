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


public partial class admin_UserControls_Order_OrderDetails : System.Web.UI.UserControl
{
    BLLDealOrders objOrders = new BLLDealOrders();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtUser = null;

        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString().Trim() != "")
            {
                RenderOrderDetailHTML(Request.QueryString["ID"].ToString());
            }
            else if (Request.QueryString["OID"] != null)
            {
                RenderOrderDetailHTMLByOrderID(Request.QueryString["OID"].ToString().Trim());
            }
        }
    }


    public void RenderOrderDetailHTML(string OrderID)
    {
        DataTable dtOrdersInfo;
        objOrders = new BLLDealOrders();
        GECEncryption objEnc = new GECEncryption();
        objOrders.dOrderID = Convert.ToInt64(OrderID);
        dtOrdersInfo = objOrders.getDealOrderDetailByOrderID();      
        if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
        {
            lblBusinessName.Text = dtOrdersInfo.Rows[0]["restaurantBusinessName"] == null ? "" : dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString();
            lblDealTitle.Text = dtOrdersInfo.Rows[0]["title"] == null ? "" : dtOrdersInfo.Rows[0]["title"].ToString();
            lblStatus.Text= dtOrdersInfo.Rows[0]["status"] == null ? "" : dtOrdersInfo.Rows[0]["status"].ToString();
            lblDateTime.Text= dtOrdersInfo.Rows[0]["createdDate"] == null ? "" : dtOrdersInfo.Rows[0]["createdDate"].ToString();                       
            if (dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString().Trim() != "0")
            {
                trCardNumber.Visible = true;
                lblCardNumber.Text = dtOrdersInfo.Rows[0]["ccInfoNumber"] == null ? "" : "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
            }
            lblBillingAddress.Text = dtOrdersInfo.Rows[0]["ccInfoBAddress"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBAddress"].ToString();
            lblBillingCity.Text = dtOrdersInfo.Rows[0]["ccInfoBCity"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBCity"].ToString();
            lblBillingProvince.Text = dtOrdersInfo.Rows[0]["ccInfoBProvince"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBProvince"].ToString();
            lblBillingName.Text = dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString());                      

            
            if (dtOrdersInfo.Rows[0]["personalQty"] != null && Convert.ToInt32(dtOrdersInfo.Rows[0]["personalQty"].ToString()) > 0)
            {
              trPersonal.Visible = true;
              lblPersoanlQty.Text=dtOrdersInfo.Rows[0]["personalQty"].ToString();
              lblPersoanlUnitPrice.Text = "$" + dtOrdersInfo.Rows[0]["sellingPrice"].ToString() + " CAD";
              lblPersoanlTotal.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["personalQty"].ToString())).ToString("###.00") + " CAD";                            
            }
            if (dtOrdersInfo.Rows[0]["giftQty"] != null && Convert.ToInt32(dtOrdersInfo.Rows[0]["giftQty"].ToString()) > 0)
            {
                trGift.Visible = true;
                lblGiftQty.Text = dtOrdersInfo.Rows[0]["giftQty"].ToString();
                lblGiftUnitPrice.Text = "$" + dtOrdersInfo.Rows[0]["sellingPrice"].ToString() + " CAD";
                lblGiftTotal.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["giftQty"].ToString())).ToString("###.00") + " CAD";
            }

            lblGrandTotal.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["Qty"].ToString())).ToString("###.00") + " CAD";

            if (dtOrdersInfo.Rows[0]["shippingInfoId"] != null && dtOrdersInfo.Rows[0]["shippingInfoId"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingInfoId"].ToString()) > 0)
            {
                trShippingAndTax.Visible = true;
                trGrandTotal.Visible = true;
                lblShippingAndTax.Text = "$" + Convert.ToDouble(Convert.ToInt32(dtOrdersInfo.Rows[0]["Qty"].ToString()) * Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString())).ToString("###.00") + " CAD";
                lblGrandTotal2.Text = "$" + Convert.ToDouble(dtOrdersInfo.Rows[0]["totalAmt"].ToString()).ToString("###.00") + " CAD";
            }

            if (dtOrdersInfo.Rows[0]["tastyCreditUsed"] != null && dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString()) > 0)
            {
                trTastyCredit.Visible = true;
                lblTastyCreditUsed.Text = "$" + dtOrdersInfo.Rows[0]["tastyCreditUsed"].ToString() + " CAD";
            }
            if (dtOrdersInfo.Rows[0]["ccCreditUsed"] != null && dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString()) > 0)
            {
                trChargeCreditCard.Visible = true;
                lblCreditCreditUsed.Text = "$" + dtOrdersInfo.Rows[0]["ccCreditUsed"].ToString() + " CAD";
            }
            if (dtOrdersInfo.Rows[0]["comissionMoneyUsed"] != null && dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString()) > 0)
            {
                trComission.Visible = true;
                lblComission.Text = "$" + dtOrdersInfo.Rows[0]["comissionMoneyUsed"].ToString() + " CAD";
            }
            try
            {
                BLLMemberUsedGiftCards objBLLMemberUsedGiftCards = new BLLMemberUsedGiftCards();
                objBLLMemberUsedGiftCards.orderId = Convert.ToInt64(OrderID);
                DataTable dtgained = objBLLMemberUsedGiftCards.getTastyCreditsByOrderID();
                if (dtgained != null && dtgained.Rows.Count > 0)
                {
                    trgiven.Visible = true;
                    BLLUser objuser = new BLLUser();
                    objuser.userId = Convert.ToInt32(dtgained.Rows[0]["createdBy"].ToString().Trim());
                    DataTable dtuser = objuser.getUserByID();
                    if (dtuser != null && dtuser.Rows.Count > 0)
                    {
                        lblTastyCreaditComissionText.Text = dtuser.Rows[0]["userName"].ToString().Trim() + " get Tasty Credit";
                    }
                    else
                    {
                        lblTastyCreaditComissionText.Text = "Tasty Credit Given";
                    }
                    lblTastyCreaditComission.Text = "$" + dtgained.Rows[0]["gainedAmount"].ToString().Trim() + " CAD";
                }
                else
                {
                    BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
                    objBLLAffiliatePartnerGained.OrderId = Convert.ToInt32(OrderID);
                    DataTable dtComission = objBLLAffiliatePartnerGained.getGetAffiliatePartnerCommisionInfoByOrderID();
                    if (dtComission != null && dtComission.Rows.Count > 0)
                    {
                        trgiven.Visible = true;
                        BLLUser objuser = new BLLUser();
                        objuser.userId = Convert.ToInt32(dtComission.Rows[0]["userId"].ToString().Trim());
                        DataTable dtuser = objuser.getUserByID();
                        if (dtuser != null && dtuser.Rows.Count > 0)
                        {
                            lblTastyCreaditComissionText.Text = dtuser.Rows[0]["userName"].ToString().Trim() + " get " + dtComission.Rows[0]["affCommPer"].ToString().Trim() + "% Comission";
                        }
                        else
                        {
                            lblTastyCreaditComissionText.Text = dtComission.Rows[0]["affCommPer"].ToString().Trim() + "% Comission Given";
                        }
                        lblTastyCreaditComission.Text = "$" + dtComission.Rows[0]["gainedAmount"].ToString().Trim() + " CAD";
                    }
                }

            }
            catch (Exception ex)
            { }
          
        }


    }

    public void RenderOrderDetailHTMLByOrderID(string OrderID)
    {
        DataTable dtOrdersInfo;
        objOrders = new BLLDealOrders();
        GECEncryption objEnc = new GECEncryption();
        objOrders.orderNo =OrderID;
        dtOrdersInfo = objOrders.getDealOrderDetailByOrderNo();
        if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
        {
            lblBusinessName.Text = dtOrdersInfo.Rows[0]["restaurantBusinessName"] == null ? "" : dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString();
            lblDealTitle.Text = dtOrdersInfo.Rows[0]["title"] == null ? "" : dtOrdersInfo.Rows[0]["title"].ToString();
            lblStatus.Text = dtOrdersInfo.Rows[0]["status"] == null ? "" : dtOrdersInfo.Rows[0]["status"].ToString();
            lblDateTime.Text = dtOrdersInfo.Rows[0]["createdDate"] == null ? "" : dtOrdersInfo.Rows[0]["createdDate"].ToString();
            if (dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString().Trim() != "0")
            {
                trCardNumber.Visible = true;
                lblCardNumber.Text = dtOrdersInfo.Rows[0]["ccInfoNumber"] == null ? "" : "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtOrdersInfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
            }
            lblBillingAddress.Text = dtOrdersInfo.Rows[0]["ccInfoBAddress"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBAddress"].ToString();
            lblBillingName.Text = dtOrdersInfo.Rows[0]["ccInfoBName"] == null ? "" : objEnc.DecryptData("colintastygochengusername", dtOrdersInfo.Rows[0]["ccInfoBName"].ToString());
            lblBillingCity.Text = dtOrdersInfo.Rows[0]["ccInfoBCity"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBCity"].ToString();
            lblBillingProvince.Text = dtOrdersInfo.Rows[0]["ccInfoBProvince"] == null ? "" : dtOrdersInfo.Rows[0]["ccInfoBProvince"].ToString();
            object sumPersonalQty;
            sumPersonalQty = dtOrdersInfo.Compute("Sum(personalQty)", "personalQty > 0");

            if (sumPersonalQty.ToString().Trim() != "" && Convert.ToInt32(sumPersonalQty.ToString().Trim()) > 0)
            {
                trPersonal.Visible = true;
                lblPersoanlQty.Text = sumPersonalQty.ToString().Trim();
                lblPersoanlUnitPrice.Text = "$" + dtOrdersInfo.Rows[0]["sellingPrice"].ToString() + " CAD";
                lblPersoanlTotal.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(sumPersonalQty.ToString())).ToString("###.00") + " CAD";
            }

            object sumGiftQty;
            sumGiftQty = dtOrdersInfo.Compute("Sum(giftQty)", "giftQty > 0");

            if (sumGiftQty.ToString().Trim() != "" && Convert.ToInt32(sumGiftQty.ToString().Trim()) > 0)
            {
                trGift.Visible = true;
                lblGiftQty.Text = sumGiftQty.ToString().Trim();
                lblGiftUnitPrice.Text = "$" + dtOrdersInfo.Rows[0]["sellingPrice"].ToString() + " CAD";
                lblGiftTotal.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(sumGiftQty)).ToString("###.00") + " CAD";
            }

            object sumTotalQty;
            sumTotalQty = dtOrdersInfo.Compute("Sum(Qty)", "Qty > 0");
            if (sumTotalQty.ToString().Trim() != "" && Convert.ToInt32(sumTotalQty.ToString()) > 0)
            {
                lblGrandTotal.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["sellingPrice"].ToString()) * Convert.ToDouble(sumTotalQty.ToString().Trim())).ToString("###.00") + " CAD";
            }

            if (dtOrdersInfo.Rows[0]["shippingInfoId"] != null && dtOrdersInfo.Rows[0]["shippingInfoId"].ToString().Trim() != "" && Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingInfoId"].ToString()) > 0)
            {
                trShippingAndTax.Visible = true;
                trGrandTotal.Visible = true;
                lblShippingAndTax.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["shippingAndTaxAmount"].ToString()) * Convert.ToDouble(sumTotalQty.ToString().Trim())).ToString("###.00") + " CAD";
                lblGrandTotal2.Text = "$" + Convert.ToDouble(Convert.ToDouble(dtOrdersInfo.Rows[0]["totalAmt"].ToString()) * Convert.ToDouble(sumTotalQty.ToString().Trim())).ToString("###.00") + " CAD";
            }


            object tastyCreditUsed;
            tastyCreditUsed = dtOrdersInfo.Compute("Sum(tastyCreditUsed)", "tastyCreditUsed > 0");

            if (tastyCreditUsed.ToString().Trim() != "" && Convert.ToDouble(tastyCreditUsed.ToString().Trim()) > 0)
            {
                trTastyCredit.Visible = true;
                lblTastyCreditUsed.Text = "$" + tastyCreditUsed.ToString().Trim() + " CAD";
            }

            object ccCreditUsed;
            ccCreditUsed = dtOrdersInfo.Compute("Sum(ccCreditUsed)", "ccCreditUsed > 0");

            if (ccCreditUsed.ToString().Trim() != "" && Convert.ToDouble(ccCreditUsed.ToString().Trim()) > 0)
            {
                trChargeCreditCard.Visible = true;
                lblCreditCreditUsed.Text = "$" + ccCreditUsed.ToString().Trim() + " CAD";
            }

             object comissionMoneyUsed;
            comissionMoneyUsed = dtOrdersInfo.Compute("Sum(comissionMoneyUsed)", "comissionMoneyUsed > 0");

            if (comissionMoneyUsed.ToString().Trim() != "" && Convert.ToDouble(comissionMoneyUsed.ToString().Trim()) > 0)
            {
                trComission.Visible = true;
                lblComission.Text = "$" + comissionMoneyUsed.ToString().Trim() + " CAD";
            }
            try
            {
                for (int i = 0; i < dtOrdersInfo.Rows.Count; i++)
                {
                    BLLMemberUsedGiftCards objBLLMemberUsedGiftCards = new BLLMemberUsedGiftCards();
                    objBLLMemberUsedGiftCards.orderId = Convert.ToInt64(dtOrdersInfo.Rows[i]["dorderid"].ToString().Trim());
                    DataTable dtgained = objBLLMemberUsedGiftCards.getTastyCreditsByOrderID();
                    if (dtgained != null && dtgained.Rows.Count > 0)
                    {
                        trgiven.Visible = true;
                        BLLUser objuser = new BLLUser();
                        objuser.userId = Convert.ToInt32(dtgained.Rows[0]["createdBy"].ToString().Trim());
                        DataTable dtuser = objuser.getUserByID();
                        if (dtuser != null && dtuser.Rows.Count > 0)
                        {
                            lblTastyCreaditComissionText.Text = dtuser.Rows[0]["userName"].ToString().Trim() + " get Tasty Credit";
                        }
                        else
                        {
                            lblTastyCreaditComissionText.Text = "Tasty Credit Given";
                        }
                        lblTastyCreaditComission.Text = "$" + dtgained.Rows[0]["gainedAmount"].ToString().Trim() + " CAD";
                        break;
                    }
                    else
                    {
                        BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
                        objBLLAffiliatePartnerGained.OrderId = Convert.ToInt32(dtOrdersInfo.Rows[i]["dorderid"].ToString().Trim());
                        DataTable dtComission = objBLLAffiliatePartnerGained.getGetAffiliatePartnerCommisionInfoByOrderID();
                        if (dtComission != null && dtComission.Rows.Count > 0)
                        {
                            trgiven.Visible = true;
                            BLLUser objuser = new BLLUser();
                            objuser.userId = Convert.ToInt32(dtComission.Rows[0]["userId"].ToString().Trim());
                            DataTable dtuser = objuser.getUserByID();
                            if (dtuser != null && dtuser.Rows.Count > 0)
                            {
                                lblTastyCreaditComissionText.Text = dtuser.Rows[0]["userName"].ToString().Trim() + " get " + dtComission.Rows[0]["affCommPer"].ToString().Trim() + "% Comission";
                            }
                            else
                            {
                                lblTastyCreaditComissionText.Text = dtComission.Rows[0]["affCommPer"].ToString().Trim() + "% Comission Given";
                            }
                            lblTastyCreaditComission.Text = "$" + dtComission.Rows[0]["gainedAmount"].ToString().Trim() + " CAD";
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            { }

        }

    }

}
