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
            bool shippingTaxDealExist = false;
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
           // strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status='Successful'),0) 'SuccessfulOrders'";
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
            double dDealAmountTotal = 0;
            double dTotalCancelOrders = 0;
            int i = 0;
            if (dtDealDetail != null && dtDealDetail.Rows.Count > 0)
            {
                lblBusinessAddress.Text = dtDealDetail.Rows[0]["restaurantAddress"].ToString().Trim();
                lblBusinessName.Text = dtDealDetail.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblBusinessPaymentAddress.Text = dtDealDetail.Rows[0]["restaurantpaymentAddress"].ToString().Trim();
                lblBusinessPaymentPhone.Text = dtDealDetail.Rows[0]["cellNumber"].ToString().Trim();
                lblBusinessPaymentTitle.Text = dtDealDetail.Rows[0]["businessPaymentTitle"].ToString().Trim();
                lblBusinessPhone.Text = dtDealDetail.Rows[0]["phone"].ToString().Trim();              

                lblInvoiceDate.Text = "INVOICE DATE: " + DateTime.Now.ToString("yyyy/MM/dd");
                if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "1st")
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-1";
                }
                else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "2nd")
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-2";
                }
                else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "3rd")
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-3";
                }
                else
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-1";
                }
            }
            for (i = 0; i < dtDealDetail.Rows.Count; i++)
            {
                DateTime dtStartDate = DateTime.Now;
                DateTime dtEndDate = DateTime.Now;
                DateTime.TryParse(dtDealDetail.Rows[i]["dealStartTime"].ToString().Trim(), out dtStartDate);
                DateTime.TryParse(dtDealDetail.Rows[i]["dealEndTime"].ToString().Trim(), out dtEndDate);
                double tempRefunded = 0;
                double tempccTransactionFee = 0;
                if (dtDealDetail.Rows[i]["shippingAndTax"] != null && Convert.ToBoolean(dtDealDetail.Rows[i]["shippingAndTax"].ToString().Trim()))
                {
                    shippingTaxDealExist = true;
                }
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

                if (i % 2 == 0)
                {
                    try
                    {
                        //Code for the Deal Titles to show
                        ltDealsDetails.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 650px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtDealDetail.Rows[i]["title"].ToString().Trim();
                        ltDealsDetails.Text += "</div><div style='width: 250px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtStartDate.ToString("yyyy/MM/dd") + " - " + dtEndDate.ToString("yyyy/MM/dd") + "</div></div>";

                        //Total Sales Area Code
                        ltDealsOrderDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 430px; float: left; text-align: left; padding-left: 10px;padding-right:10px;'>";
                        ltDealsOrderDetail.Text += dtDealDetail.Rows[i]["title"].ToString().Trim() + " Sold (" + dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + ")";
                        ltDealsOrderDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltDealsOrderDetail.Text += dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + " x $" + dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim();
                        ltDealsOrderDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        double dDealTotal = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                        ltDealsOrderDetail.Text += "$" + dDealTotal + "</div></div>";
                        dDealAmountTotal += dDealTotal;


                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        //Code for the Deal Titles to show
                        ltDealsDetails.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 650px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtDealDetail.Rows[i]["title"].ToString().Trim();
                        ltDealsDetails.Text += "</div><div style='width: 250px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtStartDate.ToString("yyyy/MM/dd") + " - " + dtEndDate.ToString("yyyy/MM/dd") + "</div></div>";

                        //Total Sales Area Code
                        ltDealsOrderDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 430px; float: left; text-align: left; padding-left: 10px;padding-right: 10px;'>";
                        ltDealsOrderDetail.Text += dtDealDetail.Rows[i]["title"].ToString().Trim() + " Sold ( " + dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + " )";
                        ltDealsOrderDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltDealsOrderDetail.Text += dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + " x $" + dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim();
                        ltDealsOrderDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        double dDealTotal = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                        ltDealsOrderDetail.Text += "$" + dDealTotal + "</div></div>";
                        dDealAmountTotal += dDealTotal;
                    }
                    catch (Exception ex)
                    { }
                }
            }

            //Code For All deals total Sold Out
            if (i % 2 == 0)
            {
                ltDealsOrderDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltDealsOrderDetail.Text += "Total Sales Generated";
                ltDealsOrderDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>&nbsp;";
                ltDealsOrderDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                ltDealsOrderDetail.Text += "$" + dDealAmountTotal + "</div></div>";
            }
            else
            {
                ltDealsOrderDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltDealsOrderDetail.Text += "Total Sales Generated";
                ltDealsOrderDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>&nbsp;";
                ltDealsOrderDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                ltDealsOrderDetail.Text += "$" + dDealAmountTotal + "</div></div>";
            }          

            // New Code

            //Tasty Comission Calculation and show
            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
            ltServiceFeeDetail.Text += "Tastygo Service (Thank You)";
            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
            ltServiceFeeDetail.Text += Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) + "% x $" + dDealAmountTotal;
            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
            double dTotalOurComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dDealAmountTotal, 2, MidpointRounding.AwayFromZero);
            ltServiceFeeDetail.Text += "$" + dTotalOurComission + "</div></div>";

            double dTotalShippingFee = 0;
            double dTotalCreditCardFee = 0;
            double dTotalCommionPlusCreditCardFee = 0;
            double dTotalSalesTax = 0;
            double dTotalCommionPlusCreditCardFeePlusTax = 0;
            double dTotalPayoutToBusiness = 0;
            double dTotalAdjustment = 0;

            if (shippingTaxDealExist)
            {
                i = 0;
                for (i = 0; i < dtDealDetail.Rows.Count; i++)
                {
                    //Shipping and Tax fee Calculation and show

                    if (i % 2 == 0)
                    {
                        if (dtDealDetail.Rows[i]["shippingAndTax"] != null && Convert.ToBoolean(dtDealDetail.Rows[i]["shippingAndTax"].ToString().Trim()))
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += "Shipping Charged on your Behalf (" + dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + ")";
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + " x $" + dtDealDetail.Rows[i]["shippingAndTaxAmount"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            double dTempShippingFee = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dTempShippingFee + "</div></div>";
                            dTotalShippingFee += dTempShippingFee;
                        }
                        else
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 740px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtDealDetail.Rows[i]["title"].ToString().Trim() + " (No Shipping and Tax)";
                            //ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>&nbsp;";                            
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            ltServiceFeeDetail.Text += "$0</div></div>";
                        }
                    }
                    else
                    {
                        if (dtDealDetail.Rows[i]["shippingAndTax"] != null && Convert.ToBoolean(dtDealDetail.Rows[i]["shippingAndTax"].ToString().Trim()))
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += "Shipping Charged on your Behalf (" + dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + ")";
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim() + " x $" + dtDealDetail.Rows[i]["shippingAndTaxAmount"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            double dTempShippingFee = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dTempShippingFee + "</div></div>";
                            dTotalShippingFee += dTempShippingFee;
                        }
                        else
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 740px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtDealDetail.Rows[i]["title"].ToString().Trim() + " (No Shipping and Tax)";
                            //ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>&nbsp;";
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            ltServiceFeeDetail.Text += "$0</div></div>";
                        }
                    }
                }

                if (i % 2 == 0)
                {
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Total Shipping Collected";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "&nbsp;";
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    ltServiceFeeDetail.Text += "$" + dTotalShippingFee + "</div></div>";

                    //Credit Card Transaction fee Calculation and show
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Credit Cards Transaction";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "3.9% x $" + dDealAmountTotal + " + $" + dTotalShippingFee;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalCreditCardFee = Math.Round((3.9 / 100) * (dDealAmountTotal + dTotalShippingFee), 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalCreditCardFee + "</div></div>";

                    //SubTotal of Comission + Credit Card Fee
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Sub Total";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "$" + dTotalOurComission + " + $" + dTotalCreditCardFee;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalCommionPlusCreditCardFee = Math.Round(dTotalOurComission + dTotalCreditCardFee, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFee + "</div></div>";

                    //Tasty Comission Calculation and show
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Harmonized Sales Tax #849725056";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "12% x $" + dTotalCommionPlusCreditCardFee;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalSalesTax = Math.Round((12.0 / 100) * dTotalCommionPlusCreditCardFee, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalSalesTax + "</div></div>";

                    //SubTotal of Comission + Credit Card Fee + Tax            
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Sub Total";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFee + " + $" + dTotalSalesTax;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalCommionPlusCreditCardFeePlusTax = Math.Round(dTotalCommionPlusCreditCardFee + dTotalSalesTax, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFeePlusTax + "</div></div>";

                    dTotalCancelOrders = dTotalCancelOrders * (-1);

                    //Display Refunded Order Amount
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Refunds";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "Please see below for detail";
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    ltServiceFeeDetail.Text += "-$" + dTotalCancelOrders + "</div></div>";

                    //Amount Pay to Business Owner calculation
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Amount Forward to You";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "$" + dDealAmountTotal + "+ $" + dTotalShippingFee + " - $" + dTotalCommionPlusCreditCardFeePlusTax + " - $" + dTotalCancelOrders;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalPayoutToBusiness = Math.Round(dDealAmountTotal + dTotalShippingFee - dTotalCommionPlusCreditCardFeePlusTax - dTotalCancelOrders, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + "</div></div>";


                }
                else
                {
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Total Shipping Collected";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "&nbsp;";
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    ltServiceFeeDetail.Text += "$" + dTotalShippingFee + "</div></div>";

                    //Credit Card Transaction fee Calculation and show
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Credit Cards Transaction";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "3.9% x $" + dDealAmountTotal + " + $" + dTotalShippingFee;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalCreditCardFee = Math.Round((3.9 / 100) * (dDealAmountTotal + dTotalShippingFee), 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalCreditCardFee + "</div></div>";

                    //SubTotal of Comission + Credit Card Fee
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Sub Total";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "$" + dTotalOurComission + " + $" + dTotalCreditCardFee;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalCommionPlusCreditCardFee = Math.Round(dTotalOurComission + dTotalCreditCardFee, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFee + "</div></div>";

                    //Tasty Comission Calculation and show
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Harmonized Sales Tax #849725056";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "12% x $" + dTotalCommionPlusCreditCardFee;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalSalesTax = Math.Round((12.0 / 100) * dTotalCommionPlusCreditCardFee, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalSalesTax + "</div></div>";

                    //SubTotal of Comission + Credit Card Fee + Tax            
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Sub Total";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFee + " + $" + dTotalSalesTax;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalCommionPlusCreditCardFeePlusTax = Math.Round(dTotalCommionPlusCreditCardFee + dTotalSalesTax, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFeePlusTax + "</div></div>";

                    dTotalCancelOrders = dTotalCancelOrders * (-1);

                    //Display Refunded Order Amount
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Refunds";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "Please see below for detail";
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    ltServiceFeeDetail.Text += "-$" + dTotalCancelOrders + "</div></div>";

                    //Amount Pay to Business Owner calculation
                    ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltServiceFeeDetail.Text += "Amount Forward to You";
                    ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltServiceFeeDetail.Text += "$" + dDealAmountTotal + "+ $" + dTotalShippingFee + " - $" + dTotalCommionPlusCreditCardFeePlusTax + " - $" + dTotalCancelOrders;
                    ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    dTotalPayoutToBusiness = Math.Round(dDealAmountTotal + dTotalShippingFee - dTotalCommionPlusCreditCardFeePlusTax - dTotalCancelOrders, 2, MidpointRounding.AwayFromZero);
                    ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + "</div></div>";

                }


                // Adjustments 
                DataTable dtpayout = null;

                if (paymentDone)
                {
                    dtpayout = Misc.search("SELECT * FROM payOut Where dealId = " + Request.QueryString["did"].Trim() + " and poDate<=CONVERT(DATETIME,'" + dtPaymentDate + "') Order by poID asc");
                }
                else
                {
                    BLLPayOut objPayout = new BLLPayOut();
                    objPayout.dealId = Convert.ToInt64(Request.QueryString["did"].Trim());
                    dtpayout = objPayout.getPayOutByDealID();
                }
                if (dtpayout != null && dtpayout.Rows.Count > 0)
                {
                    for (i = 0; i < dtpayout.Rows.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poType"].ToString().Trim() == "" ? "&nbsp;" : dtpayout.Rows[i]["poType"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poDescription"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            dTotalAdjustment += Math.Round(Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dtpayout.Rows[i]["poAmount"].ToString().Trim() + "</div></div>";
                        }
                        else
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poType"].ToString().Trim() == "" ? "&nbsp;" : dtpayout.Rows[i]["poType"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poDescription"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            dTotalAdjustment += Math.Round(Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dtpayout.Rows[i]["poAmount"].ToString().Trim() + "</div></div>";
                        }
                    }
                    if (i % 2 == 0)
                    {
                        ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                        ltServiceFeeDetail.Text += "Amount After Adjustment";
                        ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + " + $" + dTotalAdjustment;
                        ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        ltServiceFeeDetail.Text += "$" + (dTotalPayoutToBusiness + dTotalAdjustment) + "</div></div>";
                    }
                    else
                    {
                        ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                        ltServiceFeeDetail.Text += "Amount After Adjustment";
                        ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + " + $" + dTotalAdjustment;
                        ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        ltServiceFeeDetail.Text += "$" + (dTotalPayoutToBusiness + dTotalAdjustment) + "</div></div>";
                    }
                    dTotalPayoutToBusiness = dTotalPayoutToBusiness + dTotalAdjustment;
                }

            }
            else
            {
                //Credit Card Transaction fee Calculation and show
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Credit Cards Transaction";
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "3.9% x $" + dDealAmountTotal;
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                dTotalCreditCardFee = Math.Round((3.9 / 100) * dDealAmountTotal, 2, MidpointRounding.AwayFromZero);
                ltServiceFeeDetail.Text += "$" + dTotalCreditCardFee + "</div></div>";

                //SubTotal of Comission + Credit Card Fee
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Sub Total";
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "$" + dTotalOurComission + " + $" + dTotalCreditCardFee;
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                dTotalCommionPlusCreditCardFee = Math.Round(dTotalOurComission + dTotalCreditCardFee, 2, MidpointRounding.AwayFromZero);
                ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFee + "</div></div>";

                //Tasty Comission Calculation and show
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Harmonized Sales Tax #849725056";
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "12% x $" + dTotalCommionPlusCreditCardFee;
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                dTotalSalesTax = Math.Round((12.0 / 100) * dTotalCommionPlusCreditCardFee, 2, MidpointRounding.AwayFromZero);
                ltServiceFeeDetail.Text += "$" + dTotalSalesTax + "</div></div>";

                //SubTotal of Comission + Credit Card Fee + Tax            
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Sub Total";
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFee + " + $" + dTotalSalesTax;
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                dTotalCommionPlusCreditCardFeePlusTax = Math.Round(dTotalCommionPlusCreditCardFee + dTotalSalesTax, 2, MidpointRounding.AwayFromZero);
                ltServiceFeeDetail.Text += "$" + dTotalCommionPlusCreditCardFeePlusTax + "</div></div>";

                dTotalCancelOrders = dTotalCancelOrders * (-1);

                //Display Refunded Order Amount
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Refunds";
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "Please see below for detail";
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                ltServiceFeeDetail.Text += "-$" + dTotalCancelOrders + "</div></div>";

                //Amount Pay to Business Owner calculation
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Amount Forward to You";
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "$" + dDealAmountTotal + " - $" + dTotalCommionPlusCreditCardFeePlusTax + " - $" + dTotalCancelOrders;
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                dTotalPayoutToBusiness = Math.Round(dDealAmountTotal - dTotalCommionPlusCreditCardFeePlusTax - dTotalCancelOrders, 2, MidpointRounding.AwayFromZero);
                ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + "</div></div>";

                // Adjustments 
                BLLPayOut objPayout = new BLLPayOut();
                objPayout.dealId = Convert.ToInt64(Request.QueryString["did"].Trim());
                DataTable dtpayout = objPayout.getPayOutByDealID();

                if (dtpayout != null && dtpayout.Rows.Count > 0)
                {
                    for (i = 0; i < dtpayout.Rows.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poType"].ToString().Trim() == "" ? "&nbsp;" : dtpayout.Rows[i]["poType"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poDescription"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            dTotalAdjustment += Math.Round(Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dtpayout.Rows[i]["poAmount"].ToString().Trim() + "</div></div>";
                        }
                        else
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poType"].ToString().Trim() == "" ? "&nbsp;" : dtpayout.Rows[i]["poType"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poDescription"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            dTotalAdjustment += Math.Round(Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dtpayout.Rows[i]["poAmount"].ToString().Trim() + "</div></div>";
                        }
                    }
                    if (i % 2 == 0)
                    {
                        ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                        ltServiceFeeDetail.Text += "Amount After Adjustment";
                        ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + " - $" + dTotalAdjustment;
                        ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        ltServiceFeeDetail.Text += "$" + (dTotalPayoutToBusiness - dTotalAdjustment) + "</div></div>";
                    }
                    else
                    {
                        ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                        ltServiceFeeDetail.Text += "Amount After Adjustment";
                        ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + " + $" + dTotalAdjustment;
                        ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        ltServiceFeeDetail.Text += "$" + (dTotalPayoutToBusiness + dTotalAdjustment) + "</div></div>";
                    }
                    dTotalPayoutToBusiness = dTotalPayoutToBusiness + dTotalAdjustment;
                }

            }


            //Code to Write Payment Schedule           
            lblFirstPaymentDateTime.Text = "10 Business Days After Deal Ends";            
            lblFirstPaymentPer.Text = "30";

            lblFirstPaymentAmount.Text = Math.Round((30.0 / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();
            
            ViewState["dTotalPayoutToBusiness"] = dTotalPayoutToBusiness;
            //SecondPayment            
            lblSecondPaymentDateTime.Text = "50 Business Days After Deal Ends";          
            lblSecondPaymentPer.Text = "50";
            lblSecondPaymentAmount.Text = Math.Round((50.0 / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();

            //Third Payment Schedule            
            lblThirdPaymentDateTime.Text = "90 Business Days After Deal Ends";            
            
            lblThirdPaymentPercent.Text = "20";
            lblThirdPaymentAmount.Text = Math.Round((20.0 / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();

            //Displaying Business Account represeter detail
            BLLUser objAccRep = new BLLUser();
            objAccRep.email = dtDealDetail.Rows[0]["salePersonAccountName"].ToString().Trim();
            DataTable dtAccRep = objAccRep.getUserDetailByEmail();
            if (dtAccRep != null && dtAccRep.Rows.Count > 0)
            {
                lblAccountRepDetail.Text = "Your Account Manager: " + dtAccRep.Rows[0]["firstName"].ToString().Trim() + " " + dtAccRep.Rows[0]["lastName"].ToString().Trim() + " (<span style='color:Blue;'>" + dtAccRep.Rows[0]["userName"].ToString().Trim() + "</span>)";
            }
           
            if (dtResult != null && dtResult.Rows.Count > 0)
            {              
                lblBusinessAddress.Text = dtResult.Rows[0]["bAddress"].ToString().Trim();
                lblBusinessName.Text = dtResult.Rows[0]["bName"].ToString().Trim();
                lblBusinessPaymentAddress.Text = dtResult.Rows[0]["cAddress"].ToString().Trim();
                lblBusinessPaymentPhone.Text = dtResult.Rows[0]["cPhone"].ToString().Trim();
                lblBusinessPaymentTitle.Text = dtResult.Rows[0]["cName"].ToString().Trim();
                lblBusinessPhone.Text = dtResult.Rows[0]["bPhone"].ToString().Trim();
                lblFirstPaymentPer.Text = dtResult.Rows[0]["firstPer"].ToString().Trim();
                lblSecondPaymentPer.Text = dtResult.Rows[0]["secondPer"].ToString().Trim();
                lblThirdPaymentPercent.Text = dtResult.Rows[0]["thirdPer"].ToString().Trim();
                lblFirstPaymentDateTime.Text = dtResult.Rows[0]["firstPaymentText"].ToString().Trim();
                lblSecondPaymentDateTime.Text = dtResult.Rows[0]["secondPaymentText"].ToString().Trim();
                lblThirdPaymentDateTime.Text = dtResult.Rows[0]["thirdPaymenText"].ToString().Trim();

                lblInvoiceDate.Text = "INVOICE DATE: " + Convert.ToDateTime(dtResult.Rows[0]["createdDate"].ToString().Trim()).ToString("yyyy/MM/dd");

                lblFirstPaymentAmount.Text = Math.Round((Convert.ToDouble(lblFirstPaymentPer.Text) / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();
                lblSecondPaymentAmount.Text = Math.Round((Convert.ToDouble(lblSecondPaymentPer.Text) / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();
                lblThirdPaymentAmount.Text = Math.Round((Convert.ToDouble(lblThirdPaymentPercent.Text) / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();
            }           
        }
        catch (Exception ex)
        { }
    }
}
