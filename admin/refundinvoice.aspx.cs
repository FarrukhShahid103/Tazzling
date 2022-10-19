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
using SQLHelper;

public partial class admin_invoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["pdfdownload"] != null && Request.QueryString["pdfdownload"].Trim().ToLower() == "true"
                    && Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != ""
                        && Request.QueryString["myname"] != null && Request.QueryString["myname"].ToString().Trim() != ""
                        && Request.QueryString["myname"].ToString().Trim() == "colinazam"
                         && Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() != "")
                {
                    renderFirstDealInvoice();
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void renderFirstDealInvoice()
    {
        try
        {
            bool paymentDone = false;
            DateTime dtPaymentDate = DateTime.Now;
            string strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
            DataTable dtResult = Misc.search(strQueryForExisting);
            if (dtResult != null && dtResult.Rows.Count > 0
                && Convert.ToBoolean(dtResult.Rows[0]["paymentDone"].ToString().Trim()))
            {
                paymentDone = true;
                dtPaymentDate = Convert.ToDateTime(dtResult.Rows[0]["paymentdate"].ToString().Trim());
            }

            string strQuery = "Select [deals].[dealId] ,[deals].restaurantId ,restaurantBusinessName";
            strQuery += ",restaurantAddress,phone,businessPaymentTitle,restaurantpaymentAddress,cellNumber ";
            strQuery += ",salePersonAccountName,OurCommission,shippingAndTax,shippingAndTaxAmount";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId),0) 'TotalOrders'";
            //strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status='Successful'),0) 'SuccessfulOrders'";

            if (paymentDone)
            {
                strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful' and dealOrders.modifiedDate<=CONVERT(DATETIME,'" + dtPaymentDate + "')),0) 'CancelOrders'";
            }
            else
            {
                strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful'),0) 'CancelOrders'";
            }

            strQuery += ",[title],[sellingPrice],[valuePrice],dealStartTime,dealEndTime";
            strQuery += " from deals";
            strQuery += " inner join restaurant on restaurant.restaurantId=deals.restaurantId";
            strQuery += " where ([deals].[dealId] = " + Request.QueryString["did"].Trim() + " or [deals].parentdealId = " + Request.QueryString["did"].Trim() + ") Order by [deals].[dealId]";
            DataTable dtDealDetail = Misc.search(strQuery);
            double dTotalCancelOrders = 0;
            int i = 0;
            for (i = 0; i < dtDealDetail.Rows.Count; i++)
            {               
                double tempRefunded = 0;
                double tempccTransactionFee = 0;
                if (Convert.ToDouble(dtDealDetail.Rows[i]["CancelOrders"].ToString().Trim()) > 0)
                {
                    tempRefunded = Math.Round((Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["CancelOrders"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
                    double tempAdveriseFee = Math.Round((Convert.ToDouble(dtDealDetail.Rows[i]["OurCommission"].ToString().Trim()) / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);

                    if (dtDealDetail.Rows[i]["shippingAndTax"] != null && Convert.ToBoolean(dtDealDetail.Rows[i]["shippingAndTax"].ToString().Trim()))
                    {
                        tempRefunded = (Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()) + Convert.ToDouble(dtDealDetail.Rows[i]["shippingAndTaxAmount"].ToString().Trim())) * Convert.ToDouble(dtDealDetail.Rows[i]["CancelOrders"].ToString().Trim());
                        tempccTransactionFee = Math.Round((3.9 / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        tempccTransactionFee = Math.Round((3.9 / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                    }
                    double tempTax = Math.Round((12.00 / 100) * tempAdveriseFee, 2, MidpointRounding.AwayFromZero);
                    dTotalCancelOrders += Math.Round(((tempAdveriseFee + tempTax - tempccTransactionFee) - (tempRefunded)), 2, MidpointRounding.AwayFromZero);
                }
            }

            dTotalCancelOrders = dTotalCancelOrders * (-1);

            GECEncryption objEnc = new GECEncryption();
            strQuery = "";
            strQuery = "select dealOrderCode,dealOrders.createdDate, dealOrders.modifiedDate,shippingAndTax,deals.shippingAndTaxAmount, sellingPrice,qty ,title,qty, comment from dealorders ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.dOrderID = dealOrders.dOrderID";
            strQuery += " inner join deals on deals.dealID = dealOrders.dealID";
            strQuery += " left outer join userComments on (userComments.dOrderID = dealOrders.dOrderID) ";
            if (paymentDone)
            {
                strQuery += " where status<>'Successful' and dealOrders.modifiedDate<=CONVERT(DATETIME,'" + dtPaymentDate + "') and dealOrders.dealid in (select dealId from deals where dealid=";
            }
            else
            {
                strQuery += " where status<>'Successful' and dealOrders.dealid in (select dealId from deals where dealid=";
            }                 
            strQuery += Request.QueryString["did"].Trim() + " or parentDealId=" + Request.QueryString["did"].Trim() + ") order by dealOrders.modifiedDate desc";
            DataTable dtRefundedOrders = Misc.search(strQuery);
            i = 0;
            if (dtRefundedOrders != null && dtRefundedOrders.Rows.Count > 0)
            {


                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 16px; padding-top: 70px; padding-left: 15px;font-weight: bold;'>Refunds</div>    <div style='width: 100%; border-bottom: 1px solid black; clear: both;'></div>";
                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; height: 25px; padding-top: 5px;'><div style='width: 150px; float: left; text-align: left; padding-left: 15px;'>Date</div>";
                ltRefundOrders.Text += "<div style='width: 250px; float: left; text-align: left; font-weight: bold;'>Voucher Code</div><div style='width: 385px; float: left; text-align: left;'>Detail</div><div style='width: 100px; float: left; text-align: left;'>Line Total</div></div><div style='width: 100%; border-bottom: 1px solid black; clear: both;'></div>";

                for (i = 0; i < dtRefundedOrders.Rows.Count; i++)
                {
                    try
                    {
                        if (dtRefundedOrders.Rows[i]["shippingAndTax"] != null && Convert.ToBoolean(dtRefundedOrders.Rows[i]["shippingAndTax"].ToString().Trim()))
                        {

                            if (i % 2 == 0)
                            {
                                //Refunded Orders Display
                                DateTime dtModify = DateTime.Now;
                                DateTime.TryParse(dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim(), out dtModify);
                                string[] strTempReasong = dtRefundedOrders.Rows[i]["comment"].ToString().Trim().Split(':');

                                //Refund First Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "# " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString().Trim());
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                if (strTempReasong.Length > 1)
                                {
                                    if (strTempReasong[strTempReasong.Length - 1].Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += strTempReasong[strTempReasong.Length - 1].Trim();
                                    }
                                }
                                else
                                {
                                    if (dtRefundedOrders.Rows[i]["comment"].ToString().Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += dtRefundedOrders.Rows[i]["comment"].ToString().Trim();
                                    }
                                }
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";

                                double dTempOrderValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                                ltRefundOrders.Text += dTempOrderValue;
                                ltRefundOrders.Text += "</div></div>";




                                //Refund Second Reverse Comission
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse Commission";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) + "% x $" + dTempOrderValue;
                                double dTempComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTempComission;
                                ltRefundOrders.Text += "</div></div>";

                                double dTempShippingValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                                dTempOrderValue += dTempShippingValue;

                                //Shipping And Tax Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "Shipping Charged";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "-$" + dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim();
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempShippingValue;
                                ltRefundOrders.Text += "</div></div>";




                                //Refund Fourth Tax Reverse
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse HST #849725056";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "12% x $" + dTempComission;
                                double dTax = Math.Round((12.0 / 100) * dTempComission, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTax;
                                ltRefundOrders.Text += "</div></div>";


                                //Refund Third Deduct Credit Card Transaction Fee
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Credit Card Transaction Fee";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "3.9% x $" + dTempOrderValue;
                                double dTempCreditCardFee = Math.Round((3.9 / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempCreditCardFee;
                                ltRefundOrders.Text += "</div></div>";

                            }
                            else
                            {
                                //Refunded Orders Display
                                DateTime dtModify = DateTime.Now;
                                DateTime.TryParse(dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim(), out dtModify);
                                string[] strTempReasong = dtRefundedOrders.Rows[i]["comment"].ToString().Trim().Split(':');

                                //Refund First Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "# " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString().Trim());
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                if (strTempReasong.Length > 1)
                                {
                                    if (strTempReasong[strTempReasong.Length - 1].Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += strTempReasong[strTempReasong.Length - 1].Trim();
                                    }
                                }
                                else
                                {
                                    if (dtRefundedOrders.Rows[i]["comment"].ToString().Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += dtRefundedOrders.Rows[i]["comment"].ToString().Trim();
                                    }
                                }
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";

                                double dTempOrderValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                                ltRefundOrders.Text += dTempOrderValue;
                                ltRefundOrders.Text += "</div></div>";


                                //Refund Second Reverse Comission
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse Commission";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) + "% x $" + dTempOrderValue;
                                double dTempComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTempComission;
                                ltRefundOrders.Text += "</div></div>";

                                double dTempShippingValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());
                                dTempOrderValue += dTempShippingValue;

                                //Shipping And Tax Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "Shipping Charged";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";

                                ltRefundOrders.Text += "-$" + dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim();
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempShippingValue;
                                ltRefundOrders.Text += "</div></div>";


                                //Refund Fourth Tax Reverse
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse HST #849725056";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "12% x $" + dTempComission;
                                double dTax = Math.Round((12.0 / 100) * dTempComission, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTax;
                                ltRefundOrders.Text += "</div></div>";

                                //Refund Third Deduct Credit Card Transaction Fee
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Credit Card Transaction Fee";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "3.9% x $" + dTempOrderValue;
                                double dTempCreditCardFee = Math.Round((3.9 / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempCreditCardFee;
                                ltRefundOrders.Text += "</div></div>";

                            }

                        }
                        else
                        {
                            //Refunded Orders Display
                            DateTime dtModify = DateTime.Now;
                            DateTime.TryParse(dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim(), out dtModify);
                            string[] strTempReasong = dtRefundedOrders.Rows[i]["comment"].ToString().Trim().Split(':');

                            //Refund First Line
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                            ltRefundOrders.Text += "# " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString().Trim());
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            if (strTempReasong.Length > 1)
                            {
                                if (strTempReasong[strTempReasong.Length - 1].Trim() == "")
                                {
                                    ltRefundOrders.Text += "-";
                                }
                                else
                                {
                                    ltRefundOrders.Text += strTempReasong[strTempReasong.Length - 1].Trim();
                                }
                            }
                            else
                            {
                                if (dtRefundedOrders.Rows[i]["comment"].ToString().Trim() == "")
                                {
                                    ltRefundOrders.Text += "-";
                                }
                                else
                                {
                                    ltRefundOrders.Text += dtRefundedOrders.Rows[i]["comment"].ToString().Trim();
                                }
                            }
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";

                            double dTempOrderValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                            ltRefundOrders.Text += dTempOrderValue;
                            ltRefundOrders.Text += "</div></div>";

                            //Refund Second Reverse Comission
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                            ltRefundOrders.Text += "Reverse Commission";
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            ltRefundOrders.Text += Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) + "% x $" + dTempOrderValue;
                            double dTempComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                            ltRefundOrders.Text += dTempComission;
                            ltRefundOrders.Text += "</div></div>";

                            //Refund Fourth Tax Reverse
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                            ltRefundOrders.Text += "Reverse HST #849725056";
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            ltRefundOrders.Text += "12% x $" + dTempComission;
                            double dTax = Math.Round((12.0 / 100) * dTempComission, 2, MidpointRounding.AwayFromZero);
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                            ltRefundOrders.Text += dTax;
                            ltRefundOrders.Text += "</div></div>";


                            //Refund Third Deduct Credit Card Transaction Fee
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                            ltRefundOrders.Text += "Credit Card Transaction Fee";
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            ltRefundOrders.Text += "3.9% x $" + dTempOrderValue;
                            double dTempCreditCardFee = Math.Round((3.9 / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                            ltRefundOrders.Text += dTempCreditCardFee;
                            ltRefundOrders.Text += "</div></div>";


                        }

                    }
                    catch (Exception ex)
                    {

                    }


                }

                if (i % 2 == 0)
                {
                    ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                }
                else
                {
                    ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                }
                ltRefundOrders.Text += "Total Refunds";
                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                ltRefundOrders.Text += "&nbsp;";
                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                ltRefundOrders.Text += "&nbsp;";
                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                ltRefundOrders.Text += dTotalCancelOrders;
                ltRefundOrders.Text += "</div></div><div style=width: 100%; border-bottom: 1px solid black; clear: both;></div>";


            }

        }
        catch (Exception ex)
        { }
    }
}
